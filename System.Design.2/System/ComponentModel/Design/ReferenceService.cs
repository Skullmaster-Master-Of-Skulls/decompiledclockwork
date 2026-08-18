using System;
using System.Collections;
using System.Globalization;

namespace System.ComponentModel.Design
{
	// Token: 0x020001D1 RID: 465
	internal sealed class ReferenceService : IReferenceService, IDisposable
	{
		// Token: 0x0600113C RID: 4412 RVA: 0x0005F14B File Offset: 0x0005D34B
		internal ReferenceService(IServiceProvider provider)
		{
			this._provider = provider;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0005F15A File Offset: 0x0005D35A
		private void CreateReferences(IComponent component)
		{
			this.CreateReferences(string.Empty, component, component);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0005F16C File Offset: 0x0005D36C
		private void CreateReferences(string trailingName, object reference, IComponent sitedComponent)
		{
			if (reference == null)
			{
				return;
			}
			this._references.Add(new ReferenceService.ReferenceHolder(trailingName, reference, sitedComponent));
			foreach (object obj in TypeDescriptor.GetProperties(reference, ReferenceService._attributes))
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.IsReadOnly)
				{
					this.CreateReferences(string.Format(CultureInfo.CurrentCulture, "{0}.{1}", new object[]
					{
						trailingName,
						propertyDescriptor.Name
					}), propertyDescriptor.GetValue(reference), sitedComponent);
				}
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0005F214 File Offset: 0x0005D414
		private void EnsureReferences()
		{
			if (this._references == null)
			{
				if (this._provider == null)
				{
					throw new ObjectDisposedException("IReferenceService");
				}
				IComponentChangeService componentChangeService = this._provider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdded += this.OnComponentAdded;
					componentChangeService.ComponentRemoved += this.OnComponentRemoved;
					componentChangeService.ComponentRename += this.OnComponentRename;
				}
				IContainer container = this._provider.GetService(typeof(IContainer)) as IContainer;
				if (container == null)
				{
					throw new InvalidOperationException();
				}
				this._references = new ArrayList(container.Components.Count);
				using (IEnumerator enumerator = container.Components.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						IComponent component = (IComponent)obj;
						this.CreateReferences(component);
					}
					return;
				}
			}
			if (!this._populating)
			{
				this._populating = true;
				try
				{
					if (this._addedComponents != null && this._addedComponents.Count > 0)
					{
						foreach (object obj2 in this._addedComponents)
						{
							IComponent component2 = (IComponent)obj2;
							this.RemoveReferences(component2);
							this.CreateReferences(component2);
						}
						this._addedComponents.Clear();
					}
					if (this._removedComponents != null && this._removedComponents.Count > 0)
					{
						foreach (object obj3 in this._removedComponents)
						{
							IComponent component3 = (IComponent)obj3;
							this.RemoveReferences(component3);
						}
						this._removedComponents.Clear();
					}
				}
				finally
				{
					this._populating = false;
				}
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0005F434 File Offset: 0x0005D634
		private void OnComponentAdded(object sender, ComponentEventArgs cevent)
		{
			if (this._addedComponents == null)
			{
				this._addedComponents = new ArrayList();
			}
			IComponent component = cevent.Component;
			if (!(component.Site is INestedSite))
			{
				this._addedComponents.Add(component);
				if (this._removedComponents != null)
				{
					this._removedComponents.Remove(component);
				}
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0005F48C File Offset: 0x0005D68C
		private void OnComponentRemoved(object sender, ComponentEventArgs cevent)
		{
			if (this._removedComponents == null)
			{
				this._removedComponents = new ArrayList();
			}
			IComponent component = cevent.Component;
			if (!(component.Site is INestedSite))
			{
				this._removedComponents.Add(component);
				if (this._addedComponents != null)
				{
					this._addedComponents.Remove(component);
				}
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0005F4E4 File Offset: 0x0005D6E4
		private void OnComponentRename(object sender, ComponentRenameEventArgs cevent)
		{
			foreach (object obj in this._references)
			{
				ReferenceService.ReferenceHolder referenceHolder = (ReferenceService.ReferenceHolder)obj;
				if (referenceHolder.SitedComponent == cevent.Component)
				{
					referenceHolder.ResetName();
					break;
				}
			}
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0005F54C File Offset: 0x0005D74C
		private void RemoveReferences(IComponent component)
		{
			if (this._references != null)
			{
				int count = this._references.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					if (((ReferenceService.ReferenceHolder)this._references[i]).SitedComponent == component)
					{
						this._references.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0005F5A0 File Offset: 0x0005D7A0
		void IDisposable.Dispose()
		{
			if (this._references != null && this._provider != null)
			{
				IComponentChangeService componentChangeService = this._provider.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdded -= this.OnComponentAdded;
					componentChangeService.ComponentRemoved -= this.OnComponentRemoved;
					componentChangeService.ComponentRename -= this.OnComponentRename;
				}
				this._references = null;
				this._provider = null;
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0005F620 File Offset: 0x0005D820
		IComponent IReferenceService.GetComponent(object reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			this.EnsureReferences();
			foreach (object obj in this._references)
			{
				ReferenceService.ReferenceHolder referenceHolder = (ReferenceService.ReferenceHolder)obj;
				if (referenceHolder.Reference == reference)
				{
					return referenceHolder.SitedComponent;
				}
			}
			return null;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0005F69C File Offset: 0x0005D89C
		string IReferenceService.GetName(object reference)
		{
			if (reference == null)
			{
				throw new ArgumentNullException("reference");
			}
			this.EnsureReferences();
			foreach (object obj in this._references)
			{
				ReferenceService.ReferenceHolder referenceHolder = (ReferenceService.ReferenceHolder)obj;
				if (referenceHolder.Reference == reference)
				{
					return referenceHolder.Name;
				}
			}
			return null;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0005F718 File Offset: 0x0005D918
		object IReferenceService.GetReference(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.EnsureReferences();
			foreach (object obj in this._references)
			{
				ReferenceService.ReferenceHolder referenceHolder = (ReferenceService.ReferenceHolder)obj;
				if (string.Equals(referenceHolder.Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return referenceHolder.Reference;
				}
			}
			return null;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0005F79C File Offset: 0x0005D99C
		object[] IReferenceService.GetReferences()
		{
			this.EnsureReferences();
			object[] array = new object[this._references.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ((ReferenceService.ReferenceHolder)this._references[i]).Reference;
			}
			return array;
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0005F7E8 File Offset: 0x0005D9E8
		object[] IReferenceService.GetReferences(Type baseType)
		{
			if (baseType == null)
			{
				throw new ArgumentNullException("baseType");
			}
			this.EnsureReferences();
			ArrayList arrayList = new ArrayList(this._references.Count);
			foreach (object obj in this._references)
			{
				ReferenceService.ReferenceHolder referenceHolder = (ReferenceService.ReferenceHolder)obj;
				object reference = referenceHolder.Reference;
				if (baseType.IsAssignableFrom(reference.GetType()))
				{
					arrayList.Add(reference);
				}
			}
			object[] array = new object[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x040009B5 RID: 2485
		private static readonly Attribute[] _attributes = new Attribute[]
		{
			DesignerSerializationVisibilityAttribute.Content
		};

		// Token: 0x040009B6 RID: 2486
		private IServiceProvider _provider;

		// Token: 0x040009B7 RID: 2487
		private ArrayList _addedComponents;

		// Token: 0x040009B8 RID: 2488
		private ArrayList _removedComponents;

		// Token: 0x040009B9 RID: 2489
		private ArrayList _references;

		// Token: 0x040009BA RID: 2490
		private bool _populating;

		// Token: 0x0200049E RID: 1182
		private sealed class ReferenceHolder
		{
			// Token: 0x06002B83 RID: 11139 RVA: 0x00103F45 File Offset: 0x00102145
			internal ReferenceHolder(string trailingName, object reference, IComponent sitedComponent)
			{
				this._trailingName = trailingName;
				this._reference = reference;
				this._sitedComponent = sitedComponent;
			}

			// Token: 0x06002B84 RID: 11140 RVA: 0x00103F62 File Offset: 0x00102162
			internal void ResetName()
			{
				this._fullName = null;
			}

			// Token: 0x17000930 RID: 2352
			// (get) Token: 0x06002B85 RID: 11141 RVA: 0x00103F6C File Offset: 0x0010216C
			internal string Name
			{
				get
				{
					if (this._fullName == null)
					{
						if (this._sitedComponent != null)
						{
							string componentName = TypeDescriptor.GetComponentName(this._sitedComponent);
							if (componentName != null)
							{
								this._fullName = string.Format(CultureInfo.CurrentCulture, "{0}{1}", new object[]
								{
									componentName,
									this._trailingName
								});
							}
						}
						if (this._fullName == null)
						{
							this._fullName = string.Empty;
						}
					}
					return this._fullName;
				}
			}

			// Token: 0x17000931 RID: 2353
			// (get) Token: 0x06002B86 RID: 11142 RVA: 0x00103FD9 File Offset: 0x001021D9
			internal object Reference
			{
				get
				{
					return this._reference;
				}
			}

			// Token: 0x17000932 RID: 2354
			// (get) Token: 0x06002B87 RID: 11143 RVA: 0x00103FE1 File Offset: 0x001021E1
			internal IComponent SitedComponent
			{
				get
				{
					return this._sitedComponent;
				}
			}

			// Token: 0x04001E2E RID: 7726
			private string _trailingName;

			// Token: 0x04001E2F RID: 7727
			private object _reference;

			// Token: 0x04001E30 RID: 7728
			private IComponent _sitedComponent;

			// Token: 0x04001E31 RID: 7729
			private string _fullName;
		}
	}
}
