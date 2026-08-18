using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x02000218 RID: 536
	internal class TTCFunction : TTCMessage
	{
		// Token: 0x060013FC RID: 5116 RVA: 0x000D2098 File Offset: 0x000D0298
		internal TTCFunction(MarshallingEngine marshallingEngine, short functionCode, byte sequenceNumber) : base(marshallingEngine, 3)
		{
			this.m_functionCode = functionCode;
			this.m_sequenceNumber = sequenceNumber;
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x000D20BC File Offset: 0x000D02BC
		internal void WriteFunctionHeader()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				base.WriteTTCCode();
				this.m_marshallingEngine.MarshalUB1(this.m_functionCode);
				this.m_marshallingEngine.MarshalUB1((short)this.m_sequenceNumber);
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

		// Token: 0x060013FE RID: 5118 RVA: 0x000D2154 File Offset: 0x000D0354
		internal void ProcessServerSidePiggybackFunction()
		{
			switch ((byte)this.m_marshallingEngine.UnmarshalUB1(false))
			{
			case 1:
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.TTC, new string[]
					{
						"Received Server Piggy Back Message: OCQCINV"
					});
					return;
				}
				return;
			case 2:
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.TTC, new string[]
					{
						"Received Server Piggy Back Message: OCOSPID"
					});
				}
				int n = this.m_marshallingEngine.UnmarshalUB2(false);
				this.m_marshallingEngine.UnmarshalUB1(true);
				this.m_marshallingEngine.UnmarshalNBytes_ScanOnly(n);
				return;
			}
			case 3:
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.TTC, new string[]
					{
						"Received Server Piggy Back Message: OCTRCEVT"
					});
					return;
				}
				return;
			case 4:
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.TTC, new string[]
					{
						"Received Server Piggy Back Message: OCSESSRET"
					});
				}
				if (this.m_marshallingEngine.m_drcpSessionReturnValues == null)
				{
					this.m_marshallingEngine.m_drcpSessionReturnValues = new TTCSessionReturnValues(this.m_marshallingEngine);
				}
				this.m_marshallingEngine.m_drcpSessionReturnValues.Receive();
				return;
			case 5:
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.TTC, new string[]
					{
						"Received Server Piggy Back Message: OCSSYNC"
					});
					return;
				}
				return;
			case 7:
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.TTC, new string[]
					{
						"Received Server Piggy Back Message: OCLTXID"
					});
				}
				long num = this.m_marshallingEngine.UnmarshalUB4(false);
				if (num > 0L)
				{
					if (this.m_marshallingEngine.m_ltxId == null || (long)this.m_marshallingEngine.m_ltxId.Length != num)
					{
						this.m_marshallingEngine.m_ltxId = new byte[num];
					}
					this.m_marshallingEngine.UnmarshalCLR(this.m_marshallingEngine.m_ltxId, 0, this.retLen);
					return;
				}
				return;
			}
			case 8:
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.TTC, new string[]
					{
						"Received Server Piggy Back Message: OCAPPCONTCTL"
					});
				}
				this.m_marshallingEngine.UnmarshalUB2(true);
				this.m_marshallingEngine.UnmarshalUB1(true);
				this.m_marshallingEngine.UnmarshalUB4(true);
				this.m_marshallingEngine.UnmarshalUB4(true);
				this.m_marshallingEngine.UnmarshalUB1(true);
				this.m_marshallingEngine.UnmarshalDALC(true, null);
				return;
			case 9:
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.TTC, new string[]
					{
						"Received Server Piggy Back Message: OCXSSS2"
					});
				}
				this.m_marshallingEngine.UnmarshalUB2(true);
				this.m_marshallingEngine.UnmarshalUB1(true);
				return;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.TTC, new string[]
				{
					"Received Server Piggy Back Message: UNKNOWN (protocol violation)"
				});
			}
		}

		// Token: 0x0400152A RID: 5418
		internal const short OOPEN = 2;

		// Token: 0x0400152B RID: 5419
		internal const short OEXEC = 4;

		// Token: 0x0400152C RID: 5420
		internal const short OFETCH = 5;

		// Token: 0x0400152D RID: 5421
		internal const short OCLOSE = 8;

		// Token: 0x0400152E RID: 5422
		internal const short OLOGOFF = 9;

		// Token: 0x0400152F RID: 5423
		internal const short OCOMON = 12;

		// Token: 0x04001530 RID: 5424
		internal const short OCOMOFF = 13;

		// Token: 0x04001531 RID: 5425
		internal const short OCOMMIT = 14;

		// Token: 0x04001532 RID: 5426
		internal const short OROLLBACK = 15;

		// Token: 0x04001533 RID: 5427
		internal const short OCANCEL = 20;

		// Token: 0x04001534 RID: 5428
		internal const short ODSCRARR = 43;

		// Token: 0x04001535 RID: 5429
		internal const short OVERSION = 59;

		// Token: 0x04001536 RID: 5430
		internal const short OK2RPC = 67;

		// Token: 0x04001537 RID: 5431
		internal const short OALL7 = 71;

		// Token: 0x04001538 RID: 5432
		internal const short OSQL7 = 74;

		// Token: 0x04001539 RID: 5433
		internal const short OEXFEN = 78;

		// Token: 0x0400153A RID: 5434
		internal const short O3LOGON = 81;

		// Token: 0x0400153B RID: 5435
		internal const short O3LOGA = 82;

		// Token: 0x0400153C RID: 5436
		internal const short OKOD = 92;

		// Token: 0x0400153D RID: 5437
		internal const short OALL8 = 94;

		// Token: 0x0400153E RID: 5438
		internal const short OLOBOPS = 96;

		// Token: 0x0400153F RID: 5439
		internal const short ODNY = 98;

		// Token: 0x04001540 RID: 5440
		internal const short OTXSE = 103;

		// Token: 0x04001541 RID: 5441
		internal const short OTXEN = 104;

		// Token: 0x04001542 RID: 5442
		internal const short OCCA = 105;

		// Token: 0x04001543 RID: 5443
		internal const short O80SES = 107;

		// Token: 0x04001544 RID: 5444
		internal const short OAUTH = 115;

		// Token: 0x04001545 RID: 5445
		internal const short OSESSKEY = 118;

		// Token: 0x04001546 RID: 5446
		internal const short OCANA = 120;

		// Token: 0x04001547 RID: 5447
		internal const short OKPN = 125;

		// Token: 0x04001548 RID: 5448
		internal const short OPING = 147;

		// Token: 0x04001549 RID: 5449
		internal const short OSCID = 135;

		// Token: 0x0400154A RID: 5450
		internal const short OSESSGET = 162;

		// Token: 0x0400154B RID: 5451
		internal const short OSESSRLS = 163;

		// Token: 0x0400154C RID: 5452
		internal short m_functionCode;

		// Token: 0x0400154D RID: 5453
		private byte m_sequenceNumber;

		// Token: 0x0400154E RID: 5454
		private int[] retLen = new int[1];
	}
}
