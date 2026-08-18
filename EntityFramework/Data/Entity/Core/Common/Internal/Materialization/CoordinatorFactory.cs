using System;
using System.Collections.ObjectModel;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020001DA RID: 474
	internal abstract class CoordinatorFactory
	{
		// Token: 0x060010C2 RID: 4290 RVA: 0x00047BCC File Offset: 0x00045DCC
		protected CoordinatorFactory(int depth, int stateSlot, Func<Shaper, bool> hasData, Func<Shaper, bool> setKeys, Func<Shaper, bool> checkKeys, CoordinatorFactory[] nestedCoordinators, RecordStateFactory[] recordStateFactories)
		{
			this.Depth = depth;
			this.StateSlot = stateSlot;
			this.IsLeafResult = (0 == nestedCoordinators.Length);
			if (hasData == null)
			{
				this.HasData = CoordinatorFactory._alwaysTrue;
			}
			else
			{
				this.HasData = hasData;
			}
			if (setKeys == null)
			{
				this.SetKeys = CoordinatorFactory._alwaysTrue;
			}
			else
			{
				this.SetKeys = setKeys;
			}
			if (checkKeys == null)
			{
				if (this.IsLeafResult)
				{
					this.CheckKeys = CoordinatorFactory._alwaysFalse;
				}
				else
				{
					this.CheckKeys = CoordinatorFactory._alwaysTrue;
				}
			}
			else
			{
				this.CheckKeys = checkKeys;
			}
			this.NestedCoordinators = new ReadOnlyCollection<CoordinatorFactory>(nestedCoordinators);
			this.RecordStateFactories = new ReadOnlyCollection<RecordStateFactory>(recordStateFactories);
			this.IsSimple = (this.IsLeafResult && checkKeys == null && null == hasData);
		}

		// Token: 0x060010C3 RID: 4291
		internal abstract Coordinator CreateCoordinator(Coordinator parent, Coordinator next);

		// Token: 0x040004F3 RID: 1267
		private static readonly Func<Shaper, bool> _alwaysTrue = (Shaper s) => true;

		// Token: 0x040004F4 RID: 1268
		private static readonly Func<Shaper, bool> _alwaysFalse = (Shaper s) => false;

		// Token: 0x040004F5 RID: 1269
		internal readonly int Depth;

		// Token: 0x040004F6 RID: 1270
		internal readonly int StateSlot;

		// Token: 0x040004F7 RID: 1271
		internal readonly Func<Shaper, bool> HasData;

		// Token: 0x040004F8 RID: 1272
		internal readonly Func<Shaper, bool> SetKeys;

		// Token: 0x040004F9 RID: 1273
		internal readonly Func<Shaper, bool> CheckKeys;

		// Token: 0x040004FA RID: 1274
		internal readonly ReadOnlyCollection<CoordinatorFactory> NestedCoordinators;

		// Token: 0x040004FB RID: 1275
		internal readonly bool IsLeafResult;

		// Token: 0x040004FC RID: 1276
		internal readonly bool IsSimple;

		// Token: 0x040004FD RID: 1277
		internal readonly ReadOnlyCollection<RecordStateFactory> RecordStateFactories;
	}
}
