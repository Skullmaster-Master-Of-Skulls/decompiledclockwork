using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000051 RID: 81
	public struct ManifestResourceHandleCollection : IReadOnlyCollection<ManifestResourceHandle>, IEnumerable<ManifestResourceHandle>, IEnumerable
	{
		// Token: 0x06000360 RID: 864 RVA: 0x00008D13 File Offset: 0x00006F13
		internal ManifestResourceHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00008D1C File Offset: 0x00006F1C
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00008D24 File Offset: 0x00006F24
		public ManifestResourceHandleCollection.Enumerator GetEnumerator()
		{
			return new ManifestResourceHandleCollection.Enumerator(this._lastRowId);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00008D31 File Offset: 0x00006F31
		IEnumerator<ManifestResourceHandle> IEnumerable<ManifestResourceHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00008D31 File Offset: 0x00006F31
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002C5 RID: 709
		private readonly int _lastRowId;

		// Token: 0x02000182 RID: 386
		public struct Enumerator : IEnumerator<ManifestResourceHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BD4 RID: 3028 RVA: 0x00021597 File Offset: 0x0001F797
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x000215A7 File Offset: 0x0001F7A7
			public ManifestResourceHandle Current
			{
				get
				{
					return ManifestResourceHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BD6 RID: 3030 RVA: 0x000215BD File Offset: 0x0001F7BD
			public bool MoveNext()
			{
				if (this._currentRowId >= this._lastRowId)
				{
					this._currentRowId = 16777216;
					return false;
				}
				this._currentRowId++;
				return true;
			}

			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x000215E9 File Offset: 0x0001F7E9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BD8 RID: 3032 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BD9 RID: 3033 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000999 RID: 2457
			private readonly int _lastRowId;

			// Token: 0x0400099A RID: 2458
			private int _currentRowId;

			// Token: 0x0400099B RID: 2459
			private const int EnumEnded = 16777216;
		}
	}
}
