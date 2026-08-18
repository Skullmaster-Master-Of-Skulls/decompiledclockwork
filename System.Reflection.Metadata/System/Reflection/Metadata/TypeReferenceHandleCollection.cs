using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x0200004B RID: 75
	public struct TypeReferenceHandleCollection : IReadOnlyCollection<TypeReferenceHandle>, IEnumerable<TypeReferenceHandle>, IEnumerable
	{
		// Token: 0x06000345 RID: 837 RVA: 0x00008BE4 File Offset: 0x00006DE4
		internal TypeReferenceHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00008BED File Offset: 0x00006DED
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00008BF5 File Offset: 0x00006DF5
		public TypeReferenceHandleCollection.Enumerator GetEnumerator()
		{
			return new TypeReferenceHandleCollection.Enumerator(this._lastRowId);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00008C02 File Offset: 0x00006E02
		IEnumerator<TypeReferenceHandle> IEnumerable<TypeReferenceHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00008C02 File Offset: 0x00006E02
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002BC RID: 700
		private readonly int _lastRowId;

		// Token: 0x0200017E RID: 382
		public struct Enumerator : IEnumerator<TypeReferenceHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BBC RID: 3004 RVA: 0x0002138D File Offset: 0x0001F58D
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			// Token: 0x170002DD RID: 733
			// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0002139D File Offset: 0x0001F59D
			public TypeReferenceHandle Current
			{
				get
				{
					return TypeReferenceHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BBE RID: 3006 RVA: 0x000213B3 File Offset: 0x0001F5B3
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

			// Token: 0x170002DE RID: 734
			// (get) Token: 0x06000BBF RID: 3007 RVA: 0x000213DF File Offset: 0x0001F5DF
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BC0 RID: 3008 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BC1 RID: 3009 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400098C RID: 2444
			private readonly int _lastRowId;

			// Token: 0x0400098D RID: 2445
			private int _currentRowId;

			// Token: 0x0400098E RID: 2446
			private const int EnumEnded = 16777216;
		}
	}
}
