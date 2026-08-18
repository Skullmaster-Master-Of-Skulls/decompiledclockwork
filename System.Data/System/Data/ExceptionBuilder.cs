using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Security;
using System.Xml;

namespace System.Data
{
	// Token: 0x0200007A RID: 122
	internal static class ExceptionBuilder
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x001EDAF8 File Offset: 0x001ECEF8
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				Bid.Trace(trace, e.Message);
				if (Bid.AdvancedOn)
				{
					try
					{
						Bid.Trace(", StackTrace='%ls'", Environment.StackTrace);
					}
					catch (SecurityException)
					{
					}
				}
				Bid.Trace("\n");
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x001EDB58 File Offset: 0x001ECF58
		internal static void TraceExceptionAsReturnValue(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|THROW> Message='%ls'", e);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x001EDB78 File Offset: 0x001ECF78
		internal static void TraceExceptionForCapture(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|CATCH> Message='%ls'", e);
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x001EDB98 File Offset: 0x001ECF98
		internal static void TraceExceptionWithoutRethrow(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|CATCH> Message='%ls'", e);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x001EDBB8 File Offset: 0x001ECFB8
		internal static ArgumentException _Argument(string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x001EDBD8 File Offset: 0x001ECFD8
		internal static ArgumentException _Argument(string paramName, string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x001EDBF8 File Offset: 0x001ECFF8
		internal static ArgumentException _Argument(string error, Exception innerException)
		{
			ArgumentException ex = new ArgumentException(error, innerException);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x001EDC18 File Offset: 0x001ED018
		private static ArgumentNullException _ArgumentNull(string paramName, string msg)
		{
			ArgumentNullException ex = new ArgumentNullException(paramName, msg);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x001EDC38 File Offset: 0x001ED038
		internal static ArgumentOutOfRangeException _ArgumentOutOfRange(string paramName, string msg)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(paramName, msg);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x001EDC58 File Offset: 0x001ED058
		internal static Exception _ConfigurationErrors(string message, XmlNode node)
		{
			ConfigurationErrorsException ex = new ConfigurationErrorsException(message, node);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x001EDC78 File Offset: 0x001ED078
		private static IndexOutOfRangeException _IndexOutOfRange(string error)
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x001EDC98 File Offset: 0x001ED098
		private static InvalidOperationException _InvalidOperation(string error)
		{
			InvalidOperationException ex = new InvalidOperationException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x001EDCB8 File Offset: 0x001ED0B8
		private static InvalidEnumArgumentException _InvalidEnumArgumentException(string error)
		{
			InvalidEnumArgumentException ex = new InvalidEnumArgumentException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x001EDCD8 File Offset: 0x001ED0D8
		private static InvalidEnumArgumentException _InvalidEnumArgumentException<T>(T value)
		{
			string @string = Res.GetString("ADP_InvalidEnumerationValue", new object[]
			{
				typeof(T).Name,
				value.ToString()
			});
			return ExceptionBuilder._InvalidEnumArgumentException(@string);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x001EDD28 File Offset: 0x001ED128
		private static DataException _Data(string error)
		{
			DataException ex = new DataException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x001EDD48 File Offset: 0x001ED148
		private static DataException _Data(string error, Exception innerException)
		{
			DataException ex = new DataException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x001EDD68 File Offset: 0x001ED168
		private static ConstraintException _Constraint(string error)
		{
			ConstraintException ex = new ConstraintException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x001EDD88 File Offset: 0x001ED188
		private static InvalidConstraintException _InvalidConstraint(string error)
		{
			InvalidConstraintException ex = new InvalidConstraintException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x001EDDA8 File Offset: 0x001ED1A8
		private static DeletedRowInaccessibleException _DeletedRowInaccessible(string error)
		{
			DeletedRowInaccessibleException ex = new DeletedRowInaccessibleException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x001EDDC8 File Offset: 0x001ED1C8
		private static DuplicateNameException _DuplicateName(string error)
		{
			DuplicateNameException ex = new DuplicateNameException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x001EDDE8 File Offset: 0x001ED1E8
		private static InRowChangingEventException _InRowChangingEvent(string error)
		{
			InRowChangingEventException ex = new InRowChangingEventException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x001EDE08 File Offset: 0x001ED208
		private static MissingPrimaryKeyException _MissingPrimaryKey(string error)
		{
			MissingPrimaryKeyException ex = new MissingPrimaryKeyException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x001EDE28 File Offset: 0x001ED228
		private static NoNullAllowedException _NoNullAllowed(string error)
		{
			NoNullAllowedException ex = new NoNullAllowedException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x001EDE48 File Offset: 0x001ED248
		private static ReadOnlyException _ReadOnly(string error)
		{
			ReadOnlyException ex = new ReadOnlyException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x001EDE68 File Offset: 0x001ED268
		private static RowNotInTableException _RowNotInTable(string error)
		{
			RowNotInTableException ex = new RowNotInTableException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x001EDE88 File Offset: 0x001ED288
		private static VersionNotFoundException _VersionNotFound(string error)
		{
			VersionNotFoundException ex = new VersionNotFoundException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x001EDEA8 File Offset: 0x001ED2A8
		public static Exception ArgumentNull(string paramName)
		{
			return ExceptionBuilder._ArgumentNull(paramName, Res.GetString("Data_ArgumentNull", new object[]
			{
				paramName
			}));
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x001EDED8 File Offset: 0x001ED2D8
		public static Exception ArgumentOutOfRange(string paramName)
		{
			return ExceptionBuilder._ArgumentOutOfRange(paramName, Res.GetString("Data_ArgumentOutOfRange", new object[]
			{
				paramName
			}));
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x001EDF08 File Offset: 0x001ED308
		public static Exception BadObjectPropertyAccess(string error)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataConstraint_BadObjectPropertyAccess", new object[]
			{
				error
			}));
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x001EDF38 File Offset: 0x001ED338
		public static Exception ArgumentContainsNull(string paramName)
		{
			return ExceptionBuilder._Argument(paramName, Res.GetString("Data_ArgumentContainsNull", new object[]
			{
				paramName
			}));
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x001EDF68 File Offset: 0x001ED368
		public static Exception TypeNotAllowed(Type type)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("Data_TypeNotAllowed", new object[]
			{
				type.AssemblyQualifiedName
			}));
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x001EDF98 File Offset: 0x001ED398
		public static Exception ConfigElementNotAllowed(XmlNode configNode)
		{
			return ExceptionBuilder._ConfigurationErrors(Res.GetString("Config_ElementNotAllowed", new object[]
			{
				configNode.Name
			}), configNode);
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x001EDFC8 File Offset: 0x001ED3C8
		public static Exception CannotModifyCollection()
		{
			return ExceptionBuilder._Argument(Res.GetString("Data_CannotModifyCollection"));
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x001EDFE8 File Offset: 0x001ED3E8
		public static Exception CaseInsensitiveNameConflict(string name)
		{
			return ExceptionBuilder._Argument(Res.GetString("Data_CaseInsensitiveNameConflict", new object[]
			{
				name
			}));
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x001EE018 File Offset: 0x001ED418
		public static Exception NamespaceNameConflict(string name)
		{
			return ExceptionBuilder._Argument(Res.GetString("Data_NamespaceNameConflict", new object[]
			{
				name
			}));
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x001EE048 File Offset: 0x001ED448
		public static Exception InvalidOffsetLength()
		{
			return ExceptionBuilder._Argument(Res.GetString("Data_InvalidOffsetLength"));
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x001EE068 File Offset: 0x001ED468
		public static Exception ColumnNotInTheTable(string column, string table)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_NotInTheTable", new object[]
			{
				column,
				table
			}));
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x001EE098 File Offset: 0x001ED498
		public static Exception ColumnNotInAnyTable()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_NotInAnyTable"));
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x001EE0B8 File Offset: 0x001ED4B8
		public static Exception ColumnOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataColumns_OutOfRange", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x001EE0F8 File Offset: 0x001ED4F8
		public static Exception ColumnOutOfRange(string column)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataColumns_OutOfRange", new object[]
			{
				column
			}));
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x001EE128 File Offset: 0x001ED528
		public static Exception CannotAddColumn1(string column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_Add1", new object[]
			{
				column
			}));
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x001EE158 File Offset: 0x001ED558
		public static Exception CannotAddColumn2(string column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_Add2", new object[]
			{
				column
			}));
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x001EE188 File Offset: 0x001ED588
		public static Exception CannotAddColumn3()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_Add3"));
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x001EE1A8 File Offset: 0x001ED5A8
		public static Exception CannotAddColumn4(string column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_Add4", new object[]
			{
				column
			}));
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x001EE1D8 File Offset: 0x001ED5D8
		public static Exception CannotAddDuplicate(string column)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataColumns_AddDuplicate", new object[]
			{
				column
			}));
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x001EE208 File Offset: 0x001ED608
		public static Exception CannotAddDuplicate2(string table)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataColumns_AddDuplicate2", new object[]
			{
				table
			}));
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x001EE238 File Offset: 0x001ED638
		public static Exception CannotAddDuplicate3(string table)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataColumns_AddDuplicate3", new object[]
			{
				table
			}));
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x001EE268 File Offset: 0x001ED668
		public static Exception CannotRemoveColumn()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_Remove"));
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x001EE288 File Offset: 0x001ED688
		public static Exception CannotRemovePrimaryKey()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_RemovePrimaryKey"));
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x001EE2A8 File Offset: 0x001ED6A8
		public static Exception CannotRemoveChildKey(string relation)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_RemoveChildKey", new object[]
			{
				relation
			}));
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x001EE2D8 File Offset: 0x001ED6D8
		public static Exception CannotRemoveConstraint(string constraint, string table)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_RemoveConstraint", new object[]
			{
				constraint,
				table
			}));
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x001EE308 File Offset: 0x001ED708
		public static Exception CannotRemoveExpression(string column, string expression)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_RemoveExpression", new object[]
			{
				column,
				expression
			}));
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x001EE338 File Offset: 0x001ED738
		public static Exception ColumnNotInTheUnderlyingTable(string column, string table)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_NotInTheUnderlyingTable", new object[]
			{
				column,
				table
			}));
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x001EE368 File Offset: 0x001ED768
		public static Exception InvalidOrdinal(string name, int ordinal)
		{
			return ExceptionBuilder._ArgumentOutOfRange(name, Res.GetString("DataColumn_OrdinalExceedMaximun", new object[]
			{
				ordinal.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x001EE3A8 File Offset: 0x001ED7A8
		public static Exception AddPrimaryKeyConstraint()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_AddPrimaryKeyConstraint"));
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x001EE3C8 File Offset: 0x001ED7C8
		public static Exception NoConstraintName()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_NoName"));
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x001EE3E8 File Offset: 0x001ED7E8
		public static Exception ConstraintViolation(string constraint)
		{
			return ExceptionBuilder._Constraint(Res.GetString("DataConstraint_Violation", new object[]
			{
				constraint
			}));
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x001EE418 File Offset: 0x001ED818
		public static Exception ConstraintNotInTheTable(string constraint)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_NotInTheTable", new object[]
			{
				constraint
			}));
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x001EE448 File Offset: 0x001ED848
		public static string KeysToString(object[] keys)
		{
			string text = string.Empty;
			for (int i = 0; i < keys.Length; i++)
			{
				text = text + Convert.ToString(keys[i], null) + ((i < keys.Length - 1) ? ", " : string.Empty);
			}
			return text;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x001EE498 File Offset: 0x001ED898
		public static string UniqueConstraintViolationText(DataColumn[] columns, object[] values)
		{
			if (columns.Length > 1)
			{
				string text = string.Empty;
				for (int i = 0; i < columns.Length; i++)
				{
					text = text + columns[i].ColumnName + ((i < columns.Length - 1) ? ", " : "");
				}
				return Res.GetString("DataConstraint_ViolationValue", new object[]
				{
					text,
					ExceptionBuilder.KeysToString(values)
				});
			}
			return Res.GetString("DataConstraint_ViolationValue", new object[]
			{
				columns[0].ColumnName,
				Convert.ToString(values[0], null)
			});
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x001EE538 File Offset: 0x001ED938
		public static Exception ConstraintViolation(DataColumn[] columns, object[] values)
		{
			return ExceptionBuilder._Constraint(ExceptionBuilder.UniqueConstraintViolationText(columns, values));
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x001EE558 File Offset: 0x001ED958
		public static Exception ConstraintOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataConstraint_OutOfRange", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x001EE598 File Offset: 0x001ED998
		public static Exception DuplicateConstraint(string constraint)
		{
			return ExceptionBuilder._Data(Res.GetString("DataConstraint_Duplicate", new object[]
			{
				constraint
			}));
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x001EE5C8 File Offset: 0x001ED9C8
		public static Exception DuplicateConstraintName(string constraint)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataConstraint_DuplicateName", new object[]
			{
				constraint
			}));
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x001EE5F8 File Offset: 0x001ED9F8
		public static Exception NeededForForeignKeyConstraint(UniqueConstraint key, ForeignKeyConstraint fk)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_NeededForForeignKeyConstraint", new object[]
			{
				key.ConstraintName,
				fk.ConstraintName
			}));
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x001EE638 File Offset: 0x001EDA38
		public static Exception UniqueConstraintViolation()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_UniqueViolation"));
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x001EE658 File Offset: 0x001EDA58
		public static Exception ConstraintForeignTable()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_ForeignTable"));
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x001EE678 File Offset: 0x001EDA78
		public static Exception ConstraintParentValues()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_ParentValues"));
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x001EE698 File Offset: 0x001EDA98
		public static Exception ConstraintAddFailed(DataTable table)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_AddFailed", new object[]
			{
				table.TableName
			}));
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x001EE6C8 File Offset: 0x001EDAC8
		public static Exception ConstraintRemoveFailed()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_RemoveFailed"));
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x001EE6E8 File Offset: 0x001EDAE8
		public static Exception FailedCascadeDelete(string constraint)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_CascadeDelete", new object[]
			{
				constraint
			}));
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x001EE718 File Offset: 0x001EDB18
		public static Exception FailedCascadeUpdate(string constraint)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_CascadeUpdate", new object[]
			{
				constraint
			}));
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x001EE748 File Offset: 0x001EDB48
		public static Exception FailedClearParentTable(string table, string constraint, string childTable)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_ClearParentTable", new object[]
			{
				table,
				constraint,
				childTable
			}));
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x001EE778 File Offset: 0x001EDB78
		public static Exception ForeignKeyViolation(string constraint, object[] keys)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_ForeignKeyViolation", new object[]
			{
				constraint,
				ExceptionBuilder.KeysToString(keys)
			}));
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x001EE7B8 File Offset: 0x001EDBB8
		public static Exception RemoveParentRow(ForeignKeyConstraint constraint)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_RemoveParentRow", new object[]
			{
				constraint.ConstraintName
			}));
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x001EE7E8 File Offset: 0x001EDBE8
		public static string MaxLengthViolationText(string columnName)
		{
			return Res.GetString("DataColumn_ExceedMaxLength", new object[]
			{
				columnName
			});
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x001EE818 File Offset: 0x001EDC18
		public static string NotAllowDBNullViolationText(string columnName)
		{
			return Res.GetString("DataColumn_NotAllowDBNull", new object[]
			{
				columnName
			});
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x001EE848 File Offset: 0x001EDC48
		public static Exception CantAddConstraintToMultipleNestedTable(string tableName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_CantAddConstraintToMultipleNestedTable", new object[]
			{
				tableName
			}));
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x001EE878 File Offset: 0x001EDC78
		public static Exception AutoIncrementAndExpression()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_AutoIncrementAndExpression"));
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x001EE898 File Offset: 0x001EDC98
		public static Exception AutoIncrementAndDefaultValue()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_AutoIncrementAndDefaultValue"));
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x001EE8B8 File Offset: 0x001EDCB8
		public static Exception AutoIncrementSeed()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_AutoIncrementSeed"));
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x001EE8D8 File Offset: 0x001EDCD8
		public static Exception CantChangeDataType()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ChangeDataType"));
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x001EE8F8 File Offset: 0x001EDCF8
		public static Exception NullDataType()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_NullDataType"));
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x001EE918 File Offset: 0x001EDD18
		public static Exception ColumnNameRequired()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_NameRequired"));
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x001EE938 File Offset: 0x001EDD38
		public static Exception DefaultValueAndAutoIncrement()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_DefaultValueAndAutoIncrement"));
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x001EE958 File Offset: 0x001EDD58
		public static Exception DefaultValueDataType(string column, Type defaultType, Type columnType)
		{
			if (column.Length == 0)
			{
				return ExceptionBuilder._Argument(Res.GetString("DataColumn_DefaultValueDataType1", new object[]
				{
					defaultType.FullName,
					columnType.FullName
				}));
			}
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_DefaultValueDataType", new object[]
			{
				column,
				defaultType.FullName,
				columnType.FullName
			}));
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x001EE9C8 File Offset: 0x001EDDC8
		public static Exception DefaultValueColumnDataType(string column, Type defaultType, Type columnType)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_DefaultValueColumnDataType", new object[]
			{
				column,
				defaultType.FullName,
				columnType.FullName
			}));
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x001EEA08 File Offset: 0x001EDE08
		public static Exception ExpressionAndUnique()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ExpressionAndUnique"));
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x001EEA28 File Offset: 0x001EDE28
		public static Exception ExpressionAndReadOnly()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ExpressionAndReadOnly"));
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x001EEA48 File Offset: 0x001EDE48
		public static Exception ExpressionAndConstraint(DataColumn column, Constraint constraint)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ExpressionAndConstraint", new object[]
			{
				column.ColumnName,
				constraint.ConstraintName
			}));
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x001EEA88 File Offset: 0x001EDE88
		public static Exception ExpressionInConstraint(DataColumn column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ExpressionInConstraint", new object[]
			{
				column.ColumnName
			}));
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x001EEAB8 File Offset: 0x001EDEB8
		public static Exception ExpressionCircular()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ExpressionCircular"));
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x001EEAD8 File Offset: 0x001EDED8
		public static Exception NonUniqueValues(string column)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataColumn_NonUniqueValues", new object[]
			{
				column
			}));
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x001EEB08 File Offset: 0x001EDF08
		public static Exception NullKeyValues(string column)
		{
			return ExceptionBuilder._Data(Res.GetString("DataColumn_NullKeyValues", new object[]
			{
				column
			}));
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x001EEB38 File Offset: 0x001EDF38
		public static Exception NullValues(string column)
		{
			return ExceptionBuilder._NoNullAllowed(Res.GetString("DataColumn_NullValues", new object[]
			{
				column
			}));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x001EEB68 File Offset: 0x001EDF68
		public static Exception ReadOnlyAndExpression()
		{
			return ExceptionBuilder._ReadOnly(Res.GetString("DataColumn_ReadOnlyAndExpression"));
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x001EEB88 File Offset: 0x001EDF88
		public static Exception ReadOnly(string column)
		{
			return ExceptionBuilder._ReadOnly(Res.GetString("DataColumn_ReadOnly", new object[]
			{
				column
			}));
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x001EEBB8 File Offset: 0x001EDFB8
		public static Exception UniqueAndExpression()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_UniqueAndExpression"));
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x001EEBD8 File Offset: 0x001EDFD8
		public static Exception SetFailed(object value, DataColumn column, Type type, Exception innerException)
		{
			return ExceptionBuilder._Argument(innerException.Message + Res.GetString("DataColumn_SetFailed", new object[]
			{
				value.ToString(),
				column.ColumnName,
				type.Name
			}), innerException);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x001EEC28 File Offset: 0x001EE028
		public static Exception CannotSetToNull(DataColumn column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotSetToNull", new object[]
			{
				column.ColumnName
			}));
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x001EEC58 File Offset: 0x001EE058
		public static Exception LongerThanMaxLength(DataColumn column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_LongerThanMaxLength", new object[]
			{
				column.ColumnName
			}));
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x001EEC88 File Offset: 0x001EE088
		public static Exception CannotSetMaxLength(DataColumn column, int value)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotSetMaxLength", new object[]
			{
				column.ColumnName,
				value.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x001EECC8 File Offset: 0x001EE0C8
		public static Exception CannotSetMaxLength2(DataColumn column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotSetMaxLength2", new object[]
			{
				column.ColumnName
			}));
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x001EECF8 File Offset: 0x001EE0F8
		public static Exception CannotSetSimpleContentType(string columnName, Type type)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotSimpleContentType", new object[]
			{
				columnName,
				type
			}));
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x001EED28 File Offset: 0x001EE128
		public static Exception CannotSetSimpleContent(string columnName, Type type)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotSimpleContent", new object[]
			{
				columnName,
				type
			}));
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x001EED58 File Offset: 0x001EE158
		public static Exception CannotChangeNamespace(string columnName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotChangeNamespace", new object[]
			{
				columnName
			}));
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x001EED88 File Offset: 0x001EE188
		public static Exception HasToBeStringType(DataColumn column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_HasToBeStringType", new object[]
			{
				column.ColumnName
			}));
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x001EEDB8 File Offset: 0x001EE1B8
		public static Exception AutoIncrementCannotSetIfHasData(string typeName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_AutoIncrementCannotSetIfHasData", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x001EEDE8 File Offset: 0x001EE1E8
		public static Exception INullableUDTwithoutStaticNull(string typeName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_INullableUDTwithoutStaticNull", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x001EEE18 File Offset: 0x001EE218
		public static Exception IComparableNotImplemented(string typeName)
		{
			return ExceptionBuilder._Data(Res.GetString("DataStorage_IComparableNotDefined", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x001EEE48 File Offset: 0x001EE248
		public static Exception UDTImplementsIChangeTrackingButnotIRevertible(string typeName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataColumn_UDTImplementsIChangeTrackingButnotIRevertible", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x001EEE78 File Offset: 0x001EE278
		public static Exception SetAddedAndModifiedCalledOnnonUnchanged()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataColumn_SetAddedAndModifiedCalledOnNonUnchanged"));
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x001EEE98 File Offset: 0x001EE298
		public static Exception InvalidDataColumnMapping(Type type)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_InvalidDataColumnMapping", new object[]
			{
				type.AssemblyQualifiedName
			}));
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x001EEEC8 File Offset: 0x001EE2C8
		public static Exception CannotSetDateTimeModeForNonDateTimeColumns()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataColumn_CannotSetDateTimeModeForNonDateTimeColumns"));
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x001EEEE8 File Offset: 0x001EE2E8
		public static Exception InvalidDateTimeMode(DataSetDateTime mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<DataSetDateTime>(mode);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x001EEF08 File Offset: 0x001EE308
		public static Exception CantChangeDateTimeMode(DataSetDateTime oldValue, DataSetDateTime newValue)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataColumn_DateTimeMode", new object[]
			{
				oldValue.ToString(),
				newValue.ToString()
			}));
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x001EEF48 File Offset: 0x001EE348
		public static Exception ColumnTypeNotSupported()
		{
			return ADP.NotSupported(Res.GetString("DataColumn_NullableTypesNotSupported"));
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x001EEF68 File Offset: 0x001EE368
		public static Exception SetFailed(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_SetFailed", new object[]
			{
				name
			}));
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x001EEF98 File Offset: 0x001EE398
		public static Exception SetDataSetFailed()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_SetDataSetFailed"));
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x001EEFB8 File Offset: 0x001EE3B8
		public static Exception SetRowStateFilter()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_SetRowStateFilter"));
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x001EEFD8 File Offset: 0x001EE3D8
		public static Exception CanNotSetDataSet()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotSetDataSet"));
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x001EEFF8 File Offset: 0x001EE3F8
		public static Exception CanNotUseDataViewManager()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotUseDataViewManager"));
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x001EF018 File Offset: 0x001EE418
		public static Exception CanNotSetTable()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotSetTable"));
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x001EF038 File Offset: 0x001EE438
		public static Exception CanNotUse()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotUse"));
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x001EF058 File Offset: 0x001EE458
		public static Exception CanNotBindTable()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotBindTable"));
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x001EF078 File Offset: 0x001EE478
		public static Exception SetTable()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_SetTable"));
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x001EF098 File Offset: 0x001EE498
		public static Exception SetIListObject()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_SetIListObject"));
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x001EF0B8 File Offset: 0x001EE4B8
		public static Exception AddNewNotAllowNull()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_AddNewNotAllowNull"));
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x001EF0D8 File Offset: 0x001EE4D8
		public static Exception NotOpen()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_NotOpen"));
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x001EF0F8 File Offset: 0x001EE4F8
		public static Exception CreateChildView()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_CreateChildView"));
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x001EF118 File Offset: 0x001EE518
		public static Exception CanNotDelete()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotDelete"));
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x001EF138 File Offset: 0x001EE538
		public static Exception CanNotEdit()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotEdit"));
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x001EF158 File Offset: 0x001EE558
		public static Exception GetElementIndex(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataView_GetElementIndex", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x001EF198 File Offset: 0x001EE598
		public static Exception AddExternalObject()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_AddExternalObject"));
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x001EF1B8 File Offset: 0x001EE5B8
		public static Exception CanNotClear()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_CanNotClear"));
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x001EF1D8 File Offset: 0x001EE5D8
		public static Exception InsertExternalObject()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_InsertExternalObject"));
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x001EF1F8 File Offset: 0x001EE5F8
		public static Exception RemoveExternalObject()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_RemoveExternalObject"));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x001EF218 File Offset: 0x001EE618
		public static Exception PropertyNotFound(string property, string table)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataROWView_PropertyNotFound", new object[]
			{
				property,
				table
			}));
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x001EF248 File Offset: 0x001EE648
		public static Exception ColumnToSortIsOutOfRange(string column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_OutOfRange", new object[]
			{
				column
			}));
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x001EF278 File Offset: 0x001EE678
		public static Exception KeyTableMismatch()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataKey_TableMismatch"));
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x001EF298 File Offset: 0x001EE698
		public static Exception KeyNoColumns()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataKey_NoColumns"));
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x001EF2B8 File Offset: 0x001EE6B8
		public static Exception KeyTooManyColumns(int cols)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataKey_TooManyColumns", new object[]
			{
				cols.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x001EF2F8 File Offset: 0x001EE6F8
		public static Exception KeyDuplicateColumns(string columnName)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataKey_DuplicateColumns", new object[]
			{
				columnName
			}));
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x001EF328 File Offset: 0x001EE728
		public static Exception RelationDataSetMismatch()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_DataSetMismatch"));
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x001EF348 File Offset: 0x001EE748
		public static Exception NoRelationName()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_NoName"));
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x001EF368 File Offset: 0x001EE768
		public static Exception ColumnsTypeMismatch()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_ColumnsTypeMismatch"));
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x001EF388 File Offset: 0x001EE788
		public static Exception KeyLengthMismatch()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_KeyLengthMismatch"));
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x001EF3A8 File Offset: 0x001EE7A8
		public static Exception KeyLengthZero()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_KeyZeroLength"));
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x001EF3C8 File Offset: 0x001EE7C8
		public static Exception ForeignRelation()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_ForeignDataSet"));
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x001EF3E8 File Offset: 0x001EE7E8
		public static Exception KeyColumnsIdentical()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_KeyColumnsIdentical"));
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x001EF408 File Offset: 0x001EE808
		public static Exception RelationForeignTable(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_ForeignTable", new object[]
			{
				t1,
				t2
			}));
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x001EF438 File Offset: 0x001EE838
		public static Exception GetParentRowTableMismatch(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_GetParentRowTableMismatch", new object[]
			{
				t1,
				t2
			}));
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x001EF468 File Offset: 0x001EE868
		public static Exception SetParentRowTableMismatch(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_SetParentRowTableMismatch", new object[]
			{
				t1,
				t2
			}));
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x001EF498 File Offset: 0x001EE898
		public static Exception RelationForeignRow()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_ForeignRow"));
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x001EF4B8 File Offset: 0x001EE8B8
		public static Exception RelationNestedReadOnly()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_RelationNestedReadOnly"));
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x001EF4D8 File Offset: 0x001EE8D8
		public static Exception TableCantBeNestedInTwoTables(string tableName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_TableCantBeNestedInTwoTables", new object[]
			{
				tableName
			}));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x001EF508 File Offset: 0x001EE908
		public static Exception LoopInNestedRelations(string tableName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_LoopInNestedRelations", new object[]
			{
				tableName
			}));
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x001EF538 File Offset: 0x001EE938
		public static Exception RelationDoesNotExist()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_DoesNotExist"));
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x001EF558 File Offset: 0x001EE958
		public static Exception ParentRowNotInTheDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_ParentRowNotInTheDataSet"));
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x001EF578 File Offset: 0x001EE978
		public static Exception ParentOrChildColumnsDoNotHaveDataSet()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_ParentOrChildColumnsDoNotHaveDataSet"));
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x001EF598 File Offset: 0x001EE998
		public static Exception InValidNestedRelation(string childTableName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataRelation_InValidNestedRelation", new object[]
			{
				childTableName
			}));
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x001EF5C8 File Offset: 0x001EE9C8
		public static Exception InvalidParentNamespaceinNestedRelation(string childTableName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataRelation_InValidNamespaceInNestedRelation", new object[]
			{
				childTableName
			}));
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x001EF5F8 File Offset: 0x001EE9F8
		public static Exception RowNotInTheDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_NotInTheDataSet"));
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x001EF618 File Offset: 0x001EEA18
		public static Exception RowNotInTheTable()
		{
			return ExceptionBuilder._RowNotInTable(Res.GetString("DataRow_NotInTheTable"));
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x001EF638 File Offset: 0x001EEA38
		public static Exception EditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent(Res.GetString("DataRow_EditInRowChanging"));
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x001EF658 File Offset: 0x001EEA58
		public static Exception EndEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent(Res.GetString("DataRow_EndEditInRowChanging"));
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x001EF678 File Offset: 0x001EEA78
		public static Exception BeginEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent(Res.GetString("DataRow_BeginEditInRowChanging"));
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x001EF698 File Offset: 0x001EEA98
		public static Exception CancelEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent(Res.GetString("DataRow_CancelEditInRowChanging"));
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x001EF6B8 File Offset: 0x001EEAB8
		public static Exception DeleteInRowDeleting()
		{
			return ExceptionBuilder._InRowChangingEvent(Res.GetString("DataRow_DeleteInRowDeleting"));
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x001EF6D8 File Offset: 0x001EEAD8
		public static Exception ValueArrayLength()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_ValuesArrayLength"));
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x001EF6F8 File Offset: 0x001EEAF8
		public static Exception NoCurrentData()
		{
			return ExceptionBuilder._VersionNotFound(Res.GetString("DataRow_NoCurrentData"));
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x001EF718 File Offset: 0x001EEB18
		public static Exception NoOriginalData()
		{
			return ExceptionBuilder._VersionNotFound(Res.GetString("DataRow_NoOriginalData"));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x001EF738 File Offset: 0x001EEB38
		public static Exception NoProposedData()
		{
			return ExceptionBuilder._VersionNotFound(Res.GetString("DataRow_NoProposedData"));
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x001EF758 File Offset: 0x001EEB58
		public static Exception RowRemovedFromTheTable()
		{
			return ExceptionBuilder._RowNotInTable(Res.GetString("DataRow_RemovedFromTheTable"));
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x001EF778 File Offset: 0x001EEB78
		public static Exception DeletedRowInaccessible()
		{
			return ExceptionBuilder._DeletedRowInaccessible(Res.GetString("DataRow_DeletedRowInaccessible"));
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x001EF798 File Offset: 0x001EEB98
		public static Exception RowAlreadyDeleted()
		{
			return ExceptionBuilder._DeletedRowInaccessible(Res.GetString("DataRow_AlreadyDeleted"));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x001EF7B8 File Offset: 0x001EEBB8
		public static Exception RowEmpty()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_Empty"));
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x001EF7D8 File Offset: 0x001EEBD8
		public static Exception InvalidRowVersion()
		{
			return ExceptionBuilder._Data(Res.GetString("DataRow_InvalidVersion"));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x001EF7F8 File Offset: 0x001EEBF8
		public static Exception RowOutOfRange()
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRow_RowOutOfRange"));
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x001EF818 File Offset: 0x001EEC18
		public static Exception RowOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRow_OutOfRange", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x001EF858 File Offset: 0x001EEC58
		public static Exception RowInsertOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRow_RowInsertOutOfRange", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x001EF898 File Offset: 0x001EEC98
		public static Exception RowInsertTwice(int index, string tableName)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRow_RowInsertTwice", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				tableName
			}));
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x001EF8D8 File Offset: 0x001EECD8
		public static Exception RowInsertMissing(string tableName)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRow_RowInsertMissing", new object[]
			{
				tableName
			}));
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x001EF908 File Offset: 0x001EED08
		public static Exception RowAlreadyRemoved()
		{
			return ExceptionBuilder._Data(Res.GetString("DataRow_AlreadyRemoved"));
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x001EF928 File Offset: 0x001EED28
		public static Exception MultipleParents()
		{
			return ExceptionBuilder._Data(Res.GetString("DataRow_MultipleParents"));
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x001EF948 File Offset: 0x001EED48
		public static Exception InvalidRowState(DataRowState state)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<DataRowState>(state);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x001EF968 File Offset: 0x001EED68
		public static Exception InvalidRowBitPattern()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_InvalidRowBitPattern"));
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x001EF988 File Offset: 0x001EED88
		internal static Exception SetDataSetNameToEmpty()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataSet_SetNameToEmpty"));
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x001EF9A8 File Offset: 0x001EEDA8
		internal static Exception SetDataSetNameConflicting(string name)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataSet_SetDataSetNameConflicting", new object[]
			{
				name
			}));
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x001EF9D8 File Offset: 0x001EEDD8
		public static Exception DataSetUnsupportedSchema(string ns)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataSet_UnsupportedSchema", new object[]
			{
				ns
			}));
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x001EFA08 File Offset: 0x001EEE08
		public static Exception MergeMissingDefinition(string obj)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataMerge_MissingDefinition", new object[]
			{
				obj
			}));
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x001EFA38 File Offset: 0x001EEE38
		public static Exception TablesInDifferentSets()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_TablesInDifferentSets"));
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x001EFA58 File Offset: 0x001EEE58
		public static Exception RelationAlreadyExists()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_AlreadyExists"));
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x001EFA78 File Offset: 0x001EEE78
		public static Exception RowAlreadyInOtherCollection()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_AlreadyInOtherCollection"));
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x001EFA98 File Offset: 0x001EEE98
		public static Exception RowAlreadyInTheCollection()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_AlreadyInTheCollection"));
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x001EFAB8 File Offset: 0x001EEEB8
		public static Exception TableMissingPrimaryKey()
		{
			return ExceptionBuilder._MissingPrimaryKey(Res.GetString("DataTable_MissingPrimaryKey"));
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x001EFAD8 File Offset: 0x001EEED8
		public static Exception RecordStateRange()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataIndex_RecordStateRange"));
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x001EFAF8 File Offset: 0x001EEEF8
		public static Exception IndexKeyLength(int length, int keyLength)
		{
			if (length == 0)
			{
				return ExceptionBuilder._Argument(Res.GetString("DataIndex_FindWithoutSortOrder"));
			}
			return ExceptionBuilder._Argument(Res.GetString("DataIndex_KeyLength", new object[]
			{
				length.ToString(CultureInfo.InvariantCulture),
				keyLength.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x001EFB58 File Offset: 0x001EEF58
		public static Exception RemovePrimaryKey(DataTable table)
		{
			if (table.TableName.Length == 0)
			{
				return ExceptionBuilder._Argument(Res.GetString("DataKey_RemovePrimaryKey"));
			}
			return ExceptionBuilder._Argument(Res.GetString("DataKey_RemovePrimaryKey1", new object[]
			{
				table.TableName
			}));
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x001EFBA8 File Offset: 0x001EEFA8
		public static Exception RelationAlreadyInOtherDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_AlreadyInOtherDataSet"));
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x001EFBC8 File Offset: 0x001EEFC8
		public static Exception RelationAlreadyInTheDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_AlreadyInTheDataSet"));
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x001EFBE8 File Offset: 0x001EEFE8
		public static Exception RelationNotInTheDataSet(string relation)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_NotInTheDataSet", new object[]
			{
				relation
			}));
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x001EFC18 File Offset: 0x001EF018
		public static Exception RelationOutOfRange(object index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRelation_OutOfRange", new object[]
			{
				Convert.ToString(index, null)
			}));
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x001EFC48 File Offset: 0x001EF048
		public static Exception DuplicateRelation(string relation)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataRelation_DuplicateName", new object[]
			{
				relation
			}));
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x001EFC78 File Offset: 0x001EF078
		public static Exception RelationTableNull()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_TableNull"));
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x001EFC98 File Offset: 0x001EF098
		public static Exception RelationDataSetNull()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_TableNull"));
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x001EFCB8 File Offset: 0x001EF0B8
		public static Exception RelationTableWasRemoved()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_TableWasRemoved"));
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x001EFCD8 File Offset: 0x001EF0D8
		public static Exception ParentTableMismatch()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_ParentTableMismatch"));
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x001EFCF8 File Offset: 0x001EF0F8
		public static Exception ChildTableMismatch()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_ChildTableMismatch"));
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x001EFD18 File Offset: 0x001EF118
		public static Exception EnforceConstraint()
		{
			return ExceptionBuilder._Constraint(Res.GetString("Data_EnforceConstraints"));
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x001EFD38 File Offset: 0x001EF138
		public static Exception CaseLocaleMismatch()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_CaseLocaleMismatch"));
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x001EFD58 File Offset: 0x001EF158
		public static Exception CannotChangeCaseLocale()
		{
			return ExceptionBuilder.CannotChangeCaseLocale(null);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x001EFD78 File Offset: 0x001EF178
		public static Exception CannotChangeCaseLocale(Exception innerException)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataSet_CannotChangeCaseLocale"), innerException);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x001EFD98 File Offset: 0x001EF198
		public static Exception CannotChangeSchemaSerializationMode()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataSet_CannotChangeSchemaSerializationMode"));
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x001EFDB8 File Offset: 0x001EF1B8
		public static Exception InvalidSchemaSerializationMode(Type enumType, string mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException(Res.GetString("ADP_InvalidEnumerationValue", new object[]
			{
				enumType.Name,
				mode
			}));
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x001EFDF8 File Offset: 0x001EF1F8
		public static Exception InvalidRemotingFormat(SerializationFormat mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<SerializationFormat>(mode);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x001EFE18 File Offset: 0x001EF218
		public static Exception TableForeignPrimaryKey()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_ForeignPrimaryKey"));
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x001EFE38 File Offset: 0x001EF238
		public static Exception TableCannotAddToSimpleContent()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_CannotAddToSimpleContent"));
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x001EFE58 File Offset: 0x001EF258
		public static Exception NoTableName()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_NoName"));
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x001EFE78 File Offset: 0x001EF278
		public static Exception MultipleTextOnlyColumns()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_MultipleSimpleContentColumns"));
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x001EFE98 File Offset: 0x001EF298
		public static Exception InvalidSortString(string sort)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_InvalidSortString", new object[]
			{
				sort
			}));
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x001EFEC8 File Offset: 0x001EF2C8
		public static Exception DuplicateTableName(string table)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataTable_DuplicateName", new object[]
			{
				table
			}));
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x001EFEF8 File Offset: 0x001EF2F8
		public static Exception DuplicateTableName2(string table, string ns)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataTable_DuplicateName2", new object[]
			{
				table,
				ns
			}));
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x001EFF28 File Offset: 0x001EF328
		public static Exception SelfnestedDatasetConflictingName(string table)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataTable_SelfnestedDatasetConflictingName", new object[]
			{
				table
			}));
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x001EFF58 File Offset: 0x001EF358
		public static Exception DatasetConflictingName(string table)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataTable_DatasetConflictingName", new object[]
			{
				table
			}));
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x001EFF88 File Offset: 0x001EF388
		public static Exception TableAlreadyInOtherDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_AlreadyInOtherDataSet"));
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x001EFFA8 File Offset: 0x001EF3A8
		public static Exception TableAlreadyInTheDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_AlreadyInTheDataSet"));
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x001EFFC8 File Offset: 0x001EF3C8
		public static Exception TableOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataTable_OutOfRange", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x001F0008 File Offset: 0x001EF408
		public static Exception TableNotInTheDataSet(string table)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_NotInTheDataSet", new object[]
			{
				table
			}));
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x001F0038 File Offset: 0x001EF438
		public static Exception TableInRelation()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_InRelation"));
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x001F0058 File Offset: 0x001EF458
		public static Exception TableInConstraint(DataTable table, Constraint constraint)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_InConstraint", new object[]
			{
				table.TableName,
				constraint.ConstraintName
			}));
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x001F0098 File Offset: 0x001EF498
		public static Exception CanNotSerializeDataTableHierarchy()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataTable_CanNotSerializeDataTableHierarchy"));
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x001F00B8 File Offset: 0x001EF4B8
		public static Exception CanNotRemoteDataTable()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataTable_CanNotRemoteDataTable"));
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x001F00D8 File Offset: 0x001EF4D8
		public static Exception CanNotSetRemotingFormat()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_CanNotSetRemotingFormat"));
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x001F00F8 File Offset: 0x001EF4F8
		public static Exception CanNotSerializeDataTableWithEmptyName()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataTable_CanNotSerializeDataTableWithEmptyName"));
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x001F0118 File Offset: 0x001EF518
		public static Exception TableNotFound(string tableName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_TableNotFound", new object[]
			{
				tableName
			}));
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x001F0148 File Offset: 0x001EF548
		public static Exception AggregateException(AggregateType aggregateType, Type type)
		{
			return ExceptionBuilder._Data(Res.GetString("DataStorage_AggregateException", new object[]
			{
				aggregateType.ToString(),
				type.Name
			}));
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x001F0188 File Offset: 0x001EF588
		public static Exception InvalidStorageType(TypeCode typecode)
		{
			return ExceptionBuilder._Data(Res.GetString("DataStorage_InvalidStorageType", new object[]
			{
				typecode.ToString()
			}));
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x001F01C8 File Offset: 0x001EF5C8
		public static Exception RangeArgument(int min, int max)
		{
			return ExceptionBuilder._Argument(Res.GetString("Range_Argument", new object[]
			{
				min.ToString(CultureInfo.InvariantCulture),
				max.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x001F0218 File Offset: 0x001EF618
		public static Exception NullRange()
		{
			return ExceptionBuilder._Data(Res.GetString("Range_NullRange"));
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x001F0238 File Offset: 0x001EF638
		public static Exception NegativeMinimumCapacity()
		{
			return ExceptionBuilder._Argument(Res.GetString("RecordManager_MinimumCapacity"));
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x001F0258 File Offset: 0x001EF658
		public static Exception ProblematicChars(char charValue)
		{
			string str = "0x";
			ushort num = (ushort)charValue;
			string text = str + num.ToString("X", CultureInfo.InvariantCulture);
			return ExceptionBuilder._Argument(Res.GetString("DataStorage_ProblematicChars", new object[]
			{
				text
			}));
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x001F02A8 File Offset: 0x001EF6A8
		public static Exception StorageSetFailed()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataStorage_SetInvalidDataType"));
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x001F02C8 File Offset: 0x001EF6C8
		public static Exception SimpleTypeNotSupported()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_SimpleTypeNotSupported"));
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x001F02E8 File Offset: 0x001EF6E8
		public static Exception MissingAttribute(string attribute)
		{
			return ExceptionBuilder.MissingAttribute(string.Empty, attribute);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x001F0308 File Offset: 0x001EF708
		public static Exception MissingAttribute(string element, string attribute)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MissingAttribute", new object[]
			{
				element,
				attribute
			}));
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x001F0338 File Offset: 0x001EF738
		public static Exception InvalidAttributeValue(string name, string value)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_ValueOutOfRange", new object[]
			{
				name,
				value
			}));
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x001F0368 File Offset: 0x001EF768
		public static Exception AttributeValues(string name, string value1, string value2)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_AttributeValues", new object[]
			{
				name,
				value1,
				value2
			}));
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x001F0398 File Offset: 0x001EF798
		public static Exception ElementTypeNotFound(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_ElementTypeNotFound", new object[]
			{
				name
			}));
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x001F03C8 File Offset: 0x001EF7C8
		public static Exception RelationParentNameMissing(string rel)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_RelationParentNameMissing", new object[]
			{
				rel
			}));
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x001F03F8 File Offset: 0x001EF7F8
		public static Exception RelationChildNameMissing(string rel)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_RelationChildNameMissing", new object[]
			{
				rel
			}));
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x001F0428 File Offset: 0x001EF828
		public static Exception RelationTableKeyMissing(string rel)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_RelationTableKeyMissing", new object[]
			{
				rel
			}));
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x001F0458 File Offset: 0x001EF858
		public static Exception RelationChildKeyMissing(string rel)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_RelationChildKeyMissing", new object[]
			{
				rel
			}));
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x001F0488 File Offset: 0x001EF888
		public static Exception UndefinedDatatype(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_UndefinedDatatype", new object[]
			{
				name
			}));
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x001F04B8 File Offset: 0x001EF8B8
		public static Exception DatatypeNotDefined()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_DatatypeNotDefined"));
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x001F04D8 File Offset: 0x001EF8D8
		public static Exception MismatchKeyLength()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MismatchKeyLength"));
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x001F04F8 File Offset: 0x001EF8F8
		public static Exception InvalidField(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_InvalidField", new object[]
			{
				name
			}));
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x001F0528 File Offset: 0x001EF928
		public static Exception InvalidSelector(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_InvalidSelector", new object[]
			{
				name
			}));
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x001F0558 File Offset: 0x001EF958
		public static Exception CircularComplexType(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_CircularComplexType", new object[]
			{
				name
			}));
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x001F0588 File Offset: 0x001EF988
		public static Exception CannotInstantiateAbstract(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_CannotInstantiateAbstract", new object[]
			{
				name
			}));
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x001F05B8 File Offset: 0x001EF9B8
		public static Exception InvalidKey(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_InvalidKey", new object[]
			{
				name
			}));
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x001F05E8 File Offset: 0x001EF9E8
		public static Exception DiffgramMissingTable(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MissingTable", new object[]
			{
				name
			}));
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x001F0618 File Offset: 0x001EFA18
		public static Exception DiffgramMissingSQL()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MissingSQL"));
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x001F0638 File Offset: 0x001EFA38
		public static Exception DuplicateConstraintRead(string str)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_DuplicateConstraint", new object[]
			{
				str
			}));
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x001F0668 File Offset: 0x001EFA68
		public static Exception ColumnTypeConflict(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_ColumnConflict", new object[]
			{
				name
			}));
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x001F0698 File Offset: 0x001EFA98
		public static Exception CannotConvert(string name, string type)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_CannotConvert", new object[]
			{
				name,
				type
			}));
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x001F06C8 File Offset: 0x001EFAC8
		public static Exception MissingRefer(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MissingRefer", new object[]
			{
				"refer",
				"keyref",
				name
			}));
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x001F0708 File Offset: 0x001EFB08
		public static Exception InvalidPrefix(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_InvalidPrefix", new object[]
			{
				name
			}));
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x001F0738 File Offset: 0x001EFB38
		public static Exception CanNotDeserializeObjectType()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("Xml_CanNotDeserializeObjectType"));
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x001F0758 File Offset: 0x001EFB58
		public static Exception IsDataSetAttributeMissingInSchema()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_IsDataSetAttributeMissingInSchema"));
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x001F0778 File Offset: 0x001EFB78
		public static Exception TooManyIsDataSetAtributeInSchema()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_TooManyIsDataSetAtributeInSchema"));
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x001F0798 File Offset: 0x001EFB98
		public static Exception NestedCircular(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_NestedCircular", new object[]
			{
				name
			}));
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x001F07C8 File Offset: 0x001EFBC8
		public static Exception MultipleParentRows(string tableQName)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MultipleParentRows", new object[]
			{
				tableQName
			}));
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x001F07F8 File Offset: 0x001EFBF8
		public static Exception PolymorphismNotSupported(string typeName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("Xml_PolymorphismNotSupported", new object[]
			{
				typeName
			}));
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x001F0828 File Offset: 0x001EFC28
		public static Exception DataTableInferenceNotSupported()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("Xml_DataTableInferenceNotSupported"));
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x001F0848 File Offset: 0x001EFC48
		public static Exception DuplicateDeclaration(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MergeDuplicateDeclaration", new object[]
			{
				name
			}));
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x001F0878 File Offset: 0x001EFC78
		public static Exception FoundEntity()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_FoundEntity"));
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x001F0898 File Offset: 0x001EFC98
		public static Exception MergeFailed(string name)
		{
			return ExceptionBuilder._Data(name);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x001F08B8 File Offset: 0x001EFCB8
		public static DataException ConvertFailed(Type type1, Type type2)
		{
			return ExceptionBuilder.ConvertFailed(type1, type2, null);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x001F08D8 File Offset: 0x001EFCD8
		public static DataException ConvertFailed(Type type1, Type type2, Exception innerExeption)
		{
			return ExceptionBuilder._Data(Res.GetString("SqlConvert_ConvertFailed", new object[]
			{
				type1.FullName,
				type2.FullName
			}), innerExeption);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x001F0918 File Offset: 0x001EFD18
		public static Exception InvalidDataTableReader(string tableName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataTableReader_InvalidDataTableReader", new object[]
			{
				tableName
			}));
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x001F0948 File Offset: 0x001EFD48
		public static Exception DataTableReaderSchemaIsInvalid(string tableName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataTableReader_SchemaInvalidDataTableReader", new object[]
			{
				tableName
			}));
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x001F0978 File Offset: 0x001EFD78
		public static Exception CannotCreateDataReaderOnEmptyDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTableReader_CannotCreateDataReaderOnEmptyDataSet"));
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x001F0998 File Offset: 0x001EFD98
		public static Exception DataTableReaderArgumentIsEmpty()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTableReader_DataTableReaderArgumentIsEmpty"));
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x001F09B8 File Offset: 0x001EFDB8
		public static Exception ArgumentContainsNullValue()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTableReader_ArgumentContainsNullValue"));
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x001F09D8 File Offset: 0x001EFDD8
		public static Exception InvalidCurrentRowInDataTableReader()
		{
			return ExceptionBuilder._DeletedRowInaccessible(Res.GetString("DataTableReader_InvalidRowInDataTableReader"));
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x001F09F8 File Offset: 0x001EFDF8
		public static Exception EmptyDataTableReader(string tableName)
		{
			return ExceptionBuilder._DeletedRowInaccessible(Res.GetString("DataTableReader_DataTableCleared", new object[]
			{
				tableName
			}));
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x001F0A28 File Offset: 0x001EFE28
		internal static Exception InvalidDuplicateNamedSimpleTypeDelaration(string stName, string errorStr)
		{
			return ExceptionBuilder._Argument(Res.GetString("NamedSimpleType_InvalidDuplicateNamedSimpleTypeDelaration", new object[]
			{
				stName,
				errorStr
			}));
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x001F0A58 File Offset: 0x001EFE58
		internal static Exception InternalRBTreeError(RBTreeError internalError)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("RbTree_InvalidState", new object[]
			{
				(int)internalError
			}));
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x001F0A88 File Offset: 0x001EFE88
		public static Exception EnumeratorModified()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("RbTree_EnumerationBroken"));
		}
	}
}
