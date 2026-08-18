using System;
using System.Collections;
using System.Design;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001F3 RID: 499
	public class DesignerSerializationManager : IDesignerSerializationManager, IServiceProvider
	{
		// Token: 0x060012CE RID: 4814 RVA: 0x0006DEC0 File Offset: 0x0006C0C0
		public DesignerSerializationManager()
		{
			this.preserveNames = true;
			this.validateRecycledTypes = true;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0006DED6 File Offset: 0x0006C0D6
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

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x0006DF04 File Offset: 0x0006C104
		// (set) Token: 0x060012D1 RID: 4817 RVA: 0x0006DF44 File Offset: 0x0006C144
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

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060012D2 RID: 4818 RVA: 0x0006DF53 File Offset: 0x0006C153
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

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0006DF74 File Offset: 0x0006C174
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x0006DF7C File Offset: 0x0006C17C
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

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0006DF8B File Offset: 0x0006C18B
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x0006DF93 File Offset: 0x0006C193
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

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0006DFAC File Offset: 0x0006C1AC
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0006DFB4 File Offset: 0x0006C1B4
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

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0006DFC3 File Offset: 0x0006C1C3
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x0006DFCB File Offset: 0x0006C1CB
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

		// Token: 0x1400003F RID: 63
		// (add) Token: 0x060012DB RID: 4827 RVA: 0x0006DFDA File Offset: 0x0006C1DA
		// (remove) Token: 0x060012DC RID: 4828 RVA: 0x0006DFF3 File Offset: 0x0006C1F3
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

		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060012DD RID: 4829 RVA: 0x0006E00C File Offset: 0x0006C20C
		// (remove) Token: 0x060012DE RID: 4830 RVA: 0x0006E025 File Offset: 0x0006C225
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

		// Token: 0x060012DF RID: 4831 RVA: 0x0006E03E File Offset: 0x0006C23E
		private void CheckNoSession()
		{
			if (this.session != null)
			{
				throw new InvalidOperationException(SR.GetString("SerializationManagerWithinSession"));
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0006E058 File Offset: 0x0006C258
		private void CheckSession()
		{
			if (this.session == null)
			{
				throw new InvalidOperationException(SR.GetString("SerializationManagerNoSession"));
			}
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0006E074 File Offset: 0x0006C274
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
									if (!(array2[k] == null) && !parameters[k].ParameterType.IsAssignableFrom(array2[k]))
									{
										if (array[k] is IConvertible)
										{
											try
											{
												array3[k] = ((IConvertible)array[k]).ToType(parameters[k].ParameterType, null);
												goto IL_219;
											}
											catch (InvalidCastException)
											{
											}
										}
										flag2 = false;
										break;
									}
									array3[k] = array[k];
									IL_219:;
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

		// Token: 0x060012E2 RID: 4834 RVA: 0x0006E42C File Offset: 0x0006C62C
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

		// Token: 0x060012E3 RID: 4835 RVA: 0x0006E464 File Offset: 0x0006C664
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
								Type runtimeType = this.GetRuntimeType(serializerBaseTypeName);
								if (runtimeType == serializerType && designerSerializerAttribute.SerializerTypeName != null && designerSerializerAttribute.SerializerTypeName.Length > 0)
								{
									Type runtimeType2 = this.GetRuntimeType(designerSerializerAttribute.SerializerTypeName);
									if (runtimeType2 != null)
									{
										obj = Activator.CreateInstance(runtimeType2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
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
				Type type = null;
				DefaultSerializationProviderAttribute defaultSerializationProviderAttribute = (DefaultSerializationProviderAttribute)TypeDescriptor.GetAttributes(serializerType)[typeof(DefaultSerializationProviderAttribute)];
				if (defaultSerializationProviderAttribute != null)
				{
					type = this.GetRuntimeType(defaultSerializationProviderAttribute.ProviderTypeName);
					if (type != null && typeof(IDesignerSerializationProvider).IsAssignableFrom(type))
					{
						IDesignerSerializationProvider designerSerializationProvider = (IDesignerSerializationProvider)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, null);
						((IDesignerSerializationManager)this).AddSerializationProvider(designerSerializationProvider);
					}
				}
				if (this.defaultProviderTable == null)
				{
					this.defaultProviderTable = new Hashtable();
				}
				this.defaultProviderTable[serializerType] = type;
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

		// Token: 0x060012E4 RID: 4836 RVA: 0x0006E6EC File Offset: 0x0006C8EC
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

		// Token: 0x060012E5 RID: 4837 RVA: 0x0006E720 File Offset: 0x0006C920
		protected virtual Type GetType(string typeName)
		{
			Type type = this.GetRuntimeType(typeName);
			if (type != null)
			{
				TypeDescriptionProviderService typeDescriptionProviderService = this.GetService(typeof(TypeDescriptionProviderService)) as TypeDescriptionProviderService;
				if (typeDescriptionProviderService != null)
				{
					TypeDescriptionProvider typeDescriptionProvider = typeDescriptionProviderService.GetProvider(type);
					if (!typeDescriptionProvider.IsSupportedType(type))
					{
						type = null;
					}
				}
			}
			return type;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0006E76C File Offset: 0x0006C96C
		public Type GetRuntimeType(string typeName)
		{
			if (this.typeResolver == null && !this.searchedTypeResolver)
			{
				this.typeResolver = (this.GetService(typeof(ITypeResolutionService)) as ITypeResolutionService);
				this.searchedTypeResolver = true;
			}
			Type type;
			if (this.typeResolver == null)
			{
				type = Type.GetType(typeName);
			}
			else
			{
				type = this.typeResolver.GetType(typeName);
			}
			return type;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0006E7CA File Offset: 0x0006C9CA
		protected virtual void OnResolveName(ResolveNameEventArgs e)
		{
			if (this.resolveNameEventHandler != null)
			{
				this.resolveNameEventHandler(this, e);
			}
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0006E7E1 File Offset: 0x0006C9E1
		protected virtual void OnSessionCreated(EventArgs e)
		{
			if (this.sessionCreatedEventHandler != null)
			{
				this.sessionCreatedEventHandler(this, e);
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0006E7F8 File Offset: 0x0006C9F8
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

		// Token: 0x060012EA RID: 4842 RVA: 0x0006E88C File Offset: 0x0006CA8C
		private PropertyDescriptor WrapProperty(PropertyDescriptor property, object owner)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			return new DesignerSerializationManager.WrappedPropertyDescriptor(property, owner);
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060012EB RID: 4843 RVA: 0x0006E8A3 File Offset: 0x0006CAA3
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

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0006E8C4 File Offset: 0x0006CAC4
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

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x060012ED RID: 4845 RVA: 0x0006E930 File Offset: 0x0006CB30
		// (remove) Token: 0x060012EE RID: 4846 RVA: 0x0006E94F File Offset: 0x0006CB4F
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

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x060012EF RID: 4847 RVA: 0x0006E968 File Offset: 0x0006CB68
		// (remove) Token: 0x060012F0 RID: 4848 RVA: 0x0006E987 File Offset: 0x0006CB87
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

		// Token: 0x060012F1 RID: 4849 RVA: 0x0006E9A0 File Offset: 0x0006CBA0
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

		// Token: 0x060012F2 RID: 4850 RVA: 0x0006E9D0 File Offset: 0x0006CBD0
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

		// Token: 0x060012F3 RID: 4851 RVA: 0x0006EA80 File Offset: 0x0006CC80
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

		// Token: 0x060012F4 RID: 4852 RVA: 0x0006EAF8 File Offset: 0x0006CCF8
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

		// Token: 0x060012F5 RID: 4853 RVA: 0x0006EB6A File Offset: 0x0006CD6A
		object IDesignerSerializationManager.GetSerializer(Type objectType, Type serializerType)
		{
			return this.GetSerializer(objectType, serializerType);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0006EB74 File Offset: 0x0006CD74
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

		// Token: 0x060012F7 RID: 4855 RVA: 0x0006EBF1 File Offset: 0x0006CDF1
		void IDesignerSerializationManager.RemoveSerializationProvider(IDesignerSerializationProvider provider)
		{
			if (this.designerSerializationProviders != null)
			{
				this.designerSerializationProviders.Remove(provider);
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0006EC07 File Offset: 0x0006CE07
		void IDesignerSerializationManager.ReportError(object errorInformation)
		{
			this.CheckSession();
			if (errorInformation != null)
			{
				this.Errors.Add(errorInformation);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x0006EC1F File Offset: 0x0006CE1F
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

		// Token: 0x060012FA RID: 4858 RVA: 0x0006EC40 File Offset: 0x0006CE40
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

		// Token: 0x060012FB RID: 4859 RVA: 0x0006ED10 File Offset: 0x0006CF10
		object IServiceProvider.GetService(Type serviceType)
		{
			return this.GetService(serviceType);
		}

		// Token: 0x04000A3C RID: 2620
		private IServiceProvider provider;

		// Token: 0x04000A3D RID: 2621
		private ITypeResolutionService typeResolver;

		// Token: 0x04000A3E RID: 2622
		private bool searchedTypeResolver;

		// Token: 0x04000A3F RID: 2623
		private bool recycleInstances;

		// Token: 0x04000A40 RID: 2624
		private bool validateRecycledTypes;

		// Token: 0x04000A41 RID: 2625
		private bool preserveNames;

		// Token: 0x04000A42 RID: 2626
		private IContainer container;

		// Token: 0x04000A43 RID: 2627
		private IDisposable session;

		// Token: 0x04000A44 RID: 2628
		private ResolveNameEventHandler resolveNameEventHandler;

		// Token: 0x04000A45 RID: 2629
		private EventHandler serializationCompleteEventHandler;

		// Token: 0x04000A46 RID: 2630
		private EventHandler sessionCreatedEventHandler;

		// Token: 0x04000A47 RID: 2631
		private EventHandler sessionDisposedEventHandler;

		// Token: 0x04000A48 RID: 2632
		private ArrayList designerSerializationProviders;

		// Token: 0x04000A49 RID: 2633
		private Hashtable defaultProviderTable;

		// Token: 0x04000A4A RID: 2634
		private Hashtable instancesByName;

		// Token: 0x04000A4B RID: 2635
		private Hashtable namesByInstance;

		// Token: 0x04000A4C RID: 2636
		private Hashtable serializers;

		// Token: 0x04000A4D RID: 2637
		private ArrayList errorList;

		// Token: 0x04000A4E RID: 2638
		private ContextStack contextStack;

		// Token: 0x04000A4F RID: 2639
		private PropertyDescriptorCollection properties;

		// Token: 0x04000A50 RID: 2640
		private object propertyProvider;

		// Token: 0x020004B6 RID: 1206
		private sealed class SerializationSession : IDisposable
		{
			// Token: 0x06002C1B RID: 11291 RVA: 0x00106DB0 File Offset: 0x00104FB0
			internal SerializationSession(DesignerSerializationManager serializationManager)
			{
				this.serializationManager = serializationManager;
			}

			// Token: 0x06002C1C RID: 11292 RVA: 0x00106DBF File Offset: 0x00104FBF
			public void Dispose()
			{
				this.serializationManager.OnSessionDisposed(EventArgs.Empty);
			}

			// Token: 0x04001E90 RID: 7824
			private DesignerSerializationManager serializationManager;
		}

		// Token: 0x020004B7 RID: 1207
		private sealed class ReferenceComparer : IEqualityComparer
		{
			// Token: 0x06002C1D RID: 11293 RVA: 0x000EBFE3 File Offset: 0x000EA1E3
			bool IEqualityComparer.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06002C1E RID: 11294 RVA: 0x000B900B File Offset: 0x000B720B
			int IEqualityComparer.GetHashCode(object x)
			{
				if (x != null)
				{
					return x.GetHashCode();
				}
				return 0;
			}
		}

		// Token: 0x020004B8 RID: 1208
		private sealed class WrappedPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06002C20 RID: 11296 RVA: 0x00106DD1 File Offset: 0x00104FD1
			internal WrappedPropertyDescriptor(PropertyDescriptor property, object target) : base(property.Name, null)
			{
				this.property = property;
				this.target = target;
			}

			// Token: 0x1700094E RID: 2382
			// (get) Token: 0x06002C21 RID: 11297 RVA: 0x00106DEE File Offset: 0x00104FEE
			public override AttributeCollection Attributes
			{
				get
				{
					return this.property.Attributes;
				}
			}

			// Token: 0x1700094F RID: 2383
			// (get) Token: 0x06002C22 RID: 11298 RVA: 0x00106DFB File Offset: 0x00104FFB
			public override Type ComponentType
			{
				get
				{
					return this.property.ComponentType;
				}
			}

			// Token: 0x17000950 RID: 2384
			// (get) Token: 0x06002C23 RID: 11299 RVA: 0x00106E08 File Offset: 0x00105008
			public override bool IsReadOnly
			{
				get
				{
					return this.property.IsReadOnly;
				}
			}

			// Token: 0x17000951 RID: 2385
			// (get) Token: 0x06002C24 RID: 11300 RVA: 0x00106E15 File Offset: 0x00105015
			public override Type PropertyType
			{
				get
				{
					return this.property.PropertyType;
				}
			}

			// Token: 0x06002C25 RID: 11301 RVA: 0x00106E22 File Offset: 0x00105022
			public override bool CanResetValue(object component)
			{
				return this.property.CanResetValue(this.target);
			}

			// Token: 0x06002C26 RID: 11302 RVA: 0x00106E35 File Offset: 0x00105035
			public override object GetValue(object component)
			{
				return this.property.GetValue(this.target);
			}

			// Token: 0x06002C27 RID: 11303 RVA: 0x00106E48 File Offset: 0x00105048
			public override void ResetValue(object component)
			{
				this.property.ResetValue(this.target);
			}

			// Token: 0x06002C28 RID: 11304 RVA: 0x00106E5B File Offset: 0x0010505B
			public override void SetValue(object component, object value)
			{
				this.property.SetValue(this.target, value);
			}

			// Token: 0x06002C29 RID: 11305 RVA: 0x00106E6F File Offset: 0x0010506F
			public override bool ShouldSerializeValue(object component)
			{
				return this.property.ShouldSerializeValue(this.target);
			}

			// Token: 0x04001E91 RID: 7825
			private object target;

			// Token: 0x04001E92 RID: 7826
			private PropertyDescriptor property;
		}
	}
}
