using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using a;
using a.i;

namespace MailBee.Mime
{
	// Token: 0x0200052D RID: 1325
	public class EmailAddress
	{
		// Token: 0x06002BC4 RID: 11204 RVA: 0x000CED86 File Offset: 0x000CDD86
		public EmailAddress()
		{
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000CEDAF File Offset: 0x000CDDAF
		internal EmailAddress(Header A_0)
		{
			this.d = A_0;
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000CEDDF File Offset: 0x000CDDDF
		public EmailAddress(string email)
		{
			if (email == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.b = email;
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000CEE1C File Offset: 0x000CDE1C
		public EmailAddress(string email, string displayName)
		{
			if (email == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			this.b = email;
			this.a = displayName;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000CEE74 File Offset: 0x000CDE74
		public EmailAddress(string email, string displayName, string remarks)
		{
			if (email == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			if (remarks == null)
			{
				remarks = string.Empty;
			}
			this.b = email;
			this.a = displayName;
			this.c = remarks;
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06002BC9 RID: 11209 RVA: 0x000CEEDC File Offset: 0x000CDEDC
		// (set) Token: 0x06002BCA RID: 11210 RVA: 0x000CEF50 File Offset: 0x000CDF50
		public string AsString
		{
			get
			{
				if (this.d != null && this.d.ParentCollection != null && this.d.ParentCollection.MimePart != null && this.d.ParentCollection.MimePart.ParentMessage != null)
				{
					return this.d.ParentCollection.MimePart.ParentMessage.f(this.ToString());
				}
				return this.ToString();
			}
			set
			{
				EmailAddress emailAddress = EmailAddress.Parse(value);
				this.Email = emailAddress.Email;
				this.DisplayName = emailAddress.DisplayName;
				this.Remarks = emailAddress.Remarks;
				this.a();
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06002BCB RID: 11211 RVA: 0x000CEF90 File Offset: 0x000CDF90
		// (set) Token: 0x06002BCC RID: 11212 RVA: 0x000CF002 File Offset: 0x000CE002
		public string DisplayName
		{
			get
			{
				if (this.d != null && this.d.ParentCollection != null && this.d.ParentCollection.MimePart != null && this.d.ParentCollection.MimePart.ParentMessage != null)
				{
					return this.d.ParentCollection.MimePart.ParentMessage.f(this.a);
				}
				return this.a;
			}
			set
			{
				this.a = value;
				this.a();
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06002BCD RID: 11213 RVA: 0x000CF014 File Offset: 0x000CE014
		// (set) Token: 0x06002BCE RID: 11214 RVA: 0x000CF086 File Offset: 0x000CE086
		public string Email
		{
			get
			{
				if (this.d != null && this.d.ParentCollection != null && this.d.ParentCollection.MimePart != null && this.d.ParentCollection.MimePart.ParentMessage != null)
				{
					return this.d.ParentCollection.MimePart.ParentMessage.f(this.b);
				}
				return this.b;
			}
			set
			{
				this.b = value;
				this.a();
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x000CF098 File Offset: 0x000CE098
		// (set) Token: 0x06002BD0 RID: 11216 RVA: 0x000CF10A File Offset: 0x000CE10A
		public string Remarks
		{
			get
			{
				if (this.d != null && this.d.ParentCollection != null && this.d.ParentCollection.MimePart != null && this.d.ParentCollection.MimePart.ParentMessage != null)
				{
					return this.d.ParentCollection.MimePart.ParentMessage.f(this.c);
				}
				return this.c;
			}
			set
			{
				this.c = value;
				this.a();
			}
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000CF11C File Offset: 0x000CE11C
		public static string GetAccountNameFromEmail(string email)
		{
			if (email == null)
			{
				return null;
			}
			int num = email.IndexOf('@');
			if (num < 0)
			{
				return email;
			}
			return email.Substring(0, num);
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000CF148 File Offset: 0x000CE148
		public static string GetDomainFromEmail(string email)
		{
			if (email == null)
			{
				return null;
			}
			int num = email.IndexOf('@');
			if (num < 0)
			{
				return string.Empty;
			}
			return email.Substring(num + 1);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000CF176 File Offset: 0x000CE176
		public string GetAccountName()
		{
			return EmailAddress.GetAccountNameFromEmail(this.b);
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000CF183 File Offset: 0x000CE183
		public string GetDomain()
		{
			return EmailAddress.GetDomainFromEmail(this.b);
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000CF190 File Offset: 0x000CE190
		// (set) Token: 0x06002BD6 RID: 11222 RVA: 0x000CF198 File Offset: 0x000CE198
		internal Header EmailAddressHeader
		{
			get
			{
				return this.d;
			}
			set
			{
				this.d = value;
			}
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000CF1A4 File Offset: 0x000CE1A4
		public override string ToString()
		{
			string text = string.Empty;
			if (this.a != null && this.a.Length != 0 && this.c != null && this.c.Length != 0)
			{
				text = string.Format(CultureInfo.InvariantCulture, "\"{0}\" <{1}> ({2})", new object[]
				{
					global::a.i.k.a(this.a, new char[]
					{
						'"',
						'\\'
					}),
					this.b,
					global::a.i.k.a(this.c, new char[]
					{
						'(',
						')',
						'\\'
					})
				});
			}
			else if (this.a != null && this.a.Length != 0)
			{
				text = string.Format(CultureInfo.InvariantCulture, "\"{0}\" <{1}>", new object[]
				{
					global::a.i.k.a(this.a, new char[]
					{
						'"',
						'\\'
					}),
					this.b
				});
			}
			else if (this.c != null && this.c.Length != 0)
			{
				text = string.Format(CultureInfo.InvariantCulture, "{1} ({0})", new object[]
				{
					global::a.i.k.a(this.c, new char[]
					{
						'"',
						'\\'
					}),
					EmailAddress.b(this.b)
				});
			}
			else if (this.b != null && this.b.Length != 0)
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					EmailAddress.b(this.b)
				});
			}
			if (this.d != null && this.d.ParentCollection != null && this.d.ParentCollection.MimePart != null && this.d.ParentCollection.MimePart.ParentMessage != null && this.d.ParentCollection.MimePart.ParentMessage.Parser != null && this.d.ParentCollection.MimePart.ParentMessage.Parser.HeadersAsHtml)
			{
				return global::a.i.b.j(text);
			}
			return text;
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000CF3B1 File Offset: 0x000CE3B1
		public static implicit operator string(EmailAddress email)
		{
			if (email != null)
			{
				return email.ToString();
			}
			return null;
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000CF3C0 File Offset: 0x000CE3C0
		internal string a(MailTransferEncoding A_0, string A_1)
		{
			string result = string.Empty;
			if (this.a != null && this.a.Length != 0 && this.c != null && this.c.Length != 0)
			{
				result = string.Format(CultureInfo.InvariantCulture, "\"{0}\" <{1}> ({2})", new object[]
				{
					global::a.i.h.a(this.a, A_0, A_1, HeaderEncodingOptions.None),
					this.b,
					global::a.i.h.a(this.c, A_0, A_1, HeaderEncodingOptions.None)
				});
			}
			else if (this.a != null && this.a.Length != 0)
			{
				result = string.Format(CultureInfo.InvariantCulture, "\"{0}\" <{1}>", new object[]
				{
					global::a.i.h.a(this.a, A_0, A_1, HeaderEncodingOptions.None),
					this.b
				});
			}
			else if (this.b != null && this.b.Length != 0)
			{
				string text = EmailAddress.b(this.b);
				if (A_1 != null)
				{
					byte[] bytes = bb.a(A_1).GetBytes(this.b);
					text = Global.DefaultEncoding.GetString(bytes, 0, bytes.Length);
				}
				result = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
				{
					text
				});
			}
			return result;
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000CF4EB File Offset: 0x000CE4EB
		private static string b(string A_0)
		{
			if (A_0.IndexOfAny(new char[]
			{
				' ',
				'\t'
			}) >= 0)
			{
				return string.Format("<{0}>", A_0);
			}
			return A_0;
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000CF513 File Offset: 0x000CE513
		public static EmailAddress Parse(string addressString)
		{
			return EmailAddress.a(addressString, null);
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000CF51C File Offset: 0x000CE51C
		internal static EmailAddress a(string A_0, Header A_1)
		{
			EmailAddress emailAddress = new EmailAddress();
			if (A_0 == null)
			{
				return emailAddress;
			}
			emailAddress.EmailAddressHeader = A_1;
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num = 0;
			for (int i = 0; i < A_0.Length; i++)
			{
				char c = A_0[i];
				if (c <= '(')
				{
					if (c != '"')
					{
						if (c == '(')
						{
							if (!flag && !flag2 && !flag3)
							{
								flag3 = true;
								num = i;
							}
						}
					}
					else if (!flag && !flag2 && !flag3)
					{
						flag = true;
						num = i;
					}
					else if (!flag2 && !flag3)
					{
						int num2 = i;
						text = A_0.Substring(num + 1, num2 - num - 1);
						A_0 = A_0.Remove(num, num2 - num + 1);
						i = 0;
						num = 0;
						flag = false;
					}
				}
				else if (c != ')')
				{
					switch (c)
					{
					case '<':
						if (!flag && !flag2 && !flag3)
						{
							flag2 = true;
							num = i;
						}
						break;
					case '=':
						if (i < A_0.Length - 2 && A_0[i + 1] == '?')
						{
							while (i < A_0.Length - 2)
							{
								int num3 = A_0.IndexOf("?=", i + 2);
								if (num3 <= -1)
								{
									break;
								}
								if (num3 <= 2 || A_0[num3 - 2] != '?')
								{
									i = num3 + 1;
									break;
								}
								i = num3;
							}
						}
						break;
					case '>':
						if (flag2)
						{
							int num2 = i;
							text2 = A_0.Substring(num + 1, num2 - num - 1);
							A_0 = A_0.Remove(num, num2 - num + 1);
							A_0 = A_0.Insert(num, " ");
							i = 0;
							num = 0;
							flag2 = false;
						}
						break;
					default:
						if (c == '\\')
						{
							i++;
						}
						break;
					}
				}
				else if (flag3)
				{
					int num2 = i;
					text3 = A_0.Substring(num + 1, num2 - num - 1);
					A_0 = A_0.Remove(num, num2 - num + 1);
					i = 0;
					num = 0;
					flag3 = false;
				}
			}
			if (text2.Length > 0 && text.Length == 0)
			{
				if (text3.Length == 0)
				{
					text = A_0.Replace(text2, string.Empty);
				}
				else
				{
					text = A_0.Replace(text2, string.Empty).Replace(text3, string.Empty).Trim();
				}
			}
			if (text2.Length == 0)
			{
				string[] array = new string[]
				{
					A_0,
					text,
					text3
				};
				for (int j = 0; j < array.Length; j++)
				{
					Match match = global::a.i.m.a.Match(array[j]);
					if (match != null && match.Success)
					{
						text2 = match.Value;
						if (array[j] == text)
						{
							text = string.Empty;
						}
						if (array[j] == text3)
						{
							text3 = string.Empty;
						}
					}
					else
					{
						text2 = A_0;
					}
					if (text2.Length > 0)
					{
						break;
					}
				}
				if (text2.Length == 0)
				{
					text2 = A_0;
				}
			}
			Encoding a_ = null;
			if (A_1 != null && A_1.ParentCollection != null && A_1.ParentCollection.MimePart != null && A_1.ParentCollection.MimePart.ParentMessage != null && A_1.ParentCollection.MimePart.ParentMessage.Parser != null)
			{
				a_ = A_1.ParentCollection.MimePart.ParentMessage.Parser.EncodingOverride;
			}
			string text4 = EmailAddress.a(text2, a_);
			if (string.Compare(text4, EmailAddress.a(text2), true) != 0)
			{
				return EmailAddress.a(text4, A_1);
			}
			emailAddress.b = text2.Trim(new char[]
			{
				' ',
				'<',
				'>'
			}).Replace("\r", "").Replace("\n", "");
			emailAddress.a = EmailAddress.a(text.Trim(), a_);
			emailAddress.c = EmailAddress.a(text3.Trim(), a_);
			return emailAddress;
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000CF918 File Offset: 0x000CE918
		private static string a(string A_0, Encoding A_1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			Match match = global::a.i.m.b.Match(A_0);
			if (match.Success)
			{
				while (match.Success)
				{
					int length = match.Index - num;
					stringBuilder.Append(EmailAddress.a(A_0.Substring(num, length)));
					num = match.Index + match.Length;
					string text = match.Value;
					text = global::a.i.h.a(text, A_1);
					if (text.Length >= 2 && text.StartsWith("\"") && text.EndsWith("\""))
					{
						text = EmailAddress.a(text);
					}
					stringBuilder.Append(text);
					match = match.NextMatch();
				}
				if (num < A_0.Length - 1)
				{
					stringBuilder.Append(EmailAddress.a(A_0.Substring(num)));
				}
			}
			else
			{
				stringBuilder.Append(EmailAddress.a(A_0));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000CFA04 File Offset: 0x000CEA04
		private static string a(string A_0)
		{
			if (A_0 == null || A_0 == string.Empty)
			{
				return string.Empty;
			}
			return A_0.Replace("\\\"", "\"").Replace("\\(", "(").Replace("\\)", ")").Replace("\\\\", "\\");
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000CFA64 File Offset: 0x000CEA64
		internal void a()
		{
			if (this.d != null)
			{
				this.d.d();
			}
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000CFA7C File Offset: 0x000CEA7C
		internal void b()
		{
			if (this.d != null)
			{
				EmailAddress emailAddress = EmailAddress.Parse(this.d.Value.Replace("\r\n", string.Empty));
				this.b = emailAddress.Email;
				this.a = emailAddress.DisplayName;
				this.c = emailAddress.Remarks;
			}
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000CFAD8 File Offset: 0x000CEAD8
		public static string EscapeIdnDomain(string email)
		{
			if (email == null)
			{
				throw new ArgumentNullException("email");
			}
			IdnMapping idnMapping = new IdnMapping();
			int num = email.LastIndexOf('@');
			if (num < 0)
			{
				return email;
			}
			return email.Substring(0, num + 1) + idnMapping.GetAscii(email.Substring(num + 1));
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000CFB28 File Offset: 0x000CEB28
		public static string UnescapeIdnDomain(string email)
		{
			if (email == null)
			{
				throw new ArgumentNullException("email");
			}
			IdnMapping idnMapping = new IdnMapping();
			int num = email.LastIndexOf('@');
			if (num < 0)
			{
				return email;
			}
			return email.Substring(0, num + 1) + idnMapping.GetUnicode(email.Substring(num + 1));
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000CFB76 File Offset: 0x000CEB76
		public EmailAddress ToIdnAddress()
		{
			return new EmailAddress(EmailAddress.EscapeIdnDomain(this.Email), this.DisplayName, this.Remarks);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000CFB94 File Offset: 0x000CEB94
		public EmailAddress FromIdnAddress()
		{
			return new EmailAddress(EmailAddress.UnescapeIdnDomain(this.Email), this.DisplayName, this.Remarks);
		}

		// Token: 0x04001E1B RID: 7707
		private string a = string.Empty;

		// Token: 0x04001E1C RID: 7708
		private string b = string.Empty;

		// Token: 0x04001E1D RID: 7709
		private string c = string.Empty;

		// Token: 0x04001E1E RID: 7710
		private Header d;
	}
}
