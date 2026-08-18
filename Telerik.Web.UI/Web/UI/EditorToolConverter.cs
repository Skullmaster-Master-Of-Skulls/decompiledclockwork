using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020012A3 RID: 4771
	internal class EditorToolConverter : EditorConverterBase
	{
		// Token: 0x17004095 RID: 16533
		// (get) Token: 0x0600C7E4 RID: 51172 RVA: 0x002C875C File Offset: 0x002C695C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(EditorTool),
					typeof(EditorSeparator),
					typeof(EditorToolStrip)
				};
			}
		}

		// Token: 0x0600C7E5 RID: 51173 RVA: 0x002C8798 File Offset: 0x002C6998
		public EditorToolConverter(RadEditor editor)
		{
			this._editor = editor;
			EditorTool editorTool = new EditorTool();
			this.defaultEnabled = editorTool.Enabled;
			this.defaultName = editorTool.Name;
			this.defaultShortCut = editorTool.ShortCut;
			this.defaultShowIcon = editorTool.ShowIcon;
			this.defaultShowText = editorTool.ShowText;
			this.defaultText = editorTool.Text;
			this.defaultType = editorTool.Type;
			this.defaultVisible = editorTool.Visible;
		}

		// Token: 0x0600C7E6 RID: 51174 RVA: 0x002C8818 File Offset: 0x002C6A18
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			EditorTool editorTool = obj as EditorTool;
			if (editorTool != null)
			{
				if (editorTool.Attributes.Count > 0)
				{
					Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
					foreach (object obj2 in editorTool.Attributes.Keys)
					{
						string text = (string)obj2;
						dictionary2[text.ToLowerInvariant()] = editorTool.Attributes[text];
					}
					dictionary["attributes"] = dictionary2;
				}
				if (this.defaultEnabled != editorTool.Enabled)
				{
					dictionary["enabled"] = editorTool.Enabled;
				}
				if (this.defaultName != editorTool.Name)
				{
					dictionary["name"] = editorTool.Name;
				}
				if (this.defaultShortCut != editorTool.ShortCut)
				{
					dictionary["shortCut"] = editorTool.ShortCut;
				}
				if (this.defaultShowIcon != editorTool.ShowIcon)
				{
					dictionary["showIcon"] = editorTool.ShowIcon;
				}
				if (this.defaultShowText != editorTool.ShowText)
				{
					dictionary["showText"] = editorTool.ShowText;
				}
				string text2 = editorTool.Text;
				if (string.IsNullOrEmpty(text2) && editorTool.InToolStrip)
				{
					text2 = this._editor.Localization.Tools.GetString(editorTool.Name);
				}
				if (this.defaultText != text2)
				{
					dictionary["text"] = text2;
				}
				if (this.defaultType != editorTool.Type)
				{
					dictionary["type"] = editorTool.Type;
				}
				if (this.defaultVisible != editorTool.Visible)
				{
					dictionary["visible"] = editorTool.Visible;
				}
				EditorDropDown editorDropDown = obj as EditorDropDown;
				if (editorDropDown != null)
				{
					dictionary["items"] = editorDropDown.Items.GetItemsCollection();
				}
			}
			else
			{
				EditorSeparator editorSeparator = obj as EditorSeparator;
				if (editorSeparator != null)
				{
					dictionary["type"] = editorSeparator.Type;
				}
				else
				{
					EditorToolStrip editorToolStrip = obj as EditorToolStrip;
					if (editorToolStrip != null)
					{
						if (editorToolStrip.Attributes.Count > 0)
						{
							Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
							foreach (object obj3 in editorToolStrip.Attributes.Keys)
							{
								string text3 = (string)obj3;
								dictionary3[text3.ToLowerInvariant()] = editorToolStrip.Attributes[text3];
							}
							dictionary["attributes"] = dictionary3;
						}
						if (this.defaultName != editorToolStrip.Name)
						{
							dictionary["name"] = editorToolStrip.Name;
						}
						dictionary["type"] = editorToolStrip.Type;
						dictionary["showText"] = editorToolStrip.ShowText;
						dictionary["text"] = editorToolStrip.Text;
						dictionary["tools"] = editorToolStrip.Tools;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0400349A RID: 13466
		private readonly RadEditor _editor;

		// Token: 0x0400349B RID: 13467
		private readonly bool defaultEnabled;

		// Token: 0x0400349C RID: 13468
		private readonly bool defaultShowIcon;

		// Token: 0x0400349D RID: 13469
		private readonly bool defaultShowText;

		// Token: 0x0400349E RID: 13470
		private readonly bool defaultVisible;

		// Token: 0x0400349F RID: 13471
		private readonly string defaultName;

		// Token: 0x040034A0 RID: 13472
		private readonly string defaultShortCut;

		// Token: 0x040034A1 RID: 13473
		private readonly string defaultText;

		// Token: 0x040034A2 RID: 13474
		private readonly EditorToolType defaultType;
	}
}
