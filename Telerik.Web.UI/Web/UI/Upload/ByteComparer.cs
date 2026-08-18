using System;

namespace Telerik.Web.UI.Upload
{
	// Token: 0x02001B76 RID: 7030
	internal class ByteComparer
	{
		// Token: 0x06011087 RID: 69767 RVA: 0x003C27F0 File Offset: 0x003C09F0
		public static int IndexOf(byte[] pattern, byte[] buffer, int start)
		{
			int num = 0;
			int num2 = Array.IndexOf<byte>(buffer, pattern[0], start);
			if (num2 != -1)
			{
				while (num2 + num < buffer.Length)
				{
					if (buffer[num2 + num] == pattern[num])
					{
						num++;
						if (num == pattern.Length)
						{
							break;
						}
					}
					else
					{
						num2 = Array.IndexOf<byte>(buffer, pattern[0], num2 + num);
						if (num2 == -1)
						{
							break;
						}
						num = 0;
					}
				}
			}
			if (num == pattern.Length)
			{
				return num2;
			}
			return -1;
		}

		// Token: 0x06011088 RID: 69768 RVA: 0x003C284C File Offset: 0x003C0A4C
		public static int IndexOf(byte[] pattern, byte[] buffer1, byte[] buffer2, int start)
		{
			int num = 0;
			int num2 = ByteComparer.IndexOf(pattern[0], buffer1, buffer2, start);
			int num3 = buffer1.Length + buffer2.Length;
			if (num2 != -1)
			{
				while (num2 + num < num3)
				{
					if (ByteComparer.GetByteAtPosition(num2 + num, buffer1, buffer2) == pattern[num])
					{
						num++;
						if (num == pattern.Length)
						{
							break;
						}
					}
					else
					{
						num2 = ByteComparer.IndexOf(pattern[0], buffer1, buffer2, num2 + num);
						if (num2 == -1)
						{
							break;
						}
						num = 0;
					}
				}
			}
			if (num == pattern.Length)
			{
				return num2;
			}
			return -1;
		}

		// Token: 0x06011089 RID: 69769 RVA: 0x003C28B4 File Offset: 0x003C0AB4
		private static int IndexOf(byte pattern, byte[] buffer1, byte[] buffer2, int start)
		{
			if (start >= buffer1.Length)
			{
				int num = Array.IndexOf<byte>(buffer2, pattern, start - buffer1.Length);
				if (num < 0)
				{
					return num;
				}
				return buffer1.Length + num;
			}
			else
			{
				int num2 = Array.IndexOf<byte>(buffer1, pattern, start);
				if (num2 >= 0)
				{
					return num2;
				}
				int num3 = Array.IndexOf<byte>(buffer2, pattern);
				if (num3 < 0)
				{
					return num3;
				}
				return buffer1.Length + num3;
			}
		}

		// Token: 0x0601108A RID: 69770 RVA: 0x003C2902 File Offset: 0x003C0B02
		private static byte GetByteAtPosition(int position, byte[] buffer1, byte[] buffer2)
		{
			if (position < buffer1.Length)
			{
				return buffer1[position];
			}
			return buffer2[position - buffer1.Length];
		}
	}
}
