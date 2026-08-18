using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000092 RID: 146
	public struct ImportScopeCollection : IReadOnlyCollection<ImportScopeHandle>, IEnumerable<ImportScopeHandle>, IEnumerable
	{
		// Token: 0x0600065B RID: 1627 RVA: 0x0000F0CB File Offset: 0x0000D2CB
		internal ImportScopeCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.ImportScopeTable.NumberOfRows;
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0000F0FD File Offset: 0x0000D2FD
		public ImportScopeCollection.Enumerator GetEnumerator()
		{
			return new ImportScopeCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x0000F116 File Offset: 0x0000D316
		IEnumerator<ImportScopeHandle> IEnumerable<ImportScopeHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0000F116 File Offset: 0x0000D316
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040003E1 RID: 993
		private readonly MetadataReader _reader;

		// Token: 0x040003E2 RID: 994
		private readonly int _firstRowId;

		// Token: 0x040003E3 RID: 995
		private readonly int _lastRowId;

		// Token: 0x0200018F RID: 399
		public struct Enumerator : IEnumerator<ImportScopeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C08 RID: 3080 RVA: 0x000219C6 File Offset: 0x0001FBC6
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x06000C09 RID: 3081 RVA: 0x000219DF File Offset: 0x0001FBDF
			public ImportScopeHandle Current
			{
				get
				{
					return ImportScopeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000C0A RID: 3082 RVA: 0x000219F5 File Offset: 0x0001FBF5
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

			// Token: 0x170002F6 RID: 758
			// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00021A21 File Offset: 0x0001FC21
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000C0C RID: 3084 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000C0D RID: 3085 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A19 RID: 2585
			private readonly MetadataReader _reader;

			// Token: 0x04000A1A RID: 2586
			private readonly int _lastRowId;

			// Token: 0x04000A1B RID: 2587
			private int _currentRowId;

			// Token: 0x04000A1C RID: 2588
			private const int EnumEnded = 16777216;
		}
	}
}
