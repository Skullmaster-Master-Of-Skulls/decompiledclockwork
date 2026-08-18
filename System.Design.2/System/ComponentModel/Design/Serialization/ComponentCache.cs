using System;
using System.CodeDom;
using System.Collections.Generic;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001DB RID: 475
	internal class ComponentCache : IDisposable
	{
		// Token: 0x060011F5 RID: 4597 RVA: 0x000667A4 File Offset: 0x000649A4
		internal ComponentCache(IDesignerSerializationManager manager)
		{
			this.serManager = manager;
			IComponentChangeService componentChangeService = (IComponentChangeService)manager.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentChanging += this.OnComponentChanging;
				componentChangeService.ComponentChanged += this.OnComponentChanged;
				componentChangeService.ComponentRemoving += this.OnComponentRemove;
				componentChangeService.ComponentRemoved += this.OnComponentRemove;
				componentChangeService.ComponentRename += this.OnComponentRename;
			}
			DesignerOptionService designerOptionService = manager.GetService(typeof(DesignerOptionService)) as DesignerOptionService;
			object obj = null;
			if (designerOptionService != null)
			{
				PropertyDescriptor propertyDescriptor = designerOptionService.Options.Properties["UseOptimizedCodeGeneration"];
				if (propertyDescriptor != null)
				{
					obj = propertyDescriptor.GetValue(null);
				}
				if (obj != null && obj is bool)
				{
					this.enabled = (bool)obj;
				}
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0006688B File Offset: 0x00064A8B
		internal bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x17000400 RID: 1024
		internal ComponentCache.Entry this[object component]
		{
			get
			{
				if (component == null)
				{
					throw new ArgumentNullException("component");
				}
				ComponentCache.Entry entry;
				if (this.cache != null && this.cache.TryGetValue(component, out entry) && entry != null && entry.Valid && this.Enabled)
				{
					return entry;
				}
				return null;
			}
			set
			{
				if (this.cache == null && this.Enabled)
				{
					this.cache = new Dictionary<object, ComponentCache.Entry>();
				}
				if (this.cache != null && component is IComponent)
				{
					if (value != null && value.Component == null)
					{
						value.Component = component;
					}
					this.cache[component] = value;
				}
			}
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x00066938 File Offset: 0x00064B38
		internal ComponentCache.Entry GetEntryAll(object component)
		{
			ComponentCache.Entry result = null;
			if (this.cache != null && this.cache.TryGetValue(component, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x00066964 File Offset: 0x00064B64
		internal bool ContainsLocalName(string name)
		{
			if (this.cache == null)
			{
				return false;
			}
			foreach (KeyValuePair<object, ComponentCache.Entry> keyValuePair in this.cache)
			{
				List<string> localNames = keyValuePair.Value.LocalNames;
				if (localNames != null && localNames.Contains(name))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000669DC File Offset: 0x00064BDC
		public void Dispose()
		{
			if (this.serManager != null)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.serManager.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentChanging -= this.OnComponentChanging;
					componentChangeService.ComponentChanged -= this.OnComponentChanged;
					componentChangeService.ComponentRemoving -= this.OnComponentRemove;
					componentChangeService.ComponentRemoved -= this.OnComponentRemove;
					componentChangeService.ComponentRename -= this.OnComponentRename;
				}
			}
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00066A69 File Offset: 0x00064C69
		private void OnComponentRename(object source, ComponentRenameEventArgs args)
		{
			if (this.cache != null)
			{
				this.cache.Clear();
			}
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00066A80 File Offset: 0x00064C80
		private void OnComponentChanging(object source, ComponentChangingEventArgs ce)
		{
			if (this.cache != null)
			{
				if (ce.Component != null)
				{
					this.RemoveEntry(ce.Component);
					if (!(ce.Component is IComponent) && this.serManager != null)
					{
						IReferenceService referenceService = this.serManager.GetService(typeof(IReferenceService)) as IReferenceService;
						if (referenceService != null)
						{
							IComponent component = referenceService.GetComponent(ce.Component);
							if (component != null)
							{
								this.RemoveEntry(component);
								return;
							}
							this.cache.Clear();
							return;
						}
					}
				}
				else
				{
					this.cache.Clear();
				}
			}
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00066B0C File Offset: 0x00064D0C
		private void OnComponentChanged(object source, ComponentChangedEventArgs ce)
		{
			if (this.cache != null)
			{
				if (ce.Component != null)
				{
					this.RemoveEntry(ce.Component);
					if (!(ce.Component is IComponent) && this.serManager != null)
					{
						IReferenceService referenceService = this.serManager.GetService(typeof(IReferenceService)) as IReferenceService;
						if (referenceService != null)
						{
							IComponent component = referenceService.GetComponent(ce.Component);
							if (component != null)
							{
								this.RemoveEntry(component);
								return;
							}
							this.cache.Clear();
							return;
						}
					}
				}
				else
				{
					this.cache.Clear();
				}
			}
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00066B97 File Offset: 0x00064D97
		private void OnComponentRemove(object source, ComponentEventArgs ce)
		{
			if (this.cache != null)
			{
				if (ce.Component != null && !(ce.Component is IExtenderProvider))
				{
					this.RemoveEntry(ce.Component);
					return;
				}
				this.cache.Clear();
			}
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x00066BD0 File Offset: 0x00064DD0
		internal void RemoveEntry(object component)
		{
			ComponentCache.Entry entry = null;
			if (this.cache != null && this.cache.TryGetValue(component, out entry))
			{
				if (entry.Tracking)
				{
					this.cache.Clear();
					return;
				}
				this.cache.Remove(component);
				if (entry.Dependencies != null)
				{
					foreach (object component2 in entry.Dependencies)
					{
						this.RemoveEntry(component2);
					}
				}
			}
		}

		// Token: 0x040009E5 RID: 2533
		private Dictionary<object, ComponentCache.Entry> cache;

		// Token: 0x040009E6 RID: 2534
		private IDesignerSerializationManager serManager;

		// Token: 0x040009E7 RID: 2535
		private bool enabled = true;

		// Token: 0x020004A5 RID: 1189
		internal struct ResourceEntry
		{
			// Token: 0x04001E43 RID: 7747
			public bool ForceInvariant;

			// Token: 0x04001E44 RID: 7748
			public bool EnsureInvariant;

			// Token: 0x04001E45 RID: 7749
			public bool ShouldSerializeValue;

			// Token: 0x04001E46 RID: 7750
			public string Name;

			// Token: 0x04001E47 RID: 7751
			public object Value;

			// Token: 0x04001E48 RID: 7752
			public PropertyDescriptor PropertyDescriptor;

			// Token: 0x04001E49 RID: 7753
			public ExpressionContext ExpressionContext;
		}

		// Token: 0x020004A6 RID: 1190
		internal sealed class Entry
		{
			// Token: 0x06002B9F RID: 11167 RVA: 0x00104A6E File Offset: 0x00102C6E
			internal Entry(ComponentCache cache)
			{
				this.cache = cache;
				this.valid = true;
			}

			// Token: 0x17000936 RID: 2358
			// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x00104A84 File Offset: 0x00102C84
			public ICollection<ComponentCache.ResourceEntry> Metadata
			{
				get
				{
					return this.metadata;
				}
			}

			// Token: 0x17000937 RID: 2359
			// (get) Token: 0x06002BA1 RID: 11169 RVA: 0x00104A8C File Offset: 0x00102C8C
			public ICollection<ComponentCache.ResourceEntry> Resources
			{
				get
				{
					return this.resources;
				}
			}

			// Token: 0x17000938 RID: 2360
			// (get) Token: 0x06002BA2 RID: 11170 RVA: 0x00104A94 File Offset: 0x00102C94
			public List<object> Dependencies
			{
				get
				{
					return this.dependencies;
				}
			}

			// Token: 0x17000939 RID: 2361
			// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x00104A9C File Offset: 0x00102C9C
			internal List<string> LocalNames
			{
				get
				{
					return this.localNames;
				}
			}

			// Token: 0x1700093A RID: 2362
			// (get) Token: 0x06002BA4 RID: 11172 RVA: 0x00104AA4 File Offset: 0x00102CA4
			// (set) Token: 0x06002BA5 RID: 11173 RVA: 0x00104AAC File Offset: 0x00102CAC
			internal bool Valid
			{
				get
				{
					return this.valid;
				}
				set
				{
					this.valid = value;
				}
			}

			// Token: 0x1700093B RID: 2363
			// (get) Token: 0x06002BA6 RID: 11174 RVA: 0x00104AB5 File Offset: 0x00102CB5
			// (set) Token: 0x06002BA7 RID: 11175 RVA: 0x00104ABD File Offset: 0x00102CBD
			internal bool Tracking
			{
				get
				{
					return this.tracking;
				}
				set
				{
					this.tracking = value;
				}
			}

			// Token: 0x06002BA8 RID: 11176 RVA: 0x00104AC6 File Offset: 0x00102CC6
			internal void AddLocalName(string name)
			{
				if (this.localNames == null)
				{
					this.localNames = new List<string>();
				}
				this.localNames.Add(name);
			}

			// Token: 0x06002BA9 RID: 11177 RVA: 0x00104AE7 File Offset: 0x00102CE7
			public void AddDependency(object dep)
			{
				if (this.dependencies == null)
				{
					this.dependencies = new List<object>();
				}
				if (!this.dependencies.Contains(dep))
				{
					this.dependencies.Add(dep);
				}
			}

			// Token: 0x06002BAA RID: 11178 RVA: 0x00104B16 File Offset: 0x00102D16
			public void AddMetadata(ComponentCache.ResourceEntry re)
			{
				if (this.metadata == null)
				{
					this.metadata = new List<ComponentCache.ResourceEntry>();
				}
				this.metadata.Add(re);
			}

			// Token: 0x06002BAB RID: 11179 RVA: 0x00104B37 File Offset: 0x00102D37
			public void AddResource(ComponentCache.ResourceEntry re)
			{
				if (this.resources == null)
				{
					this.resources = new List<ComponentCache.ResourceEntry>();
				}
				this.resources.Add(re);
			}

			// Token: 0x04001E4A RID: 7754
			private ComponentCache cache;

			// Token: 0x04001E4B RID: 7755
			private List<object> dependencies;

			// Token: 0x04001E4C RID: 7756
			private List<string> localNames;

			// Token: 0x04001E4D RID: 7757
			private List<ComponentCache.ResourceEntry> resources;

			// Token: 0x04001E4E RID: 7758
			private List<ComponentCache.ResourceEntry> metadata;

			// Token: 0x04001E4F RID: 7759
			private bool valid;

			// Token: 0x04001E50 RID: 7760
			private bool tracking;

			// Token: 0x04001E51 RID: 7761
			public object Component;

			// Token: 0x04001E52 RID: 7762
			public CodeStatementCollection Statements;
		}
	}
}
