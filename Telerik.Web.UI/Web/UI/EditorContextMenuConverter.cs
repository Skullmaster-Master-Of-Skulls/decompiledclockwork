using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001087 RID: 4231
	internal class EditorContextMenuConverter : EditorConverterBase
	{
		// Token: 0x0600AA1B RID: 43547 RVA: 0x0024E25C File Offset: 0x0024C45C
		public EditorContextMenuConverter()
		{
			EditorContextMenu editorContextMenu = new EditorContextMenu();
			this.defaultEnabled = editorContextMenu.Enabled;
			this.defaultTagName = editorContextMenu.TagName;
		}

		// Token: 0x1700369C RID: 13980
		// (get) Token: 0x0600AA1C RID: 43548 RVA: 0x0024E290 File Offset: 0x0024C490
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(EditorContextMenu)
				};
			}
		}

		// Token: 0x0600AA1D RID: 43549 RVA: 0x0024E2B4 File Offset: 0x0024C4B4
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			EditorContextMenu editorContextMenu = obj as EditorContextMenu;
			if (this.defaultEnabled != editorContextMenu.Enabled)
			{
				dictionary["enabled"] = editorContextMenu.Enabled;
			}
			if (this.defaultTagName != editorContextMenu.TagName)
			{
				dictionary["tagName"] = editorContextMenu.TagName;
			}
			dictionary["tools"] = editorContextMenu.Tools;
			return dictionary;
		}

		// Token: 0x04002DBB RID: 11707
		private readonly bool defaultEnabled;

		// Token: 0x04002DBC RID: 11708
		private readonly string defaultTagName;
	}
}
