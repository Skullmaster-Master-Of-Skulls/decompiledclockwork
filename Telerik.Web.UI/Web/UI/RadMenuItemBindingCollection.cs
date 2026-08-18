using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B40 RID: 6976
	public class RadMenuItemBindingCollection : NavigationItemBindingCollection
	{
		// Token: 0x17005234 RID: 21044
		public RadMenuItemBinding this[int index]
		{
			get
			{
				return (RadMenuItemBinding)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
