using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020001C4 RID: 452
	[Designer("Microsoft.VSDesigner.Data.VS.SqlDataAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.SqlDataAdapterToolboxItem, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RowUpdated")]
	public sealed class SqlDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		// Token: 0x06001B9D RID: 7069 RVA: 0x000C149C File Offset: 0x000C089C
		public SqlDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001B9E RID: 7070 RVA: 0x000C14BC File Offset: 0x000C08BC
		public SqlDataAdapter(SqlCommand selectCommand) : this()
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x000C14D8 File Offset: 0x000C08D8
		public SqlDataAdapter(string selectCommandText, string selectConnectionString) : this()
		{
			SqlConnection connection = new SqlConnection(selectConnectionString);
			this.SelectCommand = new SqlCommand(selectCommandText, connection);
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x000C1500 File Offset: 0x000C0900
		public SqlDataAdapter(string selectCommandText, SqlConnection selectConnection) : this()
		{
			this.SelectCommand = new SqlCommand(selectCommandText, selectConnection);
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x000C1520 File Offset: 0x000C0920
		private SqlDataAdapter(SqlDataAdapter from) : base(from)
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001BA2 RID: 7074 RVA: 0x000C1544 File Offset: 0x000C0944
		// (set) Token: 0x06001BA3 RID: 7075 RVA: 0x000C1558 File Offset: 0x000C0958
		[ResDescription("DbDataAdapter_DeleteCommand")]
		[ResCategory("DataCategory_Update")]
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqlCommand DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001BA4 RID: 7076 RVA: 0x000C156C File Offset: 0x000C096C
		// (set) Token: 0x06001BA5 RID: 7077 RVA: 0x000C1580 File Offset: 0x000C0980
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (SqlCommand)value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x000C159C File Offset: 0x000C099C
		// (set) Token: 0x06001BA7 RID: 7079 RVA: 0x000C15B0 File Offset: 0x000C09B0
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[ResDescription("DbDataAdapter_InsertCommand")]
		[ResCategory("DataCategory_Update")]
		public new SqlCommand InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x000C15C4 File Offset: 0x000C09C4
		// (set) Token: 0x06001BA9 RID: 7081 RVA: 0x000C15D8 File Offset: 0x000C09D8
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (SqlCommand)value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x000C15F4 File Offset: 0x000C09F4
		// (set) Token: 0x06001BAB RID: 7083 RVA: 0x000C1608 File Offset: 0x000C0A08
		[ResDescription("DbDataAdapter_SelectCommand")]
		[ResCategory("DataCategory_Fill")]
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqlCommand SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = value;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x000C161C File Offset: 0x000C0A1C
		// (set) Token: 0x06001BAD RID: 7085 RVA: 0x000C1630 File Offset: 0x000C0A30
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (SqlCommand)value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x000C164C File Offset: 0x000C0A4C
		// (set) Token: 0x06001BAF RID: 7087 RVA: 0x000C1660 File Offset: 0x000C0A60
		public override int UpdateBatchSize
		{
			get
			{
				return this._updateBatchSize;
			}
			set
			{
				if (0 > value)
				{
					throw ADP.ArgumentOutOfRange("UpdateBatchSize");
				}
				this._updateBatchSize = value;
				Bid.Trace("<sc.SqlDataAdapter.set_UpdateBatchSize|API> %d#, %d\n", base.ObjectID, value);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x000C1694 File Offset: 0x000C0A94
		// (set) Token: 0x06001BB1 RID: 7089 RVA: 0x000C16A8 File Offset: 0x000C0AA8
		[ResDescription("DbDataAdapter_UpdateCommand")]
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_Update")]
		public new SqlCommand UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x000C16BC File Offset: 0x000C0ABC
		// (set) Token: 0x06001BB3 RID: 7091 RVA: 0x000C16D0 File Offset: 0x000C0AD0
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (SqlCommand)value;
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001BB4 RID: 7092 RVA: 0x000C16EC File Offset: 0x000C0AEC
		// (remove) Token: 0x06001BB5 RID: 7093 RVA: 0x000C170C File Offset: 0x000C0B0C
		[ResDescription("DbDataAdapter_RowUpdated")]
		[ResCategory("DataCategory_Update")]
		public event SqlRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(SqlDataAdapter.EventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdated, value);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001BB6 RID: 7094 RVA: 0x000C172C File Offset: 0x000C0B2C
		// (remove) Token: 0x06001BB7 RID: 7095 RVA: 0x000C1790 File Offset: 0x000C0B90
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbDataAdapter_RowUpdating")]
		public event SqlRowUpdatingEventHandler RowUpdating
		{
			add
			{
				SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.EventRowUpdating];
				if (sqlRowUpdatingEventHandler != null && value.Target is DbCommandBuilder)
				{
					SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler2 = (SqlRowUpdatingEventHandler)ADP.FindBuilder(sqlRowUpdatingEventHandler);
					if (sqlRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdating, sqlRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(SqlDataAdapter.EventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(SqlDataAdapter.EventRowUpdating, value);
			}
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x000C17B0 File Offset: 0x000C0BB0
		protected override int AddToBatch(IDbCommand command)
		{
			int commandCount = this._commandSet.CommandCount;
			this._commandSet.Append((SqlCommand)command);
			return commandCount;
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x000C17DC File Offset: 0x000C0BDC
		protected override void ClearBatch()
		{
			this._commandSet.Clear();
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x000C17F4 File Offset: 0x000C0BF4
		object ICloneable.Clone()
		{
			return new SqlDataAdapter(this);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x000C1808 File Offset: 0x000C0C08
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x000C1820 File Offset: 0x000C0C20
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000C1838 File Offset: 0x000C0C38
		protected override int ExecuteBatch()
		{
			Bid.CorrelationTrace("<sc.SqlDataAdapter.ExecuteBatch|Info|Correlation> ObjectID%d#, ActivityID %ls\n", base.ObjectID);
			return this._commandSet.ExecuteNonQuery();
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x000C1860 File Offset: 0x000C0C60
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			return this._commandSet.GetParameter(commandIdentifier, parameterIndex);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x000C187C File Offset: 0x000C0C7C
		protected override bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			return this._commandSet.GetBatchedAffected(commandIdentifier, out recordsAffected, out error);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x000C1898 File Offset: 0x000C0C98
		protected override void InitializeBatching()
		{
			Bid.Trace("<sc.SqlDataAdapter.InitializeBatching|API> %d#\n", base.ObjectID);
			this._commandSet = new SqlCommandSet();
			SqlCommand sqlCommand = this.SelectCommand;
			if (sqlCommand == null)
			{
				sqlCommand = this.InsertCommand;
				if (sqlCommand == null)
				{
					sqlCommand = this.UpdateCommand;
					if (sqlCommand == null)
					{
						sqlCommand = this.DeleteCommand;
					}
				}
			}
			if (sqlCommand != null)
			{
				this._commandSet.Connection = sqlCommand.Connection;
				this._commandSet.Transaction = sqlCommand.Transaction;
				this._commandSet.CommandTimeout = sqlCommand.CommandTimeout;
			}
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x000C191C File Offset: 0x000C0D1C
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			SqlRowUpdatedEventHandler sqlRowUpdatedEventHandler = (SqlRowUpdatedEventHandler)base.Events[SqlDataAdapter.EventRowUpdated];
			if (sqlRowUpdatedEventHandler != null && value is SqlRowUpdatedEventArgs)
			{
				sqlRowUpdatedEventHandler(this, (SqlRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x000C1960 File Offset: 0x000C0D60
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.EventRowUpdating];
			if (sqlRowUpdatingEventHandler != null && value is SqlRowUpdatingEventArgs)
			{
				sqlRowUpdatingEventHandler(this, (SqlRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x000C19A4 File Offset: 0x000C0DA4
		protected override void TerminateBatching()
		{
			if (this._commandSet != null)
			{
				this._commandSet.Dispose();
				this._commandSet = null;
			}
		}

		// Token: 0x04001006 RID: 4102
		private static readonly object EventRowUpdated = new object();

		// Token: 0x04001007 RID: 4103
		private static readonly object EventRowUpdating = new object();

		// Token: 0x04001008 RID: 4104
		private SqlCommand _deleteCommand;

		// Token: 0x04001009 RID: 4105
		private SqlCommand _insertCommand;

		// Token: 0x0400100A RID: 4106
		private SqlCommand _selectCommand;

		// Token: 0x0400100B RID: 4107
		private SqlCommand _updateCommand;

		// Token: 0x0400100C RID: 4108
		private SqlCommandSet _commandSet;

		// Token: 0x0400100D RID: 4109
		private int _updateBatchSize = 1;
	}
}
