using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A1 RID: 161
	public struct LocalVariable
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x0000FA09 File Offset: 0x0000DC09
		internal LocalVariable(MetadataReader reader, LocalVariableHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0000FA1F File Offset: 0x0000DC1F
		private LocalVariableHandle Handle
		{
			get
			{
				return LocalVariableHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0000FA2C File Offset: 0x0000DC2C
		public LocalVariableAttributes Attributes
		{
			get
			{
				return this._reader.LocalVariableTable.GetAttributes(this.Handle);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0000FA44 File Offset: 0x0000DC44
		public int Index
		{
			get
			{
				return (int)this._reader.LocalVariableTable.GetIndex(this.Handle);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0000FA5C File Offset: 0x0000DC5C
		public StringHandle Name
		{
			get
			{
				return this._reader.LocalVariableTable.GetName(this.Handle);
			}
		}

		// Token: 0x04000411 RID: 1041
		private readonly MetadataReader _reader;

		// Token: 0x04000412 RID: 1042
		private readonly int _rowId;
	}
}
