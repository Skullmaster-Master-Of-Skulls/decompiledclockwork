using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000077 RID: 119
	public struct ManifestResource
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0000AC4D File Offset: 0x00008E4D
		internal ManifestResource(MetadataReader reader, ManifestResourceHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x0000AC63 File Offset: 0x00008E63
		private ManifestResourceHandle Handle
		{
			get
			{
				return ManifestResourceHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000AC70 File Offset: 0x00008E70
		public long Offset
		{
			get
			{
				return (long)((ulong)this._reader.ManifestResourceTable.GetOffset(this.Handle));
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000AC89 File Offset: 0x00008E89
		public ManifestResourceAttributes Attributes
		{
			get
			{
				return this._reader.ManifestResourceTable.GetFlags(this.Handle);
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000ACA1 File Offset: 0x00008EA1
		public StringHandle Name
		{
			get
			{
				return this._reader.ManifestResourceTable.GetName(this.Handle);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000ACB9 File Offset: 0x00008EB9
		public EntityHandle Implementation
		{
			get
			{
				return this._reader.ManifestResourceTable.GetImplementation(this.Handle);
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000ACD1 File Offset: 0x00008ED1
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x04000348 RID: 840
		private readonly MetadataReader _reader;

		// Token: 0x04000349 RID: 841
		private readonly int _rowId;
	}
}
