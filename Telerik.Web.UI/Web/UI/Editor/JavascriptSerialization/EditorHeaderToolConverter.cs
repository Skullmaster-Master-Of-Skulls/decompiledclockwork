using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Editor.JavascriptSerialization
{
	// Token: 0x020002BC RID: 700
	internal class EditorHeaderToolConverter : EditorConverterBase
	{
		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001872 RID: 6258 RVA: 0x00050930 File Offset: 0x0004EB30
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(EditorHeaderTool)
				};
			}
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x00050954 File Offset: 0x0004EB54
		public EditorHeaderToolConverter(RadEditor editor)
		{
			this._editor = editor;
			EditorHeaderTool editorHeaderTool = new EditorHeaderTool();
			this.defaultName = editorHeaderTool.Name;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00050980 File Offset: 0x0004EB80
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			EditorHeaderTool editorHeaderTool = obj as EditorHeaderTool;
			if (editorHeaderTool != null && this.defaultName != editorHeaderTool.Name)
			{
				dictionary["name"] = editorHeaderTool.Name;
			}
			return dictionary;
		}

		// Token: 0x0400067D RID: 1661
		private readonly RadEditor _editor;

		// Token: 0x0400067E RID: 1662
		private readonly string defaultName;
	}
}
