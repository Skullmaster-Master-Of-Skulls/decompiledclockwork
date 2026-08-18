using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security;
using System.Text;
using System.Xml;
using HtmlAgilityPack;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MailBee;
using MailBee.Pdf;

namespace a.c
{
	// Token: 0x02000232 RID: 562
	internal class s
	{
		// Token: 0x060012D5 RID: 4821 RVA: 0x00053CDC File Offset: 0x00052CDC
		public s(bool A_0, Encoding A_1, string A_2, string A_3)
		{
			this.t = A_0;
			this.u = ((A_1 != null) ? A_1 : Global.DefaultEncoding);
			this.v = A_2;
			this.w = A_3;
			if (this.w != null)
			{
				FontFactory.RegisterDirectory(this.w);
			}
			this.a = null;
			this.b = null;
			this.d = null;
			this.e = null;
			this.i = new ArrayList();
			this.b(FontFactory.GetFont("Helvetica", this.h, 0));
			this.j = new l();
			this.m = string.Empty;
			this.n = false;
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00053D9B File Offset: 0x00052D9B
		public s() : this(true, Encoding.UTF8, null, null)
		{
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00053DAB File Offset: 0x00052DAB
		public XmlDocument d()
		{
			return this.a;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00053DB3 File Offset: 0x00052DB3
		public Document n()
		{
			return this.b;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00053DBB File Offset: 0x00052DBB
		public void a(Rectangle A_0)
		{
			this.c = A_0;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00053DC4 File Offset: 0x00052DC4
		public Rectangle m()
		{
			return this.c;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00053DCC File Offset: 0x00052DCC
		public PdfWriter g()
		{
			return this.d;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00053DD4 File Offset: 0x00052DD4
		public void a(ConvertXmlNodeToPdfDelegate A_0)
		{
			this.o = A_0;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00053DDD File Offset: 0x00052DDD
		public ConvertXmlNodeToPdfDelegate h()
		{
			return this.o;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00053DE5 File Offset: 0x00052DE5
		public void a(ProcessImagePathDelegate A_0)
		{
			this.p = A_0;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00053DEE File Offset: 0x00052DEE
		public ProcessImagePathDelegate r()
		{
			return this.p;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00053DF6 File Offset: 0x00052DF6
		public void a(bool A_0)
		{
			this.q = A_0;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00053DFF File Offset: 0x00052DFF
		public bool f()
		{
			return this.q;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00053E07 File Offset: 0x00052E07
		public void a(PdfSourceType A_0)
		{
			this.r = A_0;
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00053E10 File Offset: 0x00052E10
		public PdfSourceType q()
		{
			return this.r;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x00053E18 File Offset: 0x00052E18
		public void a(Font A_0)
		{
			this.s = A_0;
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x00053E21 File Offset: 0x00052E21
		public Font c()
		{
			return this.s;
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x00053E29 File Offset: 0x00052E29
		public void a(float A_0)
		{
			this.h = A_0;
			this.o().Size = this.h;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00053E43 File Offset: 0x00052E43
		public float b()
		{
			return this.h;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00053E4B File Offset: 0x00052E4B
		public string k()
		{
			return this.e;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00053E53 File Offset: 0x00052E53
		public void c(string A_0)
		{
			this.e = A_0;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00053E5C File Offset: 0x00052E5C
		public Font o()
		{
			return this.g;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00053E64 File Offset: 0x00052E64
		public void b(Font A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.g = A_0;
			this.f = new Hashtable();
			this.a("p", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 0);
			this.a("li", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 0);
			this.a("pre", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 0);
			this.a("verbatim", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 0);
			this.a("b", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 1);
			this.a("strong", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 1);
			this.a("tt", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 0);
			this.a("em", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 2);
			this.a("i", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 2);
			this.a("u", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 4);
			this.a("code", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 0);
			this.a("a", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size, 4);
			this.a("chapter", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size + 10f, 1);
			this.a("h1", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size + 6f, 1);
			this.a("h2", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size + 4f, 1);
			this.a("h3", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size + 4f, 1);
			this.a("h4", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size + 2f, 1);
			this.a("h5", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size + 2f, 2);
			this.a("h6", this.g.Familyname, this.g.BaseFont.Encoding, this.g.Size + 2f, 0);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00054281 File Offset: 0x00053281
		internal n l()
		{
			return this.j;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00054289 File Offset: 0x00053289
		internal void a(n A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("textProcessor");
			}
			this.j = A_0;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000542A0 File Offset: 0x000532A0
		public string i()
		{
			return this.k;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000542A8 File Offset: 0x000532A8
		public void b(string A_0)
		{
			this.k = A_0;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000542B1 File Offset: 0x000532B1
		public string j()
		{
			return this.l;
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000542B9 File Offset: 0x000532B9
		public void d(string A_0)
		{
			this.l = A_0;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000542C2 File Offset: 0x000532C2
		public string e()
		{
			return this.m;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000542CA File Offset: 0x000532CA
		public void e(string A_0)
		{
			if (A_0 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			this.m = A_0;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000542DE File Offset: 0x000532DE
		public bool p()
		{
			return this.n;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000542E6 File Offset: 0x000532E6
		public void b(bool A_0)
		{
			this.n = A_0;
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000542EF File Offset: 0x000532EF
		public Font a(string A_0)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("cssClass");
			}
			if (this.f.Contains(A_0))
			{
				return this.f[A_0] as Font;
			}
			return null;
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00054320 File Offset: 0x00053320
		public Font a(string A_0, string A_1, float A_2, int A_3)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("cssClass");
			}
			if (this.f.Contains(A_0))
			{
				throw new ArgumentException("cssClass already present");
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException("fontName");
			}
			Font font = FontFactory.GetFont(A_1, A_2, A_3);
			this.f[A_0] = font;
			return font;
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0005437C File Offset: 0x0005337C
		public Font a(string A_0, string A_1, string A_2, float A_3, int A_4)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("cssClass");
			}
			if (this.f.Contains(A_0))
			{
				throw new ArgumentException("cssClass already present");
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException("fontName");
			}
			Font font = FontFactory.GetFont(A_1, A_2, A_3, A_4);
			this.f[A_0] = font;
			return font;
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000543D8 File Offset: 0x000533D8
		public Font a(string A_0, string A_1, float A_2, int A_3, Color A_4)
		{
			if (A_0 == null)
			{
				throw new ArgumentNullException("cssClass");
			}
			if (this.f.Contains(A_0))
			{
				throw new ArgumentException("cssClass already present");
			}
			if (A_1 == null)
			{
				throw new ArgumentNullException("fontName");
			}
			Font font = FontFactory.GetFont(A_1, A_2, A_3, A_4);
			this.f[A_0] = font;
			return font;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00054434 File Offset: 0x00053434
		private void a()
		{
			this.b.Close();
			this.a = null;
			this.i = null;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00054450 File Offset: 0x00053450
		public void a(Uri A_0)
		{
			WebResponse webResponse = null;
			try
			{
				webResponse = WebRequest.Create(A_0).GetResponse();
				new StreamReader(webResponse.GetResponseStream());
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				if (webResponse != null)
				{
					webResponse.Close();
				}
			}
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x000544A4 File Offset: 0x000534A4
		public void b(string A_0, Stream A_1)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (!A_1.CanWrite)
			{
				throw new MailBeeStreamException(41);
			}
			string text = this.v;
			if (text == null)
			{
				text = ((this.u != null && this.u != Encoding.UTF8 && this.u != Encoding.UTF7) ? this.u.WebName : Global.DefaultEncoding.WebName);
			}
			Font font = FontFactory.GetFont("Arial", text, this.h, 0);
			if (font.BaseFont != null)
			{
				this.b(font);
			}
			this.a = new XmlDocument();
			string xml = A_0;
			if (this.r == PdfSourceType.Text)
			{
				StringBuilder stringBuilder = new StringBuilder("<?xml version=\"1.0\" encoding=\"utf-8\"?><span><html><body><pre>");
				stringBuilder.Append(au.b(A_0));
				stringBuilder.Append("</pre></body></html></span>");
				xml = stringBuilder.ToString();
			}
			if (this.r == PdfSourceType.Html)
			{
				HtmlDocument htmlDocument = new HtmlDocument();
				htmlDocument.OptionFixNestedTags = true;
				htmlDocument.LoadHtml(A_0);
				IEnumerable<HtmlNode> enumerable = htmlDocument.DocumentNode.Descendants(0);
				int num = 0;
				foreach (HtmlNode htmlNode in enumerable)
				{
					num++;
					if (htmlNode.NodeType == HtmlNodeType.Element && htmlNode.Name.Length > 0 && !char.IsLetter(htmlNode.Name[0]))
					{
						htmlNode.Name = "X" + htmlNode.Name;
					}
				}
				htmlDocument.OptionOutputAsXml = true;
				StringWriter stringWriter = new StringWriter();
				htmlDocument.Save(stringWriter);
				xml = stringWriter.ToString();
			}
			try
			{
				this.a.LoadXml(xml);
			}
			catch (XmlException a_)
			{
				throw new MailBeeIOException(33, a_);
			}
			x x = new x(this.a);
			this.a = x.b();
			k k = new k(this, null, true);
			Color color = Color.WHITE;
			XmlNode xmlNode = this.a.GetElementsByTagName("body").Item(0);
			if (xmlNode == null)
			{
				xmlNode = this.a.DocumentElement.ParentNode;
			}
			else
			{
				XmlNode xmlNode2 = xmlNode.Attributes["bgcolor"];
				if (xmlNode2 != null)
				{
					color = global::a.c.j.a(xmlNode2.Value, color);
				}
			}
			this.b = new Document(new Rectangle(this.c)
			{
				BackgroundColor = color
			});
			this.b.SetMargins(20f, 20f, 30f, 30f);
			this.i = new ArrayList();
			this.d = PdfWriter.GetInstance(this.b, A_1);
			this.d.CloseStream = false;
			this.b.Open();
			u u = new u(xmlNode);
			u.c(color);
			u.d(color);
			u.a(this.s);
			u.b((int)this.g.Size);
			u.a(this.o);
			u.a(this.p);
			u.a(this.q);
			k.a(xmlNode, u);
			this.a();
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x000547F0 File Offset: 0x000537F0
		public void b(string A_0, string A_1)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_0 == string.Empty || A_1 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			Stream stream = null;
			try
			{
				stream = new BinaryWriter(new FileStream(new FileInfo(A_1).FullName, FileMode.Create)).BaseStream;
			}
			catch (IOException a_)
			{
				throw new MailBeeIOException(30, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (ArgumentException a_3)
			{
				throw new MailBeeIOException(20, a_3);
			}
			try
			{
				this.b(A_0, stream);
			}
			finally
			{
				stream.Close();
			}
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000548B4 File Offset: 0x000538B4
		public void a(Stream A_0, Stream A_1)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!A_0.CanRead)
			{
				throw new MailBeeStreamException(40);
			}
			if (!A_1.CanWrite)
			{
				throw new MailBeeStreamException(41);
			}
			BinaryReader binaryReader = new BinaryReader(A_0);
			byte[] array;
			try
			{
				array = new byte[A_0.Length];
				binaryReader.Read(array, 0, array.Length);
			}
			catch (IOException a_)
			{
				throw new MailBeeIOException(30, a_);
			}
			this.b(this.a(array), A_1);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0005493C File Offset: 0x0005393C
		public void a(Stream A_0, string A_1)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_1 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (!A_0.CanRead)
			{
				throw new MailBeeStreamException(40);
			}
			BinaryReader binaryReader = new BinaryReader(A_0);
			byte[] array;
			try
			{
				array = new byte[A_0.Length];
				binaryReader.Read(array, 0, array.Length);
			}
			catch (IOException a_)
			{
				throw new MailBeeIOException(30, a_);
			}
			finally
			{
				binaryReader.Close();
			}
			this.b(this.a(array), A_1);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x000549DC File Offset: 0x000539DC
		public void a(Uri A_0, Stream A_1)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (!A_1.CanWrite)
			{
				throw new MailBeeStreamException(41);
			}
			this.m = A_0.Scheme + "://" + A_0.Host;
			this.n = true;
			try
			{
				byte[] a_ = new WebClient().DownloadData(A_0);
				this.b(this.a(a_), A_1);
			}
			catch (IOException a_2)
			{
				throw new MailBeeIOException(30, a_2);
			}
			catch (SecurityException a_3)
			{
				throw new MailBeeWebException(32, a_3);
			}
			catch (WebException a_4)
			{
				throw new MailBeeWebException(34, a_4);
			}
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00054A94 File Offset: 0x00053A94
		public void a(Uri A_0, string A_1)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_1 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			this.m = A_0.Scheme + "://" + A_0.Host;
			this.n = true;
			try
			{
				byte[] a_ = new WebClient().DownloadData(A_0);
				this.b(this.a(a_), A_1);
			}
			catch (IOException a_2)
			{
				throw new MailBeeIOException(30, a_2);
			}
			catch (SecurityException a_3)
			{
				throw new MailBeeWebException(32, a_3);
			}
			catch (WebException a_4)
			{
				throw new MailBeeWebException(34, a_4);
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00054B50 File Offset: 0x00053B50
		public void a(string A_0, string A_1)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_0 == string.Empty || A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			this.m = Path.GetDirectoryName(A_0);
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read);
			}
			catch (ArgumentException a_)
			{
				throw new MailBeeIOException(20, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (IOException a_3)
			{
				throw new MailBeeIOException(30, a_3);
			}
			try
			{
				this.a(fileStream, A_1);
			}
			finally
			{
				fileStream.Close();
			}
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00054C0C File Offset: 0x00053C0C
		public void a(string A_0, Stream A_1)
		{
			if (A_0 == null || A_1 == null)
			{
				throw new MailBeeInvalidArgumentException(21);
			}
			if (A_0 == string.Empty)
			{
				throw new MailBeeInvalidArgumentException(22);
			}
			if (!A_1.CanWrite)
			{
				throw new MailBeeStreamException(41);
			}
			this.m = Path.GetDirectoryName(A_0);
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(A_0, FileMode.Open, FileAccess.Read);
			}
			catch (ArgumentException a_)
			{
				throw new MailBeeIOException(20, a_);
			}
			catch (NotSupportedException a_2)
			{
				throw new MailBeeIOException(20, a_2);
			}
			catch (IOException a_3)
			{
				throw new MailBeeIOException(30, a_3);
			}
			try
			{
				this.a(fileStream, A_1);
			}
			finally
			{
				fileStream.Close();
			}
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00054CCC File Offset: 0x00053CCC
		private string a(byte[] A_0)
		{
			string @string;
			if (this.t)
			{
				@string = this.u.GetString(A_0);
				Encoding encoding = bb.b(@string);
				if (encoding != null && this.u != encoding)
				{
					this.u = encoding;
					@string = this.u.GetString(A_0);
				}
			}
			else
			{
				@string = this.u.GetString(A_0);
			}
			return @string;
		}

		// Token: 0x04000F5A RID: 3930
		private XmlDocument a;

		// Token: 0x04000F5B RID: 3931
		private Document b;

		// Token: 0x04000F5C RID: 3932
		private Rectangle c = PageSize.A4;

		// Token: 0x04000F5D RID: 3933
		private PdfWriter d;

		// Token: 0x04000F5E RID: 3934
		private string e;

		// Token: 0x04000F5F RID: 3935
		private Hashtable f;

		// Token: 0x04000F60 RID: 3936
		private Font g;

		// Token: 0x04000F61 RID: 3937
		private float h = 8f;

		// Token: 0x04000F62 RID: 3938
		private ArrayList i;

		// Token: 0x04000F63 RID: 3939
		private n j;

		// Token: 0x04000F64 RID: 3940
		private string k;

		// Token: 0x04000F65 RID: 3941
		private string l;

		// Token: 0x04000F66 RID: 3942
		private string m;

		// Token: 0x04000F67 RID: 3943
		private bool n;

		// Token: 0x04000F68 RID: 3944
		private ConvertXmlNodeToPdfDelegate o;

		// Token: 0x04000F69 RID: 3945
		private ProcessImagePathDelegate p;

		// Token: 0x04000F6A RID: 3946
		private bool q;

		// Token: 0x04000F6B RID: 3947
		private PdfSourceType r;

		// Token: 0x04000F6C RID: 3948
		private Font s;

		// Token: 0x04000F6D RID: 3949
		private bool t;

		// Token: 0x04000F6E RID: 3950
		private Encoding u;

		// Token: 0x04000F6F RID: 3951
		private string v;

		// Token: 0x04000F70 RID: 3952
		private string w;
	}
}
