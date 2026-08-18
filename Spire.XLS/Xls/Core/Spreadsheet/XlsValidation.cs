using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000203 RID: 515
	public class XlsValidation : spr\u20B0, sprṨ, ICloneParent
	{
		// Token: 0x06001D46 RID: 7494 RVA: 0x000F9740 File Offset: 0x000F8740
		internal XlsValidation(XlsDataValidationCollection A_0)
		{
			int a_ = 19;
			this.ᜄ = new Type[]
			{
				typeof(sprᣋ),
				typeof(sprᦊ),
				typeof(sprᲔ),
				typeof(spr\u1BFD)
			};
			this.ᜆ = string.Empty;
			this.ᜇ = string.Empty;
			this.ᜉ = new spr\u2530();
			base..ctor();
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("㥈⩊㽌⩎㽐❒", a_));
			}
			this.ᜅ = (sprᡣ)spr\u175E.ᜀ(TBIFFRecord.DV);
			this.ᜈ = A_0;
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x000F97F4 File Offset: 0x000F87F4
		internal XlsValidation(XlsDataValidationCollection A_0, sprᡣ A_1)
		{
			int a_ = 7;
			this..ctor(A_0);
			this.ᜅ = (sprᡣ)A_1.ᜅ();
			this.ᜀ(this.ᜅ);
			try
			{
				this.Reparse();
			}
			catch (spr\u2313)
			{
				XlsWorkbook xlsWorkbook = this.Workbook;
				if (xlsWorkbook == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("洼帾㍀♂⭄㍆楈⑊⽌╎㑐げ⅔睖㩘㩚㍜ㅞ๠ᝢ䕤զ౨䭪୬nѰᵲᅴ奶", a_));
				}
				if (!xlsWorkbook.Loading)
				{
					throw;
				}
				xlsWorkbook.ᜀ(this);
			}
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x000F9880 File Offset: 0x000F8880
		public void AddRange(XlsValidation dv)
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
			List<Rectangle> a_ = dv.ᜉ.ᜂ();
			this.ᜉ.ᜃ(a_);
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x000F98D4 File Offset: 0x000F88D4
		public void AddRange(XlsRange range)
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
			this.ᜉ.ᜀ(range);
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x000F991C File Offset: 0x000F891C
		[CLSCompliant(false)]
		internal void ᜀ(TAddr A_0)
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
			this.ᜉ.ᜄ(A_0.GetRectangle());
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x000F996C File Offset: 0x000F896C
		internal void ᜀ(ICombinedRange A_0)
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
			this.ᜉ.ᜄ(A_0.GetRectangles());
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x000F99B8 File Offset: 0x000F89B8
		public void RemoveRange(XlsRange range)
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
			this.RemoveRange(range.GetRectangles());
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x000F9A00 File Offset: 0x000F8A00
		public void RemoveRange(Rectangle[] rectangles)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_74:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				goto IL_38;
			}
			for (;;)
			{
				IL_1E:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (this.ᜉ.ᜂ().Count == 0)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					goto IL_66;
				case 2:
					return;
				}
				goto IL_38;
			}
			IL_66:
			this.ᜈ.Remove(this);
			goto IL_74;
			IL_38:
			this.ᜉ.ᜀ(rectangles);
			num = 0;
			goto IL_1E;
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x000F9A98 File Offset: 0x000F8A98
		private void ᜂ()
		{
			switch (0)
			{
			default:
			{
				int num2;
				int count;
				List<Rectangle> list;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_86:
					int num = 2;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							return;
						case 1:
							goto IL_91;
						case 2:
							goto IL_91;
						case 3:
						{
							if (num2 >= count)
							{
								num = 0;
								continue;
							}
							Rectangle rect = list[num2];
							this.ᜅ.ᜀ(new TAddr(rect));
							num2++;
							num = 1;
							continue;
						}
						}
						goto IL_55;
						IL_91:
						num = 3;
					}
					return;
				}
				}
				if (false)
				{
				}
				IL_55:
				this.ᜉ.ᜄ();
				this.ᜅ.ᜁ(new TAddr[0]);
				list = this.ᜉ.ᜂ();
				num2 = 0;
				count = list.Count;
				goto IL_86;
			}
			}
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x000F9B7C File Offset: 0x000F8B7C
		private void ᜀ(sprᡣ A_0)
		{
			switch (0)
			{
			default:
			{
				int num2;
				int num3;
				TAddr[] array;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_5A:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_65;
						case 1:
						{
							if (true)
							{
							}
							if (num2 >= num3)
							{
								num = 3;
								continue;
							}
							TAddr taddr = array[num2];
							this.ᜉ.ᜄ(taddr.GetRectangle());
							num2++;
							num = 0;
							continue;
						}
						case 2:
							goto IL_65;
						case 3:
							return;
						}
						goto IL_4D;
						IL_65:
						num = 1;
					}
					return;
				}
				}
				if (false)
				{
				}
				IL_4D:
				array = A_0.ᜑ();
				num2 = 0;
				num3 = array.Length;
				goto IL_5A;
			}
			}
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x000F9C3C File Offset: 0x000F8C3C
		private Ptg[] ᜀ(DateTime A_0)
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
			double num = A_0.ToOADate();
			spr\u180B spr_u180B = (spr\u180B)FormulaUtil.ᜀ(FormulaToken.tNumber, new object[]
			{
				num
			});
			spr_u180B.ᜀ(num);
			return new Ptg[]
			{
				spr_u180B
			};
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x000F9CB0 File Offset: 0x000F8CB0
		private DateTime ᜁ(Ptg[] A_0)
		{
			switch (0)
			{
			default:
			{
				DateTime result;
				for (;;)
				{
					result = DateTime.MinValue;
					int num = 5;
					for (;;)
					{
						Ptg ptg;
						switch (num)
						{
						case 0:
						{
							int num2 = A_0.Length;
							num = 4;
							continue;
						}
						case 1:
							goto IL_7C;
						case 2:
							goto IL_9E;
						case 3:
							goto IL_10C;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7C;
							default:
							{
								if (false)
								{
								}
								int num2;
								if (num2 == 1)
								{
									num = 9;
									continue;
								}
								return result;
							}
							}
							break;
						case 5:
							if (A_0 != null)
							{
								num = 0;
								continue;
							}
							return result;
						case 6:
							if (ptg is sprℿ)
							{
								num = 7;
								continue;
							}
							num = 8;
							continue;
						case 7:
						{
							int num3 = (int)((sprℿ)ptg).ᜀ();
							result = DateTime.FromOADate((double)num3);
							num = 2;
							continue;
						}
						case 8:
							if (ptg is spr\u180B)
							{
								num = 1;
								continue;
							}
							return result;
						case 9:
							ptg = A_0[0];
							num = 6;
							continue;
						}
						break;
						IL_7C:
						double d = ((spr\u180B)ptg).ᜀ();
						result = DateTime.FromOADate(d);
						num = 3;
					}
				}
				IL_9E:
				return result;
				IL_10C:
				if (true)
				{
				}
				return result;
			}
			}
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x000F9E00 File Offset: 0x000F8E00
		internal static Ptg[] ᜀ(ref string A_0, FormulaUtil A_1, XlsWorksheet A_2, int A_3, int A_4)
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				int num = 10;
				for (;;)
				{
					if (true)
					{
					}
					NumberFormatInfo provider;
					switch (num)
					{
					case 0:
					{
						spr\u180B spr_u180B;
						if (spr_u180B.ᜀ() == 0.0)
						{
							num = 12;
							continue;
						}
						Ptg[] result;
						return result;
					}
					case 1:
						provider = A_1.NumberFormat;
						num = 3;
						continue;
					case 2:
						goto IL_11C;
					case 3:
						goto IL_83;
					case 4:
					{
						Ptg[] result;
						return result;
					}
					case 5:
					{
						string text;
						if (text[0] == '=')
						{
							num = 15;
							continue;
						}
						goto IL_B0;
					}
					case 6:
						if (A_1 != null)
						{
							num = 1;
							continue;
						}
						goto IL_83;
					case 7:
						goto IL_7E;
					case 8:
						goto IL_B0;
					case 9:
					{
						if (A_0.Length == 0)
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
								num = 2;
								continue;
							}
						}
						string text = A_0;
						num = 5;
						continue;
					}
					case 11:
					{
						spr\u180B spr_u180B = (spr\u180B)FormulaUtil.ᜁ(FormulaToken.tNumber);
						double a_2;
						spr_u180B.ᜀ(a_2);
						Ptg[] result = new Ptg[]
						{
							spr_u180B
						};
						num = 0;
						continue;
					}
					case 12:
					{
						string text = RecordTableEnumerator.b("癅", a_);
						num = 13;
						continue;
					}
					case 13:
					{
						Ptg[] result;
						return result;
					}
					case 14:
					{
						string text;
						double a_2;
						if (double.TryParse(text, NumberStyles.Any, provider, out a_2))
						{
							num = 11;
							continue;
						}
						Ptg[] result = XlsValidation.ᜀ(text, A_2, A_1, A_3, A_4);
						num = 4;
						continue;
					}
					case 15:
					{
						string text = UtilityMethods.ᜀ(text);
						num = 8;
						continue;
					}
					}
					if (A_0 == null)
					{
						num = 7;
						continue;
					}
					num = 9;
					continue;
					IL_83:
					num = 14;
					continue;
					IL_B0:
					provider = null;
					num = 6;
				}
				IL_7E:
				throw new ArgumentNullException(RecordTableEnumerator.b("ぅ⥇♉㥋⭍", a_));
				IL_11C:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("၅⥇♉㥋⭍灏ㅑ㕓㡕㙗㕙⡛繝ɟݡ䑣ͥէᩩᡫ᝭幯", a_));
			}
			}
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x000FA048 File Offset: 0x000F9048
		internal static Ptg[] ᜀ(string A_0, XlsWorksheet A_1, FormulaUtil A_2, int A_3, int A_4)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				Ptg[] array;
				for (;;)
				{
					Dictionary<Type, sprᨳ> dictionary = new Dictionary<Type, sprᨳ>();
					dictionary.Add(typeof(sprᲔ), new sprᨳ(1));
					dictionary.Add(typeof(spr\u2596), new sprᨳ(1));
					dictionary.Add(typeof(sprᦈ), new sprᨳ(1));
					dictionary.Add(typeof(sprᦊ), new sprᨳ(1));
					dictionary.Add(typeof(spr\u1BFD), new sprᨳ(1));
					dictionary.Add(typeof(sprᣋ), new sprᨳ(1));
					dictionary.Add(typeof(spr\u25A0), new sprᨳ(1));
					XlsWorkbook parentWorkbook = A_1.ParentWorkbook;
					XlsValidation.ᜀ(true);
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (XlsValidation.ᜀ(array))
							{
								num = 5;
								continue;
							}
							return array;
						case 1:
							array = parentWorkbook.FormulaUtil.ᜁ(A_0, null, dictionary, 0, null, ParseFormulaOptions.DataValidation, A_3, A_4);
							num = 4;
							continue;
						case 2:
							goto IL_172;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_172;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								if (A_2 == null)
								{
									num = 1;
									continue;
								}
								array = A_2.ᜁ(A_0, A_1, dictionary, 0, null, ParseFormulaOptions.DataValidation, A_3, A_4);
								num = 2;
								continue;
							}
							break;
						case 4:
							goto IL_12A;
						case 5:
							array = new Ptg[]
							{
								array[0]
							};
							A_0 = RecordTableEnumerator.b("ص", a_);
							num = 6;
							continue;
						case 6:
							return array;
						}
						break;
						IL_12A:
						XlsValidation.ᜀ(false);
						num = 0;
						continue;
						IL_172:
						goto IL_12A;
					}
				}
				return array;
			}
			}
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x000FA228 File Offset: 0x000F9228
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static void ᜀ(bool A_0)
		{
			int a_ = 11;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (!A_0)
				{
					sprᨳ[] a_2 = new sprᨳ[]
					{
						new sprᨳ(typeof(sprᦊ), 2)
					};
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ୄቆшॊࡌᵎ", a_), ExcelFunction.ISNUMBER, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂݄୆ࡈՊٌ", a_), ExcelFunction.ISBLANK, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂Dᕆᭈ", a_), ExcelFunction.ISERR, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂DᕆᭈъὌ", a_), ExcelFunction.ISERROR, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂DᅆైՊ", a_), ExcelFunction.ISEVEN, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ॄࡆ่Ɋ์๎ᵐ", a_), ExcelFunction.ISLOGICAL, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ୄن", a_), ExcelFunction.ISNA, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ୄࡆ݈ὊࡌᝎՐ", a_), ExcelFunction.ISNONTEXT, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂੄͆ൈ", a_), ExcelFunction.ISODD, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ᕄ੆ᵈ", a_), ExcelFunction.ISPMT, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ᝄɆ཈", a_), ExcelFunction.ISREF, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ᅄɆᅈὊ", a_), ExcelFunction.ISTEXT, a_2, 1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("เᅂ", a_), ExcelFunction.OR, a_2, -1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ీూń", a_), ExcelFunction.MOD, a_2, -1);
					FormulaUtil.ᜀ(RecordTableEnumerator.b("ీూń", a_), ExcelFunction.MOD, a_2, -1);
					return;
				}
				break;
			}
			sprᨳ[] a_3 = new sprᨳ[]
			{
				new sprᨳ(typeof(sprᦈ), 2)
			};
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ୄቆшॊࡌᵎ", a_), ExcelFunction.ISNUMBER, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂݄୆ࡈՊٌ", a_), ExcelFunction.ISBLANK, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂Dᕆᭈ", a_), ExcelFunction.ISERR, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂DᕆᭈъὌ", a_), ExcelFunction.ISERROR, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂DᅆైՊ", a_), ExcelFunction.ISEVEN, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ॄࡆ่Ɋ์๎ᵐ", a_), ExcelFunction.ISLOGICAL, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ୄن", a_), ExcelFunction.ISNA, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ୄࡆ݈ὊࡌᝎՐ", a_), ExcelFunction.ISNONTEXT, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂੄͆ൈ", a_), ExcelFunction.ISODD, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ᕄ੆ᵈ", a_), ExcelFunction.ISPMT, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ᝄɆ཈", a_), ExcelFunction.ISREF, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ࡀ၂ᅄɆᅈὊ", a_), ExcelFunction.ISTEXT, a_3, 1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("เᅂ", a_), ExcelFunction.OR, a_3, -1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ీూń", a_), ExcelFunction.MOD, a_3, -1);
			FormulaUtil.ᜀ(RecordTableEnumerator.b("ీూń", a_), ExcelFunction.MOD, a_3, -1);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x000FA588 File Offset: 0x000F9588
		internal void ᜀ(string A_0, FormulaUtil A_1, bool A_2)
		{
			Ptg[] a_;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				a_ = XlsValidation.ᜀ(ref A_0, A_1, this.ᜈ.Worksheet, 0, 0);
				if (!A_2)
				{
					this.ᜅ.ᜀ(a_);
					this.ᜇ = A_0;
					return;
				}
				break;
			}
			this.ᜅ.ᜁ(a_);
			this.ᜆ = A_0;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x000FA608 File Offset: 0x000F9608
		private static bool ᜀ(Ptg[] A_0)
		{
			switch (0)
			{
			default:
			{
				bool result;
				for (;;)
				{
					result = true;
					spr\u180B spr_u180B = A_0[0] as spr\u180B;
					int num = 7;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							if (spr_u180B.ᜀ() == 0.0)
							{
								num = 13;
								continue;
							}
							return result;
						case 1:
							goto IL_78;
						case 2:
							goto IL_19A;
						case 3:
						{
							FormulaToken tokenCode;
							if (tokenCode != FormulaToken.tUnaryMinus)
							{
								num = 2;
								continue;
							}
							goto IL_120;
						}
						case 4:
							return result;
						case 5:
							num = 0;
							continue;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_19A;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num = 11;
								continue;
							}
							break;
						case 7:
							if (spr_u180B != null)
							{
								num = 5;
								continue;
							}
							result = false;
							num = 4;
							continue;
						case 8:
						{
							int num3;
							if (num2 >= num3)
							{
								num = 6;
								continue;
							}
							FormulaToken tokenCode = A_0[num2].TokenCode;
							num = 3;
							continue;
						}
						case 9:
							goto IL_78;
						case 10:
							return result;
						case 11:
							return result;
						case 12:
							result = false;
							num = 10;
							continue;
						case 13:
						{
							num2 = 1;
							int num3 = A_0.Length;
							num = 1;
							continue;
						}
						case 14:
						{
							FormulaToken tokenCode;
							if (tokenCode != FormulaToken.tUnaryPlus)
							{
								num = 12;
								continue;
							}
							goto IL_120;
						}
						}
						break;
						IL_78:
						num = 8;
						continue;
						IL_120:
						num2++;
						num = 9;
						continue;
						IL_19A:
						num = 14;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x000FA7B4 File Offset: 0x000F97B4
		internal XlsWorkbook Workbook
		{
			get
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
				return this.ᜈ.Workbook;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x000FA7FC File Offset: 0x000F97FC
		public XlsWorksheet Worksheet
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
				return this.ᜈ.Worksheet;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x000FA844 File Offset: 0x000F9844
		internal sprᡣ DVRecord
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
				return this.ᜅ;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x000FA888 File Offset: 0x000F9888
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x000FA8CC File Offset: 0x000F98CC
		public XlsDataValidationCollection ParentCollection
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
				return this.ᜈ;
			}
			set
			{
				int a_ = 3;
				int num = 3;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						if (this.ᜈ != value)
						{
							num = 4;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_92;
						default:
							goto IL_60;
						}
						break;
					case 4:
						goto IL_92;
					}
					if (value == null)
					{
						num = 2;
						continue;
					}
					num = 0;
					continue;
					IL_92:
					this.ᜈ = value;
					num = 1;
				}
				IL_60:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("伸娺儼䨾⑀", a_));
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x000FA984 File Offset: 0x000F9984
		internal string[] DVRanges
		{
			get
			{
				switch (0)
				{
				default:
				{
					int num = 0;
					List<string> list;
					for (;;)
					{
						int num2;
						int num3;
						TAddr[] array;
						switch (num)
						{
						case 1:
							if (true)
							{
							}
							goto IL_11C;
						case 2:
							goto IL_11C;
						case 3:
							goto IL_13B;
						case 4:
							if (num2 >= num3)
							{
								num = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_F2;
							default:
							{
								if (false)
								{
								}
								TAddr taddr = array[num2];
								string item = sprṔ.ᜀ(taddr.FirstRow + 1, taddr.FirstCol + 1, taddr.LastRow + 1, taddr.LastCol + 1);
								list.Add(item);
								num2++;
								num = 2;
								continue;
							}
							}
							break;
						case 5:
							this.ᜂ();
							num = 6;
							continue;
						case 6:
							goto IL_D2;
						}
						if (this.ᜅ.ᜑ().Length == 0)
						{
							num = 5;
							continue;
						}
						goto IL_D2;
						IL_F2:
						num = 1;
						continue;
						IL_D2:
						list = new List<string>();
						array = this.ᜅ.ᜑ();
						num3 = (int)this.ᜅ.ᜌ();
						num2 = 0;
						goto IL_F2;
						IL_11C:
						num = 4;
					}
					IL_13B:
					return list.ToArray();
				}
				}
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x000FAAD4 File Offset: 0x000F9AD4
		public int ShapesCount
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
				return this.ᜉ.ᜂ().Count;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x000FAB20 File Offset: 0x000F9B20
		internal spr\u1DF5 Application
		{
			get
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
				return this.ᜈ.AppImplementation;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06001D5F RID: 7519 RVA: 0x000FAB68 File Offset: 0x000F9B68
		public object Parent
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
				return this.ᜈ;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x000FABAC File Offset: 0x000F9BAC
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x000FABF4 File Offset: 0x000F9BF4
		public string InputTitle
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
				return this.ᜅ.ᜊ();
			}
			set
			{
				int a_ = 5;
				for (;;)
				{
					this.ᜀ(RecordTableEnumerator.b("欺似倾ⱀ㍂ㅄՆ♈㍊᥌♎═㽒ご", a_), value, 32);
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_96;
						case 1:
							this.ᜅ.ᜃ(value);
							goto IL_8B;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8B;
							}
							if (false)
							{
							}
							if (this.ᜅ.ᜊ() != value)
							{
								num = 1;
								continue;
							}
							return;
						}
						break;
						IL_8B:
						num = 0;
					}
				}
				IL_96:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x000FACA4 File Offset: 0x000F9CA4
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x000FACEC File Offset: 0x000F9CEC
		public string InputMessage
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
				return this.ᜅ.ᜐ();
			}
			set
			{
				int a_ = 18;
				for (;;)
				{
					this.ᜀ(RecordTableEnumerator.b("ᡇ㡉⍋⍍⁏♑ᙓ㥕⁗๙㥛♝ᑟ", a_), value, 255);
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_96;
							}
							if (false)
							{
							}
							if (this.ᜅ.ᜐ() != value)
							{
								num = 1;
								continue;
							}
							return;
						case 1:
							if (true)
							{
							}
							this.ᜅ.ᜅ(value);
							goto IL_96;
						case 2:
							return;
						}
						break;
						IL_96:
						num = 2;
					}
				}
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x000FAD9C File Offset: 0x000F9D9C
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x000FADE4 File Offset: 0x000F9DE4
		public string ErrorTitle
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
				return this.ᜅ.ᜈ();
			}
			set
			{
				int a_ = 10;
				for (;;)
				{
					if (true)
					{
					}
					this.ᜀ(RecordTableEnumerator.b("Կぁ㙃⥅㩇ࡉ⍋㙍я㭑⁓㩕㵗", a_), value, 32);
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜅ.ᜄ(value);
							goto IL_93;
						case 1:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_93;
							default:
								if (false)
								{
								}
								if (this.ᜅ.ᜈ() != value)
								{
									num = 0;
									continue;
								}
								return;
							}
							break;
						}
						break;
						IL_93:
						num = 1;
					}
				}
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x000FAE94 File Offset: 0x000F9E94
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x000FAEDC File Offset: 0x000F9EDC
		public string ErrorMessage
		{
			get
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
				return this.ᜅ.ᜀ();
			}
			set
			{
				int a_ = 6;
				for (;;)
				{
					this.ᜀ(RecordTableEnumerator.b("礻䰽㈿ⵁ㙃х❇㉉ᡋ⭍⡏♑", a_), value, 225);
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return;
						case 1:
							this.ᜅ.ᜂ(value);
							goto IL_96;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_96;
							}
							if (false)
							{
							}
							if (this.ᜅ.ᜀ() != value)
							{
								if (true)
								{
								}
								num = 1;
								continue;
							}
							return;
						}
						break;
						IL_96:
						num = 0;
					}
				}
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x000FAF8C File Offset: 0x000F9F8C
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x000FAFD0 File Offset: 0x000F9FD0
		public string Formula1
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
				return this.ᜆ;
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 2:
						if (true)
						{
						}
						this.ᜆ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					if (!(this.ᜆ != value))
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x000FB050 File Offset: 0x000FA050
		// (set) Token: 0x06001D6B RID: 7531 RVA: 0x000FB09C File Offset: 0x000FA09C
		public DateTime DateTime1
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
				return this.ᜁ(this.ᜅ.\u1713());
			}
			set
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
				Ptg[] a_ = this.ᜀ(value);
				this.ᜅ.ᜁ(a_);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x000FB0EC File Offset: 0x000FA0EC
		// (set) Token: 0x06001D6D RID: 7533 RVA: 0x000FB130 File Offset: 0x000FA130
		public string Formula2
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
				return this.ᜇ;
			}
			set
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						return;
					case 2:
						this.ᜇ = value;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					if (!(this.ᜇ != value))
					{
						break;
					}
					num = 2;
				}
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x000FB1B0 File Offset: 0x000FA1B0
		// (set) Token: 0x06001D6F RID: 7535 RVA: 0x000FB1FC File Offset: 0x000FA1FC
		public DateTime DateTime2
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
				return this.ᜁ(this.ᜅ.\u1714());
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
				Ptg[] a_ = this.ᜀ(value);
				this.ᜅ.ᜀ(a_);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x000FB24C File Offset: 0x000FA24C
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x000FB294 File Offset: 0x000FA294
		public CellDataType AllowType
		{
			get
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
				return this.ᜅ.\u170D();
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
				this.ᜅ.ᜀ(value);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x000FB2DC File Offset: 0x000FA2DC
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x000FB324 File Offset: 0x000FA324
		public ValidationComparisonOperator CompareOperator
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
				return this.ᜅ.ᜎ();
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
				this.ᜅ.ᜀ(value);
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x000FB36C File Offset: 0x000FA36C
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x000FB3B4 File Offset: 0x000FA3B4
		public bool IsListInFormula
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
				return this.ᜅ.\u1712();
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
				this.ᜅ.ᜃ(value);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x000FB3FC File Offset: 0x000FA3FC
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x000FB444 File Offset: 0x000FA444
		public bool IgnoreBlank
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
				return this.ᜅ.ᜃ();
			}
			set
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
				this.ᜅ.ᜀ(value);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x000FB48C File Offset: 0x000FA48C
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x000FB4D4 File Offset: 0x000FA4D4
		public bool IsSuppressDropDownArrow
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
				return this.ᜅ.ᜄ();
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
				this.ᜅ.ᜁ(value);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x000FB51C File Offset: 0x000FA51C
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x000FB564 File Offset: 0x000FA564
		public bool ShowInput
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
				return this.ᜅ.ᜇ();
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
				this.ᜅ.ᜄ(value);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x000FB5AC File Offset: 0x000FA5AC
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x000FB5F4 File Offset: 0x000FA5F4
		public bool ShowError
		{
			get
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
				return this.ᜅ.ᜁ();
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
				this.ᜅ.ᜂ(value);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x000FB63C File Offset: 0x000FA63C
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x000FB684 File Offset: 0x000FA684
		public int PromptBoxHPosition
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
				return this.ᜈ.PromptBoxHPosition;
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
				this.ᜈ.PromptBoxHPosition = value;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x000FB6CC File Offset: 0x000FA6CC
		// (set) Token: 0x06001D81 RID: 7553 RVA: 0x000FB714 File Offset: 0x000FA714
		public int PromptBoxVPosition
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
				return this.ᜈ.PromptBoxVPosition;
			}
			set
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
				this.ᜈ.PromptBoxVPosition = value;
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x000FB75C File Offset: 0x000FA75C
		// (set) Token: 0x06001D83 RID: 7555 RVA: 0x000FB7A4 File Offset: 0x000FA7A4
		public bool IsInputVisible
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
				return this.ᜈ.IsPromptBoxVisible;
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
				this.ᜈ.IsPromptBoxVisible = value;
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06001D84 RID: 7556 RVA: 0x000FB7EC File Offset: 0x000FA7EC
		// (set) Token: 0x06001D85 RID: 7557 RVA: 0x000FB834 File Offset: 0x000FA834
		public bool IsInputPositionFixed
		{
			get
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
				return this.ᜈ.IsPromptBoxPositionFixed;
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
				this.ᜈ.IsPromptBoxPositionFixed = value;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x000FB87C File Offset: 0x000FA87C
		// (set) Token: 0x06001D87 RID: 7559 RVA: 0x000FB8C4 File Offset: 0x000FA8C4
		public AlertStyleType AlertStyle
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
				return this.ᜅ.ᜋ();
			}
			set
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
				this.ᜅ.ᜀ(value);
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06001D88 RID: 7560 RVA: 0x000FB90C File Offset: 0x000FA90C
		// (set) Token: 0x06001D89 RID: 7561 RVA: 0x000FBAB4 File Offset: 0x000FAAB4
		public string[] Values
		{
			get
			{
				string[] array;
				for (;;)
				{
					IL_00:
					switch (0)
					{
					default:
					{
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								num = 12;
								continue;
							case 2:
								if (array.Length > 0)
								{
									num = 7;
									continue;
								}
								goto IL_197;
							case 3:
							{
								Ptg[] array2;
								if (array2 != null)
								{
									num = 13;
									continue;
								}
								goto IL_197;
							}
							case 4:
							{
								Ptg[] array2;
								spr\u24A7 spr_u24A = array2[0] as spr\u24A7;
								string text = spr_u24A.ᜀ();
								char[] separator = new char[1];
								array = text.Split(separator);
								num = 5;
								continue;
							}
							case 5:
								if (array != null)
								{
									num = 9;
									continue;
								}
								goto IL_197;
							case 6:
								num = 8;
								continue;
							case 7:
								return array;
							case 8:
							{
								Ptg[] array2;
								if (!(array2[0] is spr\u24A7))
								{
									goto IL_197;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									num = 4;
									continue;
								}
								break;
							}
							case 9:
								num = 2;
								continue;
							case 10:
							{
								Ptg[] array2;
								if (array2.Length == 1)
								{
									num = 6;
									continue;
								}
								goto IL_197;
							}
							case 11:
							{
								if (true)
								{
								}
								Ptg[] array2 = this.ᜅ.\u1713();
								num = 3;
								continue;
							}
							case 12:
								if (this.AllowType == CellDataType.User)
								{
									num = 11;
									continue;
								}
								goto IL_197;
							case 13:
								num = 10;
								continue;
							}
							if (!this.IsListInFormula)
							{
								goto IL_197;
							}
							num = 1;
						}
						break;
					}
					}
				}
				return array;
				IL_197:
				return null;
			}
			set
			{
				int a_ = 10;
				int num = 1;
				StringBuilder stringBuilder;
				for (;;)
				{
					int num2;
					int num3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						switch (num)
						{
						case 0:
							if (stringBuilder.Length > 256)
							{
								num = 5;
								continue;
							}
							goto IL_171;
						case 2:
							if (num2 >= num3)
							{
								num = 4;
								continue;
							}
							num = 8;
							continue;
						case 3:
							num = 9;
							continue;
						case 4:
							num = 0;
							continue;
						case 5:
							goto IL_16C;
						case 6:
							goto IL_DA;
						case 7:
							if (true)
							{
							}
							goto IL_DA;
						case 8:
							stringBuilder.Append((num2 == num3) ? value[num2] : (value[num2] + RecordTableEnumerator.b("䀿", a_)));
							num2++;
							num = 6;
							continue;
						case 9:
							goto IL_101;
						}
						if (value == null)
						{
							num = 3;
							continue;
						}
						break;
						IL_DA:
						num = 2;
						continue;
					}
					stringBuilder = new StringBuilder(RecordTableEnumerator.b("房", a_));
					num2 = 0;
					num3 = value.Length;
					num = 7;
				}
				IL_101:
				throw new ArgumentNullException(RecordTableEnumerator.b("㘿⍁⡃㍅ⵇ", a_));
				IL_16C:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㘿⍁⡃㍅ⵇ", a_), RecordTableEnumerator.b("ጿ㙁㙃⽅♇ⵉ㽋湍ㅏ⁑ㅓ癕ⱗ㕙㍛繝౟ൡ੣ť䙧", a_));
				IL_171:
				stringBuilder.Append(RecordTableEnumerator.b("房", a_));
				this.ᜀ(stringBuilder.ToString(), new FormulaUtil(this.ᜈ.Workbook.AppImplementation, this.ᜈ.Workbook, NumberFormatInfo.InvariantInfo, ',', ';'), true);
				this.IsListInFormula = true;
				this.IsSuppressDropDownArrow = false;
				this.AllowType = CellDataType.User;
				this.CompareOperator = ValidationComparisonOperator.NotEqual;
				this.AlertStyle = AlertStyleType.Stop;
				this.ShowError = true;
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x000FBCA8 File Offset: 0x000FACA8
		// (set) Token: 0x06001D8B RID: 7563 RVA: 0x000FBE00 File Offset: 0x000FAE00
		public IXLSRange DataRange
		{
			get
			{
				switch (0)
				{
				default:
				{
					if (true)
					{
					}
					int num = 3;
					Ptg[] array;
					XlsWorkbook xlsWorkbook;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (array != null)
							{
								goto IL_F0;
							}
							goto IL_148;
						case 1:
							num = 2;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_F0;
							default:
								if (false)
								{
								}
								if (Array.IndexOf<Type>(this.ᜄ, array[0].GetType()) != -1)
								{
									num = 4;
									continue;
								}
								goto IL_148;
							}
							break;
						case 4:
							goto IL_A9;
						case 5:
							num = 7;
							continue;
						case 6:
							array = this.ᜅ.\u1713();
							xlsWorkbook = this.Workbook;
							num = 0;
							continue;
						case 7:
							if (array.Length == 1)
							{
								num = 1;
								continue;
							}
							goto IL_148;
						}
						if (this.AllowType == CellDataType.User)
						{
							num = 6;
							continue;
						}
						goto IL_148;
						IL_F0:
						num = 5;
					}
					IL_A9:
					bool r1C1ReferenceMode = xlsWorkbook.CalculationOptions.R1C1ReferenceMode;
					string name = array[0].ToString(this.Workbook.FormulaUtil, 0, 0, r1C1ReferenceMode);
					return this.Workbook.Worksheets[0].AllocatedRange[name];
					IL_148:
					return null;
				}
				}
			}
			set
			{
				int a_ = 14;
				int num = 5;
				for (;;)
				{
					XlsRange xlsRange;
					switch (num)
					{
					case 0:
						if (!this.Workbook.Allow3DRangesInDataValidation)
						{
							num = 3;
							continue;
						}
						goto IL_6D;
					case 1:
						if (value.Worksheet == this.Worksheet)
						{
							num = 4;
							continue;
						}
						this.Formula1 = xlsRange.RangeGlobalAddress;
						num = 7;
						continue;
					case 2:
						goto IL_68;
					case 3:
						num = 9;
						continue;
					case 4:
						if (true)
						{
						}
						this.Formula1 = xlsRange.RangeGlobalAddressWithoutSheetName;
						goto IL_118;
					case 6:
						goto IL_123;
					case 7:
						goto IL_EE;
					case 8:
						goto IL_14C;
					case 9:
						if (value.Worksheet != this.Worksheet)
						{
							num = 8;
							continue;
						}
						goto IL_6D;
					}
					if (value != null)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_118;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					IL_6D:
					xlsRange = (XlsRange)value;
					num = 1;
					continue;
					IL_118:
					num = 6;
				}
				IL_68:
				throw new ArgumentNullException(RecordTableEnumerator.b("C❅㱇⭉ṋ⽍㹏㕑ㅓ", a_));
				IL_EE:
				IL_123:
				goto IL_151;
				IL_14C:
				throw new ArgumentException(RecordTableEnumerator.b("C❅㱇⭉汋㱍ㅏ㱑㍓㍕硗⥙㑛ㅝᕟ๡c䙥੧ཀྵ䱫࡭ɯᵱᥳ噵୷᭹ᅻ᭽ꁿ黎", a_));
				IL_151:
				this.ᜀ(this.Formula1, new FormulaUtil(this.ᜈ.Workbook.AppImplementation, this.ᜈ.Workbook, NumberFormatInfo.InvariantInfo, ',', ';'), true);
				this.Formula2 = "";
				this.CompareOperator = ValidationComparisonOperator.NotEqual;
				this.AllowType = CellDataType.User;
				this.ShowInput = true;
				this.ShowError = true;
				this.IsListInFormula = false;
				this.IgnoreBlank = true;
				this.IsSuppressDropDownArrow = false;
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x000FBFD0 File Offset: 0x000FAFD0
		// (set) Token: 0x06001D8D RID: 7565 RVA: 0x000FC018 File Offset: 0x000FB018
		public Ptg[] FirstFormulaTokens
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
				return this.ᜅ.\u1713();
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
				this.ᜅ.ᜁ(value);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x000FC060 File Offset: 0x000FB060
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x000FC0A8 File Offset: 0x000FB0A8
		public Ptg[] SecondFormulaTokens
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
				return this.ᜅ.\u1714();
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
				this.ᜅ.ᜀ(value);
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x000FC0F0 File Offset: 0x000FB0F0
		private void ᜁ()
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					XlsWorkbook xlsWorkbook = this.Workbook;
					FormulaUtil formulaUtil = xlsWorkbook.FormulaUtil;
					Ptg[] array = this.ᜅ.\u1713();
					int a_;
					int a_2;
					this.ᜀ(out a_, out a_2);
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_63;
							default:
								goto IL_A9;
							}
							break;
						case 1:
							num = 3;
							continue;
						case 2:
							this.ᜆ = formulaUtil.ᜀ(array, a_, a_2, false, false);
							num = 0;
							continue;
						case 3:
							if (array.Length > 0)
							{
								num = 2;
								continue;
							}
							return;
						case 4:
							if (array != null)
							{
								goto IL_63;
							}
							return;
						}
						break;
						IL_63:
						num = 1;
					}
				}
				IL_A9:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x000FC1D0 File Offset: 0x000FB1D0
		private void ᜀ()
		{
			switch (0)
			{
			default:
				if (true)
				{
				}
				for (;;)
				{
					XlsWorkbook xlsWorkbook = this.Workbook;
					FormulaUtil formulaUtil = xlsWorkbook.FormulaUtil;
					Ptg[] array = this.ᜅ.\u1714();
					int a_;
					int a_2;
					this.ᜀ(out a_, out a_2);
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜇ = formulaUtil.ᜀ(array, a_, a_2, false, false);
							num = 1;
							continue;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_63;
							default:
								goto IL_A9;
							}
							break;
						case 2:
							num = 3;
							continue;
						case 3:
							if (array.Length > 0)
							{
								num = 0;
								continue;
							}
							return;
						case 4:
							if (array != null)
							{
								goto IL_63;
							}
							return;
						}
						break;
						IL_63:
						num = 2;
					}
				}
				IL_A9:
				if (false)
				{
				}
				return;
			}
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x000FC2B0 File Offset: 0x000FB2B0
		private void ᜀ(out int A_0, out int A_1)
		{
			for (;;)
			{
				A_0 = 0;
				A_1 = 0;
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_3D;
					case 1:
						if (this.ᜉ.ᜂ().Count > 0)
						{
							num = 3;
							continue;
						}
						goto IL_C5;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3D;
						default:
							goto IL_85;
						}
						break;
					case 3:
					{
						Rectangle rectangle = this.ᜉ.ᜂ()[0];
						A_0 = rectangle.Top + 1;
						A_1 = rectangle.Left + 1;
						num = 2;
						continue;
					}
					case 4:
						if (this.ᜉ != null)
						{
							num = 0;
							continue;
						}
						goto IL_C5;
					}
					break;
					IL_3D:
					num = 1;
				}
			}
			IL_85:
			if (false)
			{
			}
			IL_C5:
			if (true)
			{
			}
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000FC38C File Offset: 0x000FB38C
		public void Reparse()
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
			this.ᜁ();
			this.ᜀ();
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x000FC3D4 File Offset: 0x000FB3D4
		public string GetFirstSecondFormula(FormulaUtil formulaUtil, bool bIsFirstFormula)
		{
			int num = 6;
			Ptg[] array2;
			Rectangle rectangle2;
			for (;;)
			{
				Ptg[] array;
				List<Rectangle> list;
				Rectangle rectangle;
				switch (num)
				{
				case 0:
					array = this.ᜅ.\u1714();
					goto IL_E2;
				case 1:
					num = 0;
					continue;
				case 2:
					if (list.Count <= 0)
					{
						num = 9;
						continue;
					}
					if (true)
					{
					}
					num = 7;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C7;
					default:
						goto IL_A2;
					}
					break;
				case 4:
					if (array2 == null)
					{
						num = 3;
						continue;
					}
					goto IL_116;
				case 5:
					array = this.ᜅ.\u1713();
					goto IL_E2;
				case 7:
					rectangle = list[0];
					goto IL_C6;
				case 8:
					rectangle = Rectangle.Empty;
					goto IL_C6;
				case 9:
					num = 8;
					continue;
				}
				if (!bIsFirstFormula)
				{
					num = 1;
					continue;
				}
				num = 5;
				continue;
				IL_C7:
				num = 4;
				continue;
				IL_C6:
				rectangle2 = rectangle;
				goto IL_C7;
				IL_E2:
				array2 = array;
				list = this.ᜉ.ᜂ();
				num = 2;
			}
			IL_A2:
			if (false)
			{
			}
			return string.Empty;
			IL_116:
			return formulaUtil.ᜀ(array2, rectangle2.Top + 1, rectangle2.Left + 1, false, false);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x000FC514 File Offset: 0x000FB514
		[CLSCompliant(false)]
		internal void ᜀ(RecordArrayList A_0)
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
			this.ᜂ();
			A_0.ᜀ(this.ᜅ);
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x000FC564 File Offset: 0x000FB564
		public object Clone(object parent)
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
			XlsValidation xlsValidation = (XlsValidation)base.MemberwiseClone();
			xlsValidation.ᜀ(parent as XlsDataValidationCollection);
			xlsValidation.ᜅ = (sprᡣ)spr\u1CD3.ᜀ(this.ᜅ);
			xlsValidation.ᜉ = this.ᜉ.ᜀ();
			return xlsValidation;
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x000FC5E0 File Offset: 0x000FB5E0
		private void ᜀ(XlsDataValidationCollection A_0)
		{
			int a_ = 16;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				if (A_0 == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("≅⥇㹉ⵋᡍㅏ㹑㵓㉕㥗⹙㕛ㅝ๟Ⅱୣ੥ѧཀྵཫᩭ᥯ᵱᩳ", a_));
				}
				break;
			}
			this.ᜈ = A_0;
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x000FC644 File Offset: 0x000FB644
		public bool ContainsCell(long lCellIndex)
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
			int num = sprṔ.ᜁ(lCellIndex);
			int num2 = sprṔ.ᜀ(lCellIndex);
			Rectangle a_ = Rectangle.FromLTRB(num2 - 1, num - 1, num2 - 1, num - 1);
			return this.ᜉ.ᜃ(a_);
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x000FC6AC File Offset: 0x000FB6AC
		public void UpdateNamedRangeIndexes(int[] arrNewIndex)
		{
			int a_ = 13;
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (arrNewIndex == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("≂㝄㕆݈⹊㩌َ㽐㝒ご⽖", a_));
				}
				break;
			}
			FormulaUtil formulaUtil = this.Workbook.FormulaUtil;
			formulaUtil.ᜁ(this.ᜅ.\u1713(), arrNewIndex);
			formulaUtil.ᜁ(this.ᜅ.\u1714(), arrNewIndex);
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x000FC73C File Offset: 0x000FB73C
		public void UpdateNamedRangeIndexes(IDictionary<int, int> dicNewIndex)
		{
			int a_ = 1;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (dicNewIndex == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("匶倸堺猼娾㙀ੂ⭄⍆ⱈ㍊", a_));
				}
				break;
			}
			FormulaUtil formulaUtil = this.Workbook.FormulaUtil;
			formulaUtil.ᜀ(this.ᜅ.\u1713(), dicNewIndex);
			formulaUtil.ᜀ(this.ᜅ.\u1714(), dicNewIndex);
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x000FC7CC File Offset: 0x000FB7CC
		public void BeginUpdate()
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
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x000FC808 File Offset: 0x000FB808
		public void EndUpdate()
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
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000FC844 File Offset: 0x000FB844
		public void MarkUsedReferences(bool[] usedItems)
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
			FormulaUtil.ᜀ(this.ᜅ.\u1713(), usedItems);
			FormulaUtil.ᜀ(this.ᜅ.\u1714(), usedItems);
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x000FC8A4 File Offset: 0x000FB8A4
		public void UpdateReferenceIndexes(int[] arrUpdatedIndexes)
		{
			for (;;)
			{
				Ptg[] a_ = this.ᜅ.\u1713();
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_47;
					case 1:
						goto IL_5B;
					case 2:
						goto IL_65;
					case 3:
						this.ᜅ.ᜁ(a_);
						num = 2;
						continue;
					case 4:
						if (FormulaUtil.ᜀ(a_, arrUpdatedIndexes))
						{
							num = 3;
							continue;
						}
						goto IL_65;
					case 5:
						if (FormulaUtil.ᜀ(a_, arrUpdatedIndexes))
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
					IL_47:
					this.ᜅ.ᜀ(a_);
					num = 1;
					continue;
					IL_65:
					a_ = this.ᜅ.\u1714();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_47;
					default:
						if (false)
						{
						}
						num = 5;
						break;
					}
				}
			}
			IL_5B:
			if (true)
			{
			}
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x000FC984 File Offset: 0x000FB984
		internal XlsValidation ᜀ(XlsDataValidationCollection A_0, int A_1, int A_2, int A_3, int A_4, int A_5, int A_6)
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
			XlsValidation xlsValidation = (XlsValidation)this.Clone(this.ᜈ);
			XlsWorkbook xlsWorkbook = xlsValidation.Workbook;
			Rectangle[] rectangles = new Rectangle[]
			{
				Rectangle.FromLTRB(0, 0, xlsWorkbook.MaxColumnCount - 1, A_1 - 2),
				Rectangle.FromLTRB(0, A_1 - 1, A_2 - 2, A_1 + A_5 - 1),
				Rectangle.FromLTRB(0, A_1 + A_5 - 1, xlsWorkbook.MaxColumnCount - 1, xlsWorkbook.MaxRowCount - 1),
				Rectangle.FromLTRB(A_2 + A_6 - 1, A_1 - 1, xlsWorkbook.MaxColumnCount - 1, A_1 + A_5 - 1)
			};
			xlsValidation.RemoveRange(rectangles);
			xlsValidation.ᜉ.ᜀ(A_3, A_4, xlsValidation.ᜈ.Workbook);
			return xlsValidation;
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x000FCA90 File Offset: 0x000FBA90
		private void ᜀ(string A_0, string A_1, int A_2)
		{
			int a_ = 5;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (A_1.Length > A_2)
					{
						num = 2;
						continue;
					}
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C;
					default:
						goto IL_8F;
					}
					break;
				}
				goto IL_29;
				IL_2C:
				num = 0;
				continue;
				IL_29:
				if (A_1 != null)
				{
					goto IL_2C;
				}
				return;
			}
			IL_8F:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(string.Format(RecordTableEnumerator.b("䀺഼䈾慀⁂⑄⥆❈⑊㥌潎㑐⭒㙔㉖㱘㽚絜⑞偠Ṣ䕤ѦŨ੪Ὤ๮ተݲၴն੸啺", a_), A_0, A_2));
		}

		// Token: 0x040010A5 RID: 4261
		private const int ᜀ = 256;

		// Token: 0x040010A6 RID: 4262
		private long[] \u2460\u008B\u0096ª;

		// Token: 0x040010A7 RID: 4263
		private const int ᜁ = 32;

		// Token: 0x040010A8 RID: 4264
		private const int ᜂ = 225;

		// Token: 0x040010A9 RID: 4265
		private byte[] \u2593\u00B0\u00A9\u00A7;

		// Token: 0x040010AA RID: 4266
		private const int ᜃ = 255;

		// Token: 0x040010AB RID: 4267
		private readonly Type[] ᜄ;

		// Token: 0x040010AC RID: 4268
		private sprᡣ ᜅ;

		// Token: 0x040010AD RID: 4269
		private string ᜆ;

		// Token: 0x040010AE RID: 4270
		private string ᜇ;

		// Token: 0x040010AF RID: 4271
		private XlsDataValidationCollection ᜈ;

		// Token: 0x040010B0 RID: 4272
		private spr\u2530 ᜉ;

		// Token: 0x040010B1 RID: 4273
		private FormulaUtil ᜊ;
	}
}
