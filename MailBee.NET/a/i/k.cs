using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using MailBee;
using MailBee.Mime;
using Microsoft.Win32;

namespace a.i
{
	// Token: 0x020001F4 RID: 500
	internal class k
	{
		// Token: 0x06000FFF RID: 4095 RVA: 0x000422C2 File Offset: 0x000412C2
		private k()
		{
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000422CC File Offset: 0x000412CC
		private static void d(ao A_0)
		{
			if (A_0.e() > 0)
			{
				int i;
				for (i = A_0.b(); i < A_0.b() + A_0.e() - 1; i++)
				{
					if (A_0.d()[i] == 13 && A_0.d()[i + 1] != 10)
					{
						A_0.d()[i] = 10;
					}
				}
				if (A_0.d()[i] == 13)
				{
					A_0.d()[i] = 10;
				}
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004233C File Offset: 0x0004133C
		public static bool c(ao A_0)
		{
			int num = 0;
			for (int i = A_0.b(); i < A_0.b() + A_0.e(); i++)
			{
				if (A_0.d()[i] == 13)
				{
					if (i >= A_0.b() + A_0.e() - 1 || A_0.d()[i + 1] != 10)
					{
						k.d(A_0);
						return true;
					}
					i++;
					num++;
				}
				else if (A_0.d()[i] == 10)
				{
					num++;
				}
				if (num == 10)
				{
					return false;
				}
			}
			return false;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x000423C0 File Offset: 0x000413C0
		public static DateTime a(string A_0, g A_1)
		{
			DateTime dateTime = DateTime.MinValue;
			A_0 = A_0.Trim();
			Match match = m.f.Match(A_0.ToLower());
			if (match.Success)
			{
				try
				{
					int num = int.Parse(match.Groups["year"].Value, CultureInfo.InvariantCulture);
					if (num < 100)
					{
						if (num > 30)
						{
							num += 1900;
						}
						else
						{
							num += 2000;
						}
					}
					int num2 = k.g(match.Groups["month"].Value);
					if (num2 == -1)
					{
						num2 = 1;
					}
					int day = int.Parse(match.Groups["day"].Value, CultureInfo.InvariantCulture);
					int num3 = int.Parse(match.Groups["hour"].Value, CultureInfo.InvariantCulture);
					if (num3 > 23 && Global.FixBadDates)
					{
						num3 = 23;
					}
					int num4 = int.Parse(match.Groups["minute"].Value, CultureInfo.InvariantCulture);
					if (num4 > 59 && Global.FixBadDates)
					{
						num4 = 59;
					}
					int num5 = (match.Groups["second"].Value != string.Empty) ? int.Parse(match.Groups["second"].Value, CultureInfo.InvariantCulture) : 0;
					if (num5 > 59 && Global.FixBadDates)
					{
						num5 = 59;
					}
					dateTime = new DateTime(num, num2, day, num3, num4, num5);
					dateTime = k.a(dateTime, match.Groups["offset"].Value, match.Groups["zone"].Value, A_1);
				}
				catch (ArgumentOutOfRangeException a_)
				{
					throw new MailBeeDateParsingException(43, a_);
				}
				catch (ArgumentException a_2)
				{
					throw new MailBeeDateParsingException(43, a_2);
				}
			}
			return dateTime;
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x000425C0 File Offset: 0x000415C0
		public static string a(DateTime A_0)
		{
			TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(A_0);
			string text = "+";
			int num = utcOffset.Hours;
			int num2 = utcOffset.Minutes;
			if (num < 0 || num2 < 0)
			{
				text = "-";
				num = Math.Abs(num);
				num2 = Math.Abs(num2);
			}
			string newValue = string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}", new object[]
			{
				text,
				(num < 10) ? string.Format(CultureInfo.InvariantCulture, "0{0}", new object[]
				{
					num.ToString(CultureInfo.InvariantCulture)
				}) : num.ToString(CultureInfo.InvariantCulture),
				(num2 < 10) ? string.Format(CultureInfo.InvariantCulture, "0{0}", new object[]
				{
					num2.ToString(CultureInfo.InvariantCulture)
				}) : num2.ToString(CultureInfo.InvariantCulture)
			});
			string text2 = A_0.ToString("r", CultureInfo.InvariantCulture);
			if (A_0.Kind != DateTimeKind.Utc)
			{
				text2 = text2.Replace("GMT", newValue);
			}
			return text2;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x000426CC File Offset: 0x000416CC
		private static int g(string A_0)
		{
			if (A_0 == "jan")
			{
				return 1;
			}
			if (A_0 == "feb")
			{
				return 2;
			}
			if (A_0 == "mar")
			{
				return 3;
			}
			if (A_0 == "apr")
			{
				return 4;
			}
			if (A_0 == "may")
			{
				return 5;
			}
			if (A_0 == "jun")
			{
				return 6;
			}
			if (A_0 == "jul")
			{
				return 7;
			}
			if (A_0 == "aug")
			{
				return 8;
			}
			if (A_0 == "sep")
			{
				return 9;
			}
			if (A_0 == "oct")
			{
				return 10;
			}
			if (A_0 == "nov")
			{
				return 11;
			}
			if (A_0 == "dec")
			{
				return 12;
			}
			return -1;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00042794 File Offset: 0x00041794
		private static DateTime a(DateTime A_0, string A_1, string A_2, g A_3)
		{
			if (A_1 != null && A_1.Length != 0)
			{
				A_1 = A_1.Trim();
				char c = A_1[0];
				A_1 = A_1.Remove(0, 1);
				int num = 0;
				try
				{
					num = int.Parse(A_1, CultureInfo.InvariantCulture);
				}
				catch (FormatException)
				{
				}
				int num2 = num / 100;
				int num3 = num % 100;
				int num4 = 1;
				if (c == '-')
				{
					num4 = -1;
				}
				A_0 = A_0.AddHours((double)(-(double)(num4 * num2)));
				A_0 = A_0.AddMinutes((double)(-(double)(num4 * num3)));
			}
			else if (A_2 != null && A_2.Length != 0)
			{
				char[] trimChars = new char[]
				{
					' ',
					'(',
					')'
				};
				A_2 = A_2.Trim(trimChars);
				uint num5 = global::b.a(A_2);
				if (num5 <= 1987375272U)
				{
					if (num5 <= 1195577708U)
					{
						if (num5 != 1142953439U)
						{
							if (num5 == 1195577708U)
							{
								if (!(A_2 == "ut"))
								{
								}
							}
						}
						else if (!(A_2 == "gmt"))
						{
						}
					}
					else if (num5 != 1433963168U)
					{
						if (num5 != 1464708287U)
						{
							if (num5 == 1987375272U)
							{
								if (A_2 == "edt")
								{
									A_0 = A_0.AddHours(-4.0);
								}
							}
						}
						else if (A_2 == "pdt")
						{
							A_0 = A_0.AddHours(-7.0);
						}
					}
					else if (A_2 == "pst")
					{
						A_0 = A_0.AddHours(-8.0);
					}
				}
				else if (num5 <= 3481289791U)
				{
					if (num5 != 2023343271U)
					{
						if (num5 != 3450544672U)
						{
							if (num5 == 3481289791U)
							{
								if (A_2 == "mst")
								{
									A_0 = A_0.AddHours(-7.0);
								}
							}
						}
						else if (A_2 == "mdt")
						{
							A_0 = A_0.AddHours(-6.0);
						}
					}
					else if (A_2 == "est")
					{
						A_0 = A_0.AddHours(-5.0);
					}
				}
				else if (num5 != 4168914114U)
				{
					if (num5 != 4198967685U)
					{
						if (num5 == 4278997933U)
						{
							if (!(A_2 == "z"))
							{
							}
						}
					}
					else if (A_2 == "cst")
					{
						A_0 = A_0.AddHours(-6.0);
					}
				}
				else if (A_2 == "cdt")
				{
					A_0 = A_0.AddHours(-5.0);
				}
			}
			if (A_3 == global::a.i.g.a)
			{
				A_0 = TimeZone.CurrentTimeZone.ToLocalTime(A_0);
			}
			else
			{
				A_0 = DateTime.SpecifyKind(A_0, DateTimeKind.Utc);
			}
			return A_0;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x00042AA8 File Offset: 0x00041AA8
		public static string f(string A_0)
		{
			if (A_0 != null && (!A_0.StartsWith("<") || !A_0.EndsWith(">")))
			{
				A_0 = string.Format(CultureInfo.InvariantCulture, "<{0}>", new object[]
				{
					A_0
				});
			}
			return A_0;
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x00042AE4 File Offset: 0x00041AE4
		private static string a(string A_0, string A_1)
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.ClassesRoot;
				if (registryKey != null)
				{
					registryKey = registryKey.OpenSubKey(string.Format(CultureInfo.InvariantCulture, ".{0}", new object[]
					{
						A_1
					}));
				}
				if (registryKey != null)
				{
					A_0 = (string)registryKey.GetValue("Content Type");
				}
			}
			catch
			{
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return A_0;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x00042B60 File Offset: 0x00041B60
		public static string e(string A_0)
		{
			string text = "application/octet-stream";
			if (A_0 == null || A_0.Length == 0)
			{
				return text;
			}
			if (A_0[0] == '.')
			{
				A_0 = A_0.Remove(0, 1);
			}
			string text2 = A_0.ToLower();
			uint num = global::b.a(text2);
			if (num <= 1748353692U)
			{
				if (num <= 1464607992U)
				{
					if (num <= 577951402U)
					{
						if (num <= 333626681U)
						{
							if (num != 126868124U)
							{
								if (num != 333626681U)
								{
									goto IL_786;
								}
								if (!(text2 == "wav"))
								{
									goto IL_786;
								}
								return "audio/x-wav";
							}
							else
							{
								if (!(text2 == "htm"))
								{
									goto IL_786;
								}
								goto IL_732;
							}
						}
						else if (num != 424135463U)
						{
							if (num != 476447045U)
							{
								if (num != 577951402U)
								{
									goto IL_786;
								}
								if (!(text2 == "asc"))
								{
									goto IL_786;
								}
								goto IL_744;
							}
							else
							{
								if (!(text2 == "avi"))
								{
									goto IL_786;
								}
								return "video/x-msvideo";
							}
						}
						else if (!(text2 == "aiff"))
						{
							goto IL_786;
						}
					}
					else if (num <= 1060265211U)
					{
						if (num != 913413841U)
						{
							if (num != 986687121U)
							{
								if (num != 1060265211U)
								{
									goto IL_786;
								}
								if (!(text2 == "aif"))
								{
									goto IL_786;
								}
							}
							else
							{
								if (!(text2 == "rtx"))
								{
									goto IL_786;
								}
								return "text/richtext";
							}
						}
						else
						{
							if (!(text2 == "lzh"))
							{
								goto IL_786;
							}
							goto IL_6FC;
						}
					}
					else if (num != 1143273375U)
					{
						if (num != 1154463311U)
						{
							if (num != 1464607992U)
							{
								goto IL_786;
							}
							if (!(text2 == "qt"))
							{
								goto IL_786;
							}
							goto IL_774;
						}
						else
						{
							if (!(text2 == "rtf"))
							{
								goto IL_786;
							}
							return "application/rtf";
						}
					}
					else
					{
						if (!(text2 == "ai"))
						{
							goto IL_786;
						}
						goto IL_71A;
					}
					return "audio/x-aiff";
				}
				if (num <= 1582198420U)
				{
					if (num <= 1549040540U)
					{
						if (num != 1498364840U)
						{
							if (num != 1523243198U)
							{
								if (num != 1549040540U)
								{
									goto IL_786;
								}
								if (!(text2 == "ra"))
								{
									goto IL_786;
								}
								return "audio/x-realaudio";
							}
							else
							{
								if (!(text2 == "bin"))
								{
									goto IL_786;
								}
								goto IL_6FC;
							}
						}
						else
						{
							if (!(text2 == "lha"))
							{
								goto IL_786;
							}
							goto IL_6FC;
						}
					}
					else if (num != 1581212682U)
					{
						if (num != 1581869418U)
						{
							if (num != 1582198420U)
							{
								goto IL_786;
							}
							if (!(text2 == "ps"))
							{
								goto IL_786;
							}
						}
						else
						{
							if (!(text2 == "tiff"))
							{
								goto IL_786;
							}
							goto IL_6F6;
						}
					}
					else
					{
						if (!(text2 == "js"))
						{
							goto IL_786;
						}
						return "application/x-javascript";
					}
				}
				else if (num <= 1719319908U)
				{
					if (num != 1704127225U)
					{
						if (num != 1714084033U)
						{
							if (num != 1719319908U)
							{
								goto IL_786;
							}
							if (!(text2 == "midi"))
							{
								goto IL_786;
							}
							goto IL_756;
						}
						else
						{
							if (!(text2 == "gif"))
							{
								goto IL_786;
							}
							return "image/gif";
						}
					}
					else if (!(text2 == "eps"))
					{
						goto IL_786;
					}
				}
				else if (num != 1736401595U)
				{
					if (num != 1738962391U)
					{
						if (num != 1748353692U)
						{
							goto IL_786;
						}
						if (!(text2 == "png"))
						{
							goto IL_786;
						}
						return "image/png";
					}
					else
					{
						if (!(text2 == "exe"))
						{
							goto IL_786;
						}
						goto IL_6FC;
					}
				}
				else
				{
					if (!(text2 == "ppt"))
					{
						goto IL_786;
					}
					return "application/vnd.ms-powerpoint";
				}
				IL_71A:
				return "application/postscript";
			}
			if (num <= 3305831240U)
			{
				if (num <= 2625189937U)
				{
					if (num > 2308861280U)
					{
						if (num != 2608412318U)
						{
							if (num != 2611152612U)
							{
								if (num != 2625189937U)
								{
									goto IL_786;
								}
								if (!(text2 == "mp3"))
								{
									goto IL_786;
								}
							}
							else if (!(text2 == "mpga"))
							{
								goto IL_786;
							}
						}
						else if (!(text2 == "mp2"))
						{
							goto IL_786;
						}
						return "audio/mpeg";
					}
					if (num != 1766705429U)
					{
						if (num != 2223688961U)
						{
							if (num != 2308861280U)
							{
								goto IL_786;
							}
							if (!(text2 == "mpeg"))
							{
								goto IL_786;
							}
							goto IL_76E;
						}
						else
						{
							if (!(text2 == "eml"))
							{
								goto IL_786;
							}
							return "message/rfc822";
						}
					}
					else
					{
						if (!(text2 == "pdf"))
						{
							goto IL_786;
						}
						return "application/pdf";
					}
				}
				else if (num <= 2877453236U)
				{
					if (num != 2771757551U)
					{
						if (num != 2872970239U)
						{
							if (num != 2877453236U)
							{
								goto IL_786;
							}
							if (!(text2 == "zip"))
							{
								goto IL_786;
							}
							return "application/zip";
						}
						else
						{
							if (!(text2 == "class"))
							{
								goto IL_786;
							}
							goto IL_6FC;
						}
					}
					else
					{
						if (!(text2 == "txt"))
						{
							goto IL_786;
						}
						goto IL_744;
					}
				}
				else if (num != 3202323235U)
				{
					if (num != 3280944101U)
					{
						if (num != 3305831240U)
						{
							goto IL_786;
						}
						if (!(text2 == "tif"))
						{
							goto IL_786;
						}
						goto IL_6F6;
					}
					else
					{
						if (!(text2 == "mid"))
						{
							goto IL_786;
						}
						goto IL_756;
					}
				}
				else if (!(text2 == "jpeg"))
				{
					goto IL_786;
				}
			}
			else if (num <= 3670499120U)
			{
				if (num <= 3516816505U)
				{
					if (num != 3360267371U)
					{
						if (num != 3446624627U)
						{
							if (num != 3516816505U)
							{
								goto IL_786;
							}
							if (!(text2 == "mov"))
							{
								goto IL_786;
							}
							goto IL_774;
						}
						else
						{
							if (!(text2 == "dll"))
							{
								goto IL_786;
							}
							goto IL_6FC;
						}
					}
					else
					{
						if (!(text2 == "swf"))
						{
							goto IL_786;
						}
						return "application/x-shockwave-flash";
					}
				}
				else if (num != 3614812112U)
				{
					if (num != 3664801462U)
					{
						if (num != 3670499120U)
						{
							goto IL_786;
						}
						if (!(text2 == "jpg"))
						{
							goto IL_786;
						}
					}
					else
					{
						if (!(text2 == "xml"))
						{
							goto IL_786;
						}
						return "text/xml";
					}
				}
				else
				{
					if (!(text2 == "html"))
					{
						goto IL_786;
					}
					goto IL_732;
				}
			}
			else if (num <= 3932734293U)
			{
				if (num != 3704054358U)
				{
					if (num != 3798807531U)
					{
						if (num != 3932734293U)
						{
							goto IL_786;
						}
						if (!(text2 == "doc"))
						{
							goto IL_786;
						}
						return "application/msword";
					}
					else
					{
						if (!(text2 == "dms"))
						{
							goto IL_786;
						}
						goto IL_6FC;
					}
				}
				else if (!(text2 == "jpe"))
				{
					goto IL_786;
				}
			}
			else if (num != 4000954695U)
			{
				if (num != 4034509933U)
				{
					if (num != 4081524352U)
					{
						goto IL_786;
					}
					if (!(text2 == "css"))
					{
						goto IL_786;
					}
					return "text/css";
				}
				else
				{
					if (!(text2 == "mpg"))
					{
						goto IL_786;
					}
					goto IL_76E;
				}
			}
			else
			{
				if (!(text2 == "mpe"))
				{
					goto IL_786;
				}
				goto IL_76E;
			}
			return "image/jpeg";
			IL_76E:
			return "video/mpeg";
			IL_6F6:
			return "image/tiff";
			IL_6FC:
			return "application/octet-stream";
			IL_732:
			return "text/html";
			IL_744:
			return "text/plain";
			IL_756:
			return "audio/midi";
			IL_774:
			return "video/quicktime";
			IL_786:
			if (Global.IsWindows)
			{
				text = k.a(text, A_0);
			}
			if (text != null)
			{
				return text;
			}
			return "application/octet-stream";
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0004330C File Offset: 0x0004230C
		public static string d(string A_0)
		{
			string text = A_0.ToLower();
			uint num = global::b.a(text);
			if (num <= 2899107204U)
			{
				if (num <= 1578133620U)
				{
					if (num <= 799302309U)
					{
						if (num != 308579864U)
						{
							if (num != 785129815U)
							{
								if (num == 799302309U)
								{
									if (text == "audio/x-wav")
									{
										return "wav";
									}
								}
							}
							else if (text == "application/x-shockwave-flash")
							{
								return "swf";
							}
						}
						else if (text == "text/xml")
						{
							return "xml";
						}
					}
					else if (num <= 1044886470U)
					{
						if (num != 1041262187U)
						{
							if (num == 1044886470U)
							{
								if (text == "text/richtext")
								{
									return "rtf";
								}
							}
						}
						else if (text == "audio/x-aiff")
						{
							return "aif";
						}
					}
					else if (num != 1188789558U)
					{
						if (num == 1578133620U)
						{
							if (text == "image/tiff")
							{
								return "tif";
							}
						}
					}
					else if (text == "application/vnd.ms-powerpoint")
					{
						return "ppt";
					}
				}
				else if (num <= 2230039101U)
				{
					if (num != 1977116014U)
					{
						if (num != 2040610794U)
						{
							if (num == 2230039101U)
							{
								if (text == "application/zip")
								{
									return "zip";
								}
							}
						}
						else if (text == "application/msword")
						{
							return "doc";
						}
					}
					else if (text == "text/html")
					{
						return "htm";
					}
				}
				else if (num <= 2633335257U)
				{
					if (num != 2510378291U)
					{
						if (num == 2633335257U)
						{
							if (text == "video/x-msvideo")
							{
								return "avi";
							}
						}
					}
					else if (text == "audio/midi")
					{
						return "mid";
					}
				}
				else if (num != 2693661518U)
				{
					if (num == 2899107204U)
					{
						if (text == "application/pdf")
						{
							return "pdf";
						}
					}
				}
				else if (text == "image/jpg")
				{
					return "jpg";
				}
			}
			else if (num <= 3413199541U)
			{
				if (num <= 3072015935U)
				{
					if (num != 2904788206U)
					{
						if (num != 2953494330U)
						{
							if (num == 3072015935U)
							{
								if (text == "image/gif")
								{
									return "gif";
								}
							}
						}
						else if (text == "image/png")
						{
							return "png";
						}
					}
					else if (text == "text/css")
					{
						return "css";
					}
				}
				else if (num <= 3109025507U)
				{
					if (num != 3086404243U)
					{
						if (num == 3109025507U)
						{
							if (text == "audio/mpeg")
							{
								return "mp3";
							}
						}
					}
					else if (text == "application/postscript")
					{
						return "ps";
					}
				}
				else if (num != 3382304434U)
				{
					if (num == 3413199541U)
					{
						if (text == "audio/x-realaudio")
						{
							return "ra";
						}
					}
				}
				else if (text == "application/x-javascript")
				{
					return "js";
				}
			}
			else if (num <= 3901389917U)
			{
				if (num != 3527136901U)
				{
					if (num != 3820088002U)
					{
						if (num == 3901389917U)
						{
							if (text == "image/jpeg")
							{
								return "jpg";
							}
						}
					}
					else if (text == "application/rtf")
					{
						return "rtf";
					}
				}
				else if (text == "text/plain")
				{
					return "txt";
				}
			}
			else if (num <= 4054794778U)
			{
				if (num != 3962575004U)
				{
					if (num == 4054794778U)
					{
						if (text == "message/rfc822")
						{
							return "eml";
						}
					}
				}
				else if (text == "video/mpeg")
				{
					return "mpg";
				}
			}
			else if (num != 4233377562U)
			{
				if (num == 4236983561U)
				{
					if (text == "video/quicktime")
					{
						return "mov";
					}
				}
			}
			else if (text == "image/bmp")
			{
				return "bmp";
			}
			return "dat";
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x000437E8 File Offset: 0x000427E8
		public static string a(Attachment A_0, string A_1)
		{
			if (A_0.IsMessageInside)
			{
				MailMessage mailMessage = A_0.b(true);
				if (mailMessage != null)
				{
					A_1 = mailMessage.Subject;
				}
			}
			return k.a(A_0.FilenameOriginalInternal, A_0.NameInternal, A_0.ContentType, A_1);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00043828 File Offset: 0x00042828
		public static string a(string A_0, string A_1, string A_2, string A_3)
		{
			string a_ = string.Empty;
			if (A_0 != null && A_0.Length != 0)
			{
				a_ = A_0;
			}
			else if (A_1 != null && A_1.Length != 0)
			{
				a_ = A_1;
			}
			else
			{
				string text = string.Empty;
				if (A_3 != null && A_3.Length != 0)
				{
					text = A_3;
				}
				else
				{
					text = k.a();
				}
				a_ = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
				{
					text,
					k.d(A_2)
				});
			}
			return ap.f(a_);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0004389F File Offset: 0x0004289F
		public static string a(AddressDelimeterChar A_0)
		{
			if (A_0 == AddressDelimeterChar.Comma)
			{
				return ",";
			}
			if (A_0 != AddressDelimeterChar.Semicolon)
			{
				return ",";
			}
			return ";";
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x000438BC File Offset: 0x000428BC
		public static n a(string A_0, char A_1)
		{
			n n = new n();
			if (A_0 == null)
			{
				return n;
			}
			int num = A_0.IndexOf(A_1);
			if (num != -1)
			{
				char[] trimChars = new char[]
				{
					' ',
					'\t',
					'"'
				};
				n.b(A_0.Substring(0, num).Trim(trimChars));
				n.c(h.c(A_0.Substring(num + 1, A_0.Length - (num + 1)).Trim(trimChars)));
			}
			return n;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0004392B File Offset: 0x0004292B
		public static char[] b()
		{
			return new char[]
			{
				'\t',
				' '
			};
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x00043940 File Offset: 0x00042940
		public static string a(MailSensitivity A_0)
		{
			switch (A_0)
			{
			case MailSensitivity.None:
				return string.Empty;
			case MailSensitivity.Normal:
				return "Normal";
			case MailSensitivity.Personal:
				return "Personal";
			case MailSensitivity.Private:
				return "Private";
			case MailSensitivity.Confidential:
				return "Company-Confidential";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0004398C File Offset: 0x0004298C
		public static MailSensitivity c(string A_0)
		{
			string text = A_0.ToLower();
			if (text == "company-confidential")
			{
				return MailSensitivity.Confidential;
			}
			if (text != null && text.Length == 0)
			{
				return MailSensitivity.None;
			}
			if (text == "normal")
			{
				return MailSensitivity.Normal;
			}
			if (text == "personal")
			{
				return MailSensitivity.Personal;
			}
			if (!(text == "private"))
			{
				return MailSensitivity.None;
			}
			return MailSensitivity.Private;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x000439EC File Offset: 0x000429EC
		public static string a(MailPriority A_0, bool A_1)
		{
			switch (A_0)
			{
			case MailPriority.Highest:
				if (!A_1)
				{
					return "Highest";
				}
				return "1 (Highest)";
			case MailPriority.High:
				if (!A_1)
				{
					return "High";
				}
				return "2 (High)";
			case MailPriority.Normal:
				if (!A_1)
				{
					return "Normal";
				}
				return "3 (Normal)";
			case MailPriority.Low:
				if (!A_1)
				{
					return "Low";
				}
				return "4 (Low)";
			case MailPriority.Lowest:
				if (!A_1)
				{
					return "Lowest";
				}
				return "5 (Lowest)";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00043A68 File Offset: 0x00042A68
		public static MailPriority b(string A_0)
		{
			if (A_0 != null && A_0.Length > 0)
			{
				A_0 = A_0.Trim().ToLower();
				if (A_0.Length > 2)
				{
					if (A_0.IndexOf("highest") != -1)
					{
						return MailPriority.Highest;
					}
					if (A_0.IndexOf("high") != -1)
					{
						return MailPriority.High;
					}
					if (A_0.IndexOf("normal") != -1)
					{
						return MailPriority.Normal;
					}
					if (A_0.IndexOf("lowest") != -1)
					{
						return MailPriority.Lowest;
					}
					if (A_0.IndexOf("low") != -1)
					{
						return MailPriority.Low;
					}
				}
				switch (A_0[0])
				{
				case '1':
					return MailPriority.Highest;
				case '2':
					return MailPriority.High;
				case '3':
					return MailPriority.Normal;
				case '4':
					return MailPriority.Low;
				case '5':
					return MailPriority.Lowest;
				}
			}
			return MailPriority.None;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00043B1F File Offset: 0x00042B1F
		public static string b(string A_0, int A_1)
		{
			return k.a(A_0, A_1, "\t");
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x00043B2D File Offset: 0x00042B2D
		public static string a(string A_0, int A_1)
		{
			return k.a(A_0, A_1, string.Empty);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x00043B3C File Offset: 0x00042B3C
		private static string a(string A_0, int A_1, string A_2)
		{
			char[] anyOf = new char[]
			{
				' ',
				'\t'
			};
			if (A_0 == null)
			{
				return string.Empty;
			}
			if (A_1 > A_0.Length)
			{
				return A_0;
			}
			string[] array = A_0.Split(new char[]
			{
				'\n'
			});
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Length != 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					string text = array[i];
					if (text[text.Length - 1] == '\r')
					{
						text = text.Substring(0, text.Length - 1);
					}
					if (A_2 == "\t" && text != string.Empty && text[0] == '\t')
					{
						text = text.Substring(1, text.Length - 1);
					}
					int j = 0;
					int num = A_1;
					while (j < text.Length)
					{
						if (num >= text.Length)
						{
							stringBuilder.AppendFormat(null, "{0}", new object[]
							{
								text.Substring(j)
							});
							break;
						}
						int num2 = text.LastIndexOfAny(anyOf, num, A_1);
						if (num2 < 0)
						{
							num2 = text.IndexOfAny(anyOf, num);
							if (num2 > 0)
							{
								string text2 = text.Substring(j, num2 - j);
								stringBuilder.AppendFormat(null, "{0}\r\n{1}", new object[]
								{
									text2,
									A_2
								});
								j = num2;
								num = j + A_1;
							}
							else
							{
								num += A_1;
							}
						}
						else
						{
							string text3 = text.Substring(j, num2 - j);
							stringBuilder.AppendFormat(null, "{0}\r\n{1}", new object[]
							{
								text3,
								(text[num2] == ' ') ? "" : A_2
							});
							j = num2;
							num += A_1;
						}
					}
					array[i] = stringBuilder.ToString();
				}
			}
			return string.Join("\r\n" + A_2, array);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x00043D20 File Offset: 0x00042D20
		public static string a()
		{
			byte[] array = new byte[10];
			new RNGCryptoServiceProvider().GetNonZeroBytes(array);
			return k.b(array);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x00043D48 File Offset: 0x00042D48
		public static byte[] a(string A_0)
		{
			byte[] array = new byte[0];
			if (A_0 != null && A_0.Length % 2 == 0)
			{
				array = new byte[A_0.Length / 2];
				int num = 0;
				for (int i = 0; i < A_0.Length; i += 2)
				{
					byte b = byte.Parse(A_0.Substring(i, 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
					array[num] = b;
					num++;
				}
			}
			return array;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x00043DAC File Offset: 0x00042DAC
		public static string b(byte[] A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in A_0)
			{
				stringBuilder.Append(b.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00043DEC File Offset: 0x00042DEC
		public static int a(string A_0, string A_1, MailMergeTargets A_2, MailMessage A_3)
		{
			int num = 0;
			int num2 = 0;
			if ((A_2 & MailMergeTargets.BodyHtmlText) == MailMergeTargets.BodyHtmlText)
			{
				A_3.BodyHtmlText = k.a(A_3.BodyHtmlText, A_0, A_1, out num2);
				num += num2;
			}
			if ((A_2 & MailMergeTargets.BodyPlainText) == MailMergeTargets.BodyPlainText)
			{
				A_3.BodyPlainText = k.a(A_3.BodyPlainText, A_0, A_1, out num2);
				num += num2;
			}
			if ((A_2 & MailMergeTargets.From) == MailMergeTargets.From)
			{
				A_3.From = EmailAddress.a(k.a(A_3.From.ToString(), A_0, A_1, out num2), A_3.From.EmailAddressHeader);
				num += num2;
			}
			if ((A_2 & MailMergeTargets.Recipients) == MailMergeTargets.Recipients)
			{
				A_3.To = EmailAddressCollection.a(k.a(A_3.To.ToString(), A_0, A_1, out num2), A_3.To.RecipientsHeader);
				num += num2;
				A_3.Bcc = EmailAddressCollection.a(k.a(A_3.Bcc.ToString(), A_0, A_1, out num2), A_3.Bcc.RecipientsHeader);
				num += num2;
				A_3.Cc = EmailAddressCollection.a(k.a(A_3.Cc.ToString(), A_0, A_1, out num2), A_3.Cc.RecipientsHeader);
				num += num2;
			}
			if ((A_2 & MailMergeTargets.ReplyTo) == MailMergeTargets.ReplyTo)
			{
				A_3.ReplyTo = EmailAddressCollection.a(k.a(A_3.ReplyTo.ToString(), A_0, A_1, out num2), A_3.ReplyTo.RecipientsHeader);
				num += num2;
			}
			if ((A_2 & MailMergeTargets.Subject) == MailMergeTargets.Subject)
			{
				A_3.Subject = k.a(A_3.Subject, A_0, A_1, out num2);
				num += num2;
			}
			if ((A_2 & MailMergeTargets.Other) == MailMergeTargets.Other)
			{
				num += num2;
				foreach (object obj in A_3.Headers)
				{
					Header header = (Header)obj;
					string[] value = new string[]
					{
						"subject",
						"reply-to",
						"from",
						"to",
						"cc",
						"bcc"
					};
					StringCollection stringCollection = new StringCollection();
					stringCollection.AddRange(value);
					if (!stringCollection.Contains(header.Name.ToLower()))
					{
						header.Value = k.a(header.Value, A_0, A_1, out num2);
						num += num2;
					}
				}
			}
			return num;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00044024 File Offset: 0x00043024
		private static string a(string A_0, string A_1, string A_2, out int A_3)
		{
			A_3 = 0;
			if (A_0 == null || A_1 == null || A_1 == string.Empty || A_2 == null)
			{
				return A_0;
			}
			Regex regex = new Regex(Regex.Escape(A_1), RegexOptions.Singleline);
			MatchCollection matchCollection = regex.Matches(A_0);
			A_3 = matchCollection.Count;
			if (A_3 > 0)
			{
				A_0 = regex.Replace(A_0, A_2.Replace("$", "$$"));
			}
			return A_0;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0004408C File Offset: 0x0004308C
		public static bool b(ao A_0)
		{
			return A_0.d() != null && A_0.e() > 2 && A_0.d()[A_0.b()] == 208 && A_0.d()[A_0.b() + 1] == 207;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x000440D8 File Offset: 0x000430D8
		public static byte[] a(byte[] A_0)
		{
			if (A_0 == null)
			{
				return null;
			}
			byte[] preamble = Encoding.UTF8.GetPreamble();
			if (A_0.Length < preamble.Length)
			{
				return preamble;
			}
			if (w.b(A_0, 0, preamble.Length, preamble) == 0)
			{
				return null;
			}
			return preamble;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00044110 File Offset: 0x00043110
		public static ao a(ao A_0)
		{
			if (A_0.d() != null && A_0.e() > 2)
			{
				if (A_0.d()[A_0.b()] == 239 && A_0.d()[A_0.b() + 1] == 187 && A_0.d()[A_0.b() + 2] == 191)
				{
					return new ao(A_0, A_0.b() + 3, A_0.e() - 3);
				}
				if (A_0.d()[A_0.b()] == 254 && A_0.d()[A_0.b() + 1] == 255)
				{
					return new ao(A_0, A_0.b() + 2, A_0.e() - 2);
				}
				if (A_0.d()[A_0.b()] == 255 && A_0.d()[A_0.b() + 1] == 254)
				{
					return new ao(A_0, A_0.b() + 2, A_0.e() - 2);
				}
			}
			return A_0;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00044210 File Offset: 0x00043210
		public static HeaderCollection b(HeaderCollection A_0)
		{
			if (A_0 == null)
			{
				return new HeaderCollection();
			}
			HeaderCollection headerCollection = new HeaderCollection();
			int i = 0;
			Regex g = m.g;
			while (i < A_0.Count)
			{
				if (!g.IsMatch(A_0[i].Name))
				{
					headerCollection.b(A_0[i]);
					A_0.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			return headerCollection;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00044270 File Offset: 0x00043270
		public static HeaderCollection a(HeaderCollection A_0)
		{
			int i = 0;
			Regex g = m.g;
			while (i < A_0.Count)
			{
				if (g.IsMatch(A_0[i].Name))
				{
					A_0.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
			return A_0;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000442B4 File Offset: 0x000432B4
		public static HeaderCollection a(HeaderCollection A_0, HeaderCollection A_1)
		{
			for (int i = 0; i < A_0.Count; i++)
			{
				A_1.a(i, A_0[i]);
			}
			return A_1;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x000442E4 File Offset: 0x000432E4
		public static int a(byte[] A_0, byte[] A_1, int A_2, int A_3)
		{
			int num = 0;
			bool flag = false;
			if (A_0 == null || A_1 == null || A_2 < 0)
			{
				return num;
			}
			int num2 = A_2;
			if (A_3 < 0)
			{
				A_3 = A_0.Length - A_2;
			}
			int count = A_3;
			for (;;)
			{
				IL_1F:
				num = Array.IndexOf<byte>(A_0, A_1[0], num2, count);
				if (num != -1)
				{
					int i = num;
					int num3 = 0;
					while (i < num + A_1.Length)
					{
						if (i < A_2 + A_3)
						{
							if (A_0[i] == A_1[num3])
							{
								flag = true;
								i++;
								num3++;
								continue;
							}
							flag = false;
						}
						else
						{
							flag = false;
						}
						IL_6D:
						if (flag)
						{
							return num;
						}
						num2 = num + 1;
						count = A_3 - (num2 - A_2);
						goto IL_1F;
					}
					goto IL_6D;
				}
				break;
			}
			return -1;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00044370 File Offset: 0x00043370
		public static int a(byte[] A_0, int A_1, int A_2)
		{
			int num = A_1;
			int num2;
			for (;;)
			{
				num2 = Array.IndexOf<byte>(A_0, 10, num, A_1 + A_2 - num);
				if (num2 < 0)
				{
					goto IL_49;
				}
				int num3 = num2 - num;
				if (num3 == 0)
				{
					break;
				}
				if (num3 == 1)
				{
					if (num2 > 0 && A_0[num2 - 1] == 13)
					{
						goto Block_5;
					}
				}
				num = num2 + 1;
				if (num >= A_0.Length)
				{
					goto Block_6;
				}
			}
			return num2 + 1;
			Block_5:
			return num2 + 1;
			Block_6:
			return num2 + 1;
			IL_49:
			return A_1 + A_2;
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x000443CC File Offset: 0x000433CC
		public static int a(byte[] A_0, byte[] A_1, int A_2, int A_3, out int A_4)
		{
			int num = A_2;
			int num2 = 0;
			A_4 = 0;
			int num3;
			int i;
			for (;;)
			{
				num3 = k.a(A_0, A_1, num, A_3);
				if (num3 < 0)
				{
					goto IL_EF;
				}
				bool flag;
				if (num3 - A_2 >= 2)
				{
					if (A_0[num3 - 1] != 10)
					{
						flag = false;
					}
					else
					{
						flag = true;
						if (A_0[num3 - 2] != 13)
						{
							num2 = 1;
						}
						else
						{
							num2 = 2;
						}
					}
				}
				else if (num3 - A_2 >= 1)
				{
					if (A_0[num3 - 1] != 10)
					{
						flag = false;
					}
					else
					{
						flag = true;
						num2 = 1;
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					i = num3 + A_1.Length;
					while (i < A_2 + A_3)
					{
						if (A_0[i] != 45 && A_0[i] != 9 && A_0[i] != 32 && A_0[i] != 13)
						{
							if (A_0[i] != 10 && A_0[i] != 0)
							{
								flag = false;
								break;
							}
							if (A_0[i] == 10)
							{
								i++;
								break;
							}
							break;
						}
						else
						{
							i++;
						}
					}
					if (flag)
					{
						break;
					}
					A_3 -= i - num;
					num = i;
				}
				else
				{
					A_3 -= num3 + A_1.Length - num;
					num = num3 + A_1.Length;
				}
			}
			A_4 = i - (num3 - num2);
			return num3 - num2;
			IL_EF:
			A_4 = 0;
			return -1;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x000444D0 File Offset: 0x000434D0
		public static bool a(byte[] A_0, byte[] A_1)
		{
			int num = k.a(A_0, A_1, 0, -1);
			return num != -1 && A_0.Length > num + A_1.Length + 1 && A_0[num + A_1.Length] == 45 && A_0[num + A_1.Length + 1] == 45;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00044514 File Offset: 0x00043514
		public static string a(string A_0, char[] A_1)
		{
			string text = A_0;
			bool flag = false;
			ArrayList arrayList = new ArrayList(A_1);
			for (int i = 0; i < arrayList.Count; i++)
			{
				if ((char)arrayList[i] == '\\')
				{
					flag = true;
					arrayList.RemoveAt(i);
				}
			}
			if (flag)
			{
				text = text.Replace("\\", string.Format(CultureInfo.InvariantCulture, "\\{0}", new object[]
				{
					'\\'
				}));
			}
			foreach (object obj in arrayList)
			{
				char c = (char)obj;
				text = text.Replace(new string(c, 1), string.Format(CultureInfo.InvariantCulture, "\\{0}", new object[]
				{
					c
				}));
			}
			return text;
		}
	}
}
