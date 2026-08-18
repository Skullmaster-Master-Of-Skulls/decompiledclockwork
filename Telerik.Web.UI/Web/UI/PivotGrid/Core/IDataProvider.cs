using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000C8A RID: 3210
	public interface IDataProvider : INotifyPropertyChanged, ISupportInitialize
	{
		// Token: 0x14000121 RID: 289
		// (add) Token: 0x06007850 RID: 30800
		// (remove) Token: 0x06007851 RID: 30801
		event EventHandler<DataProviderStatusChangedEventArgs> StatusChanged;

		// Token: 0x14000122 RID: 290
		// (add) Token: 0x06007852 RID: 30802
		// (remove) Token: 0x06007853 RID: 30803
		event EventHandler<PrepareDescriptionForFieldEventArgs> PrepareDescriptionForField;

		// Token: 0x170026DA RID: 9946
		// (get) Token: 0x06007854 RID: 30804
		DataProviderStatus Status { get; }

		// Token: 0x170026DB RID: 9947
		// (get) Token: 0x06007855 RID: 30805
		IPivotResults Results { get; }

		// Token: 0x170026DC RID: 9948
		// (get) Token: 0x06007856 RID: 30806
		IPivotSettings Settings { get; }

		// Token: 0x170026DD RID: 9949
		// (get) Token: 0x06007857 RID: 30807
		IFieldInfoData FieldInfos { get; }

		// Token: 0x170026DE RID: 9950
		// (get) Token: 0x06007858 RID: 30808
		// (set) Token: 0x06007859 RID: 30809
		PivotAxis AggregatesPosition { get; set; }

		// Token: 0x170026DF RID: 9951
		// (get) Token: 0x0600785A RID: 30810
		// (set) Token: 0x0600785B RID: 30811
		int AggregatesLevel { get; set; }

		// Token: 0x170026E0 RID: 9952
		// (get) Token: 0x0600785C RID: 30812
		object State { get; }

		// Token: 0x170026E1 RID: 9953
		// (get) Token: 0x0600785D RID: 30813
		// (set) Token: 0x0600785E RID: 30814
		bool DeferUpdates { get; set; }

		// Token: 0x170026E2 RID: 9954
		// (get) Token: 0x0600785F RID: 30815
		bool HasPendingChanges { get; }

		// Token: 0x06007860 RID: 30816
		void Refresh();

		// Token: 0x06007861 RID: 30817
		void BlockUntilRefreshCompletes();

		// Token: 0x06007862 RID: 30818
		IDisposable DeferRefresh();

		// Token: 0x06007863 RID: 30819
		IAggregateDescription GetAggregateDescriptionForFieldDescription(IPivotFieldInfo info);

		// Token: 0x06007864 RID: 30820
		IGroupDescription GetGroupDescriptionForFieldDescription(IPivotFieldInfo info);

		// Token: 0x06007865 RID: 30821
		FilterDescription GetFilterDescriptionForFieldDescription(IPivotFieldInfo info);

		// Token: 0x06007866 RID: 30822
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		IEnumerable<object> GetAggregateFunctionsForAggregateDescription(IAggregateDescription aggregateDescription);

		// Token: 0x06007867 RID: 30823
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		void SetAggregateFunctionToAggregateDescription(IAggregateDescription aggregateDescription, object aggregateFunction);
	}
}
