using System;

namespace Spire.Xls.Core
{
	// Token: 0x020005DB RID: 1499
	public interface IHyperLink : IExcelApplication
	{
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06005958 RID: 22872
		// (set) Token: 0x06005959 RID: 22873
		string Address { get; set; }

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x0600595A RID: 22874
		string Name { get; }

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x0600595B RID: 22875
		IXLSRange Range { get; }

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x0600595C RID: 22876
		// (set) Token: 0x0600595D RID: 22877
		string ScreenTip { get; set; }

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x0600595E RID: 22878
		// (set) Token: 0x0600595F RID: 22879
		string SubAddress { get; set; }

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06005960 RID: 22880
		// (set) Token: 0x06005961 RID: 22881
		string TextToDisplay { get; set; }

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x06005962 RID: 22882
		// (set) Token: 0x06005963 RID: 22883
		HyperLinkType Type { get; set; }
	}
}
