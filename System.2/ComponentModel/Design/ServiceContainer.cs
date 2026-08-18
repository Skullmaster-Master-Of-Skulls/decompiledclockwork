using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005FF RID: 1535
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ServiceContainer : IServiceContainer, IServiceProvider, IDisposable
	{
		// Token: 0x06003883 RID: 14467 RVA: 0x000F150D File Offset: 0x000EF70D
		public ServiceContainer()
		{
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000F1515 File Offset: 0x000EF715
		public ServiceContainer(IServiceProvider parentProvider)
		{
			this.parentProvider = parentProvider;
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x000F1524 File Offset: 0x000EF724
		private IServiceContainer Container
		{
			get
			{
				IServiceContainer result = null;
				if (this.parentProvider != null)
				{
					result = (IServiceContainer)this.parentProvider.GetService(typeof(IServiceContainer));
				}
				return result;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06003886 RID: 14470 RVA: 0x000F1557 File Offset: 0x000EF757
		protected virtual Type[] DefaultServices
		{
			get
			{
				return ServiceContainer._defaultServices;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x000F155E File Offset: 0x000EF75E
		private ServiceContainer.ServiceCollection<object> Services
		{
			get
			{
				if (this.services == null)
				{
					this.services = new ServiceContainer.ServiceCollection<object>();
				}
				return this.services;
			}
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000F1579 File Offset: 0x000EF779
		public void AddService(Type serviceType, object serviceInstance)
		{
			this.AddService(serviceType, serviceInstance, false);
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000F1584 File Offset: 0x000EF784
		public virtual void AddService(Type serviceType, object serviceInstance, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, serviceInstance, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (serviceInstance == null)
			{
				throw new ArgumentNullException("serviceInstance");
			}
			if (!(serviceInstance is ServiceCreatorCallback) && !serviceInstance.GetType().IsCOMObject && !serviceType.IsAssignableFrom(serviceInstance.GetType()))
			{
				throw new ArgumentException(SR.GetString("ErrorInvalidServiceInstance", new object[]
				{
					serviceType.FullName
				}));
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(SR.GetString("ErrorServiceExists", new object[]
				{
					serviceType.FullName
				}), "serviceType");
			}
			this.Services[serviceType] = serviceInstance;
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000F164B File Offset: 0x000EF84B
		public void AddService(Type serviceType, ServiceCreatorCallback callback)
		{
			this.AddService(serviceType, callback, false);
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000F1658 File Offset: 0x000EF858
		public virtual void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.AddService(serviceType, callback, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			if (this.Services.ContainsKey(serviceType))
			{
				throw new ArgumentException(SR.GetString("ErrorServiceExists", new object[]
				{
					serviceType.FullName
				}), "serviceType");
			}
			this.Services[serviceType] = callback;
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000F16DD File Offset: 0x000EF8DD
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000F16E8 File Offset: 0x000EF8E8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				ServiceContainer.ServiceCollection<object> serviceCollection = this.services;
				this.services = null;
				if (serviceCollection != null)
				{
					foreach (object obj in serviceCollection.Values)
					{
						if (obj is IDisposable)
						{
							((IDisposable)obj).Dispose();
						}
					}
				}
			}
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000F175C File Offset: 0x000EF95C
		public virtual object GetService(Type serviceType)
		{
			object obj = null;
			Type[] defaultServices = this.DefaultServices;
			for (int i = 0; i < defaultServices.Length; i++)
			{
				if (serviceType.IsEquivalentTo(defaultServices[i]))
				{
					obj = this;
					break;
				}
			}
			if (obj == null)
			{
				this.Services.TryGetValue(serviceType, out obj);
			}
			if (obj is ServiceCreatorCallback)
			{
				obj = ((ServiceCreatorCallback)obj)(this, serviceType);
				if (obj != null && !obj.GetType().IsCOMObject && !serviceType.IsAssignableFrom(obj.GetType()))
				{
					obj = null;
				}
				this.Services[serviceType] = obj;
			}
			if (obj == null && this.parentProvider != null)
			{
				obj = this.parentProvider.GetService(serviceType);
			}
			return obj;
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000F17FD File Offset: 0x000EF9FD
		public void RemoveService(Type serviceType)
		{
			this.RemoveService(serviceType, false);
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000F1808 File Offset: 0x000EFA08
		public virtual void RemoveService(Type serviceType, bool promote)
		{
			if (promote)
			{
				IServiceContainer container = this.Container;
				if (container != null)
				{
					container.RemoveService(serviceType, promote);
					return;
				}
			}
			if (serviceType == null)
			{
				throw new ArgumentNullException("serviceType");
			}
			this.Services.Remove(serviceType);
		}

		// Token: 0x04002B29 RID: 11049
		private ServiceContainer.ServiceCollection<object> services;

		// Token: 0x04002B2A RID: 11050
		private IServiceProvider parentProvider;

		// Token: 0x04002B2B RID: 11051
		private static Type[] _defaultServices = new Type[]
		{
			typeof(IServiceContainer),
			typeof(ServiceContainer)
		};

		// Token: 0x04002B2C RID: 11052
		private static TraceSwitch TRACESERVICE = new TraceSwitch("TRACESERVICE", "ServiceProvider: Trace service provider requests.");

		// Token: 0x020008AF RID: 2223
		private sealed class ServiceCollection<T> : Dictionary<Type, T>
		{
			// Token: 0x06004626 RID: 17958 RVA: 0x00124DCF File Offset: 0x00122FCF
			public ServiceCollection() : base(ServiceContainer.ServiceCollection<T>.serviceTypeComparer)
			{
			}

			// Token: 0x0400380B RID: 14347
			private static ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer serviceTypeComparer = new ServiceContainer.ServiceCollection<T>.EmbeddedTypeAwareTypeComparer();

			// Token: 0x0200093A RID: 2362
			private sealed class EmbeddedTypeAwareTypeComparer : IEqualityComparer<Type>
			{
				// Token: 0x060046F0 RID: 18160 RVA: 0x0012834A File Offset: 0x0012654A
				public bool Equals(Type x, Type y)
				{
					return x.IsEquivalentTo(y);
				}

				// Token: 0x060046F1 RID: 18161 RVA: 0x00128353 File Offset: 0x00126553
				public int GetHashCode(Type obj)
				{
					return obj.FullName.GetHashCode();
				}
			}
		}
	}
}
