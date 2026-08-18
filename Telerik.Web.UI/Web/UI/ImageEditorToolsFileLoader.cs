using System;
using System.Xml;

namespace Telerik.Web.UI
{
	// Token: 0x02000EAF RID: 3759
	internal class ImageEditorToolsFileLoader
	{
		// Token: 0x06008F3D RID: 36669 RVA: 0x00203BB9 File Offset: 0x00201DB9
		public ImageEditorToolsFileLoader(RadImageEditor editor)
		{
			this._editor = editor;
		}

		// Token: 0x06008F3E RID: 36670 RVA: 0x00203BC8 File Offset: 0x00201DC8
		public void LoadTools(ImageEditorToolGroupCollection tools)
		{
			tools.Clear();
			foreach (object obj in this._editor.ToolsFileContent.SelectNodes("root/ImageEditorToolGroup|root/tools"))
			{
				XmlNode groupXml = (XmlNode)obj;
				tools.Add(this.LoadGroup(groupXml));
			}
		}

		// Token: 0x06008F3F RID: 36671 RVA: 0x00203C3C File Offset: 0x00201E3C
		private ImageEditorToolGroup LoadGroup(XmlNode groupXml)
		{
			ImageEditorToolGroup imageEditorToolGroup = new ImageEditorToolGroup();
			foreach (object obj in groupXml.SelectNodes("ImageEditorTool|tool"))
			{
				XmlNode toolXml = (XmlNode)obj;
				ImageEditorToolBase imageEditorToolBase = this.LoadTool(toolXml);
				if (imageEditorToolBase != null)
				{
					imageEditorToolGroup.Tools.Add(imageEditorToolBase);
				}
			}
			return imageEditorToolGroup;
		}

		// Token: 0x06008F40 RID: 36672 RVA: 0x00203CB4 File Offset: 0x00201EB4
		private ImageEditorToolBase LoadTool(XmlNode toolXml)
		{
			if (toolXml == null)
			{
				return null;
			}
			if (toolXml.Attributes["toolstrip"] != null && ImageEditorToolsFileLoader.ParseBool(toolXml.Attributes["toolstrip"].Value, false))
			{
				return this.LoadToolStrip(toolXml);
			}
			if (toolXml.Attributes["separator"] != null && ImageEditorToolsFileLoader.ParseBool(toolXml.Attributes["separator"].Value, false))
			{
				return new ImageEditorToolSeparator();
			}
			ImageEditorTool imageEditorTool = new ImageEditorTool();
			foreach (object obj in toolXml.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string key;
				switch (key = xmlAttribute.Name.ToLowerInvariant())
				{
				case "commandname":
				case "name":
					imageEditorTool.CommandName = xmlAttribute.Value;
					break;
				case "enabled":
				case "isenabled":
					imageEditorTool.Enabled = ImageEditorToolsFileLoader.ParseBool(xmlAttribute.Value, true);
					break;
				case "text":
					imageEditorTool.Text = xmlAttribute.Value;
					break;
				case "tooltip":
					imageEditorTool.ToolTip = xmlAttribute.Value;
					break;
				case "togglebutton":
					imageEditorTool.IsToggleButton = ImageEditorToolsFileLoader.ParseBool(xmlAttribute.Value, false);
					break;
				case "shortcut":
					imageEditorTool.ShortCut = xmlAttribute.Value;
					break;
				}
			}
			return imageEditorTool;
		}

		// Token: 0x06008F41 RID: 36673 RVA: 0x00203EBC File Offset: 0x002020BC
		private ImageEditorToolStrip LoadToolStrip(XmlNode toolXml)
		{
			ImageEditorToolStrip imageEditorToolStrip = new ImageEditorToolStrip();
			foreach (object obj in toolXml.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				string key;
				switch (key = xmlAttribute.Name.ToLowerInvariant())
				{
				case "commandname":
				case "name":
					imageEditorToolStrip.CommandName = xmlAttribute.Value;
					break;
				case "enabled":
				case "isenabled":
					imageEditorToolStrip.Enabled = ImageEditorToolsFileLoader.ParseBool(xmlAttribute.Value, true);
					break;
				case "text":
					imageEditorToolStrip.Text = xmlAttribute.Value;
					break;
				case "tooltip":
					imageEditorToolStrip.ToolTip = xmlAttribute.Value;
					break;
				case "enabledefaulttool":
					imageEditorToolStrip.EnableDefaultTool = ImageEditorToolsFileLoader.ParseBool(xmlAttribute.Value, false);
					break;
				case "shortcut":
					imageEditorToolStrip.ShortCut = xmlAttribute.Value;
					break;
				}
			}
			XmlNodeList xmlNodeList = toolXml.SelectNodes("EditorTool|tool");
			foreach (object obj2 in xmlNodeList)
			{
				XmlNode toolXml2 = (XmlNode)obj2;
				ImageEditorTool imageEditorTool = this.LoadTool(toolXml2) as ImageEditorTool;
				if (imageEditorTool != null)
				{
					imageEditorToolStrip.Tools.Add(imageEditorTool);
				}
			}
			return imageEditorToolStrip;
		}

		// Token: 0x06008F42 RID: 36674 RVA: 0x002040D0 File Offset: 0x002022D0
		private static bool ParseBool(string value, bool defaultValue)
		{
			bool result;
			if (!bool.TryParse(value, out result))
			{
				return defaultValue;
			}
			return result;
		}

		// Token: 0x040027C7 RID: 10183
		private RadImageEditor _editor;
	}
}
