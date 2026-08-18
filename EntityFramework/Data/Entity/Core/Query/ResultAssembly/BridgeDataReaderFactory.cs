using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Linq;

namespace System.Data.Entity.Core.Query.ResultAssembly
{
	// Token: 0x0200027B RID: 635
	internal class BridgeDataReaderFactory
	{
		// Token: 0x06001654 RID: 5716 RVA: 0x0006C1CD File Offset: 0x0006A3CD
		public BridgeDataReaderFactory(Translator translator = null)
		{
			this._translator = (translator ?? new Translator());
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0006C1E8 File Offset: 0x0006A3E8
		public virtual DbDataReader Create(DbDataReader storeDataReader, ColumnMap columnMap, MetadataWorkspace workspace, IEnumerable<ColumnMap> nextResultColumnMaps)
		{
			KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> keyValuePair = this.CreateShaperInfo(storeDataReader, columnMap, workspace);
			return new BridgeDataReader(keyValuePair.Key, keyValuePair.Value, 0, this.GetNextResultShaperInfo(storeDataReader, workspace, nextResultColumnMaps).GetEnumerator());
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0006C224 File Offset: 0x0006A424
		private KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> CreateShaperInfo(DbDataReader storeDataReader, ColumnMap columnMap, MetadataWorkspace workspace)
		{
			ShaperFactory<RecordState> shaperFactory = this._translator.TranslateColumnMap<RecordState>(columnMap, workspace, null, MergeOption.NoTracking, true, true);
			Shaper<RecordState> shaper = shaperFactory.Create(storeDataReader, null, workspace, MergeOption.NoTracking, true, true);
			return new KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>(shaper, shaper.RootCoordinator.TypedCoordinatorFactory);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0006C284 File Offset: 0x0006A484
		private IEnumerable<KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>> GetNextResultShaperInfo(DbDataReader storeDataReader, MetadataWorkspace workspace, IEnumerable<ColumnMap> nextResultColumnMaps)
		{
			return from nextResultColumnMap in nextResultColumnMaps
			select this.CreateShaperInfo(storeDataReader, nextResultColumnMap, workspace);
		}

		// Token: 0x040007E9 RID: 2025
		private readonly Translator _translator;
	}
}
