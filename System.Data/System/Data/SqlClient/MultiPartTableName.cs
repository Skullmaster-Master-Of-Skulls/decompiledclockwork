using System;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x0200032E RID: 814
	internal struct MultiPartTableName
	{
		// Token: 0x06002A6E RID: 10862 RVA: 0x002BEA68 File Offset: 0x002BDE68
		internal MultiPartTableName(string[] parts)
		{
			this._multipartName = null;
			this._serverName = parts[0];
			this._catalogName = parts[1];
			this._schemaName = parts[2];
			this._tableName = parts[3];
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x002BEAA8 File Offset: 0x002BDEA8
		internal MultiPartTableName(string multipartName)
		{
			this._multipartName = multipartName;
			this._serverName = null;
			this._catalogName = null;
			this._schemaName = null;
			this._tableName = null;
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002A70 RID: 10864 RVA: 0x002BEAD8 File Offset: 0x002BDED8
		// (set) Token: 0x06002A71 RID: 10865 RVA: 0x002BEAF8 File Offset: 0x002BDEF8
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

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x002BEB18 File Offset: 0x002BDF18
		// (set) Token: 0x06002A73 RID: 10867 RVA: 0x002BEB38 File Offset: 0x002BDF38
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

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x002BEB58 File Offset: 0x002BDF58
		// (set) Token: 0x06002A75 RID: 10869 RVA: 0x002BEB78 File Offset: 0x002BDF78
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

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002A76 RID: 10870 RVA: 0x002BEB98 File Offset: 0x002BDF98
		// (set) Token: 0x06002A77 RID: 10871 RVA: 0x002BEBB8 File Offset: 0x002BDFB8
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

		// Token: 0x06002A78 RID: 10872 RVA: 0x002BEBD8 File Offset: 0x002BDFD8
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

		// Token: 0x06002A79 RID: 10873 RVA: 0x002BEC38 File Offset: 0x002BE038
		// Note: this type is marked as 'beforefieldinit'.
		static MultiPartTableName()
		{
			string[] parts = new string[4];
			MultiPartTableName.Null = new MultiPartTableName(parts);
		}

		// Token: 0x04001BFB RID: 7163
		private string _multipartName;

		// Token: 0x04001BFC RID: 7164
		private string _serverName;

		// Token: 0x04001BFD RID: 7165
		private string _catalogName;

		// Token: 0x04001BFE RID: 7166
		private string _schemaName;

		// Token: 0x04001BFF RID: 7167
		private string _tableName;

		// Token: 0x04001C00 RID: 7168
		internal static readonly MultiPartTableName Null;
	}
}
