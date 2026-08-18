using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020001DD RID: 477
	internal class Coordinator<T> : Coordinator
	{
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x00048072 File Offset: 0x00046272
		internal virtual T Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x0004807C File Offset: 0x0004627C
		internal Coordinator(CoordinatorFactory<T> coordinatorFactory, Coordinator parent, Coordinator next) : base(coordinatorFactory, parent, next)
		{
			this.TypedCoordinatorFactory = coordinatorFactory;
			Coordinator next2 = null;
			foreach (CoordinatorFactory coordinatorFactory2 in coordinatorFactory.NestedCoordinators.Reverse<CoordinatorFactory>())
			{
				base.Child = coordinatorFactory2.CreateCoordinator(this, next2);
				next2 = base.Child;
			}
			this.IsUsingElementCollection = (!base.IsRoot && typeof(T) != typeof(RecordState));
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x00048118 File Offset: 0x00046318
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

		// Token: 0x060010DD RID: 4317 RVA: 0x000481A0 File Offset: 0x000463A0
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
				if (e.IsCatchableExceptionType() && !shaper.Reader.IsClosed)
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

		// Token: 0x060010DE RID: 4318 RVA: 0x0004825C File Offset: 0x0004645C
		internal void RegisterCloseHandler(Action<Shaper, List<IEntityWrapper>> closeHandler)
		{
			this._handleClose = closeHandler;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x00048265 File Offset: 0x00046465
		internal void SetCurrentToDefault()
		{
			this._current = default(T);
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x00048273 File Offset: 0x00046473
		private IEnumerable<T> GetElements()
		{
			return this._elements;
		}

		// Token: 0x0400050B RID: 1291
		internal readonly CoordinatorFactory<T> TypedCoordinatorFactory;

		// Token: 0x0400050C RID: 1292
		private T _current;

		// Token: 0x0400050D RID: 1293
		private ICollection<T> _elements;

		// Token: 0x0400050E RID: 1294
		private List<IEntityWrapper> _wrappedElements;

		// Token: 0x0400050F RID: 1295
		private Action<Shaper, List<IEntityWrapper>> _handleClose;

		// Token: 0x04000510 RID: 1296
		private readonly bool IsUsingElementCollection;
	}
}
