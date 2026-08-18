using System;
using System.Collections.Generic;
using System.Data.Objects.Internal;
using System.Linq;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003C9 RID: 969
	internal class Coordinator<T> : Coordinator
	{
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06003459 RID: 13401 RVA: 0x000CA3D2 File Offset: 0x000C85D2
		internal T Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x000CA3DC File Offset: 0x000C85DC
		internal Coordinator(CoordinatorFactory<T> coordinator, Coordinator parent, Coordinator next) : base(coordinator, parent, next)
		{
			this.TypedCoordinatorFactory = coordinator;
			Coordinator next2 = null;
			foreach (CoordinatorFactory coordinatorFactory in coordinator.NestedCoordinators.Reverse<CoordinatorFactory>())
			{
				base.Child = coordinatorFactory.CreateCoordinator(this, next2);
				next2 = base.Child;
			}
			this.IsUsingElementCollection = (!base.IsRoot && typeof(T) != typeof(RecordState));
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000CA478 File Offset: 0x000C8678
		internal override void ResetCollection(Shaper shaper)
		{
			if (this._handleClose != null)
			{
				this._handleClose(shaper, this._wrappedElements);
				this._handleClose = null;
			}
			base.IsEntered = false;
			if (this.IsUsingElementCollection)
			{
				this._elements = this.TypedCoordinatorFactory.InitializeCollection(shaper);
				this._wrappedElements = new List<IEntityWrapper>();
			}
			if (base.Child != null)
			{
				base.Child.ResetCollection(shaper);
			}
			if (this.Next != null)
			{
				this.Next.ResetCollection(shaper);
			}
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x000CA500 File Offset: 0x000C8700
		internal override void ReadNextElement(Shaper shaper)
		{
			IEntityWrapper entityWrapper = null;
			T t;
			try
			{
				if (this.TypedCoordinatorFactory.WrappedElement == null)
				{
					t = this.TypedCoordinatorFactory.Element(shaper);
				}
				else
				{
					entityWrapper = this.TypedCoordinatorFactory.WrappedElement(shaper);
					t = (T)((object)entityWrapper.Entity);
				}
			}
			catch (Exception e)
			{
				if (EntityUtil.IsCatchableExceptionType(e))
				{
					this.ResetCollection(shaper);
					t = this.TypedCoordinatorFactory.ElementWithErrorHandling(shaper);
				}
				throw;
			}
			if (this.IsUsingElementCollection)
			{
				this._elements.Add(t);
				if (entityWrapper != null)
				{
					this._wrappedElements.Add(entityWrapper);
					return;
				}
			}
			else
			{
				this._current = t;
			}
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x000CA5B0 File Offset: 0x000C87B0
		internal void RegisterCloseHandler(Action<Shaper, List<IEntityWrapper>> closeHandler)
		{
			this._handleClose = closeHandler;
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x000CA5B9 File Offset: 0x000C87B9
		internal void SetCurrentToDefault()
		{
			this._current = default(T);
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x000CA5C7 File Offset: 0x000C87C7
		private IEnumerable<T> GetElements()
		{
			return this._elements;
		}

		// Token: 0x040016D3 RID: 5843
		internal readonly CoordinatorFactory<T> TypedCoordinatorFactory;

		// Token: 0x040016D4 RID: 5844
		private T _current;

		// Token: 0x040016D5 RID: 5845
		private ICollection<T> _elements;

		// Token: 0x040016D6 RID: 5846
		private List<IEntityWrapper> _wrappedElements;

		// Token: 0x040016D7 RID: 5847
		private Action<Shaper, List<IEntityWrapper>> _handleClose;

		// Token: 0x040016D8 RID: 5848
		private readonly bool IsUsingElementCollection;
	}
}
