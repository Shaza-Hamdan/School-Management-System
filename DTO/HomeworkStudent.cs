namespace Trial.DTO
{
    public record AddStudentHomeworkDTO(
        int RegistrationId,
        int homeworkTId,
        string filePath);

    public record StudentHomeworkDTO(
        int Id,
        int RegistrationId,
        int HomeworkTId,
        string filePath);

    public record ModifyStudentHomeworkDTO(
        int Id,
        string filePath);

    public record DeleteHomework(
        int homeworkTId,
        int RegistrationId
    );

}