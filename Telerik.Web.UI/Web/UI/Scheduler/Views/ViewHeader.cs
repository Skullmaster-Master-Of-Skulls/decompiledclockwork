using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02001AA6 RID: 6822
	internal class ViewHeader
	{
		// Token: 0x17005008 RID: 20488
		// (get) Token: 0x060107C4 RID: 67524 RVA: 0x003AF4CD File Offset: 0x003AD6CD
		// (set) Token: 0x060107C5 RID: 67525 RVA: 0x003AF4D5 File Offset: 0x003AD6D5
		public string Text { get; set; }

		// Token: 0x17005009 RID: 20489
		// (get) Token: 0x060107C6 RID: 67526 RVA: 0x003AF4DE File Offset: 0x003AD6DE
		// (set) Token: 0x060107C7 RID: 67527 RVA: 0x003AF4E6 File Offset: 0x003AD6E6
		public IList<ViewHeader> SubHeaders { get; set; }

		// Token: 0x1700500A RID: 20490
		// (get) Token: 0x060107C8 RID: 67528 RVA: 0x003AF4EF File Offset: 0x003AD6EF
		// (set) Token: 0x060107C9 RID: 67529 RVA: 0x003AF4F7 File Offset: 0x003AD6F7
		public string ClassName { get; set; }

		// Token: 0x1700500B RID: 20491
		// (get) Token: 0x060107CA RID: 67530 RVA: 0x003AF500 File Offset: 0x003AD700
		// (set) Token: 0x060107CB RID: 67531 RVA: 0x003AF508 File Offset: 0x003AD708
		public Unit? InnerHeight { get; set; }

		// Token: 0x1700500C RID: 20492
		// (get) Token: 0x060107CC RID: 67532 RVA: 0x003AF511 File Offset: 0x003AD711
		// (set) Token: 0x060107CD RID: 67533 RVA: 0x003AF519 File Offset: 0x003AD719
		public int ColumnSpan { get; set; }

		// Token: 0x1700500D RID: 20493
		// (get) Token: 0x060107CE RID: 67534 RVA: 0x003AF524 File Offset: 0x003AD724
		public int Depth
		{
			get
			{
				int num = 0;
				foreach (ViewHeader viewHeader in this.SubHeaders)
				{
					num = Math.Max(num, viewHeader.Depth);
				}
				return num + 1;
			}
		}

		// Token: 0x1700500E RID: 20494
		// (get) Token: 0x060107CF RID: 67535 RVA: 0x003AF57C File Offset: 0x003AD77C
		// (set) Token: 0x060107D0 RID: 67536 RVA: 0x003AF584 File Offset: 0x003AD784
		public Resource Resource { get; set; }

		// Token: 0x1700500F RID: 20495
		// (get) Token: 0x060107D1 RID: 67537 RVA: 0x003AF58D File Offset: 0x003AD78D
		// (set) Token: 0x060107D2 RID: 67538 RVA: 0x003AF595 File Offset: 0x003AD795
		public DateTime Date { get; set; }

		// Token: 0x17005010 RID: 20496
		// (get) Token: 0x060107D3 RID: 67539 RVA: 0x003AF59E File Offset: 0x003AD79E
		// (set) Token: 0x060107D4 RID: 67540 RVA: 0x003AF5A6 File Offset: 0x003AD7A6
		public bool SubHeadersVisible
		{
			get
			{
				return this._subHeadersVisible;
			}
			set
			{
				this._subHeadersVisible = value;
			}
		}

		// Token: 0x060107D5 RID: 67541 RVA: 0x003AF5AF File Offset: 0x003AD7AF
		public ViewHeader()
		{
			this.SubHeaders = new List<ViewHeader>();
			this.ColumnSpan = 1;
		}

		// Token: 0x040049D5 RID: 18901
		private bool _subHeadersVisible = true;
	}
}
