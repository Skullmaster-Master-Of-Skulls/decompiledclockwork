using System;
using System.Collections;
using System.Configuration;
using System.Design;
using System.Globalization;
using System.Windows.Forms.Design;
using Microsoft.Internal.Performance;

namespace System.ComponentModel.Design
{
	// Token: 0x02000199 RID: 409
	public class ComponentDesigner : ITreeDesigner, IDesigner, IDisposable, IDesignerFilter, IComponentInitializer
	{
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0005677A File Offset: 0x0005497A
		public virtual DesignerActionListCollection ActionLists
		{
			get
			{
				if (this.actionLists == null)
				{
					this.actionLists = new DesignerActionListCollection();
				}
				return this.actionLists;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00056795 File Offset: 0x00054995
		public virtual ICollection AssociatedComponents
		{
			get
			{
				return new IComponent[0];
			}
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x00003B0F File Offset: 0x00001D0F
		internal virtual bool CanBeAssociatedWith(IDesigner parentDesigner)
		{
			return true;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0005679D File Offset: 0x0005499D
		public IComponent Component
		{
			get
			{
				return this.component;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x000567A5 File Offset: 0x000549A5
		protected bool Inherited
		{
			get
			{
				return !this.InheritanceAttribute.Equals(InheritanceAttribute.NotInherited);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x000567BC File Offset: 0x000549BC
		protected virtual IComponent ParentComponent
		{
			get
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				IComponent rootComponent = designerHost.RootComponent;
				if (rootComponent == this.Component)
				{
					return null;
				}
				return rootComponent;
			}
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000567F2 File Offset: 0x000549F2
		protected InheritanceAttribute InvokeGetInheritanceAttribute(ComponentDesigner toInvoke)
		{
			return toInvoke.InheritanceAttribute;
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x000567FC File Offset: 0x000549FC
		protected virtual InheritanceAttribute InheritanceAttribute
		{
			get
			{
				if (this.inheritanceAttribute == null)
				{
					IInheritanceService inheritanceService = (IInheritanceService)this.GetService(typeof(IInheritanceService));
					if (inheritanceService != null)
					{
						this.inheritanceAttribute = inheritanceService.GetInheritanceAttribute(this.Component);
					}
					else
					{
						this.inheritanceAttribute = InheritanceAttribute.Default;
					}
				}
				return this.inheritanceAttribute;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00056850 File Offset: 0x00054A50
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x0005696C File Offset: 0x00054B6C
		private string SettingsKey
		{
			get
			{
				if (string.IsNullOrEmpty((string)this.ShadowProperties["SettingsKey"]))
				{
					IPersistComponentSettings persistComponentSettings = this.Component as IPersistComponentSettings;
					IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
					IComponent component = (designerHost != null) ? designerHost.RootComponent : null;
					if (persistComponentSettings != null && component != null)
					{
						if (string.IsNullOrEmpty(persistComponentSettings.SettingsKey))
						{
							if (component != null && component != persistComponentSettings)
							{
								this.ShadowProperties["SettingsKey"] = string.Format(CultureInfo.CurrentCulture, "{0}.{1}", new object[]
								{
									component.Site.Name,
									this.Component.Site.Name
								});
							}
							else
							{
								this.ShadowProperties["SettingsKey"] = this.Component.Site.Name;
							}
						}
						persistComponentSettings.SettingsKey = (this.ShadowProperties["SettingsKey"] as string);
						return persistComponentSettings.SettingsKey;
					}
				}
				return this.ShadowProperties["SettingsKey"] as string;
			}
			set
			{
				this.ShadowProperties["SettingsKey"] = value;
				this.settingsKeyExplicitlySet = true;
				IPersistComponentSettings persistComponentSettings = this.Component as IPersistComponentSettings;
				if (persistComponentSettings != null)
				{
					persistComponentSettings.SettingsKey = value;
				}
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x000569A7 File Offset: 0x00054BA7
		protected ComponentDesigner.ShadowPropertyCollection ShadowProperties
		{
			get
			{
				if (this.shadowProperties == null)
				{
					this.shadowProperties = new ComponentDesigner.ShadowPropertyCollection(this);
				}
				return this.shadowProperties;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000569C3 File Offset: 0x00054BC3
		public virtual DesignerVerbCollection Verbs
		{
			get
			{
				if (this.verbs == null)
				{
					this.verbs = new DesignerVerbCollection();
				}
				return this.verbs;
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x000569E0 File Offset: 0x00054BE0
		private void OnComponentRename(object sender, ComponentRenameEventArgs e)
		{
			if (this.Component is IPersistComponentSettings)
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				IComponent component = (designerHost != null) ? designerHost.RootComponent : null;
				if (!this.settingsKeyExplicitlySet && (e.Component == this.Component || e.Component == component))
				{
					this.ResetSettingsKey();
				}
			}
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00056A42 File Offset: 0x00054C42
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00056A54 File Offset: 0x00054C54
		~ComponentDesigner()
		{
			this.Dispose(false);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00056A84 File Offset: 0x00054C84
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentRename -= this.OnComponentRename;
				}
				this.component = null;
				this.inheritedProps = null;
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00056AD0 File Offset: 0x00054CD0
		public virtual void DoDefaultAction()
		{
			IEventBindingService eventBindingService = (IEventBindingService)this.GetService(typeof(IEventBindingService));
			if (eventBindingService == null)
			{
				return;
			}
			ISelectionService selectionService = (ISelectionService)this.GetService(typeof(ISelectionService));
			if (selectionService == null)
			{
				return;
			}
			ICollection selectedComponents = selectionService.GetSelectedComponents();
			EventDescriptor eventDescriptor = null;
			string text = null;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			DesignerTransaction designerTransaction = null;
			try
			{
				foreach (object obj in selectedComponents)
				{
					if (obj is IComponent)
					{
						EventDescriptor defaultEvent = TypeDescriptor.GetDefaultEvent(obj);
						PropertyDescriptor propertyDescriptor = null;
						string text2 = null;
						bool flag = false;
						if (defaultEvent != null)
						{
							propertyDescriptor = eventBindingService.GetEventProperty(defaultEvent);
						}
						if (propertyDescriptor != null && !propertyDescriptor.IsReadOnly)
						{
							try
							{
								if (designerHost != null && designerTransaction == null)
								{
									designerTransaction = designerHost.CreateTransaction(SR.GetString("ComponentDesignerAddEvent", new object[]
									{
										defaultEvent.Name
									}));
								}
							}
							catch (CheckoutException ex)
							{
								if (ex == CheckoutException.Canceled)
								{
									return;
								}
								throw ex;
							}
							text2 = (string)propertyDescriptor.GetValue(obj);
							if (text2 == null)
							{
								flag = true;
								text2 = eventBindingService.CreateUniqueMethodName((IComponent)obj, defaultEvent);
							}
							else
							{
								flag = true;
								foreach (object obj2 in eventBindingService.GetCompatibleMethods(defaultEvent))
								{
									string b = (string)obj2;
									if (text2 == b)
									{
										flag = false;
										break;
									}
								}
							}
							ComponentDesigner.codemarkers.CodeMarker(7511);
							if (flag && propertyDescriptor != null)
							{
								propertyDescriptor.SetValue(obj, text2);
							}
							if (this.component == obj)
							{
								eventDescriptor = defaultEvent;
								text = text2;
							}
						}
					}
				}
			}
			catch (InvalidOperationException)
			{
				if (designerTransaction != null)
				{
					designerTransaction.Cancel();
					designerTransaction = null;
				}
			}
			finally
			{
				if (designerTransaction != null)
				{
					designerTransaction.Commit();
				}
			}
			if (text != null && eventDescriptor != null)
			{
				eventBindingService.ShowCode(this.component, eventDescriptor);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00056D50 File Offset: 0x00054F50
		internal bool IsRootDesigner
		{
			get
			{
				bool result = false;
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null && this.component == designerHost.RootComponent)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00056D8C File Offset: 0x00054F8C
		public virtual void Initialize(IComponent component)
		{
			this.component = component;
			bool flag = false;
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null && component == designerHost.RootComponent)
			{
				flag = true;
			}
			IServiceContainer serviceContainer = component.Site as IServiceContainer;
			if (serviceContainer != null && this.GetService(typeof(DesignerCommandSet)) == null)
			{
				serviceContainer.AddService(typeof(DesignerCommandSet), new ComponentDesigner.CDDesignerCommandSet(this));
			}
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.ComponentRename += this.OnComponentRename;
			}
			if (flag || !this.InheritanceAttribute.Equals(InheritanceAttribute.NotInherited))
			{
				this.InitializeInheritedProperties(flag);
			}
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x00056E44 File Offset: 0x00055044
		public virtual void InitializeExistingComponent(IDictionary defaultValues)
		{
			this.InitializeNonDefault();
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x00056E4C File Offset: 0x0005504C
		public virtual void InitializeNewComponent(IDictionary defaultValues)
		{
			DesignerActionUIService designerActionUIService = (DesignerActionUIService)this.GetService(typeof(DesignerActionUIService));
			if (designerActionUIService != null && designerActionUIService.ShouldAutoShow(this.Component))
			{
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (designerHost != null && designerHost.InTransaction)
				{
					designerHost.TransactionClosed += this.ShowDesignerActionUI;
				}
				else
				{
					designerActionUIService.ShowUI(this.Component);
				}
			}
			this.OnSetComponentDefaults();
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x00056EC8 File Offset: 0x000550C8
		private void ShowDesignerActionUI(object sender, DesignerTransactionCloseEventArgs e)
		{
			DesignerActionUIService designerActionUIService = (DesignerActionUIService)this.GetService(typeof(DesignerActionUIService));
			if (designerActionUIService != null)
			{
				designerActionUIService.ShowUI(this.Component);
			}
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			if (designerHost != null)
			{
				designerHost.TransactionClosed -= this.ShowDesignerActionUI;
			}
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x00056F28 File Offset: 0x00055128
		private void InitializeInheritedProperties(bool rootComponent)
		{
			Hashtable hashtable = new Hashtable();
			if (!this.InheritanceAttribute.Equals(InheritanceAttribute.InheritedReadOnly))
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.Component);
				PropertyDescriptor[] array = new PropertyDescriptor[properties.Count];
				properties.CopyTo(array, 0);
				foreach (PropertyDescriptor propertyDescriptor in array)
				{
					if (!object.Equals(propertyDescriptor.Attributes[typeof(DesignOnlyAttribute)], DesignOnlyAttribute.Yes) && (propertyDescriptor.SerializationVisibility != DesignerSerializationVisibility.Hidden || propertyDescriptor.IsBrowsable) && (PropertyDescriptor)hashtable[propertyDescriptor.Name] == null)
					{
						hashtable[propertyDescriptor.Name] = new InheritedPropertyDescriptor(propertyDescriptor, this.component, rootComponent);
					}
				}
			}
			this.inheritedProps = hashtable;
			TypeDescriptor.Refresh(this.Component);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x00003937 File Offset: 0x00001B37
		[Obsolete("This method has been deprecated. Use InitializeExistingComponent instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual void InitializeNonDefault()
		{
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x00057004 File Offset: 0x00055204
		protected virtual object GetService(Type serviceType)
		{
			if (this.component != null)
			{
				ISite site = this.component.Site;
				if (site != null)
				{
					return site.GetService(serviceType);
				}
			}
			return null;
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x00057034 File Offset: 0x00055234
		private Attribute[] NonBrowsableAttributes(EventDescriptor e)
		{
			Attribute[] array = new Attribute[e.Attributes.Count];
			e.Attributes.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null && typeof(BrowsableAttribute).IsInstanceOfType(array[i]) && ((BrowsableAttribute)array[i]).Browsable)
				{
					array[i] = BrowsableAttribute.No;
					return array;
				}
			}
			Attribute[] array2 = new Attribute[array.Length + 1];
			Array.Copy(array, 0, array2, 0, array.Length);
			array2[array.Length] = BrowsableAttribute.No;
			return array2;
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000570C4 File Offset: 0x000552C4
		[Obsolete("This method has been deprecated. Use InitializeNewComponent instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public virtual void OnSetComponentDefaults()
		{
			ISite site = this.Component.Site;
			if (site != null)
			{
				IComponent component = this.Component;
				PropertyDescriptor defaultProperty = TypeDescriptor.GetDefaultProperty(component);
				if (defaultProperty != null && defaultProperty.PropertyType.Equals(typeof(string)))
				{
					string text = (string)defaultProperty.GetValue(component);
					if (text == null || text.Length == 0)
					{
						defaultProperty.SetValue(component, site.Name);
					}
				}
			}
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x00057130 File Offset: 0x00055330
		internal virtual void ShowContextMenu(int x, int y)
		{
			IMenuCommandService menuCommandService = (IMenuCommandService)this.GetService(typeof(IMenuCommandService));
			if (menuCommandService != null)
			{
				menuCommandService.ShowContextMenu(MenuCommands.SelectionMenu, x, y);
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x00057164 File Offset: 0x00055364
		protected virtual void PostFilterAttributes(IDictionary attributes)
		{
			if (attributes.Contains(typeof(InheritanceAttribute)))
			{
				this.inheritanceAttribute = (attributes[typeof(InheritanceAttribute)] as InheritanceAttribute);
				return;
			}
			if (!this.InheritanceAttribute.Equals(InheritanceAttribute.NotInherited))
			{
				attributes[typeof(InheritanceAttribute)] = this.InheritanceAttribute;
			}
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000571C8 File Offset: 0x000553C8
		protected virtual void PostFilterEvents(IDictionary events)
		{
			if (this.InheritanceAttribute.Equals(InheritanceAttribute.InheritedReadOnly))
			{
				EventDescriptor[] array = new EventDescriptor[events.Values.Count];
				events.Values.CopyTo(array, 0);
				foreach (EventDescriptor eventDescriptor in array)
				{
					events[eventDescriptor.Name] = TypeDescriptor.CreateEvent(eventDescriptor.ComponentType, eventDescriptor, new Attribute[]
					{
						ReadOnlyAttribute.Yes
					});
				}
			}
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x00057240 File Offset: 0x00055440
		protected virtual void PostFilterProperties(IDictionary properties)
		{
			if (this.inheritedProps != null)
			{
				bool flag = this.InheritanceAttribute.Equals(InheritanceAttribute.InheritedReadOnly);
				if (flag)
				{
					PropertyDescriptor[] array = new PropertyDescriptor[properties.Values.Count];
					properties.Values.CopyTo(array, 0);
					foreach (PropertyDescriptor propertyDescriptor in array)
					{
						properties[propertyDescriptor.Name] = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
						{
							ReadOnlyAttribute.Yes
						});
					}
					return;
				}
				foreach (object obj in this.inheritedProps)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					InheritedPropertyDescriptor inheritedPropertyDescriptor = dictionaryEntry.Value as InheritedPropertyDescriptor;
					if (inheritedPropertyDescriptor != null)
					{
						PropertyDescriptor propertyDescriptor2 = (PropertyDescriptor)properties[dictionaryEntry.Key];
						if (propertyDescriptor2 != null)
						{
							inheritedPropertyDescriptor.PropertyDescriptor = propertyDescriptor2;
							properties[dictionaryEntry.Key] = inheritedPropertyDescriptor;
						}
					}
				}
			}
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void PreFilterAttributes(IDictionary attributes)
		{
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void PreFilterEvents(IDictionary events)
		{
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00057354 File Offset: 0x00055554
		protected virtual void PreFilterProperties(IDictionary properties)
		{
			if (this.Component is IPersistComponentSettings)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)properties["SettingsKey"];
				if (propertyDescriptor != null)
				{
					properties["SettingsKey"] = TypeDescriptor.CreateProperty(typeof(ComponentDesigner), propertyDescriptor, new Attribute[0]);
				}
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000573A4 File Offset: 0x000555A4
		protected void RaiseComponentChanged(MemberDescriptor member, object oldValue, object newValue)
		{
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.OnComponentChanged(this.Component, member, oldValue, newValue);
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000573DC File Offset: 0x000555DC
		protected void RaiseComponentChanging(MemberDescriptor member)
		{
			IComponentChangeService componentChangeService = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
			if (componentChangeService != null)
			{
				componentChangeService.OnComponentChanging(this.Component, member);
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0005740F File Offset: 0x0005560F
		private void ResetSettingsKey()
		{
			if (this.Component is IPersistComponentSettings)
			{
				this.SettingsKey = null;
				this.settingsKeyExplicitlySet = false;
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0005742C File Offset: 0x0005562C
		private bool ShouldSerializeSettingsKey()
		{
			IPersistComponentSettings persistComponentSettings = this.Component as IPersistComponentSettings;
			return persistComponentSettings != null && (this.settingsKeyExplicitlySet || (persistComponentSettings.SaveSettings && !string.IsNullOrEmpty(this.SettingsKey)));
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0005746C File Offset: 0x0005566C
		void IDesignerFilter.PostFilterAttributes(IDictionary attributes)
		{
			this.PostFilterAttributes(attributes);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00057475 File Offset: 0x00055675
		void IDesignerFilter.PostFilterEvents(IDictionary events)
		{
			this.PostFilterEvents(events);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0005747E File Offset: 0x0005567E
		void IDesignerFilter.PostFilterProperties(IDictionary properties)
		{
			this.PostFilterProperties(properties);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00057487 File Offset: 0x00055687
		void IDesignerFilter.PreFilterAttributes(IDictionary attributes)
		{
			this.PreFilterAttributes(attributes);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00057490 File Offset: 0x00055690
		void IDesignerFilter.PreFilterEvents(IDictionary events)
		{
			this.PreFilterEvents(events);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00057499 File Offset: 0x00055699
		void IDesignerFilter.PreFilterProperties(IDictionary properties)
		{
			this.PreFilterProperties(properties);
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x000574A4 File Offset: 0x000556A4
		ICollection ITreeDesigner.Children
		{
			get
			{
				ICollection associatedComponents = this.AssociatedComponents;
				IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
				if (associatedComponents.Count > 0 && designerHost != null)
				{
					IDesigner[] array = new IDesigner[associatedComponents.Count];
					int num = 0;
					foreach (object obj in associatedComponents)
					{
						IComponent component = (IComponent)obj;
						array[num] = designerHost.GetDesigner(component);
						if (array[num] != null)
						{
							num++;
						}
					}
					if (num < array.Length)
					{
						IDesigner[] array2 = new IDesigner[num];
						Array.Copy(array, 0, array2, 0, num);
						array = array2;
					}
					return array;
				}
				return new object[0];
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x00057568 File Offset: 0x00055768
		IDesigner ITreeDesigner.Parent
		{
			get
			{
				IComponent parentComponent = this.ParentComponent;
				if (parentComponent != null)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						return designerHost.GetDesigner(parentComponent);
					}
				}
				return null;
			}
		}

		// Token: 0x040008DB RID: 2267
		private IComponent component;

		// Token: 0x040008DC RID: 2268
		private InheritanceAttribute inheritanceAttribute;

		// Token: 0x040008DD RID: 2269
		private Hashtable inheritedProps;

		// Token: 0x040008DE RID: 2270
		private DesignerVerbCollection verbs;

		// Token: 0x040008DF RID: 2271
		private DesignerActionListCollection actionLists;

		// Token: 0x040008E0 RID: 2272
		private ComponentDesigner.ShadowPropertyCollection shadowProperties;

		// Token: 0x040008E1 RID: 2273
		private bool settingsKeyExplicitlySet;

		// Token: 0x040008E2 RID: 2274
		private static CodeMarkers codemarkers = CodeMarkers.Instance;

		// Token: 0x02000481 RID: 1153
		private class CDDesignerCommandSet : DesignerCommandSet
		{
			// Token: 0x06002A93 RID: 10899 RVA: 0x00100198 File Offset: 0x000FE398
			public CDDesignerCommandSet(ComponentDesigner componentDesigner)
			{
				this.componentDesigner = componentDesigner;
			}

			// Token: 0x06002A94 RID: 10900 RVA: 0x001001A7 File Offset: 0x000FE3A7
			public override ICollection GetCommands(string name)
			{
				if (name.Equals("Verbs"))
				{
					return this.componentDesigner.Verbs;
				}
				if (name.Equals("ActionLists"))
				{
					return this.componentDesigner.ActionLists;
				}
				return base.GetCommands(name);
			}

			// Token: 0x04001DD3 RID: 7635
			private ComponentDesigner componentDesigner;
		}

		// Token: 0x02000482 RID: 1154
		protected sealed class ShadowPropertyCollection
		{
			// Token: 0x06002A95 RID: 10901 RVA: 0x001001E2 File Offset: 0x000FE3E2
			internal ShadowPropertyCollection(ComponentDesigner designer)
			{
				this.designer = designer;
			}

			// Token: 0x17000903 RID: 2307
			public object this[string propertyName]
			{
				get
				{
					if (propertyName == null)
					{
						throw new ArgumentNullException("propertyName");
					}
					if (this.properties != null && this.properties.ContainsKey(propertyName))
					{
						return this.properties[propertyName];
					}
					PropertyDescriptor shadowedPropertyDescriptor = this.GetShadowedPropertyDescriptor(propertyName);
					return shadowedPropertyDescriptor.GetValue(this.designer.Component);
				}
				set
				{
					if (this.properties == null)
					{
						this.properties = new Hashtable();
					}
					this.properties[propertyName] = value;
				}
			}

			// Token: 0x06002A98 RID: 10904 RVA: 0x0010026D File Offset: 0x000FE46D
			public bool Contains(string propertyName)
			{
				return this.properties != null && this.properties.ContainsKey(propertyName);
			}

			// Token: 0x06002A99 RID: 10905 RVA: 0x00100288 File Offset: 0x000FE488
			private PropertyDescriptor GetShadowedPropertyDescriptor(string propertyName)
			{
				if (this.descriptors == null)
				{
					this.descriptors = new Hashtable();
				}
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)this.descriptors[propertyName];
				if (propertyDescriptor == null)
				{
					propertyDescriptor = TypeDescriptor.GetProperties(this.designer.Component.GetType())[propertyName];
					if (propertyDescriptor == null)
					{
						throw new ArgumentException(SR.GetString("DesignerPropNotFound", new object[]
						{
							propertyName,
							this.designer.Component.GetType().FullName
						}));
					}
					this.descriptors[propertyName] = propertyDescriptor;
				}
				return propertyDescriptor;
			}

			// Token: 0x06002A9A RID: 10906 RVA: 0x0010031C File Offset: 0x000FE51C
			internal bool ShouldSerializeValue(string propertyName, object defaultValue)
			{
				if (propertyName == null)
				{
					throw new ArgumentNullException("propertyName");
				}
				if (this.Contains(propertyName))
				{
					return !object.Equals(this[propertyName], defaultValue);
				}
				return this.GetShadowedPropertyDescriptor(propertyName).ShouldSerializeValue(this.designer.Component);
			}

			// Token: 0x04001DD4 RID: 7636
			private ComponentDesigner designer;

			// Token: 0x04001DD5 RID: 7637
			private Hashtable properties;

			// Token: 0x04001DD6 RID: 7638
			private Hashtable descriptors;
		}
	}
}
