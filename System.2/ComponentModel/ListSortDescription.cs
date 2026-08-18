using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000588 RID: 1416
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ListSortDescription
	{
		// Token: 0x06003440 RID: 13376 RVA: 0x000E4D58 File Offset: 0x000E2F58
		public ListSortDescription(PropertyDescriptor property, ListSortDirection direction)
		{
			this.property = property;
			this.sortDirection = direction;
		}

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x06003441 RID: 13377 RVA: 0x000E4D6E File Offset: 0x000E2F6E
		// (set) Token: 0x06003442 RID: 13378 RVA: 0x000E4D76 File Offset: 0x000E2F76
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.property;
			}
			set
			{
				this.property = value;
			}
		}

		// Token: 0x17000CC6 RID: 3270
		// (get) Token: 0x06003443 RID: 13379 RVA: 0x000E4D7F File Offset: 0x000E2F7F
		// (set) Token: 0x06003444 RID: 13380 RVA: 0x000E4D87 File Offset: 0x000E2F87
		public ListSortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			set
			{
				this.sortDirection = value;
			}
		}

		// Token: 0x040029E9 RID: 10729
		private PropertyDescriptor property;

		// Token: 0x040029EA RID: 10730
		private ListSortDirection sortDirection;
	}
}
