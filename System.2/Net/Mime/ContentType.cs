using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000241 RID: 577
	public class ContentType
	{
		// Token: 0x060015EC RID: 5612 RVA: 0x000714D4 File Offset: 0x0006F6D4
		public ContentType() : this(ContentType.Default)
		{
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000714E4 File Offset: 0x0006F6E4
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

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00071549 File Offset: 0x0006F749
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x0007155B File Offset: 0x0006F75B
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

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0007158F File Offset: 0x0006F78F
		// (set) Token: 0x060015F1 RID: 5617 RVA: 0x000715A1 File Offset: 0x0006F7A1
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

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x000715D5 File Offset: 0x0006F7D5
		// (set) Token: 0x060015F3 RID: 5619 RVA: 0x000715F0 File Offset: 0x0006F7F0
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

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x000716B8 File Offset: 0x0006F8B8
		// (set) Token: 0x060015F5 RID: 5621 RVA: 0x000716E8 File Offset: 0x0006F8E8
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
				this.Parameters["name"] = value;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0007171C File Offset: 0x0006F91C
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

		// Token: 0x060015F7 RID: 5623 RVA: 0x0007173F File Offset: 0x0006F93F
		internal void Set(string contentType, HeaderCollection headers)
		{
			this.type = contentType;
			this.ParseValue();
			headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.ToString());
			this.isPersisted = true;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00071767 File Offset: 0x0006F967
		internal void PersistIfNeeded(HeaderCollection headers, bool forcePersist)
		{
			if (this.IsChanged || !this.isPersisted || forcePersist)
			{
				headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.ToString());
				this.isPersisted = true;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x0007179A File Offset: 0x0006F99A
		internal bool IsChanged
		{
			get
			{
				return this.isChanged || (this.parameters != null && this.parameters.IsChanged);
			}
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x000717BB File Offset: 0x0006F9BB
		public override string ToString()
		{
			if (this.type == null || this.IsChanged)
			{
				this.type = this.Encode(false);
				this.isChanged = false;
				this.parameters.IsChanged = false;
				this.isPersisted = false;
			}
			return this.type;
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000717FC File Offset: 0x0006F9FC
		internal string Encode(bool allowUnicode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.mediaType);
			stringBuilder.Append('/');
			stringBuilder.Append(this.subType);
			foreach (object obj in this.Parameters.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append("; ");
				ContentType.EncodeToBuffer(text, stringBuilder, allowUnicode);
				stringBuilder.Append('=');
				ContentType.EncodeToBuffer(this.parameters[text], stringBuilder, allowUnicode);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000718B4 File Offset: 0x0006FAB4
		private static void EncodeToBuffer(string value, StringBuilder builder, bool allowUnicode)
		{
			Encoding encoding = MimeBasePart.DecodeEncoding(value);
			if (encoding != null)
			{
				builder.Append("\"" + value + "\"");
				return;
			}
			if ((allowUnicode && !MailBnfHelper.HasCROrLF(value)) || MimeBasePart.IsAscii(value, false))
			{
				MailBnfHelper.GetTokenOrQuotedString(value, builder, allowUnicode);
				return;
			}
			encoding = Encoding.GetEncoding("utf-8");
			builder.Append("\"" + MimeBasePart.EncodeHeaderValue(value, encoding, MimeBasePart.ShouldUseBase64Encoding(encoding)) + "\"");
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0007192E File Offset: 0x0006FB2E
		public override bool Equals(object rparam)
		{
			return rparam != null && string.Compare(this.ToString(), rparam.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0007194A File Offset: 0x0006FB4A
		public override int GetHashCode()
		{
			return this.ToString().ToLowerInvariant().GetHashCode();
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0007195C File Offset: 0x0006FB5C
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

		// Token: 0x04001704 RID: 5892
		private string mediaType;

		// Token: 0x04001705 RID: 5893
		private string subType;

		// Token: 0x04001706 RID: 5894
		private bool isChanged;

		// Token: 0x04001707 RID: 5895
		private string type;

		// Token: 0x04001708 RID: 5896
		private bool isPersisted;

		// Token: 0x04001709 RID: 5897
		private TrackingStringDictionary parameters;

		// Token: 0x0400170A RID: 5898
		internal static readonly string Default = "application/octet-stream";
	}
}
