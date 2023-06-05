using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
	public class ReadonlyBytes : IEnumerable<byte>
	{
		private readonly byte[] _data;
        private readonly int _hash;
        const int Offset = 2166136;
        public int Length { get { return _data.Length; } }
        public ReadonlyBytes(params byte[] data)
		{   
            if (data == null) throw new ArgumentNullException();
            _data = data;
            _hash = CreateHashCode();
		}
        
        public byte this[int index] 
		{ 
			get 
			{
                if (index < 0 || index >= _data.Length) throw new IndexOutOfRangeException();
                return _data[index]; 
			}
		}

        public override bool Equals(object obj)
        {
			if(obj is not ReadonlyBytes || obj.GetType() != this.GetType() ) return false;
            return this.Equals(obj as ReadonlyBytes);
        }

        private bool Equals(ReadonlyBytes someByte)
        {
            if (someByte.Length != _data.Length) return false;
            for (int i = 0; i < _data.Length; i++)
                if (someByte[i] != _data[i]) return false;
            return true;
        }

        public override int GetHashCode() => _hash;
        public int CreateHashCode()
        {
            unchecked
            {
                int hash = Offset;
                int multiplier = 16777619;
                for (int i = 0; i < _data.Length; i++)
                {
                    hash ^= _data[i].GetHashCode();
                    hash *= multiplier;
                }
                return hash;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public IEnumerator<byte> GetEnumerator()
        {
            for (int i = 0; i < this.Length; i++)
                yield return _data[i];
        }

        public override string ToString()
        {
            string str = "[";
            for (int i = 0; i < this.Length; i++)
            {
                str += this[i].ToString();
                if (i == this.Length - 1) continue; str += ", ";
            }
            str += "]";
            return str;
        }
    }
}