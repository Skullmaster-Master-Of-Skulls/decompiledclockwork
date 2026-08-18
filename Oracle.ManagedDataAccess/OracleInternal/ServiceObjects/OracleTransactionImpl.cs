using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.TTC;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001BA RID: 442
	internal class OracleTransactionImpl
	{
		// Token: 0x06001127 RID: 4391 RVA: 0x000BD5F4 File Offset: 0x000BB7F4
		internal OracleTransactionImpl(OracleConnectionImpl connectionImpl, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			this.m_connectionImpl = connectionImpl;
			this.m_isolationLevel = isolationLevel;
			if (this.m_connectionImpl.m_currentIsolationLvl != this.m_isolationLevel)
			{
				this.m_connectionImpl.SwitchIsolationLevel(isolationLevel);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000BD664 File Offset: 0x000BB864
		internal void Commit(OracleConnection connection, ref OracleLogicalTransaction oracleLogicalTransaction)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
				this.m_connectionImpl.AddAllPiggyBackRequests();
				oracleLogicalTransaction = ((connection != null) ? connection.OracleLogicalTransaction : null);
				TTCSimpleOperations simpleOperationsObject = this.m_connectionImpl.SimpleOperationsObject;
				simpleOperationsObject.SetFunctionCode(14);
				simpleOperationsObject.WriteMessage();
				simpleOperationsObject.ReadResponse();
				TTCError ttcerrorObject = simpleOperationsObject.m_marshallingEngine.TTCErrorObject;
				if (ttcerrorObject.m_retCode != 0)
				{
					char[] chars = ttcerrorObject.m_marshallingEngine.m_charArrayPooler.Dequeue();
					string errMsg = ttcerrorObject.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(ttcerrorObject.m_errorMsg, 0, ttcerrorObject.m_errorMsg.Length, chars, true);
					ttcerrorObject.m_marshallingEngine.m_charArrayPooler.Enqueue(ref chars);
					throw new OracleException(ttcerrorObject.m_retCode, string.Empty, string.Empty, errMsg);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x000BD7A0 File Offset: 0x000BB9A0
		internal void Rollback(OracleConnection connection, ref OracleLogicalTransaction oracleLogicalTransaction)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262400, new string[0]);
			}
			try
			{
				this.m_connectionImpl.m_connectionFreeToUseEvent.WaitOne();
				this.m_connectionImpl.AddAllPiggyBackRequests();
				oracleLogicalTransaction = ((connection != null && !connection.bConnectionforTxnStatus && !OracleConnection.bIgnoreLogicalTransaction) ? connection.OracleLogicalTransaction : null);
				TTCSimpleOperations simpleOperationsObject = this.m_connectionImpl.SimpleOperationsObject;
				simpleOperationsObject.SetFunctionCode(15);
				simpleOperationsObject.WriteMessage();
				simpleOperationsObject.ReadResponse();
				TTCError ttcerrorObject = simpleOperationsObject.m_marshallingEngine.TTCErrorObject;
				if (ttcerrorObject.m_retCode != 0)
				{
					char[] chars = ttcerrorObject.m_marshallingEngine.m_charArrayPooler.Dequeue();
					string errMsg = ttcerrorObject.m_marshallingEngine.m_dbCharSetConv.ConvertBytesToString(ttcerrorObject.m_errorMsg, 0, ttcerrorObject.m_errorMsg.Length, chars, true);
					ttcerrorObject.m_marshallingEngine.m_charArrayPooler.Enqueue(ref chars);
					throw new OracleException(ttcerrorObject.m_retCode, string.Empty, string.Empty, errMsg);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268697600, ex, null);
				throw;
			}
			finally
			{
				this.m_connectionImpl.m_connectionFreeToUseEvent.Set();
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)262656, new string[0]);
				}
			}
		}

		// Token: 0x04001361 RID: 4961
		private OracleConnectionImpl m_connectionImpl;

		// Token: 0x04001362 RID: 4962
		private IsolationLevel m_isolationLevel;
	}
}
