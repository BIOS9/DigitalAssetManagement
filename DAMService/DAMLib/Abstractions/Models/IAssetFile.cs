using System;

namespace DAMLib.Abstractions.Models
{
    public interface IAssetFile
    {
        public int Id { get; }
        public DateTime DateAdded { get; }
    }
}