using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000049 RID: 73
	public struct InterfaceImplementationHandleCollection : IReadOnlyCollection<InterfaceImplementationHandle>, IEnumerable<InterfaceImplementationHandle>, IEnumerable
	{
		// Token: 0x0600033B RID: 827 RVA: 0x00008B61 File Offset: 0x00006D61
		internal InterfaceImplementationHandleCollection(MetadataReader reader, TypeDefinitionHandle implementingType)
		{
			this._reader = reader;
			reader.InterfaceImplTable.GetInterfaceImplRange(implementingType, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00008B82 File Offset: 0x00006D82
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00008B93 File Offset: 0x00006D93
		public InterfaceImplementationHandleCollection.Enumerator GetEnumerator()
		{
			return new InterfaceImplementationHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00008BAC File Offset: 0x00006DAC
		IEnumerator<InterfaceImplementationHandle> IEnumerable<InterfaceImplementationHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00008BAC File Offset: 0x00006DAC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002B8 RID: 696
		private readonly MetadataReader _reader;

		// Token: 0x040002B9 RID: 697
		private readonly int _firstRowId;

		// Token: 0x040002BA RID: 698
		private readonly int _lastRowId;

		// Token: 0x0200017C RID: 380
		public struct Enumerator : IEnumerator<InterfaceImplementationHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BB0 RID: 2992 RVA: 0x000212C6 File Offset: 0x0001F4C6
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			// Token: 0x170002D9 RID: 729
			// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x000212DF File Offset: 0x0001F4DF
			public InterfaceImplementationHandle Current
			{
				get
				{
					return InterfaceImplementationHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BB2 RID: 2994 RVA: 0x000212F5 File Offset: 0x0001F4F5
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

			// Token: 0x170002DA RID: 730
			// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x00021321 File Offset: 0x0001F521
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BB4 RID: 2996 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BB5 RID: 2997 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000985 RID: 2437
			private readonly MetadataReader _reader;

			// Token: 0x04000986 RID: 2438
			private readonly int _lastRowId;

			// Token: 0x04000987 RID: 2439
			private int _currentRowId;

			// Token: 0x04000988 RID: 2440
			private const int EnumEnded = 16777216;
		}
	}
}
