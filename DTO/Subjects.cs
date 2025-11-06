using System.Collections.Generic;

namespace Trial.DTO
{
    public record AddNewSubject(
    string SubName,
    string Discription,
    int RegistrationId
);

    public record AddNewSubjectDTO(
        int Id,
        string SubName,
        string Discription,
        int RegistrationId
    );

    public record UpdateSubject(
        int Id,
        string SubName,
        string Discription,
         int RegistrationId
    );
    public record SubjectRetrieving(

        String SubName,
        String Discription,
        int RegistrationId

    );
    public record MarkDTO(
        int Id,
        int Oral,
        int Written);

    public record HomeworkDTO(
        int Id,
        string Homework,
        DateTime Created,
        string Discription);

    public record SubjectDetails(
    int Id,
    string Name,
    string Discription,
    int RegistrationId);

}