using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data.OleDb
{
	// Token: 0x02000213 RID: 531
	public sealed class OleDbCommandBuilder : DbCommandBuilder
	{
		// Token: 0x06001E03 RID: 7683 RVA: 0x00271F48 File Offset: 0x00271348
		public OleDbCommandBuilder()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x00271F68 File Offset: 0x00271368
		public OleDbCommandBuilder(OleDbDataAdapter adapter) : this()
		{
			this.DataAdapter = adapter;
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00271F88 File Offset: 0x00271388
		// (set) Token: 0x06001E06 RID: 7686 RVA: 0x00271FA8 File Offset: 0x002713A8
		[DefaultValue(null)]
		[ResDescription("OleDbCommandBuilder_DataAdapter")]
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

		// Token: 0x06001E07 RID: 7687 RVA: 0x00271FC8 File Offset: 0x002713C8
		private void OleDbRowUpdatingHandler(object sender, OleDbRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00271FE8 File Offset: 0x002713E8
		public new OleDbCommand GetInsertCommand()
		{
			return (OleDbCommand)base.GetInsertCommand();
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00272008 File Offset: 0x00271408
		public new OleDbCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			return (OleDbCommand)base.GetInsertCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x00272028 File Offset: 0x00271428
		public new OleDbCommand GetUpdateCommand()
		{
			return (OleDbCommand)base.GetUpdateCommand();
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x00272048 File Offset: 0x00271448
		public new OleDbCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			return (OleDbCommand)base.GetUpdateCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x00272068 File Offset: 0x00271468
		public new OleDbCommand GetDeleteCommand()
		{
			return (OleDbCommand)base.GetDeleteCommand();
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x00272088 File Offset: 0x00271488
		public new OleDbCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			return (OleDbCommand)base.GetDeleteCommand(useColumnsForParameterNames);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x002720A8 File Offset: 0x002714A8
		protected override string GetParameterName(int parameterOrdinal)
		{
			return "p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x002720D8 File Offset: 0x002714D8
		protected override string GetParameterName(string parameterName)
		{
			return parameterName;
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x002720E8 File Offset: 0x002714E8
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "?";
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x00272108 File Offset: 0x00271508
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

		// Token: 0x06001E12 RID: 7698 RVA: 0x00272198 File Offset: 0x00271598
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

		// Token: 0x06001E13 RID: 7699 RVA: 0x00272268 File Offset: 0x00271668
		private static OleDbParameter[] DeriveParametersFromStoredProcedure(OleDbConnection connection, OleDbCommand command)
		{
			OleDbParameter[] array = new OleDbParameter[0];
			if (connection.SupportSchemaRowset(OleDbSchemaGuid.Procedure_Parameters))
			{
				string leftQuote;
				string rightQuote;
				connection.GetLiteralQuotes("DeriveParameters", out leftQuote, out rightQuote);
				object[] array2 = MultipartIdentifier.ParseMultipartIdentifier(command.CommandText, leftQuote, rightQuote, '.', 4, true, "OLEDB_OLEDBCommandText", false);
				if (array2[3] == null)
				{
					throw ADP.NoStoredProcedureExists(command.CommandText);
				}
				object[] array3 = new object[4];
				Array.Copy(array2, 1, array3, 0, 3);
				DataTable schemaRowset = connection.GetSchemaRowset(OleDbSchemaGuid.Procedure_Parameters, array3);
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
					DataRow[] array4 = schemaRowset.Select(null, "ORDINAL_POSITION ASC", DataViewRowState.CurrentRows);
					array = new OleDbParameter[array4.Length];
					i = 0;
					while (i < array4.Length)
					{
						DataRow dataRow = array4[i];
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
								goto IL_2BB;
							}
						}
						else
						{
							if (oleDbType == OleDbType.VarNumeric)
							{
								goto IL_2BB;
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
									string text = ((string)obj).ToLower(CultureInfo.InvariantCulture);
									string a;
									if ((a = text) != null)
									{
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
								}
								break;
							}
							}
						}
						IL_3F8:
						array[i] = oleDbParameter;
						i++;
						continue;
						IL_2BB:
						if (dataColumn5 != null && !dataRow.IsNull(dataColumn5, DataRowVersion.Default))
						{
							oleDbParameter.PrecisionInternal = (byte)Convert.ToInt16(dataRow[dataColumn5], CultureInfo.InvariantCulture);
						}
						if (dataColumn6 != null && !dataRow.IsNull(dataColumn6, DataRowVersion.Default))
						{
							oleDbParameter.ScaleInternal = (byte)Convert.ToInt16(dataRow[dataColumn6], CultureInfo.InvariantCulture);
							goto IL_3F8;
						}
						goto IL_3F8;
					}
				}
				if (array.Length == 0 && connection.SupportSchemaRowset(OleDbSchemaGuid.Procedures))
				{
					object[] array5 = new object[4];
					array5[2] = command.CommandText;
					array3 = array5;
					schemaRowset = connection.GetSchemaRowset(OleDbSchemaGuid.Procedures, array3);
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
				object[] array6 = new object[4];
				array6[2] = command.CommandText;
				object[] restrictions = array6;
				DataTable schemaRowset2 = connection.GetSchemaRowset(OleDbSchemaGuid.Procedures, restrictions);
				if (schemaRowset2.Rows.Count == 0)
				{
					throw ADP.NoStoredProcedureExists(command.CommandText);
				}
				throw ODB.NoProviderSupportForSProcResetParameters(connection.Provider);
			}
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00272748 File Offset: 0x00271B48
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

		// Token: 0x06001E15 RID: 7701 RVA: 0x00272788 File Offset: 0x00271B88
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			return this.QuoteIdentifier(unquotedIdentifier, null);
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x002727A8 File Offset: 0x00271BA8
		public string QuoteIdentifier(string unquotedIdentifier, OleDbConnection connection)
		{
			ADP.CheckArgumentNull(unquotedIdentifier, "unquotedIdentifier");
			string quotePrefix = this.QuotePrefix;
			string text = this.QuoteSuffix;
			if (ADP.IsEmpty(quotePrefix))
			{
				if (connection == null)
				{
					throw ADP.QuotePrefixNotSet("QuoteIdentifier");
				}
				connection.GetLiteralQuotes("QuoteIdentifier", out quotePrefix, out text);
				if (text == null)
				{
					text = quotePrefix;
				}
			}
			return ADP.BuildQuotedString(quotePrefix, text, unquotedIdentifier);
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00272808 File Offset: 0x00271C08
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((OleDbDataAdapter)adapter).RowUpdating -= this.OleDbRowUpdatingHandler;
				return;
			}
			((OleDbDataAdapter)adapter).RowUpdating += this.OleDbRowUpdatingHandler;
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00272858 File Offset: 0x00271C58
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			return this.UnquoteIdentifier(quotedIdentifier, null);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00272878 File Offset: 0x00271C78
		public string UnquoteIdentifier(string quotedIdentifier, OleDbConnection connection)
		{
			ADP.CheckArgumentNull(quotedIdentifier, "quotedIdentifier");
			string quotePrefix = this.QuotePrefix;
			string text = this.QuoteSuffix;
			if (ADP.IsEmpty(quotePrefix))
			{
				if (connection == null)
				{
					throw ADP.QuotePrefixNotSet("UnquoteIdentifier");
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
