using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x020001E4 RID: 484
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.OdbcDataAdapterToolboxItem, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("Microsoft.VSDesigner.Data.VS.OdbcDataAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RowUpdated")]
	public sealed class OdbcDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		// Token: 0x06001AE8 RID: 6888 RVA: 0x0025FBE8 File Offset: 0x0025EFE8
		public OdbcDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0025FC08 File Offset: 0x0025F008
		public OdbcDataAdapter(OdbcCommand selectCommand) : this()
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x0025FC28 File Offset: 0x0025F028
		public OdbcDataAdapter(string selectCommandText, OdbcConnection selectConnection) : this()
		{
			this.SelectCommand = new OdbcCommand(selectCommandText, selectConnection);
		}

		// Token: 0x06001AEB RID: 6891 RVA: 0x0025FC48 File Offset: 0x0025F048
		public OdbcDataAdapter(string selectCommandText, string selectConnectionString) : this()
		{
			OdbcConnection connection = new OdbcConnection(selectConnectionString);
			this.SelectCommand = new OdbcCommand(selectCommandText, connection);
		}

		// Token: 0x06001AEC RID: 6892 RVA: 0x0025FC78 File Offset: 0x0025F078
		private OdbcDataAdapter(OdbcDataAdapter from) : base(from)
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06001AED RID: 6893 RVA: 0x0025FC98 File Offset: 0x0025F098
		// (set) Token: 0x06001AEE RID: 6894 RVA: 0x0025FCB8 File Offset: 0x0025F0B8
		[ResDescription("DbDataAdapter_DeleteCommand")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_Update")]
		[DefaultValue(null)]
		public new OdbcCommand DeleteCommand
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

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x0025FCD8 File Offset: 0x0025F0D8
		// (set) Token: 0x06001AF0 RID: 6896 RVA: 0x0025FCF8 File Offset: 0x0025F0F8
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (OdbcCommand)value;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x0025FD18 File Offset: 0x0025F118
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x0025FD38 File Offset: 0x0025F138
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbDataAdapter_InsertCommand")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		public new OdbcCommand InsertCommand
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

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x0025FD58 File Offset: 0x0025F158
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x0025FD78 File Offset: 0x0025F178
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (OdbcCommand)value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x0025FD98 File Offset: 0x0025F198
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x0025FDB8 File Offset: 0x0025F1B8
		[ResCategory("DataCategory_Fill")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[ResDescription("DbDataAdapter_SelectCommand")]
		public new OdbcCommand SelectCommand
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

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0025FDD8 File Offset: 0x0025F1D8
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x0025FDF8 File Offset: 0x0025F1F8
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (OdbcCommand)value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0025FE18 File Offset: 0x0025F218
		// (set) Token: 0x06001AFA RID: 6906 RVA: 0x0025FE38 File Offset: 0x0025F238
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbDataAdapter_UpdateCommand")]
		public new OdbcCommand UpdateCommand
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

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001AFB RID: 6907 RVA: 0x0025FE58 File Offset: 0x0025F258
		// (set) Token: 0x06001AFC RID: 6908 RVA: 0x0025FE78 File Offset: 0x0025F278
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (OdbcCommand)value;
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001AFD RID: 6909 RVA: 0x0025FE98 File Offset: 0x0025F298
		// (remove) Token: 0x06001AFE RID: 6910 RVA: 0x0025FEB8 File Offset: 0x0025F2B8
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbDataAdapter_RowUpdated")]
		public event OdbcRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(OdbcDataAdapter.EventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(OdbcDataAdapter.EventRowUpdated, value);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001AFF RID: 6911 RVA: 0x0025FED8 File Offset: 0x0025F2D8
		// (remove) Token: 0x06001B00 RID: 6912 RVA: 0x0025FF48 File Offset: 0x0025F348
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbDataAdapter_RowUpdating")]
		public event OdbcRowUpdatingEventHandler RowUpdating
		{
			add
			{
				OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler = (OdbcRowUpdatingEventHandler)base.Events[OdbcDataAdapter.EventRowUpdating];
				if (odbcRowUpdatingEventHandler != null && value.Target is OdbcCommandBuilder)
				{
					OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler2 = (OdbcRowUpdatingEventHandler)ADP.FindBuilder(odbcRowUpdatingEventHandler);
					if (odbcRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(OdbcDataAdapter.EventRowUpdating, odbcRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(OdbcDataAdapter.EventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(OdbcDataAdapter.EventRowUpdating, value);
			}
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x0025FF68 File Offset: 0x0025F368
		object ICloneable.Clone()
		{
			return new OdbcDataAdapter(this);
		}

		// Token: 0x06001B02 RID: 6914 RVA: 0x0025FF88 File Offset: 0x0025F388
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001B03 RID: 6915 RVA: 0x0025FFA8 File Offset: 0x0025F3A8
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x0025FFC8 File Offset: 0x0025F3C8
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			OdbcRowUpdatedEventHandler odbcRowUpdatedEventHandler = (OdbcRowUpdatedEventHandler)base.Events[OdbcDataAdapter.EventRowUpdated];
			if (odbcRowUpdatedEventHandler != null && value is OdbcRowUpdatedEventArgs)
			{
				odbcRowUpdatedEventHandler(this, (OdbcRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00260018 File Offset: 0x0025F418
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler = (OdbcRowUpdatingEventHandler)base.Events[OdbcDataAdapter.EventRowUpdating];
			if (odbcRowUpdatingEventHandler != null && value is OdbcRowUpdatingEventArgs)
			{
				odbcRowUpdatingEventHandler(this, (OdbcRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x04000FE0 RID: 4064
		private static readonly object EventRowUpdated = new object();

		// Token: 0x04000FE1 RID: 4065
		private static readonly object EventRowUpdating = new object();

		// Token: 0x04000FE2 RID: 4066
		private OdbcCommand _deleteCommand;

		// Token: 0x04000FE3 RID: 4067
		private OdbcCommand _insertCommand;

		// Token: 0x04000FE4 RID: 4068
		private OdbcCommand _selectCommand;

		// Token: 0x04000FE5 RID: 4069
		private OdbcCommand _updateCommand;
	}
}
