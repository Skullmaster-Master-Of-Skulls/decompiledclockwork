using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000301 RID: 769
	internal class GanttKeyboardNavigationConverter : JavaScriptConverter
	{
		// Token: 0x06001A41 RID: 6721 RVA: 0x000554F6 File Offset: 0x000536F6
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x00055500 File Offset: 0x00053700
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			GanttKeyboardNavigationSettings ganttKeyboardNavigationSettings = obj as GanttKeyboardNavigationSettings;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["commandKey"] = ganttKeyboardNavigationSettings.CommandKey;
			dictionary["focusKey"] = ganttKeyboardNavigationSettings.FocusKey;
			return dictionary;
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x00055614 File Offset: 0x00053814
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(GanttKeyboardNavigationSettings);
				yield break;
			}
		}
	}
}
