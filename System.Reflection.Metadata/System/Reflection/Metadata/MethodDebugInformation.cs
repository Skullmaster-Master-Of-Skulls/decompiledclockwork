using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000A3 RID: 163
	public struct MethodDebugInformation
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x0000FA74 File Offset: 0x0000DC74
		internal MethodDebugInformation(MetadataReader reader, MethodDebugInformationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0000FA8A File Offset: 0x0000DC8A
		private MethodDebugInformationHandle Handle
		{
			get
			{
				return MethodDebugInformationHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0000FA97 File Offset: 0x0000DC97
		public BlobHandle SequencePointsBlob
		{
			get
			{
				return this._reader.MethodDebugInformationTable.GetSequencePoints(this.Handle);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0000FAAF File Offset: 0x0000DCAF
		public DocumentHandle Document
		{
			get
			{
				return this._reader.MethodDebugInformationTable.GetDocument(this.Handle);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000FAC8 File Offset: 0x0000DCC8
		public StandaloneSignatureHandle LocalSignature
		{
			get
			{
				if (this.SequencePointsBlob.IsNil)
				{
					return default(StandaloneSignatureHandle);
				}
				return StandaloneSignatureHandle.FromRowId(this._reader.GetBlobReader(this.SequencePointsBlob).ReadCompressedInteger());
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0000FB0D File Offset: 0x0000DD0D
		public SequencePointCollection GetSequencePoints()
		{
			return new SequencePointCollection(this._reader.BlobStream.GetMemoryBlock(this.SequencePointsBlob), this.Document);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0000FB30 File Offset: 0x0000DD30
		public MethodDefinitionHandle GetStateMachineKickoffMethod()
		{
			return this._reader.StateMachineMethodTable.FindKickoffMethod(this._rowId);
		}

		// Token: 0x04000416 RID: 1046
		private readonly MetadataReader _reader;

		// Token: 0x04000417 RID: 1047
		private readonly int _rowId;
	}
}
