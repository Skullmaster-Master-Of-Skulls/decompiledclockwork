using System;
using System.Collections.Generic;

namespace ClockWorkWebAPI.TestBooking
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	public class AppType
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000100C4 File Offset: 0x0000E2C4
		public AccommodationCollection ActiveAccommodations
		{
			get
			{
				return this.activeAccommodations;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000247 RID: 583 RVA: 0x000100DC File Offset: 0x0000E2DC
		public AccommodationCollection InactiveAccommodations
		{
			get
			{
				return this.inactiveAccommodations;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000248 RID: 584 RVA: 0x000100F4 File Offset: 0x0000E2F4
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0001010C File Offset: 0x0000E30C
		public int AppTypeId
		{
			get
			{
				return this.appTypeId;
			}
			set
			{
				this.appTypeId = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00010118 File Offset: 0x0000E318
		// (set) Token: 0x0600024B RID: 587 RVA: 0x00010130 File Offset: 0x0000E330
		public int AvailabilityGroupId
		{
			get
			{
				return this.availabilityGroupId;
			}
			set
			{
				this.availabilityGroupId = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0001013C File Offset: 0x0000E33C
		public List<TestType> AvailableTestTypes
		{
			get
			{
				return this.availableTestTypes;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00010154 File Offset: 0x0000E354
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0001016C File Offset: 0x0000E36C
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00010178 File Offset: 0x0000E378
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00010190 File Offset: 0x0000E390
		public TimeSpan ActiveStartMonthDay
		{
			get
			{
				return this.activeStartMonthDay;
			}
			set
			{
				this.activeStartMonthDay = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0001019C File Offset: 0x0000E39C
		// (set) Token: 0x06000252 RID: 594 RVA: 0x000101B4 File Offset: 0x0000E3B4
		public TimeSpan ActiveEndMonthDay
		{
			get
			{
				return this.activeEndMonthDay;
			}
			set
			{
				this.activeEndMonthDay = value;
			}
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000101C0 File Offset: 0x0000E3C0
		public AppType()
		{
			this.availableTestTypes = new List<TestType>();
			this.activeAccommodations = new AccommodationCollection();
			this.inactiveAccommodations = new AccommodationCollection();
			DateTime d = new DateTime(AppType.basesd.Year, 11, 13);
			DateTime d2 = new DateTime(AppType.basesd.Year, 12, 15);
			this.activeStartMonthDay = d - AppType.basesd;
			this.activeEndMonthDay = d2 - AppType.basesd;
		}

		// Token: 0x04000144 RID: 324
		private int appTypeId;

		// Token: 0x04000145 RID: 325
		private string description;

		// Token: 0x04000146 RID: 326
		private TimeSpan activeStartMonthDay;

		// Token: 0x04000147 RID: 327
		private TimeSpan activeEndMonthDay;

		// Token: 0x04000148 RID: 328
		private int availabilityGroupId;

		// Token: 0x04000149 RID: 329
		private List<TestType> availableTestTypes;

		// Token: 0x0400014A RID: 330
		private AccommodationCollection activeAccommodations;

		// Token: 0x0400014B RID: 331
		private AccommodationCollection inactiveAccommodations;

		// Token: 0x0400014C RID: 332
		private static DateTime basesd = new DateTime(2000, 1, 1);
	}
}
