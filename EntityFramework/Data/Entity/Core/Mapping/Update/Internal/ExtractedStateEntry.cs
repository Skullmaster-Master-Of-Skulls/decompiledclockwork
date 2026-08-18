using System;

namespace System.Data.Entity.Core.Mapping.Update.Internal
{
	// Token: 0x020003EF RID: 1007
	internal struct ExtractedStateEntry
	{
		// Token: 0x06002537 RID: 9527 RVA: 0x000B0289 File Offset: 0x000AE489
		internal ExtractedStateEntry(EntityState state, PropagatorResult original, PropagatorResult current, IEntityStateEntry source)
		{
			this.State = state;
			this.Original = original;
			this.Current = current;
			this.Source = source;
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x000B02A8 File Offset: 0x000AE4A8
		internal ExtractedStateEntry(UpdateTranslator translator, IEntityStateEntry stateEntry)
		{
			this.State = stateEntry.State;
			this.Source = stateEntry;
			EntityState state = stateEntry.State;
			switch (state)
			{
			case EntityState.Unchanged:
				this.Original = translator.RecordConverter.ConvertOriginalValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.NoneModified);
				this.Current = translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.NoneModified);
				return;
			case EntityState.Detached | EntityState.Unchanged:
				break;
			case EntityState.Added:
				this.Original = null;
				this.Current = translator.RecordConverter.ConvertCurrentValuesToPropagatorResult(stateEntry, ModifiedPropertiesBehavior.AllModified);
				return;
			default:
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
				break;
			}
			this.Original = null;
			this.Current = null;
		}

		// Token: 0x04000DCC RID: 3532
		internal readonly EntityState State;

		// Token: 0x04000DCD RID: 3533
		internal readonly PropagatorResult Original;

		// Token: 0x04000DCE RID: 3534
		internal readonly PropagatorResult Current;

		// Token: 0x04000DCF RID: 3535
		internal readonly IEntityStateEntry Source;
	}
}
