using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByResource
{
	// Token: 0x02001A5B RID: 6747
	internal class Model : ModelBase
	{
		// Token: 0x17004F76 RID: 20342
		// (get) Token: 0x060105D3 RID: 67027 RVA: 0x003A7682 File Offset: 0x003A5882
		// (set) Token: 0x060105D4 RID: 67028 RVA: 0x003A768A File Offset: 0x003A588A
		public IList<Resource> Resources
		{
			get
			{
				return this._resources;
			}
			protected set
			{
				this._resources = value;
			}
		}

		// Token: 0x17004F77 RID: 20343
		// (get) Token: 0x060105D5 RID: 67029 RVA: 0x003A7693 File Offset: 0x003A5893
		// (set) Token: 0x060105D6 RID: 67030 RVA: 0x003A769B File Offset: 0x003A589B
		public IList<Model> MonthModels
		{
			get
			{
				return this._monthModels;
			}
			protected set
			{
				this._monthModels = value;
			}
		}

		// Token: 0x17004F78 RID: 20344
		// (get) Token: 0x060105D7 RID: 67031 RVA: 0x003A76A4 File Offset: 0x003A58A4
		public string GroupingResourceName
		{
			get
			{
				return this._groupingResourceName;
			}
		}

		// Token: 0x17004F79 RID: 20345
		// (get) Token: 0x060105D8 RID: 67032 RVA: 0x003A76AC File Offset: 0x003A58AC
		protected IList<Resource> GroupingResources
		{
			get
			{
				return new List<Resource>(this.Owner.Resources.GetResourcesByType(this.GroupingResourceName));
			}
		}

		// Token: 0x060105D9 RID: 67033 RVA: 0x003A76C9 File Offset: 0x003A58C9
		public Model(IScheduler owner, string groupingResourceName) : base(owner)
		{
			this._groupingResourceName = groupingResourceName;
		}

		// Token: 0x060105DA RID: 67034 RVA: 0x003A76F4 File Offset: 0x003A58F4
		public override void DataBind(AppointmentCollection appointments)
		{
			this.Appointments.Clear();
			this.Resources = this.GroupingResources;
			this.MonthModels = new List<Model>(this.Resources.Count);
			int num = 0;
			List<Appointment> list = new List<Appointment>(appointments);
			foreach (Resource resource in this.Resources)
			{
				TimeSlotFactory slotFactory = new TimeSlotFactory(num, resource);
				Model model = new Model(this.Owner, slotFactory);
				this.MonthModels.Add(model);
				num++;
				Resource filteringResource = resource;
				IList<Appointment> list2 = list.FindAll((Appointment apt) => apt.Resources.Contains(filteringResource));
				model.DataBind(new AppointmentCollection(list2));
				this.Appointments.AddRange(list2);
			}
		}

		// Token: 0x060105DB RID: 67035 RVA: 0x003A77DC File Offset: 0x003A59DC
		public override ISchedulerRenderer GetRenderer()
		{
			View view;
			if (this.Owner.MonthView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Renderer(view);
		}

		// Token: 0x060105DC RID: 67036 RVA: 0x003A7814 File Offset: 0x003A5A14
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
			if (num > this.MonthModels.Count - 1)
			{
				throw new IndexOutOfRangeException("Resource index out of range: " + num);
			}
			string index2 = string.Join(":", array, 1, array.Length - 1);
			return this.MonthModels[num].GetSlotByIndex(index2);
		}

		// Token: 0x060105DD RID: 67037 RVA: 0x003A7898 File Offset: 0x003A5A98
		public override ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment)
		{
			foreach (Model model in this.MonthModels)
			{
				ISchedulerTimeSlot appointmentSlot = model.GetAppointmentSlot(appointment);
				if (appointmentSlot != null)
				{
					return appointmentSlot;
				}
			}
			return null;
		}

		// Token: 0x060105DE RID: 67038 RVA: 0x003A78F0 File Offset: 0x003A5AF0
		public override IList<ISchedulerTimeSlot> GetTimeSlots()
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			foreach (Model model in this.MonthModels)
			{
				list.AddRange(model.GetTimeSlots());
			}
			return list;
		}

		// Token: 0x060105DF RID: 67039 RVA: 0x003A794C File Offset: 0x003A5B4C
		public override void HandleMove(Appointment appointment, ISchedulerTimeSlot sourceSlot, ISchedulerTimeSlot targetSlot, bool editSeries)
		{
			int modelIndex = ((TimeSlot)sourceSlot).ModelIndex;
			int modelIndex2 = ((TimeSlot)targetSlot).ModelIndex;
			Resource oldResource = this.Resources[modelIndex];
			Resource newResource = this.Resources[modelIndex2];
			ResourceUpdateInfo resourceUpdateInfo = new ResourceUpdateInfo(oldResource, newResource);
			DateTime date = this.Owner.UtcToDisplay(targetSlot.Start) + (this.Owner.UtcToDisplay(appointment.Start) - this.Owner.UtcToDisplay(sourceSlot.Start));
			TimeSpan value = this.Owner.UtcToDisplay(appointment.End) - this.Owner.UtcToDisplay(appointment.Start);
			DateTime end = this.Owner.DisplayToUtc(date.Add(value));
			DateTime start = this.Owner.DisplayToUtc(date);
			this.Owner.HandleMove(appointment, start, end, editSeries, resourceUpdateInfo);
		}

		// Token: 0x060105E0 RID: 67040 RVA: 0x003A7A38 File Offset: 0x003A5C38
		public override void HandleInsert(ISchedulerTimeSlot targetSlot, ISchedulerTimeSlot lastSlot, Appointment appointmentToInsert)
		{
			int modelIndex = ((TimeSlot)targetSlot).ModelIndex;
			appointmentToInsert.Resources.Add(this.Resources[modelIndex]);
			this.MonthModels[modelIndex].HandleInsert(targetSlot, lastSlot, appointmentToInsert);
		}

		// Token: 0x060105E1 RID: 67041 RVA: 0x003A7A7C File Offset: 0x003A5C7C
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(base.GetScriptReferences());
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.Month.GroupedByResource.Model.js", Assembly.GetExecutingAssembly().FullName));
			return list;
		}

		// Token: 0x04004992 RID: 18834
		private IList<Resource> _resources;

		// Token: 0x04004993 RID: 18835
		private IList<Model> _monthModels;

		// Token: 0x04004994 RID: 18836
		private readonly string _groupingResourceName;
	}
}
