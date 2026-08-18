using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C90 RID: 3216
	public interface IPivotSettings : INotifyPropertyChanged, ISupportInitialize
	{
		// Token: 0x14000127 RID: 295
		// (add) Token: 0x060078BC RID: 30908
		// (remove) Token: 0x060078BD RID: 30909
		event EventHandler<EventArgs> DescriptionsChanged;

		// Token: 0x14000128 RID: 296
		// (add) Token: 0x060078BE RID: 30910
		// (remove) Token: 0x060078BF RID: 30911
		event EventHandler<SettingsChangedEventArgs> SettingsChanged;

		// Token: 0x170026F8 RID: 9976
		// (get) Token: 0x060078C0 RID: 30912
		IList FilterDescriptions { get; }

		// Token: 0x170026F9 RID: 9977
		// (get) Token: 0x060078C1 RID: 30913
		IList RowGroupDescriptions { get; }

		// Token: 0x170026FA RID: 9978
		// (get) Token: 0x060078C2 RID: 30914
		IList ColumnGroupDescriptions { get; }

		// Token: 0x170026FB RID: 9979
		// (get) Token: 0x060078C3 RID: 30915
		IList AggregateDescriptions { get; }

		// Token: 0x170026FC RID: 9980
		// (get) Token: 0x060078C4 RID: 30916
		// (set) Token: 0x060078C5 RID: 30917
		int AggregatesLevel { get; set; }

		// Token: 0x170026FD RID: 9981
		// (get) Token: 0x060078C6 RID: 30918
		// (set) Token: 0x060078C7 RID: 30919
		PivotAxis AggregatesPosition { get; set; }

		// Token: 0x060078C8 RID: 30920
		IDisposable BeginEdit();
	}
}
