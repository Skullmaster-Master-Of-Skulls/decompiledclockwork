using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x0200023F RID: 575
	public class ContentDisposition
	{
		// Token: 0x060015D0 RID: 5584 RVA: 0x00070E0C File Offset: 0x0006F00C
		static ContentDisposition()
		{
			ContentDisposition.validators.Add("creation-date", ContentDisposition.dateParser);
			ContentDisposition.validators.Add("modification-date", ContentDisposition.dateParser);
			ContentDisposition.validators.Add("read-date", ContentDisposition.dateParser);
			ContentDisposition.validators.Add("size", ContentDisposition.longParser);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00070E9D File Offset: 0x0006F09D
		public ContentDisposition()
		{
			this.isChanged = true;
			this.dispositionType = "attachment";
			this.disposition = this.dispositionType;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00070EC3 File Offset: 0x0006F0C3
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

		// Token: 0x060015D3 RID: 5587 RVA: 0x00070EF0 File Offset: 0x0006F0F0
		internal DateTime GetDateParameter(string parameterName)
		{
			SmtpDateTime smtpDateTime = ((TrackingValidationObjectDictionary)this.Parameters).InternalGet(parameterName) as SmtpDateTime;
			if (smtpDateTime == null)
			{
				return DateTime.MinValue;
			}
			return smtpDateTime.Date;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00070F23 File Offset: 0x0006F123
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x00070F2B File Offset: 0x0006F12B
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

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x00070F6B File Offset: 0x0006F16B
		public StringDictionary Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new TrackingValidationObjectDictionary(ContentDisposition.validators);
				}
				return this.parameters;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x00070F8B File Offset: 0x0006F18B
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x00070F9D File Offset: 0x0006F19D
		public string FileName
		{
			get
			{
				return this.Parameters["filename"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Parameters.Remove("filename");
					return;
				}
				this.Parameters["filename"] = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x00070FC9 File Offset: 0x0006F1C9
		// (set) Token: 0x060015DA RID: 5594 RVA: 0x00070FD8 File Offset: 0x0006F1D8
		public DateTime CreationDate
		{
			get
			{
				return this.GetDateParameter("creation-date");
			}
			set
			{
				SmtpDateTime value2 = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("creation-date", value2);
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00071002 File Offset: 0x0006F202
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x00071010 File Offset: 0x0006F210
		public DateTime ModificationDate
		{
			get
			{
				return this.GetDateParameter("modification-date");
			}
			set
			{
				SmtpDateTime value2 = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("modification-date", value2);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x0007103A File Offset: 0x0006F23A
		// (set) Token: 0x060015DE RID: 5598 RVA: 0x0007104C File Offset: 0x0006F24C
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

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0007106F File Offset: 0x0006F26F
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x0007107C File Offset: 0x0006F27C
		public DateTime ReadDate
		{
			get
			{
				return this.GetDateParameter("read-date");
			}
			set
			{
				SmtpDateTime value2 = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("read-date", value2);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x000710A8 File Offset: 0x0006F2A8
		// (set) Token: 0x060015E2 RID: 5602 RVA: 0x000710D7 File Offset: 0x0006F2D7
		public long Size
		{
			get
			{
				object obj = ((TrackingValidationObjectDictionary)this.Parameters).InternalGet("size");
				if (obj == null)
				{
					return -1L;
				}
				return (long)obj;
			}
			set
			{
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("size", value);
			}
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000710F4 File Offset: 0x0006F2F4
		internal void Set(string contentDisposition, HeaderCollection headers)
		{
			this.disposition = contentDisposition;
			this.ParseValue();
			headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.ToString());
			this.isPersisted = true;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0007111C File Offset: 0x0006F31C
		internal void PersistIfNeeded(HeaderCollection headers, bool forcePersist)
		{
			if (this.IsChanged || !this.isPersisted || forcePersist)
			{
				headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.ToString());
				this.isPersisted = true;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0007114F File Offset: 0x0006F34F
		internal bool IsChanged
		{
			get
			{
				return this.isChanged || (this.parameters != null && this.parameters.IsChanged);
			}
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00071170 File Offset: 0x0006F370
		public override string ToString()
		{
			if (this.disposition == null || this.isChanged || (this.parameters != null && this.parameters.IsChanged))
			{
				this.disposition = this.Encode(false);
				this.isChanged = false;
				this.parameters.IsChanged = false;
				this.isPersisted = false;
			}
			return this.disposition;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000711D0 File Offset: 0x0006F3D0
		internal string Encode(bool allowUnicode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.dispositionType);
			foreach (object obj in this.Parameters.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append("; ");
				ContentDisposition.EncodeToBuffer(text, stringBuilder, allowUnicode);
				stringBuilder.Append('=');
				ContentDisposition.EncodeToBuffer(this.parameters[text], stringBuilder, allowUnicode);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00071270 File Offset: 0x0006F470
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

		// Token: 0x060015E9 RID: 5609 RVA: 0x000712EA File Offset: 0x0006F4EA
		public override bool Equals(object rparam)
		{
			return rparam != null && string.Compare(this.ToString(), rparam.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00071306 File Offset: 0x0006F506
		public override int GetHashCode()
		{
			return this.ToString().ToLowerInvariant().GetHashCode();
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00071318 File Offset: 0x0006F518
		private void ParseValue()
		{
			int num = 0;
			try
			{
				this.dispositionType = MailBnfHelper.ReadToken(this.disposition, ref num, null);
				if (string.IsNullOrEmpty(this.dispositionType))
				{
					throw new FormatException(SR.GetString("MailHeaderFieldMalformedHeader"));
				}
				if (this.parameters == null)
				{
					this.parameters = new TrackingValidationObjectDictionary(ContentDisposition.validators);
				}
				else
				{
					this.parameters.Clear();
				}
				while (MailBnfHelper.SkipCFWS(this.disposition, ref num))
				{
					if (this.disposition[num++] != ';')
					{
						throw new FormatException(SR.GetString("MailHeaderFieldInvalidCharacter", new object[]
						{
							this.disposition[num - 1]
						}));
					}
					if (!MailBnfHelper.SkipCFWS(this.disposition, ref num))
					{
						break;
					}
					string text = MailBnfHelper.ReadParameterAttribute(this.disposition, ref num, null);
					if (this.disposition[num++] != '=')
					{
						throw new FormatException(SR.GetString("MailHeaderFieldMalformedHeader"));
					}
					if (!MailBnfHelper.SkipCFWS(this.disposition, ref num))
					{
						throw new FormatException(SR.GetString("ContentDispositionInvalid"));
					}
					string value;
					if (this.disposition[num] == '"')
					{
						value = MailBnfHelper.ReadQuotedString(this.disposition, ref num, null);
					}
					else
					{
						value = MailBnfHelper.ReadToken(this.disposition, ref num, null);
					}
					if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(value))
					{
						throw new FormatException(SR.GetString("ContentDispositionInvalid"));
					}
					this.Parameters.Add(text, value);
				}
			}
			catch (FormatException innerException)
			{
				throw new FormatException(SR.GetString("ContentDispositionInvalid"), innerException);
			}
			this.parameters.IsChanged = false;
		}

		// Token: 0x040016EE RID: 5870
		private string dispositionType;

		// Token: 0x040016EF RID: 5871
		private TrackingValidationObjectDictionary parameters;

		// Token: 0x040016F0 RID: 5872
		private bool isChanged;

		// Token: 0x040016F1 RID: 5873
		private bool isPersisted;

		// Token: 0x040016F2 RID: 5874
		private string disposition;

		// Token: 0x040016F3 RID: 5875
		private const string creationDate = "creation-date";

		// Token: 0x040016F4 RID: 5876
		private const string readDate = "read-date";

		// Token: 0x040016F5 RID: 5877
		private const string modificationDate = "modification-date";

		// Token: 0x040016F6 RID: 5878
		private const string size = "size";

		// Token: 0x040016F7 RID: 5879
		private const string fileName = "filename";

		// Token: 0x040016F8 RID: 5880
		private static readonly TrackingValidationObjectDictionary.ValidateAndParseValue dateParser = (object value) => new SmtpDateTime(value.ToString());

		// Token: 0x040016F9 RID: 5881
		private static readonly TrackingValidationObjectDictionary.ValidateAndParseValue longParser = delegate(object value)
		{
			long num;
			if (!long.TryParse(value.ToString(), NumberStyles.None, CultureInfo.InvariantCulture, out num))
			{
				throw new FormatException(SR.GetString("ContentDispositionInvalid"));
			}
			return num;
		};

		// Token: 0x040016FA RID: 5882
		private static readonly IDictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> validators = new Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue>();
	}
}
