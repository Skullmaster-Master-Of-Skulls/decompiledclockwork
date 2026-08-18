using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports
{
	// Token: 0x020003A2 RID: 930
	public class BareBonesPerDateEntry : BusinessBase<int>
	{
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x000208E8 File Offset: 0x0001EAE8
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00020900 File Offset: 0x0001EB00
		// (set) Token: 0x06001C5A RID: 7258 RVA: 0x00020908 File Offset: 0x0001EB08
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00020911 File Offset: 0x0001EB11
		// (set) Token: 0x06001C5C RID: 7260 RVA: 0x00020919 File Offset: 0x0001EB19
		public string Title { get; set; }

		// Token: 0x06001C5D RID: 7261 RVA: 0x00020924 File Offset: 0x0001EB24
		public override bool Equals(object obj)
		{
			BareBonesAppointment bareBonesAppointment = obj as BareBonesAppointment;
			bool flag = bareBonesAppointment == null;
			bool result;
			if (flag)
			{
				result = base.Equals(obj);
			}
			else
			{
				BareBonesAppointment bareBonesAppointment2 = bareBonesAppointment;
				result = (bareBonesAppointment2.AppointmentId == this.AppointmentId);
			}
			return result;
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x00020960 File Offset: 0x0001EB60
		public override int GetHashCode()
		{
			return this.Id;
		}
	}
}
