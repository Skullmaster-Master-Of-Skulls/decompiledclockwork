using System;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;

namespace OracleInternal.TTC
{
	// Token: 0x02000233 RID: 563
	internal class TTCSessionGet : TTCFunction
	{
		// Token: 0x0600149F RID: 5279 RVA: 0x000DDCF4 File Offset: 0x000DBEF4
		internal TTCSessionGet(MarshallingEngine mEngine) : base(mEngine, 162, 0)
		{
			this.m_s2cSessionGetflags = 1L;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x000DDD0C File Offset: 0x000DBF0C
		internal void GetSession(long c2sSessionFlags, bool bUseDRCPMultiTag)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteFunctionHeader();
				this.m_c2sSessionGetflags = 0;
				if (bUseDRCPMultiTag)
				{
					this.m_c2sSessionGetflags |= 1;
				}
				this.m_marshallingEngine.MarshalDALC(null);
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalPointer();
				if (this.m_marshallingEngine.NegotiatedTTCVersion >= 8)
				{
					this.m_marshallingEngine.MarshalUB2(this.m_c2sSessionGetflags);
					this.m_marshallingEngine.MarshalPointer();
					this.m_marshallingEngine.MarshalPointer();
				}
				this.ReceiveResponse();
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x000DDDD8 File Offset: 0x000DBFD8
		internal void ReceiveResponse()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool flag = false;
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
								this.ReadRPAMessage();
								break;
							case 9:
								if (this.m_marshallingEngine.HasEOCSCapability)
								{
									this.m_marshallingEngine.m_endOfCallStatus = this.m_marshallingEngine.UnmarshalUB4(false);
								}
								if (this.m_marshallingEngine.HasFSAPCapability)
								{
									this.m_marshallingEngine.m_endToEndECIDSequenceNumber = this.m_marshallingEngine.UnmarshalUB2(false);
								}
								flag = true;
								break;
							default:
								if (b2 != 23)
								{
									throw new Exception("TTC Error");
								}
								base.ProcessServerSidePiggybackFunction();
								break;
							}
						}
						else
						{
							this.m_marshallingEngine.TTCErrorObject.ReadErrorMessage();
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
						break;
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

		// Token: 0x060014A2 RID: 5282 RVA: 0x000DDFAC File Offset: 0x000DC1AC
		internal void ReadRPAMessage()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				int num = this.m_marshallingEngine.UnmarshalUB2(false);
				if (num > 0)
				{
					short n = this.m_marshallingEngine.UnmarshalUB1(false);
					this.m_marshallingEngine.UnmarshalNBytes(null, 0, (int)n);
				}
				this.m_s2cSessionGetflags = this.m_marshallingEngine.UnmarshalUB4(false);
				if (this.m_marshallingEngine.NegotiatedTTCVersion >= 8)
				{
					int num2 = this.m_marshallingEngine.UnmarshalUB2(false);
					if (num2 > 0)
					{
						byte[] bytes = this.m_marshallingEngine.UnmarshalNBytes(num2);
						this.m_returnTag = Encoding.ASCII.GetString(bytes);
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x040018EF RID: 6383
		internal const short SESSGET_TAG_MISMATCH = 1;

		// Token: 0x040018F0 RID: 6384
		internal const short SESSGET_PURITY_NEW = 2;

		// Token: 0x040018F1 RID: 6385
		internal const short SESSGET_SESSION_CHANGED = 4;

		// Token: 0x040018F2 RID: 6386
		internal const short SESSGET_STMTCACHE_DESTROY = 8;

		// Token: 0x040018F3 RID: 6387
		internal const short SESSGET_INFLAGS_MATCHANY = 1;

		// Token: 0x040018F4 RID: 6388
		private int m_c2sSessionGetflags;

		// Token: 0x040018F5 RID: 6389
		internal long m_s2cSessionGetflags;

		// Token: 0x040018F6 RID: 6390
		private string m_returnTag;
	}
}
