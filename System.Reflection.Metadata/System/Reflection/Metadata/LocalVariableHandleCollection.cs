using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000090 RID: 144
	public struct LocalVariableHandleCollection : IReadOnlyCollection<LocalVariableHandle>, IEnumerable<LocalVariableHandle>, IEnumerable
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x0000EFE1 File Offset: 0x0000D1E1
		internal LocalVariableHandleCollection(MetadataReader reader, LocalScopeHandle scope)
		{
			this._reader = reader;
			if (scope.IsNil)
			{
				this._firstRowId = 1;
				this._lastRowId = reader.LocalVariableTable.NumberOfRows;
				return;
			}
			reader.GetLocalVariableRange(scope, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x0000F01F File Offset: 0x0000D21F
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0000F030 File Offset: 0x0000D230
		public LocalVariableHandleCollection.Enumerator GetEnumerator()
		{
			return new LocalVariableHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0000F049 File Offset: 0x0000D249
		IEnumerator<LocalVariableHandle> IEnumerable<LocalVariableHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0000F049 File Offset: 0x0000D249
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040003DB RID: 987
		private readonly MetadataReader _reader;

		// Token: 0x040003DC RID: 988
		private readonly int _firstRowId;

		// Token: 0x040003DD RID: 989
		private readonly int _lastRowId;

		// Token: 0x0200018D RID: 397
		public struct Enumerator : IEnumerator<LocalVariableHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BFC RID: 3068 RVA: 0x000218F6 File Offset: 0x0001FAF6
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000BFD RID: 3069 RVA: 0x0002190F File Offset: 0x0001FB0F
			public LocalVariableHandle Current
			{
				get
				{
					return LocalVariableHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BFE RID: 3070 RVA: 0x00021925 File Offset: 0x0001FB25
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

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00021951 File Offset: 0x0001FB51
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000C00 RID: 3072 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000C01 RID: 3073 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A11 RID: 2577
			private readonly MetadataReader _reader;

			// Token: 0x04000A12 RID: 2578
			private readonly int _lastRowId;

			// Token: 0x04000A13 RID: 2579
			private int _currentRowId;

			// Token: 0x04000A14 RID: 2580
			private const int EnumEnded = 16777216;
		}
	}
}
