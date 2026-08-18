using System;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;

namespace OracleInternal.TTC
{
	// Token: 0x02000234 RID: 564
	internal class TTCSessionRelease : TTCFunction
	{
		// Token: 0x060014A3 RID: 5283 RVA: 0x000DE078 File Offset: 0x000DC278
		internal TTCSessionRelease(MarshallingEngine mEngine) : base(mEngine, 163, 0)
		{
			this.m_ttcCode = 26;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x000DE090 File Offset: 0x000DC290
		internal void ReleaseSession(string drcpTagName, bool bUseDRCPMultiTag)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteFunctionHeader();
				byte[] array = null;
				this.m_sessrlsmode = 0L;
				if (drcpTagName != null)
				{
					array = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(drcpTagName, 0, drcpTagName.Length, true);
					this.m_marshallingEngine.MarshalPointer();
					this.m_marshallingEngine.MarshalSWORD(array.Length);
					this.m_sessrlsmode |= 4L;
					if (this.m_marshallingEngine.NegotiatedTTCVersion >= 8 && bUseDRCPMultiTag)
					{
						this.m_sessrlsmode |= 8L;
					}
				}
				else
				{
					this.m_marshallingEngine.MarshalSWORD(0);
					this.m_marshallingEngine.MarshalNullPointer();
				}
				this.m_marshallingEngine.MarshalUB4(this.m_sessrlsmode);
				if (array != null)
				{
					this.m_marshallingEngine.MarshalCHR(array);
				}
				if (26 == this.m_ttcCode)
				{
					this.m_marshallingEngine.m_oraBufWriter.FlushData();
				}
				else
				{
					this.ReceiveResponse();
				}
				this.m_marshallingEngine.m_bDRCPSessionAttached = false;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000DE1BC File Offset: 0x000DC3BC
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

		// Token: 0x060014A6 RID: 5286 RVA: 0x000DE390 File Offset: 0x000DC590
		internal void ReadRPAMessage()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				long num = this.m_marshallingEngine.UnmarshalUB4(false);
				if (num > 0L)
				{
					byte[] bytes = this.m_marshallingEngine.UnmarshalCHR((int)num);
					this.m_sessrlstag = Encoding.ASCII.GetString(bytes);
				}
				this.m_sessrlsmode = this.m_marshallingEngine.UnmarshalUB4(false);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131584, new string[0]);
				}
			}
		}

		// Token: 0x040018F7 RID: 6391
		internal const short SESSRLS_DROPSESS = 1;

		// Token: 0x040018F8 RID: 6392
		internal const short SESSRLS_DEAUTHENTICATE = 2;

		// Token: 0x040018F9 RID: 6393
		internal const short SESSRLS_RETAG = 4;

		// Token: 0x040018FA RID: 6394
		internal const short SESSRLS_MULTIPROPERTY_TAG = 8;

		// Token: 0x040018FB RID: 6395
		private string m_sessrlstag;

		// Token: 0x040018FC RID: 6396
		private long m_sessrlsmode;
	}
}
