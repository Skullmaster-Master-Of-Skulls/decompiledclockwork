using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D15 RID: 3349
	internal class OlapResponseProcessor : IOlapResponseProcessor
	{
		// Token: 0x06007CCC RID: 31948 RVA: 0x001CA13C File Offset: 0x001C833C
		public OlapResponseProcessor(IOlapResponseData responseData)
		{
			this.ResponseData = responseData;
			this.aggregates = new AggregatesDictionary(this.ResponseData.Configuration.PivotAggregateDescriptions);
		}

		// Token: 0x170027D2 RID: 10194
		// (get) Token: 0x06007CCD RID: 31949 RVA: 0x001CA166 File Offset: 0x001C8366
		// (set) Token: 0x06007CCE RID: 31950 RVA: 0x001CA16E File Offset: 0x001C836E
		public IOlapResponseData ResponseData { get; private set; }

		// Token: 0x170027D3 RID: 10195
		// (get) Token: 0x06007CCF RID: 31951 RVA: 0x001CA177 File Offset: 0x001C8377
		public bool ResponseHasAggregates
		{
			get
			{
				return this.ResponseData.Configuration.PivotAggregateDescriptions.Count > 0;
			}
		}

		// Token: 0x170027D4 RID: 10196
		// (get) Token: 0x06007CD0 RID: 31952 RVA: 0x001CA191 File Offset: 0x001C8391
		public bool ResponseHasAggregatesOnly
		{
			get
			{
				return this.ResponseData.Configuration.PivotAggregateDescriptions.Count > 0 && !this.ResponseHasColumnDescriptions && !this.ResponseHasRowDescriptions;
			}
		}

		// Token: 0x170027D5 RID: 10197
		// (get) Token: 0x06007CD1 RID: 31953 RVA: 0x001CA1C0 File Offset: 0x001C83C0
		public bool ResponseHasColumnDescriptionsOnly
		{
			get
			{
				return this.ResponseData.Configuration.PivotRowGroupDescriptions.Count == 0 && this.ResponseData.Configuration.PivotColumnGroupDescriptions.Count > 0 && this.ResponseData.Configuration.PivotAggregateDescriptions.Count == 0;
			}
		}

		// Token: 0x170027D6 RID: 10198
		// (get) Token: 0x06007CD2 RID: 31954 RVA: 0x001CA216 File Offset: 0x001C8416
		public bool TuplesForRowGroupsAreOnColumnAxis
		{
			get
			{
				return this.ResponseData.Configuration.PivotColumnGroupDescriptions.Count == 0 && this.ResponseData.Configuration.PivotAggregateDescriptions.Count == 0;
			}
		}

		// Token: 0x170027D7 RID: 10199
		// (get) Token: 0x06007CD3 RID: 31955 RVA: 0x001CA249 File Offset: 0x001C8449
		public bool ResponseHasColumnDescriptions
		{
			get
			{
				return this.ResponseData.Configuration.PivotColumnGroupDescriptions.Count > 0;
			}
		}

		// Token: 0x170027D8 RID: 10200
		// (get) Token: 0x06007CD4 RID: 31956 RVA: 0x001CA263 File Offset: 0x001C8463
		public bool ResponseHasRowDescriptions
		{
			get
			{
				return this.ResponseData.Configuration.PivotRowGroupDescriptions.Count > 0;
			}
		}

		// Token: 0x170027D9 RID: 10201
		// (get) Token: 0x06007CD5 RID: 31957 RVA: 0x001CA280 File Offset: 0x001C8480
		public bool ResponseHasColumnDescriptionsAndAggregates
		{
			get
			{
				return this.ResponseData.Configuration.PivotRowGroupDescriptions.Count == 0 && this.ResponseData.Configuration.PivotColumnGroupDescriptions.Count > 0 && this.ResponseData.Configuration.PivotAggregateDescriptions.Count > 0;
			}
		}

		// Token: 0x170027DA RID: 10202
		// (get) Token: 0x06007CD6 RID: 31958 RVA: 0x001CA2D8 File Offset: 0x001C84D8
		public bool ResponseHasRowDescriptionsOnly
		{
			get
			{
				return this.ResponseData.Configuration.PivotRowGroupDescriptions.Count > 0 && this.ResponseData.Configuration.PivotColumnGroupDescriptions.Count == 0 && this.ResponseData.Configuration.PivotAggregateDescriptions.Count == 0;
			}
		}

		// Token: 0x170027DB RID: 10203
		// (get) Token: 0x06007CD7 RID: 31959 RVA: 0x001CA330 File Offset: 0x001C8530
		public bool ResponseHasRowDescriptionsAndAggregates
		{
			get
			{
				return this.ResponseData.Configuration.PivotRowGroupDescriptions.Count > 0 && this.ResponseData.Configuration.PivotColumnGroupDescriptions.Count == 0 && this.ResponseData.Configuration.PivotAggregateDescriptions.Count > 0;
			}
		}

		// Token: 0x170027DC RID: 10204
		// (get) Token: 0x06007CD8 RID: 31960 RVA: 0x001CA386 File Offset: 0x001C8586
		public bool ResponseHasRowDescriptionsAndColumnDescriptions
		{
			get
			{
				return this.ResponseData.Configuration.PivotRowGroupDescriptions.Count > 0 && this.ResponseData.Configuration.PivotColumnGroupDescriptions.Count > 0;
			}
		}

		// Token: 0x06007CD9 RID: 31961 RVA: 0x001CA3BC File Offset: 0x001C85BC
		public OlapProcessedResponseInfo Process()
		{
			this.ProcessTuplesForColumnGroups();
			this.ProcessTuplesForRowGroups();
			this.ProcessCells();
			return this.CreateProcessedResponse();
		}

		// Token: 0x06007CDA RID: 31962 RVA: 0x001CA3E4 File Offset: 0x001C85E4
		private OlapTupleProcessorInput GetTupleInfoForRowGroups()
		{
			OlapTupleProcessorInput olapTupleProcessorInput = new OlapTupleProcessorInput();
			olapTupleProcessorInput.GroupDescriptions = this.ResponseData.Configuration.PivotRowGroupDescriptions;
			olapTupleProcessorInput.Tuples = this.ResponseData.RowAxisTuples;
			if (this.TuplesForRowGroupsAreOnColumnAxis)
			{
				olapTupleProcessorInput.Tuples = this.ResponseData.ColumnAxisTuples;
				olapTupleProcessorInput.AggregateDescriptions = this.ResponseData.Configuration.PivotAggregateDescriptions;
			}
			return olapTupleProcessorInput;
		}

		// Token: 0x06007CDB RID: 31963 RVA: 0x001CA450 File Offset: 0x001C8650
		public void ProcessTuplesForRowGroups()
		{
			OlapTupleProcessorInput tupleInfoForRowGroups = this.GetTupleInfoForRowGroups();
			OlapTupleProcessor olapTupleProcessor = new OlapTupleProcessor(tupleInfoForRowGroups);
			this.rowGroupingInfo = olapTupleProcessor.Process();
		}

		// Token: 0x06007CDC RID: 31964 RVA: 0x001CA478 File Offset: 0x001C8678
		private OlapTupleProcessorInput GetTupleInfoForColumnGroups()
		{
			OlapTupleProcessorInput olapTupleProcessorInput = new OlapTupleProcessorInput();
			olapTupleProcessorInput.GroupDescriptions = this.ResponseData.Configuration.PivotColumnGroupDescriptions;
			olapTupleProcessorInput.AggregateDescriptions = this.ResponseData.Configuration.PivotAggregateDescriptions;
			olapTupleProcessorInput.Tuples = this.ResponseData.ColumnAxisTuples;
			if (!this.ResponseHasColumnDescriptions)
			{
				olapTupleProcessorInput.Tuples = new List<IOlapTuple>();
			}
			return olapTupleProcessorInput;
		}

		// Token: 0x06007CDD RID: 31965 RVA: 0x001CA4DC File Offset: 0x001C86DC
		public void ProcessTuplesForColumnGroups()
		{
			OlapTupleProcessorInput tupleInfoForColumnGroups = this.GetTupleInfoForColumnGroups();
			OlapTupleProcessor olapTupleProcessor = new OlapTupleProcessor(tupleInfoForColumnGroups);
			this.columnGroupingInfo = olapTupleProcessor.Process();
		}

		// Token: 0x06007CDE RID: 31966 RVA: 0x001CA503 File Offset: 0x001C8703
		private void ProcessCells()
		{
			if (!this.ResponseHasAggregates)
			{
				return;
			}
			if (this.ResponseHasAggregatesOnly)
			{
				this.ProcessAggregateCellsForAggregatesOnly();
				return;
			}
			this.ProcessAggregateCellsForRowsAndColumns();
		}

		// Token: 0x06007CDF RID: 31967 RVA: 0x001CA524 File Offset: 0x001C8724
		private void ProcessAggregateCellsForAggregatesOnly()
		{
			for (int i = 0; i < this.ResponseData.ColumnAxisTuples.Count; i++)
			{
				Coordinate coordinate = new Coordinate(this.rowGroupingInfo.RootGroup, this.columnGroupingInfo.RootGroup);
				IOlapCell cellByOrdinal = this.ResponseData.Cells.GetCellByOrdinal(i);
				if (cellByOrdinal != null)
				{
					this.aggregates.AddAggregateValue(coordinate, i, cellByOrdinal);
				}
			}
		}

		// Token: 0x06007CE0 RID: 31968 RVA: 0x001CA58C File Offset: 0x001C878C
		private void ProcessAggregateCellsForRowsAndColumns()
		{
			if (this.ResponseHasRowDescriptionsAndColumnDescriptions)
			{
				this.GenerateAggregatesForRowsAndColumns();
				return;
			}
			if (this.ResponseHasColumnDescriptions)
			{
				this.GenerateAggregatesForColumnsOnly();
				return;
			}
			if (this.ResponseHasRowDescriptions)
			{
				this.GenerateAggregatesForRowsOnly();
			}
		}

		// Token: 0x06007CE1 RID: 31969 RVA: 0x001CA5BC File Offset: 0x001C87BC
		private void GenerateAggregatesForRowsAndColumns()
		{
			int count = this.ResponseData.ColumnAxisTuples.Count;
			for (int i = 0; i < this.rowGroupingInfo.ProcessedTuples.Count; i++)
			{
				ProcessedTuple processedTuple = this.rowGroupingInfo.ProcessedTuples[i];
				int previousCells = processedTuple.SourceTupleIndex * count;
				this.GenerateAggregateForColumnTuples(processedTuple.Group, previousCells);
			}
		}

		// Token: 0x06007CE2 RID: 31970 RVA: 0x001CA61D File Offset: 0x001C881D
		private void GenerateAggregatesForColumnsOnly()
		{
			this.GenerateAggregateForColumnTuples(this.rowGroupingInfo.RootGroup, 0);
		}

		// Token: 0x06007CE3 RID: 31971 RVA: 0x001CA634 File Offset: 0x001C8834
		private void GenerateAggregatesForRowsOnly()
		{
			for (int i = 0; i < this.rowGroupingInfo.ProcessedTuples.Count; i++)
			{
				ProcessedTuple processedTuple = this.rowGroupingInfo.ProcessedTuples[i];
				int cellOrdinal = processedTuple.SourceTupleIndex * this.ResponseData.Configuration.PivotAggregateDescriptions.Count;
				Coordinate aggregateCoordinate = new Coordinate(processedTuple.Group, this.columnGroupingInfo.RootGroup);
				this.CreateAggregateForCoordinate(aggregateCoordinate, cellOrdinal);
			}
		}

		// Token: 0x06007CE4 RID: 31972 RVA: 0x001CA6AC File Offset: 0x001C88AC
		private void GenerateAggregateForColumnTuples(Group rowGroup, int previousCells)
		{
			for (int i = 0; i < this.columnGroupingInfo.ProcessedTuples.Count; i++)
			{
				ProcessedTuple processedTuple = this.columnGroupingInfo.ProcessedTuples[i];
				int ordinal = previousCells + processedTuple.SourceTupleIndex;
				Coordinate coordinate = new Coordinate(rowGroup, processedTuple.Group);
				if (processedTuple.HasAggregate)
				{
					IOlapCell cellByOrdinal = this.ResponseData.Cells.GetCellByOrdinal(ordinal);
					if (cellByOrdinal != null)
					{
						this.aggregates.AddAggregateValue(coordinate, processedTuple.AggregateIndex, cellByOrdinal);
					}
				}
			}
		}

		// Token: 0x06007CE5 RID: 31973 RVA: 0x001CA730 File Offset: 0x001C8930
		private void CreateAggregateForCoordinate(Coordinate aggregateCoordinate, int cellOrdinal)
		{
			int count = this.ResponseData.Configuration.PivotAggregateDescriptions.Count;
			for (int i = 0; i < count; i++)
			{
				int aggregateCellOrdinal = cellOrdinal + i;
				this.AddAggregate(aggregateCoordinate, aggregateCellOrdinal, i);
			}
		}

		// Token: 0x06007CE6 RID: 31974 RVA: 0x001CA76C File Offset: 0x001C896C
		private void AddAggregate(Coordinate aggregateCoordinate, int aggregateCellOrdinal, int aggregateIndex)
		{
			IOlapCell cellByOrdinal = this.ResponseData.Cells.GetCellByOrdinal(aggregateCellOrdinal);
			if (cellByOrdinal != null)
			{
				this.aggregates.AddAggregateValue(aggregateCoordinate, aggregateIndex, cellByOrdinal);
			}
		}

		// Token: 0x06007CE7 RID: 31975 RVA: 0x001CA79C File Offset: 0x001C899C
		private OlapProcessedResponseInfo CreateProcessedResponse()
		{
			return new OlapProcessedResponseInfo
			{
				Aggregates = this.aggregates.GetInternalDictionary(),
				RootCoordinate = new Coordinate(this.rowGroupingInfo.RootGroup, this.columnGroupingInfo.RootGroup),
				PivotConfiguration = this.ResponseData.Configuration
			};
		}

		// Token: 0x04002231 RID: 8753
		private readonly AggregatesDictionary aggregates;

		// Token: 0x04002232 RID: 8754
		private OlapTupleProcessorOutput columnGroupingInfo;

		// Token: 0x04002233 RID: 8755
		private OlapTupleProcessorOutput rowGroupingInfo;
	}
}
