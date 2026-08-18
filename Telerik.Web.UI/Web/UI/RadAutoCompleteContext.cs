using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020009BD RID: 2493
	[Serializable]
	public class RadAutoCompleteContext : Dictionary<string, object>
	{
		// Token: 0x17001F73 RID: 8051
		// (get) Token: 0x06005F4F RID: 24399 RVA: 0x00122716 File Offset: 0x00120916
		// (set) Token: 0x06005F50 RID: 24400 RVA: 0x00122728 File Offset: 0x00120928
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

		// Token: 0x17001F74 RID: 8052
		// (get) Token: 0x06005F51 RID: 24401 RVA: 0x00122736 File Offset: 0x00120936
		// (set) Token: 0x06005F52 RID: 24402 RVA: 0x00122748 File Offset: 0x00120948
		public bool ShowAllResults
		{
			get
			{
				return (bool)base["ShowAllResults"];
			}
			set
			{
				base["ShowAllResults"] = value;
			}
		}
	}
}
