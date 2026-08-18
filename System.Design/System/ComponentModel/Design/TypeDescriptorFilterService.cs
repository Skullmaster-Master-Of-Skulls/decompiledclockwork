using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x0200056C RID: 1388
	internal sealed class TypeDescriptorFilterService : ITypeDescriptorFilterService
	{
		// Token: 0x06003107 RID: 12551 RVA: 0x001152F3 File Offset: 0x001142F3
		internal TypeDescriptorFilterService()
		{
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x001152FC File Offset: 0x001142FC
		private IDesigner GetDesigner(IComponent component)
		{
			ISite site = component.Site;
			if (site != null)
			{
				IDesignerHost designerHost = site.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					return designerHost.GetDesigner(component);
				}
			}
			return null;
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x00115338 File Offset: 0x00114338
		bool ITypeDescriptorFilterService.FilterAttributes(IComponent component, IDictionary attributes)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			IDesigner designer = this.GetDesigner(component);
			if (designer is IDesignerFilter)
			{
				((IDesignerFilter)designer).PreFilterAttributes(attributes);
				((IDesignerFilter)designer).PostFilterAttributes(attributes);
			}
			return designer != null;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x00115390 File Offset: 0x00114390
		bool ITypeDescriptorFilterService.FilterEvents(IComponent component, IDictionary events)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (events == null)
			{
				throw new ArgumentNullException("events");
			}
			IDesigner designer = this.GetDesigner(component);
			if (designer is IDesignerFilter)
			{
				((IDesignerFilter)designer).PreFilterEvents(events);
				((IDesignerFilter)designer).PostFilterEvents(events);
			}
			return designer != null;
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x001153E8 File Offset: 0x001143E8
		bool ITypeDescriptorFilterService.FilterProperties(IComponent component, IDictionary properties)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (properties == null)
			{
				throw new ArgumentNullException("properties");
			}
			IDesigner designer = this.GetDesigner(component);
			if (designer is IDesignerFilter)
			{
				((IDesignerFilter)designer).PreFilterProperties(properties);
				((IDesignerFilter)designer).PostFilterProperties(properties);
			}
			return designer != null;
		}
	}
}
