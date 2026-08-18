using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000CA RID: 202
	public abstract class ServicesContainer : IDisposable
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x0000F24C File Offset: 0x0000D44C
		protected ServicesContainer()
		{
			this.ExceptionServicesLogger = new Lazy<IExceptionLogger>(new Func<IExceptionLogger>(this.CreateExceptionServicesLogger));
			this.ExceptionServicesHandler = new Lazy<IExceptionHandler>(new Func<IExceptionHandler>(this.CreateExceptionServicesHandler));
		}

		// Token: 0x060004AE RID: 1198
		public abstract object GetService(Type serviceType);

		// Token: 0x060004AF RID: 1199
		public abstract IEnumerable<object> GetServices(Type serviceType);

		// Token: 0x060004B0 RID: 1200
		protected abstract List<object> GetServiceInstances(Type serviceType);

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000F282 File Offset: 0x0000D482
		protected virtual void ResetCache(Type serviceType)
		{
		}

		// Token: 0x060004B2 RID: 1202
		public abstract bool IsSingleService(Type serviceType);

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000F284 File Offset: 0x0000D484
		public void Add(Type serviceType, object service)
		{
			this.Insert(serviceType, int.MaxValue, service);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000F293 File Offset: 0x0000D493
		public void AddRange(Type serviceType, IEnumerable<object> services)
		{
			this.InsertRange(serviceType, int.MaxValue, services);
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0000F2A2 File Offset: 0x0000D4A2
		public virtual void Clear(Type serviceType)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (this.IsSingleService(serviceType))
			{
				this.ClearSingle(serviceType);
			}
			else
			{
				this.ClearMultiple(serviceType);
			}
			this.ResetCache(serviceType);
		}

		// Token: 0x060004B6 RID: 1206
		protected abstract void ClearSingle(Type serviceType);

		// Token: 0x060004B7 RID: 1207 RVA: 0x0000F2D8 File Offset: 0x0000D4D8
		protected virtual void ClearMultiple(Type serviceType)
		{
			List<object> serviceInstances = this.GetServiceInstances(serviceType);
			serviceInstances.Clear();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000F2F4 File Offset: 0x0000D4F4
		public int FindIndex(Type serviceType, Predicate<object> match)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (match == null)
			{
				throw Error.ArgumentNull("match");
			}
			List<object> serviceInstances = this.GetServiceInstances(serviceType);
			return serviceInstances.FindIndex(match);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000F334 File Offset: 0x0000D534
		public void Insert(Type serviceType, int index, object service)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (service == null)
			{
				throw Error.ArgumentNull("service");
			}
			if (!serviceType.IsAssignableFrom(service.GetType()))
			{
				throw Error.Argument("service", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					service.GetType().Name,
					serviceType.Name
				});
			}
			List<object> serviceInstances = this.GetServiceInstances(serviceType);
			if (index == 2147483647)
			{
				index = serviceInstances.Count;
			}
			serviceInstances.Insert(index, service);
			this.ResetCache(serviceType);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		public void InsertRange(Type serviceType, int index, IEnumerable<object> services)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (services == null)
			{
				throw Error.ArgumentNull("services");
			}
			object[] array = (from svc in services
			where svc != null
			select svc).ToArray<object>();
			object obj = array.FirstOrDefault((object svc) => !serviceType.IsAssignableFrom(svc.GetType()));
			if (obj != null)
			{
				throw Error.Argument("services", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					obj.GetType().Name,
					serviceType.Name
				});
			}
			List<object> serviceInstances = this.GetServiceInstances(serviceType);
			if (index == 2147483647)
			{
				index = serviceInstances.Count;
			}
			serviceInstances.InsertRange(index, array);
			this.ResetCache(serviceType);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000F4DC File Offset: 0x0000D6DC
		public bool Remove(Type serviceType, object service)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (service == null)
			{
				throw Error.ArgumentNull("service");
			}
			List<object> serviceInstances = this.GetServiceInstances(serviceType);
			bool result = serviceInstances.Remove(service);
			this.ResetCache(serviceType);
			return result;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000F524 File Offset: 0x0000D724
		public int RemoveAll(Type serviceType, Predicate<object> match)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (match == null)
			{
				throw Error.ArgumentNull("match");
			}
			List<object> serviceInstances = this.GetServiceInstances(serviceType);
			int result = serviceInstances.RemoveAll(match);
			this.ResetCache(serviceType);
			return result;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000F56C File Offset: 0x0000D76C
		public void RemoveAt(Type serviceType, int index)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			List<object> serviceInstances = this.GetServiceInstances(serviceType);
			serviceInstances.RemoveAt(index);
			this.ResetCache(serviceType);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000F5A4 File Offset: 0x0000D7A4
		public void Replace(Type serviceType, object service)
		{
			if (serviceType == null)
			{
				throw Error.ArgumentNull("serviceType");
			}
			if (service != null && !serviceType.IsAssignableFrom(service.GetType()))
			{
				throw Error.Argument("service", SRResources.Common_TypeMustDriveFromType, new object[]
				{
					service.GetType().Name,
					serviceType.Name
				});
			}
			if (this.IsSingleService(serviceType))
			{
				this.ReplaceSingle(serviceType, service);
			}
			else
			{
				this.ReplaceMultiple(serviceType, service);
			}
			this.ResetCache(serviceType);
		}

		// Token: 0x060004BF RID: 1215
		protected abstract void ReplaceSingle(Type serviceType, object service);

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000F62A File Offset: 0x0000D82A
		protected virtual void ReplaceMultiple(Type serviceType, object service)
		{
			this.RemoveAll(serviceType, (object _) => true);
			this.Insert(serviceType, 0, service);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000F65D File Offset: 0x0000D85D
		public void ReplaceRange(Type serviceType, IEnumerable<object> services)
		{
			if (services == null)
			{
				throw Error.ArgumentNull("services");
			}
			this.RemoveAll(serviceType, (object _) => true);
			this.InsertRange(serviceType, 0, services);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0000F69B File Offset: 0x0000D89B
		public virtual void Dispose()
		{
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000F69D File Offset: 0x0000D89D
		private IExceptionLogger CreateExceptionServicesLogger()
		{
			return ExceptionServices.CreateLogger(this);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000F6A5 File Offset: 0x0000D8A5
		private IExceptionHandler CreateExceptionServicesHandler()
		{
			return ExceptionServices.CreateHandler(this);
		}

		// Token: 0x0400015E RID: 350
		internal readonly Lazy<IExceptionLogger> ExceptionServicesLogger;

		// Token: 0x0400015F RID: 351
		internal readonly Lazy<IExceptionHandler> ExceptionServicesHandler;
	}
}
