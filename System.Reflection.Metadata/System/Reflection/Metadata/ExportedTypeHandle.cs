using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005F RID: 95
	public struct ExportedTypeHandle : IEquatable<ExportedTypeHandle>
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x000096C2 File Offset: 0x000078C2
		private ExportedTypeHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000096CB File Offset: 0x000078CB
		internal static ExportedTypeHandle FromRowId(int rowId)
		{
			return new ExportedTypeHandle(rowId);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000096D3 File Offset: 0x000078D3
		public static implicit operator Handle(ExportedTypeHandle handle)
		{
			return new Handle(39, handle._rowId);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000096E2 File Offset: 0x000078E2
		public static implicit operator EntityHandle(ExportedTypeHandle handle)
		{
			return new EntityHandle((uint)(654311424L | (long)handle._rowId));
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000096F8 File Offset: 0x000078F8
		public static explicit operator ExportedTypeHandle(Handle handle)
		{
			if (handle.VType != 39)
			{
				Throw.InvalidCast();
			}
			return new ExportedTypeHandle(handle.RowId);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00009716 File Offset: 0x00007916
		public static explicit operator ExportedTypeHandle(EntityHandle handle)
		{
			if (handle.VType != 654311424U)
			{
				Throw.InvalidCast();
			}
			return new ExportedTypeHandle(handle.RowId);
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00009737 File Offset: 0x00007937
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00009742 File Offset: 0x00007942
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000974A File Offset: 0x0000794A
		public static bool operator ==(ExportedTypeHandle left, ExportedTypeHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000975A File Offset: 0x0000795A
		public override bool Equals(object obj)
		{
			return obj is ExportedTypeHandle && ((ExportedTypeHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000974A File Offset: 0x0000794A
		public bool Equals(ExportedTypeHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000977C File Offset: 0x0000797C
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00009797 File Offset: 0x00007997
		public static bool operator !=(ExportedTypeHandle left, ExportedTypeHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400030A RID: 778
		private const uint tokenType = 654311424U;

		// Token: 0x0400030B RID: 779
		private const byte tokenTypeSmall = 39;

		// Token: 0x0400030C RID: 780
		private readonly int _rowId;
	}
}
