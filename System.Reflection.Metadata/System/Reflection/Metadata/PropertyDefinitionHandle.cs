using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000065 RID: 101
	public struct PropertyDefinitionHandle : IEquatable<PropertyDefinitionHandle>
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x00009C2A File Offset: 0x00007E2A
		private PropertyDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00009C33 File Offset: 0x00007E33
		internal static PropertyDefinitionHandle FromRowId(int rowId)
		{
			return new PropertyDefinitionHandle(rowId);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00009C3B File Offset: 0x00007E3B
		public static implicit operator Handle(PropertyDefinitionHandle handle)
		{
			return new Handle(23, handle._rowId);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00009C4A File Offset: 0x00007E4A
		public static implicit operator EntityHandle(PropertyDefinitionHandle handle)
		{
			return new EntityHandle((uint)(385875968L | (long)handle._rowId));
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00009C60 File Offset: 0x00007E60
		public static explicit operator PropertyDefinitionHandle(Handle handle)
		{
			if (handle.VType != 23)
			{
				Throw.InvalidCast();
			}
			return new PropertyDefinitionHandle(handle.RowId);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00009C7E File Offset: 0x00007E7E
		public static explicit operator PropertyDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 385875968U)
			{
				Throw.InvalidCast();
			}
			return new PropertyDefinitionHandle(handle.RowId);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00009C9F File Offset: 0x00007E9F
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00009CAA File Offset: 0x00007EAA
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00009CB2 File Offset: 0x00007EB2
		public static bool operator ==(PropertyDefinitionHandle left, PropertyDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00009CC2 File Offset: 0x00007EC2
		public override bool Equals(object obj)
		{
			return obj is PropertyDefinitionHandle && ((PropertyDefinitionHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00009CB2 File Offset: 0x00007EB2
		public bool Equals(PropertyDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00009CE4 File Offset: 0x00007EE4
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00009CFF File Offset: 0x00007EFF
		public static bool operator !=(PropertyDefinitionHandle left, PropertyDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400031C RID: 796
		private const uint tokenType = 385875968U;

		// Token: 0x0400031D RID: 797
		private const byte tokenTypeSmall = 23;

		// Token: 0x0400031E RID: 798
		private readonly int _rowId;
	}
}
