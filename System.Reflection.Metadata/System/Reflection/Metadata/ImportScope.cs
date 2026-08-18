using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200009F RID: 159
	public struct ImportScope
	{
		// Token: 0x060006D0 RID: 1744 RVA: 0x0000F8C5 File Offset: 0x0000DAC5
		internal ImportScope(MetadataReader reader, ImportScopeHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0000F8DB File Offset: 0x0000DADB
		private ImportScopeHandle Handle
		{
			get
			{
				return ImportScopeHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
		public ImportScopeHandle Parent
		{
			get
			{
				return this._reader.ImportScopeTable.GetParent(this.Handle);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0000F900 File Offset: 0x0000DB00
		public BlobHandle ImportsBlob
		{
			get
			{
				return this._reader.ImportScopeTable.GetImports(this.Handle);
			}
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0000F918 File Offset: 0x0000DB18
		public ImportDefinitionCollection GetImports()
		{
			return new ImportDefinitionCollection(this._reader.BlobStream.GetMemoryBlock(this.ImportsBlob));
		}

		// Token: 0x0400040D RID: 1037
		private readonly MetadataReader _reader;

		// Token: 0x0400040E RID: 1038
		private readonly int _rowId;
	}
}
