using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000060 RID: 96
	public struct TypeReferenceHandle : IEquatable<TypeReferenceHandle>
	{
		// Token: 0x06000405 RID: 1029 RVA: 0x000097AA File Offset: 0x000079AA
		private TypeReferenceHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000097B3 File Offset: 0x000079B3
		internal static TypeReferenceHandle FromRowId(int rowId)
		{
			return new TypeReferenceHandle(rowId);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000097BB File Offset: 0x000079BB
		public static implicit operator Handle(TypeReferenceHandle handle)
		{
			return new Handle(1, handle._rowId);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000097C9 File Offset: 0x000079C9
		public static implicit operator EntityHandle(TypeReferenceHandle handle)
		{
			return new EntityHandle((uint)(16777216L | (long)handle._rowId));
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000097DF File Offset: 0x000079DF
		public static explicit operator TypeReferenceHandle(Handle handle)
		{
			if (handle.VType != 1)
			{
				Throw.InvalidCast();
			}
			return new TypeReferenceHandle(handle.RowId);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000097FC File Offset: 0x000079FC
		public static explicit operator TypeReferenceHandle(EntityHandle handle)
		{
			if (handle.VType != 16777216U)
			{
				Throw.InvalidCast();
			}
			return new TypeReferenceHandle(handle.RowId);
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000981D File Offset: 0x00007A1D
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00009828 File Offset: 0x00007A28
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00009830 File Offset: 0x00007A30
		public static bool operator ==(TypeReferenceHandle left, TypeReferenceHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00009840 File Offset: 0x00007A40
		public override bool Equals(object obj)
		{
			return obj is TypeReferenceHandle && ((TypeReferenceHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00009830 File Offset: 0x00007A30
		public bool Equals(TypeReferenceHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00009860 File Offset: 0x00007A60
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000987B File Offset: 0x00007A7B
		public static bool operator !=(TypeReferenceHandle left, TypeReferenceHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400030D RID: 781
		private const uint tokenType = 16777216U;

		// Token: 0x0400030E RID: 782
		private const byte tokenTypeSmall = 1;

		// Token: 0x0400030F RID: 783
		private readonly int _rowId;
	}
}
