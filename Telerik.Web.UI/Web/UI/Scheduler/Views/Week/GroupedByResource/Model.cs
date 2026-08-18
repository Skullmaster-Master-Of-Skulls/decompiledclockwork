using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource
{
	// Token: 0x02001A4D RID: 6733
	internal class Model : ModelBase
	{
		// Token: 0x17004F4C RID: 20300
		// (get) Token: 0x06010555 RID: 66901 RVA: 0x003A5481 File Offset: 0x003A3681
		// (set) Token: 0x06010556 RID: 66902 RVA: 0x003A5489 File Offset: 0x003A3689
		public IList<Resource> Resources { get; protected set; }

		// Token: 0x17004F4D RID: 20301
		// (get) Token: 0x06010557 RID: 66903 RVA: 0x003A5492 File Offset: 0x003A3692
		// (set) Token: 0x06010558 RID: 66904 RVA: 0x003A549A File Offset: 0x003A369A
		public IList<Model> WeekModels { get; protected set; }

		// Token: 0x17004F4E RID: 20302
		// (get) Token: 0x06010559 RID: 66905 RVA: 0x003A54A3 File Offset: 0x003A36A3
		public string GroupingResourceName
		{
			get
			{
				return this._groupingResourceName;
			}
		}

		// Token: 0x0601055A RID: 66906 RVA: 0x003A54AB File Offset: 0x003A36AB
		public Model(IScheduler owner, string groupingResourceName) : base(owner)
		{
			this._groupingResourceName = groupingResourceName;
		}

		// Token: 0x0601055B RID: 66907 RVA: 0x003A54D8 File Offset: 0x003A36D8
		public override void DataBind(AppointmentCollection appointments)
		{
			this.Appointments.Clear();
			this.Resources = new List<Resource>(this.Owner.Resources.GetResourcesByType(this.GroupingResourceName));
			this.WeekModels = new List<Model>(this.Resources.Count);
			int num = 0;
			List<Appointment> list = new List<Appointment>(appointments);
			foreach (Resource resource in this.Resources)
			{
				TimeSlotFactory slotFactory = new TimeSlotFactory(num, resource);
				Model model = this.CreateModel(slotFactory);
				this.WeekModels.Add(model);
				num++;
				Resource filteringResource = resource;
				IList<Appointment> list2 = list.FindAll((Appointment apt) => apt.Resources.Contains(filteringResource));
				model.DataBind(new AppointmentCollection(list2));
				this.Appointments.AddRange(list2);
			}
		}

		// Token: 0x0601055C RID: 66908 RVA: 0x003A55D0 File Offset: 0x003A37D0
		protected virtual Model CreateModel(IWeekTimeSlotFactory slotFactory)
		{
			return new Model(this.Owner, slotFactory, this.Owner.WeekView.WorkDayStartTimeResolved, this.Owner.WeekView.WorkDayEndTimeResolved);
		}

		// Token: 0x0601055D RID: 66909 RVA: 0x003A5600 File Offset: 0x003A3800
		public override ISchedulerRenderer GetRenderer()
		{
			View view;
			if (this.Owner.WeekView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Renderer(view);
		}

		// Token: 0x0601055E RID: 66910 RVA: 0x003A5638 File Offset: 0x003A3838
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			string fullName = Assembly.GetExecutingAssembly().FullName;
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.Week.Model.js", fullName));
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource.Model.js", fullName));
			return list;
		}

		// Token: 0x0601055F RID: 66911 RVA: 0x003A567C File Offset: 0x003A387C
		public override ISchedulerTimeSlot GetSlotByIndex(string index)
		{
			string[] array = index.Split(new char[]
			{
				':'
			});
			int num;
			if (!int.TryParse(array[0], out num))
			{
				throw new InvalidOperationException("Cannot parse slot index.");
			}
			if (num > this.WeekModels.Count - 1)
			{
				throw new IndexOutOfRangeException("Resource index out of range: " + num);
			}
			string index2 = string.Join(":", array, 1, array.Length - 1);
			return this.WeekModels[num].GetSlotByIndex(index2);
		}

		// Token: 0x06010560 RID: 66912 RVA: 0x003A5700 File Offset: 0x003A3900
		public override ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment)
		{
			foreach (Model model in this.WeekModels)
			{
				ISchedulerTimeSlot appointmentSlot = model.GetAppointmentSlot(appointment);
				if (appointmentSlot != null)
				{
					return appointmentSlot;
				}
			}
			return null;
		}

		// Token: 0x06010561 RID: 66913 RVA: 0x003A5758 File Offset: 0x003A3958
		public override IList<ISchedulerTimeSlot> GetTimeSlots()
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			foreach (Model model in this.WeekModels)
			{
				list.AddRange(model.GetTimeSlots());
			}
			return list;
		}

		// Token: 0x06010562 RID: 66914 RVA: 0x003A57B4 File Offset: 0x003A39B4
		public override void HandleMove(Appointment appointment, ISchedulerTimeSlot sourceSlot, ISchedulerTimeSlot targetSlot, bool editSeries)
		{
			TimeSlot timeSlot = (TimeSlot)sourceSlot;
			TimeSlot timeSlot2 = (TimeSlot)targetSlot;
			int modelIndex = timeSlot.ModelIndex;
			int modelIndex2 = timeSlot2.ModelIndex;
			Resource oldResource = this.Resources[modelIndex];
			Resource newResource = this.Resources[modelIndex2];
			bool isAllDaySlot = timeSlot2.IsAllDaySlot;
			bool isAllDaySlot2 = timeSlot.IsAllDaySlot;
			bool flag = isAllDaySlot ? isAllDaySlot2 : (!isAllDaySlot2);
			ResourceUpdateInfo resourceUpdateInfo = new ResourceUpdateInfo(oldResource, newResource);
			TimeSpan durationOfMovedAppointment = this.WeekModels[modelIndex2].GetDurationOfMovedAppointment(appointment, sourceSlot, targetSlot);
			DateTime dateTime = targetSlot.Start;
			DateTime end = dateTime.Add(durationOfMovedAppointment);
			if (flag)
			{
				dateTime += appointment.Start - sourceSlot.Start;
				end = this.Owner.DisplayToUtc(this.Owner.UtcToDisplay(dateTime).Add(durationOfMovedAppointment));
			}
			this.Owner.HandleMove(appointment, dateTime, end, editSeries, resourceUpdateInfo);
		}

		// Token: 0x06010563 RID: 66915 RVA: 0x003A58A4 File Offset: 0x003A3AA4
		public override void HandleInsert(ISchedulerTimeSlot targetSlot, ISchedulerTimeSlot lastSlot, Appointment appointmentToInsert)
		{
			int modelIndex = ((TimeSlot)targetSlot).ModelIndex;
			appointmentToInsert.Resources.Add(this.Resources[modelIndex]);
			this.WeekModels[modelIndex].HandleInsert(targetSlot, lastSlot, appointmentToInsert);
		}

		// Token: 0x06010564 RID: 66916 RVA: 0x003A58E8 File Offset: 0x003A3AE8
		public override void HandleResize(Appointment appointment, ISchedulerTimeSlot sourceSlot, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
			int modelIndex = ((TimeSlot)sourceSlot).ModelIndex;
			this.WeekModels[modelIndex].HandleResize(appointment, sourceSlot, appointmentStart, appointmentEnd, editSeries);
		}

		// Token: 0x04004980 RID: 18816
		private readonly string _groupingResourceName;
	}
}
