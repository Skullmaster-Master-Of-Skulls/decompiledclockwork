using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000042 RID: 66
	public struct DeclarativeSecurityAttributeHandleCollection : IReadOnlyCollection<DeclarativeSecurityAttributeHandle>, IEnumerable<DeclarativeSecurityAttributeHandle>, IEnumerable
	{
		// Token: 0x06000313 RID: 787 RVA: 0x00008858 File Offset: 0x00006A58
		internal DeclarativeSecurityAttributeHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.DeclSecurityTable.NumberOfRows;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00008879 File Offset: 0x00006A79
		internal DeclarativeSecurityAttributeHandleCollection(MetadataReader reader, EntityHandle handle)
		{
			this._reader = reader;
			reader.DeclSecurityTable.GetAttributeRange(handle, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000889A File Offset: 0x00006A9A
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000088AB File Offset: 0x00006AAB
		public DeclarativeSecurityAttributeHandleCollection.Enumerator GetEnumerator()
		{
			return new DeclarativeSecurityAttributeHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000088C4 File Offset: 0x00006AC4
		IEnumerator<DeclarativeSecurityAttributeHandle> IEnumerable<DeclarativeSecurityAttributeHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000088C4 File Offset: 0x00006AC4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002A4 RID: 676
		private readonly MetadataReader _reader;

		// Token: 0x040002A5 RID: 677
		private readonly int _firstRowId;

		// Token: 0x040002A6 RID: 678
		private readonly int _lastRowId;

		// Token: 0x02000175 RID: 373
		public struct Enumerator : IEnumerator<DeclarativeSecurityAttributeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000B81 RID: 2945 RVA: 0x00020EFB File Offset: 0x0001F0FB
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			// Token: 0x170002CB RID: 715
			// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00020F14 File Offset: 0x0001F114
			public DeclarativeSecurityAttributeHandle Current
			{
				get
				{
					return DeclarativeSecurityAttributeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x00020F2A File Offset: 0x0001F12A
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

			// Token: 0x170002CC RID: 716
			// (get) Token: 0x06000B84 RID: 2948 RVA: 0x00020F56 File Offset: 0x0001F156
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000B85 RID: 2949 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000B86 RID: 2950 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400096A RID: 2410
			private readonly MetadataReader _reader;

			// Token: 0x0400096B RID: 2411
			private readonly int _lastRowId;

			// Token: 0x0400096C RID: 2412
			private int _currentRowId;

			// Token: 0x0400096D RID: 2413
			private const int EnumEnded = 16777216;
		}
	}
}
