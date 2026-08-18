using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.OracleClient
{
	// Token: 0x02000061 RID: 97
	[DefaultEvent("RowUpdated")]
	[Designer("Microsoft.VSDesigner.Data.VS.OracleDataAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.OracleDataAdapterToolboxItem, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class OracleDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x000662B4 File Offset: 0x000656B4
		public OracleDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000662D4 File Offset: 0x000656D4
		public OracleDataAdapter(OracleCommand selectCommand) : this()
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000662F4 File Offset: 0x000656F4
		public OracleDataAdapter(string selectCommandText, string selectConnectionString) : this()
		{
			OracleConnection connection = new OracleConnection(selectConnectionString);
			this.SelectCommand = new OracleCommand();
			this.SelectCommand.Connection = connection;
			this.SelectCommand.CommandText = selectCommandText;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00066334 File Offset: 0x00065734
		public OracleDataAdapter(string selectCommandText, OracleConnection selectConnection) : this()
		{
			this.SelectCommand = new OracleCommand();
			this.SelectCommand.Connection = selectConnection;
			this.SelectCommand.CommandText = selectCommandText;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00066374 File Offset: 0x00065774
		private OracleDataAdapter(OracleDataAdapter from) : base(from)
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x000663A4 File Offset: 0x000657A4
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x000663C4 File Offset: 0x000657C4
		[DefaultValue(null)]
		[ResCategory("OracleCategory_Update")]
		[ResDescription("DbDataAdapter_DeleteCommand")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new OracleCommand DeleteCommand
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x000663E4 File Offset: 0x000657E4
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x00066404 File Offset: 0x00065804
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (OracleCommand)value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00066424 File Offset: 0x00065824
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00066444 File Offset: 0x00065844
		[DefaultValue(null)]
		[ResDescription("DbDataAdapter_InsertCommand")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("OracleCategory_Update")]
		public new OracleCommand InsertCommand
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00066464 File Offset: 0x00065864
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x00066484 File Offset: 0x00065884
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (OracleCommand)value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x000664A4 File Offset: 0x000658A4
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x000664C4 File Offset: 0x000658C4
		[DefaultValue(null)]
		[ResDescription("DbDataAdapter_SelectCommand")]
		[ResCategory("OracleCategory_Fill")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new OracleCommand SelectCommand
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x000664E4 File Offset: 0x000658E4
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00066504 File Offset: 0x00065904
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (OracleCommand)value;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00066524 File Offset: 0x00065924
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00066544 File Offset: 0x00065944
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
					throw ADP.MustBePositive("UpdateBatchSize");
				}
				this._updateBatchSize = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x00066574 File Offset: 0x00065974
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x00066594 File Offset: 0x00065994
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[ResDescription("DbDataAdapter_UpdateCommand")]
		[ResCategory("OracleCategory_Update")]
		public new OracleCommand UpdateCommand
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

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x000665B4 File Offset: 0x000659B4
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x000665D4 File Offset: 0x000659D4
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (OracleCommand)value;
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000665F4 File Offset: 0x000659F4
		protected override int AddToBatch(IDbCommand command)
		{
			int commandCount = this._commandSet.CommandCount;
			this._commandSet.Append((OracleCommand)command);
			return commandCount;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00066624 File Offset: 0x00065A24
		protected override void ClearBatch()
		{
			this._commandSet.Clear();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00066644 File Offset: 0x00065A44
		object ICloneable.Clone()
		{
			return new OracleDataAdapter(this);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00066664 File Offset: 0x00065A64
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OracleRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00066684 File Offset: 0x00065A84
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OracleRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000666A4 File Offset: 0x00065AA4
		protected override int ExecuteBatch()
		{
			return this._commandSet.ExecuteNonQuery();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000666C4 File Offset: 0x00065AC4
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			return this._commandSet.GetParameter(commandIdentifier, parameterIndex);
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000666E4 File Offset: 0x00065AE4
		protected override bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			error = null;
			return this._commandSet.GetBatchedRecordsAffected(commandIdentifier, out recordsAffected);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00066704 File Offset: 0x00065B04
		protected override void InitializeBatching()
		{
			this._commandSet = new OracleCommandSet();
			OracleCommand oracleCommand = this.SelectCommand;
			if (oracleCommand == null)
			{
				oracleCommand = this.InsertCommand;
				if (oracleCommand == null)
				{
					oracleCommand = this.UpdateCommand;
					if (oracleCommand == null)
					{
						oracleCommand = this.DeleteCommand;
					}
				}
			}
			if (oracleCommand != null)
			{
				this._commandSet.Connection = oracleCommand.Connection;
				this._commandSet.Transaction = oracleCommand.Transaction;
				this._commandSet.CommandTimeout = oracleCommand.CommandTimeout;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00066784 File Offset: 0x00065B84
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			OracleRowUpdatedEventHandler oracleRowUpdatedEventHandler = (OracleRowUpdatedEventHandler)base.Events[OracleDataAdapter.EventRowUpdated];
			if (oracleRowUpdatedEventHandler != null && value is OracleRowUpdatedEventArgs)
			{
				oracleRowUpdatedEventHandler(this, (OracleRowUpdatedEventArgs)value);
			}
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000667C4 File Offset: 0x00065BC4
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			OracleRowUpdatingEventHandler oracleRowUpdatingEventHandler = (OracleRowUpdatingEventHandler)base.Events[OracleDataAdapter.EventRowUpdating];
			if (oracleRowUpdatingEventHandler != null && value is OracleRowUpdatingEventArgs)
			{
				oracleRowUpdatingEventHandler(this, (OracleRowUpdatingEventArgs)value);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00066804 File Offset: 0x00065C04
		protected override void TerminateBatching()
		{
			if (this._commandSet != null)
			{
				this._commandSet.Dispose();
				this._commandSet = null;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600041F RID: 1055 RVA: 0x00066834 File Offset: 0x00065C34
		// (remove) Token: 0x06000420 RID: 1056 RVA: 0x00066854 File Offset: 0x00065C54
		[ResDescription("DbDataAdapter_RowUpdated")]
		[ResCategory("OracleCategory_Update")]
		public event OracleRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(OracleDataAdapter.EventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(OracleDataAdapter.EventRowUpdated, value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000421 RID: 1057 RVA: 0x00066874 File Offset: 0x00065C74
		// (remove) Token: 0x06000422 RID: 1058 RVA: 0x000668E4 File Offset: 0x00065CE4
		[ResCategory("OracleCategory_Update")]
		[ResDescription("DbDataAdapter_RowUpdating")]
		public event OracleRowUpdatingEventHandler RowUpdating
		{
			add
			{
				OracleRowUpdatingEventHandler oracleRowUpdatingEventHandler = (OracleRowUpdatingEventHandler)base.Events[OracleDataAdapter.EventRowUpdating];
				if (oracleRowUpdatingEventHandler != null && value.Target is OracleCommandBuilder)
				{
					OracleRowUpdatingEventHandler oracleRowUpdatingEventHandler2 = (OracleRowUpdatingEventHandler)ADP.FindBuilder(oracleRowUpdatingEventHandler);
					if (oracleRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(OracleDataAdapter.EventRowUpdating, oracleRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(OracleDataAdapter.EventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(OracleDataAdapter.EventRowUpdating, value);
			}
		}

		// Token: 0x040003FF RID: 1023
		internal static readonly object EventRowUpdated = new object();

		// Token: 0x04000400 RID: 1024
		internal static readonly object EventRowUpdating = new object();

		// Token: 0x04000401 RID: 1025
		private OracleCommand _deleteCommand;

		// Token: 0x04000402 RID: 1026
		private OracleCommand _insertCommand;

		// Token: 0x04000403 RID: 1027
		private OracleCommand _selectCommand;

		// Token: 0x04000404 RID: 1028
		private OracleCommand _updateCommand;

		// Token: 0x04000405 RID: 1029
		private OracleCommandSet _commandSet;

		// Token: 0x04000406 RID: 1030
		private int _updateBatchSize = 1;
	}
}
