using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000082 RID: 130
	public struct MethodImplementation
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x0000EA07 File Offset: 0x0000CC07
		internal MethodImplementation(MetadataReader reader, MethodImplementationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000EA1D File Offset: 0x0000CC1D
		private MethodImplementationHandle Handle
		{
			get
			{
				return MethodImplementationHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000EA2A File Offset: 0x0000CC2A
		public TypeDefinitionHandle Type
		{
			get
			{
				return this._reader.MethodImplTable.GetClass(this.Handle);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000EA42 File Offset: 0x0000CC42
		public EntityHandle MethodBody
		{
			get
			{
				return this._reader.MethodImplTable.GetMethodBody(this.Handle);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000EA5A File Offset: 0x0000CC5A
		public EntityHandle MethodDeclaration
		{
			get
			{
				return this._reader.MethodImplTable.GetMethodDeclaration(this.Handle);
			}
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000EA72 File Offset: 0x0000CC72
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x040003BE RID: 958
		private readonly MetadataReader _reader;

		// Token: 0x040003BF RID: 959
		private readonly int _rowId;
	}
}
