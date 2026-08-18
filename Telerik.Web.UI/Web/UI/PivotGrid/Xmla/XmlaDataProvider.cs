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

namespace Telerik.Web.UI.PivotGrid.Xmla
{
	// Token: 0x02000D99 RID: 3481
	public sealed class XmlaDataProvider : OlapDataProvider
	{
		// Token: 0x0600817A RID: 33146 RVA: 0x001D87E9 File Offset: 0x001D69E9
		public XmlaDataProvider() : this(new XmlaWebClient())
		{
		}

		// Token: 0x0600817B RID: 33147 RVA: 0x001D87F6 File Offset: 0x001D69F6
		internal XmlaDataProvider(IXmlaClient xmlaClient) : this(xmlaClient, null)
		{
		}

		// Token: 0x0600817C RID: 33148 RVA: 0x001D8800 File Offset: 0x001D6A00
		internal XmlaDataProvider(IXmlaClient xmlaClient, IFieldDescriptionProvider fieldInfoProvider) : this(new PivotSettings<XmlaFilterDescription, XmlaGroupDescription, XmlaAggregateDescription>(), xmlaClient, fieldInfoProvider)
		{
			base.DeferUpdates = true;
			this.engine = new OlapEngine();
		}

		// Token: 0x0600817D RID: 33149 RVA: 0x001D8824 File Offset: 0x001D6A24
		internal XmlaDataProvider(PivotSettings<XmlaFilterDescription, XmlaGroupDescription, XmlaAggregateDescription> settings, IXmlaClient xmlaClient, IFieldDescriptionProvider fieldInfoProvider) : base(settings, fieldInfoProvider)
		{
			this.settings = settings;
			this.settings.DataProvider = this;
			this.settings.DescriptionAdded += this.SettingsDescriptionAdded;
			this.engine = new OlapEngine();
			if (xmlaClient != null)
			{
				this.xmlaClient = xmlaClient;
				this.xmlaClient.SendRequestCompleted += this.XmlaClient_RequestCompleted;
			}
		}

		// Token: 0x17002906 RID: 10502
		// (get) Token: 0x0600817E RID: 33150 RVA: 0x001D888F File Offset: 0x001D6A8F
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<XmlaFilterDescription> FilterDescriptions
		{
			get
			{
				return this.settings.FilterDescriptions;
			}
		}

		// Token: 0x17002907 RID: 10503
		// (get) Token: 0x0600817F RID: 33151 RVA: 0x001D889C File Offset: 0x001D6A9C
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<XmlaGroupDescription> RowGroupDescriptions
		{
			get
			{
				return this.settings.RowGroupDescriptions;
			}
		}

		// Token: 0x17002908 RID: 10504
		// (get) Token: 0x06008180 RID: 33152 RVA: 0x001D88A9 File Offset: 0x001D6AA9
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<XmlaGroupDescription> ColumnGroupDescriptions
		{
			get
			{
				return this.settings.ColumnGroupDescriptions;
			}
		}

		// Token: 0x17002909 RID: 10505
		// (get) Token: 0x06008181 RID: 33153 RVA: 0x001D88B6 File Offset: 0x001D6AB6
		[SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Justification = "Design choice.")]
		public Collection<XmlaAggregateDescription> AggregateDescriptions
		{
			get
			{
				return this.settings.AggregateDescriptions;
			}
		}

		// Token: 0x1700290A RID: 10506
		// (get) Token: 0x06008182 RID: 33154 RVA: 0x001D88C3 File Offset: 0x001D6AC3
		// (set) Token: 0x06008183 RID: 33155 RVA: 0x001D88CB File Offset: 0x001D6ACB
		public XmlaConnectionSettings ConnectionSettings
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

		// Token: 0x1700290B RID: 10507
		// (get) Token: 0x06008184 RID: 33156 RVA: 0x001D88FE File Offset: 0x001D6AFE
		// (set) Token: 0x06008185 RID: 33157 RVA: 0x001D8906 File Offset: 0x001D6B06
		public int SetConditionListCapacity { get; set; }

		// Token: 0x1700290C RID: 10508
		// (get) Token: 0x06008186 RID: 33158 RVA: 0x001D890F File Offset: 0x001D6B0F
		protected override IPivotResults Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x1700290D RID: 10509
		// (get) Token: 0x06008187 RID: 33159 RVA: 0x001D8917 File Offset: 0x001D6B17
		public override object State
		{
			get
			{
				return this.ConnectionSettings;
			}
		}

		// Token: 0x06008188 RID: 33160 RVA: 0x001D8920 File Offset: 0x001D6B20
		protected override void RefreshOverride()
		{
			if (base.FieldInfos == null)
			{
				this.refreshRequested = true;
				this.RefreshFieldDescriptions();
				return;
			}
			this.InitializeDescriptions();
			if (this.AggregateDescriptions.Count > 0 || this.ColumnGroupDescriptions.Count > 0 || this.RowGroupDescriptions.Count > 0)
			{
				this.GenerateAndExecuteXmlaRequest();
				return;
			}
			this.ClearResults();
		}

		// Token: 0x06008189 RID: 33161 RVA: 0x001D8980 File Offset: 0x001D6B80
		internal override void OnPivotSettingsChanged(object sender, SettingsChangedEventArgs e)
		{
			base.OnPivotSettingsChanged(sender, e);
			if (base.FieldInfos != null)
			{
				this.InitializeDescriptions();
			}
		}

		// Token: 0x0600818A RID: 33162 RVA: 0x001D8998 File Offset: 0x001D6B98
		private void ClearResults()
		{
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.RetrievingData, false, null));
			this.results = OlapDataProvider.GetEmptyResults();
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Ready, true, null));
		}

		// Token: 0x0600818B RID: 33163 RVA: 0x001D89CD File Offset: 0x001D6BCD
		private void InitializeDescriptions()
		{
			this.InitializeRowGroupDescriptions();
			this.InitializeColumnGroupDescriptions();
			this.InitializeAggregateDescriptions();
			this.InitializeFilterDescriptions();
		}

		// Token: 0x0600818C RID: 33164 RVA: 0x001D89E8 File Offset: 0x001D6BE8
		private void InitializeAggregateDescriptions()
		{
			List<XmlaAggregateDescription> list = this.AggregateDescriptions.ToList<XmlaAggregateDescription>();
			foreach (XmlaAggregateDescription xmlaAggregateDescription in list)
			{
				this.InitializeDescription(xmlaAggregateDescription);
				if (xmlaAggregateDescription.FieldInfo == null)
				{
					this.AggregateDescriptions.Remove(xmlaAggregateDescription);
				}
			}
		}

		// Token: 0x0600818D RID: 33165 RVA: 0x001D8A58 File Offset: 0x001D6C58
		private void InitializeColumnGroupDescriptions()
		{
			List<XmlaGroupDescription> list = this.ColumnGroupDescriptions.ToList<XmlaGroupDescription>();
			foreach (XmlaGroupDescription xmlaGroupDescription in list)
			{
				this.InitializeDescription(xmlaGroupDescription);
				if (xmlaGroupDescription.FieldInfo == null)
				{
					this.ColumnGroupDescriptions.Remove(xmlaGroupDescription);
				}
			}
		}

		// Token: 0x0600818E RID: 33166 RVA: 0x001D8AC8 File Offset: 0x001D6CC8
		private void InitializeRowGroupDescriptions()
		{
			List<XmlaGroupDescription> list = this.RowGroupDescriptions.ToList<XmlaGroupDescription>();
			foreach (XmlaGroupDescription xmlaGroupDescription in list)
			{
				this.InitializeDescription(xmlaGroupDescription);
				if (xmlaGroupDescription.FieldInfo == null)
				{
					this.RowGroupDescriptions.Remove(xmlaGroupDescription);
				}
			}
		}

		// Token: 0x0600818F RID: 33167 RVA: 0x001D8B38 File Offset: 0x001D6D38
		private void InitializeFilterDescriptions()
		{
			List<XmlaFilterDescription> list = this.FilterDescriptions.ToList<XmlaFilterDescription>();
			foreach (XmlaFilterDescription xmlaFilterDescription in list)
			{
				this.InitializeDescription(xmlaFilterDescription);
				if (xmlaFilterDescription.FieldInfo == null)
				{
					this.FilterDescriptions.Remove(xmlaFilterDescription);
				}
			}
		}

		// Token: 0x06008190 RID: 33168 RVA: 0x001D8BA8 File Offset: 0x001D6DA8
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

		// Token: 0x06008191 RID: 33169 RVA: 0x001D8BC8 File Offset: 0x001D6DC8
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

		// Token: 0x06008192 RID: 33170 RVA: 0x001D8C07 File Offset: 0x001D6E07
		private void RequestFieldInfos()
		{
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Initializing, false, null));
			base.FieldDescriptionsProvider.GetDescriptionsDataAsyncCompleted += this.FieldDescriptionsProvider_GetDescriptionsDataAsyncCompleted;
			base.FieldDescriptionsProvider.GetDescriptionsDataAsync(this.State);
		}

		// Token: 0x06008193 RID: 33171 RVA: 0x001D8C48 File Offset: 0x001D6E48
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

		// Token: 0x06008194 RID: 33172 RVA: 0x001D8CD1 File Offset: 0x001D6ED1
		public override void BlockUntilRefreshCompletes()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008195 RID: 33173 RVA: 0x001D8CD8 File Offset: 0x001D6ED8
		protected override IFieldDescriptionProvider CreateFieldDescriptionsProvider()
		{
			return new XmlaFieldDescriptionProvider(this.ConnectionSettings);
		}

		// Token: 0x06008196 RID: 33174 RVA: 0x001D8CF4 File Offset: 0x001D6EF4
		private void GenerateAndExecuteXmlaRequest()
		{
			XmlaClientRequestInfo clientRequestInfo = this.GenerateXmlaRequestInfo();
			try
			{
				this.ExecuteXmlaRequest(clientRequestInfo);
			}
			catch (OlapCommunicationException error)
			{
				this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Faulted, false, error));
			}
		}

		// Token: 0x06008197 RID: 33175 RVA: 0x001D8D38 File Offset: 0x001D6F38
		private XmlaClientRequestInfo GenerateXmlaRequestInfo()
		{
			OlapPivotConfiguration olapPivotConfiguration = OlapPivotConfiguration.FromDataProviderCloned(this);
			XmlaMethodBase method = this.GenerateExecuteMethod(olapPivotConfiguration);
			string xmlaRequest = method.ToXml();
			return new XmlaClientRequestInfo(xmlaRequest, this.ConnectionSettings, olapPivotConfiguration);
		}

		// Token: 0x06008198 RID: 33176 RVA: 0x001D8D6A File Offset: 0x001D6F6A
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Will fix soon.")]
		private void ExecuteXmlaRequest(XmlaClientRequestInfo clientRequestInfo)
		{
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.RetrievingData, false, null));
			this.xmlaClient.SendRequestAsync(clientRequestInfo);
		}

		// Token: 0x06008199 RID: 33177 RVA: 0x001D8D8C File Offset: 0x001D6F8C
		private XmlaMethodBase GenerateExecuteMethod(OlapPivotConfiguration pivotConfiguration)
		{
			MdxQueryBuilder mdxQueryBuilder = new MdxQueryBuilder(this.ConnectionSettings.Cube, pivotConfiguration);
			string value = mdxQueryBuilder.BuildQuery();
			XmlaTextBodyCommand commandToExecute = (XmlaTextBodyCommand)XmlaCommands.Statement(value);
			XmlaMethodExecute xmlaMethodExecute = new XmlaMethodExecute(commandToExecute);
			xmlaMethodExecute.AddProperty(XmlaProperties.Catalog(this.connectionSettings.Database));
			xmlaMethodExecute.AddProperty(XmlaProperties.Format(XmlaFormatTypes.Multidimensional));
			xmlaMethodExecute.AddProperty(XmlaProperties.Content(XmlaContentTypes.Data));
			xmlaMethodExecute.MergeProperties(this.connectionSettings.QueryProperties);
			return xmlaMethodExecute;
		}

		// Token: 0x0600819A RID: 33178 RVA: 0x001D8E08 File Offset: 0x001D7008
		private void XmlaClient_RequestCompleted(object sender, XmlaClientRequestCompletedEventArgs e)
		{
			if (e.Error == null)
			{
				OlapProcessedResponseInfo info = XmlaDataProvider.ProcessResponse(e);
				this.GenerateFinalResults(info);
				return;
			}
			this.OnStatusChanged(new DataProviderStatusChangedEventArgs(base.Status, DataProviderStatus.Faulted, false, e.Error));
		}

		// Token: 0x0600819B RID: 33179 RVA: 0x001D8E48 File Offset: 0x001D7048
		private static OlapProcessedResponseInfo ProcessResponse(XmlaClientRequestCompletedEventArgs e)
		{
			IOlapResponseData responseData = XmlaDataProvider.GetResponseData(e.Result, e.RequestInfo);
			OlapResponseProcessor olapResponseProcessor = new OlapResponseProcessor(responseData);
			return olapResponseProcessor.Process();
		}

		// Token: 0x0600819C RID: 33180 RVA: 0x001D8E78 File Offset: 0x001D7078
		private static IOlapResponseData GetResponseData(string response, XmlaClientRequestInfo requestInfo)
		{
			return new XmlaResponseData(requestInfo.PivotConfiguration, response);
		}

		// Token: 0x0600819D RID: 33181 RVA: 0x001D8E94 File Offset: 0x001D7094
		private void GenerateFinalResults(OlapProcessedResponseInfo info)
		{
			PivotResultsProcessingState stateFromProcessedResponse = XmlaDataProvider.GetStateFromProcessedResponse(info);
			this.engine.Completed += this.OnEngineCompleted;
			EventCompletionSource<CompositeEngineCompletedEventArgs> eventCompletionSource = new EventCompletionSource<CompositeEngineCompletedEventArgs>(this.engine, "Completed");
			this.engine.Run(stateFromProcessedResponse);
			if (base.ExecutionStrategy == OperationExecutionStrategy.Blocking)
			{
				eventCompletionSource.AwaitEvent();
			}
			eventCompletionSource.Dispose();
		}

		// Token: 0x0600819E RID: 33182 RVA: 0x001D8F28 File Offset: 0x001D7128
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

		// Token: 0x0600819F RID: 33183 RVA: 0x001D906C File Offset: 0x001D726C
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

		// Token: 0x060081A0 RID: 33184 RVA: 0x001D90D0 File Offset: 0x001D72D0
		protected override IAggregateDescription GetAggregateDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			XmlaAggregateDescription xmlaAggregateDescription = new XmlaAggregateDescription();
			xmlaAggregateDescription.MemberName = description.Name;
			this.InitializeDescription(xmlaAggregateDescription);
			return xmlaAggregateDescription;
		}

		// Token: 0x060081A1 RID: 33185 RVA: 0x001D90F8 File Offset: 0x001D72F8
		protected override IGroupDescription GetGroupDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			XmlaGroupDescription xmlaGroupDescription = new XmlaGroupDescription();
			xmlaGroupDescription.MemberName = description.Name;
			this.InitializeDescription(xmlaGroupDescription);
			return xmlaGroupDescription;
		}

		// Token: 0x060081A2 RID: 33186 RVA: 0x001D911F File Offset: 0x001D731F
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		public override IEnumerable<object> GetAggregateFunctionsForAggregateDescription(IAggregateDescription aggregateDescription)
		{
			return null;
		}

		// Token: 0x060081A3 RID: 33187 RVA: 0x001D9122 File Offset: 0x001D7322
		[Obsolete("Not used. Obsoleted after 2013.Q2.SP1")]
		public override void SetAggregateFunctionToAggregateDescription(IAggregateDescription aggregateDescription, object aggregateFunction)
		{
		}

		// Token: 0x060081A4 RID: 33188 RVA: 0x001D9124 File Offset: 0x001D7324
		protected override FilterDescription GetFilterDescriptionForFieldDescriptionCore(IPivotFieldInfo description)
		{
			XmlaFilterDescription xmlaFilterDescription = new XmlaFilterDescription();
			xmlaFilterDescription.MemberName = description.Name;
			this.InitializeDescription(xmlaFilterDescription);
			return xmlaFilterDescription;
		}

		// Token: 0x060081A5 RID: 33189 RVA: 0x001D914C File Offset: 0x001D734C
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

		// Token: 0x040023B9 RID: 9145
		private IXmlaClient xmlaClient;

		// Token: 0x040023BA RID: 9146
		private IPivotResults results;

		// Token: 0x040023BB RID: 9147
		private bool refreshRequested;

		// Token: 0x040023BC RID: 9148
		private OlapEngine engine;

		// Token: 0x040023BD RID: 9149
		private XmlaConnectionSettings connectionSettings;

		// Token: 0x040023BE RID: 9150
		private PivotSettings<XmlaFilterDescription, XmlaGroupDescription, XmlaAggregateDescription> settings;
	}
}
