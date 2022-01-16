using System;

namespace DAMLib.Abstractions.Models
{
    public interface IAssetRepository
    {
        public int Id { get; }
        public string Name { get; }
        public DateTime DateAdded { get; }
    }
}