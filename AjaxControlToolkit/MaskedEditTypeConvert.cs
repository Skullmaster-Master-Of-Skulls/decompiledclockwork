using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x0200013F RID: 319
	public class MaskedEditTypeConvert : StringConverter
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x00015577 File Offset: 0x00013777
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001557A File Offset: 0x0001377A
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00015580 File Offset: 0x00013780
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (context == null || context.Container == null)
			{
				return null;
			}
			object[] controls = MaskedEditTypeConvert.GetControls(context.Container);
			if (controls != null)
			{
				return new TypeConverter.StandardValuesCollection(controls);
			}
			return null;
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000155B4 File Offset: 0x000137B4
		private static object[] GetControls(IContainer container)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in container.Components)
			{
				IComponent component = (IComponent)obj;
				Control control = component as Control;
				if (control != null && !(control is Page) && control.ID != null && control.ID.Length != 0 && MaskedEditTypeConvert.IncludeControl(control))
				{
					arrayList.Add(control.ID);
				}
			}
			arrayList.Sort(Comparer.Default);
			return arrayList.ToArray();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001565C File Offset: 0x0001385C
		private static bool IncludeControl(Control serverControl)
		{
			bool result = false;
			string text = serverControl.GetType().ToString();
			if (text.IndexOf("Sys.Extended.UI.maskededitextender", StringComparison.OrdinalIgnoreCase) != -1)
			{
				result = true;
			}
			return result;
		}
	}
}
