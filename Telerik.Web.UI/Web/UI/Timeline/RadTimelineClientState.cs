using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI.Timeline
{
	// Token: 0x02000933 RID: 2355
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadTimelineClientState
	{
		// Token: 0x06005977 RID: 22903 RVA: 0x00110668 File Offset: 0x0010E868
		public RadTimelineClientState()
		{
			this.Enabled = true;
			this.Value = new List<object>();
			this.Text = string.Empty;
		}

		// Token: 0x17001D80 RID: 7552
		// (get) Token: 0x06005978 RID: 22904 RVA: 0x0011068D File Offset: 0x0010E88D
		// (set) Token: 0x06005979 RID: 22905 RVA: 0x00110695 File Offset: 0x0010E895
		public List<TimelineClientStateDataItem> SelectedItems { get; set; }

		// Token: 0x17001D81 RID: 7553
		// (get) Token: 0x0600597A RID: 22906 RVA: 0x0011069E File Offset: 0x0010E89E
		// (set) Token: 0x0600597B RID: 22907 RVA: 0x001106A6 File Offset: 0x0010E8A6
		public List<TimelineClientStateDataItem> DeselectedItems { get; set; }

		// Token: 0x17001D82 RID: 7554
		// (get) Token: 0x0600597C RID: 22908 RVA: 0x001106AF File Offset: 0x0010E8AF
		// (set) Token: 0x0600597D RID: 22909 RVA: 0x001106B7 File Offset: 0x0010E8B7
		public List<TimelineClientStateDataItem> SelectedDataItems { get; set; }

		// Token: 0x17001D83 RID: 7555
		// (get) Token: 0x0600597E RID: 22910 RVA: 0x001106C0 File Offset: 0x0010E8C0
		// (set) Token: 0x0600597F RID: 22911 RVA: 0x001106C8 File Offset: 0x0010E8C8
		public IEnumerable<object> Value { get; set; }

		// Token: 0x17001D84 RID: 7556
		// (get) Token: 0x06005980 RID: 22912 RVA: 0x001106D1 File Offset: 0x0010E8D1
		// (set) Token: 0x06005981 RID: 22913 RVA: 0x001106D9 File Offset: 0x0010E8D9
		public string Text { get; set; }

		// Token: 0x17001D85 RID: 7557
		// (get) Token: 0x06005982 RID: 22914 RVA: 0x001106E2 File Offset: 0x0010E8E2
		// (set) Token: 0x06005983 RID: 22915 RVA: 0x001106EA File Offset: 0x0010E8EA
		public bool Enabled { get; set; }
	}
}
