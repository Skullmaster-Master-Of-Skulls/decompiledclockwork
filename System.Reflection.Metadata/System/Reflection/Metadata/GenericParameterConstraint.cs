using System;

namespace System.Reflection.Metadata
{
	// Token: 0x0200003E RID: 62
	public struct GenericParameterConstraint
	{
		// Token: 0x060002FC RID: 764 RVA: 0x000086B0 File Offset: 0x000068B0
		internal GenericParameterConstraint(MetadataReader reader, GenericParameterConstraintHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000086C6 File Offset: 0x000068C6
		private GenericParameterConstraintHandle Handle
		{
			get
			{
				return GenericParameterConstraintHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000086D3 File Offset: 0x000068D3
		public GenericParameterHandle Parameter
		{
			get
			{
				return this._reader.GenericParamConstraintTable.GetOwner(this.Handle);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000086EB File Offset: 0x000068EB
		public EntityHandle Type
		{
			get
			{
				return this._reader.GenericParamConstraintTable.GetConstraint(this.Handle);
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00008703 File Offset: 0x00006903
		public CustomAttributeHandleCollection GetCustomAttributes()
		{
			return new CustomAttributeHandleCollection(this._reader, this.Handle);
		}

		// Token: 0x0400029B RID: 667
		private readonly MetadataReader _reader;

		// Token: 0x0400029C RID: 668
		private readonly int _rowId;
	}
}
