using System;
using System.Collections;
using System.Globalization;

namespace System.ComponentModel.Design
{
	// Token: 0x02000569 RID: 1385
	internal sealed class ReferenceService : IReferenceService, IDisposable
	{
		// Token: 0x060030DC RID: 12508 RVA: 0x0011416D File Offset: 0x0011316D
		internal ReferenceService(IServiceProvider provider)
		{
			this._provider = provider;
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x0011417C File Offset: 0x0011317C
		private void CreateReferences(IComponent component)
		{
			this.CreateReferences(string.Empty, component, component);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x0011418C File Offset: 0x0011318C
		private void CreateReferences(string trailingName, object reference, IComponent sitedComponent)
		{
			if (object.ReferenceEquals(reference, null))
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

		// Token: 0x060030DF RID: 12511 RVA: 0x0011423C File Offset: 0x0011323C
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

		// Token: 0x060030E0 RID: 12512 RVA: 0x0011445C File Offset: 0x0011345C
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

		// Token: 0x060030E1 RID: 12513 RVA: 0x001144B4 File Offset: 0x001134B4
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

		// Token: 0x060030E2 RID: 12514 RVA: 0x0011450C File Offset: 0x0011350C
		private void OnComponentRename(object sender, ComponentRenameEventArgs cevent)
		{
			foreach (object obj in this._references)
			{
				ReferenceService.ReferenceHolder referenceHolder = (ReferenceService.ReferenceHolder)obj;
				if (object.ReferenceEquals(referenceHolder.SitedComponent, cevent.Component))
				{
					referenceHolder.ResetName();
					break;
				}
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x0011457C File Offset: 0x0011357C
		private void RemoveReferences(IComponent component)
		{
			if (this._references != null)
			{
				int count = this._references.Count;
				for (int i = count - 1; i >= 0; i--)
				{
					if (object.ReferenceEquals(((ReferenceService.ReferenceHolder)this._references[i]).SitedComponent, component))
					{
						this._references.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x001145D8 File Offset: 0x001135D8
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

		// Token: 0x060030E5 RID: 12517 RVA: 0x00114658 File Offset: 0x00113658
		IComponent IReferenceService.GetComponent(object reference)
		{
			if (object.ReferenceEquals(reference, null))
			{
				throw new ArgumentNullException("reference");
			}
			this.EnsureReferences();
			foreach (object obj in this._references)
			{
				ReferenceService.ReferenceHolder referenceHolder = (ReferenceService.ReferenceHolder)obj;
				if (object.ReferenceEquals(referenceHolder.Reference, reference))
				{
					return referenceHolder.SitedComponent;
				}
			}
			return null;
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x001146E0 File Offset: 0x001136E0
		string IReferenceService.GetName(object reference)
		{
			if (object.ReferenceEquals(reference, null))
			{
				throw new ArgumentNullException("reference");
			}
			this.EnsureReferences();
			foreach (object obj in this._references)
			{
				ReferenceService.ReferenceHolder referenceHolder = (ReferenceService.ReferenceHolder)obj;
				if (object.ReferenceEquals(referenceHolder.Reference, reference))
				{
					return referenceHolder.Name;
				}
			}
			return null;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x00114768 File Offset: 0x00113768
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

		// Token: 0x060030E8 RID: 12520 RVA: 0x001147EC File Offset: 0x001137EC
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

		// Token: 0x060030E9 RID: 12521 RVA: 0x00114838 File Offset: 0x00113838
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

		// Token: 0x040020BD RID: 8381
		private static readonly Attribute[] _attributes = new Attribute[]
		{
			DesignerSerializationVisibilityAttribute.Content
		};

		// Token: 0x040020BE RID: 8382
		private IServiceProvider _provider;

		// Token: 0x040020BF RID: 8383
		private ArrayList _addedComponents;

		// Token: 0x040020C0 RID: 8384
		private ArrayList _removedComponents;

		// Token: 0x040020C1 RID: 8385
		private ArrayList _references;

		// Token: 0x040020C2 RID: 8386
		private bool _populating;

		// Token: 0x0200056A RID: 1386
		private sealed class ReferenceHolder
		{
			// Token: 0x060030EB RID: 12523 RVA: 0x0011490A File Offset: 0x0011390A
			internal ReferenceHolder(string trailingName, object reference, IComponent sitedComponent)
			{
				this._trailingName = trailingName;
				this._reference = reference;
				this._sitedComponent = sitedComponent;
			}

			// Token: 0x060030EC RID: 12524 RVA: 0x00114927 File Offset: 0x00113927
			internal void ResetName()
			{
				this._fullName = null;
			}

			// Token: 0x17000927 RID: 2343
			// (get) Token: 0x060030ED RID: 12525 RVA: 0x00114930 File Offset: 0x00113930
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

			// Token: 0x17000928 RID: 2344
			// (get) Token: 0x060030EE RID: 12526 RVA: 0x0011499F File Offset: 0x0011399F
			internal object Reference
			{
				get
				{
					return this._reference;
				}
			}

			// Token: 0x17000929 RID: 2345
			// (get) Token: 0x060030EF RID: 12527 RVA: 0x001149A7 File Offset: 0x001139A7
			internal IComponent SitedComponent
			{
				get
				{
					return this._sitedComponent;
				}
			}

			// Token: 0x040020C3 RID: 8387
			private string _trailingName;

			// Token: 0x040020C4 RID: 8388
			private object _reference;

			// Token: 0x040020C5 RID: 8389
			private IComponent _sitedComponent;

			// Token: 0x040020C6 RID: 8390
			private string _fullName;
		}
	}
}
