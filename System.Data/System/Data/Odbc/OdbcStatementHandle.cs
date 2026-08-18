using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x02000206 RID: 518
	internal sealed class OdbcStatementHandle : OdbcHandle
	{
		// Token: 0x06001C84 RID: 7300 RVA: 0x002692F8 File Offset: 0x002686F8
		internal OdbcStatementHandle(OdbcConnectionHandle connectionHandle) : base(ODBC32.SQL_HANDLE.STMT, connectionHandle)
		{
		}

		// Token: 0x06001C85 RID: 7301 RVA: 0x00269318 File Offset: 0x00268718
		internal ODBC32.RetCode BindColumn2(int columnNumber, ODBC32.SQL_C targetType, HandleRef buffer, IntPtr length, IntPtr srLen_or_Ind)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLBindCol(this, checked((ushort)columnNumber), targetType, buffer, length, srLen_or_Ind);
			ODBC.TraceODBC(3, "SQLBindCol", retCode);
			return retCode;
		}

		// Token: 0x06001C86 RID: 7302 RVA: 0x00269348 File Offset: 0x00268748
		internal ODBC32.RetCode BindColumn3(int columnNumber, ODBC32.SQL_C targetType, IntPtr srLen_or_Ind)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLBindCol(this, checked((ushort)columnNumber), targetType, ADP.PtrZero, ADP.PtrZero, srLen_or_Ind);
			ODBC.TraceODBC(3, "SQLBindCol", retCode);
			return retCode;
		}

		// Token: 0x06001C87 RID: 7303 RVA: 0x00269378 File Offset: 0x00268778
		internal ODBC32.RetCode BindParameter(short ordinal, short parameterDirection, ODBC32.SQL_C sqlctype, ODBC32.SQL_TYPE sqltype, IntPtr cchSize, IntPtr scale, HandleRef buffer, IntPtr bufferLength, HandleRef intbuffer)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLBindParameter(this, checked((ushort)ordinal), parameterDirection, sqlctype, (short)sqltype, cchSize, scale, buffer, bufferLength, intbuffer);
			ODBC.TraceODBC(3, "SQLBindParameter", retCode);
			return retCode;
		}

		// Token: 0x06001C88 RID: 7304 RVA: 0x002693B8 File Offset: 0x002687B8
		internal ODBC32.RetCode Cancel()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLCancel(this);
			ODBC.TraceODBC(3, "SQLCancel", retCode);
			return retCode;
		}

		// Token: 0x06001C89 RID: 7305 RVA: 0x002693E8 File Offset: 0x002687E8
		internal ODBC32.RetCode CloseCursor()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLCloseCursor(this);
			ODBC.TraceODBC(3, "SQLCloseCursor", retCode);
			return retCode;
		}

		// Token: 0x06001C8A RID: 7306 RVA: 0x00269418 File Offset: 0x00268818
		internal ODBC32.RetCode ColumnAttribute(int columnNumber, short fieldIdentifier, CNativeBuffer characterAttribute, out short stringLength, out SQLLEN numericAttribute)
		{
			IntPtr value;
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLColAttributeW(this, checked((short)columnNumber), fieldIdentifier, characterAttribute, characterAttribute.ShortLength, out stringLength, out value);
			numericAttribute = new SQLLEN(value);
			ODBC.TraceODBC(3, "SQLColAttributeW", retCode);
			return retCode;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00269458 File Offset: 0x00268858
		internal ODBC32.RetCode Columns(string tableCatalog, string tableSchema, string tableName, string columnName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLColumnsW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), columnName, ODBC.ShortStringLength(columnName));
			ODBC.TraceODBC(3, "SQLColumnsW", retCode);
			return retCode;
		}

		// Token: 0x06001C8C RID: 7308 RVA: 0x00269498 File Offset: 0x00268898
		internal ODBC32.RetCode Execute()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLExecute(this);
			ODBC.TraceODBC(3, "SQLExecute", retCode);
			return retCode;
		}

		// Token: 0x06001C8D RID: 7309 RVA: 0x002694C8 File Offset: 0x002688C8
		internal ODBC32.RetCode ExecuteDirect(string commandText)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLExecDirectW(this, commandText, -3);
			ODBC.TraceODBC(3, "SQLExecDirectW", retCode);
			return retCode;
		}

		// Token: 0x06001C8E RID: 7310 RVA: 0x002694F8 File Offset: 0x002688F8
		internal ODBC32.RetCode Fetch()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLFetch(this);
			ODBC.TraceODBC(3, "SQLFetch", retCode);
			return retCode;
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x00269528 File Offset: 0x00268928
		internal ODBC32.RetCode FreeStatement(ODBC32.STMT stmt)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLFreeStmt(this, stmt);
			ODBC.TraceODBC(3, "SQLFreeStmt", retCode);
			return retCode;
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x00269558 File Offset: 0x00268958
		internal ODBC32.RetCode GetData(int index, ODBC32.SQL_C sqlctype, CNativeBuffer buffer, int cb, out IntPtr cbActual)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetData(this, checked((ushort)index), sqlctype, buffer, new IntPtr(cb), out cbActual);
			ODBC.TraceODBC(3, "SQLGetData", retCode);
			return retCode;
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00269588 File Offset: 0x00268988
		internal ODBC32.RetCode GetStatementAttribute(ODBC32.SQL_ATTR attribute, out IntPtr value, out int stringLength)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetStmtAttrW(this, attribute, out value, ADP.PtrSize, out stringLength);
			ODBC.TraceODBC(3, "SQLGetStmtAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x002695B8 File Offset: 0x002689B8
		internal ODBC32.RetCode GetTypeInfo(short fSqlType)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetTypeInfo(this, fSqlType);
			ODBC.TraceODBC(3, "SQLGetTypeInfo", retCode);
			return retCode;
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x002695E8 File Offset: 0x002689E8
		internal ODBC32.RetCode MoreResults()
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLMoreResults(this);
			ODBC.TraceODBC(3, "SQLMoreResults", retCode);
			return retCode;
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00269618 File Offset: 0x00268A18
		internal ODBC32.RetCode NumberOfResultColumns(out short columnsAffected)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLNumResultCols(this, out columnsAffected);
			ODBC.TraceODBC(3, "SQLNumResultCols", retCode);
			return retCode;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00269648 File Offset: 0x00268A48
		internal ODBC32.RetCode Prepare(string commandText)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLPrepareW(this, commandText, -3);
			ODBC.TraceODBC(3, "SQLPrepareW", retCode);
			return retCode;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00269678 File Offset: 0x00268A78
		internal ODBC32.RetCode PrimaryKeys(string catalogName, string schemaName, string tableName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLPrimaryKeysW(this, catalogName, ODBC.ShortStringLength(catalogName), schemaName, ODBC.ShortStringLength(schemaName), tableName, ODBC.ShortStringLength(tableName));
			ODBC.TraceODBC(3, "SQLPrimaryKeysW", retCode);
			return retCode;
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x002696B8 File Offset: 0x00268AB8
		internal ODBC32.RetCode Procedures(string procedureCatalog, string procedureSchema, string procedureName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLProceduresW(this, procedureCatalog, ODBC.ShortStringLength(procedureCatalog), procedureSchema, ODBC.ShortStringLength(procedureSchema), procedureName, ODBC.ShortStringLength(procedureName));
			ODBC.TraceODBC(3, "SQLProceduresW", retCode);
			return retCode;
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x002696F8 File Offset: 0x00268AF8
		internal ODBC32.RetCode ProcedureColumns(string procedureCatalog, string procedureSchema, string procedureName, string columnName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLProcedureColumnsW(this, procedureCatalog, ODBC.ShortStringLength(procedureCatalog), procedureSchema, ODBC.ShortStringLength(procedureSchema), procedureName, ODBC.ShortStringLength(procedureName), columnName, ODBC.ShortStringLength(columnName));
			ODBC.TraceODBC(3, "SQLProcedureColumnsW", retCode);
			return retCode;
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00269738 File Offset: 0x00268B38
		internal ODBC32.RetCode RowCount(out SQLLEN rowCount)
		{
			IntPtr value;
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLRowCount(this, out value);
			rowCount._value = (long)new SQLLEN(value);
			ODBC.TraceODBC(3, "SQLRowCount", retCode);
			return retCode;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00269778 File Offset: 0x00268B78
		internal ODBC32.RetCode SetStatementAttribute(ODBC32.SQL_ATTR attribute, IntPtr value, ODBC32.SQL_IS stringLength)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetStmtAttrW(this, (int)attribute, value, (int)stringLength);
			ODBC.TraceODBC(3, "SQLSetStmtAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x002697A8 File Offset: 0x00268BA8
		internal ODBC32.RetCode SpecialColumns(string quotedTable)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSpecialColumnsW(this, ODBC32.SQL_SPECIALCOLS.ROWVER, null, 0, null, 0, quotedTable, ODBC.ShortStringLength(quotedTable), ODBC32.SQL_SCOPE.SESSION, ODBC32.SQL_NULLABILITY.NO_NULLS);
			ODBC.TraceODBC(3, "SQLSpecialColumnsW", retCode);
			return retCode;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x002697D8 File Offset: 0x00268BD8
		internal ODBC32.RetCode Statistics(string tableCatalog, string tableSchema, string tableName, short unique, short accuracy)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLStatisticsW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), unique, accuracy);
			ODBC.TraceODBC(3, "SQLStatisticsW", retCode);
			return retCode;
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00269818 File Offset: 0x00268C18
		internal ODBC32.RetCode Statistics(string tableName)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLStatisticsW(this, null, 0, null, 0, tableName, ODBC.ShortStringLength(tableName), 0, 1);
			ODBC.TraceODBC(3, "SQLStatisticsW", retCode);
			return retCode;
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00269848 File Offset: 0x00268C48
		internal ODBC32.RetCode Tables(string tableCatalog, string tableSchema, string tableName, string tableType)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLTablesW(this, tableCatalog, ODBC.ShortStringLength(tableCatalog), tableSchema, ODBC.ShortStringLength(tableSchema), tableName, ODBC.ShortStringLength(tableName), tableType, ODBC.ShortStringLength(tableType));
			ODBC.TraceODBC(3, "SQLTablesW", retCode);
			return retCode;
		}
	}
}
