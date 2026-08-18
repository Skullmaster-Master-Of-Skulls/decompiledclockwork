using System;
using System.Globalization;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x0200026A RID: 618
	public class MailAddress
	{
		// Token: 0x0600173B RID: 5947 RVA: 0x00076AB1 File Offset: 0x00074CB1
		internal MailAddress(string displayName, string userName, string domain)
		{
			this.host = domain;
			this.userName = userName;
			this.displayName = displayName;
			this.displayNameEncoding = Encoding.GetEncoding("utf-8");
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x00076ADE File Offset: 0x00074CDE
		public MailAddress(string address) : this(address, null, null)
		{
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00076AE9 File Offset: 0x00074CE9
		public MailAddress(string address, string displayName) : this(address, displayName, null)
		{
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x00076AF4 File Offset: 0x00074CF4
		public MailAddress(string address, string displayName, Encoding displayNameEncoding)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"address"
				}), "address");
			}
			this.displayNameEncoding = (displayNameEncoding ?? Encoding.GetEncoding("utf-8"));
			this.displayName = (displayName ?? string.Empty);
			if (!string.IsNullOrEmpty(this.displayName))
			{
				this.displayName = MailAddressParser.NormalizeOrThrow(this.displayName);
				if (this.displayName.Length >= 2 && this.displayName[0] == '"' && this.displayName[this.displayName.Length - 1] == '"')
				{
					this.displayName = this.displayName.Substring(1, this.displayName.Length - 2);
				}
			}
			MailAddress mailAddress = MailAddressParser.ParseAddress(address);
			this.host = mailAddress.host;
			this.userName = mailAddress.userName;
			if (string.IsNullOrEmpty(this.displayName))
			{
				this.displayName = mailAddress.displayName;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x00076C19 File Offset: 0x00074E19
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x00076C21 File Offset: 0x00074E21
		public string User
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x00076C29 File Offset: 0x00074E29
		private string GetUser(bool allowUnicode)
		{
			if (!allowUnicode && !MimeBasePart.IsAscii(this.userName, true))
			{
				throw new SmtpException(SR.GetString("SmtpNonAsciiUserNotSupported", new object[]
				{
					this.Address
				}));
			}
			return this.userName;
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x00076C61 File Offset: 0x00074E61
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x00076C6C File Offset: 0x00074E6C
		private string GetHost(bool allowUnicode)
		{
			string ascii = this.host;
			if (!allowUnicode && !MimeBasePart.IsAscii(ascii, true))
			{
				IdnMapping idnMapping = new IdnMapping();
				try
				{
					ascii = idnMapping.GetAscii(ascii);
				}
				catch (ArgumentException innerException)
				{
					throw new SmtpException(SR.GetString("SmtpInvalidHostName", new object[]
					{
						this.Address
					}), innerException);
				}
			}
			if (!ServicePointManager.AllowFullDomainLiterals && ascii.IndexOfAny(MailAddress.s_newLines) >= 0)
			{
				throw new SmtpException("SmtpInvalidHostName", this.Address);
			}
			return ascii;
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x00076CF4 File Offset: 0x00074EF4
		public string Address
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[]
				{
					this.userName,
					this.host
				});
			}
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x00076D1D File Offset: 0x00074F1D
		private string GetAddress(bool allowUnicode)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[]
			{
				this.GetUser(allowUnicode),
				this.GetHost(allowUnicode)
			});
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x00076D48 File Offset: 0x00074F48
		private string SmtpAddress
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "<{0}>", new object[]
				{
					this.Address
				});
			}
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x00076D68 File Offset: 0x00074F68
		internal string GetSmtpAddress(bool allowUnicode)
		{
			return string.Format(CultureInfo.InvariantCulture, "<{0}>", new object[]
			{
				this.GetAddress(allowUnicode)
			});
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x00076D89 File Offset: 0x00074F89
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.DisplayName))
			{
				return this.Address;
			}
			return string.Format("\"{0}\" {1}", this.DisplayName, this.SmtpAddress);
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x00076DB5 File Offset: 0x00074FB5
		public override bool Equals(object value)
		{
			return value != null && this.ToString().Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x00076DCE File Offset: 0x00074FCE
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x00076DDC File Offset: 0x00074FDC
		internal string Encode(int charsConsumed, bool allowUnicode)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.displayName))
			{
				if (MimeBasePart.IsAscii(this.displayName, false) || allowUnicode)
				{
					text = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", new object[]
					{
						this.displayName
					});
				}
				else
				{
					IEncodableStream encoderForHeader = MailAddress.encoderFactory.GetEncoderForHeader(this.displayNameEncoding, false, charsConsumed);
					byte[] bytes = this.displayNameEncoding.GetBytes(this.displayName);
					encoderForHeader.EncodeBytes(bytes, 0, bytes.Length);
					text = encoderForHeader.GetEncodedString();
				}
				text = text + " " + this.GetSmtpAddress(allowUnicode);
			}
			else
			{
				text = this.GetAddress(allowUnicode);
			}
			return text;
		}

		// Token: 0x040017AE RID: 6062
		private static readonly char[] s_newLines = new char[]
		{
			'\r',
			'\n'
		};

		// Token: 0x040017AF RID: 6063
		private readonly Encoding displayNameEncoding;

		// Token: 0x040017B0 RID: 6064
		private readonly string displayName;

		// Token: 0x040017B1 RID: 6065
		private readonly string userName;

		// Token: 0x040017B2 RID: 6066
		private readonly string host;

		// Token: 0x040017B3 RID: 6067
		private static EncodedStreamFactory encoderFactory = new EncodedStreamFactory();
	}
}
