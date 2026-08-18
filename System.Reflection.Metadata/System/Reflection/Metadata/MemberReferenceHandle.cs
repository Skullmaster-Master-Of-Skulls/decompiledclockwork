using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000062 RID: 98
	public struct MemberReferenceHandle : IEquatable<MemberReferenceHandle>
	{
		// Token: 0x0600041F RID: 1055 RVA: 0x00009976 File Offset: 0x00007B76
		private MemberReferenceHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000997F File Offset: 0x00007B7F
		internal static MemberReferenceHandle FromRowId(int rowId)
		{
			return new MemberReferenceHandle(rowId);
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00009987 File Offset: 0x00007B87
		public static implicit operator Handle(MemberReferenceHandle handle)
		{
			return new Handle(10, handle._rowId);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00009996 File Offset: 0x00007B96
		public static implicit operator EntityHandle(MemberReferenceHandle handle)
		{
			return new EntityHandle((uint)(167772160L | (long)handle._rowId));
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000099AC File Offset: 0x00007BAC
		public static explicit operator MemberReferenceHandle(Handle handle)
		{
			if (handle.VType != 10)
			{
				Throw.InvalidCast();
			}
			return new MemberReferenceHandle(handle.RowId);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000099CA File Offset: 0x00007BCA
		public static explicit operator MemberReferenceHandle(EntityHandle handle)
		{
			if (handle.VType != 167772160U)
			{
				Throw.InvalidCast();
			}
			return new MemberReferenceHandle(handle.RowId);
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x000099EB File Offset: 0x00007BEB
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x000099F6 File Offset: 0x00007BF6
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000099FE File Offset: 0x00007BFE
		public static bool operator ==(MemberReferenceHandle left, MemberReferenceHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00009A0E File Offset: 0x00007C0E
		public override bool Equals(object obj)
		{
			return obj is MemberReferenceHandle && ((MemberReferenceHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000099FE File Offset: 0x00007BFE
		public bool Equals(MemberReferenceHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00009A30 File Offset: 0x00007C30
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00009A4B File Offset: 0x00007C4B
		public static bool operator !=(MemberReferenceHandle left, MemberReferenceHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000313 RID: 787
		private const uint tokenType = 167772160U;

		// Token: 0x04000314 RID: 788
		private const byte tokenTypeSmall = 10;

		// Token: 0x04000315 RID: 789
		private readonly int _rowId;
	}
}
