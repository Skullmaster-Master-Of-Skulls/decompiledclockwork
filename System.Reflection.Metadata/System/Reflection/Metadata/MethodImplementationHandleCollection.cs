using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000047 RID: 71
	public struct MethodImplementationHandleCollection : IReadOnlyCollection<MethodImplementationHandle>, IEnumerable<MethodImplementationHandle>, IEnumerable
	{
		// Token: 0x06000331 RID: 817 RVA: 0x00008AA1 File Offset: 0x00006CA1
		internal MethodImplementationHandleCollection(MetadataReader reader, TypeDefinitionHandle containingType)
		{
			if (containingType.IsNil)
			{
				this._firstRowId = 1;
				this._lastRowId = reader.MethodImplTable.NumberOfRows;
				return;
			}
			reader.MethodImplTable.GetMethodImplRange(containingType, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00008ADD File Offset: 0x00006CDD
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00008AEE File Offset: 0x00006CEE
		public MethodImplementationHandleCollection.Enumerator GetEnumerator()
		{
			return new MethodImplementationHandleCollection.Enumerator(this._firstRowId, this._lastRowId);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00008B01 File Offset: 0x00006D01
		IEnumerator<MethodImplementationHandle> IEnumerable<MethodImplementationHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00008B01 File Offset: 0x00006D01
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002B3 RID: 691
		private readonly int _firstRowId;

		// Token: 0x040002B4 RID: 692
		private readonly int _lastRowId;

		// Token: 0x0200017A RID: 378
		public struct Enumerator : IEnumerator<MethodImplementationHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BA3 RID: 2979 RVA: 0x000211CB File Offset: 0x0001F3CB
			internal Enumerator(int firstRowId, int lastRowId)
			{
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x000211DD File Offset: 0x0001F3DD
			public MethodImplementationHandle Current
			{
				get
				{
					return MethodImplementationHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BA5 RID: 2981 RVA: 0x000211F3 File Offset: 0x0001F3F3
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

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0002121F File Offset: 0x0001F41F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BA7 RID: 2983 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BA8 RID: 2984 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400097E RID: 2430
			private readonly int _lastRowId;

			// Token: 0x0400097F RID: 2431
			private int _currentRowId;

			// Token: 0x04000980 RID: 2432
			private const int EnumEnded = 16777216;
		}
	}
}
