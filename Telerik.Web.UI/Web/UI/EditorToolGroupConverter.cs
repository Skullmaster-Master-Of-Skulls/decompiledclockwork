using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x0200108A RID: 4234
	internal class EditorToolGroupConverter : EditorConverterBase
	{
		// Token: 0x0600AA22 RID: 43554 RVA: 0x0024E380 File Offset: 0x0024C580
		public EditorToolGroupConverter()
		{
			EditorToolGroup editorToolGroup = new EditorToolGroup();
			this.defaultTag = editorToolGroup.Tag;
		}

		// Token: 0x1700369F RID: 13983
		// (get) Token: 0x0600AA23 RID: 43555 RVA: 0x0024E3A8 File Offset: 0x0024C5A8
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(EditorToolGroup)
				};
			}
		}

		// Token: 0x0600AA24 RID: 43556 RVA: 0x0024E3CC File Offset: 0x0024C5CC
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			EditorToolGroup editorToolGroup = obj as EditorToolGroup;
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(editorToolGroup.Context))
			{
				dictionary2["context"] = editorToolGroup.Context;
			}
			if (!string.IsNullOrEmpty(editorToolGroup.Tab))
			{
				dictionary2["tab"] = editorToolGroup.Tab;
			}
			foreach (object obj2 in editorToolGroup.Attributes.Keys)
			{
				string text = (string)obj2;
				dictionary2[text.ToLowerInvariant()] = editorToolGroup.Attributes[text];
			}
			if (dictionary2.Count > 0)
			{
				dictionary["attributes"] = dictionary2;
			}
			if (this.defaultTag != editorToolGroup.Tag)
			{
				dictionary["tag"] = editorToolGroup.Tag;
			}
			dictionary["tools"] = editorToolGroup.Tools;
			return dictionary;
		}

		// Token: 0x04002DBD RID: 11709
		private readonly string defaultTag;
	}
}
