using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000E34 RID: 3636
	internal class RibbonBarColorPickerConverter : JavaScriptConverter
	{
		// Token: 0x06008990 RID: 35216 RVA: 0x001F5D6C File Offset: 0x001F3F6C
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008991 RID: 35217 RVA: 0x001F5D74 File Offset: 0x001F3F74
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarColorPicker ribbonBarColorPicker = (RibbonBarColorPicker)obj;
			Color selectedColor = ribbonBarColorPicker.SelectedColor;
			if (!selectedColor.Equals(Color.Empty))
			{
				dictionary["selectedColor"] = string.Format("#{0:X2}{1:X2}{2:X2}", selectedColor.R, selectedColor.G, selectedColor.B);
			}
			if (!string.IsNullOrEmpty(ribbonBarColorPicker.ToolTip))
			{
				dictionary["toolTip"] = ribbonBarColorPicker.ToolTip;
			}
			return dictionary;
		}

		// Token: 0x17002B8A RID: 11146
		// (get) Token: 0x06008992 RID: 35218 RVA: 0x001F5ED4 File Offset: 0x001F40D4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarColorPicker);
				yield break;
			}
		}
	}
}
