namespace Trial.DTO
{
    public record AddMark(
        int RegistrationId,
        int subjectsId,
        int oral,
        int written
    );

    public record GetMark(
        int RegistrationId,
        int subjectsId
    );

    public record MarksResults(
        int RegistrationId,
        int subjectsId,
        int Oral,
        int Written
    );


}