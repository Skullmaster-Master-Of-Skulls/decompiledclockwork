using System;
using System.Collections;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Interface;

namespace Spire.Doc.Collections
{
	// Token: 0x02000358 RID: 856
	public class StyleCollection : DocumentSerializableCollection, IStyleCollection
	{
		// Token: 0x17000287 RID: 647
		public IStyle this[int index]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (IStyle)base.InnerList[index];
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06002DFE RID: 11774 RVA: 0x002BDDA8 File Offset: 0x002BCDA8
		// (set) Token: 0x06002DFF RID: 11775 RVA: 0x002BDDEC File Offset: 0x002BCDEC
		internal bool FixedIndex13HasStyle
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜀ = value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06002E00 RID: 11776 RVA: 0x002BDE30 File Offset: 0x002BCE30
		// (set) Token: 0x06002E01 RID: 11777 RVA: 0x002BDE74 File Offset: 0x002BCE74
		internal bool FixedIndex14HasStyle
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜁ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜁ = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06002E02 RID: 11778 RVA: 0x002BDEB8 File Offset: 0x002BCEB8
		// (set) Token: 0x06002E03 RID: 11779 RVA: 0x002BDEFC File Offset: 0x002BCEFC
		internal string FixedIndex13StyleName
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜂ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜂ = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x002BDF40 File Offset: 0x002BCF40
		// (set) Token: 0x06002E05 RID: 11781 RVA: 0x002BDF84 File Offset: 0x002BCF84
		internal string FixedIndex14StyleName
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜃ;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜃ = value;
			}
		}

		// Token: 0x06002E06 RID: 11782 RVA: 0x002BDFC8 File Offset: 0x002BCFC8
		internal StyleCollection(Document A_0) : base(A_0, A_0)
		{
		}

		// Token: 0x06002E07 RID: 11783 RVA: 0x002BDFF4 File Offset: 0x002BCFF4
		public int Add(IStyle style)
		{
			int a_ = 9;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5C;
				case 2:
				{
					IL_73:
					if (style is spr\u173A)
					{
						num = 6;
						continue;
					}
					Style style2;
					(style as Style).ApplyBaseStyle(style2.Name);
					num = 11;
					continue;
				}
				case 3:
				{
					Style style2;
					(style as ParagraphStyle).ApplyBaseStyle(style2.Name);
					num = 7;
					continue;
				}
				case 4:
					goto IL_1D3;
				case 5:
					goto IL_88;
				case 6:
				{
					Style style2;
					(style as spr\u173A).ApplyBaseStyle(style2.Name);
					num = 4;
					continue;
				}
				case 7:
					goto IL_1D3;
				case 8:
					if (style is ParagraphStyle)
					{
						num = 3;
						continue;
					}
					num = 2;
					continue;
				case 9:
				{
					Style style3;
					if (style3 == null)
					{
						num = 12;
						continue;
					}
					goto IL_88;
				}
				case 10:
				{
					Style style2 = (style as Style).BaseStyle as Style;
					Style style3 = this.FindByName(style2.Name, style2.StyleType) as Style;
					if (true)
					{
					}
					num = 9;
					continue;
				}
				case 11:
					goto IL_1D3;
				case 12:
				{
					Style style2;
					this.Add(style2.Clone());
					num = 5;
					continue;
				}
				case 13:
					if ((style as Style).BaseStyle != null)
					{
						num = 10;
						continue;
					}
					goto IL_1D3;
				}
				if (style == null)
				{
					num = 0;
					continue;
				}
				DocumentSerializable documentSerializable = (DocumentSerializable)style;
				documentSerializable.CloneRelationsTo(base.Document, null);
				documentSerializable.ᜀ(base.Document);
				num = 13;
				continue;
				IL_1D3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_73;
				default:
					goto IL_1E9;
				}
				IL_88:
				num = 8;
			}
			IL_5C:
			throw new ArgumentNullException(ClipboardData.b("ᱮհੲᥴቶ", a_));
			IL_1E9:
			if (false)
			{
			}
			return base.InnerList.Add(style);
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x002BE1FC File Offset: 0x002BD1FC
		public Style FindByName(string name)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return StyleCollection.ᜀ(base.InnerList, name) as Style;
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x002BE248 File Offset: 0x002BD248
		public IStyle FindByName(string name, StyleType styleType)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return StyleCollection.ᜀ(base.InnerList, name, styleType);
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x002BE290 File Offset: 0x002BD290
		public IStyle FindById(int styleId)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return StyleCollection.ᜀ(base.InnerList, styleId);
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x002BE2D8 File Offset: 0x002BD2D8
		internal override void CloneToImpl(CollectionEx coll)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_31:
					int num;
					StyleCollection styleCollection;
					int num2;
					int count;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_B2:
						num = 3;
						break;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						styleCollection = (coll as StyleCollection);
						num2 = 0;
						count = base.InnerList.Count;
						num = 1;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (num2 >= count)
							{
								num = 2;
								continue;
							}
							goto IL_8F;
						case 1:
							goto IL_77;
						case 2:
							return;
						case 3:
							goto IL_77;
						}
						goto IL_31;
						IL_77:
						num = 0;
					}
					IL_8F:
					IStyle style = base.InnerList[num2] as IStyle;
					styleCollection.Add(style.Clone());
					num2++;
					goto IL_B2;
				}
				return;
			}
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x002BE3A8 File Offset: 0x002BD3A8
		internal static IStyle ᜀ(IList A_0, string A_1)
		{
			IStyle result;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				for (;;)
				{
					result = null;
					int num = 0;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 5;
							continue;
						case 1:
							goto IL_A4;
						case 2:
						{
							if (num >= A_0.Count)
							{
								num2 = 3;
								continue;
							}
							IStyle style = A_0[num] as IStyle;
							num2 = 6;
							continue;
						}
						case 3:
							goto IL_C3;
						case 4:
							goto IL_60;
						case 5:
						{
							IStyle style;
							if (style.Name == A_1)
							{
								num2 = 8;
								continue;
							}
							goto IL_60;
						}
						case 6:
						{
							IStyle style;
							if (style != null)
							{
								num2 = 0;
								continue;
							}
							goto IL_60;
						}
						case 7:
							goto IL_A4;
						case 8:
						{
							IStyle style;
							result = style;
							if (true)
							{
							}
							num2 = 4;
							continue;
						}
						}
						break;
						IL_60:
						num++;
						num2 = 7;
						continue;
						IL_A4:
						num2 = 2;
					}
				}
				IL_C3:
				break;
			}
			return result;
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x002BE4A4 File Offset: 0x002BD4A4
		internal static IStyle ᜀ(IList A_0, string A_1, StyleType A_2)
		{
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_70:
				num++;
				num2 = 10;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				goto IL_62;
			}
			IStyle result;
			for (;;)
			{
				IL_30:
				switch (num2)
				{
				case 0:
					return result;
				case 1:
				{
					if (num >= A_0.Count)
					{
						num2 = 0;
						continue;
					}
					Style style = A_0[num] as Style;
					num2 = 7;
					continue;
				}
				case 2:
					goto IL_118;
				case 3:
				{
					Style style;
					if (style.StyleType == A_2)
					{
						num2 = 8;
						continue;
					}
					goto IL_70;
				}
				case 4:
					num2 = 3;
					continue;
				case 5:
					goto IL_BE;
				case 6:
					num2 = 9;
					continue;
				case 7:
				{
					Style style;
					if (style != null)
					{
						num2 = 6;
						continue;
					}
					goto IL_70;
				}
				case 8:
				{
					Style style;
					result = style;
					num2 = 2;
					continue;
				}
				case 9:
				{
					Style style;
					if (style.Name == A_1)
					{
						num2 = 4;
						continue;
					}
					goto IL_70;
				}
				case 10:
					goto IL_BE;
				}
				goto IL_62;
				IL_BE:
				num2 = 1;
			}
			return result;
			IL_118:
			goto IL_70;
			IL_62:
			result = null;
			num = 0;
			num2 = 5;
			goto IL_30;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x002BE5D0 File Offset: 0x002BD5D0
		internal static IStyle ᜀ(IList A_0, int A_1)
		{
			IStyle result;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				for (;;)
				{
					result = null;
					int num = 0;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							Style style;
							if (style != null)
							{
								num2 = 5;
								continue;
							}
							goto IL_60;
						}
						case 1:
						{
							Style style;
							if (style.StyleId == A_1)
							{
								num2 = 3;
								continue;
							}
							goto IL_60;
						}
						case 2:
							goto IL_9C;
						case 3:
						{
							Style style;
							result = style;
							num2 = 7;
							continue;
						}
						case 4:
							goto IL_B8;
						case 5:
							num2 = 1;
							continue;
						case 6:
							goto IL_9C;
						case 7:
							goto IL_60;
						case 8:
						{
							if (num >= A_0.Count)
							{
								num2 = 4;
								continue;
							}
							Style style = A_0[num] as Style;
							num2 = 0;
							continue;
						}
						}
						break;
						IL_60:
						num++;
						num2 = 6;
						continue;
						IL_9C:
						num2 = 8;
					}
				}
				IL_B8:
				if (true)
				{
				}
				break;
			}
			return result;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x002BE6C4 File Offset: 0x002BD6C4
		protected override OwnerHolder CreateItem(IXDLSContentReader reader)
		{
			int a_ = 19;
			for (;;)
			{
				IL_21:
				int num;
				string attributeValue;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_67:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					attributeValue = reader.GetAttributeValue(ClipboardData.b("൸ɺർ᩾", a_));
					num = 2;
					break;
				}
				string a;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B3;
					case 1:
						num = 3;
						continue;
					case 2:
						goto IL_62;
					case 3:
						if (a == ClipboardData.b("㩸፺ᱼൾﮈ\ud88a歷﶐", a_))
						{
							num = 0;
							continue;
						}
						goto IL_B5;
					}
					goto IL_21;
				}
				IL_62:
				if ((a = attributeValue) != null)
				{
					goto IL_67;
				}
				goto IL_B5;
			}
			IL_B3:
			return new sprᯉ(base.Document);
			IL_B5:
			return new ParagraphStyle(base.Document);
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x002BE794 File Offset: 0x002BD794
		protected override string GetTagItemName()
		{
			int a_ = 1;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return ClipboardData.b("ᑦᵨቪŬ੮", a_);
		}

		// Token: 0x0400269D RID: 9885
		internal new bool ᜀ;

		// Token: 0x0400269E RID: 9886
		internal bool ᜁ;

		// Token: 0x0400269F RID: 9887
		internal string ᜂ = string.Empty;

		// Token: 0x040026A0 RID: 9888
		private byte[] \u2609\u00A9\u0080\u008E;

		// Token: 0x040026A1 RID: 9889
		private int \u25D8\u009E\u00A1\u0082;

		// Token: 0x040026A2 RID: 9890
		private string \u2460\u008B\u008D\u00AE;

		// Token: 0x040026A3 RID: 9891
		private string \u25D8\u0088\u0099\u0088;

		// Token: 0x040026A4 RID: 9892
		private bool[] \u2609\u00A7\u009C\u0085;

		// Token: 0x040026A5 RID: 9893
		private bool[] \u25D8\u0080\u009F\u0095;

		// Token: 0x040026A6 RID: 9894
		internal string ᜃ = string.Empty;
	}
}
