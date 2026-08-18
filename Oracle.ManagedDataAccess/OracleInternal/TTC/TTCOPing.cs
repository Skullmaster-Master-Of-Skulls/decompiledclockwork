using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;

namespace OracleInternal.TTC
{
	// Token: 0x0200022D RID: 557
	internal class TTCOPing : TTCFunction
	{
		// Token: 0x06001483 RID: 5251 RVA: 0x000DC5FC File Offset: 0x000DA7FC
		internal TTCOPing(MarshallingEngine mEngine) : base(mEngine, 147, 0)
		{
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x000DC60C File Offset: 0x000DA80C
		internal void Write()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteFunctionHeader();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex, null);
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

		// Token: 0x06001485 RID: 5253 RVA: 0x000DC680 File Offset: 0x000DA880
		internal void ReadResponse()
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
							if (b2 != 9)
							{
								if (b2 != 23)
								{
									throw new Exception("TTC Error: Unexpected Packet Received");
								}
								base.ProcessServerSidePiggybackFunction();
							}
							else
							{
								if (this.m_marshallingEngine.HasEOCSCapability)
								{
									this.m_marshallingEngine.m_endOfCallStatus = this.m_marshallingEngine.UnmarshalUB4(false);
								}
								if (this.m_marshallingEngine.HasFSAPCapability)
								{
									this.m_marshallingEngine.m_endToEndECIDSequenceNumber = this.m_marshallingEngine.UnmarshalUB2(false);
								}
								flag = true;
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
	}
}
