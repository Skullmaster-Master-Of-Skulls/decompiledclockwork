using System;
using System.IO;
using System.Text;

namespace Ionic.Zlib
{
	// Token: 0x02000016 RID: 22
	internal class SharedUtils
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x0000973A File Offset: 0x0000793A
		public static int URShift(int number, int bits)
		{
			return (int)((uint)number >> bits);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00009744 File Offset: 0x00007944
		public static int ReadInput(TextReader sourceTextReader, byte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			char[] array = new char[target.Length];
			int num = sourceTextReader.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (byte)array[i];
			}
			return num;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00009785 File Offset: 0x00007985
		internal static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00009792 File Offset: 0x00007992
		internal static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}
	}
}
