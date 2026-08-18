using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x0200004C RID: 76
	public struct ExportedTypeHandleCollection : IReadOnlyCollection<ExportedTypeHandle>, IEnumerable<ExportedTypeHandle>, IEnumerable
	{
		// Token: 0x0600034A RID: 842 RVA: 0x00008C0F File Offset: 0x00006E0F
		internal ExportedTypeHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00008C18 File Offset: 0x00006E18
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00008C20 File Offset: 0x00006E20
		public ExportedTypeHandleCollection.Enumerator GetEnumerator()
		{
			return new ExportedTypeHandleCollection.Enumerator(this._lastRowId);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00008C2D File Offset: 0x00006E2D
		IEnumerator<ExportedTypeHandle> IEnumerable<ExportedTypeHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00008C2D File Offset: 0x00006E2D
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002BD RID: 701
		private readonly int _lastRowId;

		// Token: 0x0200017F RID: 383
		public struct Enumerator : IEnumerator<ExportedTypeHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BC2 RID: 3010 RVA: 0x000213EC File Offset: 0x0001F5EC
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			// Token: 0x170002DF RID: 735
			// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x000213FC File Offset: 0x0001F5FC
			public ExportedTypeHandle Current
			{
				get
				{
					return ExportedTypeHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BC4 RID: 3012 RVA: 0x00021412 File Offset: 0x0001F612
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

			// Token: 0x170002E0 RID: 736
			// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0002143E File Offset: 0x0001F63E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BC6 RID: 3014 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BC7 RID: 3015 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0400098F RID: 2447
			private readonly int _lastRowId;

			// Token: 0x04000990 RID: 2448
			private int _currentRowId;

			// Token: 0x04000991 RID: 2449
			private const int EnumEnded = 16777216;
		}
	}
}
