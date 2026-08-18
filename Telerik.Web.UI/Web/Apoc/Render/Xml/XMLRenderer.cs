using System;
using System.IO;
using System.Text;
using Telerik.Web.Apoc.Fo.Flow;
using Telerik.Web.Apoc.Image;
using Telerik.Web.Apoc.Layout;
using Telerik.Web.Apoc.Layout.Inline;
using Telerik.Web.Apoc.Pdf;
using Telerik.Web.Apoc.Render.Pdf;

namespace Telerik.Web.Apoc.Render.Xml
{
	// Token: 0x020016A3 RID: 5795
	internal class XMLRenderer : IRenderer, IDisposable
	{
		// Token: 0x0600DFC5 RID: 57285 RVA: 0x0031C6A7 File Offset: 0x0031A8A7
		public XMLRenderer(Stream stream)
		{
			this.stream = stream;
		}

		// Token: 0x1700449C RID: 17564
		// (set) Token: 0x0600DFC6 RID: 57286 RVA: 0x0031C6C4 File Offset: 0x0031A8C4
		public IRendererOptions Options
		{
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				XmlRendererOptions xmlRendererOptions = value as XmlRendererOptions;
				if (xmlRendererOptions == null)
				{
					throw new ArgumentException("Options must be an instance of XmlRendererOptions");
				}
				this.options = xmlRendererOptions;
			}
		}

		// Token: 0x0600DFC7 RID: 57287 RVA: 0x0031C6FB File Offset: 0x0031A8FB
		public void Render(Page page)
		{
			this.RenderPage(page);
		}

		// Token: 0x0600DFC8 RID: 57288 RVA: 0x0031C704 File Offset: 0x0031A904
		protected void writeIndent()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.indent; i++)
			{
				stringBuilder = stringBuilder.Append("  ");
			}
			this.writer.Write(stringBuilder.ToString());
		}

		// Token: 0x0600DFC9 RID: 57289 RVA: 0x0031C745 File Offset: 0x0031A945
		protected void writeElement(string element)
		{
			this.writeIndent();
			this.writer.Write(element + "\n");
		}

		// Token: 0x0600DFCA RID: 57290 RVA: 0x0031C763 File Offset: 0x0031A963
		protected void writeEmptyElementTag(string tag)
		{
			this.writeIndent();
			this.writer.Write(tag + "\n");
		}

		// Token: 0x0600DFCB RID: 57291 RVA: 0x0031C781 File Offset: 0x0031A981
		protected void writeEndTag(string tag)
		{
			this.indent--;
			this.writeIndent();
			this.writer.Write(tag + "\n");
		}

		// Token: 0x0600DFCC RID: 57292 RVA: 0x0031C7AD File Offset: 0x0031A9AD
		protected void writeStartTag(string tag)
		{
			this.writeIndent();
			this.writer.Write(tag + "\n");
			this.indent++;
		}

		// Token: 0x0600DFCD RID: 57293 RVA: 0x0031C7D9 File Offset: 0x0031A9D9
		public void SetupFontInfo(FontInfo fontInfo)
		{
			this.fontSetup = new FontSetup(fontInfo, FontType.Link);
		}

		// Token: 0x0600DFCE RID: 57294 RVA: 0x0031C7E8 File Offset: 0x0031A9E8
		public void RenderAreaContainer(AreaContainer area)
		{
			this.writeStartTag("<AreaContainer name=\"" + area.getAreaName() + "\">");
			foreach (object obj in area.getChildren())
			{
				Box box = (Box)obj;
				box.render(this);
			}
			this.writeEndTag("</AreaContainer>");
		}

		// Token: 0x0600DFCF RID: 57295 RVA: 0x0031C868 File Offset: 0x0031AA68
		public void RenderBodyAreaContainer(BodyAreaContainer area)
		{
			this.writeStartTag("<BodyAreaContainer>");
			foreach (object obj in area.getChildren())
			{
				Box box = (Box)obj;
				box.render(this);
			}
			this.writeEndTag("</BodyAreaContainer>");
		}

		// Token: 0x0600DFD0 RID: 57296 RVA: 0x0031C8D8 File Offset: 0x0031AAD8
		public void RenderSpanArea(SpanArea area)
		{
			this.writeStartTag("<SpanArea>");
			foreach (object obj in area.getChildren())
			{
				Box box = (Box)obj;
				box.render(this);
			}
			this.writeEndTag("</SpanArea>");
		}

		// Token: 0x0600DFD1 RID: 57297 RVA: 0x0031C948 File Offset: 0x0031AB48
		public void RenderBlockArea(BlockArea area)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<BlockArea start-indent=\"" + area.getStartIndent() + "\"");
			stringBuilder.Append(" end-indent=\"" + area.getEndIndent() + "\"");
			stringBuilder.Append("\nis-first=\"" + area.isFirst() + "\"");
			stringBuilder.Append(" is-last=\"" + area.isLast() + "\"");
			if (area.getGeneratedBy() != null)
			{
				stringBuilder.Append(" generated-by=\"" + area.getGeneratedBy().GetName() + "//");
			}
			stringBuilder.Append(area.getGeneratedBy() + "\"");
			stringBuilder.Append(">");
			this.writeStartTag(stringBuilder.ToString());
			if (area.getMarkers().Count > 0)
			{
				this.writeStartTag("<Markers>");
				foreach (object obj in area.getMarkers())
				{
					Marker marker = (Marker)obj;
					StringBuilder stringBuilder2 = new StringBuilder();
					stringBuilder2.Append("<Marker marker-class-name=\"" + marker.GetMarkerClassName() + "\"");
					stringBuilder2.Append(" RegisteredArea=\"" + marker.GetRegistryArea() + "\"");
					stringBuilder2.Append("/>");
					this.writeEmptyElementTag(stringBuilder2.ToString());
				}
				this.writeEndTag("</Markers>");
			}
			foreach (object obj2 in area.getChildren())
			{
				Box box = (Box)obj2;
				box.render(this);
			}
			this.writeEndTag("</BlockArea>");
		}

		// Token: 0x0600DFD2 RID: 57298 RVA: 0x0031CB5C File Offset: 0x0031AD5C
		public void RenderInlineArea(InlineArea area)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<InlineArea");
			stringBuilder.Append("\nis-first=\"" + area.isFirst() + "\"");
			stringBuilder.Append(" is-last=\"" + area.isLast() + "\"");
			if (area.getGeneratedBy() != null)
			{
				stringBuilder.Append(string.Concat(new object[]
				{
					" generated-by=\"",
					area.getGeneratedBy().GetName(),
					"//",
					area.getGeneratedBy(),
					"\""
				}));
			}
			stringBuilder.Append(">");
			this.writeStartTag(stringBuilder.ToString());
			if (area.getMarkers().Count > 0)
			{
				this.writeStartTag("<Markers>");
				foreach (object obj in area.getMarkers())
				{
					Marker marker = (Marker)obj;
					StringBuilder stringBuilder2 = new StringBuilder();
					stringBuilder2.Append("<Marker marker-class-name=\"" + marker.GetMarkerClassName() + "\"");
					stringBuilder2.Append(" RegisteredArea=\"" + marker.GetRegistryArea() + "\"");
					stringBuilder2.Append("/>");
					this.writeEmptyElementTag(stringBuilder2.ToString());
				}
				this.writeEndTag("</Markers>");
			}
			foreach (object obj2 in area.getChildren())
			{
				Box box = (Box)obj2;
				box.render(this);
			}
			this.writeEndTag("</InlineArea>");
		}

		// Token: 0x0600DFD3 RID: 57299 RVA: 0x0031CD4C File Offset: 0x0031AF4C
		public void RenderDisplaySpace(DisplaySpace space)
		{
			if (this.options.FineDetail)
			{
				this.writeEmptyElementTag("<DisplaySpace size=\"" + space.getSize() + "\"/>");
			}
		}

		// Token: 0x0600DFD4 RID: 57300 RVA: 0x0031CD7B File Offset: 0x0031AF7B
		public void RenderForeignObjectArea(ForeignObjectArea area)
		{
			area.getObject().render(this);
		}

		// Token: 0x0600DFD5 RID: 57301 RVA: 0x0031CD89 File Offset: 0x0031AF89
		public void RenderImageArea(ImageArea area)
		{
			this.writeEmptyElementTag("<ImageArea/>");
		}

		// Token: 0x0600DFD6 RID: 57302 RVA: 0x0031CD98 File Offset: 0x0031AF98
		public void RenderWordArea(WordArea area)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = area.getText();
			int length = text.Length;
			for (int i = 0; i < length; i++)
			{
				char c = text[i];
				if (c > '\u007f')
				{
					stringBuilder = stringBuilder.Append("&#" + (int)c + ";");
				}
				else
				{
					stringBuilder = stringBuilder.Append(c);
				}
			}
			if (this.options.FineDetail)
			{
				this.writeElement(string.Concat(new object[]
				{
					"<WordArea font-weight=\"",
					0,
					"\" red=\"",
					area.getRed(),
					"\" green=\"",
					area.getGreen(),
					"\" blue=\"",
					area.getBlue(),
					"\" width=\"",
					area.getContentWidth(),
					"\">",
					stringBuilder.ToString(),
					"</WordArea>"
				}));
				return;
			}
			this.writer.Write(stringBuilder.ToString());
		}

		// Token: 0x0600DFD7 RID: 57303 RVA: 0x0031CEC7 File Offset: 0x0031B0C7
		public void RenderInlineSpace(InlineSpace space)
		{
			if (this.options.FineDetail)
			{
				this.writeEmptyElementTag("<InlineSpace size=\"" + space.getSize() + "\"/>");
				return;
			}
			this.writer.Write(" ");
		}

		// Token: 0x0600DFD8 RID: 57304 RVA: 0x0031CF08 File Offset: 0x0031B108
		public void RenderLineArea(LineArea area)
		{
			if (this.options.FineDetail)
			{
				string fontWeight = area.GetFontState().FontWeight;
				this.writeStartTag("<LineArea font-weight=\"" + 0 + "\">");
			}
			foreach (object obj in area.getChildren())
			{
				Box box = (Box)obj;
				box.render(this);
			}
			if (this.options.FineDetail)
			{
				this.writeEndTag("</LineArea>");
				return;
			}
			this.writer.Write("\n");
		}

		// Token: 0x0600DFD9 RID: 57305 RVA: 0x0031CFC0 File Offset: 0x0031B1C0
		public void RenderPage(Page page)
		{
			this.writeStartTag("<Page number=\"" + page.getFormattedNumber() + "\">");
			BodyAreaContainer body = page.getBody();
			AreaContainer before = page.getBefore();
			AreaContainer after = page.getAfter();
			if (before != null)
			{
				this.RenderAreaContainer(before);
			}
			this.RenderBodyAreaContainer(body);
			if (after != null)
			{
				this.RenderAreaContainer(after);
			}
			this.writeEndTag("</Page>");
		}

		// Token: 0x0600DFDA RID: 57306 RVA: 0x0031D024 File Offset: 0x0031B224
		public void RenderLeaderArea(LeaderArea area)
		{
			if (!this.options.FineDetail)
			{
				return;
			}
			string text = "";
			int leaderPattern = area.getLeaderPattern();
			if (leaderPattern <= 66)
			{
				if (leaderPattern != 19)
				{
					if (leaderPattern == 66)
					{
						text = "rule";
					}
				}
				else
				{
					text = "dots";
				}
			}
			else if (leaderPattern != 71)
			{
				if (leaderPattern == 84)
				{
					text = "use-content";
				}
			}
			else
			{
				text = "space";
			}
			this.writeEmptyElementTag(string.Concat(new object[]
			{
				"<Leader leader-pattern=\"",
				text,
				" leader-length=\"",
				area.getLeaderLength(),
				"\" rule-thickness=\"",
				area.getRuleThickness(),
				"\" rule-style=\"",
				area.getRuleStyle(),
				"\" red=\"",
				area.getRed(),
				"\" green=\"",
				area.getGreen(),
				"\" blue=\"",
				area.getBlue(),
				"\"/>"
			}));
		}

		// Token: 0x0600DFDB RID: 57307 RVA: 0x0031D139 File Offset: 0x0031B339
		public void StartRenderer()
		{
			ApocDriver.ActiveDriver.FireApocInfo("Rendering areas to XML");
			this.writer = new StreamWriter(this.stream);
			this.writer.Write("<?xml version=\"1.0\"?>\n");
			this.writeStartTag("<AreaTree>");
		}

		// Token: 0x0600DFDC RID: 57308 RVA: 0x0031D176 File Offset: 0x0031B376
		public void StopRenderer()
		{
			this.writeEndTag("</AreaTree>");
			this.writer.Flush();
			this.writer.Close();
			this.writer = null;
			ApocDriver.ActiveDriver.FireApocInfo("Written out XML");
		}

		// Token: 0x0600DFDD RID: 57309 RVA: 0x0031D1AF File Offset: 0x0031B3AF
		public void Dispose()
		{
			this.Dispose(false);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600DFDE RID: 57310 RVA: 0x0031D1BE File Offset: 0x0031B3BE
		protected virtual void Dispose(bool finalizing)
		{
			if (!finalizing && this.writer != null)
			{
				this.writer.Close();
			}
		}

		// Token: 0x0600DFDF RID: 57311 RVA: 0x0031D1D8 File Offset: 0x0031B3D8
		~XMLRenderer()
		{
			this.Dispose(true);
		}

		// Token: 0x040040B3 RID: 16563
		private Stream stream;

		// Token: 0x040040B4 RID: 16564
		private int indent;

		// Token: 0x040040B5 RID: 16565
		private TextWriter writer;

		// Token: 0x040040B6 RID: 16566
		private XmlRendererOptions options = XmlRendererOptions.Default;

		// Token: 0x040040B7 RID: 16567
		private FontSetup fontSetup;
	}
}
