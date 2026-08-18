using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.Diagnostics
{
	// Token: 0x0200076D RID: 1901
	internal class CounterDefinitionSample
	{
		// Token: 0x06003A98 RID: 15000 RVA: 0x000F9444 File Offset: 0x000F8444
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

		// Token: 0x06003A99 RID: 15001 RVA: 0x000F94AC File Offset: 0x000F84AC
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

		// Token: 0x06003A9A RID: 15002 RVA: 0x000F9500 File Offset: 0x000F8500
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

		// Token: 0x06003A9B RID: 15003 RVA: 0x000F9604 File Offset: 0x000F8604
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

		// Token: 0x06003A9C RID: 15004 RVA: 0x000F972C File Offset: 0x000F872C
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

		// Token: 0x06003A9D RID: 15005 RVA: 0x000F97A0 File Offset: 0x000F87A0
		internal void SetInstanceValue(int index, IntPtr dataRef)
		{
			long num = this.ReadValue(dataRef);
			this.instanceValues[index] = num;
		}

		// Token: 0x04003354 RID: 13140
		internal readonly int NameIndex;

		// Token: 0x04003355 RID: 13141
		internal readonly int CounterType;

		// Token: 0x04003356 RID: 13142
		internal CounterDefinitionSample BaseCounterDefinitionSample;

		// Token: 0x04003357 RID: 13143
		private readonly int size;

		// Token: 0x04003358 RID: 13144
		private readonly int offset;

		// Token: 0x04003359 RID: 13145
		private long[] instanceValues;

		// Token: 0x0400335A RID: 13146
		private CategorySample categorySample;
	}
}
