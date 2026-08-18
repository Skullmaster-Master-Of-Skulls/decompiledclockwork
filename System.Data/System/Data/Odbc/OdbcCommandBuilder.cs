using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	// Token: 0x020001D5 RID: 469
	public sealed class OdbcCommandBuilder : DbCommandBuilder
	{
		// Token: 0x060019B1 RID: 6577 RVA: 0x0025B188 File Offset: 0x0025A588
		public OdbcCommandBuilder()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0025B1A8 File Offset: 0x0025A5A8
		public OdbcCommandBuilder(OdbcDataAdapter adapter) : this()
		{
			this.DataAdapter = adapter;
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x0025B1C8 File Offset: 0x0025A5C8
		// (set) Token: 0x060019B4 RID: 6580 RVA: 0x0025B1E8 File Offset: 0x0025A5E8
		[DefaultValue(null)]
		[ResCategory("DataCategory_Update")]
		[ResDescription("OdbcCommandBuilder_DataAdapter")]
		public new OdbcDataAdapter DataAdapter
		{
			get
			{
				return base.DataAdapter as OdbcDataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0025B208 File Offset: 0x0025A608
		private void OdbcRowUpdatingHandler(object sender, OdbcRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x0025B228 File Offset: 0x0025A628
		public new OdbcCommand GetInsertCommand()
		{
			return (OdbcCommand)base.GetInsertCommand();
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x0025B248 File Offset: 0x0025A648
		public new OdbcCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0025B268 File Offset: 0x0025A668
		public new OdbcCommand GetUpdateCommand()
		{
			return (OdbcCommand)base.GetUpdateCommand();
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0025B288 File Offset: 0x0025A688
		public new OdbcCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0025B2A8 File Offset: 0x0025A6A8
		public new OdbcCommand GetDeleteCommand()
		{
			return (OdbcCommand)base.GetDeleteCommand();
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0025B2C8 File Offset: 0x0025A6C8
		public new OdbcCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0025B2E8 File Offset: 0x0025A6E8
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0025B318 File Offset: 0x0025A718
		protected override string GetParameterName(string parameterName)
		{
			return parameterName;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0025B328 File Offset: 0x0025A728
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "?";
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0025B348 File Offset: 0x0025A748
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			OdbcParameter odbcParameter = (OdbcParameter)parameter;
			object obj = datarow[SchemaTableColumn.ProviderType];
			odbcParameter.OdbcType = (OdbcType)obj;
			object obj2 = datarow[SchemaTableColumn.NumericPrecision];
			if (DBNull.Value != obj2)
			{
				byte b = (byte)((short)obj2);
				odbcParameter.PrecisionInternal = ((byte.MaxValue != b) ? b : 0);
			}
			obj2 = datarow[SchemaTableColumn.NumericScale];
			if (DBNull.Value != obj2)
			{
				byte b2 = (byte)((short)obj2);
				odbcParameter.ScaleInternal = ((byte.MaxValue != b2) ? b2 : 0);
			}
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x0025B3D8 File Offset: 0x0025A7D8
		public static void DeriveParameters(OdbcCommand command)
		{
			OdbcConnection.ExecutePermission.Demand();
			if (command == null)
			{
				throw ADP.ArgumentNull("command");
			}
			CommandType commandType = command.CommandType;
			if (commandType == CommandType.Text)
			{
				throw ADP.DeriveParametersNotSupported(command);
			}
			if (commandType != CommandType.StoredProcedure)
			{
				if (commandType != CommandType.TableDirect)
				{
					throw ADP.InvalidCommandType(command.CommandType);
				}
				throw ADP.DeriveParametersNotSupported(command);
			}
			else
			{
				if (ADP.IsEmpty(command.CommandText))
				{
					throw ADP.CommandTextRequired("DeriveParameters");
				}
				OdbcConnection connection = command.Connection;
				if (connection == null)
				{
					throw ADP.ConnectionRequired("DeriveParameters");
				}
				ConnectionState state = connection.State;
				if (ConnectionState.Open != state)
				{
					throw ADP.OpenConnectionRequired("DeriveParameters", state);
				}
				OdbcParameter[] array = OdbcCommandBuilder.DeriveParametersFromStoredProcedure(connection, command);
				OdbcParameterCollection parameters = command.Parameters;
				parameters.Clear();
				int num = array.Length;
				if (0 < num)
				{
					for (int i = 0; i < array.Length; i++)
					{
						parameters.Add(array[i]);
					}
				}
				return;
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0025B4B8 File Offset: 0x0025A8B8
		private static OdbcParameter[] DeriveParametersFromStoredProcedure(OdbcConnection connection, OdbcCommand command)
		{
			List<OdbcParameter> list = new List<OdbcParameter>();
			CMDWrapper statementHandle = command.GetStatementHandle();
			OdbcStatementHandle statementHandle2 = statementHandle.StatementHandle;
			string text = connection.QuoteChar("DeriveParameters");
			string[] array = MultipartIdentifier.ParseMultipartIdentifier(command.CommandText, text, text, '.', 4, true, "ODBC_ODBCCommandText", false);
			if (array[3] == null)
			{
				array[3] = command.CommandText;
			}
			ODBC32.RetCode retCode = statementHandle2.ProcedureColumns(array[1], array[2], array[3], null);
			if (retCode != ODBC32.RetCode.SUCCESS)
			{
				connection.HandleError(statementHandle2, retCode);
			}
			using (OdbcDataReader odbcDataReader = new OdbcDataReader(command, statementHandle, CommandBehavior.Default))
			{
				odbcDataReader.FirstResult();
				int fieldCount = odbcDataReader.FieldCount;
				while (odbcDataReader.Read())
				{
					OdbcParameter odbcParameter = new OdbcParameter();
					odbcParameter.ParameterName = odbcDataReader.GetString(3);
					switch (odbcDataReader.GetInt16(4))
					{
					case 1:
						odbcParameter.Direction = ParameterDirection.Input;
						break;
					case 2:
						odbcParameter.Direction = ParameterDirection.InputOutput;
						break;
					case 4:
						odbcParameter.Direction = ParameterDirection.Output;
						break;
					case 5:
						odbcParameter.Direction = ParameterDirection.ReturnValue;
						break;
					}
					odbcParameter.OdbcType = TypeMap.FromSqlType((ODBC32.SQL_TYPE)odbcDataReader.GetInt16(5))._odbcType;
					odbcParameter.Size = odbcDataReader.GetInt32(7);
					switch (odbcParameter.OdbcType)
					{
					case OdbcType.Decimal:
					case OdbcType.Numeric:
						odbcParameter.ScaleInternal = (byte)odbcDataReader.GetInt16(9);
						odbcParameter.PrecisionInternal = (byte)odbcDataReader.GetInt16(10);
						break;
					}
					list.Add(odbcParameter);
				}
			}
			retCode = statementHandle2.CloseCursor();
			return list.ToArray();
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0025B658 File Offset: 0x0025AA58
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			return this.QuoteIdentifier(unquotedIdentifier, null);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0025B678 File Offset: 0x0025AA78
		public string QuoteIdentifier(string unquotedIdentifier, OdbcConnection connection)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string text = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (ADP.IsEmpty(text))
			{
				if (connection == null)
				{
					throw ADP.QuotePrefixNotSet("QuoteIdentifier");
				}
				text = connection.QuoteChar("QuoteIdentifier");
				quoteSuffix = text;
			}
			if (!ADP.IsEmpty(text) && text != " ")
			{
				return ADP.BuildQuotedString(text, quoteSuffix, unquotedIdentifier);
			}
			return unquotedIdentifier;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x0025B6E8 File Offset: 0x0025AAE8
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((OdbcDataAdapter)adapter).RowUpdating -= this.OdbcRowUpdatingHandler;
				return;
			}
			((OdbcDataAdapter)adapter).RowUpdating += this.OdbcRowUpdatingHandler;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x0025B738 File Offset: 0x0025AB38
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			return this.UnquoteIdentifier(quotedIdentifier, null);
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0025B758 File Offset: 0x0025AB58
		public string UnquoteIdentifier(string quotedIdentifier, OdbcConnection connection)
		{
			ADP.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
			string text = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (ADP.IsEmpty(text))
			{
				if (connection == null)
				{
					throw ADP.QuotePrefixNotSet("UnquoteIdentifier");
				}
				text = connection.QuoteChar("UnquoteIdentifier");
				quoteSuffix = text;
			}
			string result;
			if (!ADP.IsEmpty(text) || text != " ")
			{
				ADP.RemoveStringQuotes(text, quoteSuffix, quotedIdentifier, out result);
			}
			else
			{
				result = quotedIdentifier;
			}
			return result;
		}
	}
}
