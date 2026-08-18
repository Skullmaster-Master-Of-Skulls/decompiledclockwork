using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200048A RID: 1162
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public class EventsTab : PropertyTab
	{
		// Token: 0x06004E1B RID: 19995 RVA: 0x001426C8 File Offset: 0x001408C8
		public EventsTab(IServiceProvider sp)
		{
			this.sp = sp;
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06004E1C RID: 19996 RVA: 0x001426D7 File Offset: 0x001408D7
		public override string TabName
		{
			get
			{
				return SR.GetString("PBRSToolTipEvents");
			}
		}

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06004E1D RID: 19997 RVA: 0x001426E3 File Offset: 0x001408E3
		public override string HelpKeyword
		{
			get
			{
				return "Events";
			}
		}

		// Token: 0x06004E1E RID: 19998 RVA: 0x001426EA File Offset: 0x001408EA
		public override bool CanExtend(object extendee)
		{
			return !Marshal.IsComObject(extendee);
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x001426F5 File Offset: 0x001408F5
		private void OnActiveDesignerChanged(object sender, ActiveDesignerEventArgs adevent)
		{
			this.currentHost = adevent.NewDesigner;
		}

		// Token: 0x06004E20 RID: 20000 RVA: 0x00142704 File Offset: 0x00140904
		public override PropertyDescriptor GetDefaultProperty(object obj)
		{
			IEventBindingService eventPropertyService = this.GetEventPropertyService(obj, null);
			if (eventPropertyService == null)
			{
				return null;
			}
			EventDescriptor defaultEvent = TypeDescriptor.GetDefaultEvent(obj);
			if (defaultEvent != null)
			{
				return eventPropertyService.GetEventProperty(defaultEvent);
			}
			return null;
		}

		// Token: 0x06004E21 RID: 20001 RVA: 0x00142734 File Offset: 0x00140934
		private IEventBindingService GetEventPropertyService(object obj, ITypeDescriptorContext context)
		{
			IEventBindingService eventBindingService = null;
			if (!this.sunkEvent)
			{
				IDesignerEventService designerEventService = (IDesignerEventService)this.sp.GetService(typeof(IDesignerEventService));
				if (designerEventService != null)
				{
					designerEventService.ActiveDesignerChanged += this.OnActiveDesignerChanged;
				}
				this.sunkEvent = true;
			}
			if (eventBindingService == null && this.currentHost != null)
			{
				eventBindingService = (IEventBindingService)this.currentHost.GetService(typeof(IEventBindingService));
			}
			if (eventBindingService == null && obj is IComponent)
			{
				ISite site = ((IComponent)obj).Site;
				if (site != null)
				{
					eventBindingService = (IEventBindingService)site.GetService(typeof(IEventBindingService));
				}
			}
			if (eventBindingService == null && context != null)
			{
				eventBindingService = (IEventBindingService)context.GetService(typeof(IEventBindingService));
			}
			return eventBindingService;
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x001427F5 File Offset: 0x001409F5
		public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			return this.GetProperties(null, component, attributes);
		}

		// Token: 0x06004E23 RID: 20003 RVA: 0x00142800 File Offset: 0x00140A00
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
		{
			IEventBindingService eventPropertyService = this.GetEventPropertyService(component, context);
			if (eventPropertyService == null)
			{
				return new PropertyDescriptorCollection(null);
			}
			EventDescriptorCollection events = TypeDescriptor.GetEvents(component, attributes);
			PropertyDescriptorCollection propertyDescriptorCollection = eventPropertyService.GetEventProperties(events);
			Attribute[] array = new Attribute[attributes.Length + 1];
			Array.Copy(attributes, 0, array, 0, attributes.Length);
			array[attributes.Length] = DesignerSerializationVisibilityAttribute.Content;
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component, array);
			if (properties.Count > 0)
			{
				ArrayList arrayList = null;
				for (int i = 0; i < properties.Count; i++)
				{
					PropertyDescriptor propertyDescriptor = properties[i];
					TypeConverter converter = propertyDescriptor.Converter;
					if (converter.GetPropertiesSupported())
					{
						object value = propertyDescriptor.GetValue(component);
						EventDescriptorCollection events2 = TypeDescriptor.GetEvents(value, attributes);
						if (events2.Count > 0)
						{
							if (arrayList == null)
							{
								arrayList = new ArrayList();
							}
							propertyDescriptor = TypeDescriptor.CreateProperty(propertyDescriptor.ComponentType, propertyDescriptor, new Attribute[]
							{
								MergablePropertyAttribute.No
							});
							arrayList.Add(propertyDescriptor);
						}
					}
				}
				if (arrayList != null)
				{
					PropertyDescriptor[] array2 = new PropertyDescriptor[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					PropertyDescriptor[] array3 = new PropertyDescriptor[propertyDescriptorCollection.Count + array2.Length];
					propertyDescriptorCollection.CopyTo(array3, 0);
					Array.Copy(array2, 0, array3, propertyDescriptorCollection.Count, array2.Length);
					propertyDescriptorCollection = new PropertyDescriptorCollection(array3);
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x040033F7 RID: 13303
		private IServiceProvider sp;

		// Token: 0x040033F8 RID: 13304
		private IDesignerHost currentHost;

		// Token: 0x040033F9 RID: 13305
		private bool sunkEvent;
	}
}
