using System;

namespace System.Reflection.Metadata
{
	// Token: 0x02000037 RID: 55
	public struct DeclarativeSecurityAttribute
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000806C File Offset: 0x0000626C
		internal DeclarativeSecurityAttribute(MetadataReader reader, int rowId)
		{
			this._reader = reader;
			this._rowId = rowId;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000807C File Offset: 0x0000627C
		private DeclarativeSecurityAttributeHandle Handle
		{
			get
			{
				return DeclarativeSecurityAttributeHandle.FromRowId(this._rowId);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00008089 File Offset: 0x00006289
		public DeclarativeSecurityAction Action
		{
			get
			{
				return this._reader.DeclSecurityTable.GetAction(this._rowId);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x000080A1 File Offset: 0x000062A1
		public EntityHandle Parent
		{
			get
			{
				return this._reader.DeclSecurityTable.GetParent(this._rowId);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000080B9 File Offset: 0x000062B9
		public BlobHandle PermissionSet
		{
			get
			{
				return this._reader.DeclSecurityTable.GetPermissionSet(this._rowId);
			}
		}

		// Token: 0x04000286 RID: 646
		private readonly MetadataReader _reader;

		// Token: 0x04000287 RID: 647
		private readonly int _rowId;
	}
}
