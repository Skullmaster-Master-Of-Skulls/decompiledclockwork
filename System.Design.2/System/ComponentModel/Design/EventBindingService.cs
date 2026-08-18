using System;
using System.Collections;
using System.Collections.Generic;
using System.Design;
using System.Globalization;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.Internal.Performance;

namespace System.ComponentModel.Design
{
	// Token: 0x020001CB RID: 459
	[SecurityCritical]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public abstract class EventBindingService : IEventBindingService
	{
		// Token: 0x06001116 RID: 4374 RVA: 0x0005ED27 File Offset: 0x0005CF27
		protected EventBindingService(IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._provider = provider;
		}

		// Token: 0x06001117 RID: 4375
		protected abstract string CreateUniqueMethodName(IComponent component, EventDescriptor e);

		// Token: 0x06001118 RID: 4376 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void FreeMethod(IComponent component, EventDescriptor e, string methodName)
		{
		}

		// Token: 0x06001119 RID: 4377
		protected abstract ICollection GetCompatibleMethods(EventDescriptor e);

		// Token: 0x0600111A RID: 4378 RVA: 0x0005ED44 File Offset: 0x0005CF44
		protected object GetService(Type serviceType)
		{
			if (this._provider != null)
			{
				return this._provider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x0600111B RID: 4379
		protected abstract bool ShowCode();

		// Token: 0x0600111C RID: 4380
		protected abstract bool ShowCode(int lineNumber);

		// Token: 0x0600111D RID: 4381
		protected abstract bool ShowCode(IComponent component, EventDescriptor e, string methodName);

		// Token: 0x0600111E RID: 4382 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void UseMethod(IComponent component, EventDescriptor e, string methodName)
		{
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00003937 File Offset: 0x00001B37
		protected virtual void ValidateMethodName(string methodName)
		{
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0005ED5C File Offset: 0x0005CF5C
		string IEventBindingService.CreateUniqueMethodName(IComponent component, EventDescriptor e)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			return this.CreateUniqueMethodName(component, e);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0005ED82 File Offset: 0x0005CF82
		ICollection IEventBindingService.GetCompatibleMethods(EventDescriptor e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			return this.GetCompatibleMethods(e);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x0005ED99 File Offset: 0x0005CF99
		EventDescriptor IEventBindingService.GetEvent(PropertyDescriptor property)
		{
			if (property is EventBindingService.EventPropertyDescriptor)
			{
				return ((EventBindingService.EventPropertyDescriptor)property).Event;
			}
			return null;
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0005EDB0 File Offset: 0x0005CFB0
		private bool HasGenericArgument(EventDescriptor ed)
		{
			if (ed == null || ed.ComponentType == null)
			{
				return false;
			}
			EventInfo @event = ed.ComponentType.GetEvent(ed.Name);
			if (@event == null || !@event.EventHandlerType.IsGenericType)
			{
				return false;
			}
			Type[] genericArguments = @event.EventHandlerType.GetGenericArguments();
			if (genericArguments != null && genericArguments.Length != 0)
			{
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (genericArguments[i].IsGenericType)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0005EE28 File Offset: 0x0005D028
		PropertyDescriptorCollection IEventBindingService.GetEventProperties(EventDescriptorCollection events)
		{
			if (events == null)
			{
				throw new ArgumentNullException("events");
			}
			List<PropertyDescriptor> list = new List<PropertyDescriptor>(events.Count);
			for (int i = 0; i < events.Count; i++)
			{
				if (!this.HasGenericArgument(events[i]))
				{
					PropertyDescriptor item = new EventBindingService.EventPropertyDescriptor(events[i], this);
					list.Add(item);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x0005EE90 File Offset: 0x0005D090
		PropertyDescriptor IEventBindingService.GetEventProperty(EventDescriptor e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			return new EventBindingService.EventPropertyDescriptor(e, this);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x0005EEB4 File Offset: 0x0005D0B4
		bool IEventBindingService.ShowCode()
		{
			return this.ShowCode();
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0005EEBC File Offset: 0x0005D0BC
		bool IEventBindingService.ShowCode(int lineNumber)
		{
			return this.ShowCode(lineNumber);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0005EEC8 File Offset: 0x0005D0C8
		bool IEventBindingService.ShowCode(IComponent component, EventDescriptor e)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			PropertyDescriptor eventProperty = ((IEventBindingService)this).GetEventProperty(e);
			string text = (string)eventProperty.GetValue(component);
			if (text == null)
			{
				return false;
			}
			this.showCodeComponent = component;
			this.showCodeEventDescriptor = e;
			this.showCodeMethodName = text;
			Application.Idle += this.ShowCodeIdle;
			return true;
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0005EF34 File Offset: 0x0005D134
		private void ShowCodeIdle(object sender, EventArgs e)
		{
			Application.Idle -= this.ShowCodeIdle;
			try
			{
				this.ShowCode(this.showCodeComponent, this.showCodeEventDescriptor, this.showCodeMethodName);
			}
			finally
			{
				this.showCodeComponent = null;
				this.showCodeEventDescriptor = null;
				this.showCodeMethodName = null;
				EventBindingService.codemarkers.CodeMarker(7505);
			}
		}

		// Token: 0x040009AB RID: 2475
		private IServiceProvider _provider;

		// Token: 0x040009AC RID: 2476
		private IComponent showCodeComponent;

		// Token: 0x040009AD RID: 2477
		private EventDescriptor showCodeEventDescriptor;

		// Token: 0x040009AE RID: 2478
		private string showCodeMethodName;

		// Token: 0x040009AF RID: 2479
		private static CodeMarkers codemarkers = CodeMarkers.Instance;

		// Token: 0x0200049D RID: 1181
		private class EventPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06002B78 RID: 11128 RVA: 0x00103B33 File Offset: 0x00101D33
			internal EventPropertyDescriptor(EventDescriptor eventDesc, EventBindingService eventSvc) : base(eventDesc, null)
			{
				this._eventDesc = eventDesc;
				this._eventSvc = eventSvc;
			}

			// Token: 0x06002B79 RID: 11129 RVA: 0x00103B4B File Offset: 0x00101D4B
			public override bool CanResetValue(object component)
			{
				return this.GetValue(component) != null;
			}

			// Token: 0x1700092B RID: 2347
			// (get) Token: 0x06002B7A RID: 11130 RVA: 0x00103B57 File Offset: 0x00101D57
			public override Type ComponentType
			{
				get
				{
					return this._eventDesc.ComponentType;
				}
			}

			// Token: 0x1700092C RID: 2348
			// (get) Token: 0x06002B7B RID: 11131 RVA: 0x00103B64 File Offset: 0x00101D64
			public override TypeConverter Converter
			{
				get
				{
					if (this._converter == null)
					{
						this._converter = new EventBindingService.EventPropertyDescriptor.EventConverter(this._eventDesc);
					}
					return this._converter;
				}
			}

			// Token: 0x1700092D RID: 2349
			// (get) Token: 0x06002B7C RID: 11132 RVA: 0x00103B85 File Offset: 0x00101D85
			internal EventDescriptor Event
			{
				get
				{
					return this._eventDesc;
				}
			}

			// Token: 0x1700092E RID: 2350
			// (get) Token: 0x06002B7D RID: 11133 RVA: 0x00103B8D File Offset: 0x00101D8D
			public override bool IsReadOnly
			{
				get
				{
					return this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
				}
			}

			// Token: 0x1700092F RID: 2351
			// (get) Token: 0x06002B7E RID: 11134 RVA: 0x00103BAE File Offset: 0x00101DAE
			public override Type PropertyType
			{
				get
				{
					return this._eventDesc.EventType;
				}
			}

			// Token: 0x06002B7F RID: 11135 RVA: 0x00103BBC File Offset: 0x00101DBC
			public override object GetValue(object component)
			{
				if (component == null)
				{
					throw new ArgumentNullException("component");
				}
				ISite site = null;
				if (component is IComponent)
				{
					site = ((IComponent)component).Site;
				}
				if (site == null)
				{
					IReferenceService referenceService = this._eventSvc._provider.GetService(typeof(IReferenceService)) as IReferenceService;
					if (referenceService != null)
					{
						IComponent component2 = referenceService.GetComponent(component);
						if (component2 != null)
						{
							site = component2.Site;
						}
					}
				}
				if (site == null)
				{
					return null;
				}
				IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
				if (dictionaryService == null)
				{
					return null;
				}
				return (string)dictionaryService.GetValue(new EventBindingService.EventPropertyDescriptor.ReferenceEventClosure(component, this));
			}

			// Token: 0x06002B80 RID: 11136 RVA: 0x00103C57 File Offset: 0x00101E57
			public override void ResetValue(object component)
			{
				this.SetValue(component, null);
			}

			// Token: 0x06002B81 RID: 11137 RVA: 0x00103C64 File Offset: 0x00101E64
			public override void SetValue(object component, object value)
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.GetString("EventBindingServiceEventReadOnly", new object[]
					{
						this.Name
					}))
					{
						HelpLink = "EventBindingServiceEventReadOnly"
					};
				}
				if (value != null && !(value is string))
				{
					throw new ArgumentException(SR.GetString("EventBindingServiceBadArgType", new object[]
					{
						this.Name,
						typeof(string).Name
					}))
					{
						HelpLink = "EventBindingServiceBadArgType"
					};
				}
				string text = (string)value;
				if (text != null && text.Length == 0)
				{
					text = null;
				}
				ISite site = null;
				if (component is IComponent)
				{
					site = ((IComponent)component).Site;
				}
				if (site == null)
				{
					IReferenceService referenceService = this._eventSvc._provider.GetService(typeof(IReferenceService)) as IReferenceService;
					if (referenceService != null)
					{
						IComponent component2 = referenceService.GetComponent(component);
						if (component2 != null)
						{
							site = component2.Site;
						}
					}
				}
				if (site == null)
				{
					throw new InvalidOperationException(SR.GetString("EventBindingServiceNoSite"))
					{
						HelpLink = "EventBindingServiceNoSite"
					};
				}
				IDictionaryService dictionaryService = site.GetService(typeof(IDictionaryService)) as IDictionaryService;
				if (dictionaryService == null)
				{
					throw new InvalidOperationException(SR.GetString("EventBindingServiceMissingService", new object[]
					{
						typeof(IDictionaryService).Name
					}))
					{
						HelpLink = "EventBindingServiceMissingService"
					};
				}
				EventBindingService.EventPropertyDescriptor.ReferenceEventClosure key = new EventBindingService.EventPropertyDescriptor.ReferenceEventClosure(component, this);
				string text2 = (string)dictionaryService.GetValue(key);
				if (text2 == text)
				{
					return;
				}
				if (text2 != null && text != null && text2.Equals(text))
				{
					return;
				}
				if (text != null)
				{
					this._eventSvc.ValidateMethodName(text);
				}
				IDesignerHost designerHost = site.GetService(typeof(IDesignerHost)) as IDesignerHost;
				DesignerTransaction designerTransaction = null;
				if (designerHost != null)
				{
					designerTransaction = designerHost.CreateTransaction(SR.GetString("EventBindingServiceSetValue", new object[]
					{
						site.Name,
						text
					}));
				}
				try
				{
					IComponentChangeService componentChangeService = site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
					if (componentChangeService != null)
					{
						try
						{
							componentChangeService.OnComponentChanging(component, this);
							componentChangeService.OnComponentChanging(component, this.Event);
						}
						catch (CheckoutException ex)
						{
							if (ex == CheckoutException.Canceled)
							{
								return;
							}
							throw;
						}
					}
					if (text != null)
					{
						this._eventSvc.UseMethod((IComponent)component, this._eventDesc, text);
					}
					if (text2 != null)
					{
						this._eventSvc.FreeMethod((IComponent)component, this._eventDesc, text2);
					}
					dictionaryService.SetValue(key, text);
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanged(component, this.Event, null, null);
						componentChangeService.OnComponentChanged(component, this, text2, text);
					}
					this.OnValueChanged(component, EventArgs.Empty);
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
					}
				}
				finally
				{
					if (designerTransaction != null)
					{
						((IDisposable)designerTransaction).Dispose();
					}
				}
			}

			// Token: 0x06002B82 RID: 11138 RVA: 0x00103F3C File Offset: 0x0010213C
			public override bool ShouldSerializeValue(object component)
			{
				return this.CanResetValue(component);
			}

			// Token: 0x04001E2B RID: 7723
			private EventDescriptor _eventDesc;

			// Token: 0x04001E2C RID: 7724
			private EventBindingService _eventSvc;

			// Token: 0x04001E2D RID: 7725
			private TypeConverter _converter;

			// Token: 0x020005D7 RID: 1495
			private class EventConverter : TypeConverter
			{
				// Token: 0x0600345D RID: 13405 RVA: 0x0011CF36 File Offset: 0x0011B136
				internal EventConverter(EventDescriptor evt)
				{
					this._evt = evt;
				}

				// Token: 0x0600345E RID: 13406 RVA: 0x00010631 File Offset: 0x0000E831
				public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
				{
					return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
				}

				// Token: 0x0600345F RID: 13407 RVA: 0x00010664 File Offset: 0x0000E864
				public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
				{
					return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
				}

				// Token: 0x06003460 RID: 13408 RVA: 0x0011CF45 File Offset: 0x0011B145
				public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
				{
					if (value == null)
					{
						return value;
					}
					if (!(value is string))
					{
						return base.ConvertFrom(context, culture, value);
					}
					if (((string)value).Length == 0)
					{
						return null;
					}
					return value;
				}

				// Token: 0x06003461 RID: 13409 RVA: 0x0011CF6E File Offset: 0x0011B16E
				public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
				{
					if (!(destinationType == typeof(string)))
					{
						return base.ConvertTo(context, culture, value, destinationType);
					}
					if (value != null)
					{
						return value;
					}
					return string.Empty;
				}

				// Token: 0x06003462 RID: 13410 RVA: 0x0011CF9C File Offset: 0x0011B19C
				public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
				{
					string[] array = null;
					if (context != null)
					{
						IEventBindingService eventBindingService = (IEventBindingService)context.GetService(typeof(IEventBindingService));
						if (eventBindingService != null)
						{
							ICollection compatibleMethods = eventBindingService.GetCompatibleMethods(this._evt);
							array = new string[compatibleMethods.Count];
							int num = 0;
							foreach (object obj in compatibleMethods)
							{
								string text = (string)obj;
								array[num++] = text;
							}
						}
					}
					return new TypeConverter.StandardValuesCollection(array);
				}

				// Token: 0x06003463 RID: 13411 RVA: 0x0000445B File Offset: 0x0000265B
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return false;
				}

				// Token: 0x06003464 RID: 13412 RVA: 0x00003B0F File Offset: 0x00001D0F
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x04002308 RID: 8968
				private EventDescriptor _evt;
			}

			// Token: 0x020005D8 RID: 1496
			private class ReferenceEventClosure
			{
				// Token: 0x06003465 RID: 13413 RVA: 0x0011D03C File Offset: 0x0011B23C
				public ReferenceEventClosure(object reference, EventBindingService.EventPropertyDescriptor prop)
				{
					this.reference = reference;
					this.propertyDescriptor = prop;
				}

				// Token: 0x06003466 RID: 13414 RVA: 0x0011D052 File Offset: 0x0011B252
				public override int GetHashCode()
				{
					return this.reference.GetHashCode() * this.propertyDescriptor.GetHashCode();
				}

				// Token: 0x06003467 RID: 13415 RVA: 0x0011D06C File Offset: 0x0011B26C
				public override bool Equals(object otherClosure)
				{
					if (otherClosure is EventBindingService.EventPropertyDescriptor.ReferenceEventClosure)
					{
						EventBindingService.EventPropertyDescriptor.ReferenceEventClosure referenceEventClosure = (EventBindingService.EventPropertyDescriptor.ReferenceEventClosure)otherClosure;
						return referenceEventClosure.reference == this.reference && referenceEventClosure.propertyDescriptor.Equals(this.propertyDescriptor);
					}
					return false;
				}

				// Token: 0x04002309 RID: 8969
				private object reference;

				// Token: 0x0400230A RID: 8970
				private EventBindingService.EventPropertyDescriptor propertyDescriptor;
			}
		}
	}
}
