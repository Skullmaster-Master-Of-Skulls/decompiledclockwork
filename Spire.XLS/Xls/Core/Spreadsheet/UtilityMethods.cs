using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000605 RID: 1541
	public sealed class UtilityMethods
	{
		// Token: 0x06005B38 RID: 23352 RVA: 0x0038E0F4 File Offset: 0x0038D0F4
		private UtilityMethods()
		{
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x0038E108 File Offset: 0x0038D108
		internal static bool ᜀ(Rectangle A_0, Rectangle A_1)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_C5;
				case 2:
					num = 5;
					continue;
				case 3:
					num = 4;
					continue;
				case 4:
					if (A_0.Y <= A_1.Y + A_1.Height)
					{
						num = 1;
						continue;
					}
					return false;
				case 5:
					if (A_1.X <= A_0.X + A_0.Width)
					{
						num = 3;
						continue;
					}
					return false;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (A_0.X > A_1.X + A_1.Width)
					{
						return false;
					}
					num = 2;
					break;
				}
			}
			IL_C5:
			return A_1.Y <= A_0.Y + A_0.Height;
		}

		// Token: 0x06005B3A RID: 23354 RVA: 0x0038E210 File Offset: 0x0038D210
		internal static bool ᜀ(Rectangle A_0, int A_1, int A_2)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					num = 2;
					continue;
				case 2:
					if (A_0.Y <= A_2)
					{
						num = 4;
						continue;
					}
					return false;
				case 4:
					goto IL_9D;
				case 5:
					if (A_1 <= A_0.X + A_0.Width)
					{
						num = 1;
						continue;
					}
					return false;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (A_0.X > A_1)
					{
						return false;
					}
					num = 0;
					break;
				}
			}
			IL_9D:
			return A_2 <= A_0.Y + A_0.Height;
		}

		// Token: 0x06005B3B RID: 23355 RVA: 0x0038E2E8 File Offset: 0x0038D2E8
		internal static int ᜀ(TBIFFRecord[] A_0, TBIFFRecord A_1)
		{
			int a_ = 17;
			int num = 5;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_A9;
				case 1:
					return -1;
				case 2:
					goto IL_B4;
				case 3:
					return num2;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
						goto IL_5D;
					}
					break;
				case 6:
					if (A_0[num2] == A_1)
					{
						num = 3;
						continue;
					}
					num2++;
					num = 7;
					continue;
				case 7:
					goto IL_A9;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				if (true)
				{
				}
				num2 = 0;
				int num3 = A_0.Length;
				num = 0;
				continue;
				IL_B4:
				if (num2 >= num3)
				{
					num = 1;
					continue;
				}
				num = 6;
				continue;
				IL_A9:
				num = 2;
			}
			IL_5D:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("♆㭈㥊ⱌ㙎", a_));
		}

		// Token: 0x06005B3C RID: 23356 RVA: 0x0038E3D8 File Offset: 0x0038D3D8
		internal static int ᜀ(int[] A_0, int A_1)
		{
			int a_ = 9;
			int num = 7;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					return -1;
				case 1:
					goto IL_B4;
				case 2:
					goto IL_A9;
				case 3:
					if (A_0[num2] == A_1)
					{
						num = 4;
						continue;
					}
					num2++;
					num = 6;
					continue;
				case 4:
					return num2;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
						goto IL_5D;
					}
					break;
				case 6:
					goto IL_A9;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				if (true)
				{
				}
				num2 = 0;
				int num3 = A_0.Length;
				num = 2;
				continue;
				IL_B4:
				if (num2 >= num3)
				{
					num = 0;
					continue;
				}
				num = 3;
				continue;
				IL_A9:
				num = 1;
			}
			IL_5D:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("帾㍀ㅂ⑄㹆", a_));
		}

		// Token: 0x06005B3D RID: 23357 RVA: 0x0038E4C8 File Offset: 0x0038D4C8
		internal static int ᜀ(short[] A_0, short A_1)
		{
			int a_ = 8;
			int num = 1;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					return num2;
				case 2:
					goto IL_A9;
				case 3:
					goto IL_BC;
				case 4:
					if (A_0[num2] == A_1)
					{
						num = 0;
						continue;
					}
					num2++;
					num = 2;
					continue;
				case 5:
					goto IL_A9;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BC;
					default:
						goto IL_5D;
					}
					break;
				case 7:
					return -1;
				}
				if (A_0 == null)
				{
					num = 6;
					continue;
				}
				num2 = 0;
				int num3 = A_0.Length;
				num = 5;
				continue;
				IL_BC:
				if (num2 >= num3)
				{
					num = 7;
					continue;
				}
				num = 4;
				continue;
				IL_A9:
				if (true)
				{
				}
				num = 3;
			}
			IL_5D:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("弽㈿ぁ╃㽅", a_));
		}

		// Token: 0x06005B3E RID: 23358 RVA: 0x0038E5B8 File Offset: 0x0038D5B8
		internal static double ᜀ(DateTime A_0)
		{
			double num;
			for (;;)
			{
				num = A_0.ToOADate();
				if (true)
				{
				}
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return num;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (num < 61.0)
							{
								num2 = 2;
								continue;
							}
							return num;
						}
						break;
					case 2:
						num -= 1.0;
						num2 = 0;
						continue;
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x06005B3F RID: 23359 RVA: 0x0038E644 File Offset: 0x0038D644
		internal static DateTime ᜀ(double A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_75;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						A_0 += 1.0;
						num = 0;
						continue;
					}
					break;
				}
				IL_26:
				if (true)
				{
				}
				if (A_0 < 61.0)
				{
					num = 1;
					continue;
				}
				break;
				goto IL_26;
			}
			IL_75:
			return DateTime.FromOADate(A_0);
		}

		// Token: 0x06005B40 RID: 23360 RVA: 0x0038E6D0 File Offset: 0x0038D6D0
		[CLSCompliant(false)]
		internal static spr\u23A5 ᜀ(int A_0, int A_1, TBIFFRecord A_2)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			spr\u23A5 spr_u23A = (spr\u23A5)spr\u175E.ᜀ(A_2);
			spr_u23A.ᜃ(A_0);
			spr_u23A.ᜄ(A_1);
			return spr_u23A;
		}

		// Token: 0x06005B41 RID: 23361 RVA: 0x0038E728 File Offset: 0x0038D728
		internal static string ᜀ(string A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return A_0.Substring(1, A_0.Length - 1);
		}

		// Token: 0x06005B42 RID: 23362 RVA: 0x0038E774 File Offset: 0x0038D774
		internal static string ᜀ(string A_0, List<string> A_1)
		{
			int a_ = 12;
			switch (0)
			{
			default:
			{
				int num = 10;
				StringBuilder stringBuilder;
				for (;;)
				{
					int num2;
					int count;
					int num3;
					string text;
					int num4;
					switch (num)
					{
					case 0:
						goto IL_23A;
					case 1:
						num2 += (count - 1) * A_0.Length;
						num = 13;
						continue;
					case 2:
						goto IL_23A;
					case 3:
						goto IL_2A2;
					case 4:
						if (num3 >= count)
						{
							num = 1;
							continue;
						}
						text = A_1[num3];
						num = 12;
						continue;
					case 5:
						goto IL_284;
					case 6:
						goto IL_32D;
					case 7:
						goto IL_20B;
					case 8:
						if (A_1 == null)
						{
							num = 3;
							continue;
						}
						goto IL_D6;
					case 9:
						goto IL_235;
					case 11:
						if (text == null)
						{
							num = 16;
							continue;
						}
						goto IL_16A;
					case 12:
						if (text != null)
						{
							num = 27;
							continue;
						}
						goto IL_2FA;
					case 13:
						if (true)
						{
						}
						if (num2 >= 0)
						{
							num = 23;
							continue;
						}
						goto IL_D0;
					case 14:
						goto IL_2FA;
					case 15:
						stringBuilder.Append(A_1[0]);
						num = 24;
						continue;
					case 16:
						text = string.Empty;
						num = 22;
						continue;
					case 17:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D6;
						default:
							goto IL_1C1;
						}
						break;
					case 18:
						if (A_1 != null)
						{
							num = 15;
							continue;
						}
						goto IL_2E0;
					case 19:
						if (num2 + 1 < 0)
						{
							num = 17;
							continue;
						}
						num = 26;
						continue;
					case 20:
						goto IL_20B;
					case 21:
						A_0 = string.Empty;
						num = 5;
						continue;
					case 22:
						goto IL_16A;
					case 23:
						num = 19;
						continue;
					case 24:
						goto IL_2E0;
					case 25:
						if (num4 >= count)
						{
							num = 9;
							continue;
						}
						stringBuilder.Append(A_0);
						text = A_1[num4];
						num = 11;
						continue;
					case 26:
						if (num2 == 0)
						{
							num = 6;
							continue;
						}
						stringBuilder = new StringBuilder();
						text = A_1[0];
						num = 18;
						continue;
					case 27:
						num2 += text.Length;
						num = 14;
						continue;
					}
					if (A_0 == null)
					{
						num = 21;
						continue;
					}
					goto IL_284;
					IL_D6:
					num2 = 0;
					count = A_1.Count;
					num3 = 0;
					num = 0;
					continue;
					IL_16A:
					stringBuilder.Append(text);
					num4++;
					num = 20;
					continue;
					IL_20B:
					num = 25;
					continue;
					IL_23A:
					num = 4;
					continue;
					IL_284:
					num = 8;
					continue;
					IL_2E0:
					num4 = 1;
					num = 7;
					continue;
					IL_2FA:
					num3++;
					num = 2;
				}
				IL_D0:
				throw new OutOfMemoryException();
				IL_1C1:
				if (false)
				{
				}
				goto IL_D0;
				IL_235:
				return stringBuilder.ToString();
				IL_2A2:
				throw new ArgumentNullException(RecordTableEnumerator.b("㑁╃⩅㵇⽉", a_));
				IL_32D:
				return string.Empty;
			}
			}
		}

		// Token: 0x06005B43 RID: 23363 RVA: 0x0038EAB8 File Offset: 0x0038DAB8
		internal static void ᜀ(out int A_0, out int A_1, ExcelVersion A_2)
		{
			int a_ = 0;
			for (;;)
			{
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_7B;
					case 1:
						num = 0;
						continue;
					case 2:
						switch (A_2)
						{
						case ExcelVersion.Version97to2003:
							goto IL_55;
						case ExcelVersion.Version2007:
						case ExcelVersion.Version2010:
							goto IL_64;
						default:
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			for (;;)
			{
				IL_7B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_93;
				}
			}
			IL_93:
			if (false)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("挵嘷儹刻儽㜿ⱁ摃ぅⵇ㡉㽋❍㽏㱑", a_));
			IL_55:
			A_0 = 65536;
			A_1 = 256;
			return;
			IL_64:
			A_0 = 1048576;
			A_1 = 16384;
		}

		// Token: 0x06005B44 RID: 23364 RVA: 0x0038EB74 File Offset: 0x0038DB74
		internal static void ᜀ(Stream A_0, Stream A_1)
		{
			int a_ = 3;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_F5;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CF;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					goto IL_A8;
				case 4:
					goto IL_A8;
				case 5:
				{
					byte[] buffer;
					int count;
					if ((count = A_0.Read(buffer, 0, 32768)) <= 0)
					{
						goto IL_CF;
					}
					A_1.Write(buffer, 0, count);
					num = 4;
					continue;
				}
				case 6:
				{
					if (A_1 == null)
					{
						num = 1;
						continue;
					}
					byte[] buffer = new byte[32768];
					num = 3;
					continue;
				}
				case 7:
					goto IL_63;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num = 6;
				continue;
				IL_A8:
				num = 5;
				continue;
				IL_CF:
				num = 0;
			}
			IL_63:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸吺䠼䴾≀♂", a_));
			IL_F5:
			throw new ArgumentNullException(RecordTableEnumerator.b("崸帺丼䬾⡀ⵂ⑄㍆⁈⑊⍌", a_));
		}

		// Token: 0x06005B45 RID: 23365 RVA: 0x0038EC90 File Offset: 0x0038DC90
		internal static MemoryStream ᜀ(MemoryStream A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			MemoryStream memoryStream = new MemoryStream((int)A_0.Length);
			long position = A_0.Position;
			A_0.Position = 0L;
			UtilityMethods.ᜀ(A_0, memoryStream);
			memoryStream.Position = (A_0.Position = position);
			return memoryStream;
		}

		// Token: 0x06005B46 RID: 23366 RVA: 0x0038ED00 File Offset: 0x0038DD00
		internal static XmlReader ᜀ(Stream A_0, bool A_1)
		{
			int num;
			XmlReader xmlReader;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				for (;;)
				{
					IL_28:
					switch (num)
					{
					case 0:
						if (A_1)
						{
							num = 3;
							continue;
						}
						return xmlReader;
					case 1:
						return xmlReader;
					case 2:
						if (xmlReader.NodeType == XmlNodeType.Element)
						{
							num = 1;
							continue;
						}
						xmlReader.Read();
						num = 4;
						continue;
					case 3:
						num = 5;
						continue;
					case 4:
						goto IL_6A;
					case 5:
						goto IL_6A;
					}
					goto IL_46;
					IL_6A:
					num = 2;
				}
				return xmlReader;
			default:
				if (false)
				{
				}
				break;
			}
			IL_46:
			if (true)
			{
			}
			xmlReader = new XmlTextReader(A_0);
			num = 0;
			goto IL_28;
		}

		// Token: 0x06005B47 RID: 23367 RVA: 0x0038EDB0 File Offset: 0x0038DDB0
		internal static XmlReader ᜀ(Stream A_0)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return UtilityMethods.ᜀ(A_0, true);
		}

		// Token: 0x06005B48 RID: 23368 RVA: 0x0038EDF4 File Offset: 0x0038DDF4
		internal static XmlWriter ᜀ(Stream A_0, Encoding A_1)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return new XmlTextWriter(A_0, A_1);
		}

		// Token: 0x06005B49 RID: 23369 RVA: 0x0038EE38 File Offset: 0x0038DE38
		internal static XmlWriter ᜀ(TextWriter A_0)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return new XmlTextWriter(A_0);
		}

		// Token: 0x06005B4A RID: 23370 RVA: 0x0038EE7C File Offset: 0x0038DE7C
		internal static XmlWriter ᜀ(TextWriter A_0, bool A_1)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return new XmlTextWriter(A_0)
			{
				Formatting = Formatting.Indented
			};
		}

		// Token: 0x04002C82 RID: 11394
		private long \u25D8\u00B0\u008F\u0085;

		// Token: 0x04002C83 RID: 11395
		private const int ᜀ = 61;

		// Token: 0x04002C84 RID: 11396
		private const int ᜁ = 1048576;

		// Token: 0x04002C85 RID: 11397
		private int \u2460\u0093\u0087\u009B;

		// Token: 0x04002C86 RID: 11398
		private string[] \u25D8\u0093\u0084\u008E;

		// Token: 0x04002C87 RID: 11399
		private string \u25D9\u0091\u0093\u008F;

		// Token: 0x04002C88 RID: 11400
		private long[] \u25D9\u009B\u0087\u009D;

		// Token: 0x04002C89 RID: 11401
		private byte \u2593\u0092\u00AF\u009C;

		// Token: 0x04002C8A RID: 11402
		private const int ᜂ = 16384;

		// Token: 0x04002C8B RID: 11403
		private const int ᜃ = 65536;

		// Token: 0x04002C8C RID: 11404
		private const int ᜄ = 256;
	}
}
