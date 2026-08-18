using System;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	// Token: 0x02000699 RID: 1689
	public class MailAddress
	{
		// Token: 0x0600341D RID: 13341 RVA: 0x000DBB31 File Offset: 0x000DAB31
		internal MailAddress(string address, string encodedDisplayName, uint bogusParam)
		{
			this.encodedDisplayName = encodedDisplayName;
			this.GetParts(address);
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x000DBB47 File Offset: 0x000DAB47
		public MailAddress(string address) : this(address, null, null)
		{
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x000DBB52 File Offset: 0x000DAB52
		public MailAddress(string address, string displayName) : this(address, displayName, null)
		{
		}

		// Token: 0x06003420 RID: 13344 RVA: 0x000DBB60 File Offset: 0x000DAB60
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
			if (!ServicePointManager.AllowNewLineInMailAddress && MailBnfHelper.HasCROrLF(address))
			{
				throw new FormatException(SR.GetString("MailAddressInvalidFormat"));
			}
			this.displayNameEncoding = displayNameEncoding;
			this.displayName = displayName;
			this.ParseValue(address);
			if (this.displayName != null && this.displayName != string.Empty)
			{
				if (this.displayName[0] == '"' && this.displayName[this.displayName.Length - 1] == '"')
				{
					this.displayName = this.displayName.Substring(1, this.displayName.Length - 2);
				}
				this.displayName = this.displayName.Trim();
			}
			if (this.displayName != null && this.displayName.Length > 0)
			{
				if (!MimeBasePart.IsAscii(this.displayName, false) || this.displayNameEncoding != null)
				{
					if (this.displayNameEncoding == null)
					{
						this.displayNameEncoding = Encoding.GetEncoding("utf-8");
					}
					this.encodedDisplayName = MimeBasePart.EncodeHeaderValue(this.displayName, this.displayNameEncoding, MimeBasePart.ShouldUseBase64Encoding(displayNameEncoding));
					StringBuilder stringBuilder = new StringBuilder();
					int num = 0;
					MailBnfHelper.ReadUnQuotedString(this.encodedDisplayName, ref num, stringBuilder);
					this.encodedDisplayName = stringBuilder.ToString();
					return;
				}
				this.encodedDisplayName = this.displayName;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06003421 RID: 13345 RVA: 0x000DBCF4 File Offset: 0x000DACF4
		public string DisplayName
		{
			get
			{
				if (this.displayName == null)
				{
					if (this.encodedDisplayName != null && this.encodedDisplayName.Length > 0)
					{
						this.displayName = MimeBasePart.DecodeHeaderValue(this.encodedDisplayName);
					}
					else
					{
						this.displayName = string.Empty;
					}
				}
				return this.displayName;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000DBD43 File Offset: 0x000DAD43
		public string User
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x000DBD4B File Offset: 0x000DAD4B
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x000DBD53 File Offset: 0x000DAD53
		public string Address
		{
			get
			{
				if (this.address == null)
				{
					this.CombineParts();
				}
				return this.address;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x000DBD6C File Offset: 0x000DAD6C
		internal string SmtpAddress
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append('<');
				stringBuilder.Append(this.Address);
				stringBuilder.Append('>');
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06003426 RID: 13350 RVA: 0x000DBDA4 File Offset: 0x000DADA4
		internal string ToEncodedString()
		{
			if (this.fullAddress == null)
			{
				if (this.encodedDisplayName != null && this.encodedDisplayName != string.Empty)
				{
					StringBuilder stringBuilder = new StringBuilder();
					MailBnfHelper.GetDotAtomOrQuotedString(this.encodedDisplayName, stringBuilder);
					stringBuilder.Append(" <");
					stringBuilder.Append(this.Address);
					stringBuilder.Append('>');
					this.fullAddress = stringBuilder.ToString();
				}
				else
				{
					this.fullAddress = this.Address;
				}
			}
			return this.fullAddress;
		}

		// Token: 0x06003427 RID: 13351 RVA: 0x000DBE28 File Offset: 0x000DAE28
		public override string ToString()
		{
			if (this.fullAddress == null)
			{
				if (this.encodedDisplayName != null && this.encodedDisplayName != string.Empty)
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (this.DisplayName.StartsWith("\"") && this.DisplayName.EndsWith("\""))
					{
						stringBuilder.Append(this.DisplayName);
					}
					else
					{
						stringBuilder.Append('"');
						stringBuilder.Append(this.DisplayName);
						stringBuilder.Append('"');
					}
					stringBuilder.Append(" <");
					stringBuilder.Append(this.Address);
					stringBuilder.Append('>');
					this.fullAddress = stringBuilder.ToString();
				}
				else
				{
					this.fullAddress = this.Address;
				}
			}
			return this.fullAddress;
		}

		// Token: 0x06003428 RID: 13352 RVA: 0x000DBEFA File Offset: 0x000DAEFA
		public override bool Equals(object value)
		{
			return value != null && this.ToString().Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x06003429 RID: 13353 RVA: 0x000DBF13 File Offset: 0x000DAF13
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x0600342A RID: 13354 RVA: 0x000DBF20 File Offset: 0x000DAF20
		private void GetParts(string address)
		{
			if (address == null)
			{
				return;
			}
			int num = address.IndexOf('@');
			if (num < 0)
			{
				throw new FormatException(SR.GetString("MailAddressInvalidFormat"));
			}
			this.userName = address.Substring(0, num);
			this.host = address.Substring(num + 1);
		}

		// Token: 0x0600342B RID: 13355 RVA: 0x000DBF6C File Offset: 0x000DAF6C
		private void ParseValue(string address)
		{
			string text = null;
			int num = 0;
			MailBnfHelper.SkipFWS(address, ref num);
			int num2 = address.IndexOf('"', num);
			if (num2 == num)
			{
				num2 = address.IndexOf('"', num2 + 1);
				if (num2 > num)
				{
					int num3 = num2 + 1;
					MailBnfHelper.SkipFWS(address, ref num3);
					if (address.Length > num3 && address[num3] != '@')
					{
						text = address.Substring(num, num2 + 1 - num);
						address = address.Substring(num3);
					}
				}
			}
			if (text == null)
			{
				int num4 = address.IndexOf('<', num);
				if (num4 >= num)
				{
					text = address.Substring(num, num4 - num);
					address = address.Substring(num4);
				}
			}
			if (text == null)
			{
				num2 = address.IndexOf('"', num);
				if (num2 > num)
				{
					text = address.Substring(num, num2 - num);
					address = address.Substring(num2);
				}
			}
			if (this.displayName == null)
			{
				this.displayName = text;
			}
			int num5 = 0;
			address = MailBnfHelper.ReadMailAddress(address, ref num5, out this.encodedDisplayName);
			this.GetParts(address);
		}

		// Token: 0x0600342C RID: 13356 RVA: 0x000DC054 File Offset: 0x000DB054
		private void CombineParts()
		{
			if (this.userName == null || this.host == null)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			MailBnfHelper.GetDotAtomOrQuotedString(this.User, stringBuilder);
			stringBuilder.Append('@');
			MailBnfHelper.GetDotAtomOrDomainLiteral(this.Host, stringBuilder);
			this.address = stringBuilder.ToString();
		}

		// Token: 0x04003003 RID: 12291
		private string displayName;

		// Token: 0x04003004 RID: 12292
		private Encoding displayNameEncoding;

		// Token: 0x04003005 RID: 12293
		private string encodedDisplayName;

		// Token: 0x04003006 RID: 12294
		private string address;

		// Token: 0x04003007 RID: 12295
		private string fullAddress;

		// Token: 0x04003008 RID: 12296
		private string userName;

		// Token: 0x04003009 RID: 12297
		private string host;
	}
}
