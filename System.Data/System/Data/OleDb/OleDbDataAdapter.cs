using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Data.OleDb
{
	// Token: 0x02000221 RID: 545
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.OleDbDataAdapterToolboxItem, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("Microsoft.VSDesigner.Data.VS.OleDbDataAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RowUpdated")]
	public sealed class OleDbDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, ICloneable
	{
		// Token: 0x06001EED RID: 7917 RVA: 0x00276FD8 File Offset: 0x002763D8
		public OleDbDataAdapter()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00276FF8 File Offset: 0x002763F8
		public OleDbDataAdapter(OleDbCommand selectCommand) : this()
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00277018 File Offset: 0x00276418
		public OleDbDataAdapter(string selectCommandText, string selectConnectionString) : this()
		{
			OleDbConnection connection = new OleDbConnection(selectConnectionString);
			this.SelectCommand = new OleDbCommand(selectCommandText, connection);
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00277048 File Offset: 0x00276448
		public OleDbDataAdapter(string selectCommandText, OleDbConnection selectConnection) : this()
		{
			this.SelectCommand = new OleDbCommand(selectCommandText, selectConnection);
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00277068 File Offset: 0x00276468
		private OleDbDataAdapter(OleDbDataAdapter from) : base(from)
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00277088 File Offset: 0x00276488
		// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x002770A8 File Offset: 0x002764A8
		[ResDescription("DbDataAdapter_DeleteCommand")]
		[DefaultValue(null)]
		[ResCategory("DataCategory_Update")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001EF4 RID: 7924 RVA: 0x002770C8 File Offset: 0x002764C8
		// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x002770E8 File Offset: 0x002764E8
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

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x00277108 File Offset: 0x00276508
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x00277128 File Offset: 0x00276528
		[ResDescription("DbDataAdapter_InsertCommand")]
		[ResCategory("DataCategory_Update")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x00277148 File Offset: 0x00276548
		// (set) Token: 0x06001EF9 RID: 7929 RVA: 0x00277168 File Offset: 0x00276568
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

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x00277188 File Offset: 0x00276588
		// (set) Token: 0x06001EFB RID: 7931 RVA: 0x002771A8 File Offset: 0x002765A8
		[ResCategory("DataCategory_Fill")]
		[ResDescription("DbDataAdapter_SelectCommand")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x002771C8 File Offset: 0x002765C8
		// (set) Token: 0x06001EFD RID: 7933 RVA: 0x002771E8 File Offset: 0x002765E8
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

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00277208 File Offset: 0x00276608
		// (set) Token: 0x06001EFF RID: 7935 RVA: 0x00277228 File Offset: 0x00276628
		[DefaultValue(null)]
		[ResDescription("DbDataAdapter_UpdateCommand")]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[ResCategory("DataCategory_Update")]
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

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00277248 File Offset: 0x00276648
		// (set) Token: 0x06001F01 RID: 7937 RVA: 0x00277268 File Offset: 0x00276668
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
		// (add) Token: 0x06001F02 RID: 7938 RVA: 0x00277288 File Offset: 0x00276688
		// (remove) Token: 0x06001F03 RID: 7939 RVA: 0x002772A8 File Offset: 0x002766A8
		[ResCategory("DataCategory_Update")]
		[ResDescription("DbDataAdapter_RowUpdated")]
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
		// (add) Token: 0x06001F04 RID: 7940 RVA: 0x002772C8 File Offset: 0x002766C8
		// (remove) Token: 0x06001F05 RID: 7941 RVA: 0x00277338 File Offset: 0x00276738
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

		// Token: 0x06001F06 RID: 7942 RVA: 0x00277358 File Offset: 0x00276758
		object ICloneable.Clone()
		{
			return new OleDbDataAdapter(this);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00277378 File Offset: 0x00276778
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OleDbRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x00277398 File Offset: 0x00276798
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OleDbRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x002773B8 File Offset: 0x002767B8
		internal static void FillDataTable(OleDbDataReader dataReader, params DataTable[] dataTables)
		{
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
			oleDbDataAdapter.Fill(dataTables, dataReader, 0, 0);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x002773D8 File Offset: 0x002767D8
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

		// Token: 0x06001F0B RID: 7947 RVA: 0x00277478 File Offset: 0x00276878
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

		// Token: 0x06001F0C RID: 7948 RVA: 0x00277528 File Offset: 0x00276928
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
						goto IL_120;
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
						goto IL_120;
					}
					Bid.Trace("<oledb.IUnknown.QueryInterface|API|OLEDB|ADODB> ADORecordsetConstruction\n");
					adorecordsetConstruction = (UnsafeNativeMethods.ADORecordsetConstruction)adodb;
					if (flag2)
					{
						num2++;
					}
					if (adorecordsetConstruction == null)
					{
						goto IL_120;
					}
				}
				if ((OleDbHResult)(-2146825037) != oleDbHResult)
				{
					UnsafeNativeMethods.IErrorInfo errorInfo = null;
					UnsafeNativeMethods.GetErrorInfo(0, out errorInfo);
					string empty = string.Empty;
					if (errorInfo != null)
					{
						ODB.GetErrorDescription(errorInfo, oleDbHResult, out empty);
					}
					throw new COMException(empty, (int)oleDbHResult);
				}
				IL_120:
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

		// Token: 0x06001F0D RID: 7949 RVA: 0x00277698 File Offset: 0x00276A98
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

		// Token: 0x06001F0E RID: 7950 RVA: 0x002777C8 File Offset: 0x00276BC8
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

		// Token: 0x06001F0F RID: 7951 RVA: 0x002778B8 File Offset: 0x00276CB8
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
					ODB.GetErrorDescription(errorInfo, oleDbHResult, out empty);
				}
				throw new COMException(empty, (int)oleDbHResult);
			}
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x00277948 File Offset: 0x00276D48
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			OleDbRowUpdatedEventHandler oleDbRowUpdatedEventHandler = (OleDbRowUpdatedEventHandler)base.Events[OleDbDataAdapter.EventRowUpdated];
			if (oleDbRowUpdatedEventHandler != null && value is OleDbRowUpdatedEventArgs)
			{
				oleDbRowUpdatedEventHandler(this, (OleDbRowUpdatedEventArgs)value);
			}
			base.OnRowUpdated(value);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x00277998 File Offset: 0x00276D98
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			OleDbRowUpdatingEventHandler oleDbRowUpdatingEventHandler = (OleDbRowUpdatingEventHandler)base.Events[OleDbDataAdapter.EventRowUpdating];
			if (oleDbRowUpdatingEventHandler != null && value is OleDbRowUpdatingEventArgs)
			{
				oleDbRowUpdatingEventHandler(this, (OleDbRowUpdatingEventArgs)value);
			}
			base.OnRowUpdating(value);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x002779E8 File Offset: 0x00276DE8
		private static string GetSourceTableName(string srcTable, int index)
		{
			if (index == 0)
			{
				return srcTable;
			}
			return srcTable + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x040012B6 RID: 4790
		private static readonly object EventRowUpdated = new object();

		// Token: 0x040012B7 RID: 4791
		private static readonly object EventRowUpdating = new object();

		// Token: 0x040012B8 RID: 4792
		private OleDbCommand _deleteCommand;

		// Token: 0x040012B9 RID: 4793
		private OleDbCommand _insertCommand;

		// Token: 0x040012BA RID: 4794
		private OleDbCommand _selectCommand;

		// Token: 0x040012BB RID: 4795
		private OleDbCommand _updateCommand;
	}
}
