using System;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent
{
	// Token: 0x02000569 RID: 1385
	public class TimeIntervalAttribute : Attribute
	{
		// Token: 0x06002C97 RID: 11415 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public TimeIntervalAttribute()
		{
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x00031A46 File Offset: 0x0002FC46
		public TimeIntervalAttribute(bool ignoreTimeComponent)
		{
			this.IgnoreTimeComponent = ignoreTimeComponent;
		}

		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06002C99 RID: 11417 RVA: 0x00031A58 File Offset: 0x0002FC58
		// (set) Token: 0x06002C9A RID: 11418 RVA: 0x00031A60 File Offset: 0x0002FC60
		public bool IgnoreTimeComponent { get; set; }
	}
}
