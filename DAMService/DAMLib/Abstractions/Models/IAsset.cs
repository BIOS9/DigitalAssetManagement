using System;

namespace DAMLib.Abstractions.Models
{
    public interface IAsset
    {
        public int Id { get; }
        public DateTime DateAdded { get; }
    }
}