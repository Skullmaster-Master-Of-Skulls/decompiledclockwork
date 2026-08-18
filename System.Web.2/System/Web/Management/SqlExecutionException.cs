using System;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Management
{
	// Token: 0x02000178 RID: 376
	[Serializable]
	public sealed class SqlExecutionException : SystemException
	{
		// Token: 0x060014AA RID: 5290 RVA: 0x0003E2A6 File Offset: 0x0003C4A6
		public SqlExecutionException(string message, string server, string database, string sqlFile, string commands, SqlException sqlException) : base(message)
		{
			this._server = server;
			this._database = database;
			this._sqlFile = sqlFile;
			this._commands = commands;
			this._sqlException = sqlException;
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0003E2D5 File Offset: 0x0003C4D5
		public SqlExecutionException(string message) : base(message)
		{
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0003E2DE File Offset: 0x0003C4DE
		public SqlExecutionException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0003E2E8 File Offset: 0x0003C4E8
		public SqlExecutionException()
		{
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0003E2F0 File Offset: 0x0003C4F0
		private SqlExecutionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this._server = info.GetString("_server");
			this._database = info.GetString("_database");
			this._sqlFile = info.GetString("_sqlFile");
			this._commands = info.GetString("_commands");
			this._sqlException = (SqlException)info.GetValue("_sqlException", typeof(SqlException));
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0003E36C File Offset: 0x0003C56C
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_server", this._server);
			info.AddValue("_database", this._database);
			info.AddValue("_sqlFile", this._sqlFile);
			info.AddValue("_commands", this._commands);
			info.AddValue("_sqlException", this._sqlException);
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0003E3D6 File Offset: 0x0003C5D6
		public string Server
		{
			get
			{
				return this._server;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0003E3DE File Offset: 0x0003C5DE
		public string Database
		{
			get
			{
				return this._database;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0003E3E6 File Offset: 0x0003C5E6
		public string SqlFile
		{
			get
			{
				return this._sqlFile;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0003E3EE File Offset: 0x0003C5EE
		public string Commands
		{
			get
			{
				return this._commands;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0003E3F6 File Offset: 0x0003C5F6
		public SqlException Exception
		{
			get
			{
				return this._sqlException;
			}
		}

		// Token: 0x0400157B RID: 5499
		private string _server;

		// Token: 0x0400157C RID: 5500
		private string _database;

		// Token: 0x0400157D RID: 5501
		private string _sqlFile;

		// Token: 0x0400157E RID: 5502
		private string _commands;

		// Token: 0x0400157F RID: 5503
		private SqlException _sqlException;
	}
}
