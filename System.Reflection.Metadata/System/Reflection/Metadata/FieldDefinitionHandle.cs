using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000063 RID: 99
	public struct FieldDefinitionHandle : IEquatable<FieldDefinitionHandle>
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x00009A5E File Offset: 0x00007C5E
		private FieldDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00009A67 File Offset: 0x00007C67
		internal static FieldDefinitionHandle FromRowId(int rowId)
		{
			return new FieldDefinitionHandle(rowId);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00009A6F File Offset: 0x00007C6F
		public static implicit operator Handle(FieldDefinitionHandle handle)
		{
			return new Handle(4, handle._rowId);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00009A7D File Offset: 0x00007C7D
		public static implicit operator EntityHandle(FieldDefinitionHandle handle)
		{
			return new EntityHandle((uint)(67108864L | (long)handle._rowId));
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00009A93 File Offset: 0x00007C93
		public static explicit operator FieldDefinitionHandle(Handle handle)
		{
			if (handle.VType != 4)
			{
				Throw.InvalidCast();
			}
			return new FieldDefinitionHandle(handle.RowId);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00009AB0 File Offset: 0x00007CB0
		public static explicit operator FieldDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 67108864U)
			{
				Throw.InvalidCast();
			}
			return new FieldDefinitionHandle(handle.RowId);
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00009AD1 File Offset: 0x00007CD1
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x00009ADC File Offset: 0x00007CDC
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00009AE4 File Offset: 0x00007CE4
		public static bool operator ==(FieldDefinitionHandle left, FieldDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00009AF4 File Offset: 0x00007CF4
		public override bool Equals(object obj)
		{
			return obj is FieldDefinitionHandle && ((FieldDefinitionHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00009AE4 File Offset: 0x00007CE4
		public bool Equals(FieldDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00009B14 File Offset: 0x00007D14
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00009B2F File Offset: 0x00007D2F
		public static bool operator !=(FieldDefinitionHandle left, FieldDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000316 RID: 790
		private const uint tokenType = 67108864U;

		// Token: 0x04000317 RID: 791
		private const byte tokenTypeSmall = 4;

		// Token: 0x04000318 RID: 792
		private readonly int _rowId;
	}
}
