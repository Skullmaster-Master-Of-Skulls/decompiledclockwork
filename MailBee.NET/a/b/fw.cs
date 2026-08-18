using System;
using System.IO;
using System.Text;
using a.h;

namespace a.b
{
	// Token: 0x0200025B RID: 603
	internal class fw
	{
		// Token: 0x0600149A RID: 5274 RVA: 0x0005FD20 File Offset: 0x0005ED20
		public static string a(byte[] A_0)
		{
			ii.b(A_0, 0, 4);
			int num = (int)ii.b(A_0, 4, 8);
			int num2 = (int)ii.b(A_0, 8, 12);
			ii.b(A_0, 12, 16);
			if (num2 == 1967544908)
			{
				byte[] array = new byte[num];
				int num3 = 0;
				byte[] array2 = new byte[4096];
				try
				{
					Array.Copy(Encoding.GetEncoding("US-ASCII").GetBytes("{\\rtf1\\ansi\\mac\\deff0\\deftab720{\\fonttbl;}{\\f0\\fnil \\froman \\fswiss \\fmodern \\fscript \\fdecor MS Sans SerifSymbolArialTimes New RomanCourier{\\colortbl\\red0\\green0\\blue0\n\r\\par \\pard\\plain\\f0\\fs20\\b\\i\\u\\tab\\tx"), 0, array2, 0, "{\\rtf1\\ansi\\mac\\deff0\\deftab720{\\fonttbl;}{\\f0\\fnil \\froman \\fswiss \\fmodern \\fscript \\fdecor MS Sans SerifSymbolArialTimes New RomanCourier{\\colortbl\\red0\\green0\\blue0\n\r\\par \\pard\\plain\\f0\\fs20\\b\\i\\u\\tab\\tx".Length);
				}
				catch (IOException)
				{
				}
				int num4 = "{\\rtf1\\ansi\\mac\\deff0\\deftab720{\\fonttbl;}{\\f0\\fnil \\froman \\fswiss \\fmodern \\fscript \\fdecor MS Sans SerifSymbolArialTimes New RomanCourier{\\colortbl\\red0\\green0\\blue0\n\r\\par \\pard\\plain\\f0\\fs20\\b\\i\\u\\tab\\tx".Length;
				int num5 = 16;
				while (num5 < A_0.Length - 2 && num3 < array.Length)
				{
					int num6 = (int)(A_0[num5++] & byte.MaxValue);
					int num7 = 0;
					while (num7 < 8 && num3 < array.Length)
					{
						bool flag = (num6 & 1) == 1;
						num6 >>= 1;
						if (flag)
						{
							int num8 = (int)(A_0[num5++] & byte.MaxValue);
							int num9 = (int)(A_0[num5++] & byte.MaxValue);
							int num10 = num8 << 4 | f.a(num9, 4);
							int num11 = (num9 & 15) + 2;
							try
							{
								int num12 = num10;
								int num13 = 0;
								while (num13 < num11 && num3 < array.Length)
								{
									array[num3++] = array2[num12];
									array2[num4] = array2[num12];
									num4++;
									num4 %= 4096;
									num12++;
									num12 %= 4096;
									num13++;
								}
								goto IL_16E;
							}
							catch (Exception)
							{
								goto IL_16E;
							}
							goto IL_145;
						}
						goto IL_145;
						IL_16E:
						num7++;
						continue;
						IL_145:
						array2[num4] = A_0[num5];
						num4++;
						num4 %= 4096;
						array[num3++] = A_0[num5++];
						goto IL_16E;
					}
				}
				return Encoding.UTF8.GetString(array, 0, array.Length).Trim();
			}
			if (num2 == 1095517517)
			{
				byte[] array3 = new byte[A_0.Length - 16];
				Array.Copy(A_0, 16, array3, 0, A_0.Length - 16);
				return Encoding.UTF8.GetString(array3, 0, array3.Length).Trim();
			}
			return string.Empty;
		}

		// Token: 0x04001044 RID: 4164
		public const string a = "{\\rtf1\\ansi\\mac\\deff0\\deftab720{\\fonttbl;}{\\f0\\fnil \\froman \\fswiss \\fmodern \\fscript \\fdecor MS Sans SerifSymbolArialTimes New RomanCourier{\\colortbl\\red0\\green0\\blue0\n\r\\par \\pard\\plain\\f0\\fs20\\b\\i\\u\\tab\\tx";
	}
}
