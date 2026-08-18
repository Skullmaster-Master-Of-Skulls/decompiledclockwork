using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x020004C0 RID: 1216
	[TypeConverter("System.Diagnostics.Design.CounterCreationDataConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class CounterCreationData
	{
		// Token: 0x06002D71 RID: 11633 RVA: 0x000CCA30 File Offset: 0x000CAC30
		public CounterCreationData()
		{
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000CCA59 File Offset: 0x000CAC59
		public CounterCreationData(string counterName, string counterHelp, PerformanceCounterType counterType)
		{
			this.CounterType = counterType;
			this.CounterName = counterName;
			this.CounterHelp = counterHelp;
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x000CCA97 File Offset: 0x000CAC97
		// (set) Token: 0x06002D74 RID: 11636 RVA: 0x000CCA9F File Offset: 0x000CAC9F
		[DefaultValue(PerformanceCounterType.NumberOfItems32)]
		[MonitoringDescription("CounterType")]
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
			set
			{
				if (!Enum.IsDefined(typeof(PerformanceCounterType), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(PerformanceCounterType));
				}
				this.counterType = value;
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x000CCAD5 File Offset: 0x000CACD5
		// (set) Token: 0x06002D76 RID: 11638 RVA: 0x000CCADD File Offset: 0x000CACDD
		[DefaultValue("")]
		[MonitoringDescription("CounterName")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
			set
			{
				PerformanceCounterCategory.CheckValidCounter(value);
				this.counterName = value;
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x000CCAEC File Offset: 0x000CACEC
		// (set) Token: 0x06002D78 RID: 11640 RVA: 0x000CCAF4 File Offset: 0x000CACF4
		[DefaultValue("")]
		[MonitoringDescription("CounterHelp")]
		public string CounterHelp
		{
			get
			{
				return this.counterHelp;
			}
			set
			{
				PerformanceCounterCategory.CheckValidHelp(value);
				this.counterHelp = value;
			}
		}

		// Token: 0x04002729 RID: 10025
		private PerformanceCounterType counterType = PerformanceCounterType.NumberOfItems32;

		// Token: 0x0400272A RID: 10026
		private string counterName = string.Empty;

		// Token: 0x0400272B RID: 10027
		private string counterHelp = string.Empty;
	}
}
