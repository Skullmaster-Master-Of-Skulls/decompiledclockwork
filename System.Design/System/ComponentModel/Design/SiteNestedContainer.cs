using System;
using System.Globalization;

namespace System.ComponentModel.Design
{
	// Token: 0x02000555 RID: 1365
	internal sealed class SiteNestedContainer : NestedContainer
	{
		// Token: 0x06003031 RID: 12337 RVA: 0x001122FF File Offset: 0x001112FF
		internal SiteNestedContainer(IComponent owner, string containerName, DesignerHost host) : base(owner)
		{
			this._containerName = containerName;
			this._host = host;
			this._safeToCallOwner = true;
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x00112320 File Offset: 0x00111320
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

		// Token: 0x06003033 RID: 12339 RVA: 0x00112370 File Offset: 0x00111370
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
				catch
				{
					this.Remove(component);
					throw;
				}
			}
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x001123E0 File Offset: 0x001113E0
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return new SiteNestedContainer.NestedSite(component, this._host, name, this);
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x001123FE File Offset: 0x001113FE
		public override void Remove(IComponent component)
		{
			if (this._host.RemoveFromContainerPreProcess(component, this))
			{
				ISite site = component.Site;
				base.RemoveWithoutUnsiting(component);
				this._host.RemoveFromContainerPostProcess(component, this);
			}
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x0011242C File Offset: 0x0011142C
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

		// Token: 0x06003037 RID: 12343 RVA: 0x001124D4 File Offset: 0x001114D4
		internal object GetServiceInternal(Type serviceType)
		{
			return this.GetService(serviceType);
		}

		// Token: 0x0400208C RID: 8332
		private DesignerHost _host;

		// Token: 0x0400208D RID: 8333
		private IServiceContainer _services;

		// Token: 0x0400208E RID: 8334
		private string _containerName;

		// Token: 0x0400208F RID: 8335
		private bool _safeToCallOwner;

		// Token: 0x02000556 RID: 1366
		private sealed class NestedSite : DesignerHost.Site, INestedSite, ISite, IServiceProvider
		{
			// Token: 0x06003038 RID: 12344 RVA: 0x001124DD File Offset: 0x001114DD
			internal NestedSite(IComponent component, DesignerHost host, string name, Container container) : base(component, host, name, container)
			{
				this._container = (container as SiteNestedContainer);
				this._name = name;
			}

			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x06003039 RID: 12345 RVA: 0x00112500 File Offset: 0x00111500
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

			// Token: 0x04002090 RID: 8336
			private SiteNestedContainer _container;

			// Token: 0x04002091 RID: 8337
			private string _name;
		}
	}
}
