namespace XuanLibrary.Fx
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class HybridStreamReader
    {
        private Stream _underlyingStream;
        private BinaryReader _reader;

        public HybridStreamReader(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            _underlyingStream = stream;
            _reader = new BinaryReader(_underlyingStream);
        }

        public StreamSegment ReadSegment()
        {
            int startOffset = (int)_underlyingStream.Position;
            int length = _reader.ReadInt32();
            int next = _reader.ReadInt32();
            StreamSegmentType stype = (StreamSegmentType)_reader.ReadByte();
            return new StreamSegment { StartOffset = startOffset, Length = length, Next = next, SegmentType = stype };
        }

        public object Read()
        {
            var segment = ReadSegment();
            switch (segment.SegmentType)
            {
                case StreamSegmentType.Null:
                    return null;
                case StreamSegmentType.Int:
                    return ReadInt32(segment);
                case StreamSegmentType.String:
                    return ReadString(segment);
                case StreamSegmentType.Binary:
                    return ReadStream(segment);
                case StreamSegmentType.List:
                    return ReadList(segment);
                case StreamSegmentType.Dictionary:
                    return ReadDictionary(segment);
                default:
                    throw new NotImplementedException($"Segment type {segment.SegmentType} isn't implemented!");
            }
        }

        public object ReadNext(StreamSegment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }
            int next = segment.Next;
            _underlyingStream.Seek(next, SeekOrigin.Begin);
            return Read();
        }

        public int ReadInt32(StreamSegment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }
            int contentPosition = segment.ContentStartOffset;
            _underlyingStream.Seek(contentPosition, SeekOrigin.Begin);
            return _reader.ReadInt32();
        }

        public string ReadString(StreamSegment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }
            int contentPosition = segment.ContentStartOffset;
            _underlyingStream.Seek(contentPosition, SeekOrigin.Begin);
            return _reader.ReadString();
        }

        public Stream ReadStream(StreamSegment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }
            int contentPosition = segment.ContentStartOffset;
            _underlyingStream.Seek(contentPosition, SeekOrigin.Begin);
            return new MemoryStream(_reader.ReadBytes(segment.Length - segment.HeaderLength));
        }

        public List<object> ReadList(StreamSegment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }
            int contentPosition = segment.ContentStartOffset;
            int indexLength = (segment.Length - segment.HeaderLength)/4;
            _underlyingStream.Seek(contentPosition, SeekOrigin.Begin);
            int[] indices = new int[indexLength];
            for (int i = 0; i < indexLength; i++)
            {
                indices[i] = _reader.ReadInt32();
            }
            var result = new List<object>();
            foreach (var index in indices)
            {
                _underlyingStream.Seek(index, SeekOrigin.Begin);
                result.Add(Read());
            }
            _underlyingStream.Seek(segment.Next, SeekOrigin.Begin);
            return result;
        }

        public Dictionary<string, object> ReadDictionary(StreamSegment segment)
        {
            if (segment == null)
            {
                throw new ArgumentNullException(nameof(segment));
            }
            int contentPosition = segment.ContentStartOffset;
            int indexLength = (segment.Length - segment.HeaderLength) / 4 / 2;
            _underlyingStream.Seek(contentPosition, SeekOrigin.Begin);
            int[] keyIndices = new int[indexLength];
            int[] valueIndices = new int[indexLength];
            for (int i = 0; i < indexLength; i++)
            {
                keyIndices[i] = _reader.ReadInt32();
                valueIndices[i] = _reader.ReadInt32();
            }
            var result = new Dictionary<string, object>();
            for (int i = 0; i< indexLength; i++)
            {
                _underlyingStream.Seek(keyIndices[i], SeekOrigin.Begin);
                string key = Read() as string;
                _underlyingStream.Seek(valueIndices[i], SeekOrigin.Begin);
                object value = Read();
                result.Add(key, value);
            }
            _underlyingStream.Seek(segment.Next, SeekOrigin.Begin);
            return result;
        }
    }
}
