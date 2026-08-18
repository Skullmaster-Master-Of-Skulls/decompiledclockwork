using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000E38 RID: 3640
	internal class RibbonBarComboBoxConverter : JavaScriptConverter
	{
		// Token: 0x060089A0 RID: 35232 RVA: 0x001F6339 File Offset: 0x001F4539
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060089A1 RID: 35233 RVA: 0x001F6340 File Offset: 0x001F4540
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarComboBox ribbonBarComboBox = (RibbonBarComboBox)obj;
			if (!string.IsNullOrEmpty(ribbonBarComboBox.Text))
			{
				dictionary["text"] = ribbonBarComboBox.Text;
			}
			if (!string.IsNullOrEmpty(ribbonBarComboBox.ToolTip))
			{
				dictionary["toolTip"] = ribbonBarComboBox.ToolTip;
			}
			IList<RibbonBarListItem> visibleItems = ribbonBarComboBox.GetVisibleItems();
			if (visibleItems.Count > 0)
			{
				dictionary["comboBoxItemData"] = visibleItems;
			}
			return dictionary;
		}

		// Token: 0x17002B8E RID: 11150
		// (get) Token: 0x060089A2 RID: 35234 RVA: 0x001F6480 File Offset: 0x001F4680
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarComboBox);
				yield break;
			}
		}
	}
}
