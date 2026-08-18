using System;
using System.Collections;

namespace ClockWorkAPI.AT
{
	// Token: 0x02000077 RID: 119
	public class VendorCollection : CollectionBase
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x00020394 File Offset: 0x0001F394
		public VendorContactCollection Contacts
		{
			get
			{
				return this.contacts;
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000203AC File Offset: 0x0001F3AC
		public VendorCollection()
		{
			this.contacts = new VendorContactCollection();
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000203C4 File Offset: 0x0001F3C4
		public int Add(Vendor vendor)
		{
			return base.List.Add(vendor);
		}

		// Token: 0x1700024B RID: 587
		public Vendor this[int index]
		{
			get
			{
				return (Vendor)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00020418 File Offset: 0x0001F418
		public Vendor FindVendor(int vendorId)
		{
			foreach (object obj in base.List)
			{
				Vendor vendor = (Vendor)obj;
				if (vendor.VendorId == vendorId)
				{
					return vendor;
				}
			}
			return null;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00020494 File Offset: 0x0001F494
		public VendorContact FindVendorContact(int vendorContactId)
		{
			foreach (object obj in this.contacts)
			{
				VendorContact vendorContact = (VendorContact)obj;
				if (vendorContact.VendorContactId == vendorContactId)
				{
					return vendorContact;
				}
			}
			return null;
		}

		// Token: 0x04000311 RID: 785
		private VendorContactCollection contacts;
	}
}
