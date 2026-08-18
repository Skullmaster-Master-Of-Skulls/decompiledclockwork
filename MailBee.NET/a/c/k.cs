using System;
using System.Collections;
using System.Xml;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace a.c
{
	// Token: 0x0200022B RID: 555
	internal class k : j
	{
		// Token: 0x06001297 RID: 4759 RVA: 0x000526FB File Offset: 0x000516FB
		public k(s A_0, PdfPCell A_1, bool A_2) : base(A_0)
		{
			this.c = A_1;
			this.e = A_2;
			this.b();
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00052718 File Offset: 0x00051718
		public k(s A_0, ITextElementArray A_1, bool A_2) : base(A_0)
		{
			this.b = A_1;
			this.e = A_2;
			this.b();
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00052738 File Offset: 0x00051738
		private new void b()
		{
			this.d = new Hashtable();
			this.d["a"] = new q(base.a());
			w value = new h(base.a());
			this.d["h1"] = value;
			this.d["h2"] = value;
			this.d["h3"] = value;
			this.d["h4"] = value;
			this.d["h5"] = value;
			this.d["h6"] = value;
			this.d["img"] = new v(base.a());
			value = new o(base.a());
			this.d["ol"] = value;
			this.d["ul"] = value;
			this.d["li"] = new f(base.a());
			this.d["dd"] = new d(base.a());
			this.d["header"] = new m(base.a());
			value = new t(base.a());
			this.d["div"] = value;
			this.d["p"] = value;
			this.d["pre"] = value;
			this.d["center"] = value;
			value = new y(base.a());
			this.d["b"] = value;
			this.d["br"] = value;
			this.d["code"] = value;
			this.d["em"] = value;
			this.d["font"] = value;
			this.d["hr"] = value;
			this.d["i"] = value;
			this.d["it"] = value;
			this.d["pagebreak"] = value;
			this.d["sup"] = value;
			this.d["span"] = value;
			this.d["small"] = value;
			this.d["strong"] = value;
			this.d["tt"] = value;
			this.d["u"] = value;
			this.d["strike"] = value;
			this.d["table"] = new e(base.a());
			this.d["verbatim"] = new c(base.a());
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x00052A19 File Offset: 0x00051A19
		public IDictionary c()
		{
			return this.d;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00052A21 File Offset: 0x00051A21
		public ITextElementArray d()
		{
			return this.b;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00052A2C File Offset: 0x00051A2C
		public new XmlNode b(XmlNode A_0, u A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("el");
			}
			bool flag = A_1.a();
			if (flag)
			{
				flag &= (A_0.Name.ToLower() == "div" || A_0.Name.ToLower() == "p");
			}
			if (this.c().Contains(A_0.Name.ToLower()) && !flag)
			{
				w w = this.c()[A_0.Name.ToLower()] as w;
				w.an(A_0, new u(A_0, A_1));
				if (this.b != null)
				{
					this.b.Add(w.ao());
				}
				else if (this.c != null)
				{
					if (w.ao() is Phrase)
					{
						if (this.c.Phrase == null)
						{
							if (this.c.CompositeElements == null)
							{
								this.c.Phrase = (Phrase)w.ao();
							}
							else
							{
								this.c.AddElement((Phrase)w.ao());
							}
						}
						else if (w.ao() is Paragraph && ((Paragraph)w.ao()).Count > 0)
						{
							for (int i = 0; i < ((Paragraph)w.ao()).Count; i++)
							{
								if (((Paragraph)w.ao())[i] is PdfPTable)
								{
									this.c.AddElement((IElement)((Paragraph)w.ao())[i]);
								}
								else
								{
									this.c.Phrase.Add((IElement)((Paragraph)w.ao())[i]);
								}
							}
						}
						else
						{
							this.c.Phrase.Add((Phrase)w.ao());
						}
					}
					else
					{
						this.c.AddElement(w.ao());
					}
				}
				else if (w.ao() != null)
				{
					this.b.n().Add(w.ao());
				}
				return A_0.NextSibling;
			}
			if (A_0.NodeType == XmlNodeType.Text)
			{
				IElement element = base.a().l().hi(A_0.Value.Trim());
				if (this.b != null)
				{
					this.b.Add(element);
				}
				else if (this.c != null)
				{
					this.c.AddElement(element);
				}
				else
				{
					base.a().n().Add(element);
				}
			}
			else
			{
				this.a(A_0, A_1);
			}
			return A_0.NextSibling;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00052CC8 File Offset: 0x00051CC8
		public void a(XmlNode A_0, u A_1)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("el");
			}
			Font font;
			if (A_1.b() != null)
			{
				font = A_1.b();
			}
			else
			{
				font = base.g(A_0);
			}
			font.Color = A_1.e();
			XmlNode xmlNode = A_0.FirstChild;
			while (xmlNode != null)
			{
				if (xmlNode.NodeType == XmlNodeType.Text)
				{
					if ((A_0.Name == "p" || A_0.Name == "li") && xmlNode.NextSibling != null)
					{
						this.e = true;
					}
					else if (A_0.Name == "p" && (xmlNode.NextSibling == null || A_0.NextSibling.Name == "ul" || A_0.NextSibling.Name == "ol"))
					{
						this.e = false;
					}
					else if (A_0.Name == "p" && xmlNode.NextSibling == null)
					{
						this.e = false;
					}
					string text = string.Empty;
					if (xmlNode.Value != null && xmlNode.Value != string.Empty)
					{
						text = au.c(xmlNode.Value);
						char[] trimChars = " \t\r\n".ToCharArray();
						ArrayList arrayList = new ArrayList(trimChars);
						if (arrayList.Contains(text[0]))
						{
							text = ((xmlNode.ParentNode.FirstChild != xmlNode) ? " " : "") + text.TrimStart(trimChars);
						}
						if (arrayList.Contains(text[text.Length - 1]))
						{
							text = text.TrimEnd(trimChars) + " ";
						}
						text = text.Replace("&nbsp;", " ");
					}
					if (this.b != null)
					{
						if (!this.e)
						{
							text += "\r\n\r\n";
						}
						Phrase phrase = new Phrase(text, font);
						if (A_1.h() != null)
						{
							phrase = (Phrase)A_1.h()(A_0, phrase);
						}
						this.b.Add(phrase);
					}
					else if (this.c != null)
					{
						if (!this.e)
						{
							text += "\r\n\r\n";
						}
						Phrase phrase2 = new Phrase(text, font);
						if (A_1.h() != null)
						{
							phrase2 = (Phrase)A_1.h()(A_0, phrase2);
						}
						if (this.c.Phrase == null)
						{
							if (this.c.CompositeElements == null)
							{
								this.c.Phrase = phrase2;
							}
							else
							{
								this.c.AddElement(phrase2);
							}
						}
						else
						{
							this.c.Phrase.Add(phrase2);
						}
					}
					else
					{
						if (!this.e)
						{
							text += "\r\n\r\n";
						}
						Chunk chunk = new Chunk(text, font);
						if (A_1.h() != null)
						{
							chunk = (Chunk)A_1.h()(A_0, chunk);
						}
						this.b.n().Add(chunk);
					}
					xmlNode = xmlNode.NextSibling;
				}
				else
				{
					xmlNode = this.b(xmlNode, new u(xmlNode, A_1));
				}
			}
		}

		// Token: 0x04000F3F RID: 3903
		private new ITextElementArray b;

		// Token: 0x04000F40 RID: 3904
		private PdfPCell c;

		// Token: 0x04000F41 RID: 3905
		private Hashtable d;

		// Token: 0x04000F42 RID: 3906
		private bool e;
	}
}
