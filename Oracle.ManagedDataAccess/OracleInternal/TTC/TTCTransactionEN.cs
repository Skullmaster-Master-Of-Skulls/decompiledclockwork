using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.MTS;
using OracleInternal.Network;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC
{
	// Token: 0x02000238 RID: 568
	internal class TTCTransactionEN : TTCFunction
	{
		// Token: 0x060014B0 RID: 5296 RVA: 0x000DE960 File Offset: 0x000DCB60
		internal TTCTransactionEN(MarshallingEngine mEngine) : base(mEngine, 104, 0)
		{
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000DE96C File Offset: 0x000DCB6C
		private TxnState DoTransaction(int txnOperation, OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint txnFlag, uint timeout, TxnState transactionInState)
		{
			base.WriteFunctionHeader();
			txnCtx = null;
			this.WriteTxnOperation(txnOperation, xid, txnCtx, txnFlag, timeout, transactionInState);
			TxnState result;
			this.ReadResponse(out result);
			return result;
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x000DE99C File Offset: 0x000DCB9C
		internal TxnState Prepare(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, TxnState transactionInState)
		{
			return this.DoTransaction(3, xid, txnCtx, 0U, 0U, transactionInState);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x000DE9AC File Offset: 0x000DCBAC
		internal TxnState Commit(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, TxnState transactionInState)
		{
			return this.DoTransaction(1, xid, txnCtx, 0U, timeout, transactionInState);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000DE9BC File Offset: 0x000DCBBC
		internal TxnState Abort(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, TxnState transactionInState)
		{
			return this.DoTransaction(2, xid, txnCtx, 0U, timeout, transactionInState);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x000DE9CC File Offset: 0x000DCBCC
		internal void WriteTxnOperation(int txnOpCode, OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint txnFlag, uint timeout, TxnState transactionInState)
		{
			this.m_marshallingEngine.MarshalSWORD(txnOpCode);
			if (txnCtx != null)
			{
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalUB4((long)txnCtx.Length);
			}
			else
			{
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalUB4(0L);
			}
			if (xid != null)
			{
				this.m_marshallingEngine.MarshalUB4((long)xid.m_formatID);
				this.m_marshallingEngine.MarshalUB4((long)xid.m_gtrid_length);
				this.m_marshallingEngine.MarshalUB4((long)xid.m_bqual_length);
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalUB4((long)xid.m_data.Length);
			}
			else
			{
				this.m_marshallingEngine.MarshalUB4(0L);
				this.m_marshallingEngine.MarshalUB4(0L);
				this.m_marshallingEngine.MarshalUB4(0L);
				this.m_marshallingEngine.MarshalNullPointer();
				this.m_marshallingEngine.MarshalUB4(0L);
			}
			this.m_marshallingEngine.MarshalUB4((long)((ulong)timeout));
			this.m_marshallingEngine.MarshalUB4((long)((ulong)transactionInState));
			this.m_marshallingEngine.MarshalPointer();
			if (this.m_marshallingEngine.NegotiatedTTCVersion >= 4)
			{
				this.m_marshallingEngine.MarshalUB4((long)((ulong)txnFlag));
			}
			if (txnCtx != null)
			{
				this.m_marshallingEngine.MarshalB1Array(txnCtx);
			}
			if (xid != null)
			{
				this.m_marshallingEngine.MarshalB1Array(xid.m_data);
			}
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x000DEB1C File Offset: 0x000DCD1C
		internal void ReadResponse(out TxnState txnState)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool flag = false;
			txnState = TxnState.Error;
			try
			{
				this.m_marshallingEngine.TTCErrorObject.Initialize();
				while (!flag)
				{
					try
					{
						byte b = this.m_marshallingEngine.UnmarshalSB1();
						byte b2 = b;
						if (b2 != 4)
						{
							switch (b2)
							{
							case 8:
								this.Process_RPA_Message(out txnState);
								break;
							case 9:
								if (this.m_marshallingEngine.HasEOCSCapability)
								{
									this.m_marshallingEngine.m_endOfCallStatus = this.m_marshallingEngine.UnmarshalUB4(false);
								}
								this.m_marshallingEngine.UnmarshalUB4(false);
								flag = true;
								break;
							default:
								if (b2 != 23)
								{
									throw new Exception("TTC error");
								}
								base.ProcessServerSidePiggybackFunction();
								break;
							}
						}
						else
						{
							this.m_marshallingEngine.TTCErrorObject.ReadErrorMessage();
							if (this.m_marshallingEngine.TTCErrorObject.ErrorCode == 1403)
							{
								this.m_marshallingEngine.TTCErrorObject.Initialize();
							}
							else
							{
								OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
							}
							flag = true;
						}
					}
					catch (NetworkException ex)
					{
						if (ex.ErrorCode != 3111)
						{
							throw;
						}
						this.m_marshallingEngine.ProcessReset();
						OracleConnectionImpl.CheckForAnyErrorFromDB(this.m_marshallingEngine.TTCErrorObject);
					}
					catch (Exception)
					{
						if (this.m_marshallingEngine.m_oraBufRdr != null)
						{
							this.m_marshallingEngine.m_oraBufRdr.ClearState();
						}
						this.m_marshallingEngine.m_oracleCommunication.Break();
						this.m_marshallingEngine.ProcessReset();
						throw;
					}
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex2, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x000DED34 File Offset: 0x000DCF34
		private void Process_RPA_Message(out TxnState txnState)
		{
			txnState = (TxnState)this.m_marshallingEngine.UnmarshalUB4(false);
		}

		// Token: 0x04001908 RID: 6408
		internal const int OTXCOMIT = 1;

		// Token: 0x04001909 RID: 6409
		internal const int OTXABORT = 2;

		// Token: 0x0400190A RID: 6410
		internal const int OTXPREPA = 3;

		// Token: 0x0400190B RID: 6411
		internal const int OTXFORGT = 4;

		// Token: 0x0400190C RID: 6412
		internal const int OTXRECOV = 5;

		// Token: 0x0400190D RID: 6413
		internal const int OTXMLPRE = 6;
	}
}
