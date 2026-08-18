using System;
using System.Data.Metadata.Edm;
using System.Data.Objects;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003D3 RID: 979
	internal class ShaperFactory<T> : ShaperFactory
	{
		// Token: 0x060034D1 RID: 13521 RVA: 0x000CBF28 File Offset: 0x000CA128
		internal ShaperFactory(int stateCount, CoordinatorFactory<T> rootCoordinatorFactory, Action checkPermissions, MergeOption mergeOption)
		{
			this._stateCount = stateCount;
			this._rootCoordinatorFactory = rootCoordinatorFactory;
			this._checkPermissions = checkPermissions;
			this._mergeOption = mergeOption;
		}

		// Token: 0x060034D2 RID: 13522 RVA: 0x000CBF4D File Offset: 0x000CA14D
		internal Shaper<T> Create(DbDataReader reader, ObjectContext context, MetadataWorkspace workspace, MergeOption mergeOption, bool readerOwned)
		{
			return new Shaper<T>(reader, context, workspace, mergeOption, this._stateCount, this._rootCoordinatorFactory, this._checkPermissions, readerOwned);
		}

		// Token: 0x04001720 RID: 5920
		private readonly int _stateCount;

		// Token: 0x04001721 RID: 5921
		private readonly CoordinatorFactory<T> _rootCoordinatorFactory;

		// Token: 0x04001722 RID: 5922
		private readonly Action _checkPermissions;

		// Token: 0x04001723 RID: 5923
		private readonly MergeOption _mergeOption;
	}
}
