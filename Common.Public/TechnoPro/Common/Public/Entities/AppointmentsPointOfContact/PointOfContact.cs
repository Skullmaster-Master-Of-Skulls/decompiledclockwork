using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.AppointmentsPointOfContact
{
	// Token: 0x02000554 RID: 1364
	public class PointOfContact : BaseExtendedAppointment
	{
		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x00030F54 File Offset: 0x0002F154
		// (set) Token: 0x06002BEC RID: 11244 RVA: 0x00030F5C File Offset: 0x0002F15C
		public Attendee Student { get; set; }

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x00030F65 File Offset: 0x0002F165
		// (set) Token: 0x06002BEE RID: 11246 RVA: 0x00030F6D File Offset: 0x0002F16D
		public Attendee Staff { get; set; }

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x00030F76 File Offset: 0x0002F176
		// (set) Token: 0x06002BF0 RID: 11248 RVA: 0x00030F7E File Offset: 0x0002F17E
		public List<DynamicData> SessionNotesData { get; set; }

		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x00030F88 File Offset: 0x0002F188
		// (set) Token: 0x06002BF2 RID: 11250 RVA: 0x00030FE8 File Offset: 0x0002F1E8
		public ePointOfContactContext PocContext
		{
			get
			{
				bool flag = base.OverrideColour == null || !Enum.IsDefined(typeof(ePointOfContactContext), base.OverrideColour.Value);
				ePointOfContactContext result;
				if (flag)
				{
					result = ePointOfContactContext.Normal;
				}
				else
				{
					result = (ePointOfContactContext)base.OverrideColour.Value;
				}
				return result;
			}
			set
			{
				bool flag = value == ePointOfContactContext.Normal;
				if (flag)
				{
					base.OverrideColour = null;
				}
				else
				{
					base.OverrideColour = new int?((int)value);
				}
			}
		}

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x00031020 File Offset: 0x0002F220
		// (set) Token: 0x06002BF4 RID: 11252 RVA: 0x00031040 File Offset: 0x0002F240
		public override DateTime StartDateTime
		{
			get
			{
				return base.StartDateTime.Date;
			}
			set
			{
				base.StartDateTime = value.Date;
				base.EndDateTime = value.Date.AddHours(1.0);
			}
		}

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x0003107C File Offset: 0x0002F27C
		public override DateTime EndDateTime
		{
			get
			{
				return this.StartDateTime.AddHours(1.0);
			}
		}
	}
}
