using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000088 RID: 136
	public struct Parameter
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x0000EC24 File Offset: 0x0000CE24
		internal Parameter(MetadataReader reader, ParameterHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0000EC3A File Offset: 0x0000CE3A
		private ParameterHandle Handle
		{
			get
			{
				return ParameterHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0000EC47 File Offset: 0x0000CE47
		public ParameterAttributes Attributes
		{
			get
			{
				return this._reader.ParamTable.GetFlags(this.Handle);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000EC5F File Offset: 0x0000CE5F
		public int SequenceNumber
		{
			get
			{
				return (int)this._reader.ParamTable.GetSequence(this.Handle);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x0000EC77 File Offset: 0x0000CE77
		public StringHandle Name
		{
			get
			{
				return this._reader.ParamTable.GetName(this.Handle);
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0000EC8F File Offset: 0x0000CE8F
		public ConstantHandle GetDefaultValue()
		{
			return this._reader.ConstantTable.FindConstant(this.Handle);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		public BlobHandle GetMarshallingDescriptor()
		{
			int num = this._reader.FieldMarshalTable.FindFieldMarshalRowId(this.Handle);
			if (num == 0)
			{
				return default(BlobHandle);
			}
			return this._reader.FieldMarshalTable.GetNativeType(num);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0000ECF3 File Offset: 0x0000CEF3
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x040003C9 RID: 969
		private readonly MetadataReader _reader;

		// Token: 0x040003CA RID: 970
		private readonly int _rowId;
	}
}
