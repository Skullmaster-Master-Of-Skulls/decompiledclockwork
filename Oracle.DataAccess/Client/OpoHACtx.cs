using System;
using System.Runtime.InteropServices;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000033 RID: 51
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal class OpoHACtx
	{
		// Token: 0x0600021D RID: 541 RVA: 0x0001C4E8 File Offset: 0x0001B4E8
		public OpoHACtx()
		{
			this.eventType = HAEventType.Invalid;
			this.dbName = null;
			this.dbDomainName = null;
			this.hostName = null;
			this.instName = null;
			this.serviceName = null;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0001C51A File Offset: 0x0001B51A
		public void Process()
		{
			ConnectionDispenser.HACallbackProcessing(this);
		}

		// Token: 0x0400019F RID: 415
		public HAEventType eventType;

		// Token: 0x040001A0 RID: 416
		public OracleHAEventSource source;

		// Token: 0x040001A1 RID: 417
		public OracleHAEventStatus status;

		// Token: 0x040001A2 RID: 418
		public string dbName;

		// Token: 0x040001A3 RID: 419
		public string dbDomainName;

		// Token: 0x040001A4 RID: 420
		public string hostName;

		// Token: 0x040001A5 RID: 421
		public string instName;

		// Token: 0x040001A6 RID: 422
		public string serviceName;

		// Token: 0x040001A7 RID: 423
		public short year;

		// Token: 0x040001A8 RID: 424
		public byte month;

		// Token: 0x040001A9 RID: 425
		public byte day;

		// Token: 0x040001AA RID: 426
		public byte hour;

		// Token: 0x040001AB RID: 427
		public byte min;

		// Token: 0x040001AC RID: 428
		public byte sec;

		// Token: 0x040001AD RID: 429
		public uint fsec;

		// Token: 0x040001AE RID: 430
		public int cardinality;
	}
}
