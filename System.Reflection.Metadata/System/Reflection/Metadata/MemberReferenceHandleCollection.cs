using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Reflection.Metadata
{
	// Token: 0x0200004D RID: 77
	public struct MemberReferenceHandleCollection : IReadOnlyCollection<MemberReferenceHandle>, IEnumerable<MemberReferenceHandle>, IEnumerable
	{
		// Token: 0x0600034F RID: 847 RVA: 0x00008C3A File Offset: 0x00006E3A
		internal MemberReferenceHandleCollection(int lastRowId)
		{
			this._lastRowId = lastRowId;
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00008C43 File Offset: 0x00006E43
		public int Count
		{
			get
			{
				return this._lastRowId;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00008C4B File Offset: 0x00006E4B
		public MemberReferenceHandleCollection.Enumerator GetEnumerator()
		{
			return new MemberReferenceHandleCollection.Enumerator(this._lastRowId);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00008C58 File Offset: 0x00006E58
		IEnumerator<MemberReferenceHandle> IEnumerable<MemberReferenceHandle>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00008C58 File Offset: 0x00006E58
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002BE RID: 702
		private readonly int _lastRowId;

		// Token: 0x02000180 RID: 384
		public struct Enumerator : IEnumerator<MemberReferenceHandle>, IEnumerator, IDisposable
		{
			// Token: 0x06000BC8 RID: 3016 RVA: 0x0002144B File Offset: 0x0001F64B
			internal Enumerator(int lastRowId)
			{
				this._lastRowId = lastRowId;
				this._currentRowId = 0;
			}

			// Token: 0x170002E1 RID: 737
			// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0002145B File Offset: 0x0001F65B
			public MemberReferenceHandle Current
			{
				get
				{
					return MemberReferenceHandle.FromRowId((int)((long)this._currentRowId & 16777215L));
				}
			}

			// Token: 0x06000BCA RID: 3018 RVA: 0x00021471 File Offset: 0x0001F671
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

			// Token: 0x170002E2 RID: 738
			// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0002149D File Offset: 0x0001F69D
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000BCC RID: 3020 RVA: 0x0001F5F8 File Offset: 0x0001D7F8
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x06000BCD RID: 3021 RVA: 0x000031EB File Offset: 0x000013EB
			void IDisposable.Dispose()
			{
			}

			// Token: 0x04000992 RID: 2450
			private readonly int _lastRowId;

			// Token: 0x04000993 RID: 2451
			private int _currentRowId;

			// Token: 0x04000994 RID: 2452
			private const int EnumEnded = 16777216;
		}
	}
}
