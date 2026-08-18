using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008D RID: 141
	public struct DocumentHandleCollection : IReadOnlyCollection<DocumentHandle>, IEnumerable<DocumentHandle>, IEnumerable
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x0000EEBD File Offset: 0x0000D0BD
		internal DocumentHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.DocumentTable.NumberOfRows;
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0000EEDE File Offset: 0x0000D0DE
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000EEEF File Offset: 0x0000D0EF
		public DocumentHandleCollection.Enumerator GetEnumerator()
		{
			return new DocumentHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0000EF08 File Offset: 0x0000D108
		IEnumerator<DocumentHandle> IEnumerable<DocumentHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000EF08 File Offset: 0x0000D108
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040003D2 RID: 978
		private readonly MetadataReader _reader;

		// Token: 0x040003D3 RID: 979
		private readonly int _firstRowId;

		// Token: 0x040003D4 RID: 980
		private readonly int _lastRowId;

		// Token: 0x02000189 RID: 393
		public struct Enumerator : IEnumerator<DocumentHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BE4 RID: 3044 RVA: 0x0002169F File Offset: 0x0001F89F
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x000216B8 File Offset: 0x0001F8B8
			public DocumentHandle Current
			{
				get
				{
					return DocumentHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BE6 RID: 3046 RVA: 0x000216CE File Offset: 0x0001F8CE
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

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000216FA File Offset: 0x0001F8FA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BE8 RID: 3048 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BE9 RID: 3049 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x040009FF RID: 2559
			private readonly MetadataReader _reader;

			// Token: 0x04000A00 RID: 2560
			private readonly int _lastRowId;

			// Token: 0x04000A01 RID: 2561
			private int _currentRowId;

			// Token: 0x04000A02 RID: 2562
			private const int EnumEnded = 16777216;
		}
	}
}
