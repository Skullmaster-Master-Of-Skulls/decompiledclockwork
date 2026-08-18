using System;
using System.Collections;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x02000317 RID: 791
	internal class fg
	{
		// Token: 0x06001C37 RID: 7223 RVA: 0x0007BDBC File Offset: 0x0007ADBC
		public static byte[] b(string A_0)
		{
			FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read);
			byte[] result;
			try
			{
				result = fg.a(fileStream, -1);
			}
			finally
			{
				fileStream.Close();
			}
			return result;
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x0007BDF4 File Offset: 0x0007ADF4
		public static byte[] a(Stream A_0, string A_1)
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = false;
				int num = A_0.ReadByte();
				while (num != -1)
				{
					if (num <= 13)
					{
						if (num != 10 && num != 13)
						{
							goto IL_5F;
						}
						flag = false;
						stringBuilder = new StringBuilder();
					}
					else if (num != 91)
					{
						if (num != 93)
						{
							goto IL_5F;
						}
						flag = false;
						if (stringBuilder.ToString().Equals(A_1))
						{
							return fg.a(A_0, 91);
						}
						stringBuilder = new StringBuilder();
					}
					else
					{
						flag = true;
					}
					IL_6B:
					num = A_0.ReadByte();
					continue;
					IL_5F:
					if (flag)
					{
						stringBuilder.Append((char)num);
						goto IL_6B;
					}
					goto IL_6B;
				}
			}
			finally
			{
				A_0.Close();
			}
			throw new IOException("Section '" + A_1 + "' not found");
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x0007BEA8 File Offset: 0x0007AEA8
		public static byte[] a(string A_0, string A_1)
		{
			byte[] result;
			using (FileStream fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read))
			{
				result = fg.a(fileStream, A_1);
			}
			return result;
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x0007BEE4 File Offset: 0x0007AEE4
		public static byte[] a(Stream A_0, int A_1)
		{
			int num = 0;
			byte b = 0;
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			while (!flag)
			{
				int num2 = A_0.ReadByte();
				char c = 'a';
				if (num2 == A_1)
				{
					break;
				}
				if (num2 <= 35)
				{
					if (num2 != -1)
					{
						if (num2 == 35)
						{
							fg.a(A_0);
						}
					}
					else
					{
						flag = true;
					}
				}
				else
				{
					switch (num2)
					{
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
					case 57:
						b = (byte)(b << 4);
						b += (byte)(num2 - 48);
						num++;
						if (num == 2)
						{
							arrayList.Add(b);
							num = 0;
							b = 0;
						}
						break;
					case 58:
					case 59:
					case 60:
					case 61:
					case 62:
					case 63:
					case 64:
						break;
					case 65:
					case 66:
					case 67:
					case 68:
					case 69:
					case 70:
						c = 'A';
						b = (byte)(b << 4);
						b += (byte)(num2 + 10 - (int)c);
						num++;
						if (num == 2)
						{
							arrayList.Add(b);
							num = 0;
							b = 0;
						}
						break;
					default:
						switch (num2)
						{
						case 97:
						case 98:
						case 99:
						case 100:
						case 101:
						case 102:
							b = (byte)(b << 4);
							b += (byte)(num2 + 10 - (int)c);
							num++;
							if (num == 2)
							{
								arrayList.Add(b);
								num = 0;
								b = 0;
							}
							break;
						}
						break;
					}
				}
			}
			return (byte[])arrayList.ToArray(typeof(byte));
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x0007C070 File Offset: 0x0007B070
		public static byte[] a(string A_0)
		{
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(A_0)))
			{
				result = fg.a(memoryStream, -1);
			}
			return result;
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x0007C0B4 File Offset: 0x0007B0B4
		private static void a(Stream A_0)
		{
			int num = A_0.ReadByte();
			while (num != -1 && num != 10 && num != 13)
			{
				num = A_0.ReadByte();
			}
		}
	}
}
