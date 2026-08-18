using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.SqlClient
{
	// Token: 0x020002DD RID: 733
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.SqlDataAdapterToolboxItem, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("Microsoft.VSDesigner.Data.VS.SqlDataAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RowUpdated")]
	public sealed class SqlDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		// Token: 0x0600259C RID: 9628 RVA: 0x0029CDA8 File Offset: 0x0029C1A8
		public SqlDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0029CDC8 File Offset: 0x0029C1C8
		public SqlDataAdapter(SqlCommand selectCommand) : this()
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0029CDE8 File Offset: 0x0029C1E8
		public SqlDataAdapter(string selectCommandText, string selectConnectionString) : this()
		{
			SqlConnection connection = new SqlConnection(selectConnectionString);
			this.SelectCommand = new SqlCommand(selectCommandText, connection);
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x0029CE18 File Offset: 0x0029C218
		public SqlDataAdapter(string selectCommandText, SqlConnection selectConnection) : this()
		{
			this.SelectCommand = new SqlCommand(selectCommandText, selectConnection);
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x0029CE38 File Offset: 0x0029C238
		private SqlDataAdapter(SqlDataAdapter from) : base(from)
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x0029CE68 File Offset: 0x0029C268
		// (set) Token: 0x060025A2 RID: 9634 RVA: 0x0029CE88 File Offset: 0x0029C288
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResDescription("DbDataAdapter_DeleteCommand")]
		[ResCategory("DataCategory_Update")]
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

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x0029CEA8 File Offset: 0x0029C2A8
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x0029CEC8 File Offset: 0x0029C2C8
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

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x0029CEE8 File Offset: 0x0029C2E8
		// (set) Token: 0x060025A6 RID: 9638 RVA: 0x0029CF08 File Offset: 0x0029C308
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_Update")]
		[DefaultValue(null)]
		[ResDescription("DbDataAdapter_InsertCommand")]
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

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x0029CF28 File Offset: 0x0029C328
		// (set) Token: 0x060025A8 RID: 9640 RVA: 0x0029CF48 File Offset: 0x0029C348
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

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x0029CF68 File Offset: 0x0029C368
		// (set) Token: 0x060025AA RID: 9642 RVA: 0x0029CF88 File Offset: 0x0029C388
		[ResDescription("DbDataAdapter_SelectCommand")]
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_Fill")]
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

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x0029CFA8 File Offset: 0x0029C3A8
		// (set) Token: 0x060025AC RID: 9644 RVA: 0x0029CFC8 File Offset: 0x0029C3C8
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

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x0029CFE8 File Offset: 0x0029C3E8
		// (set) Token: 0x060025AE RID: 9646 RVA: 0x0029D008 File Offset: 0x0029C408
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

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x0029D048 File Offset: 0x0029C448
		// (set) Token: 0x060025B0 RID: 9648 RVA: 0x0029D068 File Offset: 0x0029C468
		[ResCategory("DataCategory_Update")]
		[DefaultValue(null)]
		[ResDescription("DbDataAdapter_UpdateCommand")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x0029D088 File Offset: 0x0029C488
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x0029D0A8 File Offset: 0x0029C4A8
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

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x060025B3 RID: 9651 RVA: 0x0029D0C8 File Offset: 0x0029C4C8
		// (remove) Token: 0x060025B4 RID: 9652 RVA: 0x0029D0E8 File Offset: 0x0029C4E8
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

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x060025B5 RID: 9653 RVA: 0x0029D108 File Offset: 0x0029C508
		// (remove) Token: 0x060025B6 RID: 9654 RVA: 0x0029D178 File Offset: 0x0029C578
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

		// Token: 0x060025B7 RID: 9655 RVA: 0x0029D198 File Offset: 0x0029C598
		protected override int AddToBatch(IDbCommand command)
		{
			int commandCount = this._commandSet.CommandCount;
			this._commandSet.Append((SqlCommand)command);
			return commandCount;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x0029D1C8 File Offset: 0x0029C5C8
		protected override void ClearBatch()
		{
			this._commandSet.Clear();
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x0029D1E8 File Offset: 0x0029C5E8
		object ICloneable.Clone()
		{
			return new SqlDataAdapter(this);
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x0029D208 File Offset: 0x0029C608
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x0029D228 File Offset: 0x0029C628
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x0029D248 File Offset: 0x0029C648
		protected override int ExecuteBatch()
		{
			return this._commandSet.ExecuteNonQuery();
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x0029D268 File Offset: 0x0029C668
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			return this._commandSet.GetParameter(commandIdentifier, parameterIndex);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x0029D288 File Offset: 0x0029C688
		protected override bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			return this._commandSet.GetBatchedAffected(commandIdentifier, out recordsAffected, out error);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x0029D2A8 File Offset: 0x0029C6A8
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

		// Token: 0x060025C0 RID: 9664 RVA: 0x0029D338 File Offset: 0x0029C738
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			SqlRowUpdatedEventHandler sqlRowUpdatedEventHandler = (SqlRowUpdatedEventHandler)base.Events[SqlDataAdapter.EventRowUpdated];
			if (sqlRowUpdatedEventHandler != null && value is SqlRowUpdatedEventArgs)
			{
				sqlRowUpdatedEventHandler(this, (SqlRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0029D388 File Offset: 0x0029C788
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			SqlRowUpdatingEventHandler sqlRowUpdatingEventHandler = (SqlRowUpdatingEventHandler)base.Events[SqlDataAdapter.EventRowUpdating];
			if (sqlRowUpdatingEventHandler != null && value is SqlRowUpdatingEventArgs)
			{
				sqlRowUpdatingEventHandler(this, (SqlRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x0029D3D8 File Offset: 0x0029C7D8
		protected override void TerminateBatching()
		{
			if (this._commandSet != null)
			{
				this._commandSet.Dispose();
				this._commandSet = null;
			}
		}

		// Token: 0x0400180C RID: 6156
		private static readonly object EventRowUpdated = new object();

		// Token: 0x0400180D RID: 6157
		private static readonly object EventRowUpdating = new object();

		// Token: 0x0400180E RID: 6158
		private SqlCommand _deleteCommand;

		// Token: 0x0400180F RID: 6159
		private SqlCommand _insertCommand;

		// Token: 0x04001810 RID: 6160
		private SqlCommand _selectCommand;

		// Token: 0x04001811 RID: 6161
		private SqlCommand _updateCommand;

		// Token: 0x04001812 RID: 6162
		private SqlCommandSet _commandSet;

		// Token: 0x04001813 RID: 6163
		private int _updateBatchSize = 1;
	}
}
