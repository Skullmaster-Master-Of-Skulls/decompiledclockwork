using System;
using System.IO;
using System.Text;

namespace a.b
{
	// Token: 0x02000287 RID: 647
	internal class iz
	{
		// Token: 0x060016E4 RID: 5860 RVA: 0x0006899C File Offset: 0x0006799C
		private static string a(int A_0)
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

		// Token: 0x060016E5 RID: 5861 RVA: 0x00068D18 File Offset: 0x00067D18
		public iz(byte[] A_0, int A_1)
		{
			int num = p.i(A_0, A_1);
			int a_ = A_1 + 4;
			this.a = p.b(A_0, a_, num);
			byte b = this.a[num - 1];
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00068D52 File Offset: 0x00067D52
		public iz(string A_0, int A_1)
		{
			this.a(A_0, A_1);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00068D64 File Offset: 0x00067D64
		public string b(int A_0)
		{
			string @string;
			if (A_0 == -1)
			{
				@string = Encoding.UTF8.GetString(this.a);
			}
			else
			{
				@string = Encoding.GetEncoding(A_0).GetString(this.a);
			}
			int num = @string.IndexOf('\0');
			if (num == -1)
			{
				return @string;
			}
			int num2 = @string.Length - 1;
			return @string.Substring(0, num);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00068DBB File Offset: 0x00067DBB
		public int a()
		{
			return 4 + this.a.Length;
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00068DC7 File Offset: 0x00067DC7
		public void a(string A_0, int A_1)
		{
			if (A_1 == -1)
			{
				this.a = Encoding.UTF8.GetBytes(A_0 + "\0");
				return;
			}
			this.a = Encoding.GetEncoding(A_1).GetBytes(A_0 + "\0");
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00068E05 File Offset: 0x00067E05
		public int a(Stream A_0)
		{
			p.b(this.a.Length, A_0);
			A_0.Write(this.a, 0, this.a.Length);
			return 4 + this.a.Length;
		}

		// Token: 0x040010FE RID: 4350
		private byte[] a;
	}
}
