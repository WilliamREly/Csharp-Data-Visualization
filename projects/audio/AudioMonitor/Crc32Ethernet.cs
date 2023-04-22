namespace AudioMonitor
{
    public class Crc32Ethernet
    {
        private readonly uint[] _table;
        private bool _swapped;
        
        private Crc32Ethernet(uint polynomial)
        {
            _swapped = false;
            _table = new uint[256];
            
            for (uint i = 0; i < 256; i++)
            {
                var r = i;
                for (var j = 0; j < 8; j++)
                {
                    if (_swapped)
                    {
                        if ((r & 1) != 0)
                            r = (r >> 1) ^ polynomial;
                        else
                            r >>= 1;
                    }
                    else
                    {
                        if ((r & 0x80000000 ) != 0)
                            r = (r << 1) ^ polynomial;
                        else
                            r <<= 1;
                    }
                }
                _table[i] = r;
            }
        }
        private static uint Reflect32( uint x )
        {
            uint bits = 0;
            var mask = x;

            for( var i = 0; i < 4*8; i++ )
            {
                bits <<= 1;
                if( (mask & 1) != 0 )
                    bits |= 1;
                mask >>= 1;
            }

            return bits;
        }
        private void SwapTable()
        {
            for( uint b = 0; b < 256; b++ )
                _table[ b ] = (Reflect32( b ) >> 24) & 0xFF;
        }
        private uint _value = 0xFFFFFFFFU;

        public void Update(byte[] data, uint offset, int size)
        {
            for (uint i = 0; i < size; i++)
                _value = _table[(((byte) (_value)) ^ data[offset + i])] ^ (_value >> 8);
        }

        public uint GetDigest()
        {
            return _value ^ 0xFFFFFFFF;
        }

        public static uint CalculateDigest(byte[] data, uint offset, int size, uint polynomial = 0xEDB88320, bool invertOutput = false)
        {
            var crc = new Crc32Ethernet(polynomial^ 0xFFFFFFFF);
            crc.Update(data, offset, size);
            return invertOutput ? crc.GetDigest() : crc._value;
        }

        private static bool VerifyDigest(uint digest, byte[] data, uint offset, int size)
        {
            return (CalculateDigest(data, offset, size) == digest);
        }
    }

}