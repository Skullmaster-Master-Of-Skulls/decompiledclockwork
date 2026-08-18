using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x020001EF RID: 495
	internal sealed class OdbcDescriptorHandle : OdbcHandle
	{
		// Token: 0x06001B8F RID: 7055 RVA: 0x00263C78 File Offset: 0x00263078
		internal OdbcDescriptorHandle(OdbcStatementHandle statementHandle, ODBC32.SQL_ATTR attribute) : base(statementHandle, attribute)
		{
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x00263C98 File Offset: 0x00263098
		internal ODBC32.RetCode GetDescriptionField(int i, ODBC32.SQL_DESC attribute, CNativeBuffer buffer, out int numericAttribute)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetDescFieldW(this, checked((short)i), attribute, buffer, (int)buffer.ShortLength, out numericAttribute);
			ODBC.TraceODBC(3, "SQLGetDescFieldW", retCode);
			return retCode;
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x00263CC8 File Offset: 0x002630C8
		internal ODBC32.RetCode SetDescriptionField1(short ordinal, ODBC32.SQL_DESC type, IntPtr value)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetDescFieldW(this, ordinal, type, value, 0);
			ODBC.TraceODBC(3, "SQLSetDescFieldW", retCode);
			return retCode;
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00263CF8 File Offset: 0x002630F8
		internal ODBC32.RetCode SetDescriptionField2(short ordinal, ODBC32.SQL_DESC type, HandleRef value)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetDescFieldW(this, ordinal, type, value, 0);
			ODBC.TraceODBC(3, "SQLSetDescFieldW", retCode);
			return retCode;
		}
	}
}
