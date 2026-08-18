using System;
using System.Collections.ObjectModel;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003CB RID: 971
	internal abstract class CoordinatorFactory
	{
		// Token: 0x06003475 RID: 13429 RVA: 0x000CA84C File Offset: 0x000C8A4C
		protected CoordinatorFactory(int depth, int stateSlot, Func<Shaper, bool> hasData, Func<Shaper, bool> setKeys, Func<Shaper, bool> checkKeys, CoordinatorFactory[] nestedCoordinators, RecordStateFactory[] recordStateFactories)
		{
			this.Depth = depth;
			this.StateSlot = stateSlot;
			this.IsLeafResult = (nestedCoordinators.Length == 0);
			if (hasData == null)
			{
				this.HasData = CoordinatorFactory.AlwaysTrue;
			}
			else
			{
				this.HasData = hasData;
			}
			if (setKeys == null)
			{
				this.SetKeys = CoordinatorFactory.AlwaysTrue;
			}
			else
			{
				this.SetKeys = setKeys;
			}
			if (checkKeys == null)
			{
				if (this.IsLeafResult)
				{
					this.CheckKeys = CoordinatorFactory.AlwaysFalse;
				}
				else
				{
					this.CheckKeys = CoordinatorFactory.AlwaysTrue;
				}
			}
			else
			{
				this.CheckKeys = checkKeys;
			}
			this.NestedCoordinators = new ReadOnlyCollection<CoordinatorFactory>(nestedCoordinators);
			this.RecordStateFactories = new ReadOnlyCollection<RecordStateFactory>(recordStateFactories);
			this.IsSimple = (this.IsLeafResult && checkKeys == null && hasData == null);
		}

		// Token: 0x06003476 RID: 13430
		internal abstract Coordinator CreateCoordinator(Coordinator parent, Coordinator next);

		// Token: 0x040016E6 RID: 5862
		private static readonly Func<Shaper, bool> AlwaysTrue = (Shaper s) => true;

		// Token: 0x040016E7 RID: 5863
		private static readonly Func<Shaper, bool> AlwaysFalse = (Shaper s) => false;

		// Token: 0x040016E8 RID: 5864
		internal readonly int Depth;

		// Token: 0x040016E9 RID: 5865
		internal readonly int StateSlot;

		// Token: 0x040016EA RID: 5866
		internal readonly Func<Shaper, bool> HasData;

		// Token: 0x040016EB RID: 5867
		internal readonly Func<Shaper, bool> SetKeys;

		// Token: 0x040016EC RID: 5868
		internal readonly Func<Shaper, bool> CheckKeys;

		// Token: 0x040016ED RID: 5869
		internal readonly ReadOnlyCollection<CoordinatorFactory> NestedCoordinators;

		// Token: 0x040016EE RID: 5870
		internal readonly bool IsLeafResult;

		// Token: 0x040016EF RID: 5871
		internal readonly bool IsSimple;

		// Token: 0x040016F0 RID: 5872
		internal readonly ReadOnlyCollection<RecordStateFactory> RecordStateFactories;
	}
}
