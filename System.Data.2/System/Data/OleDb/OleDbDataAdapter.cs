using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	// Token: 0x0200024A RID: 586
	[Designer("Microsoft.VSDesigner.Data.VS.OleDbDataAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.OleDbDataAdapterToolboxItem, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RowUpdated")]
	public sealed class OleDbDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		// Token: 0x06002506 RID: 9478 RVA: 0x000FC8C0 File Offset: 0x000FBCC0
		public OleDbDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x000FC8DC File Offset: 0x000FBCDC
		public OleDbDataAdapter(OleDbCommand selectCommand) : this()
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000FC8F8 File Offset: 0x000FBCF8
		public OleDbDataAdapter(string selectCommandText, string selectConnectionString) : this()
		{
			OleDbConnection connection = new OleDbConnection(selectConnectionString);
			this.SelectCommand = new OleDbCommand(selectCommandText, connection);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000FC920 File Offset: 0x000FBD20
		public OleDbDataAdapter(string selectCommandText, OleDbConnection selectConnection) : this()
		{
			this.SelectCommand = new OleDbCommand(selectCommandText, selectConnection);
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000FC940 File Offset: 0x000FBD40
		private OleDbDataAdapter(OleDbDataAdapter from) : base(from)
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x0600250B RID: 9483 RVA: 0x000FC95C File Offset: 0x000FBD5C
		// (set) Token: 0x0600250C RID: 9484 RVA: 0x000FC970 File Offset: 0x000FBD70
		[ResCategory("DataCategory_Update")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResDescription("DbDataAdapter_DeleteCommand")]
		[DefaultValue(null)]
		public new OleDbCommand DeleteCommand
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

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600250D RID: 9485 RVA: 0x000FC984 File Offset: 0x000FBD84
		// (set) Token: 0x0600250E RID: 9486 RVA: 0x000FC998 File Offset: 0x000FBD98
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this._deleteCommand;
			}
			set
			{
				this._deleteCommand = (OleDbCommand)value;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x000FC9B4 File Offset: 0x000FBDB4
		// (set) Token: 0x06002510 RID: 9488 RVA: 0x000FC9C8 File Offset: 0x000FBDC8
		[ResDescription("DbDataAdapter_InsertCommand")]
		[ResCategory("DataCategory_Update")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		public new OleDbCommand InsertCommand
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

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06002511 RID: 9489 RVA: 0x000FC9DC File Offset: 0x000FBDDC
		// (set) Token: 0x06002512 RID: 9490 RVA: 0x000FC9F0 File Offset: 0x000FBDF0
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this._insertCommand;
			}
			set
			{
				this._insertCommand = (OleDbCommand)value;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06002513 RID: 9491 RVA: 0x000FCA0C File Offset: 0x000FBE0C
		// (set) Token: 0x06002514 RID: 9492 RVA: 0x000FCA20 File Offset: 0x000FBE20
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_Fill")]
		[ResDescription("DbDataAdapter_SelectCommand")]
		[DefaultValue(null)]
		public new OleDbCommand SelectCommand
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

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06002515 RID: 9493 RVA: 0x000FCA34 File Offset: 0x000FBE34
		// (set) Token: 0x06002516 RID: 9494 RVA: 0x000FCA48 File Offset: 0x000FBE48
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this._selectCommand;
			}
			set
			{
				this._selectCommand = (OleDbCommand)value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06002517 RID: 9495 RVA: 0x000FCA64 File Offset: 0x000FBE64
		// (set) Token: 0x06002518 RID: 9496 RVA: 0x000FCA78 File Offset: 0x000FBE78
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbDataAdapter_UpdateCommand")]
		public new OleDbCommand UpdateCommand
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

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06002519 RID: 9497 RVA: 0x000FCA8C File Offset: 0x000FBE8C
		// (set) Token: 0x0600251A RID: 9498 RVA: 0x000FCAA0 File Offset: 0x000FBEA0
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this._updateCommand;
			}
			set
			{
				this._updateCommand = (OleDbCommand)value;
			}
		}

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x0600251B RID: 9499 RVA: 0x000FCABC File Offset: 0x000FBEBC
		// (remove) Token: 0x0600251C RID: 9500 RVA: 0x000FCADC File Offset: 0x000FBEDC
		[ResDescription("DbDataAdapter_RowUpdated")]
		[ResCategory("DataCategory_Update")]
		public event OleDbRowUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(OleDbDataAdapter.EventRowUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(OleDbDataAdapter.EventRowUpdated, value);
			}
		}

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600251D RID: 9501 RVA: 0x000FCAFC File Offset: 0x000FBEFC
		// (remove) Token: 0x0600251E RID: 9502 RVA: 0x000FCB60 File Offset: 0x000FBF60
		[ResDescription("DbDataAdapter_RowUpdating")]
		[ResCategory("DataCategory_Update")]
		public event OleDbRowUpdatingEventHandler RowUpdating
		{
			add
			{
				OleDbRowUpdatingEventHandler oleDbRowUpdatingEventHandler = (OleDbRowUpdatingEventHandler)base.Events[OleDbDataAdapter.EventRowUpdating];
				if (oleDbRowUpdatingEventHandler != null && value.Target is DbCommandBuilder)
				{
					OleDbRowUpdatingEventHandler oleDbRowUpdatingEventHandler2 = (OleDbRowUpdatingEventHandler)ADP.FindBuilder(oleDbRowUpdatingEventHandler);
					if (oleDbRowUpdatingEventHandler2 != null)
					{
						base.Events.RemoveHandler(OleDbDataAdapter.EventRowUpdating, oleDbRowUpdatingEventHandler2);
					}
				}
				base.Events.AddHandler(OleDbDataAdapter.EventRowUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(OleDbDataAdapter.EventRowUpdating, value);
			}
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x000FCB80 File Offset: 0x000FBF80
		object ICloneable.Clone()
		{
			return new OleDbDataAdapter(this);
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x000FCB94 File Offset: 0x000FBF94
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OleDbRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x000FCBAC File Offset: 0x000FBFAC
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OleDbRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x000FCBC4 File Offset: 0x000FBFC4
		internal static void FillDataTable(OleDbDataReader dataReader, params DataTable[] dataTables)
		{
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
			oleDbDataAdapter.Fill(dataTables, dataReader, 0, 0);
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x000FCBE4 File Offset: 0x000FBFE4
		public int Fill(DataTable dataTable, object ADODBRecordSet)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(OleDbConnection.ExecutePermission);
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbDataAdapter.Fill|API> %d#, dataTable, ADODBRecordSet\n", base.ObjectID);
			int result;
			try
			{
				if (dataTable == null)
				{
					throw ADP.ArgumentNull("dataTable");
				}
				if (ADODBRecordSet == null)
				{
					throw ADP.ArgumentNull("adodb");
				}
				result = this.FillFromADODB(dataTable, ADODBRecordSet, null, false);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x000FCC78 File Offset: 0x000FC078
		public int Fill(DataSet dataSet, object ADODBRecordSet, string srcTable)
		{
			PermissionSet permissionSet = new PermissionSet(PermissionState.None);
			permissionSet.AddPermission(OleDbConnection.ExecutePermission);
			permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			permissionSet.Demand();
			IntPtr intPtr;
			Bid.ScopeEnter(out intPtr, "<oledb.OleDbDataAdapter.Fill|API> %d#, dataSet, ADODBRecordSet, srcTable='%ls'\n", base.ObjectID, srcTable);
			int result;
			try
			{
				if (dataSet == null)
				{
					throw ADP.ArgumentNull("dataSet");
				}
				if (ADODBRecordSet == null)
				{
					throw ADP.ArgumentNull("adodb");
				}
				if (ADP.IsEmpty(srcTable))
				{
					throw ADP.FillRequiresSourceTableName("srcTable");
				}
				result = this.FillFromADODB(dataSet, ADODBRecordSet, srcTable, true);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x000FCD20 File Offset: 0x000FC120
		private int FillFromADODB(object data, object adodb, string srcTable, bool multipleResults)
		{
			bool flag = multipleResults;
			Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|ADODB> ADORecordsetConstruction\n");
			UnsafeNativeMethods.ADORecordsetConstruction adorecordsetConstruction = adodb as UnsafeNativeMethods.ADORecordsetConstruction;
			UnsafeNativeMethods.ADORecordConstruction adorecordConstruction = null;
			if (adorecordsetConstruction != null)
			{
				if (multipleResults)
				{
					Bid.Trace("<oledb.Recordset15.get_ActiveConnection|API|ADODB>\n");
					if (((UnsafeNativeMethods.Recordset15)adodb).get_ActiveConnection() == null)
					{
						multipleResults = false;
					}
				}
			}
			else
			{
				Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|ADODB> ADORecordConstruction\n");
				adorecordConstruction = (adodb as UnsafeNativeMethods.ADORecordConstruction);
				if (adorecordConstruction != null)
				{
					multipleResults = false;
				}
			}
			int num = 0;
			if (adorecordsetConstruction != null)
			{
				int num2 = 0;
				object[] array = new object[1];
				OleDbHResult oleDbHResult;
				for (;;)
				{
					string srcTable2 = null;
					if (data is DataSet)
					{
						srcTable2 = OleDbDataAdapter.GetSourceTableName(srcTable, num2);
					}
					bool flag2;
					num += this.FillFromRecordset(data, adorecordsetConstruction, srcTable2, out flag2);
					if (!multipleResults)
					{
						goto IL_121;
					}
					array[0] = DBNull.Value;
					Bid.Trace("<oledb.Recordset15.NextRecordset|API|ADODB>\n");
					object obj;
					object obj2;
					oleDbHResult = ((UnsafeNativeMethods.Recordset15)adodb).NextRecordset(out obj, out obj2);
					Bid.Trace("<oledb.Recordset15.NextRecordset|API|ADODB|RET> %08X{HRESULT}\n", oleDbHResult);
					if (OleDbHResult.S_OK > oleDbHResult)
					{
						break;
					}
					adodb = obj2;
					if (adodb == null)
					{
						goto IL_121;
					}
					Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|ADODB> ADORecordsetConstruction\n");
					adorecordsetConstruction = (UnsafeNativeMethods.ADORecordsetConstruction)adodb;
					if (flag2)
					{
						num2++;
					}
					if (adorecordsetConstruction == null)
					{
						goto IL_121;
					}
				}
				if ((OleDbHResult)(-2146825037) != oleDbHResult)
				{
					UnsafeNativeMethods.IErrorInfo errorInfo = null;
					UnsafeNativeMethods.GetErrorInfo(0, out errorInfo);
					string empty = string.Empty;
					if (errorInfo != null)
					{
						OleDbHResult errorDescription = ODB.GetErrorDescription(errorInfo, oleDbHResult, out empty);
					}
					throw new COMException(empty, (int)oleDbHResult);
				}
				IL_121:
				if (adorecordsetConstruction != null && (flag || adodb == null))
				{
					this.FillClose(true, adorecordsetConstruction);
				}
			}
			else
			{
				if (adorecordConstruction == null)
				{
					throw ODB.Fill_NotADODB("adodb");
				}
				num = this.FillFromRecord(data, adorecordConstruction, srcTable);
				if (flag)
				{
					this.FillClose(false, adorecordConstruction);
				}
			}
			return num;
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x000FCE8C File Offset: 0x000FC28C
		private int FillFromRecordset(object data, UnsafeNativeMethods.ADORecordsetConstruction recordset, string srcTable, out bool incrementResultCount)
		{
			incrementResultCount = false;
			object obj = null;
			IntPtr chapter;
			try
			{
				Bid.Trace("<oledb.ADORecordsetConstruction.get_Rowset|API|ADODB>\n");
				obj = recordset.get_Rowset();
				Bid.Trace("<oledb.ADORecordsetConstruction.get_Rowset|API|ADODB|RET> %08X{HRESULT}\n", 0);
				Bid.Trace("<oledb.ADORecordsetConstruction.get_Chapter|API|ADODB>\n");
				chapter = recordset.get_Chapter();
				Bid.Trace("<oledb.ADORecordsetConstruction.get_Chapter|API|ADODB|RET> %08X{HRESULT}\n", 0);
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ODB.Fill_EmptyRecordSet("ADODBRecordSet", ex);
			}
			if (obj != null)
			{
				CommandBehavior commandBehavior = (MissingSchemaAction.AddWithKey != base.MissingSchemaAction) ? CommandBehavior.Default : CommandBehavior.KeyInfo;
				commandBehavior |= CommandBehavior.SequentialAccess;
				OleDbDataReader oleDbDataReader = null;
				try
				{
					ChapterHandle chapterHandle = ChapterHandle.CreateChapterHandle(chapter);
					oleDbDataReader = new OleDbDataReader(null, null, 0, commandBehavior);
					oleDbDataReader.InitializeIRowset(obj, chapterHandle, ADP.RecordsUnaffected);
					oleDbDataReader.BuildMetaInfo();
					incrementResultCount = (0 < oleDbDataReader.FieldCount);
					if (incrementResultCount)
					{
						if (data is DataTable)
						{
							return base.Fill((DataTable)data, oleDbDataReader);
						}
						return base.Fill((DataSet)data, srcTable, oleDbDataReader, 0, 0);
					}
				}
				finally
				{
					if (oleDbDataReader != null)
					{
						oleDbDataReader.Close();
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x000FCFB8 File Offset: 0x000FC3B8
		private int FillFromRecord(object data, UnsafeNativeMethods.ADORecordConstruction record, string srcTable)
		{
			object obj = null;
			try
			{
				Bid.Trace("<oledb.ADORecordConstruction.get_Row|API|ADODB>\n");
				obj = record.get_Row();
				Bid.Trace("<oledb.ADORecordConstruction.get_Row|API|ADODB|RET> %08X{HRESULT}\n", 0);
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				throw ODB.Fill_EmptyRecord("adodb", ex);
			}
			if (obj != null)
			{
				CommandBehavior commandBehavior = (MissingSchemaAction.AddWithKey != base.MissingSchemaAction) ? CommandBehavior.Default : CommandBehavior.KeyInfo;
				commandBehavior |= (CommandBehavior.SingleRow | CommandBehavior.SequentialAccess);
				OleDbDataReader oleDbDataReader = null;
				try
				{
					oleDbDataReader = new OleDbDataReader(null, null, 0, commandBehavior);
					oleDbDataReader.InitializeIRow(obj, ADP.RecordsUnaffected);
					oleDbDataReader.BuildMetaInfo();
					if (data is DataTable)
					{
						return base.Fill((DataTable)data, oleDbDataReader);
					}
					return base.Fill((DataSet)data, srcTable, oleDbDataReader, 0, 0);
				}
				finally
				{
					if (oleDbDataReader != null)
					{
						oleDbDataReader.Close();
					}
				}
				return 0;
			}
			return 0;
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x000FD0A4 File Offset: 0x000FC4A4
		private void FillClose(bool isrecordset, object value)
		{
			OleDbHResult oleDbHResult;
			if (isrecordset)
			{
				Bid.Trace("<oledb.Recordset15.Close|API|ADODB>\n");
				oleDbHResult = ((UnsafeNativeMethods.Recordset15)value).Close();
				Bid.Trace("<oledb.Recordset15.Close|API|ADODB|RET> %08X{HRESULT}\n", oleDbHResult);
			}
			else
			{
				Bid.Trace("<oledb._ADORecord.Close|API|ADODB>\n");
				oleDbHResult = ((UnsafeNativeMethods._ADORecord)value).Close();
				Bid.Trace("<oledb._ADORecord.Close|API|ADODB|RET> %08X{HRESULT}\n", oleDbHResult);
			}
			if (OleDbHResult.S_OK < oleDbHResult && (OleDbHResult)(-2146824584) != oleDbHResult)
			{
				UnsafeNativeMethods.IErrorInfo errorInfo = null;
				UnsafeNativeMethods.GetErrorInfo(0, out errorInfo);
				string empty = string.Empty;
				if (errorInfo != null)
				{
					OleDbHResult errorDescription = ODB.GetErrorDescription(errorInfo, oleDbHResult, out empty);
				}
				throw new COMException(empty, (int)oleDbHResult);
			}
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x000FD12C File Offset: 0x000FC52C
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			OleDbRowUpdatedEventHandler oleDbRowUpdatedEventHandler = (OleDbRowUpdatedEventHandler)base.Events[OleDbDataAdapter.EventRowUpdated];
			if (oleDbRowUpdatedEventHandler != null && value is OleDbRowUpdatedEventArgs)
			{
				oleDbRowUpdatedEventHandler(this, (OleDbRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000FD170 File Offset: 0x000FC570
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			OleDbRowUpdatingEventHandler oleDbRowUpdatingEventHandler = (OleDbRowUpdatingEventHandler)base.Events[OleDbDataAdapter.EventRowUpdating];
			if (oleDbRowUpdatingEventHandler != null && value is OleDbRowUpdatingEventArgs)
			{
				oleDbRowUpdatingEventHandler(this, (OleDbRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000FD1B4 File Offset: 0x000FC5B4
		private static string GetSourceTableName(string srcTable, int index)
		{
			if (index == 0)
			{
				return srcTable;
			}
			return srcTable + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x040015CB RID: 5579
		private static readonly object EventRowUpdated = new object();

		// Token: 0x040015CC RID: 5580
		private static readonly object EventRowUpdating = new object();

		// Token: 0x040015CD RID: 5581
		private OleDbCommand _deleteCommand;

		// Token: 0x040015CE RID: 5582
		private OleDbCommand _insertCommand;

		// Token: 0x040015CF RID: 5583
		private OleDbCommand _selectCommand;

		// Token: 0x040015D0 RID: 5584
		private OleDbCommand _updateCommand;
	}
}
