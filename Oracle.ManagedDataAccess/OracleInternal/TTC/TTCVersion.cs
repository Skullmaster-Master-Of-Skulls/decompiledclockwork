using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;

namespace OracleInternal.TTC
{
	// Token: 0x0200023B RID: 571
	internal class TTCVersion : TTCFunction
	{
		// Token: 0x060014C9 RID: 5321 RVA: 0x000DF384 File Offset: 0x000DD584
		internal TTCVersion(MarshallingEngine marshallingEngine) : base(marshallingEngine, 59, 0)
		{
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x000DF3B4 File Offset: 0x000DD5B4
		internal override void ReInit(MarshallingEngine marshallingEngine)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.ReInit(marshallingEngine);
				this.m_bufferLen = 256;
				this.m_retVersionLength = 0;
				this.m_retVersionNumber = 0L;
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

		// Token: 0x060014CB RID: 5323 RVA: 0x000DF444 File Offset: 0x000DD644
		internal void ReadResponse()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			bool flag = false;
			try
			{
				bool flag2 = false;
				while (!flag2)
				{
					try
					{
						byte b = (byte)this.m_marshallingEngine.UnmarshalUB1(false);
						byte b2 = b;
						if (b2 != 4)
						{
							switch (b2)
							{
							case 8:
								if (flag)
								{
									throw new Exception("TTC Error");
								}
								this.m_retVersionLength = this.m_marshallingEngine.UnmarshalUB2(false);
								this.m_rdbmsVersion = this.m_marshallingEngine.UnmarshalCHR(this.m_retVersionLength);
								if (this.m_rdbmsVersion == null)
								{
									throw new Exception("TTC Error");
								}
								this.m_retVersionNumber = this.m_marshallingEngine.UnmarshalUB4(false);
								flag = true;
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
								flag2 = true;
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
							this.m_marshallingEngine.TTCErrorObject.Initialize();
							this.m_marshallingEngine.TTCErrorObject.ReadErrorMessage();
							flag2 = true;
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

		// Token: 0x060014CC RID: 5324 RVA: 0x000DF680 File Offset: 0x000DD880
		internal short GetVersionNumber()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			int num = 0;
			short result;
			try
			{
				num += (int)(HelperClass.URShift(this.m_retVersionNumber, 24) & 255L) * 1000;
				num += (int)(HelperClass.URShift(this.m_retVersionNumber, 20) & 15L) * 100;
				num += (int)(HelperClass.URShift(this.m_retVersionNumber, 12) & 15L) * 10;
				num += (int)(HelperClass.URShift(this.m_retVersionNumber, 8) & 15L);
				result = (short)num;
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
			return result;
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x000DF754 File Offset: 0x000DD954
		internal void WriteMessage()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteFunctionHeader();
				bool notnull = true;
				this.m_marshallingEngine.MarshalO2U(notnull);
				this.m_marshallingEngine.MarshalSWORD(this.m_bufferLen);
				this.m_marshallingEngine.MarshalO2U(notnull);
				this.m_marshallingEngine.MarshalO2U(notnull);
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

		// Token: 0x04001934 RID: 6452
		private byte[] m_rdbmsVersion = new byte[]
		{
			78,
			111,
			116,
			32,
			100,
			101,
			116,
			101,
			114,
			109,
			105,
			110,
			101,
			100,
			32,
			121,
			101,
			116
		};

		// Token: 0x04001935 RID: 6453
		private int m_bufferLen = 256;

		// Token: 0x04001936 RID: 6454
		private int m_retVersionLength;

		// Token: 0x04001937 RID: 6455
		internal long m_retVersionNumber;
	}
}
