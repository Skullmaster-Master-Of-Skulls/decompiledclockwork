using System;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x0200006E RID: 110
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class TemplateEditingFrame : ITemplateEditingFrame, IDisposable
	{
		// Token: 0x0600035A RID: 858 RVA: 0x00011204 File Offset: 0x0000F404
		public TemplateEditingFrame(TemplatedControlDesigner owner, string frameName, string[] templateNames, Style controlStyle, Style[] templateStyles)
		{
			this.owner = owner;
			this.frameName = frameName;
			this.controlStyle = controlStyle;
			this.templateStyles = templateStyles;
			this.verb = null;
			this.templateNames = (string[])templateNames.Clone();
			if (owner.BehaviorInternal != null)
			{
				NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)((IControlDesignerBehavior)owner.BehaviorInternal).DesignTimeElementView;
				this.htmlElemParent = ihtmlelement;
			}
			this.htmlElemControlName = null;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00011279 File Offset: 0x0000F479
		private string Content
		{
			get
			{
				if (this.frameContent == null)
				{
					this.frameContent = this.CreateFrameContent();
				}
				return this.frameContent;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00011295 File Offset: 0x0000F495
		public Style ControlStyle
		{
			get
			{
				return this.controlStyle;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0001129D File Offset: 0x0000F49D
		public string Name
		{
			get
			{
				return this.frameName;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600035E RID: 862 RVA: 0x000112A5 File Offset: 0x0000F4A5
		// (set) Token: 0x0600035F RID: 863 RVA: 0x000112AD File Offset: 0x0000F4AD
		public int InitialHeight
		{
			get
			{
				return this.initialHeight;
			}
			set
			{
				this.initialHeight = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000112B6 File Offset: 0x0000F4B6
		// (set) Token: 0x06000361 RID: 865 RVA: 0x000112BE File Offset: 0x0000F4BE
		public int InitialWidth
		{
			get
			{
				return this.initialWidth;
			}
			set
			{
				this.initialWidth = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000362 RID: 866 RVA: 0x000112C7 File Offset: 0x0000F4C7
		public string[] TemplateNames
		{
			get
			{
				return this.templateNames;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000112CF File Offset: 0x0000F4CF
		public Style[] TemplateStyles
		{
			get
			{
				return this.templateStyles;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000364 RID: 868 RVA: 0x000112D7 File Offset: 0x0000F4D7
		// (set) Token: 0x06000365 RID: 869 RVA: 0x000112DF File Offset: 0x0000F4DF
		public TemplateEditingVerb Verb
		{
			get
			{
				return this.verb;
			}
			set
			{
				this.verb = value;
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000112E8 File Offset: 0x0000F4E8
		public void Close(bool saveChanges)
		{
			if (saveChanges)
			{
				this.Save();
			}
			this.ShowInternal(false);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000112FC File Offset: 0x0000F4FC
		private string CreateFrameContent()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			string text = string.Empty;
			if (this.initialWidth > 0)
			{
				text = "width:" + this.initialWidth.ToString() + "px;";
			}
			if (this.initialHeight > 0)
			{
				text = text + "height:" + this.initialHeight.ToString() + "px;";
			}
			stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<table cellspacing=0 cellpadding=0 border=0 style=\"{4}\">\r\n              <tr>\r\n                <td>\r\n                  <table cellspacing=0 cellpadding=2 border=0 width=100% height=100%>\r\n                    <tr style=\"background-color:buttonshadow\">\r\n                      <td>\r\n                        <table cellspacing=0 cellpadding=0 border=0 width=100% height=100%>\r\n                          <tr>\r\n                            <td valign=middle style=\"font:messagebox;font-weight:bold;color:buttonhighlight\">&nbsp;<span id=\"idControlName\">{0}</span> - <span id=\"idFrameName\">{1}</span>&nbsp;&nbsp;&nbsp;</td>\r\n                            <td align=right valign=middle>&nbsp;<img src=\"{2}\" height=13 width=14 title=\"{3}\">&nbsp;</td>\r\n                          </tr>\r\n                        </table>\r\n                      </td>\r\n                    </tr>\r\n                  </table>\r\n                </td>\r\n              </tr>", new object[]
			{
				this.owner.Component.GetType().Name,
				this.Name,
				TemplateEditingFrame.TemplateInfoIcon,
				TemplateEditingFrame.TemplateInfoToolTip,
				text
			}));
			string text2 = string.Empty;
			if (this.controlStyle != null)
			{
				text2 = this.StyleToCss(this.controlStyle);
			}
			string text3 = string.Empty;
			for (int i = 0; i < this.templateNames.Length; i++)
			{
				stringBuilder.Append("<tr style=\"height:1px\"><td style=\"font-size:0pt\"></td></tr>");
				if (this.templateStyles != null)
				{
					text3 = this.StyleToCss(this.templateStyles[i]);
				}
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "<tr>\r\n                <td>\r\n                  <table cellspacing=0 cellpadding=2 border=0 width=100% height=100% style=\"border:solid 1px buttonface\">\r\n                    <tr style=\"font:messagebox;background-color:buttonface;color:buttonshadow\">\r\n                      <td style=\"border-bottom:solid 1px buttonshadow\">\r\n                        &nbsp;{0}&nbsp;&nbsp;&nbsp;\r\n                      </td>\r\n                    </tr>\r\n                    <tr style=\"{1}\" height=100%>\r\n                      <td style=\"{2}\">\r\n                        <div style=\"width:100%;height:100%\" id=\"{0}\"></div>\r\n                      </td>\r\n                    </tr>\r\n                  </table>\r\n                </td>\r\n              </tr>", new object[]
				{
					this.templateNames[i],
					text2,
					text3
				}));
			}
			stringBuilder.Append("</table>");
			return stringBuilder.ToString();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00011454 File Offset: 0x0000F654
		public void Dispose()
		{
			if (this.owner != null && this.owner.InTemplateMode)
			{
				this.owner.ExitTemplateMode(false, false, false);
			}
			this.ReleaseParentElement();
			if (this.verb != null)
			{
				this.verb.Dispose();
				this.verb = null;
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000114A4 File Offset: 0x0000F6A4
		private void Initialize()
		{
			if (this.htmlElemFrame != null)
			{
				return;
			}
			try
			{
				NativeMethods.IHTMLDocument2 document = this.htmlElemParent.GetDocument();
				this.htmlElemFrame = document.CreateElement("SPAN");
				this.htmlElemFrame.SetInnerHTML(this.Content);
				NativeMethods.IHTMLDOMNode ihtmldomnode = (NativeMethods.IHTMLDOMNode)this.htmlElemFrame;
				if (ihtmldomnode != null)
				{
					this.htmlElemContent = (NativeMethods.IHTMLElement)ihtmldomnode.GetFirstChild();
				}
				NativeMethods.IHTMLElement3 ihtmlelement = (NativeMethods.IHTMLElement3)this.htmlElemFrame;
				if (ihtmlelement != null)
				{
					ihtmlelement.SetContentEditable("false");
				}
				this.templateElements = new object[this.templateNames.Length];
				object index = 0;
				NativeMethods.IHTMLElementCollection ihtmlelementCollection = (NativeMethods.IHTMLElementCollection)this.htmlElemFrame.GetAll();
				object obj;
				for (int i = 0; i < this.templateNames.Length; i++)
				{
					try
					{
						obj = this.templateNames[i];
						NativeMethods.IHTMLElement ihtmlelement2 = ihtmlelementCollection.Item(obj, index);
						ihtmlelement2.SetAttribute("templatename", obj, 0);
						string innerHTML = "<DIV contentEditable=\"true\" style=\"padding:1;height:100%;width:100%\"></DIV>";
						ihtmlelement2.SetInnerHTML(innerHTML);
						NativeMethods.IHTMLDOMNode ihtmldomnode2 = (NativeMethods.IHTMLDOMNode)ihtmlelement2;
						if (ihtmldomnode2 != null)
						{
							this.templateElements[i] = ihtmldomnode2.GetFirstChild();
						}
					}
					catch (Exception ex)
					{
						this.templateElements[i] = null;
					}
				}
				obj = "idControlName";
				this.htmlElemControlName = ihtmlelementCollection.Item(obj, index);
				obj = "idFrameName";
				object obj2 = ihtmlelementCollection.Item(obj, index);
				if (obj2 != null)
				{
					NativeMethods.IHTMLElement ihtmlelement3 = (NativeMethods.IHTMLElement)obj2;
					ihtmlelement3.SetInnerText(this.frameName);
				}
				NativeMethods.IHTMLDOMNode ihtmldomnode3 = (NativeMethods.IHTMLDOMNode)this.htmlElemParent;
				if (ihtmldomnode3 != null)
				{
					ihtmldomnode3.AppendChild(ihtmldomnode);
				}
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00011664 File Offset: 0x0000F864
		public void Open()
		{
			NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)((IControlDesignerBehavior)this.owner.BehaviorInternal).DesignTimeElementView;
			if (this.htmlElemParent != ihtmlelement)
			{
				this.ReleaseParentElement();
				this.htmlElemParent = ihtmlelement;
			}
			this.Initialize();
			try
			{
				for (int i = 0; i < this.templateNames.Length; i++)
				{
					if (this.templateElements[i] != null)
					{
						bool flag = true;
						NativeMethods.IHTMLElement ihtmlelement2 = (NativeMethods.IHTMLElement)this.templateElements[i];
						string text = this.owner.GetTemplateContent(this, this.templateNames[i], out flag);
						ihtmlelement2.SetAttribute("contentEditable", flag, 0);
						if (text != null)
						{
							text = "<body contentEditable=true>" + text + "</body>";
							ihtmlelement2.SetInnerHTML(text);
						}
					}
				}
				if (this.htmlElemControlName != null)
				{
					this.htmlElemControlName.SetInnerText(this.owner.Component.Site.Name);
				}
			}
			catch (Exception ex)
			{
			}
			this.ShowInternal(true);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00011764 File Offset: 0x0000F964
		private void ReleaseParentElement()
		{
			this.htmlElemParent = null;
			this.htmlElemFrame = null;
			this.htmlElemContent = null;
			this.htmlElemControlName = null;
			this.templateElements = null;
			this.fVisible = false;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00011790 File Offset: 0x0000F990
		public void Resize(int width, int height)
		{
			if (this.htmlElemContent != null)
			{
				NativeMethods.IHTMLStyle style = this.htmlElemContent.GetStyle();
				if (style != null)
				{
					style.SetPixelWidth(width);
					style.SetPixelHeight(height);
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000117C4 File Offset: 0x0000F9C4
		public void Save()
		{
			try
			{
				if (this.templateElements != null)
				{
					object[] array = new object[1];
					for (int i = 0; i < this.templateNames.Length; i++)
					{
						if (this.templateElements[i] != null)
						{
							NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)this.templateElements[i];
							ihtmlelement.GetAttribute("contentEditable", 0, array);
							if (array[0] != null && array[0] is string && string.Compare((string)array[0], "true", StringComparison.OrdinalIgnoreCase) == 0)
							{
								string innerHTML = ihtmlelement.GetInnerHTML();
								this.owner.SetTemplateContent(this, this.templateNames[i], innerHTML);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00011870 File Offset: 0x0000FA70
		public void Show()
		{
			this.ShowInternal(true);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001187C File Offset: 0x0000FA7C
		private void ShowInternal(bool fShow)
		{
			if (this.htmlElemFrame == null || this.fVisible == fShow)
			{
				return;
			}
			try
			{
				NativeMethods.IHTMLDOMNode ihtmldomnode = (NativeMethods.IHTMLDOMNode)this.htmlElemFrame;
				NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)ihtmldomnode;
				NativeMethods.IHTMLStyle style = ihtmlelement.GetStyle();
				if (fShow)
				{
					style.SetDisplay(string.Empty);
				}
				else
				{
					if (this.templateElements != null)
					{
						for (int i = 0; i < this.templateElements.Length; i++)
						{
							if (this.templateElements[i] != null)
							{
								NativeMethods.IHTMLElement ihtmlelement2 = (NativeMethods.IHTMLElement)this.templateElements[i];
								ihtmlelement2.SetInnerHTML(string.Empty);
							}
						}
					}
					style.SetDisplay("none");
				}
			}
			catch (Exception ex)
			{
			}
			this.fVisible = fShow;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00011930 File Offset: 0x0000FB30
		private string StyleToCss(Style style)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Color c = style.ForeColor;
			if (!c.IsEmpty)
			{
				stringBuilder.Append("color:");
				stringBuilder.Append(ColorTranslator.ToHtml(c));
				stringBuilder.Append(";");
			}
			c = style.BackColor;
			if (!c.IsEmpty)
			{
				stringBuilder.Append("background-color:");
				stringBuilder.Append(ColorTranslator.ToHtml(c));
				stringBuilder.Append(";");
			}
			FontInfo font = style.Font;
			string text = font.Name;
			if (text.Length != 0)
			{
				stringBuilder.Append("font-family:'");
				stringBuilder.Append(text);
				stringBuilder.Append("';");
			}
			if (font.Bold)
			{
				stringBuilder.Append("font-weight:bold;");
			}
			if (font.Italic)
			{
				stringBuilder.Append("font-style:italic;");
			}
			text = string.Empty;
			if (font.Underline)
			{
				text += "underline";
			}
			if (font.Strikeout)
			{
				text += " line-through";
			}
			if (font.Overline)
			{
				text += " overline";
			}
			if (text.Length != 0)
			{
				stringBuilder.Append("text-decoration:");
				stringBuilder.Append(text);
				stringBuilder.Append(';');
			}
			FontUnit size = font.Size;
			if (!size.IsEmpty)
			{
				stringBuilder.Append("font-size:");
				stringBuilder.Append(size.ToString(CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011AA8 File Offset: 0x0000FCA8
		public void UpdateControlName(string newName)
		{
			if (this.htmlElemControlName != null)
			{
				this.htmlElemControlName.SetInnerText(newName);
			}
		}

		// Token: 0x04000176 RID: 374
		private const string TemplateFrameHeaderContent = "<table cellspacing=0 cellpadding=0 border=0 style=\"{4}\">\r\n              <tr>\r\n                <td>\r\n                  <table cellspacing=0 cellpadding=2 border=0 width=100% height=100%>\r\n                    <tr style=\"background-color:buttonshadow\">\r\n                      <td>\r\n                        <table cellspacing=0 cellpadding=0 border=0 width=100% height=100%>\r\n                          <tr>\r\n                            <td valign=middle style=\"font:messagebox;font-weight:bold;color:buttonhighlight\">&nbsp;<span id=\"idControlName\">{0}</span> - <span id=\"idFrameName\">{1}</span>&nbsp;&nbsp;&nbsp;</td>\r\n                            <td align=right valign=middle>&nbsp;<img src=\"{2}\" height=13 width=14 title=\"{3}\">&nbsp;</td>\r\n                          </tr>\r\n                        </table>\r\n                      </td>\r\n                    </tr>\r\n                  </table>\r\n                </td>\r\n              </tr>";

		// Token: 0x04000177 RID: 375
		private const string TemplateFrameFooterContent = "</table>";

		// Token: 0x04000178 RID: 376
		private const string TemplateFrameSeparatorContent = "<tr style=\"height:1px\"><td style=\"font-size:0pt\"></td></tr>";

		// Token: 0x04000179 RID: 377
		private const string TemplateFrameTemplateContent = "<tr>\r\n                <td>\r\n                  <table cellspacing=0 cellpadding=2 border=0 width=100% height=100% style=\"border:solid 1px buttonface\">\r\n                    <tr style=\"font:messagebox;background-color:buttonface;color:buttonshadow\">\r\n                      <td style=\"border-bottom:solid 1px buttonshadow\">\r\n                        &nbsp;{0}&nbsp;&nbsp;&nbsp;\r\n                      </td>\r\n                    </tr>\r\n                    <tr style=\"{1}\" height=100%>\r\n                      <td style=\"{2}\">\r\n                        <div style=\"width:100%;height:100%\" id=\"{0}\"></div>\r\n                      </td>\r\n                    </tr>\r\n                  </table>\r\n                </td>\r\n              </tr>";

		// Token: 0x0400017A RID: 378
		private static readonly string TemplateInfoToolTip = SR.GetString("TemplateEdit_Tip");

		// Token: 0x0400017B RID: 379
		private static readonly string TemplateInfoIcon = "res://" + typeof(TemplateEditingFrame).Module.FullyQualifiedName + "//TEMPLATE_TIP";

		// Token: 0x0400017C RID: 380
		private string frameName;

		// Token: 0x0400017D RID: 381
		private string frameContent;

		// Token: 0x0400017E RID: 382
		private string[] templateNames;

		// Token: 0x0400017F RID: 383
		private Style controlStyle;

		// Token: 0x04000180 RID: 384
		private Style[] templateStyles;

		// Token: 0x04000181 RID: 385
		private TemplateEditingVerb verb;

		// Token: 0x04000182 RID: 386
		private int initialWidth;

		// Token: 0x04000183 RID: 387
		private int initialHeight;

		// Token: 0x04000184 RID: 388
		private NativeMethods.IHTMLElement htmlElemFrame;

		// Token: 0x04000185 RID: 389
		private NativeMethods.IHTMLElement htmlElemContent;

		// Token: 0x04000186 RID: 390
		private NativeMethods.IHTMLElement htmlElemParent;

		// Token: 0x04000187 RID: 391
		private NativeMethods.IHTMLElement htmlElemControlName;

		// Token: 0x04000188 RID: 392
		private object[] templateElements;

		// Token: 0x04000189 RID: 393
		private bool fVisible;

		// Token: 0x0400018A RID: 394
		private TemplatedControlDesigner owner;
	}
}
