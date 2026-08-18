using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports
{
	// Token: 0x020003A1 RID: 929
	public class BareBonesAppointment : BusinessBase<int>
	{
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x00020848 File Offset: 0x0001EA48
		// (set) Token: 0x06001C4D RID: 7245 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x00020860 File Offset: 0x0001EA60
		// (set) Token: 0x06001C4F RID: 7247 RVA: 0x00020868 File Offset: 0x0001EA68
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x00020871 File Offset: 0x0001EA71
		// (set) Token: 0x06001C51 RID: 7249 RVA: 0x00020879 File Offset: 0x0001EA79
		public int AppTypeId { get; set; }

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x00020882 File Offset: 0x0001EA82
		// (set) Token: 0x06001C53 RID: 7251 RVA: 0x0002088A File Offset: 0x0001EA8A
		public string AppointmentType { get; set; }

		// Token: 0x06001C54 RID: 7252 RVA: 0x00020894 File Offset: 0x0001EA94
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

		// Token: 0x06001C55 RID: 7253 RVA: 0x000208D0 File Offset: 0x0001EAD0
		public override int GetHashCode()
		{
			return this.Id;
		}
	}
}
