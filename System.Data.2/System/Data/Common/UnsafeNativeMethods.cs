using System;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Transactions;

namespace System.Data.Common
{
	// Token: 0x0200032B RID: 811
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x060032F6 RID: 13046
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLAllocHandle(ODBC32.SQL_HANDLE HandleType, IntPtr InputHandle, out IntPtr OutputHandle);

		// Token: 0x060032F7 RID: 13047
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLAllocHandle(ODBC32.SQL_HANDLE HandleType, OdbcHandle InputHandle, out IntPtr OutputHandle);

		// Token: 0x060032F8 RID: 13048
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLBindCol(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ODBC32.SQL_C TargetType, HandleRef TargetValue, IntPtr BufferLength, IntPtr StrLen_or_Ind);

		// Token: 0x060032F9 RID: 13049
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLBindCol(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ODBC32.SQL_C TargetType, IntPtr TargetValue, IntPtr BufferLength, IntPtr StrLen_or_Ind);

		// Token: 0x060032FA RID: 13050
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLBindParameter(OdbcStatementHandle StatementHandle, ushort ParameterNumber, short ParamDirection, ODBC32.SQL_C SQLCType, short SQLType, IntPtr cbColDef, IntPtr ibScale, HandleRef rgbValue, IntPtr BufferLength, HandleRef StrLen_or_Ind);

		// Token: 0x060032FB RID: 13051
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLCancel(OdbcStatementHandle StatementHandle);

		// Token: 0x060032FC RID: 13052
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLCloseCursor(OdbcStatementHandle StatementHandle);

		// Token: 0x060032FD RID: 13053
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLColAttributeW(OdbcStatementHandle StatementHandle, short ColumnNumber, short FieldIdentifier, CNativeBuffer CharacterAttribute, short BufferLength, out short StringLength, out IntPtr NumericAttribute);

		// Token: 0x060032FE RID: 13054
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLColumnsW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableName, short NameLen3, [MarshalAs(UnmanagedType.LPWStr)] [In] string ColumnName, short NameLen4);

		// Token: 0x060032FF RID: 13055
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLDisconnect(IntPtr ConnectionHandle);

		// Token: 0x06003300 RID: 13056
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLDriverConnectW(OdbcConnectionHandle hdbc, IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] [In] string connectionstring, short cbConnectionstring, IntPtr connectionstringout, short cbConnectionstringoutMax, out short cbConnectionstringout, short fDriverCompletion);

		// Token: 0x06003301 RID: 13057
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLEndTran(ODBC32.SQL_HANDLE HandleType, IntPtr Handle, short CompletionType);

		// Token: 0x06003302 RID: 13058
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLExecDirectW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string StatementText, int TextLength);

		// Token: 0x06003303 RID: 13059
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLExecute(OdbcStatementHandle StatementHandle);

		// Token: 0x06003304 RID: 13060
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLFetch(OdbcStatementHandle StatementHandle);

		// Token: 0x06003305 RID: 13061
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLFreeHandle(ODBC32.SQL_HANDLE HandleType, IntPtr StatementHandle);

		// Token: 0x06003306 RID: 13062
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLFreeStmt(OdbcStatementHandle StatementHandle, ODBC32.STMT Option);

		// Token: 0x06003307 RID: 13063
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetConnectAttrW(OdbcConnectionHandle ConnectionHandle, ODBC32.SQL_ATTR Attribute, byte[] Value, int BufferLength, out int StringLength);

		// Token: 0x06003308 RID: 13064
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetData(OdbcStatementHandle StatementHandle, ushort ColumnNumber, ODBC32.SQL_C TargetType, CNativeBuffer TargetValue, IntPtr BufferLength, out IntPtr StrLen_or_Ind);

		// Token: 0x06003309 RID: 13065
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetDescFieldW(OdbcDescriptorHandle StatementHandle, short RecNumber, ODBC32.SQL_DESC FieldIdentifier, CNativeBuffer ValuePointer, int BufferLength, out int StringLength);

		// Token: 0x0600330A RID: 13066
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLGetDiagRecW(ODBC32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, StringBuilder rchState, out int NativeError, StringBuilder MessageText, short BufferLength, out short TextLength);

		// Token: 0x0600330B RID: 13067
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLGetDiagFieldW(ODBC32.SQL_HANDLE HandleType, OdbcHandle Handle, short RecNumber, short DiagIdentifier, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder rchState, short BufferLength, out short StringLength);

		// Token: 0x0600330C RID: 13068
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetFunctions(OdbcConnectionHandle hdbc, ODBC32.SQL_API fFunction, out short pfExists);

		// Token: 0x0600330D RID: 13069
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetInfoW(OdbcConnectionHandle hdbc, ODBC32.SQL_INFO fInfoType, byte[] rgbInfoValue, short cbInfoValueMax, out short pcbInfoValue);

		// Token: 0x0600330E RID: 13070
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetInfoW(OdbcConnectionHandle hdbc, ODBC32.SQL_INFO fInfoType, byte[] rgbInfoValue, short cbInfoValueMax, IntPtr pcbInfoValue);

		// Token: 0x0600330F RID: 13071
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetStmtAttrW(OdbcStatementHandle StatementHandle, ODBC32.SQL_ATTR Attribute, out IntPtr Value, int BufferLength, out int StringLength);

		// Token: 0x06003310 RID: 13072
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLGetTypeInfo(OdbcStatementHandle StatementHandle, short fSqlType);

		// Token: 0x06003311 RID: 13073
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLMoreResults(OdbcStatementHandle StatementHandle);

		// Token: 0x06003312 RID: 13074
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLNumResultCols(OdbcStatementHandle StatementHandle, out short ColumnCount);

		// Token: 0x06003313 RID: 13075
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLPrepareW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string StatementText, int TextLength);

		// Token: 0x06003314 RID: 13076
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLPrimaryKeysW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableName, short NameLen3);

		// Token: 0x06003315 RID: 13077
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLProcedureColumnsW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string ProcName, short NameLen3, [MarshalAs(UnmanagedType.LPWStr)] [In] string ColumnName, short NameLen4);

		// Token: 0x06003316 RID: 13078
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLProceduresW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string ProcName, short NameLen3);

		// Token: 0x06003317 RID: 13079
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLRowCount(OdbcStatementHandle StatementHandle, out IntPtr RowCount);

		// Token: 0x06003318 RID: 13080
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, ODBC32.SQL_ATTR Attribute, IDtcTransaction Value, int StringLength);

		// Token: 0x06003319 RID: 13081
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, ODBC32.SQL_ATTR Attribute, string Value, int StringLength);

		// Token: 0x0600331A RID: 13082
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetConnectAttrW(OdbcConnectionHandle ConnectionHandle, ODBC32.SQL_ATTR Attribute, IntPtr Value, int StringLength);

		// Token: 0x0600331B RID: 13083
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetConnectAttrW(IntPtr ConnectionHandle, ODBC32.SQL_ATTR Attribute, IntPtr Value, int StringLength);

		// Token: 0x0600331C RID: 13084
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetDescFieldW(OdbcDescriptorHandle StatementHandle, short ColumnNumber, ODBC32.SQL_DESC FieldIdentifier, HandleRef CharacterAttribute, int BufferLength);

		// Token: 0x0600331D RID: 13085
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetDescFieldW(OdbcDescriptorHandle StatementHandle, short ColumnNumber, ODBC32.SQL_DESC FieldIdentifier, IntPtr CharacterAttribute, int BufferLength);

		// Token: 0x0600331E RID: 13086
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetEnvAttr(OdbcEnvironmentHandle EnvironmentHandle, ODBC32.SQL_ATTR Attribute, IntPtr Value, ODBC32.SQL_IS StringLength);

		// Token: 0x0600331F RID: 13087
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLSetStmtAttrW(OdbcStatementHandle StatementHandle, int Attribute, IntPtr Value, int StringLength);

		// Token: 0x06003320 RID: 13088
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLSpecialColumnsW(OdbcStatementHandle StatementHandle, ODBC32.SQL_SPECIALCOLS IdentifierType, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableName, short NameLen3, ODBC32.SQL_SCOPE Scope, ODBC32.SQL_NULLABILITY Nullable);

		// Token: 0x06003321 RID: 13089
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern ODBC32.RetCode SQLStatisticsW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableName, short NameLen3, short Unique, short Reserved);

		// Token: 0x06003322 RID: 13090
		[DllImport("odbc32.dll")]
		internal static extern ODBC32.RetCode SQLTablesW(OdbcStatementHandle StatementHandle, [MarshalAs(UnmanagedType.LPWStr)] [In] string CatalogName, short NameLen1, [MarshalAs(UnmanagedType.LPWStr)] [In] string SchemaName, short NameLen2, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableName, short NameLen3, [MarshalAs(UnmanagedType.LPWStr)] [In] string TableType, short NameLen4);

		// Token: 0x06003323 RID: 13091
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		internal static extern OleDbHResult GetErrorInfo([In] int dwReserved, [MarshalAs(UnmanagedType.Interface)] out UnsafeNativeMethods.IErrorInfo ppIErrorInfo);

		// Token: 0x06003324 RID: 13092
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode)]
		internal static extern uint GetEffectiveRightsFromAclW(byte[] pAcl, ref UnsafeNativeMethods.Trustee pTrustee, out uint pAccessMask);

		// Token: 0x06003325 RID: 13093
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool CheckTokenMembership(IntPtr tokenHandle, byte[] sidToCheck, out bool isMember);

		// Token: 0x06003326 RID: 13094
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool ConvertSidToStringSidW(IntPtr sid, out IntPtr stringSid);

		// Token: 0x06003327 RID: 13095
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int CreateWellKnownSid(int sidType, byte[] domainSid, [Out] byte[] resultSid, ref uint resultSidLength);

		// Token: 0x06003328 RID: 13096
		[DllImport("advapi32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetTokenInformation(IntPtr tokenHandle, uint token_class, IntPtr tokenStruct, uint tokenInformationLength, ref uint tokenString);

		// Token: 0x06003329 RID: 13097
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int lstrlenW(IntPtr ptr);

		// Token: 0x02000443 RID: 1091
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[SuppressUnmanagedCodeSecurity]
		[Guid("00000567-0000-0010-8000-00AA006D2EA4")]
		[ComImport]
		internal interface ADORecordConstruction
		{
			// Token: 0x0600365A RID: 13914
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_Row();
		}

		// Token: 0x02000444 RID: 1092
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[Guid("00000283-0000-0010-8000-00AA006D2EA4")]
		[ComImport]
		internal interface ADORecordsetConstruction
		{
			// Token: 0x0600365B RID: 13915
			[return: MarshalAs(UnmanagedType.Interface)]
			object get_Rowset();

			// Token: 0x0600365C RID: 13916
			[Obsolete("not used", true)]
			void put_Rowset();

			// Token: 0x0600365D RID: 13917
			IntPtr get_Chapter();
		}

		// Token: 0x02000445 RID: 1093
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[SuppressUnmanagedCodeSecurity]
		[Guid("0000050E-0000-0010-8000-00AA006D2EA4")]
		[ComImport]
		internal interface Recordset15
		{
			// Token: 0x0600365E RID: 13918
			[Obsolete("not used", true)]
			void get_Properties();

			// Token: 0x0600365F RID: 13919
			[Obsolete("not used", true)]
			void get_AbsolutePosition();

			// Token: 0x06003660 RID: 13920
			[Obsolete("not used", true)]
			void put_AbsolutePosition();

			// Token: 0x06003661 RID: 13921
			[Obsolete("not used", true)]
			void putref_ActiveConnection();

			// Token: 0x06003662 RID: 13922
			[Obsolete("not used", true)]
			void put_ActiveConnection();

			// Token: 0x06003663 RID: 13923
			object get_ActiveConnection();

			// Token: 0x06003664 RID: 13924
			[Obsolete("not used", true)]
			void get_BOF();

			// Token: 0x06003665 RID: 13925
			[Obsolete("not used", true)]
			void get_Bookmark();

			// Token: 0x06003666 RID: 13926
			[Obsolete("not used", true)]
			void put_Bookmark();

			// Token: 0x06003667 RID: 13927
			[Obsolete("not used", true)]
			void get_CacheSize();

			// Token: 0x06003668 RID: 13928
			[Obsolete("not used", true)]
			void put_CacheSize();

			// Token: 0x06003669 RID: 13929
			[Obsolete("not used", true)]
			void get_CursorType();

			// Token: 0x0600366A RID: 13930
			[Obsolete("not used", true)]
			void put_CursorType();

			// Token: 0x0600366B RID: 13931
			[Obsolete("not used", true)]
			void get_EOF();

			// Token: 0x0600366C RID: 13932
			[Obsolete("not used", true)]
			void get_Fields();

			// Token: 0x0600366D RID: 13933
			[Obsolete("not used", true)]
			void get_LockType();

			// Token: 0x0600366E RID: 13934
			[Obsolete("not used", true)]
			void put_LockType();

			// Token: 0x0600366F RID: 13935
			[Obsolete("not used", true)]
			void get_MaxRecords();

			// Token: 0x06003670 RID: 13936
			[Obsolete("not used", true)]
			void put_MaxRecords();

			// Token: 0x06003671 RID: 13937
			[Obsolete("not used", true)]
			void get_RecordCount();

			// Token: 0x06003672 RID: 13938
			[Obsolete("not used", true)]
			void putref_Source();

			// Token: 0x06003673 RID: 13939
			[Obsolete("not used", true)]
			void put_Source();

			// Token: 0x06003674 RID: 13940
			[Obsolete("not used", true)]
			void get_Source();

			// Token: 0x06003675 RID: 13941
			[Obsolete("not used", true)]
			void AddNew();

			// Token: 0x06003676 RID: 13942
			[Obsolete("not used", true)]
			void CancelUpdate();

			// Token: 0x06003677 RID: 13943
			[PreserveSig]
			OleDbHResult Close();

			// Token: 0x06003678 RID: 13944
			[Obsolete("not used", true)]
			void Delete();

			// Token: 0x06003679 RID: 13945
			[Obsolete("not used", true)]
			void GetRows();

			// Token: 0x0600367A RID: 13946
			[Obsolete("not used", true)]
			void Move();

			// Token: 0x0600367B RID: 13947
			[Obsolete("not used", true)]
			void MoveNext();

			// Token: 0x0600367C RID: 13948
			[Obsolete("not used", true)]
			void MovePrevious();

			// Token: 0x0600367D RID: 13949
			[Obsolete("not used", true)]
			void MoveFirst();

			// Token: 0x0600367E RID: 13950
			[Obsolete("not used", true)]
			void MoveLast();

			// Token: 0x0600367F RID: 13951
			[Obsolete("not used", true)]
			void Open();

			// Token: 0x06003680 RID: 13952
			[Obsolete("not used", true)]
			void Requery();

			// Token: 0x06003681 RID: 13953
			[Obsolete("not used", true)]
			void _xResync();

			// Token: 0x06003682 RID: 13954
			[Obsolete("not used", true)]
			void Update();

			// Token: 0x06003683 RID: 13955
			[Obsolete("not used", true)]
			void get_AbsolutePage();

			// Token: 0x06003684 RID: 13956
			[Obsolete("not used", true)]
			void put_AbsolutePage();

			// Token: 0x06003685 RID: 13957
			[Obsolete("not used", true)]
			void get_EditMode();

			// Token: 0x06003686 RID: 13958
			[Obsolete("not used", true)]
			void get_Filter();

			// Token: 0x06003687 RID: 13959
			[Obsolete("not used", true)]
			void put_Filter();

			// Token: 0x06003688 RID: 13960
			[Obsolete("not used", true)]
			void get_PageCount();

			// Token: 0x06003689 RID: 13961
			[Obsolete("not used", true)]
			void get_PageSize();

			// Token: 0x0600368A RID: 13962
			[Obsolete("not used", true)]
			void put_PageSize();

			// Token: 0x0600368B RID: 13963
			[Obsolete("not used", true)]
			void get_Sort();

			// Token: 0x0600368C RID: 13964
			[Obsolete("not used", true)]
			void put_Sort();

			// Token: 0x0600368D RID: 13965
			[Obsolete("not used", true)]
			void get_Status();

			// Token: 0x0600368E RID: 13966
			[Obsolete("not used", true)]
			void get_State();

			// Token: 0x0600368F RID: 13967
			[Obsolete("not used", true)]
			void _xClone();

			// Token: 0x06003690 RID: 13968
			[Obsolete("not used", true)]
			void UpdateBatch();

			// Token: 0x06003691 RID: 13969
			[Obsolete("not used", true)]
			void CancelBatch();

			// Token: 0x06003692 RID: 13970
			[Obsolete("not used", true)]
			void get_CursorLocation();

			// Token: 0x06003693 RID: 13971
			[Obsolete("not used", true)]
			void put_CursorLocation();

			// Token: 0x06003694 RID: 13972
			[PreserveSig]
			OleDbHResult NextRecordset(out object RecordsAffected, [MarshalAs(UnmanagedType.Interface)] out object ppiRs);
		}

		// Token: 0x02000446 RID: 1094
		[Guid("00000562-0000-0010-8000-00AA006D2EA4")]
		[InterfaceType(ComInterfaceType.InterfaceIsDual)]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface _ADORecord
		{
			// Token: 0x06003695 RID: 13973
			[Obsolete("not used", true)]
			void get_Properties();

			// Token: 0x06003696 RID: 13974
			object get_ActiveConnection();

			// Token: 0x06003697 RID: 13975
			[Obsolete("not used", true)]
			void put_ActiveConnection();

			// Token: 0x06003698 RID: 13976
			[Obsolete("not used", true)]
			void putref_ActiveConnection();

			// Token: 0x06003699 RID: 13977
			[Obsolete("not used", true)]
			void get_State();

			// Token: 0x0600369A RID: 13978
			[Obsolete("not used", true)]
			void get_Source();

			// Token: 0x0600369B RID: 13979
			[Obsolete("not used", true)]
			void put_Source();

			// Token: 0x0600369C RID: 13980
			[Obsolete("not used", true)]
			void putref_Source();

			// Token: 0x0600369D RID: 13981
			[Obsolete("not used", true)]
			void get_Mode();

			// Token: 0x0600369E RID: 13982
			[Obsolete("not used", true)]
			void put_Mode();

			// Token: 0x0600369F RID: 13983
			[Obsolete("not used", true)]
			void get_ParentURL();

			// Token: 0x060036A0 RID: 13984
			[Obsolete("not used", true)]
			void MoveRecord();

			// Token: 0x060036A1 RID: 13985
			[Obsolete("not used", true)]
			void CopyRecord();

			// Token: 0x060036A2 RID: 13986
			[Obsolete("not used", true)]
			void DeleteRecord();

			// Token: 0x060036A3 RID: 13987
			[Obsolete("not used", true)]
			void Open();

			// Token: 0x060036A4 RID: 13988
			[PreserveSig]
			OleDbHResult Close();
		}

		// Token: 0x02000447 RID: 1095
		[SuppressUnmanagedCodeSecurity]
		[Guid("0C733A8C-2A1C-11CE-ADE5-00AA0044773D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IAccessor
		{
			// Token: 0x060036A5 RID: 13989
			[Obsolete("not used", true)]
			void AddRefAccessor();

			// Token: 0x060036A6 RID: 13990
			[PreserveSig]
			OleDbHResult CreateAccessor([In] int dwAccessorFlags, [In] IntPtr cBindings, [In] SafeHandle rgBindings, [In] IntPtr cbRowSize, out IntPtr phAccessor, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4)] [In] [Out] int[] rgStatus);

			// Token: 0x060036A7 RID: 13991
			[Obsolete("not used", true)]
			void GetBindings();

			// Token: 0x060036A8 RID: 13992
			[PreserveSig]
			OleDbHResult ReleaseAccessor([In] IntPtr hAccessor, out int pcRefCount);
		}

		// Token: 0x02000448 RID: 1096
		[Guid("0C733A93-2A1C-11CE-ADE5-00AA0044773D")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IChapteredRowset
		{
			// Token: 0x060036A9 RID: 13993
			[Obsolete("not used", true)]
			void AddRefChapter();

			// Token: 0x060036AA RID: 13994
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[PreserveSig]
			OleDbHResult ReleaseChapter([In] IntPtr hChapter, out int pcRefCount);
		}

		// Token: 0x02000449 RID: 1097
		[Guid("0C733A11-2A1C-11CE-ADE5-00AA0044773D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface IColumnsInfo
		{
			// Token: 0x060036AB RID: 13995
			[PreserveSig]
			OleDbHResult GetColumnInfo(out IntPtr pcColumns, out IntPtr prgInfo, out IntPtr ppStringsBuffer);
		}

		// Token: 0x0200044A RID: 1098
		[SuppressUnmanagedCodeSecurity]
		[Guid("0C733A10-2A1C-11CE-ADE5-00AA0044773D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IColumnsRowset
		{
			// Token: 0x060036AC RID: 13996
			[PreserveSig]
			OleDbHResult GetAvailableColumns(out IntPtr pcOptColumns, out IntPtr prgOptColumns);

			// Token: 0x060036AD RID: 13997
			[PreserveSig]
			OleDbHResult GetColumnsRowset([In] IntPtr pUnkOuter, [In] IntPtr cOptColumns, [In] SafeHandle rgOptColumns, [In] ref Guid riid, [In] int cPropertySets, [In] IntPtr rgPropertySets, [MarshalAs(UnmanagedType.Interface)] out UnsafeNativeMethods.IRowset ppColRowset);
		}

		// Token: 0x0200044B RID: 1099
		[Guid("0C733A26-2A1C-11CE-ADE5-00AA0044773D")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface ICommandPrepare
		{
			// Token: 0x060036AE RID: 13998
			[PreserveSig]
			OleDbHResult Prepare([In] int cExpectedRuns);
		}

		// Token: 0x0200044C RID: 1100
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[SuppressUnmanagedCodeSecurity]
		[Guid("0C733A79-2A1C-11CE-ADE5-00AA0044773D")]
		[ComImport]
		internal interface ICommandProperties
		{
			// Token: 0x060036AF RID: 13999
			[PreserveSig]
			OleDbHResult GetProperties([In] int cPropertyIDSets, [In] SafeHandle rgPropertyIDSets, out int pcPropertySets, out IntPtr prgPropertySets);

			// Token: 0x060036B0 RID: 14000
			[PreserveSig]
			OleDbHResult SetProperties([In] int cPropertySets, [In] SafeHandle rgPropertySets);
		}

		// Token: 0x0200044D RID: 1101
		[Guid("0C733A27-2A1C-11CE-ADE5-00AA0044773D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface ICommandText
		{
			// Token: 0x060036B1 RID: 14001
			[PreserveSig]
			OleDbHResult Cancel();

			// Token: 0x060036B2 RID: 14002
			[PreserveSig]
			OleDbHResult Execute([In] IntPtr pUnkOuter, [In] ref Guid riid, [In] tagDBPARAMS pDBParams, out IntPtr pcRowsAffected, [MarshalAs(UnmanagedType.Interface)] out object ppRowset);

			// Token: 0x060036B3 RID: 14003
			[Obsolete("not used", true)]
			void GetDBSession();

			// Token: 0x060036B4 RID: 14004
			[Obsolete("not used", true)]
			void GetCommandText();

			// Token: 0x060036B5 RID: 14005
			[PreserveSig]
			OleDbHResult SetCommandText([In] ref Guid rguidDialect, [MarshalAs(UnmanagedType.LPWStr)] [In] string pwszCommand);
		}

		// Token: 0x0200044E RID: 1102
		[SuppressUnmanagedCodeSecurity]
		[Guid("0C733A64-2A1C-11CE-ADE5-00AA0044773D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface ICommandWithParameters
		{
			// Token: 0x060036B6 RID: 14006
			[Obsolete("not used", true)]
			void GetParameterInfo();

			// Token: 0x060036B7 RID: 14007
			[Obsolete("not used", true)]
			void MapParameterNames();

			// Token: 0x060036B8 RID: 14008
			[PreserveSig]
			OleDbHResult SetParameterInfo([In] IntPtr cParams, [MarshalAs(UnmanagedType.LPArray)] [In] IntPtr[] rgParamOrdinals, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] [In] tagDBPARAMBINDINFO[] rgParamBindInfo);
		}

		// Token: 0x0200044F RID: 1103
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("2206CCB1-19C1-11D1-89E0-00C04FD7A829")]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface IDataInitialize
		{
		}

		// Token: 0x02000450 RID: 1104
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0C733A89-2A1C-11CE-ADE5-00AA0044773D")]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface IDBInfo
		{
			// Token: 0x060036B9 RID: 14009
			[PreserveSig]
			OleDbHResult GetKeywords([MarshalAs(UnmanagedType.LPWStr)] out string ppwszKeywords);

			// Token: 0x060036BA RID: 14010
			[PreserveSig]
			OleDbHResult GetLiteralInfo([In] int cLiterals, [MarshalAs(UnmanagedType.LPArray)] [In] int[] rgLiterals, out int pcLiteralInfo, out IntPtr prgLiteralInfo, out IntPtr ppCharBuffer);
		}

		// Token: 0x02000451 RID: 1105
		[SuppressUnmanagedCodeSecurity]
		[Guid("0C733A8A-2A1C-11CE-ADE5-00AA0044773D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IDBProperties
		{
			// Token: 0x060036BB RID: 14011
			[PreserveSig]
			OleDbHResult GetProperties([In] int cPropertyIDSets, [In] SafeHandle rgPropertyIDSets, out int pcPropertySets, out IntPtr prgPropertySets);

			// Token: 0x060036BC RID: 14012
			[PreserveSig]
			OleDbHResult GetPropertyInfo([In] int cPropertyIDSets, [In] SafeHandle rgPropertyIDSets, out int pcPropertySets, out IntPtr prgPropertyInfoSets, out IntPtr ppDescBuffer);

			// Token: 0x060036BD RID: 14013
			[PreserveSig]
			OleDbHResult SetProperties([In] int cPropertySets, [In] SafeHandle rgPropertySets);
		}

		// Token: 0x02000452 RID: 1106
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[SuppressUnmanagedCodeSecurity]
		[Guid("0C733A7B-2A1C-11CE-ADE5-00AA0044773D")]
		[ComImport]
		internal interface IDBSchemaRowset
		{
			// Token: 0x060036BE RID: 14014
			[PreserveSig]
			OleDbHResult GetRowset([In] IntPtr pUnkOuter, [In] ref Guid rguidSchema, [In] int cRestrictions, [MarshalAs(UnmanagedType.LPArray)] [In] object[] rgRestrictions, [In] ref Guid riid, [In] int cPropertySets, [In] IntPtr rgPropertySets, [MarshalAs(UnmanagedType.Interface)] out UnsafeNativeMethods.IRowset ppRowset);

			// Token: 0x060036BF RID: 14015
			[PreserveSig]
			OleDbHResult GetSchemas(out int pcSchemas, out IntPtr rguidSchema, out IntPtr prgRestrictionSupport);
		}

		// Token: 0x02000453 RID: 1107
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("1CF2B120-547D-101B-8E65-08002B2BD119")]
		[ComImport]
		internal interface IErrorInfo
		{
			// Token: 0x060036C0 RID: 14016
			[Obsolete("not used", true)]
			void GetGUID();

			// Token: 0x060036C1 RID: 14017
			[PreserveSig]
			OleDbHResult GetSource([MarshalAs(UnmanagedType.BStr)] out string pBstrSource);

			// Token: 0x060036C2 RID: 14018
			[PreserveSig]
			OleDbHResult GetDescription([MarshalAs(UnmanagedType.BStr)] out string pBstrDescription);
		}

		// Token: 0x02000454 RID: 1108
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0C733A67-2A1C-11CE-ADE5-00AA0044773D")]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface IErrorRecords
		{
			// Token: 0x060036C3 RID: 14019
			[Obsolete("not used", true)]
			void AddErrorRecord();

			// Token: 0x060036C4 RID: 14020
			[Obsolete("not used", true)]
			void GetBasicErrorInfo();

			// Token: 0x060036C5 RID: 14021
			[PreserveSig]
			OleDbHResult GetCustomErrorObject([In] int ulRecordNum, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out UnsafeNativeMethods.ISQLErrorInfo ppObject);

			// Token: 0x060036C6 RID: 14022
			[return: MarshalAs(UnmanagedType.Interface)]
			UnsafeNativeMethods.IErrorInfo GetErrorInfo([In] int ulRecordNum, [In] int lcid);

			// Token: 0x060036C7 RID: 14023
			[Obsolete("not used", true)]
			void GetErrorParameters();

			// Token: 0x060036C8 RID: 14024
			int GetRecordCount();
		}

		// Token: 0x02000455 RID: 1109
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0C733A90-2A1C-11CE-ADE5-00AA0044773D")]
		[SuppressUnmanagedCodeSecurity]
		[ComImport]
		internal interface IMultipleResults
		{
			// Token: 0x060036C9 RID: 14025
			[PreserveSig]
			OleDbHResult GetResult([In] IntPtr pUnkOuter, [In] IntPtr lResultFlag, [In] ref Guid riid, out IntPtr pcRowsAffected, [MarshalAs(UnmanagedType.Interface)] out object ppRowset);
		}

		// Token: 0x02000456 RID: 1110
		[SuppressUnmanagedCodeSecurity]
		[Guid("0C733A69-2A1C-11CE-ADE5-00AA0044773D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IOpenRowset
		{
			// Token: 0x060036CA RID: 14026
			[PreserveSig]
			OleDbHResult OpenRowset([In] IntPtr pUnkOuter, [In] tagDBID pTableID, [In] IntPtr pIndexID, [In] ref Guid riid, [In] int cPropertySets, [In] IntPtr rgPropertySets, [MarshalAs(UnmanagedType.Interface)] out object ppRowset);
		}

		// Token: 0x02000457 RID: 1111
		[Guid("0C733AB4-2A1C-11CE-ADE5-00AA0044773D")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IRow
		{
			// Token: 0x060036CB RID: 14027
			[PreserveSig]
			OleDbHResult GetColumns([In] IntPtr cColumns, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct)] [In] [Out] tagDBCOLUMNACCESS[] rgColumns);
		}

		// Token: 0x02000458 RID: 1112
		[Guid("0C733A7C-2A1C-11CE-ADE5-00AA0044773D")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IRowset
		{
			// Token: 0x060036CC RID: 14028
			[Obsolete("not used", true)]
			void AddRefRows();

			// Token: 0x060036CD RID: 14029
			[PreserveSig]
			OleDbHResult GetData([In] IntPtr hRow, [In] IntPtr hAccessor, [In] IntPtr pData);

			// Token: 0x060036CE RID: 14030
			[PreserveSig]
			OleDbHResult GetNextRows([In] IntPtr hChapter, [In] IntPtr lRowsOffset, [In] IntPtr cRows, out IntPtr pcRowsObtained, [In] ref IntPtr pprghRows);

			// Token: 0x060036CF RID: 14031
			[PreserveSig]
			OleDbHResult ReleaseRows([In] IntPtr cRows, [In] SafeHandle rghRows, [In] IntPtr rgRowOptions, [In] IntPtr rgRefCounts, [In] IntPtr rgRowStatus);

			// Token: 0x060036D0 RID: 14032
			[Obsolete("not used", true)]
			void RestartPosition();
		}

		// Token: 0x02000459 RID: 1113
		[Guid("0C733A55-2A1C-11CE-ADE5-00AA0044773D")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface IRowsetInfo
		{
			// Token: 0x060036D1 RID: 14033
			[PreserveSig]
			OleDbHResult GetProperties([In] int cPropertyIDSets, [In] SafeHandle rgPropertyIDSets, out int pcPropertySets, out IntPtr prgPropertySets);

			// Token: 0x060036D2 RID: 14034
			[PreserveSig]
			OleDbHResult GetReferencedRowset([In] IntPtr iOrdinal, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out UnsafeNativeMethods.IRowset ppRowset);
		}

		// Token: 0x0200045A RID: 1114
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("0C733A74-2A1C-11CE-ADE5-00AA0044773D")]
		[ComImport]
		internal interface ISQLErrorInfo
		{
			// Token: 0x060036D3 RID: 14035
			[return: MarshalAs(UnmanagedType.I4)]
			int GetSQLInfo([MarshalAs(UnmanagedType.BStr)] out string pbstrSQLState);
		}

		// Token: 0x0200045B RID: 1115
		[Guid("0C733A5F-2A1C-11CE-ADE5-00AA0044773D")]
		[SuppressUnmanagedCodeSecurity]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		internal interface ITransactionLocal
		{
			// Token: 0x060036D4 RID: 14036
			[Obsolete("not used", true)]
			void Commit();

			// Token: 0x060036D5 RID: 14037
			[Obsolete("not used", true)]
			void Abort();

			// Token: 0x060036D6 RID: 14038
			[Obsolete("not used", true)]
			void GetTransactionInfo();

			// Token: 0x060036D7 RID: 14039
			[Obsolete("not used", true)]
			void GetOptionsObject();

			// Token: 0x060036D8 RID: 14040
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[PreserveSig]
			OleDbHResult StartTransaction([In] int isoLevel, [In] int isoFlags, [In] IntPtr pOtherOptions, out int pulTransactionLevel);
		}

		// Token: 0x0200045C RID: 1116
		// (Invoke) Token: 0x060036DA RID: 14042
		[SuppressUnmanagedCodeSecurity]
		internal delegate int IUnknownQueryInterface(IntPtr pThis, ref Guid riid, ref IntPtr ppInterface);

		// Token: 0x0200045D RID: 1117
		// (Invoke) Token: 0x060036DE RID: 14046
		[SuppressUnmanagedCodeSecurity]
		internal delegate OleDbHResult IDataInitializeGetDataSource(IntPtr pThis, IntPtr pUnkOuter, int dwClsCtx, [MarshalAs(UnmanagedType.LPWStr)] string pwszInitializationString, ref Guid riid, ref DataSourceWrapper ppDataSource);

		// Token: 0x0200045E RID: 1118
		// (Invoke) Token: 0x060036E2 RID: 14050
		[SuppressUnmanagedCodeSecurity]
		internal delegate OleDbHResult IDBInitializeInitialize(IntPtr pThis);

		// Token: 0x0200045F RID: 1119
		// (Invoke) Token: 0x060036E6 RID: 14054
		[SuppressUnmanagedCodeSecurity]
		internal delegate OleDbHResult IDBCreateSessionCreateSession(IntPtr pThis, IntPtr pUnkOuter, ref Guid riid, ref SessionWrapper ppDBSession);

		// Token: 0x02000460 RID: 1120
		// (Invoke) Token: 0x060036EA RID: 14058
		[SuppressUnmanagedCodeSecurity]
		internal delegate OleDbHResult IDBCreateCommandCreateCommand(IntPtr pThis, IntPtr pUnkOuter, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] ref object ppCommand);

		// Token: 0x02000461 RID: 1121
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct Trustee
		{
			// Token: 0x060036ED RID: 14061 RVA: 0x00149C1C File Offset: 0x0014901C
			internal Trustee(string name)
			{
				this._pMultipleTrustee = IntPtr.Zero;
				this._MultipleTrusteeOperation = 0;
				this._TrusteeForm = 1;
				this._TrusteeType = 1;
				this._name = name;
			}

			// Token: 0x0400237A RID: 9082
			internal IntPtr _pMultipleTrustee;

			// Token: 0x0400237B RID: 9083
			internal int _MultipleTrusteeOperation;

			// Token: 0x0400237C RID: 9084
			internal int _TrusteeForm;

			// Token: 0x0400237D RID: 9085
			internal int _TrusteeType;

			// Token: 0x0400237E RID: 9086
			[MarshalAs(UnmanagedType.LPTStr)]
			internal string _name;
		}
	}
}
