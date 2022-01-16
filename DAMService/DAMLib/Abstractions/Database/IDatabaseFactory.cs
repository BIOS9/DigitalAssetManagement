namespace DAMLib.Abstractions.Database
{
    public interface IDatabaseFactory
    {
        public IRepositoryDatabase GetRepositoryDatabase();
        public IAssetDatabase GetAssetDatabase(int repositoryId);
        public IAssetFileDatabase GetAssetFileDatabase(int repositoryId, int assetId);
    }
}