using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.util;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.xml.simpleparser;

namespace iTextSharp.text.html.simpleparser
{
	// Token: 0x02000226 RID: 550
	public class HTMLWorker : ISimpleXMLDocHandler, IDocListener, IElementListener
	{
		// Token: 0x06001562 RID: 5474 RVA: 0x00079DFC File Offset: 0x00078DFC
		public HTMLWorker(IDocListener document)
		{
			this.document = document;
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00079E56 File Offset: 0x00078E56
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x00079E4D File Offset: 0x00078E4D
		public StyleSheet Style
		{
			get
			{
				return this.style;
			}
			set
			{
				this.style = value;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x00079EB5 File Offset: 0x00078EB5
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x00079E60 File Offset: 0x00078E60
		public Dictionary<string, object> InterfaceProps
		{
			get
			{
				return this.interfaceProps;
			}
			set
			{
				this.interfaceProps = value;
				IFontProvider fontProvider = null;
				if (this.interfaceProps != null && this.interfaceProps.ContainsKey("font_factory"))
				{
					fontProvider = (IFontProvider)this.interfaceProps["font_factory"];
				}
				if (fontProvider != null)
				{
					this.factoryProperties.FontImp = fontProvider;
				}
			}
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00079EBD File Offset: 0x00078EBD
		public void Parse(TextReader reader)
		{
			SimpleXMLParser.Parse(this, null, reader, true);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00079EC8 File Offset: 0x00078EC8
		public static List<IElement> ParseToList(TextReader reader, StyleSheet style)
		{
			return HTMLWorker.ParseToList(reader, style, null);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x00079ED4 File Offset: 0x00078ED4
		public static List<IElement> ParseToList(TextReader reader, StyleSheet style, Dictionary<string, object> interfaceProps)
		{
			HTMLWorker htmlworker = new HTMLWorker(null);
			if (style != null)
			{
				htmlworker.Style = style;
			}
			htmlworker.document = htmlworker;
			htmlworker.InterfaceProps = interfaceProps;
			htmlworker.objectList = new List<IElement>();
			htmlworker.Parse(reader);
			return htmlworker.objectList;
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00079F18 File Offset: 0x00078F18
		public virtual void EndDocument()
		{
			foreach (IElement element in this.stack)
			{
				this.document.Add(element);
			}
			if (this.currentParagraph != null)
			{
				this.document.Add(this.currentParagraph);
			}
			this.currentParagraph = null;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00079F94 File Offset: 0x00078F94
		public virtual void StartDocument()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			this.style.ApplyStyle("body", dictionary);
			this.cprops.AddToChain("body", dictionary);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00079FCC File Offset: 0x00078FCC
		public virtual void StartElement(string tag, Dictionary<string, string> h)
		{
			if (!HTMLWorker.tagsSupported.ContainsKey(tag))
			{
				return;
			}
			this.style.ApplyStyle(tag, h);
			string text = null;
			if (FactoryProperties.followTags.ContainsKey(tag))
			{
				text = FactoryProperties.followTags[tag];
			}
			if (text != null)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				dictionary[text] = null;
				this.cprops.AddToChain(text, dictionary);
				return;
			}
			FactoryProperties.InsertStyle(h, this.cprops);
			if (tag.Equals("a"))
			{
				this.cprops.AddToChain(tag, h);
				if (this.currentParagraph == null)
				{
					this.currentParagraph = new Paragraph();
				}
				this.stack.Push(this.currentParagraph);
				this.currentParagraph = new Paragraph();
				return;
			}
			if (tag.Equals("br"))
			{
				if (this.currentParagraph == null)
				{
					this.currentParagraph = new Paragraph();
				}
				this.currentParagraph.Add(this.factoryProperties.CreateChunk("\n", this.cprops));
				return;
			}
			if (tag.Equals("hr"))
			{
				bool flag = true;
				if (this.currentParagraph == null)
				{
					this.currentParagraph = new Paragraph();
					flag = false;
				}
				if (flag)
				{
					int count = this.currentParagraph.Chunks.Count;
					if (count == 0 || this.currentParagraph.Chunks[count - 1].Content.EndsWith("\n"))
					{
						flag = false;
					}
				}
				int align = 1;
				string s;
				if (h.TryGetValue("align", out s))
				{
					if (Util.EqualsIgnoreCase(s, "left"))
					{
						align = 0;
					}
					if (Util.EqualsIgnoreCase(s, "right"))
					{
						align = 2;
					}
				}
				float percentage = 1f;
				string text2;
				if (h.TryGetValue("width", out text2))
				{
					float num = Markup.ParseLength(text2, 12f);
					if (num > 0f)
					{
						percentage = num;
					}
					if (!text2.EndsWith("%"))
					{
						percentage = 100f;
					}
				}
				float lineWidth = 1f;
				string str;
				if (h.TryGetValue("size", out str))
				{
					float num2 = Markup.ParseLength(str, 12f);
					if (num2 > 0f)
					{
						lineWidth = num2;
					}
				}
				if (flag)
				{
					this.currentParagraph.Add(Chunk.NEWLINE);
				}
				this.currentParagraph.Add(new LineSeparator(lineWidth, percentage, null, align, this.currentParagraph.Leading / 2f));
				this.currentParagraph.Add(Chunk.NEWLINE);
				return;
			}
			if (tag.Equals("font") || tag.Equals("span"))
			{
				this.cprops.AddToChain(tag, h);
				return;
			}
			if (tag.Equals("img"))
			{
				string text3;
				if (!h.TryGetValue("src", out text3))
				{
					return;
				}
				this.cprops.AddToChain(tag, h);
				Image image = null;
				if (this.interfaceProps != null)
				{
					if (this.interfaceProps.ContainsKey("img_provider"))
					{
						IImageProvider imageProvider = (IImageProvider)this.interfaceProps["img_provider"];
						image = imageProvider.GetImage(text3, h, this.cprops, this.document);
					}
					if (image == null)
					{
						if (this.interfaceProps.ContainsKey("img_static"))
						{
							Dictionary<string, Image> dictionary2 = (Dictionary<string, Image>)this.interfaceProps["img_static"];
							Image image2;
							if (dictionary2.TryGetValue(text3, out image2))
							{
								image = Image.GetInstance(image2);
							}
						}
						else if (!text3.StartsWith("http") && this.interfaceProps.ContainsKey("img_baseurl"))
						{
							text3 = (string)this.interfaceProps["img_baseurl"] + text3;
							image = Image.GetInstance(text3);
						}
					}
				}
				if (image == null)
				{
					if (!text3.StartsWith("http"))
					{
						string text4 = this.cprops["image_path"];
						if (text4 == null)
						{
							text4 = "";
						}
						text3 = Path.Combine(text4, text3);
					}
					image = Image.GetInstance(text3);
				}
				string text5;
				h.TryGetValue("align", out text5);
				string str2;
				h.TryGetValue("width", out str2);
				string str3;
				h.TryGetValue("height", out str3);
				string text6 = this.cprops["before"];
				string text7 = this.cprops["after"];
				if (text6 != null)
				{
					image.SpacingBefore = float.Parse(text6, NumberFormatInfo.InvariantInfo);
				}
				if (text7 != null)
				{
					image.SpacingAfter = float.Parse(text7, NumberFormatInfo.InvariantInfo);
				}
				float num3 = Markup.ParseLength(this.cprops["size"], 12f);
				if (num3 <= 0f)
				{
					num3 = 12f;
				}
				float num4 = Markup.ParseLength(str2, num3);
				float num5 = Markup.ParseLength(str3, num3);
				if (num4 > 0f && num5 > 0f)
				{
					image.ScaleAbsolute(num4, num5);
				}
				else if (num4 > 0f)
				{
					num5 = image.Height * num4 / image.Width;
					image.ScaleAbsolute(num4, num5);
				}
				else if (num5 > 0f)
				{
					num4 = image.Width * num5 / image.Height;
					image.ScaleAbsolute(num4, num5);
				}
				image.WidthPercentage = 0f;
				if (text5 != null)
				{
					this.EndElement("p");
					int alignment = 1;
					if (Util.EqualsIgnoreCase(text5, "left"))
					{
						alignment = 0;
					}
					else if (Util.EqualsIgnoreCase(text5, "right"))
					{
						alignment = 2;
					}
					image.Alignment = alignment;
					IImg img = null;
					bool flag2 = false;
					if (this.interfaceProps != null)
					{
						if (this.interfaceProps.ContainsKey("img_interface"))
						{
							img = (IImg)this.interfaceProps["img_interface"];
						}
						if (img != null)
						{
							flag2 = img.Process(image, h, this.cprops, this.document);
						}
					}
					if (!flag2)
					{
						this.document.Add(image);
					}
					this.cprops.RemoveChain(tag);
					return;
				}
				this.cprops.RemoveChain(tag);
				if (this.currentParagraph == null)
				{
					this.currentParagraph = FactoryProperties.CreateParagraph(this.cprops);
				}
				this.currentParagraph.Add(new Chunk(image, 0f, 0f));
				return;
			}
			else
			{
				this.EndElement("p");
				if (tag.Equals("h1") || tag.Equals("h2") || tag.Equals("h3") || tag.Equals("h4") || tag.Equals("h5") || tag.Equals("h6"))
				{
					if (!h.ContainsKey("size"))
					{
						h["size"] = (7 - int.Parse(tag.Substring(1))).ToString();
					}
					this.cprops.AddToChain(tag, h);
					return;
				}
				if (tag.Equals("ul"))
				{
					if (this.pendingLI)
					{
						this.EndElement("li");
					}
					this.skipText = true;
					this.cprops.AddToChain(tag, h);
					List list = new List(false);
					try
					{
						list.IndentationLeft = float.Parse(this.cprops["indent"], NumberFormatInfo.InvariantInfo);
					}
					catch
					{
						list.Autoindent = true;
					}
					list.SetListSymbol("•");
					this.stack.Push(list);
					return;
				}
				if (tag.Equals("ol"))
				{
					if (this.pendingLI)
					{
						this.EndElement("li");
					}
					this.skipText = true;
					this.cprops.AddToChain(tag, h);
					List list2 = new List(true);
					try
					{
						list2.IndentationLeft = float.Parse(this.cprops["indent"], NumberFormatInfo.InvariantInfo);
					}
					catch
					{
						list2.Autoindent = true;
					}
					this.stack.Push(list2);
					return;
				}
				if (tag.Equals("li"))
				{
					if (this.pendingLI)
					{
						this.EndElement("li");
					}
					this.skipText = false;
					this.pendingLI = true;
					this.cprops.AddToChain(tag, h);
					this.stack.Push(FactoryProperties.CreateListItem(this.cprops));
					return;
				}
				if (tag.Equals("div") || tag.Equals("body") || tag.Equals("p"))
				{
					this.cprops.AddToChain(tag, h);
					return;
				}
				if (tag.Equals("pre"))
				{
					if (!h.ContainsKey("face"))
					{
						h["face"] = "Courier";
					}
					this.cprops.AddToChain(tag, h);
					this.isPRE = true;
					return;
				}
				if (tag.Equals("tr"))
				{
					if (this.pendingTR)
					{
						this.EndElement("tr");
					}
					this.skipText = true;
					this.pendingTR = true;
					this.cprops.AddToChain("tr", h);
					return;
				}
				if (tag.Equals("td") || tag.Equals("th"))
				{
					if (this.pendingTD)
					{
						this.EndElement(tag);
					}
					this.skipText = false;
					this.pendingTD = true;
					this.cprops.AddToChain("td", h);
					this.stack.Push(new IncCell(tag, this.cprops));
					return;
				}
				if (tag.Equals("table"))
				{
					this.cprops.AddToChain("table", h);
					IncTable item = new IncTable(h);
					this.stack.Push(item);
					this.tableState.Push(new bool[]
					{
						this.pendingTR,
						this.pendingTD
					});
					this.pendingTR = (this.pendingTD = false);
					this.skipText = true;
				}
				return;
			}
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0007A958 File Offset: 0x00079958
		public virtual void EndElement(string tag)
		{
			if (!HTMLWorker.tagsSupported.ContainsKey(tag))
			{
				return;
			}
			string key;
			if (FactoryProperties.followTags.TryGetValue(tag, out key))
			{
				this.cprops.RemoveChain(key);
				return;
			}
			if (tag.Equals("font") || tag.Equals("span"))
			{
				this.cprops.RemoveChain(tag);
				return;
			}
			if (tag.Equals("a"))
			{
				if (this.currentParagraph == null)
				{
					this.currentParagraph = new Paragraph();
				}
				IALink ialink = null;
				bool flag = false;
				if (this.interfaceProps != null)
				{
					if (this.interfaceProps.ContainsKey("alink_interface"))
					{
						ialink = (IALink)this.interfaceProps["alink_interface"];
					}
					if (ialink != null)
					{
						flag = ialink.Process(this.currentParagraph, this.cprops);
					}
				}
				if (!flag)
				{
					string text = this.cprops["href"];
					if (text != null)
					{
						foreach (Chunk chunk in this.currentParagraph.Chunks)
						{
							chunk.SetAnchor(text);
						}
					}
				}
				Paragraph paragraph = (Paragraph)this.stack.Pop();
				paragraph.Add(new Phrase
				{
					this.currentParagraph
				});
				this.currentParagraph = paragraph;
				this.cprops.RemoveChain("a");
				return;
			}
			if (tag.Equals("br"))
			{
				return;
			}
			if (this.currentParagraph != null)
			{
				if (this.stack.Count == 0)
				{
					this.document.Add(this.currentParagraph);
				}
				else
				{
					IElement element = this.stack.Pop();
					if (element is ITextElementArray)
					{
						ITextElementArray textElementArray = (ITextElementArray)element;
						textElementArray.Add(this.currentParagraph);
					}
					this.stack.Push(element);
				}
			}
			this.currentParagraph = null;
			if (tag.Equals("ul") || tag.Equals("ol"))
			{
				if (this.pendingLI)
				{
					this.EndElement("li");
				}
				this.skipText = false;
				this.cprops.RemoveChain(tag);
				if (this.stack.Count == 0)
				{
					return;
				}
				IElement element2 = this.stack.Pop();
				if (!(element2 is List))
				{
					this.stack.Push(element2);
					return;
				}
				if (this.stack.Count == 0)
				{
					this.document.Add(element2);
					return;
				}
				((ITextElementArray)this.stack.Peek()).Add(element2);
				return;
			}
			else if (tag.Equals("li"))
			{
				this.pendingLI = false;
				this.skipText = true;
				this.cprops.RemoveChain(tag);
				if (this.stack.Count == 0)
				{
					return;
				}
				IElement element3 = this.stack.Pop();
				if (!(element3 is ListItem))
				{
					this.stack.Push(element3);
					return;
				}
				if (this.stack.Count == 0)
				{
					this.document.Add(element3);
					return;
				}
				IElement element4 = this.stack.Pop();
				if (!(element4 is List))
				{
					this.stack.Push(element4);
					return;
				}
				ListItem listItem = (ListItem)element3;
				((List)element4).Add(listItem);
				List<Chunk> chunks = listItem.Chunks;
				if (chunks.Count > 0)
				{
					listItem.ListSymbol.Font = chunks[0].Font;
				}
				this.stack.Push(element4);
				return;
			}
			else
			{
				if (tag.Equals("div") || tag.Equals("body"))
				{
					this.cprops.RemoveChain(tag);
					return;
				}
				if (tag.Equals("pre"))
				{
					this.cprops.RemoveChain(tag);
					this.isPRE = false;
					return;
				}
				if (tag.Equals("p"))
				{
					this.cprops.RemoveChain(tag);
					return;
				}
				if (tag.Equals("h1") || tag.Equals("h2") || tag.Equals("h3") || tag.Equals("h4") || tag.Equals("h5") || tag.Equals("h6"))
				{
					this.cprops.RemoveChain(tag);
					return;
				}
				if (tag.Equals("table"))
				{
					if (this.pendingTR)
					{
						this.EndElement("tr");
					}
					this.cprops.RemoveChain("table");
					IncTable incTable = (IncTable)this.stack.Pop();
					PdfPTable pdfPTable = incTable.BuildTable();
					pdfPTable.SplitRows = true;
					if (this.stack.Count == 0)
					{
						this.document.Add(pdfPTable);
					}
					else
					{
						((ITextElementArray)this.stack.Peek()).Add(pdfPTable);
					}
					bool[] array = this.tableState.Pop();
					this.pendingTR = array[0];
					this.pendingTD = array[1];
					this.skipText = false;
					return;
				}
				if (tag.Equals("tr"))
				{
					if (this.pendingTD)
					{
						this.EndElement("td");
					}
					this.pendingTR = false;
					this.cprops.RemoveChain("tr");
					List<PdfPCell> list = new List<PdfPCell>();
					IElement element5;
					do
					{
						element5 = this.stack.Pop();
						if (element5 is IncCell)
						{
							list.Add(((IncCell)element5).Cell);
						}
					}
					while (!(element5 is IncTable));
					IncTable incTable2 = (IncTable)element5;
					incTable2.AddCols(list);
					incTable2.EndRow();
					this.stack.Push(incTable2);
					this.skipText = true;
					return;
				}
				if (tag.Equals("td") || tag.Equals("th"))
				{
					this.pendingTD = false;
					this.cprops.RemoveChain("td");
					this.skipText = true;
				}
				return;
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0007AF30 File Offset: 0x00079F30
		public virtual void Text(string str)
		{
			if (this.skipText)
			{
				return;
			}
			if (this.isPRE)
			{
				if (this.currentParagraph == null)
				{
					this.currentParagraph = FactoryProperties.CreateParagraph(this.cprops);
				}
				this.currentParagraph.Add(this.factoryProperties.CreateChunk(str, this.cprops));
				return;
			}
			if (str.Trim().Length == 0 && str.IndexOf(' ') < 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			bool flag = false;
			int i = 0;
			while (i < length)
			{
				char c;
				char value = c = str[i];
				switch (c)
				{
				case '\t':
				case '\r':
					break;
				case '\n':
					if (i > 0)
					{
						flag = true;
						stringBuilder.Append(' ');
					}
					break;
				case '\v':
				case '\f':
					goto IL_CA;
				default:
					if (c != ' ')
					{
						goto IL_CA;
					}
					if (!flag)
					{
						stringBuilder.Append(value);
					}
					break;
				}
				IL_D5:
				i++;
				continue;
				IL_CA:
				flag = false;
				stringBuilder.Append(value);
				goto IL_D5;
			}
			if (this.currentParagraph == null)
			{
				this.currentParagraph = FactoryProperties.CreateParagraph(this.cprops);
			}
			this.currentParagraph.Add(this.factoryProperties.CreateChunk(stringBuilder.ToString(), this.cprops));
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x0007B059 File Offset: 0x0007A059
		public bool Add(IElement element)
		{
			this.objectList.Add(element);
			return true;
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0007B068 File Offset: 0x0007A068
		public void ClearTextWrap()
		{
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0007B06A File Offset: 0x0007A06A
		public void Close()
		{
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0007B06C File Offset: 0x0007A06C
		public bool NewPage()
		{
			return true;
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0007B06F File Offset: 0x0007A06F
		public void Open()
		{
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0007B071 File Offset: 0x0007A071
		public void ResetFooter()
		{
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0007B073 File Offset: 0x0007A073
		public void ResetHeader()
		{
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0007B075 File Offset: 0x0007A075
		public void ResetPageCount()
		{
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0007B077 File Offset: 0x0007A077
		public bool SetMarginMirroring(bool marginMirroring)
		{
			return false;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0007B07A File Offset: 0x0007A07A
		public bool SetMarginMirroringTopBottom(bool marginMirroring)
		{
			return false;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0007B07D File Offset: 0x0007A07D
		public bool SetMargins(float marginLeft, float marginRight, float marginTop, float marginBottom)
		{
			return true;
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0007B080 File Offset: 0x0007A080
		public bool SetPageSize(Rectangle pageSize)
		{
			return true;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0007B084 File Offset: 0x0007A084
		static HTMLWorker()
		{
			StringTokenizer stringTokenizer = new StringTokenizer("ol ul li a pre font span br p div body table td th tr i b u sub sup em strong s strike h1 h2 h3 h4 h5 h6 img hr");
			while (stringTokenizer.HasMoreTokens())
			{
				HTMLWorker.tagsSupported[stringTokenizer.NextToken()] = null;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (set) Token: 0x0600157C RID: 5500 RVA: 0x0007B0C1 File Offset: 0x0007A0C1
		public int PageCount
		{
			set
			{
			}
		}

		// Token: 0x04000E79 RID: 3705
		public const string tagsSupportedString = "ol ul li a pre font span br p div body table td th tr i b u sub sup em strong s strike h1 h2 h3 h4 h5 h6 img hr";

		// Token: 0x04000E7A RID: 3706
		protected List<IElement> objectList;

		// Token: 0x04000E7B RID: 3707
		protected IDocListener document;

		// Token: 0x04000E7C RID: 3708
		private Paragraph currentParagraph;

		// Token: 0x04000E7D RID: 3709
		private ChainedProperties cprops = new ChainedProperties();

		// Token: 0x04000E7E RID: 3710
		private Stack<IElement> stack = new Stack<IElement>();

		// Token: 0x04000E7F RID: 3711
		private bool pendingTR;

		// Token: 0x04000E80 RID: 3712
		private bool pendingTD;

		// Token: 0x04000E81 RID: 3713
		private bool pendingLI;

		// Token: 0x04000E82 RID: 3714
		private StyleSheet style = new StyleSheet();

		// Token: 0x04000E83 RID: 3715
		private bool isPRE;

		// Token: 0x04000E84 RID: 3716
		private Stack<bool[]> tableState = new Stack<bool[]>();

		// Token: 0x04000E85 RID: 3717
		private bool skipText;

		// Token: 0x04000E86 RID: 3718
		private Dictionary<string, object> interfaceProps;

		// Token: 0x04000E87 RID: 3719
		private FactoryProperties factoryProperties = new FactoryProperties();

		// Token: 0x04000E88 RID: 3720
		public static Dictionary<string, object> tagsSupported = new Dictionary<string, object>();
	}
}
