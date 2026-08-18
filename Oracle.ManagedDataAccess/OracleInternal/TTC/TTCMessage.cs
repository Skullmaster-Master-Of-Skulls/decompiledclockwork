using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;

namespace OracleInternal.TTC
{
	// Token: 0x02000217 RID: 535
	internal class TTCMessage
	{
		// Token: 0x060013F9 RID: 5113 RVA: 0x000D1FF4 File Offset: 0x000D01F4
		internal TTCMessage(MarshallingEngine marshallingEngine, byte ttcCode)
		{
			this.m_marshallingEngine = marshallingEngine;
			this.m_ttcCode = ttcCode;
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x000D200C File Offset: 0x000D020C
		internal void WriteTTCCode()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)131328, new string[0]);
			}
			try
			{
				this.m_marshallingEngine.MarshalUB1((short)this.m_ttcCode);
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

		// Token: 0x060013FB RID: 5115 RVA: 0x000D208C File Offset: 0x000D028C
		internal virtual void ReInit(MarshallingEngine marshallingEngine)
		{
			this.m_marshallingEngine = marshallingEngine;
		}

		// Token: 0x04001509 RID: 5385
		internal const byte TTIPRO = 1;

		// Token: 0x0400150A RID: 5386
		internal const byte TTIDTY = 2;

		// Token: 0x0400150B RID: 5387
		internal const byte TTIFUN = 3;

		// Token: 0x0400150C RID: 5388
		internal const byte TTIOER = 4;

		// Token: 0x0400150D RID: 5389
		internal const byte TTIRXH = 6;

		// Token: 0x0400150E RID: 5390
		internal const byte TTIRXD = 7;

		// Token: 0x0400150F RID: 5391
		internal const byte TTIRPA = 8;

		// Token: 0x04001510 RID: 5392
		internal const byte TTISTA = 9;

		// Token: 0x04001511 RID: 5393
		internal const byte TTIIOV = 11;

		// Token: 0x04001512 RID: 5394
		internal const byte TTISLG = 12;

		// Token: 0x04001513 RID: 5395
		internal const byte TTIOAC = 13;

		// Token: 0x04001514 RID: 5396
		internal const byte TTILOBD = 14;

		// Token: 0x04001515 RID: 5397
		internal const byte TTIWRN = 15;

		// Token: 0x04001516 RID: 5398
		internal const byte TTIDCB = 16;

		// Token: 0x04001517 RID: 5399
		internal const byte TTIPFN = 17;

		// Token: 0x04001518 RID: 5400
		internal const byte TTIFOB = 19;

		// Token: 0x04001519 RID: 5401
		internal const byte TTIBVC = 21;

		// Token: 0x0400151A RID: 5402
		internal const byte TTISPF = 23;

		// Token: 0x0400151B RID: 5403
		internal const byte TTIONEWAYFN = 26;

		// Token: 0x0400151C RID: 5404
		internal const byte TTIIMPLRES = 27;

		// Token: 0x0400151D RID: 5405
		internal const byte TTIRENEG = 28;

		// Token: 0x0400151E RID: 5406
		internal const byte OCQCINV = 1;

		// Token: 0x0400151F RID: 5407
		internal const byte OCOSPID = 2;

		// Token: 0x04001520 RID: 5408
		internal const byte OCTRCEVT = 3;

		// Token: 0x04001521 RID: 5409
		internal const byte OCSESSRET = 4;

		// Token: 0x04001522 RID: 5410
		internal const byte OCSSYNC = 5;

		// Token: 0x04001523 RID: 5411
		internal const byte OCXSSS = 6;

		// Token: 0x04001524 RID: 5412
		internal const byte OCLTXID = 7;

		// Token: 0x04001525 RID: 5413
		internal const byte OCAPPCONTCTL = 8;

		// Token: 0x04001526 RID: 5414
		internal const byte OCXSSS2 = 9;

		// Token: 0x04001527 RID: 5415
		internal const byte MAX_OCFN = 7;

		// Token: 0x04001528 RID: 5416
		internal byte m_ttcCode;

		// Token: 0x04001529 RID: 5417
		internal MarshallingEngine m_marshallingEngine;
	}
}
