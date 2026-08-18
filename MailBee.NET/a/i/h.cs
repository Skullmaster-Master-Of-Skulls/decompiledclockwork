using System;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using MailBee;
using MailBee.Mime;

namespace a.i
{
	// Token: 0x020001DA RID: 474
	internal class h
	{
		// Token: 0x06000F44 RID: 3908 RVA: 0x00039A62 File Offset: 0x00038A62
		private h()
		{
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00039A6C File Offset: 0x00038A6C
		public static string a(string A_0, string A_1, MailTransferEncoding A_2, string A_3, HeaderEncodingOptions A_4, bool A_5)
		{
			if (h.d(A_1) || (A_4 & HeaderEncodingOptions.ForceEncoding) == HeaderEncodingOptions.ForceEncoding)
			{
				Encoding encoding = bb.a(A_3);
				if (!A_5)
				{
					return h.a(A_1, A_2, A_3, A_4);
				}
				double num;
				if (A_2 != MailTransferEncoding.QuotedPrintable)
				{
					if (A_2 == MailTransferEncoding.Base64)
					{
						num = 0.76;
					}
					else
					{
						A_2 = MailTransferEncoding.QuotedPrintable;
						num = 0.33;
					}
				}
				else
				{
					num = 0.33;
				}
				StringBuilder stringBuilder = new StringBuilder();
				int num2 = Global.UnwrappedLineLengthLimit - (A_0.Length + A_3.Length + 9);
				num2 = Convert.ToInt32((double)num2 * num);
				if (num2 > A_1.Length)
				{
					stringBuilder.Append(h.a(A_1, A_2, A_3, A_4));
				}
				else
				{
					int num3 = 0;
					StringBuilder stringBuilder2 = new StringBuilder();
					for (int i = 0; i < A_1.Length; i++)
					{
						bool flag = false;
						int num4;
						if (char.IsHighSurrogate(A_1[i]) && i + 1 < A_1.Length)
						{
							flag = true;
							num4 = encoding.GetBytes(new char[]
							{
								A_1[i],
								A_1[i + 1]
							}).Length;
						}
						else
						{
							num4 = encoding.GetBytes(new char[]
							{
								A_1[i]
							}).Length;
						}
						if (num3 + num4 > num2 && num3 > 0)
						{
							stringBuilder.AppendFormat(null, "\t{0}\r\n", new object[]
							{
								h.a(stringBuilder2.ToString(), A_2, A_3, HeaderEncodingOptions.ForceEncoding)
							});
							num3 = 0;
							stringBuilder2.Length = 0;
						}
						num3 += num4;
						if (flag)
						{
							stringBuilder2.Append(A_1.Substring(i, 2));
							i++;
						}
						else
						{
							stringBuilder2.Append(A_1[i]);
						}
					}
					if (num3 > 0)
					{
						stringBuilder.AppendFormat(null, "\t{0}\r\n", new object[]
						{
							h.a(stringBuilder2.ToString(), A_2, A_3, HeaderEncodingOptions.ForceEncoding)
						});
					}
					stringBuilder.Remove(0, 1);
					stringBuilder.Remove(stringBuilder.Length - 2, 2);
				}
				return stringBuilder.ToString();
			}
			else
			{
				if (!A_5)
				{
					return A_1;
				}
				return k.b(A_1, Global.UnwrappedLineLengthLimit);
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00039C78 File Offset: 0x00038C78
		public static void a(MimePartCollection A_0, HeaderEncodingOptions A_1, string A_2, string A_3)
		{
			foreach (object obj in A_0)
			{
				MimePart mimePart = (MimePart)obj;
				if ((A_1 & HeaderEncodingOptions.IgnoreAttachments) != HeaderEncodingOptions.IgnoreAttachments || !mimePart.IsFile)
				{
					MailTransferEncoding mailTransferEncoding = ((A_1 & HeaderEncodingOptions.Base64) == HeaderEncodingOptions.Base64) ? MailTransferEncoding.Base64 : MailTransferEncoding.QuotedPrintable;
					foreach (object obj2 in mimePart.Headers)
					{
						Header header = (Header)obj2;
						StringCollection stringCollection = new StringCollection();
						string[] value = new string[]
						{
							"bcc",
							"cc",
							"from",
							"reply-to",
							"to"
						};
						stringCollection.AddRange(value);
						if (stringCollection.Contains(header.Name.ToLower()))
						{
							if (header.Value != null && header.Value.Length != 0)
							{
								string value2 = EmailAddressCollection.a(header.Value, header).a(A_3, true, mailTransferEncoding, A_2);
								header.Value = value2;
							}
						}
						else
						{
							header.Value = h.a(header.Name, header.Value, mailTransferEncoding, A_2, A_1, true);
						}
						if (header.HeaderParameters != null)
						{
							foreach (object obj3 in header.HeaderParameters)
							{
								n n = (n)obj3;
								n.b(h.a(n.a(), mailTransferEncoding, A_2, A_1));
								n.c(h.a(n.c(), mailTransferEncoding, A_2, A_1));
							}
						}
					}
				}
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00039E80 File Offset: 0x00038E80
		public static bool d(string A_0)
		{
			foreach (char c in A_0)
			{
				if (c != '\t' && c != '\r' && c != '\n')
				{
					int num = Convert.ToInt32(c);
					if (num < 32 || num > 127)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00039ECC File Offset: 0x00038ECC
		public static string a(string A_0, MailTransferEncoding A_1, string A_2, HeaderEncodingOptions A_3)
		{
			if (!h.d(A_0) && (A_3 & HeaderEncodingOptions.ForceEncoding) != HeaderEncodingOptions.ForceEncoding)
			{
				return A_0;
			}
			Encoding encoding = bb.a(A_2);
			if (A_1 == MailTransferEncoding.QuotedPrintable)
			{
				string text = h.d(encoding.GetBytes(A_0)).ToString();
				text = text.Replace("_", "=5F").Replace(' ', '_');
				return string.Format(CultureInfo.InvariantCulture, "=?{0}?Q?{1}?=", new object[]
				{
					A_2,
					text
				});
			}
			byte[] array = h.e(encoding.GetBytes(A_0));
			string @string = Encoding.Default.GetString(array, 0, array.Length);
			return string.Format(CultureInfo.InvariantCulture, "=?{0}?B?{1}?=", new object[]
			{
				A_2,
				@string
			});
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00039F7C File Offset: 0x00038F7C
		public static string a(string A_0, Encoding A_1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = new byte[0];
			Encoding encoding = null;
			int i = 0;
			int num = 0;
			int j = 0;
			while (j >= 0)
			{
				j = A_0.IndexOf("?=", j);
				if (j < 0)
				{
					break;
				}
				int num2 = A_0.IndexOf("=?", j);
				if (num2 < 0)
				{
					break;
				}
				if (j > num2 - 2)
				{
					j++;
				}
				else
				{
					string text = A_0.Substring(j + 2, num2 - (j + 2));
					if (text.Length > 0 && text.Trim(new char[]
					{
						' ',
						'\f',
						'\n',
						'\r',
						'\t',
						'\v'
					}).Length == 0)
					{
						A_0 = A_0.Remove(j + 2, num2 - (j + 2));
					}
					j++;
				}
			}
			try
			{
				while (i < A_0.Length)
				{
					bool flag = false;
					int num3 = A_0.IndexOf("=?", i);
					if (num3 > -1)
					{
						if (num3 - i > 0 && encoding != null && array.Length != 0)
						{
							stringBuilder.Append(encoding.GetString(array, 0, array.Length));
							array = new byte[0];
						}
						stringBuilder.Append(A_0.Substring(i, num3 - i));
						i = num3;
						int num4 = A_0.IndexOf("?", num3 + 2);
						if (num4 > num3)
						{
							int num5 = A_0.IndexOf("?", num4 + 1);
							if (num5 == num4 + 2)
							{
								num = A_0.IndexOf("?=", num5 + 1);
								if (num > -1)
								{
									string text2 = A_0.Substring(num4 + 1, 1).ToLower();
									if (text2 == "b" || text2 == "q")
									{
										string a_ = A_0.Substring(num3 + 2, num4 - (num3 + 2));
										string text3 = A_0.Substring(num5 + 1, num - (num5 + 1));
										Encoding encoding2 = (A_1 == null) ? bb.a(a_) : A_1;
										if (encoding == null)
										{
											encoding = encoding2;
										}
										else if (encoding != encoding2)
										{
											stringBuilder.Append(encoding.GetString(array, 0, array.Length));
											array = new byte[0];
											encoding = encoding2;
										}
										MailTransferEncoding a_2;
										if (text2 == "q")
										{
											text3 = text3.Replace('_', ' ');
											a_2 = MailTransferEncoding.QuotedPrintable;
										}
										else
										{
											a_2 = MailTransferEncoding.Base64;
										}
										array = w.b(array, h.a(a_2, Global.DefaultEncoding.GetBytes(text3)));
										flag = true;
									}
								}
							}
						}
					}
					if (!flag)
					{
						if (encoding != null && array.Length != 0)
						{
							stringBuilder.Append(encoding.GetString(array, 0, array.Length));
							array = new byte[0];
						}
						if (num3 > -1)
						{
							stringBuilder.Append(A_0, i, num3 + 2 - i);
							i = num3 + 2;
						}
						else
						{
							stringBuilder.Append(A_0, i, A_0.Length - i);
							i = A_0.Length;
						}
					}
					else
					{
						i = num + 2;
					}
				}
				if (encoding != null && array.Length != 0)
				{
					stringBuilder.Append(encoding.GetString(array, 0, array.Length));
					array = new byte[0];
				}
			}
			catch
			{
				return A_0;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0003A280 File Offset: 0x00039280
		public static string c(string A_0)
		{
			return h.a(A_0, null);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0003A28C File Offset: 0x0003928C
		public static void a(ao A_0, byte[] A_1, MailTransferEncoding A_2)
		{
			byte[] array = new byte[0];
			switch (A_2)
			{
			case MailTransferEncoding.None:
			case MailTransferEncoding.Raw7bit:
			case MailTransferEncoding.Raw8bit:
				array = Global.DefaultEncoding.GetBytes(k.a(Global.DefaultEncoding.GetString(A_1, 0, A_1.Length), Global.UnwrappedLineLengthLimit));
				break;
			case MailTransferEncoding.QuotedPrintable:
				array = Global.DefaultEncoding.GetBytes(h.b(A_1, Global.UnwrappedLineLengthLimit));
				break;
			case MailTransferEncoding.Base64:
				h.a(A_0, A_1, Global.UnwrappedLineLengthLimit);
				return;
			case MailTransferEncoding.Uue:
				array = Global.DefaultEncoding.GetBytes(h.a(A_1, string.Empty));
				break;
			default:
				array = Global.DefaultEncoding.GetBytes(k.a(Global.DefaultEncoding.GetString(A_1, 0, A_1.Length), Global.UnwrappedLineLengthLimit));
				break;
			}
			if (array.Length != 0)
			{
				A_0.a(array, 0, array.Length);
			}
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0003A358 File Offset: 0x00039358
		public static byte[] a(byte[] A_0, MailTransferEncoding A_1, out int A_2)
		{
			byte[] array = new byte[0];
			switch (A_1)
			{
			case MailTransferEncoding.None:
			case MailTransferEncoding.Raw7bit:
			case MailTransferEncoding.Raw8bit:
				array = Global.DefaultEncoding.GetBytes(k.a(Global.DefaultEncoding.GetString(A_0, 0, A_0.Length), Global.UnwrappedLineLengthLimit));
				break;
			case MailTransferEncoding.QuotedPrintable:
				array = Global.DefaultEncoding.GetBytes(h.b(A_0, Global.UnwrappedLineLengthLimit));
				break;
			case MailTransferEncoding.Base64:
				return h.a(A_0, Global.UnwrappedLineLengthLimit, out A_2);
			case MailTransferEncoding.Uue:
				array = Global.DefaultEncoding.GetBytes(h.a(A_0, string.Empty));
				break;
			default:
				array = Global.DefaultEncoding.GetBytes(k.a(Global.DefaultEncoding.GetString(A_0, 0, A_0.Length), Global.UnwrappedLineLengthLimit));
				break;
			}
			A_2 = array.Length;
			return array;
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003A41C File Offset: 0x0003941C
		public static byte[] a(MailTransferEncoding A_0, byte[] A_1)
		{
			int num = 0;
			Array sourceArray = h.a(A_0, new ao(A_1), out num);
			byte[] array = new byte[num];
			Array.Copy(sourceArray, 0, array, 0, num);
			return array;
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0003A44C File Offset: 0x0003944C
		public static byte[] a(MailTransferEncoding A_0, ao A_1, out int A_2)
		{
			A_2 = A_1.e();
			try
			{
				byte[] array = new byte[0];
				switch (A_0)
				{
				case MailTransferEncoding.None:
				case MailTransferEncoding.Raw7bit:
				case MailTransferEncoding.Raw8bit:
					return A_1.c();
				case MailTransferEncoding.QuotedPrintable:
					array = h.a(A_1.c());
					break;
				case MailTransferEncoding.Base64:
					return h.a(A_1.d(), A_1.b(), A_1.e(), out A_2);
				case MailTransferEncoding.Uue:
					array = h.a(Encoding.ASCII.GetString(A_1.d(), A_1.b(), A_1.e()));
					break;
				}
				if (array.Length != 0)
				{
					A_2 = array.Length;
					return array;
				}
			}
			catch
			{
			}
			return A_1.c();
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0003A508 File Offset: 0x00039508
		public static MailTransferEncoding b(string A_0)
		{
			string a_ = A_0.ToLower();
			uint num = global::b.a(a_);
			if (num <= 1815660811U)
			{
				if (num != 251340885U)
				{
					if (num != 789508238U)
					{
						if (num != 1815660811U)
						{
							return MailTransferEncoding.None;
						}
						if (!(a_ == "x-uue"))
						{
							return MailTransferEncoding.None;
						}
					}
					else if (!(a_ == "x-uuencode"))
					{
						return MailTransferEncoding.None;
					}
				}
				else if (!(a_ == "uuencode"))
				{
					return MailTransferEncoding.None;
				}
				return MailTransferEncoding.Uue;
			}
			if (num <= 2312238551U)
			{
				if (num != 1879953953U)
				{
					if (num == 2312238551U)
					{
						if (a_ == "quoted-printable")
						{
							return MailTransferEncoding.QuotedPrintable;
						}
					}
				}
				else if (a_ == "7bit")
				{
					return MailTransferEncoding.Raw7bit;
				}
			}
			else if (num != 2564323490U)
			{
				if (num == 4031671994U)
				{
					if (a_ == "base64")
					{
						return MailTransferEncoding.Base64;
					}
				}
			}
			else if (a_ == "8bit")
			{
				return MailTransferEncoding.Raw8bit;
			}
			return MailTransferEncoding.None;
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0003A5F0 File Offset: 0x000395F0
		public static string a(MailTransferEncoding A_0)
		{
			switch (A_0)
			{
			case MailTransferEncoding.Raw7bit:
				return "7bit";
			case MailTransferEncoding.Raw8bit:
				return "8bit";
			case MailTransferEncoding.QuotedPrintable:
				return "quoted-printable";
			case MailTransferEncoding.Base64:
				return "base64";
			case MailTransferEncoding.Uue:
				return "x-uue";
			default:
				return string.Empty;
			}
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0003A63E File Offset: 0x0003963E
		public static string b(Encoding A_0)
		{
			if (A_0 != null)
			{
				return A_0.HeaderName;
			}
			return Global.DefaultEncoding.HeaderName;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0003A654 File Offset: 0x00039654
		public static bool a(Encoding A_0)
		{
			return A_0 == Encoding.Unicode || A_0 == Encoding.BigEndianUnicode || A_0 == Encoding.UTF32;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0003A670 File Offset: 0x00039670
		public static Encoding a(Encoding A_0, Encoding A_1)
		{
			if (h.a(A_0))
			{
				return A_1;
			}
			return A_0;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0003A680 File Offset: 0x00039680
		public static byte[] e(byte[] A_0)
		{
			int num = 0;
			Array sourceArray = h.a(A_0, -1, out num);
			byte[] array = new byte[num];
			Array.Copy(sourceArray, 0, array, 0, num);
			return array;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0003A6AC File Offset: 0x000396AC
		public static byte[] a(byte[] A_0, int A_1, out int A_2)
		{
			A_2 = 0;
			if (A_0 == null || A_0.Length == 0)
			{
				return new byte[A_2];
			}
			int i = A_0.Length;
			if (i < 4)
			{
				A_2 = 4;
			}
			else
			{
				A_2 = h.a(i, A_1, 1f);
			}
			byte[] array = new byte[A_2];
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (i >= 3)
			{
				array[num++] = Convert.ToByte(h.b[A_0[num3] >> 2]);
				h.a(++num2, ref array, ref num, A_1);
				array[num++] = Convert.ToByte(h.b[((int)A_0[num3] << 4 & 48) | A_0[num3 + 1] >> 4]);
				h.a(++num2, ref array, ref num, A_1);
				array[num++] = Convert.ToByte(h.b[((int)A_0[num3 + 1] << 2 & 60) | A_0[num3 + 2] >> 6]);
				h.a(++num2, ref array, ref num, A_1);
				array[num++] = Convert.ToByte(h.b[(int)(A_0[num3 + 2] & 63)]);
				h.a(++num2, ref array, ref num, A_1);
				num3 += 3;
				i -= 3;
			}
			if (i > 0)
			{
				array[num++] = Convert.ToByte(h.b[A_0[num3] >> 2]);
				h.a(++num2, ref array, ref num, A_1);
				byte b = Convert.ToByte((int)A_0[num3] << 4 & 48);
				if (i > 1)
				{
					b |= Convert.ToByte(A_0[num3 + 1] >> 4);
				}
				array[num++] = Convert.ToByte(h.b[(int)b]);
				h.a(++num2, ref array, ref num, A_1);
				array[num++] = Convert.ToByte((i < 2) ? '=' : h.b[(int)A_0[num3 + 1] << 2 & 60]);
				h.a(num2 + 1, ref array, ref num, A_1);
				array[num++] = Convert.ToByte('=');
			}
			A_2 = num;
			return array;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0003A8A0 File Offset: 0x000398A0
		public static void a(ao A_0, byte[] A_1, int A_2)
		{
			if (A_1 == null || A_1.Length == 0)
			{
				return;
			}
			int i = A_1.Length;
			int a_;
			if (i < 4)
			{
				a_ = 6;
			}
			else
			{
				a_ = h.a(i, A_2, 1f);
			}
			A_0.a(a_);
			byte[] array = A_0.d();
			int num = A_0.b() + A_0.e();
			int num2 = 0;
			int num3 = 0;
			while (i >= 3)
			{
				array[num++] = Convert.ToByte(h.b[A_1[num3] >> 2]);
				h.a(++num2, ref array, ref num, A_2);
				array[num++] = Convert.ToByte(h.b[((int)A_1[num3] << 4 & 48) | A_1[num3 + 1] >> 4]);
				h.a(++num2, ref array, ref num, A_2);
				array[num++] = Convert.ToByte(h.b[((int)A_1[num3 + 1] << 2 & 60) | A_1[num3 + 2] >> 6]);
				h.a(++num2, ref array, ref num, A_2);
				array[num++] = Convert.ToByte(h.b[(int)(A_1[num3 + 2] & 63)]);
				h.a(++num2, ref array, ref num, A_2);
				num3 += 3;
				i -= 3;
			}
			if (i > 0)
			{
				array[num++] = Convert.ToByte(h.b[A_1[num3] >> 2]);
				h.a(++num2, ref array, ref num, A_2);
				byte b = Convert.ToByte((int)A_1[num3] << 4 & 48);
				if (i > 1)
				{
					b |= Convert.ToByte(A_1[num3 + 1] >> 4);
				}
				array[num++] = Convert.ToByte(h.b[(int)b]);
				h.a(++num2, ref array, ref num, A_2);
				array[num++] = Convert.ToByte((i < 2) ? '=' : h.b[(int)A_1[num3 + 1] << 2 & 60]);
				h.a(num2 + 1, ref array, ref num, A_2);
				array[num++] = Convert.ToByte('=');
			}
			if (array[num - 1] != 10)
			{
				A_0.a(num + 2);
				array = A_0.d();
				array[num++] = 13;
				array[num++] = 10;
			}
			A_0.b(A_0.e() + (num - (A_0.b() + A_0.e())));
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0003AAEC File Offset: 0x00039AEC
		public static int a(int A_0, int A_1, float A_2)
		{
			int num = (A_0 + 2 - (A_0 + 2) % 3) / 3 * 4;
			for (int i = 0; i < 4; i++)
			{
				num += ((num % 4 != 0) ? 1 : 0);
			}
			if (A_1 > 0 && num / A_1 > 0)
			{
				num += (int)Math.Ceiling((double)num / (double)A_1) * 2;
			}
			if (A_2 <= 0f)
			{
				return num;
			}
			return (int)Math.Floor((double)((float)num * A_2));
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0003AB50 File Offset: 0x00039B50
		private static void a(int A_0, ref byte[] A_1, ref int A_2, int A_3)
		{
			if (A_3 > 0 && A_0 % A_3 == 0)
			{
				byte[] array = A_1;
				int num = A_2;
				A_2 = num + 1;
				array[num] = 13;
				byte[] array2 = A_1;
				num = A_2;
				A_2 = num + 1;
				array2[num] = 10;
			}
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0003AB84 File Offset: 0x00039B84
		public static string d(byte[] A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = A_0.Length;
			stringBuilder.Length = num * 4;
			stringBuilder.Capacity = num * 4;
			int i = 0;
			int num2 = 0;
			while (i < num)
			{
				char c = Convert.ToChar(A_0[i]);
				if (c == '\r' && i < num - 1 && A_0[i + 1] == 10)
				{
					stringBuilder[num2++] = '\r';
					stringBuilder[num2++] = '\n';
					i++;
				}
				else if ((c >= '!' && c <= '<') || (c == '>' || (c >= '@' && c <= '~')) || c == '\t' || c == ' ')
				{
					stringBuilder[num2++] = c;
				}
				else
				{
					stringBuilder[num2++] = '=';
					h.a(stringBuilder, c, num2);
					num2 += 2;
				}
				i++;
			}
			stringBuilder.Length = num2;
			return stringBuilder.ToString();
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0003AC5C File Offset: 0x00039C5C
		public static string b(byte[] A_0, int A_1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			int i;
			for (i = 0; i < A_0.Length; i++)
			{
				byte b = A_0[i];
				if ((b >= 33 && b <= 60) || (b >= 62 && b <= 126) || b == 9 || b == 32)
				{
					stringBuilder.Append(Convert.ToChar(A_0[i]));
					num++;
				}
				else if (b == 13)
				{
					if (i < A_0.Length - 1)
					{
						if (A_0[i + 1] == 10)
						{
							stringBuilder.Append(Convert.ToChar(A_0[i]));
							num = 0;
							i++;
							continue;
						}
						stringBuilder.Append(h.c(b));
						num += 3;
					}
					else
					{
						stringBuilder.Append(h.c(b));
						num += 3;
					}
				}
				else if (b == 10)
				{
					if (i > 0)
					{
						if (A_0[i - 1] == 13)
						{
							stringBuilder.Append(Convert.ToChar(A_0[i]));
							i++;
							continue;
						}
						stringBuilder.Append(h.c(b));
						num += 3;
					}
					else
					{
						stringBuilder.Append(h.c(b));
						num += 3;
					}
				}
				else
				{
					stringBuilder.Append(h.c(b));
					num += 3;
				}
				if (num >= A_1 - 1)
				{
					if (num == A_1 - 1)
					{
						if (A_0.Length - 1 - i > 3)
						{
							b = A_0[i + 1];
							if ((b >= 33 && b <= 60) || (b >= 62 && b <= 126) || b == 9 || b == 32)
							{
								if (A_0[i + 2] == 13 && A_0[i + 3] == 10)
								{
									stringBuilder.Append(Convert.ToChar(A_0[i + 1]));
									stringBuilder.Append(Convert.ToChar(A_0[i + 2]));
									stringBuilder.Append(Convert.ToChar(A_0[i + 3]));
									i += 3;
								}
								else
								{
									stringBuilder.Append("=\r\n");
								}
							}
							else
							{
								stringBuilder.Append("=\r\n");
							}
						}
						num = 0;
					}
					else
					{
						stringBuilder.Insert(stringBuilder.Length - 3, "=\r\n");
						num = 3;
					}
				}
			}
			i = A_0.Length - 1;
			while (i >= 0)
			{
				bool flag = false;
				char c = Convert.ToChar(A_0[i]);
				switch (c)
				{
				case '\t':
					goto IL_208;
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					goto IL_219;
				default:
					if (c == ' ')
					{
						goto IL_208;
					}
					goto IL_219;
				}
				IL_21C:
				if (!flag)
				{
					i--;
					continue;
				}
				break;
				IL_208:
				stringBuilder.Append("=\r\n");
				flag = true;
				goto IL_21C;
				IL_219:
				flag = true;
				goto IL_21C;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003AE98 File Offset: 0x00039E98
		public static string a(byte[] A_0, string A_1)
		{
			MemoryStream memoryStream = new MemoryStream(A_0);
			short num = 45;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("begin 644 " + Path.GetFileName(A_1) + "\r\n");
			int num2 = Convert.ToInt32(memoryStream.Length / (long)num);
			BinaryReader binaryReader = new BinaryReader(memoryStream);
			byte[] array = new byte[(int)num];
			for (int i = 1; i <= num2; i++)
			{
				array = binaryReader.ReadBytes((int)num);
				stringBuilder.Append(h.a(num) + h.c(array) + "\r\n");
			}
			if (memoryStream.Length % (long)num > 0L)
			{
				array = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
				stringBuilder.Append(h.a((short)array.Length) + h.c(array) + "\r\n");
			}
			stringBuilder.Append(h.a(0) + "\r\nend\r\n");
			return stringBuilder.ToString();
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0003AF90 File Offset: 0x00039F90
		private static string c(byte[] A_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = new byte[4];
			byte[] array2 = new byte[3];
			if (A_0.Length % 3 != 0)
			{
				byte[] array3 = new byte[A_0.Length + (3 - A_0.Length % 3)];
				Array.Copy(A_0, 0, array3, 0, A_0.Length);
				A_0 = array3;
			}
			for (int i = 0; i < A_0.Length; i += 3)
			{
				array2[0] = A_0[i];
				array2[1] = A_0[i + 1];
				array2[2] = A_0[i + 2];
				array[0] = Convert.ToByte((int)(array2[0] / 4 + 32));
				array[1] = Convert.ToByte((int)(array2[0] % 4 * 16 + (array2[1] / 16 + 32)));
				array[2] = Convert.ToByte((int)(array2[1] % 16 * 4 + (array2[2] / 64 + 32)));
				array[3] = Convert.ToByte((int)(array2[2] % 64 + 32));
				if (array[0] == 32)
				{
					array[0] = 96;
				}
				if (array[1] == 32)
				{
					array[1] = 96;
				}
				if (array[2] == 32)
				{
					array[2] = 96;
				}
				if (array[3] == 32)
				{
					array[3] = 96;
				}
				stringBuilder.Append(Convert.ToChar(array[0]));
				stringBuilder.Append(Convert.ToChar(array[1]));
				stringBuilder.Append(Convert.ToChar(array[2]));
				stringBuilder.Append(Convert.ToChar(array[3]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0003B0D0 File Offset: 0x0003A0D0
		private static string a(short A_0)
		{
			if (A_0 == 0)
			{
				return "`";
			}
			A_0 = Convert.ToInt16((int)(A_0 + 32));
			return Convert.ToChar(A_0).ToString();
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0003B100 File Offset: 0x0003A100
		private static string c(byte A_0)
		{
			string text = Convert.ToInt32(A_0).ToString("X", CultureInfo.InvariantCulture).ToUpper();
			if (text != null && text.Length > 1)
			{
				return string.Format(CultureInfo.InvariantCulture, "={0}", new object[]
				{
					text
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "=0{0}", new object[]
			{
				text
			});
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0003B16C File Offset: 0x0003A16C
		public static byte[] b(byte[] A_0)
		{
			int num = 0;
			Array sourceArray = h.a(A_0, 0, A_0.Length, out num);
			byte[] array = new byte[num];
			Array.Copy(sourceArray, 0, array, 0, num);
			return array;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0003B198 File Offset: 0x0003A198
		public static byte[] a(byte[] A_0, int A_1, int A_2, out int A_3)
		{
			A_3 = 0;
			if (A_0 == null || A_0.Length == 0)
			{
				return new byte[0];
			}
			int num = A_2;
			byte[] array = new byte[num];
			int num2 = 0;
			int num3 = 0;
			if (num >= 2 && A_0[A_1] == 43 && A_0[A_1 + 1] == 32)
			{
				num3 += 2;
				num -= 2;
			}
			int num4 = num;
			if (num % 4 > 0)
			{
				num4 += num % 4;
			}
			int num5 = -1;
			int num6 = -1;
			int num7 = -1;
			int num8 = -1;
			int i = 0;
			while (i < num4)
			{
				if (i < num)
				{
					if (num5 < 0)
					{
						num5 = h.b(A_0[A_1 + i]);
					}
					else if (num6 < 0)
					{
						num6 = h.b(A_0[A_1 + i]);
					}
					else if (num7 < 0)
					{
						num7 = h.b(A_0[A_1 + i]);
					}
					else
					{
						if (num8 < 0)
						{
							num8 = h.b(A_0[A_1 + i]);
							goto IL_F0;
						}
						goto IL_F0;
					}
				}
				else if (num5 < 0)
				{
					num5 = 64;
				}
				else if (num6 < 0)
				{
					num6 = 64;
				}
				else if (num7 < 0)
				{
					num7 = 64;
				}
				else
				{
					if (num8 < 0)
					{
						num8 = 64;
						goto IL_F0;
					}
					goto IL_F0;
				}
				IL_1B5:
				i++;
				continue;
				IL_F0:
				if (num5 > -1 && num6 > -1 && num7 > -1 && num8 > -1)
				{
					int num9 = Convert.ToInt32(num5 << 2 | num6 >> 4);
					array[num2++] = ((num9 >= 0 && num9 <= 255) ? Convert.ToByte(num9) : 0);
					if (num7 != 64)
					{
						num9 = ((num6 << 4 & 240) | num7 >> 2);
						array[num2++] = ((num9 >= 0 && num9 <= 255) ? Convert.ToByte(num9) : 0);
						if (num8 != 64)
						{
							num9 = ((num7 << 6 & 192) | num8);
							array[num2++] = ((num9 >= 0 && num9 <= 255) ? Convert.ToByte(num9) : 0);
						}
					}
					num5 = -1;
					num6 = -1;
					num7 = -1;
					num8 = -1;
					goto IL_1B5;
				}
				goto IL_1B5;
			}
			A_3 = num2;
			return array;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003B36D File Offset: 0x0003A36D
		private static int b(byte A_0)
		{
			if (A_0 >= 0 && A_0 <= 127)
			{
				return h.a[(int)A_0];
			}
			return -1;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0003B384 File Offset: 0x0003A384
		public static byte[] a(byte[] A_0)
		{
			int num = 0;
			int i = 0;
			int num2 = A_0.Length - 1;
			byte[] array = new byte[A_0.Length];
			int num3 = 0;
			while (i < A_0.Length)
			{
				int num4 = 2;
				if (A_0[i] != 61)
				{
					i = w.a(A_0, 61, i);
					if (i < 0)
					{
						i = A_0.Length;
					}
					Buffer.BlockCopy(A_0, num, array, num3, i - num);
					num3 += i - num;
				}
				int num5 = i + 1;
				if (num5 < num2)
				{
					byte b = A_0[num5];
					if (b != 32 || b != 9 || b != 13 || b != 10 || b != 12)
					{
						if (b == 10 && A_0[num5 - 1] != 13)
						{
							num4 = 1;
						}
						if (h.a(ref b, A_0, num5))
						{
							array[num3] = b;
							num3++;
						}
						else if (!char.IsWhiteSpace((char)b))
						{
							Buffer.BlockCopy(A_0, i, array, num3, 3);
							num3 += 3;
						}
					}
					i += num4;
				}
				else if (i < A_0.Length && A_0[i] != 61)
				{
					array[num3] = A_0[i];
					num3++;
				}
				i++;
				num = i;
			}
			byte[] array2 = new byte[num3];
			Buffer.BlockCopy(array, 0, array2, 0, num3);
			return array2;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0003B4A4 File Offset: 0x0003A4A4
		public static byte[] a(string A_0)
		{
			byte[] array = new byte[A_0.Length];
			int num = 0;
			using (MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(A_0)))
			{
				StreamReader streamReader = new StreamReader(memoryStream, Encoding.ASCII);
				for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine())
				{
					if (!text.StartsWith("begin "))
					{
						if (!(text != "end"))
						{
							break;
						}
						if (text.Length > 0)
						{
							int num2 = Convert.ToInt32(text[0]);
							if (num2 != 96)
							{
								byte[] array2 = h.a(text.Substring(1, text.Length - 1), num2 - 32);
								Array.Copy(array2, 0, array, num, array2.Length);
								num += array2.Length;
							}
						}
					}
				}
			}
			byte[] array3 = new byte[num];
			Array.Copy(array, 0, array3, 0, array3.Length);
			return array3;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0003B594 File Offset: 0x0003A594
		private static byte a(byte A_0)
		{
			if (A_0 >= 65 && A_0 <= 90)
			{
				return Convert.ToByte((int)(A_0 - 65));
			}
			if (A_0 >= 97 && A_0 <= 122)
			{
				return Convert.ToByte((int)(A_0 - 97 + 26));
			}
			if (A_0 >= 48 && A_0 <= 57)
			{
				return Convert.ToByte((int)(A_0 - 48 + 52));
			}
			if (A_0 == 43)
			{
				return 62;
			}
			if (A_0 == 47)
			{
				return 63;
			}
			if (A_0 == 61)
			{
				return 64;
			}
			return 65;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0003B600 File Offset: 0x0003A600
		private static byte[] a(string A_0, int A_1)
		{
			if (A_1 <= 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[A_1];
			int num = 0;
			byte[] array2 = new byte[4];
			byte[] array3 = new byte[3];
			int num2 = 0;
			while (num2 < A_0.Length && num2 < A_0.Length)
			{
				array2[0] = Convert.ToByte(A_0[num2]);
				if (num2 + 1 >= A_0.Length)
				{
					break;
				}
				array2[1] = Convert.ToByte(A_0[num2 + 1]);
				if (num2 + 2 >= A_0.Length)
				{
					break;
				}
				array2[2] = Convert.ToByte(A_0[num2 + 2]);
				if (num2 + 3 >= A_0.Length)
				{
					break;
				}
				array2[3] = Convert.ToByte(A_0[num2 + 3]);
				if (array2[0] == 96)
				{
					array2[0] = 32;
				}
				if (array2[1] == 96)
				{
					array2[1] = 32;
				}
				if (array2[2] == 96)
				{
					array2[2] = 32;
				}
				if (array2[3] == 96)
				{
					array2[3] = 32;
				}
				array3[0] = Convert.ToByte((int)((array2[0] - 32) * 4 + (array2[1] - 32) / 16));
				array3[1] = Convert.ToByte((int)(array2[1] % 16 * 16 + (array2[2] - 32) / 4));
				array3[2] = Convert.ToByte((int)(array2[2] % 4 * 64 + (array2[3] - 32)));
				if (num + 3 > A_1)
				{
					for (int i = 0; i <= A_1 - num; i++)
					{
						array[num++] = array3[i];
					}
				}
				else
				{
					array[num++] = array3[0];
					array[num++] = array3[1];
					array[num++] = array3[2];
				}
				num2 += 4;
			}
			return array;
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0003B78C File Offset: 0x0003A78C
		private static void a(StringBuilder A_0, char A_1, int A_2)
		{
			char c = Convert.ToChar((int)((A_1 & 'ð') >> 4));
			A_0[A_2] = Convert.ToChar((int)((c > '\t') ? (c + '7') : (c + '0')));
			c = Convert.ToChar((int)(A_1 & '\u000f'));
			A_0[A_2 + 1] = Convert.ToChar((int)((c > '\t') ? (c + '7') : (c + '0')));
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0003B7EC File Offset: 0x0003A7EC
		private static bool a(ref byte A_0, byte[] A_1, int A_2)
		{
			byte b = (A_1[A_2] >= 97 && A_1[A_2] <= 102) ? (A_1[A_2] - 32) : A_1[A_2];
			if ((b < 48 || b > 57) && (b < 65 || b > 70))
			{
				return false;
			}
			A_0 = (byte)((b > 64) ? ((int)(b - 55) << 4) : ((int)(b - 48) << 4));
			b = ((A_1[A_2 + 1] >= 97 && A_1[A_2 + 1] <= 102) ? (A_1[A_2 + 1] - 32) : A_1[A_2 + 1]);
			if ((b >= 48 && b <= 57) || (b >= 65 && b <= 70))
			{
				A_0 += ((b > 64) ? (b - 55) : (b - 48));
				return true;
			}
			return false;
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0003B890 File Offset: 0x0003A890
		public static byte a(byte[] A_0, int A_1)
		{
			byte result = 0;
			char c = '0';
			char c2 = 'a';
			if (A_1 < A_0.Length)
			{
				c = Convert.ToChar(A_0[A_1]);
			}
			if (A_1 + 1 < A_0.Length)
			{
				c2 = Convert.ToChar(A_0[A_1 + 1]);
			}
			try
			{
				result = byte.Parse(new string(new char[]
				{
					c,
					c2
				}), NumberStyles.HexNumber);
			}
			catch (FormatException)
			{
			}
			return result;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003B8FC File Offset: 0x0003A8FC
		public static byte[] a(char[] A_0)
		{
			Encoding ascii = Encoding.ASCII;
			Encoding unicode = Encoding.Unicode;
			byte[] bytes = unicode.GetBytes(A_0);
			return Encoding.Convert(unicode, ascii, bytes);
		}

		// Token: 0x04000AEF RID: 2799
		private static int[] a = new int[]
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			62,
			-1,
			-1,
			-1,
			63,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			-1,
			-1,
			-1,
			64,
			-1,
			-1,
			-1,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			-1,
			-1,
			-1,
			-1,
			-1
		};

		// Token: 0x04000AF0 RID: 2800
		private static string b = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/???????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????";
	}
}
