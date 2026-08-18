using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	// Token: 0x02000743 RID: 1859
	[TypeConverter("System.Diagnostics.Design.CounterCreationDataConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class CounterCreationData
	{
		// Token: 0x060038BA RID: 14522 RVA: 0x000EF750 File Offset: 0x000EE750
		public CounterCreationData()
		{
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000EF779 File Offset: 0x000EE779
		public CounterCreationData(string counterName, string counterHelp, PerformanceCounterType counterType)
		{
			this.CounterType = counterType;
			this.CounterName = counterName;
			this.CounterHelp = counterHelp;
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x060038BC RID: 14524 RVA: 0x000EF7B7 File Offset: 0x000EE7B7
		// (set) Token: 0x060038BD RID: 14525 RVA: 0x000EF7BF File Offset: 0x000EE7BF
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

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060038BE RID: 14526 RVA: 0x000EF7F5 File Offset: 0x000EE7F5
		// (set) Token: 0x060038BF RID: 14527 RVA: 0x000EF7FD File Offset: 0x000EE7FD
		[DefaultValue("")]
		[MonitoringDescription("CounterName")]
		[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x000EF80C File Offset: 0x000EE80C
		// (set) Token: 0x060038C1 RID: 14529 RVA: 0x000EF814 File Offset: 0x000EE814
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

		// Token: 0x04003272 RID: 12914
		private PerformanceCounterType counterType = PerformanceCounterType.NumberOfItems32;

		// Token: 0x04003273 RID: 12915
		private string counterName = string.Empty;

		// Token: 0x04003274 RID: 12916
		private string counterHelp = string.Empty;
	}
}
