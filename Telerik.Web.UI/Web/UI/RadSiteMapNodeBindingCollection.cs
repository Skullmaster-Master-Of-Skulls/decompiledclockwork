using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001AB9 RID: 6841
	public class RadSiteMapNodeBindingCollection : NavigationItemBindingCollection
	{
		// Token: 0x17005062 RID: 20578
		public RadSiteMapNodeBinding this[int index]
		{
			get
			{
				return (RadSiteMapNodeBinding)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
