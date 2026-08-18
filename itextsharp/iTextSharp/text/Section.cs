using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.util;
using iTextSharp.text.error_messages;

namespace iTextSharp.text
{
	// Token: 0x020000BF RID: 191
	public class Section : List<IElement>, ITextElementArray, ILargeElement, IElement
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x0001E765 File Offset: 0x0001D765
		protected internal Section()
		{
			this.title = new Paragraph();
			this.numberDepth = 1;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001E794 File Offset: 0x0001D794
		protected internal Section(Paragraph title, int numberDepth)
		{
			this.numberDepth = numberDepth;
			this.title = title;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001E7BF File Offset: 0x0001D7BF
		private void SetNumbers(int number, List<int> numbers)
		{
			this.numbers = new List<int>();
			this.numbers.Add(number);
			this.numbers.AddRange(numbers);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001E7E4 File Offset: 0x0001D7E4
		public bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				foreach (IElement element in this)
				{
					listener.Add(element);
				}
				result = true;
			}
			catch (DocumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0001E84C File Offset: 0x0001D84C
		public virtual int Type
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0001E850 File Offset: 0x0001D850
		public List<Chunk> Chunks
		{
			get
			{
				List<Chunk> list = new List<Chunk>();
				foreach (IElement element in this)
				{
					list.AddRange(element.Chunks);
				}
				return list;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001E8AC File Offset: 0x0001D8AC
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001E8AF File Offset: 0x0001D8AF
		public virtual bool IsNestable()
		{
			return false;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001E8B4 File Offset: 0x0001D8B4
		public void Add(int index, IElement element)
		{
			if (this.AddedCompletely)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("this.largeelement.has.already.been.added.to.the.document"));
			}
			try
			{
				if (!element.IsNestable())
				{
					throw new Exception(element.Type.ToString());
				}
				base.Insert(index, element);
			}
			catch (Exception ex)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("insertion.of.illegal.element.1", ex.Message));
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001E928 File Offset: 0x0001D928
		public new bool Add(IElement element)
		{
			bool result;
			try
			{
				if (element.Type == 13)
				{
					Section section = (Section)element;
					section.SetNumbers(++this.subsections, this.numbers);
					base.Add(section);
					result = true;
				}
				else if (element is MarkedSection && ((MarkedObject)element).element.Type == 13)
				{
					MarkedSection markedSection = (MarkedSection)element;
					Section section2 = (Section)markedSection.element;
					section2.SetNumbers(++this.subsections, this.numbers);
					base.Add(markedSection);
					result = true;
				}
				else
				{
					if (!element.IsNestable())
					{
						throw new InvalidCastException(MessageLocalization.GetComposedMessage("you.can.t.add.a.1.to.a.section", element.Type.ToString()));
					}
					base.Add(element);
					result = true;
				}
			}
			catch (InvalidCastException ex)
			{
				throw new InvalidCastException(MessageLocalization.GetComposedMessage("insertion.of.illegal.element.1", ex.Message));
			}
			return result;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001EA2C File Offset: 0x0001DA2C
		public bool AddAll<T>(ICollection<T> collection) where T : IElement
		{
			foreach (T t in collection)
			{
				IElement element = t;
				this.Add(element);
			}
			return true;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001EA7C File Offset: 0x0001DA7C
		public virtual Section AddSection(float indentation, Paragraph title, int numberDepth)
		{
			if (this.AddedCompletely)
			{
				throw new InvalidOperationException(MessageLocalization.GetComposedMessage("this.largeelement.has.already.been.added.to.the.document"));
			}
			Section section = new Section(title, numberDepth);
			section.Indentation = indentation;
			this.Add(section);
			return section;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001EAB9 File Offset: 0x0001DAB9
		public virtual Section AddSection(float indentation, Paragraph title)
		{
			return this.AddSection(indentation, title, this.numberDepth + 1);
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0001EACB File Offset: 0x0001DACB
		public virtual Section AddSection(Paragraph title, int numberDepth)
		{
			return this.AddSection(0f, title, numberDepth);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001EADC File Offset: 0x0001DADC
		public MarkedSection AddMarkedSection()
		{
			MarkedSection markedSection = new MarkedSection(new Section(null, this.numberDepth + 1));
			this.Add(markedSection);
			return markedSection;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001EB06 File Offset: 0x0001DB06
		public virtual Section AddSection(Paragraph title)
		{
			return this.AddSection(0f, title, this.numberDepth + 1);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001EB1C File Offset: 0x0001DB1C
		public virtual Section AddSection(float indentation, string title, int numberDepth)
		{
			return this.AddSection(indentation, new Paragraph(title), numberDepth);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001EB2C File Offset: 0x0001DB2C
		public virtual Section AddSection(string title, int numberDepth)
		{
			return this.AddSection(new Paragraph(title), numberDepth);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001EB3B File Offset: 0x0001DB3B
		public virtual Section AddSection(float indentation, string title)
		{
			return this.AddSection(indentation, new Paragraph(title));
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001EB4A File Offset: 0x0001DB4A
		public virtual Section AddSection(string title)
		{
			return this.AddSection(new Paragraph(title));
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001EB58 File Offset: 0x0001DB58
		public void Set(Properties attributes)
		{
			string s;
			if ((s = attributes.Remove("numberdepth")) != null)
			{
				this.NumberDepth = int.Parse(s);
			}
			if ((s = attributes.Remove("indent")) != null)
			{
				this.Indentation = float.Parse(s, NumberFormatInfo.InvariantInfo);
			}
			if ((s = attributes.Remove("indentationleft")) != null)
			{
				this.IndentationLeft = float.Parse(s, NumberFormatInfo.InvariantInfo);
			}
			if ((s = attributes.Remove("indentationright")) != null)
			{
				this.IndentationRight = float.Parse(s, NumberFormatInfo.InvariantInfo);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001EBE0 File Offset: 0x0001DBE0
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x0001EBFF File Offset: 0x0001DBFF
		public Paragraph Title
		{
			get
			{
				return Section.ConstructTitle(this.title, this.numbers, this.numberDepth, this.numberStyle);
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0001EC11 File Offset: 0x0001DC11
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x0001EC08 File Offset: 0x0001DC08
		public int NumberStyle
		{
			get
			{
				return this.numberStyle;
			}
			set
			{
				this.numberStyle = value;
			}
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001EC1C File Offset: 0x0001DC1C
		public static Paragraph ConstructTitle(Paragraph title, List<int> numbers, int numberDepth, int numberStyle)
		{
			if (title == null)
			{
				return null;
			}
			int num = Math.Min(numbers.Count, numberDepth);
			if (num < 1)
			{
				return title;
			}
			StringBuilder stringBuilder = new StringBuilder(" ");
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Insert(0, ".");
				stringBuilder.Insert(0, numbers[i]);
			}
			if (numberStyle == 1)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 1);
			}
			Paragraph paragraph = new Paragraph(title);
			paragraph.Insert(0, new Chunk(stringBuilder.ToString(), title.Font));
			return paragraph;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001ECA8 File Offset: 0x0001DCA8
		public bool IsChapter()
		{
			return this.Type == 16;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001ECB4 File Offset: 0x0001DCB4
		public bool IsSection()
		{
			return this.Type == 13;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0001ECC0 File Offset: 0x0001DCC0
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0001ECC8 File Offset: 0x0001DCC8
		public int NumberDepth
		{
			get
			{
				return this.numberDepth;
			}
			set
			{
				this.numberDepth = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0001ECD1 File Offset: 0x0001DCD1
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0001ECD9 File Offset: 0x0001DCD9
		public float IndentationLeft
		{
			get
			{
				return this.indentationLeft;
			}
			set
			{
				this.indentationLeft = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0001ECE2 File Offset: 0x0001DCE2
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0001ECEA File Offset: 0x0001DCEA
		public float IndentationRight
		{
			get
			{
				return this.indentationRight;
			}
			set
			{
				this.indentationRight = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001ECF3 File Offset: 0x0001DCF3
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001ECFB File Offset: 0x0001DCFB
		public float Indentation
		{
			get
			{
				return this.indentation;
			}
			set
			{
				this.indentation = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001ED04 File Offset: 0x0001DD04
		public int Depth
		{
			get
			{
				return this.numbers.Count;
			}
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001ED11 File Offset: 0x0001DD11
		public static bool IsTitle(string tag)
		{
			return "title".Equals(tag);
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001ED1E File Offset: 0x0001DD1E
		public static bool IsTag(string tag)
		{
			return "section".Equals(tag);
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0001ED2B File Offset: 0x0001DD2B
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0001ED33 File Offset: 0x0001DD33
		public bool BookmarkOpen
		{
			get
			{
				return this.bookmarkOpen;
			}
			set
			{
				this.bookmarkOpen = value;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001ED3C File Offset: 0x0001DD3C
		public Paragraph GetBookmarkTitle()
		{
			if (this.bookmarkTitle == null)
			{
				return this.Title;
			}
			return new Paragraph(this.bookmarkTitle);
		}

		// Token: 0x1700011C RID: 284
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0001ED58 File Offset: 0x0001DD58
		public string BookmarkTitle
		{
			set
			{
				this.bookmarkTitle = value;
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001ED61 File Offset: 0x0001DD61
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0001ED69 File Offset: 0x0001DD69
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0001ED7B File Offset: 0x0001DD7B
		public virtual bool TriggerNewPage
		{
			get
			{
				return this.triggerNewPage && this.notAddedYet;
			}
			set
			{
				this.triggerNewPage = value;
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001ED84 File Offset: 0x0001DD84
		public void SetChapterNumber(int number)
		{
			this.numbers[this.numbers.Count - 1] = number;
			foreach (IElement element in this)
			{
				if (element is Section)
				{
					((Section)element).SetChapterNumber(number);
				}
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x0001EDF8 File Offset: 0x0001DDF8
		// (set) Token: 0x0600061F RID: 1567 RVA: 0x0001EE00 File Offset: 0x0001DE00
		public bool NotAddedYet
		{
			get
			{
				return this.notAddedYet;
			}
			set
			{
				this.notAddedYet = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001EE09 File Offset: 0x0001DE09
		// (set) Token: 0x06000621 RID: 1569 RVA: 0x0001EE11 File Offset: 0x0001DE11
		protected bool AddedCompletely
		{
			get
			{
				return this.addedCompletely;
			}
			set
			{
				this.addedCompletely = value;
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001EE1C File Offset: 0x0001DE1C
		public void FlushContent()
		{
			this.NotAddedYet = false;
			this.title = null;
			for (int i = 0; i < base.Count; i++)
			{
				IElement element = base[i];
				if (element is Section)
				{
					Section section = (Section)element;
					if (!section.ElementComplete && base.Count == 1)
					{
						section.FlushContent();
						return;
					}
					section.AddedCompletely = true;
				}
				base.RemoveAt(i);
				i--;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0001EE89 File Offset: 0x0001DE89
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0001EE91 File Offset: 0x0001DE91
		public bool ElementComplete
		{
			get
			{
				return this.complete;
			}
			set
			{
				this.complete = value;
			}
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001EE9A File Offset: 0x0001DE9A
		public void NewPage()
		{
			this.Add(Chunk.NEXTPAGE);
		}

		// Token: 0x040002D6 RID: 726
		public const int NUMBERSTYLE_DOTTED = 0;

		// Token: 0x040002D7 RID: 727
		public const int NUMBERSTYLE_DOTTED_WITHOUT_FINAL_DOT = 1;

		// Token: 0x040002D8 RID: 728
		protected Paragraph title;

		// Token: 0x040002D9 RID: 729
		protected int numberDepth;

		// Token: 0x040002DA RID: 730
		protected int numberStyle;

		// Token: 0x040002DB RID: 731
		protected float indentationLeft;

		// Token: 0x040002DC RID: 732
		protected float indentationRight;

		// Token: 0x040002DD RID: 733
		protected float indentation;

		// Token: 0x040002DE RID: 734
		protected int subsections;

		// Token: 0x040002DF RID: 735
		protected internal List<int> numbers;

		// Token: 0x040002E0 RID: 736
		protected bool complete = true;

		// Token: 0x040002E1 RID: 737
		protected bool addedCompletely;

		// Token: 0x040002E2 RID: 738
		protected bool notAddedYet = true;

		// Token: 0x040002E3 RID: 739
		protected bool bookmarkOpen = true;

		// Token: 0x040002E4 RID: 740
		protected bool triggerNewPage;

		// Token: 0x040002E5 RID: 741
		protected string bookmarkTitle;
	}
}
