using System;

namespace System.Diagnostics
{
	// Token: 0x020004D9 RID: 1241
	public class InstanceData
	{
		// Token: 0x06002EE7 RID: 12007 RVA: 0x000D2D55 File Offset: 0x000D0F55
		public InstanceData(string instanceName, CounterSample sample)
		{
			this.instanceName = instanceName;
			this.sample = sample;
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x000D2D6B File Offset: 0x000D0F6B
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x000D2D73 File Offset: 0x000D0F73
		public CounterSample Sample
		{
			get
			{
				return this.sample;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x000D2D7B File Offset: 0x000D0F7B
		public long RawValue
		{
			get
			{
				return this.sample.RawValue;
			}
		}

		// Token: 0x040027A4 RID: 10148
		private string instanceName;

		// Token: 0x040027A5 RID: 10149
		private CounterSample sample;
	}
}
