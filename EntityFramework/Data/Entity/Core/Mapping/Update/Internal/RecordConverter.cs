using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x0200040E RID: 1038
	internal class RecordConverter
	{
		// Token: 0x0600263D RID: 9789 RVA: 0x000B5B42 File Offset: 0x000B3D42
		internal RecordConverter(UpdateTranslator updateTranslator)
		{
			this.m_updateTranslator = updateTranslator;
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x000B5B51 File Offset: 0x000B3D51
		internal PropagatorResult ConvertOriginalValuesToPropagatorResult(IEntityStateEntry stateEntry, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			return this.ConvertStateEntryToPropagatorResult(stateEntry, false, modifiedPropertiesBehavior);
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x000B5B5C File Offset: 0x000B3D5C
		internal PropagatorResult ConvertCurrentValuesToPropagatorResult(IEntityStateEntry stateEntry, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			return this.ConvertStateEntryToPropagatorResult(stateEntry, true, modifiedPropertiesBehavior);
		}

		// Token: 0x06002640 RID: 9792 RVA: 0x000B5B68 File Offset: 0x000B3D68
		private PropagatorResult ConvertStateEntryToPropagatorResult(IEntityStateEntry stateEntry, bool useCurrentValues, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			PropagatorResult result;
			try
			{
				IExtendedDataRecord record = useCurrentValues ? stateEntry.CurrentValues : ((IExtendedDataRecord)stateEntry.OriginalValues);
				bool isModified = false;
				result = ExtractorMetadata.ExtractResultFromRecord(stateEntry, isModified, record, useCurrentValues, this.m_updateTranslator, modifiedPropertiesBehavior);
			}
			catch (Exception ex)
			{
				if (ex.RequiresContext())
				{
					throw EntityUtil.Update(Strings.Update_ErrorLoadingRecord, ex, new IEntityStateEntry[]
					{
						stateEntry
					});
				}
				throw;
			}
			return result;
		}

		// Token: 0x04000E4F RID: 3663
		private readonly UpdateTranslator m_updateTranslator;
	}
}
