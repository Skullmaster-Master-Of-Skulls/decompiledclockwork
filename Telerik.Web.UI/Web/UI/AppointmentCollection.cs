using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020012CA RID: 4810
	public class AppointmentCollection : StateManagedCollection, IEnumerable<Appointment>, IEnumerable
	{
		// Token: 0x17004161 RID: 16737
		public Appointment this[int index]
		{
			get
			{
				return (Appointment)this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		// Token: 0x17004162 RID: 16738
		// (get) Token: 0x0600CA26 RID: 51750 RVA: 0x002D1C2A File Offset: 0x002CFE2A
		// (set) Token: 0x0600CA27 RID: 51751 RVA: 0x002D1C32 File Offset: 0x002CFE32
		internal IAppointmentFactory AppointmentFactory { get; set; }

		// Token: 0x0600CA28 RID: 51752 RVA: 0x002D1C3B File Offset: 0x002CFE3B
		public AppointmentCollection()
		{
			this._list = this;
		}

		// Token: 0x0600CA29 RID: 51753 RVA: 0x002D1C4A File Offset: 0x002CFE4A
		internal AppointmentCollection(IAppointmentFactory appointmentFactory) : this()
		{
			this.AppointmentFactory = appointmentFactory;
		}

		// Token: 0x0600CA2A RID: 51754 RVA: 0x002D1C59 File Offset: 0x002CFE59
		public AppointmentCollection(IEnumerable<Appointment> appointments) : this()
		{
			this.AddRange(appointments);
		}

		// Token: 0x0600CA2B RID: 51755 RVA: 0x002D1C68 File Offset: 0x002CFE68
		public bool Contains(Appointment appointment)
		{
			return this._list.Contains(appointment);
		}

		// Token: 0x0600CA2C RID: 51756 RVA: 0x002D1C76 File Offset: 0x002CFE76
		public void CopyTo(Appointment[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x0600CA2D RID: 51757 RVA: 0x002D1C85 File Offset: 0x002CFE85
		public int IndexOf(Appointment appointment)
		{
			return this._list.IndexOf(appointment);
		}

		// Token: 0x0600CA2E RID: 51758 RVA: 0x002D1C94 File Offset: 0x002CFE94
		public Appointment FindByID(object id)
		{
			foreach (Appointment appointment in this)
			{
				if (appointment.ID != null && appointment.ID.Equals(id))
				{
					return appointment;
				}
			}
			return null;
		}

		// Token: 0x0600CA2F RID: 51759 RVA: 0x002D1CF4 File Offset: 0x002CFEF4
		public IList<Appointment> FindByRecurrenceParentID(object parentId)
		{
			List<Appointment> list = new List<Appointment>();
			foreach (Appointment appointment in this)
			{
				if (appointment.RecurrenceParentID != null && appointment.RecurrenceParentID.Equals(parentId))
				{
					list.Add(appointment);
				}
			}
			return list;
		}

		// Token: 0x0600CA30 RID: 51760 RVA: 0x002D1D5C File Offset: 0x002CFF5C
		public IList<Appointment> FindByRecurrenceParentID(object parentId, RecurrenceState state)
		{
			List<Appointment> list = new List<Appointment>();
			foreach (Appointment appointment in this)
			{
				if (appointment.RecurrenceParentID != null && appointment.RecurrenceParentID.Equals(parentId) && appointment.RecurrenceState == state)
				{
					list.Add(appointment);
				}
			}
			return list;
		}

		// Token: 0x0600CA31 RID: 51761 RVA: 0x002D1DCC File Offset: 0x002CFFCC
		public IList<Appointment> GetAppointmentsStartingInRange(DateTime rangeStart, DateTime rangeEnd)
		{
			List<Appointment> list = new List<Appointment>();
			foreach (Appointment appointment in this)
			{
				if (appointment.Start.CompareTo(rangeStart) >= 0 && appointment.Start.CompareTo(rangeEnd) < 0)
				{
					list.Add(appointment);
				}
			}
			return list;
		}

		// Token: 0x0600CA32 RID: 51762 RVA: 0x002D1E40 File Offset: 0x002D0040
		public IList<Appointment> GetAppointmentsInRange(DateTime rangeStart, DateTime rangeEnd)
		{
			List<Appointment> list = new List<Appointment>();
			foreach (Appointment appointment in this)
			{
				if (appointment.Overlaps(rangeStart, rangeEnd))
				{
					list.Add(appointment);
				}
			}
			return list;
		}

		// Token: 0x0600CA33 RID: 51763 RVA: 0x002D1E9C File Offset: 0x002D009C
		public IList<Appointment> GetAppointmentsEnclosingRange(DateTime rangeStart, DateTime rangeEnd)
		{
			List<Appointment> list = new List<Appointment>();
			foreach (Appointment appointment in this)
			{
				if (AppointmentCollection.RangeIsInsideAppointment(rangeStart, rangeEnd, appointment))
				{
					list.Add(appointment);
				}
			}
			return list;
		}

		// Token: 0x0600CA34 RID: 51764 RVA: 0x002D1EF8 File Offset: 0x002D00F8
		public Appointment[] ToArray()
		{
			List<Appointment> list = new List<Appointment>();
			foreach (Appointment item in this)
			{
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x0600CA35 RID: 51765 RVA: 0x002D2094 File Offset: 0x002D0294
		public new IEnumerator<Appointment> GetEnumerator()
		{
			foreach (object obj in this._list)
			{
				Appointment appointment = (Appointment)obj;
				yield return appointment;
			}
			yield break;
		}

		// Token: 0x0600CA36 RID: 51766 RVA: 0x002D20B0 File Offset: 0x002D02B0
		internal void Add(Appointment apt)
		{
			this._list.Add(apt);
		}

		// Token: 0x0600CA37 RID: 51767 RVA: 0x002D20C0 File Offset: 0x002D02C0
		internal void AddRange(IEnumerable<Appointment> array)
		{
			foreach (Appointment apt in array)
			{
				this.Add(apt);
			}
		}

		// Token: 0x0600CA38 RID: 51768 RVA: 0x002D2108 File Offset: 0x002D0308
		internal void Insert(int index, Appointment apt)
		{
			this._list.Insert(index, apt);
		}

		// Token: 0x0600CA39 RID: 51769 RVA: 0x002D2117 File Offset: 0x002D0317
		internal void Remove(Appointment apt)
		{
			this._list.Remove(apt);
		}

		// Token: 0x0600CA3A RID: 51770 RVA: 0x002D2128 File Offset: 0x002D0328
		internal void Remove(IEnumerable<Appointment> subset)
		{
			foreach (Appointment value in subset)
			{
				if (this._list.Contains(value))
				{
					this._list.Remove(value);
				}
			}
		}

		// Token: 0x0600CA3B RID: 51771 RVA: 0x002D2184 File Offset: 0x002D0384
		internal void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x0600CA3C RID: 51772 RVA: 0x002D2194 File Offset: 0x002D0394
		internal void Sort(IComparer<Appointment> comparer)
		{
			List<Appointment> list = new List<Appointment>(this);
			list.Sort(comparer);
			base.Clear();
			this.AddRange(list);
		}

		// Token: 0x0600CA3D RID: 51773 RVA: 0x002D21BC File Offset: 0x002D03BC
		internal static bool RangeIsInsideAppointment(DateTime start, DateTime end, Appointment appointment)
		{
			return appointment.Start <= start && appointment.End >= end;
		}

		// Token: 0x0600CA3E RID: 51774 RVA: 0x002D21DC File Offset: 0x002D03DC
		protected override Type[] GetKnownTypes()
		{
			return new Type[]
			{
				typeof(Appointment)
			};
		}

		// Token: 0x0600CA3F RID: 51775 RVA: 0x002D21FE File Offset: 0x002D03FE
		protected override object CreateKnownType(int index)
		{
			if (this.AppointmentFactory != null)
			{
				return this.AppointmentFactory.CreateAppointment();
			}
			return new Appointment();
		}

		// Token: 0x0600CA40 RID: 51776 RVA: 0x002D221C File Offset: 0x002D041C
		protected override void SetDirtyObject(object o)
		{
			Appointment appointment = o as Appointment;
			if (appointment != null)
			{
				appointment.SetDirty();
			}
		}

		// Token: 0x04003507 RID: 13575
		private readonly IList _list;
	}
}
