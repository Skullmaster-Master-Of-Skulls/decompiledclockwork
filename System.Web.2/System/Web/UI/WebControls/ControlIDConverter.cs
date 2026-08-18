using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200036D RID: 877
	public class ControlIDConverter : StringConverter
	{
		// Token: 0x0600286F RID: 10351 RVA: 0x000097B7 File Offset: 0x000079B7
		protected virtual bool FilterControl(Control control)
		{
			return true;
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x00082BD0 File Offset: 0x00080DD0
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

		// Token: 0x06002871 RID: 10353 RVA: 0x00082CC8 File Offset: 0x00080EC8
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

		// Token: 0x06002872 RID: 10354 RVA: 0x00007722 File Offset: 0x00005922
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x00082D0E File Offset: 0x00080F0E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null;
		}
	}
}
