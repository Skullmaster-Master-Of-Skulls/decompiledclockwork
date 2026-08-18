using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005A RID: 90
	public struct InterfaceImplementationHandle : IEquatable<InterfaceImplementationHandle>
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x00009236 File Offset: 0x00007436
		internal InterfaceImplementationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000923F File Offset: 0x0000743F
		internal static InterfaceImplementationHandle FromRowId(int rowId)
		{
			return new InterfaceImplementationHandle(rowId);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00009247 File Offset: 0x00007447
		public static implicit operator Handle(InterfaceImplementationHandle handle)
		{
			return new Handle(9, handle._rowId);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00009256 File Offset: 0x00007456
		public static implicit operator EntityHandle(InterfaceImplementationHandle handle)
		{
			return new EntityHandle((uint)(150994944L | (long)handle._rowId));
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000926C File Offset: 0x0000746C
		public static explicit operator InterfaceImplementationHandle(Handle handle)
		{
			if (handle.VType != 9)
			{
				Throw.InvalidCast();
			}
			return new InterfaceImplementationHandle(handle.RowId);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000928A File Offset: 0x0000748A
		public static explicit operator InterfaceImplementationHandle(EntityHandle handle)
		{
			if (handle.VType != 150994944U)
			{
				Throw.InvalidCast();
			}
			return new InterfaceImplementationHandle(handle.RowId);
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000092AB File Offset: 0x000074AB
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003BD RID: 957 RVA: 0x000092B6 File Offset: 0x000074B6
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000092BE File Offset: 0x000074BE
		public static bool operator ==(InterfaceImplementationHandle left, InterfaceImplementationHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000092CE File Offset: 0x000074CE
		public override bool Equals(object obj)
		{
			return obj is InterfaceImplementationHandle && ((InterfaceImplementationHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000092BE File Offset: 0x000074BE
		public bool Equals(InterfaceImplementationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000092F0 File Offset: 0x000074F0
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000930B File Offset: 0x0000750B
		public static bool operator !=(InterfaceImplementationHandle left, InterfaceImplementationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002FB RID: 763
		private const uint tokenType = 150994944U;

		// Token: 0x040002FC RID: 764
		private const byte tokenTypeSmall = 9;

		// Token: 0x040002FD RID: 765
		private readonly int _rowId;
	}
}
