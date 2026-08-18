using System;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020001DC RID: 476
	internal abstract class Coordinator
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00047F5E File Offset: 0x0004615E
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x00047F66 File Offset: 0x00046166
		public Coordinator Child { get; protected set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00047F6F File Offset: 0x0004616F
		// (set) Token: 0x060010D2 RID: 4306 RVA: 0x00047F77 File Offset: 0x00046177
		public bool IsEntered { get; protected set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00047F80 File Offset: 0x00046180
		internal bool IsRoot
		{
			get
			{
				return null == this.Parent;
			}
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00047F8B File Offset: 0x0004618B
		protected Coordinator(CoordinatorFactory coordinatorFactory, Coordinator parent, Coordinator next)
		{
			this.CoordinatorFactory = coordinatorFactory;
			this.Parent = parent;
			this.Next = next;
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00047FA8 File Offset: 0x000461A8
		internal void Initialize(Shaper shaper)
		{
			this.ResetCollection(shaper);
			shaper.State[this.CoordinatorFactory.StateSlot] = this;
			if (this.Child != null)
			{
				this.Child.Initialize(shaper);
			}
			if (this.Next != null)
			{
				this.Next.Initialize(shaper);
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00047FF8 File Offset: 0x000461F8
		internal int MaxDistanceToLeaf()
		{
			int num = 0;
			for (Coordinator coordinator = this.Child; coordinator != null; coordinator = coordinator.Next)
			{
				num = Math.Max(num, coordinator.MaxDistanceToLeaf() + 1);
			}
			return num;
		}

		// Token: 0x060010D7 RID: 4311
		internal abstract void ResetCollection(Shaper shaper);

		// Token: 0x060010D8 RID: 4312 RVA: 0x0004802C File Offset: 0x0004622C
		internal bool HasNextElement(Shaper shaper)
		{
			bool result = false;
			if (!this.IsEntered || !this.CoordinatorFactory.CheckKeys(shaper))
			{
				this.CoordinatorFactory.SetKeys(shaper);
				this.IsEntered = true;
				result = true;
			}
			return result;
		}

		// Token: 0x060010D9 RID: 4313
		internal abstract void ReadNextElement(Shaper shaper);

		// Token: 0x04000506 RID: 1286
		internal readonly CoordinatorFactory CoordinatorFactory;

		// Token: 0x04000507 RID: 1287
		internal readonly Coordinator Parent;

		// Token: 0x04000508 RID: 1288
		internal readonly Coordinator Next;
	}
}
