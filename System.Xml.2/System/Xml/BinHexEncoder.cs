using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000067 RID: 103
	internal static class BinHexEncoder
	{
		// Token: 0x06000393 RID: 915 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		internal static void Encode(byte[] buffer, int index, int count, XmlWriter writer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			char[] array = new char[(count * 2 < 128) ? (count * 2) : 128];
			int num = index + count;
			while (index < num)
			{
				int num2 = (count < 64) ? count : 64;
				int count2 = BinHexEncoder.Encode(buffer, index, num2, array);
				writer.WriteRaw(array, 0, count2);
				index += num2;
				count -= num2;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000E568 File Offset: 0x0000C768
		internal static string Encode(byte[] inArray, int offsetIn, int count)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (0 > offsetIn)
			{
				throw new ArgumentOutOfRangeException("offsetIn");
			}
			if (0 > count)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > inArray.Length - offsetIn)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			char[] array = new char[2 * count];
			int length = BinHexEncoder.Encode(inArray, offsetIn, count, array);
			return new string(array, 0, length);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		private static int Encode(byte[] inArray, int offsetIn, int count, char[] outArray)
		{
			int num = 0;
			int num2 = 0;
			int num3 = outArray.Length;
			for (int i = 0; i < count; i++)
			{
				byte b = inArray[offsetIn++];
				outArray[num++] = "0123456789ABCDEF"[b >> 4];
				if (num == num3)
				{
					break;
				}
				outArray[num++] = "0123456789ABCDEF"[(int)(b & 15)];
				if (num == num3)
				{
					break;
				}
			}
			return num - num2;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000E634 File Offset: 0x0000C834
		internal static Task EncodeAsync(byte[] buffer, int index, int count, XmlWriter writer)
		{
			BinHexEncoder.<EncodeAsync>d__5 <EncodeAsync>d__;
			<EncodeAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<EncodeAsync>d__.buffer = buffer;
			<EncodeAsync>d__.index = index;
			<EncodeAsync>d__.count = count;
			<EncodeAsync>d__.writer = writer;
			<EncodeAsync>d__.<>1__state = -1;
			<EncodeAsync>d__.<>t__builder.Start<BinHexEncoder.<EncodeAsync>d__5>(ref <EncodeAsync>d__);
			return <EncodeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040001A7 RID: 423
		private const string s_hexDigits = "0123456789ABCDEF";

		// Token: 0x040001A8 RID: 424
		private const int CharsChunkSize = 128;
	}
}
