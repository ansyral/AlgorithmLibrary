namespace XuanLibrary.Fx
{
    public class StreamSegment
    {
        /// <summary>
        /// for list and dictionary, the length is header length + index length
        /// </summary>
        public int Length { get; set; }

        public int Next { get; set; }

        public StreamSegmentType SegmentType { get; set; }

        public int StartOffset { get; set; }

        public int HeaderLength { get { return 4 + 4 + 1; } }

        public int ContentStartOffset { get { return StartOffset + HeaderLength; } }
    }

    public enum StreamSegmentType
    {
        Null,
        Dictionary,
        List,
        String,
        Int,
        Binary,
    }
}
