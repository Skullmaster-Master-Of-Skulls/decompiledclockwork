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
	// Token: 0x0200019F RID: 415
	internal class al : DomainKeys
	{
		// Token: 0x06000EC8 RID: 3784 RVA: 0x00037656 File Offset: 0x00036656
		public al()
		{
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003765E File Offset: 0x0003665E
		public al(int A_0, bool A_1)
		{
			this.d = A_0;
			this.c = A_1;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00037674 File Offset: 0x00036674
		public HashAlgorithm a(out string A_0)
		{
			if (Global.FipsMode)
			{
				A_0 = "sha256";
				return new SHA256CryptoServiceProvider();
			}
			A_0 = "sha256";
			HashAlgorithm result;
			try
			{
				result = new SHA256Managed();
			}
			catch (InvalidOperationException)
			{
				result = new SHA256CryptoServiceProvider();
			}
			return result;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x000376C0 File Offset: 0x000366C0
		public MailMessage a(MailMessage A_0, bool A_1, string[] A_2, byte[] A_3, string A_4, ref RSACryptoServiceProvider A_5)
		{
			if (A_0 == null || A_3 == null || A_0.Headers == null || A_4 == null)
			{
				this.d = 21;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			Header header = A_0.Headers.a("Sender");
			if (header == null || header.Value == string.Empty)
			{
				header = A_0.Headers.a("X-Sender");
				if (header == null || header.Value == string.Empty)
				{
					header = A_0.Headers.a("From");
					if (header == null || header.Value == string.Empty)
					{
						this.d = 20;
						throw new MailBeeInvalidArgumentException(this.d);
					}
				}
			}
			string domain = EmailAddress.Parse(header.Value).GetDomain();
			if (domain == string.Empty)
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
					this.d = 20;
					throw new MailBeeInvalidArgumentException(this.d);
				}
			}
			this.d = 0;
			m m = m.b;
			m m2 = m.b;
			byte[] messageRawData = A_0.GetMessageRawData();
			int a_ = -1;
			string a_2 = this.a(m2, A_0, a_);
			string text;
			HashAlgorithm a_3 = this.a(out text);
			string text2 = Convert.ToBase64String(base.a(a_3, a_2));
			string text3 = null;
			string text4 = this.a(m, A_0, A_2, out text3, true);
			string text5 = string.Format("DKIM-Signature: v=1; a=rsa-" + text + "; bh={0};\r\n c={1}/{2}; d={3}; s={5};\r\n h={4};\r\n b=", new object[]
			{
				text2,
				(m == m.a) ? "simple" : "relaxed",
				(m2 == m.a) ? "simple" : "relaxed",
				domain,
				text3,
				A_4
			});
			text4 += ((m == m.b) ? this.b(text5).TrimEnd(null) : text5);
			byte[] rgbHash = base.a(a_3, text4, bb.a(A_0.Charset));
			if (A_5 == null)
			{
				try
				{
					A_5 = base.c(A_3);
				}
				catch (CryptographicException a_4)
				{
					this.d = 35;
					if (this.c)
					{
						throw new MailBeeDomainKeysException(this.d, a_4);
					}
					return null;
				}
			}
			if (A_5 == null)
			{
				this.d = 1130;
				if (this.c)
				{
					throw new MailBeeDomainKeysException(this.d);
				}
				return null;
			}
			else
			{
				string str = CryptoConfig.MapNameToOID(text);
				byte[] inArray = new byte[0];
				try
				{
					inArray = A_5.SignHash(rgbHash, str);
				}
				catch (CryptographicException a_5)
				{
					this.d = 35;
					if (this.c)
					{
						throw new MailBeeDomainKeysException(this.d, a_5);
					}
					return null;
				}
				string str2 = Convert.ToBase64String(inArray);
				text5 = text5 + str2 + "\r\n";
				byte[] bytes = Encoding.GetEncoding(1252).GetBytes(text5);
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
			MailMessage result;
			return result;
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00037A0C File Offset: 0x00036A0C
		private DomainKeysVerifyResult a(j A_0)
		{
			if (A_0.b("v") == null || A_0.b("a") == null || A_0.b("b") == null || A_0.b("bh") == null || A_0.b("d") == null || A_0.b("h") == null || A_0.b("s") == null)
			{
				return DomainKeysVerifyResult.SignatureInvalidTag;
			}
			if (A_0.b("i") != null)
			{
				string text = A_0.b("i").c();
				string value = A_0.b("d").c();
				if (!text.EndsWith(value))
				{
					return DomainKeysVerifyResult.SignatureInvalidTag;
				}
			}
			string[] array = A_0.b("h").c().Split(new char[]
			{
				':'
			});
			bool flag = false;
			foreach (string text2 in array)
			{
				if (string.Compare("from", text2.Trim(), true) == 0)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return DomainKeysVerifyResult.SignatureInvalidTag;
			}
			if (A_0.b("x") != null)
			{
				uint num = 0U;
				string s = A_0.b("x").c();
				try
				{
					num = uint.Parse(s);
				}
				catch (Exception)
				{
					return DomainKeysVerifyResult.SignatureInvalidTag;
				}
				DateTime t = new DateTime(1970, 1, 1, 0, 0, 0);
				t = t.AddSeconds(num);
				if (t < DateTime.Now)
				{
					return DomainKeysVerifyResult.SignatureExpired;
				}
				return DomainKeysVerifyResult.OK;
			}
			return DomainKeysVerifyResult.OK;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00037B78 File Offset: 0x00036B78
		private DomainKeysVerifyResult a(MailMessage A_0, Header A_1, j A_2, j A_3)
		{
			if (A_3 == null || A_3.b("p") == null)
			{
				return DomainKeysVerifyResult.DnsEntryInvalidTag;
			}
			string text = "rsa-sha256";
			if (A_2.b("a") != null)
			{
				text = A_2.b("a").c();
			}
			if (A_3.b("k") != null)
			{
				if (string.Compare(A_3.b("k").c(), "rsa", true) != 0)
				{
					return DomainKeysVerifyResult.DnsEntryInvalidTag;
				}
				if (string.Compare(text, "rsa-sha1", true) != 0 && string.Compare(text, "rsa-sha256", true) != 0)
				{
					return DomainKeysVerifyResult.SignatureInvalidTag;
				}
			}
			int a_ = 0;
			m m = m.a;
			m a_2 = m.a;
			if (A_2.b("l") != null)
			{
				try
				{
					a_ = int.Parse(A_2.b("l").c());
				}
				catch (Exception)
				{
					return DomainKeysVerifyResult.SignatureInvalidTag;
				}
			}
			if (A_2.b("c") != null)
			{
				string[] array = A_2.b("c").c().Split(new char[]
				{
					'/'
				});
				if (array.Length > 1)
				{
					m = ((string.Compare(array[0], "simple", true) == 0) ? m.a : m.b);
					a_2 = ((string.Compare(array[1], "simple", true) == 0) ? m.a : m.b);
				}
				else
				{
					m = ((string.Compare(array[0], "simple", true) == 0) ? m.a : m.b);
				}
			}
			string text2 = this.a(a_2, A_0, a_);
			string text3 = null;
			string text4 = (A_2.b("h") != null) ? A_2.b("h").c() : null;
			string text5 = this.a(m, A_0, text4.Split(new char[]
			{
				':'
			}), out text3, false);
			if (text5 == null || text2 == null)
			{
				return DomainKeysVerifyResult.SignatureInvalidTag;
			}
			text5 += this.a(A_1.RawBody.c(), m);
			byte[] array2 = null;
			string text6 = base.c(text);
			if (text6 == null)
			{
				return DomainKeysVerifyResult.SignatureInvalidTag;
			}
			HashAlgorithm hashAlgorithm = null;
			try
			{
				hashAlgorithm = base.e(text6);
				if (hashAlgorithm == null)
				{
					return DomainKeysVerifyResult.SignatureInvalidTag;
				}
				array2 = base.a(hashAlgorithm, text2);
			}
			catch (InvalidOperationException a_3)
			{
				this.d = 36;
				if (this.c)
				{
					throw new MailBeeDomainKeysException(this.d, a_3);
				}
				return DomainKeysVerifyResult.Sha256NotSupported;
			}
			if (array2 == null)
			{
				return DomainKeysVerifyResult.SignatureInvalidTag;
			}
			if (A_2.b("b") == null || A_2.b("bh") == null)
			{
				return DomainKeysVerifyResult.SignatureInvalidTag;
			}
			byte[] array3 = new byte[0];
			array3 = h.b(Encoding.ASCII.GetBytes(A_2.b("bh").c()));
			if (array2.Length != array3.Length)
			{
				return DomainKeysVerifyResult.SignatureInvalid;
			}
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] != array3[i])
				{
					return DomainKeysVerifyResult.SignatureInvalid;
				}
			}
			byte[] a_4 = h.b(Encoding.ASCII.GetBytes(A_2.b("b").c()));
			byte[] array4 = h.b(Encoding.ASCII.GetBytes(A_3.b("p").c()));
			RSACryptoServiceProvider rsacryptoServiceProvider = DomainKeys.b(array4);
			if (rsacryptoServiceProvider == null)
			{
				return DomainKeysVerifyResult.PublicKeyBadFormat;
			}
			try
			{
				array2 = base.a(hashAlgorithm, text5, bb.a(A_0.Charset));
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
			if (!base.a(rsacryptoServiceProvider, a_4, array2, text, array4))
			{
				return DomainKeysVerifyResult.SignatureInvalid;
			}
			return DomainKeysVerifyResult.OK;
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00037EEC File Offset: 0x00036EEC
		public DomainKeysVerifyResult b(MailMessage A_0, Smtp A_1, Header A_2)
		{
			if (A_0 == null || A_0.Headers == null || A_0.Headers == null)
			{
				this.d = 21;
				throw new MailBeeInvalidArgumentException(this.d);
			}
			this.d = 0;
			if (A_2 == null)
			{
				HeaderCollection headerCollection = A_0.Headers.Items("DKIM-Signature");
				if (headerCollection == null)
				{
					return DomainKeysVerifyResult.MessageNotSigned;
				}
				A_2 = ((headerCollection.Count > 0) ? headerCollection[headerCollection.Count - 1] : null);
			}
			if (A_2 == null)
			{
				return DomainKeysVerifyResult.MessageNotSigned;
			}
			j j = j.a(A_2.Value);
			DomainKeysVerifyResult domainKeysVerifyResult = this.a(j);
			if (domainKeysVerifyResult != DomainKeysVerifyResult.OK)
			{
				return domainKeysVerifyResult;
			}
			j a_ = null;
			j.b("s").c();
			j.b("d").c();
			try
			{
				a_ = base.a(A_1, j.b("s").c(), j.b("d").c());
			}
			catch (MailBeeNetworkException ex)
			{
				this.d = ex.ErrorCode;
				if (ex is MailBeeDnsProtocolNegativeResponseException || ex is MailBeeDnsRecordsDisabledException)
				{
					return DomainKeysVerifyResult.DomainInvalid;
				}
				if (this.c)
				{
					throw;
				}
				return DomainKeysVerifyResult.DnsQueryFailed;
			}
			return this.a(A_0, A_2, j, a_);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0003801C File Offset: 0x0003701C
		private string a(string A_0, m A_1)
		{
			Match match = new Regex("(;|\\s)b(\\s)*=(?<signature>(\\s)*[^;]+)", RegexOptions.Singleline).Match(A_0);
			if (match.Success)
			{
				A_0 = A_0.Remove(match.Groups["signature"].Index, match.Groups["signature"].Length);
			}
			if (A_1 == m.b)
			{
				A_0 = this.b(A_0);
			}
			return A_0.TrimEnd(null);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003808C File Offset: 0x0003708C
		private string a(m A_0, MailMessage A_1, string[] A_2, out string A_3, bool A_4)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			HeaderCollection headers = A_1.Headers;
			if (A_2 == null)
			{
				A_2 = new string[A_1.Headers.Count];
				for (int i = 0; i < A_1.Headers.Count; i++)
				{
					A_2[i] = A_1.Headers[i].Name;
				}
			}
			string[] array = new string[A_2.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = null;
			}
			int num = 0;
			Hashtable hashtable = new Hashtable();
			foreach (string text in A_2)
			{
				Header header = null;
				string text2 = text.Trim().ToLower();
				int num2 = headers.Count - 1;
				if (hashtable.ContainsKey(text2))
				{
					num2 = (int)hashtable[text2];
				}
				num2 = headers.a(text2, num2);
				if (num2 > -1)
				{
					header = headers[num2];
					hashtable[text2] = num2 - 1;
				}
				if (header != null && (!A_4 || (A_4 && this.a(header.Name))))
				{
					stringBuilder.AppendFormat(null, "{0}:", new object[]
					{
						header.Name
					});
					if (A_0 == m.a)
					{
						stringBuilder2.AppendFormat(null, "{0}\r\n", new object[]
						{
							header.RawBody.c()
						});
					}
					else if (A_0 == m.b)
					{
						stringBuilder2.Append(this.b(header.RawBody.c()));
					}
					array[num] = text2;
					num++;
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 1, 1);
			}
			A_3 = stringBuilder.ToString();
			return stringBuilder2.ToString();
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00038250 File Offset: 0x00037250
		private string b(string A_0)
		{
			int num = A_0.IndexOf(':');
			string arg = (num > 0) ? A_0.Substring(0, num).ToLower().TrimEnd(null) : string.Empty;
			string text = (num > 0) ? A_0.Substring(num + 1) : string.Empty;
			text = new Regex("\\r\\n").Replace(text, string.Empty);
			text = new Regex("[\\s]+").Replace(text, " ");
			text = text.Trim();
			return string.Format("{0}:{1}\r\n", arg, text);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000382DC File Offset: 0x000372DC
		private bool a(string A_0)
		{
			bool result = true;
			if (string.Compare(A_0, "X-Sender", true) == 0)
			{
				return true;
			}
			if (A_0.StartsWith("X-") || A_0.StartsWith("x-") || string.Compare(A_0, "Authentication-Results", true) == 0 || string.Compare(A_0, "Bcc", true) == 0 || string.Compare(A_0, "Resent-Bcc", true) == 0 || string.Compare(A_0, "Return-Path", true) == 0)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00038350 File Offset: 0x00037350
		private string a(m A_0, MailMessage A_1, int A_2)
		{
			string text = Encoding.GetEncoding(1252).GetString(A_1.MimePart.RawBody, 0, A_1.MimePart.RawBody.Length).TrimStart(new char[]
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
			if (A_0 == m.a)
			{
				text = text.TrimEnd(new char[]
				{
					'\r',
					'\n'
				}) + "\r\n";
			}
			else if (A_0 == m.b)
			{
				text = new Regex("[\\f\\t\\v ]+\\r\\n").Replace(text, "\r\n");
				text = new Regex("[\\f\\t\\v ]+").Replace(text, " ");
				string text2 = text.TrimEnd(new char[]
				{
					'\r',
					'\n'
				});
				if (text2.Length > 0)
				{
					text2 += "\r\n";
				}
				text = text2;
			}
			else
			{
				text = null;
			}
			if (text != null && A_2 > 0 && A_2 < text.Length)
			{
				text = text.Substring(0, A_2);
			}
			return text;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00038484 File Offset: 0x00037484
		public Task<DomainKeysVerifyResult> a(MailMessage A_0, Smtp A_1, Header A_2)
		{
			al.a a;
			a.d = this;
			a.c = A_0;
			a.f = A_1;
			a.e = A_2;
			a.b = AsyncTaskMethodBuilder<DomainKeysVerifyResult>.Create();
			a.a = -1;
			AsyncTaskMethodBuilder<DomainKeysVerifyResult> b = a.b;
			b.Start<al.a>(ref a);
			return a.b.Task;
		}
	}
}
