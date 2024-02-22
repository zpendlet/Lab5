namespace QueryBuilderProject
{
    /// <summary>
    /// This enforces objects read in by QueryBuilder to have an ID, which reflects in the database as a PKey
    /// </summary>
    public interface IClassModel
    {
        public int Id { get; set; }
    }
}
