using System;
using System.Data.Entity;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002CB RID: 715
	internal class RecordConverter
	{
		// Token: 0x06002A1A RID: 10778 RVA: 0x000A47FE File Offset: 0x000A29FE
		internal RecordConverter(UpdateTranslator updateTranslator)
		{
			this.m_updateTranslator = updateTranslator;
		}

		// Token: 0x06002A1B RID: 10779 RVA: 0x000A480D File Offset: 0x000A2A0D
		internal PropagatorResult ConvertOriginalValuesToPropagatorResult(IEntityStateEntry stateEntry, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			return this.ConvertStateEntryToPropagatorResult(stateEntry, false, modifiedPropertiesBehavior);
		}

		// Token: 0x06002A1C RID: 10780 RVA: 0x000A4818 File Offset: 0x000A2A18
		internal PropagatorResult ConvertCurrentValuesToPropagatorResult(IEntityStateEntry stateEntry, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			return this.ConvertStateEntryToPropagatorResult(stateEntry, true, modifiedPropertiesBehavior);
		}

		// Token: 0x06002A1D RID: 10781 RVA: 0x000A4824 File Offset: 0x000A2A24
		private PropagatorResult ConvertStateEntryToPropagatorResult(IEntityStateEntry stateEntry, bool useCurrentValues, ModifiedPropertiesBehavior modifiedPropertiesBehavior)
		{
			PropagatorResult result;
			try
			{
				EntityUtil.CheckArgumentNull<IEntityStateEntry>(stateEntry, "stateEntry");
				IExtendedDataRecord record = useCurrentValues ? EntityUtil.CheckArgumentNull<IExtendedDataRecord>(stateEntry.CurrentValues, "stateEntry.CurrentValues") : EntityUtil.CheckArgumentNull<IExtendedDataRecord>(stateEntry.OriginalValues as IExtendedDataRecord, "stateEntry.OriginalValues");
				bool isModified = false;
				result = ExtractorMetadata.ExtractResultFromRecord(stateEntry, isModified, record, useCurrentValues, this.m_updateTranslator, modifiedPropertiesBehavior);
			}
			catch (Exception ex)
			{
				if (UpdateTranslator.RequiresContext(ex))
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

		// Token: 0x040012C2 RID: 4802
		private UpdateTranslator m_updateTranslator;
	}
}
