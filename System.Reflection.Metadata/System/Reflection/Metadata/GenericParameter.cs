using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200003D RID: 61
	public struct GenericParameter
	{
		// Token: 0x060002F4 RID: 756 RVA: 0x000085FD File Offset: 0x000067FD
		internal GenericParameter(MetadataReader reader, GenericParameterHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00008613 File Offset: 0x00006813
		private GenericParameterHandle Handle
		{
			get
			{
				return GenericParameterHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00008620 File Offset: 0x00006820
		public EntityHandle Parent
		{
			get
			{
				return this._reader.GenericParamTable.GetOwner(this.Handle);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00008638 File Offset: 0x00006838
		public GenericParameterAttributes Attributes
		{
			get
			{
				return this._reader.GenericParamTable.GetFlags(this.Handle);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00008650 File Offset: 0x00006850
		public int Index
		{
			get
			{
				return (int)this._reader.GenericParamTable.GetNumber(this.Handle);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00008668 File Offset: 0x00006868
		public StringHandle Name
		{
			get
			{
				return this._reader.GenericParamTable.GetName(this.Handle);
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00008680 File Offset: 0x00006880
		public GenericParameterConstraintHandleCollection GetConstraints()
		{
			return this._reader.GenericParamConstraintTable.FindConstraintsForGenericParam(this.Handle);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00008698 File Offset: 0x00006898
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x04000299 RID: 665
		private readonly MetadataReader _reader;

		// Token: 0x0400029A RID: 666
		private readonly int _rowId;
	}
}
