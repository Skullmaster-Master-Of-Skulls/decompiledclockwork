using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000041 RID: 65
	public struct CustomAttributeHandleCollection : IReadOnlyCollection<CustomAttributeHandle>, IEnumerable<CustomAttributeHandle>, IEnumerable
	{
		// Token: 0x0600030D RID: 781 RVA: 0x000087DF File Offset: 0x000069DF
		internal CustomAttributeHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.CustomAttributeTable.NumberOfRows;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00008800 File Offset: 0x00006A00
		internal CustomAttributeHandleCollection(MetadataReader reader, EntityHandle handle)
		{
			this._reader = reader;
			reader.CustomAttributeTable.GetAttributeRange(handle, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00008821 File Offset: 0x00006A21
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00008832 File Offset: 0x00006A32
		public CustomAttributeHandleCollection.Enumerator GetEnumerator()
		{
			return new CustomAttributeHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000884B File Offset: 0x00006A4B
		IEnumerator<CustomAttributeHandle> IEnumerable<CustomAttributeHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000884B File Offset: 0x00006A4B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002A1 RID: 673
		private readonly MetadataReader _reader;

		// Token: 0x040002A2 RID: 674
		private readonly int _firstRowId;

		// Token: 0x040002A3 RID: 675
		private readonly int _lastRowId;

		// Token: 0x02000174 RID: 372
		public struct Enumerator : IEnumerator<CustomAttributeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000B7A RID: 2938 RVA: 0x00020E54 File Offset: 0x0001F054
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			// Token: 0x170002C9 RID: 713
			// (get) Token: 0x06000B7B RID: 2939 RVA: 0x00020E6D File Offset: 0x0001F06D
			public CustomAttributeHandle Current
			{
				get
				{
					if (this._reader.CustomAttributeTable.PtrTable != null)
					{
						return this.GetCurrentCustomAttributeIndirect();
					}
					return CustomAttributeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000B7C RID: 2940 RVA: 0x00020E9C File Offset: 0x0001F09C
			private CustomAttributeHandle GetCurrentCustomAttributeIndirect()
			{
				return CustomAttributeHandle.FromRowId(this._reader.CustomAttributeTable.PtrTable[(this._currentRowId & 16777215) - 1]);
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x00020EC2 File Offset: 0x0001F0C2
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

			// Token: 0x170002CA RID: 714
			// (get) Token: 0x06000B7E RID: 2942 RVA: 0x00020EEE File Offset: 0x0001F0EE
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000B80 RID: 2944 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000966 RID: 2406
			private readonly MetadataReader _reader;

			// Token: 0x04000967 RID: 2407
			private readonly int _lastRowId;

			// Token: 0x04000968 RID: 2408
			private int _currentRowId;

			// Token: 0x04000969 RID: 2409
			private const int EnumEnded = 16777216;
		}
	}
}
