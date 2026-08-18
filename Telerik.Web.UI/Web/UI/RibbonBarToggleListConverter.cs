using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000F29 RID: 3881
	internal class RibbonBarToggleListConverter : JavaScriptConverter
	{
		// Token: 0x06009400 RID: 37888 RVA: 0x00212F31 File Offset: 0x00211131
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06009401 RID: 37889 RVA: 0x00212F38 File Offset: 0x00211138
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			RibbonBarToggleList toggleList = obj as RibbonBarToggleList;
			dictionary["buttonIndices"] = this.GetToggleButtonIndicesInsideGroup(toggleList);
			return dictionary;
		}

		// Token: 0x06009402 RID: 37890 RVA: 0x00212F68 File Offset: 0x00211168
		private int[] GetToggleButtonIndicesInsideGroup(RibbonBarToggleList toggleList)
		{
			RibbonBarGroup ribbonBarGroup = toggleList.ParentWebControl as RibbonBarGroup;
			List<RibbonBarItem> visibleFunctionalItems = ribbonBarGroup.GetVisibleFunctionalItems();
			List<RibbonBarToggleButton> visibleButtons = toggleList.GetVisibleButtons();
			int[] array = new int[visibleButtons.Count];
			for (int i = 0; i < visibleButtons.Count; i++)
			{
				array[i] = visibleFunctionalItems.IndexOf(visibleButtons[i]);
			}
			return array;
		}

		// Token: 0x17002ED2 RID: 11986
		// (get) Token: 0x06009403 RID: 37891 RVA: 0x00213090 File Offset: 0x00211290
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RibbonBarToggleList);
				yield break;
			}
		}
	}
}
