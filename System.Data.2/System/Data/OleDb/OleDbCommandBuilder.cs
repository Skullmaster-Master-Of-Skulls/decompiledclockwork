using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.OleDb
{
	// Token: 0x02000242 RID: 578
	public sealed class OleDbCommandBuilder : DbCommandBuilder
	{
		// Token: 0x0600242D RID: 9261 RVA: 0x000F8250 File Offset: 0x000F7650
		public OleDbCommandBuilder()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x000F826C File Offset: 0x000F766C
		public OleDbCommandBuilder(OleDbDataAdapter adapter) : this()
		{
			this.DataAdapter = adapter;
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600242F RID: 9263 RVA: 0x000F8288 File Offset: 0x000F7688
		// (set) Token: 0x06002430 RID: 9264 RVA: 0x000F82A0 File Offset: 0x000F76A0
		[ResDescription("OleDbCommandBuilder_DataAdapter")]
		[DefaultValue(null)]
		[ResCategory("DataCategory_Update")]
		public new OleDbDataAdapter DataAdapter
		{
			get
			{
				return base.DataAdapter as OleDbDataAdapter;
			}
			set
			{
				base.DataAdapter = value;
			}
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000F82B4 File Offset: 0x000F76B4
		private void OleDbRowUpdatingHandler(object sender, OleDbRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x000F82C8 File Offset: 0x000F76C8
		public new OleDbCommand GetInsertCommand()
		{
			return (OleDbCommand)base.GetInsertCommand();
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000F82E0 File Offset: 0x000F76E0
		public new OleDbCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (OleDbCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000F82FC File Offset: 0x000F76FC
		public new OleDbCommand GetUpdateCommand()
		{
			return (OleDbCommand)base.GetUpdateCommand();
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000F8314 File Offset: 0x000F7714
		public new OleDbCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (OleDbCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000F8330 File Offset: 0x000F7730
		public new OleDbCommand GetDeleteCommand()
		{
			return (OleDbCommand)base.GetDeleteCommand();
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x000F8348 File Offset: 0x000F7748
		public new OleDbCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (OleDbCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000F8364 File Offset: 0x000F7764
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x000F8388 File Offset: 0x000F7788
		protected override string GetParameterName(string parameterName)
		{
			return parameterName;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000F8398 File Offset: 0x000F7798
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "?";
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000F83AC File Offset: 0x000F77AC
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow datarow, StatementType statementType, bool whereClause)
		{
			OleDbParameter oleDbParameter = (OleDbParameter)parameter;
			object obj = datarow[SchemaTableColumn.ProviderType];
			oleDbParameter.OleDbType = (OleDbType)obj;
			object obj2 = datarow[SchemaTableColumn.NumericPrecision];
			if (DBNull.Value != obj2)
			{
				byte b = (byte)((short)obj2);
				oleDbParameter.PrecisionInternal = ((byte.MaxValue != b) ? b : 0);
			}
			obj2 = datarow[SchemaTableColumn.NumericScale];
			if (DBNull.Value != obj2)
			{
				byte b2 = (byte)((short)obj2);
				oleDbParameter.ScaleInternal = ((byte.MaxValue != b2) ? b2 : 0);
			}
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000F8438 File Offset: 0x000F7838
		public static void DeriveParameters(OleDbCommand command)
		{
			OleDbConnection.ExecutePermission.Demand();
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
				OleDbConnection connection = command.Connection;
				if (connection == null)
				{
					throw ADP.ConnectionRequired("DeriveParameters");
				}
				ConnectionState state = connection.State;
				if (ConnectionState.Open != state)
				{
					throw ADP.OpenConnectionRequired("DeriveParameters", state);
				}
				OleDbParameter[] array = OleDbCommandBuilder.DeriveParametersFromStoredProcedure(connection, command);
				OleDbParameterCollection parameters = command.Parameters;
				parameters.Clear();
				for (int i = 0; i < array.Length; i++)
				{
					parameters.Add(array[i]);
				}
				return;
			}
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x000F8508 File Offset: 0x000F7908
		private static OleDbParameter[] DeriveParametersFromStoredProcedure(OleDbConnection connection, OleDbCommand command)
		{
			OleDbParameter[] array = new OleDbParameter[0];
			if (connection.SupportSchemaRowset(OleDbSchemaGuid.Procedure_Parameters))
			{
				string leftQuote;
				string rightQuote;
				connection.GetLiteralQuotes("DeriveParameters", out leftQuote, out rightQuote);
				object[] array2 = MultipartIdentifier.ParseMultipartIdentifier(command.CommandText, leftQuote, rightQuote, '.', 4, true, "OLEDB_OLEDBCommandText", false);
				object[] array3 = array2;
				if (array3[3] == null)
				{
					throw ADP.NoStoredProcedureExists(command.CommandText);
				}
				object[] array4 = new object[4];
				Array.Copy(array3, 1, array4, 0, 3);
				DataTable schemaRowset = connection.GetSchemaRowset(OleDbSchemaGuid.Procedure_Parameters, array4);
				if (schemaRowset != null)
				{
					DataColumnCollection columns = schemaRowset.Columns;
					DataColumn dataColumn = null;
					DataColumn dataColumn2 = null;
					DataColumn dataColumn3 = null;
					DataColumn dataColumn4 = null;
					DataColumn dataColumn5 = null;
					DataColumn dataColumn6 = null;
					DataColumn column = null;
					int i = columns.IndexOf("PARAMETER_NAME");
					if (-1 != i)
					{
						dataColumn = columns[i];
					}
					i = columns.IndexOf("PARAMETER_TYPE");
					if (-1 != i)
					{
						dataColumn2 = columns[i];
					}
					i = columns.IndexOf("DATA_TYPE");
					if (-1 != i)
					{
						dataColumn3 = columns[i];
					}
					i = columns.IndexOf("CHARACTER_MAXIMUM_LENGTH");
					if (-1 != i)
					{
						dataColumn4 = columns[i];
					}
					i = columns.IndexOf("NUMERIC_PRECISION");
					if (-1 != i)
					{
						dataColumn5 = columns[i];
					}
					i = columns.IndexOf("NUMERIC_SCALE");
					if (-1 != i)
					{
						dataColumn6 = columns[i];
					}
					i = columns.IndexOf("TYPE_NAME");
					if (-1 != i)
					{
						column = columns[i];
					}
					DataRow[] array5 = schemaRowset.Select(null, "ORDINAL_POSITION ASC", DataViewRowState.CurrentRows);
					array = new OleDbParameter[array5.Length];
					i = 0;
					while (i < array5.Length)
					{
						DataRow dataRow = array5[i];
						OleDbParameter oleDbParameter = new OleDbParameter();
						if (dataColumn != null && !dataRow.IsNull(dataColumn, DataRowVersion.Default))
						{
							oleDbParameter.ParameterName = Convert.ToString(dataRow[dataColumn, DataRowVersion.Default], CultureInfo.InvariantCulture).TrimStart(new char[]
							{
								'@',
								' ',
								':'
							});
						}
						if (dataColumn2 != null && !dataRow.IsNull(dataColumn2, DataRowVersion.Default))
						{
							short value = Convert.ToInt16(dataRow[dataColumn2, DataRowVersion.Default], CultureInfo.InvariantCulture);
							oleDbParameter.Direction = OleDbCommandBuilder.ConvertToParameterDirection((int)value);
						}
						if (dataColumn3 != null && !dataRow.IsNull(dataColumn3, DataRowVersion.Default))
						{
							short dbType = Convert.ToInt16(dataRow[dataColumn3, DataRowVersion.Default], CultureInfo.InvariantCulture);
							oleDbParameter.OleDbType = NativeDBType.FromDBType(dbType, false, false).enumOleDbType;
						}
						if (dataColumn4 != null && !dataRow.IsNull(dataColumn4, DataRowVersion.Default))
						{
							oleDbParameter.Size = Convert.ToInt32(dataRow[dataColumn4, DataRowVersion.Default], CultureInfo.InvariantCulture);
						}
						OleDbType oleDbType = oleDbParameter.OleDbType;
						if (oleDbType <= OleDbType.Numeric)
						{
							if (oleDbType == OleDbType.Decimal || oleDbType == OleDbType.Numeric)
							{
								goto IL_2BF;
							}
						}
						else
						{
							if (oleDbType == OleDbType.VarNumeric)
							{
								goto IL_2BF;
							}
							switch (oleDbType)
							{
							case OleDbType.VarChar:
							case OleDbType.VarWChar:
							case OleDbType.VarBinary:
							{
								object obj = dataRow[column, DataRowVersion.Default];
								if (obj is string)
								{
									string a = ((string)obj).ToLower(CultureInfo.InvariantCulture);
									if (!(a == "binary"))
									{
										if (!(a == "image"))
										{
											if (!(a == "char"))
											{
												if (!(a == "text"))
												{
													if (!(a == "nchar"))
													{
														if (a == "ntext")
														{
															oleDbParameter.OleDbType = OleDbType.LongVarWChar;
														}
													}
													else
													{
														oleDbParameter.OleDbType = OleDbType.WChar;
													}
												}
												else
												{
													oleDbParameter.OleDbType = OleDbType.LongVarChar;
												}
											}
											else
											{
												oleDbParameter.OleDbType = OleDbType.Char;
											}
										}
										else
										{
											oleDbParameter.OleDbType = OleDbType.LongVarBinary;
										}
									}
									else
									{
										oleDbParameter.OleDbType = OleDbType.Binary;
									}
								}
								break;
							}
							}
						}
						IL_3F2:
						array[i] = oleDbParameter;
						i++;
						continue;
						IL_2BF:
						if (dataColumn5 != null && !dataRow.IsNull(dataColumn5, DataRowVersion.Default))
						{
							oleDbParameter.PrecisionInternal = (byte)Convert.ToInt16(dataRow[dataColumn5], CultureInfo.InvariantCulture);
						}
						if (dataColumn6 != null && !dataRow.IsNull(dataColumn6, DataRowVersion.Default))
						{
							oleDbParameter.ScaleInternal = (byte)Convert.ToInt16(dataRow[dataColumn6], CultureInfo.InvariantCulture);
							goto IL_3F2;
						}
						goto IL_3F2;
					}
				}
				if (array.Length == 0 && connection.SupportSchemaRowset(OleDbSchemaGuid.Procedures))
				{
					object[] array6 = new object[4];
					array6[2] = command.CommandText;
					array4 = array6;
					schemaRowset = connection.GetSchemaRowset(OleDbSchemaGuid.Procedures, array4);
					if (schemaRowset.Rows.Count == 0)
					{
						throw ADP.NoStoredProcedureExists(command.CommandText);
					}
				}
				return array;
			}
			else
			{
				if (!connection.SupportSchemaRowset(OleDbSchemaGuid.Procedures))
				{
					throw ODB.NoProviderSupportForSProcResetParameters(connection.Provider);
				}
				object[] array7 = new object[4];
				array7[2] = command.CommandText;
				object[] restrictions = array7;
				DataTable schemaRowset2 = connection.GetSchemaRowset(OleDbSchemaGuid.Procedures, restrictions);
				if (schemaRowset2.Rows.Count == 0)
				{
					throw ADP.NoStoredProcedureExists(command.CommandText);
				}
				throw ODB.NoProviderSupportForSProcResetParameters(connection.Provider);
			}
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000F89D0 File Offset: 0x000F7DD0
		private static ParameterDirection ConvertToParameterDirection(int value)
		{
			switch (value)
			{
			case 1:
				return ParameterDirection.Input;
			case 2:
				return ParameterDirection.InputOutput;
			case 3:
				return ParameterDirection.Output;
			case 4:
				return ParameterDirection.ReturnValue;
			default:
				return ParameterDirection.Input;
			}
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x000F8A00 File Offset: 0x000F7E00
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			return this.QuoteIdentifier(unquotedIdentifier, null);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x000F8A18 File Offset: 0x000F7E18
		public string QuoteIdentifier(string unquotedIdentifier, OleDbConnection connection)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string quotePrefix = this.QuotePrefix;
			string text = this.QuoteSuffix;
			if (ADP.IsEmpty(quotePrefix))
			{
				if (connection == null)
				{
					connection = (base.GetConnection() as OleDbConnection);
					if (connection == null)
					{
						throw ADP.QuotePrefixNotSet("QuoteIdentifier");
					}
				}
				connection.GetLiteralQuotes("QuoteIdentifier", out quotePrefix, out text);
				if (text == null)
				{
					text = quotePrefix;
				}
			}
			return ADP.BuildQuotedString(quotePrefix, text, unquotedIdentifier);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x000F8A80 File Offset: 0x000F7E80
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((OleDbDataAdapter)adapter).RowUpdating -= this.OleDbRowUpdatingHandler;
				return;
			}
			((OleDbDataAdapter)adapter).RowUpdating += this.OleDbRowUpdatingHandler;
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000F8AC8 File Offset: 0x000F7EC8
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			return this.UnquoteIdentifier(quotedIdentifier, null);
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000F8AE0 File Offset: 0x000F7EE0
		public string UnquoteIdentifier(string quotedIdentifier, OleDbConnection connection)
		{
			ADP.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
			string quotePrefix = this.QuotePrefix;
			string text = this.QuoteSuffix;
			if (ADP.IsEmpty(quotePrefix))
			{
				if (connection == null)
				{
					connection = (base.GetConnection() as OleDbConnection);
					if (connection == null)
					{
						throw ADP.QuotePrefixNotSet("UnquoteIdentifier");
					}
				}
				connection.GetLiteralQuotes("UnquoteIdentifier", out quotePrefix, out text);
				if (text == null)
				{
					text = quotePrefix;
				}
			}
			string result;
			ADP.RemoveStringQuotes(quotePrefix, text, quotedIdentifier, out result);
			return result;
		}
	}
}
