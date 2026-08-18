using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x020001D4 RID: 468
	internal sealed class CMDWrapper
	{
		// Token: 0x0600199E RID: 6558 RVA: 0x0025ADA8 File Offset: 0x0025A1A8
		internal CMDWrapper(OdbcConnection connection)
		{
			this._connection = connection;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x0600199F RID: 6559 RVA: 0x0025ADC8 File Offset: 0x0025A1C8
		// (set) Token: 0x060019A0 RID: 6560 RVA: 0x0025ADE8 File Offset: 0x0025A1E8
		internal bool Canceling
		{
			get
			{
				return this._canceling;
			}
			set
			{
				this._canceling = value;
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x060019A1 RID: 6561 RVA: 0x0025AE08 File Offset: 0x0025A208
		internal OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x17000345 RID: 837
		// (set) Token: 0x060019A2 RID: 6562 RVA: 0x0025AE28 File Offset: 0x0025A228
		internal bool HasBoundColumns
		{
			set
			{
				this._hasBoundColumns = value;
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x0025AE48 File Offset: 0x0025A248
		internal OdbcStatementHandle StatementHandle
		{
			get
			{
				return this._stmt;
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x0025AE68 File Offset: 0x0025A268
		internal OdbcStatementHandle KeyInfoStatement
		{
			get
			{
				return this._keyinfostmt;
			}
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0025AE88 File Offset: 0x0025A288
		internal void CreateKeyInfoStatementHandle()
		{
			this.DisposeKeyInfoStatementHandle();
			this._keyinfostmt = this._connection.CreateStatementHandle();
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0025AEB8 File Offset: 0x0025A2B8
		internal void CreateStatementHandle()
		{
			this.DisposeStatementHandle();
			this._stmt = this._connection.CreateStatementHandle();
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0025AEE8 File Offset: 0x0025A2E8
		internal void Dispose()
		{
			if (this._dataReaderBuf != null)
			{
				this._dataReaderBuf.Dispose();
				this._dataReaderBuf = null;
			}
			this.DisposeStatementHandle();
			CNativeBuffer nativeParameterBuffer = this._nativeParameterBuffer;
			this._nativeParameterBuffer = null;
			if (nativeParameterBuffer != null)
			{
				nativeParameterBuffer.Dispose();
			}
			this._ssKeyInfoModeOn = false;
			this._ssKeyInfoModeOff = false;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0025AF48 File Offset: 0x0025A348
		private void DisposeDescriptorHandle()
		{
			OdbcDescriptorHandle hdesc = this._hdesc;
			if (hdesc != null)
			{
				this._hdesc = null;
				hdesc.Dispose();
			}
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x0025AF78 File Offset: 0x0025A378
		internal void DisposeStatementHandle()
		{
			this.DisposeKeyInfoStatementHandle();
			this.DisposeDescriptorHandle();
			OdbcStatementHandle stmt = this._stmt;
			if (stmt != null)
			{
				this._stmt = null;
				stmt.Dispose();
			}
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0025AFA8 File Offset: 0x0025A3A8
		internal void DisposeKeyInfoStatementHandle()
		{
			OdbcStatementHandle keyinfostmt = this._keyinfostmt;
			if (keyinfostmt != null)
			{
				this._keyinfostmt = null;
				keyinfostmt.Dispose();
			}
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x0025AFD8 File Offset: 0x0025A3D8
		internal void FreeStatementHandle(ODBC32.STMT stmt)
		{
			this.DisposeDescriptorHandle();
			OdbcStatementHandle stmt2 = this._stmt;
			if (stmt2 != null)
			{
				try
				{
					ODBC32.RetCode retcode = stmt2.FreeStatement(stmt);
					this.StatementErrorHandler(retcode);
				}
				catch (Exception e)
				{
					if (ADP.IsCatchableExceptionType(e))
					{
						this._stmt = null;
						stmt2.Dispose();
					}
					throw;
				}
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0025B048 File Offset: 0x0025A448
		internal void FreeKeyInfoStatementHandle(ODBC32.STMT stmt)
		{
			OdbcStatementHandle keyinfostmt = this._keyinfostmt;
			if (keyinfostmt != null)
			{
				try
				{
					keyinfostmt.FreeStatement(stmt);
				}
				catch (Exception e)
				{
					if (ADP.IsCatchableExceptionType(e))
					{
						this._keyinfostmt = null;
						keyinfostmt.Dispose();
					}
					throw;
				}
			}
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0025B0A8 File Offset: 0x0025A4A8
		internal OdbcDescriptorHandle GetDescriptorHandle(ODBC32.SQL_ATTR attribute)
		{
			OdbcDescriptorHandle result = this._hdesc;
			if (this._hdesc == null)
			{
				result = (this._hdesc = new OdbcDescriptorHandle(this._stmt, attribute));
			}
			return result;
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0025B0E8 File Offset: 0x0025A4E8
		internal string GetDiagSqlState()
		{
			string result;
			this._stmt.GetDiagnosticField(out result);
			return result;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0025B108 File Offset: 0x0025A508
		internal void StatementErrorHandler(ODBC32.RetCode retcode)
		{
			switch (retcode)
			{
			case ODBC32.RetCode.SUCCESS:
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				this._connection.HandleErrorNoThrow(this._stmt, retcode);
				return;
			default:
				throw this._connection.HandleErrorNoThrow(this._stmt, retcode);
			}
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0025B158 File Offset: 0x0025A558
		internal void UnbindStmtColumns()
		{
			if (this._hasBoundColumns)
			{
				this.FreeStatementHandle(ODBC32.STMT.UNBIND);
				this._hasBoundColumns = false;
			}
		}

		// Token: 0x04000F88 RID: 3976
		private OdbcStatementHandle _stmt;

		// Token: 0x04000F89 RID: 3977
		private OdbcStatementHandle _keyinfostmt;

		// Token: 0x04000F8A RID: 3978
		internal OdbcDescriptorHandle _hdesc;

		// Token: 0x04000F8B RID: 3979
		internal CNativeBuffer _nativeParameterBuffer;

		// Token: 0x04000F8C RID: 3980
		internal CNativeBuffer _dataReaderBuf;

		// Token: 0x04000F8D RID: 3981
		private readonly OdbcConnection _connection;

		// Token: 0x04000F8E RID: 3982
		private bool _canceling;

		// Token: 0x04000F8F RID: 3983
		internal bool _hasBoundColumns;

		// Token: 0x04000F90 RID: 3984
		internal bool _ssKeyInfoModeOn;

		// Token: 0x04000F91 RID: 3985
		internal bool _ssKeyInfoModeOff;
	}
}
