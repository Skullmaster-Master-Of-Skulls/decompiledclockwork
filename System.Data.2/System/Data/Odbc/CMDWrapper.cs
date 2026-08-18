using System;
using System.Data.Common;

namespace System.Data.Odbc
{
	// Token: 0x0200028E RID: 654
	internal sealed class CMDWrapper
	{
		// Token: 0x0600276D RID: 10093 RVA: 0x0010A644 File Offset: 0x00109A44
		internal CMDWrapper(OdbcConnection connection)
		{
			this._connection = connection;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x0010A660 File Offset: 0x00109A60
		// (set) Token: 0x0600276F RID: 10095 RVA: 0x0010A674 File Offset: 0x00109A74
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

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x0010A688 File Offset: 0x00109A88
		internal OdbcConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (set) Token: 0x06002771 RID: 10097 RVA: 0x0010A69C File Offset: 0x00109A9C
		internal bool HasBoundColumns
		{
			set
			{
				this._hasBoundColumns = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06002772 RID: 10098 RVA: 0x0010A6B0 File Offset: 0x00109AB0
		internal OdbcStatementHandle StatementHandle
		{
			get
			{
				return this._stmt;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x0010A6C4 File Offset: 0x00109AC4
		internal OdbcStatementHandle KeyInfoStatement
		{
			get
			{
				return this._keyinfostmt;
			}
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x0010A6D8 File Offset: 0x00109AD8
		internal void CreateKeyInfoStatementHandle()
		{
			this.DisposeKeyInfoStatementHandle();
			this._keyinfostmt = this._connection.CreateStatementHandle();
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0010A6FC File Offset: 0x00109AFC
		internal void CreateStatementHandle()
		{
			this.DisposeStatementHandle();
			this._stmt = this._connection.CreateStatementHandle();
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x0010A720 File Offset: 0x00109B20
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

		// Token: 0x06002777 RID: 10103 RVA: 0x0010A774 File Offset: 0x00109B74
		private void DisposeDescriptorHandle()
		{
			OdbcDescriptorHandle hdesc = this._hdesc;
			if (hdesc != null)
			{
				this._hdesc = null;
				hdesc.Dispose();
			}
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0010A798 File Offset: 0x00109B98
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

		// Token: 0x06002779 RID: 10105 RVA: 0x0010A7C8 File Offset: 0x00109BC8
		internal void DisposeKeyInfoStatementHandle()
		{
			OdbcStatementHandle keyinfostmt = this._keyinfostmt;
			if (keyinfostmt != null)
			{
				this._keyinfostmt = null;
				keyinfostmt.Dispose();
			}
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0010A7EC File Offset: 0x00109BEC
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

		// Token: 0x0600277B RID: 10107 RVA: 0x0010A850 File Offset: 0x00109C50
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

		// Token: 0x0600277C RID: 10108 RVA: 0x0010A8A8 File Offset: 0x00109CA8
		internal OdbcDescriptorHandle GetDescriptorHandle(ODBC32.SQL_ATTR attribute)
		{
			OdbcDescriptorHandle result = this._hdesc;
			if (this._hdesc == null)
			{
				result = (this._hdesc = new OdbcDescriptorHandle(this._stmt, attribute));
			}
			return result;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x0010A8DC File Offset: 0x00109CDC
		internal string GetDiagSqlState()
		{
			string result;
			this._stmt.GetDiagnosticField(out result);
			return result;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0010A8F8 File Offset: 0x00109CF8
		internal void StatementErrorHandler(ODBC32.RetCode retcode)
		{
			if (retcode <= ODBC32.RetCode.SUCCESS_WITH_INFO)
			{
				this._connection.HandleErrorNoThrow(this._stmt, retcode);
				return;
			}
			throw this._connection.HandleErrorNoThrow(this._stmt, retcode);
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x0010A930 File Offset: 0x00109D30
		internal void UnbindStmtColumns()
		{
			if (this._hasBoundColumns)
			{
				this.FreeStatementHandle(ODBC32.STMT.UNBIND);
				this._hasBoundColumns = false;
			}
		}

		// Token: 0x04001A53 RID: 6739
		private OdbcStatementHandle _stmt;

		// Token: 0x04001A54 RID: 6740
		private OdbcStatementHandle _keyinfostmt;

		// Token: 0x04001A55 RID: 6741
		internal OdbcDescriptorHandle _hdesc;

		// Token: 0x04001A56 RID: 6742
		internal CNativeBuffer _nativeParameterBuffer;

		// Token: 0x04001A57 RID: 6743
		internal CNativeBuffer _dataReaderBuf;

		// Token: 0x04001A58 RID: 6744
		private readonly OdbcConnection _connection;

		// Token: 0x04001A59 RID: 6745
		private bool _canceling;

		// Token: 0x04001A5A RID: 6746
		internal bool _hasBoundColumns;

		// Token: 0x04001A5B RID: 6747
		internal bool _ssKeyInfoModeOn;

		// Token: 0x04001A5C RID: 6748
		internal bool _ssKeyInfoModeOff;
	}
}
