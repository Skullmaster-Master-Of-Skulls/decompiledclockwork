using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005E RID: 94
	public struct TypeDefinitionHandle : IEquatable<TypeDefinitionHandle>
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x000095DE File Offset: 0x000077DE
		private TypeDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000095E7 File Offset: 0x000077E7
		internal static TypeDefinitionHandle FromRowId(int rowId)
		{
			return new TypeDefinitionHandle(rowId);
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000095EF File Offset: 0x000077EF
		public static implicit operator Handle(TypeDefinitionHandle handle)
		{
			return new Handle(2, handle._rowId);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000095FD File Offset: 0x000077FD
		public static implicit operator EntityHandle(TypeDefinitionHandle handle)
		{
			return new EntityHandle((uint)(33554432L | (long)handle._rowId));
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00009613 File Offset: 0x00007813
		public static explicit operator TypeDefinitionHandle(Handle handle)
		{
			if (handle.VType != 2)
			{
				Throw.InvalidCast();
			}
			return new TypeDefinitionHandle(handle.RowId);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00009630 File Offset: 0x00007830
		public static explicit operator TypeDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 33554432U)
			{
				Throw.InvalidCast();
			}
			return new TypeDefinitionHandle(handle.RowId);
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00009651 File Offset: 0x00007851
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000965C File Offset: 0x0000785C
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00009664 File Offset: 0x00007864
		public static bool operator ==(TypeDefinitionHandle left, TypeDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00009674 File Offset: 0x00007874
		public override bool Equals(object obj)
		{
			return obj is TypeDefinitionHandle && ((TypeDefinitionHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00009664 File Offset: 0x00007864
		public bool Equals(TypeDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00009694 File Offset: 0x00007894
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000096AF File Offset: 0x000078AF
		public static bool operator !=(TypeDefinitionHandle left, TypeDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000307 RID: 775
		private const uint tokenType = 33554432U;

		// Token: 0x04000308 RID: 776
		private const byte tokenTypeSmall = 2;

		// Token: 0x04000309 RID: 777
		private readonly int _rowId;
	}
}
