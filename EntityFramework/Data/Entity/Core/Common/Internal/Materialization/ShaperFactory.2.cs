using System;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002E7 RID: 743
	internal class ShaperFactory<T> : ShaperFactory
	{
		// Token: 0x06001A35 RID: 6709 RVA: 0x00081F01 File Offset: 0x00080101
		internal ShaperFactory(int stateCount, CoordinatorFactory<T> rootCoordinatorFactory, Type[] columnTypes, bool[] nullableColumns, MergeOption mergeOption)
		{
			this._stateCount = stateCount;
			this._rootCoordinatorFactory = rootCoordinatorFactory;
			this.ColumnTypes = columnTypes;
			this.NullableColumns = nullableColumns;
			this._mergeOption = mergeOption;
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x00081F2E File Offset: 0x0008012E
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x00081F36 File Offset: 0x00080136
		public Type[] ColumnTypes { get; private set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00081F3F File Offset: 0x0008013F
		// (set) Token: 0x06001A39 RID: 6713 RVA: 0x00081F47 File Offset: 0x00080147
		public bool[] NullableColumns { get; private set; }

		// Token: 0x06001A3A RID: 6714 RVA: 0x00081F50 File Offset: 0x00080150
		internal Shaper<T> Create(DbDataReader reader, ObjectContext context, MetadataWorkspace workspace, MergeOption mergeOption, bool readerOwned, bool streaming)
		{
			return new Shaper<T>(reader, context, workspace, mergeOption, this._stateCount, this._rootCoordinatorFactory, readerOwned, streaming);
		}

		// Token: 0x04000909 RID: 2313
		private readonly int _stateCount;

		// Token: 0x0400090A RID: 2314
		private readonly CoordinatorFactory<T> _rootCoordinatorFactory;

		// Token: 0x0400090B RID: 2315
		[SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Used in the debug build")]
		private readonly MergeOption _mergeOption;
	}
}
