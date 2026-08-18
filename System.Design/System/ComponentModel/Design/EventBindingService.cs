using System;
using System.Collections;
using System.Collections.Generic;
using System.Design;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using Microsoft.Internal.Performance;

namespace System.ComponentModel.Design
{
	// Token: 0x02000560 RID: 1376
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public abstract class EventBindingService : IEventBindingService
	{
		// Token: 0x0600309F RID: 12447 RVA: 0x00113643 File Offset: 0x00112643
		protected EventBindingService(IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._provider = provider;
		}

		// Token: 0x060030A0 RID: 12448
		protected abstract string CreateUniqueMethodName(IComponent component, EventDescriptor e);

		// Token: 0x060030A1 RID: 12449 RVA: 0x00113660 File Offset: 0x00112660
		protected virtual void FreeMethod(IComponent component, EventDescriptor e, string methodName)
		{
		}

		// Token: 0x060030A2 RID: 12450
		protected abstract ICollection GetCompatibleMethods(EventDescriptor e);

		// Token: 0x060030A3 RID: 12451 RVA: 0x00113664 File Offset: 0x00112664
		private string GetEventDescriptorHashCode(EventDescriptor eventDesc)
		{
			StringBuilder stringBuilder = new StringBuilder(eventDesc.Name);
			stringBuilder.Append(eventDesc.EventType.GetHashCode().ToString(CultureInfo.InvariantCulture));
			foreach (object obj in eventDesc.Attributes)
			{
				Attribute attribute = (Attribute)obj;
				stringBuilder.Append(attribute.GetHashCode().ToString(CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x00113708 File Offset: 0x00112708
		protected object GetService(Type serviceType)
		{
			if (this._provider != null)
			{
				return this._provider.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x060030A5 RID: 12453
		protected abstract bool ShowCode();

		// Token: 0x060030A6 RID: 12454
		protected abstract bool ShowCode(int lineNumber);

		// Token: 0x060030A7 RID: 12455
		protected abstract bool ShowCode(IComponent component, EventDescriptor e, string methodName);

		// Token: 0x060030A8 RID: 12456 RVA: 0x00113720 File Offset: 0x00112720
		protected virtual void UseMethod(IComponent component, EventDescriptor e, string methodName)
		{
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x00113722 File Offset: 0x00112722
		protected virtual void ValidateMethodName(string methodName)
		{
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x00113724 File Offset: 0x00112724
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

		// Token: 0x060030AB RID: 12459 RVA: 0x0011374A File Offset: 0x0011274A
		ICollection IEventBindingService.GetCompatibleMethods(EventDescriptor e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			return this.GetCompatibleMethods(e);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x00113761 File Offset: 0x00112761
		EventDescriptor IEventBindingService.GetEvent(PropertyDescriptor property)
		{
			if (property is EventBindingService.EventPropertyDescriptor)
			{
				return ((EventBindingService.EventPropertyDescriptor)property).Event;
			}
			return null;
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x00113778 File Offset: 0x00112778
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
			if (genericArguments != null && genericArguments.Length > 0)
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

		// Token: 0x060030AE RID: 12462 RVA: 0x001137E8 File Offset: 0x001127E8
		PropertyDescriptorCollection IEventBindingService.GetEventProperties(EventDescriptorCollection events)
		{
			if (events == null)
			{
				throw new ArgumentNullException("events");
			}
			List<PropertyDescriptor> list = new List<PropertyDescriptor>(events.Count);
			if (this._eventProperties == null)
			{
				this._eventProperties = new Hashtable();
			}
			for (int i = 0; i < events.Count; i++)
			{
				if (!this.HasGenericArgument(events[i]))
				{
					object eventDescriptorHashCode = this.GetEventDescriptorHashCode(events[i]);
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)this._eventProperties[eventDescriptorHashCode];
					if (propertyDescriptor == null)
					{
						propertyDescriptor = new EventBindingService.EventPropertyDescriptor(events[i], this);
						this._eventProperties[eventDescriptorHashCode] = propertyDescriptor;
					}
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x00113894 File Offset: 0x00112894
		PropertyDescriptor IEventBindingService.GetEventProperty(EventDescriptor e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (this._eventProperties == null)
			{
				this._eventProperties = new Hashtable();
			}
			object eventDescriptorHashCode = this.GetEventDescriptorHashCode(e);
			PropertyDescriptor propertyDescriptor = (PropertyDescriptor)this._eventProperties[eventDescriptorHashCode];
			if (propertyDescriptor == null)
			{
				propertyDescriptor = new EventBindingService.EventPropertyDescriptor(e, this);
				this._eventProperties[eventDescriptorHashCode] = propertyDescriptor;
			}
			return propertyDescriptor;
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x001138F5 File Offset: 0x001128F5
		bool IEventBindingService.ShowCode()
		{
			return this.ShowCode();
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x001138FD File Offset: 0x001128FD
		bool IEventBindingService.ShowCode(int lineNumber)
		{
			return this.ShowCode(lineNumber);
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x00113908 File Offset: 0x00112908
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

		// Token: 0x060030B3 RID: 12467 RVA: 0x00113974 File Offset: 0x00112974
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
				EventBindingService.codemarkers.CodeMarker(CodeMarkerEvent.perfFXDesignShowCode);
			}
		}

		// Token: 0x040020AC RID: 8364
		private Hashtable _eventProperties;

		// Token: 0x040020AD RID: 8365
		private IServiceProvider _provider;

		// Token: 0x040020AE RID: 8366
		private IComponent showCodeComponent;

		// Token: 0x040020AF RID: 8367
		private EventDescriptor showCodeEventDescriptor;

		// Token: 0x040020B0 RID: 8368
		private string showCodeMethodName;

		// Token: 0x040020B1 RID: 8369
		private static CodeMarkers codemarkers = CodeMarkers.Instance;

		// Token: 0x02000561 RID: 1377
		private class EventPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x060030B5 RID: 12469 RVA: 0x001139F0 File Offset: 0x001129F0
			internal EventPropertyDescriptor(EventDescriptor eventDesc, EventBindingService eventSvc) : base(eventDesc, null)
			{
				this._eventDesc = eventDesc;
				this._eventSvc = eventSvc;
			}

			// Token: 0x060030B6 RID: 12470 RVA: 0x00113A08 File Offset: 0x00112A08
			public override bool CanResetValue(object component)
			{
				return this.GetValue(component) != null;
			}

			// Token: 0x1700091F RID: 2335
			// (get) Token: 0x060030B7 RID: 12471 RVA: 0x00113A17 File Offset: 0x00112A17
			public override Type ComponentType
			{
				get
				{
					return this._eventDesc.ComponentType;
				}
			}

			// Token: 0x17000920 RID: 2336
			// (get) Token: 0x060030B8 RID: 12472 RVA: 0x00113A24 File Offset: 0x00112A24
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

			// Token: 0x17000921 RID: 2337
			// (get) Token: 0x060030B9 RID: 12473 RVA: 0x00113A45 File Offset: 0x00112A45
			internal EventDescriptor Event
			{
				get
				{
					return this._eventDesc;
				}
			}

			// Token: 0x17000922 RID: 2338
			// (get) Token: 0x060030BA RID: 12474 RVA: 0x00113A4D File Offset: 0x00112A4D
			public override bool IsReadOnly
			{
				get
				{
					return this.Attributes[typeof(ReadOnlyAttribute)].Equals(ReadOnlyAttribute.Yes);
				}
			}

			// Token: 0x17000923 RID: 2339
			// (get) Token: 0x060030BB RID: 12475 RVA: 0x00113A6E File Offset: 0x00112A6E
			public override Type PropertyType
			{
				get
				{
					return this._eventDesc.EventType;
				}
			}

			// Token: 0x060030BC RID: 12476 RVA: 0x00113A7C File Offset: 0x00112A7C
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

			// Token: 0x060030BD RID: 12477 RVA: 0x00113B17 File Offset: 0x00112B17
			public override void ResetValue(object component)
			{
				this.SetValue(component, null);
			}

			// Token: 0x060030BE RID: 12478 RVA: 0x00113B24 File Offset: 0x00112B24
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
				if (object.ReferenceEquals(text2, text))
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

			// Token: 0x060030BF RID: 12479 RVA: 0x00113E14 File Offset: 0x00112E14
			public override bool ShouldSerializeValue(object component)
			{
				return this.CanResetValue(component);
			}

			// Token: 0x040020B2 RID: 8370
			private EventDescriptor _eventDesc;

			// Token: 0x040020B3 RID: 8371
			private EventBindingService _eventSvc;

			// Token: 0x040020B4 RID: 8372
			private TypeConverter _converter;

			// Token: 0x02000562 RID: 1378
			private class EventConverter : TypeConverter
			{
				// Token: 0x060030C0 RID: 12480 RVA: 0x00113E1D File Offset: 0x00112E1D
				internal EventConverter(EventDescriptor evt)
				{
					this._evt = evt;
				}

				// Token: 0x060030C1 RID: 12481 RVA: 0x00113E2C File Offset: 0x00112E2C
				public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
				{
					return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
				}

				// Token: 0x060030C2 RID: 12482 RVA: 0x00113E45 File Offset: 0x00112E45
				public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
				{
					return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
				}

				// Token: 0x060030C3 RID: 12483 RVA: 0x00113E5E File Offset: 0x00112E5E
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

				// Token: 0x060030C4 RID: 12484 RVA: 0x00113E87 File Offset: 0x00112E87
				public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
				{
					if (destinationType != typeof(string))
					{
						return base.ConvertTo(context, culture, value, destinationType);
					}
					if (value != null)
					{
						return value;
					}
					return string.Empty;
				}

				// Token: 0x060030C5 RID: 12485 RVA: 0x00113EB0 File Offset: 0x00112EB0
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

				// Token: 0x060030C6 RID: 12486 RVA: 0x00113F50 File Offset: 0x00112F50
				public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
				{
					return false;
				}

				// Token: 0x060030C7 RID: 12487 RVA: 0x00113F53 File Offset: 0x00112F53
				public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
				{
					return true;
				}

				// Token: 0x040020B5 RID: 8373
				private EventDescriptor _evt;
			}

			// Token: 0x02000563 RID: 1379
			private class ReferenceEventClosure
			{
				// Token: 0x060030C8 RID: 12488 RVA: 0x00113F56 File Offset: 0x00112F56
				public ReferenceEventClosure(object reference, EventBindingService.EventPropertyDescriptor prop)
				{
					this.reference = reference;
					this.propertyDescriptor = prop;
				}

				// Token: 0x060030C9 RID: 12489 RVA: 0x00113F6C File Offset: 0x00112F6C
				public override int GetHashCode()
				{
					return this.reference.GetHashCode() * this.propertyDescriptor.GetHashCode();
				}

				// Token: 0x060030CA RID: 12490 RVA: 0x00113F88 File Offset: 0x00112F88
				public override bool Equals(object otherClosure)
				{
					if (otherClosure is EventBindingService.EventPropertyDescriptor.ReferenceEventClosure)
					{
						EventBindingService.EventPropertyDescriptor.ReferenceEventClosure referenceEventClosure = (EventBindingService.EventPropertyDescriptor.ReferenceEventClosure)otherClosure;
						return referenceEventClosure.reference == this.reference && referenceEventClosure.propertyDescriptor.Equals(this.propertyDescriptor);
					}
					return false;
				}

				// Token: 0x040020B6 RID: 8374
				private object reference;

				// Token: 0x040020B7 RID: 8375
				private EventBindingService.EventPropertyDescriptor propertyDescriptor;
			}
		}
	}
}
