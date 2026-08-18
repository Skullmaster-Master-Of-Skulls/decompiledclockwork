using System;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Security;
using System.Xml;

namespace System.Data
{
	// Token: 0x020000B8 RID: 184
	internal static class ExceptionBuilder
	{
		// Token: 0x0600095C RID: 2396 RVA: 0x0005C810 File Offset: 0x0005BC10
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

		// Token: 0x0600095D RID: 2397 RVA: 0x0005C870 File Offset: 0x0005BC70
		internal static void TraceExceptionAsReturnValue(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|THROW> Message='%ls'", e);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0005C888 File Offset: 0x0005BC88
		internal static void TraceExceptionForCapture(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|CATCH> Message='%ls'", e);
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0005C8A0 File Offset: 0x0005BCA0
		internal static void TraceExceptionWithoutRethrow(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|CATCH> Message='%ls'", e);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0005C8B8 File Offset: 0x0005BCB8
		internal static ArgumentException _Argument(string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0005C8D4 File Offset: 0x0005BCD4
		internal static ArgumentException _Argument(string paramName, string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0005C8F0 File Offset: 0x0005BCF0
		internal static ArgumentException _Argument(string error, Exception innerException)
		{
			ArgumentException ex = new ArgumentException(error, innerException);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0005C90C File Offset: 0x0005BD0C
		private static ArgumentNullException _ArgumentNull(string paramName, string msg)
		{
			ArgumentNullException ex = new ArgumentNullException(paramName, msg);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0005C928 File Offset: 0x0005BD28
		internal static ArgumentOutOfRangeException _ArgumentOutOfRange(string paramName, string msg)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(paramName, msg);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0005C944 File Offset: 0x0005BD44
		internal static Exception _ConfigurationErrors(string message, XmlNode node)
		{
			ConfigurationErrorsException ex = new ConfigurationErrorsException(message, node);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0005C960 File Offset: 0x0005BD60
		private static IndexOutOfRangeException _IndexOutOfRange(string error)
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0005C97C File Offset: 0x0005BD7C
		private static InvalidOperationException _InvalidOperation(string error)
		{
			InvalidOperationException ex = new InvalidOperationException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0005C998 File Offset: 0x0005BD98
		private static InvalidEnumArgumentException _InvalidEnumArgumentException(string error)
		{
			InvalidEnumArgumentException ex = new InvalidEnumArgumentException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0005C9B4 File Offset: 0x0005BDB4
		private static InvalidEnumArgumentException _InvalidEnumArgumentException<T>(T value)
		{
			string @string = Res.GetString("ADP_InvalidEnumerationValue", new object[]
			{
				typeof(T).Name,
				value.ToString()
			});
			return ExceptionBuilder._InvalidEnumArgumentException(@string);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0005C9FC File Offset: 0x0005BDFC
		private static DataException _Data(string error)
		{
			DataException ex = new DataException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0005CA18 File Offset: 0x0005BE18
		private static void ThrowDataException(string error, Exception innerException)
		{
			DataException ex = new DataException(error, innerException);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			throw ex;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0005CA34 File Offset: 0x0005BE34
		private static ConstraintException _Constraint(string error)
		{
			ConstraintException ex = new ConstraintException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0005CA50 File Offset: 0x0005BE50
		private static InvalidConstraintException _InvalidConstraint(string error)
		{
			InvalidConstraintException ex = new InvalidConstraintException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0005CA6C File Offset: 0x0005BE6C
		private static DeletedRowInaccessibleException _DeletedRowInaccessible(string error)
		{
			DeletedRowInaccessibleException ex = new DeletedRowInaccessibleException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0005CA88 File Offset: 0x0005BE88
		private static DuplicateNameException _DuplicateName(string error)
		{
			DuplicateNameException ex = new DuplicateNameException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0005CAA4 File Offset: 0x0005BEA4
		private static InRowChangingEventException _InRowChangingEvent(string error)
		{
			InRowChangingEventException ex = new InRowChangingEventException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x0005CAC0 File Offset: 0x0005BEC0
		private static MissingPrimaryKeyException _MissingPrimaryKey(string error)
		{
			MissingPrimaryKeyException ex = new MissingPrimaryKeyException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0005CADC File Offset: 0x0005BEDC
		private static NoNullAllowedException _NoNullAllowed(string error)
		{
			NoNullAllowedException ex = new NoNullAllowedException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0005CAF8 File Offset: 0x0005BEF8
		private static ReadOnlyException _ReadOnly(string error)
		{
			ReadOnlyException ex = new ReadOnlyException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0005CB14 File Offset: 0x0005BF14
		private static RowNotInTableException _RowNotInTable(string error)
		{
			RowNotInTableException ex = new RowNotInTableException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0005CB30 File Offset: 0x0005BF30
		private static VersionNotFoundException _VersionNotFound(string error)
		{
			VersionNotFoundException ex = new VersionNotFoundException(error);
			ExceptionBuilder.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0005CB4C File Offset: 0x0005BF4C
		public static Exception ArgumentNull(string paramName)
		{
			return ExceptionBuilder._ArgumentNull(paramName, Res.GetString("Data_ArgumentNull", new object[]
			{
				paramName
			}));
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x0005CB74 File Offset: 0x0005BF74
		public static Exception ArgumentOutOfRange(string paramName)
		{
			return ExceptionBuilder._ArgumentOutOfRange(paramName, Res.GetString("Data_ArgumentOutOfRange", new object[]
			{
				paramName
			}));
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0005CB9C File Offset: 0x0005BF9C
		public static Exception BadObjectPropertyAccess(string error)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataConstraint_BadObjectPropertyAccess", new object[]
			{
				error
			}));
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0005CBC4 File Offset: 0x0005BFC4
		public static Exception ArgumentContainsNull(string paramName)
		{
			return ExceptionBuilder._Argument(paramName, Res.GetString("Data_ArgumentContainsNull", new object[]
			{
				paramName
			}));
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0005CBEC File Offset: 0x0005BFEC
		public static Exception TypeNotAllowed(Type type)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("Data_TypeNotAllowed", new object[]
			{
				type.AssemblyQualifiedName
			}));
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x0005CC18 File Offset: 0x0005C018
		public static Exception ConfigElementNotAllowed(XmlNode configNode)
		{
			return ExceptionBuilder._ConfigurationErrors(Res.GetString("Config_ElementNotAllowed", new object[]
			{
				configNode.Name
			}), configNode);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0005CC44 File Offset: 0x0005C044
		public static Exception CannotModifyCollection()
		{
			return ExceptionBuilder._Argument(Res.GetString("Data_CannotModifyCollection"));
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0005CC60 File Offset: 0x0005C060
		public static Exception CaseInsensitiveNameConflict(string name)
		{
			return ExceptionBuilder._Argument(Res.GetString("Data_CaseInsensitiveNameConflict", new object[]
			{
				name
			}));
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0005CC88 File Offset: 0x0005C088
		public static Exception NamespaceNameConflict(string name)
		{
			return ExceptionBuilder._Argument(Res.GetString("Data_NamespaceNameConflict", new object[]
			{
				name
			}));
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0005CCB0 File Offset: 0x0005C0B0
		public static Exception InvalidOffsetLength()
		{
			return ExceptionBuilder._Argument(Res.GetString("Data_InvalidOffsetLength"));
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0005CCCC File Offset: 0x0005C0CC
		public static Exception ColumnNotInTheTable(string column, string table)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_NotInTheTable", new object[]
			{
				column,
				table
			}));
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0005CCF8 File Offset: 0x0005C0F8
		public static Exception ColumnNotInAnyTable()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_NotInAnyTable"));
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0005CD14 File Offset: 0x0005C114
		public static Exception ColumnOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataColumns_OutOfRange", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0005CD48 File Offset: 0x0005C148
		public static Exception ColumnOutOfRange(string column)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataColumns_OutOfRange", new object[]
			{
				column
			}));
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0005CD70 File Offset: 0x0005C170
		public static Exception CannotAddColumn1(string column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_Add1", new object[]
			{
				column
			}));
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0005CD98 File Offset: 0x0005C198
		public static Exception CannotAddColumn2(string column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_Add2", new object[]
			{
				column
			}));
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0005CDC0 File Offset: 0x0005C1C0
		public static Exception CannotAddColumn3()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_Add3"));
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0005CDDC File Offset: 0x0005C1DC
		public static Exception CannotAddColumn4(string column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_Add4", new object[]
			{
				column
			}));
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0005CE04 File Offset: 0x0005C204
		public static Exception CannotAddDuplicate(string column)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataColumns_AddDuplicate", new object[]
			{
				column
			}));
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0005CE2C File Offset: 0x0005C22C
		public static Exception CannotAddDuplicate2(string table)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataColumns_AddDuplicate2", new object[]
			{
				table
			}));
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0005CE54 File Offset: 0x0005C254
		public static Exception CannotAddDuplicate3(string table)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataColumns_AddDuplicate3", new object[]
			{
				table
			}));
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0005CE7C File Offset: 0x0005C27C
		public static Exception CannotRemoveColumn()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_Remove"));
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0005CE98 File Offset: 0x0005C298
		public static Exception CannotRemovePrimaryKey()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_RemovePrimaryKey"));
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0005CEB4 File Offset: 0x0005C2B4
		public static Exception CannotRemoveChildKey(string relation)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_RemoveChildKey", new object[]
			{
				relation
			}));
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0005CEDC File Offset: 0x0005C2DC
		public static Exception CannotRemoveConstraint(string constraint, string table)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_RemoveConstraint", new object[]
			{
				constraint,
				table
			}));
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0005CF08 File Offset: 0x0005C308
		public static Exception CannotRemoveExpression(string column, string expression)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_RemoveExpression", new object[]
			{
				column,
				expression
			}));
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0005CF34 File Offset: 0x0005C334
		public static Exception ColumnNotInTheUnderlyingTable(string column, string table)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_NotInTheUnderlyingTable", new object[]
			{
				column,
				table
			}));
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0005CF60 File Offset: 0x0005C360
		public static Exception InvalidOrdinal(string name, int ordinal)
		{
			return ExceptionBuilder._ArgumentOutOfRange(name, Res.GetString("DataColumn_OrdinalExceedMaximun", new object[]
			{
				ordinal.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0005CF94 File Offset: 0x0005C394
		public static Exception AddPrimaryKeyConstraint()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_AddPrimaryKeyConstraint"));
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0005CFB0 File Offset: 0x0005C3B0
		public static Exception NoConstraintName()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_NoName"));
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0005CFCC File Offset: 0x0005C3CC
		public static Exception ConstraintViolation(string constraint)
		{
			return ExceptionBuilder._Constraint(Res.GetString("DataConstraint_Violation", new object[]
			{
				constraint
			}));
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0005CFF4 File Offset: 0x0005C3F4
		public static Exception ConstraintNotInTheTable(string constraint)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_NotInTheTable", new object[]
			{
				constraint
			}));
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0005D01C File Offset: 0x0005C41C
		public static string KeysToString(object[] keys)
		{
			string text = string.Empty;
			for (int i = 0; i < keys.Length; i++)
			{
				text = text + Convert.ToString(keys[i], null) + ((i < keys.Length - 1) ? ", " : string.Empty);
			}
			return text;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0005D064 File Offset: 0x0005C464
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

		// Token: 0x06000998 RID: 2456 RVA: 0x0005D0F4 File Offset: 0x0005C4F4
		public static Exception ConstraintViolation(DataColumn[] columns, object[] values)
		{
			return ExceptionBuilder._Constraint(ExceptionBuilder.UniqueConstraintViolationText(columns, values));
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0005D110 File Offset: 0x0005C510
		public static Exception ConstraintOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataConstraint_OutOfRange", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0005D144 File Offset: 0x0005C544
		public static Exception DuplicateConstraint(string constraint)
		{
			return ExceptionBuilder._Data(Res.GetString("DataConstraint_Duplicate", new object[]
			{
				constraint
			}));
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0005D16C File Offset: 0x0005C56C
		public static Exception DuplicateConstraintName(string constraint)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataConstraint_DuplicateName", new object[]
			{
				constraint
			}));
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0005D194 File Offset: 0x0005C594
		public static Exception NeededForForeignKeyConstraint(UniqueConstraint key, ForeignKeyConstraint fk)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_NeededForForeignKeyConstraint", new object[]
			{
				key.ConstraintName,
				fk.ConstraintName
			}));
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0005D1C8 File Offset: 0x0005C5C8
		public static Exception UniqueConstraintViolation()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_UniqueViolation"));
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0005D1E4 File Offset: 0x0005C5E4
		public static Exception ConstraintForeignTable()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_ForeignTable"));
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0005D200 File Offset: 0x0005C600
		public static Exception ConstraintParentValues()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_ParentValues"));
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0005D21C File Offset: 0x0005C61C
		public static Exception ConstraintAddFailed(DataTable table)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_AddFailed", new object[]
			{
				table.TableName
			}));
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0005D248 File Offset: 0x0005C648
		public static Exception ConstraintRemoveFailed()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_RemoveFailed"));
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0005D264 File Offset: 0x0005C664
		public static Exception FailedCascadeDelete(string constraint)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_CascadeDelete", new object[]
			{
				constraint
			}));
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0005D28C File Offset: 0x0005C68C
		public static Exception FailedCascadeUpdate(string constraint)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_CascadeUpdate", new object[]
			{
				constraint
			}));
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0005D2B4 File Offset: 0x0005C6B4
		public static Exception FailedClearParentTable(string table, string constraint, string childTable)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_ClearParentTable", new object[]
			{
				table,
				constraint,
				childTable
			}));
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0005D2E4 File Offset: 0x0005C6E4
		public static Exception ForeignKeyViolation(string constraint, object[] keys)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_ForeignKeyViolation", new object[]
			{
				constraint,
				ExceptionBuilder.KeysToString(keys)
			}));
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0005D314 File Offset: 0x0005C714
		public static Exception RemoveParentRow(ForeignKeyConstraint constraint)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataConstraint_RemoveParentRow", new object[]
			{
				constraint.ConstraintName
			}));
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0005D340 File Offset: 0x0005C740
		public static string MaxLengthViolationText(string columnName)
		{
			return Res.GetString("DataColumn_ExceedMaxLength", new object[]
			{
				columnName
			});
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0005D364 File Offset: 0x0005C764
		public static string NotAllowDBNullViolationText(string columnName)
		{
			return Res.GetString("DataColumn_NotAllowDBNull", new object[]
			{
				columnName
			});
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0005D388 File Offset: 0x0005C788
		public static Exception CantAddConstraintToMultipleNestedTable(string tableName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataConstraint_CantAddConstraintToMultipleNestedTable", new object[]
			{
				tableName
			}));
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0005D3B0 File Offset: 0x0005C7B0
		public static Exception AutoIncrementAndExpression()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_AutoIncrementAndExpression"));
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0005D3CC File Offset: 0x0005C7CC
		public static Exception AutoIncrementAndDefaultValue()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_AutoIncrementAndDefaultValue"));
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0005D3E8 File Offset: 0x0005C7E8
		public static Exception AutoIncrementSeed()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_AutoIncrementSeed"));
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0005D404 File Offset: 0x0005C804
		public static Exception CantChangeDataType()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ChangeDataType"));
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0005D420 File Offset: 0x0005C820
		public static Exception NullDataType()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_NullDataType"));
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0005D43C File Offset: 0x0005C83C
		public static Exception ColumnNameRequired()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_NameRequired"));
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0005D458 File Offset: 0x0005C858
		public static Exception DefaultValueAndAutoIncrement()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_DefaultValueAndAutoIncrement"));
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0005D474 File Offset: 0x0005C874
		public static Exception DefaultValueDataType(string column, Type defaultType, Type columnType, Exception inner)
		{
			if (column.Length == 0)
			{
				return ExceptionBuilder._Argument(Res.GetString("DataColumn_DefaultValueDataType1", new object[]
				{
					defaultType.FullName,
					columnType.FullName
				}), inner);
			}
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_DefaultValueDataType", new object[]
			{
				column,
				defaultType.FullName,
				columnType.FullName
			}), inner);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0005D4E0 File Offset: 0x0005C8E0
		public static Exception DefaultValueColumnDataType(string column, Type defaultType, Type columnType, Exception inner)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_DefaultValueColumnDataType", new object[]
			{
				column,
				defaultType.FullName,
				columnType.FullName
			}), inner);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0005D51C File Offset: 0x0005C91C
		public static Exception ExpressionAndUnique()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ExpressionAndUnique"));
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0005D538 File Offset: 0x0005C938
		public static Exception ExpressionAndReadOnly()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ExpressionAndReadOnly"));
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0005D554 File Offset: 0x0005C954
		public static Exception ExpressionAndConstraint(DataColumn column, Constraint constraint)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ExpressionAndConstraint", new object[]
			{
				column.ColumnName,
				constraint.ConstraintName
			}));
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0005D588 File Offset: 0x0005C988
		public static Exception ExpressionInConstraint(DataColumn column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ExpressionInConstraint", new object[]
			{
				column.ColumnName
			}));
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0005D5B4 File Offset: 0x0005C9B4
		public static Exception ExpressionCircular()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_ExpressionCircular"));
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0005D5D0 File Offset: 0x0005C9D0
		public static Exception NonUniqueValues(string column)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataColumn_NonUniqueValues", new object[]
			{
				column
			}));
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0005D5F8 File Offset: 0x0005C9F8
		public static Exception NullKeyValues(string column)
		{
			return ExceptionBuilder._Data(Res.GetString("DataColumn_NullKeyValues", new object[]
			{
				column
			}));
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0005D620 File Offset: 0x0005CA20
		public static Exception NullValues(string column)
		{
			return ExceptionBuilder._NoNullAllowed(Res.GetString("DataColumn_NullValues", new object[]
			{
				column
			}));
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0005D648 File Offset: 0x0005CA48
		public static Exception ReadOnlyAndExpression()
		{
			return ExceptionBuilder._ReadOnly(Res.GetString("DataColumn_ReadOnlyAndExpression"));
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0005D664 File Offset: 0x0005CA64
		public static Exception ReadOnly(string column)
		{
			return ExceptionBuilder._ReadOnly(Res.GetString("DataColumn_ReadOnly", new object[]
			{
				column
			}));
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0005D68C File Offset: 0x0005CA8C
		public static Exception UniqueAndExpression()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_UniqueAndExpression"));
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0005D6A8 File Offset: 0x0005CAA8
		public static Exception SetFailed(object value, DataColumn column, Type type, Exception innerException)
		{
			return ExceptionBuilder._Argument(innerException.Message + Res.GetString("DataColumn_SetFailed", new object[]
			{
				value.ToString(),
				column.ColumnName,
				type.Name
			}), innerException);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0005D6F4 File Offset: 0x0005CAF4
		public static Exception CannotSetToNull(DataColumn column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotSetToNull", new object[]
			{
				column.ColumnName
			}));
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0005D720 File Offset: 0x0005CB20
		public static Exception LongerThanMaxLength(DataColumn column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_LongerThanMaxLength", new object[]
			{
				column.ColumnName
			}));
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0005D74C File Offset: 0x0005CB4C
		public static Exception CannotSetMaxLength(DataColumn column, int value)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotSetMaxLength", new object[]
			{
				column.ColumnName,
				value.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0005D788 File Offset: 0x0005CB88
		public static Exception CannotSetMaxLength2(DataColumn column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotSetMaxLength2", new object[]
			{
				column.ColumnName
			}));
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0005D7B4 File Offset: 0x0005CBB4
		public static Exception CannotSetSimpleContentType(string columnName, Type type)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotSimpleContentType", new object[]
			{
				columnName,
				type
			}));
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0005D7E0 File Offset: 0x0005CBE0
		public static Exception CannotSetSimpleContent(string columnName, Type type)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotSimpleContent", new object[]
			{
				columnName,
				type
			}));
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0005D80C File Offset: 0x0005CC0C
		public static Exception CannotChangeNamespace(string columnName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_CannotChangeNamespace", new object[]
			{
				columnName
			}));
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0005D834 File Offset: 0x0005CC34
		public static Exception HasToBeStringType(DataColumn column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_HasToBeStringType", new object[]
			{
				column.ColumnName
			}));
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0005D860 File Offset: 0x0005CC60
		public static Exception AutoIncrementCannotSetIfHasData(string typeName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_AutoIncrementCannotSetIfHasData", new object[]
			{
				typeName
			}));
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0005D888 File Offset: 0x0005CC88
		public static Exception INullableUDTwithoutStaticNull(string typeName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_INullableUDTwithoutStaticNull", new object[]
			{
				typeName
			}));
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0005D8B0 File Offset: 0x0005CCB0
		public static Exception IComparableNotImplemented(string typeName)
		{
			return ExceptionBuilder._Data(Res.GetString("DataStorage_IComparableNotDefined", new object[]
			{
				typeName
			}));
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0005D8D8 File Offset: 0x0005CCD8
		public static Exception UDTImplementsIChangeTrackingButnotIRevertible(string typeName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataColumn_UDTImplementsIChangeTrackingButnotIRevertible", new object[]
			{
				typeName
			}));
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0005D900 File Offset: 0x0005CD00
		public static Exception SetAddedAndModifiedCalledOnnonUnchanged()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataColumn_SetAddedAndModifiedCalledOnNonUnchanged"));
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0005D91C File Offset: 0x0005CD1C
		public static Exception InvalidDataColumnMapping(Type type)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumn_InvalidDataColumnMapping", new object[]
			{
				type.AssemblyQualifiedName
			}));
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0005D948 File Offset: 0x0005CD48
		public static Exception CannotSetDateTimeModeForNonDateTimeColumns()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataColumn_CannotSetDateTimeModeForNonDateTimeColumns"));
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0005D964 File Offset: 0x0005CD64
		public static Exception InvalidDateTimeMode(DataSetDateTime mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<DataSetDateTime>(mode);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0005D978 File Offset: 0x0005CD78
		public static Exception CantChangeDateTimeMode(DataSetDateTime oldValue, DataSetDateTime newValue)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataColumn_DateTimeMode", new object[]
			{
				oldValue.ToString(),
				newValue.ToString()
			}));
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0005D9BC File Offset: 0x0005CDBC
		public static Exception ColumnTypeNotSupported()
		{
			return ADP.NotSupported(Res.GetString("DataColumn_NullableTypesNotSupported"));
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0005D9D8 File Offset: 0x0005CDD8
		public static Exception SetFailed(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_SetFailed", new object[]
			{
				name
			}));
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0005DA00 File Offset: 0x0005CE00
		public static Exception SetDataSetFailed()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_SetDataSetFailed"));
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0005DA1C File Offset: 0x0005CE1C
		public static Exception SetRowStateFilter()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_SetRowStateFilter"));
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0005DA38 File Offset: 0x0005CE38
		public static Exception CanNotSetDataSet()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotSetDataSet"));
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0005DA54 File Offset: 0x0005CE54
		public static Exception CanNotUseDataViewManager()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotUseDataViewManager"));
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0005DA70 File Offset: 0x0005CE70
		public static Exception CanNotSetTable()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotSetTable"));
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0005DA8C File Offset: 0x0005CE8C
		public static Exception CanNotUse()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotUse"));
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0005DAA8 File Offset: 0x0005CEA8
		public static Exception CanNotBindTable()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotBindTable"));
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0005DAC4 File Offset: 0x0005CEC4
		public static Exception SetTable()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_SetTable"));
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0005DAE0 File Offset: 0x0005CEE0
		public static Exception SetIListObject()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_SetIListObject"));
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0005DAFC File Offset: 0x0005CEFC
		public static Exception AddNewNotAllowNull()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_AddNewNotAllowNull"));
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0005DB18 File Offset: 0x0005CF18
		public static Exception NotOpen()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_NotOpen"));
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0005DB34 File Offset: 0x0005CF34
		public static Exception CreateChildView()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_CreateChildView"));
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0005DB50 File Offset: 0x0005CF50
		public static Exception CanNotDelete()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotDelete"));
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0005DB6C File Offset: 0x0005CF6C
		public static Exception CanNotEdit()
		{
			return ExceptionBuilder._Data(Res.GetString("DataView_CanNotEdit"));
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0005DB88 File Offset: 0x0005CF88
		public static Exception GetElementIndex(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataView_GetElementIndex", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0005DBBC File Offset: 0x0005CFBC
		public static Exception AddExternalObject()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_AddExternalObject"));
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0005DBD8 File Offset: 0x0005CFD8
		public static Exception CanNotClear()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_CanNotClear"));
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0005DBF4 File Offset: 0x0005CFF4
		public static Exception InsertExternalObject()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_InsertExternalObject"));
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0005DC10 File Offset: 0x0005D010
		public static Exception RemoveExternalObject()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataView_RemoveExternalObject"));
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0005DC2C File Offset: 0x0005D02C
		public static Exception PropertyNotFound(string property, string table)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataROWView_PropertyNotFound", new object[]
			{
				property,
				table
			}));
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0005DC58 File Offset: 0x0005D058
		public static Exception ColumnToSortIsOutOfRange(string column)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataColumns_OutOfRange", new object[]
			{
				column
			}));
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0005DC80 File Offset: 0x0005D080
		public static Exception KeyTableMismatch()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataKey_TableMismatch"));
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0005DC9C File Offset: 0x0005D09C
		public static Exception KeyNoColumns()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataKey_NoColumns"));
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0005DCB8 File Offset: 0x0005D0B8
		public static Exception KeyTooManyColumns(int cols)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataKey_TooManyColumns", new object[]
			{
				cols.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0005DCEC File Offset: 0x0005D0EC
		public static Exception KeyDuplicateColumns(string columnName)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataKey_DuplicateColumns", new object[]
			{
				columnName
			}));
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0005DD14 File Offset: 0x0005D114
		public static Exception RelationDataSetMismatch()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_DataSetMismatch"));
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0005DD30 File Offset: 0x0005D130
		public static Exception NoRelationName()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_NoName"));
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0005DD4C File Offset: 0x0005D14C
		public static Exception ColumnsTypeMismatch()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_ColumnsTypeMismatch"));
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0005DD68 File Offset: 0x0005D168
		public static Exception KeyLengthMismatch()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_KeyLengthMismatch"));
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0005DD84 File Offset: 0x0005D184
		public static Exception KeyLengthZero()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_KeyZeroLength"));
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0005DDA0 File Offset: 0x0005D1A0
		public static Exception ForeignRelation()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_ForeignDataSet"));
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0005DDBC File Offset: 0x0005D1BC
		public static Exception KeyColumnsIdentical()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_KeyColumnsIdentical"));
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0005DDD8 File Offset: 0x0005D1D8
		public static Exception RelationForeignTable(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_ForeignTable", new object[]
			{
				t1,
				t2
			}));
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0005DE04 File Offset: 0x0005D204
		public static Exception GetParentRowTableMismatch(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_GetParentRowTableMismatch", new object[]
			{
				t1,
				t2
			}));
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0005DE30 File Offset: 0x0005D230
		public static Exception SetParentRowTableMismatch(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_SetParentRowTableMismatch", new object[]
			{
				t1,
				t2
			}));
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0005DE5C File Offset: 0x0005D25C
		public static Exception RelationForeignRow()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_ForeignRow"));
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0005DE78 File Offset: 0x0005D278
		public static Exception RelationNestedReadOnly()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_RelationNestedReadOnly"));
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0005DE94 File Offset: 0x0005D294
		public static Exception TableCantBeNestedInTwoTables(string tableName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_TableCantBeNestedInTwoTables", new object[]
			{
				tableName
			}));
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0005DEBC File Offset: 0x0005D2BC
		public static Exception LoopInNestedRelations(string tableName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_LoopInNestedRelations", new object[]
			{
				tableName
			}));
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0005DEE4 File Offset: 0x0005D2E4
		public static Exception RelationDoesNotExist()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_DoesNotExist"));
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0005DF00 File Offset: 0x0005D300
		public static Exception ParentRowNotInTheDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_ParentRowNotInTheDataSet"));
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0005DF1C File Offset: 0x0005D31C
		public static Exception ParentOrChildColumnsDoNotHaveDataSet()
		{
			return ExceptionBuilder._InvalidConstraint(Res.GetString("DataRelation_ParentOrChildColumnsDoNotHaveDataSet"));
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0005DF38 File Offset: 0x0005D338
		public static Exception InValidNestedRelation(string childTableName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataRelation_InValidNestedRelation", new object[]
			{
				childTableName
			}));
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0005DF60 File Offset: 0x0005D360
		public static Exception InvalidParentNamespaceinNestedRelation(string childTableName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataRelation_InValidNamespaceInNestedRelation", new object[]
			{
				childTableName
			}));
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0005DF88 File Offset: 0x0005D388
		public static Exception RowNotInTheDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_NotInTheDataSet"));
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0005DFA4 File Offset: 0x0005D3A4
		public static Exception RowNotInTheTable()
		{
			return ExceptionBuilder._RowNotInTable(Res.GetString("DataRow_NotInTheTable"));
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0005DFC0 File Offset: 0x0005D3C0
		public static Exception EditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent(Res.GetString("DataRow_EditInRowChanging"));
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0005DFDC File Offset: 0x0005D3DC
		public static Exception EndEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent(Res.GetString("DataRow_EndEditInRowChanging"));
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0005DFF8 File Offset: 0x0005D3F8
		public static Exception BeginEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent(Res.GetString("DataRow_BeginEditInRowChanging"));
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0005E014 File Offset: 0x0005D414
		public static Exception CancelEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent(Res.GetString("DataRow_CancelEditInRowChanging"));
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0005E030 File Offset: 0x0005D430
		public static Exception DeleteInRowDeleting()
		{
			return ExceptionBuilder._InRowChangingEvent(Res.GetString("DataRow_DeleteInRowDeleting"));
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0005E04C File Offset: 0x0005D44C
		public static Exception ValueArrayLength()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_ValuesArrayLength"));
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0005E068 File Offset: 0x0005D468
		public static Exception NoCurrentData()
		{
			return ExceptionBuilder._VersionNotFound(Res.GetString("DataRow_NoCurrentData"));
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0005E084 File Offset: 0x0005D484
		public static Exception NoOriginalData()
		{
			return ExceptionBuilder._VersionNotFound(Res.GetString("DataRow_NoOriginalData"));
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0005E0A0 File Offset: 0x0005D4A0
		public static Exception NoProposedData()
		{
			return ExceptionBuilder._VersionNotFound(Res.GetString("DataRow_NoProposedData"));
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0005E0BC File Offset: 0x0005D4BC
		public static Exception RowRemovedFromTheTable()
		{
			return ExceptionBuilder._RowNotInTable(Res.GetString("DataRow_RemovedFromTheTable"));
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0005E0D8 File Offset: 0x0005D4D8
		public static Exception DeletedRowInaccessible()
		{
			return ExceptionBuilder._DeletedRowInaccessible(Res.GetString("DataRow_DeletedRowInaccessible"));
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0005E0F4 File Offset: 0x0005D4F4
		public static Exception RowAlreadyDeleted()
		{
			return ExceptionBuilder._DeletedRowInaccessible(Res.GetString("DataRow_AlreadyDeleted"));
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0005E110 File Offset: 0x0005D510
		public static Exception RowEmpty()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_Empty"));
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0005E12C File Offset: 0x0005D52C
		public static Exception InvalidRowVersion()
		{
			return ExceptionBuilder._Data(Res.GetString("DataRow_InvalidVersion"));
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0005E148 File Offset: 0x0005D548
		public static Exception RowOutOfRange()
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRow_RowOutOfRange"));
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0005E164 File Offset: 0x0005D564
		public static Exception RowOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRow_OutOfRange", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0005E198 File Offset: 0x0005D598
		public static Exception RowInsertOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRow_RowInsertOutOfRange", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0005E1CC File Offset: 0x0005D5CC
		public static Exception RowInsertTwice(int index, string tableName)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRow_RowInsertTwice", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture),
				tableName
			}));
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0005E204 File Offset: 0x0005D604
		public static Exception RowInsertMissing(string tableName)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRow_RowInsertMissing", new object[]
			{
				tableName
			}));
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0005E22C File Offset: 0x0005D62C
		public static Exception RowAlreadyRemoved()
		{
			return ExceptionBuilder._Data(Res.GetString("DataRow_AlreadyRemoved"));
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0005E248 File Offset: 0x0005D648
		public static Exception MultipleParents()
		{
			return ExceptionBuilder._Data(Res.GetString("DataRow_MultipleParents"));
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0005E264 File Offset: 0x0005D664
		public static Exception InvalidRowState(DataRowState state)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<DataRowState>(state);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0005E278 File Offset: 0x0005D678
		public static Exception InvalidRowBitPattern()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_InvalidRowBitPattern"));
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0005E294 File Offset: 0x0005D694
		internal static Exception SetDataSetNameToEmpty()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataSet_SetNameToEmpty"));
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0005E2B0 File Offset: 0x0005D6B0
		internal static Exception SetDataSetNameConflicting(string name)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataSet_SetDataSetNameConflicting", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0005E2D8 File Offset: 0x0005D6D8
		public static Exception DataSetUnsupportedSchema(string ns)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataSet_UnsupportedSchema", new object[]
			{
				ns
			}));
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0005E300 File Offset: 0x0005D700
		public static Exception MergeMissingDefinition(string obj)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataMerge_MissingDefinition", new object[]
			{
				obj
			}));
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0005E328 File Offset: 0x0005D728
		public static Exception TablesInDifferentSets()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_TablesInDifferentSets"));
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0005E344 File Offset: 0x0005D744
		public static Exception RelationAlreadyExists()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_AlreadyExists"));
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0005E360 File Offset: 0x0005D760
		public static Exception RowAlreadyInOtherCollection()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_AlreadyInOtherCollection"));
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0005E37C File Offset: 0x0005D77C
		public static Exception RowAlreadyInTheCollection()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRow_AlreadyInTheCollection"));
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0005E398 File Offset: 0x0005D798
		public static Exception TableMissingPrimaryKey()
		{
			return ExceptionBuilder._MissingPrimaryKey(Res.GetString("DataTable_MissingPrimaryKey"));
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0005E3B4 File Offset: 0x0005D7B4
		public static Exception RecordStateRange()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataIndex_RecordStateRange"));
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0005E3D0 File Offset: 0x0005D7D0
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

		// Token: 0x06000A22 RID: 2594 RVA: 0x0005E424 File Offset: 0x0005D824
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

		// Token: 0x06000A23 RID: 2595 RVA: 0x0005E46C File Offset: 0x0005D86C
		public static Exception RelationAlreadyInOtherDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_AlreadyInOtherDataSet"));
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0005E488 File Offset: 0x0005D888
		public static Exception RelationAlreadyInTheDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_AlreadyInTheDataSet"));
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0005E4A4 File Offset: 0x0005D8A4
		public static Exception RelationNotInTheDataSet(string relation)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_NotInTheDataSet", new object[]
			{
				relation
			}));
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0005E4CC File Offset: 0x0005D8CC
		public static Exception RelationOutOfRange(object index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataRelation_OutOfRange", new object[]
			{
				Convert.ToString(index, null)
			}));
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0005E4F8 File Offset: 0x0005D8F8
		public static Exception DuplicateRelation(string relation)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataRelation_DuplicateName", new object[]
			{
				relation
			}));
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0005E520 File Offset: 0x0005D920
		public static Exception RelationTableNull()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_TableNull"));
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0005E53C File Offset: 0x0005D93C
		public static Exception RelationDataSetNull()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_TableNull"));
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0005E558 File Offset: 0x0005D958
		public static Exception RelationTableWasRemoved()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_TableWasRemoved"));
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0005E574 File Offset: 0x0005D974
		public static Exception ParentTableMismatch()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_ParentTableMismatch"));
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0005E590 File Offset: 0x0005D990
		public static Exception ChildTableMismatch()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_ChildTableMismatch"));
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0005E5AC File Offset: 0x0005D9AC
		public static Exception EnforceConstraint()
		{
			return ExceptionBuilder._Constraint(Res.GetString("Data_EnforceConstraints"));
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0005E5C8 File Offset: 0x0005D9C8
		public static Exception CaseLocaleMismatch()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataRelation_CaseLocaleMismatch"));
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0005E5E4 File Offset: 0x0005D9E4
		public static Exception CannotChangeCaseLocale()
		{
			return ExceptionBuilder.CannotChangeCaseLocale(null);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0005E5F8 File Offset: 0x0005D9F8
		public static Exception CannotChangeCaseLocale(Exception innerException)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataSet_CannotChangeCaseLocale"), innerException);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0005E618 File Offset: 0x0005DA18
		public static Exception CannotChangeSchemaSerializationMode()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataSet_CannotChangeSchemaSerializationMode"));
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0005E634 File Offset: 0x0005DA34
		public static Exception InvalidSchemaSerializationMode(Type enumType, string mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException(Res.GetString("ADP_InvalidEnumerationValue", new object[]
			{
				enumType.Name,
				mode
			}));
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0005E664 File Offset: 0x0005DA64
		public static Exception InvalidRemotingFormat(SerializationFormat mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<SerializationFormat>(mode);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0005E678 File Offset: 0x0005DA78
		public static Exception TableForeignPrimaryKey()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_ForeignPrimaryKey"));
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0005E694 File Offset: 0x0005DA94
		public static Exception TableCannotAddToSimpleContent()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_CannotAddToSimpleContent"));
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0005E6B0 File Offset: 0x0005DAB0
		public static Exception NoTableName()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_NoName"));
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0005E6CC File Offset: 0x0005DACC
		public static Exception MultipleTextOnlyColumns()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_MultipleSimpleContentColumns"));
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0005E6E8 File Offset: 0x0005DAE8
		public static Exception InvalidSortString(string sort)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_InvalidSortString", new object[]
			{
				sort
			}));
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0005E710 File Offset: 0x0005DB10
		public static Exception DuplicateTableName(string table)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataTable_DuplicateName", new object[]
			{
				table
			}));
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0005E738 File Offset: 0x0005DB38
		public static Exception DuplicateTableName2(string table, string ns)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataTable_DuplicateName2", new object[]
			{
				table,
				ns
			}));
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0005E764 File Offset: 0x0005DB64
		public static Exception SelfnestedDatasetConflictingName(string table)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataTable_SelfnestedDatasetConflictingName", new object[]
			{
				table
			}));
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0005E78C File Offset: 0x0005DB8C
		public static Exception DatasetConflictingName(string table)
		{
			return ExceptionBuilder._DuplicateName(Res.GetString("DataTable_DatasetConflictingName", new object[]
			{
				table
			}));
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0005E7B4 File Offset: 0x0005DBB4
		public static Exception TableAlreadyInOtherDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_AlreadyInOtherDataSet"));
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0005E7D0 File Offset: 0x0005DBD0
		public static Exception TableAlreadyInTheDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_AlreadyInTheDataSet"));
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0005E7EC File Offset: 0x0005DBEC
		public static Exception TableOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(Res.GetString("DataTable_OutOfRange", new object[]
			{
				index.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0005E820 File Offset: 0x0005DC20
		public static Exception TableNotInTheDataSet(string table)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_NotInTheDataSet", new object[]
			{
				table
			}));
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0005E848 File Offset: 0x0005DC48
		public static Exception TableInRelation()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_InRelation"));
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0005E864 File Offset: 0x0005DC64
		public static Exception TableInConstraint(DataTable table, Constraint constraint)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_InConstraint", new object[]
			{
				table.TableName,
				constraint.ConstraintName
			}));
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0005E898 File Offset: 0x0005DC98
		public static Exception CanNotSerializeDataTableHierarchy()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataTable_CanNotSerializeDataTableHierarchy"));
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0005E8B4 File Offset: 0x0005DCB4
		public static Exception CanNotRemoteDataTable()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataTable_CanNotRemoteDataTable"));
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0005E8D0 File Offset: 0x0005DCD0
		public static Exception CanNotSetRemotingFormat()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_CanNotSetRemotingFormat"));
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0005E8EC File Offset: 0x0005DCEC
		public static Exception CanNotSerializeDataTableWithEmptyName()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataTable_CanNotSerializeDataTableWithEmptyName"));
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0005E908 File Offset: 0x0005DD08
		public static Exception TableNotFound(string tableName)
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTable_TableNotFound", new object[]
			{
				tableName
			}));
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0005E930 File Offset: 0x0005DD30
		public static Exception AggregateException(AggregateType aggregateType, Type type)
		{
			return ExceptionBuilder._Data(Res.GetString("DataStorage_AggregateException", new object[]
			{
				aggregateType.ToString(),
				type.Name
			}));
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0005E96C File Offset: 0x0005DD6C
		public static Exception InvalidStorageType(TypeCode typecode)
		{
			return ExceptionBuilder._Data(Res.GetString("DataStorage_InvalidStorageType", new object[]
			{
				typecode.ToString()
			}));
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x0005E99C File Offset: 0x0005DD9C
		public static Exception RangeArgument(int min, int max)
		{
			return ExceptionBuilder._Argument(Res.GetString("Range_Argument", new object[]
			{
				min.ToString(CultureInfo.InvariantCulture),
				max.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0005E9DC File Offset: 0x0005DDDC
		public static Exception NullRange()
		{
			return ExceptionBuilder._Data(Res.GetString("Range_NullRange"));
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0005E9F8 File Offset: 0x0005DDF8
		public static Exception NegativeMinimumCapacity()
		{
			return ExceptionBuilder._Argument(Res.GetString("RecordManager_MinimumCapacity"));
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0005EA14 File Offset: 0x0005DE14
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

		// Token: 0x06000A4E RID: 2638 RVA: 0x0005EA58 File Offset: 0x0005DE58
		public static Exception StorageSetFailed()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataStorage_SetInvalidDataType"));
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0005EA74 File Offset: 0x0005DE74
		public static Exception SimpleTypeNotSupported()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_SimpleTypeNotSupported"));
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0005EA90 File Offset: 0x0005DE90
		public static Exception MissingAttribute(string attribute)
		{
			return ExceptionBuilder.MissingAttribute(string.Empty, attribute);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0005EAA8 File Offset: 0x0005DEA8
		public static Exception MissingAttribute(string element, string attribute)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MissingAttribute", new object[]
			{
				element,
				attribute
			}));
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0005EAD4 File Offset: 0x0005DED4
		public static Exception InvalidAttributeValue(string name, string value)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_ValueOutOfRange", new object[]
			{
				name,
				value
			}));
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0005EB00 File Offset: 0x0005DF00
		public static Exception AttributeValues(string name, string value1, string value2)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_AttributeValues", new object[]
			{
				name,
				value1,
				value2
			}));
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0005EB30 File Offset: 0x0005DF30
		public static Exception ElementTypeNotFound(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_ElementTypeNotFound", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0005EB58 File Offset: 0x0005DF58
		public static Exception RelationParentNameMissing(string rel)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_RelationParentNameMissing", new object[]
			{
				rel
			}));
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0005EB80 File Offset: 0x0005DF80
		public static Exception RelationChildNameMissing(string rel)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_RelationChildNameMissing", new object[]
			{
				rel
			}));
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0005EBA8 File Offset: 0x0005DFA8
		public static Exception RelationTableKeyMissing(string rel)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_RelationTableKeyMissing", new object[]
			{
				rel
			}));
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0005EBD0 File Offset: 0x0005DFD0
		public static Exception RelationChildKeyMissing(string rel)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_RelationChildKeyMissing", new object[]
			{
				rel
			}));
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0005EBF8 File Offset: 0x0005DFF8
		public static Exception UndefinedDatatype(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_UndefinedDatatype", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0005EC20 File Offset: 0x0005E020
		public static Exception DatatypeNotDefined()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_DatatypeNotDefined"));
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0005EC3C File Offset: 0x0005E03C
		public static Exception MismatchKeyLength()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MismatchKeyLength"));
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0005EC58 File Offset: 0x0005E058
		public static Exception InvalidField(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_InvalidField", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0005EC80 File Offset: 0x0005E080
		public static Exception InvalidSelector(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_InvalidSelector", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0005ECA8 File Offset: 0x0005E0A8
		public static Exception CircularComplexType(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_CircularComplexType", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x0005ECD0 File Offset: 0x0005E0D0
		public static Exception CannotInstantiateAbstract(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_CannotInstantiateAbstract", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A60 RID: 2656 RVA: 0x0005ECF8 File Offset: 0x0005E0F8
		public static Exception InvalidKey(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_InvalidKey", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x0005ED20 File Offset: 0x0005E120
		public static Exception DiffgramMissingTable(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MissingTable", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x0005ED48 File Offset: 0x0005E148
		public static Exception DiffgramMissingSQL()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MissingSQL"));
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0005ED64 File Offset: 0x0005E164
		public static Exception DuplicateConstraintRead(string str)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_DuplicateConstraint", new object[]
			{
				str
			}));
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0005ED8C File Offset: 0x0005E18C
		public static Exception ColumnTypeConflict(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_ColumnConflict", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0005EDB4 File Offset: 0x0005E1B4
		public static Exception CannotConvert(string name, string type)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_CannotConvert", new object[]
			{
				name,
				type
			}));
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0005EDE0 File Offset: 0x0005E1E0
		public static Exception MissingRefer(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MissingRefer", new object[]
			{
				"refer",
				"keyref",
				name
			}));
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0005EE18 File Offset: 0x0005E218
		public static Exception InvalidPrefix(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_InvalidPrefix", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0005EE40 File Offset: 0x0005E240
		public static Exception CanNotDeserializeObjectType()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("Xml_CanNotDeserializeObjectType"));
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0005EE5C File Offset: 0x0005E25C
		public static Exception IsDataSetAttributeMissingInSchema()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_IsDataSetAttributeMissingInSchema"));
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0005EE78 File Offset: 0x0005E278
		public static Exception TooManyIsDataSetAtributeInSchema()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_TooManyIsDataSetAtributeInSchema"));
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0005EE94 File Offset: 0x0005E294
		public static Exception NestedCircular(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_NestedCircular", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0005EEBC File Offset: 0x0005E2BC
		public static Exception MultipleParentRows(string tableQName)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MultipleParentRows", new object[]
			{
				tableQName
			}));
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0005EEE4 File Offset: 0x0005E2E4
		public static Exception PolymorphismNotSupported(string typeName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("Xml_PolymorphismNotSupported", new object[]
			{
				typeName
			}));
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0005EF0C File Offset: 0x0005E30C
		public static Exception DataTableInferenceNotSupported()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("Xml_DataTableInferenceNotSupported"));
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0005EF28 File Offset: 0x0005E328
		internal static void ThrowMultipleTargetConverter(Exception innerException)
		{
			string name = (innerException != null) ? "Xml_MultipleTargetConverterError" : "Xml_MultipleTargetConverterEmpty";
			ExceptionBuilder.ThrowDataException(Res.GetString(name), innerException);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0005EF54 File Offset: 0x0005E354
		public static Exception DuplicateDeclaration(string name)
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_MergeDuplicateDeclaration", new object[]
			{
				name
			}));
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0005EF7C File Offset: 0x0005E37C
		public static Exception FoundEntity()
		{
			return ExceptionBuilder._Data(Res.GetString("Xml_FoundEntity"));
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0005EF98 File Offset: 0x0005E398
		public static Exception MergeFailed(string name)
		{
			return ExceptionBuilder._Data(name);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0005EFAC File Offset: 0x0005E3AC
		public static DataException ConvertFailed(Type type1, Type type2)
		{
			return ExceptionBuilder._Data(Res.GetString("SqlConvert_ConvertFailed", new object[]
			{
				type1.FullName,
				type2.FullName
			}));
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0005EFE0 File Offset: 0x0005E3E0
		public static Exception InvalidDataTableReader(string tableName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataTableReader_InvalidDataTableReader", new object[]
			{
				tableName
			}));
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0005F008 File Offset: 0x0005E408
		public static Exception DataTableReaderSchemaIsInvalid(string tableName)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("DataTableReader_SchemaInvalidDataTableReader", new object[]
			{
				tableName
			}));
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0005F030 File Offset: 0x0005E430
		public static Exception CannotCreateDataReaderOnEmptyDataSet()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTableReader_CannotCreateDataReaderOnEmptyDataSet"));
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0005F04C File Offset: 0x0005E44C
		public static Exception DataTableReaderArgumentIsEmpty()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTableReader_DataTableReaderArgumentIsEmpty"));
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0005F068 File Offset: 0x0005E468
		public static Exception ArgumentContainsNullValue()
		{
			return ExceptionBuilder._Argument(Res.GetString("DataTableReader_ArgumentContainsNullValue"));
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0005F084 File Offset: 0x0005E484
		public static Exception InvalidCurrentRowInDataTableReader()
		{
			return ExceptionBuilder._DeletedRowInaccessible(Res.GetString("DataTableReader_InvalidRowInDataTableReader"));
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0005F0A0 File Offset: 0x0005E4A0
		public static Exception EmptyDataTableReader(string tableName)
		{
			return ExceptionBuilder._DeletedRowInaccessible(Res.GetString("DataTableReader_DataTableCleared", new object[]
			{
				tableName
			}));
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0005F0C8 File Offset: 0x0005E4C8
		internal static Exception InvalidDuplicateNamedSimpleTypeDelaration(string stName, string errorStr)
		{
			return ExceptionBuilder._Argument(Res.GetString("NamedSimpleType_InvalidDuplicateNamedSimpleTypeDelaration", new object[]
			{
				stName,
				errorStr
			}));
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0005F0F4 File Offset: 0x0005E4F4
		internal static Exception InternalRBTreeError(RBTreeError internalError)
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("RbTree_InvalidState", new object[]
			{
				(int)internalError
			}));
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0005F120 File Offset: 0x0005E520
		public static Exception EnumeratorModified()
		{
			return ExceptionBuilder._InvalidOperation(Res.GetString("RbTree_EnumerationBroken"));
		}
	}
}
