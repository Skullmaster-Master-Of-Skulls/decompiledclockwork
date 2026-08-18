using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200009E RID: 158
	public struct LocalConstant
	{
		// Token: 0x060006CC RID: 1740 RVA: 0x0000F872 File Offset: 0x0000DA72
		internal LocalConstant(MetadataReader reader, LocalConstantHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0000F888 File Offset: 0x0000DA88
		private LocalConstantHandle Handle
		{
			get
			{
				return LocalConstantHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0000F895 File Offset: 0x0000DA95
		public StringHandle Name
		{
			get
			{
				return this._reader.LocalConstantTable.GetName(this.Handle);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0000F8AD File Offset: 0x0000DAAD
		public BlobHandle Signature
		{
			get
			{
				return this._reader.LocalConstantTable.GetSignature(this.Handle);
			}
		}

		// Token: 0x0400040B RID: 1035
		private readonly MetadataReader _reader;

		// Token: 0x0400040C RID: 1036
		private readonly int _rowId;
	}
}
