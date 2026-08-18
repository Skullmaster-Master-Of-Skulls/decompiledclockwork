using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;

namespace System.Net.Mime
{
	// Token: 0x0200068A RID: 1674
	internal class HeaderCollection : NameValueCollection
	{
		// Token: 0x060033DE RID: 13278 RVA: 0x000DB0E0 File Offset: 0x000DA0E0
		internal HeaderCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x000DB0F0 File Offset: 0x000DA0F0
		public override void Remove(string name)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, "Remove", name);
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"name"
				}), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this.part != null)
			{
				this.part.ContentType = null;
			}
			else if (id == MailHeaderID.ContentDisposition && this.part is MimePart)
			{
				((MimePart)this.part).ContentDisposition = null;
			}
			base.Remove(name);
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x000DB1A0 File Offset: 0x000DA1A0
		public override string Get(string name)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, "Get", name);
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"name"
				}), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this.part != null)
			{
				this.part.ContentType.PersistIfNeeded(this, false);
			}
			else if (id == MailHeaderID.ContentDisposition && this.part is MimePart)
			{
				((MimePart)this.part).ContentDisposition.PersistIfNeeded(this, false);
			}
			return base.Get(name);
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x000DB25C File Offset: 0x000DA25C
		public override string[] GetValues(string name)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, "Get", name);
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"name"
				}), "name");
			}
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this.part != null)
			{
				this.part.ContentType.PersistIfNeeded(this, false);
			}
			else if (id == MailHeaderID.ContentDisposition && this.part is MimePart)
			{
				((MimePart)this.part).ContentDisposition.PersistIfNeeded(this, false);
			}
			return base.GetValues(name);
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x000DB317 File Offset: 0x000DA317
		internal void InternalRemove(string name)
		{
			base.Remove(name);
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x000DB320 File Offset: 0x000DA320
		internal void InternalSet(string name, string value)
		{
			base.Set(name, value);
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x000DB32C File Offset: 0x000DA32C
		public override void Set(string name, string value)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, "Set", name.ToString() + "=" + value.ToString());
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"name"
				}), "name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"value"
				}), "name");
			}
			if (!MimeBasePart.IsAscii(name, false))
			{
				throw new FormatException(SR.GetString("InvalidHeaderName"));
			}
			if (!MimeBasePart.IsAnsi(value, false))
			{
				throw new FormatException(SR.GetString("InvalidHeaderValue"));
			}
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this.part != null)
			{
				this.part.ContentType.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (id == MailHeaderID.ContentDisposition && this.part is MimePart)
			{
				((MimePart)this.part).ContentDisposition.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			base.Set(name, value);
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x000DB48C File Offset: 0x000DA48C
		public override void Add(string name, string value)
		{
			if (Logging.On)
			{
				Logging.PrintInfo(Logging.Web, this, "Add", name.ToString() + "=" + value.ToString());
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (name == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"name"
				}), "name");
			}
			if (value == string.Empty)
			{
				throw new ArgumentException(SR.GetString("net_emptystringcall", new object[]
				{
					"value"
				}), "name");
			}
			MailBnfHelper.ValidateHeaderName(name);
			if (!MimeBasePart.IsAnsi(value, false))
			{
				throw new FormatException(SR.GetString("InvalidHeaderValue"));
			}
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			if (id == MailHeaderID.ContentType && this.part != null)
			{
				this.part.ContentType.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (id == MailHeaderID.ContentDisposition && this.part is MimePart)
			{
				((MimePart)this.part).ContentDisposition.Set(value.ToLower(CultureInfo.InvariantCulture), this);
				return;
			}
			if (MailHeaderInfo.IsSingleton(name))
			{
				base.Set(name, value);
				return;
			}
			base.Add(name, value);
		}

		// Token: 0x04002FCD RID: 12237
		private MimeBasePart part;
	}
}
