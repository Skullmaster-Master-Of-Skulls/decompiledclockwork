using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200006D RID: 109
	public struct DeclarativeSecurityAttributeHandle : IEquatable<DeclarativeSecurityAttributeHandle>
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x0000A396 File Offset: 0x00008596
		private DeclarativeSecurityAttributeHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000A39F File Offset: 0x0000859F
		internal static DeclarativeSecurityAttributeHandle FromRowId(int rowId)
		{
			return new DeclarativeSecurityAttributeHandle(rowId);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000A3A7 File Offset: 0x000085A7
		public static implicit operator Handle(DeclarativeSecurityAttributeHandle handle)
		{
			return new Handle(14, handle._rowId);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000A3B6 File Offset: 0x000085B6
		public static implicit operator EntityHandle(DeclarativeSecurityAttributeHandle handle)
		{
			return new EntityHandle((uint)(234881024L | (long)handle._rowId));
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000A3CC File Offset: 0x000085CC
		public static explicit operator DeclarativeSecurityAttributeHandle(Handle handle)
		{
			if (handle.VType != 14)
			{
				Throw.InvalidCast();
			}
			return new DeclarativeSecurityAttributeHandle(handle.RowId);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000A3EA File Offset: 0x000085EA
		public static explicit operator DeclarativeSecurityAttributeHandle(EntityHandle handle)
		{
			if (handle.VType != 234881024U)
			{
				Throw.InvalidCast();
			}
			return new DeclarativeSecurityAttributeHandle(handle.RowId);
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000A40B File Offset: 0x0000860B
		public bool IsNil
		{
			get
			{
				return this._rowId == 0;
			}
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000A416 File Offset: 0x00008616
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000A41E File Offset: 0x0000861E
		public static bool operator ==(DeclarativeSecurityAttributeHandle left, DeclarativeSecurityAttributeHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000A42E File Offset: 0x0000862E
		public override bool Equals(object obj)
		{
			return obj is DeclarativeSecurityAttributeHandle && ((DeclarativeSecurityAttributeHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000A41E File Offset: 0x0000861E
		public bool Equals(DeclarativeSecurityAttributeHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000A450 File Offset: 0x00008650
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000A46B File Offset: 0x0000866B
		public static bool operator !=(DeclarativeSecurityAttributeHandle left, DeclarativeSecurityAttributeHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000334 RID: 820
		private const uint tokenType = 234881024U;

		// Token: 0x04000335 RID: 821
		private const byte tokenTypeSmall = 14;

		// Token: 0x04000336 RID: 822
		private readonly int _rowId;
	}
}
