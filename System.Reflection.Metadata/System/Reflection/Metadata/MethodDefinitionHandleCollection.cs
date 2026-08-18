using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000043 RID: 67
	public struct MethodDefinitionHandleCollection : IReadOnlyCollection<MethodDefinitionHandle>, IEnumerable<MethodDefinitionHandle>, IEnumerable
	{
		// Token: 0x06000319 RID: 793 RVA: 0x000088D1 File Offset: 0x00006AD1
		internal MethodDefinitionHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
			this._firstRowId = 1;
			this._lastRowId = reader.MethodDefTable.NumberOfRows;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000088F2 File Offset: 0x00006AF2
		internal MethodDefinitionHandleCollection(MetadataReader reader, TypeDefinitionHandle containingType)
		{
			this._reader = reader;
			reader.GetMethodRange(containingType, out this._firstRowId, out this._lastRowId);
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000890E File Offset: 0x00006B0E
		public int Count
		{
			get
			{
				return this._lastRowId - this._firstRowId + 1;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000891F File Offset: 0x00006B1F
		public MethodDefinitionHandleCollection.Enumerator GetEnumerator()
		{
			return new MethodDefinitionHandleCollection.Enumerator(this._reader, this._firstRowId, this._lastRowId);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00008938 File Offset: 0x00006B38
		IEnumerator<MethodDefinitionHandle> IEnumerable<MethodDefinitionHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00008938 File Offset: 0x00006B38
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002A7 RID: 679
		private readonly MetadataReader _reader;

		// Token: 0x040002A8 RID: 680
		private readonly int _firstRowId;

		// Token: 0x040002A9 RID: 681
		private readonly int _lastRowId;

		// Token: 0x02000176 RID: 374
		public struct Enumerator : IEnumerator<MethodDefinitionHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000B87 RID: 2951 RVA: 0x00020F63 File Offset: 0x0001F163
			internal Enumerator(MetadataReader reader, int firstRowId, int lastRowId)
			{
				this._reader = reader;
				this._currentRowId = firstRowId - 1;
				this._lastRowId = lastRowId;
			}

			// Token: 0x170002CD RID: 717
			// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00020F7C File Offset: 0x0001F17C
			public MethodDefinitionHandle Current
			{
				get
				{
					if (this._reader.UseMethodPtrTable)
					{
						return this.GetCurrentMethodIndirect();
					}
					return MethodDefinitionHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000B89 RID: 2953 RVA: 0x00020FA6 File Offset: 0x0001F1A6
			private MethodDefinitionHandle GetCurrentMethodIndirect()
			{
				return this._reader.MethodPtrTable.GetMethodFor(this._currentRowId & 16777215);
			}

			// Token: 0x06000B8A RID: 2954 RVA: 0x00020FC4 File Offset: 0x0001F1C4
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

			// Token: 0x170002CE RID: 718
			// (get) Token: 0x06000B8B RID: 2955 RVA: 0x00020FF0 File Offset: 0x0001F1F0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000B8C RID: 2956 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000B8D RID: 2957 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400096E RID: 2414
			private readonly MetadataReader _reader;

			// Token: 0x0400096F RID: 2415
			private readonly int _lastRowId;

			// Token: 0x04000970 RID: 2416
			private int _currentRowId;

			// Token: 0x04000971 RID: 2417
			private const int EnumEnded = 16777216;
		}
	}
}
