using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005E RID: 94
	internal struct Document
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000720E File Offset: 0x0000540E
		internal Document(MetadataReader reader, DocumentHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00007224 File Offset: 0x00005424
		private DocumentHandle Handle
		{
			get
			{
				return DocumentHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00007231 File Offset: 0x00005431
		public DocumentNameBlobHandle Name
		{
			get
			{
				return this._reader.DocumentTable.GetName(this.Handle);
			}
		}

		// Token: 0x04000349 RID: 841
		private readonly MetadataReader _reader;

		// Token: 0x0400034A RID: 842
		private readonly int _rowId;
	}
}
