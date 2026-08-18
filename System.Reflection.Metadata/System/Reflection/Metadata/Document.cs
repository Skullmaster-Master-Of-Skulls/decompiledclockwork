using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200008B RID: 139
	public struct Document
	{
		// Token: 0x06000632 RID: 1586 RVA: 0x0000EDAE File Offset: 0x0000CFAE
		internal Document(MetadataReader reader, DocumentHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
		private DocumentHandle Handle
		{
			get
			{
				return DocumentHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0000EDD1 File Offset: 0x0000CFD1
		public DocumentNameBlobHandle Name
		{
			get
			{
				return this._reader.DocumentTable.GetName(this.Handle);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0000EDE9 File Offset: 0x0000CFE9
		public GuidHandle Language
		{
			get
			{
				return this._reader.DocumentTable.GetLanguage(this.Handle);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000EE01 File Offset: 0x0000D001
		public GuidHandle HashAlgorithm
		{
			get
			{
				return this._reader.DocumentTable.GetHashAlgorithm(this.Handle);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0000EE19 File Offset: 0x0000D019
		public BlobHandle Hash
		{
			get
			{
				return this._reader.DocumentTable.GetHash(this.Handle);
			}
		}

		// Token: 0x040003CF RID: 975
		private readonly MetadataReader _reader;

		// Token: 0x040003D0 RID: 976
		private readonly int _rowId;
	}
}
