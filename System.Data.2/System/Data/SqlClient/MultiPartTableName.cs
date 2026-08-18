using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x02000227 RID: 551
	internal struct MultiPartTableName
	{
		// Token: 0x0600222C RID: 8748 RVA: 0x000ED064 File Offset: 0x000EC464
		internal MultiPartTableName(string[] parts)
		{
			this._multipartName = null;
			this._serverName = parts[0];
			this._catalogName = parts[1];
			this._schemaName = parts[2];
			this._tableName = parts[3];
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000ED09C File Offset: 0x000EC49C
		internal MultiPartTableName(string multipartName)
		{
			this._multipartName = multipartName;
			this._serverName = null;
			this._catalogName = null;
			this._schemaName = null;
			this._tableName = null;
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x000ED0CC File Offset: 0x000EC4CC
		// (set) Token: 0x0600222F RID: 8751 RVA: 0x000ED0E8 File Offset: 0x000EC4E8
		internal string ServerName
		{
			get
			{
				this.ParseMultipartName();
				return this._serverName;
			}
			set
			{
				this._serverName = value;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06002230 RID: 8752 RVA: 0x000ED0FC File Offset: 0x000EC4FC
		// (set) Token: 0x06002231 RID: 8753 RVA: 0x000ED118 File Offset: 0x000EC518
		internal string CatalogName
		{
			get
			{
				this.ParseMultipartName();
				return this._catalogName;
			}
			set
			{
				this._catalogName = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x000ED12C File Offset: 0x000EC52C
		// (set) Token: 0x06002233 RID: 8755 RVA: 0x000ED148 File Offset: 0x000EC548
		internal string SchemaName
		{
			get
			{
				this.ParseMultipartName();
				return this._schemaName;
			}
			set
			{
				this._schemaName = value;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x000ED15C File Offset: 0x000EC55C
		// (set) Token: 0x06002235 RID: 8757 RVA: 0x000ED178 File Offset: 0x000EC578
		internal string TableName
		{
			get
			{
				this.ParseMultipartName();
				return this._tableName;
			}
			set
			{
				this._tableName = value;
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000ED18C File Offset: 0x000EC58C
		private void ParseMultipartName()
		{
			if (this._multipartName != null)
			{
				string[] array = MultipartIdentifier.ParseMultipartIdentifier(this._multipartName, "[\"", "]\"", "SQL_TDSParserTableName", false);
				this._serverName = array[0];
				this._catalogName = array[1];
				this._schemaName = array[2];
				this._tableName = array[3];
				this._multipartName = null;
			}
		}

		// Token: 0x040014A8 RID: 5288
		private string _multipartName;

		// Token: 0x040014A9 RID: 5289
		private string _serverName;

		// Token: 0x040014AA RID: 5290
		private string _catalogName;

		// Token: 0x040014AB RID: 5291
		private string _schemaName;

		// Token: 0x040014AC RID: 5292
		private string _tableName;

		// Token: 0x040014AD RID: 5293
		internal static readonly MultiPartTableName Null = new MultiPartTableName(new string[4]);
	}
}
