using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x0200003B RID: 59
	public class XlsFontsCollection : CollectionExtended<XlsFont>
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x00024850 File Offset: 0x00023850
		internal XlsFontsCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x17000154 RID: 340
		public IFont this[int index]
		{
			get
			{
				int a_ = 16;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_8F;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8F;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							break;
						}
						break;
					case 2:
						goto IL_A5;
					case 3:
						num = 0;
						continue;
					}
					if (index >= 0)
					{
						num = 3;
						continue;
					}
					break;
					IL_8F:
					if (index < base.InnerList.Count)
					{
						goto IL_A7;
					}
					num = 2;
				}
				IL_65:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⽅♇⹉⥋㙍", a_), RecordTableEnumerator.b("ཅ♇⹉⥋㙍灏㭑❓癕㝗⽙⡛繝ཟѡ䑣ᑥ१ѩ୫୭幯", a_));
				IL_A5:
				goto IL_65;
				IL_A7:
				return base.InnerList[index];
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00024938 File Offset: 0x00023938
		internal new XlsFont ᜀ(spr\u2267 A_0)
		{
			int a_ = 16;
			if (A_0 != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					XlsFont font = base.AppImplementation.ᜀ(this, A_0);
					return (XlsFont)this.Add(font);
				}
				}
			}
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("㑅ⵇ⥉⍋㱍㑏", a_));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000249B0 File Offset: 0x000239B0
		protected internal IFont Add(IFont font)
		{
			int a_ = 15;
			XlsFont xlsFont;
			for (;;)
			{
				xlsFont = (font as XlsFont);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_99;
					case 1:
						if (xlsFont == null)
						{
							num = 2;
							continue;
						}
						num = 3;
						continue;
					case 2:
						goto IL_3B;
					case 3:
						if (!this.ᜁ.ContainsKey(xlsFont))
						{
							goto IL_AF;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3D;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_3B:
			throw new ArgumentException(RecordTableEnumerator.b("ф⍆ⵈ歊⭌⁎㽐❒畔ㅖ㡘㉚ㅜ㩞ՠ佢䕤⹦ݨᵪ౬ͮᡰᝲ啴Ͷx୺᡼彾ꞈ", a_));
			IL_3D:
			return this.ᜁ[xlsFont];
			IL_99:
			goto IL_3D;
			IL_AF:
			this.ForceAdd(xlsFont);
			return xlsFont;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00024A74 File Offset: 0x00023A74
		public void InsertDefaultFonts()
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
			spr\u2267 spr_u = (spr\u2267)spr\u175E.ᜀ(TBIFFRecord.Font);
			spr_u.ᜀ(base.AppImplementation.\u1715());
			spr_u.ᜀ((ushort)XlsFont.SizeInTwips(base.AppImplementation.\u171A()));
			this.ForceAdd(base.AppImplementation.ᜀ(this, spr_u));
			spr_u = (spr\u2267)spr_u.Clone();
			this.ForceAdd(base.AppImplementation.ᜀ(this, spr_u));
			spr_u = (spr\u2267)spr_u.Clone();
			this.ForceAdd(base.AppImplementation.ᜀ(this, spr_u));
			spr_u = (spr\u2267)spr_u.Clone();
			this.ForceAdd(base.AppImplementation.ᜀ(this, spr_u));
			base.InnerList.Add(base.InnerList[0]);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00024B6C File Offset: 0x00023B6C
		private new void ᜀ()
		{
			int a_ = 6;
			this.ᜀ = (base.FindParent(typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜀ != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2E;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return;
			}
			IL_2E:
			throw new ArgumentNullException(RecordTableEnumerator.b("砻栽ℿ⹁摃㑅ⵇ⥉⍋㱍㑏牑㝓㝕㙗㑙㍛⩝䁟aţ䙥๧թᥫmᑯ山", a_));
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00024BEC File Offset: 0x00023BEC
		protected internal void ForceAdd(XlsFont font)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_8A;
				case 2:
					goto IL_65;
				case 3:
					base.InnerList.Add(base.InnerList[0]);
					num = 2;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8A;
					}
					if (false)
					{
					}
					if (true)
					{
					}
					this.ᜁ.Add(font, font);
					num = 0;
					continue;
				}
				if (base.InnerList.Count == 4)
				{
					num = 3;
					continue;
				}
				IL_65:
				font.Index = base.InnerList.Count;
				base.InnerList.Add(font);
				num = 1;
				continue;
				IL_8A:
				if (this.ᜁ.ContainsKey(font))
				{
					break;
				}
				num = 4;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00024CF0 File Offset: 0x00023CF0
		public void SerializeDataToList(RecordArrayList records)
		{
			for (;;)
			{
				int num = 0;
				int count = base.InnerList.Count;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						XlsFont xlsFont = base.InnerList[num];
						xlsFont.SerializeDataToList(records);
						num2 = 6;
						continue;
					}
					case 1:
						goto IL_AF;
					case 2:
						return;
					case 3:
						if (num == 4)
						{
							goto IL_3C;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BA;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					case 4:
						goto IL_BA;
					case 5:
						goto IL_AF;
					case 6:
						goto IL_3C;
					}
					break;
					IL_3C:
					num++;
					if (true)
					{
					}
					num2 = 1;
					continue;
					IL_BA:
					if (num >= count)
					{
						num2 = 2;
						continue;
					}
					num2 = 3;
					continue;
					IL_AF:
					num2 = 4;
				}
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00024DC8 File Offset: 0x00023DC8
		public new bool Contains(XlsFont font)
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
			return this.ᜁ.ContainsKey(font);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00024E10 File Offset: 0x00023E10
		protected internal Dictionary<int, int> AddRange(XlsFontsCollection fonts)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_D6;
					case 1:
					{
						if (num2 == 4)
						{
							num = 6;
							continue;
						}
						XlsFont a_2 = fonts[num2] as XlsFont;
						int value = this.ᜀ(a_2);
						Dictionary<int, int> dictionary;
						dictionary.Add(num2, value);
						num = 11;
						continue;
					}
					case 2:
					{
						if (fonts == this)
						{
							num = 0;
							continue;
						}
						Dictionary<int, int> dictionary = new Dictionary<int, int>();
						num2 = 0;
						int count = fonts.Count;
						num = 7;
						continue;
					}
					case 3:
						goto IL_DB;
					case 4:
						goto IL_65;
					case 6:
					{
						Dictionary<int, int> dictionary;
						dictionary.Add(num2, num2);
						num = 3;
						continue;
					}
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
						{
							Dictionary<int, int> dictionary;
							return dictionary;
						}
						default:
							if (false)
							{
							}
							goto IL_121;
						}
						break;
					case 8:
					{
						Dictionary<int, int> dictionary;
						return dictionary;
					}
					case 9:
						goto IL_121;
					case 10:
					{
						int count;
						if (num2 >= count)
						{
							num = 8;
							continue;
						}
						num = 1;
						continue;
					}
					case 11:
						if (true)
						{
						}
						goto IL_DB;
					}
					if (fonts == null)
					{
						num = 4;
						continue;
					}
					num = 2;
					continue;
					IL_DB:
					num2++;
					num = 9;
					continue;
					IL_121:
					num = 10;
				}
				IL_65:
				throw new ArgumentNullException(RecordTableEnumerator.b("☿ⵁ⩃㉅㭇", a_));
				IL_D6:
				return null;
			}
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00024F9C File Offset: 0x00023F9C
		protected internal Dictionary<int, int> AddRange(ICollection<int> colFonts, XlsFontsCollection sourceFonts)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						if (sourceFonts == null)
						{
							num = 4;
							continue;
						}
						if (true)
						{
						}
						Dictionary<int, int> dictionary = new Dictionary<int, int>();
						IEnumerator<int> enumerator = colFonts.GetEnumerator();
						num = 2;
						continue;
					}
					case 1:
						goto IL_4C;
					case 2:
						goto IL_1AA;
					case 4:
						goto IL_17C;
					}
					if (colFonts == null)
					{
						num = 1;
					}
					else
					{
						num = 0;
					}
				}
				IL_4C:
				throw new ArgumentNullException(RecordTableEnumerator.b("吶嘸场笼倾⽀㝂㙄", a_));
				IL_139:
				throw new ArgumentNullException(RecordTableEnumerator.b("䐶嘸为似尾⑀Ղ⩄⥆㵈㡊", a_));
				IL_17C:
				goto IL_139;
				IL_1AA:
				try
				{
					num = 4;
					Dictionary<int, int> dictionary;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							IEnumerator<int> enumerator;
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							int index = enumerator.Current;
							XlsFont xlsFont = (XlsFont)sourceFonts[index];
							int key = xlsFont.Index;
							int value = this.ᜀ(xlsFont);
							dictionary.Add(key, value);
							num = 2;
							continue;
						}
						case 1:
							num = 3;
							continue;
						case 3:
							goto IL_D8;
						}
						IL_79:
						num = 0;
						continue;
						goto IL_79;
					}
					IL_D8:
					return dictionary;
				}
				finally
				{
					num = 0;
					for (;;)
					{
						IEnumerator<int> enumerator;
						switch (num)
						{
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								enumerator.Dispose();
								break;
							}
							num = 2;
							continue;
						case 2:
							goto IL_136;
						}
						if (enumerator == null)
						{
							break;
						}
						num = 1;
					}
					IL_136:;
				}
				goto IL_139;
			}
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0002516C File Offset: 0x0002416C
		private new int ᜀ(XlsFont A_0)
		{
			int a_ = 13;
			if (true)
			{
			}
			if (A_0 != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					A_0 = this.ᜀ(A_0.Record);
					return A_0.Index;
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("╂⩄⥆㵈", a_));
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000251E0 File Offset: 0x000241E0
		protected override void OnClearComplete()
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
			this.ᜁ.Clear();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00025228 File Offset: 0x00024228
		public XlsFontsCollection Clone(XlsWorkbook parent)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					int num2;
					int count;
					List<XlsFont> innerList;
					XlsFontsCollection xlsFontsCollection;
					switch (num)
					{
					case 0:
					{
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						XlsFont xlsFont = innerList[num2];
						xlsFont = xlsFont.Clone(xlsFontsCollection);
						num = 6;
						continue;
					}
					case 1:
						return xlsFontsCollection;
					case 2:
						goto IL_C1;
					case 3:
						for (;;)
						{
							XlsFont xlsFont;
							xlsFontsCollection.ForceAdd(xlsFont);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_128;
							}
						}
						IL_128:
						if (false)
						{
						}
						num = 7;
						continue;
					case 4:
						goto IL_5C;
					case 6:
						if (num2 != 4)
						{
							num = 3;
							continue;
						}
						goto IL_5E;
					case 7:
						goto IL_5E;
					case 8:
						goto IL_C1;
					}
					if (parent == null)
					{
						num = 4;
						continue;
					}
					xlsFontsCollection = new XlsFontsCollection(base.ReservedHandle, parent);
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 2;
					continue;
					IL_5E:
					if (true)
					{
					}
					num2++;
					num = 8;
					continue;
					IL_C1:
					num = 0;
				}
				IL_5C:
				throw new ArgumentNullException(RecordTableEnumerator.b("䰻弽㈿❁⩃㉅", a_));
			}
			}
		}

		// Token: 0x040000AA RID: 170
		private new XlsWorkbook ᜀ;

		// Token: 0x040000AB RID: 171
		private bool[] \u2460\u0087\u0092\u00A7;

		// Token: 0x040000AC RID: 172
		private bool \u25D9\u008A\u0097\u008F;

		// Token: 0x040000AD RID: 173
		private new Dictionary<XlsFont, XlsFont> ᜁ = new Dictionary<XlsFont, XlsFont>();
	}
}
