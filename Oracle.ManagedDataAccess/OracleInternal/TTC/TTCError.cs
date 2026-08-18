using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x02000227 RID: 551
	internal class TTCError : TTCMessage
	{
		// Token: 0x06001453 RID: 5203 RVA: 0x000D937C File Offset: 0x000D757C
		internal TTCError(MarshallingEngine mEngine) : base(mEngine, 4)
		{
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x000D9394 File Offset: 0x000D7594
		internal int ErrorCode
		{
			get
			{
				return this.m_retCode;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x000D939C File Offset: 0x000D759C
		internal byte[] ErrorMessage
		{
			get
			{
				return this.m_errorMsg;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x000D93A4 File Offset: 0x000D75A4
		internal int CursorId
		{
			get
			{
				return this.m_currCursorID;
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x000D93AC File Offset: 0x000D75AC
		internal short Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x000D93B4 File Offset: 0x000D75B4
		internal void Initialize()
		{
			this.m_bindErrors = null;
			this.m_retCode = 0;
			this.m_errorMsg = null;
			this.m_TTIWRNFlag = 0;
			this.m_warningFlag = 0;
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x000D93DC File Offset: 0x000D75DC
		internal int ReadErrorMessage()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			int currCursorID;
			try
			{
				if (this.m_marshallingEngine.HasEOCSCapability)
				{
					this.m_marshallingEngine.m_endOfCallStatus = this.m_marshallingEngine.UnmarshalUB4(false);
				}
				if (this.m_marshallingEngine.HasFSAPCapability)
				{
					this.m_marshallingEngine.m_endToEndECIDSequenceNumber = this.m_marshallingEngine.UnmarshalUB2(false);
				}
				this.m_curRowNumber = this.m_marshallingEngine.UnmarshalUB4(false);
				this.m_retCode = this.m_marshallingEngine.UnmarshalUB2(false);
				this.m_arrayElemWError = this.m_marshallingEngine.UnmarshalUB2(true);
				this.m_arrayElemErrno = this.m_marshallingEngine.UnmarshalUB2(true);
				this.m_currCursorID = this.m_marshallingEngine.UnmarshalUB2(false);
				this.m_errorPosition = (short)this.m_marshallingEngine.UnmarshalUB2(true);
				this.m_sqlType = this.m_marshallingEngine.UnmarshalUB1(false);
				this.m_oerFatal = (byte)this.m_marshallingEngine.UnmarshalUB1(true);
				this.m_flags = (short)this.m_marshallingEngine.UnmarshalUB2(false);
				this.m_userCursorOpt = (short)this.m_marshallingEngine.UnmarshalUB2(true);
				this.m_upiParam = this.m_marshallingEngine.UnmarshalUB1(true);
				this.m_warningFlag = this.m_marshallingEngine.UnmarshalUB1(false);
				this.m_rba = this.m_marshallingEngine.UnmarshalUB4(true);
				this.m_partitionId = this.m_marshallingEngine.UnmarshalUB2(true);
				this.m_tableId = this.m_marshallingEngine.UnmarshalUB1(true);
				this.m_blockNumber = this.m_marshallingEngine.UnmarshalUB4(true);
				this.m_slotNumber = this.m_marshallingEngine.UnmarshalUB2(true);
				this.m_osError = (int)this.m_marshallingEngine.UnmarshalUB4(true);
				this.m_stmtNumber = this.m_marshallingEngine.UnmarshalUB1(true);
				this.m_callNumber = this.m_marshallingEngine.UnmarshalUB1(true);
				this.m_pad1 = this.m_marshallingEngine.UnmarshalUB2(true);
				this.m_successIters = this.m_marshallingEngine.UnmarshalUB4(true);
				this.m_marshallingEngine.UnmarshalDALC(true, null);
				int num = this.m_marshallingEngine.UnmarshalUB2(false);
				if (num > 0)
				{
					this.m_bindErrors = new TTCArrayBindError[num];
					short num2 = this.m_marshallingEngine.UnmarshalUB1(false);
					bool flag = num2 == 254;
					for (int i = 0; i < num; i++)
					{
						if (flag)
						{
							if (this.m_marshallingEngine.m_bUseBigCLRChunks)
							{
								this.m_marshallingEngine.UnmarshalSB4();
							}
							else
							{
								this.m_marshallingEngine.UnmarshalUB1(false);
							}
						}
						this.m_bindErrors[i] = default(TTCArrayBindError);
						this.m_bindErrors[i].m_errorCode = this.m_marshallingEngine.UnmarshalUB2(false);
					}
					if (flag)
					{
						this.m_marshallingEngine.UnmarshalUB1(false);
					}
				}
				int num3 = (int)this.m_marshallingEngine.UnmarshalUB4(false);
				if (num3 > 0)
				{
					short num4 = this.m_marshallingEngine.UnmarshalUB1(false);
					bool flag2 = num4 == 254;
					for (int j = 0; j < num3; j++)
					{
						if (flag2)
						{
							if (this.m_marshallingEngine.m_bUseBigCLRChunks)
							{
								this.m_marshallingEngine.UnmarshalSB4();
							}
							else
							{
								this.m_marshallingEngine.UnmarshalUB1(false);
							}
						}
						this.m_bindErrors[j].m_rowOffset = (int)this.m_marshallingEngine.UnmarshalUB4(false);
					}
					if (flag2)
					{
						this.m_marshallingEngine.UnmarshalUB1(false);
					}
				}
				int num5 = this.m_marshallingEngine.UnmarshalUB2(false);
				if (num5 > 0)
				{
					this.m_marshallingEngine.UnmarshalUB1(false);
					int[] array = new int[1];
					for (int k = 0; k < num5; k++)
					{
						int buflen = this.m_marshallingEngine.UnmarshalUB2(false);
						this.m_bindErrors[k].m_errorMsg = this.m_marshallingEngine.UnmarshalCLR(buflen, array);
						this.m_bindErrors[k].m_errorLength = array[0];
						this.m_marshallingEngine.UnmarshalUB1(false);
						this.m_marshallingEngine.UnmarshalUB1(false);
					}
				}
				if (this.m_marshallingEngine.NegotiatedTTCVersion >= 7)
				{
					this.m_retCode = (int)this.m_marshallingEngine.UnmarshalUB4(false);
					this.m_curRowNumber = this.m_marshallingEngine.UnmarshalSB8();
				}
				if (this.m_sqlType == 3 && 1403 == this.m_retCode)
				{
					this.m_marshallingEngine.UnmarshalCLRforREFS(true);
				}
				else if (this.m_retCode != 0)
				{
					this.m_errorMsg = this.m_marshallingEngine.UnmarshalCLRforREFS(false);
					this.m_errorLength[0] = (short)this.m_errorMsg.Length;
				}
				currCursorID = this.m_currCursorID;
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
			return currCursorID;
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x000D98BC File Offset: 0x000D7ABC
		internal void ReadWarning()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.m_retCode = this.m_marshallingEngine.UnmarshalUB2(false);
				this.m_warnLength = this.m_marshallingEngine.UnmarshalUB2(false);
				this.m_TTIWRNFlag = this.m_marshallingEngine.UnmarshalUB2(false);
				if (this.m_retCode != 0 && this.m_warnLength > 0)
				{
					this.m_errorMsg = this.m_marshallingEngine.UnmarshalCHR(this.m_warnLength);
					this.m_errorLength[0] = (short)this.m_warnLength;
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

		// Token: 0x0400182E RID: 6190
		internal const int OERFUPD = 8;

		// Token: 0x0400182F RID: 6191
		internal const int OERFEXIT = 16;

		// Token: 0x04001830 RID: 6192
		internal const int OERFNCF = 32;

		// Token: 0x04001831 RID: 6193
		internal const int OERwNVIC = 4;

		// Token: 0x04001832 RID: 6194
		internal const int OERwUDnW = 16;

		// Token: 0x04001833 RID: 6195
		internal const int OERwCPER = 32;

		// Token: 0x04001834 RID: 6196
		internal const int OERFPLSW = 4;

		// Token: 0x04001835 RID: 6197
		internal TTCArrayBindError[] m_bindErrors;

		// Token: 0x04001836 RID: 6198
		internal long m_curRowNumber;

		// Token: 0x04001837 RID: 6199
		internal int m_retCode;

		// Token: 0x04001838 RID: 6200
		private int m_arrayElemWError;

		// Token: 0x04001839 RID: 6201
		private int m_arrayElemErrno;

		// Token: 0x0400183A RID: 6202
		private int m_currCursorID;

		// Token: 0x0400183B RID: 6203
		private short m_errorPosition;

		// Token: 0x0400183C RID: 6204
		private short m_sqlType;

		// Token: 0x0400183D RID: 6205
		private byte m_oerFatal;

		// Token: 0x0400183E RID: 6206
		internal short m_flags;

		// Token: 0x0400183F RID: 6207
		private short m_userCursorOpt;

		// Token: 0x04001840 RID: 6208
		private short m_upiParam;

		// Token: 0x04001841 RID: 6209
		internal short m_warningFlag;

		// Token: 0x04001842 RID: 6210
		private int m_osError;

		// Token: 0x04001843 RID: 6211
		private short m_stmtNumber;

		// Token: 0x04001844 RID: 6212
		private short m_callNumber;

		// Token: 0x04001845 RID: 6213
		private int m_pad1;

		// Token: 0x04001846 RID: 6214
		private long m_successIters;

		// Token: 0x04001847 RID: 6215
		private int m_partitionId;

		// Token: 0x04001848 RID: 6216
		private short m_tableId;

		// Token: 0x04001849 RID: 6217
		private int m_slotNumber;

		// Token: 0x0400184A RID: 6218
		private long m_rba;

		// Token: 0x0400184B RID: 6219
		private long m_blockNumber;

		// Token: 0x0400184C RID: 6220
		private int m_warnLength;

		// Token: 0x0400184D RID: 6221
		internal int m_TTIWRNFlag;

		// Token: 0x0400184E RID: 6222
		private short[] m_errorLength = new short[1];

		// Token: 0x0400184F RID: 6223
		internal byte[] m_errorMsg;
	}
}
