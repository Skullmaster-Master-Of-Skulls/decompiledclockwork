using System;
using System.Collections;

namespace ClockWorkWebAPI
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class AppointmentCollection : CollectionBase
	{
		// Token: 0x17000032 RID: 50
		public Appointment this[int index]
		{
			get
			{
				return (Appointment)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004C40 File Offset: 0x00002E40
		public int Add(Appointment appointment)
		{
			return base.List.Add(appointment);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004C60 File Offset: 0x00002E60
		public int Add(DateTime sdt, DateTime edt)
		{
			Appointment appointment = new Appointment(sdt, edt);
			bool flag = false;
			foreach (object obj in base.List)
			{
				Appointment appointment2 = (Appointment)obj;
				bool flag2 = appointment2.StartDateTime.CompareTo(sdt) == 0 && appointment2.EndDateTime.CompareTo(edt) == 0;
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			bool flag3 = !flag;
			int result;
			if (flag3)
			{
				result = this.Add(appointment);
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000365E File Offset: 0x0000185E
		public void Insert(int index, Appointment appointment)
		{
			base.List.Insert(index, appointment);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000366F File Offset: 0x0000186F
		public void Remove(Appointment appointment)
		{
			base.List.Remove(appointment);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004D14 File Offset: 0x00002F14
		public bool Contains(Appointment appointment)
		{
			return base.List.Contains(appointment);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004D34 File Offset: 0x00002F34
		public bool Contains(DateTime sdt, DateTime edt)
		{
			foreach (object obj in base.List)
			{
				Appointment appointment = (Appointment)obj;
				bool flag = appointment.StartDateTime.Year == sdt.Year && appointment.StartDateTime.Month == sdt.Month && appointment.StartDateTime.Day == sdt.Day && appointment.StartDateTime.Hour == sdt.Hour && appointment.StartDateTime.Minute == sdt.Minute && appointment.EndDateTime.Year == edt.Year && appointment.EndDateTime.Month == edt.Month && appointment.EndDateTime.Day == edt.Day && appointment.EndDateTime.Hour == edt.Hour && appointment.EndDateTime.Minute == edt.Minute;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}
	}
}
