using System;
using System.Collections;
using System.Collections.Specialized;
using System.Design;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x02000015 RID: 21
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class ContainerControlDesigner : ControlDesigner
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003ABC File Offset: 0x00001CBC
		public ContainerControlDesigner()
		{
			this._defaultFrameStyle = new Style();
			this._defaultFrameStyle.Font.Name = "Tahoma";
			this._defaultFrameStyle.ForeColor = SystemColors.ControlText;
			this._defaultFrameStyle.BackColor = SystemColors.Control;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected virtual bool NoWrap
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003B12 File Offset: 0x00001D12
		internal virtual string DesignTimeHtml
		{
			get
			{
				if (string.IsNullOrEmpty(this.FrameCaption))
				{
					return "<div height=\"{8}\" width=\"{9}\" style=\"{0}{2}{3}{4}{6}{10}\" {7}=0></div>";
				}
				if (this.NoWrap)
				{
					return "<table height=\"{8}\" width=\"{9}\" style=\"{0}{2}{10}\" cellpadding=1 cellspacing=0>\r\n                <tr>\r\n                    <td nowrap align=center valign=middle style=\"{1}{2}{3}{4}\">{5}</td>\r\n                </tr>\r\n                <tr>\r\n                    <td nowrap style=\"vertical-align:top;{6}{10}\" {7}=0></td>\r\n                </tr>\r\n            </table>";
				}
				return "<table height=\"{8}\" width=\"{9}\" style=\"{0}{2}{10}\" cellpadding=1 cellspacing=0>\r\n                <tr>\r\n                    <td nowrap align=center valign=middle style=\"{1}{2}{3}{4}\">{5}</td>\r\n                </tr>\r\n                <tr>\r\n                    <td style=\"vertical-align:top;{6}{10}\" {7}=0></td>\r\n                </tr>\r\n            </table>";
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003B3A File Offset: 0x00001D3A
		public virtual string FrameCaption
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00003B42 File Offset: 0x00001D42
		public virtual Style FrameStyle
		{
			get
			{
				return this._defaultFrameStyle;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003B42 File Offset: 0x00001D42
		internal Style FrameStyleInternal
		{
			get
			{
				return this._defaultFrameStyle;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003B4C File Offset: 0x00001D4C
		public virtual IDictionary GetDesignTimeCssAttributes()
		{
			IDictionary dictionary = new HybridDictionary();
			this.AddDesignTimeCssAttributes(dictionary);
			return dictionary;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003B68 File Offset: 0x00001D68
		protected virtual void AddDesignTimeCssAttributes(IDictionary styleAttributes)
		{
			if (!base.IsWebControl)
			{
				return;
			}
			WebControl webControl = base.ViewControl as WebControl;
			Unit width = webControl.Width;
			if (!width.IsEmpty && width.Value != 0.0)
			{
				styleAttributes["width"] = width.ToString(CultureInfo.InvariantCulture);
			}
			Unit height = webControl.Height;
			if (!height.IsEmpty && height.Value != 0.0)
			{
				styleAttributes["height"] = height.ToString(CultureInfo.InvariantCulture);
			}
			string text = ColorTranslator.ToHtml(webControl.BackColor);
			if (text.Length > 0)
			{
				styleAttributes["background-color"] = text;
			}
			text = ColorTranslator.ToHtml(webControl.ForeColor);
			if (text.Length > 0)
			{
				styleAttributes["color"] = text;
			}
			text = ColorTranslator.ToHtml(webControl.BorderColor);
			if (text.Length > 0)
			{
				styleAttributes["border-color"] = text;
			}
			Unit borderWidth = webControl.BorderWidth;
			if (!borderWidth.IsEmpty && borderWidth.Value != 0.0)
			{
				styleAttributes["border-width"] = borderWidth.ToString(CultureInfo.InvariantCulture);
			}
			BorderStyle borderStyle = webControl.BorderStyle;
			if (borderStyle != BorderStyle.NotSet)
			{
				styleAttributes["border-style"] = borderStyle;
			}
			else if (!borderWidth.IsEmpty && borderWidth.Value != 0.0)
			{
				styleAttributes["border-style"] = BorderStyle.Solid;
			}
			string name = webControl.Font.Name;
			if (name.Length != 0)
			{
				styleAttributes["font-family"] = HttpUtility.HtmlEncode(name);
			}
			FontUnit size = webControl.Font.Size;
			if (size != FontUnit.Empty)
			{
				styleAttributes["font-size"] = size.ToString(CultureInfo.InvariantCulture);
			}
			bool flag = webControl.Font.Bold;
			if (flag)
			{
				styleAttributes["font-weight"] = "bold";
			}
			flag = webControl.Font.Italic;
			if (flag)
			{
				styleAttributes["font-style"] = "italic";
			}
			string text2 = string.Empty;
			flag = webControl.Font.Underline;
			if (flag)
			{
				text2 += "underline ";
			}
			flag = webControl.Font.Strikeout;
			if (flag)
			{
				text2 += "line-through";
			}
			flag = webControl.Font.Overline;
			if (flag)
			{
				text2 += "overline";
			}
			if (text2.Length > 0)
			{
				styleAttributes["text-decoration"] = text2.Trim();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003E01 File Offset: 0x00002001
		public override string GetPersistenceContent()
		{
			if (base.LocalizedInnerContent != null)
			{
				return base.LocalizedInnerContent;
			}
			return null;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003E14 File Offset: 0x00002014
		public override string GetEditableDesignerRegionContent(EditableDesignerRegion region)
		{
			string text = string.Empty;
			if (base.Tag != null)
			{
				try
				{
					text = base.Tag.GetContent();
					if (text == null)
					{
						text = string.Empty;
					}
				}
				catch (Exception ex)
				{
				}
			}
			return text;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003E5C File Offset: 0x0000205C
		public override void SetEditableDesignerRegionContent(EditableDesignerRegion region, string content)
		{
			if (base.Tag != null)
			{
				try
				{
					base.Tag.SetContent(content);
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003E94 File Offset: 0x00002094
		private IDictionary ConvertFontInfoToCss(FontInfo font)
		{
			IDictionary dictionary = new HybridDictionary();
			string name = font.Name;
			if (name.Length != 0)
			{
				dictionary["font-family"] = HttpUtility.HtmlEncode(name);
			}
			FontUnit size = font.Size;
			if (size != FontUnit.Empty)
			{
				dictionary["font-size"] = size.ToString(CultureInfo.CurrentCulture);
			}
			bool flag = font.Bold;
			if (flag)
			{
				dictionary["font-weight"] = "bold";
			}
			flag = font.Italic;
			if (flag)
			{
				dictionary["font-style"] = "italic";
			}
			string text = string.Empty;
			flag = font.Underline;
			if (flag)
			{
				text += "underline ";
			}
			flag = font.Strikeout;
			if (flag)
			{
				text += "line-through";
			}
			flag = font.Overline;
			if (flag)
			{
				text += "overline";
			}
			if (text.Length > 0)
			{
				dictionary["text-decoration"] = text.Trim();
			}
			return dictionary;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003F94 File Offset: 0x00002194
		private string CssDictionaryToString(IDictionary dictionary)
		{
			string text = string.Empty;
			if (dictionary != null)
			{
				foreach (object obj in dictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					string[] array = new string[5];
					array[0] = text;
					int num = 1;
					object key = dictionaryEntry.Key;
					array[num] = ((key != null) ? key.ToString() : null);
					array[2] = ":";
					int num2 = 3;
					object value = dictionaryEntry.Value;
					array[num2] = ((value != null) ? value.ToString() : null);
					array[4] = "; ";
					text = string.Concat(array);
				}
			}
			return text;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004038 File Offset: 0x00002238
		private string GenerateDesignTimeHtml()
		{
			string text = this.FrameCaption;
			Unit height = this.FrameStyle.Height;
			Unit width = this.FrameStyle.Width;
			string text2 = string.Empty;
			if (base.IsWebControl)
			{
				WebControl webControl = (WebControl)base.ViewControl;
				if (height.IsEmpty)
				{
					height = webControl.Height;
				}
				if (width.IsEmpty)
				{
					width = webControl.Width;
				}
				text2 = webControl.CssClass;
			}
			if (text == null)
			{
				text = string.Empty;
			}
			else if (text.IndexOf('\0') > -1)
			{
				text = text.Replace("\0", string.Empty);
			}
			string text3 = string.Empty;
			WebControl webControl2 = base.ViewControl as WebControl;
			if (webControl2 != null)
			{
				text3 = webControl2.Style.Value + ";";
			}
			string text4 = string.Empty;
			string text5 = string.Empty;
			if ((!this.FrameStyle.BorderWidth.IsEmpty && this.FrameStyle.BorderWidth.Value != 0.0) || this.FrameStyle.BorderStyle != BorderStyle.NotSet || !this.FrameStyle.BorderColor.IsEmpty)
			{
				text4 = string.Format(CultureInfo.InvariantCulture, "border:{0} {1} {2}; ", new object[]
				{
					this.FrameStyle.BorderWidth,
					this.FrameStyle.BorderStyle,
					ColorTranslator.ToHtml(this.FrameStyle.BorderColor)
				});
				text5 = string.Format(CultureInfo.InvariantCulture, "border-bottom:{0} {1} {2}; ", new object[]
				{
					this.FrameStyle.BorderWidth,
					this.FrameStyle.BorderStyle,
					ColorTranslator.ToHtml(this.FrameStyle.BorderColor)
				});
			}
			string text6 = string.Empty;
			if (!this.FrameStyle.ForeColor.IsEmpty)
			{
				text6 = string.Format(CultureInfo.InvariantCulture, "color:{0}; ", new object[]
				{
					ColorTranslator.ToHtml(this.FrameStyle.ForeColor)
				});
			}
			string text7 = string.Empty;
			if (!this.FrameStyle.BackColor.IsEmpty)
			{
				text7 = string.Format(CultureInfo.InvariantCulture, "background-color:{0}; ", new object[]
				{
					ColorTranslator.ToHtml(this.FrameStyle.BackColor)
				});
			}
			return string.Format(CultureInfo.InvariantCulture, this.DesignTimeHtml, new object[]
			{
				text4,
				text5,
				text6,
				text7,
				this.CssDictionaryToString(this.ConvertFontInfoToCss(this.FrameStyle.Font)),
				text,
				this.CssDictionaryToString(this.GetDesignTimeCssAttributes()),
				DesignerRegion.DesignerRegionAttributeName,
				height,
				width,
				text3,
				text2
			});
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004314 File Offset: 0x00002514
		public override string GetDesignTimeHtml(DesignerRegionCollection regions)
		{
			Control control = (Control)base.Component;
			control.Controls.Clear();
			EditableDesignerRegion editableDesignerRegion = new EditableDesignerRegion(this, "Content");
			editableDesignerRegion.Description = SR.GetString("ContainerControlDesigner_RegionWatermark");
			editableDesignerRegion.Properties[typeof(Control)] = control;
			editableDesignerRegion.EnsureSize = true;
			regions.Add(editableDesignerRegion);
			return this.GenerateDesignTimeHtml();
		}

		// Token: 0x040000BE RID: 190
		private const string ContainerControlWithCaptionNoWrapDesignTimeHtml = "<table height=\"{8}\" width=\"{9}\" style=\"{0}{2}{10}\" cellpadding=1 cellspacing=0>\r\n                <tr>\r\n                    <td nowrap align=center valign=middle style=\"{1}{2}{3}{4}\">{5}</td>\r\n                </tr>\r\n                <tr>\r\n                    <td nowrap style=\"vertical-align:top;{6}{10}\" {7}=0></td>\r\n                </tr>\r\n            </table>";

		// Token: 0x040000BF RID: 191
		private const string ContainerControlWithCaptionDesignTimeHtml = "<table height=\"{8}\" width=\"{9}\" style=\"{0}{2}{10}\" cellpadding=1 cellspacing=0>\r\n                <tr>\r\n                    <td nowrap align=center valign=middle style=\"{1}{2}{3}{4}\">{5}</td>\r\n                </tr>\r\n                <tr>\r\n                    <td style=\"vertical-align:top;{6}{10}\" {7}=0></td>\r\n                </tr>\r\n            </table>";

		// Token: 0x040000C0 RID: 192
		private const string ContainerControlNoCaptionDesignTimeHtml = "<div height=\"{8}\" width=\"{9}\" style=\"{0}{2}{3}{4}{6}{10}\" {7}=0></div>";

		// Token: 0x040000C1 RID: 193
		private Style _defaultFrameStyle;
	}
}
