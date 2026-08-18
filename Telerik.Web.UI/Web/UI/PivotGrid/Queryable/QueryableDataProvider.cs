using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.DataProviders;
using Telerik.Web.UI.PivotGrid.Core.DataSouceView;
using Telerik.Web.UI.PivotGrid.Core.Design;
using Telerik.Web.UI.PivotGrid.Core.Engine;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.DataProviders.Queryable;

namespace Telerik.Web.UI.PivotGrid.Queryable
{
	// Token: 0x02000D6D RID: 3437
	public class QueryableDataProvider : DataProviderBase, IAggregateDescriptionsGenerator
	{
		// Token: 0x06008012 RID: 32786 RVA: 0x001D4801 File Offset: 0x001D2A01
		public QueryableDataProvider() : this(new PivotEngine())
		{
		}

		// Token: 0x06008013 RID: 32787 RVA: 0x001D480E File Offset: 0x001D2A0E
		internal QueryableDataProvider(IPivotEngine engine) : this(engine, null)
		{
		}

		// Token: 0x06008014 RID: 32788 RVA: 0x001D4818 File Offset: 0x001D2A18
		internal QueryableDataProvider(IPivotEngine engine, IFieldDescriptionProvider fieldInfoProvider) : this(new PivotSettings<QueryableFilterDescription, QueryableGroupDescription, QueryableAggregateDescriptionBase>(), engine, fieldInfoProvider)
		{
		}

		// Token: 0x06008015 RID: 32789 RVA: 0x001D4828 File Offset: 0x001D2A28
		internal QueryableDataProvider(PivotSettings<QueryableFilterDescription, QueryableGroupDescription, QueryableAggregateDescriptionBase> settings, IPivotEngine engine, IFieldDescriptionProvider fieldInfoProvider) : base(settings, fieldInfoProvider)
		{
			this.settings = settings;
			this.settings.DataProvider = this;
			this.settings.DescriptionAdded += this.SettingsDescriptionAdded;
			if (engine != null)
			{
				this.engine = engine;
				this.engine.Completed += this.OnPivotEngineCompleted;
			}
		}

		// Token: 0x170028B6 RID: 10422
		// (get) Token: 0x06008016 RID: 32790 RVA: 0x001D4893 File Offset: 0x001D2A93
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<QueryableFilterDescription> FilterDescriptions
		{
			get
			{
				return this.settings.FilterDescriptions;
			}
		}

		// Token: 0x170028B7 RID: 10423
		// (get) Token: 0x06008017 RID: 32791 RVA: 0x001D48A0 File Offset: 0x001D2AA0
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<QueryableGroupDescription> RowGroupDescriptions
		{
			get
			{
				return this.settings.RowGroupDescriptions;
			}
		}

		// Token: 0x170028B8 RID: 10424
		// (get) Token: 0x06008018 RID: 32792 RVA: 0x001D48AD File Offset: 0x001D2AAD
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<QueryableGroupDescription> ColumnGroupDescriptions
		{
			get
			{
				return this.settings.ColumnGroupDescriptions;
			}
		}

		// Token: 0x170028B9 RID: 10425
		// (get) Token: 0x06008019 RID: 32793 RVA: 0x001D48BA File Offset: 0x001D2ABA
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<QueryableAggregateDescriptionBase> AggregateDescriptions
		{
			get
			{
				return this.settings.AggregateDescriptions;
			}
		}

		// Token: 0x170028BA RID: 10426
		// (get) Token: 0x0600801A RID: 32794 RVA: 0x001D48C7 File Offset: 0x001D2AC7
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<CalculatedField> CalculatedFields
		{
			get
			{
				return this.calculatedFields;
			}
		}

		// Token: 0x170028BB RID: 10427
		// (get) Token: 0x0600801B RID: 32795 RVA: 0x001D48CF File Offset: 0x001D2ACF
		// (set) Token: 0x0600801C RID: 32796 RVA: 0x001D48D7 File Offset: 0x001D2AD7
		public IQueryable Source
		{
			get
			{
				return this.source;
			}
			set
			{
				if (this.source != value)
				{
					this.source = value;
					this.OnSourceChanged(value);
				}
			}
		}

		// Token: 0x170028BC RID: 10428
		// (get) Token: 0x0600801D RID: 32797 RVA: 0x001D48F0 File Offset: 0x001D2AF0
		protected override IPivotResults Results
		{
			get
			{
				return this.engine;
			}
		}

		// Token: 0x0600801E RID: 32798 RVA: 0x001D48F8 File Offset: 0x001D2AF8
		private void OnSourceChanged(object newValue)
		{
			if (newValue == null)
			{
				this.engine.Clear();
			}
			base.FieldInfos = null;
			base.RefreshOrDefer(DataProviderFlags.ResetStatus);
		}

		// Token: 0x0600801F RID: 32799 RVA: 0x001D4918 File Offset: 0x001D2B18
		protected override void RefreshOverride()
		{
			if (this.Source == null)
			{
				return;
			}
			if (base.FieldInfos == null)
			{
				this.refreshRequested = true;
				this.RefreshFieldDescriptions();
				return;
			}
			this.InitializeDescriptions();
			if (this.AggregateDescriptions.Count > 0 || this.ColumnGroupDescriptions.Count > 0 || this.RowGroupDescriptions.Count > 0 || this.FilterDescriptions.Count > 0)
			{
				this.GenerateAndExecuteQuery();
			}
		}

		// Token: 0x06008020 RID: 32800 RVA: 0x001D4988 File Offset: 0x001D2B88
		private void SettingsDescriptionAdded(object sender, PivotSettingsDescriptionAddedEventArgs e)
		{
			IInitializeDescription initializeDescription = e.Description as IInitializeDescription;
			bool flag = base.Status == DataProviderStatus.Ready;
			if (initializeDescription == null || !flag)
			{
				return;
			}
			this.InitializeDescription(initializeDescription);
		}

		// Token: 0x06008021 RID: 32801 RVA: 0x001D49BA File Offset: 0x001D2BBA
		private void InitializeDescriptions()
		{
			if (base.FieldInfos == null)
			{
				return;
			}
			this.InitializeDescriptionsCollection<QueryableFilterDescription>(this.FilterDescriptions);
			this.InitializeDescriptionsCollection<QueryableGroupDescription>(this.RowGroupDescriptions);
			this.InitializeDescriptionsCollection<QueryableGroupDescription>(this.ColumnGroupDescriptions);
			this.InitializeDescriptionsCollection<QueryableAggregateDescriptionBase>(this.AggregateDescriptions);
		}

		// Token: 0x06008022 RID: 32802 RVA: 0x001D49F8 File Offset: 0x001D2BF8
		private void InitializeDescriptionsCollection<T>(Collection<T> collection) where T : DescriptionBase
		{
			foreach (T t in collection.ToList<T>())
			{
				if (!this.InitializeDescription(t as IInitializeDescription))
				{
					collection.Remove(t);
				}
			}
		}

		// Token: 0x06008023 RID: 32803 RVA: 0x001D4A64 File Offset: 0x001D2C64
		private bool InitializeDescription(IInitializeDescription description)
		{
			if (description == null)
			{
				return false;
			}
			description.Initialize(this);
			return description.Initialized;
		}

		// Token: 0x06008024 RID: 32804 RVA: 0x001D4A78 File Offset: 0x001D2C78
		AggregateDescriptionBase IAggregateDescriptionsGenerator.GenerateAggregateDescription(RequiredField field)
		{
			AggregateDescriptionBase aggregateDescriptionBase;
			if (field.IsCalculated)
			{
				aggregateDescriptionBase = new QueryableCalculatedAggregateDescription
				{
					CalculatedFieldName = field.Name
				};
			}
			else
			{
				QueryableAggregateFunction aggregateFunction = QueryableAggregateFunction.Sum;
				if (field != null && field.AggregateFunction is QueryableAggregateFunction)
				{
					aggregateFunction = (QueryableAggregateFunction)field.AggregateFunction;
				}
				aggregateDescriptionBase = new QueryablePropertyAggregateDescription
				{
					PropertyName = field.Name,
					AggregateFunction = aggregateFunction
				};
			}
			((IInitializeDescription)aggregateDescriptionBase).Initialize(this);
			return aggregateDescriptionBase;
		}

		// Token: 0x06008025 RID: 32805 RVA: 0x001D4B00 File Offset: 0x001D2D00
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We should not catch general exceptions, however the IQueryable Source can vary.")]
		private void GenerateAndExecuteQuery()
		{
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.RetrievingData, false, null));
			RequiredAggregateDescriptionsGenerator requiredAggregateDescriptionsGenerator = new RequiredAggregateDescriptionsGenerator(this);
			List<AggregateDescriptionBase> aggregateDescriptions = (from l in this.AggregateDescriptions
			select (AggregateDescriptionBase)l.Clone()).ToList<AggregateDescriptionBase>();
			AggregateDescriptionInfo[] aggregateDescriptionInfos = requiredAggregateDescriptionsGenerator.AddRequiredAggregateDescriptions(aggregateDescriptions);
			List<QueryableAggregateDescriptionBase> aggregateDescriptions2 = (from l in aggregateDescriptions
			select (QueryableAggregateDescriptionBase)l).ToList<QueryableAggregateDescriptionBase>();
			try
			{
				QueryableGroupingInfo queryableGroupingInfo = new QueryableGroupingInfo(this.Source.ElementType, this.RowGroupDescriptions, this.ColumnGroupDescriptions, this.FilterDescriptions, aggregateDescriptions2);
				IQueryable resultQuery = queryableGroupingInfo.CreateQuery(this.Source);
				IList<PivotResultItem> enumerableSource = queryableGroupingInfo.ProcessQuery(resultQuery);
				ParallelState parallelState = QueryableDataProvider.GenerateParallelState(queryableGroupingInfo);
				parallelState.AggregateDescriptionInfos = aggregateDescriptionInfos;
				parallelState.AggregateDescriptionCount = this.AggregateDescriptions.Count;
				parallelState.ItemsSource = new EnumerableDataSourceView(enumerableSource);
				this.engine.RebuildCubeParallel(parallelState);
				this.refreshRequested = false;
			}
			catch (Exception error)
			{
				this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Faulted, false, error));
			}
		}

		// Token: 0x06008026 RID: 32806 RVA: 0x001D4C38 File Offset: 0x001D2E38
		private void RefreshFieldDescriptions()
		{
			if (!Designer.IsInDesignMode)
			{
				base.FieldInfos = null;
				bool flag = base.FieldDescriptionsProvider != null && !base.FieldDescriptionsProvider.IsBusy;
				if (flag && this.Source != null)
				{
					this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Initializing, false, null));
					base.FieldDescriptionsProvider.GetDescriptionsDataAsyncCompleted += this.FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted;
					base.FieldDescriptionsProvider.GetDescriptionsDataAsync(this);
				}
			}
		}

		// Token: 0x06008027 RID: 32807 RVA: 0x001D4CB0 File Offset: 0x001D2EB0
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "Design choice.")]
		private void FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted(object sender, GetDescriptionsDataCompletedEventArgs e)
		{
			IFieldDescriptionProvider fieldDescriptionProvider = sender as IFieldDescriptionProvider;
			this.StopListeningForGetDescriptionsData(fieldDescriptionProvider);
			QueryableDataProvider queryableDataProvider = e.State as QueryableDataProvider;
			if (base.FieldDescriptionsProvider != fieldDescriptionProvider || this != queryableDataProvider || this.Source != queryableDataProvider.Source)
			{
				return;
			}
			if (e.Error != null)
			{
				base.UpdateStatus(DataProviderStatus.Uninitialized, false, e.Error);
				return;
			}
			base.FieldInfos = e.DescriptionsData;
			this.InitializeDescriptions();
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Ready, false, null));
			if (this.refreshRequested)
			{
				base.Refresh();
			}
		}

		// Token: 0x06008028 RID: 32808 RVA: 0x001D4D3E File Offset: 0x001D2F3E
		private void StopListeningForGetDescriptionsData(IFieldDescriptionProvider filedDescriptionProvider)
		{
			if (filedDescriptionProvider != null)
			{
				filedDescriptionProvider.GetDescriptionsDataAsyncCompleted -= this.FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted;
			}
		}

		// Token: 0x06008029 RID: 32809 RVA: 0x001D4D55 File Offset: 0x001D2F55
		public override void BlockUntilRefreshCompletes()
		{
			this.engine.WaitForParallel();
		}

		// Token: 0x170028BD RID: 10429
		// (get) Token: 0x0600802A RID: 32810 RVA: 0x001D4D62 File Offset: 0x001D2F62
		public override object State
		{
			get
			{
				return this.Source;
			}
		}

		// Token: 0x0600802B RID: 32811 RVA: 0x001D4D6C File Offset: 0x001D2F6C
		private void OnPivotEngineCompleted(object sender, PivotEngineCompletedEventArgs e)
		{
			DataProviderStatus dataProviderStatusFromEngineStatus = DataProviderBase.GetDataProviderStatusFromEngineStatus(e.Status);
			Exception error = e.InnerExceptions.FirstOrDefault<Exception>();
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, dataProviderStatusFromEngineStatus, true, error));
		}

		// Token: 0x0600802C RID: 32812 RVA: 0x001D4DA5 File Offset: 0x001D2FA5
		protected override IFieldDescriptionProvider CreateFieldDescriptionsProvider()
		{
			return new QueryableFieldDescriptionsProvider();
		}

		// Token: 0x0600802D RID: 32813 RVA: 0x001D4DAC File Offset: 0x001D2FAC
		private static ParallelState GenerateParallelState(QueryableGroupingInfo gi)
		{
			ReadOnlyList<GroupDescription, GroupDescription> rowGroupDescriptions = new ReadOnlyList<GroupDescription, GroupDescription>(gi.RowGroupDescriptions.ToList<GroupDescription>());
			ReadOnlyList<GroupDescription, GroupDescription> columnGroupDescriptions = new ReadOnlyList<GroupDescription, GroupDescription>(gi.ColumnGroupDescriptions.ToList<GroupDescription>());
			ReadOnlyList<IAggregateDescription, IAggregateDescription> aggregateDescriptions = new ReadOnlyList<IAggregateDescription, IAggregateDescription>(gi.AggregateDescriptions.ToList<IAggregateDescription>());
			ReadOnlyList<FilterDescription, FilterDescription> filterDescriptions = new ReadOnlyList<FilterDescription, FilterDescription>(gi.FilterDescriptions.ToList<FilterDescription>());
			return new ParallelState
			{
				MaxDegreeOfParallelism = Environment.ProcessorCount,
				TaskScheduler = TaskScheduler.Default,
				RowGroupDescriptions = rowGroupDescriptions,
				ColumnGroupDescriptions = columnGroupDescriptions,
				AggregateDescriptions = aggregateDescriptions,
				ValueProvider = new QueryableValueProvider(gi),
				FilterDescriptions = filterDescriptions
			};
		}

		// Token: 0x0600802E RID: 32814 RVA: 0x001D4E50 File Offset: 0x001D3050
		protected override IAggregateDescription GetAggregateDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			if (description is CalculatedPivotFieldInfo)
			{
				return new QueryableCalculatedAggregateDescription
				{
					CalculatedFieldName = description.Name
				};
			}
			QueryablePropertyAggregateDescription queryablePropertyAggregateDescription = new QueryablePropertyAggregateDescription
			{
				PropertyName = description.Name
			};
			if (!FieldInfoHelper.IsNumericType(description.DataType))
			{
				queryablePropertyAggregateDescription.AggregateFunction = QueryableAggregateFunction.Count;
			}
			return queryablePropertyAggregateDescription;
		}

		// Token: 0x0600802F RID: 32815 RVA: 0x001D4EB0 File Offset: 0x001D30B0
		protected override IGroupDescription GetGroupDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			if (FieldInfoHelper.IsNumericType(description.DataType))
			{
				return new QueryableDoubleGroupDescription
				{
					PropertyName = description.Name
				};
			}
			if (description.GetType() == typeof(DateTimePropertyFieldInfo))
			{
				DateTimePropertyFieldInfo dateTimePropertyFieldInfo = description as DateTimePropertyFieldInfo;
				return new QueryableDateTimeGroupDescription
				{
					PropertyName = dateTimePropertyFieldInfo.PropertyName,
					Step = dateTimePropertyFieldInfo.DateTimeStep
				};
			}
			return new QueryablePropertyGroupDescription
			{
				PropertyName = description.Name
			};
		}

		// Token: 0x06008030 RID: 32816 RVA: 0x001D4F40 File Offset: 0x001D3140
		protected override FilterDescription GetFilterDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			if (description == null)
			{
				throw new ArgumentNullException("description");
			}
			return new QueryablePropertyFilterDescription
			{
				PropertyName = description.Name
			};
		}

		// Token: 0x06008031 RID: 32817 RVA: 0x001D50D8 File Offset: 0x001D32D8
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		public override IEnumerable<object> GetAggregateFunctionsForAggregateDescription(IAggregateDescription aggregateDescription)
		{
			if (aggregateDescription is QueryablePropertyAggregateDescription)
			{
				yield return QueryableAggregateFunction.Sum;
				yield return QueryableAggregateFunction.Count;
				yield return QueryableAggregateFunction.Average;
				yield return QueryableAggregateFunction.Min;
				yield return QueryableAggregateFunction.Max;
			}
			yield break;
		}

		// Token: 0x06008032 RID: 32818 RVA: 0x001D50FC File Offset: 0x001D32FC
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		public override void SetAggregateFunctionToAggregateDescription(IAggregateDescription aggregateDescription, object aggregateFunction)
		{
			QueryablePropertyAggregateDescription queryablePropertyAggregateDescription = aggregateDescription as QueryablePropertyAggregateDescription;
			if (queryablePropertyAggregateDescription != null && aggregateFunction is QueryableAggregateFunction)
			{
				QueryableAggregateFunction aggregateFunction2 = (QueryableAggregateFunction)aggregateFunction;
				queryablePropertyAggregateDescription.AggregateFunction = aggregateFunction2;
			}
		}

		// Token: 0x0400233B RID: 9019
		private IPivotEngine engine;

		// Token: 0x0400233C RID: 9020
		private IQueryable source;

		// Token: 0x0400233D RID: 9021
		private bool refreshRequested;

		// Token: 0x0400233E RID: 9022
		private Collection<CalculatedField> calculatedFields = new Collection<CalculatedField>();

		// Token: 0x0400233F RID: 9023
		private PivotSettings<QueryableFilterDescription, QueryableGroupDescription, QueryableAggregateDescriptionBase> settings;
	}
}
