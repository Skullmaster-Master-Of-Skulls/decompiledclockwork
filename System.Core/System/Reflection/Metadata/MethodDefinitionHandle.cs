using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000066 RID: 102
	internal struct MethodDefinitionHandle : IEquatable<MethodDefinitionHandle>
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0000770E File Offset: 0x0000590E
		private MethodDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00007717 File Offset: 0x00005917
		internal static MethodDefinitionHandle FromRowId(int rowId)
		{
			return new MethodDefinitionHandle(rowId);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000771F File Offset: 0x0000591F
		public static implicit operator Handle(MethodDefinitionHandle handle)
		{
			return new Handle(6, handle._rowId);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000772D File Offset: 0x0000592D
		public static explicit operator MethodDefinitionHandle(Handle handle)
		{
			if (handle.VType != 6)
			{
				Throw.InvalidCast();
			}
			return new MethodDefinitionHandle(handle.RowId);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000774A File Offset: 0x0000594A
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00007755 File Offset: 0x00005955
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000775D File Offset: 0x0000595D
		public static bool operator ==(MethodDefinitionHandle left, MethodDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000776D File Offset: 0x0000596D
		public override bool Equals(object obj)
		{
			return obj is MethodDefinitionHandle && ((MethodDefinitionHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000778C File Offset: 0x0000598C
		public bool Equals(MethodDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000779C File Offset: 0x0000599C
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000077B7 File Offset: 0x000059B7
		public static bool operator !=(MethodDefinitionHandle left, MethodDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000077CA File Offset: 0x000059CA
		public MethodDebugInformationHandle ToDebugInformationHandle()
		{
			return MethodDebugInformationHandle.FromRowId(this._rowId);
		}

		// Token: 0x04000363 RID: 867
		private const uint tokenType = 100663296U;

		// Token: 0x04000364 RID: 868
		private const byte tokenTypeSmall = 6;

		// Token: 0x04000365 RID: 869
		private readonly int _rowId;
	}
}
