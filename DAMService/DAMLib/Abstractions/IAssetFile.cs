using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAMLib.Abstractions
{
    public interface IAssetFile
    {
        public int Id { get; }
        public DateTime DateAdded { get; }

        public Task<IReadOnlyCollection<IMetadataRecord>> GetAllMetadataRecordsAsync();
    }
}