using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003C4 RID: 964
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ControlIDConverter : StringConverter
	{
		// Token: 0x06002F3B RID: 12091 RVA: 0x000D2B62 File Offset: 0x000D1B62
		protected virtual bool FilterControl(Control control)
		{
			return true;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x000D2B68 File Offset: 0x000D1B68
		private string[] GetControls(IDesignerHost host, object instance)
		{
			IContainer container = host.Container;
			IComponent component = instance as IComponent;
			if (component != null && component.Site != null)
			{
				container = component.Site.Container;
			}
			if (container == null)
			{
				return null;
			}
			ComponentCollection components = container.Components;
			ArrayList arrayList = new ArrayList();
			foreach (object obj in ((IEnumerable)components))
			{
				IComponent component2 = (IComponent)obj;
				Control control = component2 as Control;
				if (control != null && control != instance && control != host.RootComponent && control.ID != null && control.ID.Length > 0 && this.FilterControl(control))
				{
					arrayList.Add(control.ID);
				}
			}
			arrayList.Sort(Comparer.Default);
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x000D2C60 File Offset: 0x000D1C60
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context == null)
			{
				return null;
			}
			IDesignerHost designerHost = (IDesignerHost)context.GetService(typeof(IDesignerHost));
			if (designerHost == null)
			{
				return null;
			}
			string[] controls = this.GetControls(designerHost, context.Instance);
			if (controls == null)
			{
				return null;
			}
			return new TypeConverter.StandardValuesCollection(controls);
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x000D2CA6 File Offset: 0x000D1CA6
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x000D2CA9 File Offset: 0x000D1CA9
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null;
		}
	}
}
