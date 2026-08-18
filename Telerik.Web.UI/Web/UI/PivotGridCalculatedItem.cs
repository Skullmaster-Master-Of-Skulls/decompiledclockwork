using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000767 RID: 1895
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridCalculatedItem : StateManager, INamingContainer
	{
		// Token: 0x060042D4 RID: 17108 RVA: 0x000D09B7 File Offset: 0x000CEBB7
		protected override void TrackViewState()
		{
			if (this.IsTrackingViewState)
			{
				base.TrackViewState();
				return;
			}
			base.TrackViewState();
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000D09D0 File Offset: 0x000CEBD0
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int num = 0;
				base.LoadViewState(array[num++]);
			}
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000D09F8 File Offset: 0x000CEBF8
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState()
			}.ToArray(typeof(object));
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x060042D8 RID: 17112 RVA: 0x000D0A30 File Offset: 0x000CEC30
		// (set) Token: 0x060042D9 RID: 17113 RVA: 0x000D0A38 File Offset: 0x000CEC38
		public virtual string GroupName { get; set; }

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x060042DA RID: 17114 RVA: 0x000D0A41 File Offset: 0x000CEC41
		// (set) Token: 0x060042DB RID: 17115 RVA: 0x000D0A49 File Offset: 0x000CEC49
		public virtual int SolveOrder { get; set; }
	}
}
