using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc.Fields
{
	// Token: 0x0200051E RID: 1310
	public class TableOfContent : ParagraphBase, spr\u1D30
	{
		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06004454 RID: 17492 RVA: 0x003FC21C File Offset: 0x003FB21C
		// (set) Token: 0x06004455 RID: 17493 RVA: 0x003FC260 File Offset: 0x003FB260
		public bool UseAbsolutePos
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
				return this.\u171B;
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
				this.\u171B = value;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06004456 RID: 17494 RVA: 0x003FC2A4 File Offset: 0x003FB2A4
		// (set) Token: 0x06004457 RID: 17495 RVA: 0x003FC2EC File Offset: 0x003FB2EC
		public bool UseHeadingStyles
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
				this.ᜅ();
				return this.ᜊ;
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
				this.ᜆ();
				this.ᜊ = value;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x003FC334 File Offset: 0x003FB334
		// (set) Token: 0x06004459 RID: 17497 RVA: 0x003FC37C File Offset: 0x003FB37C
		public int UpperHeadingLevel
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
				this.ᜅ();
				return this.ᜋ;
			}
			set
			{
				int a_ = 9;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(ClipboardData.b("㩮ŰͲၴնㅸṺᱼ᭾쮆ﶊ", a_), value);
				this.ᜆ();
				this.ᜋ = value;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x003FC3E4 File Offset: 0x003FB3E4
		// (set) Token: 0x0600445B RID: 17499 RVA: 0x003FC42C File Offset: 0x003FB42C
		public int LowerHeadingLevel
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
				this.ᜅ();
				return this.ᜌ;
			}
			set
			{
				int a_ = 6;
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ(ClipboardData.b("⁫ŭݯ᝱ٳ㹵ᵷ᭹᡻᝽좃ﺇ", a_), value);
				this.ᜆ();
				this.ᜌ = value;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600445C RID: 17500 RVA: 0x003FC494 File Offset: 0x003FB494
		// (set) Token: 0x0600445D RID: 17501 RVA: 0x003FC4D8 File Offset: 0x003FB4D8
		public bool UseTableEntryFields
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
				return this.\u170D;
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
				this.\u170D = value;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x0600445E RID: 17502 RVA: 0x003FC51C File Offset: 0x003FB51C
		// (set) Token: 0x0600445F RID: 17503 RVA: 0x003FC560 File Offset: 0x003FB560
		public string TableID
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
				return this.ᜎ;
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
				this.ᜎ = value;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06004460 RID: 17504 RVA: 0x003FC5A4 File Offset: 0x003FB5A4
		// (set) Token: 0x06004461 RID: 17505 RVA: 0x003FC5EC File Offset: 0x003FB5EC
		public bool RightAlignPageNumbers
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
				this.ᜅ();
				return this.ᜏ;
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
				this.ᜆ();
				this.ᜏ = value;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06004462 RID: 17506 RVA: 0x003FC634 File Offset: 0x003FB634
		// (set) Token: 0x06004463 RID: 17507 RVA: 0x003FC67C File Offset: 0x003FB67C
		public bool IncludePageNumbers
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
				this.ᜅ();
				return this.\u1712;
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
				this.ᜆ();
				this.\u1712 = value;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06004464 RID: 17508 RVA: 0x003FC6C4 File Offset: 0x003FB6C4
		// (set) Token: 0x06004465 RID: 17509 RVA: 0x003FC70C File Offset: 0x003FB70C
		public bool UseHyperlinks
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
				this.ᜅ();
				return this.ᜐ;
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
				this.ᜆ();
				this.ᜐ = value;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06004466 RID: 17510 RVA: 0x003FC754 File Offset: 0x003FB754
		// (set) Token: 0x06004467 RID: 17511 RVA: 0x003FC79C File Offset: 0x003FB79C
		public bool UseOutlineLevels
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
				this.ᜅ();
				return this.ᜑ;
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
				this.ᜆ();
				this.ᜑ = value;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06004468 RID: 17512 RVA: 0x003FC7E4 File Offset: 0x003FB7E4
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.TOC;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06004469 RID: 17513 RVA: 0x003FC824 File Offset: 0x003FB824
		// (set) Token: 0x0600446A RID: 17514 RVA: 0x003FC86C File Offset: 0x003FB86C
		internal string FormattingString
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
				return this.ᜉ.m_formattingString;
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
				this.ᜉ.m_formattingString = value;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600446B RID: 17515 RVA: 0x003FC8B4 File Offset: 0x003FB8B4
		internal Field TOCField
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
				return this.ᜉ;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x003FC8F8 File Offset: 0x003FB8F8
		internal Dictionary<int, ParagraphStyle> TOCStyles
		{
			get
			{
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
							goto IL_52;
						}
						if (false)
						{
						}
						break;
					case 1:
						goto IL_67;
					case 2:
						goto IL_52;
					}
					if (this.\u1713 == null)
					{
						num = 2;
						continue;
					}
					break;
					IL_52:
					this.\u1713 = new Dictionary<int, ParagraphStyle>();
					num = 1;
				}
				IL_67:
				if (true)
				{
				}
				return this.\u1713;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x0600446D RID: 17517 RVA: 0x003FC97C File Offset: 0x003FB97C
		internal Dictionary<int, string> TOCLevels
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_52;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52;
						}
						if (false)
						{
						}
						break;
					case 2:
						goto IL_67;
					}
					if (this.\u1717 == null)
					{
						num = 0;
						continue;
					}
					goto IL_71;
					IL_52:
					this.\u1717 = new Dictionary<int, string>();
					num = 2;
				}
				IL_67:
				if (true)
				{
				}
				IL_71:
				return this.\u1717;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x003FCA00 File Offset: 0x003FBA00
		// (set) Token: 0x0600446F RID: 17519 RVA: 0x003FCA84 File Offset: 0x003FBA84
		private List<int> TOCEntryPageNumbers
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5A;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5A;
						}
						if (true)
						{
						}
						if (false)
						{
						}
						break;
					case 2:
						goto IL_6F;
					}
					if (this.\u1718 == null)
					{
						num = 0;
						continue;
					}
					break;
					IL_5A:
					this.\u1718 = new List<int>();
					num = 2;
				}
				IL_6F:
				return this.\u1718;
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
				this.\u1718 = value;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x003FCAC8 File Offset: 0x003FBAC8
		private Paragraph LastTOCParagraph
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_70;
					case 1:
						goto IL_52;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52;
						}
						if (false)
						{
						}
						break;
					}
					if (this.\u171A == null)
					{
						num = 1;
						continue;
					}
					break;
					IL_52:
					this.\u171A = base.OwnerParagraph;
					if (true)
					{
					}
					num = 0;
				}
				IL_70:
				return this.\u171A;
			}
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x003FCB50 File Offset: 0x003FBB50
		public TableOfContent(IDocument doc) : base(doc as Document)
		{
			this.ᜉ = new Field(doc);
			this.ᜉ.Type = FieldType.FieldTOC;
			this.ᜎ = string.Empty;
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x003FCBCC File Offset: 0x003FBBCC
		public TableOfContent(IDocument doc, string switches) : this(doc)
		{
			this.TOCField.m_formattingString = switches;
			this.ᜏ();
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x003FCBF4 File Offset: 0x003FBBF4
		public void SetTOCLevelStyle(int levelNumber, string styleName)
		{
			int a_ = 8;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜀ(ClipboardData.b("ɭᕯѱᅳ᩵㙷ཹᅻᱽ", a_), levelNumber);
			this.ᜀ(levelNumber, styleName, true);
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x003FCC58 File Offset: 0x003FBC58
		public string GetTOCLevelStyle(int levelNumber)
		{
			int a_ = 1;
			int num = 7;
			ParagraphStyle paragraphStyle;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!this.\u1713.ContainsKey(levelNumber))
					{
						num = 3;
						continue;
					}
					paragraphStyle = this.\u1713[levelNumber];
					num = 1;
					continue;
				case 1:
					goto IL_FD;
				case 2:
					if (levelNumber > this.ᜋ)
					{
						num = 5;
						continue;
					}
					goto IL_96;
				case 3:
					paragraphStyle = (this.ᜀ((BuiltinStyle)levelNumber) as ParagraphStyle);
					num = 4;
					continue;
				case 4:
					goto IL_94;
				case 5:
					goto IL_E3;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_96;
					}
					if (false)
					{
					}
					num = 2;
					continue;
				case 7:
					if (true)
					{
					}
					break;
				}
				if (levelNumber >= this.ᜌ)
				{
					num = 6;
					continue;
				}
				goto IL_FF;
				IL_96:
				this.ᜏ();
				paragraphStyle = null;
				num = 0;
			}
			IL_94:
			goto IL_113;
			IL_E3:
			goto IL_FF;
			IL_FD:
			goto IL_113;
			IL_FF:
			throw new ArgumentException(ClipboardData.b("⭦౨ᵪ࡬ͮ兰ᩲ᭴፶ᱸͺ嵼ቾꞆ권놎겐뎒\ud994ﺚ힞쒠슢솤캦잨첪쪮잰횲\ud9b4鞶\ud8b8햺\ud9bc龾ﷀﻂ鋆마믊꣌뷎駐뛒듔돖냘뗚뫜鏞蓠闢胤详", a_));
			IL_113:
			return paragraphStyle.Name;
		}

		// Token: 0x06004475 RID: 17525 RVA: 0x003FCD80 File Offset: 0x003FBD80
		private void ᜏ()
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 26;
				for (;;)
				{
					string text2;
					int num2;
					int num3;
					string[] array;
					bool flag;
					switch (num)
					{
					case 0:
						goto IL_252;
					case 1:
						num = 24;
						continue;
					case 2:
					{
						string text;
						if (text.Length != 0)
						{
							num = 30;
							continue;
						}
						goto IL_361;
					}
					case 3:
						if (text2.Contains(ClipboardData.b("㝪䝬佮㱰ᙲݴၶᱸᵺቼൾ", a_)))
						{
							num = 13;
							continue;
						}
						goto IL_1D6;
					case 4:
						return;
					case 5:
						goto IL_361;
					case 6:
						goto IL_1D6;
					case 7:
					{
						if (num2 >= num3)
						{
							num = 1;
							continue;
						}
						string text = array[num2];
						num = 2;
						continue;
					}
					case 8:
						goto IL_361;
					case 9:
						num = 23;
						continue;
					case 10:
						num = 16;
						continue;
					case 11:
					{
						char c;
						switch (c)
						{
						case 'f':
						{
							string text;
							this.ᜃ(text);
							num = 8;
							continue;
						}
						case 'g':
							goto IL_361;
						case 'h':
							this.ᜐ = true;
							num = 18;
							continue;
						default:
							num = 10;
							continue;
						}
						break;
					}
					case 12:
						goto IL_126;
					case 13:
						if (true)
						{
						}
						text2 = text2.Remove(text2.IndexOf(ClipboardData.b("㝪䝬佮㱰ᙲݴၶᱸᵺቼൾ", a_))).Trim();
						num = 21;
						continue;
					case 14:
						num = 22;
						continue;
					case 15:
						goto IL_361;
					case 16:
					{
						char c;
						switch (c)
						{
						case 'n':
							this.\u1712 = false;
							num = 20;
							continue;
						case 'o':
						{
							this.ᜊ = true;
							flag = true;
							string text;
							this.ᜅ(text);
							num = 5;
							continue;
						}
						case 'p':
							this.ᜏ = false;
							num = 15;
							continue;
						case 'q':
						case 'r':
						case 's':
							goto IL_361;
						case 't':
						{
							string text;
							this.ᜂ(text);
							num = 28;
							continue;
						}
						case 'u':
							this.ᜑ = true;
							num = 29;
							continue;
						default:
							num = 14;
							continue;
						}
						break;
					}
					case 17:
						goto IL_126;
					case 18:
						goto IL_361;
					case 19:
						if (text2.Contains(ClipboardData.b("㝪䝬佮㱰㙲❴ぶ㱸㵺㉼⵾첀슂톄", a_)))
						{
							num = 27;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 20:
						goto IL_361;
					case 21:
						goto IL_1D6;
					case 22:
						goto IL_361;
					case 23:
						if (!flag)
						{
							num = 25;
							continue;
						}
						goto IL_406;
					case 24:
						if (this.TOCStyles.Count == 0)
						{
							num = 9;
							continue;
						}
						goto IL_406;
					case 25:
						this.ᜋ = 9;
						num = 0;
						continue;
					case 27:
						text2 = text2.Remove(text2.IndexOf(ClipboardData.b("㝪䝬佮㱰㙲❴ぶ㱸㵺㉼⵾첀슂톄", a_))).Trim();
						num = 6;
						continue;
					case 28:
						goto IL_361;
					case 29:
						goto IL_361;
					case 30:
					{
						string text;
						char c = text[0];
						num = 11;
						continue;
					}
					}
					if (this.\u1715)
					{
						num = 4;
						continue;
					}
					text2 = this.TOCField.m_formattingString;
					num = 19;
					continue;
					IL_126:
					num = 7;
					continue;
					IL_1D6:
					array = text2.Split(new char[]
					{
						'\\'
					});
					flag = false;
					num2 = 0;
					num3 = array.Length;
					num = 12;
					continue;
					IL_361:
					num2++;
					num = 17;
				}
				return;
				IL_252:
				IL_406:
				this.\u1715 = true;
				return;
			}
			}
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x003FD19C File Offset: 0x003FC19C
		private new IParagraphStyle ᜀ(BuiltinStyle A_0)
		{
			IParagraphStyle paragraphStyle;
			for (;;)
			{
				IL_38:
				string name = Style.ᜁ(A_0);
				paragraphStyle = (this.m_doc.Styles.FindByName(name, StyleType.ParagraphStyle) as IParagraphStyle);
				int num = 1;
				for (;;)
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
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							goto IL_6A;
						case 1:
							if (paragraphStyle == null)
							{
								num = 0;
								continue;
							}
							return paragraphStyle;
						case 2:
							return paragraphStyle;
						}
						goto IL_38;
					}
					IL_6A:
					paragraphStyle = (IParagraphStyle)Style.CreateBuiltinStyle(A_0, this.m_doc);
					this.m_doc.Styles.Add(paragraphStyle);
					num = 2;
				}
			}
			return paragraphStyle;
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x003FD254 File Offset: 0x003FC254
		private void ᜎ()
		{
			for (;;)
			{
				IL_18:
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_92:
					goto IL_52;
				default:
					if (false)
					{
					}
					num = 1;
					num2 = 2;
					break;
				}
				for (;;)
				{
					IL_02:
					switch (num2)
					{
					case 0:
						goto IL_92;
					case 1:
					{
						if (num > 9)
						{
							num2 = 3;
							continue;
						}
						BuiltinStyle a_ = (BuiltinStyle)num;
						this.TOCStyles.Add(num, this.ᜀ(a_) as ParagraphStyle);
						num++;
						num2 = 0;
						continue;
					}
					case 2:
						goto IL_3E;
					case 3:
						return;
					}
					goto IL_18;
				}
				IL_3E:
				if (true)
				{
				}
				IL_52:
				num2 = 1;
				goto IL_02;
			}
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x003FD2F8 File Offset: 0x003FC2F8
		internal void \u1718()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.\u170D();
						break;
					}
					num = 3;
					continue;
				case 3:
					goto IL_44;
				case 4:
					if (this.ᜊ)
					{
						num = 2;
						continue;
					}
					goto IL_9F;
				}
				if (!this.\u1714)
				{
					num = 1;
				}
				else
				{
					this.TOCField.m_formattingString = string.Empty;
					if (true)
					{
					}
					num = 4;
				}
			}
			return;
			IL_44:
			IL_9F:
			this.ᜇ();
			this.ᜋ();
			this.ᜊ();
			this.ᜈ();
			this.ᜌ();
			this.ᜉ();
			this.\u1715 = true;
		}

		// Token: 0x06004479 RID: 17529 RVA: 0x003FD3D0 File Offset: 0x003FC3D0
		private void \u170D()
		{
			int a_ = 13;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			string str = string.Format(ClipboardData.b("⽲ᩴ坶學z䵼ɾ검뒄惘ꮈꮊ", a_), this.ᜌ, this.ᜋ);
			Field field = this.TOCField;
			field.m_formattingString += str;
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x003FD458 File Offset: 0x003FC458
		private void ᜌ()
		{
			int a_ = 15;
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
						goto IL_89;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					return;
				case 2:
				{
					Field field = this.TOCField;
					field.m_formattingString += ClipboardData.b("⥴ὶ奸❺ݼ彾", a_);
					goto IL_89;
				}
				}
				if (true)
				{
				}
				if (this.ᜐ)
				{
					num = 2;
					continue;
				}
				break;
				IL_89:
				num = 1;
			}
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x003FD4F8 File Offset: 0x003FC4F8
		private void ᜋ()
		{
			int a_ = 6;
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					}
					if (false)
					{
					}
					break;
				case 2:
				{
					Field field = this.TOCField;
					object formattingString = field.m_formattingString;
					field.m_formattingString = string.Concat(new object[]
					{
						formattingString,
						ClipboardData.b("に", a_),
						'n',
						ClipboardData.b("䱫", a_)
					});
					goto IL_B4;
				}
				}
				if (!this.\u1712)
				{
					num = 2;
					continue;
				}
				break;
				IL_B4:
				num = 0;
			}
		}

		// Token: 0x0600447C RID: 17532 RVA: 0x003FD5C4 File Offset: 0x003FC5C4
		private void ᜊ()
		{
			int a_ = 0;
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
						goto IL_B4;
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
				case 1:
				{
					Field field = this.TOCField;
					object formattingString = field.m_formattingString;
					field.m_formattingString = string.Concat(new object[]
					{
						formattingString,
						ClipboardData.b("㩥", a_),
						'p',
						ClipboardData.b("䙥䩧䩩乫乭", a_)
					});
					goto IL_B4;
				}
				case 2:
					return;
				}
				if (!this.ᜏ)
				{
					num = 1;
					continue;
				}
				break;
				IL_B4:
				num = 2;
			}
		}

		// Token: 0x0600447D RID: 17533 RVA: 0x003FD690 File Offset: 0x003FC690
		private void ᜉ()
		{
			int a_ = 11;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_B4;
				case 1:
				{
					Field field = this.TOCField;
					object formattingString = field.m_formattingString;
					field.m_formattingString = string.Concat(new object[]
					{
						formattingString,
						ClipboardData.b("⵰", a_),
						'u',
						ClipboardData.b("兰", a_)
					});
					goto IL_AC;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AC;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (this.ᜑ)
				{
					num = 1;
					continue;
				}
				return;
				IL_AC:
				num = 0;
			}
			IL_B4:
			if (true)
			{
			}
		}

		// Token: 0x0600447E RID: 17534 RVA: 0x003FD75C File Offset: 0x003FC75C
		private void ᜈ()
		{
			int a_ = 7;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					Field field = this.TOCField;
					object formattingString = field.m_formattingString;
					field.m_formattingString = string.Concat(new object[]
					{
						formattingString,
						ClipboardData.b("ㅬ", a_),
						'f',
						ClipboardData.b("䵬", a_),
						this.ᜎ
					});
					goto IL_BD;
				}
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BD;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (this.\u170D)
				{
					num = 1;
					continue;
				}
				break;
				IL_BD:
				num = 0;
			}
		}

		// Token: 0x0600447F RID: 17535 RVA: 0x003FD834 File Offset: 0x003FC834
		private void ᜇ()
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 1:
						goto IL_19C;
					case 2:
						num = 6;
						continue;
					case 3:
						if (this.TOCStyles.ContainsKey(num2))
						{
							num = 2;
							continue;
						}
						goto IL_19E;
					case 4:
					{
						Field field = this.TOCField;
						object formattingString = field.m_formattingString;
						field.m_formattingString = string.Concat(new object[]
						{
							formattingString,
							((IStyle)this.TOCStyles[num2]).Name,
							this.\u1716,
							num2,
							this.\u1716
						});
						num = 9;
						continue;
					}
					case 5:
						goto IL_12A;
					case 6:
						if (Style.BuiltinStyleLoader.ᜃ[num2] != ((IStyle)this.TOCStyles[num2]).Name)
						{
							num = 4;
							continue;
						}
						goto IL_19E;
					case 7:
						goto IL_14E;
					case 8:
						if (num2 > this.ᜋ)
						{
							num = 7;
							continue;
						}
						goto IL_1B3;
					case 9:
						goto IL_19E;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1B3;
						default:
						{
							if (false)
							{
							}
							if (this.\u1713.Count == 0)
							{
								num = 1;
								continue;
							}
							Field field2 = this.TOCField;
							object formattingString2 = field2.m_formattingString;
							field2.m_formattingString = string.Concat(new object[]
							{
								formattingString2,
								ClipboardData.b("⡳", a_),
								't',
								ClipboardData.b("味呵", a_)
							});
							num2 = this.ᜌ;
							num = 5;
							continue;
						}
						}
						break;
					case 11:
						if (true)
						{
						}
						num = 10;
						continue;
					case 12:
						goto IL_12A;
					}
					if (this.\u1713 != null)
					{
						num = 11;
						continue;
					}
					break;
					IL_12A:
					num = 8;
					continue;
					IL_19E:
					num2++;
					num = 12;
					continue;
					IL_1B3:
					num = 3;
				}
				return;
				IL_14E:
				Field field3 = this.TOCField;
				field3.m_formattingString += ClipboardData.b("噳", a_);
				return;
				IL_19C:
				return;
			}
			}
		}

		// Token: 0x06004480 RID: 17536 RVA: 0x003FDAAC File Offset: 0x003FCAAC
		private void ᜅ(string A_0)
		{
			int a_ = 17;
			MatchCollection matchCollection;
			for (;;)
			{
				Regex regex = new Regex(ClipboardData.b("ⱶ䥸噺䑼≾", a_));
				matchCollection = regex.Matches(A_0);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (matchCollection.Count == 0)
						{
							num = 4;
							continue;
						}
						return;
					case 1:
						if (matchCollection.Count == 2)
						{
							num = 3;
							continue;
						}
						if (true)
						{
						}
						num = 0;
						continue;
					case 2:
						return;
					case 3:
						goto IL_5A;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							break;
						}
						this.ᜋ = 9;
						num = 2;
						continue;
					}
					break;
				}
			}
			IL_5A:
			this.ᜌ = int.Parse(matchCollection[0].Groups[0].Value);
			this.ᜋ = int.Parse(matchCollection[1].Groups[0].Value);
		}

		// Token: 0x06004481 RID: 17537 RVA: 0x003FDBBC File Offset: 0x003FCBBC
		private void ᜄ(string A_0)
		{
			int a_ = 5;
			for (;;)
			{
				IL_39:
				Regex regex = new Regex(ClipboardData.b("な佬㉮⩰卲⡴ⱶ學♺", a_));
				Match match = regex.Match(A_0);
				int num = 2;
				for (;;)
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
						switch (num)
						{
						case 0:
							return;
						case 1:
							goto IL_7B;
						case 2:
							if (true)
							{
							}
							if (match.Captures.Count == 1)
							{
								num = 1;
								continue;
							}
							return;
						}
						goto IL_39;
					}
					IL_7B:
					this.ᜏ = false;
					num = 0;
				}
			}
		}

		// Token: 0x06004482 RID: 17538 RVA: 0x003FDC64 File Offset: 0x003FCC64
		private void ᜃ(string A_0)
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
			this.\u170D = true;
			this.ᜎ = A_0.Substring(1, A_0.Length - 1);
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x003FDCBC File Offset: 0x003FCCBC
		private void ᜂ(string A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					char c = this.\u1716.ToCharArray()[0];
					string[] array = A_0.Split(new char[]
					{
						'"'
					});
					string[] array2 = array[1].Split(new char[]
					{
						c
					});
					int num = 0;
					int num2 = array2.Length;
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_132;
						case 1:
							num3 = 5;
							continue;
						case 2:
							goto IL_B5;
						case 3:
							if (true)
							{
							}
							this.ᜊ = false;
							num3 = 0;
							continue;
						case 4:
						{
							if (num + 1 >= num2)
							{
								goto IL_E5;
							}
							int a_ = int.Parse(array2[num + 1]);
							this.ᜀ(a_, array2[num], false);
							num += 2;
							num3 = 6;
							continue;
						}
						case 5:
							if (this.TOCStyles.Count > 0)
							{
								num3 = 3;
								continue;
							}
							goto IL_13E;
						case 6:
							goto IL_B5;
						}
						break;
						IL_B5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							IL_E5:
							num3 = 1;
							break;
						default:
							if (false)
							{
							}
							num3 = 4;
							break;
						}
					}
				}
				IL_132:
				IL_13E:
				this.\u1714 = false;
				return;
			}
		}

		// Token: 0x06004484 RID: 17540 RVA: 0x003FDE10 File Offset: 0x003FCE10
		private void ᜆ()
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
			this.ᜏ();
			this.\u1714 = true;
		}

		// Token: 0x06004485 RID: 17541 RVA: 0x003FDE58 File Offset: 0x003FCE58
		private void ᜅ()
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
			this.ᜏ();
		}

		// Token: 0x06004486 RID: 17542 RVA: 0x003FDE9C File Offset: 0x003FCE9C
		private new void ᜀ(int A_0, string A_1, bool A_2)
		{
			int num = 1;
			IParagraphStyle paragraphStyle2;
			for (;;)
			{
				IParagraphStyle paragraphStyle;
				BuiltinStyle builtinStyle;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1BD;
					default:
						if (false)
						{
						}
						if (paragraphStyle == null)
						{
							num = 3;
							continue;
						}
						goto IL_15A;
					}
					break;
				case 2:
					goto IL_15A;
				case 3:
					paragraphStyle2 = (IParagraphStyle)Style.CreateBuiltinStyle(builtinStyle, this.m_doc);
					this.m_doc.Styles.Add(paragraphStyle2);
					num = 2;
					continue;
				case 4:
					num = 0;
					continue;
				case 5:
					goto IL_1BD;
				case 6:
					if (paragraphStyle2 == null)
					{
						num = 4;
						continue;
					}
					goto IL_15A;
				case 7:
					if (paragraphStyle2 != null)
					{
						num = 9;
						continue;
					}
					num = 8;
					continue;
				case 8:
					if (paragraphStyle != null)
					{
						if (true)
						{
						}
						num = 12;
						continue;
					}
					return;
				case 9:
					goto IL_173;
				case 10:
					num = 5;
					continue;
				case 11:
					goto IL_197;
				case 12:
					this.TOCStyles[A_0] = (paragraphStyle as ParagraphStyle);
					num = 16;
					continue;
				case 13:
					if (paragraphStyle2 == null)
					{
						num = 10;
						continue;
					}
					goto IL_197;
				case 14:
					goto IL_AB;
				case 15:
					this.ᜆ();
					num = 14;
					continue;
				case 16:
					return;
				}
				if (A_2)
				{
					num = 15;
					continue;
				}
				IL_AB:
				builtinStyle = Style.NameToBuiltIn(A_1);
				paragraphStyle2 = (this.m_doc.Styles.FindByName(A_1, StyleType.ParagraphStyle) as IParagraphStyle);
				Style.NameToBuiltIn(A_1.ToLower());
				paragraphStyle = (this.m_doc.Styles.FindByName(A_1.ToLower(), StyleType.ParagraphStyle) as IParagraphStyle);
				num = 13;
				continue;
				IL_15A:
				num = 7;
				continue;
				IL_1BD:
				if (builtinStyle != BuiltinStyle.User)
				{
					num = 11;
					continue;
				}
				goto IL_15A;
				IL_197:
				num = 6;
			}
			IL_173:
			this.TOCStyles[A_0] = (paragraphStyle2 as ParagraphStyle);
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x003FE0B8 File Offset: 0x003FD0B8
		private new void ᜀ(string A_0, int A_1)
		{
			int a_ = 10;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (A_1 > 9)
					{
						num = 3;
						continue;
					}
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_53;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					num = 0;
					continue;
				case 3:
					goto IL_8F;
				}
				if (A_1 < 1)
				{
					break;
				}
				num = 2;
			}
			IL_53:
			throw new ArgumentOutOfRangeException(A_0, ClipboardData.b("㱯᝱ɳ፵ᑷ婹ቻ୽ꢇﲉ뒓ﮕ뺝슟잡蒣솥\udaa7쾩춫\udaad햯삱钳습킷\udbb9튻麽ꗃꣅ곇뿋ꏍ뇏뻑룓돕꫗龎꣛뛝臟賡쓣ퟥ\ud8e7쓩", a_));
			IL_8F:
			goto IL_53;
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x003FE158 File Offset: 0x003FD158
		internal void \u1712()
		{
			int a_ = 9;
			switch (0)
			{
			default:
			{
				int num = 0;
				for (;;)
				{
					int num2;
					string str;
					switch (num)
					{
					case 1:
						return;
					case 2:
						goto IL_14D;
					case 3:
						if (num2 > this.ᜋ)
						{
							num = 1;
							continue;
						}
						this.TOCLevels.Add(num2, str + num2.ToString());
						num2++;
						num = 7;
						continue;
					case 4:
					{
						Dictionary<int, ParagraphStyle>.Enumerator enumerator = this.TOCStyles.GetEnumerator();
						num = 11;
						continue;
					}
					case 5:
						if (this.TOCStyles.Count > 0)
						{
							num = 6;
							continue;
						}
						goto IL_78;
					case 6:
						if (true)
						{
						}
						num = 10;
						continue;
					case 7:
						goto IL_1BF;
					case 8:
						goto IL_1BF;
					case 9:
						this.TOCLevels.Clear();
						num = 2;
						continue;
					case 10:
						if (!this.UseHeadingStyles)
						{
							num = 4;
							continue;
						}
						goto IL_78;
					case 11:
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 1:
								{
									Dictionary<int, ParagraphStyle>.Enumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 2;
										continue;
									}
									KeyValuePair<int, ParagraphStyle> keyValuePair = enumerator.Current;
									this.TOCLevels.Add(keyValuePair.Key, keyValuePair.Value.Name);
									num = 0;
									continue;
								}
								case 2:
									for (;;)
									{
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
									num = 4;
									continue;
								case 4:
									goto IL_13A;
								}
								IL_C4:
								num = 1;
								continue;
								goto IL_C4;
							}
							IL_13A:
							return;
						}
						finally
						{
							Dictionary<int, ParagraphStyle>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
						goto IL_14D;
					}
					if (this.TOCLevels.Count > 0)
					{
						num = 9;
						continue;
					}
					goto IL_14D;
					IL_78:
					str = ClipboardData.b("ݮᑰቲᅴṶ᝸ᱺ嵼", a_);
					num2 = this.ᜌ;
					num = 8;
					continue;
					IL_14D:
					num = 5;
					continue;
					IL_1BF:
					num = 3;
				}
				return;
			}
			}
		}

		// Token: 0x06004489 RID: 17545 RVA: 0x003FE3AC File Offset: 0x003FD3AC
		internal void \u1716()
		{
			for (;;)
			{
				this.\u1712();
				this.ᜃ();
				this.ᜂ();
				this.ᜄ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.IncludePageNumbers)
						{
							num = 1;
							continue;
						}
						return;
					case 1:
					{
						if (true)
						{
						}
						spr\u1A69 spr_u1A = new spr\u1A69();
						spr_u1A.ᜀ(this.TOCLevels);
						spr_u1A.ᜃ(this.UseTableEntryFields);
						this.TOCEntryPageNumbers = spr_u1A.ᜀ(base.Document);
						spr_u1A.ᜠ();
						this.ᜀ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					case 2:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x003FE478 File Offset: 0x003FD478
		private void ᜄ()
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				Document document = base.Document;
				IEnumerator enumerator = document.Sections.GetEnumerator();
				try
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_A5;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							ISection section = (ISection)enumerator.Current;
							this.ᜀ(section.Body);
							num = 0;
							continue;
						}
						case 4:
							num = 1;
							continue;
						}
						IL_80:
						num = 3;
						continue;
						goto IL_80;
					}
					IL_A5:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable = enumerator as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_105;
							case 1:
								disposable.Dispose();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							case 2:
								if (disposable != null)
								{
									num = 1;
									continue;
								}
								goto IL_107;
							}
							break;
						}
					}
					IL_105:
					IL_107:;
				}
				return;
			}
			}
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x003FE5A0 File Offset: 0x003FD5A0
		private new void ᜀ(Body A_0)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 9;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (A_0.Items[num] is Paragraph)
						{
							num2 = 7;
							continue;
						}
						num2 = 6;
						continue;
					case 1:
						goto IL_A7;
					case 2:
						if (num >= A_0.Items.Count)
						{
							num2 = 5;
							continue;
						}
						num2 = 0;
						continue;
					case 3:
						goto IL_46;
					case 4:
						IL_11D:
						goto IL_46;
					case 5:
						goto IL_EA;
					case 6:
						if (A_0.Items[num] is Table)
						{
							num2 = 8;
							continue;
						}
						goto IL_46;
					case 7:
					{
						IParagraph paragraph = A_0.Items[num] as Paragraph;
						this.ᜁ(paragraph);
						num = A_0.Items.IndexOf(paragraph);
						num2 = 4;
						continue;
					}
					case 8:
					{
						ITable table = A_0.ChildObjects[num] as Table;
						this.ᜀ(table);
						num = A_0.Items.IndexOf(table);
						num2 = 3;
						continue;
					}
					case 9:
						goto IL_A7;
					}
					break;
					IL_46:
					num++;
					num2 = 1;
					continue;
					IL_A7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11D;
					default:
						if (false)
						{
						}
						num2 = 2;
						break;
					}
				}
			}
			IL_EA:
			if (true)
			{
			}
		}

		// Token: 0x0600448C RID: 17548 RVA: 0x003FE710 File Offset: 0x003FD710
		private new void ᜀ(ITable A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num;
					TableRow tableRow;
					int num2;
					int num3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_91:
						if (true)
						{
						}
						tableRow = A_0.Rows[num];
						num2 = 0;
						num3 = 0;
						break;
					default:
						if (false)
						{
						}
						num = 0;
						num3 = 1;
						break;
					}
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_6F;
						case 1:
							goto IL_B6;
						case 2:
							goto IL_B6;
						case 3:
							num++;
							num3 = 2;
							continue;
						case 4:
							goto IL_6F;
						case 5:
							if (num >= A_0.Rows.Count)
							{
								num3 = 7;
								continue;
							}
							goto IL_91;
						case 6:
						{
							if (num2 >= tableRow.Cells.Count)
							{
								num3 = 3;
								continue;
							}
							TableCell a_ = tableRow.Cells[num2];
							this.ᜀ(a_);
							num2++;
							num3 = 4;
							continue;
						}
						case 7:
							return;
						}
						break;
						IL_6F:
						num3 = 6;
						continue;
						IL_B6:
						num3 = 5;
					}
				}
				return;
			}
		}

		// Token: 0x0600448D RID: 17549 RVA: 0x003FE830 File Offset: 0x003FD830
		private new void ᜁ(IParagraph A_0)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 16;
				for (;;)
				{
					int num2;
					List<int> list;
					switch (num)
					{
					case 0:
					{
						Field field = A_0.Items[num2] as Field;
						num = 15;
						continue;
					}
					case 1:
						goto IL_139;
					case 2:
						if (!string.IsNullOrEmpty(A_0.Text))
						{
							num = 3;
							continue;
						}
						goto IL_250;
					case 3:
					{
						int a_2 = 0;
						IEnumerator enumerator = A_0.Items.GetEnumerator();
						num = 4;
						continue;
					}
					case 4:
					{
						int a_2;
						try
						{
							num = 6;
							for (;;)
							{
								ParagraphBase paragraphBase;
								switch (num)
								{
								case 0:
									goto IL_3EF;
								case 1:
									num = 7;
									continue;
								case 2:
									goto IL_2E5;
								case 3:
									if ((paragraphBase as TextRange).Text != ClipboardData.b("灸", a_))
									{
										num = 11;
										continue;
									}
									break;
								case 4:
									goto IL_3FB;
								case 5:
									if (paragraphBase is TextRange)
									{
										num = 12;
										continue;
									}
									break;
								case 7:
									if (!((paragraphBase as TextRange).Text == ClipboardData.b("⵸㑺㹼", a_)))
									{
										num = 2;
										continue;
									}
									break;
								case 8:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 0;
										continue;
									}
									paragraphBase = (ParagraphBase)enumerator.Current;
									num = 5;
									continue;
								}
								case 9:
									goto IL_3EF;
								case 10:
									if (base.OwnerParagraph == A_0)
									{
										num = 1;
										continue;
									}
									goto IL_2E5;
								case 11:
									num = 10;
									continue;
								case 12:
									num = 3;
									continue;
								}
								goto IL_2E0;
								IL_2E5:
								a_2 = A_0.Items.IndexOf(paragraphBase);
								num = 9;
								continue;
								IL_3CC:
								num = 8;
								continue;
								IL_2E0:
								goto IL_3CC;
								IL_3EF:
								num = 4;
							}
							IL_3FB:
							goto IL_1D4;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											continue;
										default:
											if (false)
											{
											}
											num = 2;
											continue;
										}
										break;
									case 1:
										if (disposable != null)
										{
											num = 0;
											continue;
										}
										goto IL_464;
									case 2:
										goto IL_462;
									}
									break;
								}
							}
							IL_462:
							IL_464:;
						}
						goto IL_465;
						IL_1D4:
						this.ᜀ(A_0, null, a_2, A_0.Items.Count + 1);
						num = 8;
						continue;
					}
					case 5:
						this.ᜀ(A_0);
						num = 2;
						continue;
					case 6:
					{
						TextBox textBox = A_0.Items[num2] as TextBox;
						this.ᜀ(textBox.Body);
						num = 12;
						continue;
					}
					case 7:
						goto IL_1F8;
					case 8:
						goto IL_250;
					case 9:
					{
						int num3;
						if (num3 >= list.Count)
						{
							num = 11;
							continue;
						}
						int num4 = list[num3] + num3 * 2;
						list.RemoveAt(num3);
						this.ᜀ(A_0, A_0.Items[num4] as Field, num4, num4 + 2);
						num3++;
						num = 19;
						continue;
					}
					case 10:
						if (A_0.Items[num2] is TextBox)
						{
							num = 6;
							continue;
						}
						num = 20;
						continue;
					case 11:
						return;
					case 12:
						if (true)
						{
						}
						goto IL_139;
					case 13:
						goto IL_226;
					case 14:
					{
						int num3 = 0;
						num = 13;
						continue;
					}
					case 15:
					{
						Field field;
						if (field.Type == FieldType.FieldTOCEntry)
						{
							num = 23;
							continue;
						}
						goto IL_139;
					}
					case 17:
						if (num2 >= A_0.Items.Count)
						{
							num = 14;
							continue;
						}
						goto IL_465;
					case 18:
						if (this.UseTableEntryFields)
						{
							num = 21;
							continue;
						}
						goto IL_139;
					case 19:
						goto IL_226;
					case 20:
						if (A_0.Items[num2] is Field)
						{
							num = 0;
							continue;
						}
						goto IL_139;
					case 21:
						list.Add(num2);
						num = 1;
						continue;
					case 22:
						goto IL_1F8;
					case 23:
						num = 18;
						continue;
					}
					if (this.ᜁ(A_0.StyleName))
					{
						num = 5;
						continue;
					}
					goto IL_250;
					IL_139:
					num2++;
					num = 22;
					continue;
					IL_1F8:
					num = 17;
					continue;
					IL_226:
					num = 9;
					continue;
					IL_250:
					list = new List<int>();
					num2 = 0;
					num = 7;
					continue;
					IL_465:
					num = 10;
				}
				return;
			}
			}
		}

		// Token: 0x0600448E RID: 17550 RVA: 0x003FED4C File Offset: 0x003FDD4C
		private new void ᜀ(IParagraph A_0)
		{
			int a_ = 1;
			int num = 10;
			TextRange textRange;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_163;
				case 1:
					if (num2 >= A_0.Items.Count)
					{
						goto IL_17F;
					}
					num = 7;
					continue;
				case 2:
					if (textRange.Text.Contains(ClipboardData.b("橦", a_)))
					{
						num = 19;
						continue;
					}
					goto IL_106;
				case 3:
					goto IL_EA;
				case 4:
					if (textRange.Text.Contains(ClipboardData.b("浦", a_)))
					{
						num = 11;
						continue;
					}
					num = 2;
					continue;
				case 5:
					if (textRange.Text.Contains(ClipboardData.b("湦", a_)))
					{
						num = 16;
						continue;
					}
					goto IL_20C;
				case 6:
					goto IL_163;
				case 7:
					if (A_0.Items[num2] is TextRange)
					{
						num = 13;
						continue;
					}
					goto IL_106;
				case 8:
					num = 12;
					continue;
				case 9:
					goto IL_18A;
				case 11:
					goto IL_240;
				case 12:
					if (A_0.Text.Contains(ClipboardData.b("橦", a_)))
					{
						num = 3;
						continue;
					}
					return;
				case 13:
				{
					textRange = (A_0.Items[num2] as TextRange);
					string text = textRange.Text;
					num = 14;
					continue;
				}
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17F;
					default:
						if (false)
						{
						}
						if (textRange.Text != ClipboardData.b("湦", a_))
						{
							num = 18;
							continue;
						}
						goto IL_20C;
					}
					break;
				case 15:
					num = 20;
					continue;
				case 16:
					this.ᜀ(textRange);
					num = 17;
					continue;
				case 17:
					if (true)
					{
					}
					goto IL_20C;
				case 18:
					num = 5;
					continue;
				case 19:
					goto IL_C6;
				case 20:
					if (!A_0.Text.Contains(ClipboardData.b("浦", a_)))
					{
						num = 8;
						continue;
					}
					goto IL_EA;
				}
				if (!A_0.Text.Contains(ClipboardData.b("湦", a_)))
				{
					num = 15;
					continue;
				}
				IL_EA:
				num2 = 0;
				num = 6;
				continue;
				IL_106:
				num2++;
				num = 0;
				continue;
				IL_163:
				num = 1;
				continue;
				IL_17F:
				num = 9;
				continue;
				IL_20C:
				num = 4;
			}
			IL_C6:
			this.ᜀ(textRange, ClipboardData.b("橦", a_));
			return;
			IL_18A:
			return;
			IL_240:
			this.ᜀ(textRange, ClipboardData.b("浦", a_));
		}

		// Token: 0x0600448F RID: 17551 RVA: 0x003FF048 File Offset: 0x003FE048
		private new void ᜀ(TextRange A_0)
		{
			int a_ = 6;
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				Paragraph ownerParagraph;
				int num2;
				TextRange textRange;
				for (;;)
				{
					ownerParagraph = A_0.OwnerParagraph;
					string text = A_0.Text;
					int num = text.IndexOf(ClipboardData.b("敫", a_));
					num2 = ownerParagraph.Items.IndexOf(A_0);
					string text2 = text.Substring(num + 1);
					textRange = (A_0.Clone() as TextRange);
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_E5;
						case 1:
							if (num > 0)
							{
								num3 = 5;
								continue;
							}
							num3 = 2;
							continue;
						case 2:
							if (text2 != string.Empty)
							{
								num3 = 3;
								continue;
							}
							goto IL_143;
						case 3:
							for (;;)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_B6;
								}
							}
							IL_B6:
							if (false)
							{
							}
							textRange.Text = text2;
							A_0.Text = ClipboardData.b("敫", a_);
							num3 = 0;
							continue;
						case 4:
							goto IL_141;
						case 5:
							textRange.Text = text.Substring(num);
							A_0.Text = text.Substring(0, num);
							num3 = 4;
							continue;
						}
						break;
					}
				}
				IL_E5:
				IL_141:
				IL_143:
				ownerParagraph.Items.Insert(num2 + 1, textRange);
				return;
			}
			}
		}

		// Token: 0x06004490 RID: 17552 RVA: 0x003FF1A8 File Offset: 0x003FE1A8
		private new void ᜀ(TextRange A_0, string A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Paragraph ownerParagraph = A_0.OwnerParagraph;
					string text = A_0.Text;
					int num = text.IndexOf(A_1);
					int num2 = ownerParagraph.Items.IndexOf(A_0);
					string text2 = text.Substring(num + 1);
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D4;
							default:
								if (false)
								{
								}
								goto IL_83;
							}
							break;
						case 1:
							if (text2 != string.Empty)
							{
								num3 = 6;
								continue;
							}
							A_0.Text = text.Substring(0, num);
							goto IL_D4;
						case 2:
							if (A_0.Text == string.Empty)
							{
								num3 = 4;
								continue;
							}
							return;
						case 3:
							goto IL_83;
						case 4:
							if (true)
							{
							}
							ownerParagraph.Items.Remove(A_0);
							num3 = 5;
							continue;
						case 5:
							return;
						case 6:
						{
							TextRange textRange = A_0.Clone() as TextRange;
							textRange.Text = text2;
							ownerParagraph.Items.Insert(num2 + 1, textRange);
							A_0.Text = text.Substring(0, num);
							num3 = 0;
							continue;
						}
						}
						break;
						IL_83:
						this.ᜀ(ownerParagraph, num2 + 1);
						num3 = 2;
						continue;
						IL_D4:
						num3 = 3;
					}
				}
				return;
			}
		}

		// Token: 0x06004491 RID: 17553 RVA: 0x003FF31C File Offset: 0x003FE31C
		private new void ᜀ(Paragraph A_0, int A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Body ownerTextBody = A_0.OwnerTextBody;
					int num = ownerTextBody.Items.IndexOf(A_0);
					Paragraph paragraph = A_0.Clone() as Paragraph;
					int count = paragraph.Items.Count;
					int num2 = 0;
					int num3 = 0;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_146;
						case 1:
							if (num2 >= count)
							{
								num3 = 4;
								continue;
							}
							paragraph.Items.RemoveAt(0);
							num2++;
							num3 = 2;
							continue;
						case 2:
							goto IL_110;
						case 3:
						{
							int num4;
							if (num4 >= count)
							{
								num3 = 6;
								continue;
							}
							paragraph.Items.Insert(paragraph.Items.Count, A_0.Items[A_1]);
							num4++;
							num3 = 7;
							continue;
						}
						case 4:
						{
							ownerTextBody.Items.Insert(num + 1, paragraph);
							count = A_0.Items.Count;
							int num4 = A_1;
							num3 = 5;
							continue;
						}
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_110;
							default:
								if (false)
								{
								}
								goto IL_112;
							}
							break;
						case 6:
							goto IL_13C;
						case 7:
							goto IL_112;
						}
						break;
						IL_112:
						num3 = 3;
						continue;
						IL_146:
						num3 = 1;
						continue;
						IL_110:
						goto IL_146;
					}
				}
				IL_13C:
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x06004492 RID: 17554 RVA: 0x003FF494 File Offset: 0x003FE494
		private void ᜃ()
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				Paragraph ownerParagraph;
				Body ownerTextBody;
				Paragraph paragraph;
				for (;;)
				{
					ownerParagraph = base.OwnerParagraph;
					ownerTextBody = ownerParagraph.OwnerTextBody;
					int num = ownerTextBody.Items.IndexOf(ownerParagraph);
					bool flag = true;
					int num2 = num;
					int num3 = 0;
					if (true)
					{
					}
					int num4 = 33;
					for (;;)
					{
						int num5;
						int num6;
						switch (num4)
						{
						case 0:
							return;
						case 1:
							goto IL_2AD;
						case 2:
							num4 = 44;
							continue;
						case 3:
							if (num2 == num)
							{
								num4 = 42;
								continue;
							}
							goto IL_1F5;
						case 4:
							if (paragraph.Items[num5] == this)
							{
								num4 = 26;
								continue;
							}
							goto IL_503;
						case 5:
							if (paragraph.Items[num5 + 1] is FieldMark)
							{
								num4 = 13;
								continue;
							}
							goto IL_503;
						case 6:
							num4 = 19;
							continue;
						case 7:
							if (num5 == 0)
							{
								num4 = 34;
								continue;
							}
							goto IL_5A3;
						case 8:
							goto IL_20A;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1E4;
							default:
								if (false)
								{
								}
								goto IL_5A3;
							}
							break;
						case 10:
							goto IL_5F6;
						case 11:
							if ((paragraph.Items[num5 + 1] as FieldMark).Type == FieldMarkType.FieldSeparator)
							{
								num4 = 20;
								continue;
							}
							goto IL_503;
						case 12:
							flag = false;
							num4 = 18;
							continue;
						case 13:
							num4 = 11;
							continue;
						case 14:
							goto IL_11B;
						case 15:
							if (ownerTextBody.Items[num2] is Paragraph)
							{
								num4 = 28;
								continue;
							}
							goto IL_620;
						case 16:
							if (paragraph.Items[0] is FieldMark)
							{
								num4 = 6;
								continue;
							}
							goto IL_28D;
						case 17:
							goto IL_4D2;
						case 18:
							goto IL_11B;
						case 19:
							if (flag)
							{
								goto IL_1E4;
							}
							goto IL_28D;
						case 20:
							num3++;
							num5++;
							num4 = 21;
							continue;
						case 21:
							goto IL_5F6;
						case 22:
							goto IL_20A;
						case 23:
							num3++;
							num4 = 17;
							continue;
						case 24:
							if (num5 >= paragraph.Items.Count)
							{
								num4 = 14;
								continue;
							}
							num4 = 4;
							continue;
						case 25:
						{
							FieldMark fieldMark;
							if (fieldMark.Type == FieldMarkType.FieldEnd)
							{
								num4 = 41;
								continue;
							}
							goto IL_4D2;
						}
						case 26:
							num4 = 5;
							continue;
						case 27:
							if (paragraph.Items[num5] is TextRange)
							{
								num4 = 2;
								continue;
							}
							goto IL_4D2;
						case 28:
							paragraph = (ownerTextBody.Items[num2] as Paragraph);
							num6 = 0;
							num4 = 3;
							continue;
						case 29:
							if (paragraph.Items[num5] is FieldMark)
							{
								num4 = 39;
								continue;
							}
							num4 = 27;
							continue;
						case 30:
							if (num2 >= ownerTextBody.Items.Count)
							{
								num4 = 0;
								continue;
							}
							num4 = 15;
							continue;
						case 31:
							return;
						case 32:
							goto IL_4D2;
						case 33:
							goto IL_2AD;
						case 34:
							num4 = 37;
							continue;
						case 35:
							goto IL_620;
						case 36:
							goto IL_1F5;
						case 37:
							if (num3 != 1)
							{
								num4 = 9;
								continue;
							}
							goto IL_11B;
						case 38:
							ownerTextBody.Items.Remove(paragraph);
							num2--;
							num4 = 35;
							continue;
						case 39:
						{
							FieldMark fieldMark = paragraph.Items[num5] as FieldMark;
							num4 = 45;
							continue;
						}
						case 40:
							if (!flag)
							{
								num4 = 31;
								continue;
							}
							goto IL_620;
						case 41:
							num4 = 7;
							continue;
						case 42:
							num6 = paragraph.Items.IndexOf(this);
							num4 = 36;
							continue;
						case 43:
							if (paragraph.Items.Count == 0)
							{
								num4 = 38;
								continue;
							}
							num4 = 16;
							continue;
						case 44:
							if ((paragraph.Items[num5] as TextRange).Text == ClipboardData.b("╰㱲㙴", a_))
							{
								num4 = 12;
								continue;
							}
							goto IL_4D2;
						case 45:
						{
							FieldMark fieldMark;
							if (fieldMark.Type == FieldMarkType.FieldSeparator)
							{
								num4 = 23;
								continue;
							}
							num4 = 25;
							continue;
						}
						case 46:
							goto IL_1F0;
						}
						break;
						IL_11B:
						num4 = 43;
						continue;
						IL_1E4:
						num4 = 46;
						continue;
						IL_1F5:
						num5 = num6;
						num4 = 22;
						continue;
						IL_20A:
						num4 = 24;
						continue;
						IL_28D:
						num4 = 40;
						continue;
						IL_2AD:
						num4 = 30;
						continue;
						IL_4D2:
						paragraph.Items.Remove(paragraph.Items[num5]);
						num5--;
						num4 = 10;
						continue;
						IL_503:
						num4 = 29;
						continue;
						IL_5A3:
						num3--;
						num4 = 32;
						continue;
						IL_5F6:
						num5++;
						num4 = 8;
						continue;
						IL_620:
						num2++;
						num4 = 1;
					}
				}
				IL_1F0:
				paragraph.Items.Insert(0, this);
				paragraph.Items.Insert(1, ownerParagraph.Items[0]);
				TextRange textRange = new TextRange(base.Document);
				textRange.Text = ClipboardData.b("╰㱲㙴", a_);
				paragraph.Items.Insert(2, textRange);
				ownerTextBody.Items.Remove(ownerParagraph);
				return;
			}
			}
		}

		// Token: 0x06004493 RID: 17555 RVA: 0x003FFAD8 File Offset: 0x003FEAD8
		private void ᜂ()
		{
			int a_ = 9;
			for (;;)
			{
				int num = base.Document.Bookmarks.Count - 1;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_4D;
					case 2:
					{
						if (num < 0)
						{
							num2 = 0;
							continue;
						}
						Bookmark bookmark = base.Document.Bookmarks[num];
						if (true)
						{
						}
						num2 = 4;
						continue;
					}
					case 3:
					{
						Bookmark bookmark;
						base.Document.Bookmarks.Remove(bookmark);
						num2 = 1;
						continue;
					}
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_63;
						default:
						{
							if (false)
							{
							}
							Bookmark bookmark;
							if (bookmark.Name.StartsWith(ClipboardData.b("の╰ᱲᙴ", a_)))
							{
								num2 = 3;
								continue;
							}
							goto IL_4D;
						}
						}
						break;
					case 5:
						goto IL_F2;
					case 6:
						goto IL_63;
					}
					break;
					IL_4D:
					num--;
					num2 = 6;
					continue;
					IL_F2:
					num2 = 2;
					continue;
					IL_63:
					goto IL_F2;
				}
			}
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x003FFBF4 File Offset: 0x003FEBF4
		private new bool ᜁ(string A_0)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 1;
				Dictionary<int, string>.Enumerator enumerator;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4B;
					case 2:
						goto IL_6A;
					case 3:
						A_0 = A_0.ToLower().Replace(ClipboardData.b("卲", a_), "");
						num = 4;
						continue;
					case 4:
						goto IL_4B;
					}
					if (A_0 != null)
					{
						num = 3;
						continue;
					}
					A_0 = ClipboardData.b("ᵲᩴնᑸ᩺ᅼ", a_);
					if (true)
					{
					}
					num = 0;
					continue;
					IL_4B:
					enumerator = this.TOCLevels.GetEnumerator();
					num = 2;
				}
				IL_6A:
				bool result;
				try
				{
					num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_133;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							KeyValuePair<int, string> keyValuePair = enumerator.Current;
							string a = keyValuePair.Value.ToLower().Replace(ClipboardData.b("卲", a_), "");
							goto IL_162;
						}
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_162;
							default:
								if (false)
								{
								}
								num = 6;
								continue;
							}
							break;
						case 4:
						{
							string a;
							if (a == A_0)
							{
								num = 5;
								continue;
							}
							break;
						}
						case 5:
							result = true;
							num = 1;
							continue;
						case 6:
							goto IL_194;
						}
						IL_EF:
						num = 2;
						continue;
						goto IL_EF;
						IL_162:
						num = 4;
					}
					IL_133:
					return result;
					IL_194:
					return false;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
				return result;
			}
			}
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x003FFDBC File Offset: 0x003FEDBC
		private new int ᜀ(string A_0)
		{
			int a_ = 1;
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				int result = 0;
				A_0 = A_0.ToLower().Replace(ClipboardData.b("䝦", a_), "");
				using (Dictionary<int, string>.Enumerator enumerator = this.TOCLevels.GetEnumerator())
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
						{
							KeyValuePair<int, string> keyValuePair;
							result = keyValuePair.Key;
							num = 5;
							continue;
						}
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 6;
								continue;
							}
							KeyValuePair<int, string> keyValuePair = enumerator.Current;
							string a = keyValuePair.Value.ToLower().Replace(ClipboardData.b("䝦", a_), "");
							num = 3;
							continue;
						}
						case 3:
						{
							string a;
							if (a == A_0)
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_109;
								default:
									if (false)
									{
									}
									num = 1;
									continue;
								}
							}
							break;
						}
						case 4:
							goto IL_12D;
						case 5:
							goto IL_121;
						case 6:
							goto IL_109;
						}
						IL_E8:
						num = 2;
						continue;
						goto IL_E8;
						IL_121:
						num = 4;
						continue;
						IL_109:
						goto IL_121;
					}
					IL_12D:;
				}
				return result;
			}
			}
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x003FFF24 File Offset: 0x003FEF24
		private new void ᜀ(IParagraph A_0, Field A_1, int A_2, int A_3)
		{
			string text;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_99;
				default:
				{
					if (false)
					{
					}
					text = this.ᜁ();
					BookmarkStart entity = new BookmarkStart(base.Document, text);
					A_0.Items.Insert(A_2, entity);
					this.ᜀ(A_0, A_1, text);
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_99;
						case 1:
							if (A_1 == null)
							{
								num = 2;
								continue;
							}
							goto IL_9B;
						case 2:
							if (true)
							{
							}
							A_3 = A_0.Items.Count;
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
				}
			}
			IL_99:
			IL_9B:
			BookmarkEnd entity2 = new BookmarkEnd(base.Document, text);
			A_0.Items.Insert(A_3, entity2);
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x003FFFE8 File Offset: 0x003FEFE8
		private new void ᜀ(IParagraph A_0, Field A_1, string A_2)
		{
			int a_ = 19;
			switch (0)
			{
			default:
				for (;;)
				{
					int a_2 = this.ᜀ(A_0.StyleName);
					string a_3 = string.Empty;
					int num = 0;
					for (;;)
					{
						Paragraph paragraph;
						switch (num)
						{
						case 0:
							if (A_1 != null)
							{
								num = 3;
								continue;
							}
							goto IL_8A;
						case 1:
							if (!this.IncludePageNumbers)
							{
								return;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9C;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 2:
							return;
						case 3:
						{
							a_3 = A_1.Value;
							string text = A_1.FormattingString;
							text = text.Replace(ClipboardData.b("╸᝺", a_), "").Replace(ClipboardData.b("奸", a_), "");
							a_2 = int.Parse(text);
							num = 5;
							continue;
						}
						case 4:
							this.ᜀ(paragraph, A_2);
							if (true)
							{
							}
							num = 2;
							continue;
						case 5:
							goto IL_8A;
						}
						break;
						IL_9C:
						num = 1;
						continue;
						IL_8A:
						paragraph = this.ᜀ(a_2);
						this.ᜀ(A_0, paragraph, a_3, A_2);
						goto IL_9C;
					}
				}
				return;
			}
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x0040012C File Offset: 0x003FF12C
		private new void ᜀ(IParagraph A_0, Paragraph A_1, string A_2, string A_3)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				Field field;
				for (;;)
				{
					field = new Field(base.Document);
					field.Type = FieldType.FieldHyperlink;
					A_1.Items.Add(field);
					A_1.ᜀ(FieldMarkType.FieldSeparator);
					int num = 1;
					for (;;)
					{
						IEnumerator enumerator;
						switch (num)
						{
						case 0:
							try
							{
								num = 2;
								for (;;)
								{
									ITextRange textRange;
									switch (num)
									{
									case 0:
									{
										if (!enumerator.MoveNext())
										{
											num = 9;
											continue;
										}
										ParagraphBase paragraphBase = (ParagraphBase)enumerator.Current;
										num = 4;
										continue;
									}
									case 1:
									{
										ParagraphBase paragraphBase;
										if ((paragraphBase as TextRange).Text != ClipboardData.b("杭", a_))
										{
											num = 11;
											continue;
										}
										break;
									}
									case 4:
									{
										ParagraphBase paragraphBase;
										if (paragraphBase is TextRange)
										{
											num = 10;
											continue;
										}
										break;
									}
									case 5:
									{
										ParagraphBase paragraphBase;
										textRange.CharacterFormat.Italic = (paragraphBase as TextRange).CharacterFormat.Italic;
										num = 12;
										continue;
									}
									case 6:
										goto IL_268;
									case 7:
										goto IL_1EA;
									case 8:
									{
										ParagraphBase paragraphBase;
										textRange.CharacterFormat.Bold = (paragraphBase as TextRange).CharacterFormat.Bold;
										num = 7;
										continue;
									}
									case 9:
										num = 6;
										continue;
									case 10:
										num = 1;
										continue;
									case 11:
									{
										ParagraphBase paragraphBase;
										textRange = A_1.AppendText((paragraphBase as TextRange).Text);
										num = 14;
										continue;
									}
									case 12:
										goto IL_1C0;
									case 13:
									{
										ParagraphBase paragraphBase;
										if ((paragraphBase as TextRange).CharacterFormat.HasValue(5))
										{
											num = 5;
											continue;
										}
										goto IL_1C0;
									}
									case 14:
									{
										ParagraphBase paragraphBase;
										if ((paragraphBase as TextRange).CharacterFormat.HasValue(4))
										{
											num = 8;
											continue;
										}
										goto IL_1EA;
									}
									}
									IL_139:
									num = 0;
									continue;
									goto IL_139;
									IL_1C0:
									textRange.CharacterFormat.CharStyleName = ClipboardData.b("♭९ɱᅳѵᑷ፹ቻᕽ", a_);
									num = 3;
									continue;
									IL_1EA:
									num = 13;
								}
								IL_268:
								goto IL_334;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_2CF;
										case 1:
											if (disposable != null)
											{
												goto IL_298;
											}
											goto IL_2D1;
										case 2:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_298;
											default:
												if (false)
												{
												}
												disposable.Dispose();
												num = 0;
												continue;
											}
											break;
										}
										break;
										IL_298:
										num = 2;
									}
								}
								IL_2CF:
								IL_2D1:;
							}
							goto IL_2D2;
						case 1:
							if (!string.IsNullOrEmpty(A_2))
							{
								num = 2;
								continue;
							}
							goto IL_2D2;
						case 2:
						{
							ITextRange textRange = A_1.AppendText(A_2);
							textRange.CharacterFormat.CharStyleName = ClipboardData.b("♭९ɱᅳѵᑷ፹ቻᕽ", a_);
							num = 3;
							continue;
						}
						case 3:
							goto IL_32F;
						}
						break;
						IL_2D2:
						this.ᜀ(A_0, A_1);
						enumerator = A_0.Items.GetEnumerator();
						num = 0;
					}
				}
				IL_32F:
				IL_334:
				FieldMark entity = new FieldMark(base.Document, FieldMarkType.FieldEnd);
				A_1.Items.Add(entity);
				Hyperlink hyperlink = new Hyperlink(field);
				hyperlink.Type = HyperlinkType.Bookmark;
				hyperlink.BookmarkName = A_3;
				return;
			}
			}
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x004004BC File Offset: 0x003FF4BC
		private new void ᜀ(Paragraph A_0, string A_1)
		{
			int a_ = 18;
			int num = 2;
			TextRange textRange;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					A_0.Format.Tabs.AddTab(this.ᜀ(A_0), Spire.Doc.Documents.TabJustification.Right, Spire.Doc.Documents.TabLeader.Dotted);
					textRange = new TextRange(base.Document);
					textRange.Text = ClipboardData.b("煷", a_);
					A_0.Items.Insert(A_0.Items.Count - 1, textRange);
					goto IL_BB;
				case 1:
					goto IL_C6;
				}
				if (!this.RightAlignPageNumbers)
				{
					break;
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
					num = 0;
					continue;
				}
				IL_BB:
				num = 1;
			}
			IL_C6:
			Field field = new Field(base.Document);
			field.Type = FieldType.FieldPageRef;
			field.Code = ClipboardData.b("⡷㭹㭻㭽퉿잁슃ꚅ", a_) + A_1 + ClipboardData.b("塷♹ᑻ", a_);
			field.m_fieldValue = A_1 + ClipboardData.b("塷♹ᑻ", a_);
			A_0.Items.Insert(A_0.Items.Count - 1, field);
			FieldMark entity = new FieldMark(base.Document, FieldMarkType.FieldSeparator);
			A_0.Items.Insert(A_0.Items.Count - 1, entity);
			textRange = new TextRange(base.Document);
			A_0.Items.Insert(A_0.Items.Count - 1, textRange);
			entity = new FieldMark(base.Document, FieldMarkType.FieldEnd);
			A_0.Items.Insert(A_0.Items.Count - 1, entity);
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x00400674 File Offset: 0x003FF674
		private new float ᜀ(DocumentObject A_0)
		{
			float result;
			for (;;)
			{
				result = 0f;
				DocumentObject documentObject = A_0;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						documentObject = documentObject.Owner;
						num = 6;
						continue;
					case 2:
						goto IL_5A;
					case 3:
						if (true)
						{
						}
						result = (float)((double)(documentObject as Section).PageSetup.ClientWidth - 0.5);
						num = 0;
						continue;
					case 4:
						if (documentObject is Section)
						{
							num = 3;
							continue;
						}
						return result;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (false)
							{
							}
							goto IL_81;
						}
						break;
					case 6:
						goto IL_81;
					case 7:
						if (documentObject.Owner != null)
						{
							num = 1;
							continue;
						}
						goto IL_5A;
					case 8:
						if (documentObject is Section)
						{
							num = 2;
							continue;
						}
						num = 7;
						continue;
					}
					break;
					IL_5A:
					num = 4;
					continue;
					IL_81:
					num = 8;
				}
			}
			return result;
		}

		// Token: 0x0600449B RID: 17563 RVA: 0x0040078C File Offset: 0x003FF78C
		private new Paragraph ᜀ(int A_0)
		{
			switch (0)
			{
			default:
			{
				Paragraph paragraph;
				for (;;)
				{
					Body ownerTextBody = this.LastTOCParagraph.OwnerTextBody;
					int index = ownerTextBody.Items.IndexOf(this.LastTOCParagraph);
					int num = this.LastTOCParagraph.Items.IndexOf(this);
					int num2 = 7;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							if (this.LastTOCParagraph.Items[num3] is FieldMark)
							{
								num2 = 14;
								continue;
							}
							num2 = 8;
							continue;
						case 1:
							if (num3 >= this.LastTOCParagraph.Items.Count)
							{
								num2 = 11;
								continue;
							}
							num2 = 16;
							continue;
						case 2:
							this.LastTOCParagraph.Items.Remove(this.LastTOCParagraph.Items[num3]);
							num3--;
							num2 = 13;
							continue;
						case 3:
							num3 = 0;
							num2 = 9;
							continue;
						case 4:
							goto IL_190;
						case 5:
							this.ᜀ(this.LastTOCParagraph, num);
							this.\u171A = base.OwnerParagraph;
							num2 = 10;
							continue;
						case 6:
							paragraph.Items.Insert(paragraph.Items.Count, this.LastTOCParagraph.Items[num3]);
							num3--;
							num2 = 17;
							continue;
						case 7:
							if (num > 0)
							{
								num2 = 5;
								continue;
							}
							goto IL_12A;
						case 8:
							if (this.LastTOCParagraph.Items[num3] is TextRange)
							{
								num2 = 2;
								continue;
							}
							goto IL_190;
						case 9:
							IL_2AF:
							goto IL_207;
						case 10:
							goto IL_12A;
						case 11:
							return paragraph;
						case 12:
							if (this.LastTOCParagraph == base.OwnerParagraph)
							{
								num2 = 3;
								continue;
							}
							return paragraph;
						case 13:
							return paragraph;
						case 14:
							paragraph.Items.Insert(paragraph.Items.Count, this.LastTOCParagraph.Items[num3]);
							num3--;
							num2 = 4;
							continue;
						case 15:
							goto IL_207;
						case 16:
							if (this.LastTOCParagraph.Items[num3] == this)
							{
								num2 = 6;
								continue;
							}
							num2 = 0;
							continue;
						case 17:
							goto IL_190;
						}
						break;
						IL_12A:
						index = ownerTextBody.Items.IndexOf(this.LastTOCParagraph);
						paragraph = new Paragraph(base.Document);
						ownerTextBody.Items.Insert(index, paragraph);
						A_0 += 18;
						paragraph.ApplyStyle((BuiltinStyle)A_0);
						num2 = 12;
						continue;
						IL_190:
						num3++;
						num2 = 15;
						continue;
						IL_207:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2AF;
						default:
							if (false)
							{
							}
							num2 = 1;
							break;
						}
					}
				}
				return paragraph;
			}
			}
		}

		// Token: 0x0600449C RID: 17564 RVA: 0x00400AC4 File Offset: 0x003FFAC4
		private new string ᜁ()
		{
			int a_ = 18;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.\u1719++;
			return ClipboardData.b("❷⹹፻ᵽ", a_) + string.Format(ClipboardData.b("ͷ䩹䙻乽끿늁뒃뚅뢇몉벋뺍ꂏ", a_), this.\u1719);
		}

		// Token: 0x0600449D RID: 17565 RVA: 0x00400B4C File Offset: 0x003FFB4C
		private new void ᜀ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_4F:
					Paragraph ownerParagraph = base.OwnerParagraph;
					Body ownerTextBody = ownerParagraph.OwnerTextBody;
					int num = ownerTextBody.ChildObjects.IndexOf(ownerParagraph);
					int num2 = ownerTextBody.ChildObjects.IndexOf(this.LastTOCParagraph);
					int num3 = num;
					int num4 = 0;
					int num5 = 5;
					for (;;)
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
							switch (num5)
							{
							case 0:
							{
								Paragraph paragraph = ownerTextBody.ChildObjects[num3] as Paragraph;
								(paragraph.Items[paragraph.Items.Count - 3] as TextRange).Text = this.TOCEntryPageNumbers[num4].ToString();
								num4++;
								num5 = 3;
								continue;
							}
							case 1:
								return;
							case 2:
								goto IL_147;
							case 3:
								goto IL_90;
							case 4:
								if (ownerTextBody.ChildObjects[num3] is Paragraph)
								{
									num5 = 0;
									continue;
								}
								goto IL_90;
							case 5:
								goto IL_147;
							case 6:
								if (num3 >= num2)
								{
									num5 = 1;
									continue;
								}
								num5 = 4;
								continue;
							}
							goto IL_4F;
							IL_90:
							num3++;
							num5 = 2;
							continue;
						}
						IL_147:
						if (true)
						{
						}
						num5 = 6;
					}
				}
				return;
			}
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x00400CC8 File Offset: 0x003FFCC8
		private new void ᜀ(IParagraph A_0, Paragraph A_1)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
				for (;;)
				{
					Paragraph paragraph = A_0 as Paragraph;
					ListFormat listFormat = null;
					ParagraphStyle paragraphStyle = paragraph.ParaStyle;
					int num = 8;
					for (;;)
					{
						ListStyle currentListStyle;
						int num2;
						spr\u177D spr_u177D;
						ListLevel a_;
						string text;
						switch (num)
						{
						case 0:
							goto IL_100;
						case 1:
							currentListStyle = listFormat.CurrentListStyle;
							num2 = 0;
							num = 30;
							continue;
						case 2:
							if (listFormat.LFOStyleName.Length > 0)
							{
								num = 13;
								continue;
							}
							goto IL_DC;
						case 3:
							num = 11;
							continue;
						case 4:
							if (paragraphStyle.ListFormat.ListType != ListType.NoList)
							{
								num = 15;
								continue;
							}
							goto IL_132;
						case 5:
							goto IL_132;
						case 6:
							goto IL_132;
						case 7:
							listFormat = paragraph.ListFormat;
							num = 5;
							continue;
						case 8:
							if (paragraph.ListFormat.ListType != ListType.NoList)
							{
								num = 7;
								continue;
							}
							num = 4;
							continue;
						case 9:
							num = 16;
							continue;
						case 10:
							num2 = paragraph.ListFormat.ListLevelNumber;
							num = 27;
							continue;
						case 11:
							if (spr_u177D.ᜃ().ᜀ(num2).OverrideFormatting)
							{
								num = 17;
								continue;
							}
							goto IL_1CC;
						case 12:
							num = 2;
							continue;
						case 13:
							spr_u177D = base.Document.ListOverrides.ᜀ(listFormat.LFOStyleName);
							num = 22;
							continue;
						case 14:
							goto IL_1CC;
						case 15:
							listFormat = paragraphStyle.ListFormat;
							num = 6;
							continue;
						case 16:
							if (spr_u177D.ᜃ().ᜁ(num2))
							{
								num = 3;
								continue;
							}
							goto IL_1CC;
						case 17:
							a_ = spr_u177D.ᜃ().ᜀ(num2).OverrideListLevel;
							num = 14;
							continue;
						case 18:
							if (text != string.Empty)
							{
								num = 24;
								continue;
							}
							return;
						case 19:
							goto IL_13E;
						case 20:
							if (spr_u177D != null)
							{
								num = 9;
								continue;
							}
							goto IL_1CC;
						case 21:
							if (listFormat.CurrentListStyle != null)
							{
								num = 1;
								continue;
							}
							return;
						case 22:
							goto IL_DC;
						case 23:
							return;
						case 24:
							this.ᜀ(A_0, A_1, text);
							num = 23;
							continue;
						case 25:
							num2 = paragraphStyle.ListFormat.ListLevelNumber;
							num = 0;
							continue;
						case 26:
							num = 21;
							continue;
						case 27:
							goto IL_100;
						case 28:
							if (listFormat.LFOStyleName != null)
							{
								num = 12;
								continue;
							}
							goto IL_DC;
						case 29:
							if (paragraphStyle.ListFormat.HasKey(0))
							{
								num = 25;
								continue;
							}
							goto IL_100;
						case 30:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_13E;
							default:
								if (false)
								{
								}
								if (paragraph.ListFormat.HasKey(0))
								{
									num = 10;
									continue;
								}
								num = 29;
								continue;
							}
							break;
						}
						break;
						IL_DC:
						num = 20;
						continue;
						IL_100:
						a_ = currentListStyle.GetNearLevel(num2);
						spr_u177D = null;
						num = 28;
						continue;
						IL_132:
						num = 19;
						continue;
						IL_13E:
						if (listFormat != null)
						{
							num = 26;
							continue;
						}
						return;
						IL_1CC:
						text = base.Document.ᜀ(paragraph, listFormat, a_);
						num = 18;
					}
				}
				return;
			}
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x004010A8 File Offset: 0x004000A8
		private new void ᜀ(IParagraph A_0, Paragraph A_1, string A_2)
		{
			int a_ = 0;
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					ITextRange textRange = A_1.AppendText(A_2);
					int num = 2;
					for (;;)
					{
						int num2;
						spr\u19E0 spr_u19E;
						switch (num)
						{
						case 0:
							if (A_1.Format.BaseFormat != null)
							{
								num = 9;
								continue;
							}
							goto IL_207;
						case 1:
							goto IL_207;
						case 2:
							if (A_0.BreakCharacterFormat.HasValue(4))
							{
								num = 11;
								continue;
							}
							goto IL_E6;
						case 3:
							goto IL_207;
						case 4:
							num2 = (int)A_1.Format.LeftIndent;
							num = 3;
							continue;
						case 5:
							num2 = (int)(A_1.Format.BaseFormat as ParagraphFormat).LeftIndent;
							num = 1;
							continue;
						case 6:
							textRange.CharacterFormat.Italic = A_0.BreakCharacterFormat.Italic;
							num = 14;
							continue;
						case 7:
							if (A_0.BreakCharacterFormat.HasValue(5))
							{
								num = 6;
								continue;
							}
							goto IL_28D;
						case 8:
							goto IL_E6;
						case 9:
							num = 12;
							continue;
						case 10:
							try
							{
								num2 += (int)spr_u19E.ᜀ(textRange as TextRange, textRange.Text).Width;
								num2 += 14;
								num2 = (int)Math.Ceiling(num2 / 11m) * 11;
								A_1.Format.Tabs.AddTab((float)num2, Spire.Doc.Documents.TabJustification.Left, Spire.Doc.Documents.TabLeader.NoLeader);
								textRange = A_1.AppendText(ClipboardData.b("潥", a_));
								return;
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_1DD;
									case 1:
										((IDisposable)spr_u19E).Dispose();
										goto IL_1D4;
									}
									if (spr_u19E == null)
									{
										break;
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
										num = 1;
										continue;
									}
									IL_1D4:
									num = 0;
								}
								IL_1DD:;
							}
							goto IL_1E0;
						case 11:
							goto IL_1E0;
						case 12:
							if (A_1.Format.BaseFormat.HasValue(2))
							{
								num = 5;
								continue;
							}
							goto IL_207;
						case 13:
							if (A_1.Format.HasValue(2))
							{
								num = 4;
								continue;
							}
							num = 0;
							continue;
						case 14:
							goto IL_28D;
						}
						break;
						IL_E6:
						num = 7;
						continue;
						IL_1E0:
						textRange.CharacterFormat.Bold = A_0.BreakCharacterFormat.Bold;
						num = 8;
						continue;
						IL_207:
						spr_u19E = new spr\u19E0();
						num = 10;
						continue;
						IL_28D:
						textRange.CharacterFormat.CharStyleName = ClipboardData.b("⹥ᅧᩩ५ᱭᱯ᭱ᩳᵵ", a_);
						num2 = 0;
						num = 13;
					}
				}
				return;
			}
		}

		// Token: 0x060044A0 RID: 17568 RVA: 0x004013A8 File Offset: 0x004003A8
		private new int[] ᜀ(int[] A_0)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 2;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						if (num >= A_0.Length - 1)
						{
							num2 = 4;
							continue;
						}
						num3 = num + 1;
						num2 = 9;
						continue;
					case 1:
						goto IL_40;
					case 2:
						goto IL_80;
					case 3:
						goto IL_58;
					case 4:
						goto IL_9E;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4E;
						default:
						{
							if (false)
							{
							}
							int num4 = A_0[num];
							A_0[num] = A_0[num3];
							A_0[num3] = num4;
							num2 = 3;
							continue;
						}
						}
						break;
					case 6:
						if (num3 >= A_0.Length)
						{
							goto IL_4E;
						}
						num2 = 8;
						continue;
					case 7:
						num++;
						num2 = 10;
						continue;
					case 8:
						if (A_0[num] > A_0[num3])
						{
							num2 = 5;
							continue;
						}
						goto IL_58;
					case 9:
						goto IL_40;
					case 10:
						goto IL_80;
					}
					break;
					IL_40:
					num2 = 6;
					continue;
					IL_4E:
					num2 = 7;
					continue;
					IL_58:
					num3++;
					num2 = 1;
					continue;
					IL_80:
					num2 = 0;
				}
			}
			IL_9E:
			if (true)
			{
			}
			return A_0;
		}

		// Token: 0x060044A1 RID: 17569 RVA: 0x004014C4 File Offset: 0x004004C4
		protected override void CreateLayoutInfo()
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
			this.ᜀ = new spr\u22A8();
		}

		// Token: 0x060044A2 RID: 17570 RVA: 0x0040150C File Offset: 0x0040050C
		protected override void InitXDLSHolder()
		{
			int a_ = 1;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_73;
				case 1:
					this.\u1718();
					goto IL_6B;
				}
				if (true)
				{
				}
				if (!this.\u1714)
				{
					break;
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
					num = 1;
					continue;
				}
				IL_6B:
				num = 0;
			}
			IL_73:
			base.XDLSHolder.AddElement(ClipboardData.b("፦٨ࡪ䁬८ᡰᙲᥴ፶", a_), this.ᜉ);
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x004015B0 File Offset: 0x004005B0
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 19;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.WriteXmlAttributes(writer);
			writer.WriteValue(ClipboardData.b("൸ɺർ᩾", a_), ParagraphItemType.TOC);
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x00401618 File Offset: 0x00400618
		protected override object CloneImpl()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_6A;
				case 2:
					this.\u1718();
					goto IL_62;
				}
				if (true)
				{
				}
				if (!this.\u1714)
				{
					break;
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
					num = 2;
					continue;
				}
				IL_62:
				num = 1;
			}
			IL_6A:
			TableOfContent tableOfContent = (TableOfContent)base.CloneImpl();
			tableOfContent.ᜉ = (Field)this.ᜉ.Clone();
			return tableOfContent;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060044A5 RID: 17573 RVA: 0x004016B4 File Offset: 0x004006B4
		// (set) Token: 0x060044A6 RID: 17574 RVA: 0x004016F4 File Offset: 0x004006F4
		bool spr\u1D30.IsClipped
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060044A7 RID: 17575 RVA: 0x00401734 File Offset: 0x00400734
		// (set) Token: 0x060044A8 RID: 17576 RVA: 0x00401774 File Offset: 0x00400774
		bool spr\u1D30.IsVerticalText
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060044A9 RID: 17577 RVA: 0x004017B4 File Offset: 0x004007B4
		// (set) Token: 0x060044AA RID: 17578 RVA: 0x004017F0 File Offset: 0x004007F0
		bool spr\u1D30.IsSkip
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
				return true;
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060044AB RID: 17579 RVA: 0x00401830 File Offset: 0x00400830
		// (set) Token: 0x060044AC RID: 17580 RVA: 0x00401870 File Offset: 0x00400870
		bool spr\u1D30.IsSkipBottomAlign
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060044AD RID: 17581 RVA: 0x004018B0 File Offset: 0x004008B0
		bool spr\u1D30.IsLineContainer
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x004018F0 File Offset: 0x004008F0
		ChildrenLayoutDirection spr\u1D30.ChildrenLayoutDirection
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060044AF RID: 17583 RVA: 0x00401930 File Offset: 0x00400930
		// (set) Token: 0x060044B0 RID: 17584 RVA: 0x00401970 File Offset: 0x00400970
		bool spr\u1D30.IsLineBreak
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060044B1 RID: 17585 RVA: 0x004019B0 File Offset: 0x004009B0
		// (set) Token: 0x060044B2 RID: 17586 RVA: 0x004019F0 File Offset: 0x004009F0
		bool spr\u1D30.TextWrap
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x00401A30 File Offset: 0x00400A30
		// (set) Token: 0x060044B4 RID: 17588 RVA: 0x00401A70 File Offset: 0x00400A70
		bool spr\u1D30.IsPageBreakItem
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060044B5 RID: 17589 RVA: 0x00401AB0 File Offset: 0x00400AB0
		spr\u2326 sprḰ.Paddings
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x00401AF0 File Offset: 0x00400AF0
		spr\u2326 sprḰ.Margins
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x040035E7 RID: 13799
		private new const int ᜀ = 3;

		// Token: 0x040035E8 RID: 13800
		private new const int ᜁ = 1;

		// Token: 0x040035E9 RID: 13801
		private const char ᜂ = 'o';

		// Token: 0x040035EA RID: 13802
		private long[] \u2593\u0089\u008F\u009D;

		// Token: 0x040035EB RID: 13803
		private const char ᜃ = 'h';

		// Token: 0x040035EC RID: 13804
		private new const char ᜄ = 'n';

		// Token: 0x040035ED RID: 13805
		private const char ᜅ = 'p';

		// Token: 0x040035EE RID: 13806
		private const char ᜆ = 'u';

		// Token: 0x040035EF RID: 13807
		private const char ᜇ = 'f';

		// Token: 0x040035F0 RID: 13808
		private const char ᜈ = 't';

		// Token: 0x040035F1 RID: 13809
		private Field ᜉ;

		// Token: 0x040035F2 RID: 13810
		private bool ᜊ = true;

		// Token: 0x040035F3 RID: 13811
		private long \u2593\u00A2\u0098\u0081;

		// Token: 0x040035F4 RID: 13812
		private int ᜋ = 3;

		// Token: 0x040035F5 RID: 13813
		private int ᜌ = 1;

		// Token: 0x040035F6 RID: 13814
		private bool \u170D;

		// Token: 0x040035F7 RID: 13815
		private string ᜎ;

		// Token: 0x040035F8 RID: 13816
		private long[] \u2609\u00AF\u0089\u00A2;

		// Token: 0x040035F9 RID: 13817
		private bool ᜏ = true;

		// Token: 0x040035FA RID: 13818
		private bool ᜐ = true;

		// Token: 0x040035FB RID: 13819
		private bool ᜑ;

		// Token: 0x040035FC RID: 13820
		private new bool \u1712 = true;

		// Token: 0x040035FD RID: 13821
		private new Dictionary<int, ParagraphStyle> \u1713;

		// Token: 0x040035FE RID: 13822
		private bool \u1714;

		// Token: 0x040035FF RID: 13823
		private bool \u1715;

		// Token: 0x04003600 RID: 13824
		private float[] \u2593\u0099\u0089\u009E;

		// Token: 0x04003601 RID: 13825
		private string \u1716 = CultureInfo.CurrentCulture.TextInfo.ListSeparator;

		// Token: 0x04003602 RID: 13826
		private Dictionary<int, string> \u1717;

		// Token: 0x04003603 RID: 13827
		private List<int> \u1718;

		// Token: 0x04003604 RID: 13828
		private float[] \u2460\u0094\u008D\u00A7;

		// Token: 0x04003605 RID: 13829
		private int \u1719;

		// Token: 0x04003606 RID: 13830
		private Paragraph \u171A;

		// Token: 0x04003607 RID: 13831
		private long \u2460\u007F\u008B\u0080;

		// Token: 0x04003608 RID: 13832
		private bool \u171B;
	}
}
