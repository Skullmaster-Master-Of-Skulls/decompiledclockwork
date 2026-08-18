using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using a.i;
using MailBee;
using MailBee.DnsMX;
using MailBee.Mime;
using MailBee.Security;
using MailBee.SmtpMail;

namespace a.j
{
	// Token: 0x0200019C RID: 412
	internal class ae : DomainKeys
	{
		// Token: 0x06000EBC RID: 3772 RVA: 0x000368BC File Offset: 0x000358BC
		public ae()
		{
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x000368C4 File Offset: 0x000358C4
		public ae(int A_0, bool A_1)
		{
			this.d = A_0;
			this.c = A_1;
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000368DC File Offset: 0x000358DC
		private HashAlgorithm a()
		{
			if (Global.FipsMode)
			{
				return new SHA1CryptoServiceProvider();
			}
			HashAlgorithm result;
			try
			{
				result = new SHA1Managed();
			}
			catch (InvalidOperationException)
			{
				result = new SHA1CryptoServiceProvider();
			}
			return result;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0003691C File Offset: 0x0003591C
		public MailMessage a(MailMessage A_0, bool A_1, string[] A_2, byte[] A_3, string A_4, ref RSACryptoServiceProvider A_5)
		{
			if (A_0 == null || A_3 == null || A_0.Headers == null || A_4 == null)
			{
				this.d = 21;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			string text = null;
			Header header = A_0.Headers.a("Sender");
			if (header == null || header.Value == string.Empty)
			{
				header = A_0.Headers.a("X-Sender");
				if (header == null || header.Value == string.Empty)
				{
					header = A_0.Headers.a("From");
					if (header == null || header.Value == string.Empty)
					{
						this.d = 312;
						throw new MailBeeInvalidArgumentException(this.d);
					}
				}
			}
			text = EmailAddress.Parse(header.Value).GetDomain();
			if (text == string.Empty)
			{
				this.d = 20;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			if (A_2 != null)
			{
				bool flag = false;
				for (int i = 0; i < A_2.Length; i++)
				{
					if (string.Compare(A_2[i], header.Name, true) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.d = 312;
					throw new MailBeeInvalidArgumentException(this.d);
				}
			}
			this.d = 0;
			af af = af.a;
			byte[] messageRawData = A_0.GetMessageRawData();
			string text2 = (A_2 != null) ? string.Join(":", A_2) : null;
			string a_ = this.a(A_0, af, ref text2, true);
			byte[] rgbHash = base.a(this.a(), a_, bb.a(A_0.Charset));
			if (A_5 == null)
			{
				A_5 = base.c(A_3);
			}
			if (A_5 != null)
			{
				string str = CryptoConfig.MapNameToOID("SHA1");
				byte[] inArray = new byte[0];
				try
				{
					inArray = A_5.SignHash(rgbHash, str);
				}
				catch (CryptographicException a_2)
				{
					this.d = 35;
					if (this.c)
					{
						throw new MailBeeDomainKeysException(this.d, a_2);
					}
					return null;
				}
				string text3 = Convert.ToBase64String(inArray);
				string s = string.Format("DomainKey-Signature: q=dns; a=rsa-sha1; c={0};\r\n d={1}; s={2};\r\n h={3};\r\n b={4}\r\n", new object[]
				{
					(af == af.a) ? "simple" : "relaxed",
					text,
					A_4,
					text2,
					text3
				});
				byte[] bytes = Global.DefaultEncoding.GetBytes(s);
				byte[] array = new byte[bytes.Length + messageRawData.Length];
				Array.Copy(bytes, 0, array, 0, bytes.Length);
				Array.Copy(messageRawData, 0, array, bytes.Length, messageRawData.Length);
				if (A_1)
				{
					return new MailMessage(array);
				}
				A_0.LoadMessage(array);
				return A_0;
			}
			this.d = 1130;
			if (this.c)
			{
				throw new MailBeeDomainKeysException(this.d);
			}
			return null;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00036BD0 File Offset: 0x00035BD0
		private DomainKeysVerifyResult a(MailMessage A_0, j A_1)
		{
			if (A_1.b("b") == null || A_1.b("c") == null || A_1.b("d") == null || A_1.b("s") == null)
			{
				return DomainKeysVerifyResult.SignatureInvalidTag;
			}
			Header header = A_0.Headers.a("Sender");
			if (header == null)
			{
				header = A_0.Headers.a("X-Sender");
				if (header == null)
				{
					header = A_0.Headers.a("From");
					if (header == null)
					{
						return DomainKeysVerifyResult.SignatureInvalidTag;
					}
				}
			}
			if (string.Compare(EmailAddress.Parse(header.Value).GetDomain(), A_1.b("d").c(), true) != 0)
			{
				return DomainKeysVerifyResult.SignatureInvalidTag;
			}
			if (A_1.b("h") != null)
			{
				string[] array = A_1.b("h").c().Split(new char[]
				{
					':'
				});
				bool flag = false;
				foreach (string text in array)
				{
					if (string.Compare("From", text.Trim(), true) == 0 || string.Compare("Sender", text.Trim(), true) == 0 || string.Compare("X-Sender", text.Trim(), true) == 0)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return DomainKeysVerifyResult.SignatureInvalidTag;
				}
			}
			return DomainKeysVerifyResult.OK;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00036D04 File Offset: 0x00035D04
		private DomainKeysVerifyResult a(MailMessage A_0, j A_1, j A_2)
		{
			if (A_2 == null || A_2.b("p") == null)
			{
				return DomainKeysVerifyResult.DnsEntryInvalidTag;
			}
			if (A_2.b("k") != null && string.Compare(A_2.b("k").c(), "rsa", true) != 0)
			{
				return DomainKeysVerifyResult.DnsEntryInvalidTag;
			}
			string text = "rsa-sha1";
			if (A_1.b("a") != null)
			{
				text = A_1.b("a").c();
				if (string.Compare(text, "rsa-sha1", true) != 0)
				{
					return DomainKeysVerifyResult.SignatureInvalidTag;
				}
			}
			if (A_1.b("b") == null)
			{
				return DomainKeysVerifyResult.SignatureInvalidTag;
			}
			af a_ = af.a;
			if (string.Compare(A_1.b("c").c(), "nofws", true) == 0)
			{
				a_ = af.b;
			}
			string text2 = (A_1.b("h") != null) ? A_1.b("h").c() : null;
			string a_2 = this.a(A_0, a_, ref text2, false);
			byte[] a_3 = h.b(Encoding.ASCII.GetBytes(A_1.b("b").c()));
			byte[] array = h.b(Encoding.ASCII.GetBytes(A_2.b("p").c()));
			RSACryptoServiceProvider rsacryptoServiceProvider = DomainKeys.b(array);
			if (rsacryptoServiceProvider == null)
			{
				return DomainKeysVerifyResult.PublicKeyBadFormat;
			}
			string text3 = base.c(text);
			if (text3 == null)
			{
				return DomainKeysVerifyResult.SignatureInvalidTag;
			}
			try
			{
				HashAlgorithm hashAlgorithm = base.e(text3);
				if (hashAlgorithm == null)
				{
					return DomainKeysVerifyResult.SignatureInvalidTag;
				}
				byte[] a_4 = base.a(hashAlgorithm, a_2, bb.a(A_0.Charset));
				if (!base.a(rsacryptoServiceProvider, a_3, a_4, text, array))
				{
					return DomainKeysVerifyResult.SignatureInvalid;
				}
			}
			catch (CryptographicException)
			{
				return DomainKeysVerifyResult.SignatureInvalid;
			}
			catch (InvalidOperationException a_5)
			{
				this.d = 36;
				if (this.c)
				{
					throw new MailBeeDomainKeysException(this.d, a_5);
				}
				return DomainKeysVerifyResult.Sha256NotSupported;
			}
			return DomainKeysVerifyResult.OK;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x00036ED0 File Offset: 0x00035ED0
		public DomainKeysVerifyResult b(MailMessage A_0, Smtp A_1, Header A_2)
		{
			if (A_0 == null || A_0.Headers == null || A_1 == null)
			{
				this.d = 21;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			this.d = 0;
			if (A_2 == null)
			{
				A_2 = A_0.Headers.a("DomainKey-Signature");
			}
			if (A_2 == null)
			{
				return DomainKeysVerifyResult.MessageNotSigned;
			}
			j j = j.a(A_2.Value);
			DomainKeysVerifyResult domainKeysVerifyResult = this.a(A_0, j);
			if (domainKeysVerifyResult != DomainKeysVerifyResult.OK)
			{
				return domainKeysVerifyResult;
			}
			j a_ = null;
			string a_2 = j.b("s").c();
			string a_3 = j.b("d").c();
			try
			{
				a_ = base.a(A_1, a_2, a_3);
			}
			catch (MailBeeNetworkException ex)
			{
				this.d = ex.ErrorCode;
				if (ex is MailBeeDnsProtocolNegativeResponseException)
				{
					return DomainKeysVerifyResult.DomainInvalid;
				}
				if (this.c)
				{
					throw;
				}
				return DomainKeysVerifyResult.DnsQueryFailed;
			}
			return this.a(A_0, j, a_);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x00036FB4 File Offset: 0x00035FB4
		private string a(MailMessage A_0, af A_1, ref string A_2, bool A_3)
		{
			StringBuilder stringBuilder = new StringBuilder();
			HeaderCollection headerCollection = A_3 ? A_0.Headers : base.a(A_0.Headers, "DomainKey-Signature");
			StringBuilder stringBuilder2 = new StringBuilder();
			if (A_2 != null)
			{
				string[] array = A_2.Split(new char[]
				{
					':'
				});
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = array[i].Trim().ToLower();
				}
				using (IEnumerator enumerator = headerCollection.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Header header = (Header)obj;
						if (Array.IndexOf<string>(array, header.Name.ToLower()) >= 0)
						{
							stringBuilder.Append(this.a(A_1, header));
							stringBuilder2.AppendFormat(null, "{0}:", new object[]
							{
								header.Name
							});
						}
					}
					goto IL_14C;
				}
			}
			foreach (object obj2 in headerCollection)
			{
				Header header2 = (Header)obj2;
				stringBuilder.Append(this.a(A_1, header2));
				stringBuilder2.AppendFormat(null, "{0}:", new object[]
				{
					header2.Name
				});
			}
			IL_14C:
			if (stringBuilder2.Length > 0)
			{
				stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
			}
			if (A_3)
			{
				A_2 = stringBuilder2.ToString();
			}
			string text = bb.a(A_0.Charset).GetString(A_0.MimePart.RawBody, 0, A_0.MimePart.RawBody.Length).TrimStart(new char[]
			{
				'\t',
				'\r',
				'\n',
				' '
			});
			Match match = new Regex("(\\r)?\\n(\\r)?\\n").Match(text);
			int num = match.Index + match.Length;
			if (num > 0)
			{
				if (num < text.Length)
				{
					text = text.Substring(num);
				}
				else
				{
					text = string.Empty;
				}
			}
			else
			{
				text = string.Empty;
			}
			stringBuilder.Append("\r\n");
			string[] array2 = bb.a(text, -1, true);
			for (int j = 0; j < array2.Length; j++)
			{
				if (A_1 == af.b)
				{
					char[] array3 = new char[]
					{
						'\t',
						'\n',
						'\r',
						' '
					};
					string text2 = array2[j];
					for (int k = 0; k < array3.Length; k++)
					{
						text2 = text2.Replace(array3[k].ToString(), string.Empty);
					}
					array2[j] = text2;
				}
				stringBuilder.AppendFormat(null, "{0}\r\n", new object[]
				{
					array2[j]
				});
			}
			string text3 = stringBuilder.ToString();
			string text4 = text3;
			if (A_1 == af.b)
			{
				text4 = text3.TrimEnd(new char[]
				{
					'\t',
					'\n',
					'\r',
					' '
				});
			}
			else if (A_1 == af.a)
			{
				text4 = text3.TrimEnd(new char[]
				{
					'\n',
					'\r'
				});
			}
			if (text3 != text4)
			{
				text4 += "\r\n";
			}
			text3 = text4;
			if (string.Compare(text3, "\r\n", true) == 0)
			{
				text3 = string.Empty;
			}
			return text3;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x000372F0 File Offset: 0x000362F0
		private string a(af A_0, Header A_1)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = A_1.RawBody.c();
			if (A_0 == af.a)
			{
				foreach (string text2 in bb.a(text, -1, false))
				{
					stringBuilder.AppendFormat(null, "{0}\r\n", new object[]
					{
						text2
					});
				}
			}
			else if (A_0 == af.b)
			{
				char[] array2 = new char[]
				{
					'\t',
					'\n',
					'\r',
					' '
				};
				string text3 = text;
				for (int j = 0; j < array2.Length; j++)
				{
					text3 = text3.Replace(array2[j].ToString(), string.Empty);
				}
				stringBuilder.AppendFormat(null, "{0}\r\n", new object[]
				{
					text3
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x000373B0 File Offset: 0x000363B0
		public Task<DomainKeysVerifyResult> a(MailMessage A_0, Smtp A_1, Header A_2)
		{
			ae.a a;
			a.e = this;
			a.c = A_0;
			a.d = A_1;
			a.f = A_2;
			a.b = AsyncTaskMethodBuilder<DomainKeysVerifyResult>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<DomainKeysVerifyResult> b = a.b;
			b.Start<ae.a>(ref a);
			return a.b.Task;
		}
	}
}
