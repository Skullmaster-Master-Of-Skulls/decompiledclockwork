using System;
using System.ComponentModel;

namespace Telerik.Web.Data
{
	// Token: 0x02001BA7 RID: 7079
	public class SortDescriptor
	{
		// Token: 0x17005394 RID: 21396
		// (get) Token: 0x060111EF RID: 70127 RVA: 0x003C6829 File Offset: 0x003C4A29
		// (set) Token: 0x060111F0 RID: 70128 RVA: 0x003C6831 File Offset: 0x003C4A31
		public string Member
		{
			get
			{
				return this.member;
			}
			set
			{
				if (this.member != value)
				{
					this.member = value;
				}
			}
		}

		// Token: 0x17005395 RID: 21397
		// (get) Token: 0x060111F1 RID: 70129 RVA: 0x003C6848 File Offset: 0x003C4A48
		// (set) Token: 0x060111F2 RID: 70130 RVA: 0x003C6850 File Offset: 0x003C4A50
		public ListSortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			set
			{
				if (this.sortDirection != value)
				{
					this.sortDirection = value;
				}
			}
		}

		// Token: 0x04004CAB RID: 19627
		private string member;

		// Token: 0x04004CAC RID: 19628
		private ListSortDirection sortDirection;
	}
}
