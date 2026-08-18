using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005A2 RID: 1442
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ReflectEventDescriptor : EventDescriptor
	{
		// Token: 0x060035A8 RID: 13736 RVA: 0x000E90F4 File Offset: 0x000E72F4
		public ReflectEventDescriptor(Type componentClass, string name, Type type, Attribute[] attributes) : base(name, attributes)
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[]
				{
					"componentClass"
				}));
			}
			if (type == null || !typeof(Delegate).IsAssignableFrom(type))
			{
				throw new ArgumentException(SR.GetString("ErrorInvalidEventType", new object[]
				{
					name
				}));
			}
			this.componentClass = componentClass;
			this.type = type;
		}

		// Token: 0x060035A9 RID: 13737 RVA: 0x000E9174 File Offset: 0x000E7374
		public ReflectEventDescriptor(Type componentClass, EventInfo eventInfo) : base(eventInfo.Name, new Attribute[0])
		{
			if (componentClass == null)
			{
				throw new ArgumentException(SR.GetString("InvalidNullArgument", new object[]
				{
					"componentClass"
				}));
			}
			this.componentClass = componentClass;
			this.realEvent = eventInfo;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x000E91C8 File Offset: 0x000E73C8
		public ReflectEventDescriptor(Type componentType, EventDescriptor oldReflectEventDescriptor, Attribute[] attributes) : base(oldReflectEventDescriptor, attributes)
		{
			this.componentClass = componentType;
			this.type = oldReflectEventDescriptor.EventType;
			ReflectEventDescriptor reflectEventDescriptor = oldReflectEventDescriptor as ReflectEventDescriptor;
			if (reflectEventDescriptor != null)
			{
				this.addMethod = reflectEventDescriptor.addMethod;
				this.removeMethod = reflectEventDescriptor.removeMethod;
				this.filledMethods = true;
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x060035AB RID: 13739 RVA: 0x000E9219 File Offset: 0x000E7419
		public override Type ComponentType
		{
			get
			{
				return this.componentClass;
			}
		}

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000E9221 File Offset: 0x000E7421
		public override Type EventType
		{
			get
			{
				this.FillMethods();
				return this.type;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060035AD RID: 13741 RVA: 0x000E922F File Offset: 0x000E742F
		public override bool IsMulticast
		{
			get
			{
				return typeof(MulticastDelegate).IsAssignableFrom(this.EventType);
			}
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x000E9248 File Offset: 0x000E7448
		public override void AddEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
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
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					if (this.EventType != value.GetType())
					{
						throw new ArgumentException(SR.GetString("ErrorInvalidEventHandler", new object[]
						{
							this.Name
						}));
					}
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Combine(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					SecurityUtils.MethodInfoInvoke(this.addMethod, component, new object[]
					{
						value
					});
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000E9358 File Offset: 0x000E7558
		protected override void FillAttributes(IList attributes)
		{
			this.FillMethods();
			if (this.realEvent != null)
			{
				this.FillEventInfoAttribute(this.realEvent, attributes);
			}
			else
			{
				this.FillSingleMethodAttribute(this.removeMethod, attributes);
				this.FillSingleMethodAttribute(this.addMethod, attributes);
			}
			base.FillAttributes(attributes);
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x000E93AC File Offset: 0x000E75AC
		private void FillEventInfoAttribute(EventInfo realEventInfo, IList attributes)
		{
			string name = realEventInfo.Name;
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realEventInfo.ReflectedType;
			int num = 0;
			while (type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realEventInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != typeof(object))
				{
					MemberInfo @event = type.GetEvent(name, bindingAttr);
					if (@event != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(@event);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute value in array3)
						{
							attributes.Add(value);
						}
					}
				}
			}
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x000E9488 File Offset: 0x000E7688
		private void FillMethods()
		{
			if (this.filledMethods)
			{
				return;
			}
			if (this.realEvent != null)
			{
				this.addMethod = this.realEvent.GetAddMethod();
				this.removeMethod = this.realEvent.GetRemoveMethod();
				EventInfo eventInfo = null;
				if (this.addMethod == null || this.removeMethod == null)
				{
					Type baseType = this.componentClass.BaseType;
					while (baseType != null && baseType != typeof(object))
					{
						BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
						EventInfo @event = baseType.GetEvent(this.realEvent.Name, bindingAttr);
						if (@event.GetAddMethod() != null)
						{
							eventInfo = @event;
							break;
						}
					}
				}
				if (eventInfo != null)
				{
					this.addMethod = eventInfo.GetAddMethod();
					this.removeMethod = eventInfo.GetRemoveMethod();
					this.type = eventInfo.EventHandlerType;
				}
				else
				{
					this.type = this.realEvent.EventHandlerType;
				}
			}
			else
			{
				this.realEvent = this.componentClass.GetEvent(this.Name);
				if (this.realEvent != null)
				{
					this.FillMethods();
					return;
				}
				Type[] args = new Type[]
				{
					this.type
				};
				this.addMethod = MemberDescriptor.FindMethod(this.componentClass, "AddOn" + this.Name, args, typeof(void));
				this.removeMethod = MemberDescriptor.FindMethod(this.componentClass, "RemoveOn" + this.Name, args, typeof(void));
				if (this.addMethod == null || this.removeMethod == null)
				{
					throw new ArgumentException(SR.GetString("ErrorMissingEventAccessors", new object[]
					{
						this.Name
					}));
				}
			}
			this.filledMethods = true;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000E9664 File Offset: 0x000E7864
		private void FillSingleMethodAttribute(MethodInfo realMethodInfo, IList attributes)
		{
			string name = realMethodInfo.Name;
			BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realMethodInfo.ReflectedType;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realMethodInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo method = type.GetMethod(name, bindingAttr);
					if (method != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(method);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute value in array3)
						{
							attributes.Add(value);
						}
					}
				}
			}
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000E9754 File Offset: 0x000E7954
		public override void RemoveEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
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
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Remove(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					SecurityUtils.MethodInfoInvoke(this.removeMethod, component, new object[]
					{
						value
					});
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x04002A60 RID: 10848
		private static readonly Type[] argsNone = new Type[0];

		// Token: 0x04002A61 RID: 10849
		private static readonly object noDefault = new object();

		// Token: 0x04002A62 RID: 10850
		private Type type;

		// Token: 0x04002A63 RID: 10851
		private readonly Type componentClass;

		// Token: 0x04002A64 RID: 10852
		private MethodInfo addMethod;

		// Token: 0x04002A65 RID: 10853
		private MethodInfo removeMethod;

		// Token: 0x04002A66 RID: 10854
		private EventInfo realEvent;

		// Token: 0x04002A67 RID: 10855
		private bool filledMethods;
	}
}
