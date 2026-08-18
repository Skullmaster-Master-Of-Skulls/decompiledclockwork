using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000686 RID: 1670
	public class ContentDisposition
	{
		// Token: 0x060033B3 RID: 13235 RVA: 0x000DA4A0 File Offset: 0x000D94A0
		public ContentDisposition()
		{
			this.isChanged = true;
			this.disposition = "attachment";
			this.ParseValue();
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x000DA4C0 File Offset: 0x000D94C0
		public ContentDisposition(string disposition)
		{
			if (disposition == null)
			{
				throw new ArgumentNullException("disposition");
			}
			this.isChanged = true;
			this.disposition = disposition;
			this.ParseValue();
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060033B5 RID: 13237 RVA: 0x000DA4EA File Offset: 0x000D94EA
		// (set) Token: 0x060033B6 RID: 13238 RVA: 0x000DA4F2 File Offset: 0x000D94F2
		public string DispositionType
		{
			get
			{
				return this.dispositionType;
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
				this.isChanged = true;
				this.dispositionType = value;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x000DA532 File Offset: 0x000D9532
		public StringDictionary Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new TrackingStringDictionary();
				}
				return this.parameters;
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060033B8 RID: 13240 RVA: 0x000DA54D File Offset: 0x000D954D
		// (set) Token: 0x060033B9 RID: 13241 RVA: 0x000DA55F File Offset: 0x000D955F
		public string FileName
		{
			get
			{
				return this.Parameters["filename"];
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("filename");
					return;
				}
				this.Parameters["filename"] = value;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060033BA RID: 13242 RVA: 0x000DA594 File Offset: 0x000D9594
		// (set) Token: 0x060033BB RID: 13243 RVA: 0x000DA5C5 File Offset: 0x000D95C5
		public DateTime CreationDate
		{
			get
			{
				string text = this.Parameters["creation-date"];
				if (text == null)
				{
					return DateTime.MinValue;
				}
				int num = 0;
				return MailBnfHelper.ReadDateTime(text, ref num);
			}
			set
			{
				this.Parameters["creation-date"] = MailBnfHelper.GetDateTimeString(value, null);
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060033BC RID: 13244 RVA: 0x000DA5E0 File Offset: 0x000D95E0
		// (set) Token: 0x060033BD RID: 13245 RVA: 0x000DA611 File Offset: 0x000D9611
		public DateTime ModificationDate
		{
			get
			{
				string text = this.Parameters["modification-date"];
				if (text == null)
				{
					return DateTime.MinValue;
				}
				int num = 0;
				return MailBnfHelper.ReadDateTime(text, ref num);
			}
			set
			{
				this.Parameters["modification-date"] = MailBnfHelper.GetDateTimeString(value, null);
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060033BE RID: 13246 RVA: 0x000DA62A File Offset: 0x000D962A
		// (set) Token: 0x060033BF RID: 13247 RVA: 0x000DA63C File Offset: 0x000D963C
		public bool Inline
		{
			get
			{
				return this.dispositionType == "inline";
			}
			set
			{
				this.isChanged = true;
				if (value)
				{
					this.dispositionType = "inline";
					return;
				}
				this.dispositionType = "attachment";
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x000DA660 File Offset: 0x000D9660
		// (set) Token: 0x060033C1 RID: 13249 RVA: 0x000DA691 File Offset: 0x000D9691
		public DateTime ReadDate
		{
			get
			{
				string text = this.Parameters["read-date"];
				if (text == null)
				{
					return DateTime.MinValue;
				}
				int num = 0;
				return MailBnfHelper.ReadDateTime(text, ref num);
			}
			set
			{
				this.Parameters["read-date"] = MailBnfHelper.GetDateTimeString(value, null);
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060033C2 RID: 13250 RVA: 0x000DA6AC File Offset: 0x000D96AC
		// (set) Token: 0x060033C3 RID: 13251 RVA: 0x000DA6DB File Offset: 0x000D96DB
		public long Size
		{
			get
			{
				string text = this.Parameters["size"];
				if (text == null)
				{
					return -1L;
				}
				return long.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				this.Parameters["size"] = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000DA6F9 File Offset: 0x000D96F9
		internal void Set(string contentDisposition, HeaderCollection headers)
		{
			this.disposition = contentDisposition;
			this.ParseValue();
			headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.ToString());
			this.isPersisted = true;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000DA721 File Offset: 0x000D9721
		internal void PersistIfNeeded(HeaderCollection headers, bool forcePersist)
		{
			if (this.IsChanged || !this.isPersisted || forcePersist)
			{
				headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.ToString());
				this.isPersisted = true;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000DA74F File Offset: 0x000D974F
		internal bool IsChanged
		{
			get
			{
				return this.isChanged || (this.parameters != null && this.parameters.IsChanged);
			}
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000DA770 File Offset: 0x000D9770
		public override string ToString()
		{
			if (this.disposition == null || this.isChanged || (this.parameters != null && this.parameters.IsChanged))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.dispositionType);
				foreach (object obj in this.Parameters.Keys)
				{
					string text = (string)obj;
					stringBuilder.Append("; ");
					stringBuilder.Append(text);
					stringBuilder.Append('=');
					MailBnfHelper.GetTokenOrQuotedString(this.parameters[text], stringBuilder);
				}
				this.disposition = stringBuilder.ToString();
				this.isChanged = false;
				this.parameters.IsChanged = false;
				this.isPersisted = false;
			}
			return this.disposition;
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000DA864 File Offset: 0x000D9864
		public override bool Equals(object rparam)
		{
			return rparam != null && string.Compare(this.ToString(), rparam.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000DA880 File Offset: 0x000D9880
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000DA890 File Offset: 0x000D9890
		private void ParseValue()
		{
			int index = 0;
			this.parameters = new TrackingStringDictionary();
			Exception ex = null;
			try
			{
				this.dispositionType = MailBnfHelper.ReadToken(this.disposition, ref index, null);
				if (this.dispositionType == null || this.dispositionType.Length == 0)
				{
					ex = new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter"));
				}
				if (ex == null)
				{
					while (MailBnfHelper.SkipCFWS(this.disposition, ref index))
					{
						if (this.disposition[index++] != ';')
						{
							ex = new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter"));
						}
						if (!MailBnfHelper.SkipCFWS(this.disposition, ref index))
						{
							break;
						}
						string text = MailBnfHelper.ReadParameterAttribute(this.disposition, ref index, null);
						if (this.disposition[index++] != '=')
						{
							ex = new FormatException(SR.GetString("MailHeaderFieldMalformedHeader"));
							break;
						}
						string text2;
						if (!MailBnfHelper.SkipCFWS(this.disposition, ref index))
						{
							text2 = string.Empty;
						}
						else if (this.disposition[index] == '"')
						{
							text2 = MailBnfHelper.ReadQuotedString(this.disposition, ref index, null);
						}
						else
						{
							text2 = MailBnfHelper.ReadToken(this.disposition, ref index, null);
						}
						if (text == null || text2 == null || text.Length == 0 || text2.Length == 0)
						{
							ex = new FormatException(SR.GetString("ContentDispositionInvalid"));
							break;
						}
						if (string.Compare(text, "creation-date", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text, "modification-date", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(text, "read-date", StringComparison.OrdinalIgnoreCase) == 0)
						{
							int num = 0;
							MailBnfHelper.ReadDateTime(text2, ref num);
						}
						this.parameters.Add(text, text2);
					}
				}
			}
			catch (FormatException)
			{
				throw new FormatException(SR.GetString("ContentDispositionInvalid"));
			}
			if (ex != null)
			{
				throw ex;
			}
			this.parameters.IsChanged = false;
		}

		// Token: 0x04002FB7 RID: 12215
		private string dispositionType;

		// Token: 0x04002FB8 RID: 12216
		private TrackingStringDictionary parameters;

		// Token: 0x04002FB9 RID: 12217
		private bool isChanged;

		// Token: 0x04002FBA RID: 12218
		private bool isPersisted;

		// Token: 0x04002FBB RID: 12219
		private string disposition;
	}
}
