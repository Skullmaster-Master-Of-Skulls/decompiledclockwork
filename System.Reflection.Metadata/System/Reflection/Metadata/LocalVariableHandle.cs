using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000097 RID: 151
	public struct LocalVariableHandle : IEquatable<LocalVariableHandle>
	{
		// Token: 0x0600068E RID: 1678 RVA: 0x0000F45E File Offset: 0x0000D65E
		private LocalVariableHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0000F467 File Offset: 0x0000D667
		internal static LocalVariableHandle FromRowId(int rowId)
		{
			return new LocalVariableHandle(rowId);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x0000F46F File Offset: 0x0000D66F
		public static implicit operator Handle(LocalVariableHandle handle)
		{
			return new Handle(51, handle._rowId);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0000F47E File Offset: 0x0000D67E
		public static implicit operator EntityHandle(LocalVariableHandle handle)
		{
			return new EntityHandle((uint)(855638016L | (long)handle._rowId));
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0000F494 File Offset: 0x0000D694
		public static explicit operator LocalVariableHandle(Handle handle)
		{
			if (handle.VType != 51)
			{
				Throw.InvalidCast();
			}
			return new LocalVariableHandle(handle.RowId);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0000F4B2 File Offset: 0x0000D6B2
		public static explicit operator LocalVariableHandle(EntityHandle handle)
		{
			if (handle.VType != 855638016U)
			{
				Throw.InvalidCast();
			}
			return new LocalVariableHandle(handle.RowId);
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x0000F4D3 File Offset: 0x0000D6D3
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000F4DE File Offset: 0x0000D6DE
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0000F4E6 File Offset: 0x0000D6E6
		public static bool operator ==(LocalVariableHandle left, LocalVariableHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0000F4F6 File Offset: 0x0000D6F6
		public override bool Equals(object obj)
		{
			return obj is LocalVariableHandle && ((LocalVariableHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0000F4E6 File Offset: 0x0000D6E6
		public bool Equals(LocalVariableHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0000F518 File Offset: 0x0000D718
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0000F533 File Offset: 0x0000D733
		public static bool operator !=(LocalVariableHandle left, LocalVariableHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040003F0 RID: 1008
		private const uint tokenType = 855638016U;

		// Token: 0x040003F1 RID: 1009
		private const byte tokenTypeSmall = 51;

		// Token: 0x040003F2 RID: 1010
		private readonly int _rowId;
	}
}
