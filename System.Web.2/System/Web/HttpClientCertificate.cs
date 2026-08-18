using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200008E RID: 142
	public class HttpClientCertificate : NameValueCollection
	{
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x000133D8 File Offset: 0x000115D8
		public string Cookie
		{
			get
			{
				return this._Cookie;
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x000133E0 File Offset: 0x000115E0
		public byte[] Certificate
		{
			get
			{
				return this._Certificate;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x000133E8 File Offset: 0x000115E8
		public int Flags
		{
			get
			{
				return this._Flags;
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x000133F0 File Offset: 0x000115F0
		public int KeySize
		{
			get
			{
				return this._KeySize;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x000133F8 File Offset: 0x000115F8
		public int SecretKeySize
		{
			get
			{
				return this._SecretKeySize;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00013400 File Offset: 0x00011600
		public string Issuer
		{
			get
			{
				return this._Issuer;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00013408 File Offset: 0x00011608
		public string ServerIssuer
		{
			get
			{
				return this._ServerIssuer;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00013410 File Offset: 0x00011610
		public string Subject
		{
			get
			{
				return this._Subject;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00013418 File Offset: 0x00011618
		public string ServerSubject
		{
			get
			{
				return this._ServerSubject;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00013420 File Offset: 0x00011620
		public string SerialNumber
		{
			get
			{
				return this._SerialNumber;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00013428 File Offset: 0x00011628
		public DateTime ValidFrom
		{
			get
			{
				return this._ValidFrom;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x00013430 File Offset: 0x00011630
		public DateTime ValidUntil
		{
			get
			{
				return this._ValidUntil;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00013438 File Offset: 0x00011638
		public int CertEncoding
		{
			get
			{
				return this._CertEncoding;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x00013440 File Offset: 0x00011640
		public byte[] PublicKey
		{
			get
			{
				return this._PublicKey;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00013448 File Offset: 0x00011648
		public byte[] BinaryIssuer
		{
			get
			{
				return this._BinaryIssuer;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00013450 File Offset: 0x00011650
		public bool IsPresent
		{
			get
			{
				return (this._Flags & 1) == 1;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0001345D File Offset: 0x0001165D
		public bool IsValid
		{
			get
			{
				return (this._Flags & 2) == 0;
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0001346C File Offset: 0x0001166C
		internal HttpClientCertificate(HttpContext context)
		{
			string text = context.Request.ServerVariables["CERT_FLAGS"];
			if (!string.IsNullOrEmpty(text))
			{
				this._Flags = int.Parse(text, CultureInfo.InvariantCulture);
			}
			else
			{
				this._Flags = 0;
			}
			if (!this.IsPresent)
			{
				return;
			}
			this._Cookie = context.Request.ServerVariables["CERT_COOKIE"];
			this._Issuer = context.Request.ServerVariables["CERT_ISSUER"];
			this._ServerIssuer = context.Request.ServerVariables["CERT_SERVER_ISSUER"];
			this._Subject = context.Request.ServerVariables["CERT_SUBJECT"];
			this._ServerSubject = context.Request.ServerVariables["CERT_SERVER_SUBJECT"];
			this._SerialNumber = context.Request.ServerVariables["CERT_SERIALNUMBER"];
			this._Certificate = context.WorkerRequest.GetClientCertificate();
			this._ValidFrom = context.WorkerRequest.GetClientCertificateValidFrom();
			this._ValidUntil = context.WorkerRequest.GetClientCertificateValidUntil();
			this._BinaryIssuer = context.WorkerRequest.GetClientCertificateBinaryIssuer();
			this._PublicKey = context.WorkerRequest.GetClientCertificatePublicKey();
			this._CertEncoding = context.WorkerRequest.GetClientCertificateEncoding();
			string text2 = context.Request.ServerVariables["CERT_KEYSIZE"];
			string text3 = context.Request.ServerVariables["CERT_SECRETKEYSIZE"];
			if (!string.IsNullOrEmpty(text2))
			{
				this._KeySize = int.Parse(text2, CultureInfo.InvariantCulture);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				this._SecretKeySize = int.Parse(text3, CultureInfo.InvariantCulture);
			}
			base.Add("ISSUER", null);
			base.Add("SUBJECTEMAIL", null);
			base.Add("BINARYISSUER", null);
			base.Add("FLAGS", null);
			base.Add("ISSUERO", null);
			base.Add("PUBLICKEY", null);
			base.Add("ISSUEROU", null);
			base.Add("ENCODING", null);
			base.Add("ISSUERCN", null);
			base.Add("SERIALNUMBER", null);
			base.Add("SUBJECT", null);
			base.Add("SUBJECTCN", null);
			base.Add("CERTIFICATE", null);
			base.Add("SUBJECTO", null);
			base.Add("SUBJECTOU", null);
			base.Add("VALIDUNTIL", null);
			base.Add("VALIDFROM", null);
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x00013770 File Offset: 0x00011970
		public override string Get(string field)
		{
			if (field == null)
			{
				return string.Empty;
			}
			field = field.ToLower(CultureInfo.InvariantCulture);
			uint num = <PrivateImplementationDetails>.ComputeStringHash(field);
			if (num <= 2037381814U)
			{
				if (num <= 925601446U)
				{
					if (num != 417515763U)
					{
						if (num != 695908869U)
						{
							if (num == 925601446U)
							{
								if (field == "serversubject")
								{
									return this.ServerSubject;
								}
							}
						}
						else if (field == "keysize")
						{
							return this.KeySize.ToString("G", CultureInfo.InvariantCulture);
						}
					}
					else if (field == "validuntil")
					{
						return HttpUtility.FormatHttpDateTime(this.ValidUntil);
					}
				}
				else if (num <= 1674669193U)
				{
					if (num != 1173119600U)
					{
						if (num == 1674669193U)
						{
							if (field == "validfrom")
							{
								return HttpUtility.FormatHttpDateTime(this.ValidFrom);
							}
						}
					}
					else if (field == "certificate")
					{
						return Encoding.Default.GetString(this.Certificate);
					}
				}
				else if (num != 2007449791U)
				{
					if (num == 2037381814U)
					{
						if (field == "issuer")
						{
							return this.Issuer;
						}
					}
				}
				else if (field == "cookie")
				{
					return this.Cookie;
				}
			}
			else if (num <= 2834411305U)
			{
				if (num <= 2399368031U)
				{
					if (num != 2300378703U)
					{
						if (num == 2399368031U)
						{
							if (field == "publickey")
							{
								return Encoding.Default.GetString(this.PublicKey);
							}
						}
					}
					else if (field == "subject")
					{
						return this.Subject;
					}
				}
				else if (num != 2624027180U)
				{
					if (num == 2834411305U)
					{
						if (field == "secretkeysize")
						{
							return this.SecretKeySize.ToString(CultureInfo.InvariantCulture);
						}
					}
				}
				else if (field == "flags")
				{
					return this.Flags.ToString("G", CultureInfo.InvariantCulture);
				}
			}
			else if (num <= 3808038436U)
			{
				if (num != 3204174320U)
				{
					if (num == 3808038436U)
					{
						if (field == "serialnumber")
						{
							return this.SerialNumber;
						}
					}
				}
				else if (field == "encoding")
				{
					return this.CertEncoding.ToString("G", CultureInfo.InvariantCulture);
				}
			}
			else if (num != 4144779657U)
			{
				if (num == 4170414823U)
				{
					if (field == "binaryissuer")
					{
						return Encoding.Default.GetString(this.BinaryIssuer);
					}
				}
			}
			else if (field == "serverissuer")
			{
				return this.ServerIssuer;
			}
			if (StringUtil.StringStartsWith(field, "issuer"))
			{
				return this.ExtractString(this.Issuer, field.Substring(6));
			}
			if (StringUtil.StringStartsWith(field, "subject"))
			{
				if (field.Equals("subjectemail"))
				{
					return this.ExtractString(this.Subject, "e");
				}
				return this.ExtractString(this.Subject, field.Substring(7));
			}
			else
			{
				if (StringUtil.StringStartsWith(field, "serversubject"))
				{
					return this.ExtractString(this.ServerSubject, field.Substring(13));
				}
				if (StringUtil.StringStartsWith(field, "serverissuer"))
				{
					return this.ExtractString(this.ServerIssuer, field.Substring(12));
				}
				return string.Empty;
			}
		}

		// Token: 0x060008BD RID: 2237 RVA: 0x00013B44 File Offset: 0x00011D44
		private string ExtractString(string strAll, string strSubject)
		{
			if (strAll == null || strSubject == null)
			{
				return string.Empty;
			}
			string text = string.Empty;
			int i = 0;
			string text2 = strAll.ToLower(CultureInfo.InvariantCulture);
			while (i < text2.Length)
			{
				i = text2.IndexOf(strSubject + "=", i, StringComparison.Ordinal);
				if (i < 0)
				{
					return text;
				}
				if (text.Length > 0)
				{
					text += ";";
				}
				i += strSubject.Length + 1;
				int num;
				if (strAll[i] == '"')
				{
					i++;
					num = strAll.IndexOf('"', i);
				}
				else
				{
					num = strAll.IndexOf(',', i);
				}
				if (num < 0)
				{
					num = strAll.Length;
				}
				text += strAll.Substring(i, num - i);
				i = num + 1;
			}
			return text;
		}

		// Token: 0x04000322 RID: 802
		private string _Cookie = string.Empty;

		// Token: 0x04000323 RID: 803
		private byte[] _Certificate = new byte[0];

		// Token: 0x04000324 RID: 804
		private int _Flags;

		// Token: 0x04000325 RID: 805
		private int _KeySize;

		// Token: 0x04000326 RID: 806
		private int _SecretKeySize;

		// Token: 0x04000327 RID: 807
		private string _Issuer = string.Empty;

		// Token: 0x04000328 RID: 808
		private string _ServerIssuer = string.Empty;

		// Token: 0x04000329 RID: 809
		private string _Subject = string.Empty;

		// Token: 0x0400032A RID: 810
		private string _ServerSubject = string.Empty;

		// Token: 0x0400032B RID: 811
		private string _SerialNumber = string.Empty;

		// Token: 0x0400032C RID: 812
		private DateTime _ValidFrom = DateTime.Now;

		// Token: 0x0400032D RID: 813
		private DateTime _ValidUntil = DateTime.Now;

		// Token: 0x0400032E RID: 814
		private int _CertEncoding;

		// Token: 0x0400032F RID: 815
		private byte[] _PublicKey = new byte[0];

		// Token: 0x04000330 RID: 816
		private byte[] _BinaryIssuer = new byte[0];
	}
}
