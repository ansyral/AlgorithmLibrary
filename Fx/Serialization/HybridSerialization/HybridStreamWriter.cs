namespace XuanLibrary.Fx
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The design for the writer is for user to control which part of an object to binary serialized and which part to json/xml serialized at user's ease.
    /// Only support int(bool, byte, long, char, float, double, byte[] could be supported in a similar way), string, and list/ dict of above types
    /// </summary>
    public class HybridStreamWriter
    {
        private Stream _underlyingStream;
        private BinaryWriter _writer;

        public HybridStreamWriter(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            _underlyingStream = stream;
            _writer = new BinaryWriter(_underlyingStream);
        }

        public StreamSegment Write(object obj)
        {
            if (obj == null)
            {
                return WriteNull();
            }
            if (obj is string)
            {
                return Write(obj as string);
            }
            if (obj is int)
            {
                return Write((int)obj);
            }
            if (obj is IReadOnlyCollection<object>)
            {
                return Write(obj as IReadOnlyCollection<object>);
            }
            if (obj is IReadOnlyCollection<KeyValuePair<string, object>>)
            {
                return Write(obj as IReadOnlyCollection<KeyValuePair<string, object>>);
            }
            throw new NotImplementedException($"{obj.GetType()} isn't implemented!");
        }

        public StreamSegment WriteNull()
        {
            int start = (int)_underlyingStream.Position;
            int length = 4 + 4 + 1;
            int next = start + length;
            _writer.Write(length);
            _writer.Write(next);
            _writer.Write((byte)StreamSegmentType.Null);
            return new StreamSegment { Length = length, StartOffset = start, Next = next, SegmentType = StreamSegmentType.Null };
        }

        public StreamSegment Write(Action<Stream> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }
            int start = (int)_underlyingStream.Position;
            _underlyingStream.Seek(4 + 4 + 1, SeekOrigin.Current);
            action(_underlyingStream);
            int next = (int)_underlyingStream.Position;
            int length = next - start;
            _underlyingStream.Seek(start, SeekOrigin.Begin);
            _writer.Write(length);
            _writer.Write(next);
            _writer.Write((byte)StreamSegmentType.Binary);
            _underlyingStream.Seek(next, SeekOrigin.Begin);
            return new StreamSegment { Length = length, StartOffset = start, Next = next, SegmentType = StreamSegmentType.Binary };
        }

        #region Primitive type
        public StreamSegment Write(int value)
        {
            int start = (int)_underlyingStream.Position;
            int length = 4 + 4 + 1 + 4;
            int next = start + length;
            _writer.Write(length);
            _writer.Write(next);
            _writer.Write((byte)StreamSegmentType.Int);
            _writer.Write(value);
            return new StreamSegment { Length = length, StartOffset = start, Next = next, SegmentType = StreamSegmentType.Int };
        }
        #endregion

        public StreamSegment Write(string value)
        {
            if (value == null)
            {
                return WriteNull();
            }
            int start = (int)_underlyingStream.Position;
            _underlyingStream.Seek(4 + 4 + 1, SeekOrigin.Current);
            _writer.Write(value);
            int next = (int)_underlyingStream.Position;
            int length = next - start;
            _underlyingStream.Seek(start, SeekOrigin.Begin);
            _writer.Write(length);
            _writer.Write(next);
            _writer.Write((byte)StreamSegmentType.String);
            _underlyingStream.Seek(next, SeekOrigin.Begin);
            return new StreamSegment { Length = length, StartOffset = start, Next = next, SegmentType = StreamSegmentType.String };
        }

        public StreamSegment Write(IReadOnlyCollection<object> value)
        {
            if (value == null)
            {
                return WriteNull();
            }
            int start = (int)_underlyingStream.Position;
            _underlyingStream.Seek(4 + 4 + 1 + value.Count * 4, SeekOrigin.Current);
            int[] indices = new int[value.Count];
            int i = 0;
            foreach (var v in value)
            {
                indices[i++] = Write(v).StartOffset;
            }
            int next = (int)_underlyingStream.Position;
            int length = 4 + 4 + 1 + value.Count * 4;
            _underlyingStream.Seek(start, SeekOrigin.Begin);
            _writer.Write(length);
            _writer.Write(next);
            _writer.Write((byte)StreamSegmentType.List);
            foreach (var index in indices)
            {
                _writer.Write(index);
            }
            _underlyingStream.Seek(next, SeekOrigin.Begin);
            return new StreamSegment { Length = length, StartOffset = start, Next = next, SegmentType = StreamSegmentType.List };
        }

        public StreamSegment Write(IReadOnlyCollection<KeyValuePair<string, object>> value)
        {
            if (value == null)
            {
                return WriteNull();
            }
            int start = (int)_underlyingStream.Position;
            _underlyingStream.Seek(4 + 4 + 1 + value.Count * 2 * 4, SeekOrigin.Current);
            int[] keyIndices = new int[value.Count];
            int[] valueIndices = new int[value.Count];
            int i = 0;
            foreach (var v in value)
            {
                keyIndices[i] = Write(v.Key).StartOffset;
                valueIndices[i++] = Write(v.Value).StartOffset;
            }
            int next = (int)_underlyingStream.Position;
            int length = 4 + 4 + 1 + value.Count * 2 * 4;
            _underlyingStream.Seek(start, SeekOrigin.Begin);
            _writer.Write(length);
            _writer.Write(next);
            _writer.Write((byte)StreamSegmentType.Dictionary);
            for (int j = 0; j < i; j++)
            {
                _writer.Write(keyIndices[j]);
                _writer.Write(valueIndices[j]);
            }
            _underlyingStream.Seek(next, SeekOrigin.Begin);
            return new StreamSegment { Length = length, StartOffset = start, Next = next, SegmentType = StreamSegmentType.Dictionary };
        }
    }
}
