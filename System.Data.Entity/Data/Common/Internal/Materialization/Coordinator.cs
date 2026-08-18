using System;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003C8 RID: 968
	internal abstract class Coordinator
	{
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x0600344E RID: 13390 RVA: 0x000CA2BD File Offset: 0x000C84BD
		// (set) Token: 0x0600344F RID: 13391 RVA: 0x000CA2C5 File Offset: 0x000C84C5
		public Coordinator Child
		{
			get
			{
				return this._child;
			}
			protected set
			{
				this._child = value;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06003450 RID: 13392 RVA: 0x000CA2CE File Offset: 0x000C84CE
		// (set) Token: 0x06003451 RID: 13393 RVA: 0x000CA2D6 File Offset: 0x000C84D6
		public bool IsEntered
		{
			get
			{
				return this._isEntered;
			}
			protected set
			{
				this._isEntered = value;
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003452 RID: 13394 RVA: 0x000CA2DF File Offset: 0x000C84DF
		internal bool IsRoot
		{
			get
			{
				return this.Parent == null;
			}
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000CA2EA File Offset: 0x000C84EA
		protected Coordinator(CoordinatorFactory coordinatorFactory, Coordinator parent, Coordinator next)
		{
			this.CoordinatorFactory = coordinatorFactory;
			this.Parent = parent;
			this.Next = next;
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000CA308 File Offset: 0x000C8508
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

		// Token: 0x06003455 RID: 13397 RVA: 0x000CA358 File Offset: 0x000C8558
		internal int MaxDistanceToLeaf()
		{
			int num = 0;
			for (Coordinator coordinator = this.Child; coordinator != null; coordinator = coordinator.Next)
			{
				num = Math.Max(num, coordinator.MaxDistanceToLeaf() + 1);
			}
			return num;
		}

		// Token: 0x06003456 RID: 13398
		internal abstract void ResetCollection(Shaper shaper);

		// Token: 0x06003457 RID: 13399 RVA: 0x000CA38C File Offset: 0x000C858C
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

		// Token: 0x06003458 RID: 13400
		internal abstract void ReadNextElement(Shaper shaper);

		// Token: 0x040016CE RID: 5838
		internal readonly CoordinatorFactory CoordinatorFactory;

		// Token: 0x040016CF RID: 5839
		internal readonly Coordinator Parent;

		// Token: 0x040016D0 RID: 5840
		private Coordinator _child;

		// Token: 0x040016D1 RID: 5841
		internal readonly Coordinator Next;

		// Token: 0x040016D2 RID: 5842
		private bool _isEntered;
	}
}
