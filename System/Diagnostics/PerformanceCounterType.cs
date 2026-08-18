using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x02000774 RID: 1908
	[TypeConverter(typeof(AlphabeticalEnumConverter))]
	public enum PerformanceCounterType
	{
		// Token: 0x0400336B RID: 13163
		NumberOfItems32 = 65536,
		// Token: 0x0400336C RID: 13164
		NumberOfItems64 = 65792,
		// Token: 0x0400336D RID: 13165
		NumberOfItemsHEX32 = 0,
		// Token: 0x0400336E RID: 13166
		NumberOfItemsHEX64 = 256,
		// Token: 0x0400336F RID: 13167
		RateOfCountsPerSecond32 = 272696320,
		// Token: 0x04003370 RID: 13168
		RateOfCountsPerSecond64 = 272696576,
		// Token: 0x04003371 RID: 13169
		CountPerTimeInterval32 = 4523008,
		// Token: 0x04003372 RID: 13170
		CountPerTimeInterval64 = 4523264,
		// Token: 0x04003373 RID: 13171
		RawFraction = 537003008,
		// Token: 0x04003374 RID: 13172
		RawBase = 1073939459,
		// Token: 0x04003375 RID: 13173
		AverageTimer32 = 805438464,
		// Token: 0x04003376 RID: 13174
		AverageBase = 1073939458,
		// Token: 0x04003377 RID: 13175
		AverageCount64 = 1073874176,
		// Token: 0x04003378 RID: 13176
		SampleFraction = 549585920,
		// Token: 0x04003379 RID: 13177
		SampleCounter = 4260864,
		// Token: 0x0400337A RID: 13178
		SampleBase = 1073939457,
		// Token: 0x0400337B RID: 13179
		CounterTimer = 541132032,
		// Token: 0x0400337C RID: 13180
		CounterTimerInverse = 557909248,
		// Token: 0x0400337D RID: 13181
		Timer100Ns = 542180608,
		// Token: 0x0400337E RID: 13182
		Timer100NsInverse = 558957824,
		// Token: 0x0400337F RID: 13183
		ElapsedTime = 807666944,
		// Token: 0x04003380 RID: 13184
		CounterMultiTimer = 574686464,
		// Token: 0x04003381 RID: 13185
		CounterMultiTimerInverse = 591463680,
		// Token: 0x04003382 RID: 13186
		CounterMultiTimer100Ns = 575735040,
		// Token: 0x04003383 RID: 13187
		CounterMultiTimer100NsInverse = 592512256,
		// Token: 0x04003384 RID: 13188
		CounterMultiBase = 1107494144,
		// Token: 0x04003385 RID: 13189
		CounterDelta32 = 4195328,
		// Token: 0x04003386 RID: 13190
		CounterDelta64 = 4195584
	}
}
