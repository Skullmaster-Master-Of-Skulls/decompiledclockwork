using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.Network;

namespace OracleInternal.TTC
{
	// Token: 0x0200022E RID: 558
	internal class TTCNotification : TTCFunction
	{
		// Token: 0x06001486 RID: 5254 RVA: 0x000DC844 File Offset: 0x000DAA44
		internal TTCNotification(MarshallingEngine mEngine) : base(mEngine, 125, 0)
		{
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x000DC850 File Offset: 0x000DAA50
		internal void WriteOKPNMessage(int opcode, int mode, string userName, string location, int numRegistrationInfo, int[] nameSpace, string[] registeredAgentName, byte[][] kpdnrcx, int[] payloadType, int[] qosFlags, int[] timeout, int[] dbchangeOpFilter, int[] dbchangeTxnLag, int[] dbchangeRegistrationId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				int val = 1;
				int val2 = 2;
				base.WriteFunctionHeader();
				this.m_marshallingEngine.MarshalUB1((short)((byte)opcode));
				this.m_marshallingEngine.MarshalUB4((long)mode);
				byte[] array = null;
				if (userName != null)
				{
					array = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(userName, 0, userName.Length, true);
					this.m_marshallingEngine.MarshalPointer();
					this.m_marshallingEngine.MarshalUB4((long)array.Length);
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalUB4(0L);
				}
				byte[] array2 = null;
				if (location != null)
				{
					array2 = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(location, 0, location.Length, true);
					this.m_marshallingEngine.MarshalPointer();
					this.m_marshallingEngine.MarshalUB4((long)array2.Length);
				}
				else
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalUB4(0L);
				}
				this.m_marshallingEngine.MarshalPointer();
				this.m_marshallingEngine.MarshalUB4((long)numRegistrationInfo);
				this.m_marshallingEngine.MarshalUB2(val);
				this.m_marshallingEngine.MarshalUB2(val2);
				if (this.m_marshallingEngine.NegotiatedTTCVersion >= 4)
				{
					this.m_marshallingEngine.MarshalNullPointer();
					this.m_marshallingEngine.MarshalPointer();
					if (this.m_marshallingEngine.NegotiatedTTCVersion >= 5)
					{
						this.m_marshallingEngine.MarshalNullPointer();
						this.m_marshallingEngine.MarshalPointer();
						if (this.m_marshallingEngine.NegotiatedTTCVersion >= 7)
						{
							this.m_marshallingEngine.MarshalPointer();
							this.m_marshallingEngine.MarshalPointer();
							this.m_marshallingEngine.MarshalPointer();
							this.m_marshallingEngine.MarshalPointer();
							this.m_marshallingEngine.MarshalPointer();
							this.m_marshallingEngine.MarshalSB4(29);
							this.m_marshallingEngine.MarshalPointer();
						}
					}
				}
				if (array != null)
				{
					this.m_marshallingEngine.MarshalCHR(array);
				}
				if (array2 != null)
				{
					this.m_marshallingEngine.MarshalCHR(array2);
				}
				for (int i = 0; i < numRegistrationInfo; i++)
				{
					this.m_marshallingEngine.MarshalUB4((long)nameSpace[i]);
					byte[] array3 = null;
					if (registeredAgentName[i] != null)
					{
						array3 = this.m_marshallingEngine.m_dbCharSetConv.ConvertStringToBytes(registeredAgentName[i], 0, registeredAgentName[i].Length, true);
					}
					if (array3 != null && array3.Length > 0)
					{
						this.m_marshallingEngine.MarshalUB4((long)array3.Length);
						this.m_marshallingEngine.MarshalCLR(array3, 0, array3.Length);
					}
					else
					{
						this.m_marshallingEngine.MarshalUB4(0L);
					}
					if (kpdnrcx[i] != null && kpdnrcx[i].Length > 0)
					{
						this.m_marshallingEngine.MarshalUB4((long)kpdnrcx[i].Length);
						this.m_marshallingEngine.MarshalCLR(kpdnrcx[i], 0, kpdnrcx[i].Length);
					}
					else
					{
						this.m_marshallingEngine.MarshalUB4(0L);
					}
					this.m_marshallingEngine.MarshalUB4((long)payloadType[i]);
					if (this.m_marshallingEngine.NegotiatedTTCVersion >= 4)
					{
						this.m_marshallingEngine.MarshalUB4((long)qosFlags[i]);
						int num = 0;
						this.m_marshallingEngine.MarshalUB4((long)num);
						this.m_marshallingEngine.MarshalUB4((long)timeout[i]);
						int num2 = 0;
						this.m_marshallingEngine.MarshalUB4((long)num2);
						this.m_marshallingEngine.MarshalUB4((long)dbchangeOpFilter[i]);
						this.m_marshallingEngine.MarshalUB4((long)dbchangeTxnLag[i]);
						this.m_marshallingEngine.MarshalUB4((long)dbchangeRegistrationId[i]);
						if (this.m_marshallingEngine.NegotiatedTTCVersion >= 5)
						{
							this.m_marshallingEngine.MarshalUB1(0);
							this.m_marshallingEngine.MarshalUB4(0L);
							this.m_marshallingEngine.MarshalUB1(0);
							this.m_marshallingEngine.MarshalDALC(null);
							this.m_marshallingEngine.MarshalSB4(0);
							this.m_marshallingEngine.MarshalSB8((long)dbchangeRegistrationId[i]);
						}
					}
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

		// Token: 0x06001488 RID: 5256 RVA: 0x000DCC70 File Offset: 0x000DAE70
		internal int ReceiveOKPNResponse()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			int num = 0;
			bool flag = false;
			int result;
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
							{
								int num2 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
								for (int i = 0; i < num2; i++)
								{
									this.m_marshallingEngine.UnmarshalUB4(false);
								}
								for (int j = 0; j < num2; j++)
								{
									if (j == 0)
									{
										num = (int)this.m_marshallingEngine.UnmarshalUB4(false);
									}
									else
									{
										this.m_marshallingEngine.UnmarshalUB4(false);
									}
								}
								if (this.m_marshallingEngine.NegotiatedTTCVersion >= 5)
								{
									int num3 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
									long num4 = 0L;
									for (int k = 0; k < num3; k++)
									{
										if (k == 0)
										{
											num4 = this.m_marshallingEngine.UnmarshalSB8();
										}
										else
										{
											this.m_marshallingEngine.UnmarshalSB8();
										}
										if (this.m_marshallingEngine.NegotiatedTTCVersion >= 7)
										{
											int num5 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
											if (num5 > 0)
											{
												byte[] byteValue = new byte[num5];
												this.m_marshallingEngine.UnmarshalBuffer(byteValue, 0, num5);
											}
										}
									}
									num = (int)num4;
									if (this.m_marshallingEngine.NegotiatedTTCVersion >= 7)
									{
										int num6 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
										for (int l = 0; l < num6; l++)
										{
											int num7 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
											if (num7 > 0)
											{
												byte[] bytes = new byte[num7];
												int[] intArray = new int[1];
												this.m_marshallingEngine.UnmarshalCLR(bytes, 0, intArray, num7);
											}
										}
										int num8 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
										for (int m = 0; m < num8; m++)
										{
											int num9 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
											if (num9 > 0)
											{
												byte[] bytes2 = new byte[num9];
												int[] intArray2 = new int[1];
												this.m_marshallingEngine.UnmarshalCLR(bytes2, 0, intArray2, num9);
											}
										}
										int num10 = this.m_marshallingEngine.UnmarshalUB2(false);
										if (num10 > 0)
										{
											byte[] array = new byte[num10];
											array = this.m_marshallingEngine.UnmarshalCHR(num10);
											if (array != null)
											{
											}
										}
									}
								}
								break;
							}
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
									throw new Exception("TTCNotification:ReceiveOKPNResponse - Unexpected Packet received.");
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
						return num;
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
				result = num;
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
			return result;
		}

		// Token: 0x040018B7 RID: 6327
		internal const int REGISTER_KPNDEF = 1;

		// Token: 0x040018B8 RID: 6328
		internal const int UNREGISTER_KPNDEF = 2;

		// Token: 0x040018B9 RID: 6329
		internal const int POST_KPNDEF = 3;

		// Token: 0x040018BA RID: 6330
		internal const int EXISTINGCLIENT_KPNDEF = 0;

		// Token: 0x040018BB RID: 6331
		internal const int NEWCLIENT_KPNDEF = 1;

		// Token: 0x040018BC RID: 6332
		internal const int KPD_NTFN_CONNID_LEN = 29;

		// Token: 0x040018BD RID: 6333
		internal const int KPUN_PRS_RAW = 1;

		// Token: 0x040018BE RID: 6334
		internal const int KPUN_VER_10200 = 2;

		// Token: 0x040018BF RID: 6335
		internal const int OCI_SUBSCR_NAMESPACE_ANONYMOUS = 0;

		// Token: 0x040018C0 RID: 6336
		internal const int OCI_SUBSCR_NAMESPACE_AQ = 1;

		// Token: 0x040018C1 RID: 6337
		internal const int OCI_SUBSCR_NAMESPACE_DBCHANGE = 2;

		// Token: 0x040018C2 RID: 6338
		internal const int OCI_SUBSCR_NAMESPACE_MAX = 3;

		// Token: 0x040018C3 RID: 6339
		internal const int KPD_CHNF_OPFILTER = 1;

		// Token: 0x040018C4 RID: 6340
		internal const int KPD_CHNF_INSERT = 2;

		// Token: 0x040018C5 RID: 6341
		internal const int KPD_CHNF_UPDATE = 4;

		// Token: 0x040018C6 RID: 6342
		internal const int KPD_CHNF_DELETE = 8;

		// Token: 0x040018C7 RID: 6343
		internal const int KPD_CHNF_ROWID = 16;

		// Token: 0x040018C8 RID: 6344
		internal const int KPD_CQ_QUERYNF = 32;

		// Token: 0x040018C9 RID: 6345
		internal const int KPD_CQ_BEST_EFFORT = 64;

		// Token: 0x040018CA RID: 6346
		internal const int KPD_CQ_CLQRYCACHE = 128;

		// Token: 0x040018CB RID: 6347
		internal const int KPD_CHNF_INVALID_REGID = 0;

		// Token: 0x040018CC RID: 6348
		internal const int SUBSCR_QOS_RELIABLE = 1;

		// Token: 0x040018CD RID: 6349
		internal const int SUBSCR_QOS_PAYLOAD = 2;

		// Token: 0x040018CE RID: 6350
		internal const int SUBSCR_QOS_REPLICATE = 4;

		// Token: 0x040018CF RID: 6351
		internal const int SUBSCR_QOS_SECURE = 8;

		// Token: 0x040018D0 RID: 6352
		internal const int SUBSCR_QOS_PURGE_ON_NTFN = 16;

		// Token: 0x040018D1 RID: 6353
		internal const int SUBSCR_QOS_MULTICBK = 32;
	}
}
