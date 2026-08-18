using System;
using System.Collections.Generic;
using iTextSharp.text.error_messages;

namespace iTextSharp.text
{
	// Token: 0x02000527 RID: 1319
	public class Document : IDocListener, IElementListener
	{
		// Token: 0x06002CE8 RID: 11496 RVA: 0x00112130 File Offset: 0x00111130
		public Document() : this(iTextSharp.text.PageSize.A4)
		{
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x0011213D File Offset: 0x0011113D
		public Document(Rectangle pageSize) : this(pageSize, 36f, 36f, 36f, 36f)
		{
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x0011215A File Offset: 0x0011115A
		public Document(Rectangle pageSize, float marginLeft, float marginRight, float marginTop, float marginBottom)
		{
			this.pageSize = pageSize;
			this.marginLeft = marginLeft;
			this.marginRight = marginRight;
			this.marginTop = marginTop;
			this.marginBottom = marginBottom;
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x00112192 File Offset: 0x00111192
		public void AddDocListener(IDocListener listener)
		{
			this.listeners.Add(listener);
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x001121A0 File Offset: 0x001111A0
		public void RemoveIDocListener(IDocListener listener)
		{
			this.listeners.Remove(listener);
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x001121B0 File Offset: 0x001111B0
		public virtual bool Add(IElement element)
		{
			if (this.close)
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("the.document.has.been.closed.you.can.t.add.any.elements"));
			}
			if (!this.open && element.IsContent())
			{
				throw new DocumentException(MessageLocalization.GetComposedMessage("the.document.is.not.open.yet.you.can.only.add.meta.information"));
			}
			bool flag = false;
			if (element is ChapterAutoNumber)
			{
				this.chapternumber = ((ChapterAutoNumber)element).SetAutomaticNumber(this.chapternumber);
			}
			foreach (IDocListener docListener in this.listeners)
			{
				flag |= docListener.Add(element);
			}
			if (element is ILargeElement)
			{
				ILargeElement largeElement = (ILargeElement)element;
				if (!largeElement.ElementComplete)
				{
					largeElement.FlushContent();
				}
			}
			return flag;
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x00112280 File Offset: 0x00111280
		public virtual void Open()
		{
			if (!this.close)
			{
				this.open = true;
			}
			foreach (IDocListener docListener in this.listeners)
			{
				docListener.SetPageSize(this.pageSize);
				docListener.SetMargins(this.marginLeft, this.marginRight, this.marginTop, this.marginBottom);
				docListener.Open();
			}
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x00112310 File Offset: 0x00111310
		public virtual void OpenDocument()
		{
			this.Open();
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x00112318 File Offset: 0x00111318
		public virtual bool SetPageSize(Rectangle pageSize)
		{
			this.pageSize = pageSize;
			foreach (IDocListener docListener in this.listeners)
			{
				docListener.SetPageSize(pageSize);
			}
			return true;
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x00112374 File Offset: 0x00111374
		public virtual bool SetMargins(float marginLeft, float marginRight, float marginTop, float marginBottom)
		{
			this.marginLeft = marginLeft;
			this.marginRight = marginRight;
			this.marginTop = marginTop;
			this.marginBottom = marginBottom;
			foreach (IDocListener docListener in this.listeners)
			{
				docListener.SetMargins(marginLeft, marginRight, marginTop, marginBottom);
			}
			return true;
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x001123EC File Offset: 0x001113EC
		public virtual bool NewPage()
		{
			if (!this.open || this.close)
			{
				return false;
			}
			foreach (IDocListener docListener in this.listeners)
			{
				docListener.NewPage();
			}
			return true;
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x00112454 File Offset: 0x00111454
		public virtual void ResetPageCount()
		{
			this.pageN = 0;
			foreach (IDocListener docListener in this.listeners)
			{
				docListener.ResetPageCount();
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (set) Token: 0x06002CF4 RID: 11508 RVA: 0x001124B0 File Offset: 0x001114B0
		public virtual int PageCount
		{
			set
			{
				this.pageN = value;
				foreach (IDocListener docListener in this.listeners)
				{
					docListener.PageCount = value;
				}
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x0011250C File Offset: 0x0011150C
		public int PageNumber
		{
			get
			{
				return this.pageN;
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x00112514 File Offset: 0x00111514
		public virtual void Close()
		{
			if (!this.close)
			{
				this.open = false;
				this.close = true;
			}
			foreach (IDocListener docListener in this.listeners)
			{
				docListener.Close();
			}
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x0011257C File Offset: 0x0011157C
		public virtual void CloseDocument()
		{
			this.Close();
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x00112584 File Offset: 0x00111584
		public bool AddHeader(string name, string content)
		{
			return this.Add(new Header(name, content));
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x00112593 File Offset: 0x00111593
		public bool AddTitle(string title)
		{
			return this.Add(new Meta(1, title));
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x001125A2 File Offset: 0x001115A2
		public bool AddSubject(string subject)
		{
			return this.Add(new Meta(2, subject));
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x001125B1 File Offset: 0x001115B1
		public bool AddKeywords(string keywords)
		{
			return this.Add(new Meta(3, keywords));
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x001125C0 File Offset: 0x001115C0
		public bool AddAuthor(string author)
		{
			return this.Add(new Meta(4, author));
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x001125CF File Offset: 0x001115CF
		public bool AddCreator(string creator)
		{
			return this.Add(new Meta(7, creator));
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x001125DE File Offset: 0x001115DE
		public bool AddProducer()
		{
			return this.Add(new Meta(5, Document.Version));
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x001125F4 File Offset: 0x001115F4
		public bool AddCreationDate()
		{
			return this.Add(new Meta(6, DateTime.Now.ToString("ddd MMM dd HH:mm:ss zzz yyyy")));
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x0011261F File Offset: 0x0011161F
		public float LeftMargin
		{
			get
			{
				return this.marginLeft;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x00112627 File Offset: 0x00111627
		public float RightMargin
		{
			get
			{
				return this.marginRight;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x0011262F File Offset: 0x0011162F
		public float TopMargin
		{
			get
			{
				return this.marginTop;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x00112637 File Offset: 0x00111637
		public float BottomMargin
		{
			get
			{
				return this.marginBottom;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x0011263F File Offset: 0x0011163F
		public float Left
		{
			get
			{
				return this.pageSize.GetLeft(this.marginLeft);
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x00112652 File Offset: 0x00111652
		public float Right
		{
			get
			{
				return this.pageSize.GetRight(this.marginRight);
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x00112665 File Offset: 0x00111665
		public float Top
		{
			get
			{
				return this.pageSize.GetTop(this.marginTop);
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x00112678 File Offset: 0x00111678
		public float Bottom
		{
			get
			{
				return this.pageSize.GetBottom(this.marginBottom);
			}
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x0011268B File Offset: 0x0011168B
		public float GetLeft(float margin)
		{
			return this.pageSize.GetLeft(this.marginLeft + margin);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x001126A0 File Offset: 0x001116A0
		public float GetRight(float margin)
		{
			return this.pageSize.GetRight(this.marginRight + margin);
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x001126B5 File Offset: 0x001116B5
		public float GetTop(float margin)
		{
			return this.pageSize.GetTop(this.marginTop + margin);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x001126CA File Offset: 0x001116CA
		public float GetBottom(float margin)
		{
			return this.pageSize.GetBottom(this.marginBottom + margin);
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x001126DF File Offset: 0x001116DF
		public Rectangle PageSize
		{
			get
			{
				return this.pageSize;
			}
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x001126E7 File Offset: 0x001116E7
		public bool IsOpen()
		{
			return this.open;
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x001126EF File Offset: 0x001116EF
		public static string Product
		{
			get
			{
				return "iTextSharp";
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002D0F RID: 11535 RVA: 0x001126F6 File Offset: 0x001116F6
		public static string Release
		{
			get
			{
				return "5.0.2";
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x001126FD File Offset: 0x001116FD
		public static string Version
		{
			get
			{
				return "iTextSharp 5.0.2 (c) 1T3XT BVBA";
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002D11 RID: 11537 RVA: 0x00112704 File Offset: 0x00111704
		// (set) Token: 0x06002D12 RID: 11538 RVA: 0x0011270C File Offset: 0x0011170C
		public string JavaScript_onLoad
		{
			get
			{
				return this.javaScript_onLoad;
			}
			set
			{
				this.javaScript_onLoad = value;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x00112715 File Offset: 0x00111715
		// (set) Token: 0x06002D14 RID: 11540 RVA: 0x0011271D File Offset: 0x0011171D
		public string JavaScript_onUnLoad
		{
			get
			{
				return this.javaScript_onUnLoad;
			}
			set
			{
				this.javaScript_onUnLoad = value;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06002D15 RID: 11541 RVA: 0x00112726 File Offset: 0x00111726
		// (set) Token: 0x06002D16 RID: 11542 RVA: 0x0011272E File Offset: 0x0011172E
		public string HtmlStyleClass
		{
			get
			{
				return this.htmlStyleClass;
			}
			set
			{
				this.htmlStyleClass = value;
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x00112738 File Offset: 0x00111738
		public virtual bool SetMarginMirroring(bool marginMirroring)
		{
			this.marginMirroring = marginMirroring;
			foreach (IDocListener docListener in this.listeners)
			{
				docListener.SetMarginMirroring(marginMirroring);
			}
			return true;
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x00112794 File Offset: 0x00111794
		public virtual bool SetMarginMirroringTopBottom(bool marginMirroringTopBottom)
		{
			this.marginMirroringTopBottom = marginMirroringTopBottom;
			foreach (IDocListener docListener in this.listeners)
			{
				docListener.SetMarginMirroringTopBottom(marginMirroringTopBottom);
			}
			return true;
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x001127F0 File Offset: 0x001117F0
		public bool IsMarginMirroring()
		{
			return this.marginMirroring;
		}

		// Token: 0x04001F0A RID: 7946
		private const string ITEXT = "iTextSharp";

		// Token: 0x04001F0B RID: 7947
		private const string RELEASE = "5.0.2";

		// Token: 0x04001F0C RID: 7948
		private const string ITEXT_VERSION = "iTextSharp 5.0.2 (c) 1T3XT BVBA";

		// Token: 0x04001F0D RID: 7949
		public static bool Compress = true;

		// Token: 0x04001F0E RID: 7950
		public static float WmfFontCorrection = 0.86f;

		// Token: 0x04001F0F RID: 7951
		private List<IDocListener> listeners = new List<IDocListener>();

		// Token: 0x04001F10 RID: 7952
		protected bool open;

		// Token: 0x04001F11 RID: 7953
		protected bool close;

		// Token: 0x04001F12 RID: 7954
		protected Rectangle pageSize;

		// Token: 0x04001F13 RID: 7955
		protected float marginLeft;

		// Token: 0x04001F14 RID: 7956
		protected float marginRight;

		// Token: 0x04001F15 RID: 7957
		protected float marginTop;

		// Token: 0x04001F16 RID: 7958
		protected float marginBottom;

		// Token: 0x04001F17 RID: 7959
		protected bool marginMirroring;

		// Token: 0x04001F18 RID: 7960
		protected bool marginMirroringTopBottom;

		// Token: 0x04001F19 RID: 7961
		protected string javaScript_onLoad;

		// Token: 0x04001F1A RID: 7962
		protected string javaScript_onUnLoad;

		// Token: 0x04001F1B RID: 7963
		protected string htmlStyleClass;

		// Token: 0x04001F1C RID: 7964
		protected int pageN;

		// Token: 0x04001F1D RID: 7965
		protected int chapternumber;
	}
}
