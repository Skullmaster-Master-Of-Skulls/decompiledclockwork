using System;
using System.Collections.Generic;
using Spire.CompoundFile.Doc;
using Spire.CompoundFile.Doc.Native;
using Spire.Doc.Interface;

namespace Spire.Doc
{
	// Token: 0x020000A3 RID: 163
	public class BuiltinDocumentProperties : SummaryDocumentProperties
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00015C24 File Offset: 0x00014C24
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00015C88 File Offset: 0x00014C88
		public string Category
		{
			get
			{
				if (!this.ᜀ.ContainsKey(1000))
				{
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
						break;
					}
					return null;
				}
				return this[(PIDDSI)1000].Text;
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
				this.ᜀ((PIDDSI)1000, value);
				this[(PIDDSI)1000].Text = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00015CE0 File Offset: 0x00014CE0
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00015D40 File Offset: 0x00014D40
		public int BytesCount
		{
			get
			{
				if (!this.ᜀ.ContainsKey(4))
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
						break;
					}
					if (true)
					{
					}
					return int.MinValue;
				}
				return this[PIDDSI.ByteCount].Int32;
			}
			internal set
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
				this.ᜀ(PIDDSI.ByteCount, value);
				this[PIDDSI.ByteCount].Int32 = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00015D98 File Offset: 0x00014D98
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00015DF8 File Offset: 0x00014DF8
		public int LinesCount
		{
			get
			{
				if (!this.ᜀ.ContainsKey(5))
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
						break;
					}
					return int.MinValue;
				}
				return this[PIDDSI.LineCount].ToInt();
			}
			internal set
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
				this.ᜀ(PIDDSI.LineCount, value);
				this[PIDDSI.LineCount].Int32 = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00015E50 File Offset: 0x00014E50
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00015EB0 File Offset: 0x00014EB0
		public int ParagraphCount
		{
			get
			{
				if (!this.ᜀ.ContainsKey(6))
				{
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
						break;
					}
					return int.MinValue;
				}
				return this[PIDDSI.ParCount].ToInt();
			}
			internal set
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
				this.ᜀ(PIDDSI.ParCount, value);
				this[PIDDSI.ParCount].Int32 = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00015F08 File Offset: 0x00014F08
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00015F68 File Offset: 0x00014F68
		public int SlideCount
		{
			get
			{
				if (!this.ᜀ.ContainsKey(7))
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return int.MinValue;
					}
					if (false)
					{
					}
					return int.MinValue;
				}
				return this[PIDDSI.SlideCount].ToInt();
			}
			internal set
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
				this.ᜀ((PIDDSI)1005, value);
				this[(PIDDSI)1005].Int32 = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00015FC8 File Offset: 0x00014FC8
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00016028 File Offset: 0x00015028
		public int NoteCount
		{
			get
			{
				if (true)
				{
				}
				if (!this.ᜀ.ContainsKey(8))
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return int.MinValue;
					}
					if (false)
					{
					}
					return int.MinValue;
				}
				return this[PIDDSI.NoteCount].ToInt();
			}
			internal set
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
				this.ᜀ((PIDDSI)1006, value);
				this[(PIDDSI)1006].Int32 = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000204 RID: 516 RVA: 0x00016088 File Offset: 0x00015088
		// (set) Token: 0x06000205 RID: 517 RVA: 0x000160E8 File Offset: 0x000150E8
		public int HiddenCount
		{
			get
			{
				if (!this.ᜀ.ContainsKey(9))
				{
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return int.MinValue;
					}
					if (false)
					{
					}
					return int.MinValue;
				}
				return this[PIDDSI.HiddenCount].ToInt();
			}
			internal set
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
				this.ᜀ((PIDDSI)1007, value);
				this[(PIDDSI)1007].Int32 = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00016148 File Offset: 0x00015148
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000161AC File Offset: 0x000151AC
		public string Company
		{
			get
			{
				if (!this.ᜀ.ContainsKey(1013))
				{
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
						break;
					}
					return null;
				}
				return this[(PIDDSI)1013].Text;
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
				this.ᜀ((PIDDSI)1013, value);
				this[(PIDDSI)1013].Text = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000208 RID: 520 RVA: 0x00016204 File Offset: 0x00015204
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00016268 File Offset: 0x00015268
		public string Manager
		{
			get
			{
				if (!this.ᜀ.ContainsKey(1012))
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
						return null;
					}
				}
				return this[(PIDDSI)1012].Text;
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
				this.ᜀ((PIDDSI)1012, value);
				this[(PIDDSI)1012].Text = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600020A RID: 522 RVA: 0x000162C0 File Offset: 0x000152C0
		internal Dictionary<int, DocumentProperty> DocumentHash
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

		// Token: 0x170000BF RID: 191
		internal DocumentProperty this[PIDDSI A_0]
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
				return this.ᜀ[(int)A_0];
			}
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0001634C File Offset: 0x0001534C
		internal BuiltinDocumentProperties() : this(0, 0)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00016364 File Offset: 0x00015364
		internal BuiltinDocumentProperties(int A_0, int A_1) : base(A_1)
		{
			this.ᜀ = new Dictionary<int, DocumentProperty>(A_0);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00016390 File Offset: 0x00015390
		private bool ᜀ(int A_0)
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
			return this.ᜀ.ContainsKey(A_0);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000163D8 File Offset: 0x000153D8
		public BuiltinDocumentProperties Clone()
		{
			switch (0)
			{
			default:
			{
				BuiltinDocumentProperties builtinDocumentProperties = new BuiltinDocumentProperties(this.ᜀ.Count, this.m_summaryHash.Count);
				Dictionary<int, DocumentProperty>.KeyCollection.Enumerator enumerator = this.ᜀ.Keys.GetEnumerator();
				try
				{
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_199;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							int key = enumerator.Current;
							DocumentProperty documentProperty = this.ᜀ[key];
							builtinDocumentProperties.ᜀ.Add(key, documentProperty.Clone());
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
						}
						}
						IL_11F:
						num = 2;
						continue;
						goto IL_11F;
					}
					IL_199:
					goto IL_E0;
				}
				finally
				{
					if (true)
					{
					}
					((IDisposable)enumerator).Dispose();
				}
				return builtinDocumentProperties;
				for (;;)
				{
					IL_E0:
					using (Dictionary<int, DocumentProperty>.KeyCollection.Enumerator enumerator2 = this.m_summaryHash.Keys.GetEnumerator())
					{
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 1:
								num = 2;
								continue;
							case 2:
								goto IL_CD;
							case 4:
							{
								if (!enumerator2.MoveNext())
								{
									num = 1;
									continue;
								}
								int key2 = enumerator2.Current;
								DocumentProperty documentProperty2 = this.m_summaryHash[key2];
								builtinDocumentProperties.m_summaryHash.Add(key2, documentProperty2.Clone());
								num = 3;
								continue;
							}
							}
							IL_73:
							num = 4;
							continue;
							goto IL_73;
						}
						IL_CD:
						break;
					}
				}
				return builtinDocumentProperties;
			}
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000165B8 File Offset: 0x000155B8
		internal void ᜀ(PIDDSI A_0, object A_1)
		{
			if (this.ᜀ.ContainsKey((int)A_0))
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
					this[A_0].Value = A_1;
					return;
				}
			}
			DocumentProperty value = new DocumentProperty((BuiltInProperty)A_0, A_1);
			this.ᜀ[(int)A_0] = value;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00016628 File Offset: 0x00015628
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 7;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜀ(4))
						{
							num = 17;
							continue;
						}
						goto IL_1DC;
					case 1:
						writer.WriteValue(ClipboardData.b("⹬nᱰͲᑴ᥶x", a_), this.Company);
						num = 24;
						continue;
					case 2:
						if (this.ᜀ(9))
						{
							num = 4;
							continue;
						}
						return;
					case 3:
						if (this.ᜀ(6))
						{
							num = 8;
							continue;
						}
						goto IL_AC;
					case 4:
						writer.WriteValue(ClipboardData.b("╬ٮᕰᝲၴ᥶㩸ᑺࡼᅾ", a_), this.HiddenCount);
						num = 9;
						continue;
					case 5:
						writer.WriteValue(ClipboardData.b("⍬nհᙲ㙴ᡶ౸ᕺॼ", a_), this.NoteCount);
						num = 18;
						continue;
					case 6:
						writer.WriteValue(ClipboardData.b("⁬๮ὰቲቴቶ୸", a_), this.Manager);
						num = 20;
						continue;
					case 7:
						if (this.ᜀ(15))
						{
							num = 1;
							continue;
						}
						goto IL_161;
					case 8:
						writer.WriteValue(ClipboardData.b("㵬๮Ͱቲቴնᡸ୺ᕼ㱾", a_), this.ParagraphCount);
						if (true)
						{
						}
						num = 14;
						continue;
					case 9:
						return;
					case 10:
						writer.WriteValue(ClipboardData.b("Ⅼٮὰᙲٴ㑶ᙸ๺፼୾", a_), this.LinesCount);
						num = 23;
						continue;
					case 11:
						if (this.ᜀ(8))
						{
							num = 5;
							continue;
						}
						goto IL_1B7;
					case 12:
						goto IL_27B;
					case 13:
						if (this.ᜀ(7))
						{
							num = 16;
							continue;
						}
						goto IL_22A;
					case 14:
						goto IL_AC;
					case 15:
						goto IL_22A;
					case 16:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2F3;
						default:
							if (false)
							{
							}
							writer.WriteValue(ClipboardData.b("㹬ͮᡰᝲၴ㑶ᙸ๺፼୾", a_), this.SlideCount);
							num = 15;
							continue;
						}
						break;
					case 17:
						goto IL_2F3;
					case 18:
						goto IL_1B7;
					case 19:
						if (this.ᜀ(14))
						{
							num = 6;
							continue;
						}
						goto IL_113;
					case 20:
						goto IL_113;
					case 21:
						writer.WriteValue(ClipboardData.b("⹬๮հᙲቴᡶ୸ɺ", a_), this.Category);
						num = 12;
						continue;
					case 22:
						if (this.ᜀ(2))
						{
							num = 21;
							continue;
						}
						goto IL_27B;
					case 23:
						goto IL_EF;
					case 24:
						goto IL_161;
					case 25:
						if (this.ᜀ(5))
						{
							num = 10;
							continue;
						}
						goto IL_EF;
					case 26:
						goto IL_1DC;
					}
					break;
					IL_AC:
					num = 13;
					continue;
					IL_EF:
					num = 3;
					continue;
					IL_113:
					num = 22;
					continue;
					IL_161:
					num = 19;
					continue;
					IL_1B7:
					num = 2;
					continue;
					IL_1DC:
					num = 25;
					continue;
					IL_22A:
					num = 11;
					continue;
					IL_27B:
					num = 0;
					continue;
					IL_2F3:
					writer.WriteValue(ClipboardData.b("⽬᙮հᙲٴ㑶ᙸ๺፼୾", a_), this.BytesCount);
					num = 26;
				}
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000169B0 File Offset: 0x000159B0
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 19;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜀ(PIDDSI.HiddenCount, reader.ReadInt(ClipboardData.b("ㅸቺ᥼᭾욄ﲈ歷", a_)));
						num = 6;
						continue;
					case 1:
						goto IL_23C;
					case 2:
						goto IL_B8;
					case 3:
						goto IL_108;
					case 4:
						if (reader.HasAttribute(ClipboardData.b("㕸ቺ፼᩾삂ﾊ", a_)))
						{
							num = 25;
							continue;
						}
						goto IL_108;
					case 5:
						if (reader.HasAttribute(ClipboardData.b("ㅸቺ᥼᭾욄ﲈ歷", a_)))
						{
							num = 0;
							continue;
						}
						return;
					case 6:
						return;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_381;
						default:
							if (false)
							{
							}
							this.ᜀ(PIDDSI.SlideCount, reader.ReadInt(ClipboardData.b("⩸᝺ᑼ᭾삂ﾊ", a_)));
							num = 14;
							continue;
						}
						break;
					case 8:
						this.Company = reader.ReadString(ClipboardData.b("㩸ᑺၼཾﲄ", a_));
						num = 9;
						continue;
					case 9:
						goto IL_19A;
					case 10:
						if (reader.HasAttribute(ClipboardData.b("㝸ᑺॼ᩾슀ﶈ", a_)))
						{
							num = 17;
							continue;
						}
						goto IL_208;
					case 11:
						this.Manager = reader.ReadString(ClipboardData.b("㑸᩺፼Ṿ", a_));
						num = 20;
						continue;
					case 12:
						if (reader.HasAttribute(ClipboardData.b("⥸ོ᩺Ṿ좊搜ﾐ", a_)))
						{
							num = 22;
							continue;
						}
						goto IL_B8;
					case 13:
						if (reader.HasAttribute(ClipboardData.b("㩸ᑺၼཾﲄ", a_)))
						{
							num = 8;
							continue;
						}
						goto IL_19A;
					case 14:
						goto IL_297;
					case 15:
						goto IL_208;
					case 16:
						if (reader.HasAttribute(ClipboardData.b("㩸᩺ॼ᩾ﺆ", a_)))
						{
							num = 26;
							continue;
						}
						goto IL_2FC;
					case 17:
						this.ᜀ(PIDDSI.NoteCount, reader.ReadInt(ClipboardData.b("㝸ᑺॼ᩾슀ﶈ", a_)));
						num = 15;
						continue;
					case 18:
						if (reader.HasAttribute(ClipboardData.b("⩸᝺ᑼ᭾삂ﾊ", a_)))
						{
							num = 7;
							continue;
						}
						goto IL_297;
					case 19:
						goto IL_2FC;
					case 20:
						goto IL_139;
					case 21:
						goto IL_381;
					case 22:
						this.ᜀ(PIDDSI.ParCount, reader.ReadInt(ClipboardData.b("⥸ོ᩺Ṿ좊搜ﾐ", a_)));
						num = 2;
						continue;
					case 23:
						if (reader.HasAttribute(ClipboardData.b("㑸᩺፼Ṿ", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_139;
					case 24:
						if (reader.HasAttribute(ClipboardData.b("㭸ɺॼ᩾삂ﾊ", a_)))
						{
							num = 21;
							continue;
						}
						goto IL_23C;
					case 25:
						this.ᜀ(PIDDSI.LineCount, reader.ReadInt(ClipboardData.b("㕸ቺ፼᩾삂ﾊ", a_)));
						num = 3;
						continue;
					case 26:
						this.Category = reader.ReadString(ClipboardData.b("㩸᩺ॼ᩾ﺆ", a_));
						num = 19;
						continue;
					}
					break;
					IL_B8:
					num = 18;
					continue;
					IL_108:
					num = 12;
					continue;
					IL_139:
					num = 16;
					continue;
					IL_19A:
					num = 23;
					continue;
					IL_208:
					num = 5;
					continue;
					IL_23C:
					num = 4;
					continue;
					IL_297:
					num = 10;
					continue;
					IL_2FC:
					num = 24;
					continue;
					IL_381:
					if (true)
					{
					}
					this.ᜀ(PIDDSI.ByteCount, reader.ReadInt(ClipboardData.b("㭸ɺॼ᩾삂ﾊ", a_)));
					num = 1;
				}
			}
		}

		// Token: 0x040009A7 RID: 2471
		private string \u2460\u0084\u0099\u009A;

		// Token: 0x040009A8 RID: 2472
		private byte \u2593\u00A7\u00A2\u00A0;

		// Token: 0x040009A9 RID: 2473
		private bool \u2593\u007F\u0096\u00A7;

		// Token: 0x040009AA RID: 2474
		private new Dictionary<int, DocumentProperty> ᜀ = new Dictionary<int, DocumentProperty>();
	}
}
