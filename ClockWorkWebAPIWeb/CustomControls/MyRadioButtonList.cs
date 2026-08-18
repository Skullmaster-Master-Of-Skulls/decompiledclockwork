using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace ClockWorkWebAPIWeb.CustomControls
{
	// Token: 0x02000018 RID: 24
	public class MyRadioButtonList : RadioButtonList
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000141 RID: 321 RVA: 0x000105C0 File Offset: 0x0000E7C0
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000105D8 File Offset: 0x0000E7D8
		public Dictionary<int, int> LookupListItemAndControlIds
		{
			get
			{
				return this.lookupListItemAndControlIds;
			}
			set
			{
				this.lookupListItemAndControlIds = value;
			}
		}

		// Token: 0x04000080 RID: 128
		private Dictionary<int, int> lookupListItemAndControlIds = null;
	}
}
