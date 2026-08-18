using System;

namespace OracleInternal.Common
{
	// Token: 0x020000C4 RID: 196
	internal enum OracleTraceTag
	{
		// Token: 0x04000A2D RID: 2605
		None,
		// Token: 0x04000A2E RID: 2606
		Error = 268435456,
		// Token: 0x04000A2F RID: 2607
		Environment = 1,
		// Token: 0x04000A30 RID: 2608
		Version,
		// Token: 0x04000A31 RID: 2609
		Config = 4,
		// Token: 0x04000A32 RID: 2610
		Sqlnet = 8,
		// Token: 0x04000A33 RID: 2611
		Tnsnames = 16,
		// Token: 0x04000A34 RID: 2612
		Entry = 256,
		// Token: 0x04000A35 RID: 2613
		Exit = 512,
		// Token: 0x04000A36 RID: 2614
		SQL = 1024,
		// Token: 0x04000A37 RID: 2615
		CP = 2048,
		// Token: 0x04000A38 RID: 2616
		MTS = 4096,
		// Token: 0x04000A39 RID: 2617
		EDM = 8192,
		// Token: 0x04000A3A RID: 2618
		REFCursor = 16384,
		// Token: 0x04000A3B RID: 2619
		EF = 32768,
		// Token: 0x04000A3C RID: 2620
		SelfTuning = 65536,
		// Token: 0x04000A3D RID: 2621
		TTC = 131072,
		// Token: 0x04000A3E RID: 2622
		SvcObj = 262144,
		// Token: 0x04000A3F RID: 2623
		RLB = 524288,
		// Token: 0x04000A40 RID: 2624
		HA = 1048576,
		// Token: 0x04000A41 RID: 2625
		ONS = 2097152,
		// Token: 0x04000A42 RID: 2626
		BUF = 4194304,
		// Token: 0x04000A43 RID: 2627
		XML = 8388608,
		// Token: 0x04000A44 RID: 2628
		BinXML = 16777216,
		// Token: 0x04000A45 RID: 2629
		Send = 33554432,
		// Token: 0x04000A46 RID: 2630
		Receive = 67108864,
		// Token: 0x04000A47 RID: 2631
		Prm = 134217728
	}
}
