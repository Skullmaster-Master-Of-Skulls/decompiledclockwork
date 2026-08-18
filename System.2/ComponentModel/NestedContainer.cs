using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000593 RID: 1427
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class NestedContainer : Container, INestedContainer, IContainer, IDisposable
	{
		// Token: 0x06003509 RID: 13577 RVA: 0x000E7801 File Offset: 0x000E5A01
		public NestedContainer(IComponent owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			this._owner.Disposed += this.OnOwnerDisposed;
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x000E7835 File Offset: 0x000E5A35
		public IComponent Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x000E7840 File Offset: 0x000E5A40
		protected virtual string OwnerName
		{
			get
			{
				string result = null;
				if (this._owner != null && this._owner.Site != null)
				{
					INestedSite nestedSite = this._owner.Site as INestedSite;
					if (nestedSite != null)
					{
						result = nestedSite.FullName;
					}
					else
					{
						result = this._owner.Site.Name;
					}
				}
				return result;
			}
		}

		// Token: 0x0600350C RID: 13580 RVA: 0x000E7893 File Offset: 0x000E5A93
		protected override ISite CreateSite(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return new NestedContainer.Site(component, this, name);
		}

		// Token: 0x0600350D RID: 13581 RVA: 0x000E78AB File Offset: 0x000E5AAB
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._owner.Disposed -= this.OnOwnerDisposed;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600350E RID: 13582 RVA: 0x000E78CE File Offset: 0x000E5ACE
		protected override object GetService(Type service)
		{
			if (service == typeof(INestedContainer))
			{
				return this;
			}
			return base.GetService(service);
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000E78EB File Offset: 0x000E5AEB
		private void OnOwnerDisposed(object sender, EventArgs e)
		{
			base.Dispose();
		}

		// Token: 0x04002A38 RID: 10808
		private IComponent _owner;

		// Token: 0x0200089A RID: 2202
		private class Site : INestedSite, ISite, IServiceProvider
		{
			// Token: 0x060045AA RID: 17834 RVA: 0x0012366E File Offset: 0x0012186E
			internal Site(IComponent component, NestedContainer container, string name)
			{
				this.component = component;
				this.container = container;
				this.name = name;
			}

			// Token: 0x17000FC0 RID: 4032
			// (get) Token: 0x060045AB RID: 17835 RVA: 0x0012368B File Offset: 0x0012188B
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x17000FC1 RID: 4033
			// (get) Token: 0x060045AC RID: 17836 RVA: 0x00123693 File Offset: 0x00121893
			public IContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x060045AD RID: 17837 RVA: 0x0012369B File Offset: 0x0012189B
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return this.container.GetService(service);
				}
				return this;
			}

			// Token: 0x17000FC2 RID: 4034
			// (get) Token: 0x060045AE RID: 17838 RVA: 0x001236C0 File Offset: 0x001218C0
			public bool DesignMode
			{
				get
				{
					IComponent owner = this.container.Owner;
					return owner != null && owner.Site != null && owner.Site.DesignMode;
				}
			}

			// Token: 0x17000FC3 RID: 4035
			// (get) Token: 0x060045AF RID: 17839 RVA: 0x001236F4 File Offset: 0x001218F4
			public string FullName
			{
				get
				{
					if (this.name != null)
					{
						string ownerName = this.container.OwnerName;
						string text = this.name;
						if (ownerName != null)
						{
							text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
							{
								ownerName,
								text
							});
						}
						return text;
					}
					return this.name;
				}
			}

			// Token: 0x17000FC4 RID: 4036
			// (get) Token: 0x060045B0 RID: 17840 RVA: 0x00123745 File Offset: 0x00121945
			// (set) Token: 0x060045B1 RID: 17841 RVA: 0x0012374D File Offset: 0x0012194D
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					if (value == null || this.name == null || !value.Equals(this.name))
					{
						this.container.ValidateName(this.component, value);
						this.name = value;
					}
				}
			}

			// Token: 0x040037E7 RID: 14311
			private IComponent component;

			// Token: 0x040037E8 RID: 14312
			private NestedContainer container;

			// Token: 0x040037E9 RID: 14313
			private string name;
		}
	}
}
