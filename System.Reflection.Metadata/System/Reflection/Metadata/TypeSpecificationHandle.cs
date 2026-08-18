using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000061 RID: 97
	public struct TypeSpecificationHandle : IEquatable<TypeSpecificationHandle>
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000988E File Offset: 0x00007A8E
		private TypeSpecificationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00009897 File Offset: 0x00007A97
		internal static TypeSpecificationHandle FromRowId(int rowId)
		{
			return new TypeSpecificationHandle(rowId);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000989F File Offset: 0x00007A9F
		public static implicit operator Handle(TypeSpecificationHandle handle)
		{
			return new Handle(27, handle._rowId);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000098AE File Offset: 0x00007AAE
		public static implicit operator EntityHandle(TypeSpecificationHandle handle)
		{
			return new EntityHandle((uint)(452984832L | (long)handle._rowId));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000098C4 File Offset: 0x00007AC4
		public static explicit operator TypeSpecificationHandle(Handle handle)
		{
			if (handle.VType != 27)
			{
				Throw.InvalidCast();
			}
			return new TypeSpecificationHandle(handle.RowId);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000098E2 File Offset: 0x00007AE2
		public static explicit operator TypeSpecificationHandle(EntityHandle handle)
		{
			if (handle.VType != 452984832U)
			{
				Throw.InvalidCast();
			}
			return new TypeSpecificationHandle(handle.RowId);
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x00009903 File Offset: 0x00007B03
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000990E File Offset: 0x00007B0E
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00009916 File Offset: 0x00007B16
		public static bool operator ==(TypeSpecificationHandle left, TypeSpecificationHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00009926 File Offset: 0x00007B26
		public override bool Equals(object obj)
		{
			return obj is TypeSpecificationHandle && ((TypeSpecificationHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00009916 File Offset: 0x00007B16
		public bool Equals(TypeSpecificationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00009948 File Offset: 0x00007B48
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00009963 File Offset: 0x00007B63
		public static bool operator !=(TypeSpecificationHandle left, TypeSpecificationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000310 RID: 784
		private const uint tokenType = 452984832U;

		// Token: 0x04000311 RID: 785
		private const byte tokenTypeSmall = 27;

		// Token: 0x04000312 RID: 786
		private readonly int _rowId;
	}
}
