using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B4D RID: 6989
	public class RadPanelItemBindingCollection : NavigationItemBindingCollection
	{
		// Token: 0x170052A3 RID: 21155
		public RadPanelItemBinding this[int index]
		{
			get
			{
				return (RadPanelItemBinding)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
