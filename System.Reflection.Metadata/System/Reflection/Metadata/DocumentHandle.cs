using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000094 RID: 148
	public struct DocumentHandle : IEquatable<DocumentHandle>
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x0000F19C File Offset: 0x0000D39C
		private DocumentHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0000F1A5 File Offset: 0x0000D3A5
		internal static DocumentHandle FromRowId(int rowId)
		{
			return new DocumentHandle(rowId);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x0000F1AD File Offset: 0x0000D3AD
		public static implicit operator Handle(DocumentHandle handle)
		{
			return new Handle(48, handle._rowId);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0000F1BC File Offset: 0x0000D3BC
		public static implicit operator EntityHandle(DocumentHandle handle)
		{
			return new EntityHandle((uint)(805306368L | (long)handle._rowId));
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0000F1D2 File Offset: 0x0000D3D2
		public static explicit operator DocumentHandle(Handle handle)
		{
			if (handle.VType != 48)
			{
				Throw.InvalidCast();
			}
			return new DocumentHandle(handle.RowId);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x0000F1F0 File Offset: 0x0000D3F0
		public static explicit operator DocumentHandle(EntityHandle handle)
		{
			if (handle.VType != 805306368U)
			{
				Throw.InvalidCast();
			}
			return new DocumentHandle(handle.RowId);
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0000F211 File Offset: 0x0000D411
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0000F21C File Offset: 0x0000D41C
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000F224 File Offset: 0x0000D424
		public static bool operator ==(DocumentHandle left, DocumentHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0000F234 File Offset: 0x0000D434
		public override bool Equals(object obj)
		{
			return obj is DocumentHandle && ((DocumentHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0000F224 File Offset: 0x0000D424
		public bool Equals(DocumentHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0000F254 File Offset: 0x0000D454
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0000F26F File Offset: 0x0000D46F
		public static bool operator !=(DocumentHandle left, DocumentHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040003E7 RID: 999
		private const uint tokenType = 805306368U;

		// Token: 0x040003E8 RID: 1000
		private const byte tokenTypeSmall = 48;

		// Token: 0x040003E9 RID: 1001
		private readonly int _rowId;
	}
}
