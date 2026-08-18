using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000062 RID: 98
	internal struct MethodDebugInformation
	{
		// Token: 0x060002BE RID: 702 RVA: 0x00007426 File Offset: 0x00005626
		internal MethodDebugInformation(MetadataReader reader, MethodDebugInformationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000743C File Offset: 0x0000563C
		private MethodDebugInformationHandle Handle
		{
			get
			{
				return MethodDebugInformationHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00007449 File Offset: 0x00005649
		public BlobHandle SequencePointsBlob
		{
			get
			{
				return this._reader.MethodDebugInformationTable.GetSequencePoints(this.Handle);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00007461 File Offset: 0x00005661
		public DocumentHandle Document
		{
			get
			{
				return this._reader.MethodDebugInformationTable.GetDocument(this.Handle);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00007479 File Offset: 0x00005679
		public SequencePointCollection GetSequencePoints()
		{
			return new SequencePointCollection(this._reader.BlobHeap.GetMemoryBlock(this.SequencePointsBlob), this.Document);
		}

		// Token: 0x04000350 RID: 848
		private readonly MetadataReader _reader;

		// Token: 0x04000351 RID: 849
		private readonly int _rowId;
	}
}
