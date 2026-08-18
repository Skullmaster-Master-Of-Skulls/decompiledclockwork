using System;

namespace System.Data.Mapping.Update.Internal
{
	// Token: 0x020002BE RID: 702
	internal struct ExtractedStateEntry
	{
		// Token: 0x060029B4 RID: 10676 RVA: 0x000A2248 File Offset: 0x000A0448
		internal ExtractedStateEntry(UpdateTranslator translator, IEntityStateEntry stateEntry)
		{
			this.State = stateEntry.State;
			this.Source = stateEntry;
			EntityState state = stateEntry.State;
			if (state <= EntityState.Added)
			{
				if (state == EntityState.Unchanged)
				{
					this.Original = translator.RecordConverter.ConvertOriginalValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.NoneModified);
					this.Current = translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.NoneModified);
					return;
				}
				if (state == EntityState.Added)
				{
					this.Original = null;
					this.Current = translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.AllModified);
					return;
				}
			}
			else
			{
				if (state == EntityState.Deleted)
				{
					this.Original = translator.RecordConverter.ConvertOriginalValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.AllModified);
					this.Current = null;
					return;
				}
				if (state == EntityState.Modified)
				{
					this.Original = translator.RecordConverter.ConvertOriginalValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.SomeModified);
					this.Current = translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.SomeModified);
					return;
				}
			}
			this.Original = null;
			this.Current = null;
		}

		// Token: 0x0400128F RID: 4751
		internal readonly EntityState State;

		// Token: 0x04001290 RID: 4752
		internal readonly PropagatorResult Original;

		// Token: 0x04001291 RID: 4753
		internal readonly PropagatorResult Current;

		// Token: 0x04001292 RID: 4754
		internal readonly IEntityStateEntry Source;
	}
}
