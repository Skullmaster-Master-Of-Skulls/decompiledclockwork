using System;
using System.Collections;
using System.Collections.Generic;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000017 RID: 23
	public class XlsStylesCollection : CollectionExtended<IStyle>, IStyles
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00005FA8 File Offset: 0x00004FA8
		internal XlsStylesCollection(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 9;
			this.ᜀ = new Dictionary<XlsStyle, XlsStyle>();
			base..ctor(A_0, A_1);
			this.ᜁ = new Dictionary<string, XlsStyle>();
			object obj = base.FindParent(typeof(XlsWorkbook));
			if (obj == null)
			{
				throw new ArgumentException(RecordTableEnumerator.b("氾㕀㩂⥄≆楈⡊≌⍎㵐㙒㙔⍖じ㑚㍜罞ౠᙢᙤ፦䥨४࡬佮ᡰᵲ啴⁶ᙸॺᙼᵾꞆ떔ﺚ놞", a_));
			}
			this.ᜃ = (XlsWorkbook)obj;
			this.ᜄ = new EventHandler(this.ᜁ);
			this.ᜅ = new EventHandler(this.ᜀ);
		}

		// Token: 0x1700007F RID: 127
		public IStyle this[string name]
		{
			get
			{
				int a_ = 11;
				XlsStyle result;
				if (!this.ᜁ.TryGetValue(name, out result))
				{
					for (;;)
					{
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_43;
						}
					}
					IL_43:
					if (false)
					{
					}
					throw new ArgumentException(RecordTableEnumerator.b("ቀ㝂㱄⭆ⱈ歊⍌⹎㱐㙒畔㑖㡘㕚㍜ぞᕠ䍢ݤɦ䥨൪ɬᩮὰᝲ孴坶㝸᩺ၼ᩾뮀ꎂ", a_) + name, RecordTableEnumerator.b("㝀≂⥄㉆ⱈ", a_));
				}
				return result;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000060B8 File Offset: 0x000050B8
		protected internal IStyle Add(string name, object BasedOn)
		{
			int a_ = 14;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_DA;
				case 1:
				{
					if (this.ᜁ.ContainsKey(name))
					{
						num = 3;
						continue;
					}
					IStyle style = null;
					num = 8;
					continue;
				}
				case 2:
				{
					IStyle style = base.AppImplementation.ᜀ(this.ᜃ, name);
					num = 9;
					continue;
				}
				case 3:
					goto IL_D5;
				case 4:
					if (BasedOn is XlsStyle)
					{
						num = 15;
						continue;
					}
					goto IL_DA;
				case 5:
					goto IL_DA;
				case 6:
				{
					IStyle style;
					if (style != null)
					{
						num = 14;
						continue;
					}
					return style;
				}
				case 7:
					if (BasedOn is string)
					{
						num = 12;
						continue;
					}
					num = 4;
					continue;
				case 8:
					if (BasedOn == null)
					{
						num = 2;
						continue;
					}
					num = 7;
					continue;
				case 9:
					goto IL_DA;
				case 11:
				{
					IStyle style;
					return style;
				}
				case 12:
				{
					if (true)
					{
					}
					IStyle style = base.AppImplementation.ᜀ(this.ᜃ, name, (XlsStyle)this[(string)BasedOn]);
					goto IL_1CA;
				}
				case 13:
					goto IL_64;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1CA;
					default:
					{
						if (false)
						{
						}
						IStyle style;
						base.Add(style);
						num = 11;
						continue;
					}
					}
					break;
				case 15:
				{
					IStyle style = base.AppImplementation.ᜀ(this.ᜃ, name, (XlsStyle)BasedOn);
					num = 0;
					continue;
				}
				}
				if (name == null)
				{
					num = 13;
					continue;
				}
				num = 1;
				continue;
				IL_DA:
				num = 6;
				continue;
				IL_1CA:
				num = 5;
			}
			IL_64:
			throw new ArgumentNullException(RecordTableEnumerator.b("⩃❅╇⽉", a_));
			IL_D5:
			throw new ArgumentException(RecordTableEnumerator.b("C㍅㡇♉╋ⵍㅏ♑ㅓ㉕硗⥙⡛❝౟ݡ䑣ࡥ१ݩ५乭ᡯ፱ݳ噵᩷ό᥻ၽꁿꊋ", a_));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000062C4 File Offset: 0x000052C4
		protected internal IStyle Add(string name)
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
			return this.Add(name, null);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006308 File Offset: 0x00005308
		protected internal IStyles Merge(object Workbook)
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
			return this.Merge(Workbook, false);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000634C File Offset: 0x0000534C
		protected internal IStyles Merge(object Workbook, bool overwrite)
		{
			int a_ = 6;
			int num = 1;
			XlsWorkbook a_2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8C;
				case 2:
					goto IL_BC;
				case 3:
					if (!(Workbook is XlsWorkbook))
					{
						num = 0;
						continue;
					}
					a_2 = (XlsWorkbook)Workbook;
					goto IL_B1;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B1;
					default:
						goto IL_5A;
					}
					break;
				}
				if (Workbook == null)
				{
					num = 4;
					continue;
				}
				num = 3;
				continue;
				IL_B1:
				num = 2;
			}
			IL_5A:
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("欻儽㈿⥁♃⥅❇ⅉ", a_));
			IL_8C:
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("画倽㘿⍁⡃⽅ⱇ橉㭋⅍≏㥑㙓㥕㝗ㅙ牛", a_));
			IL_BC:
			this.ᜀ(a_2, overwrite ? StyleMergeType.Replace : StyleMergeType.Leave);
			return this;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000642C File Offset: 0x0000542C
		public void Remove(string styleName)
		{
			int num = 2;
			XlsStyle xlsStyle;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (xlsStyle.BuiltIn)
					{
						num = 1;
						continue;
					}
					goto IL_D4;
				case 1:
					goto IL_60;
				case 3:
					num = 5;
					continue;
				case 4:
					if (xlsStyle != null)
					{
						num = 7;
						continue;
					}
					return;
				case 5:
					if (styleName.Length == 0)
					{
						num = 6;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_60;
					default:
						if (false)
						{
						}
						this.ᜁ.TryGetValue(styleName, out xlsStyle);
						num = 4;
						continue;
					}
					break;
				case 6:
					goto IL_D0;
				case 7:
					num = 0;
					continue;
				}
				if (styleName == null)
				{
					return;
				}
				num = 3;
			}
			IL_60:
			return;
			IL_D0:
			return;
			IL_D4:
			int extendedFormatIndex = xlsStyle.ExtendedFormatIndex;
			this.ᜀ.Remove(xlsStyle);
			base.Remove(xlsStyle);
			this.ᜁ.Remove(styleName);
			XlsWorkbook workbook = xlsStyle.Workbook;
			workbook.RemoveExtenededFormatIndex(extendedFormatIndex);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006544 File Offset: 0x00005544
		protected internal new void Add(IStyle style)
		{
			int a_ = 7;
			switch (0)
			{
			default:
				for (;;)
				{
					for (;;)
					{
						string name = style.Name;
						int num = 11;
						for (;;)
						{
							bool flag;
							bool flag2;
							switch (num)
							{
							case 0:
								goto IL_1C3;
							case 1:
								if (!flag)
								{
									num = 12;
									continue;
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
									num = 4;
									continue;
								}
								break;
							case 2:
								num = 7;
								continue;
							case 3:
							{
								XlsStyle xlsStyle;
								XlsStyle xlsStyle2;
								if (xlsStyle.Index != xlsStyle2.Index)
								{
									num = 14;
									continue;
								}
								num = 5;
								continue;
							}
							case 4:
								num = 10;
								continue;
							case 5:
								flag2 = true;
								goto IL_209;
							case 6:
							{
								XlsStyle xlsStyle2;
								if (!xlsStyle2.BuiltIn)
								{
									num = 2;
									continue;
								}
								goto IL_246;
							}
							case 7:
								if (!this.ᜃ.Loading)
								{
									num = 8;
									continue;
								}
								goto IL_246;
							case 8:
								goto IL_10D;
							case 9:
								if (true)
								{
								}
								flag2 = false;
								goto IL_209;
							case 10:
							{
								XlsStyle xlsStyle;
								XlsStyle xlsStyle2;
								if (xlsStyle.BuiltIn == xlsStyle2.BuiltIn)
								{
									num = 0;
									continue;
								}
								goto IL_246;
							}
							case 11:
								if (this.ᜁ(name))
								{
									num = 15;
									continue;
								}
								goto IL_246;
							case 12:
							{
								XlsStyle xlsStyle;
								if (xlsStyle.BuiltIn)
								{
									num = 16;
									continue;
								}
								num = 6;
								continue;
							}
							case 13:
								goto IL_A7;
							case 14:
								num = 9;
								continue;
							case 15:
							{
								XlsStyle xlsStyle = (XlsStyle)style;
								XlsStyle xlsStyle2;
								this.ᜁ.TryGetValue(name, out xlsStyle2);
								num = 3;
								continue;
							}
							case 16:
							{
								XlsStyle xlsStyle;
								this.ᜁ[name] = xlsStyle;
								num = 13;
								continue;
							}
							}
							break;
							IL_209:
							flag = flag2;
							num = 1;
						}
					}
				}
				IL_A7:
				goto IL_246;
				IL_10D:
				throw new ArgumentException(string.Format(RecordTableEnumerator.b("礼䨾ㅀ⽂ⱄ⑆⡈㽊⡌⭎煐⁒⅔⹖㕘㹚絜ㅞ`๢d䝦Ũ੪Ṭ佮፰ᙲၴ᥶奸ᵺቼ੾ꖄ붆", a_), style.Name));
				IL_1C3:
				throw new ArgumentException(string.Format(RecordTableEnumerator.b("礼䨾ㅀ⽂ⱄ⑆⡈㽊⡌⭎煐⁒⅔⹖㕘㹚絜ㅞ`๢d䝦Ũ੪Ṭ佮፰ᙲၴ᥶奸ᵺቼ੾ꮄ", a_), style.Name));
				IL_246:
				base.Add(style);
				return;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000067A0 File Offset: 0x000057A0
		protected internal void Add(IStyle style, bool bReplace)
		{
			int num = 8;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_13C;
				case 1:
					return;
				case 2:
					if (base.List[num2].Name == style.Name)
					{
						num = 0;
						continue;
					}
					num2++;
					num = 6;
					continue;
				case 3:
				{
					num2 = 0;
					int count = base.List.Count;
					num = 5;
					continue;
				}
				case 4:
				{
					int count;
					if (num2 >= count)
					{
						num = 1;
						continue;
					}
					num = 2;
					continue;
				}
				case 5:
					goto IL_70;
				case 6:
					goto IL_70;
				case 7:
					goto IL_D9;
				case 9:
					if (bReplace)
					{
						num = 3;
						continue;
					}
					return;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_70;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				}
				if (this.ᜁ(style.Name))
				{
					num = 10;
					continue;
				}
				base.Add(style);
				if (true)
				{
				}
				num = 7;
				continue;
				IL_70:
				num = 4;
			}
			return;
			IL_D9:
			return;
			IL_13C:
			base.List[num2] = style;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000068EC File Offset: 0x000058EC
		public bool Contains(string name)
		{
			int a_ = 15;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (name.Length == 0)
					{
						goto IL_7E;
					}
					goto IL_A6;
				case 1:
					goto IL_58;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7E;
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
				case 3:
					goto IL_86;
				}
				if (name == null)
				{
					num = 1;
					continue;
				}
				num = 0;
				continue;
				IL_7E:
				num = 3;
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("⭄♆⑈⹊", a_));
			IL_86:
			throw new ArgumentException(RecordTableEnumerator.b("⭄♆⑈⹊浌ⱎぐ㵒㭔㡖ⵘ筚㽜㩞䅠٢ࡤᝦᵨቪ䍬", a_));
			IL_A6:
			return this.ᜁ(name);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000069A8 File Offset: 0x000059A8
		protected internal IStyle ContainsSameStyle(IStyle style)
		{
			int a_ = 9;
			while (style != null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					XlsStyle xlsStyle = style as XlsStyle;
					xlsStyle.NotCompareNames = true;
					style = this.ᜀ[xlsStyle];
					xlsStyle.NotCompareNames = false;
					return style;
				}
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾㕀㩂⥄≆", a_));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006A2C File Offset: 0x00005A2C
		protected internal static bool CompareStyles(IStyle source, IStyle destination)
		{
			int num = 19;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (source.WrapText == destination.WrapText)
					{
						num = 5;
						continue;
					}
					goto IL_2AD;
				case 1:
					if (source.Font.Equals(destination.Font))
					{
						num = 28;
						continue;
					}
					goto IL_2AD;
				case 2:
					num = 11;
					continue;
				case 3:
					if (source.IncludeProtection == destination.IncludeProtection)
					{
						num = 9;
						continue;
					}
					goto IL_2AD;
				case 4:
					num = 7;
					continue;
				case 5:
					num = 1;
					continue;
				case 6:
					num = 32;
					continue;
				case 7:
					if (source.PatternColor == destination.PatternColor)
					{
						num = 24;
						continue;
					}
					goto IL_2AD;
				case 8:
					if (source.IncludeNumberFormat == destination.IncludeNumberFormat)
					{
						num = 14;
						continue;
					}
					goto IL_2AD;
				case 9:
					num = 21;
					continue;
				case 10:
					num = 36;
					continue;
				case 11:
					if (source.HorizontalAlignment == destination.HorizontalAlignment)
					{
						num = 10;
						continue;
					}
					goto IL_2AD;
				case 12:
					num = 23;
					continue;
				case 13:
					num = 34;
					continue;
				case 14:
					num = 37;
					continue;
				case 15:
					if (source.FillPattern == destination.FillPattern)
					{
						num = 33;
						continue;
					}
					goto IL_2AD;
				case 16:
					num = 8;
					continue;
				case 17:
					goto IL_21C;
				case 18:
					num = 26;
					continue;
				case 20:
					num = 17;
					continue;
				case 21:
					if (source.IndentLevel == destination.IndentLevel)
					{
						goto IL_3BA;
					}
					goto IL_2AD;
				case 22:
					if (source.NumberFormat == destination.NumberFormat)
					{
						num = 6;
						continue;
					}
					goto IL_2AD;
				case 23:
					if (XlsStylesCollection.CompareBorders(source.Borders, destination.Borders))
					{
						num = 18;
						continue;
					}
					goto IL_2AD;
				case 24:
					num = 15;
					continue;
				case 25:
					if (source.IncludeAlignment == destination.IncludeAlignment)
					{
						num = 13;
						continue;
					}
					goto IL_2AD;
				case 26:
					if (source.Locked == destination.Locked)
					{
						num = 20;
						continue;
					}
					goto IL_2AD;
				case 27:
					num = 0;
					continue;
				case 28:
					num = 25;
					continue;
				case 29:
					num = 31;
					continue;
				case 30:
					goto IL_2B8;
				case 31:
					if (source.IncludeFont == destination.IncludeFont)
					{
						num = 16;
						continue;
					}
					goto IL_2AD;
				case 32:
					if (source.FormulaHidden == destination.FormulaHidden)
					{
						num = 2;
						continue;
					}
					goto IL_2AD;
				case 33:
					num = 22;
					continue;
				case 34:
					if (source.IncludeBorder == destination.IncludeBorder)
					{
						num = 29;
						continue;
					}
					goto IL_2AD;
				case 35:
					num = 3;
					continue;
				case 36:
					if (source.VerticalAlignment == destination.VerticalAlignment)
					{
						num = 27;
						continue;
					}
					goto IL_2AD;
				case 37:
					if (source.IncludePatterns == destination.IncludePatterns)
					{
						num = 35;
						continue;
					}
					goto IL_2AD;
				}
				if (source.Color == destination.Color)
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3BA;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
				}
				IL_2AD:
				num = 30;
				continue;
				IL_3BA:
				num = 12;
			}
			IL_21C:
			return source.ShrinkToFit == destination.ShrinkToFit;
			IL_2B8:
			return false;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006E60 File Offset: 0x00005E60
		protected internal static bool CompareBorders(IBorders source, IBorders destination)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (XlsStylesCollection.CompareBorder(source[BordersLineType.DiagonalDown], destination[BordersLineType.DiagonalDown]))
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					return false;
				case 1:
					if (XlsStylesCollection.CompareBorder(source[BordersLineType.EdgeLeft], destination[BordersLineType.EdgeLeft]))
					{
						num = 8;
						continue;
					}
					return false;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C8;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 4:
					num = 0;
					continue;
				case 5:
					goto IL_92;
				case 6:
					if (XlsStylesCollection.CompareBorder(source[BordersLineType.EdgeRight], destination[BordersLineType.EdgeRight]))
					{
						num = 2;
						continue;
					}
					return false;
				case 7:
					num = 1;
					continue;
				case 8:
					num = 6;
					continue;
				case 9:
					goto IL_C8;
				}
				if (XlsStylesCollection.CompareBorder(source[BordersLineType.EdgeBottom], destination[BordersLineType.EdgeBottom]))
				{
					num = 7;
					continue;
				}
				return false;
				IL_C8:
				if (!XlsStylesCollection.CompareBorder(source[BordersLineType.EdgeTop], destination[BordersLineType.EdgeTop]))
				{
					return false;
				}
				num = 4;
			}
			IL_92:
			return XlsStylesCollection.CompareBorder(source[BordersLineType.DiagonalUp], destination[BordersLineType.DiagonalUp]);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006FD4 File Offset: 0x00005FD4
		protected internal static bool CompareBorder(IBorder source, IBorder destination)
		{
			for (;;)
			{
				IL_00:
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9B;
					case 2:
						num = 3;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							if (source.LineStyle == destination.LineStyle)
							{
								num = 0;
								continue;
							}
							return false;
						}
						break;
					}
					if (true)
					{
					}
					if (!(source.OColor == destination.OColor))
					{
						return false;
					}
					num = 2;
				}
			}
			IL_9B:
			return source.ShowDiagonalLine == destination.ShowDiagonalLine;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00007080 File Offset: 0x00006080
		internal new Dictionary<string, string> ᜀ(IWorkbook A_0, StyleMergeType A_1)
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
			Dictionary<int, int> dictionary;
			Dictionary<int, int> dictionary2;
			return this.ᜀ(A_0, A_1, out dictionary, out dictionary2);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000070C8 File Offset: 0x000060C8
		internal new Dictionary<string, string> ᜀ(IWorkbook A_0, StyleMergeType A_1, out Dictionary<int, int> A_2, out Dictionary<int, int> A_3)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				int num = 8;
				for (;;)
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
						XlsWorkbook xlsWorkbook;
						int num2;
						switch (num)
						{
						case 0:
							goto IL_177;
						case 1:
							goto IL_1B2;
						case 2:
							num = 14;
							continue;
						case 3:
						{
							if (xlsWorkbook == this.ᜃ)
							{
								num = 0;
								continue;
							}
							XlsStylesCollection xlsStylesCollection = xlsWorkbook.InnerStyles;
							Dictionary<string, string> dictionary = new Dictionary<string, string>();
							A_3 = this.ᜃ.InnerExtFormats.ᜀ(xlsWorkbook.InnerExtFormats, out A_2);
							num2 = 0;
							int count = xlsStylesCollection.Count;
							num = 1;
							continue;
						}
						case 4:
							goto IL_17C;
						case 5:
							goto IL_23A;
						case 6:
							goto IL_17C;
						case 7:
						{
							Dictionary<string, string> dictionary;
							return dictionary;
						}
						case 9:
						{
							int count;
							if (num2 >= count)
							{
								num = 7;
								continue;
							}
							XlsStylesCollection xlsStylesCollection;
							XlsStyle xlsStyle = xlsStylesCollection[num2] as XlsStyle;
							string text = xlsStyle.Name;
							bool flag = this.ᜁ.ContainsKey(text);
							num = 13;
							continue;
						}
						case 10:
						{
							XlsStyle xlsStyle;
							ICloneable cloneable = xlsStyle.Record;
							sprᬐ sprᬐ = (sprᬐ)cloneable.Clone();
							int key = (int)sprᬐ.ᜅ();
							sprᬐ.ᜀ((ushort)A_3[key]);
							string text;
							sprᬐ.ᜀ(text);
							this.ᜀ(sprᬐ);
							num = 16;
							continue;
						}
						case 11:
							goto IL_A1;
						case 12:
						{
							if (true)
							{
							}
							string text;
							if (text != null)
							{
								num = 10;
								continue;
							}
							goto IL_22A;
						}
						case 13:
						{
							bool flag;
							if (flag)
							{
								num = 2;
								continue;
							}
							goto IL_17C;
						}
						case 14:
							switch (A_1)
							{
							case StyleMergeType.Leave:
							{
								string text = null;
								num = 6;
								continue;
							}
							case StyleMergeType.Replace:
								goto IL_17C;
							case StyleMergeType.CreateDiffName:
							{
								string text = CollectionExtended<IStyle>.GenerateDefaultName(this, text + RecordTableEnumerator.b("挻", a_));
								Dictionary<string, string> dictionary;
								dictionary.Add(text, text);
								num = 4;
								continue;
							}
							default:
								num = 17;
								continue;
							}
							break;
						case 15:
							goto IL_1E3;
						case 16:
							goto IL_22A;
						case 17:
							num = 15;
							continue;
						}
						if (!(A_0 is XlsWorkbook))
						{
							num = 11;
							continue;
						}
						xlsWorkbook = (XlsWorkbook)A_0;
						A_3 = null;
						A_2 = null;
						num = 3;
						continue;
						IL_17C:
						num = 12;
						continue;
						IL_22A:
						num2++;
						num = 5;
						continue;
					}
					}
					IL_1B2:
					num = 9;
					continue;
					IL_23A:
					goto IL_1B2;
				}
				IL_A1:
				throw new ArgumentException(RecordTableEnumerator.b("画倽㘿⍁⡃⽅ⱇ橉㭋⅍≏㥑㙓㥕㝗ㅙ牛", a_));
				IL_177:
				return null;
				IL_1E3:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("椻倽⬿ⱁ⭃ㅅ♇橉⍋㹍⑏㭑㭓㡕", a_));
			}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000073B8 File Offset: 0x000063B8
		protected internal string GenerateDefaultName(string strStart)
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
			return CollectionExtended<IStyle>.GenerateDefaultName(strStart, new ICollection[]
			{
				this.ᜁ.Values
			});
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007410 File Offset: 0x00006410
		internal new string ᜀ(string A_0, Dictionary<string, sprᬐ> A_1)
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
			return CollectionExtended<IStyle>.GenerateDefaultName(A_0, new ICollection[]
			{
				this.ᜁ.Values,
				A_1.Keys
			});
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007470 File Offset: 0x00006470
		protected internal XlsStyle CreateBuiltInStyle(string styleName)
		{
			int a_ = 14;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_58;
				case 1:
					goto IL_86;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7E;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					if (styleName.Length == 0)
					{
						goto IL_7E;
					}
					goto IL_A6;
				}
				if (true)
				{
				}
				if (styleName == null)
				{
					num = 0;
					continue;
				}
				num = 3;
				continue;
				IL_7E:
				num = 1;
			}
			IL_58:
			throw new ArgumentNullException(RecordTableEnumerator.b("㝃㉅ㅇ♉⥋Mㅏ㽑ㅓ", a_));
			IL_86:
			throw new ArgumentException(RecordTableEnumerator.b("㝃㉅ㅇ♉⥋Mㅏ㽑ㅓ癕㭗㭙㉛そཟᙡ䑣ѥ൧䩩५ͭoٱ൳", a_));
			IL_A6:
			XlsStyle xlsStyle = base.AppImplementation.ᜀ(this.ᜃ, styleName, true);
			base.Add(xlsStyle);
			return xlsStyle;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007540 File Offset: 0x00006540
		protected internal XlsStyle GetByXFIndex(int index)
		{
			int num = 5;
			XlsStyle xlsStyle;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_C4;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4D;
					default:
						if (false)
						{
						}
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						xlsStyle = (base[num2] as XlsStyle);
						num = 4;
						continue;
					}
					break;
				case 2:
					if (this.ᜂ.ContainsKey(index))
					{
						num = 8;
						continue;
					}
					goto IL_FD;
				case 3:
					goto IL_8E;
				case 4:
					if (xlsStyle.Index == index)
					{
						num = 9;
						continue;
					}
					num2++;
					num = 3;
					continue;
				case 6:
					num = 2;
					continue;
				case 7:
					goto IL_8E;
				case 8:
					goto IL_EA;
				case 9:
					return xlsStyle;
				}
				goto IL_42;
				IL_4D:
				num = 6;
				continue;
				IL_42:
				if (this.ᜂ != null)
				{
					goto IL_4D;
				}
				goto IL_FD;
				IL_8E:
				num = 1;
				continue;
				IL_FD:
				num2 = 0;
				count = base.Count;
				num = 7;
			}
			return xlsStyle;
			IL_C4:
			if (true)
			{
			}
			return null;
			IL_EA:
			return this.ᜂ[index];
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000766C File Offset: 0x0000666C
		public void UpdateStyleRecords()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_4B:
					List<IStyle> innerList = base.InnerList;
					int num = 0;
					int count = innerList.Count;
					int num2 = 2;
					for (;;)
					{
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
							switch (num2)
							{
							case 0:
								goto IL_A1;
							case 1:
								return;
							case 2:
								goto IL_66;
							case 3:
							{
								if (num >= count)
								{
									num2 = 1;
									continue;
								}
								XlsStyle xlsStyle = (XlsStyle)innerList[num];
								xlsStyle.UpdateStyleRecord();
								num++;
								num2 = 0;
								continue;
							}
							}
							goto IL_4B;
						}
						IL_66:
						num2 = 3;
						continue;
						IL_A1:
						goto IL_66;
					}
				}
				return;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00007728 File Offset: 0x00006728
		internal new IStyle ᜀ(string A_0)
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
			return this[A_0];
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000776C File Offset: 0x0000676C
		internal new bool ᜁ(string A_0)
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
			return this.ᜁ.ContainsKey(A_0);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000077B4 File Offset: 0x000067B4
		internal new void ᜀ(sprᬐ A_0)
		{
			int a_ = 0;
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
				{
					if (false)
					{
					}
					XlsStyle style = base.AppImplementation.ᜀ(this.ᜃ, A_0);
					this.Add(style);
					return;
				}
				}
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("䔵䰷䌹倻嬽", a_));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000782C File Offset: 0x0000682C
		public override object Clone(object parent)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					XlsStylesCollection xlsStylesCollection;
					int num2;
					int count;
					List<IStyle> innerList;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_6D;
						}
						break;
					case 1:
						goto IL_F8;
					case 2:
						goto IL_F8;
					case 3:
						return xlsStylesCollection;
					case 4:
					{
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						XlsStyle xlsStyle = (XlsStyle)innerList[num2];
						xlsStyle = (XlsStyle)xlsStyle.Clone(xlsStylesCollection);
						xlsStylesCollection.Add(xlsStyle);
						num2++;
						num = 1;
						continue;
					}
					}
					if (parent == null)
					{
						num = 0;
						continue;
					}
					xlsStylesCollection = new XlsStylesCollection((spr\u2158)base.ReservedHandle, parent);
					innerList = base.InnerList;
					num2 = 0;
					count = innerList.Count;
					num = 2;
					continue;
					IL_F8:
					num = 4;
				}
				IL_6D:
				if (false)
				{
				}
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("丽ℿぁ⅃⡅㱇", a_));
			}
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007954 File Offset: 0x00006954
		protected internal Dictionary<XlsStyle, XlsStyle> Map
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
				return this.ᜀ;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007998 File Offset: 0x00006998
		protected override void OnClearComplete()
		{
			for (;;)
			{
				this.ᜁ.Clear();
				this.ᜀ.Clear();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_60;
					case 1:
						goto IL_73;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_60;
						default:
							if (false)
							{
							}
							if (this.ᜂ != null)
							{
								num = 0;
								continue;
							}
							goto IL_87;
						}
						break;
					}
					break;
					IL_60:
					this.ᜂ.Clear();
					num = 1;
				}
			}
			IL_73:
			if (true)
			{
			}
			IL_87:
			base.OnClearComplete();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007A34 File Offset: 0x00006A34
		protected override void OnInsertComplete(int index, IStyle value)
		{
			XlsStyle xlsStyle;
			for (;;)
			{
				IL_20:
				string name = value.Name;
				for (;;)
				{
					IL_27:
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
								goto IL_27;
							default:
								if (false)
								{
								}
								this.ᜁ[value.Name] = (XlsStyle)value;
								num = 2;
								continue;
							}
							break;
						case 1:
							if (this.ᜂ != null)
							{
								num = 3;
								continue;
							}
							goto IL_DF;
						case 2:
							goto IL_94;
						case 3:
							this.ᜂ[xlsStyle.Index] = xlsStyle;
							num = 5;
							continue;
						case 4:
							if (!this.ᜁ.ContainsKey(name))
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							goto IL_94;
						case 5:
							goto IL_92;
						}
						goto IL_20;
						IL_94:
						xlsStyle = (XlsStyle)value;
						num = 1;
					}
				}
			}
			IL_92:
			IL_DF:
			this.ᜀ.Add(xlsStyle, xlsStyle);
			xlsStyle.BeforeChange += this.ᜄ;
			xlsStyle.AfterChange += this.ᜅ;
			base.OnInsertComplete(index, value);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007B50 File Offset: 0x00006B50
		protected override void OnRemoveComplete(int index, IStyle value)
		{
			XlsStyle xlsStyle;
			for (;;)
			{
				xlsStyle = (XlsStyle)value;
				this.ᜁ.Remove(xlsStyle.Name);
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
							goto IL_63;
						default:
							if (false)
							{
							}
							if (this.ᜂ != null)
							{
								num = 1;
								continue;
							}
							goto IL_94;
						}
						break;
					case 1:
						goto IL_63;
					case 2:
						goto IL_88;
					}
					break;
					IL_63:
					if (true)
					{
					}
					this.ᜂ.Remove(xlsStyle.Index);
					num = 2;
				}
			}
			IL_88:
			IL_94:
			this.ᜀ.Remove(xlsStyle);
			xlsStyle.BeforeChange -= this.ᜄ;
			xlsStyle.AfterChange -= this.ᜅ;
			base.OnRemoveComplete(index, value);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007C20 File Offset: 0x00006C20
		protected override void OnSetComplete(int index, IStyle oldValue, IStyle newValue)
		{
			int a_ = 9;
			XlsStyle xlsStyle2;
			for (;;)
			{
				XlsStyle xlsStyle = (XlsStyle)oldValue;
				xlsStyle2 = (XlsStyle)newValue;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜁ.ContainsKey(xlsStyle2.Name))
						{
							num = 2;
							continue;
						}
						goto IL_120;
					case 1:
						IL_11B:
						goto IL_64;
					case 2:
						goto IL_DF;
					case 3:
						if (this.ᜂ != null)
						{
							num = 4;
							continue;
						}
						goto IL_64;
					case 4:
						if (true)
						{
						}
						this.ᜂ.Remove(xlsStyle.Index);
						this.ᜂ[xlsStyle2.Index] = xlsStyle2;
						num = 1;
						continue;
					}
					break;
					IL_64:
					this.ᜁ.Remove(xlsStyle.Name);
					this.ᜀ.Remove(xlsStyle);
					this.ᜀ.Add(xlsStyle2, xlsStyle2);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11B;
					default:
						if (false)
						{
						}
						num = 0;
						break;
					}
				}
			}
			IL_DF:
			throw new ArgumentException(RecordTableEnumerator.b("笾㑀㍂⥄⹆⩈⩊㥌⩎㕐獒♔⍖⁘㝚㡜罞འɢࡤɦ䥨ͪ౬ᱮ兰ᅲၴቶ᝸孺᭼ၾꦆ", a_));
			IL_120:
			this.ᜁ[xlsStyle2.Name] = xlsStyle2;
			base.OnSetComplete(index, oldValue, newValue);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007D68 File Offset: 0x00006D68
		private new void ᜁ(object A_0, EventArgs A_1)
		{
			int a_ = 13;
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_8B;
				case 1:
					goto IL_3C;
				case 3:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					goto IL_A1;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					num = 3;
				}
			}
			IL_3C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_A1:
				XlsStyle xlsStyle = (XlsStyle)A_0;
				this.ᜀ.Remove(xlsStyle);
				xlsStyle.BeforeChange -= this.ᜄ;
				return;
			}
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("あ⁄⥆ⵈ⹊㽌", a_));
			}
			IL_8B:
			throw new ArgumentNullException(RecordTableEnumerator.b("≂㝄⁆㩈", a_));
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007E38 File Offset: 0x00006E38
		private new void ᜀ(object A_0, EventArgs A_1)
		{
			int a_ = 19;
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_8B;
				case 1:
					goto IL_3C;
				case 2:
					if (A_1 == null)
					{
						num = 0;
						continue;
					}
					goto IL_A1;
				}
				if (A_0 == null)
				{
					num = 1;
				}
				else
				{
					num = 2;
				}
			}
			IL_3C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
			{
				IL_A1:
				XlsStyle xlsStyle = (XlsStyle)A_0;
				this.ᜀ.Add(xlsStyle, xlsStyle);
				xlsStyle.BeforeChange += this.ᜄ;
				return;
			}
			default:
				if (false)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("㩈⹊⍌⭎㑐⅒", a_));
			}
			IL_8B:
			throw new ArgumentNullException(RecordTableEnumerator.b("⡈㥊⩌㱎", a_));
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007F08 File Offset: 0x00006F08
		internal new void ᜀ()
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
			this.ᜂ = null;
			this.ᜀ = null;
			this.ᜁ = null;
		}

		// Token: 0x04000049 RID: 73
		private new Dictionary<XlsStyle, XlsStyle> ᜀ;

		// Token: 0x0400004A RID: 74
		private new Dictionary<string, XlsStyle> ᜁ;

		// Token: 0x0400004B RID: 75
		private string \u25D8\u008D\u0099\u00A9;

		// Token: 0x0400004C RID: 76
		private string \u2609\u008C\u008B\u008D;

		// Token: 0x0400004D RID: 77
		private new Dictionary<int, XlsStyle> ᜂ;

		// Token: 0x0400004E RID: 78
		private XlsWorkbook ᜃ;

		// Token: 0x0400004F RID: 79
		private EventHandler ᜄ;

		// Token: 0x04000050 RID: 80
		private EventHandler ᜅ;
	}
}
