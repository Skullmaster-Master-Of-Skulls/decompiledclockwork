using System;
using System.Collections;

namespace ClockWorkAPI.AT
{
	// Token: 0x0200000A RID: 10
	public class VendorContactCollection : CollectionBase
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002BE0 File Offset: 0x00001BE0
		public int Add(VendorContact contact)
		{
			return base.List.Add(contact);
		}

		// Token: 0x1700000E RID: 14
		public VendorContact this[int index]
		{
			get
			{
				return (VendorContact)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}
	}
}
