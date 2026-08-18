using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x020004E7 RID: 1255
	internal class CounterDefinitionSample
	{
		// Token: 0x06002F7B RID: 12155 RVA: 0x000D6CA8 File Offset: 0x000D4EA8
		internal CounterDefinitionSample(NativeMethods.PERF_COUNTER_DEFINITION perfCounter, CategorySample categorySample, int instanceNumber)
		{
			this.NameIndex = perfCounter.CounterNameTitleIndex;
			this.CounterType = perfCounter.CounterType;
			this.offset = perfCounter.CounterOffset;
			this.size = perfCounter.CounterSize;
			if (instanceNumber == -1)
			{
				this.instanceValues = new long[1];
			}
			else
			{
				this.instanceValues = new long[instanceNumber];
			}
			this.categorySample = categorySample;
		}

		// Token: 0x06002F7C RID: 12156 RVA: 0x000D6D10 File Offset: 0x000D4F10
		private long ReadValue(IntPtr pointer)
		{
			if (this.size == 4)
			{
				return (long)((ulong)Marshal.ReadInt32((IntPtr)((long)pointer + (long)this.offset)));
			}
			if (this.size == 8)
			{
				return Marshal.ReadInt64((IntPtr)((long)pointer + (long)this.offset));
			}
			return -1L;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000D6D64 File Offset: 0x000D4F64
		internal CounterSample GetInstanceValue(string instanceName)
		{
			if (!this.categorySample.InstanceNameTable.ContainsKey(instanceName))
			{
				if (instanceName.Length > 127)
				{
					instanceName = instanceName.Substring(0, 127);
				}
				if (!this.categorySample.InstanceNameTable.ContainsKey(instanceName))
				{
					throw new InvalidOperationException(SR.GetString("CantReadInstance", new object[]
					{
						instanceName
					}));
				}
			}
			int num = (int)this.categorySample.InstanceNameTable[instanceName];
			long rawValue = this.instanceValues[num];
			long baseValue = 0L;
			if (this.BaseCounterDefinitionSample != null)
			{
				CategorySample categorySample = this.BaseCounterDefinitionSample.categorySample;
				int num2 = (int)categorySample.InstanceNameTable[instanceName];
				baseValue = this.BaseCounterDefinitionSample.instanceValues[num2];
			}
			return new CounterSample(rawValue, baseValue, this.categorySample.CounterFrequency, this.categorySample.SystemFrequency, this.categorySample.TimeStamp, this.categorySample.TimeStamp100nSec, (PerformanceCounterType)this.CounterType, this.categorySample.CounterTimeStamp);
		}

		// Token: 0x06002F7E RID: 12158 RVA: 0x000D6E64 File Offset: 0x000D5064
		internal InstanceDataCollection ReadInstanceData(string counterName)
		{
			InstanceDataCollection instanceDataCollection = new InstanceDataCollection(counterName);
			string[] array = new string[this.categorySample.InstanceNameTable.Count];
			this.categorySample.InstanceNameTable.Keys.CopyTo(array, 0);
			int[] array2 = new int[this.categorySample.InstanceNameTable.Count];
			this.categorySample.InstanceNameTable.Values.CopyTo(array2, 0);
			for (int i = 0; i < array.Length; i++)
			{
				long baseValue = 0L;
				if (this.BaseCounterDefinitionSample != null)
				{
					CategorySample categorySample = this.BaseCounterDefinitionSample.categorySample;
					int num = (int)categorySample.InstanceNameTable[array[i]];
					baseValue = this.BaseCounterDefinitionSample.instanceValues[num];
				}
				CounterSample sample = new CounterSample(this.instanceValues[array2[i]], baseValue, this.categorySample.CounterFrequency, this.categorySample.SystemFrequency, this.categorySample.TimeStamp, this.categorySample.TimeStamp100nSec, (PerformanceCounterType)this.CounterType, this.categorySample.CounterTimeStamp);
				instanceDataCollection.Add(array[i], new InstanceData(array[i], sample));
			}
			return instanceDataCollection;
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x000D6F8C File Offset: 0x000D518C
		internal CounterSample GetSingleValue()
		{
			long rawValue = this.instanceValues[0];
			long baseValue = 0L;
			if (this.BaseCounterDefinitionSample != null)
			{
				baseValue = this.BaseCounterDefinitionSample.instanceValues[0];
			}
			return new CounterSample(rawValue, baseValue, this.categorySample.CounterFrequency, this.categorySample.SystemFrequency, this.categorySample.TimeStamp, this.categorySample.TimeStamp100nSec, (PerformanceCounterType)this.CounterType, this.categorySample.CounterTimeStamp);
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x000D7000 File Offset: 0x000D5200
		internal void SetInstanceValue(int index, IntPtr dataRef)
		{
			long num = this.ReadValue(dataRef);
			this.instanceValues[index] = num;
		}

		// Token: 0x04002800 RID: 10240
		internal readonly int NameIndex;

		// Token: 0x04002801 RID: 10241
		internal readonly int CounterType;

		// Token: 0x04002802 RID: 10242
		internal CounterDefinitionSample BaseCounterDefinitionSample;

		// Token: 0x04002803 RID: 10243
		private readonly int size;

		// Token: 0x04002804 RID: 10244
		private readonly int offset;

		// Token: 0x04002805 RID: 10245
		private long[] instanceValues;

		// Token: 0x04002806 RID: 10246
		private CategorySample categorySample;
	}
}
