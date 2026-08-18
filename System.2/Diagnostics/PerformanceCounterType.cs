using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x020004EE RID: 1262
	[TypeConverter(typeof(AlphabeticalEnumConverter))]
	public enum PerformanceCounterType
	{
		// Token: 0x04002817 RID: 10263
		NumberOfItems32 = 65536,
		// Token: 0x04002818 RID: 10264
		NumberOfItems64 = 65792,
		// Token: 0x04002819 RID: 10265
		NumberOfItemsHEX32 = 0,
		// Token: 0x0400281A RID: 10266
		NumberOfItemsHEX64 = 256,
		// Token: 0x0400281B RID: 10267
		RateOfCountsPerSecond32 = 272696320,
		// Token: 0x0400281C RID: 10268
		RateOfCountsPerSecond64 = 272696576,
		// Token: 0x0400281D RID: 10269
		CountPerTimeInterval32 = 4523008,
		// Token: 0x0400281E RID: 10270
		CountPerTimeInterval64 = 4523264,
		// Token: 0x0400281F RID: 10271
		RawFraction = 537003008,
		// Token: 0x04002820 RID: 10272
		RawBase = 1073939459,
		// Token: 0x04002821 RID: 10273
		AverageTimer32 = 805438464,
		// Token: 0x04002822 RID: 10274
		AverageBase = 1073939458,
		// Token: 0x04002823 RID: 10275
		AverageCount64 = 1073874176,
		// Token: 0x04002824 RID: 10276
		SampleFraction = 549585920,
		// Token: 0x04002825 RID: 10277
		SampleCounter = 4260864,
		// Token: 0x04002826 RID: 10278
		SampleBase = 1073939457,
		// Token: 0x04002827 RID: 10279
		CounterTimer = 541132032,
		// Token: 0x04002828 RID: 10280
		CounterTimerInverse = 557909248,
		// Token: 0x04002829 RID: 10281
		Timer100Ns = 542180608,
		// Token: 0x0400282A RID: 10282
		Timer100NsInverse = 558957824,
		// Token: 0x0400282B RID: 10283
		ElapsedTime = 807666944,
		// Token: 0x0400282C RID: 10284
		CounterMultiTimer = 574686464,
		// Token: 0x0400282D RID: 10285
		CounterMultiTimerInverse = 591463680,
		// Token: 0x0400282E RID: 10286
		CounterMultiTimer100Ns = 575735040,
		// Token: 0x0400282F RID: 10287
		CounterMultiTimer100NsInverse = 592512256,
		// Token: 0x04002830 RID: 10288
		CounterMultiBase = 1107494144,
		// Token: 0x04002831 RID: 10289
		CounterDelta32 = 4195328,
		// Token: 0x04002832 RID: 10290
		CounterDelta64 = 4195584
	}
}
