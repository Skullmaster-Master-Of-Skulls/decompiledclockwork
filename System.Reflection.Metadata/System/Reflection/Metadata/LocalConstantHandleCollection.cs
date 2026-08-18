using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000091 RID: 145
	public struct LocalConstantHandleCollection : IReadOnlyCollection<LocalConstantHandle>, IEnumerable<LocalConstantHandle>, IEnumerable
	{
		// Token: 0x06000656 RID: 1622 RVA: 0x0000F056 File Offset: 0x0000D256
		internal LocalConstantHandleCollection(MetadataReader reader, LocalScopeHandle scope)
		{
			this._reader = reader;
			if (scope.IsNil)
			{
				this._firstRowId = 1;
				this._lastRowId = reader.LocalConstantTable.NumberOfRows;
				return;
			}
			reader.GetLocalConstantRange(scope, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0000F094 File Offset: 0x0000D294
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0000F0A5 File Offset: 0x0000D2A5
		public LocalConstantHandleCollection.Enumerator GetEnumerator()
		{
			return new LocalConstantHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0000F0BE File Offset: 0x0000D2BE
		IEnumerator<LocalConstantHandle> IEnumerable<LocalConstantHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0000F0BE File Offset: 0x0000D2BE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040003DE RID: 990
		private readonly MetadataReader _reader;

		// Token: 0x040003DF RID: 991
		private readonly int _firstRowId;

		// Token: 0x040003E0 RID: 992
		private readonly int _lastRowId;

		// Token: 0x0200018E RID: 398
		public struct Enumerator : IEnumerator<LocalConstantHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000C02 RID: 3074 RVA: 0x0002195E File Offset: 0x0001FB5E
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._lastRowId = lastRowId;
				this._currentRowId = firstRowId - 1;
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00021977 File Offset: 0x0001FB77
			public LocalConstantHandle Current
			{
				get
				{
					return LocalConstantHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000C04 RID: 3076 RVA: 0x0002198D File Offset: 0x0001FB8D
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

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000C05 RID: 3077 RVA: 0x000219B9 File Offset: 0x0001FBB9
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000C06 RID: 3078 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000C07 RID: 3079 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000A15 RID: 2581
			private readonly MetadataReader _reader;

			// Token: 0x04000A16 RID: 2582
			private readonly int _lastRowId;

			// Token: 0x04000A17 RID: 2583
			private int _currentRowId;

			// Token: 0x04000A18 RID: 2584
			private const int EnumEnded = 16777216;
		}
	}
}
