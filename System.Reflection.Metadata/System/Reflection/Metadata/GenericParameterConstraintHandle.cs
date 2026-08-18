using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000069 RID: 105
	public struct GenericParameterConstraintHandle : IEquatable<GenericParameterConstraintHandle>
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00009FC6 File Offset: 0x000081C6
		private GenericParameterConstraintHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00009FCF File Offset: 0x000081CF
		internal static GenericParameterConstraintHandle FromRowId(int rowId)
		{
			return new GenericParameterConstraintHandle(rowId);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00009FD7 File Offset: 0x000081D7
		public static implicit operator Handle(GenericParameterConstraintHandle handle)
		{
			return new Handle(44, handle._rowId);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00009FE6 File Offset: 0x000081E6
		public static implicit operator EntityHandle(GenericParameterConstraintHandle handle)
		{
			return new EntityHandle((uint)(738197504L | (long)handle._rowId));
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00009FFC File Offset: 0x000081FC
		public static explicit operator GenericParameterConstraintHandle(Handle handle)
		{
			if (handle.VType != 44)
			{
				Throw.InvalidCast();
			}
			return new GenericParameterConstraintHandle(handle.RowId);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000A01A File Offset: 0x0000821A
		public static explicit operator GenericParameterConstraintHandle(EntityHandle handle)
		{
			if (handle.VType != 738197504U)
			{
				Throw.InvalidCast();
			}
			return new GenericParameterConstraintHandle(handle.RowId);
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000A03B File Offset: 0x0000823B
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000A046 File Offset: 0x00008246
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000A04E File Offset: 0x0000824E
		public static bool operator ==(GenericParameterConstraintHandle left, GenericParameterConstraintHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000A05E File Offset: 0x0000825E
		public override bool Equals(object obj)
		{
			return obj is GenericParameterConstraintHandle && ((GenericParameterConstraintHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000A04E File Offset: 0x0000824E
		public bool Equals(GenericParameterConstraintHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000A080 File Offset: 0x00008280
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000A09B File Offset: 0x0000829B
		public static bool operator !=(GenericParameterConstraintHandle left, GenericParameterConstraintHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000328 RID: 808
		private const uint tokenType = 738197504U;

		// Token: 0x04000329 RID: 809
		private const byte tokenTypeSmall = 44;

		// Token: 0x0400032A RID: 810
		private readonly int _rowId;
	}
}
