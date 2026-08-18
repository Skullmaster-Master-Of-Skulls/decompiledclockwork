using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000099 RID: 153
	public struct ImportScopeHandle : IEquatable<ImportScopeHandle>
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x0000F62E File Offset: 0x0000D82E
		private ImportScopeHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0000F637 File Offset: 0x0000D837
		internal static ImportScopeHandle FromRowId(int rowId)
		{
			return new ImportScopeHandle(rowId);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0000F63F File Offset: 0x0000D83F
		public static implicit operator Handle(ImportScopeHandle handle)
		{
			return new Handle(53, handle._rowId);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0000F64E File Offset: 0x0000D84E
		public static implicit operator EntityHandle(ImportScopeHandle handle)
		{
			return new EntityHandle((uint)(889192448L | (long)handle._rowId));
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0000F664 File Offset: 0x0000D864
		public static explicit operator ImportScopeHandle(Handle handle)
		{
			if (handle.VType != 53)
			{
				Throw.InvalidCast();
			}
			return new ImportScopeHandle(handle.RowId);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0000F682 File Offset: 0x0000D882
		public static explicit operator ImportScopeHandle(EntityHandle handle)
		{
			if (handle.VType != 889192448U)
			{
				Throw.InvalidCast();
			}
			return new ImportScopeHandle(handle.RowId);
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x0000F6A3 File Offset: 0x0000D8A3
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0000F6AE File Offset: 0x0000D8AE
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0000F6B6 File Offset: 0x0000D8B6
		public static bool operator ==(ImportScopeHandle left, ImportScopeHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0000F6C6 File Offset: 0x0000D8C6
		public override bool Equals(object obj)
		{
			return obj is ImportScopeHandle && ((ImportScopeHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0000F6B6 File Offset: 0x0000D8B6
		public bool Equals(ImportScopeHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0000F6E8 File Offset: 0x0000D8E8
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000F703 File Offset: 0x0000D903
		public static bool operator !=(ImportScopeHandle left, ImportScopeHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040003F6 RID: 1014
		private const uint tokenType = 889192448U;

		// Token: 0x040003F7 RID: 1015
		private const byte tokenTypeSmall = 53;

		// Token: 0x040003F8 RID: 1016
		private readonly int _rowId;
	}
}
