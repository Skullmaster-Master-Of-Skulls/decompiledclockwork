using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020016B0 RID: 5808
	public class BinaryImageFilterCollection : List<BinaryImageFilter>
	{
		// Token: 0x170044B7 RID: 17591
		public BinaryImageFilter this[string filterName]
		{
			get
			{
				return base.Find((BinaryImageFilter x) => x.Name == filterName);
			}
		}
	}
}
