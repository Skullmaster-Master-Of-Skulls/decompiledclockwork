using System;
using NLog.Config;

namespace NLog.Time
{
	// Token: 0x02000189 RID: 393
	[NLogConfigurationItem]
	public abstract class TimeSource
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E8A RID: 3722
		public abstract DateTime Time { get; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00023940 File Offset: 0x00021B40
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x00023947 File Offset: 0x00021B47
		public static TimeSource Current
		{
			get
			{
				return TimeSource.currentSource;
			}
			set
			{
				TimeSource.currentSource = value;
			}
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00023950 File Offset: 0x00021B50
		public override string ToString()
		{
			TimeSourceAttribute timeSourceAttribute = (TimeSourceAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(TimeSourceAttribute));
			if (timeSourceAttribute != null)
			{
				return timeSourceAttribute.Name + " (time source)";
			}
			return base.GetType().Name;
		}

		// Token: 0x06000E8E RID: 3726
		public abstract DateTime FromSystemTime(DateTime systemTime);

		// Token: 0x04000429 RID: 1065
		private static TimeSource currentSource = new FastLocalTimeSource();
	}
}
