using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000096 RID: 150
	public struct LocalScopeHandle : IEquatable<LocalScopeHandle>
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x0000F377 File Offset: 0x0000D577
		private LocalScopeHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0000F380 File Offset: 0x0000D580
		internal static LocalScopeHandle FromRowId(int rowId)
		{
			return new LocalScopeHandle(rowId);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000F388 File Offset: 0x0000D588
		public static implicit operator Handle(LocalScopeHandle handle)
		{
			return new Handle(50, handle._rowId);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0000F397 File Offset: 0x0000D597
		public static implicit operator EntityHandle(LocalScopeHandle handle)
		{
			return new EntityHandle((uint)(838860800L | (long)handle._rowId));
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0000F3AD File Offset: 0x0000D5AD
		public static explicit operator LocalScopeHandle(Handle handle)
		{
			if (handle.VType != 50)
			{
				Throw.InvalidCast();
			}
			return new LocalScopeHandle(handle.RowId);
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0000F3CB File Offset: 0x0000D5CB
		public static explicit operator LocalScopeHandle(EntityHandle handle)
		{
			if (handle.VType != 838860800U)
			{
				Throw.InvalidCast();
			}
			return new LocalScopeHandle(handle.RowId);
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x0000F3F7 File Offset: 0x0000D5F7
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0000F3FF File Offset: 0x0000D5FF
		public static bool operator ==(LocalScopeHandle left, LocalScopeHandle right)
		{
			return left._rowId == right._rowId;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000F40F File Offset: 0x0000D60F
		public override bool Equals(object obj)
		{
			return obj is LocalScopeHandle && ((LocalScopeHandle)obj)._rowId == this._rowId;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0000F3FF File Offset: 0x0000D5FF
		public bool Equals(LocalScopeHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0000F430 File Offset: 0x0000D630
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0000F44B File Offset: 0x0000D64B
		public static bool operator !=(LocalScopeHandle left, LocalScopeHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040003ED RID: 1005
		private const uint tokenType = 838860800U;

		// Token: 0x040003EE RID: 1006
		private const byte tokenTypeSmall = 50;

		// Token: 0x040003EF RID: 1007
		private readonly int _rowId;
	}
}
