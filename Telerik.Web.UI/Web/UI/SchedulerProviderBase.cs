using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI
{
	// Token: 0x020012DA RID: 4826
	public abstract class SchedulerProviderBase : ProviderBase
	{
		// Token: 0x17004179 RID: 16761
		// (get) Token: 0x0600CA95 RID: 51861 RVA: 0x002D37F4 File Offset: 0x002D19F4
		// (set) Token: 0x0600CA96 RID: 51862 RVA: 0x002D37FC File Offset: 0x002D19FC
		internal virtual RadScheduler LegacyOwner
		{
			get
			{
				return this._legacyOwner;
			}
			set
			{
				this._legacyOwner = value;
			}
		}

		// Token: 0x0600CA97 RID: 51863 RVA: 0x002D3805 File Offset: 0x002D1A05
		public virtual IEnumerable<Appointment> GetAppointments(ISchedulerInfo schedulerInfo)
		{
			return this.GetAppointments(this.LegacyOwner);
		}

		// Token: 0x0600CA98 RID: 51864 RVA: 0x002D3814 File Offset: 0x002D1A14
		public virtual IDictionary<ResourceType, IEnumerable<Resource>> GetResources(ISchedulerInfo schedulerInfo)
		{
			IEnumerable<ResourceType> resourceTypes = this.GetResourceTypes(this.LegacyOwner);
			if (resourceTypes == null)
			{
				return null;
			}
			Dictionary<ResourceType, IEnumerable<Resource>> dictionary = new Dictionary<ResourceType, IEnumerable<Resource>>();
			foreach (ResourceType resourceType in resourceTypes)
			{
				dictionary[resourceType] = this.GetResourcesByType(this.LegacyOwner, resourceType.Name);
			}
			return dictionary;
		}

		// Token: 0x0600CA99 RID: 51865 RVA: 0x002D3888 File Offset: 0x002D1A88
		public virtual void Insert(ISchedulerInfo schedulerInfo, Appointment appointmentToInsert)
		{
			this.Insert(this.LegacyOwner, appointmentToInsert);
		}

		// Token: 0x0600CA9A RID: 51866 RVA: 0x002D3897 File Offset: 0x002D1A97
		public virtual void Update(ISchedulerInfo schedulerInfo, Appointment appointmentToUpdate)
		{
			this.Update(this.LegacyOwner, appointmentToUpdate);
		}

		// Token: 0x0600CA9B RID: 51867 RVA: 0x002D38A6 File Offset: 0x002D1AA6
		public virtual void Delete(ISchedulerInfo schedulerInfo, Appointment appointmentToDelete)
		{
			this.Delete(this.LegacyOwner, appointmentToDelete);
		}

		// Token: 0x0600CA9C RID: 51868 RVA: 0x002D38B5 File Offset: 0x002D1AB5
		public virtual SchedulerProviderBase Synchronized()
		{
			return new SynchronizedSchedulerProvider(this);
		}

		// Token: 0x0600CA9D RID: 51869 RVA: 0x002D38BD File Offset: 0x002D1ABD
		public virtual IEnumerable<Appointment> GetAppointments(RadScheduler owner)
		{
			throw new NotImplementedException("Please override GetAppointments(ISchedulerInfo) when implementing a new provider.");
		}

		// Token: 0x0600CA9E RID: 51870 RVA: 0x002D38C9 File Offset: 0x002D1AC9
		public virtual IEnumerable<ResourceType> GetResourceTypes(RadScheduler owner)
		{
			throw new NotImplementedException("Please override GetResources(ISchedulerInfo) when implementing a new provider.");
		}

		// Token: 0x0600CA9F RID: 51871 RVA: 0x002D38D5 File Offset: 0x002D1AD5
		public virtual IEnumerable<Resource> GetResourcesByType(RadScheduler owner, string resourceType)
		{
			throw new NotImplementedException("Please override GetResources(ISchedulerInfo) when implementing a new provider.");
		}

		// Token: 0x0600CAA0 RID: 51872 RVA: 0x002D38E1 File Offset: 0x002D1AE1
		public virtual void Insert(RadScheduler owner, Appointment appointmentToInsert)
		{
			throw new NotImplementedException("Please override Insert(ISchedulerInfo, Appointment) when implementing a new provider.");
		}

		// Token: 0x0600CAA1 RID: 51873 RVA: 0x002D38ED File Offset: 0x002D1AED
		public virtual void Update(RadScheduler owner, Appointment appointmentToUpdate)
		{
			throw new NotImplementedException("Please override Update(ISchedulerInfo, Appointment) when implementing a new provider.");
		}

		// Token: 0x0600CAA2 RID: 51874 RVA: 0x002D38F9 File Offset: 0x002D1AF9
		public virtual void Delete(RadScheduler owner, Appointment appointmentToDelete)
		{
			throw new NotImplementedException("Please override Delete(ISchedulerInfo, Appointment) when implementing a new provider.");
		}

		// Token: 0x04003533 RID: 13619
		[ThreadStatic]
		private RadScheduler _legacyOwner;
	}
}
