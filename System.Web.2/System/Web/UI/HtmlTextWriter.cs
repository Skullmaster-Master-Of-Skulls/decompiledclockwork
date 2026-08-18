using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000293 RID: 659
	public class HtmlTextWriter : TextWriter
	{
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x000097B7 File Offset: 0x000079B7
		internal virtual bool RenderDivAroundHiddenInputs
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x00063A2C File Offset: 0x00061C2C
		public virtual void EnterStyle(Style style, HtmlTextWriterTag tag)
		{
			if (!style.IsEmpty || tag != HtmlTextWriterTag.Span)
			{
				style.AddAttributesToRender(this);
				this.RenderBeginTag(tag);
			}
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x00063A49 File Offset: 0x00061C49
		public virtual void ExitStyle(Style style, HtmlTextWriterTag tag)
		{
			if (!style.IsEmpty || tag != HtmlTextWriterTag.Span)
			{
				this.RenderEndTag();
			}
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x00063A5E File Offset: 0x00061C5E
		internal virtual void OpenDiv()
		{
			this.OpenDiv(this._currentLayout, this._currentLayout != null && this._currentLayout.Align > HorizontalAlign.NotSet, this._currentLayout != null && !this._currentLayout.Wrap);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x00063AA0 File Offset: 0x00061CA0
		private void OpenDiv(HtmlTextWriter.Layout layout, bool writeHorizontalAlign, bool writeWrapping)
		{
			this.WriteBeginTag("div");
			if (writeHorizontalAlign)
			{
				HorizontalAlign align = layout.Align;
				string value;
				if (align != HorizontalAlign.Center)
				{
					if (align == HorizontalAlign.Right)
					{
						value = "text-align:right";
					}
					else
					{
						value = "text-align:left";
					}
				}
				else
				{
					value = "text-align:center";
				}
				this.WriteAttribute("style", value);
			}
			if (writeWrapping)
			{
				this.WriteAttribute("mode", layout.Wrap ? "wrap" : "nowrap");
			}
			this.Write('>');
			this._currentWrittenLayout = layout;
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000097B7 File Offset: 0x000079B7
		public virtual bool IsValidFormAttribute(string attribute)
		{
			return true;
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00063B20 File Offset: 0x00061D20
		static HtmlTextWriter()
		{
			HtmlTextWriter.RegisterTag(string.Empty, HtmlTextWriterTag.Unknown, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("a", HtmlTextWriterTag.A, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("acronym", HtmlTextWriterTag.Acronym, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("address", HtmlTextWriterTag.Address, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("area", HtmlTextWriterTag.Area, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("b", HtmlTextWriterTag.B, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("base", HtmlTextWriterTag.Base, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("basefont", HtmlTextWriterTag.Basefont, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("bdo", HtmlTextWriterTag.Bdo, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("bgsound", HtmlTextWriterTag.Bgsound, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("big", HtmlTextWriterTag.Big, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("blockquote", HtmlTextWriterTag.Blockquote, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("body", HtmlTextWriterTag.Body, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("br", HtmlTextWriterTag.Br, BinaryCompatibility.Current.TargetsAtLeastFramework46 ? HtmlTextWriter.TagType.NonClosing : HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("button", HtmlTextWriterTag.Button, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("caption", HtmlTextWriterTag.Caption, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("center", HtmlTextWriterTag.Center, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("cite", HtmlTextWriterTag.Cite, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("code", HtmlTextWriterTag.Code, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("col", HtmlTextWriterTag.Col, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("colgroup", HtmlTextWriterTag.Colgroup, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("del", HtmlTextWriterTag.Del, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("dd", HtmlTextWriterTag.Dd, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("dfn", HtmlTextWriterTag.Dfn, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("dir", HtmlTextWriterTag.Dir, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("div", HtmlTextWriterTag.Div, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("dl", HtmlTextWriterTag.Dl, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("dt", HtmlTextWriterTag.Dt, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("em", HtmlTextWriterTag.Em, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("embed", HtmlTextWriterTag.Embed, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("fieldset", HtmlTextWriterTag.Fieldset, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("font", HtmlTextWriterTag.Font, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("form", HtmlTextWriterTag.Form, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("frame", HtmlTextWriterTag.Frame, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("frameset", HtmlTextWriterTag.Frameset, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("h1", HtmlTextWriterTag.H1, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("h2", HtmlTextWriterTag.H2, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("h3", HtmlTextWriterTag.H3, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("h4", HtmlTextWriterTag.H4, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("h5", HtmlTextWriterTag.H5, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("h6", HtmlTextWriterTag.H6, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("head", HtmlTextWriterTag.Head, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("hr", HtmlTextWriterTag.Hr, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("html", HtmlTextWriterTag.Html, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("i", HtmlTextWriterTag.I, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("iframe", HtmlTextWriterTag.Iframe, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("img", HtmlTextWriterTag.Img, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("input", HtmlTextWriterTag.Input, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("ins", HtmlTextWriterTag.Ins, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("isindex", HtmlTextWriterTag.Isindex, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("kbd", HtmlTextWriterTag.Kbd, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("label", HtmlTextWriterTag.Label, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("legend", HtmlTextWriterTag.Legend, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("li", HtmlTextWriterTag.Li, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("link", HtmlTextWriterTag.Link, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("map", HtmlTextWriterTag.Map, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("marquee", HtmlTextWriterTag.Marquee, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("menu", HtmlTextWriterTag.Menu, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("meta", HtmlTextWriterTag.Meta, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("nobr", HtmlTextWriterTag.Nobr, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("noframes", HtmlTextWriterTag.Noframes, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("noscript", HtmlTextWriterTag.Noscript, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("object", HtmlTextWriterTag.Object, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("ol", HtmlTextWriterTag.Ol, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("option", HtmlTextWriterTag.Option, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("p", HtmlTextWriterTag.P, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("param", HtmlTextWriterTag.Param, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("pre", HtmlTextWriterTag.Pre, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("ruby", HtmlTextWriterTag.Ruby, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("rt", HtmlTextWriterTag.Rt, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("q", HtmlTextWriterTag.Q, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("s", HtmlTextWriterTag.S, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("samp", HtmlTextWriterTag.Samp, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("script", HtmlTextWriterTag.Script, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("select", HtmlTextWriterTag.Select, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("small", HtmlTextWriterTag.Small, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("span", HtmlTextWriterTag.Span, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("strike", HtmlTextWriterTag.Strike, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("strong", HtmlTextWriterTag.Strong, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("style", HtmlTextWriterTag.Style, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("sub", HtmlTextWriterTag.Sub, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("sup", HtmlTextWriterTag.Sup, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("table", HtmlTextWriterTag.Table, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("tbody", HtmlTextWriterTag.Tbody, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("td", HtmlTextWriterTag.Td, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("textarea", HtmlTextWriterTag.Textarea, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("tfoot", HtmlTextWriterTag.Tfoot, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("th", HtmlTextWriterTag.Th, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("thead", HtmlTextWriterTag.Thead, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("title", HtmlTextWriterTag.Title, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("tr", HtmlTextWriterTag.Tr, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("tt", HtmlTextWriterTag.Tt, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("u", HtmlTextWriterTag.U, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("ul", HtmlTextWriterTag.Ul, HtmlTextWriter.TagType.Other);
			HtmlTextWriter.RegisterTag("var", HtmlTextWriterTag.Var, HtmlTextWriter.TagType.Inline);
			HtmlTextWriter.RegisterTag("wbr", HtmlTextWriterTag.Wbr, HtmlTextWriter.TagType.NonClosing);
			HtmlTextWriter.RegisterTag("xml", HtmlTextWriterTag.Xml, HtmlTextWriter.TagType.Other);
			HtmlTextWriter._attrKeyLookupTable = new Hashtable(54);
			HtmlTextWriter._attrNameLookupArray = new HtmlTextWriter.AttributeInformation[54];
			HtmlTextWriter.RegisterAttribute("abbr", HtmlTextWriterAttribute.Abbr, true);
			HtmlTextWriter.RegisterAttribute("accesskey", HtmlTextWriterAttribute.Accesskey, true);
			HtmlTextWriter.RegisterAttribute("align", HtmlTextWriterAttribute.Align, false);
			HtmlTextWriter.RegisterAttribute("alt", HtmlTextWriterAttribute.Alt, true);
			HtmlTextWriter.RegisterAttribute("autocomplete", HtmlTextWriterAttribute.AutoComplete, false);
			HtmlTextWriter.RegisterAttribute("axis", HtmlTextWriterAttribute.Axis, true);
			HtmlTextWriter.RegisterAttribute("background", HtmlTextWriterAttribute.Background, true, true);
			HtmlTextWriter.RegisterAttribute("bgcolor", HtmlTextWriterAttribute.Bgcolor, false);
			HtmlTextWriter.RegisterAttribute("border", HtmlTextWriterAttribute.Border, false);
			HtmlTextWriter.RegisterAttribute("bordercolor", HtmlTextWriterAttribute.Bordercolor, false);
			HtmlTextWriter.RegisterAttribute("cellpadding", HtmlTextWriterAttribute.Cellpadding, false);
			HtmlTextWriter.RegisterAttribute("cellspacing", HtmlTextWriterAttribute.Cellspacing, false);
			HtmlTextWriter.RegisterAttribute("checked", HtmlTextWriterAttribute.Checked, false);
			HtmlTextWriter.RegisterAttribute("class", HtmlTextWriterAttribute.Class, true);
			HtmlTextWriter.RegisterAttribute("cols", HtmlTextWriterAttribute.Cols, false);
			HtmlTextWriter.RegisterAttribute("colspan", HtmlTextWriterAttribute.Colspan, false);
			HtmlTextWriter.RegisterAttribute("content", HtmlTextWriterAttribute.Content, true);
			HtmlTextWriter.RegisterAttribute("coords", HtmlTextWriterAttribute.Coords, false);
			HtmlTextWriter.RegisterAttribute("dir", HtmlTextWriterAttribute.Dir, false);
			HtmlTextWriter.RegisterAttribute("disabled", HtmlTextWriterAttribute.Disabled, false);
			HtmlTextWriter.RegisterAttribute("for", HtmlTextWriterAttribute.For, false);
			HtmlTextWriter.RegisterAttribute("headers", HtmlTextWriterAttribute.Headers, true);
			HtmlTextWriter.RegisterAttribute("height", HtmlTextWriterAttribute.Height, false);
			HtmlTextWriter.RegisterAttribute("href", HtmlTextWriterAttribute.Href, true, true);
			HtmlTextWriter.RegisterAttribute("id", HtmlTextWriterAttribute.Id, false);
			HtmlTextWriter.RegisterAttribute("longdesc", HtmlTextWriterAttribute.Longdesc, true, true);
			HtmlTextWriter.RegisterAttribute("maxlength", HtmlTextWriterAttribute.Maxlength, false);
			HtmlTextWriter.RegisterAttribute("multiple", HtmlTextWriterAttribute.Multiple, false);
			HtmlTextWriter.RegisterAttribute("name", HtmlTextWriterAttribute.Name, false);
			HtmlTextWriter.RegisterAttribute("nowrap", HtmlTextWriterAttribute.Nowrap, false);
			HtmlTextWriter.RegisterAttribute("onclick", HtmlTextWriterAttribute.Onclick, true);
			HtmlTextWriter.RegisterAttribute("onchange", HtmlTextWriterAttribute.Onchange, true);
			HtmlTextWriter.RegisterAttribute("readonly", HtmlTextWriterAttribute.ReadOnly, false);
			HtmlTextWriter.RegisterAttribute("rel", HtmlTextWriterAttribute.Rel, false);
			HtmlTextWriter.RegisterAttribute("rows", HtmlTextWriterAttribute.Rows, false);
			HtmlTextWriter.RegisterAttribute("rowspan", HtmlTextWriterAttribute.Rowspan, false);
			HtmlTextWriter.RegisterAttribute("rules", HtmlTextWriterAttribute.Rules, false);
			HtmlTextWriter.RegisterAttribute("scope", HtmlTextWriterAttribute.Scope, false);
			HtmlTextWriter.RegisterAttribute("selected", HtmlTextWriterAttribute.Selected, false);
			HtmlTextWriter.RegisterAttribute("shape", HtmlTextWriterAttribute.Shape, false);
			HtmlTextWriter.RegisterAttribute("size", HtmlTextWriterAttribute.Size, false);
			HtmlTextWriter.RegisterAttribute("src", HtmlTextWriterAttribute.Src, true, true);
			HtmlTextWriter.RegisterAttribute("style", HtmlTextWriterAttribute.Style, false);
			HtmlTextWriter.RegisterAttribute("tabindex", HtmlTextWriterAttribute.Tabindex, false);
			HtmlTextWriter.RegisterAttribute("target", HtmlTextWriterAttribute.Target, false);
			HtmlTextWriter.RegisterAttribute("title", HtmlTextWriterAttribute.Title, true);
			HtmlTextWriter.RegisterAttribute("type", HtmlTextWriterAttribute.Type, false);
			HtmlTextWriter.RegisterAttribute("usemap", HtmlTextWriterAttribute.Usemap, false);
			HtmlTextWriter.RegisterAttribute("valign", HtmlTextWriterAttribute.Valign, false);
			HtmlTextWriter.RegisterAttribute("value", HtmlTextWriterAttribute.Value, true);
			HtmlTextWriter.RegisterAttribute("vcard_name", HtmlTextWriterAttribute.VCardName, false);
			HtmlTextWriter.RegisterAttribute("width", HtmlTextWriterAttribute.Width, false);
			HtmlTextWriter.RegisterAttribute("wrap", HtmlTextWriterAttribute.Wrap, false);
			HtmlTextWriter.RegisterAttribute("_designerRegion", HtmlTextWriterAttribute.DesignerRegion, false);
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00064309 File Offset: 0x00062509
		public override Encoding Encoding
		{
			get
			{
				return this.writer.Encoding;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00064316 File Offset: 0x00062516
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x00064323 File Offset: 0x00062523
		public override string NewLine
		{
			get
			{
				return this.writer.NewLine;
			}
			set
			{
				this.writer.NewLine = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00064331 File Offset: 0x00062531
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00064339 File Offset: 0x00062539
		public int Indent
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				if (value < 0)
				{
					value = 0;
				}
				this.indentLevel = value;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x00064349 File Offset: 0x00062549
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x00064351 File Offset: 0x00062551
		public TextWriter InnerWriter
		{
			get
			{
				return this.writer;
			}
			set
			{
				this.writer = value;
				this._httpWriter = (value as HttpWriter);
			}
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void BeginRender()
		{
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00064366 File Offset: 0x00062566
		public override void Close()
		{
			this.writer.Close();
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00006164 File Offset: 0x00004364
		public virtual void EndRender()
		{
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x00064373 File Offset: 0x00062573
		public virtual void EnterStyle(Style style)
		{
			this.EnterStyle(style, HtmlTextWriterTag.Span);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x0006437E File Offset: 0x0006257E
		public virtual void ExitStyle(Style style)
		{
			this.ExitStyle(style, HtmlTextWriterTag.Span);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00064389 File Offset: 0x00062589
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00064398 File Offset: 0x00062598
		protected virtual void OutputTabs()
		{
			if (this.tabsPending)
			{
				for (int i = 0; i < this.indentLevel; i++)
				{
					this.writer.Write(this.tabString);
				}
				this.tabsPending = false;
			}
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000643D6 File Offset: 0x000625D6
		public override void Write(string s)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(s);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x000643F2 File Offset: 0x000625F2
		public override void Write(bool value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(value);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0006440E File Offset: 0x0006260E
		public override void Write(char value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(value);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x0006442A File Offset: 0x0006262A
		public override void Write(char[] buffer)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(buffer);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x00064446 File Offset: 0x00062646
		public override void Write(char[] buffer, int index, int count)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(buffer, index, count);
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x00064464 File Offset: 0x00062664
		public override void Write(double value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(value);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00064480 File Offset: 0x00062680
		public override void Write(float value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(value);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0006449C File Offset: 0x0006269C
		public override void Write(int value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(value);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x000644B8 File Offset: 0x000626B8
		public override void Write(long value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(value);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000644D4 File Offset: 0x000626D4
		public override void Write(object value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(value);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x000644F0 File Offset: 0x000626F0
		public override void Write(string format, object arg0)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(format, arg0);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0006450D File Offset: 0x0006270D
		public override void Write(string format, object arg0, object arg1)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(format, arg0, arg1);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0006452B File Offset: 0x0006272B
		public override void Write(string format, params object[] arg)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write(format, arg);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00064548 File Offset: 0x00062748
		public void WriteLineNoTabs(string s)
		{
			this.writer.WriteLine(s);
			this.tabsPending = true;
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x0006455D File Offset: 0x0006275D
		public override void WriteLine(string s)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(s);
			this.tabsPending = true;
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00064580 File Offset: 0x00062780
		public override void WriteLine()
		{
			this.writer.WriteLine();
			this.tabsPending = true;
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00064594 File Offset: 0x00062794
		public override void WriteLine(bool value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x000645B7 File Offset: 0x000627B7
		public override void WriteLine(char value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000645DA File Offset: 0x000627DA
		public override void WriteLine(char[] buffer)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(buffer);
			this.tabsPending = true;
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x000645FD File Offset: 0x000627FD
		public override void WriteLine(char[] buffer, int index, int count)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(buffer, index, count);
			this.tabsPending = true;
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00064622 File Offset: 0x00062822
		public override void WriteLine(double value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00064645 File Offset: 0x00062845
		public override void WriteLine(float value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00064668 File Offset: 0x00062868
		public override void WriteLine(int value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x0006468B File Offset: 0x0006288B
		public override void WriteLine(long value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000646AE File Offset: 0x000628AE
		public override void WriteLine(object value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x000646D1 File Offset: 0x000628D1
		public override void WriteLine(string format, object arg0)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(format, arg0);
			this.tabsPending = true;
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x000646F5 File Offset: 0x000628F5
		public override void WriteLine(string format, object arg0, object arg1)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(format, arg0, arg1);
			this.tabsPending = true;
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x0006471A File Offset: 0x0006291A
		public override void WriteLine(string format, params object[] arg)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(format, arg);
			this.tabsPending = true;
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x0006473E File Offset: 0x0006293E
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.WriteLine(value);
			this.tabsPending = true;
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00064761 File Offset: 0x00062961
		protected static void RegisterTag(string name, HtmlTextWriterTag key)
		{
			HtmlTextWriter.RegisterTag(name, key, HtmlTextWriter.TagType.Other);
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x0006476C File Offset: 0x0006296C
		private static void RegisterTag(string name, HtmlTextWriterTag key, HtmlTextWriter.TagType type)
		{
			string text = name.ToLower(CultureInfo.InvariantCulture);
			HtmlTextWriter._tagKeyLookupTable.Add(text, key);
			string closingTag = null;
			if (type != HtmlTextWriter.TagType.NonClosing && key != HtmlTextWriterTag.Unknown)
			{
				closingTag = "</" + text + '>'.ToString(CultureInfo.InvariantCulture);
			}
			if (key < (HtmlTextWriterTag)HtmlTextWriter._tagNameLookupArray.Length)
			{
				HtmlTextWriter._tagNameLookupArray[(int)key] = new HtmlTextWriter.TagInformation(name, type, closingTag);
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x000647D7 File Offset: 0x000629D7
		protected static void RegisterAttribute(string name, HtmlTextWriterAttribute key)
		{
			HtmlTextWriter.RegisterAttribute(name, key, false);
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000647E1 File Offset: 0x000629E1
		private static void RegisterAttribute(string name, HtmlTextWriterAttribute key, bool encode)
		{
			HtmlTextWriter.RegisterAttribute(name, key, encode, false);
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x000647EC File Offset: 0x000629EC
		private static void RegisterAttribute(string name, HtmlTextWriterAttribute key, bool encode, bool isUrl)
		{
			string key2 = name.ToLower(CultureInfo.InvariantCulture);
			HtmlTextWriter._attrKeyLookupTable.Add(key2, key);
			if (key < (HtmlTextWriterAttribute)HtmlTextWriter._attrNameLookupArray.Length)
			{
				HtmlTextWriter._attrNameLookupArray[(int)key] = new HtmlTextWriter.AttributeInformation(name, encode, isUrl);
			}
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00064833 File Offset: 0x00062A33
		protected static void RegisterStyle(string name, HtmlTextWriterStyle key)
		{
			CssTextWriter.RegisterAttribute(name, key);
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x0006483C File Offset: 0x00062A3C
		public HtmlTextWriter(TextWriter writer) : this(writer, "\t")
		{
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x0006484C File Offset: 0x00062A4C
		public HtmlTextWriter(TextWriter writer, string tabString) : base(CultureInfo.InvariantCulture)
		{
			this.writer = writer;
			this.tabString = tabString;
			this.indentLevel = 0;
			this.tabsPending = false;
			this._httpWriter = (writer as HttpWriter);
			this._isDescendant = (base.GetType() != typeof(HtmlTextWriter));
			this._attrCount = 0;
			this._styleCount = 0;
			this._endTagCount = 0;
			this._inlineCount = 0;
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x000648D0 File Offset: 0x00062AD0
		// (set) Token: 0x06001F41 RID: 8001 RVA: 0x000648D8 File Offset: 0x00062AD8
		protected HtmlTextWriterTag TagKey
		{
			get
			{
				return this._tagKey;
			}
			set
			{
				this._tagIndex = (int)value;
				if (this._tagIndex < 0 || this._tagIndex >= HtmlTextWriter._tagNameLookupArray.Length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._tagKey = value;
				if (value != HtmlTextWriterTag.Unknown)
				{
					this._tagName = HtmlTextWriter._tagNameLookupArray[this._tagIndex].name;
				}
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x00064934 File Offset: 0x00062B34
		// (set) Token: 0x06001F43 RID: 8003 RVA: 0x0006493C File Offset: 0x00062B3C
		protected string TagName
		{
			get
			{
				return this._tagName;
			}
			set
			{
				this._tagName = value;
				this._tagKey = this.GetTagKey(this._tagName);
				this._tagIndex = (int)this._tagKey;
			}
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00064964 File Offset: 0x00062B64
		public virtual void AddAttribute(string name, string value)
		{
			HtmlTextWriterAttribute attributeKey = this.GetAttributeKey(name);
			value = this.EncodeAttributeValue(attributeKey, value);
			this.AddAttribute(name, value, attributeKey);
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0006498C File Offset: 0x00062B8C
		public virtual void AddAttribute(string name, string value, bool fEndode)
		{
			value = this.EncodeAttributeValue(value, fEndode);
			this.AddAttribute(name, value, this.GetAttributeKey(name));
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x000649A8 File Offset: 0x00062BA8
		public virtual void AddAttribute(HtmlTextWriterAttribute key, string value)
		{
			if (key >= HtmlTextWriterAttribute.Accesskey && key < (HtmlTextWriterAttribute)HtmlTextWriter._attrNameLookupArray.Length)
			{
				HtmlTextWriter.AttributeInformation attributeInformation = HtmlTextWriter._attrNameLookupArray[(int)key];
				this.AddAttribute(attributeInformation.name, value, key, attributeInformation.encode, attributeInformation.isUrl);
			}
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x000649EC File Offset: 0x00062BEC
		public virtual void AddAttribute(HtmlTextWriterAttribute key, string value, bool fEncode)
		{
			if (key >= HtmlTextWriterAttribute.Accesskey && key < (HtmlTextWriterAttribute)HtmlTextWriter._attrNameLookupArray.Length)
			{
				HtmlTextWriter.AttributeInformation attributeInformation = HtmlTextWriter._attrNameLookupArray[(int)key];
				this.AddAttribute(attributeInformation.name, value, key, fEncode, attributeInformation.isUrl);
			}
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00064A2A File Offset: 0x00062C2A
		protected virtual void AddAttribute(string name, string value, HtmlTextWriterAttribute key)
		{
			this.AddAttribute(name, value, key, false, false);
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00064A38 File Offset: 0x00062C38
		private void AddAttribute(string name, string value, HtmlTextWriterAttribute key, bool encode, bool isUrl)
		{
			if (this._attrList == null)
			{
				this._attrList = new HtmlTextWriter.RenderAttribute[20];
			}
			else if (this._attrCount >= this._attrList.Length)
			{
				HtmlTextWriter.RenderAttribute[] array = new HtmlTextWriter.RenderAttribute[this._attrList.Length * 2];
				Array.Copy(this._attrList, array, this._attrList.Length);
				this._attrList = array;
			}
			HtmlTextWriter.RenderAttribute renderAttribute;
			renderAttribute.name = name;
			renderAttribute.value = value;
			renderAttribute.key = key;
			renderAttribute.encode = encode;
			renderAttribute.isUrl = isUrl;
			this._attrList[this._attrCount] = renderAttribute;
			this._attrCount++;
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00064AE1 File Offset: 0x00062CE1
		public virtual void AddStyleAttribute(string name, string value)
		{
			this.AddStyleAttribute(name, value, CssTextWriter.GetStyleKey(name));
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00064AF1 File Offset: 0x00062CF1
		public virtual void AddStyleAttribute(HtmlTextWriterStyle key, string value)
		{
			this.AddStyleAttribute(CssTextWriter.GetStyleName(key), value, key);
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x00064B04 File Offset: 0x00062D04
		protected virtual void AddStyleAttribute(string name, string value, HtmlTextWriterStyle key)
		{
			if (this._styleList == null)
			{
				this._styleList = new RenderStyle[20];
			}
			else if (this._styleCount > this._styleList.Length)
			{
				RenderStyle[] array = new RenderStyle[this._styleList.Length * 2];
				Array.Copy(this._styleList, array, this._styleList.Length);
				this._styleList = array;
			}
			RenderStyle renderStyle;
			renderStyle.name = name;
			renderStyle.key = key;
			string value2 = value;
			if (CssTextWriter.IsStyleEncoded(key))
			{
				value2 = HttpUtility.HtmlAttributeEncode(value);
			}
			renderStyle.value = value2;
			this._styleList[this._styleCount] = renderStyle;
			this._styleCount++;
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x00064BAC File Offset: 0x00062DAC
		protected string EncodeAttributeValue(string value, bool fEncode)
		{
			if (value == null)
			{
				return null;
			}
			if (!fEncode)
			{
				return value;
			}
			return HttpUtility.HtmlAttributeEncode(value);
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00064BC0 File Offset: 0x00062DC0
		protected virtual string EncodeAttributeValue(HtmlTextWriterAttribute attrKey, string value)
		{
			bool fEncode = true;
			if (HtmlTextWriterAttribute.Accesskey <= attrKey && attrKey < (HtmlTextWriterAttribute)HtmlTextWriter._attrNameLookupArray.Length)
			{
				fEncode = HtmlTextWriter._attrNameLookupArray[(int)attrKey].encode;
			}
			return this.EncodeAttributeValue(value, fEncode);
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00064BF6 File Offset: 0x00062DF6
		protected string EncodeUrl(string url)
		{
			if (!UrlPath.IsUncSharePath(url))
			{
				return HttpUtility.UrlPathEncode(url);
			}
			return url;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x00064C08 File Offset: 0x00062E08
		protected HtmlTextWriterAttribute GetAttributeKey(string attrName)
		{
			if (!string.IsNullOrEmpty(attrName))
			{
				object obj = HtmlTextWriter._attrKeyLookupTable[attrName.ToLower(CultureInfo.InvariantCulture)];
				if (obj != null)
				{
					return (HtmlTextWriterAttribute)obj;
				}
			}
			return (HtmlTextWriterAttribute)(-1);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00064C3E File Offset: 0x00062E3E
		protected string GetAttributeName(HtmlTextWriterAttribute attrKey)
		{
			if (attrKey >= HtmlTextWriterAttribute.Accesskey && attrKey < (HtmlTextWriterAttribute)HtmlTextWriter._attrNameLookupArray.Length)
			{
				return HtmlTextWriter._attrNameLookupArray[(int)attrKey].name;
			}
			return string.Empty;
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x00064C64 File Offset: 0x00062E64
		protected HtmlTextWriterStyle GetStyleKey(string styleName)
		{
			return CssTextWriter.GetStyleKey(styleName);
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x00064C6C File Offset: 0x00062E6C
		protected string GetStyleName(HtmlTextWriterStyle styleKey)
		{
			return CssTextWriter.GetStyleName(styleKey);
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00064C74 File Offset: 0x00062E74
		protected virtual HtmlTextWriterTag GetTagKey(string tagName)
		{
			if (!string.IsNullOrEmpty(tagName))
			{
				object obj = HtmlTextWriter._tagKeyLookupTable[tagName.ToLower(CultureInfo.InvariantCulture)];
				if (obj != null)
				{
					return (HtmlTextWriterTag)obj;
				}
			}
			return HtmlTextWriterTag.Unknown;
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00064CAC File Offset: 0x00062EAC
		protected virtual string GetTagName(HtmlTextWriterTag tagKey)
		{
			if (tagKey >= HtmlTextWriterTag.Unknown && tagKey < (HtmlTextWriterTag)HtmlTextWriter._tagNameLookupArray.Length)
			{
				return HtmlTextWriter._tagNameLookupArray[(int)tagKey].name;
			}
			return string.Empty;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00064CE0 File Offset: 0x00062EE0
		protected bool IsAttributeDefined(HtmlTextWriterAttribute key)
		{
			for (int i = 0; i < this._attrCount; i++)
			{
				if (this._attrList[i].key == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00064D18 File Offset: 0x00062F18
		protected bool IsAttributeDefined(HtmlTextWriterAttribute key, out string value)
		{
			value = null;
			for (int i = 0; i < this._attrCount; i++)
			{
				if (this._attrList[i].key == key)
				{
					value = this._attrList[i].value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00064D64 File Offset: 0x00062F64
		protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key)
		{
			for (int i = 0; i < this._styleCount; i++)
			{
				if (this._styleList[i].key == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00064D9C File Offset: 0x00062F9C
		protected bool IsStyleAttributeDefined(HtmlTextWriterStyle key, out string value)
		{
			value = null;
			for (int i = 0; i < this._styleCount; i++)
			{
				if (this._styleList[i].key == key)
				{
					value = this._styleList[i].value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x000097B7 File Offset: 0x000079B7
		protected virtual bool OnAttributeRender(string name, string value, HtmlTextWriterAttribute key)
		{
			return true;
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x000097B7 File Offset: 0x000079B7
		protected virtual bool OnStyleAttributeRender(string name, string value, HtmlTextWriterStyle key)
		{
			return true;
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x000097B7 File Offset: 0x000079B7
		protected virtual bool OnTagRender(string name, HtmlTextWriterTag key)
		{
			return true;
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x00064DE8 File Offset: 0x00062FE8
		protected string PopEndTag()
		{
			if (this._endTagCount <= 0)
			{
				throw new InvalidOperationException(SR.GetString("HTMLTextWriterUnbalancedPop"));
			}
			this._endTagCount--;
			this.TagKey = this._endTags[this._endTagCount].tagKey;
			return this._endTags[this._endTagCount].endTagText;
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00064E50 File Offset: 0x00063050
		protected void PushEndTag(string endTag)
		{
			if (this._endTags == null)
			{
				this._endTags = new HtmlTextWriter.TagStackEntry[16];
			}
			else if (this._endTagCount >= this._endTags.Length)
			{
				HtmlTextWriter.TagStackEntry[] array = new HtmlTextWriter.TagStackEntry[this._endTags.Length * 2];
				Array.Copy(this._endTags, array, this._endTags.Length);
				this._endTags = array;
			}
			this._endTags[this._endTagCount].tagKey = this._tagKey;
			this._endTags[this._endTagCount].endTagText = endTag;
			this._endTagCount++;
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00064EF0 File Offset: 0x000630F0
		protected virtual void FilterAttributes()
		{
			int num = 0;
			for (int i = 0; i < this._styleCount; i++)
			{
				RenderStyle renderStyle = this._styleList[i];
				if (this.OnStyleAttributeRender(renderStyle.name, renderStyle.value, renderStyle.key))
				{
					this._styleList[num] = renderStyle;
					num++;
				}
			}
			this._styleCount = num;
			int num2 = 0;
			for (int j = 0; j < this._attrCount; j++)
			{
				HtmlTextWriter.RenderAttribute renderAttribute = this._attrList[j];
				if (this.OnAttributeRender(renderAttribute.name, renderAttribute.value, renderAttribute.key))
				{
					this._attrList[num2] = renderAttribute;
					num2++;
				}
			}
			this._attrCount = num2;
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00064FAB File Offset: 0x000631AB
		public virtual void RenderBeginTag(string tagName)
		{
			this.TagName = tagName;
			this.RenderBeginTag(this._tagKey);
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00064FC0 File Offset: 0x000631C0
		public virtual void RenderBeginTag(HtmlTextWriterTag tagKey)
		{
			this.TagKey = tagKey;
			bool flag = true;
			if (this._isDescendant)
			{
				flag = this.OnTagRender(this._tagName, this._tagKey);
				this.FilterAttributes();
				string text = this.RenderBeforeTag();
				if (text != null)
				{
					if (this.tabsPending)
					{
						this.OutputTabs();
					}
					this.writer.Write(text);
				}
			}
			HtmlTextWriter.TagInformation tagInformation = HtmlTextWriter._tagNameLookupArray[this._tagIndex];
			HtmlTextWriter.TagType tagType = tagInformation.tagType;
			bool flag2 = flag && tagType != HtmlTextWriter.TagType.NonClosing;
			string text2 = flag2 ? tagInformation.closingTag : null;
			if (flag)
			{
				if (this.tabsPending)
				{
					this.OutputTabs();
				}
				this.writer.Write('<');
				this.writer.Write(this._tagName);
				string text3 = null;
				for (int i = 0; i < this._attrCount; i++)
				{
					HtmlTextWriter.RenderAttribute renderAttribute = this._attrList[i];
					if (renderAttribute.key == HtmlTextWriterAttribute.Style)
					{
						text3 = renderAttribute.value;
					}
					else
					{
						this.writer.Write(' ');
						this.writer.Write(renderAttribute.name);
						if (renderAttribute.value != null)
						{
							this.writer.Write("=\"");
							string text4 = renderAttribute.value;
							if (renderAttribute.isUrl && (renderAttribute.key != HtmlTextWriterAttribute.Href || !text4.StartsWith("javascript:", StringComparison.Ordinal)))
							{
								text4 = this.EncodeUrl(text4);
							}
							if (renderAttribute.encode)
							{
								this.WriteHtmlAttributeEncode(text4);
							}
							else
							{
								this.writer.Write(text4);
							}
							this.writer.Write('"');
						}
					}
				}
				if (this._styleCount > 0 || text3 != null)
				{
					this.writer.Write(' ');
					this.writer.Write("style");
					this.writer.Write("=\"");
					CssTextWriter.WriteAttributes(this.writer, this._styleList, this._styleCount);
					if (text3 != null)
					{
						this.writer.Write(text3);
					}
					this.writer.Write('"');
				}
				if (tagType == HtmlTextWriter.TagType.NonClosing)
				{
					this.writer.Write(" />");
				}
				else
				{
					this.writer.Write('>');
				}
			}
			string text5 = this.RenderBeforeContent();
			if (text5 != null)
			{
				if (this.tabsPending)
				{
					this.OutputTabs();
				}
				this.writer.Write(text5);
			}
			if (flag2)
			{
				if (tagType == HtmlTextWriter.TagType.Inline)
				{
					this._inlineCount++;
				}
				else
				{
					this.WriteLine();
					int indent = this.Indent;
					this.Indent = indent + 1;
				}
				if (text2 == null)
				{
					text2 = "</" + this._tagName + '>'.ToString(CultureInfo.InvariantCulture);
				}
			}
			if (this._isDescendant)
			{
				string text6 = this.RenderAfterTag();
				if (text6 != null)
				{
					text2 = ((text2 == null) ? text6 : (text6 + text2));
				}
				string text7 = this.RenderAfterContent();
				if (text7 != null)
				{
					text2 = ((text2 == null) ? text7 : (text7 + text2));
				}
			}
			this.PushEndTag(text2);
			this._attrCount = 0;
			this._styleCount = 0;
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x000652D0 File Offset: 0x000634D0
		public virtual void RenderEndTag()
		{
			string text = this.PopEndTag();
			if (text != null)
			{
				if (HtmlTextWriter._tagNameLookupArray[this._tagIndex].tagType == HtmlTextWriter.TagType.Inline)
				{
					this._inlineCount--;
					this.Write(text);
					return;
				}
				this.WriteLine();
				int indent = this.Indent;
				this.Indent = indent - 1;
				this.Write(text);
			}
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string RenderBeforeTag()
		{
			return null;
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string RenderBeforeContent()
		{
			return null;
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string RenderAfterContent()
		{
			return null;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual string RenderAfterTag()
		{
			return null;
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00065331 File Offset: 0x00063531
		public virtual void WriteAttribute(string name, string value)
		{
			this.WriteAttribute(name, value, false);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x0006533C File Offset: 0x0006353C
		public virtual void WriteAttribute(string name, string value, bool fEncode)
		{
			this.writer.Write(' ');
			this.writer.Write(name);
			if (value != null)
			{
				this.writer.Write("=\"");
				if (fEncode)
				{
					this.WriteHtmlAttributeEncode(value);
				}
				else
				{
					this.writer.Write(value);
				}
				this.writer.Write('"');
			}
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0006539A File Offset: 0x0006359A
		public virtual void WriteBeginTag(string tagName)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write('<');
			this.writer.Write(tagName);
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x000653C3 File Offset: 0x000635C3
		public virtual void WriteBreak()
		{
			this.Write("<br />");
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00055737 File Offset: 0x00053937
		internal void WriteObsoleteBreak()
		{
			this.Write("<br>");
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x000653D0 File Offset: 0x000635D0
		public virtual void WriteFullBeginTag(string tagName)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write('<');
			this.writer.Write(tagName);
			this.writer.Write('>');
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x00065408 File Offset: 0x00063608
		public virtual void WriteEndTag(string tagName)
		{
			if (this.tabsPending)
			{
				this.OutputTabs();
			}
			this.writer.Write('<');
			this.writer.Write('/');
			this.writer.Write(tagName);
			this.writer.Write('>');
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00065456 File Offset: 0x00063656
		public virtual void WriteStyleAttribute(string name, string value)
		{
			this.WriteStyleAttribute(name, value, false);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x00065461 File Offset: 0x00063661
		public virtual void WriteStyleAttribute(string name, string value, bool fEncode)
		{
			this.writer.Write(name);
			this.writer.Write(':');
			if (fEncode)
			{
				this.WriteHtmlAttributeEncode(value);
			}
			else
			{
				this.writer.Write(value);
			}
			this.writer.Write(';');
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000654A1 File Offset: 0x000636A1
		internal void WriteUTF8ResourceString(IntPtr pv, int offset, int size, bool fAsciiOnly)
		{
			if (this._httpWriter != null)
			{
				if (this.tabsPending)
				{
					this.OutputTabs();
				}
				this._httpWriter.WriteUTF8ResourceString(pv, offset, size, fAsciiOnly);
				return;
			}
			this.Write(StringResourceManager.ResourceToString(pv, offset, size));
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x000654D8 File Offset: 0x000636D8
		public virtual void WriteEncodedUrl(string url)
		{
			int num = url.IndexOf('?');
			if (num != -1)
			{
				this.WriteUrlEncodedString(url.Substring(0, num), false);
				this.Write(url.Substring(num));
				return;
			}
			this.WriteUrlEncodedString(url, false);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00065517 File Offset: 0x00063717
		public virtual void WriteEncodedUrlParameter(string urlText)
		{
			this.WriteUrlEncodedString(urlText, true);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x00065524 File Offset: 0x00063724
		public virtual void WriteEncodedText(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			int length = text.Length;
			int i = 0;
			while (i < length)
			{
				int num = text.IndexOf('\u00a0', i);
				if (num < 0)
				{
					HttpUtility.HtmlEncode((i == 0) ? text : text.Substring(i, length - i), this);
					i = length;
				}
				else
				{
					if (num > i)
					{
						HttpUtility.HtmlEncode(text.Substring(i, num - i), this);
					}
					this.Write("&nbsp;");
					i = num + 1;
				}
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0006559C File Offset: 0x0006379C
		protected void WriteUrlEncodedString(string text, bool argument)
		{
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (HttpEncoderUtility.IsUrlSafeChar(c))
				{
					this.Write(c);
				}
				else if (!argument && (c == '/' || c == ':' || c == '#' || c == ','))
				{
					this.Write(c);
				}
				else if (c == ' ' && argument)
				{
					this.Write('+');
				}
				else if ((c & 'ﾀ') == '\0')
				{
					this.Write('%');
					this.Write(HttpEncoderUtility.IntToHex((int)(c >> 4 & '\u000f')));
					this.Write(HttpEncoderUtility.IntToHex((int)(c & '\u000f')));
				}
				else
				{
					this.Write(HttpUtility.UrlEncodeNonAscii(char.ToString(c), Encoding.UTF8));
				}
			}
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x00065657 File Offset: 0x00063857
		internal void WriteHtmlAttributeEncode(string s)
		{
			HttpUtility.HtmlAttributeEncode(s, this._httpWriter ?? this.writer);
		}

		// Token: 0x040019CA RID: 6602
		private HtmlTextWriter.Layout _currentLayout = new HtmlTextWriter.Layout(HorizontalAlign.NotSet, true);

		// Token: 0x040019CB RID: 6603
		private HtmlTextWriter.Layout _currentWrittenLayout;

		// Token: 0x040019CC RID: 6604
		private TextWriter writer;

		// Token: 0x040019CD RID: 6605
		private int indentLevel;

		// Token: 0x040019CE RID: 6606
		private bool tabsPending;

		// Token: 0x040019CF RID: 6607
		private string tabString;

		// Token: 0x040019D0 RID: 6608
		public const char TagLeftChar = '<';

		// Token: 0x040019D1 RID: 6609
		public const char TagRightChar = '>';

		// Token: 0x040019D2 RID: 6610
		public const string SelfClosingChars = " /";

		// Token: 0x040019D3 RID: 6611
		public const string SelfClosingTagEnd = " />";

		// Token: 0x040019D4 RID: 6612
		public const string EndTagLeftChars = "</";

		// Token: 0x040019D5 RID: 6613
		public const char DoubleQuoteChar = '"';

		// Token: 0x040019D6 RID: 6614
		public const char SingleQuoteChar = '\'';

		// Token: 0x040019D7 RID: 6615
		public const char SpaceChar = ' ';

		// Token: 0x040019D8 RID: 6616
		public const char EqualsChar = '=';

		// Token: 0x040019D9 RID: 6617
		public const char SlashChar = '/';

		// Token: 0x040019DA RID: 6618
		public const string EqualsDoubleQuoteString = "=\"";

		// Token: 0x040019DB RID: 6619
		public const char SemicolonChar = ';';

		// Token: 0x040019DC RID: 6620
		public const char StyleEqualsChar = ':';

		// Token: 0x040019DD RID: 6621
		public const string DefaultTabString = "\t";

		// Token: 0x040019DE RID: 6622
		internal const string DesignerRegionAttributeName = "_designerRegion";

		// Token: 0x040019DF RID: 6623
		private static Hashtable _tagKeyLookupTable = new Hashtable(97);

		// Token: 0x040019E0 RID: 6624
		private static Hashtable _attrKeyLookupTable;

		// Token: 0x040019E1 RID: 6625
		private static HtmlTextWriter.TagInformation[] _tagNameLookupArray = new HtmlTextWriter.TagInformation[97];

		// Token: 0x040019E2 RID: 6626
		private static HtmlTextWriter.AttributeInformation[] _attrNameLookupArray;

		// Token: 0x040019E3 RID: 6627
		private HtmlTextWriter.RenderAttribute[] _attrList;

		// Token: 0x040019E4 RID: 6628
		private int _attrCount;

		// Token: 0x040019E5 RID: 6629
		private int _endTagCount;

		// Token: 0x040019E6 RID: 6630
		private HtmlTextWriter.TagStackEntry[] _endTags;

		// Token: 0x040019E7 RID: 6631
		private HttpWriter _httpWriter;

		// Token: 0x040019E8 RID: 6632
		private int _inlineCount;

		// Token: 0x040019E9 RID: 6633
		private bool _isDescendant;

		// Token: 0x040019EA RID: 6634
		private RenderStyle[] _styleList;

		// Token: 0x040019EB RID: 6635
		private int _styleCount;

		// Token: 0x040019EC RID: 6636
		private int _tagIndex;

		// Token: 0x040019ED RID: 6637
		private HtmlTextWriterTag _tagKey;

		// Token: 0x040019EE RID: 6638
		private string _tagName;

		// Token: 0x02000968 RID: 2408
		internal class Layout
		{
			// Token: 0x060069F9 RID: 27129 RVA: 0x00178CD9 File Offset: 0x00176ED9
			public Layout(HorizontalAlign alignment, bool wrapping)
			{
				this.Align = alignment;
				this.Wrap = wrapping;
			}

			// Token: 0x17001D35 RID: 7477
			// (get) Token: 0x060069FA RID: 27130 RVA: 0x00178CEF File Offset: 0x00176EEF
			// (set) Token: 0x060069FB RID: 27131 RVA: 0x00178CF7 File Offset: 0x00176EF7
			public bool Wrap
			{
				get
				{
					return this._wrap;
				}
				set
				{
					this._wrap = value;
				}
			}

			// Token: 0x17001D36 RID: 7478
			// (get) Token: 0x060069FC RID: 27132 RVA: 0x00178D00 File Offset: 0x00176F00
			// (set) Token: 0x060069FD RID: 27133 RVA: 0x00178D08 File Offset: 0x00176F08
			public HorizontalAlign Align
			{
				get
				{
					return this._align;
				}
				set
				{
					this._align = value;
				}
			}

			// Token: 0x0400384C RID: 14412
			private bool _wrap;

			// Token: 0x0400384D RID: 14413
			private HorizontalAlign _align;
		}

		// Token: 0x02000969 RID: 2409
		private struct TagStackEntry
		{
			// Token: 0x0400384E RID: 14414
			public HtmlTextWriterTag tagKey;

			// Token: 0x0400384F RID: 14415
			public string endTagText;
		}

		// Token: 0x0200096A RID: 2410
		private struct RenderAttribute
		{
			// Token: 0x04003850 RID: 14416
			public string name;

			// Token: 0x04003851 RID: 14417
			public string value;

			// Token: 0x04003852 RID: 14418
			public HtmlTextWriterAttribute key;

			// Token: 0x04003853 RID: 14419
			public bool encode;

			// Token: 0x04003854 RID: 14420
			public bool isUrl;
		}

		// Token: 0x0200096B RID: 2411
		private struct AttributeInformation
		{
			// Token: 0x060069FE RID: 27134 RVA: 0x00178D11 File Offset: 0x00176F11
			public AttributeInformation(string name, bool encode, bool isUrl)
			{
				this.name = name;
				this.encode = encode;
				this.isUrl = isUrl;
			}

			// Token: 0x04003855 RID: 14421
			public string name;

			// Token: 0x04003856 RID: 14422
			public bool isUrl;

			// Token: 0x04003857 RID: 14423
			public bool encode;
		}

		// Token: 0x0200096C RID: 2412
		private enum TagType
		{
			// Token: 0x04003859 RID: 14425
			Inline,
			// Token: 0x0400385A RID: 14426
			NonClosing,
			// Token: 0x0400385B RID: 14427
			Other
		}

		// Token: 0x0200096D RID: 2413
		private struct TagInformation
		{
			// Token: 0x060069FF RID: 27135 RVA: 0x00178D28 File Offset: 0x00176F28
			public TagInformation(string name, HtmlTextWriter.TagType tagType, string closingTag)
			{
				this.name = name;
				this.tagType = tagType;
				this.closingTag = closingTag;
			}

			// Token: 0x0400385C RID: 14428
			public string name;

			// Token: 0x0400385D RID: 14429
			public HtmlTextWriter.TagType tagType;

			// Token: 0x0400385E RID: 14430
			public string closingTag;
		}
	}
}
