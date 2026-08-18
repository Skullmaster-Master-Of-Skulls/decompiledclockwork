using System;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x020002A0 RID: 672
	internal sealed class OdbcDescriptorHandle : OdbcHandle
	{
		// Token: 0x060028F6 RID: 10486 RVA: 0x001110EC File Offset: 0x001104EC
		internal OdbcDescriptorHandle(OdbcStatementHandle statementHandle, ODBC32.SQL_ATTR attribute) : base(statementHandle, attribute)
		{
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x00111104 File Offset: 0x00110504
		internal ODBC32.RetCode GetDescriptionField(int i, ODBC32.SQL_DESC attribute, CNativeBuffer buffer, out int numericAttribute)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetDescFieldW(this, checked((short)i), attribute, buffer, (int)buffer.ShortLength, out numericAttribute);
			ODBC.TraceODBC(3, "SQLGetDescFieldW", retCode);
			return retCode;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x00111134 File Offset: 0x00110534
		internal ODBC32.RetCode SetDescriptionField1(short ordinal, ODBC32.SQL_DESC type, IntPtr value)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetDescFieldW(this, ordinal, type, value, 0);
			ODBC.TraceODBC(3, "SQLSetDescFieldW", retCode);
			return retCode;
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x0011115C File Offset: 0x0011055C
		internal ODBC32.RetCode SetDescriptionField2(short ordinal, ODBC32.SQL_DESC type, HandleRef value)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetDescFieldW(this, ordinal, type, value, 0);
			ODBC.TraceODBC(3, "SQLSetDescFieldW", retCode);
			return retCode;
		}
	}
}
