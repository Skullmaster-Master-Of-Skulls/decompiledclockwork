using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005C RID: 92
	public struct MethodImplementationHandle : IEquatable<MethodImplementationHandle>
	{
		// Token: 0x060003D1 RID: 977 RVA: 0x0000940F File Offset: 0x0000760F
		private MethodImplementationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00009418 File Offset: 0x00007618
		internal static MethodImplementationHandle FromRowId(int rowId)
		{
			return new MethodImplementationHandle(rowId);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00009420 File Offset: 0x00007620
		public static implicit operator Handle(MethodImplementationHandle handle)
		{
			return new Handle(25, handle._rowId);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000942F File Offset: 0x0000762F
		public static implicit operator EntityHandle(MethodImplementationHandle handle)
		{
			return new EntityHandle((uint)(419430400L | (long)handle._rowId));
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00009445 File Offset: 0x00007645
		public static explicit operator MethodImplementationHandle(Handle handle)
		{
			if (handle.VType != 25)
			{
				Throw.InvalidCast();
			}
			return new MethodImplementationHandle(handle.RowId);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00009463 File Offset: 0x00007663
		public static explicit operator MethodImplementationHandle(EntityHandle handle)
		{
			if (handle.VType != 419430400U)
			{
				Throw.InvalidCast();
			}
			return new MethodImplementationHandle(handle.RowId);
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00009484 File Offset: 0x00007684
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000948F File Offset: 0x0000768F
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00009497 File Offset: 0x00007697
		public static bool operator ==(MethodImplementationHandle left, MethodImplementationHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000094A7 File Offset: 0x000076A7
		public override bool Equals(object obj)
		{
			return obj is MethodImplementationHandle && ((MethodImplementationHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00009497 File Offset: 0x00007697
		public bool Equals(MethodImplementationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000094C8 File Offset: 0x000076C8
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000094E3 File Offset: 0x000076E3
		public static bool operator !=(MethodImplementationHandle left, MethodImplementationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000301 RID: 769
		private const uint tokenType = 419430400U;

		// Token: 0x04000302 RID: 770
		private const byte tokenTypeSmall = 25;

		// Token: 0x04000303 RID: 771
		private readonly int _rowId;
	}
}
