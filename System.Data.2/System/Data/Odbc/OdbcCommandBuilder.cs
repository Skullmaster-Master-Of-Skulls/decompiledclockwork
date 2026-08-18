using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	// Token: 0x0200028F RID: 655
	public sealed class OdbcCommandBuilder : DbCommandBuilder
	{
		// Token: 0x06002780 RID: 10112 RVA: 0x0010A954 File Offset: 0x00109D54
		public OdbcCommandBuilder()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x0010A970 File Offset: 0x00109D70
		public OdbcCommandBuilder(OdbcDataAdapter adapter) : this()
		{
			this.DataAdapter = adapter;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x0010A98C File Offset: 0x00109D8C
		// (set) Token: 0x06002783 RID: 10115 RVA: 0x0010A9A4 File Offset: 0x00109DA4
		[ResDescription("OdbcCommandBuilder_DataAdapter")]
		[DefaultValue(null)]
		[ResCategory("DataCategory_Update")]
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

		// Token: 0x06002784 RID: 10116 RVA: 0x0010A9B8 File Offset: 0x00109DB8
		private void OdbcRowUpdatingHandler(object sender, OdbcRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x0010A9CC File Offset: 0x00109DCC
		public new OdbcCommand GetInsertCommand()
		{
			return (OdbcCommand)base.GetInsertCommand();
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0010A9E4 File Offset: 0x00109DE4
		public new OdbcCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0010AA00 File Offset: 0x00109E00
		public new OdbcCommand GetUpdateCommand()
		{
			return (OdbcCommand)base.GetUpdateCommand();
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0010AA18 File Offset: 0x00109E18
		public new OdbcCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0010AA34 File Offset: 0x00109E34
		public new OdbcCommand GetDeleteCommand()
		{
			return (OdbcCommand)base.GetDeleteCommand();
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x0010AA4C File Offset: 0x00109E4C
		public new OdbcCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (OdbcCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x0010AA68 File Offset: 0x00109E68
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x0010AA8C File Offset: 0x00109E8C
		protected override string GetParameterName(string parameterName)
		{
			return parameterName;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x0010AA9C File Offset: 0x00109E9C
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "?";
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x0010AAB0 File Offset: 0x00109EB0
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

		// Token: 0x0600278F RID: 10127 RVA: 0x0010AB3C File Offset: 0x00109F3C
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

		// Token: 0x06002790 RID: 10128 RVA: 0x0010AC18 File Offset: 0x0010A018
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
					OdbcType odbcType = odbcParameter.OdbcType;
					if (odbcType - OdbcType.Decimal <= 1)
					{
						odbcParameter.ScaleInternal = (byte)odbcDataReader.GetInt16(9);
						odbcParameter.PrecisionInternal = (byte)odbcDataReader.GetInt16(10);
					}
					list.Add(odbcParameter);
				}
			}
			retCode = statementHandle2.CloseCursor();
			return list.ToArray();
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x0010ADA8 File Offset: 0x0010A1A8
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			return this.QuoteIdentifier(unquotedIdentifier, null);
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x0010ADC0 File Offset: 0x0010A1C0
		public string QuoteIdentifier(string unquotedIdentifier, OdbcConnection connection)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string text = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (ADP.IsEmpty(text))
			{
				if (connection == null)
				{
					connection = (base.GetConnection() as OdbcConnection);
					if (connection == null)
					{
						throw ADP.QuotePrefixNotSet("QuoteIdentifier");
					}
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

		// Token: 0x06002793 RID: 10131 RVA: 0x0010AE3C File Offset: 0x0010A23C
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((OdbcDataAdapter)adapter).RowUpdating -= this.OdbcRowUpdatingHandler;
				return;
			}
			((OdbcDataAdapter)adapter).RowUpdating += this.OdbcRowUpdatingHandler;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x0010AE84 File Offset: 0x0010A284
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			return this.UnquoteIdentifier(quotedIdentifier, null);
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x0010AE9C File Offset: 0x0010A29C
		public string UnquoteIdentifier(string quotedIdentifier, OdbcConnection connection)
		{
			ADP.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
			string text = this.QuotePrefix;
			string quoteSuffix = this.QuoteSuffix;
			if (ADP.IsEmpty(text))
			{
				if (connection == null)
				{
					connection = (base.GetConnection() as OdbcConnection);
					if (connection == null)
					{
						throw ADP.QuotePrefixNotSet("UnquoteIdentifier");
					}
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
