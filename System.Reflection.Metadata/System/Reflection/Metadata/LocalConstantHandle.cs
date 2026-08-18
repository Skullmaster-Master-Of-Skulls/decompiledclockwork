using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000098 RID: 152
	public struct LocalConstantHandle : IEquatable<LocalConstantHandle>
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x0000F546 File Offset: 0x0000D746
		private LocalConstantHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0000F54F File Offset: 0x0000D74F
		internal static LocalConstantHandle FromRowId(int rowId)
		{
			return new LocalConstantHandle(rowId);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0000F557 File Offset: 0x0000D757
		public static implicit operator Handle(LocalConstantHandle handle)
		{
			return new Handle(52, handle._rowId);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0000F566 File Offset: 0x0000D766
		public static implicit operator EntityHandle(LocalConstantHandle handle)
		{
			return new EntityHandle((uint)(872415232L | (long)handle._rowId));
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0000F57C File Offset: 0x0000D77C
		public static explicit operator LocalConstantHandle(Handle handle)
		{
			if (handle.VType != 52)
			{
				Throw.InvalidCast();
			}
			return new LocalConstantHandle(handle.RowId);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0000F59A File Offset: 0x0000D79A
		public static explicit operator LocalConstantHandle(EntityHandle handle)
		{
			if (handle.VType != 872415232U)
			{
				Throw.InvalidCast();
			}
			return new LocalConstantHandle(handle.RowId);
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0000F5BB File Offset: 0x0000D7BB
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0000F5C6 File Offset: 0x0000D7C6
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000F5CE File Offset: 0x0000D7CE
		public static bool operator ==(LocalConstantHandle left, LocalConstantHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0000F5DE File Offset: 0x0000D7DE
		public override bool Equals(object obj)
		{
			return obj is LocalConstantHandle && ((LocalConstantHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0000F5CE File Offset: 0x0000D7CE
		public bool Equals(LocalConstantHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000F600 File Offset: 0x0000D800
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0000F61B File Offset: 0x0000D81B
		public static bool operator !=(LocalConstantHandle left, LocalConstantHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040003F3 RID: 1011
		private const uint tokenType = 872415232U;

		// Token: 0x040003F4 RID: 1012
		private const byte tokenTypeSmall = 52;

		// Token: 0x040003F5 RID: 1013
		private readonly int _rowId;
	}
}
