using LZW.Dependencies;

namespace Compressor;

internal static class Compressor
{
    public static byte[] Compress(byte[] bytes, int BWTPosition = -1)
    {
        var trie = new Trie();
        var buffer = new ByteBuffer();
        buffer.SetBWTPosition(BWTPosition);
        for (int i = 0; i < bytes.Length; ++i)
        {
            if (trie.Size == buffer.MaxSize)
            {
                buffer.MaxSize *= 2;
                ++buffer.currentBitCount;
            }
            int number = trie.Add(ref i, bytes);
            buffer.AddNumber(number);
        }
        return buffer.ToByteArray();
    }
}
