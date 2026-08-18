using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x02000050 RID: 80
	public struct AssemblyReferenceHandleCollection : IReadOnlyCollection<AssemblyReferenceHandle>, IEnumerable<AssemblyReferenceHandle>, IEnumerable
	{
		// Token: 0x0600035B RID: 859 RVA: 0x00008CCD File Offset: 0x00006ECD
		internal AssemblyReferenceHandleCollection(MetadataReader reader)
		{
			this._reader = reader;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00008CD6 File Offset: 0x00006ED6
		public int Count
		{
			get
			{
				return this._reader.AssemblyRefTable.NumberOfNonVirtualRows + this._reader.AssemblyRefTable.NumberOfVirtualRows;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00008CF9 File Offset: 0x00006EF9
		public AssemblyReferenceHandleCollection.Enumerator GetEnumerator()
		{
			return new AssemblyReferenceHandleCollection.Enumerator(this._reader);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00008D06 File Offset: 0x00006F06
		IEnumerator<AssemblyReferenceHandle> IEnumerable<AssemblyReferenceHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00008D06 File Offset: 0x00006F06
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002C4 RID: 708
		private readonly MetadataReader _reader;

		// Token: 0x02000181 RID: 385
		public struct Enumerator : IEnumerator<AssemblyReferenceHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BCE RID: 3022 RVA: 0x000214AA File Offset: 0x0001F6AA
			internal Enumerator(MetadataReader reader)
			{
				this._reader = reader;
				this._currentRowId = 0;
				this._virtualRowId = -1;
			}

			// Token: 0x170002E3 RID: 739
			// (get) Token: 0x06000BCF RID: 3023 RVA: 0x000214C4 File Offset: 0x0001F6C4
			public AssemblyReferenceHandle Current
			{
				get
				{
					if (this._virtualRowId < 0)
					{
						return AssemblyReferenceHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
					}
					if (this._virtualRowId == 16777216)
					{
						return default(AssemblyReferenceHandle);
					}
					return AssemblyReferenceHandle.FromVirtualIndex((AssemblyReferenceHandle.VirtualIndex)this._virtualRowId);
				}
			}

			// Token: 0x06000BD0 RID: 3024 RVA: 0x00021514 File Offset: 0x0001F714
			public bool MoveNext()
			{
				if (this._currentRowId < this._reader.AssemblyRefTable.NumberOfNonVirtualRows)
				{
					this._currentRowId++;
					return true;
				}
				if (this._virtualRowId < this._reader.AssemblyRefTable.NumberOfVirtualRows - 1)
				{
					this._virtualRowId++;
					return true;
				}
				this._currentRowId = 16777216;
				this._virtualRowId = 16777216;
				return false;
			}

			// Token: 0x170002E4 RID: 740
			// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0002158A File Offset: 0x0001F78A
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BD2 RID: 3026 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BD3 RID: 3027 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000995 RID: 2453
			private readonly MetadataReader _reader;

			// Token: 0x04000996 RID: 2454
			private int _currentRowId;

			// Token: 0x04000997 RID: 2455
			private const int EnumEnded = 16777216;

			// Token: 0x04000998 RID: 2456
			private int _virtualRowId;
		}
	}
}
