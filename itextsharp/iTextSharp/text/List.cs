using System;
using System.Collections.Generic;
using iTextSharp.text.factories;

namespace iTextSharp.text
{
	// Token: 0x02000064 RID: 100
	public class List : ITextElementArray, IElement
	{
		// Token: 0x06000315 RID: 789 RVA: 0x0001078C File Offset: 0x0000F78C
		public List() : this(false, false)
		{
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00010798 File Offset: 0x0000F798
		public List(float symbolIndent)
		{
			this.list = new List<IElement>();
			this.first = 1;
			this.symbol = new Chunk("-");
			this.preSymbol = "";
			this.postSymbol = ". ";
			base..ctor();
			this.symbolIndent = symbolIndent;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000107EA File Offset: 0x0000F7EA
		public List(bool numbered) : this(numbered, false)
		{
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000107F4 File Offset: 0x0000F7F4
		public List(bool numbered, bool lettered)
		{
			this.list = new List<IElement>();
			this.first = 1;
			this.symbol = new Chunk("-");
			this.preSymbol = "";
			this.postSymbol = ". ";
			base..ctor();
			this.numbered = numbered;
			this.lettered = lettered;
			this.autoindent = true;
			this.alignindent = true;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001085B File Offset: 0x0000F85B
		public List(bool numbered, float symbolIndent) : this(numbered, false, symbolIndent)
		{
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00010868 File Offset: 0x0000F868
		public List(bool numbered, bool lettered, float symbolIndent)
		{
			this.list = new List<IElement>();
			this.first = 1;
			this.symbol = new Chunk("-");
			this.preSymbol = "";
			this.postSymbol = ". ";
			base..ctor();
			this.numbered = numbered;
			this.lettered = lettered;
			this.symbolIndent = symbolIndent;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000108C8 File Offset: 0x0000F8C8
		public bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				foreach (IElement element in this.list)
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00010934 File Offset: 0x0000F934
		public int Type
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00010938 File Offset: 0x0000F938
		public List<Chunk> Chunks
		{
			get
			{
				List<Chunk> list = new List<Chunk>();
				foreach (IElement element in this.list)
				{
					list.AddRange(element.Chunks);
				}
				return list;
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00010998 File Offset: 0x0000F998
		public virtual bool Add(string s)
		{
			return s != null && this.Add(new ListItem(s));
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000109AC File Offset: 0x0000F9AC
		public virtual bool Add(IElement o)
		{
			if (o is ListItem)
			{
				ListItem listItem = (ListItem)o;
				if (this.numbered || this.lettered)
				{
					Chunk chunk = new Chunk(this.preSymbol, this.symbol.Font);
					int index = this.first + this.list.Count;
					if (this.lettered)
					{
						chunk.Append(RomanAlphabetFactory.GetString(index, this.lowercase));
					}
					else
					{
						chunk.Append(index.ToString());
					}
					chunk.Append(this.postSymbol);
					listItem.ListSymbol = chunk;
				}
				else
				{
					listItem.ListSymbol = this.symbol;
				}
				listItem.SetIndentationLeft(this.symbolIndent, this.autoindent);
				listItem.IndentationRight = 0f;
				this.list.Add(listItem);
				return true;
			}
			if (o is List)
			{
				List list = (List)o;
				list.IndentationLeft += this.symbolIndent;
				this.first--;
				this.list.Add(list);
				return true;
			}
			return false;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00010ABC File Offset: 0x0000FABC
		public void NormalizeIndentation()
		{
			float val = 0f;
			foreach (IElement element in this.list)
			{
				if (element is ListItem)
				{
					val = Math.Max(val, ((ListItem)element).IndentationLeft);
				}
			}
			foreach (IElement element2 in this.list)
			{
				if (element2 is ListItem)
				{
					((ListItem)element2).IndentationLeft = val;
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00010B81 File Offset: 0x0000FB81
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00010B78 File Offset: 0x0000FB78
		public bool Numbered
		{
			get
			{
				return this.numbered;
			}
			set
			{
				this.numbered = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00010B92 File Offset: 0x0000FB92
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00010B89 File Offset: 0x0000FB89
		public bool Lettered
		{
			get
			{
				return this.lettered;
			}
			set
			{
				this.lettered = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00010BA3 File Offset: 0x0000FBA3
		// (set) Token: 0x06000325 RID: 805 RVA: 0x00010B9A File Offset: 0x0000FB9A
		public bool Lowercase
		{
			get
			{
				return this.lowercase;
			}
			set
			{
				this.lowercase = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00010BB4 File Offset: 0x0000FBB4
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00010BAB File Offset: 0x0000FBAB
		public bool IsLowercase
		{
			get
			{
				return this.lowercase;
			}
			set
			{
				this.lowercase = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00010BC5 File Offset: 0x0000FBC5
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00010BBC File Offset: 0x0000FBBC
		public bool Autoindent
		{
			get
			{
				return this.autoindent;
			}
			set
			{
				this.autoindent = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00010BD6 File Offset: 0x0000FBD6
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00010BCD File Offset: 0x0000FBCD
		public bool Alignindent
		{
			get
			{
				return this.alignindent;
			}
			set
			{
				this.alignindent = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00010BDE File Offset: 0x0000FBDE
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00010BE6 File Offset: 0x0000FBE6
		public int First
		{
			get
			{
				return this.first;
			}
			set
			{
				this.first = value;
			}
		}

		// Token: 0x17000093 RID: 147
		// (set) Token: 0x0600032F RID: 815 RVA: 0x00010BEF File Offset: 0x0000FBEF
		public Chunk ListSymbol
		{
			set
			{
				this.symbol = value;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x00010BF8 File Offset: 0x0000FBF8
		public void SetListSymbol(string symbol)
		{
			this.symbol = new Chunk(symbol);
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00010C06 File Offset: 0x0000FC06
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00010C0E File Offset: 0x0000FC0E
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00010C17 File Offset: 0x0000FC17
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00010C1F File Offset: 0x0000FC1F
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

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00010C31 File Offset: 0x0000FC31
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00010C28 File Offset: 0x0000FC28
		public float SymbolIndent
		{
			get
			{
				return this.symbolIndent;
			}
			set
			{
				this.symbolIndent = value;
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010C39 File Offset: 0x0000FC39
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00010C3C File Offset: 0x0000FC3C
		public bool IsNestable()
		{
			return true;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00010C3F File Offset: 0x0000FC3F
		public List<IElement> Items
		{
			get
			{
				return this.list;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00010C47 File Offset: 0x0000FC47
		public int Size
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00010C54 File Offset: 0x0000FC54
		public virtual bool IsEmpty()
		{
			return this.list.Count == 0;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00010C64 File Offset: 0x0000FC64
		public float TotalLeading
		{
			get
			{
				if (this.list.Count < 1)
				{
					return -1f;
				}
				ListItem listItem = (ListItem)this.list[0];
				return listItem.TotalLeading;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00010C9D File Offset: 0x0000FC9D
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00010CA5 File Offset: 0x0000FCA5
		public Chunk Symbol
		{
			get
			{
				return this.symbol;
			}
			set
			{
				this.symbol = value;
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00010CAE File Offset: 0x0000FCAE
		public string getPostSymbol()
		{
			return this.postSymbol;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00010CBF File Offset: 0x0000FCBF
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00010CB6 File Offset: 0x0000FCB6
		public string PostSymbol
		{
			get
			{
				return this.postSymbol;
			}
			set
			{
				this.postSymbol = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00010CD0 File Offset: 0x0000FCD0
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00010CC7 File Offset: 0x0000FCC7
		public string PreSymbol
		{
			get
			{
				return this.preSymbol;
			}
			set
			{
				this.preSymbol = value;
			}
		}

		// Token: 0x040001A9 RID: 425
		public const bool ORDERED = true;

		// Token: 0x040001AA RID: 426
		public const bool UNORDERED = false;

		// Token: 0x040001AB RID: 427
		public const bool NUMERICAL = false;

		// Token: 0x040001AC RID: 428
		public const bool ALPHABETICAL = true;

		// Token: 0x040001AD RID: 429
		public const bool UPPERCASE = false;

		// Token: 0x040001AE RID: 430
		public const bool LOWERCASE = true;

		// Token: 0x040001AF RID: 431
		protected List<IElement> list;

		// Token: 0x040001B0 RID: 432
		protected bool numbered;

		// Token: 0x040001B1 RID: 433
		protected bool lettered;

		// Token: 0x040001B2 RID: 434
		protected bool lowercase;

		// Token: 0x040001B3 RID: 435
		protected bool autoindent;

		// Token: 0x040001B4 RID: 436
		protected bool alignindent;

		// Token: 0x040001B5 RID: 437
		protected int first;

		// Token: 0x040001B6 RID: 438
		protected Chunk symbol;

		// Token: 0x040001B7 RID: 439
		protected string preSymbol;

		// Token: 0x040001B8 RID: 440
		protected string postSymbol;

		// Token: 0x040001B9 RID: 441
		protected float indentationLeft;

		// Token: 0x040001BA RID: 442
		protected float indentationRight;

		// Token: 0x040001BB RID: 443
		protected float symbolIndent;
	}
}
