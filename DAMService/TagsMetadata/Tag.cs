using System;

namespace TagsMetadata
{
    public class Tag
    {
        public readonly string Name;

        public Tag(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}