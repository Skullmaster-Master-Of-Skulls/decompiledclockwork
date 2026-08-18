using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005D RID: 93
	public struct MethodSpecificationHandle : IEquatable<MethodSpecificationHandle>
	{
		// Token: 0x060003DE RID: 990 RVA: 0x000094F6 File Offset: 0x000076F6
		private MethodSpecificationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000094FF File Offset: 0x000076FF
		internal static MethodSpecificationHandle FromRowId(int rowId)
		{
			return new MethodSpecificationHandle(rowId);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00009507 File Offset: 0x00007707
		public static implicit operator Handle(MethodSpecificationHandle handle)
		{
			return new Handle(43, handle._rowId);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00009516 File Offset: 0x00007716
		public static implicit operator EntityHandle(MethodSpecificationHandle handle)
		{
			return new EntityHandle((uint)(721420288L | (long)handle._rowId));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000952C File Offset: 0x0000772C
		public static explicit operator MethodSpecificationHandle(Handle handle)
		{
			if (handle.VType != 43)
			{
				Throw.InvalidCast();
			}
			return new MethodSpecificationHandle(handle.RowId);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000954A File Offset: 0x0000774A
		public static explicit operator MethodSpecificationHandle(EntityHandle handle)
		{
			if (handle.VType != 721420288U)
			{
				Throw.InvalidCast();
			}
			return new MethodSpecificationHandle(handle.RowId);
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000956B File Offset: 0x0000776B
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00009576 File Offset: 0x00007776
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000957E File Offset: 0x0000777E
		public static bool operator ==(MethodSpecificationHandle left, MethodSpecificationHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000958E File Offset: 0x0000778E
		public override bool Equals(object obj)
		{
			return obj is MethodSpecificationHandle && ((MethodSpecificationHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000957E File Offset: 0x0000777E
		public bool Equals(MethodSpecificationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000095B0 File Offset: 0x000077B0
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x000095CB File Offset: 0x000077CB
		public static bool operator !=(MethodSpecificationHandle left, MethodSpecificationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000304 RID: 772
		private const uint tokenType = 721420288U;

		// Token: 0x04000305 RID: 773
		private const byte tokenTypeSmall = 43;

		// Token: 0x04000306 RID: 774
		private readonly int _rowId;
	}
}
