using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x02000559 RID: 1369
	public sealed class DesignSurfaceCollection : ICollection, IEnumerable
	{
		// Token: 0x0600306B RID: 12395 RVA: 0x00112E6E File Offset: 0x00111E6E
		internal DesignSurfaceCollection(DesignerCollection designers)
		{
			this._designers = designers;
			if (this._designers == null)
			{
				this._designers = new DesignerCollection(null);
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x00112E91 File Offset: 0x00111E91
		public int Count
		{
			get
			{
				return this._designers.Count;
			}
		}

		// Token: 0x17000915 RID: 2325
		public DesignSurface this[int index]
		{
			get
			{
				IDesignerHost designerHost = this._designers[index];
				DesignSurface designSurface = designerHost.GetService(typeof(DesignSurface)) as DesignSurface;
				if (designSurface == null)
				{
					throw new NotSupportedException();
				}
				return designSurface;
			}
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x00112EDA File Offset: 0x00111EDA
		public IEnumerator GetEnumerator()
		{
			return new DesignSurfaceCollection.DesignSurfaceEnumerator(this._designers.GetEnumerator());
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x00112EEC File Offset: 0x00111EEC
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x00112EF4 File Offset: 0x00111EF4
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x00112EF7 File Offset: 0x00111EF7
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x00112EFC File Offset: 0x00111EFC
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object obj in this)
			{
				DesignSurface value = (DesignSurface)obj;
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x00112F58 File Offset: 0x00111F58
		public void CopyTo(DesignSurface[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x00112F62 File Offset: 0x00111F62
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040020A0 RID: 8352
		private DesignerCollection _designers;

		// Token: 0x0200055A RID: 1370
		private class DesignSurfaceEnumerator : IEnumerator
		{
			// Token: 0x06003075 RID: 12405 RVA: 0x00112F6A File Offset: 0x00111F6A
			internal DesignSurfaceEnumerator(IEnumerator designerEnumerator)
			{
				this._designerEnumerator = designerEnumerator;
			}

			// Token: 0x17000919 RID: 2329
			// (get) Token: 0x06003076 RID: 12406 RVA: 0x00112F7C File Offset: 0x00111F7C
			public object Current
			{
				get
				{
					IDesignerHost designerHost = (IDesignerHost)this._designerEnumerator.Current;
					DesignSurface designSurface = designerHost.GetService(typeof(DesignSurface)) as DesignSurface;
					if (designSurface == null)
					{
						throw new NotSupportedException();
					}
					return designSurface;
				}
			}

			// Token: 0x06003077 RID: 12407 RVA: 0x00112FBA File Offset: 0x00111FBA
			public bool MoveNext()
			{
				return this._designerEnumerator.MoveNext();
			}

			// Token: 0x06003078 RID: 12408 RVA: 0x00112FC7 File Offset: 0x00111FC7
			public void Reset()
			{
				this._designerEnumerator.Reset();
			}

			// Token: 0x040020A1 RID: 8353
			private IEnumerator _designerEnumerator;
		}
	}
}
