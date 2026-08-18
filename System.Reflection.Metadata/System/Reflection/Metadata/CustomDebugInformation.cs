using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000089 RID: 137
	public struct CustomDebugInformation
	{
		// Token: 0x06000628 RID: 1576 RVA: 0x0000ED0B File Offset: 0x0000CF0B
		internal CustomDebugInformation(MetadataReader reader, CustomDebugInformationHandle handle)
		{
			this._reader = reader;
			this._rowId = handle.RowId;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000ED21 File Offset: 0x0000CF21
		private CustomDebugInformationHandle Handle
		{
			get
			{
				return CustomDebugInformationHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000ED2E File Offset: 0x0000CF2E
		public EntityHandle Parent
		{
			get
			{
				return this._reader.CustomDebugInformationTable.GetParent(this.Handle);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000ED46 File Offset: 0x0000CF46
		public GuidHandle Kind
		{
			get
			{
				return this._reader.CustomDebugInformationTable.GetKind(this.Handle);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000ED5E File Offset: 0x0000CF5E
		public BlobHandle Value
		{
			get
			{
				return this._reader.CustomDebugInformationTable.GetValue(this.Handle);
			}
		}

		// Token: 0x040003CB RID: 971
		private readonly MetadataReader _reader;

		// Token: 0x040003CC RID: 972
		private readonly int _rowId;
	}
}
