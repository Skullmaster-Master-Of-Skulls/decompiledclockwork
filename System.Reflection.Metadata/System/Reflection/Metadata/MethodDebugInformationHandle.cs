using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000095 RID: 149
	public struct MethodDebugInformationHandle : IEquatable<MethodDebugInformationHandle>
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0000F282 File Offset: 0x0000D482
		private MethodDebugInformationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0000F28B File Offset: 0x0000D48B
		internal static MethodDebugInformationHandle FromRowId(int rowId)
		{
			return new MethodDebugInformationHandle(rowId);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000F293 File Offset: 0x0000D493
		public static implicit operator Handle(MethodDebugInformationHandle handle)
		{
			return new Handle(49, handle._rowId);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000F2A2 File Offset: 0x0000D4A2
		public static implicit operator EntityHandle(MethodDebugInformationHandle handle)
		{
			return new EntityHandle((uint)(822083584L | (long)handle._rowId));
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000F2B8 File Offset: 0x0000D4B8
		public static explicit operator MethodDebugInformationHandle(Handle handle)
		{
			if (handle.VType != 49)
			{
				Throw.InvalidCast();
			}
			return new MethodDebugInformationHandle(handle.RowId);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000F2D6 File Offset: 0x0000D4D6
		public static explicit operator MethodDebugInformationHandle(EntityHandle handle)
		{
			if (handle.VType != 822083584U)
			{
				Throw.InvalidCast();
			}
			return new MethodDebugInformationHandle(handle.RowId);
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0000F2F7 File Offset: 0x0000D4F7
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000F302 File Offset: 0x0000D502
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0000F30A File Offset: 0x0000D50A
		public static bool operator ==(MethodDebugInformationHandle left, MethodDebugInformationHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0000F31A File Offset: 0x0000D51A
		public override bool Equals(object obj)
		{
			return obj is MethodDebugInformationHandle && ((MethodDebugInformationHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0000F30A File Offset: 0x0000D50A
		public bool Equals(MethodDebugInformationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0000F33C File Offset: 0x0000D53C
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0000F357 File Offset: 0x0000D557
		public static bool operator !=(MethodDebugInformationHandle left, MethodDebugInformationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000F36A File Offset: 0x0000D56A
		public MethodDefinitionHandle ToDefinitionHandle()
		{
			return MethodDefinitionHandle.FromRowId(this._rowId);
		}

		// Token: 0x040003EA RID: 1002
		private const uint tokenType = 822083584U;

		// Token: 0x040003EB RID: 1003
		private const byte tokenTypeSmall = 49;

		// Token: 0x040003EC RID: 1004
		private readonly int _rowId;
	}
}
