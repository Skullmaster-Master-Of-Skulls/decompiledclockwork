using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000086 RID: 134
	public struct ModuleReference
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x0000EB87 File Offset: 0x0000CD87
		internal ModuleReference(MetadataReader reader, ModuleReferenceHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000EB9D File Offset: 0x0000CD9D
		private ModuleReferenceHandle Handle
		{
			get
			{
				return ModuleReferenceHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000EBAA File Offset: 0x0000CDAA
		public StringHandle Name
		{
			get
			{
				return this._reader.ModuleRefTable.GetName(this.Handle);
			}
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0000EBC2 File Offset: 0x0000CDC2
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x040003C6 RID: 966
		private readonly MetadataReader _reader;

		// Token: 0x040003C7 RID: 967
		private readonly int _rowId;
	}
}
