using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource
{
	// Token: 0x0200082F RID: 2095
	internal class Model : ModelBase
	{
		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x06004DA1 RID: 19873 RVA: 0x000F3616 File Offset: 0x000F1816
		// (set) Token: 0x06004DA2 RID: 19874 RVA: 0x000F361E File Offset: 0x000F181E
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

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x06004DA3 RID: 19875 RVA: 0x000F3627 File Offset: 0x000F1827
		// (set) Token: 0x06004DA4 RID: 19876 RVA: 0x000F362F File Offset: 0x000F182F
		public IList<Model> AgendaModels
		{
			get
			{
				return this._agendaModels;
			}
			protected set
			{
				this._agendaModels = value;
			}
		}

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x06004DA5 RID: 19877 RVA: 0x000F3638 File Offset: 0x000F1838
		public string GroupingResourceName
		{
			get
			{
				return this._groupingResourceName;
			}
		}

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x06004DA6 RID: 19878 RVA: 0x000F3640 File Offset: 0x000F1840
		protected IList<Resource> GroupingResources
		{
			get
			{
				return new List<Resource>(this.Owner.Resources.GetResourcesByType(this.GroupingResourceName));
			}
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x000F365D File Offset: 0x000F185D
		public Model(IScheduler owner, string groupingResourceName) : base(owner)
		{
			this._groupingResourceName = groupingResourceName;
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x000F3688 File Offset: 0x000F1888
		public override void DataBind(AppointmentCollection appointments)
		{
			this.Appointments.Clear();
			this.Resources = this.GroupingResources;
			this.AgendaModels = new List<Model>(this.Resources.Count);
			int num = 0;
			List<Appointment> list = new List<Appointment>(appointments);
			foreach (Resource resource in this.Resources)
			{
				TimeSlotFactory slotFactory = new TimeSlotFactory(num, resource);
				Model model = new Model(this.Owner, slotFactory);
				this.AgendaModels.Add(model);
				num++;
				Resource filteringResource = resource;
				IList<Appointment> list2 = list.FindAll((Appointment apt) => apt.Resources.Contains(filteringResource));
				model.DataBind(new AppointmentCollection(list2));
				this.Appointments.AddRange(list2);
			}
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x000F3770 File Offset: 0x000F1970
		public override ISchedulerRenderer GetRenderer()
		{
			View view;
			if (this.Owner.AgendaView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Renderer(view);
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x000F37A8 File Offset: 0x000F19A8
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
			if (num > this.AgendaModels.Count - 1)
			{
				throw new IndexOutOfRangeException("Resource index out of range: " + num);
			}
			string index2 = string.Join(":", array, 1, array.Length - 1);
			return this.AgendaModels[num].GetSlotByIndex(index2);
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x000F382C File Offset: 0x000F1A2C
		public override ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment)
		{
			foreach (Model model in this.AgendaModels)
			{
				ISchedulerTimeSlot appointmentSlot = model.GetAppointmentSlot(appointment);
				if (appointmentSlot != null)
				{
					return appointmentSlot;
				}
			}
			return null;
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x000F3884 File Offset: 0x000F1A84
		public override IList<ISchedulerTimeSlot> GetTimeSlots()
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			foreach (Model model in this.AgendaModels)
			{
				list.AddRange(model.GetTimeSlots());
			}
			return list;
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x000F38E0 File Offset: 0x000F1AE0
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(base.GetScriptReferences());
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource.Model.js", Assembly.GetExecutingAssembly().FullName));
			return list;
		}

		// Token: 0x04001364 RID: 4964
		private IList<Resource> _resources;

		// Token: 0x04001365 RID: 4965
		private IList<Model> _agendaModels;

		// Token: 0x04001366 RID: 4966
		private readonly string _groupingResourceName;
	}
}
