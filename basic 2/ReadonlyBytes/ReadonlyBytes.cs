using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable<byte>
	{
		private readonly byte[] data;
		private readonly int hashCode;
		public int Length { get { return length; }}

		private int length = 0;

		public ReadonlyBytes(params byte[] source)
		{
			if(source == null)
				throw new ArgumentNullException();

			length = source.Length;
			data = new byte[length];
			source.CopyTo(data, 0);
			hashCode = HashCode();
		}

        public override bool Equals(object obj)
        {
			if (!(obj is ReadonlyBytes) || obj.GetType() != typeof(ReadonlyBytes))
				return false;

			var comparable = obj as ReadonlyBytes;
			if (comparable.Length == length)
			{
				if(comparable == null && data == null)
					return true;

				for (var i = 0; i < length; i++)
				{
					if (comparable[i] != data[i])
						return false;
				}

				return true;
			}
			return false;
        }

        private int HashCode()
        {
			unchecked
			{
				var hashCode = 2166136261;
				for (var i = 0; i < length; i++)
				{
					hashCode ^= data[i];
					hashCode *= 16777619;
                }
				return (int)hashCode;
			}
        }

        public override int GetHashCode()
        {
			return hashCode;
        }

        public override string ToString()
        {
			var outputStrBuilder = new StringBuilder("[");

			for(var i = 0; i < length; i++)
			{
				outputStrBuilder.Append(data[i].ToString());
				if(i != length - 1)
					outputStrBuilder.Append(", ");
			}
			outputStrBuilder.Append("]");

            return outputStrBuilder.ToString();
        }

        public byte this[int index]
		{
			get
			{
				if(index < 0 || index >= length)
					throw new IndexOutOfRangeException();
				return data[index];
			}
		}

		public IEnumerator<byte> GetEnumerator()
        {
            for(var i = 0; i < length; i++)
				yield return data[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
			return GetEnumerator();
        }
    }
}