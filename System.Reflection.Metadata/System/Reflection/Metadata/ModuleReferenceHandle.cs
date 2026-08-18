using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200006A RID: 106
	public struct ModuleReferenceHandle : IEquatable<ModuleReferenceHandle>
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0000A0AE File Offset: 0x000082AE
		private ModuleReferenceHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000A0B7 File Offset: 0x000082B7
		internal static ModuleReferenceHandle FromRowId(int rowId)
		{
			return new ModuleReferenceHandle(rowId);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0000A0BF File Offset: 0x000082BF
		public static implicit operator Handle(ModuleReferenceHandle handle)
		{
			return new Handle(26, handle._rowId);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0000A0CE File Offset: 0x000082CE
		public static implicit operator EntityHandle(ModuleReferenceHandle handle)
		{
			return new EntityHandle((uint)(436207616L | (long)handle._rowId));
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0000A0E4 File Offset: 0x000082E4
		public static explicit operator ModuleReferenceHandle(Handle handle)
		{
			if (handle.VType != 26)
			{
				Throw.InvalidCast();
			}
			return new ModuleReferenceHandle(handle.RowId);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000A102 File Offset: 0x00008302
		public static explicit operator ModuleReferenceHandle(EntityHandle handle)
		{
			if (handle.VType != 436207616U)
			{
				Throw.InvalidCast();
			}
			return new ModuleReferenceHandle(handle.RowId);
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000A123 File Offset: 0x00008323
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000A12E File Offset: 0x0000832E
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000A136 File Offset: 0x00008336
		public static bool operator ==(ModuleReferenceHandle left, ModuleReferenceHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000A146 File Offset: 0x00008346
		public override bool Equals(object obj)
		{
			return obj is ModuleReferenceHandle && ((ModuleReferenceHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0000A136 File Offset: 0x00008336
		public bool Equals(ModuleReferenceHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0000A168 File Offset: 0x00008368
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0000A183 File Offset: 0x00008383
		public static bool operator !=(ModuleReferenceHandle left, ModuleReferenceHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400032B RID: 811
		private const uint tokenType = 436207616U;

		// Token: 0x0400032C RID: 812
		private const byte tokenTypeSmall = 26;

		// Token: 0x0400032D RID: 813
		private readonly int _rowId;
	}
}
