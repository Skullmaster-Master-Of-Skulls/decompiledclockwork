using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views.Year.GroupedByResource
{
	// Token: 0x0200084F RID: 2127
	internal class Model : ModelBase
	{
		// Token: 0x170019AE RID: 6574
		// (get) Token: 0x06004E81 RID: 20097 RVA: 0x000F5E7A File Offset: 0x000F407A
		public string GroupingResourceName
		{
			get
			{
				return this._groupingResourceName;
			}
		}

		// Token: 0x170019AF RID: 6575
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x000F5E82 File Offset: 0x000F4082
		protected IList<Resource> GroupingResources
		{
			get
			{
				return new List<Resource>(this.Owner.Resources.GetResourcesByType(this.GroupingResourceName));
			}
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x06004E83 RID: 20099 RVA: 0x000F5E9F File Offset: 0x000F409F
		// (set) Token: 0x06004E84 RID: 20100 RVA: 0x000F5EA7 File Offset: 0x000F40A7
		public IList<Resource> Resources { get; protected set; }

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x06004E85 RID: 20101 RVA: 0x000F5EB0 File Offset: 0x000F40B0
		// (set) Token: 0x06004E86 RID: 20102 RVA: 0x000F5EB8 File Offset: 0x000F40B8
		public IList<Model> YearModels { get; protected set; }

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x06004E87 RID: 20103 RVA: 0x000F5EC1 File Offset: 0x000F40C1
		public override string CssClass
		{
			get
			{
				if (this.Owner.YearView.GroupingDirectionResolved == GroupingDirection.Vertical)
				{
					return "rsYearView rsVertical";
				}
				return "rsYearView rsHorizontal";
			}
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x000F5EE1 File Offset: 0x000F40E1
		public Model(IScheduler owner, string groupingResourceName) : base(owner)
		{
			this._groupingResourceName = groupingResourceName;
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x000F5F0C File Offset: 0x000F410C
		public override void DataBind(AppointmentCollection appointments)
		{
			this.Appointments.Clear();
			this.Resources = this.GroupingResources;
			this.YearModels = new List<Model>(this.Resources.Count);
			int num = 0;
			List<Appointment> list = new List<Appointment>(appointments);
			foreach (Resource resource in this.Resources)
			{
				TimeSlotFactory slotFactory = new TimeSlotFactory(num, resource);
				Model model = new Model(this.Owner, slotFactory);
				this.YearModels.Add(model);
				num++;
				Resource filteringResource = resource;
				IList<Appointment> list2 = list.FindAll((Appointment apt) => apt.Resources.Contains(filteringResource));
				model.DataBind(new AppointmentCollection(list2));
				this.Appointments.AddRange(list2);
			}
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x000F5FF4 File Offset: 0x000F41F4
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(base.GetScriptReferences());
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.Year.GroupedByResource.Model.js", Assembly.GetExecutingAssembly().FullName));
			return list;
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x000F6030 File Offset: 0x000F4230
		public override ISchedulerRenderer GetRenderer()
		{
			View view;
			if (this.Owner.YearView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Renderer(view);
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x000F6068 File Offset: 0x000F4268
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
			if (num > this.YearModels.Count - 1)
			{
				throw new IndexOutOfRangeException("Resource index out of range: " + num);
			}
			string index2 = string.Join(":", array, 1, array.Length - 1);
			return this.YearModels[num].GetSlotByIndex(index2);
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x000F60EC File Offset: 0x000F42EC
		public override ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment)
		{
			foreach (Model model in this.YearModels)
			{
				ISchedulerTimeSlot appointmentSlot = model.GetAppointmentSlot(appointment);
				if (appointmentSlot != null)
				{
					return appointmentSlot;
				}
			}
			return null;
		}

		// Token: 0x06004E8E RID: 20110 RVA: 0x000F6144 File Offset: 0x000F4344
		public override IList<ISchedulerTimeSlot> GetTimeSlots()
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			foreach (Model model in this.YearModels)
			{
				list.AddRange(model.GetTimeSlots());
			}
			return list;
		}

		// Token: 0x0400138A RID: 5002
		private readonly string _groupingResourceName;
	}
}
