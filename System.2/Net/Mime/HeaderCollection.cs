using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000245 RID: 581
	internal class HeaderCollection : NameValueCollection
	{
		// Token: 0x06001613 RID: 5651 RVA: 0x00071F63 File Offset: 0x00070163
		internal HeaderCollection() : base(StringComparer.OrdinalIgnoreCase)
		{
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00071F70 File Offset: 0x00070170
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

		// Token: 0x06001615 RID: 5653 RVA: 0x00072020 File Offset: 0x00070220
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

		// Token: 0x06001616 RID: 5654 RVA: 0x000720DC File Offset: 0x000702DC
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

		// Token: 0x06001617 RID: 5655 RVA: 0x00072195 File Offset: 0x00070395
		internal void InternalRemove(string name)
		{
			base.Remove(name);
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0007219E File Offset: 0x0007039E
		internal void InternalSet(string name, string value)
		{
			base.Set(name, value);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x000721A8 File Offset: 0x000703A8
		internal void InternalAdd(string name, string value)
		{
			if (MailHeaderInfo.IsSingleton(name))
			{
				base.Set(name, value);
				return;
			}
			base.Add(name, value);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x000721C4 File Offset: 0x000703C4
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
				}), "value");
			}
			if (!MimeBasePart.IsAscii(name, false))
			{
				throw new FormatException(SR.GetString("InvalidHeaderName"));
			}
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			value = value.Normalize(NormalizationForm.FormC);
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

		// Token: 0x0600161B RID: 5659 RVA: 0x00072310 File Offset: 0x00070510
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
				}), "value");
			}
			MailBnfHelper.ValidateHeaderName(name);
			name = MailHeaderInfo.NormalizeCase(name);
			MailHeaderID id = MailHeaderInfo.GetID(name);
			value = value.Normalize(NormalizationForm.FormC);
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
			this.InternalAdd(name, value);
		}

		// Token: 0x04001712 RID: 5906
		private MimeBasePart part;
	}
}
