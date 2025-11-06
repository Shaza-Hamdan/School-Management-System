using TRIAL.Persistence.entity;
using TRIAL.Persistence.Repository;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace TRIAL.Services.Implementations
{
    public class StudentHomeworkService : IStudentHomeworkService
    {
        private readonly AppDBContext appdbContext;
        public StudentHomeworkService(AppDBContext appDbContext)
        {
            appdbContext = appDbContext;
        }

        public async Task<string> UploadHomeworkFileAsync(int homeworkTId, int registrationId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded.");

            // Generate unique file path
            var filePath = Path.Combine("uploads", Guid.NewGuid() + Path.GetExtension(file.FileName));

            // Save file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var homeworkStudent = new HomeworkStudent
            {
                HomeworkTId = homeworkTId,
                RegistrationId = registrationId,
                FilePath = filePath,
                Created = DateTime.Now
            };

            appdbContext.HwS.Add(homeworkStudent);
            await appdbContext.SaveChangesAsync();

            return "File uploaded successfully.";
        }
        public async Task<HomeworkStudent> GetStudentHomeworkByIdAsync(int studentId)
        {
            var homework = await appdbContext.HwS
                .Where(hw => hw.RegistrationId == studentId)
                .OrderByDescending(hw => hw.Created) // to get the latest homework first
                .FirstOrDefaultAsync();

            if (homework == null)
                throw new KeyNotFoundException("Homework not found for the specified student.");

            return homework;
        }
        public async Task<string> UpdateHomeworkFileAsync(int homeworkTId, int registrationId, IFormFile newFile)
        {

            if (newFile == null || newFile.Length == 0)
                throw new ArgumentException("No file uploaded.");

            var homeworkStudent = await appdbContext.HwS
                .FirstOrDefaultAsync(hs => hs.HomeworkTId == homeworkTId && hs.RegistrationId == registrationId);

            if (homeworkStudent == null)
                throw new InvalidOperationException("Homework record not found.");

            // Delete the old file
            if (File.Exists(homeworkStudent.FilePath))
            {
                File.Delete(homeworkStudent.FilePath);
            }

            // Save new file to folder
            var newFilePath = Path.Combine("uploads", Guid.NewGuid() + Path.GetExtension(newFile.FileName));
            using (var stream = new FileStream(newFilePath, FileMode.Create))
            {
                await newFile.CopyToAsync(stream);
            }

            homeworkStudent.FilePath = newFilePath;
            homeworkStudent.Created = DateTime.Now;
            await appdbContext.SaveChangesAsync();

            return "Homework updated successfully.";
        }


        public async Task<bool> DeleteHomework(int homeworkTId, int registrationId)
        {
            var homework = await appdbContext.HwS
                .FirstOrDefaultAsync(h => h.HomeworkTId == homeworkTId && h.RegistrationId == registrationId);

            if (homework == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(homework.FilePath) && File.Exists(homework.FilePath))
            {
                File.Delete(homework.FilePath);
            }

            appdbContext.HwS.Remove(homework);
            await appdbContext.SaveChangesAsync();

            return true;
        }



    }
}