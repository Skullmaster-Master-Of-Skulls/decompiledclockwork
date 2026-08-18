using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200052F RID: 1327
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class Container : IContainer, IDisposable
	{
		// Token: 0x0600322C RID: 12844 RVA: 0x000E10F8 File Offset: 0x000DF2F8
		~Container()
		{
			this.Dispose(false);
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x000E1128 File Offset: 0x000DF328
		public virtual void Add(IComponent component)
		{
			this.Add(component, null);
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x000E1134 File Offset: 0x000DF334
		public virtual void Add(IComponent component, string name)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site == null || site.Container != this)
					{
						if (this.sites == null)
						{
							this.sites = new ISite[4];
						}
						else
						{
							this.ValidateName(component, name);
							if (this.sites.Length == this.siteCount)
							{
								ISite[] destinationArray = new ISite[this.siteCount * 2];
								Array.Copy(this.sites, 0, destinationArray, 0, this.siteCount);
								this.sites = destinationArray;
							}
						}
						if (site != null)
						{
							site.Container.Remove(component);
						}
						ISite site2 = this.CreateSite(component, name);
						ISite[] array = this.sites;
						int num = this.siteCount;
						this.siteCount = num + 1;
						array[num] = site2;
						component.Site = site2;
						this.components = null;
					}
				}
			}
		}

		// Token: 0x0600322F RID: 12847 RVA: 0x000E122C File Offset: 0x000DF42C
		protected virtual ISite CreateSite(IComponent component, string name)
		{
			return new Container.Site(component, this, name);
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x000E1236 File Offset: 0x000DF436
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000E1248 File Offset: 0x000DF448
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				object obj = this.syncObj;
				lock (obj)
				{
					while (this.siteCount > 0)
					{
						ISite[] array = this.sites;
						int num = this.siteCount - 1;
						this.siteCount = num;
						ISite site = array[num];
						site.Component.Site = null;
						site.Component.Dispose();
					}
					this.sites = null;
					this.components = null;
				}
			}
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x000E12D0 File Offset: 0x000DF4D0
		protected virtual object GetService(Type service)
		{
			if (!(service == typeof(IContainer)))
			{
				return null;
			}
			return this;
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000E12E8 File Offset: 0x000DF4E8
		public virtual ComponentCollection Components
		{
			get
			{
				object obj = this.syncObj;
				ComponentCollection result;
				lock (obj)
				{
					if (this.components == null)
					{
						IComponent[] array = new IComponent[this.siteCount];
						for (int i = 0; i < this.siteCount; i++)
						{
							array[i] = this.sites[i].Component;
						}
						this.components = new ComponentCollection(array);
						if (this.filter == null && this.checkedFilter)
						{
							this.checkedFilter = false;
						}
					}
					if (!this.checkedFilter)
					{
						this.filter = (this.GetService(typeof(ContainerFilterService)) as ContainerFilterService);
						this.checkedFilter = true;
					}
					if (this.filter != null)
					{
						ComponentCollection componentCollection = this.filter.FilterComponents(this.components);
						if (componentCollection != null)
						{
							this.components = componentCollection;
						}
					}
					result = this.components;
				}
				return result;
			}
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x000E13D8 File Offset: 0x000DF5D8
		public virtual void Remove(IComponent component)
		{
			this.Remove(component, false);
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x000E13E4 File Offset: 0x000DF5E4
		private void Remove(IComponent component, bool preserveSite)
		{
			object obj = this.syncObj;
			lock (obj)
			{
				if (component != null)
				{
					ISite site = component.Site;
					if (site != null && site.Container == this)
					{
						if (!preserveSite)
						{
							component.Site = null;
						}
						for (int i = 0; i < this.siteCount; i++)
						{
							if (this.sites[i] == site)
							{
								this.siteCount--;
								Array.Copy(this.sites, i + 1, this.sites, i, this.siteCount - i);
								this.sites[this.siteCount] = null;
								this.components = null;
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x000E14A4 File Offset: 0x000DF6A4
		protected void RemoveWithoutUnsiting(IComponent component)
		{
			this.Remove(component, true);
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x000E14B0 File Offset: 0x000DF6B0
		protected virtual void ValidateName(IComponent component, string name)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (name != null)
			{
				for (int i = 0; i < Math.Min(this.siteCount, this.sites.Length); i++)
				{
					ISite site = this.sites[i];
					if (site != null && site.Name != null && string.Equals(site.Name, name, StringComparison.OrdinalIgnoreCase) && site.Component != component)
					{
						InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(site.Component)[typeof(InheritanceAttribute)];
						if (inheritanceAttribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly)
						{
							throw new ArgumentException(SR.GetString("DuplicateComponentName", new object[]
							{
								name
							}));
						}
					}
				}
			}
		}

		// Token: 0x04002966 RID: 10598
		private ISite[] sites;

		// Token: 0x04002967 RID: 10599
		private int siteCount;

		// Token: 0x04002968 RID: 10600
		private ComponentCollection components;

		// Token: 0x04002969 RID: 10601
		private ContainerFilterService filter;

		// Token: 0x0400296A RID: 10602
		private bool checkedFilter;

		// Token: 0x0400296B RID: 10603
		private object syncObj = new object();

		// Token: 0x02000891 RID: 2193
		private class Site : ISite, IServiceProvider
		{
			// Token: 0x06004593 RID: 17811 RVA: 0x00122194 File Offset: 0x00120394
			internal Site(IComponent component, Container container, string name)
			{
				this.component = component;
				this.container = container;
				this.name = name;
			}

			// Token: 0x17000FBB RID: 4027
			// (get) Token: 0x06004594 RID: 17812 RVA: 0x001221B1 File Offset: 0x001203B1
			public IComponent Component
			{
				get
				{
					return this.component;
				}
			}

			// Token: 0x17000FBC RID: 4028
			// (get) Token: 0x06004595 RID: 17813 RVA: 0x001221B9 File Offset: 0x001203B9
			public IContainer Container
			{
				get
				{
					return this.container;
				}
			}

			// Token: 0x06004596 RID: 17814 RVA: 0x001221C1 File Offset: 0x001203C1
			public object GetService(Type service)
			{
				if (!(service == typeof(ISite)))
				{
					return this.container.GetService(service);
				}
				return this;
			}

			// Token: 0x17000FBD RID: 4029
			// (get) Token: 0x06004597 RID: 17815 RVA: 0x001221E3 File Offset: 0x001203E3
			public bool DesignMode
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000FBE RID: 4030
			// (get) Token: 0x06004598 RID: 17816 RVA: 0x001221E6 File Offset: 0x001203E6
			// (set) Token: 0x06004599 RID: 17817 RVA: 0x001221EE File Offset: 0x001203EE
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

			// Token: 0x040037C8 RID: 14280
			private IComponent component;

			// Token: 0x040037C9 RID: 14281
			private Container container;

			// Token: 0x040037CA RID: 14282
			private string name;
		}
	}
}
