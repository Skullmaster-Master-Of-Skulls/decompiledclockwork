using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x02000292 RID: 658
	internal sealed class OdbcConnectionHandle : OdbcHandle
	{
		// Token: 0x06002804 RID: 10244 RVA: 0x0010C694 File Offset: 0x0010BA94
		internal OdbcConnectionHandle(OdbcConnection connection, OdbcConnectionString constr, OdbcEnvironmentHandle environmentHandle) : base(ODBC32.SQL_HANDLE.DBC, environmentHandle)
		{
			if (connection == null)
			{
				throw ADP.ArgumentNull("connection");
			}
			if (constr == null)
			{
				throw ADP.ArgumentNull("constr");
			}
			int connectionTimeout = connection.ConnectionTimeout;
			ODBC32.RetCode retcode = this.SetConnectionAttribute2(ODBC32.SQL_ATTR.LOGIN_TIMEOUT, (IntPtr)connectionTimeout, -5);
			string connectionString = constr.UsersConnectionString(false);
			retcode = this.Connect(connectionString);
			connection.HandleError(this, retcode);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0010C6F8 File Offset: 0x0010BAF8
		private ODBC32.RetCode AutoCommitOff()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode retCode;
			try
			{
			}
			finally
			{
				retCode = UnsafeNativeMethods.SQLSetConnectAttrW(this, ODBC32.SQL_ATTR.AUTOCOMMIT, ODBC32.SQL_AUTOCOMMIT_OFF, -5);
				if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this._handleState = OdbcConnectionHandle.HandleState.Transacted;
				}
			}
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x0010C754 File Offset: 0x0010BB54
		internal ODBC32.RetCode BeginTransaction(ref IsolationLevel isolevel)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			if (IsolationLevel.Unspecified != isolevel)
			{
				IsolationLevel isolationLevel = isolevel;
				ODBC32.SQL_TRANSACTION value;
				ODBC32.SQL_ATTR attribute;
				if (isolationLevel <= IsolationLevel.ReadCommitted)
				{
					if (isolationLevel == IsolationLevel.Chaos)
					{
						throw ODBC.NotSupportedIsolationLevel(isolevel);
					}
					if (isolationLevel == IsolationLevel.ReadUncommitted)
					{
						value = ODBC32.SQL_TRANSACTION.READ_UNCOMMITTED;
						attribute = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_7D;
					}
					if (isolationLevel == IsolationLevel.ReadCommitted)
					{
						value = ODBC32.SQL_TRANSACTION.READ_COMMITTED;
						attribute = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_7D;
					}
				}
				else
				{
					if (isolationLevel == IsolationLevel.RepeatableRead)
					{
						value = ODBC32.SQL_TRANSACTION.REPEATABLE_READ;
						attribute = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_7D;
					}
					if (isolationLevel == IsolationLevel.Serializable)
					{
						value = ODBC32.SQL_TRANSACTION.SERIALIZABLE;
						attribute = ODBC32.SQL_ATTR.TXN_ISOLATION;
						goto IL_7D;
					}
					if (isolationLevel == IsolationLevel.Snapshot)
					{
						value = ODBC32.SQL_TRANSACTION.SNAPSHOT;
						attribute = ODBC32.SQL_ATTR.SQL_COPT_SS_TXN_ISOLATION;
						goto IL_7D;
					}
				}
				throw ADP.InvalidIsolationLevel(isolevel);
				IL_7D:
				retCode = this.SetConnectionAttribute2(attribute, (IntPtr)((int)value), -6);
				if (ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
				{
					isolevel = IsolationLevel.Unspecified;
				}
			}
			if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				retCode = this.AutoCommitOff();
				this._handleState = OdbcConnectionHandle.HandleState.TransactionInProgress;
			}
			return retCode;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x0010C808 File Offset: 0x0010BC08
		internal ODBC32.RetCode CompleteTransaction(short transactionOperation)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode result;
			try
			{
				base.DangerousAddRef(ref flag);
				ODBC32.RetCode retCode = this.CompleteTransaction(transactionOperation, this.handle);
				result = retCode;
			}
			finally
			{
				if (flag)
				{
					base.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x0010C860 File Offset: 0x0010BC60
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private ODBC32.RetCode CompleteTransaction(short transactionOperation, IntPtr handle)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (OdbcConnectionHandle.HandleState.TransactionInProgress == this._handleState)
				{
					retCode = UnsafeNativeMethods.SQLEndTran(base.HandleType, handle, transactionOperation);
					if (retCode == ODBC32.RetCode.SUCCESS || ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
					{
						this._handleState = OdbcConnectionHandle.HandleState.Transacted;
					}
					Bid.TraceSqlReturn("<odbc.SQLEndTran|API|ODBC|RET> %08X{SQLRETURN}\n", retCode);
				}
				if (OdbcConnectionHandle.HandleState.Transacted == this._handleState)
				{
					retCode = UnsafeNativeMethods.SQLSetConnectAttrW(handle, ODBC32.SQL_ATTR.AUTOCOMMIT, ODBC32.SQL_AUTOCOMMIT_ON, -5);
					this._handleState = OdbcConnectionHandle.HandleState.Connected;
					Bid.TraceSqlReturn("<odbc.SQLSetConnectAttr|API|ODBC|RET> %08X{SQLRETURN}\n", retCode);
				}
			}
			return retCode;
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x0010C8F0 File Offset: 0x0010BCF0
		private ODBC32.RetCode Connect(string connectionString)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			ODBC32.RetCode retCode;
			try
			{
			}
			finally
			{
				short num;
				retCode = UnsafeNativeMethods.SQLDriverConnectW(this, ADP.PtrZero, connectionString, -3, ADP.PtrZero, 0, out num, 0);
				if (retCode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
				{
					this._handleState = OdbcConnectionHandle.HandleState.Connected;
				}
			}
			ODBC.TraceODBC(3, "SQLDriverConnectW", retCode);
			return retCode;
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x0010C954 File Offset: 0x0010BD54
		protected override bool ReleaseHandle()
		{
			ODBC32.RetCode a = this.CompleteTransaction(1, this.handle);
			if (OdbcConnectionHandle.HandleState.Connected == this._handleState || OdbcConnectionHandle.HandleState.TransactionInProgress == this._handleState)
			{
				a = UnsafeNativeMethods.SQLDisconnect(this.handle);
				this._handleState = OdbcConnectionHandle.HandleState.Allocated;
				Bid.TraceSqlReturn("<odbc.SQLDisconnect|API|ODBC|RET> %08X{SQLRETURN}\n", a);
			}
			return base.ReleaseHandle();
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x0010C9A8 File Offset: 0x0010BDA8
		internal ODBC32.RetCode GetConnectionAttribute(ODBC32.SQL_ATTR attribute, byte[] buffer, out int cbActual)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetConnectAttrW(this, attribute, buffer, buffer.Length, out cbActual);
			Bid.Trace("<odbc.SQLGetConnectAttr|ODBC> SQLRETURN=%d, Attribute=%d, BufferLength=%d, StringLength=%d\n", (int)retCode, (int)attribute, buffer.Length, cbActual);
			return retCode;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x0010C9D4 File Offset: 0x0010BDD4
		internal ODBC32.RetCode GetFunctions(ODBC32.SQL_API fFunction, out short fExists)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetFunctions(this, fFunction, out fExists);
			ODBC.TraceODBC(3, "SQLGetFunctions", retCode);
			return retCode;
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x0010C9F8 File Offset: 0x0010BDF8
		internal ODBC32.RetCode GetInfo2(ODBC32.SQL_INFO info, byte[] buffer, out short cbActual)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), out cbActual);
			Bid.Trace("<odbc.SQLGetInfo|ODBC> SQLRETURN=%d, InfoType=%d, BufferLength=%d, StringLength=%d\n", (int)retCode, (int)info, buffer.Length, (int)cbActual);
			return retCode;
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0010CA28 File Offset: 0x0010BE28
		internal ODBC32.RetCode GetInfo1(ODBC32.SQL_INFO info, byte[] buffer)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), ADP.PtrZero);
			Bid.Trace("<odbc.SQLGetInfo|ODBC> SQLRETURN=%d, InfoType=%d, BufferLength=%d\n", (int)retCode, (int)info, buffer.Length);
			return retCode;
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0010CA58 File Offset: 0x0010BE58
		internal ODBC32.RetCode SetConnectionAttribute2(ODBC32.SQL_ATTR attribute, IntPtr value, int length)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetConnectAttrW(this, attribute, value, length);
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x0010CA7C File Offset: 0x0010BE7C
		internal ODBC32.RetCode SetConnectionAttribute3(ODBC32.SQL_ATTR attribute, string buffer, int length)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetConnectAttrW(this, attribute, buffer, length);
			Bid.Trace("<odbc.SQLSetConnectAttr|ODBC> SQLRETURN=%d, Attribute=%d, BufferLength=%d\n", (int)retCode, (int)attribute, buffer.Length);
			return retCode;
		}

		// Token: 0x06002811 RID: 10257 RVA: 0x0010CAA8 File Offset: 0x0010BEA8
		internal ODBC32.RetCode SetConnectionAttribute4(ODBC32.SQL_ATTR attribute, IDtcTransaction transaction, int length)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetConnectAttrW(this, attribute, transaction, length);
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x04001A6D RID: 6765
		private OdbcConnectionHandle.HandleState _handleState;

		// Token: 0x0200041D RID: 1053
		private enum HandleState
		{
			// Token: 0x040022B8 RID: 8888
			Allocated,
			// Token: 0x040022B9 RID: 8889
			Connected,
			// Token: 0x040022BA RID: 8890
			Transacted,
			// Token: 0x040022BB RID: 8891
			TransactionInProgress
		}
	}
}
