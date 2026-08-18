using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Editor.Animations;

namespace Telerik.Web.UI.Editor.JavascriptSerialization
{
	// Token: 0x020002BA RID: 698
	internal class EditorAnimationSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001867 RID: 6247 RVA: 0x000506CC File Offset: 0x0004E8CC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(EditorAnimationSettings)
				};
			}
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x000506F0 File Offset: 0x0004E8F0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			EditorAnimationSettings editorAnimationSettings = (EditorAnimationSettings)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "toolBarAnimation", editorAnimationSettings.ToolbarAnimation, null);
			return dictionary;
		}
	}
}
