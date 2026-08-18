using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020001D3 RID: 467
	internal sealed class TypeDescriptorFilterService : ITypeDescriptorFilterService
	{
		// Token: 0x06001162 RID: 4450 RVA: 0x0000362F File Offset: 0x0000182F
		internal TypeDescriptorFilterService()
		{
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000601F4 File Offset: 0x0005E3F4
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

		// Token: 0x06001164 RID: 4452 RVA: 0x00060230 File Offset: 0x0005E430
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

		// Token: 0x06001165 RID: 4453 RVA: 0x00060288 File Offset: 0x0005E488
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

		// Token: 0x06001166 RID: 4454 RVA: 0x000602E0 File Offset: 0x0005E4E0
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
