using System;
using System.Collections;
using System.Text;

namespace ClockWorkAPI.AT
{
	// Token: 0x02000019 RID: 25
	public class VendorItemCollection : CollectionBase
	{
		// Token: 0x060000DA RID: 218 RVA: 0x000067DC File Offset: 0x000057DC
		public int Add(VendorItem vendorItem)
		{
			return base.List.Add(vendorItem);
		}

		// Token: 0x17000050 RID: 80
		public VendorItem this[int index]
		{
			get
			{
				return (VendorItem)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006830 File Offset: 0x00005830
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("count=" + base.List.Count.ToString() + "; ");
			foreach (object obj in base.List)
			{
				VendorItem vendorItem = (VendorItem)obj;
				stringBuilder.Append("[" + vendorItem.ToString() + "] ");
			}
			return stringBuilder.ToString();
		}
	}
}
