using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x02000212 RID: 530
	public class PdfOutline : PdfDictionary
	{
		// Token: 0x0600148F RID: 5263 RVA: 0x0007501C File Offset: 0x0007401C
		internal PdfOutline(PdfWriter writer) : base(PdfDictionary.OUTLINES)
		{
			this.open = true;
			this.parent = null;
			this.writer = writer;
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x00075049 File Offset: 0x00074049
		public PdfOutline(PdfOutline parent, PdfAction action, string title) : this(parent, action, title, true)
		{
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x00075055 File Offset: 0x00074055
		public PdfOutline(PdfOutline parent, PdfAction action, string title, bool open)
		{
			this.action = action;
			this.InitOutline(parent, title, open);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x00075079 File Offset: 0x00074079
		public PdfOutline(PdfOutline parent, PdfDestination destination, string title) : this(parent, destination, title, true)
		{
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x00075085 File Offset: 0x00074085
		public PdfOutline(PdfOutline parent, PdfDestination destination, string title, bool open)
		{
			this.destination = destination;
			this.InitOutline(parent, title, open);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x000750A9 File Offset: 0x000740A9
		public PdfOutline(PdfOutline parent, PdfAction action, PdfString title) : this(parent, action, title, true)
		{
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x000750B5 File Offset: 0x000740B5
		public PdfOutline(PdfOutline parent, PdfAction action, PdfString title, bool open) : this(parent, action, title.ToString(), open)
		{
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x000750C7 File Offset: 0x000740C7
		public PdfOutline(PdfOutline parent, PdfDestination destination, PdfString title) : this(parent, destination, title, true)
		{
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x000750D3 File Offset: 0x000740D3
		public PdfOutline(PdfOutline parent, PdfDestination destination, PdfString title, bool open) : this(parent, destination, title.ToString(), true)
		{
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000750E4 File Offset: 0x000740E4
		public PdfOutline(PdfOutline parent, PdfAction action, Paragraph title) : this(parent, action, title, true)
		{
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x000750F0 File Offset: 0x000740F0
		public PdfOutline(PdfOutline parent, PdfAction action, Paragraph title, bool open)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Chunk chunk in title.Chunks)
			{
				stringBuilder.Append(chunk.Content);
			}
			this.action = action;
			this.InitOutline(parent, stringBuilder.ToString(), open);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00075178 File Offset: 0x00074178
		public PdfOutline(PdfOutline parent, PdfDestination destination, Paragraph title) : this(parent, destination, title, true)
		{
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x00075184 File Offset: 0x00074184
		public PdfOutline(PdfOutline parent, PdfDestination destination, Paragraph title, bool open)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Chunk chunk in title.Chunks)
			{
				stringBuilder.Append(chunk.Content);
			}
			this.destination = destination;
			this.InitOutline(parent, stringBuilder.ToString(), open);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0007520C File Offset: 0x0007420C
		internal void InitOutline(PdfOutline parent, string title, bool open)
		{
			this.open = open;
			this.parent = parent;
			this.writer = parent.writer;
			base.Put(PdfName.TITLE, new PdfString(title, "UnicodeBig"));
			parent.AddKid(this);
			if (this.destination != null && !this.destination.HasPage())
			{
				this.SetDestinationPage(this.writer.CurrentPage);
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00075277 File Offset: 0x00074277
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x0007527F File Offset: 0x0007427F
		public PdfIndirectReference IndirectReference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00075288 File Offset: 0x00074288
		public PdfOutline Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x00075290 File Offset: 0x00074290
		public bool SetDestinationPage(PdfIndirectReference pageReference)
		{
			return this.destination != null && this.destination.AddPage(pageReference);
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x000752A8 File Offset: 0x000742A8
		public PdfDestination PdfDestination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x000752B0 File Offset: 0x000742B0
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x000752B8 File Offset: 0x000742B8
		internal int Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x000752C1 File Offset: 0x000742C1
		public int Level
		{
			get
			{
				if (this.parent == null)
				{
					return 0;
				}
				return this.parent.Level + 1;
			}
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000752DC File Offset: 0x000742DC
		public override void ToPdf(PdfWriter writer, Stream os)
		{
			if (this.color != null && !this.color.Equals(BaseColor.BLACK))
			{
				base.Put(PdfName.C, new PdfArray(new float[]
				{
					(float)this.color.R / 255f,
					(float)this.color.G / 255f,
					(float)this.color.B / 255f
				}));
			}
			int num = 0;
			if ((this.style & 1) != 0)
			{
				num |= 2;
			}
			if ((this.style & 2) != 0)
			{
				num |= 1;
			}
			if (num != 0)
			{
				base.Put(PdfName.F, new PdfNumber(num));
			}
			if (this.parent != null)
			{
				base.Put(PdfName.PARENT, this.parent.IndirectReference);
			}
			if (this.destination != null && this.destination.HasPage())
			{
				base.Put(PdfName.DEST, this.destination);
			}
			if (this.action != null)
			{
				base.Put(PdfName.A, this.action);
			}
			if (this.count != 0)
			{
				base.Put(PdfName.COUNT, new PdfNumber(this.count));
			}
			base.ToPdf(writer, os);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0007540F File Offset: 0x0007440F
		public void AddKid(PdfOutline outline)
		{
			this.kids.Add(outline);
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0007541D File Offset: 0x0007441D
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x00075425 File Offset: 0x00074425
		public List<PdfOutline> Kids
		{
			get
			{
				return this.kids;
			}
			set
			{
				this.kids = value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0007542E File Offset: 0x0007442E
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x00075436 File Offset: 0x00074436
		public string Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x00075440 File Offset: 0x00074440
		// (set) Token: 0x060014AC RID: 5292 RVA: 0x00075464 File Offset: 0x00074464
		public string Title
		{
			get
			{
				PdfString pdfString = (PdfString)base.Get(PdfName.TITLE);
				return pdfString.ToString();
			}
			set
			{
				base.Put(PdfName.TITLE, new PdfString(value, "UnicodeBig"));
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x00075485 File Offset: 0x00074485
		// (set) Token: 0x060014AD RID: 5293 RVA: 0x0007547C File Offset: 0x0007447C
		public bool Open
		{
			get
			{
				return this.open;
			}
			set
			{
				this.open = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0007548D File Offset: 0x0007448D
		// (set) Token: 0x060014B0 RID: 5296 RVA: 0x00075495 File Offset: 0x00074495
		public BaseColor Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0007549E File Offset: 0x0007449E
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x000754A6 File Offset: 0x000744A6
		public int Style
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

		// Token: 0x04000E0F RID: 3599
		private PdfIndirectReference reference;

		// Token: 0x04000E10 RID: 3600
		private int count;

		// Token: 0x04000E11 RID: 3601
		private PdfOutline parent;

		// Token: 0x04000E12 RID: 3602
		private PdfDestination destination;

		// Token: 0x04000E13 RID: 3603
		private PdfAction action;

		// Token: 0x04000E14 RID: 3604
		protected List<PdfOutline> kids = new List<PdfOutline>();

		// Token: 0x04000E15 RID: 3605
		protected PdfWriter writer;

		// Token: 0x04000E16 RID: 3606
		private string tag;

		// Token: 0x04000E17 RID: 3607
		private bool open;

		// Token: 0x04000E18 RID: 3608
		private BaseColor color;

		// Token: 0x04000E19 RID: 3609
		private int style;
	}
}
