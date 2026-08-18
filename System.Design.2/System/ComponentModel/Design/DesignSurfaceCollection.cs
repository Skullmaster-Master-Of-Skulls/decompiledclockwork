using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020001C6 RID: 454
	public sealed class DesignSurfaceCollection : ICollection, IEnumerable
	{
		// Token: 0x060010E8 RID: 4328 RVA: 0x0005E610 File Offset: 0x0005C810
		internal DesignSurfaceCollection(DesignerCollection designers)
		{
			this._designers = designers;
			if (this._designers == null)
			{
				this._designers = new DesignerCollection(null);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0005E633 File Offset: 0x0005C833
		public int Count
		{
			get
			{
				return this._designers.Count;
			}
		}

		// Token: 0x170003E2 RID: 994
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

		// Token: 0x060010EB RID: 4331 RVA: 0x0005E67A File Offset: 0x0005C87A
		public IEnumerator GetEnumerator()
		{
			return new DesignSurfaceCollection.DesignSurfaceEnumerator(this._designers.GetEnumerator());
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x0005E68C File Offset: 0x0005C88C
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0000445B File Offset: 0x0000265B
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00003598 File Offset: 0x00001798
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0005E694 File Offset: 0x0005C894
		void ICollection.CopyTo(Array array, int index)
		{
			foreach (object obj in this)
			{
				DesignSurface value = (DesignSurface)obj;
				array.SetValue(value, index++);
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0005E6F0 File Offset: 0x0005C8F0
		public void CopyTo(DesignSurface[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0005E6FA File Offset: 0x0005C8FA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040009A2 RID: 2466
		private DesignerCollection _designers;

		// Token: 0x0200049B RID: 1179
		private class DesignSurfaceEnumerator : IEnumerator
		{
			// Token: 0x06002B72 RID: 11122 RVA: 0x00103A74 File Offset: 0x00101C74
			internal DesignSurfaceEnumerator(IEnumerator designerEnumerator)
			{
				this._designerEnumerator = designerEnumerator;
			}

			// Token: 0x1700092A RID: 2346
			// (get) Token: 0x06002B73 RID: 11123 RVA: 0x00103A84 File Offset: 0x00101C84
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

			// Token: 0x06002B74 RID: 11124 RVA: 0x00103AC2 File Offset: 0x00101CC2
			public bool MoveNext()
			{
				return this._designerEnumerator.MoveNext();
			}

			// Token: 0x06002B75 RID: 11125 RVA: 0x00103ACF File Offset: 0x00101CCF
			public void Reset()
			{
				this._designerEnumerator.Reset();
			}

			// Token: 0x04001E28 RID: 7720
			private IEnumerator _designerEnumerator;
		}
	}
}
