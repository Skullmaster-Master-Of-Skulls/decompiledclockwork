using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000290 RID: 656
	public class Html32TextWriter : HtmlTextWriter
	{
		// Token: 0x06001EE1 RID: 7905 RVA: 0x0006273D File Offset: 0x0006093D
		public Html32TextWriter(TextWriter writer) : this(writer, "\t")
		{
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x0006274C File Offset: 0x0006094C
		public Html32TextWriter(TextWriter writer, string tabString) : base(writer, tabString)
		{
			this._beforeTag = new StringBuilder(256);
			this._beforeContent = new StringBuilder(256);
			this._afterContent = new StringBuilder(128);
			this._afterTag = new StringBuilder(128);
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x000627AF File Offset: 0x000609AF
		protected Stack FontStack
		{
			get
			{
				if (this._fontStack == null)
				{
					this._fontStack = new Stack(3);
				}
				return this._fontStack;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x00007722 File Offset: 0x00005922
		internal override bool RenderDivAroundHiddenInputs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x000627CB File Offset: 0x000609CB
		// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x000627D3 File Offset: 0x000609D3
		public bool ShouldPerformDivTableSubstitution
		{
			get
			{
				return this._shouldPerformDivTableSubstitution;
			}
			set
			{
				this._shouldPerformDivTableSubstitution = value;
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x000627DC File Offset: 0x000609DC
		// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x000627E4 File Offset: 0x000609E4
		public bool SupportsBold
		{
			get
			{
				return this._supportsBold;
			}
			set
			{
				this._supportsBold = value;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x000627ED File Offset: 0x000609ED
		// (set) Token: 0x06001EEA RID: 7914 RVA: 0x000627F5 File Offset: 0x000609F5
		public bool SupportsItalic
		{
			get
			{
				return this._supportsItalic;
			}
			set
			{
				this._supportsItalic = value;
			}
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x000627FE File Offset: 0x000609FE
		private void AppendFontTag(StringBuilder sbBegin, StringBuilder sbEnd)
		{
			this.AppendFontTag(this._fontFace, this._fontColor, this._fontSize, sbBegin, sbEnd);
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0006281C File Offset: 0x00060A1C
		private void AppendFontTag(string fontFace, string fontColor, string fontSize, StringBuilder sbBegin, StringBuilder sbEnd)
		{
			sbBegin.Append('<');
			sbBegin.Append("font");
			if (fontFace != null)
			{
				sbBegin.Append(" face");
				sbBegin.Append("=\"");
				sbBegin.Append(fontFace);
				sbBegin.Append('"');
			}
			if (fontColor != null)
			{
				sbBegin.Append(" color=");
				sbBegin.Append('"');
				sbBegin.Append(fontColor);
				sbBegin.Append('"');
			}
			if (fontSize != null)
			{
				sbBegin.Append(" size=");
				sbBegin.Append('"');
				sbBegin.Append(fontSize);
				sbBegin.Append('"');
			}
			sbBegin.Append('>');
			sbEnd.Insert(0, "</font>");
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x000628E2 File Offset: 0x00060AE2
		private void AppendOtherTag(string tag)
		{
			if (this.Supports(1))
			{
				this.AppendOtherTag(tag, this._beforeContent, this._afterContent);
				return;
			}
			this.AppendOtherTag(tag, this._beforeTag, this._afterTag);
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00062914 File Offset: 0x00060B14
		private void AppendOtherTag(string tag, StringBuilder sbBegin, StringBuilder sbEnd)
		{
			sbBegin.Append('<');
			sbBegin.Append(tag);
			sbBegin.Append('>');
			sbEnd.Insert(0, "</" + tag + ">");
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00062948 File Offset: 0x00060B48
		private void AppendOtherTag(string tag, object[] attribs, StringBuilder sbBegin, StringBuilder sbEnd)
		{
			sbBegin.Append('<');
			sbBegin.Append(tag);
			for (int i = 0; i < attribs.Length; i++)
			{
				sbBegin.Append(' ');
				sbBegin.Append(((string[])attribs[i])[0]);
				sbBegin.Append("=\"");
				sbBegin.Append(((string[])attribs[i])[1]);
				sbBegin.Append('"');
			}
			sbBegin.Append('>');
			sbEnd.Insert(0, "</" + tag + ">");
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000629D8 File Offset: 0x00060BD8
		private void ConsumeFont(StringBuilder sbBegin, StringBuilder sbEnd)
		{
			if (this.FontStack.Count > 0)
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				foreach (object obj in this.FontStack)
				{
					Html32TextWriter.FontStackItem fontStackItem = (Html32TextWriter.FontStackItem)obj;
					if (text == null)
					{
						text = fontStackItem.name;
					}
					if (text2 == null)
					{
						text2 = fontStackItem.color;
					}
					if (text3 == null)
					{
						text3 = fontStackItem.size;
					}
					if (!flag)
					{
						flag = fontStackItem.underline;
					}
					if (!flag2)
					{
						flag2 = fontStackItem.italic;
					}
					if (!flag3)
					{
						flag3 = fontStackItem.bold;
					}
					if (!flag4)
					{
						flag4 = fontStackItem.strikeout;
					}
				}
				if (text != null || text2 != null || text3 != null)
				{
					this.AppendFontTag(text, text2, text3, sbBegin, sbEnd);
				}
				if (flag)
				{
					this.AppendOtherTag("u", sbBegin, sbEnd);
				}
				if (flag2 && this.SupportsItalic)
				{
					this.AppendOtherTag("i", sbBegin, sbEnd);
				}
				if (flag3 && this.SupportsBold)
				{
					this.AppendOtherTag("b", sbBegin, sbEnd);
				}
				if (flag4)
				{
					this.AppendOtherTag("strike", sbBegin, sbEnd);
				}
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00062AE8 File Offset: 0x00060CE8
		private string ConvertToHtmlFontSize(string value)
		{
			FontUnit fontUnit = new FontUnit(value, CultureInfo.InvariantCulture);
			if (fontUnit.Type > FontSize.Larger)
			{
				return (fontUnit.Type - FontSize.Larger).ToString(CultureInfo.InvariantCulture);
			}
			if (fontUnit.Type != FontSize.AsUnit || fontUnit.Unit.Type != UnitType.Point)
			{
				return null;
			}
			if (fontUnit.Unit.Value <= 8.0)
			{
				return "1";
			}
			if (fontUnit.Unit.Value <= 10.0)
			{
				return "2";
			}
			if (fontUnit.Unit.Value <= 12.0)
			{
				return "3";
			}
			if (fontUnit.Unit.Value <= 14.0)
			{
				return "4";
			}
			if (fontUnit.Unit.Value <= 18.0)
			{
				return "5";
			}
			if (fontUnit.Unit.Value <= 24.0)
			{
				return "6";
			}
			return "7";
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00062C0C File Offset: 0x00060E0C
		private string ConvertToHtmlSize(string value)
		{
			Unit unit = new Unit(value, CultureInfo.InvariantCulture);
			if (unit.Type == UnitType.Pixel)
			{
				return unit.Value.ToString(CultureInfo.InvariantCulture);
			}
			if (unit.Type == UnitType.Percentage)
			{
				return value;
			}
			return null;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00062C54 File Offset: 0x00060E54
		protected override bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
		{
			if (this.Supports(1))
			{
				switch (key)
				{
				case HtmlTextWriterStyle.Color:
					this._fontColor = value;
					this._renderFontTag = true;
					break;
				case HtmlTextWriterStyle.FontFamily:
					this._fontFace = value;
					this._renderFontTag = true;
					break;
				case HtmlTextWriterStyle.FontSize:
					this._fontSize = this.ConvertToHtmlFontSize(value);
					if (this._fontSize != null)
					{
						this._renderFontTag = true;
					}
					break;
				case HtmlTextWriterStyle.FontStyle:
					if (!StringUtil.EqualsIgnoreCase(value, "normal") && this.SupportsItalic)
					{
						this.AppendOtherTag("i");
					}
					break;
				case HtmlTextWriterStyle.FontWeight:
					if (StringUtil.EqualsIgnoreCase(value, "bold") && this.SupportsBold)
					{
						this.AppendOtherTag("b");
					}
					break;
				case HtmlTextWriterStyle.TextDecoration:
				{
					string text = value.ToLower(CultureInfo.InvariantCulture);
					if (text.IndexOf("underline", StringComparison.Ordinal) != -1)
					{
						this.AppendOtherTag("u");
					}
					if (text.IndexOf("line-through", StringComparison.Ordinal) != -1)
					{
						this.AppendOtherTag("strike");
					}
					break;
				}
				}
			}
			else if (this.Supports(16))
			{
				Html32TextWriter.FontStackItem fontStackItem = (Html32TextWriter.FontStackItem)this.FontStack.Peek();
				switch (key)
				{
				case HtmlTextWriterStyle.Color:
					fontStackItem.color = value;
					break;
				case HtmlTextWriterStyle.FontFamily:
					fontStackItem.name = value;
					break;
				case HtmlTextWriterStyle.FontSize:
					fontStackItem.size = this.ConvertToHtmlFontSize(value);
					break;
				case HtmlTextWriterStyle.FontStyle:
					if (!StringUtil.EqualsIgnoreCase(value, "normal"))
					{
						fontStackItem.italic = true;
					}
					break;
				case HtmlTextWriterStyle.FontWeight:
					if (StringUtil.EqualsIgnoreCase(value, "bold"))
					{
						fontStackItem.bold = true;
					}
					break;
				case HtmlTextWriterStyle.TextDecoration:
				{
					string text = value.ToLower(CultureInfo.InvariantCulture);
					if (text.IndexOf("underline", StringComparison.Ordinal) != -1)
					{
						fontStackItem.underline = true;
					}
					if (text.IndexOf("line-through", StringComparison.Ordinal) != -1)
					{
						fontStackItem.strikeout = true;
					}
					break;
				}
				}
			}
			if (this.Supports(128) && key == HtmlTextWriterStyle.BorderWidth)
			{
				string text = this.ConvertToHtmlSize(value);
				if (text != null)
				{
					this.AddAttribute(HtmlTextWriterAttribute.Border, text);
				}
			}
			if (this.Supports(256) && key == HtmlTextWriterStyle.WhiteSpace)
			{
				this.AddAttribute(HtmlTextWriterAttribute.Nowrap, value);
			}
			if (this.Supports(64))
			{
				if (key != HtmlTextWriterStyle.Height)
				{
					if (key == HtmlTextWriterStyle.Width)
					{
						string text = this.ConvertToHtmlSize(value);
						if (text != null)
						{
							this.AddAttribute(HtmlTextWriterAttribute.Width, text);
						}
					}
				}
				else
				{
					string text = this.ConvertToHtmlSize(value);
					if (text != null)
					{
						this.AddAttribute(HtmlTextWriterAttribute.Height, text);
					}
				}
			}
			if (this.Supports(4) || this.Supports(8))
			{
				switch (key)
				{
				case HtmlTextWriterStyle.BackgroundColor:
				{
					HtmlTextWriterTag tagKey = base.TagKey;
					if (tagKey <= HtmlTextWriterTag.Table)
					{
						if (tagKey != HtmlTextWriterTag.Body)
						{
							if (tagKey != HtmlTextWriterTag.Div)
							{
								if (tagKey != HtmlTextWriterTag.Table)
								{
									break;
								}
							}
							else
							{
								if (this.ShouldPerformDivTableSubstitution)
								{
									this.AddAttribute(HtmlTextWriterAttribute.Bgcolor, value);
									break;
								}
								break;
							}
						}
					}
					else if (tagKey != HtmlTextWriterTag.Td && tagKey != HtmlTextWriterTag.Th && tagKey != HtmlTextWriterTag.Tr)
					{
						break;
					}
					this.AddAttribute(HtmlTextWriterAttribute.Bgcolor, value);
					break;
				}
				case HtmlTextWriterStyle.BackgroundImage:
				{
					HtmlTextWriterTag tagKey2 = base.TagKey;
					if (tagKey2 <= HtmlTextWriterTag.Div)
					{
						if (tagKey2 != HtmlTextWriterTag.Body)
						{
							if (tagKey2 != HtmlTextWriterTag.Div)
							{
								break;
							}
							if (this.ShouldPerformDivTableSubstitution)
							{
								if (StringUtil.StringStartsWith(value, "url("))
								{
									value = value.Substring(4, value.Length - 5);
								}
								this.AddAttribute(HtmlTextWriterAttribute.Background, value);
								break;
							}
							break;
						}
					}
					else if (tagKey2 != HtmlTextWriterTag.Table && tagKey2 != HtmlTextWriterTag.Td && tagKey2 != HtmlTextWriterTag.Th)
					{
						break;
					}
					if (StringUtil.StringStartsWith(value, "url("))
					{
						value = value.Substring(4, value.Length - 5);
					}
					this.AddAttribute(HtmlTextWriterAttribute.Background, value);
					break;
				}
				case HtmlTextWriterStyle.BorderColor:
				{
					HtmlTextWriterTag tagKey3 = base.TagKey;
					if (tagKey3 == HtmlTextWriterTag.Div && this.ShouldPerformDivTableSubstitution)
					{
						this.AddAttribute(HtmlTextWriterAttribute.Bordercolor, value);
					}
					break;
				}
				}
			}
			if (key <= HtmlTextWriterStyle.Display)
			{
				if (key != HtmlTextWriterStyle.ListStyleType)
				{
					if (key == HtmlTextWriterStyle.Display)
					{
						return true;
					}
				}
				else
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(value);
					if (num <= 831800219U)
					{
						if (num <= 520654156U)
						{
							if (num != 71098662U)
							{
								if (num != 520654156U)
								{
									goto IL_52E;
								}
								if (!(value == "decimal"))
								{
									goto IL_52E;
								}
								this.AddAttribute(HtmlTextWriterAttribute.Type, "1");
								return false;
							}
							else
							{
								if (!(value == "upper-alpha"))
								{
									goto IL_52E;
								}
								this.AddAttribute(HtmlTextWriterAttribute.Type, "A");
								return false;
							}
						}
						else if (num != 673280137U)
						{
							if (num != 831800219U)
							{
								goto IL_52E;
							}
							if (!(value == "lower-alpha"))
							{
								goto IL_52E;
							}
							this.AddAttribute(HtmlTextWriterAttribute.Type, "a");
							return false;
						}
						else if (!(value == "circle"))
						{
							goto IL_52E;
						}
					}
					else if (num <= 1374042131U)
					{
						if (num != 1029714956U)
						{
							if (num != 1374042131U)
							{
								goto IL_52E;
							}
							if (!(value == "upper-roman"))
							{
								goto IL_52E;
							}
							this.AddAttribute(HtmlTextWriterAttribute.Type, "I");
							return false;
						}
						else if (!(value == "disc"))
						{
							goto IL_52E;
						}
					}
					else if (num != 3031831110U)
					{
						if (num != 4050104546U)
						{
							goto IL_52E;
						}
						if (!(value == "lower-roman"))
						{
							goto IL_52E;
						}
						this.AddAttribute(HtmlTextWriterAttribute.Type, "i");
						return false;
					}
					else if (!(value == "square"))
					{
						goto IL_52E;
					}
					this.AddAttribute(HtmlTextWriterAttribute.Type, value);
					return false;
					IL_52E:
					this.AddAttribute(HtmlTextWriterAttribute.Type, "disc");
				}
			}
			else if (key != HtmlTextWriterStyle.TextAlign)
			{
				if (key == HtmlTextWriterStyle.VerticalAlign)
				{
					this.AddAttribute(HtmlTextWriterAttribute.Valign, value);
				}
			}
			else
			{
				this.AddAttribute(HtmlTextWriterAttribute.Align, value);
			}
			return false;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000631B6 File Offset: 0x000613B6
		protected override bool OnTagRender(string name, HtmlTextWriterTag key)
		{
			this.SetTagSupports();
			if (this.Supports(16))
			{
				this.FontStack.Push(new Html32TextWriter.FontStackItem());
			}
			if (key == HtmlTextWriterTag.Div && this.ShouldPerformDivTableSubstitution)
			{
				base.TagKey = HtmlTextWriterTag.Table;
			}
			return base.OnTagRender(name, key);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000631F5 File Offset: 0x000613F5
		protected override string GetTagName(HtmlTextWriterTag tagKey)
		{
			if (tagKey == HtmlTextWriterTag.Div && this.ShouldPerformDivTableSubstitution)
			{
				return "table";
			}
			return base.GetTagName(tagKey);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00063214 File Offset: 0x00061414
		public override void RenderBeginTag(HtmlTextWriterTag tagKey)
		{
			this._beforeTag.Length = 0;
			this._beforeContent.Length = 0;
			this._afterContent.Length = 0;
			this._afterTag.Length = 0;
			this._renderFontTag = false;
			this._fontFace = null;
			this._fontColor = null;
			this._fontSize = null;
			if (this.ShouldPerformDivTableSubstitution && tagKey == HtmlTextWriterTag.Div)
			{
				this.AppendOtherTag("tr", this._beforeContent, this._afterContent);
				string text;
				if (base.IsAttributeDefined(HtmlTextWriterAttribute.Align, out text))
				{
					string[] array = new string[]
					{
						base.GetAttributeName(HtmlTextWriterAttribute.Align),
						text
					};
					this.AppendOtherTag("td", new object[]
					{
						array
					}, this._beforeContent, this._afterContent);
				}
				else
				{
					this.AppendOtherTag("td", this._beforeContent, this._afterContent);
				}
				if (!base.IsAttributeDefined(HtmlTextWriterAttribute.Cellpadding))
				{
					this.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "0");
				}
				if (!base.IsAttributeDefined(HtmlTextWriterAttribute.Cellspacing))
				{
					this.AddAttribute(HtmlTextWriterAttribute.Cellspacing, "0");
				}
				if (!base.IsStyleAttributeDefined(HtmlTextWriterStyle.BorderWidth))
				{
					this.AddAttribute(HtmlTextWriterAttribute.Border, "0");
				}
				if (!base.IsStyleAttributeDefined(HtmlTextWriterStyle.Width))
				{
					this.AddAttribute(HtmlTextWriterAttribute.Width, "100%");
				}
			}
			base.RenderBeginTag(tagKey);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00063350 File Offset: 0x00061550
		protected override string RenderBeforeTag()
		{
			if (this._renderFontTag && this.Supports(2))
			{
				this.AppendFontTag(this._beforeTag, this._afterTag);
			}
			if (this._beforeTag.Length > 0)
			{
				return this._beforeTag.ToString();
			}
			return base.RenderBeforeTag();
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x000633A0 File Offset: 0x000615A0
		protected override string RenderBeforeContent()
		{
			if (this.Supports(32))
			{
				this.ConsumeFont(this._beforeContent, this._afterContent);
			}
			else if (this._renderFontTag && this.Supports(1))
			{
				this.AppendFontTag(this._beforeContent, this._afterContent);
			}
			if (this._beforeContent.Length > 0)
			{
				return this._beforeContent.ToString();
			}
			return base.RenderBeforeContent();
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0006340E File Offset: 0x0006160E
		protected override string RenderAfterContent()
		{
			if (this._afterContent.Length > 0)
			{
				return this._afterContent.ToString();
			}
			return base.RenderAfterContent();
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00063430 File Offset: 0x00061630
		protected override string RenderAfterTag()
		{
			if (this._afterTag.Length > 0)
			{
				return this._afterTag.ToString();
			}
			return base.RenderAfterTag();
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00063452 File Offset: 0x00061652
		public override void RenderEndTag()
		{
			base.RenderEndTag();
			this.SetTagSupports();
			if (this.Supports(16))
			{
				this.FontStack.Pop();
			}
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00063478 File Offset: 0x00061678
		private void SetTagSupports()
		{
			this._tagSupports = 0;
			HtmlTextWriterTag tagKey = base.TagKey;
			if (tagKey <= HtmlTextWriterTag.Ol)
			{
				if (tagKey <= HtmlTextWriterTag.Input)
				{
					if (tagKey != HtmlTextWriterTag.A)
					{
						if (tagKey == HtmlTextWriterTag.Div)
						{
							this._tagSupports |= 17;
							goto IL_DE;
						}
						if (tagKey != HtmlTextWriterTag.Input)
						{
							goto IL_DE;
						}
						this._tagSupports |= 128;
						goto IL_DE;
					}
				}
				else if (tagKey != HtmlTextWriterTag.Label)
				{
					if (tagKey == HtmlTextWriterTag.Li)
					{
						this._tagSupports |= 33;
						goto IL_DE;
					}
					if (tagKey != HtmlTextWriterTag.Ol)
					{
						goto IL_DE;
					}
					goto IL_AA;
				}
			}
			else if (tagKey <= HtmlTextWriterTag.Table)
			{
				if (tagKey != HtmlTextWriterTag.P && tagKey != HtmlTextWriterTag.Span)
				{
					if (tagKey != HtmlTextWriterTag.Table)
					{
						goto IL_DE;
					}
					goto IL_AA;
				}
			}
			else if (tagKey <= HtmlTextWriterTag.Th)
			{
				if (tagKey != HtmlTextWriterTag.Td && tagKey != HtmlTextWriterTag.Th)
				{
					goto IL_DE;
				}
				this._tagSupports |= 48;
				goto IL_DE;
			}
			else
			{
				if (tagKey != HtmlTextWriterTag.Tr && tagKey != HtmlTextWriterTag.Ul)
				{
					goto IL_DE;
				}
				goto IL_AA;
			}
			this._tagSupports |= 1;
			goto IL_DE;
			IL_AA:
			this._tagSupports |= 16;
			IL_DE:
			HtmlTextWriterTag tagKey2 = base.TagKey;
			if (tagKey2 <= HtmlTextWriterTag.Img)
			{
				if (tagKey2 != HtmlTextWriterTag.Div)
				{
					if (tagKey2 == HtmlTextWriterTag.Img)
					{
						this._tagSupports |= 192;
					}
				}
				else
				{
					if (this.ShouldPerformDivTableSubstitution)
					{
						this._tagSupports |= 192;
					}
					this._tagSupports |= 256;
				}
			}
			else if (tagKey2 != HtmlTextWriterTag.Table)
			{
				if (tagKey2 == HtmlTextWriterTag.Td || tagKey2 == HtmlTextWriterTag.Th)
				{
					this._tagSupports |= 320;
				}
			}
			else
			{
				this._tagSupports |= 64;
			}
			HtmlTextWriterTag tagKey3 = base.TagKey;
			if (tagKey3 <= HtmlTextWriterTag.Table)
			{
				if (tagKey3 != HtmlTextWriterTag.Body && tagKey3 != HtmlTextWriterTag.Table)
				{
					goto IL_1A1;
				}
			}
			else if (tagKey3 != HtmlTextWriterTag.Td && tagKey3 != HtmlTextWriterTag.Th && tagKey3 != HtmlTextWriterTag.Tr)
			{
				goto IL_1A1;
			}
			this._tagSupports |= 4;
			IL_1A1:
			HtmlTextWriterTag tagKey4 = base.TagKey;
			if (tagKey4 == HtmlTextWriterTag.Div && this.ShouldPerformDivTableSubstitution)
			{
				this._tagSupports |= 8;
			}
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00063648 File Offset: 0x00061848
		private bool Supports(int flag)
		{
			return (this._tagSupports & flag) == flag;
		}

		// Token: 0x040019AD RID: 6573
		private const int NOTHING = 0;

		// Token: 0x040019AE RID: 6574
		private const int FONT_AROUND_CONTENT = 1;

		// Token: 0x040019AF RID: 6575
		private const int FONT_AROUND_TAG = 2;

		// Token: 0x040019B0 RID: 6576
		private const int TABLE_ATTRIBUTES = 4;

		// Token: 0x040019B1 RID: 6577
		private const int TABLE_AROUND_CONTENT = 8;

		// Token: 0x040019B2 RID: 6578
		private const int FONT_PROPAGATE = 16;

		// Token: 0x040019B3 RID: 6579
		private const int FONT_CONSUME = 32;

		// Token: 0x040019B4 RID: 6580
		private const int SUPPORTS_HEIGHT_WIDTH = 64;

		// Token: 0x040019B5 RID: 6581
		private const int SUPPORTS_BORDER = 128;

		// Token: 0x040019B6 RID: 6582
		private const int SUPPORTS_NOWRAP = 256;

		// Token: 0x040019B7 RID: 6583
		private StringBuilder _afterContent;

		// Token: 0x040019B8 RID: 6584
		private StringBuilder _afterTag;

		// Token: 0x040019B9 RID: 6585
		private StringBuilder _beforeContent;

		// Token: 0x040019BA RID: 6586
		private StringBuilder _beforeTag;

		// Token: 0x040019BB RID: 6587
		private string _fontColor;

		// Token: 0x040019BC RID: 6588
		private string _fontFace;

		// Token: 0x040019BD RID: 6589
		private string _fontSize;

		// Token: 0x040019BE RID: 6590
		private Stack _fontStack;

		// Token: 0x040019BF RID: 6591
		private bool _shouldPerformDivTableSubstitution;

		// Token: 0x040019C0 RID: 6592
		private bool _renderFontTag;

		// Token: 0x040019C1 RID: 6593
		private bool _supportsBold = true;

		// Token: 0x040019C2 RID: 6594
		private bool _supportsItalic = true;

		// Token: 0x040019C3 RID: 6595
		private int _tagSupports;

		// Token: 0x02000967 RID: 2407
		private sealed class FontStackItem
		{
			// Token: 0x04003845 RID: 14405
			public string name;

			// Token: 0x04003846 RID: 14406
			public string color;

			// Token: 0x04003847 RID: 14407
			public string size;

			// Token: 0x04003848 RID: 14408
			public bool bold;

			// Token: 0x04003849 RID: 14409
			public bool italic;

			// Token: 0x0400384A RID: 14410
			public bool underline;

			// Token: 0x0400384B RID: 14411
			public bool strikeout;
		}
	}
}
