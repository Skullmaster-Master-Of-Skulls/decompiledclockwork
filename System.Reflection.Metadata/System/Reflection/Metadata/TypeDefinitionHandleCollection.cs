using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x0200004A RID: 74
	public struct TypeDefinitionHandleCollection : IReadOnlyCollection<TypeDefinitionHandle>, IEnumerable<TypeDefinitionHandle>, IEnumerable
	{
		// Token: 0x06000340 RID: 832 RVA: 0x00008BB9 File Offset: 0x00006DB9
		internal TypeDefinitionHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00008BC2 File Offset: 0x00006DC2
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00008BCA File Offset: 0x00006DCA
		public TypeDefinitionHandleCollection.Enumerator GetEnumerator()
		{
			return new TypeDefinitionHandleCollection.Enumerator(this._lastRowId);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00008BD7 File Offset: 0x00006DD7
		IEnumerator<TypeDefinitionHandle> IEnumerable<TypeDefinitionHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00008BD7 File Offset: 0x00006DD7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002BB RID: 699
		private readonly int _lastRowId;

		// Token: 0x0200017D RID: 381
		public struct Enumerator : IEnumerator<TypeDefinitionHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BB6 RID: 2998 RVA: 0x0002132E File Offset: 0x0001F52E
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			// Token: 0x170002DB RID: 731
			// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0002133E File Offset: 0x0001F53E
			public TypeDefinitionHandle Current
			{
				get
				{
					return TypeDefinitionHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BB8 RID: 3000 RVA: 0x00021354 File Offset: 0x0001F554
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

			// Token: 0x170002DC RID: 732
			// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x00021380 File Offset: 0x0001F580
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BBA RID: 3002 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BBB RID: 3003 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000989 RID: 2441
			private readonly int _lastRowId;

			// Token: 0x0400098A RID: 2442
			private int _currentRowId;

			// Token: 0x0400098B RID: 2443
			private const int EnumEnded = 16777216;
		}
	}
}
