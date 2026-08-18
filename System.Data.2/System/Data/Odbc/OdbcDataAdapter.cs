using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x02000297 RID: 663
	[Designer("Microsoft.VSDesigner.Data.VS.OdbcDataAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RowUpdated")]
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.OdbcDataAdapterToolboxItem, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class OdbcDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		// Token: 0x06002852 RID: 10322 RVA: 0x0010D3D8 File Offset: 0x0010C7D8
		public OdbcDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x0010D3F4 File Offset: 0x0010C7F4
		public OdbcDataAdapter(OdbcCommand selectCommand) : this()
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x0010D410 File Offset: 0x0010C810
		public OdbcDataAdapter(string selectCommandText, OdbcConnection selectConnection) : this()
		{
			this.SelectCommand = new OdbcCommand(selectCommandText, selectConnection);
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x0010D430 File Offset: 0x0010C830
		public OdbcDataAdapter(string selectCommandText, string selectConnectionString) : this()
		{
			OdbcConnection connection = new OdbcConnection(selectConnectionString);
			this.SelectCommand = new OdbcCommand(selectCommandText, connection);
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x0010D458 File Offset: 0x0010C858
		private OdbcDataAdapter(OdbcDataAdapter from) : base(from)
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x0010D474 File Offset: 0x0010C874
		// (set) Token: 0x06002858 RID: 10328 RVA: 0x0010D488 File Offset: 0x0010C888
		[ResDescription("DbDataAdapter_DeleteCommand")]
		[ResCategory("DataCategory_Update")]
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x0010D49C File Offset: 0x0010C89C
		// (set) Token: 0x0600285A RID: 10330 RVA: 0x0010D4B0 File Offset: 0x0010C8B0
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

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x0010D4CC File Offset: 0x0010C8CC
		// (set) Token: 0x0600285C RID: 10332 RVA: 0x0010D4E0 File Offset: 0x0010C8E0
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbDataAdapter_InsertCommand")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600285D RID: 10333 RVA: 0x0010D4F4 File Offset: 0x0010C8F4
		// (set) Token: 0x0600285E RID: 10334 RVA: 0x0010D508 File Offset: 0x0010C908
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

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600285F RID: 10335 RVA: 0x0010D524 File Offset: 0x0010C924
		// (set) Token: 0x06002860 RID: 10336 RVA: 0x0010D538 File Offset: 0x0010C938
		[ResCategory("DataCategory_Fill")]
		[ResDescription("DbDataAdapter_SelectCommand")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
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

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x0010D54C File Offset: 0x0010C94C
		// (set) Token: 0x06002862 RID: 10338 RVA: 0x0010D560 File Offset: 0x0010C960
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

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002863 RID: 10339 RVA: 0x0010D57C File Offset: 0x0010C97C
		// (set) Token: 0x06002864 RID: 10340 RVA: 0x0010D590 File Offset: 0x0010C990
		[ResCategory("DataCategory_Update")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResDescription("DbDataAdapter_UpdateCommand")]
		[DefaultValue(null)]
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

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x0010D5A4 File Offset: 0x0010C9A4
		// (set) Token: 0x06002866 RID: 10342 RVA: 0x0010D5B8 File Offset: 0x0010C9B8
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

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06002867 RID: 10343 RVA: 0x0010D5D4 File Offset: 0x0010C9D4
		// (remove) Token: 0x06002868 RID: 10344 RVA: 0x0010D5F4 File Offset: 0x0010C9F4
		[ResDescription("DbDataAdapter_RowUpdated")]
		[ResCategory("DataCategory_Update")]
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

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06002869 RID: 10345 RVA: 0x0010D614 File Offset: 0x0010CA14
		// (remove) Token: 0x0600286A RID: 10346 RVA: 0x0010D678 File Offset: 0x0010CA78
		[ResDescription("DbDataAdapter_RowUpdating")]
		[ResCategory("DataCategory_Update")]
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

		// Token: 0x0600286B RID: 10347 RVA: 0x0010D698 File Offset: 0x0010CA98
		object ICloneable.Clone()
		{
			return new OdbcDataAdapter(this);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x0010D6AC File Offset: 0x0010CAAC
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x0010D6C4 File Offset: 0x0010CAC4
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x0010D6DC File Offset: 0x0010CADC
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			OdbcRowUpdatedEventHandler odbcRowUpdatedEventHandler = (OdbcRowUpdatedEventHandler)base.Events[OdbcDataAdapter.EventRowUpdated];
			if (odbcRowUpdatedEventHandler != null && value is OdbcRowUpdatedEventArgs)
			{
				odbcRowUpdatedEventHandler(this, (OdbcRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x0010D720 File Offset: 0x0010CB20
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			OdbcRowUpdatingEventHandler odbcRowUpdatingEventHandler = (OdbcRowUpdatingEventHandler)base.Events[OdbcDataAdapter.EventRowUpdating];
			if (odbcRowUpdatingEventHandler != null && value is OdbcRowUpdatingEventArgs)
			{
				odbcRowUpdatingEventHandler(this, (OdbcRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x04001A85 RID: 6789
		private static readonly object EventRowUpdated = new object();

		// Token: 0x04001A86 RID: 6790
		private static readonly object EventRowUpdating = new object();

		// Token: 0x04001A87 RID: 6791
		private OdbcCommand _deleteCommand;

		// Token: 0x04001A88 RID: 6792
		private OdbcCommand _insertCommand;

		// Token: 0x04001A89 RID: 6793
		private OdbcCommand _selectCommand;

		// Token: 0x04001A8A RID: 6794
		private OdbcCommand _updateCommand;
	}
}
