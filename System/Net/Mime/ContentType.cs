using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000688 RID: 1672
	public class ContentType
	{
		// Token: 0x060033CB RID: 13259 RVA: 0x000DAA64 File Offset: 0x000D9A64
		public ContentType() : this(ContentType.Default)
		{
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000DAA74 File Offset: 0x000D9A74
		public ContentType(string contentType)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (contentType == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"contentType"
				}), "contentType");
			}
			this.isChanged = true;
			this.type = contentType;
			this.ParseValue();
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060033CD RID: 13261 RVA: 0x000DAADB File Offset: 0x000D9ADB
		// (set) Token: 0x060033CE RID: 13262 RVA: 0x000DAAED File Offset: 0x000D9AED
		public string Boundary
		{
			get
			{
				return this.Parameters["boundary"];
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("boundary");
					return;
				}
				this.Parameters["boundary"] = value;
			}
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060033CF RID: 13263 RVA: 0x000DAB21 File Offset: 0x000D9B21
		// (set) Token: 0x060033D0 RID: 13264 RVA: 0x000DAB33 File Offset: 0x000D9B33
		public string CharSet
		{
			get
			{
				return this.Parameters["charset"];
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("charset");
					return;
				}
				this.Parameters["charset"] = value;
			}
		}

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000DAB67 File Offset: 0x000D9B67
		// (set) Token: 0x060033D2 RID: 13266 RVA: 0x000DAB80 File Offset: 0x000D9B80
		public string MediaType
		{
			get
			{
				return this.mediaType + "/" + this.subType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == string.Empty)
				{
					throw new ArgumentException(SR.GetString("net_emptystringset"), "value");
				}
				int num = 0;
				this.mediaType = MailBnfHelper.ReadToken(value, ref num, null);
				if (this.mediaType.Length == 0 || num >= value.Length || value[num++] != '/')
				{
					throw new FormatException(SR.GetString("MediaTypeInvalid"));
				}
				this.subType = MailBnfHelper.ReadToken(value, ref num, null);
				if (this.subType.Length == 0 || num < value.Length)
				{
					throw new FormatException(SR.GetString("MediaTypeInvalid"));
				}
				this.isChanged = true;
				this.isPersisted = false;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060033D3 RID: 13267 RVA: 0x000DAC48 File Offset: 0x000D9C48
		// (set) Token: 0x060033D4 RID: 13268 RVA: 0x000DAC78 File Offset: 0x000D9C78
		public string Name
		{
			get
			{
				string text = this.Parameters["name"];
				Encoding encoding = MimeBasePart.DecodeEncoding(text);
				if (encoding != null)
				{
					text = MimeBasePart.DecodeHeaderValue(text);
				}
				return text;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("name");
					return;
				}
				if (MimeBasePart.IsAscii(value, false))
				{
					this.Parameters["name"] = value;
					return;
				}
				Encoding encoding = Encoding.GetEncoding("utf-8");
				this.Parameters["name"] = MimeBasePart.EncodeHeaderValue(value, encoding, MimeBasePart.ShouldUseBase64Encoding(encoding));
			}
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x000DACE9 File Offset: 0x000D9CE9
		public StringDictionary Parameters
		{
			get
			{
				if (this.parameters == null && this.type == null)
				{
					this.parameters = new TrackingStringDictionary();
				}
				return this.parameters;
			}
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x000DAD0C File Offset: 0x000D9D0C
		internal void Set(string contentType, HeaderCollection headers)
		{
			this.type = contentType;
			this.ParseValue();
			headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.ToString());
			this.isPersisted = true;
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x000DAD34 File Offset: 0x000D9D34
		internal void PersistIfNeeded(HeaderCollection headers, bool forcePersist)
		{
			if (this.IsChanged || !this.isPersisted || forcePersist)
			{
				headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.ToString());
				this.isPersisted = true;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060033D8 RID: 13272 RVA: 0x000DAD62 File Offset: 0x000D9D62
		internal bool IsChanged
		{
			get
			{
				return this.isChanged || (this.parameters != null && this.parameters.IsChanged);
			}
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x000DAD84 File Offset: 0x000D9D84
		public override string ToString()
		{
			if (this.type == null || this.IsChanged)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.mediaType);
				stringBuilder.Append('/');
				stringBuilder.Append(this.subType);
				foreach (object obj in this.Parameters.Keys)
				{
					string text = (string)obj;
					stringBuilder.Append("; ");
					stringBuilder.Append(text);
					stringBuilder.Append('=');
					MailBnfHelper.GetTokenOrQuotedString(this.parameters[text], stringBuilder);
				}
				this.type = stringBuilder.ToString();
				this.isChanged = false;
				this.parameters.IsChanged = false;
				this.isPersisted = false;
			}
			return this.type;
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x000DAE74 File Offset: 0x000D9E74
		public override bool Equals(object rparam)
		{
			return rparam != null && string.Compare(this.ToString(), rparam.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x000DAE90 File Offset: 0x000D9E90
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x000DAEA0 File Offset: 0x000D9EA0
		private void ParseValue()
		{
			int num = 0;
			Exception ex = null;
			this.parameters = new TrackingStringDictionary();
			try
			{
				this.mediaType = MailBnfHelper.ReadToken(this.type, ref num, null);
				if (this.mediaType == null || this.mediaType.Length == 0 || num >= this.type.Length || this.type[num++] != '/')
				{
					ex = new FormatException(SR.GetString("ContentTypeInvalid"));
				}
				if (ex == null)
				{
					this.subType = MailBnfHelper.ReadToken(this.type, ref num, null);
					if (this.subType == null || this.subType.Length == 0)
					{
						ex = new FormatException(SR.GetString("ContentTypeInvalid"));
					}
				}
				if (ex == null)
				{
					while (MailBnfHelper.SkipCFWS(this.type, ref num))
					{
						if (this.type[num++] != ';')
						{
							ex = new FormatException(SR.GetString("ContentTypeInvalid"));
							break;
						}
						if (!MailBnfHelper.SkipCFWS(this.type, ref num))
						{
							break;
						}
						string text = MailBnfHelper.ReadParameterAttribute(this.type, ref num, null);
						if (text == null || text.Length == 0)
						{
							ex = new FormatException(SR.GetString("ContentTypeInvalid"));
							break;
						}
						if (num >= this.type.Length || this.type[num++] != '=')
						{
							ex = new FormatException(SR.GetString("ContentTypeInvalid"));
							break;
						}
						if (!MailBnfHelper.SkipCFWS(this.type, ref num))
						{
							ex = new FormatException(SR.GetString("ContentTypeInvalid"));
							break;
						}
						string text2;
						if (this.type[num] == '"')
						{
							text2 = MailBnfHelper.ReadQuotedString(this.type, ref num, null);
						}
						else
						{
							text2 = MailBnfHelper.ReadToken(this.type, ref num, null);
						}
						if (text2 == null)
						{
							ex = new FormatException(SR.GetString("ContentTypeInvalid"));
							break;
						}
						this.parameters.Add(text, text2);
					}
				}
				this.parameters.IsChanged = false;
			}
			catch (FormatException)
			{
				throw new FormatException(SR.GetString("ContentTypeInvalid"));
			}
			if (ex != null)
			{
				throw new FormatException(SR.GetString("ContentTypeInvalid"));
			}
		}

		// Token: 0x04002FC4 RID: 12228
		private string mediaType;

		// Token: 0x04002FC5 RID: 12229
		private string subType;

		// Token: 0x04002FC6 RID: 12230
		private bool isChanged;

		// Token: 0x04002FC7 RID: 12231
		private string type;

		// Token: 0x04002FC8 RID: 12232
		private bool isPersisted;

		// Token: 0x04002FC9 RID: 12233
		private TrackingStringDictionary parameters;

		// Token: 0x04002FCA RID: 12234
		internal static readonly string Default = "application/octet-stream";
	}
}
