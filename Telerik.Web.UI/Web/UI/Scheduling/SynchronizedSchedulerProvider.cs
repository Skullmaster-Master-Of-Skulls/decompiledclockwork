using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x020012DF RID: 4831
	internal sealed class SynchronizedSchedulerProvider : SchedulerProviderBase
	{
		// Token: 0x0600CAC7 RID: 51911 RVA: 0x002D46EC File Offset: 0x002D28EC
		public SynchronizedSchedulerProvider(SchedulerProviderBase parent)
		{
			if (parent == null)
			{
				throw new ArgumentException("Parent cannot be null.", "parent");
			}
			this._parent = parent;
		}

		// Token: 0x17004180 RID: 16768
		// (get) Token: 0x0600CAC8 RID: 51912 RVA: 0x002D470E File Offset: 0x002D290E
		public override string Name
		{
			get
			{
				return this._parent.Name;
			}
		}

		// Token: 0x17004181 RID: 16769
		// (get) Token: 0x0600CAC9 RID: 51913 RVA: 0x002D471B File Offset: 0x002D291B
		// (set) Token: 0x0600CACA RID: 51914 RVA: 0x002D4728 File Offset: 0x002D2928
		internal override RadScheduler LegacyOwner
		{
			get
			{
				return this._parent.LegacyOwner;
			}
			set
			{
				this._parent.LegacyOwner = value;
			}
		}

		// Token: 0x0600CACB RID: 51915 RVA: 0x002D4736 File Offset: 0x002D2936
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override IEnumerable<Appointment> GetAppointments(ISchedulerInfo schedulerInfo)
		{
			return this._parent.GetAppointments(schedulerInfo);
		}

		// Token: 0x0600CACC RID: 51916 RVA: 0x002D4744 File Offset: 0x002D2944
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override IDictionary<ResourceType, IEnumerable<Resource>> GetResources(ISchedulerInfo schedulerInfo)
		{
			return this._parent.GetResources(schedulerInfo);
		}

		// Token: 0x0600CACD RID: 51917 RVA: 0x002D4752 File Offset: 0x002D2952
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Insert(ISchedulerInfo schedulerInfo, Appointment appointmentToInsert)
		{
			this._parent.Insert(schedulerInfo, appointmentToInsert);
		}

		// Token: 0x0600CACE RID: 51918 RVA: 0x002D4761 File Offset: 0x002D2961
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Update(ISchedulerInfo schedulerInfo, Appointment appointmentToUpdate)
		{
			this._parent.Update(schedulerInfo, appointmentToUpdate);
		}

		// Token: 0x0600CACF RID: 51919 RVA: 0x002D4770 File Offset: 0x002D2970
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Delete(ISchedulerInfo schedulerInfo, Appointment appointmentToDelete)
		{
			this._parent.Delete(schedulerInfo, appointmentToDelete);
		}

		// Token: 0x0600CAD0 RID: 51920 RVA: 0x002D477F File Offset: 0x002D297F
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override IEnumerable<Appointment> GetAppointments(RadScheduler owner)
		{
			return this._parent.GetAppointments(owner);
		}

		// Token: 0x0600CAD1 RID: 51921 RVA: 0x002D478D File Offset: 0x002D298D
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Insert(RadScheduler owner, Appointment appointmentToInsert)
		{
			this._parent.Insert(owner, appointmentToInsert);
		}

		// Token: 0x0600CAD2 RID: 51922 RVA: 0x002D479C File Offset: 0x002D299C
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Update(RadScheduler owner, Appointment appointmentToUpdate)
		{
			this._parent.Update(owner, appointmentToUpdate);
		}

		// Token: 0x0600CAD3 RID: 51923 RVA: 0x002D47AB File Offset: 0x002D29AB
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Delete(RadScheduler owner, Appointment appointmentToDelete)
		{
			this._parent.Delete(owner, appointmentToDelete);
		}

		// Token: 0x0600CAD4 RID: 51924 RVA: 0x002D47BA File Offset: 0x002D29BA
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override IEnumerable<ResourceType> GetResourceTypes(RadScheduler owner)
		{
			return this._parent.GetResourceTypes(owner);
		}

		// Token: 0x0600CAD5 RID: 51925 RVA: 0x002D47C8 File Offset: 0x002D29C8
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override IEnumerable<Resource> GetResourcesByType(RadScheduler owner, string resourceType)
		{
			return this._parent.GetResourcesByType(owner, resourceType);
		}

		// Token: 0x0600CAD6 RID: 51926 RVA: 0x002D47D7 File Offset: 0x002D29D7
		public override SchedulerProviderBase Synchronized()
		{
			return this;
		}

		// Token: 0x0400353C RID: 13628
		private readonly SchedulerProviderBase _parent;
	}
}
