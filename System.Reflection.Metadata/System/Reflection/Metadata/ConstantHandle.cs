using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200006E RID: 110
	public struct ConstantHandle : IEquatable<ConstantHandle>
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x0000A47E File Offset: 0x0000867E
		private ConstantHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000A487 File Offset: 0x00008687
		internal static ConstantHandle FromRowId(int rowId)
		{
			return new ConstantHandle(rowId);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000A48F File Offset: 0x0000868F
		public static implicit operator Handle(ConstantHandle handle)
		{
			return new Handle(11, handle._rowId);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000A49E File Offset: 0x0000869E
		public static implicit operator EntityHandle(ConstantHandle handle)
		{
			return new EntityHandle((uint)(184549376L | (long)handle._rowId));
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000A4B4 File Offset: 0x000086B4
		public static explicit operator ConstantHandle(Handle handle)
		{
			if (handle.VType != 11)
			{
				Throw.InvalidCast();
			}
			return new ConstantHandle(handle.RowId);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000A4D2 File Offset: 0x000086D2
		public static explicit operator ConstantHandle(EntityHandle handle)
		{
			if (handle.VType != 184549376U)
			{
				Throw.InvalidCast();
			}
			return new ConstantHandle(handle.RowId);
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000A4F3 File Offset: 0x000086F3
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0000A4FE File Offset: 0x000086FE
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000A506 File Offset: 0x00008706
		public static bool operator ==(ConstantHandle left, ConstantHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000A516 File Offset: 0x00008716
		public override bool Equals(object obj)
		{
			return obj is ConstantHandle && ((ConstantHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000A506 File Offset: 0x00008706
		public bool Equals(ConstantHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000A538 File Offset: 0x00008738
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000A553 File Offset: 0x00008753
		public static bool operator !=(ConstantHandle left, ConstantHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000337 RID: 823
		private const uint tokenType = 184549376U;

		// Token: 0x04000338 RID: 824
		private const byte tokenTypeSmall = 11;

		// Token: 0x04000339 RID: 825
		private readonly int _rowId;
	}
}
