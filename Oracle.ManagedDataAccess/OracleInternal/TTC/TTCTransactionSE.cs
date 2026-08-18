using System;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.MTS;
using OracleInternal.Network;
using OracleInternal.ServiceObjects;

namespace OracleInternal.TTC
{
	// Token: 0x02000239 RID: 569
	internal class TTCTransactionSE : TTCFunction
	{
		// Token: 0x060014B8 RID: 5304 RVA: 0x000DED48 File Offset: 0x000DCF48
		internal TTCTransactionSE(MarshallingEngine mEngine) : base(mEngine, 103, 0)
		{
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x000DED54 File Offset: 0x000DCF54
		private byte[] DoTransaction(int txnOperation, OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint txnFlag, uint timeout, ref long applicationValue, string connectionInternalName, string connectionExternalName)
		{
			base.WriteFunctionHeader();
			this.WriteTxnOperation(txnOperation, xid, txnCtx, txnFlag, timeout, applicationValue, connectionInternalName, connectionExternalName);
			byte[] result;
			long num;
			this.ReadResponse(out result, out num);
			applicationValue = num;
			return result;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x000DED8C File Offset: 0x000DCF8C
		internal byte[] Start(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, ref long applicationValue)
		{
			return this.Start(xid, txnCtx, timeout, ref applicationValue, null, null);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x000DED9C File Offset: 0x000DCF9C
		internal byte[] Start(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, ref long applicationValue, string connectionInternalName, string connectionExternalName)
		{
			return this.DoTransaction(1, xid, txnCtx, 1U, timeout, ref applicationValue, connectionInternalName, connectionExternalName);
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x000DEDBC File Offset: 0x000DCFBC
		internal byte[] Resume(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, ref long applicationValue)
		{
			return this.Resume(xid, txnCtx, timeout, ref applicationValue, null, null);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x000DEDCC File Offset: 0x000DCFCC
		internal byte[] Resume(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, ref long applicationValue, string connectionInternalName, string connectionExternalName)
		{
			return this.DoTransaction(1, xid, txnCtx, 4U, timeout, ref applicationValue, connectionInternalName, connectionExternalName);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000DEDEC File Offset: 0x000DCFEC
		internal byte[] Promote(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, ref long applicationValue)
		{
			return this.Promote(xid, txnCtx, timeout, ref applicationValue, null, null);
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x000DEDFC File Offset: 0x000DCFFC
		internal byte[] Promote(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, ref long applicationValue, string connectionInternalName, string connectionExternalName)
		{
			return this.DoTransaction(1, xid, txnCtx, 8U, timeout, ref applicationValue, connectionInternalName, connectionExternalName);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x000DEE1C File Offset: 0x000DD01C
		internal byte[] Detach(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, ref long applicationValue)
		{
			return this.Detach(xid, txnCtx, 0U, ref applicationValue, null, null);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x000DEE2C File Offset: 0x000DD02C
		internal byte[] Detach(OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint timeout, ref long applicationValue, string connectionInternalName, string connectionExternalName)
		{
			return this.DoTransaction(2, xid, txnCtx, 0U, timeout, ref applicationValue, connectionInternalName, connectionExternalName);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x000DEE4C File Offset: 0x000DD04C
		internal void WriteTxnOperation(int txnOpCode, OpoDTCTxnXIDRefCtx xid, byte[] txnCtx, uint txnFlag, uint timeout, long applicationValue, string connectionInternalName, string connectionExternalName)
		{
			byte[] array = null;
			byte[] array2 = null;
			bool flag = true;
			this.m_marshallingEngine.MarshalSWORD(txnOpCode);
			if (txnCtx == null || (txnOpCode == 1 && txnFlag == 1U))
			{
				flag = false;
			}
			if (flag)
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
			this.m_marshallingEngine.MarshalUB4((long)((ulong)txnFlag));
			this.m_marshallingEngine.MarshalUB4((long)((ulong)timeout));
			this.m_marshallingEngine.MarshalPointer();
			this.m_marshallingEngine.MarshalPointer();
			this.m_marshallingEngine.MarshalPointer();
			if (this.m_marshallingEngine.NegotiatedTTCVersion >= 5)
			{
				if (connectionInternalName != null || connectionExternalName != null)
				{
					UTF8Encoding utf8Encoding = new UTF8Encoding();
					if (connectionInternalName != null)
					{
						array = utf8Encoding.GetBytes(connectionInternalName);
						this.m_marshallingEngine.MarshalPointer();
						this.m_marshallingEngine.MarshalUB4((long)array.Length);
					}
					else
					{
						this.m_marshallingEngine.MarshalNullPointer();
						this.m_marshallingEngine.MarshalUB4(0L);
					}
					if (connectionExternalName != null)
					{
						array2 = utf8Encoding.GetBytes(connectionExternalName);
						this.m_marshallingEngine.MarshalPointer();
						this.m_marshallingEngine.MarshalUB4((long)array2.Length);
					}
					else
					{
						this.m_marshallingEngine.MarshalNullPointer();
						this.m_marshallingEngine.MarshalUB4(0L);
					}
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalUB4(0L);
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalUB4(0L);
				}
			}
			if (flag)
			{
				this.m_marshallingEngine.MarshalB1Array(txnCtx);
			}
			if (xid != null)
			{
				this.m_marshallingEngine.MarshalB1Array(xid.m_data);
			}
			this.m_marshallingEngine.MarshalUB4(applicationValue);
			if (this.m_marshallingEngine.NegotiatedTTCVersion >= 5)
			{
				if (array != null)
				{
					this.m_marshallingEngine.MarshalCHR(array);
				}
				if (array2 != null)
				{
					this.m_marshallingEngine.MarshalCHR(array2);
				}
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000DF0B8 File Offset: 0x000DD2B8
		internal void ReadResponse(out byte[] txnCtx, out long applicationValue)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool flag = false;
			txnCtx = null;
			applicationValue = 0L;
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
								this.Process_RPA_Message(out txnCtx, out applicationValue);
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

		// Token: 0x060014C4 RID: 5316 RVA: 0x000DF2D4 File Offset: 0x000DD4D4
		private void Process_RPA_Message(out byte[] txnCtx, out long applicationValue)
		{
			applicationValue = this.m_marshallingEngine.UnmarshalUB4(false);
			int num = this.m_marshallingEngine.UnmarshalUB2(false);
			if (num > 0)
			{
				txnCtx = this.m_marshallingEngine.UnmarshalNBytes(num);
				return;
			}
			txnCtx = null;
		}

		// Token: 0x0400190E RID: 6414
		internal const int OTXSTA = 1;

		// Token: 0x0400190F RID: 6415
		internal const int OTXDET = 2;

		// Token: 0x04001910 RID: 6416
		internal const int OCI_TRANS_NEW = 1;

		// Token: 0x04001911 RID: 6417
		internal const int OCI_TRANS_JOIN = 2;

		// Token: 0x04001912 RID: 6418
		internal const int OCI_TRANS_RESUME = 4;

		// Token: 0x04001913 RID: 6419
		internal const int OCI_TRANS_PROMOTE = 8;

		// Token: 0x04001914 RID: 6420
		internal const int OCI_TRANS_STARTMASK = 255;

		// Token: 0x04001915 RID: 6421
		internal const int OCI_TRANS_READONLY = 256;

		// Token: 0x04001916 RID: 6422
		internal const int OCI_TRANS_READWRITE = 512;

		// Token: 0x04001917 RID: 6423
		internal const int OCI_TRANS_SERIALIZABLE = 1024;

		// Token: 0x04001918 RID: 6424
		internal const int OCI_TRANS_ISOLMASK = 65280;

		// Token: 0x04001919 RID: 6425
		internal const int OCI_TRANS_LOOSE = 65536;

		// Token: 0x0400191A RID: 6426
		internal const int OCI_TRANS_TIGHT = 131072;

		// Token: 0x0400191B RID: 6427
		internal const int OCI_TRANS_TYPEMASK = 983040;

		// Token: 0x0400191C RID: 6428
		internal const int OCI_TRANS_NOMIGRATE = 1048576;

		// Token: 0x0400191D RID: 6429
		internal const int OCI_TRANS_SEPARABLE = 2097152;

		// Token: 0x0400191E RID: 6430
		internal const int OCI_TRANS_OTSRESUME = 4194304;
	}
}
