using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x020002B0 RID: 688
	internal sealed class OdbcStatementHandle : OdbcHandle
	{
		// Token: 0x060029C1 RID: 10689 RVA: 0x00114C70 File Offset: 0x00114070
		internal OdbcStatementHandle(OdbcConnectionHandle connectionHandle) : base(ODBC32.SQL_HANDLE.STMT, connectionHandle)
		{
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x00114C88 File Offset: 0x00114088
		internal ODBC32.RetCode BindColumn2(int columnNumber, ODBC32.SQL_C targetType, HandleRef buffer, IntPtr length, IntPtr srLen_or_Ind)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLBindCol(this, checked((ushort)columnNumber), targetType, buffer, length, srLen_or_Ind);
			ODBC.TraceODBC(3, "SQLBindCol", retCode);
			return retCode;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00114CB4 File Offset: 0x001140B4
		internal ODBC32.RetCode BindColumn3(int columnNumber, ODBC32.SQL_C targetType, IntPtr srLen_or_Ind)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLBindCol(this, checked((ushort)columnNumber), targetType, ADP.PtrZero, ADP.PtrZero, srLen_or_Ind);
			ODBC.TraceODBC(3, "SQLBindCol", retCode);
			return retCode;
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00114CE4 File Offset: 0x001140E4
		internal ODBC32.RetCode BindParameter(short ordinal, short parameterDirection, ODBC32.SQL_C sqlctype, ODBC32.SQL_TYPE sqltype, IntPtr cchSize, IntPtr scale, HandleRef buffer, IntPtr bufferLength, HandleRef intbuffer)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLBindParameter(this, checked((ushort)ordinal), parameterDirection, sqlctype, (short)sqltype, cchSize, scale, buffer, bufferLength, intbuffer);
			ODBC.TraceODBC(3, "SQLBindParameter", retCode);
			return retCode;
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x00114D18 File Offset: 0x00114118
		internal ODBC32.RetCode Cancel()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLCancel(this);
			ODBC.TraceODBC(3, "SQLCancel", retCode);
			return retCode;
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x00114D3C File Offset: 0x0011413C
		internal ODBC32.RetCode CloseCursor()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLCloseCursor(this);
			ODBC.TraceODBC(3, "SQLCloseCursor", retCode);
			return retCode;
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x00114D60 File Offset: 0x00114160
		internal ODBC32.RetCode ColumnAttribute(int columnNumber, short fieldIdentifier, CNativeBuffer characterAttribute, out short stringLength, out SQLLEN numericAttribute)
		{
			IntPtr value;
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLColAttributeW(this, checked((short)columnNumber), fieldIdentifier, characterAttribute, characterAttribute.ShortLength, out stringLength, out value);
			numericAttribute = new SQLLEN(value);
			ODBC.TraceODBC(3, "SQLColAttributeW", retCode);
			return retCode;
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x00114D9C File Offset: 0x0011419C
		internal ODBC32.RetCode Columns(string tableCatalog, string tableSchema, string tableName, string columnName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLColumnsW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), columnName, ODBC.ShortStringLength(columnName));
			ODBC.TraceODBC(3, "SQLColumnsW", retCode);
			return retCode;
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x00114DDC File Offset: 0x001141DC
		internal ODBC32.RetCode Execute()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLExecute(this);
			ODBC.TraceODBC(3, "SQLExecute", retCode);
			return retCode;
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x00114E00 File Offset: 0x00114200
		internal ODBC32.RetCode ExecuteDirect(string commandText)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLExecDirectW(this, commandText, -3);
			ODBC.TraceODBC(3, "SQLExecDirectW", retCode);
			return retCode;
		}

		// Token: 0x060029CB RID: 10699 RVA: 0x00114E24 File Offset: 0x00114224
		internal ODBC32.RetCode Fetch()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLFetch(this);
			ODBC.TraceODBC(3, "SQLFetch", retCode);
			return retCode;
		}

		// Token: 0x060029CC RID: 10700 RVA: 0x00114E48 File Offset: 0x00114248
		internal ODBC32.RetCode FreeStatement(ODBC32.STMT stmt)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLFreeStmt(this, stmt);
			ODBC.TraceODBC(3, "SQLFreeStmt", retCode);
			return retCode;
		}

		// Token: 0x060029CD RID: 10701 RVA: 0x00114E6C File Offset: 0x0011426C
		internal ODBC32.RetCode GetData(int index, ODBC32.SQL_C sqlctype, CNativeBuffer buffer, int cb, out IntPtr cbActual)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetData(this, checked((ushort)index), sqlctype, buffer, new IntPtr(cb), out cbActual);
			ODBC.TraceODBC(3, "SQLGetData", retCode);
			return retCode;
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x00114E9C File Offset: 0x0011429C
		internal ODBC32.RetCode GetStatementAttribute(ODBC32.SQL_ATTR attribute, out IntPtr value, out int stringLength)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetStmtAttrW(this, attribute, out value, ADP.PtrSize, out stringLength);
			ODBC.TraceODBC(3, "SQLGetStmtAttrW", retCode);
			return retCode;
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x00114EC8 File Offset: 0x001142C8
		internal ODBC32.RetCode GetTypeInfo(short fSqlType)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetTypeInfo(this, fSqlType);
			ODBC.TraceODBC(3, "SQLGetTypeInfo", retCode);
			return retCode;
		}

		// Token: 0x060029D0 RID: 10704 RVA: 0x00114EEC File Offset: 0x001142EC
		internal ODBC32.RetCode MoreResults()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLMoreResults(this);
			ODBC.TraceODBC(3, "SQLMoreResults", retCode);
			return retCode;
		}

		// Token: 0x060029D1 RID: 10705 RVA: 0x00114F10 File Offset: 0x00114310
		internal ODBC32.RetCode NumberOfResultColumns(out short columnsAffected)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLNumResultCols(this, out columnsAffected);
			ODBC.TraceODBC(3, "SQLNumResultCols", retCode);
			return retCode;
		}

		// Token: 0x060029D2 RID: 10706 RVA: 0x00114F34 File Offset: 0x00114334
		internal ODBC32.RetCode Prepare(string commandText)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLPrepareW(this, commandText, -3);
			ODBC.TraceODBC(3, "SQLPrepareW", retCode);
			return retCode;
		}

		// Token: 0x060029D3 RID: 10707 RVA: 0x00114F58 File Offset: 0x00114358
		internal ODBC32.RetCode PrimaryKeys(string catalogName, string schemaName, string tableName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLPrimaryKeysW(this, catalogName, ODBC.ShortStringLength(catalogName), schemaName, ODBC.ShortStringLength(schemaName), tableName, ODBC.ShortStringLength(tableName));
			ODBC.TraceODBC(3, "SQLPrimaryKeysW", retCode);
			return retCode;
		}

		// Token: 0x060029D4 RID: 10708 RVA: 0x00114F90 File Offset: 0x00114390
		internal ODBC32.RetCode Procedures(string procedureCatalog, string procedureSchema, string procedureName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLProceduresW(this, procedureCatalog, ODBC.ShortStringLength(procedureCatalog), procedureSchema, ODBC.ShortStringLength(procedureSchema), procedureName, ODBC.ShortStringLength(procedureName));
			ODBC.TraceODBC(3, "SQLProceduresW", retCode);
			return retCode;
		}

		// Token: 0x060029D5 RID: 10709 RVA: 0x00114FC8 File Offset: 0x001143C8
		internal ODBC32.RetCode ProcedureColumns(string procedureCatalog, string procedureSchema, string procedureName, string columnName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLProcedureColumnsW(this, procedureCatalog, ODBC.ShortStringLength(procedureCatalog), procedureSchema, ODBC.ShortStringLength(procedureSchema), procedureName, ODBC.ShortStringLength(procedureName), columnName, ODBC.ShortStringLength(columnName));
			ODBC.TraceODBC(3, "SQLProcedureColumnsW", retCode);
			return retCode;
		}

		// Token: 0x060029D6 RID: 10710 RVA: 0x00115008 File Offset: 0x00114408
		internal ODBC32.RetCode RowCount(out SQLLEN rowCount)
		{
			IntPtr value;
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLRowCount(this, out value);
			rowCount = new SQLLEN(value);
			ODBC.TraceODBC(3, "SQLRowCount", retCode);
			return retCode;
		}

		// Token: 0x060029D7 RID: 10711 RVA: 0x00115038 File Offset: 0x00114438
		internal ODBC32.RetCode SetStatementAttribute(ODBC32.SQL_ATTR attribute, IntPtr value, ODBC32.SQL_IS stringLength)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetStmtAttrW(this, (int)attribute, value, (int)stringLength);
			ODBC.TraceODBC(3, "SQLSetStmtAttrW", retCode);
			return retCode;
		}

		// Token: 0x060029D8 RID: 10712 RVA: 0x0011505C File Offset: 0x0011445C
		internal ODBC32.RetCode SpecialColumns(string quotedTable)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSpecialColumnsW(this, ODBC32.SQL_SPECIALCOLS.ROWVER, null, 0, null, 0, quotedTable, ODBC.ShortStringLength(quotedTable), ODBC32.SQL_SCOPE.SESSION, ODBC32.SQL_NULLABILITY.NO_NULLS);
			ODBC.TraceODBC(3, "SQLSpecialColumnsW", retCode);
			return retCode;
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x0011508C File Offset: 0x0011448C
		internal ODBC32.RetCode Statistics(string tableCatalog, string tableSchema, string tableName, short unique, short accuracy)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLStatisticsW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), unique, accuracy);
			ODBC.TraceODBC(3, "SQLStatisticsW", retCode);
			return retCode;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x001150C8 File Offset: 0x001144C8
		internal ODBC32.RetCode Statistics(string tableName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLStatisticsW(this, null, 0, null, 0, tableName, ODBC.ShortStringLength(tableName), 0, 1);
			ODBC.TraceODBC(3, "SQLStatisticsW", retCode);
			return retCode;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x001150F8 File Offset: 0x001144F8
		internal ODBC32.RetCode Tables(string tableCatalog, string tableSchema, string tableName, string tableType)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLTablesW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), tableType, ODBC.ShortStringLength(tableType));
			ODBC.TraceODBC(3, "SQLTablesW", retCode);
			return retCode;
		}
	}
}
