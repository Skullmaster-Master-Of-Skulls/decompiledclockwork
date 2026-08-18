using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Xml;

namespace Telerik.Web.UI
{
	// Token: 0x02001096 RID: 4246
	internal class ToolsFileLoader
	{
		// Token: 0x0600AC96 RID: 44182 RVA: 0x002509E4 File Offset: 0x0024EBE4
		public void LoadColors(EditorColorCollection colors)
		{
			colors.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/colors/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorColor item = new EditorColor(ColorTranslator.FromHtml(xmlNode.Attributes["value"].Value));
				colors.Add(item);
			}
		}

		// Token: 0x0600AC97 RID: 44183 RVA: 0x00250A74 File Offset: 0x0024EC74
		public void LoadContextMenus(EditorContextMenuCollection contextMenus)
		{
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/contextMenus/*"))
			{
				XmlNode contextMenuXml = (XmlNode)obj;
				EditorContextMenu editorContextMenu = this.LoadContextMenu(contextMenuXml);
				if (this._editor.ContextMenus.FindByTagName(editorContextMenu.TagName) == null)
				{
					contextMenus.Add(editorContextMenu);
				}
			}
			foreach (object obj2 in this._editor.DefaultContextMenus)
			{
				EditorContextMenu editorContextMenu2 = (EditorContextMenu)obj2;
				if (this._editor.ContextMenus.FindByTagName(editorContextMenu2.TagName) == null)
				{
					contextMenus.Add(editorContextMenu2);
				}
			}
		}

		// Token: 0x0600AC98 RID: 44184 RVA: 0x00250B6C File Offset: 0x0024ED6C
		public void LoadCssClasses(EditorCssClassCollection cssClasses)
		{
			cssClasses.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/classes/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorCssClass item = new EditorCssClass(xmlNode.Attributes["name"].Value, xmlNode.Attributes["value"].Value);
				cssClasses.Add(item);
			}
		}

		// Token: 0x0600AC99 RID: 44185 RVA: 0x00250C0C File Offset: 0x0024EE0C
		public void LoadCssFiles(EditorCssFileCollection cssFiles)
		{
			cssFiles.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/cssFiles/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorCssFile item = new EditorCssFile(xmlNode.Attributes["name"].Value);
				cssFiles.Add(item);
			}
		}

		// Token: 0x0600AC9A RID: 44186 RVA: 0x00250C98 File Offset: 0x0024EE98
		public void LoadFontNames(EditorFontCollection fonts)
		{
			fonts.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/fontNames/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorFont item = new EditorFont(xmlNode.Attributes["name"].Value);
				fonts.Add(item);
			}
		}

		// Token: 0x0600AC9B RID: 44187 RVA: 0x00250D24 File Offset: 0x0024EF24
		public void LoadFontSizes(EditorFontSizeCollection fontSizes)
		{
			fontSizes.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/fontSizes/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorFontSize item = new EditorFontSize(xmlNode.InnerText);
				fontSizes.Add(item);
			}
		}

		// Token: 0x0600AC9C RID: 44188 RVA: 0x00250DA0 File Offset: 0x0024EFA0
		public void LoadLanguages(SpellCheckerLanguageCollection languages)
		{
			languages.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/languages/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				SpellCheckerLanguage item = new SpellCheckerLanguage(xmlNode.Attributes["code"].Value, xmlNode.Attributes["title"].Value);
				languages.Add(item);
			}
		}

		// Token: 0x0600AC9D RID: 44189 RVA: 0x00250E40 File Offset: 0x0024F040
		public void LoadLinks(EditorLinkCollection links)
		{
			links.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/links/*"))
			{
				XmlNode linkXml = (XmlNode)obj;
				links.Add(this.LoadLink(linkXml));
			}
		}

		// Token: 0x0600AC9E RID: 44190 RVA: 0x00250EB4 File Offset: 0x0024F0B4
		public void LoadModules(EditorModuleCollection modules)
		{
			modules.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/modules/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorModule editorModule = new EditorModule();
				foreach (object obj2 in xmlNode.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj2;
					string a;
					if ((a = xmlAttribute.Name.ToLowerInvariant()) != null)
					{
						if (a == "enabled")
						{
							editorModule.Enabled = ToolsFileLoader.ParseBool(xmlAttribute.Value, true);
							continue;
						}
						if (a == "visible")
						{
							editorModule.Visible = ToolsFileLoader.ParseBool(xmlAttribute.Value, true);
							continue;
						}
						if (a == "name")
						{
							editorModule.Name = xmlAttribute.Value;
							continue;
						}
						if (a == "scriptfile")
						{
							editorModule.ScriptFile = xmlAttribute.Value;
							continue;
						}
					}
					editorModule.Attributes[xmlAttribute.Name] = xmlAttribute.Value;
				}
				modules.Add(editorModule);
			}
		}

		// Token: 0x0600AC9F RID: 44191 RVA: 0x0025103C File Offset: 0x0024F23C
		public void LoadParagraphs(EditorParagraphCollection paragraphs)
		{
			paragraphs.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/paragraphs/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorParagraph item = new EditorParagraph(xmlNode.Attributes["value"].Value, xmlNode.Attributes["name"].Value);
				paragraphs.Add(item);
			}
		}

		// Token: 0x0600ACA0 RID: 44192 RVA: 0x002510DC File Offset: 0x0024F2DC
		public void LoadFormatSets(EditorFormatSetCollection formatSets)
		{
			formatSets.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/formatSets/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorFormatSet editorFormatSet = new EditorFormatSet(xmlNode.Attributes["tag"].Value, xmlNode.Attributes["title"].Value);
				foreach (object obj2 in xmlNode.SelectNodes("attributes/item"))
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					EditorFormatSetAttribute item = new EditorFormatSetAttribute(xmlNode2.Attributes["name"].Value, xmlNode2.Attributes["value"].Value);
					editorFormatSet.Attributes.Add(item);
				}
				formatSets.Add(editorFormatSet);
			}
		}

		// Token: 0x0600ACA1 RID: 44193 RVA: 0x00251210 File Offset: 0x0024F410
		public void LoadRealFontSizes(EditorRealFontSizeCollection realFontSizes)
		{
			realFontSizes.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/realFontSizes/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorRealFontSize item = new EditorRealFontSize(xmlNode.Attributes["value"].Value);
				realFontSizes.Add(item);
			}
		}

		// Token: 0x0600ACA2 RID: 44194 RVA: 0x0025129C File Offset: 0x0024F49C
		public void LoadSnippets(EditorSnippetCollection snippets)
		{
			snippets.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/snippets/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorSnippet item = new EditorSnippet(xmlNode.Attributes["name"].Value, xmlNode.InnerText);
				snippets.Add(item);
			}
		}

		// Token: 0x0600ACA3 RID: 44195 RVA: 0x0025132C File Offset: 0x0024F52C
		public void LoadSymbols(EditorSymbolCollection symbols)
		{
			symbols.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/symbols/*"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorSymbol item = new EditorSymbol(ToolsFileLoader.ParseSymbol(xmlNode.Attributes["value"].Value, ' '));
				symbols.Add(item);
			}
		}

		// Token: 0x0600ACA4 RID: 44196 RVA: 0x002513BC File Offset: 0x0024F5BC
		public void LoadTools(EditorToolGroupCollection tools)
		{
			tools.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/EditorToolGroup|root/tools"))
			{
				XmlNode groupXml = (XmlNode)obj;
				tools.Add(this.LoadGroup(groupXml));
			}
		}

		// Token: 0x0600ACA5 RID: 44197 RVA: 0x00251430 File Offset: 0x0024F630
		public void LoadHeaderTools(EditorHeaderToolCollection headerTools)
		{
			headerTools.Clear();
			XmlNodeList xmlNodeList = this._editor.ToolsFileContent.SelectNodes("root/HeaderTools|root/headertools");
			if (xmlNodeList != null && xmlNodeList.Count > 0)
			{
				XmlNode xmlNode = xmlNodeList.Item(0);
				foreach (object obj in xmlNode)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					if (xmlNode2.NodeType == XmlNodeType.Element)
					{
						headerTools.Add(this.LoadHeaderTool(xmlNode2));
					}
				}
			}
		}

		// Token: 0x0600ACA6 RID: 44198 RVA: 0x002514C8 File Offset: 0x0024F6C8
		public ToolsFileLoader(RadEditor editor)
		{
			this._editor = editor;
		}

		// Token: 0x0600ACA7 RID: 44199 RVA: 0x002514D8 File Offset: 0x0024F6D8
		private EditorHeaderTool LoadHeaderTool(XmlNode tool)
		{
			string name = string.Empty;
			EditorHeaderToolPosition position = EditorHeaderToolPosition.Left;
			foreach (object obj in tool.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string a;
				if ((a = xmlAttribute.Name.ToLowerInvariant()) != null)
				{
					if (!(a == "name"))
					{
						if (a == "position")
						{
							position = (EditorHeaderToolPosition)Enum.Parse(typeof(EditorHeaderToolPosition), xmlAttribute.Value, true);
						}
					}
					else
					{
						name = xmlAttribute.Value;
					}
				}
			}
			return new EditorHeaderTool(name, position);
		}

		// Token: 0x0600ACA8 RID: 44200 RVA: 0x00251590 File Offset: 0x0024F790
		private EditorLink LoadLink(XmlNode linkXml)
		{
			EditorLink editorLink = new EditorLink();
			foreach (object obj in linkXml.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string a;
				if ((a = xmlAttribute.Name.ToLowerInvariant()) != null)
				{
					if (!(a == "name"))
					{
						if (!(a == "href"))
						{
							if (!(a == "target"))
							{
								if (a == "tooltip")
								{
									editorLink.ToolTip = xmlAttribute.Value;
								}
							}
							else
							{
								editorLink.Target = xmlAttribute.Value;
							}
						}
						else
						{
							editorLink.Href = xmlAttribute.Value;
						}
					}
					else
					{
						editorLink.Name = xmlAttribute.Value;
					}
				}
			}
			foreach (object obj2 in linkXml)
			{
				XmlNode linkXml2 = (XmlNode)obj2;
				editorLink.ChildLinks.Add(this.LoadLink(linkXml2));
			}
			return editorLink;
		}

		// Token: 0x0600ACA9 RID: 44201 RVA: 0x002516C8 File Offset: 0x0024F8C8
		private EditorToolGroup LoadGroup(XmlNode groupXml)
		{
			EditorToolGroup editorToolGroup = new EditorToolGroup();
			foreach (object obj in groupXml.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string text = xmlAttribute.Name.ToLowerInvariant();
				string a;
				if ((a = text) != null)
				{
					if (a == "name")
					{
						editorToolGroup.Tag = groupXml.Attributes["name"].Value;
						continue;
					}
					if (a == "tab")
					{
						editorToolGroup.Tab = xmlAttribute.Value;
						continue;
					}
				}
				editorToolGroup.Attributes[text] = xmlAttribute.Value;
			}
			foreach (object obj2 in groupXml.SelectNodes("EditorTool|tool|EditorToolStrip"))
			{
				XmlNode toolXml = (XmlNode)obj2;
				EditorToolBase editorToolBase = this.LoadTool(toolXml);
				if (editorToolBase != null)
				{
					editorToolGroup.Tools.Add(editorToolBase);
				}
			}
			return editorToolGroup;
		}

		// Token: 0x0600ACAA RID: 44202 RVA: 0x00251800 File Offset: 0x0024FA00
		private EditorContextMenu LoadContextMenu(XmlNode contextMenuXml)
		{
			EditorContextMenu editorContextMenu = new EditorContextMenu();
			if (contextMenuXml.Attributes["forElement"] != null)
			{
				editorContextMenu.TagName = contextMenuXml.Attributes["forElement"].Value;
			}
			else if (contextMenuXml.Attributes["forelement"] != null)
			{
				editorContextMenu.TagName = contextMenuXml.Attributes["forelement"].Value;
			}
			if (contextMenuXml.Attributes["enabled"] != null)
			{
				editorContextMenu.Enabled = ToolsFileLoader.ParseBool(contextMenuXml.Attributes["enabled"].Value, true);
			}
			List<EditorTool> items = this.LoadContextMenuItemsFromNode(contextMenuXml);
			editorContextMenu.Tools.AddRange(items);
			return editorContextMenu;
		}

		// Token: 0x0600ACAB RID: 44203 RVA: 0x002518B8 File Offset: 0x0024FAB8
		private List<EditorTool> LoadContextMenuItemsFromNode(XmlNode toolNode)
		{
			List<EditorTool> list = new List<EditorTool>();
			foreach (object obj in toolNode.SelectNodes("EditorTool|tool"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				EditorTool editorTool = (EditorTool)this.LoadTool(xmlNode);
				if (editorTool != null)
				{
					list.Add(editorTool);
					List<EditorTool> list2 = this.LoadContextMenuItemsFromNode(xmlNode);
					if (list2.Count > 0)
					{
						(editorTool as EditorContextMenuTool).Tools.AddRange(list2);
					}
				}
			}
			return list;
		}

		// Token: 0x0600ACAC RID: 44204 RVA: 0x00251958 File Offset: 0x0024FB58
		private EditorToolBase LoadTool(XmlNode toolXml)
		{
			if (toolXml == null)
			{
				return null;
			}
			XmlNodeList xmlNodeList = toolXml.SelectNodes("EditorTool|tool");
			if (xmlNodeList != null && xmlNodeList.Count > 0 && !this.IsContextMenuTool(toolXml))
			{
				return this.LoadToolStrip(toolXml, xmlNodeList);
			}
			EditorToolType editorToolType = EditorToolType.Button;
			try
			{
				if (toolXml.Attributes["type"] != null)
				{
					editorToolType = (EditorToolType)Enum.Parse(typeof(EditorToolType), toolXml.Attributes["type"].Value, true);
				}
			}
			catch (Exception)
			{
			}
			EditorTool editorTool;
			switch (editorToolType)
			{
			case EditorToolType.DropDown:
			case EditorToolType.SplitButton:
				editorTool = ((EditorToolType.SplitButton == editorToolType) ? new EditorSplitButton() : new EditorDropDown());
				using (IEnumerator enumerator = toolXml.SelectNodes("item").GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						XmlNode xmlNode = (XmlNode)obj;
						string name = (xmlNode.Attributes["name"] != null) ? xmlNode.Attributes["name"].Value : string.Empty;
						string value = (xmlNode.Attributes["value"] != null) ? xmlNode.Attributes["value"].Value : string.Empty;
						((EditorDropDown)editorTool).Items.Add(name, value);
					}
					goto IL_185;
				}
				break;
			}
			editorTool = ((xmlNodeList != null && xmlNodeList.Count > 0 && this.IsContextMenuTool(toolXml)) ? new EditorContextMenuTool() : new EditorTool());
			editorTool.Type = editorToolType;
			IL_185:
			foreach (object obj2 in toolXml.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj2;
				string key;
				switch (key = xmlAttribute.Name.ToLowerInvariant())
				{
				case "name":
					editorTool.Name = xmlAttribute.Value;
					continue;
				case "enabled":
				case "isenabled":
					editorTool.Enabled = ToolsFileLoader.ParseBool(xmlAttribute.Value, true);
					continue;
				case "shortcut":
					editorTool.ShortCut = xmlAttribute.Value;
					continue;
				case "showicon":
					editorTool.ShowIcon = ToolsFileLoader.ParseBool(xmlAttribute.Value, true);
					continue;
				case "showtext":
					editorTool.ShowText = ToolsFileLoader.ParseBool(xmlAttribute.Value, true);
					continue;
				case "text":
					editorTool.Text = xmlAttribute.Value;
					continue;
				case "type":
					continue;
				case "separator":
					if (ToolsFileLoader.ParseBool(xmlAttribute.Value, false))
					{
						editorTool.Type = EditorToolType.Separator;
						continue;
					}
					continue;
				case "imageurl":
					editorTool.ImageUrl = xmlAttribute.Value;
					continue;
				case "imageurllarge":
					editorTool.ImageUrlLarge = xmlAttribute.Value;
					continue;
				}
				editorTool.Attributes[xmlAttribute.Name.ToLowerInvariant()] = xmlAttribute.Value;
			}
			return editorTool;
		}

		// Token: 0x0600ACAD RID: 44205 RVA: 0x00251D44 File Offset: 0x0024FF44
		private bool IsContextMenuTool(XmlNode toolXml)
		{
			XmlNode xmlNode = toolXml;
			while (xmlNode.Name == "tool")
			{
				xmlNode = xmlNode.ParentNode;
			}
			return xmlNode.Name == "contextMenu";
		}

		// Token: 0x0600ACAE RID: 44206 RVA: 0x00251D80 File Offset: 0x0024FF80
		private EditorToolStrip LoadToolStrip(XmlNode toolXml, XmlNodeList childTools)
		{
			EditorToolStrip editorToolStrip = new EditorToolStrip();
			foreach (object obj in toolXml.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string a;
				if ((a = xmlAttribute.Name.ToLowerInvariant()) != null && a == "name")
				{
					editorToolStrip.Name = xmlAttribute.Value;
				}
				else
				{
					editorToolStrip.Attributes[xmlAttribute.Name.ToLowerInvariant()] = xmlAttribute.Value;
				}
			}
			foreach (object obj2 in childTools)
			{
				XmlNode toolXml2 = (XmlNode)obj2;
				EditorTool editorTool = (EditorTool)this.LoadTool(toolXml2);
				if (editorTool != null)
				{
					editorToolStrip.Tools.Add(editorTool);
				}
			}
			return editorToolStrip;
		}

		// Token: 0x0600ACAF RID: 44207 RVA: 0x00251E88 File Offset: 0x00250088
		private static bool ParseBool(string value, bool defaultValue)
		{
			bool result;
			if (!bool.TryParse(value, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x0600ACB0 RID: 44208 RVA: 0x00251EA4 File Offset: 0x002500A4
		internal static char ParseSymbol(string value, char defaultValue)
		{
			char result = defaultValue;
			if (value.StartsWith("\\u"))
			{
				value = value.Replace("\\u", "");
				int value2;
				if (int.TryParse(value, NumberStyles.AllowHexSpecifier, null, out value2))
				{
					result = Convert.ToChar(value2);
				}
			}
			else
			{
				if (value.Length > 1)
				{
					value = value.Remove(1);
				}
				char.TryParse(value, out result);
			}
			return result;
		}

		// Token: 0x04002DC2 RID: 11714
		private readonly RadEditor _editor;
	}
}
