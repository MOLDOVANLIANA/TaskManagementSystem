namespace TaskManagement.Entities
{
    public enum Status
    {
        Backlog,
        DevInProgress,
        PrApproval,
        PrApproved,
        QaInProgress,
        QaPassed,
        QaFailed,
        Deploying,
        Done
    }
}
