namespace Trial.DTO
{
    public record HomeworkTDTO(
        int Id,
        string Homework,
        string Discription,
        DateTime Deadline);

    public record AddHomeworkTDTO(
        string Homework,
        string Discription,
        DateTime Deadline,
        int subjectsId
    );
    public record ModifyHomeworkTDTO(
        int Id,
        string Homework,
        string Discription,
        DateTime Deadline,
        int subjectsId
        );


}

