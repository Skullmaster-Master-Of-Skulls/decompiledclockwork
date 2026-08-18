using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x0200029F RID: 671
	internal abstract class OdbcHandle : SafeHandle
	{
		// Token: 0x060028EF RID: 10479 RVA: 0x00110E38 File Offset: 0x00110238
		protected OdbcHandle(ODBC32.SQL_HANDLE handleType, OdbcHandle parentHandle) : base(IntPtr.Zero, true)
		{
			this._handleType = handleType;
			bool flag = false;
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				if (handleType != ODBC32.SQL_HANDLE.ENV)
				{
					if (handleType - ODBC32.SQL_HANDLE.DBC <= 1)
					{
						parentHandle.DangerousAddRef(ref flag);
						retCode = UnsafeNativeMethods.SQLAllocHandle(handleType, parentHandle, out this.handle);
					}
				}
				else
				{
					retCode = UnsafeNativeMethods.SQLAllocHandle(handleType, IntPtr.Zero, out this.handle);
				}
			}
			finally
			{
				if (flag && handleType - ODBC32.SQL_HANDLE.DBC <= 1)
				{
					if (IntPtr.Zero != this.handle)
					{
						this._parentHandle = parentHandle;
					}
					else
					{
						parentHandle.DangerousRelease();
					}
				}
			}
			Bid.TraceSqlReturn("<odbc.SQLAllocHandle|API|ODBC|RET> %08X{SQLRETURN}\n", retCode);
			if (ADP.PtrZero == this.handle || retCode != ODBC32.RetCode.SUCCESS)
			{
				throw ODBC.CantAllocateEnvironmentHandle(retCode);
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x00110F08 File Offset: 0x00110308
		internal OdbcHandle(OdbcStatementHandle parentHandle, ODBC32.SQL_ATTR attribute) : base(IntPtr.Zero, true)
		{
			this._handleType = ODBC32.SQL_HANDLE.DESC;
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode statementAttribute;
			try
			{
				parentHandle.DangerousAddRef(ref flag);
				int num;
				statementAttribute = parentHandle.GetStatementAttribute(attribute, out this.handle, out num);
			}
			finally
			{
				if (flag)
				{
					if (IntPtr.Zero != this.handle)
					{
						this._parentHandle = parentHandle;
					}
					else
					{
						parentHandle.DangerousRelease();
					}
				}
			}
			if (ADP.PtrZero == this.handle)
			{
				throw ODBC.FailedToGetDescriptorHandle(statementAttribute);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x00110FA4 File Offset: 0x001103A4
		internal ODBC32.SQL_HANDLE HandleType
		{
			get
			{
				return this._handleType;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060028F2 RID: 10482 RVA: 0x00110FB8 File Offset: 0x001103B8
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x00110FD8 File Offset: 0x001103D8
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			if (IntPtr.Zero != handle)
			{
				ODBC32.SQL_HANDLE handleType = this.HandleType;
				if (handleType - ODBC32.SQL_HANDLE.ENV > 2)
				{
					if (handleType != ODBC32.SQL_HANDLE.DESC)
					{
					}
				}
				else
				{
					ODBC32.RetCode a = UnsafeNativeMethods.SQLFreeHandle(handleType, handle);
					Bid.TraceSqlReturn("<odbc.SQLFreeHandle|API|ODBC|RET> %08X{SQLRETURN}\n", a);
				}
			}
			OdbcHandle parentHandle = this._parentHandle;
			this._parentHandle = null;
			if (parentHandle != null)
			{
				parentHandle.DangerousRelease();
			}
			return true;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x00111044 File Offset: 0x00110444
		internal ODBC32.RetCode GetDiagnosticField(out string sqlState)
		{
			StringBuilder stringBuilder = new StringBuilder(6);
			short num;
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetDiagFieldW(this.HandleType, this, 1, 4, stringBuilder, checked((short)(2 * stringBuilder.Capacity)), out num);
			ODBC.TraceODBC(3, "SQLGetDiagFieldW", retCode);
			if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				sqlState = stringBuilder.ToString();
			}
			else
			{
				sqlState = ADP.StrEmpty;
			}
			return retCode;
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x00111098 File Offset: 0x00110498
		internal ODBC32.RetCode GetDiagnosticRecord(short record, out string sqlState, StringBuilder message, out int nativeError, out short cchActual)
		{
			StringBuilder stringBuilder = new StringBuilder(5);
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetDiagRecW(this.HandleType, this, record, stringBuilder, out nativeError, message, checked((short)message.Capacity), out cchActual);
			ODBC.TraceODBC(3, "SQLGetDiagRecW", retCode);
			if (retCode == ODBC32.RetCode.SUCCESS || retCode == ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				sqlState = stringBuilder.ToString();
			}
			else
			{
				sqlState = ADP.StrEmpty;
			}
			return retCode;
		}

		// Token: 0x04001AAB RID: 6827
		private ODBC32.SQL_HANDLE _handleType;

		// Token: 0x04001AAC RID: 6828
		private OdbcHandle _parentHandle;
	}
}
