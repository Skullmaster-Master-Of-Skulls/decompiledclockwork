using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000093 RID: 147
	public struct CustomDebugInformationHandleCollection : IReadOnlyCollection<CustomDebugInformationHandle>, IEnumerable<CustomDebugInformationHandle>, IEnumerable
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x0000F123 File Offset: 0x0000D323
		internal CustomDebugInformationHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.CustomDebugInformationTable.NumberOfRows;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0000F144 File Offset: 0x0000D344
		internal CustomDebugInformationHandleCollection(MetadataReader reader, EntityHandle handle)
		{
			this._reader = reader;
			reader.CustomDebugInformationTable.GetRange(handle, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0000F165 File Offset: 0x0000D365
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0000F176 File Offset: 0x0000D376
		public CustomDebugInformationHandleCollection.Enumerator GetEnumerator()
		{
			return new CustomDebugInformationHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0000F18F File Offset: 0x0000D38F
		IEnumerator<CustomDebugInformationHandle> IEnumerable<CustomDebugInformationHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0000F18F File Offset: 0x0000D38F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040003E4 RID: 996
		private readonly MetadataReader _reader;

		// Token: 0x040003E5 RID: 997
		private readonly int _firstRowId;

		// Token: 0x040003E6 RID: 998
		private readonly int _lastRowId;

		// Token: 0x02000190 RID: 400
		public struct Enumerator : IEnumerator<CustomDebugInformationHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C0E RID: 3086 RVA: 0x00021A2E File Offset: 0x0001FC2E
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			// Token: 0x170002F7 RID: 759
			// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00021A47 File Offset: 0x0001FC47
			public CustomDebugInformationHandle Current
			{
				get
				{
					return CustomDebugInformationHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000C10 RID: 3088 RVA: 0x00021A5D File Offset: 0x0001FC5D
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

			// Token: 0x170002F8 RID: 760
			// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00021A89 File Offset: 0x0001FC89
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000C12 RID: 3090 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000C13 RID: 3091 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A1D RID: 2589
			private readonly MetadataReader _reader;

			// Token: 0x04000A1E RID: 2590
			private readonly int _lastRowId;

			// Token: 0x04000A1F RID: 2591
			private int _currentRowId;

			// Token: 0x04000A20 RID: 2592
			private const int EnumEnded = 16777216;
		}
	}
}
