using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001B69 RID: 7017
	public class RadTreeNodeBindingCollection : NavigationItemBindingCollection
	{
		// Token: 0x17005306 RID: 21254
		public RadTreeNodeBinding this[int index]
		{
			get
			{
				return (RadTreeNodeBinding)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
