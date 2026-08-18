using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005A3 RID: 1443
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ReflectPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060035B5 RID: 13749 RVA: 0x000E9848 File Offset: 0x000E7A48
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Attribute[] attributes) : base(name, attributes)
		{
			try
			{
				if (type == null)
				{
					throw new ArgumentException(SR.GetString("ErrorInvalidPropertyType", new object[]
					{
						name
					}));
				}
				if (componentClass == null)
				{
					throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[]
					{
						"componentClass"
					}));
				}
				this.type = type;
				this.componentClass = componentClass;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x000E98CC File Offset: 0x000E7ACC
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, PropertyInfo propInfo, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs) : this(componentClass, name, type, attrs)
		{
			this.propInfo = propInfo;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			if (getMethod != null && propInfo != null && setMethod == null)
			{
				this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetOnDemand] = true;
				return;
			}
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x000E9949 File Offset: 0x000E7B49
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Type receiverType, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs) : this(componentClass, name, type, attrs)
		{
			this.receiverType = receiverType;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x000E9988 File Offset: 0x000E7B88
		public ReflectPropertyDescriptor(Type componentClass, PropertyDescriptor oldReflectPropertyDescriptor, Attribute[] attributes) : base(oldReflectPropertyDescriptor, attributes)
		{
			this.componentClass = componentClass;
			this.type = oldReflectPropertyDescriptor.PropertyType;
			if (componentClass == null)
			{
				throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[]
				{
					"componentClass"
				}));
			}
			ReflectPropertyDescriptor reflectPropertyDescriptor = oldReflectPropertyDescriptor as ReflectPropertyDescriptor;
			if (reflectPropertyDescriptor != null)
			{
				if (reflectPropertyDescriptor.ComponentType == componentClass)
				{
					this.propInfo = reflectPropertyDescriptor.propInfo;
					this.getMethod = reflectPropertyDescriptor.getMethod;
					this.setMethod = reflectPropertyDescriptor.setMethod;
					this.shouldSerializeMethod = reflectPropertyDescriptor.shouldSerializeMethod;
					this.resetMethod = reflectPropertyDescriptor.resetMethod;
					this.defaultValue = reflectPropertyDescriptor.defaultValue;
					this.ambientValue = reflectPropertyDescriptor.ambientValue;
					this.state = reflectPropertyDescriptor.state;
				}
				if (attributes != null)
				{
					foreach (Attribute attribute in attributes)
					{
						DefaultValueAttribute defaultValueAttribute = attribute as DefaultValueAttribute;
						if (defaultValueAttribute != null)
						{
							this.defaultValue = defaultValueAttribute.Value;
							if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
							{
								this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
							}
							this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
						}
						else
						{
							AmbientValueAttribute ambientValueAttribute = attribute as AmbientValueAttribute;
							if (ambientValueAttribute != null)
							{
								this.ambientValue = ambientValueAttribute.Value;
								this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x060035B9 RID: 13753 RVA: 0x000E9B14 File Offset: 0x000E7D14
		private object AmbientValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitAmbientValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(AmbientValueAttribute)];
					if (attribute != null)
					{
						this.ambientValue = ((AmbientValueAttribute)attribute).Value;
					}
					else
					{
						this.ambientValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.ambientValue;
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x060035BA RID: 13754 RVA: 0x000E9B84 File Offset: 0x000E7D84
		private EventDescriptor ChangedEventValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitChangedQueried])
				{
					this.state[ReflectPropertyDescriptor.BitChangedQueried] = true;
					this.realChangedEvent = TypeDescriptor.GetEvents(this.ComponentType)[string.Format(CultureInfo.InvariantCulture, "{0}Changed", new object[]
					{
						this.Name
					})];
				}
				return this.realChangedEvent;
			}
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060035BB RID: 13755 RVA: 0x000E9BF0 File Offset: 0x000E7DF0
		// (set) Token: 0x060035BC RID: 13756 RVA: 0x000E9C5C File Offset: 0x000E7E5C
		private EventDescriptor IPropChangedEventValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitIPropChangedQueried])
				{
					this.state[ReflectPropertyDescriptor.BitIPropChangedQueried] = true;
					if (typeof(INotifyPropertyChanged).IsAssignableFrom(this.ComponentType))
					{
						this.realIPropChangedEvent = TypeDescriptor.GetEvents(typeof(INotifyPropertyChanged))["PropertyChanged"];
					}
				}
				return this.realIPropChangedEvent;
			}
			set
			{
				this.realIPropChangedEvent = value;
				this.state[ReflectPropertyDescriptor.BitIPropChangedQueried] = true;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x060035BD RID: 13757 RVA: 0x000E9C76 File Offset: 0x000E7E76
		public override Type ComponentType
		{
			get
			{
				return this.componentClass;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x060035BE RID: 13758 RVA: 0x000E9C80 File Offset: 0x000E7E80
		private object DefaultValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitDefaultValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(DefaultValueAttribute)];
					if (attribute != null)
					{
						this.defaultValue = ((DefaultValueAttribute)attribute).Value;
						if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
						{
							this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
						}
					}
					else
					{
						this.defaultValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.defaultValue;
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x060035BF RID: 13759 RVA: 0x000E9D3C File Offset: 0x000E7F3C
		private MethodInfo GetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitGetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitGetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingAttr, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.getMethod = this.propInfo.GetGetMethod(true);
						}
						if (this.getMethod == null)
						{
							throw new InvalidOperationException(SR.GetString("ErrorMissingPropertyAccessors", new object[]
							{
								this.componentClass.FullName + "." + this.Name
							}));
						}
					}
					else
					{
						this.getMethod = MemberDescriptor.FindMethod(this.componentClass, "Get" + this.Name, new Type[]
						{
							this.receiverType
						}, this.type);
						if (this.getMethod == null)
						{
							throw new ArgumentException(SR.GetString("ErrorMissingPropertyAccessors", new object[]
							{
								this.Name
							}));
						}
					}
				}
				return this.getMethod;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x060035C0 RID: 13760 RVA: 0x000E9E8D File Offset: 0x000E808D
		private bool IsExtender
		{
			get
			{
				return this.receiverType != null;
			}
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x060035C1 RID: 13761 RVA: 0x000E9E9B File Offset: 0x000E809B
		public override bool IsReadOnly
		{
			get
			{
				return this.SetMethodValue == null || ((ReadOnlyAttribute)this.Attributes[typeof(ReadOnlyAttribute)]).IsReadOnly;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x060035C2 RID: 13762 RVA: 0x000E9ECC File Offset: 0x000E80CC
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x060035C3 RID: 13763 RVA: 0x000E9ED4 File Offset: 0x000E80D4
		private MethodInfo ResetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitResetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitResetQueried] = true;
					Type[] args;
					if (this.receiverType == null)
					{
						args = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						args = new Type[]
						{
							this.receiverType
						};
					}
					IntSecurity.FullReflection.Assert();
					try
					{
						this.resetMethod = MemberDescriptor.FindMethod(this.componentClass, "Reset" + this.Name, args, typeof(void), false);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return this.resetMethod;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x000E9F80 File Offset: 0x000E8180
		private MethodInfo SetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried] && this.state[ReflectPropertyDescriptor.BitSetOnDemand])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					string name = this.propInfo.Name;
					if (this.setMethod == null)
					{
						Type baseType = this.ComponentType.BaseType;
						while (baseType != null && baseType != typeof(object) && !(baseType == null))
						{
							PropertyInfo property = baseType.GetProperty(name, bindingAttr, null, this.PropertyType, new Type[0], null);
							if (property != null)
							{
								this.setMethod = property.GetSetMethod();
								if (this.setMethod != null)
								{
									break;
								}
							}
							baseType = baseType.BaseType;
						}
					}
				}
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingAttr2 = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingAttr2, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.setMethod = this.propInfo.GetSetMethod(true);
						}
					}
					else
					{
						this.setMethod = MemberDescriptor.FindMethod(this.componentClass, "Set" + this.Name, new Type[]
						{
							this.receiverType,
							this.type
						}, typeof(void));
					}
				}
				return this.setMethod;
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x060035C5 RID: 13765 RVA: 0x000EA138 File Offset: 0x000E8338
		private MethodInfo ShouldSerializeMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried])
				{
					this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried] = true;
					Type[] args;
					if (this.receiverType == null)
					{
						args = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						args = new Type[]
						{
							this.receiverType
						};
					}
					IntSecurity.FullReflection.Assert();
					try
					{
						this.shouldSerializeMethod = MemberDescriptor.FindMethod(this.componentClass, "ShouldSerialize" + this.Name, args, typeof(bool), false);
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return this.shouldSerializeMethod;
			}
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000EA1E4 File Offset: 0x000E83E4
		public override void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			EventDescriptor changedEventValue = this.ChangedEventValue;
			if (changedEventValue != null && changedEventValue.EventType.IsInstanceOfType(handler))
			{
				changedEventValue.AddEventHandler(component, handler);
				return;
			}
			if (base.GetValueChangedHandler(component) == null)
			{
				EventDescriptor ipropChangedEventValue = this.IPropChangedEventValue;
				if (ipropChangedEventValue != null)
				{
					ipropChangedEventValue.AddEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
				}
			}
			base.AddValueChanged(component, handler);
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000EA25C File Offset: 0x000E845C
		internal bool ExtenderCanResetValue(IExtenderProvider provider, object component)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.ExtenderGetValue(provider, component), this.defaultValue);
			}
			MethodInfo resetMethodValue = this.ResetMethodValue;
			if (resetMethodValue != null)
			{
				MethodInfo shouldSerializeMethodValue = this.ShouldSerializeMethodValue;
				if (shouldSerializeMethodValue != null)
				{
					try
					{
						provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
						return (bool)shouldSerializeMethodValue.Invoke(provider, new object[]
						{
							component
						});
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000EA2F4 File Offset: 0x000E84F4
		internal Type ExtenderGetReceiverType()
		{
			return this.receiverType;
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000EA2FC File Offset: 0x000E84FC
		internal Type ExtenderGetType(IExtenderProvider provider)
		{
			return this.PropertyType;
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000EA304 File Offset: 0x000E8504
		internal object ExtenderGetValue(IExtenderProvider provider, object component)
		{
			if (provider != null)
			{
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				return this.GetMethodValue.Invoke(provider, new object[]
				{
					component
				});
			}
			return null;
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000EA338 File Offset: 0x000E8538
		internal void ExtenderResetValue(IExtenderProvider provider, object component, PropertyDescriptor notifyDesc)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.DefaultValue, notifyDesc);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.AmbientValue, notifyDesc);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object oldValue = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					oldValue = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.ResetMethodValue != null)
				{
					this.ResetMethodValue.Invoke(provider, new object[]
					{
						component
					});
					if (componentChangeService != null)
					{
						object newValue = this.ExtenderGetValue(provider, component);
						componentChangeService.OnComponentChanged(component, notifyDesc, oldValue, newValue);
					}
				}
			}
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000EA43C File Offset: 0x000E863C
		internal void ExtenderSetValue(IExtenderProvider provider, object component, object value, PropertyDescriptor notifyDesc)
		{
			if (provider != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object oldValue = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					oldValue = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.SetMethodValue != null)
				{
					this.SetMethodValue.Invoke(provider, new object[]
					{
						component,
						value
					});
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanged(component, notifyDesc, oldValue, value);
					}
				}
			}
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000EA4F4 File Offset: 0x000E86F4
		internal bool ExtenderShouldSerializeValue(IExtenderProvider provider, object component)
		{
			provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[]
						{
							component
						});
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[]
						{
							component
						});
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.ExtenderGetValue(provider, component));
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000EA5D0 File Offset: 0x000E87D0
		public override bool CanResetValue(object component)
		{
			if (this.IsExtender || this.IsReadOnly)
			{
				return false;
			}
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.GetValue(component), this.DefaultValue);
			}
			if (this.ResetMethodValue != null)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					component = this.GetInvocationTarget(this.componentClass, component);
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return this.AmbientValue != ReflectPropertyDescriptor.noValue && this.ShouldSerializeValue(component);
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000EA680 File Offset: 0x000E8880
		protected override void FillAttributes(IList attributes)
		{
			foreach (object obj in TypeDescriptor.GetAttributes(this.PropertyType))
			{
				Attribute value = (Attribute)obj;
				attributes.Add(value);
			}
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			Type baseType = this.componentClass;
			int num = 0;
			while (baseType != null && baseType != typeof(object))
			{
				num++;
				baseType = baseType.BaseType;
			}
			if (num > 0)
			{
				baseType = this.componentClass;
				Attribute[][] array = new Attribute[num][];
				while (baseType != null && baseType != typeof(object))
				{
					MemberInfo memberInfo;
					if (this.IsExtender)
					{
						memberInfo = baseType.GetMethod("Get" + this.Name, bindingAttr, null, new Type[]
						{
							this.receiverType
						}, null);
					}
					else
					{
						memberInfo = baseType.GetProperty(this.Name, bindingAttr, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
					}
					if (memberInfo != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(memberInfo);
					}
					baseType = baseType.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							AttributeProviderAttribute attributeProviderAttribute = attribute as AttributeProviderAttribute;
							if (attributeProviderAttribute != null)
							{
								Type type = Type.GetType(attributeProviderAttribute.TypeName);
								if (type != null)
								{
									Attribute[] array5 = null;
									if (!string.IsNullOrEmpty(attributeProviderAttribute.PropertyName))
									{
										MemberInfo[] member = type.GetMember(attributeProviderAttribute.PropertyName);
										if (member.Length != 0 && member[0] != null)
										{
											array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(member[0]);
										}
									}
									else
									{
										array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(type);
									}
									if (array5 != null)
									{
										foreach (Attribute value2 in array5)
										{
											attributes.Add(value2);
										}
									}
								}
							}
						}
					}
				}
				foreach (Attribute[] array8 in array)
				{
					if (array8 != null)
					{
						foreach (Attribute value3 in array8)
						{
							attributes.Add(value3);
						}
					}
				}
			}
			base.FillAttributes(attributes);
			if (this.SetMethodValue == null)
			{
				attributes.Add(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x000EA91C File Offset: 0x000E8B1C
		public override object GetValue(object component)
		{
			if (this.IsExtender)
			{
				return null;
			}
			if (component != null)
			{
				component = this.GetInvocationTarget(this.componentClass, component);
				try
				{
					return SecurityUtils.MethodInfoInvoke(this.GetMethodValue, component, null);
				}
				catch (Exception innerException)
				{
					string text = null;
					IComponent component2 = component as IComponent;
					if (component2 != null)
					{
						ISite site = component2.Site;
						if (site != null && site.Name != null)
						{
							text = site.Name;
						}
					}
					if (text == null)
					{
						text = component.GetType().FullName;
					}
					if (innerException is TargetInvocationException)
					{
						innerException = innerException.InnerException;
					}
					string text2 = innerException.Message;
					if (text2 == null)
					{
						text2 = innerException.GetType().Name;
					}
					throw new TargetInvocationException(SR.GetString("ErrorPropertyAccessorException", new object[]
					{
						this.Name,
						text,
						text2
					}), innerException);
				}
			}
			return null;
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x000EA9F8 File Offset: 0x000E8BF8
		internal void OnINotifyPropertyChanged(object component, PropertyChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.PropertyName) || string.Compare(e.PropertyName, this.Name, true, CultureInfo.InvariantCulture) == 0)
			{
				this.OnValueChanged(component, e);
			}
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x000EAA28 File Offset: 0x000E8C28
		protected override void OnValueChanged(object component, EventArgs e)
		{
			if (this.state[ReflectPropertyDescriptor.BitChangedQueried] && this.realChangedEvent == null)
			{
				base.OnValueChanged(component, e);
			}
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000EAA4C File Offset: 0x000E8C4C
		public override void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			EventDescriptor changedEventValue = this.ChangedEventValue;
			if (changedEventValue != null && changedEventValue.EventType.IsInstanceOfType(handler))
			{
				changedEventValue.RemoveEventHandler(component, handler);
				return;
			}
			base.RemoveValueChanged(component, handler);
			if (base.GetValueChangedHandler(component) == null)
			{
				EventDescriptor ipropChangedEventValue = this.IPropChangedEventValue;
				if (ipropChangedEventValue != null)
				{
					ipropChangedEventValue.RemoveEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
				}
			}
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000EAAC4 File Offset: 0x000E8CC4
		public override void ResetValue(object component)
		{
			object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.DefaultValue);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.AmbientValue);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object oldValue = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					oldValue = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				if (this.ResetMethodValue != null)
				{
					SecurityUtils.MethodInfoInvoke(this.ResetMethodValue, invocationTarget, null);
					if (componentChangeService != null)
					{
						object newValue = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
						componentChangeService.OnComponentChanged(component, this, oldValue, newValue);
					}
				}
			}
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000EABC0 File Offset: 0x000E8DC0
		public override void SetValue(object component, object value)
		{
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
				if (!this.IsReadOnly)
				{
					if (site != null)
					{
						componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					}
					if (componentChangeService != null)
					{
						obj = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
						try
						{
							componentChangeService.OnComponentChanging(component, this);
						}
						catch (CheckoutException ex)
						{
							if (ex == CheckoutException.Canceled)
							{
								return;
							}
							throw ex;
						}
					}
					try
					{
						SecurityUtils.MethodInfoInvoke(this.SetMethodValue, invocationTarget, new object[]
						{
							value
						});
						this.OnValueChanged(invocationTarget, EventArgs.Empty);
					}
					catch (Exception ex2)
					{
						value = obj;
						if (ex2 is TargetInvocationException && ex2.InnerException != null)
						{
							throw ex2.InnerException;
						}
						throw ex2;
					}
					finally
					{
						if (componentChangeService != null)
						{
							componentChangeService.OnComponentChanged(component, this, obj, value);
						}
					}
				}
			}
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000EACBC File Offset: 0x000E8EBC
		public override bool ShouldSerializeValue(object component)
		{
			component = this.GetInvocationTarget(this.componentClass, component);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.GetValue(component));
		}

		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x060035D7 RID: 13783 RVA: 0x000EAD80 File Offset: 0x000E8F80
		public override bool SupportsChangeEvents
		{
			get
			{
				return this.IPropChangedEventValue != null || this.ChangedEventValue != null;
			}
		}

		// Token: 0x04002A68 RID: 10856
		private static readonly Type[] argsNone = new Type[0];

		// Token: 0x04002A69 RID: 10857
		private static readonly object noValue = new object();

		// Token: 0x04002A6A RID: 10858
		private static TraceSwitch PropDescCreateSwitch = new TraceSwitch("PropDescCreate", "ReflectPropertyDescriptor: Dump errors when creating property info");

		// Token: 0x04002A6B RID: 10859
		private static TraceSwitch PropDescUsageSwitch = new TraceSwitch("PropDescUsage", "ReflectPropertyDescriptor: Debug propertydescriptor usage");

		// Token: 0x04002A6C RID: 10860
		private static TraceSwitch PropDescSwitch = new TraceSwitch("PropDesc", "ReflectPropertyDescriptor: Debug property descriptor");

		// Token: 0x04002A6D RID: 10861
		private static readonly int BitDefaultValueQueried = BitVector32.CreateMask();

		// Token: 0x04002A6E RID: 10862
		private static readonly int BitGetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitDefaultValueQueried);

		// Token: 0x04002A6F RID: 10863
		private static readonly int BitSetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitGetQueried);

		// Token: 0x04002A70 RID: 10864
		private static readonly int BitShouldSerializeQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitSetQueried);

		// Token: 0x04002A71 RID: 10865
		private static readonly int BitResetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitShouldSerializeQueried);

		// Token: 0x04002A72 RID: 10866
		private static readonly int BitChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitResetQueried);

		// Token: 0x04002A73 RID: 10867
		private static readonly int BitIPropChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitChangedQueried);

		// Token: 0x04002A74 RID: 10868
		private static readonly int BitReadOnlyChecked = BitVector32.CreateMask(ReflectPropertyDescriptor.BitIPropChangedQueried);

		// Token: 0x04002A75 RID: 10869
		private static readonly int BitAmbientValueQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitReadOnlyChecked);

		// Token: 0x04002A76 RID: 10870
		private static readonly int BitSetOnDemand = BitVector32.CreateMask(ReflectPropertyDescriptor.BitAmbientValueQueried);

		// Token: 0x04002A77 RID: 10871
		private BitVector32 state;

		// Token: 0x04002A78 RID: 10872
		private Type componentClass;

		// Token: 0x04002A79 RID: 10873
		private Type type;

		// Token: 0x04002A7A RID: 10874
		private object defaultValue;

		// Token: 0x04002A7B RID: 10875
		private object ambientValue;

		// Token: 0x04002A7C RID: 10876
		private PropertyInfo propInfo;

		// Token: 0x04002A7D RID: 10877
		private MethodInfo getMethod;

		// Token: 0x04002A7E RID: 10878
		private MethodInfo setMethod;

		// Token: 0x04002A7F RID: 10879
		private MethodInfo shouldSerializeMethod;

		// Token: 0x04002A80 RID: 10880
		private MethodInfo resetMethod;

		// Token: 0x04002A81 RID: 10881
		private EventDescriptor realChangedEvent;

		// Token: 0x04002A82 RID: 10882
		private EventDescriptor realIPropChangedEvent;

		// Token: 0x04002A83 RID: 10883
		private Type receiverType;
	}
}
