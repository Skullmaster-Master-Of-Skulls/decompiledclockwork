using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Design;
using Telerik.Web.UI.PivotGrid.Core.Engine;
using Telerik.Web.UI.PivotGrid.Core.Fields;
using Telerik.Web.UI.PivotGrid.Core.Internal;
using Telerik.Web.UI.PivotGrid.Core.Olap;

namespace Telerik.Web.UI.PivotGrid.DataProviders.Adomd
{
	// Token: 0x02000D57 RID: 3415
	public sealed class AdomdDataProvider : OlapDataProvider
	{
		// Token: 0x06007F4E RID: 32590 RVA: 0x001D15C2 File Offset: 0x001CF7C2
		public AdomdDataProvider() : this(new DefaultAdomdClient())
		{
			base.DeferUpdates = true;
		}

		// Token: 0x06007F4F RID: 32591 RVA: 0x001D15D6 File Offset: 0x001CF7D6
		internal AdomdDataProvider(IAdomdClient adomdClient) : this(adomdClient, null)
		{
			this.adomdClient = adomdClient;
			this.adomdClient.SendRequestCompleted += this.AdomdClientRequestCompleted;
		}

		// Token: 0x06007F50 RID: 32592 RVA: 0x001D15FE File Offset: 0x001CF7FE
		internal AdomdDataProvider(IAdomdClient adomdClient, IFieldDescriptionProvider fieldInfoProvider) : this(new PivotSettings<AdomdFilterDescription, AdomdGroupDescription, AdomdAggregateDescription>(), adomdClient, fieldInfoProvider)
		{
			base.DeferUpdates = true;
			this.engine = new OlapEngine();
		}

		// Token: 0x06007F51 RID: 32593 RVA: 0x001D1620 File Offset: 0x001CF820
		internal AdomdDataProvider(PivotSettings<AdomdFilterDescription, AdomdGroupDescription, AdomdAggregateDescription> settings, IAdomdClient adomdClient, IFieldDescriptionProvider fieldInfoProvider) : base(settings, fieldInfoProvider)
		{
			this.settings = settings;
			this.settings.DataProvider = this;
			this.settings.DescriptionAdded += this.SettingsDescriptionAdded;
			this.engine = new OlapEngine();
			if (adomdClient != null)
			{
				this.adomdClient = adomdClient;
				this.adomdClient.SendRequestCompleted += this.AdomdClientRequestCompleted;
			}
		}

		// Token: 0x17002891 RID: 10385
		// (get) Token: 0x06007F52 RID: 32594 RVA: 0x001D168B File Offset: 0x001CF88B
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<AdomdFilterDescription> FilterDescriptions
		{
			get
			{
				return this.settings.FilterDescriptions;
			}
		}

		// Token: 0x17002892 RID: 10386
		// (get) Token: 0x06007F53 RID: 32595 RVA: 0x001D1698 File Offset: 0x001CF898
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<AdomdGroupDescription> RowGroupDescriptions
		{
			get
			{
				return this.settings.RowGroupDescriptions;
			}
		}

		// Token: 0x17002893 RID: 10387
		// (get) Token: 0x06007F54 RID: 32596 RVA: 0x001D16A5 File Offset: 0x001CF8A5
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<AdomdGroupDescription> ColumnGroupDescriptions
		{
			get
			{
				return this.settings.ColumnGroupDescriptions;
			}
		}

		// Token: 0x17002894 RID: 10388
		// (get) Token: 0x06007F55 RID: 32597 RVA: 0x001D16B2 File Offset: 0x001CF8B2
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<AdomdAggregateDescription> AggregateDescriptions
		{
			get
			{
				return this.settings.AggregateDescriptions;
			}
		}

		// Token: 0x17002895 RID: 10389
		// (get) Token: 0x06007F56 RID: 32598 RVA: 0x001D16BF File Offset: 0x001CF8BF
		protected override IPivotResults Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x17002896 RID: 10390
		// (get) Token: 0x06007F57 RID: 32599 RVA: 0x001D16C7 File Offset: 0x001CF8C7
		// (set) Token: 0x06007F58 RID: 32600 RVA: 0x001D16CF File Offset: 0x001CF8CF
		public int SetConditionListCapacity { get; set; }

		// Token: 0x17002897 RID: 10391
		// (get) Token: 0x06007F59 RID: 32601 RVA: 0x001D16D8 File Offset: 0x001CF8D8
		// (set) Token: 0x06007F5A RID: 32602 RVA: 0x001D16E0 File Offset: 0x001CF8E0
		public AdomdConnectionSettings ConnectionSettings
		{
			get
			{
				return this.connectionSettings;
			}
			set
			{
				if (this.connectionSettings != value)
				{
					this.connectionSettings = value;
					base.OnPropertyChanged("ConnectionSettings");
					base.OnPropertyChanged("State");
					base.Invalidate();
				}
			}
		}

		// Token: 0x17002898 RID: 10392
		// (get) Token: 0x06007F5B RID: 32603 RVA: 0x001D1713 File Offset: 0x001CF913
		public override object State
		{
			get
			{
				return this.ConnectionSettings;
			}
		}

		// Token: 0x06007F5C RID: 32604 RVA: 0x001D1720 File Offset: 0x001CF920
		protected override void RefreshOverride()
		{
			if (base.FieldInfos == null)
			{
				this.refreshRequested = true;
				this.RefreshFieldDescriptions();
				return;
			}
			this.InitializeDescriptions();
			bool flag = this.AggregateDescriptions.Count > 0 || this.ColumnGroupDescriptions.Count > 0 || this.RowGroupDescriptions.Count > 0;
			if (flag)
			{
				this.GenerateAndExecuteAdomdRequest();
				return;
			}
			this.ClearResults();
		}

		// Token: 0x06007F5D RID: 32605 RVA: 0x001D1787 File Offset: 0x001CF987
		internal override void OnPivotSettingsChanged(object sender, SettingsChangedEventArgs e)
		{
			base.OnPivotSettingsChanged(sender, e);
			if (base.FieldInfos != null)
			{
				this.InitializeDescriptions();
			}
		}

		// Token: 0x06007F5E RID: 32606 RVA: 0x001D179F File Offset: 0x001CF99F
		private void ClearResults()
		{
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.RetrievingData, false, null));
			this.results = OlapDataProvider.GetEmptyResults();
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Ready, true, null));
		}

		// Token: 0x06007F5F RID: 32607 RVA: 0x001D17D4 File Offset: 0x001CF9D4
		private void InitializeDescriptions()
		{
			this.InitializeRowGroupDescriptions();
			this.InitializeColumnGroupDescriptions();
			this.InitializeAggregateDescriptions();
			this.InitializeFilterDescriptions();
		}

		// Token: 0x06007F60 RID: 32608 RVA: 0x001D17F0 File Offset: 0x001CF9F0
		private void InitializeAggregateDescriptions()
		{
			List<AdomdAggregateDescription> list = this.AggregateDescriptions.ToList<AdomdAggregateDescription>();
			foreach (AdomdAggregateDescription adomdAggregateDescription in list)
			{
				this.InitializeDescription(adomdAggregateDescription);
				if (adomdAggregateDescription.FieldInfo == null)
				{
					this.AggregateDescriptions.Remove(adomdAggregateDescription);
				}
			}
		}

		// Token: 0x06007F61 RID: 32609 RVA: 0x001D1860 File Offset: 0x001CFA60
		private void InitializeColumnGroupDescriptions()
		{
			List<AdomdGroupDescription> list = this.ColumnGroupDescriptions.ToList<AdomdGroupDescription>();
			foreach (AdomdGroupDescription adomdGroupDescription in list)
			{
				this.InitializeDescription(adomdGroupDescription);
				if (adomdGroupDescription.FieldInfo == null)
				{
					this.ColumnGroupDescriptions.Remove(adomdGroupDescription);
				}
			}
		}

		// Token: 0x06007F62 RID: 32610 RVA: 0x001D18D0 File Offset: 0x001CFAD0
		private void InitializeRowGroupDescriptions()
		{
			List<AdomdGroupDescription> list = this.RowGroupDescriptions.ToList<AdomdGroupDescription>();
			foreach (AdomdGroupDescription adomdGroupDescription in list)
			{
				this.InitializeDescription(adomdGroupDescription);
				if (adomdGroupDescription.FieldInfo == null)
				{
					this.RowGroupDescriptions.Remove(adomdGroupDescription);
				}
			}
		}

		// Token: 0x06007F63 RID: 32611 RVA: 0x001D1940 File Offset: 0x001CFB40
		private void InitializeFilterDescriptions()
		{
			List<AdomdFilterDescription> list = this.FilterDescriptions.ToList<AdomdFilterDescription>();
			foreach (AdomdFilterDescription adomdFilterDescription in list)
			{
				this.InitializeDescription(adomdFilterDescription);
				if (adomdFilterDescription.FieldInfo == null)
				{
					this.FilterDescriptions.Remove(adomdFilterDescription);
				}
			}
		}

		// Token: 0x06007F64 RID: 32612 RVA: 0x001D19B0 File Offset: 0x001CFBB0
		private void InitializeDescription(IInitializeDescription description)
		{
			if (base.FieldInfos == null || description == null)
			{
				return;
			}
			if (description != null && !description.Initialized)
			{
				description.Initialize(this);
			}
		}

		// Token: 0x06007F65 RID: 32613 RVA: 0x001D19D0 File Offset: 0x001CFBD0
		private void RefreshFieldDescriptions()
		{
			if (Designer.IsInDesignMode)
			{
				return;
			}
			base.FieldInfos = null;
			bool flag = base.FieldDescriptionsProvider != null && !base.FieldDescriptionsProvider.IsBusy;
			if (flag)
			{
				this.RequestFieldInfos();
			}
		}

		// Token: 0x06007F66 RID: 32614 RVA: 0x001D1A0F File Offset: 0x001CFC0F
		private void RequestFieldInfos()
		{
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Initializing, false, null));
			base.FieldDescriptionsProvider.GetDescriptionsDataAsyncCompleted += this.FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted;
			base.FieldDescriptionsProvider.GetDescriptionsDataAsync(this.State);
		}

		// Token: 0x06007F67 RID: 32615 RVA: 0x001D1A50 File Offset: 0x001CFC50
		private void FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted(object sender, GetDescriptionsDataCompletedEventArgs e)
		{
			IFieldDescriptionProvider fieldDescriptionProvider = sender as IFieldDescriptionProvider;
			if (fieldDescriptionProvider != null)
			{
				fieldDescriptionProvider.GetDescriptionsDataAsyncCompleted -= this.FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted;
			}
			if (base.FieldDescriptionsProvider != fieldDescriptionProvider)
			{
				return;
			}
			if (e.Error == null)
			{
				base.FieldInfos = e.DescriptionsData;
				this.InitializeDescriptions();
				this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Ready, false, null));
				if (this.refreshRequested)
				{
					base.Refresh();
					return;
				}
			}
			else
			{
				this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Uninitialized, false, e.Error));
			}
		}

		// Token: 0x06007F68 RID: 32616 RVA: 0x001D1AD9 File Offset: 0x001CFCD9
		public override void BlockUntilRefreshCompletes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06007F69 RID: 32617 RVA: 0x001D1AE0 File Offset: 0x001CFCE0
		protected override IFieldDescriptionProvider CreateFieldDescriptionsProvider()
		{
			return new AdomdFieldDescriptionProvider(this.ConnectionSettings);
		}

		// Token: 0x06007F6A RID: 32618 RVA: 0x001D1AFC File Offset: 0x001CFCFC
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Design choice")]
		private void GenerateAndExecuteAdomdRequest()
		{
			AdomdClientRequestInfo clientRequestInfo = this.GenerateAdomdRequestInfo();
			try
			{
				this.ExecuteAdomdRequest(clientRequestInfo);
			}
			catch (OlapCommunicationException error)
			{
				this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Faulted, false, error));
			}
		}

		// Token: 0x06007F6B RID: 32619 RVA: 0x001D1B40 File Offset: 0x001CFD40
		private AdomdClientRequestInfo GenerateAdomdRequestInfo()
		{
			OlapPivotConfiguration pivotConfiguration = OlapPivotConfiguration.FromDataProviderCloned(this);
			string mdxQuery = this.GenerateMdxQuery(pivotConfiguration);
			return new AdomdClientRequestInfo(mdxQuery, this.ConnectionSettings, pivotConfiguration);
		}

		// Token: 0x06007F6C RID: 32620 RVA: 0x001D1B6B File Offset: 0x001CFD6B
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Will fix soon.")]
		private void ExecuteAdomdRequest(AdomdClientRequestInfo clientRequestInfo)
		{
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.RetrievingData, false, null));
			this.adomdClient.SendRequestAsync(clientRequestInfo);
		}

		// Token: 0x06007F6D RID: 32621 RVA: 0x001D1B90 File Offset: 0x001CFD90
		private void AdomdClientRequestCompleted(object sender, AdomdClientRequestCompletedEventArgs e)
		{
			if (e.Error == null)
			{
				OlapProcessedResponseInfo info = AdomdDataProvider.ProcessResponse(e);
				this.GenerateFinalResults(info);
				return;
			}
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Faulted, false, e.Error));
		}

		// Token: 0x06007F6E RID: 32622 RVA: 0x001D1BD0 File Offset: 0x001CFDD0
		private void GenerateFinalResults(OlapProcessedResponseInfo info)
		{
			PivotResultsProcessingState stateFromProcessedResponse = AdomdDataProvider.GetStateFromProcessedResponse(info);
			this.engine.Completed += this.OnEngineCompleted;
			EventCompletionSource<CompositeEngineCompletedEventArgs> eventCompletionSource = new EventCompletionSource<CompositeEngineCompletedEventArgs>(this.engine, "Completed");
			this.engine.Run(stateFromProcessedResponse);
			if (base.ExecutionStrategy == OperationExecutionStrategy.Blocking)
			{
				eventCompletionSource.AwaitEvent();
			}
			eventCompletionSource.Dispose();
		}

		// Token: 0x06007F6F RID: 32623 RVA: 0x001D1C64 File Offset: 0x001CFE64
		private static PivotResultsProcessingState GetStateFromProcessedResponse(OlapProcessedResponseInfo info)
		{
			OlapAggregateResultProvider aggregatesProvider = new OlapAggregateResultProvider(info.RootCoordinate, info.Aggregates);
			List<GroupDescription> source = (from l in info.PivotConfiguration.PivotRowGroupDescriptions
			select (GroupDescription)l.Clone()).ToList<GroupDescription>();
			List<GroupDescription> source2 = (from l in info.PivotConfiguration.PivotColumnGroupDescriptions
			select (GroupDescription)l.Clone()).ToList<GroupDescription>();
			List<IAggregateDescription> source3 = (from l in info.PivotConfiguration.PivotAggregateDescriptions
			select (IAggregateDescription)l.Clone()).ToList<IAggregateDescription>();
			List<FilterDescription> source4 = (from l in info.PivotConfiguration.PivotFilterDescriptions
			select (FilterDescription)l.Clone()).ToList<FilterDescription>();
			ReadOnlyList<GroupDescription, GroupDescription> rowGroupDescriptions = new ReadOnlyList<GroupDescription, GroupDescription>(source);
			ReadOnlyList<GroupDescription, GroupDescription> columnGroupDescriptions = new ReadOnlyList<GroupDescription, GroupDescription>(source2);
			ReadOnlyList<IAggregateDescription, IAggregateDescription> aggregateDescriptions = new ReadOnlyList<IAggregateDescription, IAggregateDescription>(source3);
			ReadOnlyList<FilterDescription, FilterDescription> filterDescriptions = new ReadOnlyList<FilterDescription, FilterDescription>(source4);
			return new PivotResultsProcessingState
			{
				AggregatesProvider = aggregatesProvider,
				AggregateDescriptions = aggregateDescriptions,
				ColumnGroupDescriptions = columnGroupDescriptions,
				RowGroupDescriptions = rowGroupDescriptions,
				FilterDescriptions = filterDescriptions
			};
		}

		// Token: 0x06007F70 RID: 32624 RVA: 0x001D1DA8 File Offset: 0x001CFFA8
		private void OnEngineCompleted(object sender, CompositeEngineCompletedEventArgs e)
		{
			this.engine.Completed -= this.OnEngineCompleted;
			if (e.Error == null)
			{
				this.results = this.engine.Result;
			}
			else
			{
				this.results = OlapDataProvider.GetEmptyResults();
			}
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Ready, true, e.Error));
		}

		// Token: 0x06007F71 RID: 32625 RVA: 0x001D1E0C File Offset: 0x001D000C
		private static OlapProcessedResponseInfo ProcessResponse(AdomdClientRequestCompletedEventArgs e)
		{
			AdomdResponseData responseData = new AdomdResponseData(e.RequestInfo.PivotConfiguration, e.Result);
			OlapResponseProcessor olapResponseProcessor = new OlapResponseProcessor(responseData);
			return olapResponseProcessor.Process();
		}

		// Token: 0x06007F72 RID: 32626 RVA: 0x001D1E40 File Offset: 0x001D0040
		private string GenerateMdxQuery(IOlapPivotConfiguration pivotConfiguration)
		{
			MdxQueryBuilder mdxQueryBuilder = new MdxQueryBuilder(this.ConnectionSettings.Cube, pivotConfiguration);
			return mdxQueryBuilder.BuildQuery();
		}

		// Token: 0x06007F73 RID: 32627 RVA: 0x001D1E6C File Offset: 0x001D006C
		protected override IAggregateDescription GetAggregateDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			AdomdAggregateDescription adomdAggregateDescription = new AdomdAggregateDescription();
			adomdAggregateDescription.MemberName = description.Name;
			this.InitializeDescription(adomdAggregateDescription);
			return adomdAggregateDescription;
		}

		// Token: 0x06007F74 RID: 32628 RVA: 0x001D1E94 File Offset: 0x001D0094
		protected override IGroupDescription GetGroupDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			AdomdGroupDescription adomdGroupDescription = new AdomdGroupDescription();
			adomdGroupDescription.MemberName = description.Name;
			this.InitializeDescription(adomdGroupDescription);
			return adomdGroupDescription;
		}

		// Token: 0x06007F75 RID: 32629 RVA: 0x001D1EBB File Offset: 0x001D00BB
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		public override IEnumerable<object> GetAggregateFunctionsForAggregateDescription(IAggregateDescription aggregateDescription)
		{
			return null;
		}

		// Token: 0x06007F76 RID: 32630 RVA: 0x001D1EBE File Offset: 0x001D00BE
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		public override void SetAggregateFunctionToAggregateDescription(IAggregateDescription aggregateDescription, object aggregateFunction)
		{
		}

		// Token: 0x06007F77 RID: 32631 RVA: 0x001D1EC0 File Offset: 0x001D00C0
		protected override FilterDescription GetFilterDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			AdomdFilterDescription adomdFilterDescription = new AdomdFilterDescription();
			adomdFilterDescription.MemberName = description.Name;
			this.InitializeDescription(adomdFilterDescription);
			return adomdFilterDescription;
		}

		// Token: 0x06007F78 RID: 32632 RVA: 0x001D1EE8 File Offset: 0x001D00E8
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

		// Token: 0x0400230D RID: 8973
		private IAdomdClient adomdClient;

		// Token: 0x0400230E RID: 8974
		private IPivotResults results;

		// Token: 0x0400230F RID: 8975
		private bool refreshRequested;

		// Token: 0x04002310 RID: 8976
		private OlapEngine engine;

		// Token: 0x04002311 RID: 8977
		private AdomdConnectionSettings connectionSettings;

		// Token: 0x04002312 RID: 8978
		private PivotSettings<AdomdFilterDescription, AdomdGroupDescription, AdomdAggregateDescription> settings;
	}
}
