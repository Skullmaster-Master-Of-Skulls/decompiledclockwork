using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000068 RID: 104
	public struct GenericParameterHandle : IEquatable<GenericParameterHandle>
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00009EDE File Offset: 0x000080DE
		private GenericParameterHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00009EE7 File Offset: 0x000080E7
		internal static GenericParameterHandle FromRowId(int rowId)
		{
			return new GenericParameterHandle(rowId);
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00009EEF File Offset: 0x000080EF
		public static implicit operator Handle(GenericParameterHandle handle)
		{
			return new Handle(42, handle._rowId);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00009EFE File Offset: 0x000080FE
		public static implicit operator EntityHandle(GenericParameterHandle handle)
		{
			return new EntityHandle((uint)(704643072L | (long)handle._rowId));
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00009F14 File Offset: 0x00008114
		public static explicit operator GenericParameterHandle(Handle handle)
		{
			if (handle.VType != 42)
			{
				Throw.InvalidCast();
			}
			return new GenericParameterHandle(handle.RowId);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00009F32 File Offset: 0x00008132
		public static explicit operator GenericParameterHandle(EntityHandle handle)
		{
			if (handle.VType != 704643072U)
			{
				Throw.InvalidCast();
			}
			return new GenericParameterHandle(handle.RowId);
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00009F53 File Offset: 0x00008153
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00009F5E File Offset: 0x0000815E
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00009F66 File Offset: 0x00008166
		public static bool operator ==(GenericParameterHandle left, GenericParameterHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00009F76 File Offset: 0x00008176
		public override bool Equals(object obj)
		{
			return obj is GenericParameterHandle && ((GenericParameterHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00009F66 File Offset: 0x00008166
		public bool Equals(GenericParameterHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00009F98 File Offset: 0x00008198
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00009FB3 File Offset: 0x000081B3
		public static bool operator !=(GenericParameterHandle left, GenericParameterHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000325 RID: 805
		private const uint tokenType = 704643072U;

		// Token: 0x04000326 RID: 806
		private const byte tokenTypeSmall = 42;

		// Token: 0x04000327 RID: 807
		private readonly int _rowId;
	}
}
