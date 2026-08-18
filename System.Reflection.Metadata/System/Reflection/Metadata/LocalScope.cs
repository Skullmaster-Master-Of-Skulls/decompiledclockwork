using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A0 RID: 160
	public struct LocalScope
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0000F935 File Offset: 0x0000DB35
		internal LocalScope(MetadataReader reader, LocalScopeHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0000F94B File Offset: 0x0000DB4B
		private LocalScopeHandle Handle
		{
			get
			{
				return LocalScopeHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0000F958 File Offset: 0x0000DB58
		public MethodDefinitionHandle Method
		{
			get
			{
				return this._reader.LocalScopeTable.GetMethod(this._rowId);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0000F970 File Offset: 0x0000DB70
		public ImportScopeHandle ImportScope
		{
			get
			{
				return this._reader.LocalScopeTable.GetImportScope(this.Handle);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0000F988 File Offset: 0x0000DB88
		public int StartOffset
		{
			get
			{
				return this._reader.LocalScopeTable.GetStartOffset(this._rowId);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000F9A0 File Offset: 0x0000DBA0
		public int Length
		{
			get
			{
				return this._reader.LocalScopeTable.GetLength(this._rowId);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
		public int EndOffset
		{
			get
			{
				return this._reader.LocalScopeTable.GetEndOffset(this._rowId);
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
		public LocalVariableHandleCollection GetLocalVariables()
		{
			return new LocalVariableHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000F9E3 File Offset: 0x0000DBE3
		public LocalConstantHandleCollection GetLocalConstants()
		{
			return new LocalConstantHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000F9F6 File Offset: 0x0000DBF6
		public LocalScopeHandleCollection.ChildrenEnumerator GetChildren()
		{
			return new LocalScopeHandleCollection.ChildrenEnumerator(this._reader, this._rowId);
		}

		// Token: 0x0400040F RID: 1039
		private readonly MetadataReader _reader;

		// Token: 0x04000410 RID: 1040
		private readonly int _rowId;
	}
}
