using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200009A RID: 154
	public struct CustomDebugInformationHandle : IEquatable<CustomDebugInformationHandle>
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x0000F716 File Offset: 0x0000D916
		private CustomDebugInformationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000F71F File Offset: 0x0000D91F
		internal static CustomDebugInformationHandle FromRowId(int rowId)
		{
			return new CustomDebugInformationHandle(rowId);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0000F727 File Offset: 0x0000D927
		public static implicit operator Handle(CustomDebugInformationHandle handle)
		{
			return new Handle(55, handle._rowId);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0000F736 File Offset: 0x0000D936
		public static implicit operator EntityHandle(CustomDebugInformationHandle handle)
		{
			return new EntityHandle((uint)(922746880L | (long)handle._rowId));
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000F74C File Offset: 0x0000D94C
		public static explicit operator CustomDebugInformationHandle(Handle handle)
		{
			if (handle.VType != 55)
			{
				Throw.InvalidCast();
			}
			return new CustomDebugInformationHandle(handle.RowId);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0000F76A File Offset: 0x0000D96A
		public static explicit operator CustomDebugInformationHandle(EntityHandle handle)
		{
			if (handle.VType != 922746880U)
			{
				Throw.InvalidCast();
			}
			return new CustomDebugInformationHandle(handle.RowId);
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0000F78B File Offset: 0x0000D98B
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0000F796 File Offset: 0x0000D996
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000F79E File Offset: 0x0000D99E
		public static bool operator ==(CustomDebugInformationHandle left, CustomDebugInformationHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0000F7AE File Offset: 0x0000D9AE
		public override bool Equals(object obj)
		{
			return obj is CustomDebugInformationHandle && ((CustomDebugInformationHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0000F79E File Offset: 0x0000D99E
		public bool Equals(CustomDebugInformationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0000F7D0 File Offset: 0x0000D9D0
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0000F7EB File Offset: 0x0000D9EB
		public static bool operator !=(CustomDebugInformationHandle left, CustomDebugInformationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040003F9 RID: 1017
		private const uint tokenType = 922746880U;

		// Token: 0x040003FA RID: 1018
		private const byte tokenTypeSmall = 55;

		// Token: 0x040003FB RID: 1019
		private readonly int _rowId;
	}
}
