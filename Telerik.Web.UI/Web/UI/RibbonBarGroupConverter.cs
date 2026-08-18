using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000F2D RID: 3885
	internal class RibbonBarGroupConverter : JavaScriptConverter
	{
		// Token: 0x06009420 RID: 37920 RVA: 0x002137F9 File Offset: 0x002119F9
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009421 RID: 37921 RVA: 0x00213800 File Offset: 0x00211A00
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarGroup ribbonBarGroup = obj as RibbonBarGroup;
			dictionary["index"] = this.GetHierarhcicalIndex(ribbonBarGroup);
			if (!string.IsNullOrEmpty(ribbonBarGroup.Value))
			{
				dictionary["value"] = ribbonBarGroup.Value;
			}
			if (!string.IsNullOrEmpty(ribbonBarGroup.CollapsedImageUrl))
			{
				dictionary["collapsedImageUrl"] = ribbonBarGroup.CollapsedImageUrl;
			}
			List<RibbonBarItem> serializableItems = ribbonBarGroup.GetSerializableItems(true);
			if (serializableItems.Count > 0)
			{
				dictionary["itemData"] = serializableItems;
			}
			List<RibbonBarToggleList> toggleLists = ribbonBarGroup.GetToggleLists();
			if (toggleLists.Count > 0)
			{
				dictionary["toggleListData"] = toggleLists;
			}
			return dictionary;
		}

		// Token: 0x06009422 RID: 37922 RVA: 0x002138A4 File Offset: 0x00211AA4
		private string GetHierarhcicalIndex(RibbonBarGroup group)
		{
			int num = group.Tab.RibbonBar.GetTabsToRender().IndexOf(group.Tab);
			int num2 = group.Tab.GetVisibleGroups().IndexOf(group);
			return string.Format("{0}:{1}", num, num2);
		}

		// Token: 0x17002ED6 RID: 11990
		// (get) Token: 0x06009423 RID: 37923 RVA: 0x002139C4 File Offset: 0x00211BC4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarGroup);
				yield break;
			}
		}
	}
}
