using System;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text.error_messages;
using iTextSharp.text.pdf;

namespace iTextSharp.text
{
	// Token: 0x02000171 RID: 369
	public class Phrase : List<IElement>, ITextElementArray, IElement
	{
		// Token: 0x06000E17 RID: 3607 RVA: 0x000524C4 File Offset: 0x000514C4
		public Phrase() : this(16f)
		{
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x000524D1 File Offset: 0x000514D1
		public Phrase(Phrase phrase)
		{
			this.leading = float.NaN;
			base..ctor();
			this.AddAll<IElement>(phrase);
			this.leading = phrase.Leading;
			this.font = phrase.Font;
			this.hyphenation = phrase.hyphenation;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00052510 File Offset: 0x00051510
		public Phrase(float leading)
		{
			this.leading = float.NaN;
			base..ctor();
			this.leading = leading;
			this.font = new Font();
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00052535 File Offset: 0x00051535
		public Phrase(Chunk chunk)
		{
			this.leading = float.NaN;
			base..ctor();
			base.Add(chunk);
			this.font = chunk.Font;
			this.hyphenation = chunk.GetHyphenation();
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00052567 File Offset: 0x00051567
		public Phrase(float leading, Chunk chunk)
		{
			this.leading = float.NaN;
			base..ctor();
			this.leading = leading;
			base.Add(chunk);
			this.font = chunk.Font;
			this.hyphenation = chunk.GetHyphenation();
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x000525A0 File Offset: 0x000515A0
		public Phrase(string str) : this(float.NaN, str, new Font())
		{
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x000525B3 File Offset: 0x000515B3
		public Phrase(string str, Font font) : this(float.NaN, str, font)
		{
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x000525C2 File Offset: 0x000515C2
		public Phrase(float leading, string str) : this(leading, str, new Font())
		{
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x000525D1 File Offset: 0x000515D1
		public Phrase(float leading, string str, Font font)
		{
			this.leading = float.NaN;
			base..ctor();
			this.leading = leading;
			this.font = font;
			if (str != null && str.Length != 0)
			{
				base.Add(new Chunk(str, font));
			}
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0005260C File Offset: 0x0005160C
		public virtual bool Process(IElementListener listener)
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

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x00052674 File Offset: 0x00051674
		public virtual int Type
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00052678 File Offset: 0x00051678
		public virtual List<Chunk> Chunks
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

		// Token: 0x06000E23 RID: 3619 RVA: 0x000526D4 File Offset: 0x000516D4
		public bool IsContent()
		{
			return true;
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x000526D7 File Offset: 0x000516D7
		public bool IsNestable()
		{
			return true;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x000526DC File Offset: 0x000516DC
		public virtual void Add(int index, IElement element)
		{
			if (element == null)
			{
				return;
			}
			try
			{
				if (element.Type == 10)
				{
					Chunk chunk = (Chunk)element;
					if (!this.font.IsStandardFont())
					{
						chunk.Font = this.font.Difference(chunk.Font);
					}
					if (this.hyphenation != null && chunk.GetHyphenation() == null && !chunk.IsEmpty())
					{
						chunk.SetHyphenation(this.hyphenation);
					}
					base.Insert(index, chunk);
				}
				else
				{
					if (element.Type != 11 && element.Type != 17 && element.Type != 29 && element.Type != 55 && element.Type != 50)
					{
						throw new Exception(element.Type.ToString());
					}
					base.Insert(index, element);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("insertion.of.illegal.element.1", ex.Message));
			}
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x000527CC File Offset: 0x000517CC
		public bool Add(string s)
		{
			if (s == null)
			{
				return false;
			}
			base.Add(new Chunk(s, this.font));
			return true;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x000527E8 File Offset: 0x000517E8
		public new virtual bool Add(IElement element)
		{
			if (element == null)
			{
				return false;
			}
			try
			{
				int type = element.Type;
				if (type <= 23)
				{
					switch (type)
					{
					case 10:
						return this.AddChunk((Chunk)element);
					case 11:
					case 12:
					{
						Phrase phrase = (Phrase)element;
						bool flag = true;
						foreach (IElement element2 in phrase)
						{
							if (element2 is Chunk)
							{
								flag &= this.AddChunk((Chunk)element2);
							}
							else
							{
								flag &= this.Add(element2);
							}
						}
						return flag;
					}
					case 13:
					case 15:
					case 16:
						goto IL_DA;
					case 14:
					case 17:
						break;
					default:
						if (type != 23)
						{
							goto IL_DA;
						}
						break;
					}
				}
				else if (type != 29 && type != 50 && type != 55)
				{
					goto IL_DA;
				}
				base.Add(element);
				return true;
				IL_DA:
				throw new Exception(element.Type.ToString());
			}
			catch (Exception ex)
			{
				throw new Exception(MessageLocalization.GetComposedMessage("insertion.of.illegal.element.1", ex.Message));
			}
			bool result;
			return result;
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0005291C File Offset: 0x0005191C
		public bool AddAll<T>(ICollection<T> collection) where T : IElement
		{
			foreach (T t in collection)
			{
				IElement element = t;
				this.Add(element);
			}
			return true;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0005296C File Offset: 0x0005196C
		protected bool AddChunk(Chunk chunk)
		{
			Font font = chunk.Font;
			string content = chunk.Content;
			if (this.font != null && !this.font.IsStandardFont())
			{
				font = this.font.Difference(chunk.Font);
			}
			if (base.Count > 0 && !chunk.HasAttributes())
			{
				try
				{
					Chunk chunk2 = (Chunk)base[base.Count - 1];
					if (!chunk2.HasAttributes() && (font == null || font.CompareTo(chunk2.Font) == 0) && chunk2.Font.CompareTo(font) == 0 && !"".Equals(chunk2.Content.Trim()) && !"".Equals(content.Trim()))
					{
						chunk2.Append(content);
						return true;
					}
				}
				catch
				{
				}
			}
			Chunk chunk3 = new Chunk(content, font);
			chunk3.Attributes = chunk.Attributes;
			if (this.hyphenation != null && chunk3.GetHyphenation() == null && !chunk3.IsEmpty())
			{
				chunk3.SetHyphenation(this.hyphenation);
			}
			base.Add(chunk3);
			return true;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00052A8C File Offset: 0x00051A8C
		public void AddSpecial(IElement obj)
		{
			base.Add(obj);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x00052A98 File Offset: 0x00051A98
		public bool IsEmpty()
		{
			switch (base.Count)
			{
			case 0:
				return true;
			case 1:
			{
				IElement element = base[0];
				return element.Type == 10 && ((Chunk)element).IsEmpty();
			}
			default:
				return false;
			}
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00052AE2 File Offset: 0x00051AE2
		public bool HasLeading()
		{
			return !float.IsNaN(this.leading);
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x00052AF4 File Offset: 0x00051AF4
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x00052B22 File Offset: 0x00051B22
		public virtual float Leading
		{
			get
			{
				if (float.IsNaN(this.leading) && this.font != null)
				{
					return this.font.GetCalculatedLeading(1.5f);
				}
				return this.leading;
			}
			set
			{
				this.leading = value;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x00052B2B File Offset: 0x00051B2B
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x00052B33 File Offset: 0x00051B33
		public Font Font
		{
			get
			{
				return this.font;
			}
			set
			{
				this.font = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x00052B3C File Offset: 0x00051B3C
		public string Content
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Chunk chunk in this.Chunks)
				{
					stringBuilder.Append(chunk.ToString());
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00052BA4 File Offset: 0x00051BA4
		public static bool IsTag(string tag)
		{
			return "phrase".Equals(tag);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00052BB1 File Offset: 0x00051BB1
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x00052BC2 File Offset: 0x00051BC2
		// (set) Token: 0x06000E34 RID: 3636 RVA: 0x00052BB9 File Offset: 0x00051BB9
		public IHyphenationEvent Hyphenation
		{
			get
			{
				return this.hyphenation;
			}
			set
			{
				this.hyphenation = value;
			}
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00052BCA File Offset: 0x00051BCA
		private Phrase(bool dummy)
		{
			this.leading = float.NaN;
			base..ctor();
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00052BDD File Offset: 0x00051BDD
		public static Phrase GetInstance(string str)
		{
			return Phrase.GetInstance(16, str, new Font());
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00052BEC File Offset: 0x00051BEC
		public static Phrase GetInstance(int leading, string str)
		{
			return Phrase.GetInstance(leading, str, new Font());
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00052BFC File Offset: 0x00051BFC
		public static Phrase GetInstance(int leading, string str, Font font)
		{
			Phrase phrase = new Phrase(true);
			phrase.Leading = (float)leading;
			phrase.font = font;
			if (font.Family != Font.FontFamily.SYMBOL && font.Family != Font.FontFamily.ZAPFDINGBATS && font.BaseFont == null)
			{
				int num;
				while ((num = SpecialSymbol.Index(str)) > -1)
				{
					if (num > 0)
					{
						string content = str.Substring(0, num);
						phrase.Add(new Chunk(content, font));
						str = str.Substring(num);
					}
					Font font2 = new Font(Font.FontFamily.SYMBOL, font.Size, font.Style, font.Color);
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append(SpecialSymbol.GetCorrespondingSymbol(str[0]));
					str = str.Substring(1);
					while (SpecialSymbol.Index(str) == 0)
					{
						stringBuilder.Append(SpecialSymbol.GetCorrespondingSymbol(str[0]));
						str = str.Substring(1);
					}
					phrase.Add(new Chunk(stringBuilder.ToString(), font2));
				}
			}
			if (str != null && str.Length != 0)
			{
				phrase.Add(new Chunk(str, font));
			}
			return phrase;
		}

		// Token: 0x04000A69 RID: 2665
		protected float leading;

		// Token: 0x04000A6A RID: 2666
		protected Font font;

		// Token: 0x04000A6B RID: 2667
		protected IHyphenationEvent hyphenation;
	}
}
