using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000839 RID: 2105
	internal abstract class SchedulerTimeSlot : ISchedulerTimeSlot
	{
		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x06004DF9 RID: 19961
		public abstract string Index { get; }

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x06004DFA RID: 19962 RVA: 0x000F4AD2 File Offset: 0x000F2CD2
		// (set) Token: 0x06004DFB RID: 19963 RVA: 0x000F4ADA File Offset: 0x000F2CDA
		public IList<Appointment> Appointments
		{
			get
			{
				return this._appointments;
			}
			protected set
			{
				this._appointments = value;
			}
		}

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x06004DFC RID: 19964 RVA: 0x000F4AE3 File Offset: 0x000F2CE3
		// (set) Token: 0x06004DFD RID: 19965 RVA: 0x000F4AEB File Offset: 0x000F2CEB
		public WebControl Control
		{
			get
			{
				return this._control;
			}
			set
			{
				this._control = value;
			}
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x06004DFE RID: 19966 RVA: 0x000F4AF4 File Offset: 0x000F2CF4
		// (set) Token: 0x06004DFF RID: 19967 RVA: 0x000F4AFC File Offset: 0x000F2CFC
		public DateTime Start
		{
			get
			{
				return this._start;
			}
			protected set
			{
				this._start = value;
			}
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x06004E00 RID: 19968 RVA: 0x000F4B05 File Offset: 0x000F2D05
		// (set) Token: 0x06004E01 RID: 19969 RVA: 0x000F4B0D File Offset: 0x000F2D0D
		public DateTime End
		{
			get
			{
				return this._end;
			}
			protected set
			{
				this._end = value;
			}
		}

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x06004E02 RID: 19970 RVA: 0x000F4B16 File Offset: 0x000F2D16
		public TimeSpan Duration
		{
			get
			{
				return this.End - this.Start;
			}
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x06004E03 RID: 19971 RVA: 0x000F4B29 File Offset: 0x000F2D29
		// (set) Token: 0x06004E04 RID: 19972 RVA: 0x000F4B31 File Offset: 0x000F2D31
		public bool IsWorkHour
		{
			get
			{
				return this._isWorkHour;
			}
			set
			{
				this._isWorkHour = value;
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x06004E05 RID: 19973 RVA: 0x000F4B3A File Offset: 0x000F2D3A
		// (set) Token: 0x06004E06 RID: 19974 RVA: 0x000F4B42 File Offset: 0x000F2D42
		public DayOfWeek DayOfWeek
		{
			get
			{
				return this._dayOfWeek;
			}
			set
			{
				this._dayOfWeek = value;
			}
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x06004E07 RID: 19975 RVA: 0x000F4B4B File Offset: 0x000F2D4B
		// (set) Token: 0x06004E08 RID: 19976 RVA: 0x000F4B53 File Offset: 0x000F2D53
		public SchedulerFormContainer FormContainer
		{
			get
			{
				return this._formContainer;
			}
			set
			{
				this._formContainer = value;
			}
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x000F4B5C File Offset: 0x000F2D5C
		// (set) Token: 0x06004E0A RID: 19978 RVA: 0x000F4B64 File Offset: 0x000F2D64
		public string CssClass
		{
			get
			{
				return this._cssClass;
			}
			set
			{
				this._cssClass = value;
			}
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x06004E0B RID: 19979 RVA: 0x000F4B6D File Offset: 0x000F2D6D
		// (set) Token: 0x06004E0C RID: 19980 RVA: 0x000F4B75 File Offset: 0x000F2D75
		public ISchedulerModel Owner
		{
			get
			{
				return this._owner;
			}
			protected set
			{
				this._owner = value;
			}
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x06004E0D RID: 19981 RVA: 0x000F4B7E File Offset: 0x000F2D7E
		// (set) Token: 0x06004E0E RID: 19982 RVA: 0x000F4B86 File Offset: 0x000F2D86
		public Resource Resource
		{
			get
			{
				return this._resource;
			}
			set
			{
				this._resource = value;
			}
		}

		// Token: 0x06004E0F RID: 19983 RVA: 0x000F4B8F File Offset: 0x000F2D8F
		protected SchedulerTimeSlot(IEnumerable<Appointment> appointmentsList, ISchedulerModel ownerModel, DateTime start, DateTime end)
		{
			this.Appointments = new List<Appointment>(appointmentsList).AsReadOnly();
			this.Owner = ownerModel;
			this.Start = start;
			this.End = end;
			this.CssClass = string.Empty;
		}

		// Token: 0x06004E10 RID: 19984 RVA: 0x000F4BC9 File Offset: 0x000F2DC9
		protected SchedulerTimeSlot()
		{
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x000F4BD4 File Offset: 0x000F2DD4
		internal bool ContainsAppointment(Appointment appointment)
		{
			foreach (Appointment appointment2 in this.Appointments)
			{
				if (appointment2 == appointment || appointment2.ID.Equals(appointment.ID))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400136C RID: 4972
		private IList<Appointment> _appointments;

		// Token: 0x0400136D RID: 4973
		private WebControl _control;

		// Token: 0x0400136E RID: 4974
		private bool _isWorkHour;

		// Token: 0x0400136F RID: 4975
		private DayOfWeek _dayOfWeek;

		// Token: 0x04001370 RID: 4976
		private SchedulerFormContainer _formContainer;

		// Token: 0x04001371 RID: 4977
		private ISchedulerModel _owner;

		// Token: 0x04001372 RID: 4978
		private DateTime _start;

		// Token: 0x04001373 RID: 4979
		private DateTime _end;

		// Token: 0x04001374 RID: 4980
		private string _cssClass;

		// Token: 0x04001375 RID: 4981
		private Resource _resource;
	}
}
