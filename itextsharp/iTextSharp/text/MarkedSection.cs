using System;
using System.Collections.Generic;

namespace iTextSharp.text
{
	// Token: 0x020003BE RID: 958
	public class MarkedSection : MarkedObject
	{
		// Token: 0x0600214D RID: 8525 RVA: 0x000C9889 File Offset: 0x000C8889
		public MarkedSection(Section section)
		{
			if (section.Title != null)
			{
				this.title = new MarkedObject(section.Title);
				section.Title = null;
			}
			this.element = section;
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x000C98B8 File Offset: 0x000C88B8
		public void Add(int index, IElement o)
		{
			((Section)this.element).Add(index, o);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000C98CC File Offset: 0x000C88CC
		public bool Add(IElement o)
		{
			return ((Section)this.element).Add(o);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x000C98E0 File Offset: 0x000C88E0
		public override bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				foreach (IElement element in ((Section)this.element))
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

		// Token: 0x06002151 RID: 8529 RVA: 0x000C9950 File Offset: 0x000C8950
		public bool AddAll<T>(ICollection<T> collection) where T : IElement
		{
			return ((Section)this.element).AddAll<T>(collection);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000C9964 File Offset: 0x000C8964
		public MarkedSection AddSection(float indentation, int numberDepth)
		{
			MarkedSection markedSection = ((Section)this.element).AddMarkedSection();
			markedSection.Indentation = indentation;
			markedSection.NumberDepth = numberDepth;
			return markedSection;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000C9994 File Offset: 0x000C8994
		public MarkedSection AddSection(float indentation)
		{
			MarkedSection markedSection = ((Section)this.element).AddMarkedSection();
			markedSection.Indentation = indentation;
			return markedSection;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x000C99BC File Offset: 0x000C89BC
		public MarkedSection AddSection(int numberDepth)
		{
			MarkedSection markedSection = ((Section)this.element).AddMarkedSection();
			markedSection.NumberDepth = numberDepth;
			return markedSection;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x000C99E2 File Offset: 0x000C89E2
		public MarkedSection AddSection()
		{
			return ((Section)this.element).AddMarkedSection();
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x000C9A0C File Offset: 0x000C8A0C
		// (set) Token: 0x06002156 RID: 8534 RVA: 0x000C99F4 File Offset: 0x000C89F4
		public MarkedObject Title
		{
			get
			{
				Paragraph element = Section.ConstructTitle((Paragraph)this.title.element, ((Section)this.element).numbers, ((Section)this.element).NumberDepth, ((Section)this.element).NumberStyle);
				return new MarkedObject(element)
				{
					markupAttributes = this.title.MarkupAttributes
				};
			}
			set
			{
				if (value.element is Paragraph)
				{
					this.title = value;
				}
			}
		}

		// Token: 0x170005BB RID: 1467
		// (set) Token: 0x06002158 RID: 8536 RVA: 0x000C9A78 File Offset: 0x000C8A78
		public int NumberDepth
		{
			set
			{
				((Section)this.element).NumberDepth = value;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (set) Token: 0x06002159 RID: 8537 RVA: 0x000C9A8B File Offset: 0x000C8A8B
		public float IndentationLeft
		{
			set
			{
				((Section)this.element).IndentationLeft = value;
			}
		}

		// Token: 0x170005BD RID: 1469
		// (set) Token: 0x0600215A RID: 8538 RVA: 0x000C9A9E File Offset: 0x000C8A9E
		public float IndentationRight
		{
			set
			{
				((Section)this.element).IndentationRight = value;
			}
		}

		// Token: 0x170005BE RID: 1470
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x000C9AB1 File Offset: 0x000C8AB1
		public float Indentation
		{
			set
			{
				((Section)this.element).Indentation = value;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (set) Token: 0x0600215C RID: 8540 RVA: 0x000C9AC4 File Offset: 0x000C8AC4
		public bool BookmarkOpen
		{
			set
			{
				((Section)this.element).BookmarkOpen = value;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (set) Token: 0x0600215D RID: 8541 RVA: 0x000C9AD7 File Offset: 0x000C8AD7
		public bool TriggerNewPage
		{
			set
			{
				((Section)this.element).TriggerNewPage = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (set) Token: 0x0600215E RID: 8542 RVA: 0x000C9AEA File Offset: 0x000C8AEA
		public string BookmarkTitle
		{
			set
			{
				((Section)this.element).BookmarkTitle = value;
			}
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000C9AFD File Offset: 0x000C8AFD
		public void NewPage()
		{
			((Section)this.element).NewPage();
		}

		// Token: 0x040016F3 RID: 5875
		protected MarkedObject title;
	}
}
