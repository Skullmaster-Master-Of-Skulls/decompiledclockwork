using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001AF0 RID: 6896
	[Serializable]
	public class RadComboBoxContext : Dictionary<string, object>
	{
		// Token: 0x17005139 RID: 20793
		// (get) Token: 0x06010B17 RID: 68375 RVA: 0x003B7A12 File Offset: 0x003B5C12
		// (set) Token: 0x06010B18 RID: 68376 RVA: 0x003B7A24 File Offset: 0x003B5C24
		public int NumberOfItems
		{
			get
			{
				return (int)base["NumberOfItems"];
			}
			set
			{
				base["NumberOfItems"] = value;
			}
		}

		// Token: 0x1700513A RID: 20794
		// (get) Token: 0x06010B19 RID: 68377 RVA: 0x003B7A37 File Offset: 0x003B5C37
		// (set) Token: 0x06010B1A RID: 68378 RVA: 0x003B7A49 File Offset: 0x003B5C49
		public string Text
		{
			get
			{
				return (string)base["Text"];
			}
			set
			{
				base["Text"] = value;
			}
		}
	}
}
