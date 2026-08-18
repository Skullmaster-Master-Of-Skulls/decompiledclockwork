using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Transactions;

namespace System.Data.Odbc
{
	// Token: 0x020001DA RID: 474
	internal sealed class OdbcConnectionHandle : OdbcHandle
	{
		// Token: 0x06001A5C RID: 6748 RVA: 0x0025DED8 File Offset: 0x0025D2D8
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

		// Token: 0x06001A5D RID: 6749 RVA: 0x0025DF48 File Offset: 0x0025D348
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
				switch (retCode)
				{
				case ODBC32.RetCode.SUCCESS:
				case ODBC32.RetCode.SUCCESS_WITH_INFO:
					this._handleState = OdbcConnectionHandle.HandleState.Transacted;
					break;
				}
			}
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x0025DFB8 File Offset: 0x0025D3B8
		internal ODBC32.RetCode BeginTransaction(ref IsolationLevel isolevel)
		{
			ODBC32.RetCode retCode = ODBC32.RetCode.SUCCESS;
			if (IsolationLevel.Unspecified != isolevel)
			{
				IsolationLevel isolationLevel = isolevel;
				ODBC32.SQL_TRANSACTION sql_TRANSACTION;
				if (isolationLevel <= IsolationLevel.ReadCommitted)
				{
					if (isolationLevel == IsolationLevel.Chaos)
					{
						throw ODBC.NotSupportedIsolationLevel(isolevel);
					}
					if (isolationLevel == IsolationLevel.ReadUncommitted)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.READ_UNCOMMITTED;
						goto IL_6B;
					}
					if (isolationLevel == IsolationLevel.ReadCommitted)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.READ_COMMITTED;
						goto IL_6B;
					}
				}
				else
				{
					if (isolationLevel == IsolationLevel.RepeatableRead)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.REPEATABLE_READ;
						goto IL_6B;
					}
					if (isolationLevel == IsolationLevel.Serializable)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.SERIALIZABLE;
						goto IL_6B;
					}
					if (isolationLevel == IsolationLevel.Snapshot)
					{
						sql_TRANSACTION = ODBC32.SQL_TRANSACTION.SNAPSHOT;
						goto IL_6B;
					}
				}
				throw ADP.InvalidIsolationLevel(isolevel);
				IL_6B:
				retCode = this.SetConnectionAttribute2(ODBC32.SQL_ATTR.TXN_ISOLATION, (IntPtr)((long)sql_TRANSACTION), -6);
				if (ODBC32.RetCode.SUCCESS_WITH_INFO == retCode)
				{
					isolevel = IsolationLevel.Unspecified;
				}
			}
			switch (retCode)
			{
			case ODBC32.RetCode.SUCCESS:
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				retCode = this.AutoCommitOff();
				this._handleState = OdbcConnectionHandle.HandleState.TransactionInProgress;
				break;
			}
			return retCode;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0025E078 File Offset: 0x0025D478
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

		// Token: 0x06001A60 RID: 6752 RVA: 0x0025E0D8 File Offset: 0x0025D4D8
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

		// Token: 0x06001A61 RID: 6753 RVA: 0x0025E168 File Offset: 0x0025D568
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
				switch (retCode)
				{
				case ODBC32.RetCode.SUCCESS:
				case ODBC32.RetCode.SUCCESS_WITH_INFO:
					this._handleState = OdbcConnectionHandle.HandleState.Connected;
					break;
				}
			}
			ODBC.TraceODBC(3, "SQLDriverConnectW", retCode);
			return retCode;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x0025E1D8 File Offset: 0x0025D5D8
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

		// Token: 0x06001A63 RID: 6755 RVA: 0x0025E238 File Offset: 0x0025D638
		internal ODBC32.RetCode GetConnectionAttribute(ODBC32.SQL_ATTR attribute, byte[] buffer, out int cbActual)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetConnectAttrW(this, attribute, buffer, buffer.Length, out cbActual);
			Bid.Trace("<odbc.SQLGetConnectAttr|ODBC> SQLRETURN=%d, Attribute=%d, BufferLength=%d, StringLength=%d\n", (int)retCode, (int)attribute, buffer.Length, cbActual);
			return retCode;
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x0025E268 File Offset: 0x0025D668
		internal ODBC32.RetCode GetFunctions(ODBC32.SQL_API fFunction, out short fExists)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetFunctions(this, fFunction, out fExists);
			ODBC.TraceODBC(3, "SQLGetFunctions", retCode);
			return retCode;
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x0025E298 File Offset: 0x0025D698
		internal ODBC32.RetCode GetInfo2(ODBC32.SQL_INFO info, byte[] buffer, out short cbActual)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), out cbActual);
			Bid.Trace("<odbc.SQLGetInfo|ODBC> SQLRETURN=%d, InfoType=%d, BufferLength=%d, StringLength=%d\n", (int)retCode, (int)info, buffer.Length, (int)cbActual);
			return retCode;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x0025E2C8 File Offset: 0x0025D6C8
		internal ODBC32.RetCode GetInfo1(ODBC32.SQL_INFO info, byte[] buffer)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLGetInfoW(this, info, buffer, checked((short)buffer.Length), ADP.PtrZero);
			Bid.Trace("<odbc.SQLGetInfo|ODBC> SQLRETURN=%d, InfoType=%d, BufferLength=%d\n", (int)retCode, (int)info, buffer.Length);
			return retCode;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0025E2F8 File Offset: 0x0025D6F8
		internal ODBC32.RetCode SetConnectionAttribute2(ODBC32.SQL_ATTR attribute, IntPtr value, int length)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetConnectAttrW(this, attribute, value, length);
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0025E328 File Offset: 0x0025D728
		internal ODBC32.RetCode SetConnectionAttribute3(ODBC32.SQL_ATTR attribute, string buffer, int length)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetConnectAttrW(this, attribute, buffer, length);
			Bid.Trace("<odbc.SQLSetConnectAttr|ODBC> SQLRETURN=%d, Attribute=%d, BufferLength=%d\n", (int)retCode, (int)attribute, buffer.Length);
			return retCode;
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x0025E358 File Offset: 0x0025D758
		internal ODBC32.RetCode SetConnectionAttribute4(ODBC32.SQL_ATTR attribute, IDtcTransaction transaction, int length)
		{
			ODBC32.RetCode retCode = UnsafeNativeMethods.SQLSetConnectAttrW(this, attribute, transaction, length);
			ODBC.TraceODBC(3, "SQLSetConnectAttrW", retCode);
			return retCode;
		}

		// Token: 0x04000FAD RID: 4013
		private OdbcConnectionHandle.HandleState _handleState;

		// Token: 0x020001DB RID: 475
		private enum HandleState
		{
			// Token: 0x04000FAF RID: 4015
			Allocated,
			// Token: 0x04000FB0 RID: 4016
			Connected,
			// Token: 0x04000FB1 RID: 4017
			Transacted,
			// Token: 0x04000FB2 RID: 4018
			TransactionInProgress
		}
	}
}
