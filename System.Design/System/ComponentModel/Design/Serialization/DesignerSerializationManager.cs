using System;
using System.Collections;
using System.Design;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200057F RID: 1407
	public class DesignerSerializationManager : IDesignerSerializationManager, IServiceProvider
	{
		// Token: 0x060031D4 RID: 12756 RVA: 0x00119DBA File Offset: 0x00118DBA
		public DesignerSerializationManager()
		{
			this.preserveNames = true;
			this.validateRecycledTypes = true;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x00119DD0 File Offset: 0x00118DD0
		public DesignerSerializationManager(IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this.provider = provider;
			this.preserveNames = true;
			this.validateRecycledTypes = true;
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x00119DFC File Offset: 0x00118DFC
		// (set) Token: 0x060031D7 RID: 12759 RVA: 0x00119E3C File Offset: 0x00118E3C
		public IContainer Container
		{
			get
			{
				if (this.container == null)
				{
					IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
					if (designerHost != null)
					{
						this.container = designerHost.Container;
					}
				}
				return this.container;
			}
			set
			{
				this.CheckNoSession();
				this.container = value;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x060031D8 RID: 12760 RVA: 0x00119E4B File Offset: 0x00118E4B
		public IList Errors
		{
			get
			{
				this.CheckSession();
				if (this.errorList == null)
				{
					this.errorList = new ArrayList();
				}
				return this.errorList;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x060031D9 RID: 12761 RVA: 0x00119E6C File Offset: 0x00118E6C
		// (set) Token: 0x060031DA RID: 12762 RVA: 0x00119E74 File Offset: 0x00118E74
		public bool PreserveNames
		{
			get
			{
				return this.preserveNames;
			}
			set
			{
				this.CheckNoSession();
				this.preserveNames = value;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x00119E83 File Offset: 0x00118E83
		// (set) Token: 0x060031DC RID: 12764 RVA: 0x00119E8B File Offset: 0x00118E8B
		public object PropertyProvider
		{
			get
			{
				return this.propertyProvider;
			}
			set
			{
				if (this.propertyProvider != value)
				{
					this.propertyProvider = value;
					this.properties = null;
				}
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x00119EA4 File Offset: 0x00118EA4
		// (set) Token: 0x060031DE RID: 12766 RVA: 0x00119EAC File Offset: 0x00118EAC
		public bool RecycleInstances
		{
			get
			{
				return this.recycleInstances;
			}
			set
			{
				this.CheckNoSession();
				this.recycleInstances = value;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x00119EBB File Offset: 0x00118EBB
		// (set) Token: 0x060031E0 RID: 12768 RVA: 0x00119EC3 File Offset: 0x00118EC3
		public bool ValidateRecycledTypes
		{
			get
			{
				return this.validateRecycledTypes;
			}
			set
			{
				this.CheckNoSession();
				this.validateRecycledTypes = value;
			}
		}

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x060031E1 RID: 12769 RVA: 0x00119ED2 File Offset: 0x00118ED2
		// (remove) Token: 0x060031E2 RID: 12770 RVA: 0x00119EEB File Offset: 0x00118EEB
		public event EventHandler SessionCreated
		{
			add
			{
				this.sessionCreatedEventHandler = (EventHandler)Delegate.Combine(this.sessionCreatedEventHandler, value);
			}
			remove
			{
				this.sessionCreatedEventHandler = (EventHandler)Delegate.Remove(this.sessionCreatedEventHandler, value);
			}
		}

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x060031E3 RID: 12771 RVA: 0x00119F04 File Offset: 0x00118F04
		// (remove) Token: 0x060031E4 RID: 12772 RVA: 0x00119F1D File Offset: 0x00118F1D
		public event EventHandler SessionDisposed
		{
			add
			{
				this.sessionDisposedEventHandler = (EventHandler)Delegate.Combine(this.sessionDisposedEventHandler, value);
			}
			remove
			{
				this.sessionDisposedEventHandler = (EventHandler)Delegate.Remove(this.sessionDisposedEventHandler, value);
			}
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x00119F36 File Offset: 0x00118F36
		private void CheckNoSession()
		{
			if (this.session != null)
			{
				throw new InvalidOperationException(SR.GetString("SerializationManagerWithinSession"));
			}
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x00119F50 File Offset: 0x00118F50
		private void CheckSession()
		{
			if (this.session == null)
			{
				throw new InvalidOperationException(SR.GetString("SerializationManagerNoSession"));
			}
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x00119F6C File Offset: 0x00118F6C
		protected virtual object CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
		{
			object[] array = null;
			if (arguments != null && arguments.Count > 0)
			{
				array = new object[arguments.Count];
				arguments.CopyTo(array, 0);
			}
			object obj = null;
			if (this.RecycleInstances && name != null)
			{
				if (this.instancesByName != null)
				{
					obj = this.instancesByName[name];
				}
				if (obj == null && addToContainer && this.Container != null)
				{
					obj = this.Container.Components[name];
				}
				if (obj != null && this.ValidateRecycledTypes && obj.GetType() != type)
				{
					obj = null;
				}
			}
			if (obj == null && addToContainer && typeof(IComponent).IsAssignableFrom(type) && (array == null || array.Length == 0 || (array.Length == 1 && array[0] == this.Container)))
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null && designerHost.Container == this.Container)
				{
					bool flag = false;
					if (!this.PreserveNames && name != null && this.Container.Components[name] != null)
					{
						flag = true;
					}
					if (name == null || flag)
					{
						obj = designerHost.CreateComponent(type);
					}
					else
					{
						obj = designerHost.CreateComponent(type, name);
					}
				}
			}
			if (obj == null)
			{
				try
				{
					try
					{
						obj = TypeDescriptor.CreateInstance(this.provider, type, null, array);
					}
					catch (MissingMethodException ex)
					{
						Type[] array2 = new Type[array.Length];
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] != null)
							{
								array2[i] = array[i].GetType();
							}
						}
						object[] array3 = new object[array.Length];
						foreach (ConstructorInfo constructorInfo in TypeDescriptor.GetReflectionType(type).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance))
						{
							ParameterInfo[] parameters = constructorInfo.GetParameters();
							if (parameters != null && parameters.Length == array2.Length)
							{
								bool flag2 = true;
								for (int k = 0; k < array2.Length; k++)
								{
									if (array2[k] != null && !parameters[k].ParameterType.IsAssignableFrom(array2[k]))
									{
										if (array[k] is IConvertible)
										{
											try
											{
												array3[k] = ((IConvertible)array[k]).ToType(parameters[k].ParameterType, null);
												goto IL_20C;
											}
											catch (InvalidCastException)
											{
											}
										}
										flag2 = false;
										break;
									}
									array3[k] = array[k];
									IL_20C:;
								}
								if (flag2)
								{
									obj = TypeDescriptor.CreateInstance(this.provider, type, null, array3);
									break;
								}
							}
						}
						if (obj == null)
						{
							throw ex;
						}
					}
				}
				catch (MissingMethodException)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj2 in array)
					{
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(", ");
						}
						if (obj2 != null)
						{
							stringBuilder.Append(obj2.GetType().Name);
						}
						else
						{
							stringBuilder.Append("null");
						}
					}
					throw new SerializationException(SR.GetString("SerializationManagerNoMatchingCtor", new object[]
					{
						type.FullName,
						stringBuilder.ToString()
					}))
					{
						HelpLink = "SerializationManagerNoMatchingCtor"
					};
				}
				if (addToContainer && obj is IComponent && this.Container != null)
				{
					bool flag3 = false;
					if (!this.PreserveNames && name != null && this.Container.Components[name] != null)
					{
						flag3 = true;
					}
					if (name == null || flag3)
					{
						this.Container.Add((IComponent)obj);
					}
					else
					{
						this.Container.Add((IComponent)obj, name);
					}
				}
			}
			return obj;
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x0011A31C File Offset: 0x0011931C
		public IDisposable CreateSession()
		{
			if (this.session != null)
			{
				throw new InvalidOperationException(SR.GetString("SerializationManagerAreadyInSession"));
			}
			this.session = new DesignerSerializationManager.SerializationSession(this);
			this.OnSessionCreated(EventArgs.Empty);
			return this.session;
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x0011A354 File Offset: 0x00119354
		public object GetSerializer(Type objectType, Type serializerType)
		{
			if (serializerType == null)
			{
				throw new ArgumentNullException("serializerType");
			}
			object obj = null;
			if (objectType != null)
			{
				if (this.serializers != null)
				{
					obj = this.serializers[objectType];
					if (obj != null && !serializerType.IsAssignableFrom(obj.GetType()))
					{
						obj = null;
					}
				}
				if (obj == null)
				{
					AttributeCollection attributes = TypeDescriptor.GetAttributes(objectType);
					foreach (object obj2 in attributes)
					{
						Attribute attribute = (Attribute)obj2;
						if (attribute is DesignerSerializerAttribute)
						{
							DesignerSerializerAttribute designerSerializerAttribute = (DesignerSerializerAttribute)attribute;
							string serializerBaseTypeName = designerSerializerAttribute.SerializerBaseTypeName;
							if (serializerBaseTypeName != null)
							{
								Type type = this.GetType(serializerBaseTypeName);
								if (type == serializerType && designerSerializerAttribute.SerializerTypeName != null && designerSerializerAttribute.SerializerTypeName.Length > 0)
								{
									Type type2 = this.GetType(designerSerializerAttribute.SerializerTypeName);
									if (type2 != null)
									{
										obj = Activator.CreateInstance(type2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
										break;
									}
								}
							}
						}
					}
					if (obj != null && this.session != null)
					{
						if (this.serializers == null)
						{
							this.serializers = new Hashtable();
						}
						this.serializers[objectType] = obj;
					}
				}
			}
			if (this.defaultProviderTable == null || !this.defaultProviderTable.ContainsKey(serializerType))
			{
				Type type3 = null;
				DefaultSerializationProviderAttribute defaultSerializationProviderAttribute = (DefaultSerializationProviderAttribute)TypeDescriptor.GetAttributes(serializerType)[typeof(DefaultSerializationProviderAttribute)];
				if (defaultSerializationProviderAttribute != null)
				{
					type3 = this.GetType(defaultSerializationProviderAttribute.ProviderTypeName);
					if (type3 != null && typeof(IDesignerSerializationProvider).IsAssignableFrom(type3))
					{
						IDesignerSerializationProvider designerSerializationProvider = (IDesignerSerializationProvider)Activator.CreateInstance(type3, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
						((IDesignerSerializationManager)this).AddSerializationProvider(designerSerializationProvider);
					}
				}
				if (this.defaultProviderTable == null)
				{
					this.defaultProviderTable = new Hashtable();
				}
				this.defaultProviderTable[serializerType] = type3;
			}
			if (this.designerSerializationProviders != null)
			{
				bool flag = true;
				int num = 0;
				while (flag && num < this.designerSerializationProviders.Count)
				{
					flag = false;
					foreach (object obj3 in this.designerSerializationProviders)
					{
						IDesignerSerializationProvider designerSerializationProvider2 = (IDesignerSerializationProvider)obj3;
						object serializer = designerSerializationProvider2.GetSerializer(this, obj, objectType, serializerType);
						if (serializer != null)
						{
							flag = (obj != serializer);
							obj = serializer;
						}
					}
					num++;
				}
			}
			return obj;
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x0011A5B8 File Offset: 0x001195B8
		protected virtual object GetService(Type serviceType)
		{
			if (serviceType == typeof(IContainer))
			{
				return this.Container;
			}
			if (this.provider != null)
			{
				return this.provider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x0011A5E4 File Offset: 0x001195E4
		protected virtual Type GetType(string typeName)
		{
			if (this.typeResolver == null && !this.searchedTypeResolver)
			{
				this.typeResolver = (this.GetService(typeof(ITypeResolutionService)) as ITypeResolutionService);
				this.searchedTypeResolver = true;
			}
			if (this.typeResolver == null)
			{
				return Type.GetType(typeName);
			}
			return this.typeResolver.GetType(typeName);
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x0011A63E File Offset: 0x0011963E
		protected virtual void OnResolveName(ResolveNameEventArgs e)
		{
			if (this.resolveNameEventHandler != null)
			{
				this.resolveNameEventHandler(this, e);
			}
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x0011A655 File Offset: 0x00119655
		protected virtual void OnSessionCreated(EventArgs e)
		{
			if (this.sessionCreatedEventHandler != null)
			{
				this.sessionCreatedEventHandler(this, e);
			}
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x0011A66C File Offset: 0x0011966C
		protected virtual void OnSessionDisposed(EventArgs e)
		{
			try
			{
				try
				{
					if (this.sessionDisposedEventHandler != null)
					{
						this.sessionDisposedEventHandler(this, e);
					}
				}
				finally
				{
					if (this.serializationCompleteEventHandler != null)
					{
						this.serializationCompleteEventHandler(this, EventArgs.Empty);
					}
				}
			}
			finally
			{
				this.resolveNameEventHandler = null;
				this.serializationCompleteEventHandler = null;
				this.instancesByName = null;
				this.namesByInstance = null;
				this.serializers = null;
				this.contextStack = null;
				this.errorList = null;
				this.session = null;
			}
		}

		// Token: 0x060031EF RID: 12783 RVA: 0x0011A704 File Offset: 0x00119704
		private PropertyDescriptor WrapProperty(PropertyDescriptor property, object owner)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			return new DesignerSerializationManager.WrappedPropertyDescriptor(property, owner);
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x0011A71B File Offset: 0x0011971B
		ContextStack IDesignerSerializationManager.Context
		{
			get
			{
				if (this.contextStack == null)
				{
					this.CheckSession();
					this.contextStack = new ContextStack();
				}
				return this.contextStack;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x0011A73C File Offset: 0x0011973C
		PropertyDescriptorCollection IDesignerSerializationManager.Properties
		{
			get
			{
				if (this.properties == null)
				{
					object obj = this.PropertyProvider;
					PropertyDescriptor[] array;
					if (obj == null)
					{
						array = new PropertyDescriptor[0];
					}
					else
					{
						PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
						array = new PropertyDescriptor[propertyDescriptorCollection.Count];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = this.WrapProperty(propertyDescriptorCollection[i], obj);
						}
					}
					this.properties = new PropertyDescriptorCollection(array);
				}
				return this.properties;
			}
		}

		// Token: 0x1400006C RID: 108
		// (add) Token: 0x060031F2 RID: 12786 RVA: 0x0011A7A8 File Offset: 0x001197A8
		// (remove) Token: 0x060031F3 RID: 12787 RVA: 0x0011A7C7 File Offset: 0x001197C7
		event ResolveNameEventHandler IDesignerSerializationManager.ResolveName
		{
			add
			{
				this.CheckSession();
				this.resolveNameEventHandler = (ResolveNameEventHandler)Delegate.Combine(this.resolveNameEventHandler, value);
			}
			remove
			{
				this.resolveNameEventHandler = (ResolveNameEventHandler)Delegate.Remove(this.resolveNameEventHandler, value);
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x060031F4 RID: 12788 RVA: 0x0011A7E0 File Offset: 0x001197E0
		// (remove) Token: 0x060031F5 RID: 12789 RVA: 0x0011A7FF File Offset: 0x001197FF
		event EventHandler IDesignerSerializationManager.SerializationComplete
		{
			add
			{
				this.CheckSession();
				this.serializationCompleteEventHandler = (EventHandler)Delegate.Combine(this.serializationCompleteEventHandler, value);
			}
			remove
			{
				this.serializationCompleteEventHandler = (EventHandler)Delegate.Remove(this.serializationCompleteEventHandler, value);
			}
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x0011A818 File Offset: 0x00119818
		void IDesignerSerializationManager.AddSerializationProvider(IDesignerSerializationProvider provider)
		{
			if (this.designerSerializationProviders == null)
			{
				this.designerSerializationProviders = new ArrayList();
			}
			if (!this.designerSerializationProviders.Contains(provider))
			{
				this.designerSerializationProviders.Add(provider);
			}
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x0011A848 File Offset: 0x00119848
		object IDesignerSerializationManager.CreateInstance(Type type, ICollection arguments, string name, bool addToContainer)
		{
			this.CheckSession();
			if (name != null && this.instancesByName != null && this.instancesByName.ContainsKey(name))
			{
				throw new SerializationException(SR.GetString("SerializationManagerDuplicateComponentDecl", new object[]
				{
					name
				}))
				{
					HelpLink = "SerializationManagerDuplicateComponentDecl"
				};
			}
			object obj = this.CreateInstance(type, arguments, name, addToContainer);
			if (name != null && (!(obj is IComponent) || !this.RecycleInstances))
			{
				if (this.instancesByName == null)
				{
					this.instancesByName = new Hashtable();
					this.namesByInstance = new Hashtable(new DesignerSerializationManager.ReferenceComparer());
				}
				this.instancesByName[name] = obj;
				this.namesByInstance[obj] = name;
			}
			return obj;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x0011A8FC File Offset: 0x001198FC
		object IDesignerSerializationManager.GetInstance(string name)
		{
			object obj = null;
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.CheckSession();
			if (this.instancesByName != null)
			{
				obj = this.instancesByName[name];
			}
			if (obj == null && this.PreserveNames && this.Container != null)
			{
				obj = this.Container.Components[name];
			}
			if (obj == null)
			{
				ResolveNameEventArgs resolveNameEventArgs = new ResolveNameEventArgs(name);
				this.OnResolveName(resolveNameEventArgs);
				obj = resolveNameEventArgs.Value;
			}
			return obj;
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x0011A974 File Offset: 0x00119974
		string IDesignerSerializationManager.GetName(object value)
		{
			string text = null;
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.CheckSession();
			if (this.namesByInstance != null)
			{
				text = (string)this.namesByInstance[value];
			}
			if (text == null && value is IComponent)
			{
				ISite site = ((IComponent)value).Site;
				if (site != null)
				{
					INestedSite nestedSite = site as INestedSite;
					if (nestedSite != null)
					{
						text = nestedSite.FullName;
					}
					else
					{
						text = site.Name;
					}
				}
			}
			return text;
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x0011A9E6 File Offset: 0x001199E6
		object IDesignerSerializationManager.GetSerializer(Type objectType, Type serializerType)
		{
			return this.GetSerializer(objectType, serializerType);
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x0011A9F0 File Offset: 0x001199F0
		Type IDesignerSerializationManager.GetType(string typeName)
		{
			this.CheckSession();
			Type type = null;
			while (type == null)
			{
				type = this.GetType(typeName);
				if (type == null && typeName != null && typeName.Length > 0)
				{
					int num = typeName.LastIndexOf('.');
					if (num == -1 || num == typeName.Length - 1)
					{
						break;
					}
					typeName = typeName.Substring(0, num) + "+" + typeName.Substring(num + 1, typeName.Length - num - 1);
				}
			}
			return type;
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x0011AA61 File Offset: 0x00119A61
		void IDesignerSerializationManager.RemoveSerializationProvider(IDesignerSerializationProvider provider)
		{
			if (this.designerSerializationProviders != null)
			{
				this.designerSerializationProviders.Remove(provider);
			}
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x0011AA77 File Offset: 0x00119A77
		void IDesignerSerializationManager.ReportError(object errorInformation)
		{
			this.CheckSession();
			if (errorInformation != null)
			{
				this.Errors.Add(errorInformation);
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x0011AA8F File Offset: 0x00119A8F
		internal ArrayList SerializationProviders
		{
			get
			{
				if (this.designerSerializationProviders == null)
				{
					return new ArrayList();
				}
				return this.designerSerializationProviders.Clone() as ArrayList;
			}
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x0011AAB0 File Offset: 0x00119AB0
		void IDesignerSerializationManager.SetName(object instance, string name)
		{
			this.CheckSession();
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.instancesByName == null)
			{
				this.instancesByName = new Hashtable();
				this.namesByInstance = new Hashtable(new DesignerSerializationManager.ReferenceComparer());
			}
			if (this.instancesByName[name] != null)
			{
				throw new ArgumentException(SR.GetString("SerializationManagerNameInUse", new object[]
				{
					name
				}));
			}
			if (this.namesByInstance[instance] != null)
			{
				throw new ArgumentException(SR.GetString("SerializationManagerObjectHasName", new object[]
				{
					name,
					(string)this.namesByInstance[instance]
				}));
			}
			this.instancesByName[name] = instance;
			this.namesByInstance[instance] = name;
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x0011AB84 File Offset: 0x00119B84
		object IServiceProvider.GetService(Type serviceType)
		{
			return this.GetService(serviceType);
		}

		// Token: 0x0400213B RID: 8507
		private IServiceProvider provider;

		// Token: 0x0400213C RID: 8508
		private ITypeResolutionService typeResolver;

		// Token: 0x0400213D RID: 8509
		private bool searchedTypeResolver;

		// Token: 0x0400213E RID: 8510
		private bool recycleInstances;

		// Token: 0x0400213F RID: 8511
		private bool validateRecycledTypes;

		// Token: 0x04002140 RID: 8512
		private bool preserveNames;

		// Token: 0x04002141 RID: 8513
		private IContainer container;

		// Token: 0x04002142 RID: 8514
		private IDisposable session;

		// Token: 0x04002143 RID: 8515
		private ResolveNameEventHandler resolveNameEventHandler;

		// Token: 0x04002144 RID: 8516
		private EventHandler serializationCompleteEventHandler;

		// Token: 0x04002145 RID: 8517
		private EventHandler sessionCreatedEventHandler;

		// Token: 0x04002146 RID: 8518
		private EventHandler sessionDisposedEventHandler;

		// Token: 0x04002147 RID: 8519
		private ArrayList designerSerializationProviders;

		// Token: 0x04002148 RID: 8520
		private Hashtable defaultProviderTable;

		// Token: 0x04002149 RID: 8521
		private Hashtable instancesByName;

		// Token: 0x0400214A RID: 8522
		private Hashtable namesByInstance;

		// Token: 0x0400214B RID: 8523
		private Hashtable serializers;

		// Token: 0x0400214C RID: 8524
		private ArrayList errorList;

		// Token: 0x0400214D RID: 8525
		private ContextStack contextStack;

		// Token: 0x0400214E RID: 8526
		private PropertyDescriptorCollection properties;

		// Token: 0x0400214F RID: 8527
		private object propertyProvider;

		// Token: 0x02000580 RID: 1408
		private sealed class SerializationSession : IDisposable
		{
			// Token: 0x06003201 RID: 12801 RVA: 0x0011AB8D File Offset: 0x00119B8D
			internal SerializationSession(DesignerSerializationManager serializationManager)
			{
				this.serializationManager = serializationManager;
			}

			// Token: 0x06003202 RID: 12802 RVA: 0x0011AB9C File Offset: 0x00119B9C
			public void Dispose()
			{
				this.serializationManager.OnSessionDisposed(EventArgs.Empty);
			}

			// Token: 0x04002150 RID: 8528
			private DesignerSerializationManager serializationManager;
		}

		// Token: 0x02000581 RID: 1409
		private sealed class ReferenceComparer : IEqualityComparer
		{
			// Token: 0x06003203 RID: 12803 RVA: 0x0011ABAE File Offset: 0x00119BAE
			bool IEqualityComparer.Equals(object x, object y)
			{
				return object.ReferenceEquals(x, y);
			}

			// Token: 0x06003204 RID: 12804 RVA: 0x0011ABB7 File Offset: 0x00119BB7
			int IEqualityComparer.GetHashCode(object x)
			{
				if (x != null)
				{
					return x.GetHashCode();
				}
				return 0;
			}
		}

		// Token: 0x02000582 RID: 1410
		private sealed class WrappedPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06003206 RID: 12806 RVA: 0x0011ABCC File Offset: 0x00119BCC
			internal WrappedPropertyDescriptor(PropertyDescriptor property, object target) : base(property.Name, null)
			{
				this.property = property;
				this.target = target;
			}

			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x06003207 RID: 12807 RVA: 0x0011ABE9 File Offset: 0x00119BE9
			public override AttributeCollection Attributes
			{
				get
				{
					return this.property.Attributes;
				}
			}

			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x06003208 RID: 12808 RVA: 0x0011ABF6 File Offset: 0x00119BF6
			public override Type ComponentType
			{
				get
				{
					return this.property.ComponentType;
				}
			}

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x06003209 RID: 12809 RVA: 0x0011AC03 File Offset: 0x00119C03
			public override bool IsReadOnly
			{
				get
				{
					return this.property.IsReadOnly;
				}
			}

			// Token: 0x17000956 RID: 2390
			// (get) Token: 0x0600320A RID: 12810 RVA: 0x0011AC10 File Offset: 0x00119C10
			public override Type PropertyType
			{
				get
				{
					return this.property.PropertyType;
				}
			}

			// Token: 0x0600320B RID: 12811 RVA: 0x0011AC1D File Offset: 0x00119C1D
			public override bool CanResetValue(object component)
			{
				return this.property.CanResetValue(this.target);
			}

			// Token: 0x0600320C RID: 12812 RVA: 0x0011AC30 File Offset: 0x00119C30
			public override object GetValue(object component)
			{
				return this.property.GetValue(this.target);
			}

			// Token: 0x0600320D RID: 12813 RVA: 0x0011AC43 File Offset: 0x00119C43
			public override void ResetValue(object component)
			{
				this.property.ResetValue(this.target);
			}

			// Token: 0x0600320E RID: 12814 RVA: 0x0011AC56 File Offset: 0x00119C56
			public override void SetValue(object component, object value)
			{
				this.property.SetValue(this.target, value);
			}

			// Token: 0x0600320F RID: 12815 RVA: 0x0011AC6A File Offset: 0x00119C6A
			public override bool ShouldSerializeValue(object component)
			{
				return this.property.ShouldSerializeValue(this.target);
			}

			// Token: 0x04002151 RID: 8529
			private object target;

			// Token: 0x04002152 RID: 8530
			private PropertyDescriptor property;
		}
	}
}
