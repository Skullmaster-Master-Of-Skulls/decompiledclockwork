using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200036E RID: 878
	public class ControlPropertyNameConverter : StringConverter
	{
		// Token: 0x06002875 RID: 10357 RVA: 0x00082D1C File Offset: 0x00080F1C
		private string[] GetPropertyNames(Control control)
		{
			ArrayList arrayList = new ArrayList();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(control.GetType());
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				arrayList.Add(propertyDescriptor.Name);
			}
			arrayList.Sort(Comparer.Default);
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x00082DAC File Offset: 0x00080FAC
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context == null)
			{
				return null;
			}
			ControlParameter controlParameter = (ControlParameter)context.Instance;
			string controlID = controlParameter.ControlID;
			if (string.IsNullOrEmpty(controlID))
			{
				return null;
			}
			IDesignerHost designerHost = (IDesignerHost)context.GetService(typeof(IDesignerHost));
			if (designerHost == null)
			{
				return null;
			}
			ComponentCollection components = designerHost.Container.Components;
			Control control = components[controlID] as Control;
			if (control == null)
			{
				return null;
			}
			string[] propertyNames = this.GetPropertyNames(control);
			return new TypeConverter.StandardValuesCollection(propertyNames);
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00007722 File Offset: 0x00005922
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x00082D0E File Offset: 0x00080F0E
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return context != null;
		}
	}
}
