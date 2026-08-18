using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using MailBee.Outlook;

namespace a.b
{
	// Token: 0x020002A4 RID: 676
	internal class e3 : iu
	{
		// Token: 0x060017B2 RID: 6066 RVA: 0x0006C34D File Offset: 0x0006B34D
		public new static bool a()
		{
			return e3.a;
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0006C354 File Offset: 0x0006B354
		public new static void a(bool A_0)
		{
			e3.a = A_0;
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0006C35C File Offset: 0x0006B35C
		public new static void a(UnsupportedVariantTypeException A_0)
		{
			if (e3.a())
			{
				if (e3.b == null)
				{
					e3.b = new List<long>();
				}
				long item = A_0.VariantType;
				if (!e3.b.Contains(item))
				{
					e3.b.Add(item);
				}
			}
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0006C3A0 File Offset: 0x0006B3A0
		public new bool b(int A_0)
		{
			for (int i = 0; i < e3.c.Length; i++)
			{
				if (A_0 == e3.c[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0006C3CC File Offset: 0x0006B3CC
		public new static object a(byte[] A_0, int A_1, int A_2, long A_3, int A_4)
		{
			ds ds = new ds((int)A_3, null);
			int num;
			try
			{
				num = ds.b(A_0, A_1);
			}
			catch (InvalidOperationException)
			{
				int num2 = Math.Min(A_2, A_0.Length - A_1);
				byte[] array = new byte[num2];
				Array.Copy(A_0, A_1, array, 0, num2);
				throw new ReadingNotSupportedException(A_3, array);
			}
			int num3 = (int)A_3;
			if (num3 <= 20)
			{
				switch (num3)
				{
				case 0:
				case 3:
				case 5:
					break;
				case 1:
				case 4:
					goto IL_12A;
				case 2:
					return (short)ds.a();
				default:
					if (num3 == 11)
					{
						return ((a2)ds.a()).a();
					}
					if (num3 != 20)
					{
						goto IL_12A;
					}
					break;
				}
				return ds.a();
			}
			if (num3 <= 31)
			{
				if (num3 == 30)
				{
					return ((iz)ds.a()).b(A_4);
				}
				if (num3 == 31)
				{
					return ((ft)ds.a()).b();
				}
			}
			else
			{
				if (num3 == 64)
				{
					gv gv = (gv)ds.a();
					return a8.a((int)gv.a(), (int)gv.b());
				}
				if (num3 == 71)
				{
					return ((c)ds.a()).c();
				}
			}
			IL_12A:
			byte[] array2 = new byte[num];
			Array.Copy(A_0, A_1, array2, 0, num);
			throw new ReadingNotSupportedException(A_3, array2);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0006C530 File Offset: 0x0006B530
		public new static string a(int A_0)
		{
			if (A_0 <= 0)
			{
				throw new gk("Codepage number may not be " + A_0);
			}
			if (A_0 > 10029)
			{
				if (A_0 <= 50225)
				{
					if (A_0 <= 20127)
					{
						switch (A_0)
						{
						case 10079:
							return "MacIceland";
						case 10080:
							goto IL_35C;
						case 10081:
							return "MacTurkish";
						case 10082:
							return "MacCroatian";
						default:
							if (A_0 != 20127)
							{
								goto IL_35C;
							}
							break;
						}
					}
					else
					{
						if (A_0 == 20866)
						{
							return "KOI8-R";
						}
						switch (A_0)
						{
						case 28591:
							return "ISO-8859-1";
						case 28592:
							return "ISO-8859-2";
						case 28593:
							return "ISO-8859-3";
						case 28594:
							return "ISO-8859-4";
						case 28595:
							return "ISO-8859-5";
						case 28596:
							return "ISO-8859-6";
						case 28597:
							return "ISO-8859-7";
						case 28598:
							return "ISO-8859-8";
						case 28599:
							return "ISO-8859-9";
						default:
							switch (A_0)
							{
							case 50220:
							case 50221:
							case 50222:
								return "ISO-2022-JP";
							case 50223:
							case 50224:
								goto IL_35C;
							case 50225:
								return "ISO-2022-KR";
							default:
								goto IL_35C;
							}
							break;
						}
					}
				}
				else if (A_0 <= 52936)
				{
					if (A_0 == 51932)
					{
						return "EUC-JP";
					}
					if (A_0 == 51949)
					{
						return "EUC-KR";
					}
					if (A_0 != 52936)
					{
						goto IL_35C;
					}
					return "GB2312";
				}
				else
				{
					if (A_0 == 54936)
					{
						return "GB18030";
					}
					if (A_0 != 65000)
					{
						if (A_0 != 65001)
						{
							goto IL_35C;
						}
						return "UTF-8";
					}
				}
				return "US-ASCII";
			}
			if (A_0 <= 1200)
			{
				if (A_0 <= 932)
				{
					if (A_0 == 37)
					{
						return "cp037";
					}
					if (A_0 == 932)
					{
						return "SJIS";
					}
				}
				else
				{
					if (A_0 == 936)
					{
						return "GBK";
					}
					if (A_0 == 949)
					{
						return "ms949";
					}
					if (A_0 == 1200)
					{
						return "UTF-16";
					}
				}
			}
			else if (A_0 <= 1258)
			{
				if (A_0 == 1201)
				{
					return "UTF-16BE";
				}
				switch (A_0)
				{
				case 1250:
					return "windows-1250";
				case 1251:
					return "windows-1251";
				case 1252:
					return "windows-1252";
				case 1253:
					return "windows-1253";
				case 1254:
					return "windows-1254";
				case 1255:
					return "windows-1255";
				case 1256:
					return "windows-1256";
				case 1257:
					return "windows-1257";
				case 1258:
					return "windows-1258";
				}
			}
			else
			{
				if (A_0 == 1361)
				{
					return "johab";
				}
				switch (A_0)
				{
				case 10000:
					return "MacRoman";
				case 10001:
					return "SJIS";
				case 10002:
					return "Big5";
				case 10003:
					return "EUC-KR";
				case 10004:
					return "MacArabic";
				case 10005:
					return "MacHebrew";
				case 10006:
					return "MacGreek";
				case 10007:
					return "MacCyrillic";
				case 10008:
					return "EUC_CN";
				case 10009:
				case 10011:
				case 10012:
				case 10013:
				case 10014:
				case 10015:
				case 10016:
				case 10018:
				case 10019:
				case 10020:
					break;
				case 10010:
					return "MacRomania";
				case 10017:
					return "MacUkraine";
				case 10021:
					return "MacThai";
				default:
					if (A_0 == 10029)
					{
						return "MacCentralEurope";
					}
					break;
				}
			}
			IL_35C:
			return "cp" + A_0;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0006C8AC File Offset: 0x0006B8AC
		public new static int a(Stream A_0, long A_1, object A_2, int A_3)
		{
			int num = 0;
			int num2 = (int)A_1;
			if (num2 <= 20)
			{
				switch (num2)
				{
				case 0:
					num += h7.a(A_0, 0U);
					break;
				case 1:
				case 4:
					goto IL_248;
				case 2:
				{
					short a_;
					try
					{
						a_ = Convert.ToInt16(A_2, CultureInfo.InvariantCulture);
					}
					catch (OverflowException)
					{
						a_ = (short)((int)A_2);
					}
					num += h7.a(A_0, a_);
					break;
				}
				case 3:
					if (!(A_2 is int))
					{
						throw new Exception("Could not cast an object To int: " + A_2.GetType().Name + ", " + A_2.ToString());
					}
					num += h7.b(A_0, (int)A_2);
					break;
				case 5:
					num += h7.a(A_0, (double)A_2);
					break;
				default:
					if (num2 != 11)
					{
						if (num2 != 20)
						{
							goto IL_248;
						}
						num += h7.a(A_0, Convert.ToInt64(A_2, CultureInfo.CurrentCulture));
					}
					else
					{
						new byte[2];
						if ((bool)A_2)
						{
							A_0.WriteByte(byte.MaxValue);
							A_0.WriteByte(byte.MaxValue);
						}
						else
						{
							A_0.WriteByte(0);
							A_0.WriteByte(0);
						}
						num += 2;
					}
					break;
				}
			}
			else if (num2 <= 31)
			{
				if (num2 != 30)
				{
					if (num2 != 31)
					{
						goto IL_248;
					}
					int a_2 = ((string)A_2).Length + 1;
					num += h7.a(A_0, (uint)a_2);
					char[] array = ((string)A_2).ToCharArray();
					for (int i = 0; i < array.Length; i++)
					{
						int num3 = (int)((array[i] & '＀') >> 8);
						byte b = (byte)(array[i] & 'ÿ');
						byte value = (byte)num3;
						byte value2 = b;
						A_0.WriteByte(value2);
						A_0.WriteByte(value);
						num += 2;
					}
					A_0.WriteByte(0);
					A_0.WriteByte(0);
					num += 2;
				}
				else
				{
					iz iz = new iz((string)A_2, A_3);
					num += iz.a(A_0);
				}
			}
			else if (num2 != 64)
			{
				if (num2 != 71)
				{
					goto IL_248;
				}
				byte[] array2 = (byte[])A_2;
				A_0.Write(array2, 0, array2.Length);
				num = array2.Length;
			}
			else
			{
				long num4;
				if (A_2 != null)
				{
					num4 = a8.a((DateTime)A_2);
				}
				else
				{
					num4 = 0L;
				}
				int a_3 = (int)(num4 >> 32 & (long)((ulong)-1));
				gv gv = new gv((int)(num4 & (long)((ulong)-1)), a_3);
				num += gv.a(A_0);
			}
			IL_28B:
			while ((num & 3) != 0)
			{
				A_0.WriteByte(0);
				num++;
			}
			return num;
			IL_248:
			if (A_2 is byte[])
			{
				byte[] array3 = (byte[])A_2;
				A_0.Write(array3, 0, array3.Length);
				num = array3.Length;
				e3.a(new WritingNotSupportedException(A_1, A_2));
				goto IL_28B;
			}
			throw new WritingNotSupportedException(A_1, A_2);
		}

		// Token: 0x040011AD RID: 4525
		private new static bool a = false;

		// Token: 0x040011AE RID: 4526
		protected new static List<long> b;

		// Token: 0x040011AF RID: 4527
		public new static int[] c = new int[]
		{
			0,
			2,
			3,
			20,
			5,
			64,
			30,
			31,
			71,
			11
		};
	}
}
