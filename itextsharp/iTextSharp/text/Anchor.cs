using System;
using System.Collections.Generic;

namespace iTextSharp.text
{
	// Token: 0x02000538 RID: 1336
	public class Anchor : Phrase
	{
		// Token: 0x06002DF7 RID: 11767 RVA: 0x0011C134 File Offset: 0x0011B134
		public Anchor() : base(16f)
		{
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x0011C141 File Offset: 0x0011B141
		public Anchor(float leading) : base(leading)
		{
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x0011C14A File Offset: 0x0011B14A
		public Anchor(Chunk chunk) : base(chunk)
		{
		}

		// Token: 0x06002DFA RID: 11770 RVA: 0x0011C153 File Offset: 0x0011B153
		public Anchor(string str) : base(str)
		{
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x0011C15C File Offset: 0x0011B15C
		public Anchor(string str, Font font) : base(str, font)
		{
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0011C166 File Offset: 0x0011B166
		public Anchor(float leading, Chunk chunk) : base(leading, chunk)
		{
		}

		// Token: 0x06002DFD RID: 11773 RVA: 0x0011C170 File Offset: 0x0011B170
		public Anchor(float leading, string str) : base(leading, str)
		{
		}

		// Token: 0x06002DFE RID: 11774 RVA: 0x0011C17A File Offset: 0x0011B17A
		public Anchor(float leading, string str, Font font) : base(leading, str, font)
		{
		}

		// Token: 0x06002DFF RID: 11775 RVA: 0x0011C188 File Offset: 0x0011B188
		public Anchor(Phrase phrase) : base(phrase)
		{
			if (phrase is Anchor)
			{
				Anchor anchor = (Anchor)phrase;
				this.Name = anchor.name;
				this.Reference = anchor.reference;
			}
		}

		// Token: 0x06002E00 RID: 11776 RVA: 0x0011C1C4 File Offset: 0x0011B1C4
		public override bool Process(IElementListener listener)
		{
			bool result;
			try
			{
				bool flag = this.reference != null && this.reference.StartsWith("#");
				bool flag2 = true;
				foreach (Chunk chunk in this.Chunks)
				{
					if (this.name != null && flag2 && !chunk.IsEmpty())
					{
						chunk.SetLocalDestination(this.name);
						flag2 = false;
					}
					if (flag)
					{
						chunk.SetLocalGoto(this.reference.Substring(1));
					}
					else if (this.reference != null)
					{
						chunk.SetAnchor(this.reference);
					}
					listener.Add(chunk);
				}
				result = true;
			}
			catch (DocumentException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x0011C29C File Offset: 0x0011B29C
		public override List<Chunk> Chunks
		{
			get
			{
				List<Chunk> list = new List<Chunk>();
				bool flag = this.reference != null && this.reference.StartsWith("#");
				bool flag2 = true;
				foreach (IElement element in this)
				{
					Chunk chunk = (Chunk)element;
					if (this.name != null && flag2 && !chunk.IsEmpty())
					{
						chunk.SetLocalDestination(this.name);
						flag2 = false;
					}
					if (flag)
					{
						chunk.SetLocalGoto(this.reference.Substring(1));
					}
					else if (this.reference != null)
					{
						chunk.SetAnchor(this.reference);
					}
					list.Add(chunk);
				}
				return list;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x0011C364 File Offset: 0x0011B364
		public override int Type
		{
			get
			{
				return 17;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002E03 RID: 11779 RVA: 0x0011C368 File Offset: 0x0011B368
		// (set) Token: 0x06002E04 RID: 11780 RVA: 0x0011C370 File Offset: 0x0011B370
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x0011C379 File Offset: 0x0011B379
		// (set) Token: 0x06002E06 RID: 11782 RVA: 0x0011C381 File Offset: 0x0011B381
		public string Reference
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

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002E07 RID: 11783 RVA: 0x0011C38C File Offset: 0x0011B38C
		public Uri Url
		{
			get
			{
				Uri result;
				try
				{
					result = new Uri(this.reference);
				}
				catch
				{
					result = null;
				}
				return result;
			}
		}

		// Token: 0x04001FC5 RID: 8133
		protected string name;

		// Token: 0x04001FC6 RID: 8134
		protected string reference;
	}
}
