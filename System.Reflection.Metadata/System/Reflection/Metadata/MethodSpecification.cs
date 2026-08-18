using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000084 RID: 132
	public struct MethodSpecification
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x0000EAB9 File Offset: 0x0000CCB9
		internal MethodSpecification(MetadataReader reader, MethodSpecificationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x0000EACF File Offset: 0x0000CCCF
		private MethodSpecificationHandle Handle
		{
			get
			{
				return MethodSpecificationHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000EADC File Offset: 0x0000CCDC
		public EntityHandle Method
		{
			get
			{
				return this._reader.MethodSpecTable.GetMethod(this.Handle);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x0000EAF4 File Offset: 0x0000CCF4
		public BlobHandle Signature
		{
			get
			{
				return this._reader.MethodSpecTable.GetInstantiation(this.Handle);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x040003C3 RID: 963
		private readonly MetadataReader _reader;

		// Token: 0x040003C4 RID: 964
		private readonly int _rowId;
	}
}
