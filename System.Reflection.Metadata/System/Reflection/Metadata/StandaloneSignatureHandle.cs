using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000066 RID: 102
	public struct StandaloneSignatureHandle : IEquatable<StandaloneSignatureHandle>
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x00009D12 File Offset: 0x00007F12
		private StandaloneSignatureHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00009D1B File Offset: 0x00007F1B
		internal static StandaloneSignatureHandle FromRowId(int rowId)
		{
			return new StandaloneSignatureHandle(rowId);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00009D23 File Offset: 0x00007F23
		public static implicit operator Handle(StandaloneSignatureHandle handle)
		{
			return new Handle(17, handle._rowId);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00009D32 File Offset: 0x00007F32
		public static implicit operator EntityHandle(StandaloneSignatureHandle handle)
		{
			return new EntityHandle((uint)(285212672L | (long)handle._rowId));
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x00009D48 File Offset: 0x00007F48
		public static explicit operator StandaloneSignatureHandle(Handle handle)
		{
			if (handle.VType != 17)
			{
				Throw.InvalidCast();
			}
			return new StandaloneSignatureHandle(handle.RowId);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00009D66 File Offset: 0x00007F66
		public static explicit operator StandaloneSignatureHandle(EntityHandle handle)
		{
			if (handle.VType != 285212672U)
			{
				Throw.InvalidCast();
			}
			return new StandaloneSignatureHandle(handle.RowId);
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00009D87 File Offset: 0x00007F87
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00009D92 File Offset: 0x00007F92
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00009D9A File Offset: 0x00007F9A
		public static bool operator ==(StandaloneSignatureHandle left, StandaloneSignatureHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00009DAA File Offset: 0x00007FAA
		public override bool Equals(object obj)
		{
			return obj is StandaloneSignatureHandle && ((StandaloneSignatureHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00009D9A File Offset: 0x00007F9A
		public bool Equals(StandaloneSignatureHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00009DCC File Offset: 0x00007FCC
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00009DE7 File Offset: 0x00007FE7
		public static bool operator !=(StandaloneSignatureHandle left, StandaloneSignatureHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400031F RID: 799
		private const uint tokenType = 285212672U;

		// Token: 0x04000320 RID: 800
		private const byte tokenTypeSmall = 17;

		// Token: 0x04000321 RID: 801
		private readonly int _rowId;
	}
}
