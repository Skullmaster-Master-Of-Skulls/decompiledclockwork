using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000030 RID: 48
	public struct AssemblyFile
	{
		// Token: 0x0600026D RID: 621 RVA: 0x000073CF File Offset: 0x000055CF
		internal AssemblyFile(MetadataReader reader, AssemblyFileHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000073E5 File Offset: 0x000055E5
		private AssemblyFileHandle Handle
		{
			get
			{
				return AssemblyFileHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000073F2 File Offset: 0x000055F2
		public bool ContainsMetadata
		{
			get
			{
				return this._reader.FileTable.GetFlags(this.Handle) == 0U;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000740D File Offset: 0x0000560D
		public StringHandle Name
		{
			get
			{
				return this._reader.FileTable.GetName(this.Handle);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007425 File Offset: 0x00005625
		public BlobHandle HashValue
		{
			get
			{
				return this._reader.FileTable.GetHashValue(this.Handle);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000743D File Offset: 0x0000563D
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x04000264 RID: 612
		private readonly MetadataReader _reader;

		// Token: 0x04000265 RID: 613
		private readonly int _rowId;
	}
}
