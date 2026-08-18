using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000033 RID: 51
	public struct Constant
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x00007DC5 File Offset: 0x00005FC5
		internal Constant(MetadataReader reader, int rowId)
		{
			this._reader = reader;
			this._rowId = rowId;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00007DD5 File Offset: 0x00005FD5
		private ConstantHandle Handle
		{
			get
			{
				return ConstantHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00007DE2 File Offset: 0x00005FE2
		public ConstantTypeCode TypeCode
		{
			get
			{
				return this._reader.ConstantTable.GetType(this.Handle);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00007DFA File Offset: 0x00005FFA
		public BlobHandle Value
		{
			get
			{
				return this._reader.ConstantTable.GetValue(this.Handle);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00007E12 File Offset: 0x00006012
		public EntityHandle Parent
		{
			get
			{
				return this._reader.ConstantTable.GetParent(this.Handle);
			}
		}

		// Token: 0x0400026F RID: 623
		private readonly MetadataReader _reader;

		// Token: 0x04000270 RID: 624
		private readonly int _rowId;
	}
}
