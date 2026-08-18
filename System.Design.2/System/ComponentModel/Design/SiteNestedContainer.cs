using System;
using System.Globalization;

namespace System.ComponentModel.Design
{
	// Token: 0x020001C4 RID: 452
	internal sealed class SiteNestedContainer : NestedContainer
	{
		// Token: 0x060010B2 RID: 4274 RVA: 0x0005D9E6 File Offset: 0x0005BBE6
		internal SiteNestedContainer(IComponent owner, string containerName, DesignerHost host) : base(owner)
		{
			this._containerName = containerName;
			this._host = host;
			this._safeToCallOwner = true;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060010B3 RID: 4275 RVA: 0x0005DA04 File Offset: 0x0005BC04
		protected override string OwnerName
		{
			get
			{
				string text = base.OwnerName;
				if (this._containerName != null && this._containerName.Length > 0)
				{
					text = string.Format(CultureInfo.CurrentCulture, "{0}.{1}", new object[]
					{
						text,
						this._containerName
					});
				}
				return text;
			}
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0005DA54 File Offset: 0x0005BC54
		public override void Add(IComponent component, string name)
		{
			if (this._host.AddToContainerPreProcess(component, name, this))
			{
				base.Add(component, name);
				try
				{
					this._host.AddToContainerPostProcess(component, name, this);
				}
				catch (Exception ex)
				{
					if (ex != CheckoutException.Canceled)
					{
						this.Remove(component);
					}
					throw;
				}
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0005DAAC File Offset: 0x0005BCAC
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return new SiteNestedContainer.NestedSite(component, this._host, name, this);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0005DACC File Offset: 0x0005BCCC
		public override void Remove(IComponent component)
		{
			if (this._host.RemoveFromContainerPreProcess(component, this))
			{
				ISite site = component.Site;
				base.RemoveWithoutUnsiting(component);
				this._host.RemoveFromContainerPostProcess(component, this);
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0005DB04 File Offset: 0x0005BD04
		protected override object GetService(Type serviceType)
		{
			object service = base.GetService(serviceType);
			if (service != null)
			{
				return service;
			}
			if (serviceType == typeof(IServiceContainer))
			{
				if (this._services == null)
				{
					this._services = new ServiceContainer(this._host);
				}
				return this._services;
			}
			if (this._services != null)
			{
				return this._services.GetService(serviceType);
			}
			if (base.Owner.Site != null && this._safeToCallOwner)
			{
				try
				{
					this._safeToCallOwner = false;
					return base.Owner.Site.GetService(serviceType);
				}
				finally
				{
					this._safeToCallOwner = true;
				}
			}
			return null;
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0005D8D1 File Offset: 0x0005BAD1
		internal object GetServiceInternal(Type serviceType)
		{
			return this.GetService(serviceType);
		}

		// Token: 0x04000991 RID: 2449
		private DesignerHost _host;

		// Token: 0x04000992 RID: 2450
		private IServiceContainer _services;

		// Token: 0x04000993 RID: 2451
		private string _containerName;

		// Token: 0x04000994 RID: 2452
		private bool _safeToCallOwner;

		// Token: 0x02000499 RID: 1177
		private sealed class NestedSite : DesignerHost.Site, INestedSite, ISite, IServiceProvider
		{
			// Token: 0x06002B6C RID: 11116 RVA: 0x0010394D File Offset: 0x00101B4D
			internal NestedSite(IComponent component, DesignerHost host, string name, Container container) : base(component, host, name, container)
			{
				this._container = (container as SiteNestedContainer);
				this._name = name;
			}

			// Token: 0x17000929 RID: 2345
			// (get) Token: 0x06002B6D RID: 11117 RVA: 0x00103970 File Offset: 0x00101B70
			public string FullName
			{
				get
				{
					if (this._name != null)
					{
						string ownerName = this._container.OwnerName;
						string text = ((ISite)this).Name;
						if (ownerName != null)
						{
							text = string.Format(CultureInfo.CurrentCulture, "{0}.{1}", new object[]
							{
								ownerName,
								text
							});
						}
						return text;
					}
					return this._name;
				}
			}

			// Token: 0x04001E24 RID: 7716
			private SiteNestedContainer _container;

			// Token: 0x04001E25 RID: 7717
			private string _name;
		}
	}
}
