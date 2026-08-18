using System;
using System.Data.Common;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.EntityClient;
using System.Data.Metadata.Edm;

namespace System.Data.Objects
{
	// Token: 0x02000129 RID: 297
	internal class NextResultGenerator
	{
		// Token: 0x0600156B RID: 5483 RVA: 0x000489F8 File Offset: 0x00046BF8
		internal NextResultGenerator(ObjectContext context, EntityCommand entityCommand, EdmType[] edmTypes, ReadOnlyMetadataCollection<EntitySet> entitySets, MergeOption mergeOption, int resultSetIndex)
		{
			this._context = context;
			this._entityCommand = entityCommand;
			this._entitySets = entitySets;
			this._edmTypes = edmTypes;
			this._resultSetIndex = resultSetIndex;
			this._mergeOption = mergeOption;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00048A30 File Offset: 0x00046C30
		internal ObjectResult<TElement> GetNextResult<TElement>(DbDataReader storeReader)
		{
			bool flag = false;
			try
			{
				flag = storeReader.NextResult();
			}
			catch (Exception ex)
			{
				if (EntityUtil.IsCatchableExceptionType(ex))
				{
					throw EntityUtil.CommandExecution(Strings.EntityClient_StoreReaderFailed, ex);
				}
				throw;
			}
			if (flag)
			{
				EdmType expectedEdmType = this._edmTypes[this._resultSetIndex];
				MetadataHelper.CheckFunctionImportReturnType<TElement>(expectedEdmType, this._context.MetadataWorkspace);
				return this._context.MaterializedDataRecord<TElement>(this._entityCommand, storeReader, this._resultSetIndex, this._entitySets, this._edmTypes, this._mergeOption);
			}
			return null;
		}

		// Token: 0x04000A39 RID: 2617
		private EntityCommand _entityCommand;

		// Token: 0x04000A3A RID: 2618
		private ReadOnlyMetadataCollection<EntitySet> _entitySets;

		// Token: 0x04000A3B RID: 2619
		private ObjectContext _context;

		// Token: 0x04000A3C RID: 2620
		private EdmType[] _edmTypes;

		// Token: 0x04000A3D RID: 2621
		private int _resultSetIndex;

		// Token: 0x04000A3E RID: 2622
		private MergeOption _mergeOption;
	}
}
