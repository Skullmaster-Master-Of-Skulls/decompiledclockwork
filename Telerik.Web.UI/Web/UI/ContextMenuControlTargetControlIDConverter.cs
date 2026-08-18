using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020019FB RID: 6651
	internal class ContextMenuControlTargetControlIDConverter : TypeConverter
	{
		// Token: 0x06010196 RID: 65942 RVA: 0x0039E694 File Offset: 0x0039C894
		private static string[] GetControls(IDesignerHost host, ContextMenuControlTarget target)
		{
			IContainer container = host.Container;
			IComponent owner = target.Owner;
			if (owner != null && owner.Site != null)
			{
				container = owner.Site.Container;
			}
			if (container == null)
			{
				return null;
			}
			ComponentCollection components = container.Components;
			List<string> list = new List<string>();
			foreach (object obj in components)
			{
				IComponent component = (IComponent)obj;
				Control control = component as Control;
				if (control != null && control != target.Owner && control != host.RootComponent && control.ID != null && control.ID.Length > 0)
				{
					list.Add(control.ID);
				}
			}
			list.Sort();
			return list.ToArray();
		}

		// Token: 0x06010197 RID: 65943 RVA: 0x0039E774 File Offset: 0x0039C974
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context == null)
			{
				return null;
			}
			ContextMenuControlTarget contextMenuControlTarget = (ContextMenuControlTarget)context.Instance;
			IDesignerHost designerHost = (IDesignerHost)contextMenuControlTarget.Owner.Site.GetService(typeof(IDesignerHost));
			if (designerHost == null)
			{
				return null;
			}
			string[] controls = ContextMenuControlTargetControlIDConverter.GetControls(designerHost, (ContextMenuControlTarget)context.Instance);
			if (controls == null)
			{
				return null;
			}
			return new TypeConverter.StandardValuesCollection(controls);
		}

		// Token: 0x06010198 RID: 65944 RVA: 0x0039E7D4 File Offset: 0x0039C9D4
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06010199 RID: 65945 RVA: 0x0039E7D7 File Offset: 0x0039C9D7
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null;
		}
	}
}
