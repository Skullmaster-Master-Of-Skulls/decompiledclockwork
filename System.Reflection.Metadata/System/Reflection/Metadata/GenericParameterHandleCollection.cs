using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x0200003F RID: 63
	public struct GenericParameterHandleCollection : IReadOnlyList<GenericParameterHandle>, IReadOnlyCollection<GenericParameterHandle>, IEnumerable<GenericParameterHandle>, IEnumerable
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000871B File Offset: 0x0000691B
		internal GenericParameterHandleCollection(int firstRowId, ushort count)
		{
			this._firstRowId = firstRowId;
			this._count = count;
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000872B File Offset: 0x0000692B
		public int Count
		{
			get
			{
				return (int)this._count;
			}
		}

		// Token: 0x17000148 RID: 328
		public GenericParameterHandle this[int index]
		{
			get
			{
				if (index < 0 || index >= (int)this._count)
				{
					Throw.IndexOutOfRange();
				}
				return GenericParameterHandle.FromRowId(this._firstRowId + index);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00008754 File Offset: 0x00006954
		public GenericParameterHandleCollection.Enumerator GetEnumerator()
		{
			return new GenericParameterHandleCollection.Enumerator(this._firstRowId, this._firstRowId + (int)this._count - 1);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00008770 File Offset: 0x00006970
		IEnumerator<GenericParameterHandle> IEnumerable<GenericParameterHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00008770 File Offset: 0x00006970
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400029D RID: 669
		private readonly int _firstRowId;

		// Token: 0x0400029E RID: 670
		private readonly ushort _count;

		// Token: 0x02000172 RID: 370
		public struct Enumerator : IEnumerator<GenericParameterHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000B6E RID: 2926 RVA: 0x00020D92 File Offset: 0x0001EF92
			internal Enumerator(int firstRowId, int lastRowId)
			{
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			// Token: 0x170002C5 RID: 709
			// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00020DA4 File Offset: 0x0001EFA4
			public GenericParameterHandle Current
			{
				get
				{
					return GenericParameterHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000B70 RID: 2928 RVA: 0x00020DBA File Offset: 0x0001EFBA
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

			// Token: 0x170002C6 RID: 710
			// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00020DE6 File Offset: 0x0001EFE6
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000B72 RID: 2930 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000B73 RID: 2931 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000960 RID: 2400
			private readonly int _lastRowId;

			// Token: 0x04000961 RID: 2401
			private int _currentRowId;

			// Token: 0x04000962 RID: 2402
			private const int EnumEnded = 16777216;
		}
	}
}
