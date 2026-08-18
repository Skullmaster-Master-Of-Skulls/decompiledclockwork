using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200006C RID: 108
	public struct CustomAttributeHandle : IEquatable<CustomAttributeHandle>
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000A2AE File Offset: 0x000084AE
		private CustomAttributeHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000A2B7 File Offset: 0x000084B7
		internal static CustomAttributeHandle FromRowId(int rowId)
		{
			return new CustomAttributeHandle(rowId);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000A2BF File Offset: 0x000084BF
		public static implicit operator Handle(CustomAttributeHandle handle)
		{
			return new Handle(12, handle._rowId);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000A2CE File Offset: 0x000084CE
		public static implicit operator EntityHandle(CustomAttributeHandle handle)
		{
			return new EntityHandle((uint)(201326592L | (long)handle._rowId));
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000A2E4 File Offset: 0x000084E4
		public static explicit operator CustomAttributeHandle(Handle handle)
		{
			if (handle.VType != 12)
			{
				Throw.InvalidCast();
			}
			return new CustomAttributeHandle(handle.RowId);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000A302 File Offset: 0x00008502
		public static explicit operator CustomAttributeHandle(EntityHandle handle)
		{
			if (handle.VType != 201326592U)
			{
				Throw.InvalidCast();
			}
			return new CustomAttributeHandle(handle.RowId);
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000A323 File Offset: 0x00008523
		public bool IsNil
		{
			get
			{
				return this._rowId == 0;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000A32E File Offset: 0x0000852E
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0000A336 File Offset: 0x00008536
		public static bool operator ==(CustomAttributeHandle left, CustomAttributeHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000A346 File Offset: 0x00008546
		public override bool Equals(object obj)
		{
			return obj is CustomAttributeHandle && ((CustomAttributeHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000A336 File Offset: 0x00008536
		public bool Equals(CustomAttributeHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000A368 File Offset: 0x00008568
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000A383 File Offset: 0x00008583
		public static bool operator !=(CustomAttributeHandle left, CustomAttributeHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000331 RID: 817
		private const uint tokenType = 201326592U;

		// Token: 0x04000332 RID: 818
		private const byte tokenTypeSmall = 12;

		// Token: 0x04000333 RID: 819
		private readonly int _rowId;
	}
}
