using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Xml;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x02001839 RID: 6201
	[ToolboxItem(false)]
	[RequiredScript(typeof(LayoutBuilderEngine))]
	[ClientScriptResource("Telerik.Web.UI.LayoutBuilder", "Telerik.Web.UI.Common.LayoutBuilder.LayoutBuilder.js")]
	public class LayoutBuilder : RadWebControl
	{
		// Token: 0x0600F0EA RID: 61674 RVA: 0x0036BE14 File Offset: 0x0036A014
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "layoutHeight", this.LayoutHeight, null);
			base.DescribeProperty<string>(descriptor, "layoutWidth", this.LayoutWidth, null);
			base.DescribeProperty<string>(descriptor, "layoutXmlFile", this.LayoutXmlFile, "");
			base.DescribeProperty<bool>(descriptor, "requireCellId", this.RequireCellId, false);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600F0EB RID: 61675 RVA: 0x0036BE78 File Offset: 0x0036A078
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0600F0EC RID: 61676 RVA: 0x0036BE81 File Offset: 0x0036A081
		public LayoutBuilder()
		{
			this.EnableEmbeddedSkins = false;
			this.EnableEmbeddedBaseStylesheet = false;
		}

		// Token: 0x170048CE RID: 18638
		// (get) Token: 0x0600F0ED RID: 61677 RVA: 0x0036BEAD File Offset: 0x0036A0AD
		public List<LayoutBuilderRow> RowCollection
		{
			get
			{
				if (this._rowCollection == null)
				{
					this._rowCollection = new List<LayoutBuilderRow>();
				}
				return this._rowCollection;
			}
		}

		// Token: 0x170048CF RID: 18639
		// (get) Token: 0x0600F0EE RID: 61678 RVA: 0x0036BEC8 File Offset: 0x0036A0C8
		// (set) Token: 0x0600F0EF RID: 61679 RVA: 0x0036BEDC File Offset: 0x0036A0DC
		[Category("Misc")]
		[Description("Gets or sets the xml file.")]
		[ClientControlProperty]
		[DefaultValue("")]
		public string LayoutXmlFile
		{
			get
			{
				return base.GetViewStateValue<string>("LayoutXmlFile", "");
			}
			set
			{
				this.ViewState["LayoutXmlFile"] = value;
				this.ResetLayoutXmlFileContent();
				XmlDocument xmlDocument = new XmlDocument();
				string xmlFilePath = this.GetXmlFilePath(value);
				try
				{
					xmlDocument.Load(xmlFilePath);
					this.LoadLayoutXmlFile(xmlDocument);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x170048D0 RID: 18640
		// (get) Token: 0x0600F0F0 RID: 61680 RVA: 0x0036BF34 File Offset: 0x0036A134
		// (set) Token: 0x0600F0F1 RID: 61681 RVA: 0x0036BF46 File Offset: 0x0036A146
		[Description("Gets or sets the width of the Layout")]
		[ClientControlProperty]
		[Category("Misc")]
		public string LayoutWidth
		{
			get
			{
				return base.GetViewStateValue<string>("LayoutWidth", "");
			}
			set
			{
				this.ViewState["LayoutWidth"] = value;
			}
		}

		// Token: 0x170048D1 RID: 18641
		// (get) Token: 0x0600F0F2 RID: 61682 RVA: 0x0036BF59 File Offset: 0x0036A159
		// (set) Token: 0x0600F0F3 RID: 61683 RVA: 0x0036BF6B File Offset: 0x0036A16B
		[Category("Misc")]
		[Description("Gets or sets the width of the Layout")]
		[ClientControlProperty]
		public string LayoutHeight
		{
			get
			{
				return base.GetViewStateValue<string>("LayoutHeight", "");
			}
			set
			{
				this.ViewState["LayoutHeight"] = value;
			}
		}

		// Token: 0x170048D2 RID: 18642
		// (get) Token: 0x0600F0F4 RID: 61684 RVA: 0x0036BF7E File Offset: 0x0036A17E
		internal XmlDocument LayoutXmlFileContent
		{
			get
			{
				if (this._layoutXmlFileContent == null)
				{
					this._layoutXmlFileContent = new XmlDocument();
					this._layoutXmlFileContent.Load(this.GetXmlFilePath(this.LayoutXmlFile));
				}
				return this._layoutXmlFileContent;
			}
		}

		// Token: 0x0600F0F5 RID: 61685 RVA: 0x0036BFB0 File Offset: 0x0036A1B0
		private void ResetLayoutXmlFileContent()
		{
			this._layoutXmlFileContent = null;
			this._layoutXmlFileLoaded = false;
		}

		// Token: 0x0600F0F6 RID: 61686 RVA: 0x0036BFC0 File Offset: 0x0036A1C0
		public virtual void EnsureLayoutXmlFileLoaded()
		{
			if (!this._layoutXmlFileLoaded)
			{
				this.LoadLayoutXmlFile();
			}
		}

		// Token: 0x0600F0F7 RID: 61687 RVA: 0x0036BFD0 File Offset: 0x0036A1D0
		public virtual void LoadLayoutXmlFile(XmlDocument doc)
		{
			this._layoutXmlFileContent = doc;
			this.EnsureLayoutXmlFileLoaded();
		}

		// Token: 0x0600F0F8 RID: 61688 RVA: 0x0036BFE0 File Offset: 0x0036A1E0
		private void LoadLayoutXmlFile()
		{
			foreach (object obj in this.LayoutXmlFileContent.SelectNodes("root"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes["width"] != null)
				{
					this.LayoutWidth = xmlNode.Attributes["width"].Value;
				}
				if (xmlNode.Attributes["height"] != null)
				{
					this.LayoutHeight = xmlNode.Attributes["height"].Value;
				}
			}
			this.RowCollection.Clear();
			foreach (object obj2 in this.LayoutXmlFileContent.SelectNodes("root/row"))
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				LayoutBuilderRow layoutBuilderRow = new LayoutBuilderRow();
				foreach (object obj3 in xmlNode2.SelectNodes("cell"))
				{
					XmlNode xmlNode3 = (XmlNode)obj3;
					LayoutBuilderCell layoutBuilderCell = new LayoutBuilderCell();
					if (xmlNode3.Attributes["width"] != null)
					{
						layoutBuilderCell.Width = xmlNode3.Attributes["width"].Value;
					}
					if (xmlNode3.Attributes["height"] != null)
					{
						layoutBuilderCell.Height = xmlNode3.Attributes["height"].Value;
					}
					if (xmlNode3.Attributes["colspan"] != null)
					{
						layoutBuilderCell.ColSpan = xmlNode3.Attributes["colspan"].Value;
					}
					if (xmlNode3.Attributes["rowspan"] != null)
					{
						layoutBuilderCell.RowSpan = xmlNode3.Attributes["rowspan"].Value;
					}
					if (xmlNode3.Attributes["id"] != null)
					{
						layoutBuilderCell.ID = xmlNode3.Attributes["id"].Value;
					}
					layoutBuilderCell.Content = xmlNode3.InnerText;
					foreach (object obj4 in xmlNode3.Attributes)
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj4;
						layoutBuilderCell.Attributes[xmlAttribute.Name] = xmlAttribute.Value;
					}
					layoutBuilderRow.LayoutBuilderCells.Add(layoutBuilderCell);
				}
				this.RowCollection.Add(layoutBuilderRow);
			}
			this.SaveClientState();
			this._layoutXmlFileLoaded = true;
		}

		// Token: 0x170048D3 RID: 18643
		// (get) Token: 0x0600F0F9 RID: 61689 RVA: 0x0036C310 File Offset: 0x0036A510
		// (set) Token: 0x0600F0FA RID: 61690 RVA: 0x0036C31E File Offset: 0x0036A51E
		[Category("Misc")]
		[DefaultValue(false)]
		[Description("Gets or sets the value indicating whether every cell should has id.")]
		[ClientControlProperty]
		public bool RequireCellId
		{
			get
			{
				return base.GetViewStateValue<bool>("RequireCellId", false);
			}
			set
			{
				this.ViewState["RequireCellId"] = value;
			}
		}

		// Token: 0x170048D4 RID: 18644
		// (get) Token: 0x0600F0FB RID: 61691 RVA: 0x0036C336 File Offset: 0x0036A536
		// (set) Token: 0x0600F0FC RID: 61692 RVA: 0x0036C34D File Offset: 0x0036A54D
		public XmlDocument LayoutXmlDoc
		{
			get
			{
				if (this._layoutXmlDoc == null)
				{
					return this.GetLayoutXml();
				}
				return this._layoutXmlDoc;
			}
			set
			{
				this._layoutXmlDoc = value;
			}
		}

		// Token: 0x170048D5 RID: 18645
		// (get) Token: 0x0600F0FD RID: 61693 RVA: 0x0036C356 File Offset: 0x0036A556
		// (set) Token: 0x0600F0FE RID: 61694 RVA: 0x0036C35E File Offset: 0x0036A55E
		public string TableHtml
		{
			get
			{
				return this._tableHtml;
			}
			set
			{
				this._tableHtml = value;
			}
		}

		// Token: 0x170048D6 RID: 18646
		// (get) Token: 0x0600F0FF RID: 61695 RVA: 0x0036C367 File Offset: 0x0036A567
		public XmlDocument TableHtmlXml
		{
			get
			{
				this._tableHtmlXml = new XmlDocument();
				if (!string.IsNullOrEmpty(this.TableHtml))
				{
					this._tableHtmlXml.LoadXml(this.TableHtml);
				}
				return this._tableHtmlXml;
			}
		}

		// Token: 0x0600F100 RID: 61696 RVA: 0x0036C398 File Offset: 0x0036A598
		protected void LoadTableHtmlXml()
		{
			this.RowCollection.Clear();
			foreach (object obj in this.TableHtmlXml.SelectNodes("table"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				this.LayoutWidth = "";
				this.LayoutHeight = "";
				if (xmlNode.Attributes["style"] != null)
				{
					string input = xmlNode.Attributes["style"].Value + ";";
					string pattern = "width\\s*:\\s*([^;]*);";
					Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
					Match match = regex.Match(input);
					if (match.Groups.Count == 2)
					{
						this.LayoutWidth = match.Groups[1].Captures[0].Value;
					}
					pattern = "height\\s*:\\s*([^;]*);";
					regex = new Regex(pattern, RegexOptions.IgnoreCase);
					match = regex.Match(input);
					if (match.Groups.Count == 2)
					{
						this.LayoutHeight = match.Groups[1].Captures[0].Value;
					}
				}
				if (!string.IsNullOrEmpty(this.LayoutWidth) && xmlNode.Attributes["width"] != null)
				{
					this.LayoutWidth = xmlNode.Attributes["width"].Value;
				}
				if (!string.IsNullOrEmpty(this.LayoutHeight) && xmlNode.Attributes["height"] != null)
				{
					this.LayoutHeight = xmlNode.Attributes["height"].Value;
				}
				foreach (object obj2 in xmlNode.SelectNodes("thead/tr|tbody/tr|tr"))
				{
					XmlNode xmlNode2 = (XmlNode)obj2;
					LayoutBuilderRow layoutBuilderRow = new LayoutBuilderRow();
					foreach (object obj3 in xmlNode2.SelectNodes("th|td"))
					{
						XmlNode xmlNode3 = (XmlNode)obj3;
						LayoutBuilderCell layoutBuilderCell = new LayoutBuilderCell();
						if (xmlNode3.Attributes["id"] != null)
						{
							layoutBuilderCell.ID = xmlNode3.Attributes["id"].Value;
						}
						if (xmlNode3.Attributes["rowspan"] != null)
						{
							layoutBuilderCell.RowSpan = xmlNode3.Attributes["rowspan"].Value;
						}
						if (xmlNode3.Attributes["colspan"] != null)
						{
							layoutBuilderCell.ColSpan = xmlNode3.Attributes["colspan"].Value;
						}
						if (xmlNode3.Attributes["style"] != null)
						{
							string input2 = xmlNode3.Attributes["style"].Value + ";";
							string pattern2 = "width\\s*:\\s*([^;]*);";
							Regex regex2 = new Regex(pattern2, RegexOptions.IgnoreCase);
							Match match2 = regex2.Match(input2);
							if (match2.Groups.Count == 2)
							{
								layoutBuilderCell.Width = match2.Groups[1].Captures[0].Value;
							}
							pattern2 = "height\\s*:\\s*([^;]*);";
							regex2 = new Regex(pattern2, RegexOptions.IgnoreCase);
							match2 = regex2.Match(input2);
							if (match2.Groups.Count == 2)
							{
								layoutBuilderCell.Height = match2.Groups[1].Captures[0].Value;
							}
						}
						if (!string.IsNullOrEmpty(layoutBuilderCell.Width) && xmlNode3.Attributes["width"] != null)
						{
							layoutBuilderCell.Width = xmlNode3.Attributes["width"].Value;
						}
						if (!string.IsNullOrEmpty(layoutBuilderCell.Height) && xmlNode3.Attributes["height"] != null)
						{
							layoutBuilderCell.Height = xmlNode3.Attributes["height"].Value;
						}
						layoutBuilderCell.Content = xmlNode3.InnerText;
						foreach (object obj4 in xmlNode3.Attributes)
						{
							XmlAttribute xmlAttribute = (XmlAttribute)obj4;
							layoutBuilderCell.Attributes[xmlAttribute.Name] = xmlAttribute.Value;
						}
						layoutBuilderRow.LayoutBuilderCells.Add(layoutBuilderCell);
					}
					this.RowCollection.Add(layoutBuilderRow);
				}
			}
		}

		// Token: 0x0600F101 RID: 61697 RVA: 0x0036C8B4 File Offset: 0x0036AAB4
		public XmlDocument GetLayoutXml()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlDeclaration newChild = xmlDocument.CreateXmlDeclaration("1.0", "utf-8", null);
			xmlDocument.InsertBefore(newChild, xmlDocument.DocumentElement);
			XmlElement xmlElement = xmlDocument.CreateElement("root");
			if (!string.IsNullOrEmpty(this.LayoutWidth))
			{
				xmlElement.SetAttribute("width", this.LayoutWidth);
			}
			if (!string.IsNullOrEmpty(this.LayoutHeight))
			{
				xmlElement.SetAttribute("height", this.LayoutHeight);
			}
			xmlDocument.AppendChild(xmlElement);
			foreach (LayoutBuilderRow layoutBuilderRow in this.RowCollection)
			{
				XmlElement xmlElement2 = xmlDocument.CreateElement("row");
				foreach (object obj in layoutBuilderRow.LayoutBuilderCells)
				{
					LayoutBuilderCell layoutBuilderCell = (LayoutBuilderCell)obj;
					XmlElement xmlElement3 = xmlDocument.CreateElement("cell");
					if (!string.IsNullOrEmpty(layoutBuilderCell.ID))
					{
						xmlElement3.SetAttribute("id", layoutBuilderCell.ID);
					}
					if (!string.IsNullOrEmpty(layoutBuilderCell.Width))
					{
						xmlElement3.SetAttribute("width", layoutBuilderCell.Width);
					}
					if (!string.IsNullOrEmpty(layoutBuilderCell.Height))
					{
						xmlElement3.SetAttribute("height", layoutBuilderCell.Height);
					}
					if (!string.IsNullOrEmpty(layoutBuilderCell.RowSpan))
					{
						xmlElement3.SetAttribute("rowspan", layoutBuilderCell.RowSpan);
					}
					if (!string.IsNullOrEmpty(layoutBuilderCell.ColSpan))
					{
						xmlElement3.SetAttribute("colspan", layoutBuilderCell.ColSpan);
					}
					if (!string.IsNullOrEmpty(layoutBuilderCell.Content))
					{
						XmlCDataSection newChild2 = xmlDocument.CreateCDataSection(layoutBuilderCell.Content);
						xmlElement3.AppendChild(newChild2);
					}
					xmlElement2.AppendChild(xmlElement3);
				}
				xmlElement.AppendChild(xmlElement2);
			}
			return xmlDocument;
		}

		// Token: 0x0600F102 RID: 61698 RVA: 0x0036CAE0 File Offset: 0x0036ACE0
		public string GetLayoutTableHTML()
		{
			StringWriter stringWriter = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			if (!string.IsNullOrEmpty(this.LayoutWidth))
			{
				htmlTextWriter.AddStyleAttribute("width", this.LayoutWidth);
			}
			if (!string.IsNullOrEmpty(this.LayoutHeight))
			{
				htmlTextWriter.AddStyleAttribute("height", this.LayoutHeight);
			}
			htmlTextWriter.RenderBeginTag("table");
			foreach (LayoutBuilderRow layoutBuilderRow in this.RowCollection)
			{
				htmlTextWriter.RenderBeginTag("tr");
				foreach (object obj in layoutBuilderRow.LayoutBuilderCells)
				{
					LayoutBuilderCell layoutBuilderCell = (LayoutBuilderCell)obj;
					foreach (object obj2 in layoutBuilderCell.Attributes.Keys)
					{
						string text = (string)obj2;
						string pattern = "^(width|height)$";
						Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
						Match match = regex.Match(text);
						if (match.Length == 0)
						{
							htmlTextWriter.AddAttribute(text, layoutBuilderCell.Attributes[text]);
						}
					}
					if (!string.IsNullOrEmpty(layoutBuilderCell.ID))
					{
						htmlTextWriter.AddAttribute("id", layoutBuilderCell.ID);
					}
					if (!string.IsNullOrEmpty(layoutBuilderCell.Width))
					{
						htmlTextWriter.AddStyleAttribute("width", layoutBuilderCell.Width);
					}
					if (!string.IsNullOrEmpty(layoutBuilderCell.Height))
					{
						htmlTextWriter.AddStyleAttribute("height", layoutBuilderCell.Height);
					}
					if (!string.IsNullOrEmpty(layoutBuilderCell.RowSpan))
					{
						htmlTextWriter.AddAttribute("rowspan", layoutBuilderCell.RowSpan);
					}
					if (!string.IsNullOrEmpty(layoutBuilderCell.ColSpan))
					{
						htmlTextWriter.AddAttribute("colspan", layoutBuilderCell.ColSpan);
					}
					htmlTextWriter.RenderBeginTag("td");
					htmlTextWriter.Write(layoutBuilderCell.Content);
					htmlTextWriter.RenderEndTag();
				}
				htmlTextWriter.RenderEndTag();
			}
			htmlTextWriter.RenderEndTag();
			return stringWriter.ToString();
		}

		// Token: 0x0600F103 RID: 61699 RVA: 0x0036CD54 File Offset: 0x0036AF54
		public void WriteLayoutXmlToFile(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				return;
			}
			this.LayoutXmlDoc.Save(filePath);
		}

		// Token: 0x0600F104 RID: 61700 RVA: 0x0036CD6C File Offset: 0x0036AF6C
		public void LoadXmlDocument(XmlDocument xmlDoc)
		{
			foreach (object obj in xmlDoc.SelectNodes("root"))
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Attributes["width"] != null)
				{
					this.LayoutWidth = xmlNode.Attributes["width"].Value;
				}
				if (xmlNode.Attributes["height"] != null)
				{
					this.LayoutHeight = xmlNode.Attributes["height"].Value;
				}
			}
			this._rowCollection.Clear();
			foreach (object obj2 in xmlDoc.SelectNodes("root/row"))
			{
				XmlNode xmlNode2 = (XmlNode)obj2;
				LayoutBuilderRow layoutBuilderRow = new LayoutBuilderRow();
				foreach (object obj3 in xmlNode2.SelectNodes("cell"))
				{
					XmlNode xmlNode3 = (XmlNode)obj3;
					LayoutBuilderCell layoutBuilderCell = new LayoutBuilderCell();
					if (xmlNode3.Attributes["width"] != null)
					{
						layoutBuilderCell.Width = xmlNode3.Attributes["width"].Value;
					}
					if (xmlNode3.Attributes["height"] != null)
					{
						layoutBuilderCell.Height = xmlNode3.Attributes["height"].Value;
					}
					if (xmlNode3.Attributes["colspan"] != null)
					{
						layoutBuilderCell.ColSpan = xmlNode3.Attributes["colspan"].Value;
					}
					if (xmlNode3.Attributes["rowspan"] != null)
					{
						layoutBuilderCell.RowSpan = xmlNode3.Attributes["rowspan"].Value;
					}
					if (xmlNode3.Attributes["id"] != null)
					{
						layoutBuilderCell.ID = xmlNode3.Attributes["id"].Value;
					}
					layoutBuilderCell.Content = xmlNode3.InnerText;
					layoutBuilderRow.LayoutBuilderCells.Add(layoutBuilderCell);
				}
				this._rowCollection.Add(layoutBuilderRow);
			}
		}

		// Token: 0x0600F105 RID: 61701 RVA: 0x0036D014 File Offset: 0x0036B214
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.TableHtml = ContentEncoder.Decode((string)clientState["TableHtml"]);
			this.LoadTableHtmlXml();
		}

		// Token: 0x0600F106 RID: 61702 RVA: 0x0036D040 File Offset: 0x0036B240
		protected override string SaveClientState()
		{
			string text = this.GetLayoutTableHTML();
			text = text.Replace("\n", "<telerikcr />");
			text = text.Replace("\r", "<teleriklf />");
			return "{ TableHtml : \"" + ContentEncoder.Encode(text) + "\" }";
		}

		// Token: 0x0600F107 RID: 61703 RVA: 0x0036D08C File Offset: 0x0036B28C
		private string GetXmlFilePath(string path)
		{
			if (path.StartsWith("http://") || path.StartsWith("https://"))
			{
				return path;
			}
			try
			{
				string text = this.Context.Request.MapPath(path);
				if (File.Exists(text))
				{
					return text;
				}
			}
			catch (Exception)
			{
			}
			return path;
		}

		// Token: 0x0400455C RID: 17756
		private List<LayoutBuilderRow> _rowCollection = new List<LayoutBuilderRow>();

		// Token: 0x0400455D RID: 17757
		private bool _layoutXmlFileLoaded;

		// Token: 0x0400455E RID: 17758
		private XmlDocument _layoutXmlFileContent;

		// Token: 0x0400455F RID: 17759
		private XmlDocument _layoutXmlDoc;

		// Token: 0x04004560 RID: 17760
		private string _tableHtml = "";

		// Token: 0x04004561 RID: 17761
		private XmlDocument _tableHtmlXml;
	}
}
