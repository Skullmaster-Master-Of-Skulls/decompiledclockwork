using System;
using System.IO;
using System.Text;

namespace Oracle.SqlAndPlsqlParser
{
	// Token: 0x0200028D RID: 653
	internal static class SupportClass
	{
		// Token: 0x06001960 RID: 6496 RVA: 0x00109160 File Offset: 0x00107360
		public static byte[] ToByteArray(sbyte[] sbyteArray)
		{
			byte[] array = null;
			if (sbyteArray != null)
			{
				array = new byte[sbyteArray.Length];
				for (int i = 0; i < sbyteArray.Length; i++)
				{
					array[i] = (byte)sbyteArray[i];
				}
			}
			return array;
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x00109194 File Offset: 0x00107394
		public static byte[] ToByteArray(string sourceString)
		{
			return Encoding.UTF8.GetBytes(sourceString);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x001091A4 File Offset: 0x001073A4
		public static byte[] ToByteArray(object[] tempObjectArray)
		{
			byte[] array = null;
			if (tempObjectArray != null)
			{
				array = new byte[tempObjectArray.Length];
				for (int i = 0; i < tempObjectArray.Length; i++)
				{
					array[i] = (byte)tempObjectArray[i];
				}
			}
			return array;
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x001091DC File Offset: 0x001073DC
		public static char[] ToCharArray(sbyte[] sByteArray)
		{
			return Encoding.UTF8.GetChars(SupportClass.ToByteArray(sByteArray));
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x001091F0 File Offset: 0x001073F0
		public static char[] ToCharArray(byte[] byteArray)
		{
			return Encoding.UTF8.GetChars(byteArray);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x00109200 File Offset: 0x00107400
		public static int ReadInput(Stream sourceStream, sbyte[] target, int start, int count)
		{
			if (target.Length == 0)
			{
				return 0;
			}
			byte[] array = new byte[target.Length];
			int num = sourceStream.Read(array, start, count);
			if (num == 0)
			{
				return -1;
			}
			for (int i = start; i < start + num; i++)
			{
				target[i] = (sbyte)array[i];
			}
			return num;
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x00109244 File Offset: 0x00107444
		public static int ReadInput(TextReader sourceTextReader, sbyte[] target, int start, int count)
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
				target[i] = (sbyte)array[i];
			}
			return num;
		}
	}
}
