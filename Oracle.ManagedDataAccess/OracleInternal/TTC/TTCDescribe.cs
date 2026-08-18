using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;
using OracleInternal.TTC.Accessors;

namespace OracleInternal.TTC
{
	// Token: 0x02000223 RID: 547
	internal class TTCDescribe : TTCFunction
	{
		// Token: 0x06001448 RID: 5192 RVA: 0x000D89CC File Offset: 0x000D6BCC
		internal TTCDescribe(MarshallingEngine mEngine) : base(mEngine, 98, 0)
		{
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x000D89D8 File Offset: 0x000D6BD8
		private void WriteMessage(int cursorId, byte[] sqltext)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				bool flag = cursorId != 0 || sqltext == null || sqltext.Length <= 0;
				base.WriteFunctionHeader();
				this.m_marshallingEngine.MarshalUB1(7);
				this.m_marshallingEngine.MarshalSWORD(cursorId);
				if (flag)
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalSB4(0);
				}
				else
				{
					this.m_marshallingEngine.MarshalPointer();
					this.m_marshallingEngine.MarshalSB4(sqltext.Length);
				}
				this.m_marshallingEngine.MarshalUB4(2L);
				this.m_marshallingEngine.MarshalO2U(true);
				this.m_marshallingEngine.MarshalO2U(true);
				if (!flag)
				{
					this.m_marshallingEngine.MarshalCHR(sqltext);
				}
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

		// Token: 0x0600144A RID: 5194 RVA: 0x000D8AE4 File Offset: 0x000D6CE4
		internal void WriteMessage(int cursor)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.WriteMessage(cursor, null);
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

		// Token: 0x0600144B RID: 5195 RVA: 0x000D8B5C File Offset: 0x000D6D5C
		internal void WriteMessage(byte[] sqltext)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.WriteMessage(0, sqltext);
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

		// Token: 0x0600144C RID: 5196 RVA: 0x000D8BD4 File Offset: 0x000D6DD4
		internal void ReadMessage(Accessor[] accessors)
		{
			int n = (int)this.m_marshallingEngine.UnmarshalUB1(false);
			this.m_marshallingEngine.UnmarshalNBytes_ScanOnly(n);
			this.m_marshallingEngine.UnmarshalUB4(false);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x000D8C08 File Offset: 0x000D6E08
		internal void ReadMessageForRefCursor(Accessor[] accessors)
		{
			this.m_marshallingEngine.UnmarshalUB1(false);
			this.m_marshallingEngine.UnmarshalUB4(false);
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000D8C24 File Offset: 0x000D6E24
		internal void ReadMessage(SQLMetaData sqlMetaData)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			Exception ex = null;
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
								TTCDescribeInfo.ReadMessage(true, false, this.m_marshallingEngine, sqlMetaData, false);
								break;
							case 9:
								if (this.m_marshallingEngine.HasEOCSCapability)
								{
									this.m_marshallingEngine.m_endOfCallStatus = this.m_marshallingEngine.UnmarshalUB4(false);
								}
								flag = true;
								break;
							default:
								if (b2 != 23)
								{
									throw new Exception("TTCDescribe:ReadMessage() - Unexpected Packet received.");
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
					catch (NetworkException ex2)
					{
						if (ex2.ErrorCode != 3111)
						{
							throw;
						}
						this.m_marshallingEngine.m_oracleCommunication.Reset();
					}
					catch (Exception ex3)
					{
						ex = ex3;
						if (this.m_marshallingEngine.m_oraBufRdr != null)
						{
							this.m_marshallingEngine.m_oraBufRdr.ClearState();
						}
						this.m_marshallingEngine.m_oracleCommunication.Break();
						this.m_marshallingEngine.m_oracleCommunication.Reset();
					}
				}
				if (ex != null)
				{
					throw ex;
				}
			}
			catch (Exception ex4)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268566528, ex4, null);
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

		// Token: 0x0400181B RID: 6171
		private const byte OPERATIONFLAGS = 7;

		// Token: 0x0400181C RID: 6172
		private const long SQLPARSEVERSION = 2L;

		// Token: 0x0400181D RID: 6173
		private const int INVALID_CURSOR_ID = 0;

		// Token: 0x0400181E RID: 6174
		internal const int DESCRIBE_SQLTEXT = 0;

		// Token: 0x0400181F RID: 6175
		private const bool UDSARRAYO2U = true;

		// Token: 0x04001820 RID: 6176
		private const bool NUMUDSO2U = true;
	}
}
