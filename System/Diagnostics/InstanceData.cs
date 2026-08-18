using System;

namespace System.Diagnostics
{
	// Token: 0x0200075F RID: 1887
	public class InstanceData
	{
		// Token: 0x06003A07 RID: 14855 RVA: 0x000F58C8 File Offset: 0x000F48C8
		public InstanceData(string instanceName, CounterSample sample)
		{
			this.instanceName = instanceName;
			this.sample = sample;
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000F58DE File Offset: 0x000F48DE
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06003A09 RID: 14857 RVA: 0x000F58E6 File Offset: 0x000F48E6
		public CounterSample Sample
		{
			get
			{
				return this.sample;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06003A0A RID: 14858 RVA: 0x000F58EE File Offset: 0x000F48EE
		public long RawValue
		{
			get
			{
				return this.sample.RawValue;
			}
		}

		// Token: 0x040032FC RID: 13052
		private string instanceName;

		// Token: 0x040032FD RID: 13053
		private CounterSample sample;
	}
}
