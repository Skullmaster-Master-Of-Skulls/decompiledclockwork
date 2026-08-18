using System;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200059E RID: 1438
	internal class NextResultGenerator
	{
		// Token: 0x0600385D RID: 14429 RVA: 0x0010A378 File Offset: 0x00108578
		internal NextResultGenerator(ObjectContext context, EntityCommand entityCommand, EdmType[] edmTypes, ReadOnlyCollection<EntitySet> entitySets, MergeOption mergeOption, bool streaming, int resultSetIndex)
		{
			this._context = context;
			this._entityCommand = entityCommand;
			this._entitySets = entitySets;
			this._edmTypes = edmTypes;
			this._resultSetIndex = resultSetIndex;
			this._streaming = streaming;
			this._mergeOption = mergeOption;
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x0010A3B8 File Offset: 0x001085B8
		internal ObjectResult<TElement> GetNextResult<TElement>(DbDataReader storeReader)
		{
			bool flag = false;
			try
			{
				flag = storeReader.NextResult();
			}
			catch (Exception ex)
			{
				if (ex.IsCatchableExceptionType())
				{
					throw new EntityCommandExecutionException(Strings.EntityClient_StoreReaderFailed, ex);
				}
				throw;
			}
			if (flag)
			{
				EdmType expectedEdmType = this._edmTypes[this._resultSetIndex];
				MetadataHelper.CheckFunctionImportReturnType<TElement>(expectedEdmType, this._context.MetadataWorkspace);
				return this._context.MaterializedDataRecord<TElement>(this._entityCommand, storeReader, this._resultSetIndex, this._entitySets, this._edmTypes, null, this._mergeOption, this._streaming);
			}
			return null;
		}

		// Token: 0x040015A9 RID: 5545
		private readonly EntityCommand _entityCommand;

		// Token: 0x040015AA RID: 5546
		private readonly ReadOnlyCollection<EntitySet> _entitySets;

		// Token: 0x040015AB RID: 5547
		private readonly ObjectContext _context;

		// Token: 0x040015AC RID: 5548
		private readonly EdmType[] _edmTypes;

		// Token: 0x040015AD RID: 5549
		private readonly int _resultSetIndex;

		// Token: 0x040015AE RID: 5550
		private readonly bool _streaming;

		// Token: 0x040015AF RID: 5551
		private readonly MergeOption _mergeOption;
	}
}
