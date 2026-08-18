using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Metadata
{
	// Token: 0x02000102 RID: 258
	public abstract class RoleDescriptor
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x0001F099 File Offset: 0x0001D299
		protected RoleDescriptor() : this(new Collection<Uri>())
		{
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001F0A6 File Offset: 0x0001D2A6
		protected RoleDescriptor(Collection<Uri> protocolsSupported)
		{
			this.protocolsSupported = protocolsSupported;
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001F0E1 File Offset: 0x0001D2E1
		public ICollection<ContactPerson> Contacts
		{
			get
			{
				return this.contacts;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x0001F0E9 File Offset: 0x0001D2E9
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0001F0F1 File Offset: 0x0001D2F1
		public Uri ErrorUrl
		{
			get
			{
				return this.errorUrl;
			}
			set
			{
				this.errorUrl = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0001F0FA File Offset: 0x0001D2FA
		public ICollection<KeyDescriptor> Keys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001F102 File Offset: 0x0001D302
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001F10A File Offset: 0x0001D30A
		public Organization Organization
		{
			get
			{
				return this.organization;
			}
			set
			{
				this.organization = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001F113 File Offset: 0x0001D313
		public ICollection<Uri> ProtocolsSupported
		{
			get
			{
				return this.protocolsSupported;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0001F11B File Offset: 0x0001D31B
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0001F123 File Offset: 0x0001D323
		public DateTime ValidUntil
		{
			get
			{
				return this.validUntil;
			}
			set
			{
				this.validUntil = value;
			}
		}

		// Token: 0x04000A91 RID: 2705
		private Collection<ContactPerson> contacts = new Collection<ContactPerson>();

		// Token: 0x04000A92 RID: 2706
		private Uri errorUrl;

		// Token: 0x04000A93 RID: 2707
		private Collection<KeyDescriptor> keys = new Collection<KeyDescriptor>();

		// Token: 0x04000A94 RID: 2708
		private Organization organization;

		// Token: 0x04000A95 RID: 2709
		private Collection<Uri> protocolsSupported = new Collection<Uri>();

		// Token: 0x04000A96 RID: 2710
		private DateTime validUntil = DateTime.MaxValue;
	}
}
