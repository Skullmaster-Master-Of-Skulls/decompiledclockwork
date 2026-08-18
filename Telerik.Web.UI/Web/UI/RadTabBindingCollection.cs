using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001ADA RID: 6874
	public class RadTabBindingCollection : NavigationItemBindingCollection
	{
		// Token: 0x170050F9 RID: 20729
		public RadTabBinding this[int index]
		{
			get
			{
				return (RadTabBinding)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}
