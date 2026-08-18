using System;
using System.Web.UI;

namespace Telerik.Web.UI.DropDownTree
{
	// Token: 0x02000B2B RID: 2859
	internal interface ITreeNodeAdapter<T> : ITreeNodeBase where T : ITreeNodeBase
	{
		// Token: 0x17002328 RID: 9000
		// (get) Token: 0x06006B5F RID: 27487
		// (set) Token: 0x06006B60 RID: 27488
		string Text { get; set; }

		// Token: 0x17002329 RID: 9001
		// (get) Token: 0x06006B61 RID: 27489
		// (set) Token: 0x06006B62 RID: 27490
		bool Selected { get; set; }

		// Token: 0x1700232A RID: 9002
		// (get) Token: 0x06006B63 RID: 27491
		// (set) Token: 0x06006B64 RID: 27492
		bool Checked { get; set; }

		// Token: 0x1700232B RID: 9003
		// (get) Token: 0x06006B65 RID: 27493
		// (set) Token: 0x06006B66 RID: 27494
		string Value { get; set; }

		// Token: 0x1700232C RID: 9004
		// (get) Token: 0x06006B67 RID: 27495
		// (set) Token: 0x06006B68 RID: 27496
		string CssClass { get; set; }

		// Token: 0x06006B69 RID: 27497
		string FullPath(string delimiter);

		// Token: 0x1700232D RID: 9005
		// (get) Token: 0x06006B6A RID: 27498
		string ID { get; }

		// Token: 0x1700232E RID: 9006
		// (get) Token: 0x06006B6B RID: 27499
		object DataItem { get; }

		// Token: 0x1700232F RID: 9007
		// (get) Token: 0x06006B6C RID: 27500
		int Level { get; }

		// Token: 0x17002330 RID: 9008
		// (get) Token: 0x06006B6D RID: 27501
		// (set) Token: 0x06006B6E RID: 27502
		bool Checkable { get; set; }

		// Token: 0x17002331 RID: 9009
		// (get) Token: 0x06006B6F RID: 27503
		// (set) Token: 0x06006B70 RID: 27504
		bool Expanded { get; set; }

		// Token: 0x06006B71 RID: 27505
		T GetTreeViewNode();

		// Token: 0x06006B72 RID: 27506
		Control FindControl(string controlID);
	}
}
