using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000067 RID: 103
	public struct ParameterHandle : IEquatable<ParameterHandle>
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00009DFA File Offset: 0x00007FFA
		private ParameterHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00009E03 File Offset: 0x00008003
		internal static ParameterHandle FromRowId(int rowId)
		{
			return new ParameterHandle(rowId);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00009E0B File Offset: 0x0000800B
		public static implicit operator Handle(ParameterHandle handle)
		{
			return new Handle(8, handle._rowId);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00009E19 File Offset: 0x00008019
		public static implicit operator EntityHandle(ParameterHandle handle)
		{
			return new EntityHandle((uint)(134217728L | (long)handle._rowId));
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00009E2F File Offset: 0x0000802F
		public static explicit operator ParameterHandle(Handle handle)
		{
			if (handle.VType != 8)
			{
				Throw.InvalidCast();
			}
			return new ParameterHandle(handle.RowId);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00009E4C File Offset: 0x0000804C
		public static explicit operator ParameterHandle(EntityHandle handle)
		{
			if (handle.VType != 134217728U)
			{
				Throw.InvalidCast();
			}
			return new ParameterHandle(handle.RowId);
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00009E6D File Offset: 0x0000806D
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00009E78 File Offset: 0x00008078
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00009E80 File Offset: 0x00008080
		public static bool operator ==(ParameterHandle left, ParameterHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00009E90 File Offset: 0x00008090
		public override bool Equals(object obj)
		{
			return obj is ParameterHandle && ((ParameterHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00009E80 File Offset: 0x00008080
		public bool Equals(ParameterHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00009EB0 File Offset: 0x000080B0
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00009ECB File Offset: 0x000080CB
		public static bool operator !=(ParameterHandle left, ParameterHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000322 RID: 802
		private const uint tokenType = 134217728U;

		// Token: 0x04000323 RID: 803
		private const byte tokenTypeSmall = 8;

		// Token: 0x04000324 RID: 804
		private readonly int _rowId;
	}
}
