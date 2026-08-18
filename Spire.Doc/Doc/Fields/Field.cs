using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Doc.Rendering;
using Spire.Layouting;

namespace Spire.Doc.Fields
{
	// Token: 0x0200043A RID: 1082
	public class Field : TextRange, IField, spr\u2297
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06003BD8 RID: 15320 RVA: 0x0037460C File Offset: 0x0037360C
		// (set) Token: 0x06003BD9 RID: 15321 RVA: 0x00374650 File Offset: 0x00373650
		public TextFormat TextFormat
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
				return this.m_textFormat;
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
				this.m_textFormat = value;
				this.ᜂ();
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06003BDA RID: 15322 RVA: 0x00374698 File Offset: 0x00373698
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.Field;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x003746D8 File Offset: 0x003736D8
		// (set) Token: 0x06003BDC RID: 15324 RVA: 0x0037471C File Offset: 0x0037371C
		public string Pattern
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
				this.ᜉ = value;
				this.ᜂ();
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06003BDD RID: 15325 RVA: 0x00374764 File Offset: 0x00373764
		public string Value
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
				return this.m_fieldValue;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06003BDE RID: 15326 RVA: 0x003747A8 File Offset: 0x003737A8
		// (set) Token: 0x06003BDF RID: 15327 RVA: 0x003747EC File Offset: 0x003737EC
		public FieldType Type
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
				return this.m_fieldType;
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
				this.m_fieldType = value;
				this.ᜂ();
			}
		}

		// Token: 0x17000293 RID: 659
		// (set) Token: 0x06003BE0 RID: 15328 RVA: 0x00374834 File Offset: 0x00373834
		internal string FieldValue
		{
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
				this.m_fieldValue = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06003BE1 RID: 15329 RVA: 0x00374878 File Offset: 0x00373878
		// (set) Token: 0x06003BE2 RID: 15330 RVA: 0x003748BC File Offset: 0x003738BC
		internal bool IsLocal
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
				return this.ᜏ;
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
				this.ᜏ = value;
				this.ᜂ();
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06003BE3 RID: 15331 RVA: 0x00374904 File Offset: 0x00373904
		// (set) Token: 0x06003BE4 RID: 15332 RVA: 0x00374948 File Offset: 0x00373948
		internal bool ConvertedToText
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
				return this.m_bConvertedToText;
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
				this.m_bConvertedToText = value;
				this.ᜂ();
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06003BE5 RID: 15333 RVA: 0x00374990 File Offset: 0x00373990
		// (set) Token: 0x06003BE6 RID: 15334 RVA: 0x003749D4 File Offset: 0x003739D4
		internal string FormattingString
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
				return this.m_formattingString;
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
				this.m_formattingString = value;
				this.ᜂ();
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06003BE7 RID: 15335 RVA: 0x00374A1C File Offset: 0x00373A1C
		// (set) Token: 0x06003BE8 RID: 15336 RVA: 0x00374A60 File Offset: 0x00373A60
		internal bool IsFieldRange
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
				return this.ᜈ;
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
				this.ᜈ = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06003BE9 RID: 15337 RVA: 0x00374AA4 File Offset: 0x00373AA4
		// (set) Token: 0x06003BEA RID: 15338 RVA: 0x00374AE8 File Offset: 0x00373AE8
		internal string LocalReference
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
				return this.ᜐ;
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
				this.ᜐ = value;
				this.ᜂ();
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06003BEB RID: 15339 RVA: 0x00374B30 File Offset: 0x00373B30
		// (set) Token: 0x06003BEC RID: 15340 RVA: 0x00374B74 File Offset: 0x00373B74
		public string Code
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
				return this.ᜑ;
			}
			set
			{
				for (;;)
				{
					this.ᜑ = value;
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_123;
						case 1:
						{
							char c = '«';
							char c2 = '»';
							this.Text = c + (this as MergeField).FieldName + c2;
							num = 0;
							continue;
						}
						case 2:
							num = 9;
							continue;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								if (!base.Document.ᜇ)
								{
									num = 2;
									continue;
								}
								return;
							}
							break;
						case 4:
							if (!base.Document.ᜇ)
							{
								num = 5;
								continue;
							}
							goto IL_12D;
						case 5:
							this.m_fieldType = spr\u1C8B.ᜀ(this.ᜑ);
							this.UpdateFieldCode(this.ᜑ);
							num = 11;
							continue;
						case 6:
							num = 10;
							continue;
						case 7:
							num = 8;
							continue;
						case 8:
							if (this.Type == FieldType.FieldMergeField)
							{
								num = 6;
								continue;
							}
							return;
						case 9:
							if (!base.Document.ᜈ)
							{
								num = 7;
								continue;
							}
							return;
						case 10:
							if (this.Text == string.Empty)
							{
								num = 1;
								continue;
							}
							return;
						case 11:
							goto IL_12D;
						}
						break;
						IL_12D:
						num = 3;
					}
				}
				IL_123:
				if (true)
				{
				}
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06003BED RID: 15341 RVA: 0x00374D18 File Offset: 0x00373D18
		// (set) Token: 0x06003BEE RID: 15342 RVA: 0x00374D5C File Offset: 0x00373D5C
		internal int SourceFieldType
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
				this.\u1712 = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x00374DA0 File Offset: 0x00373DA0
		// (set) Token: 0x06003BF0 RID: 15344 RVA: 0x00374DE4 File Offset: 0x00373DE4
		internal string FieldResult
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
				return this.\u1716;
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
				this.\u1716 = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x00374E28 File Offset: 0x00373E28
		internal string NestedFieldCode
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
				return this.ᜃ();
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x00374E6C File Offset: 0x00373E6C
		internal spr\u24EF Range
		{
			get
			{
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!this.\u1715)
						{
							num = 15;
							continue;
						}
						goto IL_1C6;
					case 1:
						goto IL_16D;
					case 2:
						goto IL_184;
					case 3:
						goto IL_139;
					case 4:
						goto IL_139;
					case 5:
						if (!base.Document.ᜇ)
						{
							num = 6;
							continue;
						}
						goto IL_1C6;
					case 6:
						num = 14;
						continue;
					case 7:
						this.\u1714 = new spr\u24EF(base.Document, this);
						num = 3;
						continue;
					case 8:
						this.ᜄ();
						num = 1;
						continue;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_184;
						default:
							if (false)
							{
							}
							if (!this.\u1715)
							{
								num = 12;
								continue;
							}
							goto IL_139;
						}
						break;
					case 10:
						if (!base.Document.ᜇ)
						{
							num = 16;
							continue;
						}
						goto IL_139;
					case 12:
						num = 10;
						continue;
					case 13:
						this.\u1714.ᜁ().Clear();
						if (true)
						{
						}
						num = 4;
						continue;
					case 14:
						if (!base.Document.ᜉ)
						{
							num = 8;
							continue;
						}
						goto IL_1C6;
					case 15:
						num = 5;
						continue;
					case 16:
						num = 2;
						continue;
					}
					if (this.\u1714 == null)
					{
						num = 7;
						continue;
					}
					num = 9;
					continue;
					IL_139:
					num = 0;
					continue;
					IL_184:
					if (base.Document.ᜉ)
					{
						goto IL_139;
					}
					num = 13;
				}
				IL_16D:
				IL_1C6:
				return this.\u1714;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06003BF3 RID: 15347 RVA: 0x00375048 File Offset: 0x00374048
		// (set) Token: 0x06003BF4 RID: 15348 RVA: 0x0037508C File Offset: 0x0037408C
		internal FieldMark Separator
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u171B = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06003BF5 RID: 15349 RVA: 0x003750D0 File Offset: 0x003740D0
		// (set) Token: 0x06003BF6 RID: 15350 RVA: 0x00375114 File Offset: 0x00374114
		internal FieldMark End
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
				return this.\u171C;
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
				this.\u171C = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x00375158 File Offset: 0x00374158
		// (set) Token: 0x06003BF8 RID: 15352 RVA: 0x003752BC File Offset: 0x003742BC
		public string FieldText
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_34;
				}
				if (false)
				{
				}
				switch (0)
				{
				default:
				{
					IL_34:
					string text = string.Empty;
					IEnumerator enumerator = this.Range.ᜁ().GetEnumerator();
					try
					{
						int num = 6;
						for (;;)
						{
							switch (num)
							{
							case 1:
							{
								object obj;
								text += (obj as TextRange).Text;
								num = 0;
								continue;
							}
							case 2:
								goto IL_F9;
							case 3:
								num = 2;
								continue;
							case 4:
							{
								if (!enumerator.MoveNext())
								{
									num = 3;
									continue;
								}
								if (true)
								{
								}
								object obj = enumerator.Current;
								num = 5;
								continue;
							}
							case 5:
							{
								object obj;
								if ((obj as DocumentObject).DocumentObjectType == DocumentObjectType.TextRange)
								{
									num = 1;
									continue;
								}
								break;
							}
							}
							IL_B1:
							num = 4;
							continue;
							goto IL_B1;
						}
						IL_F9:;
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
									goto IL_140;
								case 1:
									disposable.Dispose();
									num = 0;
									continue;
								case 2:
									if (disposable != null)
									{
										num = 1;
										continue;
									}
									goto IL_142;
								}
								break;
							}
						}
						IL_140:
						IL_142:;
					}
					return text;
				}
				}
			}
			set
			{
				switch (0)
				{
				default:
				{
					int num = 1;
					for (;;)
					{
						TextRange textRange;
						switch (num)
						{
						case 0:
							if (base.Owner is Paragraph)
							{
								num = 5;
								continue;
							}
							return;
						case 2:
						{
							List<object> list;
							if (list.Count > 0)
							{
								num = 22;
								continue;
							}
							goto IL_19E;
						}
						case 3:
							num = 0;
							continue;
						case 4:
							num = 23;
							continue;
						case 5:
						{
							Paragraph paragraph = this.Owner as Paragraph;
							goto IL_3BB;
						}
						case 6:
							goto IL_19E;
						case 7:
						{
							if (true)
							{
							}
							List<object> list;
							if (list.Count > 1)
							{
								num = 20;
								continue;
							}
							return;
						}
						case 8:
							num = 2;
							continue;
						case 9:
							num = 17;
							continue;
						case 10:
							return;
						case 11:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3BB;
							default:
							{
								if (false)
								{
								}
								List<object> list;
								try
								{
									num = 4;
									for (;;)
									{
										switch (num)
										{
										case 0:
											num = 6;
											continue;
										case 1:
										{
											IEnumerator enumerator;
											if (!enumerator.MoveNext())
											{
												num = 0;
												continue;
											}
											object obj = enumerator.Current;
											num = 5;
											continue;
										}
										case 3:
										{
											object obj;
											list.Add(obj);
											num = 2;
											continue;
										}
										case 5:
										{
											object obj;
											if ((obj as DocumentObject).DocumentObjectType == DocumentObjectType.TextRange)
											{
												num = 3;
												continue;
											}
											break;
										}
										case 6:
											goto IL_47B;
										}
										IL_427:
										num = 1;
										continue;
										goto IL_427;
									}
									IL_47B:;
								}
								finally
								{
									for (;;)
									{
										IEnumerator enumerator;
										IDisposable disposable = enumerator as IDisposable;
										num = 2;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_4C3;
											case 1:
												disposable.Dispose();
												num = 0;
												continue;
											case 2:
												if (disposable != null)
												{
													num = 1;
													continue;
												}
												goto IL_4C5;
											}
											break;
										}
									}
									IL_4C3:
									IL_4C5:;
								}
								List<object>.Enumerator enumerator2 = list.GetEnumerator();
								num = 19;
								continue;
							}
							}
							break;
						case 12:
							if (this.Range.ᜁ().Count > 1)
							{
								num = 8;
								continue;
							}
							goto IL_19E;
						case 13:
							if (this.Type == FieldType.FieldHyperlink)
							{
								num = 16;
								continue;
							}
							return;
						case 14:
						{
							List<object> list = new List<object>();
							IEnumerator enumerator = this.Range.ᜁ().GetEnumerator();
							num = 11;
							continue;
						}
						case 15:
							if (this.Type != FieldType.FieldMergeField)
							{
								num = 18;
								continue;
							}
							return;
						case 16:
							num = 7;
							continue;
						case 17:
							if (base.NextSibling is FieldMark)
							{
								num = 3;
								continue;
							}
							return;
						case 18:
							goto IL_174;
						case 19:
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_161;
									case 2:
										num = 0;
										continue;
									case 4:
									{
										List<object>.Enumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 2;
											continue;
										}
										object obj2 = enumerator2.Current;
										Paragraph paragraph;
										paragraph.Items.Remove(obj2 as DocumentObject);
										this.Range.ᜁ().Remove(obj2);
										num = 3;
										continue;
									}
									}
									IL_103:
									num = 4;
									continue;
									goto IL_103;
								}
								IL_161:
								goto IL_9B;
							}
							finally
							{
								List<object>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
							goto IL_174;
							IL_9B:
							textRange = new TextRange(base.Document);
							num = 12;
							continue;
						case 20:
						{
							List<object> list;
							textRange.CharacterFormat.TextColor = (list[0] as TextRange).CharacterFormat.TextColor;
							textRange.CharacterFormat.UnderlineStyle = (list[0] as TextRange).CharacterFormat.UnderlineStyle;
							num = 10;
							continue;
						}
						case 21:
							if (this.Type != FieldType.FieldNext)
							{
								num = 4;
								continue;
							}
							return;
						case 22:
						{
							Paragraph paragraph;
							paragraph.Items.Insert(paragraph.Items.IndexOf(this.Range.ᜁ()[0] as DocumentObject) + 1, textRange);
							this.Range.ᜁ().Insert(1, textRange);
							num = 6;
							continue;
						}
						case 23:
							if (this.Type != FieldType.FieldSequence)
							{
								num = 14;
								continue;
							}
							return;
						}
						if (this.DocumentObjectType == DocumentObjectType.Field)
						{
							num = 9;
							continue;
						}
						break;
						IL_174:
						num = 21;
						continue;
						IL_19E:
						textRange.ApplyCharacterFormat(base.CharacterFormat);
						textRange.Text = value;
						textRange.TextLength = value.Length;
						num = 13;
						continue;
						IL_3BB:
						num = 15;
					}
					return;
				}
				}
			}
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x003757D0 File Offset: 0x003747D0
		public Field(IDocument doc)
		{
			int a_ = 14;
			this.ᜉ = ClipboardData.b("ཱི䙵շ", a_);
			this.m_formattingString = string.Empty;
			this.m_fieldValue = string.Empty;
			this.ᜑ = string.Empty;
			this.\u1716 = string.Empty;
			this.\u1719 = new Stack<Field>();
			this.\u171A = new List<DocumentObject>();
			base..ctor(doc);
			this.m_paraItemType = ParagraphItemType.Field;
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x0037584C File Offset: 0x0037484C
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 4;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				this.m_fieldType = (FieldType)reader.ReadEnum(ClipboardData.b("㹩ᕫṭᕯ", a_), typeof(FieldType));
				this.m_bConvertedToText = reader.ReadBoolean(ClipboardData.b("⥩ͫmٯ᝱ٳɵᵷṹ⡻ᅽ푿ﲃ", a_));
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (reader.HasAttribute(ClipboardData.b("Ⱪի୭ᱯᙱ㉳᥵੷᝹ᵻ੽", a_)))
						{
							num = 2;
							continue;
						}
						goto IL_1D9;
					case 1:
						goto IL_1D9;
					case 2:
						this.m_formattingString = reader.ReadString(ClipboardData.b("Ⱪի୭ᱯᙱ㉳᥵੷᝹ᵻ੽", a_));
						num = 1;
						continue;
					case 3:
						this.ᜏ = reader.ReadBoolean(ClipboardData.b("⍩Ὣ≭Ὧᅱᕳ᩵", a_));
						num = 5;
						continue;
					case 4:
						goto IL_121;
					case 5:
						goto IL_F0;
					case 6:
						if (!reader.HasAttribute(ClipboardData.b("⍩Ὣ≭Ὧᅱᕳ᩵", a_)))
						{
							goto IL_F0;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_190;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 7:
						this.m_fieldValue = reader.ReadString(ClipboardData.b("㱩൫ɭկ᝱", a_));
						goto IL_190;
					case 8:
						this.m_textFormat = (TextFormat)reader.ReadEnum(ClipboardData.b("㹩५᙭ѯ㑱᭳ѵᕷ᭹ࡻ", a_), typeof(TextFormat));
						num = 4;
						continue;
					case 9:
						if (reader.HasAttribute(ClipboardData.b("㹩५᙭ѯ㑱᭳ѵᕷ᭹ࡻ", a_)))
						{
							num = 8;
							continue;
						}
						goto IL_121;
					case 10:
						return;
					case 11:
						if (reader.HasAttribute(ClipboardData.b("㱩൫ɭկ᝱", a_)))
						{
							num = 7;
							continue;
						}
						return;
					}
					break;
					IL_F0:
					num = 0;
					continue;
					IL_121:
					num = 6;
					continue;
					IL_190:
					num = 10;
					continue;
					IL_1D9:
					num = 11;
				}
			}
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x00375A90 File Offset: 0x00374A90
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 16;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				writer.WriteValue(ClipboardData.b("ɵŷ੹᥻", a_), this.m_paraItemType);
				writer.WriteValue(ClipboardData.b("≵ŷ੹᥻", a_), this.Type);
				writer.WriteValue(ClipboardData.b("㕵᝷ᑹ੻᭽\udc87\ud88b", a_), this.ConvertedToText);
				writer.WriteValue(ClipboardData.b("≵ᵷɹࡻ㡽ﲇ", a_), this.m_textFormat);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_144;
					case 1:
						if (this.m_formattingString != string.Empty)
						{
							num = 6;
							continue;
						}
						goto IL_1BF;
					case 2:
						if (this.ᜏ)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_192;
					case 3:
						goto IL_192;
					case 4:
						goto IL_1BF;
					case 5:
						if (this.m_fieldValue != null)
						{
							num = 10;
							continue;
						}
						goto IL_1E2;
					case 6:
						writer.WriteValue(ClipboardData.b("ふᅷόၻ᩽왿ﺉﺏ", a_), this.m_formattingString);
						num = 4;
						continue;
					case 7:
						writer.WriteValue(ClipboardData.b("⁵᥷ᙹॻ᭽", a_), this.m_fieldValue);
						num = 9;
						continue;
					case 8:
						if (this.m_fieldValue != "")
						{
							num = 7;
							continue;
						}
						goto IL_1E2;
					case 9:
						goto IL_1E2;
					case 10:
						num = 8;
						continue;
					}
					break;
					IL_144:
					writer.WriteValue(ClipboardData.b("㽵୷㙹፻ᵽ", a_), this.ᜏ);
					num = 3;
					continue;
					IL_1E2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_144;
					default:
						goto IL_1F8;
					}
					IL_192:
					num = 1;
					continue;
					IL_1BF:
					num = 5;
				}
			}
			IL_1F8:
			if (false)
			{
			}
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x00375C9C File Offset: 0x00374C9C
		protected override void CreateLayoutInfo()
		{
			switch (0)
			{
			default:
			{
				TextRange textRange2;
				for (;;)
				{
					this.ᜀ = new spr\u1DBA(ChildrenLayoutDirection.Horizontal);
					int num = 6;
					for (;;)
					{
						spr\u1DBA spr_u1DBA;
						switch (num)
						{
						case 0:
							spr_u1DBA.ᜀ(2);
							num = 31;
							continue;
						case 1:
						{
							if (base.Owner is spr\u1AD2)
							{
								num = 24;
								continue;
							}
							int num2 = base.OwnerParagraph.Items.IndexOf(this);
							num = 25;
							continue;
						}
						case 2:
							this.ᜀ.ᜋ().ᜀ((double)base.CharacterFormat.Position);
							num = 17;
							continue;
						case 3:
							goto IL_3E5;
						case 4:
							goto IL_2D5;
						case 5:
							goto IL_156;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2D5;
							default:
								if (false)
								{
								}
								if (base.NextSibling != null)
								{
									num = 27;
									continue;
								}
								goto IL_156;
							}
							break;
						case 7:
							num = 1;
							continue;
						case 8:
							this.ᜀ.ᜋ().ᜁ((double)(-(double)base.CharacterFormat.Position));
							num = 18;
							continue;
						case 9:
						{
							int num2;
							TextRange textRange = base.OwnerParagraph[num2 + 1] as TextRange;
							num = 10;
							continue;
						}
						case 10:
						{
							TextRange textRange;
							if (textRange != null)
							{
								num = 28;
								continue;
							}
							return;
						}
						case 11:
							if (base.CharacterFormat.Position > 0f)
							{
								num = 2;
								continue;
							}
							goto IL_33A;
						case 12:
							if (this.Type == FieldType.FieldNumPages)
							{
								num = 0;
								continue;
							}
							spr_u1DBA.ᜀ(0);
							num = 3;
							continue;
						case 13:
							if (base.CharacterFormat.Position < 0f)
							{
								num = 8;
								continue;
							}
							goto IL_2DA;
						case 14:
							if (textRange2 != null)
							{
								num = 15;
								continue;
							}
							return;
						case 15:
							goto IL_3E0;
						case 16:
							if (base.NextSibling is Break)
							{
								num = 19;
								continue;
							}
							goto IL_156;
						case 17:
							if (true)
							{
							}
							goto IL_33A;
						case 18:
							goto IL_2DA;
						case 19:
							num = 30;
							continue;
						case 20:
							if (spr_u1DBA.ᜀ() > 0)
							{
								num = 7;
								continue;
							}
							return;
						case 21:
							this.ᜀ.ᜃ(true);
							num = 5;
							continue;
						case 22:
							goto IL_3E5;
						case 23:
							spr_u1DBA.ᜀ(1);
							num = 22;
							continue;
						case 24:
						{
							spr\u1AD2 spr_u1AD = base.Owner as spr\u1AD2;
							int num3 = spr_u1AD.ᜂ().IndexOf(this);
							num = 29;
							continue;
						}
						case 25:
						{
							int num2;
							if (base.OwnerParagraph.Items.Count > num2 + 1)
							{
								num = 9;
								continue;
							}
							return;
						}
						case 26:
						{
							spr\u1AD2 spr_u1AD;
							int num3;
							textRange2 = (spr_u1AD.ᜂ()[num3 + 1] as TextRange);
							num = 14;
							continue;
						}
						case 27:
							num = 16;
							continue;
						case 28:
						{
							TextRange textRange;
							textRange.Text = "";
							num = 4;
							continue;
						}
						case 29:
						{
							spr\u1AD2 spr_u1AD;
							int num3;
							if (spr_u1AD.ᜂ().Count > num3 + 1)
							{
								num = 26;
								continue;
							}
							return;
						}
						case 30:
							if ((base.NextSibling as Break).BreakType == BreakType.LineBreak)
							{
								num = 21;
								continue;
							}
							goto IL_156;
						case 31:
							goto IL_3E5;
						case 32:
							if (this.Type == FieldType.FieldPage)
							{
								num = 23;
								continue;
							}
							num = 12;
							continue;
						}
						break;
						IL_156:
						num = 11;
						continue;
						IL_2DA:
						spr_u1DBA = (this.ᜀ as spr\u1DBA);
						num = 32;
						continue;
						IL_33A:
						num = 13;
						continue;
						IL_3E5:
						num = 20;
					}
				}
				IL_2D5:
				return;
				IL_3E0:
				textRange2.Text = "";
				return;
			}
			}
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x003760FC File Offset: 0x003750FC
		protected internal virtual void ParseFieldCode(string fieldCode)
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
			this.m_fieldType = spr\u1C8B.ᜀ(fieldCode);
			this.Code = fieldCode;
			this.UpdateFieldCode(fieldCode);
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x00376154 File Offset: 0x00375154
		protected internal virtual void UpdateFieldCode(string fieldCode)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				Match match;
				int num3;
				for (;;)
				{
					FieldType fieldType = this.m_fieldType;
					int num = 32;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							if (match.Groups[1].Length <= 0)
							{
								num = 28;
								continue;
							}
							goto IL_1D0;
						case 1:
							goto IL_44A;
						case 2:
							num = 3;
							continue;
						case 3:
							switch (fieldType)
							{
							case FieldType.FieldPageRef:
								goto IL_17F;
							case FieldType.FieldAsk:
								goto IL_253;
							case FieldType.FieldFillIn:
							{
								fieldCode = fieldCode.Trim();
								Match match2 = Field.ᜋ.Match(fieldCode);
								num2 = 2;
								int count = match2.Groups.Count;
								num = 20;
								continue;
							}
							default:
								num = 12;
								continue;
							}
							break;
						case 4:
							fieldCode = fieldCode.Trim();
							match = Field.ᜌ.Match(fieldCode);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_212;
							default:
								if (false)
								{
								}
								num = 34;
								continue;
							}
							break;
						case 5:
						{
							Match match2;
							if (match2.Groups[num2].Length > 0)
							{
								num = 24;
								continue;
							}
							goto IL_22C;
						}
						case 6:
							num = 31;
							continue;
						case 7:
							num = 14;
							continue;
						case 8:
							if (fieldType != FieldType.FieldFormula)
							{
								num = 7;
								continue;
							}
							goto IL_E1;
						case 9:
							goto IL_1D0;
						case 10:
							if (fieldType != FieldType.FieldIncludePicture)
							{
								num = 6;
								continue;
							}
							goto IL_58D;
						case 11:
						{
							int count;
							if (num2 >= count)
							{
								num = 17;
								continue;
							}
							num = 5;
							continue;
						}
						case 12:
							num = 8;
							continue;
						case 13:
							if (num3 != -1)
							{
								num = 9;
								continue;
							}
							return;
						case 14:
							goto IL_253;
						case 15:
							goto IL_22C;
						case 16:
							goto IL_44A;
						case 17:
							return;
						case 18:
							if (fieldType != FieldType.FieldLink)
							{
								num = 25;
								continue;
							}
							goto IL_3D9;
						case 19:
							num = 21;
							continue;
						case 20:
							goto IL_1AB;
						case 21:
							if (fieldType != FieldType.FieldTOC)
							{
								num = 2;
								continue;
							}
							goto IL_493;
						case 22:
							goto IL_44A;
						case 23:
							this.m_fieldValue = fieldCode.Replace(ClipboardData.b("ㅸ≺⵼㩾펀쾂첄즆슈", a_), string.Empty);
							num = 22;
							continue;
						case 24:
						{
							Match match2;
							this.m_fieldValue += match2.Groups[num2].Value;
							num = 15;
							continue;
						}
						case 25:
							num = 10;
							continue;
						case 26:
							goto IL_266;
						case 27:
							goto IL_1AB;
						case 28:
							num = 13;
							continue;
						case 29:
							if (fieldCode.IndexOf(this.m_fieldValue) < num3)
							{
								num = 33;
								continue;
							}
							return;
						case 30:
							if (match.Groups[2].Value == ClipboardData.b("╸᝺", a_))
							{
								num = 35;
								continue;
							}
							this.m_fieldValue = ClipboardData.b("學", a_) + match.Groups[2].Value + ClipboardData.b("學", a_);
							num = 1;
							continue;
						case 31:
							goto IL_212;
						case 32:
							if (fieldType <= FieldType.FieldFormula)
							{
								num = 19;
								continue;
							}
							num = 18;
							continue;
						case 33:
							goto IL_201;
						case 34:
							if (match.Groups[2].Value == string.Empty)
							{
								num = 23;
								continue;
							}
							num = 30;
							continue;
						case 35:
							this.m_fieldValue = fieldCode.Replace(ClipboardData.b("ㅸ≺⵼㩾펀쾂첄즆슈", a_), string.Empty);
							this.m_fieldValue = this.m_fieldValue.Replace(ClipboardData.b("╸᝺", a_), string.Empty);
							num = 16;
							continue;
						}
						break;
						IL_1AB:
						num = 11;
						continue;
						IL_1D0:
						this.ᜏ = true;
						num = 29;
						continue;
						IL_212:
						if (fieldType == FieldType.FieldHyperlink)
						{
							num = 4;
							continue;
						}
						goto IL_253;
						IL_22C:
						if (true)
						{
						}
						num2++;
						num = 27;
						continue;
						IL_253:
						this.ParseField(fieldCode);
						num = 26;
						continue;
						IL_44A:
						num3 = fieldCode.IndexOf(ClipboardData.b("╸᝺", a_));
						num = 0;
					}
				}
				IL_E1:
				fieldCode = fieldCode.Trim();
				fieldCode = fieldCode.Replace(ClipboardData.b("䑸", a_), string.Empty);
				this.m_fieldValue = fieldCode;
				return;
				IL_17F:
				fieldCode = fieldCode.Trim();
				match = Field.ᜊ.Match(fieldCode);
				this.m_fieldValue = match.Groups[2].Value;
				return;
				IL_201:
				this.ᜁ(fieldCode, num3);
				return;
				IL_266:
				return;
				IL_3D9:
				fieldCode = fieldCode.Trim();
				fieldCode = fieldCode.Replace(ClipboardData.b("㕸㉺㍼㑾ꆀ", a_), string.Empty);
				this.m_fieldValue = fieldCode;
				return;
				IL_493:
				fieldCode = fieldCode.Trim();
				match = Field.\u170D.Match(fieldCode);
				this.m_formattingString = match.Groups[ClipboardData.b("㙸୺ॼᙾ", a_)].Value;
				return;
				IL_58D:
				fieldCode = fieldCode.Trim();
				match = Field.ᜎ.Match(fieldCode);
				this.m_fieldValue = ClipboardData.b("學", a_) + match.Groups[1].Value + ClipboardData.b("學", a_);
				this.m_formattingString = match.Groups[2].Value;
				return;
			}
			}
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x00376788 File Offset: 0x00375788
		protected internal virtual string ConvertSwitchesToString()
		{
			int a_ = 11;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_94:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				goto IL_5F;
			}
			TextFormat textFormat;
			string str;
			for (;;)
			{
				IL_31:
				switch (num)
				{
				case 0:
					goto IL_1A3;
				case 1:
					goto IL_94;
				case 2:
					if (this.IsLocal)
					{
						num = 9;
						continue;
					}
					goto IL_211;
				case 3:
					goto IL_1A3;
				case 4:
					goto IL_1A3;
				case 5:
					goto IL_18E;
				case 6:
					goto IL_1A3;
				case 7:
					goto IL_1A3;
				case 8:
					switch (textFormat)
					{
					case TextFormat.Uppercase:
						this.m_formattingString = this.m_formattingString.Replace(ClipboardData.b("⵰奲⁴ݶॸṺོ", a_), string.Empty);
						str += ClipboardData.b("⵰奲啴≶ॸ୺᡼ൾꆀ", a_);
						num = 0;
						continue;
					case TextFormat.Lowercase:
						if (true)
						{
						}
						this.m_formattingString = this.m_formattingString.Replace(ClipboardData.b("⵰奲㥴ᡶ๸Ṻོ", a_), string.Empty);
						str += ClipboardData.b("⵰奲啴㭶ᙸ౺᡼ൾꆀ", a_);
						num = 7;
						continue;
					case TextFormat.FirstCapital:
						this.m_formattingString = this.m_formattingString.Replace(ClipboardData.b("⵰奲㍴Ṷ୸ࡺॼ㱾", a_), string.Empty);
						str += ClipboardData.b("⵰奲啴ㅶၸॺ๼୾슀Ꞇ", a_);
						num = 6;
						continue;
					case TextFormat.Titlecase:
						this.m_formattingString = this.m_formattingString.Replace(ClipboardData.b("⵰奲㙴ᙶॸࡺ", a_), string.Empty);
						str += ClipboardData.b("⵰奲啴㑶ᡸ୺๼彾", a_);
						num = 4;
						continue;
					default:
						num = 1;
						continue;
					}
					break;
				case 9:
					str += ClipboardData.b("兰⽲ᥴ", a_);
					num = 5;
					continue;
				}
				goto IL_5F;
				IL_1A3:
				num = 2;
			}
			IL_18E:
			IL_211:
			return str + this.m_formattingString;
			IL_5F:
			str = "";
			textFormat = this.m_textFormat;
			num = 8;
			goto IL_31;
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x003769B4 File Offset: 0x003759B4
		internal override void Attach(Paragraph owner, int itemPos)
		{
			base.Attach(owner, itemPos);
			if (base.DeepDetached)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1A;
				}
				if (false)
				{
				}
				this.\u1713 = true;
				return;
			}
			if (true)
			{
			}
			IL_1A:
			base.Document.Fields.ᜁ(this);
			this.\u1713 = false;
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x00376A24 File Offset: 0x00375A24
		internal override void Detach()
		{
			switch (0)
			{
			default:
			{
				int num = 8;
				for (;;)
				{
					int num3;
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 >= base.OwnerParagraph.Items.Count)
						{
							num = 3;
							continue;
						}
						DocumentObject documentObject = base.OwnerParagraph.Items[num2];
						int count = base.OwnerParagraph.Items.Count;
						base.OwnerParagraph.Items.Remove(documentObject);
						num2--;
						num = 25;
						continue;
					}
					case 1:
						num = 9;
						continue;
					case 2:
						goto IL_390;
					case 3:
						num = 23;
						continue;
					case 4:
						base.Document.Fields.ᜀ(this);
						num = 21;
						continue;
					case 5:
					{
						int num2;
						num2++;
						num = 13;
						continue;
					}
					case 6:
					{
						if (num3 >= base.OwnerParagraph.OwnerTextBody.Items.Count)
						{
							num = 18;
							continue;
						}
						DocumentObject documentObject2 = base.OwnerParagraph.OwnerTextBody.Items[num3];
						int count2 = base.OwnerParagraph.OwnerTextBody.Items.Count;
						base.OwnerParagraph.OwnerTextBody.Items.Remove(documentObject2);
						num3--;
						num = 12;
						continue;
					}
					case 7:
						goto IL_1C8;
					case 9:
						if (this.End.OwnerParagraph != null)
						{
							num = 11;
							continue;
						}
						goto IL_2CD;
					case 10:
						if (base.OwnerParagraph != this.End.OwnerParagraph)
						{
							if (true)
							{
							}
							int num4 = base.ឯ() + 1;
							num = 20;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_253;
						default:
							if (false)
							{
							}
							num = 24;
							continue;
						}
						break;
					case 11:
						num = 10;
						continue;
					case 12:
					{
						DocumentObject documentObject2;
						if (documentObject2 != this.End.OwnerParagraph)
						{
							num = 19;
							continue;
						}
						goto IL_2CD;
					}
					case 13:
						goto IL_310;
					case 14:
						if (!base.DeepDetached)
						{
							num = 4;
							continue;
						}
						return;
					case 15:
						goto IL_253;
					case 16:
						goto IL_1C8;
					case 17:
						goto IL_310;
					case 18:
						goto IL_2CD;
					case 19:
						num3++;
						num = 16;
						continue;
					case 20:
						goto IL_390;
					case 21:
						return;
					case 22:
					{
						int num4;
						if (num4 >= base.OwnerParagraph.Items.Count)
						{
							num = 15;
							continue;
						}
						int count3 = base.OwnerParagraph.Items.Count;
						base.OwnerParagraph.Items.Remove(base.OwnerParagraph.Items[num4]);
						num4--;
						num4++;
						num = 2;
						continue;
					}
					case 23:
						goto IL_2CD;
					case 24:
					{
						int num2 = base.ឯ() + 1;
						num = 17;
						continue;
					}
					case 25:
					{
						DocumentObject documentObject;
						if (documentObject != this.End)
						{
							num = 5;
							continue;
						}
						goto IL_2CD;
					}
					}
					if (this.End != null)
					{
						num = 1;
						continue;
					}
					goto IL_2CD;
					IL_1C8:
					num = 6;
					continue;
					IL_253:
					num3 = base.OwnerParagraph.ឯ() + 1;
					num = 7;
					continue;
					IL_2CD:
					base.Detach();
					num = 14;
					continue;
					IL_310:
					num = 0;
					continue;
					IL_390:
					num = 22;
				}
				return;
			}
			}
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x00376DF4 File Offset: 0x00375DF4
		internal override void CloneCommit()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					for (;;)
					{
						base.Document.Fields.ᜁ(this);
						this.\u1713 = false;
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_64;
						}
					}
					IL_64:
					if (false)
					{
					}
					num = 2;
					continue;
				case 2:
					return;
				}
				if (!this.\u1713)
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x00376E80 File Offset: 0x00375E80
		protected override object CloneImpl()
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
			Field field = (Field)base.CloneImpl();
			field.\u1714 = new spr\u24EF(base.Document, field);
			field.\u1715 = false;
			field.\u1713 = true;
			return field;
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x00376EE8 File Offset: 0x00375EE8
		internal Symbol \u1714()
		{
			int a_ = 16;
			switch (0)
			{
			default:
			{
				Symbol symbol;
				for (;;)
				{
					string[] array = this.Code.Split(new char[]
					{
						'\\'
					});
					string text = string.Empty;
					float num = 0f;
					symbol = new Symbol(base.Document);
					symbol.ᜀ(base.OwnerParagraph);
					string[] array2 = array;
					int num2 = 0;
					int num3 = 5;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_10F;
						case 1:
						{
							string text2;
							symbol.CharacterCode = Convert.ToByte(text2.Replace(ClipboardData.b("╵ⅷ㝹㹻ㅽ챿", a_), string.Empty).Trim());
							num3 = 0;
							continue;
						}
						case 2:
						{
							string text2;
							if (text2.StartsWith(ClipboardData.b("յ", a_)))
							{
								num3 = 11;
								continue;
							}
							goto IL_10F;
						}
						case 3:
							goto IL_10F;
						case 4:
							goto IL_15D;
						case 5:
							goto IL_1C0;
						case 6:
							if (base.CharacterFormat.BaseFormat != null)
							{
								num3 = 15;
								continue;
							}
							goto IL_15D;
						case 7:
							if (num > 0f)
							{
								num3 = 16;
								continue;
							}
							return symbol;
						case 8:
						{
							if (num2 >= array2.Length)
							{
								num3 = 18;
								continue;
							}
							string text2 = array2[num2];
							num3 = 10;
							continue;
						}
						case 9:
						{
							string text2;
							if (text2.StartsWith(ClipboardData.b("ၵ", a_)))
							{
								num3 = 13;
								continue;
							}
							num3 = 2;
							continue;
						}
						case 10:
						{
							string text2;
							if (text2.StartsWith(ClipboardData.b("╵ⅷ㝹㹻ㅽ챿", a_)))
							{
								num3 = 1;
								continue;
							}
							num3 = 9;
							continue;
						}
						case 11:
						{
							IL_25B:
							string text2;
							num = (float)Convert.ToDouble(text2.Replace(ClipboardData.b("յ", a_), string.Empty).Trim());
							num3 = 17;
							continue;
						}
						case 12:
							return symbol;
						case 13:
						{
							string text2;
							text = text2.Replace(ClipboardData.b("ၵ", a_), string.Empty).Trim();
							symbol.FontName = text.Trim(new char[]
							{
								'"'
							});
							num3 = 3;
							continue;
						}
						case 14:
							goto IL_1C0;
						case 15:
							symbol.CharacterFormat.ApplyBase(base.CharacterFormat.BaseFormat);
							num3 = 4;
							continue;
						case 16:
							symbol.CharacterFormat.FontSize = num;
							num3 = 12;
							continue;
						case 17:
							goto IL_10F;
						case 18:
							symbol.CharacterFormat.ImportContainer(base.CharacterFormat);
							symbol.CharacterFormat.ᜃ(base.CharacterFormat);
							num3 = 6;
							continue;
						}
						break;
						IL_10F:
						num2++;
						num3 = 14;
						continue;
						IL_15D:
						num3 = 7;
						continue;
						IL_1C0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_25B;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num3 = 8;
							break;
						}
					}
				}
				return symbol;
			}
			}
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x00377248 File Offset: 0x00376248
		internal void ᜎ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IL_0E:
					for (;;)
					{
						IL_BE:
						this.\u1715 = false;
						FieldType type = this.Type;
						int num = 13;
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_0E;
							default:
								if (false)
								{
								}
								switch (num)
								{
								case 0:
									if (type <= FieldType.FieldSection)
									{
										num = 7;
										continue;
									}
									num = 29;
									continue;
								case 1:
									goto IL_1D8;
								case 2:
									goto IL_1D8;
								case 3:
									if (true)
									{
									}
									if (!base.Document.\u1757.Contains(this))
									{
										num = 32;
										continue;
									}
									return;
								case 4:
									goto IL_1D8;
								case 5:
									num = 22;
									continue;
								case 6:
									if (type != FieldType.FieldIf)
									{
										num = 26;
										continue;
									}
									this.ᜇ();
									num = 4;
									continue;
								case 7:
									num = 8;
									continue;
								case 8:
									if (type != FieldType.FieldFormula)
									{
										num = 10;
										continue;
									}
									goto IL_1C4;
								case 9:
									goto IL_1D8;
								case 10:
									num = 31;
									continue;
								case 11:
									num = 23;
									continue;
								case 12:
									num = 1;
									continue;
								case 13:
									if (type <= FieldType.FieldExpression)
									{
										num = 15;
										continue;
									}
									num = 0;
									continue;
								case 14:
									return;
								case 15:
									num = 6;
									continue;
								case 16:
								{
									string text;
									this.\u1715(text);
									num = 28;
									continue;
								}
								case 17:
									goto IL_1D8;
								case 18:
									goto IL_1D8;
								case 19:
									goto IL_1D8;
								case 20:
									goto IL_1D8;
								case 21:
									num = 25;
									continue;
								case 22:
									switch (type)
									{
									case FieldType.FieldDate:
									case FieldType.FieldTime:
										this.ᜊ();
										num = 17;
										continue;
									case FieldType.FieldPage:
										goto IL_1D8;
									case FieldType.FieldExpression:
										goto IL_1C4;
									default:
										num = 30;
										continue;
									}
									break;
								case 23:
									if (type != FieldType.FieldDocProperty)
									{
										num = 12;
										continue;
									}
									this.ᜈ();
									num = 2;
									continue;
								case 24:
									if (type != FieldType.FieldNumPages)
									{
										num = 5;
										continue;
									}
									this.\u1715(base.Document.\u175E.ToString());
									num = 33;
									continue;
								case 25:
									goto IL_1D8;
								case 26:
									num = 24;
									continue;
								case 27:
								{
									string text;
									if (text != null)
									{
										num = 16;
										continue;
									}
									goto IL_1D8;
								}
								case 28:
									goto IL_1D8;
								case 29:
									if (type != FieldType.FieldCompare)
									{
										num = 11;
										continue;
									}
									this.ᜆ();
									num = 20;
									continue;
								case 30:
									num = 9;
									continue;
								case 31:
									switch (type)
									{
									case FieldType.FieldDocVariable:
									{
										string oldValue = '"'.ToString();
										string text = this.m_doc.Variables[this.Value.Replace(oldValue, string.Empty)];
										num = 27;
										continue;
									}
									case FieldType.FieldSection:
										this.ᜉ();
										num = 18;
										continue;
									default:
										num = 21;
										continue;
									}
									break;
								case 32:
									base.Document.\u1757.Add(this);
									num = 14;
									continue;
								case 33:
									goto IL_1D8;
								}
								goto IL_BE;
								IL_1C4:
								this.ᜅ();
								num = 19;
								break;
								IL_1D8:
								num = 3;
								break;
							}
						}
					}
				}
				return;
			}
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x00377624 File Offset: 0x00376624
		private void ᜐ(string A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Paragraph ownerParagraph = base.OwnerParagraph;
					int num = base.ឯ();
					int num2 = 0;
					ParagraphBase paragraphBase = null;
					int num3 = num;
					int num4 = 2;
					for (;;)
					{
						CharacterFormat characterFormat;
						int num7;
						switch (num4)
						{
						case 0:
							goto IL_25C;
						case 1:
							num4 = 45;
							continue;
						case 2:
							goto IL_108;
						case 3:
							num4 = 38;
							continue;
						case 4:
							return;
						case 5:
							if (paragraphBase is TextRange)
							{
								num4 = 8;
								continue;
							}
							goto IL_684;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_50A;
							default:
								if (false)
								{
								}
								goto IL_42F;
							}
							break;
						case 7:
						{
							Paragraph paragraph;
							int num5;
							if (paragraph.Items[num5] is TextRange)
							{
								num4 = 27;
								continue;
							}
							num5++;
							num4 = 32;
							continue;
						}
						case 8:
							num4 = 14;
							continue;
						case 9:
						{
							bool flag = false;
							int num6 = base.OwnerParagraph.ឯ();
							characterFormat = new CharacterFormat(this.m_doc);
							characterFormat.ImportContainer(base.CharacterFormat);
							Body ownerTextBody = base.OwnerParagraph.OwnerTextBody;
							num7 = num6;
							num4 = 35;
							continue;
						}
						case 10:
							goto IL_276;
						case 11:
							goto IL_50A;
						case 12:
						{
							Paragraph paragraph;
							int num5;
							if (paragraph.Items.Count > num5)
							{
								num4 = 11;
								continue;
							}
							goto IL_30D;
						}
						case 13:
						{
							Paragraph paragraph;
							int num5;
							if ((paragraph.Items[num5] as FieldMark).Type != FieldMarkType.FieldEnd)
							{
								num4 = 41;
								continue;
							}
							goto IL_153;
						}
						case 14:
							if (paragraphBase.NextSibling != null)
							{
								num4 = 22;
								continue;
							}
							goto IL_25C;
						case 15:
							if (ownerParagraph.Items[num3] is FieldMark)
							{
								num4 = 23;
								continue;
							}
							goto IL_245;
						case 16:
							goto IL_42F;
						case 17:
							if (ownerParagraph.Items[num2].NextSibling == null)
							{
								num4 = 9;
								continue;
							}
							num4 = 18;
							continue;
						case 18:
							if (ownerParagraph.Items[num2].NextSibling is TextRange)
							{
								num4 = 33;
								continue;
							}
							ownerParagraph.Items.Insert(num2 + 1, new TextRange(this.m_doc));
							ownerParagraph.Items[num2 + 1].ParaItemCharFormat.ImportContainer(base.CharacterFormat);
							paragraphBase = ownerParagraph.Items[num2 + 1];
							num4 = 16;
							continue;
						case 19:
							goto IL_42F;
						case 20:
							if ((ownerParagraph.Items[num3] as FieldMark).Type == FieldMarkType.FieldSeparator)
							{
								num4 = 21;
								continue;
							}
							goto IL_245;
						case 21:
							num2 = num3;
							num4 = 17;
							continue;
						case 22:
							num4 = 39;
							continue;
						case 23:
							num4 = 20;
							continue;
						case 24:
							goto IL_153;
						case 25:
						{
							Body ownerTextBody;
							if (num7 >= ownerTextBody.Items.Count)
							{
								num4 = 24;
								continue;
							}
							Paragraph paragraph = ownerTextBody.Items[num7 + 1] as Paragraph;
							int num5 = 0;
							num4 = 34;
							continue;
						}
						case 26:
							if ((paragraphBase.NextSibling as FieldMark).Type != FieldMarkType.FieldEnd)
							{
								num4 = 0;
								continue;
							}
							goto IL_684;
						case 27:
						{
							Paragraph paragraph;
							int num5;
							characterFormat.ImportContainer(paragraph.Items[num5].ParaItemCharFormat);
							bool flag = true;
							num4 = 40;
							continue;
						}
						case 28:
							if (num3 >= ownerParagraph.Items.Count)
							{
								num4 = 6;
								continue;
							}
							num4 = 15;
							continue;
						case 29:
							(paragraphBase as TextRange).Text = A_0;
							num4 = 4;
							continue;
						case 30:
							if (paragraphBase is TextRange)
							{
								num4 = 29;
								continue;
							}
							return;
						case 31:
							num4 = 26;
							continue;
						case 32:
							goto IL_4DF;
						case 33:
							paragraphBase = ownerParagraph.Items[num2 + 1];
							num4 = 36;
							continue;
						case 34:
							goto IL_4DF;
						case 35:
							goto IL_276;
						case 36:
							goto IL_42F;
						case 37:
						{
							Paragraph paragraph;
							int num5;
							if (paragraph.Items[num5] is FieldMark)
							{
								num4 = 3;
								continue;
							}
							goto IL_36D;
						}
						case 38:
						{
							Paragraph paragraph;
							int num5;
							if ((paragraph.Items[num5] as FieldMark).Type == FieldMarkType.FieldEnd)
							{
								num4 = 44;
								continue;
							}
							goto IL_36D;
						}
						case 39:
							if (paragraphBase.NextSibling is FieldMark)
							{
								num4 = 31;
								continue;
							}
							goto IL_25C;
						case 40:
							goto IL_30D;
						case 41:
							goto IL_5A8;
						case 42:
							goto IL_108;
						case 43:
							num4 = 5;
							continue;
						case 44:
							goto IL_30D;
						case 45:
						{
							Paragraph paragraph;
							int num5;
							if (paragraph.Items[num5] is FieldMark)
							{
								num4 = 47;
								continue;
							}
							goto IL_5A8;
						}
						case 46:
						{
							bool flag;
							if (!flag)
							{
								num4 = 1;
								continue;
							}
							goto IL_153;
						}
						case 47:
							num4 = 13;
							continue;
						case 48:
							if (paragraphBase != null)
							{
								num4 = 43;
								continue;
							}
							goto IL_684;
						case 49:
							goto IL_684;
						}
						break;
						IL_108:
						num4 = 28;
						continue;
						IL_153:
						ownerParagraph.Items.Insert(num2 + 1, new TextRange(this.m_doc));
						ownerParagraph.Items[num2 + 1].ParaItemCharFormat.ImportContainer(characterFormat);
						paragraphBase = ownerParagraph.Items[num2 + 1];
						num4 = 19;
						continue;
						IL_245:
						num3++;
						num4 = 42;
						continue;
						IL_25C:
						this.ᜁ(num2 + 1);
						num4 = 49;
						continue;
						IL_276:
						num4 = 25;
						continue;
						IL_30D:
						if (true)
						{
						}
						num4 = 46;
						continue;
						IL_36D:
						num4 = 7;
						continue;
						IL_42F:
						num4 = 48;
						continue;
						IL_4DF:
						num4 = 12;
						continue;
						IL_50A:
						num4 = 37;
						continue;
						IL_5A8:
						num7++;
						num4 = 10;
						continue;
						IL_684:
						num4 = 30;
					}
				}
				return;
			}
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x00377CE4 File Offset: 0x00376CE4
		private new void ᜁ(int A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					Paragraph ownerParagraph = base.OwnerParagraph;
					int num = A_0 + 1;
					int num2 = 21;
					for (;;)
					{
						Paragraph paragraph;
						switch (num2)
						{
						case 0:
							if (num >= ownerParagraph.Items.Count)
							{
								num2 = 14;
								continue;
							}
							num2 = 6;
							continue;
						case 1:
							num2 = 24;
							continue;
						case 2:
							if (paragraph.Items[0] is FieldMark)
							{
								num2 = 23;
								continue;
							}
							goto IL_22A;
						case 3:
							if ((paragraph.Items[0] as FieldMark).Type == FieldMarkType.FieldEnd)
							{
								num2 = 16;
								continue;
							}
							goto IL_22A;
						case 4:
							goto IL_22A;
						case 5:
							if (paragraph.Items.Count == 0)
							{
								num2 = 8;
								continue;
							}
							return;
						case 6:
							if (ownerParagraph.Items[num] is FieldMark)
							{
								num2 = 17;
								continue;
							}
							goto IL_11D;
						case 7:
							if (paragraph.Items.Count == 0)
							{
								num2 = 19;
								continue;
							}
							num2 = 2;
							continue;
						case 8:
							paragraph.RemoveSelf();
							num2 = 12;
							continue;
						case 9:
							goto IL_3A1;
						case 10:
							if ((paragraph.Items[0] as FieldMark).Type == FieldMarkType.FieldEnd)
							{
								num2 = 20;
								continue;
							}
							goto IL_31E;
						case 11:
							return;
						case 12:
							goto IL_228;
						case 13:
							goto IL_3A1;
						case 14:
						{
							int num3 = base.OwnerParagraph.ឯ();
							Body ownerTextBody = base.OwnerParagraph.OwnerTextBody;
							int num4 = num3;
							num2 = 28;
							continue;
						}
						case 15:
							goto IL_1E7;
						case 16:
							ownerParagraph.Items.Add(paragraph.Items[0]);
							num2 = 5;
							continue;
						case 17:
							num2 = 22;
							continue;
						case 18:
							return;
						case 19:
							paragraph.RemoveSelf();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_3A1;
							default:
								if (false)
								{
								}
								num2 = 4;
								continue;
							}
							break;
						case 20:
							if (true)
							{
							}
							goto IL_373;
						case 21:
							goto IL_1E7;
						case 22:
							if ((ownerParagraph.Items[num] as FieldMark).Type == FieldMarkType.FieldEnd)
							{
								num2 = 18;
								continue;
							}
							goto IL_11D;
						case 23:
							num2 = 3;
							continue;
						case 24:
							if (paragraph.Items[0] is FieldMark)
							{
								num2 = 25;
								continue;
							}
							goto IL_31E;
						case 25:
							num2 = 10;
							continue;
						case 26:
						{
							Body ownerTextBody;
							int num4;
							if (num4 >= ownerTextBody.Items.Count)
							{
								num2 = 11;
								continue;
							}
							int num3;
							paragraph = (ownerTextBody.Items[num3 + 1] as Paragraph);
							num2 = 13;
							continue;
						}
						case 27:
							if (paragraph.Items.Count > 0)
							{
								num2 = 1;
								continue;
							}
							goto IL_373;
						case 28:
							goto IL_22A;
						}
						break;
						IL_11D:
						ownerParagraph.Items.RemoveAt(A_0 + 1);
						num2 = 15;
						continue;
						IL_1E7:
						num2 = 0;
						continue;
						IL_22A:
						num2 = 26;
						continue;
						IL_31E:
						paragraph.Items.RemoveAt(0);
						num2 = 9;
						continue;
						IL_373:
						num2 = 7;
						continue;
						IL_3A1:
						num2 = 27;
					}
				}
				return;
				IL_228:
				return;
			}
		}

		// Token: 0x06003C08 RID: 15368 RVA: 0x003780C0 File Offset: 0x003770C0
		private void ᜊ()
		{
			int a_ = 16;
			int num = 17;
			string text;
			for (;;)
			{
				DateTime now;
				bool flag;
				switch (num)
				{
				case 0:
					goto IL_202;
				case 1:
					if (text.Contains(ClipboardData.b("⩵剷婹ㅻ᭽", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_253;
				case 2:
					if (true)
					{
					}
					text = this.ᜁ(text, now);
					num = 16;
					continue;
				case 3:
					text = text.Remove(text.IndexOf(ClipboardData.b("⩵剷婹ㅻ㭽퉿얁솃삅잇\ud889솋쾍쒏", a_))).Trim();
					num = 5;
					continue;
				case 4:
					num = 9;
					continue;
				case 5:
					goto IL_253;
				case 6:
					text = text.Remove(text.IndexOf(ClipboardData.b("⩵剷婹ㅻ᭽", a_))).Trim();
					num = 8;
					continue;
				case 7:
					if (flag)
					{
						num = 2;
						continue;
					}
					goto IL_2B5;
				case 8:
					goto IL_253;
				case 9:
					if (base.Document.ᜇ)
					{
						num = 18;
						continue;
					}
					goto IL_202;
				case 10:
					if (text.Contains(ClipboardData.b("⩵剷婹ㅻ㭽퉿얁솃삅잇\ud889솋쾍쒏", a_)))
					{
						num = 3;
						continue;
					}
					num = 1;
					continue;
				case 11:
					num = 14;
					continue;
				case 12:
					goto IL_1CC;
				case 13:
					if (this.FormattingString.Trim() != string.Empty)
					{
						num = 11;
						continue;
					}
					goto IL_1B9;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FA;
					default:
						if (false)
						{
						}
						if (text.Contains(ClipboardData.b("⩵㡷", a_)))
						{
							num = 15;
							continue;
						}
						goto IL_1B9;
					}
					break;
				case 15:
					text = this.ᜀ(text, text.IndexOf(ClipboardData.b("⩵㡷", a_)));
					text = this.ᜀ(text, out flag);
					text = this.ᜀ(text, now);
					num = 7;
					continue;
				case 16:
					goto IL_13C;
				case 18:
					goto IL_FA;
				}
				if (!this.\u1715)
				{
					num = 4;
					continue;
				}
				goto IL_202;
				IL_FA:
				this.Range.ᜁ().Clear();
				this.ᜄ();
				num = 0;
				continue;
				IL_1B9:
				text = now.ToShortDateString();
				num = 12;
				continue;
				IL_202:
				now = DateTime.Now;
				flag = false;
				text = this.FormattingString.Trim().ToString();
				num = 10;
				continue;
				IL_253:
				num = 13;
			}
			IL_13C:
			IL_1CC:
			IL_2B5:
			this.\u1715(text);
		}

		// Token: 0x06003C09 RID: 15369 RVA: 0x0037838C File Offset: 0x0037738C
		internal bool ᜌ()
		{
			int a_ = 19;
			string a_2 = this.\u170D(this.NestedFieldCode);
			a_2 = this.ᜂ(a_2, ClipboardData.b("᝸Ṻռ୾", a_));
			string a = string.Empty;
			a = this.ᜎ(a_2);
			if (!(a == ClipboardData.b("䡸", a_)))
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
				return false;
			}
			return true;
		}

		// Token: 0x06003C0A RID: 15370 RVA: 0x0037841C File Offset: 0x0037741C
		private void ᜉ()
		{
			int num;
			for (;;)
			{
				DocumentObject documentObject = base.OwnerBase as DocumentObject;
				num = 1;
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (documentObject != null)
						{
							num2 = 3;
							continue;
						}
						goto IL_11E;
					case 1:
						goto IL_69;
					case 2:
						goto IL_69;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_96;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 9;
							continue;
						}
						break;
					case 4:
						num += (documentObject as Section).ឯ();
						num2 = 5;
						continue;
					case 5:
						goto IL_BC;
					case 6:
						documentObject = documentObject.Owner;
						num2 = 1;
						continue;
					case 7:
						goto IL_DC;
					case 8:
						if (documentObject.Owner != null)
						{
							goto IL_96;
						}
						goto IL_DC;
					case 9:
						if (documentObject is Section)
						{
							num2 = 4;
							continue;
						}
						goto IL_11E;
					case 10:
						if (documentObject is Section)
						{
							num2 = 7;
							continue;
						}
						num2 = 8;
						continue;
					}
					break;
					IL_69:
					num2 = 10;
					continue;
					IL_96:
					num2 = 6;
					continue;
					IL_DC:
					num2 = 0;
				}
			}
			IL_BC:
			IL_11E:
			this.\u1715(num.ToString());
		}

		// Token: 0x06003C0B RID: 15371 RVA: 0x00378554 File Offset: 0x00377554
		private void ᜈ()
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				string a_2;
				for (;;)
				{
					string text = this.\u170D(this.NestedFieldCode);
					text = this.ᜂ(text, ClipboardData.b("ᑯᵱᝳٵ੷ᕹ౻᭽ﶃ", a_));
					a_2 = string.Empty;
					string text2 = ClipboardData.b("㕯qٳ᥵੷孹屻⭽ﾇ겋ﾏﮕﶗ뺝킟킡쮣횥춧\ud8a9\ud8ab힭邯\udcb1햳\udbb5\uddb7钹", a_);
					int num = 38;
					for (;;)
					{
						switch (num)
						{
						case 0:
							a_2 = base.Document.BuiltinDocumentProperties.ApplicationName;
							num = 48;
							continue;
						case 1:
							goto IL_2B5;
						case 2:
							goto IL_23A;
						case 3:
							goto IL_28E;
						case 4:
							a_2 = base.Document.BuiltinDocumentProperties.LastAuthor;
							num = 42;
							continue;
						case 5:
							goto IL_1CC;
						case 6:
							if (base.Document.BuiltinDocumentProperties.Title == null)
							{
								num = 55;
								continue;
							}
							a_2 = base.Document.BuiltinDocumentProperties.Title;
							num = 62;
							continue;
						case 7:
							spr᧓.ᜭ = new Dictionary<string, int>(25)
							{
								{
									ClipboardData.b("ᅯݱsṵ᝷ࡹ", a_),
									0
								},
								{
									ClipboardData.b("ቯୱs፵୷", a_),
									1
								},
								{
									ClipboardData.b("፯፱s፵ίᕹ๻ݽ", a_),
									2
								},
								{
									ClipboardData.b("፯ᩱᕳѵ᥷᥹ࡻ᭽", a_),
									3
								},
								{
									ClipboardData.b("፯ᩱᕳѵ᥷᥹ࡻ᭽ﲇﾋﺍ", a_),
									4
								},
								{
									ClipboardData.b("፯ᵱᥳ᭵ᵷᑹࡻൽ", a_),
									5
								},
								{
									ClipboardData.b("፯ᵱᥳٵ᥷ᑹջ", a_),
									6
								},
								{
									ClipboardData.b("፯qᅳ᝵౷όࡻ᝽", a_),
									7
								},
								{
									ClipboardData.b("᭯᝱൳ŵ᝷ࡹ᡻ൽ", a_),
									8
								},
								{
									ClipboardData.b("ᱯ፱ݳɵࡷࡹᕻၽ", a_),
									9
								},
								{
									ClipboardData.b("ᱯ፱ݳɵ୷᭹੻᭽ﶃ", a_),
									10
								},
								{
									ClipboardData.b("ᱯ፱ݳɵ୷᭹੻᭽", a_),
									11
								},
								{
									ClipboardData.b("ᱯ᭱ᩳ፵୷", a_),
									12
								},
								{
									ClipboardData.b("ᵯ፱ᩳ᝵ίό๻", a_),
									13
								},
								{
									ClipboardData.b("ṯ፱ᥳ፵᝷ᱹᵻ๽ﺉﺏ", a_),
									14
								},
								{
									ClipboardData.b("Ὧᙱᥳ᝵ᱷᕹύ᝽", a_),
									15
								},
								{
									ClipboardData.b("o፱፳፵୷", a_),
									16
								},
								{
									ClipboardData.b("o፱ٳ᝵ίࡹᵻ๽", a_),
									17
								},
								{
									ClipboardData.b("ɯ᝱ɳή୷፹፻ၽ", a_),
									18
								},
								{
									ClipboardData.b("ͯ᝱ᝳ͵੷፹ࡻݽ", a_),
									19
								},
								{
									ClipboardData.b("ͯݱᙳᱵᵷ᥹ࡻ", a_),
									20
								},
								{
									ClipboardData.b("ѯ᝱ᥳٵᑷ᭹ࡻ᭽", a_),
									21
								},
								{
									ClipboardData.b("ѯ᭱s᩵ᵷ", a_),
									22
								},
								{
									ClipboardData.b("ѯᵱs᝵ᑷό᡻᝽ﲇ", a_),
									23
								},
								{
									ClipboardData.b("ݯᵱٳት୷", a_),
									24
								}
							};
							num = 67;
							continue;
						case 8:
							num = 20;
							continue;
						case 9:
							goto IL_4EA;
						case 10:
							a_2 = base.Document.BuiltinDocumentProperties.Author;
							num = 3;
							continue;
						case 11:
							goto IL_55B;
						case 12:
							goto IL_BF7;
						case 13:
							goto IL_CC2;
						case 14:
						{
							string key;
							int num2;
							if (spr᧓.ᜭ.TryGetValue(key, out num2))
							{
								num = 8;
								continue;
							}
							goto IL_A24;
						}
						case 15:
							goto IL_3D6;
						case 16:
							goto IL_68E;
						case 17:
							goto IL_C3C;
						case 18:
							a_2 = base.Document.BuiltinDocumentProperties.RevisionNumber.ToString();
							num = 1;
							continue;
						case 19:
							if (spr᧓.ᜭ == null)
							{
								num = 7;
								continue;
							}
							goto IL_9F3;
						case 20:
						{
							int num2;
							switch (num2)
							{
							case 0:
								num = 35;
								continue;
							case 1:
							{
								int bytesCount = base.Document.BuiltinDocumentProperties.BytesCount;
								a_2 = base.Document.BuiltinDocumentProperties.BytesCount.ToString();
								num = 31;
								continue;
							}
							case 2:
								num = 50;
								continue;
							case 3:
							case 4:
							{
								int charCount = base.Document.BuiltinDocumentProperties.CharCount;
								a_2 = base.Document.BuiltinDocumentProperties.CharCount.ToString();
								num = 2;
								continue;
							}
							case 5:
								num = 44;
								continue;
							case 6:
								num = 59;
								continue;
							case 7:
							{
								DateTime createDate = base.Document.BuiltinDocumentProperties.CreateDate;
								a_2 = base.Document.BuiltinDocumentProperties.CreateDate.ToString(ClipboardData.b("ᝯ", a_));
								num = 41;
								continue;
							}
							case 8:
								num = 37;
								continue;
							case 9:
							{
								DateTime lastPrinted = base.Document.BuiltinDocumentProperties.LastPrinted;
								a_2 = base.Document.BuiltinDocumentProperties.LastPrinted.ToString(ClipboardData.b("ᝯ", a_));
								num = 15;
								continue;
							}
							case 10:
								num = 24;
								continue;
							case 11:
							{
								DateTime lastSaveDate = base.Document.BuiltinDocumentProperties.LastSaveDate;
								a_2 = base.Document.BuiltinDocumentProperties.LastSaveDate.ToString(ClipboardData.b("ᝯ", a_));
								num = 13;
								continue;
							}
							case 12:
							{
								int linesCount = base.Document.BuiltinDocumentProperties.LinesCount;
								a_2 = base.Document.BuiltinDocumentProperties.LinesCount.ToString();
								num = 30;
								continue;
							}
							case 13:
								num = 22;
								continue;
							case 14:
								num = 28;
								continue;
							case 15:
								a_2 = ClipboardData.b("㕯qٳ᥵੷孹屻⩽ꚅﺍ뢗뺝쾟첡좣\udfa5袧\udca9춫슭\ud9af횱钳킵ힷ좹鲻蒿迁藃곇ꗉ꿋믍뷏럑뫓ꋕꯗ", a_);
								num = 47;
								continue;
							case 16:
							{
								int pageCount = base.Document.BuiltinDocumentProperties.PageCount;
								a_2 = base.Document.BuiltinDocumentProperties.PageCount.ToString();
								num = 11;
								continue;
							}
							case 17:
							{
								int paragraphCount = base.Document.BuiltinDocumentProperties.ParagraphCount;
								a_2 = base.Document.BuiltinDocumentProperties.ParagraphCount.ToString();
								num = 32;
								continue;
							}
							case 18:
								num = 63;
								continue;
							case 19:
							{
								int docSecurity = base.Document.BuiltinDocumentProperties.DocSecurity;
								a_2 = base.Document.BuiltinDocumentProperties.DocSecurity.ToString();
								num = 5;
								continue;
							}
							case 20:
								num = 58;
								continue;
							case 21:
								num = 34;
								continue;
							case 22:
								num = 6;
								continue;
							case 23:
							{
								TimeSpan totalEditingTime = base.Document.BuiltinDocumentProperties.TotalEditingTime;
								num = 40;
								continue;
							}
							case 24:
							{
								int wordCount = base.Document.BuiltinDocumentProperties.WordCount;
								a_2 = base.Document.BuiltinDocumentProperties.WordCount.ToString();
								num = 33;
								continue;
							}
							default:
								num = 21;
								continue;
							}
							break;
						}
						case 21:
							num = 53;
							continue;
						case 22:
							if (base.Document.BuiltinDocumentProperties.Manager != null)
							{
								num = 49;
								continue;
							}
							goto IL_CC7;
						case 23:
							a_2 = base.Document.BuiltinDocumentProperties.Category.ToString();
							num = 61;
							continue;
						case 24:
							if (base.Document.BuiltinDocumentProperties.LastAuthor != null)
							{
								num = 4;
								continue;
							}
							goto IL_CC7;
						case 25:
							goto IL_ACE;
						case 26:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_C2E;
							default:
								if (false)
								{
								}
								a_2 = base.Document.BuiltinDocumentProperties.Company;
								num = 36;
								continue;
							}
							break;
						case 27:
							if (base.Document.CustomDocumentProperties.CustomHash.ContainsKey(text))
							{
								num = 43;
								continue;
							}
							a_2 = text2;
							num = 12;
							continue;
						case 28:
							if (base.Document.BuiltinDocumentProperties.ApplicationName != null)
							{
								num = 0;
								continue;
							}
							goto IL_CC7;
						case 29:
							goto IL_6B0;
						case 30:
							goto IL_B92;
						case 31:
							goto IL_462;
						case 32:
							goto IL_C78;
						case 33:
							goto IL_9DD;
						case 34:
							if (base.Document.BuiltinDocumentProperties.Template != null)
							{
								num = 45;
								continue;
							}
							goto IL_CC7;
						case 35:
							if (base.Document.BuiltinDocumentProperties.Author != null)
							{
								num = 10;
								continue;
							}
							goto IL_CC7;
						case 36:
							goto IL_57D;
						case 37:
							if (base.Document.BuiltinDocumentProperties.Keywords == null)
							{
								num = 54;
								continue;
							}
							a_2 = base.Document.BuiltinDocumentProperties.Keywords;
							num = 60;
							continue;
						case 38:
						{
							string key;
							if ((key = text.ToLower()) != null)
							{
								num = 56;
								continue;
							}
							goto IL_A24;
						}
						case 39:
							a_2 = text2;
							num = 16;
							continue;
						case 40:
						{
							TimeSpan totalEditingTime;
							if (totalEditingTime.TotalMinutes > 0.0)
							{
								num = 46;
								continue;
							}
							goto IL_CC7;
						}
						case 41:
							goto IL_61B;
						case 42:
							goto IL_9A1;
						case 43:
							a_2 = base.Document.CustomDocumentProperties.CustomHash[text].ToString();
							num = 57;
							continue;
						case 44:
							if (base.Document.BuiltinDocumentProperties.Comments == null)
							{
								num = 39;
								continue;
							}
							a_2 = base.Document.BuiltinDocumentProperties.Comments;
							num = 64;
							continue;
						case 45:
							a_2 = base.Document.BuiltinDocumentProperties.Template;
							num = 65;
							continue;
						case 46:
							a_2 = base.Document.BuiltinDocumentProperties.TotalEditingTime.TotalMinutes.ToString();
							num = 51;
							continue;
						case 47:
							goto IL_BE4;
						case 48:
							goto IL_30B;
						case 49:
							a_2 = base.Document.BuiltinDocumentProperties.Manager;
							num = 29;
							continue;
						case 50:
							if (base.Document.BuiltinDocumentProperties.Category != null)
							{
								num = 23;
								continue;
							}
							goto IL_CC7;
						case 51:
							goto IL_2E9;
						case 52:
							a_2 = text2;
							num = 25;
							continue;
						case 53:
							goto IL_A24;
						case 54:
							a_2 = text2;
							num = 66;
							continue;
						case 55:
							goto IL_C2E;
						case 56:
							num = 19;
							continue;
						case 57:
							goto IL_38C;
						case 58:
							if (base.Document.BuiltinDocumentProperties.Subject == null)
							{
								num = 52;
								continue;
							}
							a_2 = base.Document.BuiltinDocumentProperties.Subject;
							num = 9;
							continue;
						case 59:
							if (base.Document.BuiltinDocumentProperties.Company != null)
							{
								num = 26;
								continue;
							}
							goto IL_CC7;
						case 60:
							goto IL_50C;
						case 61:
							goto IL_426;
						case 62:
							goto IL_35F;
						case 63:
							if (base.Document.BuiltinDocumentProperties.RevisionNumber != null)
							{
								num = 18;
								continue;
							}
							goto IL_CC7;
						case 64:
							goto IL_484;
						case 65:
							goto IL_5D1;
						case 66:
							goto IL_51F;
						case 67:
							goto IL_9F3;
						}
						break;
						IL_9F3:
						num = 14;
						continue;
						IL_A24:
						num = 27;
						continue;
						IL_C2E:
						a_2 = text2;
						num = 17;
					}
				}
				IL_1CC:
				IL_23A:
				IL_28E:
				IL_2B5:
				IL_2E9:
				IL_30B:
				IL_35F:
				IL_38C:
				IL_3D6:
				IL_426:
				IL_462:
				IL_484:
				IL_4EA:
				IL_50C:
				IL_51F:
				IL_55B:
				IL_57D:
				IL_5D1:
				goto IL_CC7;
				IL_61B:
				if (true)
				{
				}
				IL_68E:
				IL_6B0:
				IL_9A1:
				IL_9DD:
				IL_ACE:
				IL_B92:
				IL_BE4:
				IL_BF7:
				IL_C3C:
				IL_C78:
				IL_CC2:
				IL_CC7:
				this.\u1715(a_2);
				return;
			}
			}
		}

		// Token: 0x06003C0C RID: 15372 RVA: 0x00379230 File Offset: 0x00378230
		private void ᜇ()
		{
			int a_ = 3;
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				string a_2 = this.\u170D(this.NestedFieldCode);
				a_2 = this.ᜂ(a_2, ClipboardData.b("h൪", a_));
				List<string> list = this.ᜏ(a_2);
				string a_3 = string.Empty;
				string text = string.Empty;
				try
				{
					for (;;)
					{
						text = this.ᜎ(list[0]);
						int num = 3;
						for (;;)
						{
							string text2;
							switch (num)
							{
							case 0:
								goto IL_107;
							case 1:
								text2 = list[2];
								goto IL_FA;
							case 2:
								text2 = list[1];
								goto IL_FA;
							case 3:
								if (text == ClipboardData.b("塨", a_))
								{
									num = 2;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_FB;
								default:
									if (false)
									{
									}
									num = 4;
									continue;
								}
								break;
							case 4:
								num = 1;
								continue;
							}
							break;
							IL_FB:
							num = 0;
							continue;
							IL_FA:
							a_3 = text2;
							goto IL_FB;
						}
					}
					IL_107:;
				}
				catch (Exception)
				{
					a_3 = ClipboardData.b("ⱨᥪὬnͰ割啴≶᝸ၺ፼ၾꖄ麗ꮊ떔붜ﲞ캠춢솤캦\udda8슪슬솮킰\udfb2鮴", a_);
				}
				this.ᜀ(text, a_3);
				return;
			}
			}
		}

		// Token: 0x06003C0D RID: 15373 RVA: 0x00379370 File Offset: 0x00378370
		private string ᜂ(string A_0, string A_1)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7E;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					A_0 = A_0.Substring(A_1.Length, A_0.Length - A_1.Length).Trim();
					num = 0;
					continue;
				}
				if (!A_0.StartsWith(A_1, StringComparison.InvariantCultureIgnoreCase))
				{
					break;
				}
				num = 2;
			}
			IL_7E:
			if (true)
			{
			}
			return A_0;
		}

		// Token: 0x06003C0E RID: 15374 RVA: 0x00379408 File Offset: 0x00378408
		private List<string> ᜏ(string A_0)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				List<string> list = new List<string>();
				List<string> a_2 = new List<string>(new string[]
				{
					ClipboardData.b("䑷䝹", a_),
					ClipboardData.b("䙷䝹", a_),
					ClipboardData.b("䑷䑹", a_),
					ClipboardData.b("䕷", a_),
					ClipboardData.b("䑷", a_),
					ClipboardData.b("䙷", a_)
				});
				string empty = string.Empty;
				bool flag = false;
				List<int> a_3 = this.ᜀ(a_2, A_0);
				try
				{
					int num = 17;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_124;
						case 2:
							if (list.Count == 0)
							{
								num = 7;
								continue;
							}
							goto IL_306;
						case 3:
						{
							if (!(A_0 != string.Empty))
							{
								num = 4;
								continue;
							}
							int a_4 = A_0.IndexOf('\u0013');
							num = 22;
							continue;
						}
						case 4:
							num = 15;
							continue;
						case 5:
							flag = this.ᜀ(a_2, ref A_0, ref empty);
							num = 0;
							continue;
						case 6:
							if (flag)
							{
								num = 10;
								continue;
							}
							goto IL_306;
						case 7:
							list.Insert(0, empty);
							empty = string.Empty;
							flag = false;
							num = 13;
							continue;
						case 8:
							goto IL_144;
						case 9:
							if (A_0.TrimStart(new char[0]).StartsWith(ClipboardData.b("婷", a_)))
							{
								num = 23;
								continue;
							}
							goto IL_24C;
						case 10:
							num = 2;
							continue;
						case 11:
							if (list.Count > 0)
							{
								num = 20;
								continue;
							}
							goto IL_144;
						case 12:
							goto IL_124;
						case 13:
							goto IL_144;
						case 14:
						{
							int a_4;
							this.ᜀ(a_4, ref A_0, ref empty);
							num = 12;
							continue;
						}
						case 15:
							goto IL_37C;
						case 16:
							if (!A_0.StartsWith(ClipboardData.b("婷", a_)))
							{
								num = 18;
								continue;
							}
							goto IL_24C;
						case 18:
							num = 9;
							continue;
						case 19:
							goto IL_24C;
						case 20:
							list.Add(empty);
							empty = string.Empty;
							num = 16;
							continue;
						case 21:
							if (list.Count == 0)
							{
								num = 5;
								continue;
							}
							break;
						case 22:
							if (A_0.StartsWith(ClipboardData.b("婷", a_)))
							{
								num = 14;
								continue;
							}
							this.ᜀ(a_2, list, a_3, flag, ref A_0, ref empty);
							num = 1;
							continue;
						case 23:
							A_0 = A_0.TrimStart(new char[0]);
							num = 19;
							continue;
						}
						goto IL_11F;
						IL_124:
						num = 6;
						continue;
						IL_144:
						num = 21;
						continue;
						IL_1A2:
						num = 3;
						continue;
						IL_11F:
						goto IL_1A2;
						IL_24C:
						list.Add(A_0.Trim(new char[]
						{
							'"'
						}));
						A_0 = string.Empty;
						num = 8;
						continue;
						IL_306:
						num = 11;
					}
					IL_37C:;
				}
				catch
				{
					while (list.Count < 3)
					{
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return list;
						default:
							if (false)
							{
							}
							list.Add(string.Empty);
							break;
						}
					}
				}
				return list;
			}
			}
		}

		// Token: 0x06003C0F RID: 15375 RVA: 0x003797FC File Offset: 0x003787FC
		private new void ᜀ(int A_0, ref string A_1, ref string A_2)
		{
			int a_ = 18;
			for (;;)
			{
				A_1 = A_1.Substring(A_1.IndexOf(ClipboardData.b("婷", a_)) + 1);
				A_0 = A_1.IndexOf('\u0013');
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0 >= 0)
						{
							num = 4;
							continue;
						}
						goto IL_E1;
					case 1:
						A_2 += this.ᜀ(ref A_1);
						num = 2;
						continue;
					case 2:
						goto IL_AF;
					case 3:
						if (A_0 < A_1.IndexOf(ClipboardData.b("婷", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_E1;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AF;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_AF:
			IL_E1:
			A_2 += A_1.Substring(0, A_1.IndexOf(ClipboardData.b("婷", a_))).Trim(new char[]
			{
				spr\u20E8.\u1719
			});
			A_1 = A_1.Substring(A_1.IndexOf(ClipboardData.b("婷", a_)) + 1).Trim(new char[]
			{
				spr\u20E8.\u1719
			});
		}

		// Token: 0x06003C10 RID: 15376 RVA: 0x0037995C File Offset: 0x0037895C
		private new void ᜀ(List<string> A_0, List<string> A_1, List<int> A_2, bool A_3, ref string A_4, ref string A_5)
		{
			int a_ = 18;
			switch (0)
			{
			default:
				if (true)
				{
				}
				for (;;)
				{
					int num = 0;
					int num2 = 13;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (A_2[0] == A_4.IndexOf(A_0[num]))
							{
								goto IL_DD;
							}
							goto IL_21E;
						case 1:
							goto IL_196;
						case 2:
							if (!A_3)
							{
								num2 = 3;
								continue;
							}
							goto IL_196;
						case 3:
							num2 = 14;
							continue;
						case 4:
							return;
						case 5:
							goto IL_78;
						case 6:
							if (A_4.Contains(A_0[num]))
							{
								num2 = 11;
								continue;
							}
							goto IL_21E;
						case 7:
							goto IL_78;
						case 8:
							A_5 += A_4.Substring(0, A_4.IndexOf(A_0[num])).Trim(new char[]
							{
								spr\u20E8.\u1719
							});
							A_4 = A_4.Substring(A_4.IndexOf(A_0[num])).Trim(new char[]
							{
								spr\u20E8.\u1719
							});
							num2 = 5;
							continue;
						case 9:
							if (A_1.Count != 0)
							{
								num2 = 7;
								continue;
							}
							num2 = 6;
							continue;
						case 10:
							goto IL_93;
						case 11:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_DD;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							break;
						case 12:
							num2 = 9;
							continue;
						case 13:
							goto IL_93;
						case 14:
							if (A_1.Count > 0)
							{
								num2 = 1;
								continue;
							}
							return;
						case 15:
							if (num < A_0.Count)
							{
								num2 = 12;
								continue;
							}
							goto IL_78;
						}
						break;
						IL_78:
						num2 = 2;
						continue;
						IL_93:
						num2 = 15;
						continue;
						IL_DD:
						num2 = 8;
						continue;
						IL_196:
						A_5 += A_4.Substring(0, A_4.IndexOf(ClipboardData.b("塷", a_))).Trim(new char[]
						{
							spr\u20E8.\u1719
						});
						A_4 = A_4.Substring(A_4.IndexOf(ClipboardData.b("塷", a_))).Trim(new char[]
						{
							spr\u20E8.\u1719
						});
						num2 = 4;
						continue;
						IL_21E:
						num++;
						num2 = 10;
					}
				}
				return;
			}
		}

		// Token: 0x06003C11 RID: 15377 RVA: 0x00379C14 File Offset: 0x00378C14
		private new bool ᜀ(List<string> A_0, ref string A_1, ref string A_2)
		{
			int num;
			for (;;)
			{
				num = 0;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_A7;
					case 1:
						if (A_1.StartsWith(A_0[num]))
						{
							num2 = 2;
							continue;
						}
						num++;
						num2 = 0;
						continue;
					case 2:
						goto IL_A5;
					case 3:
						return false;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
							if (false)
							{
							}
							if (num >= A_0.Count)
							{
								if (true)
								{
								}
								num2 = 3;
								continue;
							}
							num2 = 1;
							continue;
						}
						break;
					case 5:
						goto IL_A7;
					}
					break;
					IL_A7:
					num2 = 4;
				}
			}
			IL_A5:
			A_2 += A_0[num];
			A_1 = A_1.Substring(A_1.IndexOf(A_0[num]) + A_0[num].Length).Trim();
			return true;
		}

		// Token: 0x06003C12 RID: 15378 RVA: 0x00379D10 File Offset: 0x00378D10
		private new List<int> ᜀ(List<string> A_0, string A_1)
		{
			List<int> list;
			for (;;)
			{
				list = new List<int>();
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_AE;
					case 1:
						goto IL_36;
					case 2:
						list.Add(A_1.IndexOf(A_0[num]));
						num2 = 1;
						continue;
					case 3:
						IL_C1:
						if (num >= A_0.Count)
						{
							num2 = 6;
							continue;
						}
						num2 = 5;
						continue;
					case 4:
						goto IL_AE;
					case 5:
						if (A_1.Contains(A_0[num]))
						{
							num2 = 2;
							continue;
						}
						goto IL_36;
					case 6:
						goto IL_D5;
					}
					break;
					IL_36:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C1;
					default:
						if (false)
						{
						}
						num2 = 4;
						continue;
					}
					IL_AE:
					if (true)
					{
					}
					num2 = 3;
				}
			}
			IL_D5:
			list.Sort();
			return list;
		}

		// Token: 0x06003C13 RID: 15379 RVA: 0x00379DFC File Offset: 0x00378DFC
		private new string ᜀ(ref string A_0)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				string text;
				for (;;)
				{
					text = string.Empty;
					int num = A_0.IndexOf('\u0013');
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_1BA;
						case 1:
							goto IL_78;
						case 2:
							goto IL_96;
						case 3:
							return text;
						case 4:
						{
							int num3;
							if (num3 <= 0)
							{
								num2 = 9;
								continue;
							}
							text += A_0.Substring(0, A_0.IndexOf('\u0015') + 1);
							A_0 = A_0.Substring(A_0.IndexOf('\u0015') + 1);
							num2 = 6;
							continue;
						}
						case 5:
						{
							if (num >= A_0.IndexOf(ClipboardData.b("噳", a_)))
							{
								num2 = 3;
								continue;
							}
							string text2 = A_0.Substring(0, A_0.IndexOf('\u0015') + 1);
							text += text2;
							A_0 = A_0.Substring(A_0.IndexOf('\u0015') + 1);
							string[] array = text2.Split(new char[]
							{
								'\u0013'
							});
							int num3 = array.Length - 2;
							num2 = 2;
							continue;
						}
						case 6:
							goto IL_96;
						case 7:
							if (num >= 0)
							{
								num2 = 8;
								continue;
							}
							return text;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1BA;
							default:
								if (false)
								{
								}
								num2 = 5;
								continue;
							}
							break;
						case 9:
							num = A_0.IndexOf('\u0013');
							num2 = 0;
							continue;
						}
						break;
						IL_78:
						num2 = 7;
						continue;
						IL_96:
						num2 = 4;
						continue;
						IL_1BA:
						goto IL_78;
					}
				}
				return text;
			}
			}
		}

		// Token: 0x06003C14 RID: 15380 RVA: 0x00379FCC File Offset: 0x00378FCC
		private string ᜎ(string A_0)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				string result;
				for (;;)
				{
					List<string> list = new List<string>(new string[]
					{
						ClipboardData.b("坪偬", a_),
						ClipboardData.b("啪偬", a_),
						ClipboardData.b("坪卬", a_),
						ClipboardData.b("噪", a_),
						ClipboardData.b("坪", a_),
						ClipboardData.b("啪", a_)
					});
					string[] array = A_0.Split(list.ToArray(), StringSplitOptions.RemoveEmptyEntries);
					int num = 15;
					for (;;)
					{
						string text;
						string text2;
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
						{
							double a_2;
							if (double.TryParse(array[1], out a_2))
							{
								num = 24;
								continue;
							}
							goto IL_55F;
						}
						case 2:
							text = ClipboardData.b("婪", a_);
							goto IL_391;
						case 3:
						{
							double a_3;
							if (!double.TryParse(array[0], out a_3))
							{
								num = 9;
								continue;
							}
							goto IL_475;
						}
						case 4:
							text2 = ClipboardData.b("婪", a_);
							goto IL_49E;
						case 5:
							goto IL_3BF;
						case 6:
						{
							double a_2;
							if (!double.TryParse(array[1], out a_2))
							{
								num = 14;
								continue;
							}
							goto IL_4CC;
						}
						case 7:
							return result;
						case 8:
							goto IL_23B;
						case 9:
							try
							{
								array[0] = this.ᜌ(array[0].Trim());
								goto IL_475;
							}
							catch (Exception)
							{
								goto IL_475;
							}
							goto IL_35D;
						case 10:
							if (!(array[0].Trim() != array[1].Trim()))
							{
								num = 12;
								continue;
							}
							num = 2;
							continue;
						case 11:
							text = ClipboardData.b("孪", a_);
							goto IL_391;
						case 12:
							num = 11;
							continue;
						case 13:
							if (A_0.Contains(ClipboardData.b("坪卬", a_)))
							{
								num = 25;
								continue;
							}
							return result;
						case 14:
							try
							{
								array[1] = this.ᜌ(array[1].Trim());
								goto IL_4CC;
							}
							catch (Exception)
							{
								goto IL_4CC;
							}
							goto IL_3BF;
						case 15:
							if (array.Length > 1)
							{
								num = 5;
								continue;
							}
							num = 23;
							continue;
						case 16:
							goto IL_417;
						case 17:
							return result;
						case 18:
							text2 = ClipboardData.b("孪", a_);
							goto IL_49E;
						case 19:
						{
							double a_3;
							if (double.TryParse(array[0], out a_3))
							{
								num = 0;
								continue;
							}
							goto IL_55F;
						}
						case 20:
							goto IL_417;
						case 21:
							try
							{
								num = 6;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										double a_2;
										double a_3;
										string text3;
										result = this.ᜀ(a_3, a_2, text3);
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_228;
										default:
											if (false)
											{
											}
											num = 4;
											continue;
										}
										break;
									}
									case 1:
										goto IL_228;
									case 2:
									{
										List<string>.Enumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 3;
											continue;
										}
										string text3 = enumerator.Current;
										num = 5;
										continue;
									}
									case 3:
										goto IL_21C;
									case 4:
										goto IL_21C;
									case 5:
									{
										string text3;
										if (A_0.Contains(text3))
										{
											num = 0;
											continue;
										}
										break;
									}
									}
									IL_1C9:
									num = 2;
									continue;
									goto IL_1C9;
									IL_21C:
									num = 1;
								}
								IL_228:
								return result;
							}
							finally
							{
								List<string>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							goto IL_23B;
						case 22:
							if (A_0.Contains(ClipboardData.b("噪", a_)))
							{
								num = 28;
								continue;
							}
							num = 13;
							continue;
						case 23:
							if (array.Length == 0)
							{
								num = 29;
								continue;
							}
							array = new string[]
							{
								array[0],
								string.Empty
							};
							num = 16;
							continue;
						case 24:
						{
							List<string>.Enumerator enumerator = list.GetEnumerator();
							num = 21;
							continue;
						}
						case 25:
							goto IL_35D;
						case 26:
							if (!(array[0].Trim() == array[1].Trim()))
							{
								num = 27;
								continue;
							}
							num = 4;
							continue;
						case 27:
							num = 18;
							continue;
						case 28:
							num = 26;
							continue;
						case 29:
							array = new string[]
							{
								string.Empty,
								string.Empty
							};
							num = 20;
							continue;
						case 30:
							if (true)
							{
							}
							goto IL_417;
						case 31:
							if (array.Length > 1)
							{
								num = 8;
								continue;
							}
							return result;
						}
						break;
						IL_23B:
						num = 3;
						continue;
						IL_35D:
						num = 10;
						continue;
						IL_391:
						result = text;
						num = 17;
						continue;
						IL_3BF:
						array[0] = array[0].Trim(new char[]
						{
							'"',
							' '
						});
						array[1] = array[1].Trim(new char[]
						{
							'"',
							' '
						});
						num = 30;
						continue;
						IL_417:
						result = ClipboardData.b("婪", a_);
						num = 31;
						continue;
						IL_475:
						num = 6;
						continue;
						IL_49E:
						result = text2;
						num = 7;
						continue;
						IL_4CC:
						num = 19;
						continue;
						IL_55F:
						num = 22;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06003C15 RID: 15381 RVA: 0x0037A598 File Offset: 0x00379598
		private void ᜆ()
		{
			int a_ = 6;
			string a_3;
			for (;;)
			{
				string a_2 = this.\u170D(this.NestedFieldCode);
				a_2 = this.ᜂ(a_2, ClipboardData.b("ཫŭᵯɱᕳѵᵷ", a_));
				a_3 = string.Empty;
				try
				{
					a_3 = this.ᜎ(a_2);
				}
				catch (Exception)
				{
					a_3 = ClipboardData.b("⥫ᱭɯᵱٳ坵塷⽹ቻᕽꢇﲋ꺍﶑뢗ﲙ肟송쮣좥첧쎩\ud8ab잭\udfaf\udcb1햳\udab5隷", a_);
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_66;
				}
			}
			IL_66:
			if (true)
			{
			}
			if (false)
			{
			}
			this.\u1715(a_3);
		}

		// Token: 0x06003C16 RID: 15382 RVA: 0x0037A63C File Offset: 0x0037963C
		private new string ᜀ(double A_0, double A_1, string A_2)
		{
			int a_ = 18;
			double num;
			for (;;)
			{
				num = 0.0;
				int num2 = 34;
				for (;;)
				{
					double num3;
					double num4;
					double num5;
					double num6;
					double num7;
					double num8;
					switch (num2)
					{
					case 0:
						if (A_0 != A_1)
						{
							num2 = 13;
							continue;
						}
						goto IL_29E;
					case 1:
						goto IL_441;
					case 2:
						if (!(A_2 == ClipboardData.b("䑷䝹", a_)))
						{
							num2 = 15;
							continue;
						}
						num2 = 38;
						continue;
					case 3:
						if (!(A_2 == ClipboardData.b("䙷䝹", a_)))
						{
							num2 = 29;
							continue;
						}
						num2 = 30;
						continue;
					case 4:
						if (A_0 <= A_1)
						{
							num2 = 43;
							continue;
						}
						num2 = 36;
						continue;
					case 5:
						num2 = 35;
						continue;
					case 6:
						num3 = (double)1;
						goto IL_2AF;
					case 7:
						num4 = (double)1;
						goto IL_1E5;
					case 8:
						num2 = 24;
						continue;
					case 9:
						num5 = (double)0;
						goto IL_397;
					case 10:
						num2 = 27;
						continue;
					case 11:
						goto IL_1F2;
					case 12:
						num6 = (double)0;
						goto IL_434;
					case 13:
						num2 = 18;
						continue;
					case 14:
						num2 = 3;
						continue;
					case 15:
						num2 = 17;
						continue;
					case 16:
						num2 = 44;
						continue;
					case 17:
						if (!(A_2 == ClipboardData.b("䙷", a_)))
						{
							num2 = 14;
							continue;
						}
						num2 = 4;
						continue;
					case 18:
						num7 = (double)0;
						goto IL_335;
					case 19:
						if (!(A_2 == ClipboardData.b("䑷䑹", a_)))
						{
							num2 = 16;
							continue;
						}
						num2 = 25;
						continue;
					case 20:
						num5 = (double)1;
						goto IL_397;
					case 21:
						goto IL_2BC;
					case 22:
						if (A_0 >= A_1)
						{
							num2 = 37;
							continue;
						}
						num2 = 20;
						continue;
					case 23:
						goto IL_3F4;
					case 24:
						if (!(A_2 == ClipboardData.b("䕷", a_)))
						{
							num2 = 5;
							continue;
						}
						num2 = 0;
						continue;
					case 25:
						if (A_0 == A_1)
						{
							num2 = 31;
							continue;
						}
						num2 = 7;
						continue;
					case 26:
						num8 = (double)0;
						goto IL_3E7;
					case 27:
						num3 = (double)0;
						goto IL_2AF;
					case 28:
						goto IL_3C0;
					case 29:
						num2 = 19;
						continue;
					case 30:
						if (A_0 < A_1)
						{
							num2 = 10;
							continue;
						}
						num2 = 6;
						continue;
					case 31:
						num2 = 42;
						continue;
					case 32:
						num7 = (double)1;
						goto IL_335;
					case 33:
						num2 = 2;
						continue;
					case 34:
						if (A_2 != null)
						{
							num2 = 8;
							continue;
						}
						goto IL_454;
					case 35:
						if (!(A_2 == ClipboardData.b("䑷", a_)))
						{
							num2 = 33;
							continue;
						}
						num2 = 22;
						continue;
					case 36:
						num8 = (double)1;
						goto IL_3E7;
					case 37:
						num2 = 9;
						continue;
					case 38:
						if (A_0 > A_1)
						{
							num2 = 41;
							continue;
						}
						if (true)
						{
						}
						num2 = 40;
						continue;
					case 39:
						goto IL_342;
					case 40:
						num6 = (double)1;
						goto IL_434;
					case 41:
						num2 = 12;
						continue;
					case 42:
						num4 = (double)0;
						goto IL_1E5;
					case 43:
						num2 = 26;
						continue;
					case 44:
						goto IL_12A;
					}
					break;
					IL_1E5:
					num = num4;
					num2 = 11;
					continue;
					IL_29E:
					num2 = 32;
					continue;
					IL_397:
					num = num5;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_29E;
					default:
						if (false)
						{
						}
						num2 = 28;
						continue;
					}
					IL_2AF:
					num = num3;
					num2 = 21;
					continue;
					IL_335:
					num = num7;
					num2 = 39;
					continue;
					IL_3E7:
					num = num8;
					num2 = 23;
					continue;
					IL_434:
					num = num6;
					num2 = 1;
				}
			}
			IL_12A:
			IL_1F2:
			IL_2BC:
			IL_342:
			IL_3C0:
			IL_3F4:
			IL_441:
			IL_454:
			return num.ToString();
		}

		// Token: 0x06003C17 RID: 15383 RVA: 0x0037AAA4 File Offset: 0x00379AA4
		private void ᜅ()
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				string text2;
				string a_2;
				for (;;)
				{
					string text = this.\u170D(this.NestedFieldCode);
					text2 = string.Empty;
					int num = 5;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_19E;
						case 1:
							text2 = text.Substring(text.IndexOf(ClipboardData.b("⭶婸", a_)));
							text2 = text2.Substring(text2.IndexOf(ClipboardData.b("啶", a_)) + 1);
							text2 = text2.Remove(text2.LastIndexOf(ClipboardData.b("啶", a_))).Trim();
							text = text.Remove(text.IndexOf(ClipboardData.b("⭶婸", a_))).Trim();
							goto IL_155;
						case 2:
							goto IL_163;
						case 3:
							goto IL_BF;
						case 4:
							if (true)
							{
							}
							if (text.StartsWith(ClipboardData.b("䩶", a_)))
							{
								num = 3;
								continue;
							}
							goto IL_19E;
						case 5:
							if (!text.Contains(ClipboardData.b("⭶婸", a_)))
							{
								goto IL_163;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_155;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						case 6:
							try
							{
								a_2 = this.ᜌ(text);
								goto IL_1B5;
							}
							catch (Exception ex)
							{
								a_2 = ex.Message;
								goto IL_1B5;
							}
							goto IL_BF;
						}
						break;
						IL_BF:
						text = text.Substring(1).Trim();
						num = 0;
						continue;
						IL_155:
						num = 2;
						continue;
						IL_163:
						num = 4;
						continue;
						IL_19E:
						a_2 = string.Empty;
						num = 6;
					}
				}
				IL_1B5:
				a_2 = this.ᜁ(a_2, text2);
				this.\u1715(a_2);
				return;
			}
			}
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x0037AC88 File Offset: 0x00379C88
		private new string ᜁ(string A_0, string A_1)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				int num = 18;
				for (;;)
				{
					int num2;
					int num4;
					double num5;
					switch (num)
					{
					case 0:
					{
						if (A_0[num2] == '0')
						{
							num = 17;
							continue;
						}
						A_0 = A_0.Remove(num2, 1);
						A_1 = A_1.Remove(num2, 1);
						num2--;
						int num3;
						num3--;
						num = 11;
						continue;
					}
					case 1:
						if (A_0[num2] == '0')
						{
							num = 21;
							continue;
						}
						goto IL_1AF;
					case 2:
						num4++;
						num = 4;
						continue;
					case 3:
						goto IL_136;
					case 4:
						goto IL_219;
					case 5:
						return A_0;
					case 6:
						goto IL_325;
					case 7:
						num = 1;
						continue;
					case 8:
					{
						string text = A_1.TrimEnd(new char[]
						{
							'%'
						});
						text = text.Replace(ClipboardData.b("乬", a_), ClipboardData.b("嵬", a_));
						A_0 = num5.ToString(text);
						int num3 = A_1.Length;
						num = 25;
						continue;
					}
					case 9:
						goto IL_1AF;
					case 10:
						if (A_1.Contains(ClipboardData.b("䡬", a_)))
						{
							num = 13;
							continue;
						}
						return A_0;
					case 11:
						goto IL_1AA;
					case 12:
						if (A_1[num2] != '0')
						{
							num = 26;
							continue;
						}
						goto IL_23A;
					case 13:
						if (true)
						{
						}
						A_0 += ClipboardData.b("䡬", a_);
						num = 5;
						continue;
					case 14:
						goto IL_36D;
					case 15:
						if (num4 < 0)
						{
							num = 20;
							continue;
						}
						goto IL_36D;
					case 16:
						goto IL_136;
					case 17:
						A_0 = A_0.Remove(num2, 1);
						A_0 = A_0.Insert(num2, ClipboardData.b("䵬", a_));
						num = 22;
						continue;
					case 19:
						if (A_0.StartsWith('('.ToString()))
						{
							num = 2;
							continue;
						}
						goto IL_219;
					case 20:
						num4 = 0;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1AA;
						default:
							if (false)
							{
							}
							num = 14;
							continue;
						}
						break;
					case 21:
						goto IL_EB;
					case 22:
						goto IL_23A;
					case 23:
						if (char.IsNumber(A_0[num2]))
						{
							num = 7;
							continue;
						}
						goto IL_EB;
					case 24:
					{
						int num3 = A_1.IndexOf(ClipboardData.b("䍬", a_));
						num = 6;
						continue;
					}
					case 25:
						if (A_1.Contains(ClipboardData.b("䍬", a_)))
						{
							num = 24;
							continue;
						}
						goto IL_325;
					case 26:
						num = 0;
						continue;
					case 27:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 9;
							continue;
						}
						num = 23;
						continue;
					}
					}
					if (double.TryParse(A_0, out num5))
					{
						num = 8;
						continue;
					}
					break;
					IL_EB:
					num = 12;
					continue;
					IL_136:
					num = 27;
					continue;
					IL_1AF:
					num = 10;
					continue;
					IL_219:
					num = 15;
					continue;
					IL_23A:
					num2++;
					num = 16;
					continue;
					IL_1AA:
					goto IL_23A;
					IL_325:
					num4 = A_1.IndexOf(ClipboardData.b("乬", a_));
					num = 19;
					continue;
					IL_36D:
					num2 = num4;
					num = 3;
				}
				return A_0;
			}
			}
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x0037B070 File Offset: 0x0037A070
		private string \u170D(string A_0)
		{
			int a_ = 12;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Contains(ClipboardData.b("⹱平噵㕷ό๻᥽", a_)))
					{
						num = 5;
						continue;
					}
					goto IL_109;
				case 1:
					goto IL_AB;
				case 2:
					A_0 = A_0.Remove(A_0.IndexOf(ClipboardData.b("⹱平噵㕷㽹⹻㥽앿쒁쮃풅얇쮉\ud88b", a_))).Trim();
					num = 3;
					continue;
				case 3:
					goto IL_107;
				case 4:
					if (true)
					{
					}
					break;
				case 5:
					goto IL_D9;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_D9:
					A_0 = A_0.Remove(A_0.IndexOf(ClipboardData.b("⹱平噵㕷ό๻᥽", a_))).Trim();
					num = 1;
					break;
				default:
					if (false)
					{
					}
					if (A_0.Contains(ClipboardData.b("⹱平噵㕷㽹⹻㥽앿쒁쮃풅얇쮉\ud88b", a_)))
					{
						num = 2;
					}
					else
					{
						num = 0;
					}
					break;
				}
			}
			IL_AB:
			IL_107:
			IL_109:
			return A_0.Trim();
		}

		// Token: 0x06003C1A RID: 15386 RVA: 0x0037B18C File Offset: 0x0037A18C
		private string ᜌ(string A_0)
		{
			int a_ = 10;
			int num = 11;
			double num2;
			for (;;)
			{
				string s;
				switch (num)
				{
				case 0:
					num2 = this.ᜋ(A_0);
					num = 7;
					continue;
				case 1:
				{
					Bookmark bookmark;
					if (bookmark != null)
					{
						num = 5;
						continue;
					}
					goto IL_125;
				}
				case 2:
					goto IL_170;
				case 3:
					goto IL_15C;
				case 4:
				{
					if (this.ᜈ(A_0))
					{
						num = 6;
						continue;
					}
					if (true)
					{
					}
					Bookmark bookmark = base.Document.Bookmarks.FindByName(A_0);
					num = 1;
					continue;
				}
				case 5:
					s = string.Empty;
					num = 8;
					continue;
				case 6:
					num2 = this.ᜇ(A_0);
					num = 9;
					continue;
				case 7:
					goto IL_157;
				case 8:
				{
					Bookmark bookmark;
					if (bookmark.BookmarkStart.OwnerParagraph == bookmark.BookmarkEnd.OwnerParagraph)
					{
						num = 10;
						continue;
					}
					goto IL_15C;
				}
				case 9:
					goto IL_1C7;
				case 10:
				{
					Bookmark bookmark;
					s = bookmark.BookmarkStart.OwnerParagraph.Text.Substring(bookmark.BookmarkStart.EndPos, bookmark.BookmarkEnd.EndPos - bookmark.BookmarkStart.EndPos);
					num = 3;
					continue;
				}
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1CC;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (this.ᜉ(A_0.ToLower()))
				{
					num = 0;
					continue;
				}
				num = 4;
				continue;
				IL_15C:
				double.TryParse(s, out num2);
				num = 2;
			}
			IL_125:
			throw new Exception(ClipboardData.b("兯❱ᩳትᵷᱹᕻၽꒃ쒅ﾓ몕뢗", a_) + A_0.ToUpper());
			IL_157:
			IL_170:
			IL_1C7:
			IL_1CC:
			return num2.ToString();
		}

		// Token: 0x06003C1B RID: 15387 RVA: 0x0037B36C File Offset: 0x0037A36C
		private double ᜋ(string A_0)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				string text;
				double result;
				for (;;)
				{
					text = A_0.ToLower();
					int num = 9;
					for (;;)
					{
						List<double> list;
						double num2;
						switch (num)
						{
						case 0:
							return result;
						case 1:
							if (A_0.IndexOf('(') + 1 < A_0.LastIndexOf(')'))
							{
								num = 17;
								continue;
							}
							num = 39;
							continue;
						case 2:
							goto IL_5A0;
						case 3:
							goto IL_546;
						case 4:
							return result;
						case 5:
						{
							string key;
							if ((key = text) != null)
							{
								num = 10;
								continue;
							}
							goto IL_3FA;
						}
						case 6:
							text = A_0.Remove(A_0.IndexOf('(')).ToLower().Trim();
							num = 36;
							continue;
						case 7:
							num = 37;
							continue;
						case 8:
							goto IL_55F;
						case 9:
							if (A_0.Contains('('.ToString()))
							{
								num = 6;
								continue;
							}
							goto IL_6E1;
						case 10:
							num = 11;
							continue;
						case 11:
							if (spr᧓.ᜮ == null)
							{
								num = 32;
								continue;
							}
							goto IL_607;
						case 12:
							goto IL_1EA;
						case 13:
							num2 = list[1];
							goto IL_757;
						case 14:
							if (!(text == ClipboardData.b("ᙯ፱ᡳյᵷ", a_)))
							{
								num = 19;
								continue;
							}
							goto IL_153;
						case 15:
							return result;
						case 16:
							return result;
						case 17:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_738;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								list = this.ᜆ(A_0.Substring(A_0.IndexOf('(') + 1, A_0.LastIndexOf(')') - A_0.IndexOf('(') - 1));
								num = 24;
								continue;
							}
							break;
						case 18:
							goto IL_221;
						case 19:
							goto IL_1C2;
						case 20:
							return result;
						case 21:
							goto IL_18F;
						case 22:
							goto IL_127;
						case 23:
						{
							int num3;
							switch (num3)
							{
							case 0:
								result = this.ᜅ(list);
								num = 41;
								continue;
							case 1:
								result = this.ᜄ(list);
								num = 34;
								continue;
							case 2:
								result = this.ᜃ(list);
								num = 8;
								continue;
							case 3:
								result = this.ᜀ(list[0], list[1]);
								num = 0;
								continue;
							case 4:
								result = this.ᜂ(list[0]);
								num = 4;
								continue;
							case 5:
								goto IL_738;
							case 6:
								result = this.ᜀ(list[0], (int)list[1]);
								num = 31;
								continue;
							case 7:
								result = this.ᜁ(list[0]);
								num = 18;
								continue;
							case 8:
								result = this.ᜂ(list);
								num = 20;
								continue;
							case 9:
								result = this.ᜊ(list[0].ToString());
								num = 2;
								continue;
							case 10:
								result = this.ᜁ((int)list[0], (int)list[1]);
								num = 12;
								continue;
							case 11:
								result = this.ᜀ((int)list[0], (int)list[1]);
								num = 22;
								continue;
							case 12:
								result = this.ᜀ((int)list[0]);
								num = 28;
								continue;
							case 13:
								result = this.ᜀ(list);
								num = 3;
								continue;
							case 14:
								result = this.ᜁ(list);
								num = 16;
								continue;
							case 15:
								result = 1.0;
								num = 29;
								continue;
							case 16:
								result = 0.0;
								num = 21;
								continue;
							case 17:
								num = 40;
								continue;
							default:
								num = 33;
								continue;
							}
							break;
						}
						case 24:
							goto IL_153;
						case 25:
						{
							string key;
							int num3;
							if (spr᧓.ᜮ.TryGetValue(key, out num3))
							{
								num = 35;
								continue;
							}
							goto IL_3FA;
						}
						case 26:
							num = 14;
							continue;
						case 27:
							goto IL_607;
						case 28:
							return result;
						case 29:
							return result;
						case 30:
							goto IL_5B1;
						case 31:
							goto IL_14E;
						case 32:
							spr᧓.ᜮ = new Dictionary<string, int>(18)
							{
								{
									ClipboardData.b("oq᭳ት൷᥹ࡻ", a_),
									0
								},
								{
									ClipboardData.b("ͯݱᥳ", a_),
									1
								},
								{
									ClipboardData.b("ᅯѱᅳѵ᥷ᵹ᥻", a_),
									2
								},
								{
									ClipboardData.b("ᵯᵱၳ", a_),
									3
								},
								{
									ClipboardData.b("ᅯၱݳ", a_),
									4
								},
								{
									ClipboardData.b("᥯ᱱs", a_),
									5
								},
								{
									ClipboardData.b("ɯᵱųᡵᱷ", a_),
									6
								},
								{
									ClipboardData.b("ͯ᭱፳ᡵ", a_),
									7
								},
								{
									ClipboardData.b("፯ᵱųᡵ౷", a_),
									8
								},
								{
									ClipboardData.b("ᑯ᝱ታήᙷό᡻", a_),
									9
								},
								{
									ClipboardData.b("Ὧq", a_),
									10
								},
								{
									ClipboardData.b("ᅯᱱၳ", a_),
									11
								},
								{
									ClipboardData.b("ṯᵱs", a_),
									12
								},
								{
									ClipboardData.b("ᵯ፱౳", a_),
									13
								},
								{
									ClipboardData.b("ᵯ᭱ᩳ", a_),
									14
								},
								{
									ClipboardData.b("ѯqų፵", a_),
									15
								},
								{
									ClipboardData.b("ᙯ፱ᡳյᵷ", a_),
									16
								},
								{
									ClipboardData.b("᥯ᑱ", a_),
									17
								}
							};
							num = 27;
							continue;
						case 33:
							num = 30;
							continue;
						case 34:
							goto IL_436;
						case 35:
							num = 23;
							continue;
						case 36:
							goto IL_6E1;
						case 37:
							num2 = list[2];
							goto IL_757;
						case 38:
							return result;
						case 39:
							if (!(text == ClipboardData.b("ѯqų፵", a_)))
							{
								num = 26;
								continue;
							}
							goto IL_153;
						case 40:
							if (list[0] != 1.0)
							{
								num = 7;
								continue;
							}
							num = 13;
							continue;
						case 41:
							goto IL_578;
						}
						break;
						IL_153:
						num = 5;
						continue;
						IL_607:
						num = 25;
						continue;
						IL_6E1:
						list = new List<double>();
						num = 1;
						continue;
						IL_738:
						result = this.ᜀ(list[0]);
						num = 15;
						continue;
						IL_757:
						result = num2;
						num = 38;
					}
				}
				IL_127:
				IL_14E:
				IL_18F:
				return result;
				IL_1C2:
				throw new Exception(ClipboardData.b("兯ⅱ൳ᡵ౷᭹ѻ幽앿慎ꚉ겋", a_) + ')');
				IL_1EA:
				IL_221:
				return result;
				IL_3FA:
				throw new NotSupportedException(ClipboardData.b("⑯ᩱᅳ噵᝷੹᥻౽", a_) + text + ClipboardData.b("᥯ű味ᡵ᝷๹屻ൽ慎ﺉ뺏", a_));
				IL_436:
				IL_546:
				IL_55F:
				IL_578:
				IL_5A0:
				return result;
				IL_5B1:
				goto IL_3FA;
			}
			}
		}

		// Token: 0x06003C1C RID: 15388 RVA: 0x0037BB18 File Offset: 0x0037AB18
		private double ᜅ(List<double> A_0)
		{
			double num;
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
					num = 1.0;
					int num2 = 0;
					int num3 = 1;
					for (;;)
					{
						if (true)
						{
						}
						switch (num3)
						{
						case 0:
							return num;
						case 1:
							goto IL_5C;
						case 2:
							if (num2 >= A_0.Count)
							{
								num3 = 0;
								continue;
							}
							num *= A_0[num2];
							num2++;
							num3 = 3;
							continue;
						case 3:
							goto IL_5C;
						}
						break;
						IL_5C:
						num3 = 2;
					}
				}
				break;
			}
			return num;
		}

		// Token: 0x06003C1D RID: 15389 RVA: 0x0037BBB8 File Offset: 0x0037ABB8
		private new double ᜄ(List<double> A_0)
		{
			double num;
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
					num = 0.0;
					int num2 = 0;
					int num3 = 1;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							return num;
						case 1:
							goto IL_4A;
						case 2:
							goto IL_4A;
						case 3:
							if (num2 >= A_0.Count)
							{
								num3 = 0;
								continue;
							}
							num += A_0[num2];
							num2++;
							num3 = 2;
							continue;
						}
						break;
						IL_4A:
						if (true)
						{
						}
						num3 = 3;
					}
				}
				break;
			}
			return num;
		}

		// Token: 0x06003C1E RID: 15390 RVA: 0x0037BC58 File Offset: 0x0037AC58
		private new double ᜃ(List<double> A_0)
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
			return this.ᜄ(A_0) / (double)A_0.Count;
		}

		// Token: 0x06003C1F RID: 15391 RVA: 0x0037BCA4 File Offset: 0x0037ACA4
		private new double ᜀ(double A_0, double A_1)
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
			return A_0 % A_1;
		}

		// Token: 0x06003C20 RID: 15392 RVA: 0x0037BCE4 File Offset: 0x0037ACE4
		private double ᜂ(double A_0)
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
			return Math.Abs(A_0);
		}

		// Token: 0x06003C21 RID: 15393 RVA: 0x0037BD28 File Offset: 0x0037AD28
		private new double ᜀ(double A_0, int A_1)
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
			return Math.Round(A_0, A_1);
		}

		// Token: 0x06003C22 RID: 15394 RVA: 0x0037BD6C File Offset: 0x0037AD6C
		private double ᜂ(List<double> A_0)
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
			return (double)(A_0.Count - 1);
		}

		// Token: 0x06003C23 RID: 15395 RVA: 0x0037BDB0 File Offset: 0x0037ADB0
		private new double ᜁ(double A_0)
		{
			if (A_0 >= 0.0)
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
					return 1.0;
				}
			}
			if (true)
			{
			}
			return 0.0;
		}

		// Token: 0x06003C24 RID: 15396 RVA: 0x0037BE0C File Offset: 0x0037AE0C
		private new double ᜁ(List<double> A_0)
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
			A_0.Sort();
			return A_0[0];
		}

		// Token: 0x06003C25 RID: 15397 RVA: 0x0037BE54 File Offset: 0x0037AE54
		private new double ᜀ(List<double> A_0)
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
			A_0.Sort();
			return A_0[A_0.Count - 1];
		}

		// Token: 0x06003C26 RID: 15398 RVA: 0x0037BEA4 File Offset: 0x0037AEA4
		private new double ᜀ(double A_0)
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
			return Math.Floor(A_0);
		}

		// Token: 0x06003C27 RID: 15399 RVA: 0x0037BEE8 File Offset: 0x0037AEE8
		private double ᜊ(string A_0)
		{
			double num;
			if (!double.TryParse(A_0, out num))
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
					return 0.0;
				}
			}
			return 1.0;
		}

		// Token: 0x06003C28 RID: 15400 RVA: 0x0037BF44 File Offset: 0x0037AF44
		private new double ᜁ(int A_0, int A_1)
		{
			if (true)
			{
			}
			if ((A_0 | A_1) != 0)
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
					return 1.0;
				}
			}
			return 0.0;
		}

		// Token: 0x06003C29 RID: 15401 RVA: 0x0037BF9C File Offset: 0x0037AF9C
		private new double ᜀ(int A_0, int A_1)
		{
			if ((A_0 & A_1) != 0)
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
					return 1.0;
				}
			}
			if (true)
			{
			}
			return 0.0;
		}

		// Token: 0x06003C2A RID: 15402 RVA: 0x0037BFF4 File Offset: 0x0037AFF4
		private new double ᜀ(int A_0)
		{
			if (true)
			{
			}
			if (A_0 != 0)
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
					return 0.0;
				}
			}
			return 1.0;
		}

		// Token: 0x06003C2B RID: 15403 RVA: 0x0037C048 File Offset: 0x0037B048
		private bool ᜉ(string A_0)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				List<string> list = new List<string>(new string[]
				{
					ClipboardData.b("ᱫᱭὯᙱųᕵ౷", a_),
					ClipboardData.b("Ὣ᭭ᵯ", a_),
					ClipboardData.b("൫ᡭᕯqᕳᅵᵷ", a_),
					ClipboardData.b("ūŭᑯ", a_),
					ClipboardData.b("൫౭ͯ", a_),
					ClipboardData.b("իmѯ", a_),
					ClipboardData.b("ṫŭկᱱၳ", a_),
					ClipboardData.b("Ὣݭᝯᱱ", a_),
					ClipboardData.b("ཫŭկᱱs", a_),
					ClipboardData.b("࡫୭ᙯ᭱ᩳ፵ᱷ", a_),
					ClipboardData.b("ͫᱭ", a_),
					ClipboardData.b("൫mᑯ", a_),
					ClipboardData.b("ɫŭѯ", a_),
					ClipboardData.b("ū཭࡯", a_),
					ClipboardData.b("ūݭṯ", a_),
					ClipboardData.b("ᡫᱭկ᝱", a_),
					ClipboardData.b("੫཭ᱯűᅳ", a_),
					ClipboardData.b("ի࡭", a_)
				});
				bool result = false;
				using (List<string>.Enumerator enumerator = list.GetEnumerator())
				{
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_222;
						case 1:
							goto IL_216;
						case 2:
						{
							if (!enumerator.MoveNext())
							{
								num = 3;
								continue;
							}
							string value = enumerator.Current;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 6;
								continue;
							}
							break;
						}
						case 3:
							goto IL_216;
						case 5:
							result = true;
							num = 1;
							continue;
						case 6:
						{
							string value;
							if (A_0.StartsWith(value))
							{
								num = 5;
								continue;
							}
							break;
						}
						}
						IL_1E6:
						num = 2;
						continue;
						IL_1A3:
						goto IL_1E6;
						goto IL_1A3;
						IL_216:
						num = 0;
					}
					IL_222:;
				}
				return result;
			}
			}
		}

		// Token: 0x06003C2C RID: 15404 RVA: 0x0037C2A4 File Offset: 0x0037B2A4
		private bool ᜈ(string A_0)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				List<string> list = new List<string>(new string[]
				{
					ClipboardData.b("䝫", a_),
					ClipboardData.b("䅫", a_),
					ClipboardData.b("䙫", a_),
					ClipboardData.b("䍫", a_),
					ClipboardData.b("䥫", a_),
					ClipboardData.b("㉫", a_),
					ClipboardData.b("八", a_),
					ClipboardData.b("偫", a_),
					ClipboardData.b("偫卭", a_),
					ClipboardData.b("剫", a_),
					ClipboardData.b("剫卭", a_),
					ClipboardData.b("偫偭", a_)
				});
				bool result = false;
				using (List<string>.Enumerator enumerator = list.GetEnumerator())
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_1AA;
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 0;
								continue;
							}
							string value = enumerator.Current;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num = 3;
								continue;
							}
							break;
						}
						case 3:
						{
							if (true)
							{
							}
							string value;
							if (A_0.Contains(value))
							{
								num = 6;
								continue;
							}
							break;
						}
						case 4:
							goto IL_1AA;
						case 5:
							goto IL_1B6;
						case 6:
							result = true;
							num = 4;
							continue;
						}
						IL_17A:
						num = 1;
						continue;
						IL_12F:
						goto IL_17A;
						goto IL_12F;
						IL_1AA:
						num = 5;
					}
					IL_1B6:;
				}
				return result;
			}
			}
		}

		// Token: 0x06003C2D RID: 15405 RVA: 0x0037C494 File Offset: 0x0037B494
		private double ᜇ(string A_0)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				List<string> list = new List<string>(new string[]
				{
					ClipboardData.b("䕷", a_),
					ClipboardData.b("䑷", a_),
					ClipboardData.b("䑷䝹", a_),
					ClipboardData.b("䙷", a_),
					ClipboardData.b("䙷䝹", a_),
					ClipboardData.b("䑷䑹", a_),
					ClipboardData.b("♷", a_),
					ClipboardData.b("嵷", a_),
					ClipboardData.b("坷", a_),
					ClipboardData.b("剷", a_),
					ClipboardData.b("啷", a_),
					ClipboardData.b("卷", a_)
				});
				List<string> list2 = this.ᜀ(A_0, list);
				using (List<string>.Enumerator enumerator = list.GetEnumerator())
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_191:
						if (!enumerator.MoveNext())
						{
							num = 0;
						}
						else
						{
							string a_2 = enumerator.Current;
							this.ᜀ(ref list2, a_2);
							num = 2;
						}
						break;
					default:
						if (false)
						{
						}
						num = 1;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 3;
							continue;
						case 3:
							goto IL_1AE;
						case 4:
							goto IL_191;
						}
						IL_188:
						num = 4;
						continue;
						goto IL_188;
					}
					IL_1AE:;
				}
				return double.Parse(list2[0]);
			}
			}
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x0037C688 File Offset: 0x0037B688
		private new void ᜀ(ref List<string> A_0, string A_1)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 30;
				for (;;)
				{
					double num2;
					double num3;
					int num4;
					double num5;
					double num6;
					double num7;
					double num8;
					double num9;
					switch (num)
					{
					case 0:
						num = 46;
						continue;
					case 1:
						num = 8;
						continue;
					case 2:
						goto IL_71D;
					case 3:
						goto IL_41C;
					case 4:
						goto IL_41C;
					case 5:
						goto IL_41C;
					case 6:
						goto IL_41C;
					case 7:
						num2 = (double)0;
						goto IL_606;
					case 8:
						num3 = (double)0;
						goto IL_192;
					case 9:
						goto IL_41C;
					case 10:
						if (double.Parse(A_0[num4 - 1]) < double.Parse(A_0[num4 + 1]))
						{
							num = 18;
							continue;
						}
						num = 37;
						continue;
					case 11:
						return;
					case 13:
						if (double.Parse(A_0[num4 - 1]) >= double.Parse(A_0[num4 + 1]))
						{
							num = 21;
							continue;
						}
						num = 40;
						continue;
					case 14:
						num5 = (double)1;
						goto IL_570;
					case 15:
						if (true)
						{
						}
						if (double.Parse(A_0[num4 - 1]) == double.Parse(A_0[num4 + 1]))
						{
							num = 1;
							continue;
						}
						num = 45;
						continue;
					case 16:
						num = 19;
						continue;
					case 17:
						goto IL_41C;
					case 18:
						num = 20;
						continue;
					case 19:
						num5 = (double)0;
						goto IL_570;
					case 20:
						num6 = (double)0;
						goto IL_541;
					case 21:
						num = 32;
						continue;
					case 22:
						goto IL_41C;
					case 23:
						if (double.Parse(A_0[num4 - 1]) != double.Parse(A_0[num4 + 1]))
						{
							num = 41;
							continue;
						}
						num = 24;
						continue;
					case 24:
						num2 = (double)1;
						goto IL_606;
					case 25:
						if (double.Parse(A_0[num4 - 1]) <= double.Parse(A_0[num4 + 1]))
						{
							num = 16;
							continue;
						}
						num = 14;
						continue;
					case 26:
						goto IL_41C;
					case 27:
						if (A_1 != null)
						{
							num = 31;
							continue;
						}
						goto IL_41C;
					case 28:
						num = 38;
						continue;
					case 29:
						num7 = (double)1;
						goto IL_69E;
					case 31:
						num = 39;
						continue;
					case 32:
						num8 = (double)0;
						goto IL_312;
					case 33:
						if (!A_0.Contains(A_1))
						{
							num = 11;
							continue;
						}
						num4 = A_0.LastIndexOf(A_1);
						num9 = 0.0;
						num = 27;
						continue;
					case 34:
					{
						int num10;
						if (spr᧓.ᜯ.TryGetValue(A_1, out num10))
						{
							num = 47;
							continue;
						}
						goto IL_41C;
					}
					case 35:
						goto IL_41C;
					case 36:
						goto IL_41C;
					case 37:
						num6 = (double)1;
						goto IL_541;
					case 38:
						goto IL_41C;
					case 39:
						if (spr᧓.ᜯ == null)
						{
							num = 2;
							continue;
						}
						goto IL_5C3;
					case 40:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_71D;
						default:
							if (false)
							{
							}
							num8 = (double)1;
							goto IL_312;
						}
						break;
					case 41:
						num = 7;
						continue;
					case 42:
					{
						int num10;
						switch (num10)
						{
						case 0:
							num9 = double.Parse(A_0[num4 - 1]) + double.Parse(A_0[num4 + 1]);
							num = 3;
							continue;
						case 1:
							num9 = double.Parse(A_0[num4 - 1]) - double.Parse(A_0[num4 + 1]);
							num = 5;
							continue;
						case 2:
							num9 = double.Parse(A_0[num4 - 1]) * double.Parse(A_0[num4 + 1]);
							num = 17;
							continue;
						case 3:
							num9 = double.Parse(A_0[num4 - 1]) / double.Parse(A_0[num4 + 1]);
							num = 6;
							continue;
						case 4:
							num9 = double.Parse(A_0[num4 - 1]) % double.Parse(A_0[num4 + 1]);
							num = 26;
							continue;
						case 5:
							num9 = Math.Pow(double.Parse(A_0[num4 - 1]), double.Parse(A_0[num4 + 1]));
							num = 43;
							continue;
						case 6:
							num = 23;
							continue;
						case 7:
							num = 13;
							continue;
						case 8:
							num = 44;
							continue;
						case 9:
							num = 25;
							continue;
						case 10:
							num = 10;
							continue;
						case 11:
							num = 15;
							continue;
						default:
							num = 28;
							continue;
						}
						break;
					}
					case 43:
						goto IL_41C;
					case 44:
						if (double.Parse(A_0[num4 - 1]) > double.Parse(A_0[num4 + 1]))
						{
							num = 0;
							continue;
						}
						num = 29;
						continue;
					case 45:
						num3 = (double)1;
						goto IL_192;
					case 46:
						num7 = (double)0;
						goto IL_69E;
					case 47:
						num = 42;
						continue;
					case 48:
						goto IL_5C3;
					case 49:
						goto IL_41C;
					}
					goto IL_F4;
					IL_192:
					num9 = num3;
					num = 35;
					continue;
					IL_312:
					num9 = num8;
					num = 9;
					continue;
					IL_41C:
					A_0.RemoveAt(num4 + 1);
					A_0.RemoveAt(num4);
					A_0.RemoveAt(num4 - 1);
					A_0.Insert(num4 - 1, num9.ToString());
					num = 12;
					continue;
					IL_497:
					num = 33;
					continue;
					IL_F4:
					goto IL_497;
					IL_541:
					num9 = num6;
					num = 49;
					continue;
					IL_570:
					num9 = num5;
					num = 22;
					continue;
					IL_5C3:
					num = 34;
					continue;
					IL_606:
					num9 = num2;
					num = 36;
					continue;
					IL_69E:
					num9 = num7;
					num = 4;
					continue;
					IL_71D:
					spr᧓.ᜯ = new Dictionary<string, int>(12)
					{
						{
							ClipboardData.b("䁪", a_),
							0
						},
						{
							ClipboardData.b("䙪", a_),
							1
						},
						{
							ClipboardData.b("䅪", a_),
							2
						},
						{
							ClipboardData.b("䑪", a_),
							3
						},
						{
							ClipboardData.b("乪", a_),
							4
						},
						{
							ClipboardData.b("㕪", a_),
							5
						},
						{
							ClipboardData.b("噪", a_),
							6
						},
						{
							ClipboardData.b("坪", a_),
							7
						},
						{
							ClipboardData.b("坪偬", a_),
							8
						},
						{
							ClipboardData.b("啪", a_),
							9
						},
						{
							ClipboardData.b("啪偬", a_),
							10
						},
						{
							ClipboardData.b("坪卬", a_),
							11
						}
					};
					num = 48;
				}
				return;
			}
			}
		}

		// Token: 0x06003C2F RID: 15407 RVA: 0x0037CE20 File Offset: 0x0037BE20
		private new List<string> ᜀ(string A_0, List<string> A_1)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				List<string> list;
				string text;
				for (;;)
				{
					list = new List<string>();
					int num = 0;
					text = string.Empty;
					string text2 = string.Empty;
					int num2 = 0;
					int num3 = 20;
					for (;;)
					{
						char c2;
						switch (num3)
						{
						case 0:
							if (num <= 1)
							{
								num3 = 38;
								continue;
							}
							goto IL_23B;
						case 1:
							if (A_0[num2] == ')')
							{
								num3 = 14;
								continue;
							}
							goto IL_31B;
						case 2:
							if (A_0[num2] == '(')
							{
								num3 = 47;
								continue;
							}
							goto IL_4FE;
						case 3:
							if (A_0[num2] != ')')
							{
								num3 = 11;
								continue;
							}
							goto IL_4D5;
						case 4:
							return list;
						case 5:
							if (num2 >= A_0.Length)
							{
								num3 = 4;
								continue;
							}
							text2 = string.Empty;
							num3 = 49;
							continue;
						case 6:
							if (num != 0)
							{
								num3 = 32;
								continue;
							}
							goto IL_587;
						case 7:
							text2 = A_0[num2].ToString();
							num3 = 41;
							continue;
						case 8:
						{
							char c = A_0[num2 + 1];
							num3 = 22;
							continue;
						}
						case 9:
							goto IL_3B6;
						case 10:
							if (c2.ToString() == ClipboardData.b("塥", a_))
							{
								num3 = 9;
								continue;
							}
							goto IL_38A;
						case 11:
							text += A_0[num2];
							num3 = 28;
							continue;
						case 12:
							num3 = 45;
							continue;
						case 13:
						{
							if (true)
							{
							}
							double num4;
							if (double.TryParse(text, out num4))
							{
								num3 = 52;
								continue;
							}
							goto IL_492;
						}
						case 14:
							num--;
							num3 = 51;
							continue;
						case 15:
							goto IL_2F1;
						case 16:
							num3 = 42;
							continue;
						case 17:
							if (A_0[num2] != ')')
							{
								num3 = 23;
								continue;
							}
							goto IL_672;
						case 18:
							goto IL_38A;
						case 19:
							num3 = 6;
							continue;
						case 20:
							goto IL_2F1;
						case 21:
							num3 = 3;
							continue;
						case 22:
						{
							char c;
							if (!(c.ToString() == ClipboardData.b("孥", a_)))
							{
								num3 = 25;
								continue;
							}
							goto IL_3B6;
						}
						case 23:
							goto IL_23B;
						case 24:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1CA;
							default:
								if (false)
								{
								}
								num3 = 17;
								continue;
							}
							break;
						case 25:
							goto IL_1CA;
						case 26:
						{
							double num4;
							list.Add(num4.ToString());
							text = string.Empty;
							num3 = 53;
							continue;
						}
						case 27:
							if (num2 == A_0.Length - 1)
							{
								num3 = 33;
								continue;
							}
							num3 = 2;
							continue;
						case 28:
							goto IL_4D5;
						case 29:
							if (num2 != A_0.Length - 1)
							{
								num3 = 12;
								continue;
							}
							goto IL_672;
						case 30:
							goto IL_31B;
						case 31:
							if (num2 == A_0.Length - 1)
							{
								num3 = 21;
								continue;
							}
							goto IL_4D5;
						case 32:
							goto IL_466;
						case 33:
							goto IL_587;
						case 34:
							if (A_0[num2] == '(')
							{
								num3 = 48;
								continue;
							}
							num3 = 1;
							continue;
						case 35:
							goto IL_672;
						case 36:
							if (A_0[num2] == ')')
							{
								num3 = 16;
								continue;
							}
							goto IL_3F6;
						case 37:
							if (A_0[num2] != '(')
							{
								num3 = 24;
								continue;
							}
							goto IL_672;
						case 38:
							goto IL_4FE;
						case 39:
							goto IL_3F6;
						case 40:
							if (A_1.Contains(text2))
							{
								num3 = 19;
								continue;
							}
							goto IL_466;
						case 41:
							if (num2 != A_0.Length - 1)
							{
								num3 = 8;
								continue;
							}
							goto IL_38A;
						case 42:
							if (num <= 0)
							{
								num3 = 39;
								continue;
							}
							goto IL_23B;
						case 43:
							goto IL_672;
						case 44:
						{
							double num4;
							if (double.TryParse(text, out num4))
							{
								num3 = 26;
								continue;
							}
							text = this.ᜌ(text);
							num3 = 13;
							continue;
						}
						case 45:
							if (A_1.Contains(text2))
							{
								num3 = 46;
								continue;
							}
							goto IL_672;
						case 46:
							list.Add(text2.Trim());
							num3 = 43;
							continue;
						case 47:
							num3 = 0;
							continue;
						case 48:
							num++;
							num3 = 30;
							continue;
						case 49:
							if (A_1.Contains(A_0[num2].ToString()))
							{
								num3 = 7;
								continue;
							}
							goto IL_38A;
						case 50:
							goto IL_5B8;
						case 51:
							goto IL_31B;
						case 52:
						{
							double num4;
							list.Add(num4.ToString());
							text = string.Empty;
							num3 = 50;
							continue;
						}
						case 53:
							goto IL_5B8;
						}
						break;
						IL_1CA:
						c2 = A_0[num2 + 1];
						num3 = 10;
						continue;
						IL_23B:
						text += A_0[num2];
						num3 = 35;
						continue;
						IL_2F1:
						num3 = 5;
						continue;
						IL_31B:
						num3 = 40;
						continue;
						IL_38A:
						num3 = 31;
						continue;
						IL_3B6:
						text2 += A_0[num2++].ToString();
						num3 = 18;
						continue;
						IL_3F6:
						num3 = 37;
						continue;
						IL_466:
						num3 = 27;
						continue;
						IL_4D5:
						num3 = 34;
						continue;
						IL_4FE:
						num3 = 36;
						continue;
						IL_587:
						text = text.Trim();
						num3 = 44;
						continue;
						IL_5B8:
						num3 = 29;
						continue;
						IL_672:
						num2++;
						num3 = 15;
					}
				}
				return list;
				IL_492:
				throw new Exception(ClipboardData.b("䝥㭧፩ɫᩭᅯੱ味㍵੷ࡹ፻౽걿ꊁ", a_) + text);
			}
			}
		}

		// Token: 0x06003C30 RID: 15408 RVA: 0x0037D4B8 File Offset: 0x0037C4B8
		private List<double> ᜆ(string A_0)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					List<double> list = new List<double>();
					int num = 0;
					text = string.Empty;
					int num2 = 0;
					int num3 = 24;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							if (true)
							{
							}
							goto IL_DB;
						case 1:
							num--;
							num3 = 7;
							continue;
						case 2:
						{
							double item;
							if (double.TryParse(text, out item))
							{
								num3 = 15;
								continue;
							}
							text = this.ᜌ(text);
							num3 = 13;
							continue;
						}
						case 3:
							goto IL_1A5;
						case 4:
							if (A_0[num2] == ',')
							{
								num3 = 25;
								continue;
							}
							goto IL_2D2;
						case 5:
							goto IL_1A5;
						case 6:
							goto IL_1A5;
						case 7:
							goto IL_2AA;
						case 8:
							if (A_0[num2] == ')')
							{
								num3 = 1;
								continue;
							}
							goto IL_2AA;
						case 9:
							if (num2 == A_0.Length - 1)
							{
								num3 = 0;
								continue;
							}
							text += A_0[num2];
							num3 = 6;
							continue;
						case 10:
							return list;
						case 11:
						{
							double item;
							list.Add(item);
							text = string.Empty;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_28B;
							default:
								if (false)
								{
								}
								num3 = 3;
								continue;
							}
							break;
						}
						case 12:
							if (num2 >= A_0.Length)
							{
								num3 = 10;
								continue;
							}
							num3 = 14;
							continue;
						case 13:
						{
							double item;
							if (double.TryParse(text, out item))
							{
								num3 = 11;
								continue;
							}
							goto IL_202;
						}
						case 14:
							if (num2 == A_0.Length - 1)
							{
								num3 = 18;
								continue;
							}
							goto IL_27F;
						case 15:
						{
							double item;
							list.Add(item);
							text = string.Empty;
							num3 = 5;
							continue;
						}
						case 16:
							goto IL_2D2;
						case 17:
							goto IL_21C;
						case 18:
							text += A_0[num2];
							num3 = 23;
							continue;
						case 19:
							goto IL_2AA;
						case 20:
							num++;
							num3 = 19;
							continue;
						case 21:
							if (num != 0)
							{
								num3 = 16;
								continue;
							}
							goto IL_DB;
						case 22:
							goto IL_28B;
						case 23:
							goto IL_27F;
						case 24:
							goto IL_21C;
						case 25:
							num3 = 21;
							continue;
						}
						break;
						IL_DB:
						text = text.Trim();
						num3 = 2;
						continue;
						IL_1A5:
						num2++;
						num3 = 17;
						continue;
						IL_28B:
						if (A_0[num2] == '(')
						{
							num3 = 20;
							continue;
						}
						num3 = 8;
						continue;
						IL_21C:
						num3 = 12;
						continue;
						IL_27F:
						num3 = 22;
						continue;
						IL_2AA:
						num3 = 4;
						continue;
						IL_2D2:
						num3 = 9;
					}
				}
				IL_202:
				throw new Exception(ClipboardData.b("呴⑶xᕺॼṾ呂ꎂ삄ﮈﾌꎎ놐", a_) + text);
			}
			}
		}

		// Token: 0x06003C31 RID: 15409 RVA: 0x0037D7EC File Offset: 0x0037C7EC
		private new void ᜄ()
		{
			try
			{
				int num = 10;
				for (;;)
				{
					int num4;
					switch (num)
					{
					case 0:
						goto IL_2BF;
					case 1:
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
							int num2 = base.ឯ() + 1;
							num = 4;
							continue;
						}
						}
						break;
					case 2:
						goto IL_186;
					case 3:
						goto IL_27C;
					case 4:
						goto IL_27C;
					case 5:
						goto IL_2AD;
					case 6:
					{
						int num3;
						num3++;
						num = 7;
						continue;
					}
					case 7:
						goto IL_186;
					case 8:
						goto IL_155;
					case 9:
						goto IL_155;
					case 11:
					{
						int num3;
						if (num3 >= base.OwnerParagraph.OwnerTextBody.Items.Count)
						{
							num = 5;
							continue;
						}
						this.\u1714.ᜁ().Add(base.OwnerParagraph.OwnerTextBody.Items[num3]);
						num = 19;
						continue;
					}
					case 12:
						num = 17;
						continue;
					case 13:
					{
						int num3 = base.OwnerParagraph.ឯ() + 1;
						num = 2;
						continue;
					}
					case 14:
					{
						int num2;
						if (num2 >= base.OwnerParagraph.Items.Count)
						{
							num = 12;
							continue;
						}
						this.\u1714.ᜁ().Add(base.OwnerParagraph.Items[num2]);
						num = 15;
						continue;
					}
					case 15:
					{
						int num2;
						if (base.OwnerParagraph.Items[num2] != this.End)
						{
							num = 16;
							continue;
						}
						goto IL_2AD;
					}
					case 16:
					{
						int num2;
						num2++;
						num = 3;
						continue;
					}
					case 17:
						goto IL_2AD;
					case 18:
						if (num4 >= base.OwnerParagraph.Items.Count)
						{
							num = 13;
							continue;
						}
						this.\u1714.ᜁ().Add(base.OwnerParagraph.Items[num4]);
						num4++;
						num = 8;
						continue;
					case 19:
					{
						int num3;
						if (base.OwnerParagraph.OwnerTextBody.Items[num3] != this.End.OwnerParagraph)
						{
							num = 6;
							continue;
						}
						goto IL_2AD;
					}
					}
					if (base.OwnerParagraph == this.End.OwnerParagraph)
					{
						num = 1;
						continue;
					}
					num4 = base.ឯ() + 1;
					num = 9;
					continue;
					IL_155:
					num = 18;
					continue;
					IL_186:
					num = 11;
					continue;
					IL_27C:
					num = 14;
					continue;
					IL_2AD:
					this.\u1715 = true;
					num = 0;
				}
				IL_2BF:;
			}
			catch
			{
				this.\u1714.ᜁ().Clear();
			}
			if (true)
			{
			}
		}

		// Token: 0x06003C32 RID: 15410 RVA: 0x0037DAFC File Offset: 0x0037CAFC
		private new string ᜃ()
		{
			string text;
			for (;;)
			{
				text = this.Code;
				this.\u1717 = false;
				this.\u1718 = false;
				this.\u1719.Clear();
				int num = 0;
				int num2 = 7;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num++;
						num2 = 1;
						continue;
					case 1:
						goto IL_E6;
					case 2:
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
						DocumentObject documentObject;
						text += this.ᜁ(documentObject);
						num2 = 6;
						continue;
					}
					case 3:
						if (!this.\u1717)
						{
							num2 = 0;
							continue;
						}
						goto IL_159;
					case 4:
					{
						DocumentObject documentObject;
						if (documentObject is ParagraphBase)
						{
							num2 = 2;
							continue;
						}
						text += this.ᜃ(documentObject);
						num2 = 5;
						continue;
					}
					case 5:
						goto IL_136;
					case 6:
						goto IL_136;
					case 7:
						goto IL_E6;
					case 8:
					{
						if (num >= this.Range.ᜁ().Count)
						{
							num2 = 9;
							continue;
						}
						DocumentObject documentObject = this.Range.ᜁ()[num] as DocumentObject;
						num2 = 4;
						continue;
					}
					case 9:
						goto IL_10F;
					}
					break;
					IL_E6:
					num2 = 8;
					continue;
					IL_136:
					num2 = 3;
				}
			}
			IL_10F:
			IL_159:
			this.\u1717 = false;
			this.\u1718 = false;
			this.\u1719.Clear();
			return text;
		}

		// Token: 0x06003C33 RID: 15411 RVA: 0x0037DC7C File Offset: 0x0037CC7C
		private new string ᜃ(DocumentObject A_0)
		{
			int a_ = 1;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					text = string.Empty;
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (this.\u1717)
							{
								num = 15;
								continue;
							}
							int num2;
							num2++;
							num = 4;
							continue;
						}
						case 1:
						{
							int num2;
							if (num2 >= (A_0 as Paragraph).Items.Count)
							{
								num = 12;
								continue;
							}
							text += this.ᜁ((A_0 as Paragraph).Items[num2]);
							goto IL_ED;
						}
						case 2:
							if (!this.\u1718)
							{
								num = 10;
								continue;
							}
							return text;
						case 3:
							text += ClipboardData.b("橦", a_);
							num = 14;
							continue;
						case 4:
							goto IL_1D1;
						case 5:
							return text;
						case 6:
							if (A_0 is Paragraph)
							{
								num = 11;
								continue;
							}
							num = 7;
							continue;
						case 7:
							if (A_0 is Table)
							{
								num = 9;
								continue;
							}
							return text;
						case 8:
							goto IL_1D1;
						case 9:
							num = 2;
							continue;
						case 10:
						{
							object obj = text;
							text = string.Concat(new object[]
							{
								obj,
								'\u0013',
								this.ᜂ(A_0),
								'\u0015'
							});
							num = 5;
							continue;
						}
						case 11:
						{
							int num2 = 0;
							num = 8;
							continue;
						}
						case 12:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_ED;
							default:
								if (false)
								{
								}
								num = 13;
								continue;
							}
							break;
						case 13:
							if (!this.\u1718)
							{
								num = 3;
								continue;
							}
							return text;
						case 14:
							return text;
						case 15:
							return text;
						}
						break;
						IL_ED:
						num = 0;
						continue;
						IL_1D1:
						if (true)
						{
						}
						num = 1;
					}
				}
				return text;
			}
			}
		}

		// Token: 0x06003C34 RID: 15412 RVA: 0x0037DEB4 File Offset: 0x0037CEB4
		private string ᜂ(DocumentObject A_0)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					text = string.Empty;
					int num = 0;
					int num2 = 11;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							goto IL_26C;
						case 1:
							text += ClipboardData.b("畷絹", a_);
							num2 = 10;
							continue;
						case 2:
						{
							if (num >= (A_0 as Table).Rows.Count)
							{
								num2 = 16;
								continue;
							}
							TableRow tableRow = (A_0 as Table).Rows[num];
							num3 = 0;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1F0;
							default:
								if (false)
								{
								}
								num2 = 6;
								continue;
							}
							break;
						}
						case 3:
						{
							TableRow tableRow;
							if (num3 >= tableRow.Cells.Count)
							{
								num2 = 7;
								continue;
							}
							TableCell tableCell = tableRow.Cells[num3];
							int num4 = 0;
							num2 = 0;
							continue;
						}
						case 4:
						{
							TableCell tableCell;
							int num4;
							if (num4 >= tableCell.Items.Count)
							{
								num2 = 18;
								continue;
							}
							text += this.ᜃ(tableCell.Items[num4]);
							num2 = 13;
							continue;
						}
						case 5:
							goto IL_26C;
						case 6:
							goto IL_86;
						case 7:
							num2 = 14;
							continue;
						case 8:
							if (true)
							{
							}
							if (!this.\u1718)
							{
								num2 = 17;
								continue;
							}
							goto IL_202;
						case 9:
							return text;
						case 10:
							goto IL_1F0;
						case 11:
							goto IL_18E;
						case 12:
							goto IL_18E;
						case 13:
						{
							if (this.\u1717)
							{
								num2 = 9;
								continue;
							}
							int num4;
							num4++;
							num2 = 5;
							continue;
						}
						case 14:
							if (!this.\u1718)
							{
								num2 = 1;
								continue;
							}
							goto IL_1F0;
						case 15:
							goto IL_202;
						case 16:
							return text;
						case 17:
							text += ClipboardData.b("罷", a_);
							num2 = 15;
							continue;
						case 18:
							num2 = 8;
							continue;
						case 19:
							goto IL_86;
						}
						break;
						IL_86:
						num2 = 3;
						continue;
						IL_18E:
						num2 = 2;
						continue;
						IL_1F0:
						num++;
						num2 = 12;
						continue;
						IL_202:
						num3++;
						num2 = 19;
						continue;
						IL_26C:
						num2 = 4;
					}
				}
				return text;
			}
			}
		}

		// Token: 0x06003C35 RID: 15413 RVA: 0x0037E160 File Offset: 0x0037D160
		private new string ᜁ(DocumentObject A_0)
		{
			string result;
			for (;;)
			{
				result = string.Empty;
				int num = 18;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 2;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_19B;
						default:
							if (false)
							{
							}
							this.\u1717 = true;
							num = 10;
							continue;
						}
						break;
					case 2:
						if ((A_0 as Field).Type == FieldType.FieldMergeField)
						{
							num = 8;
							continue;
						}
						num = 23;
						continue;
					case 3:
						return result;
					case 4:
						num = 6;
						continue;
					case 5:
						this.\u1719.Push(A_0 as Field);
						this.\u1718 = true;
						num = 12;
						continue;
					case 6:
						goto IL_19B;
					case 7:
						goto IL_CB;
					case 8:
						result = (A_0 as TextRange).Text;
						num = 3;
						continue;
					case 9:
						if (!this.\u1718)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_314;
					case 10:
						return result;
					case 11:
						if (A_0 is Field)
						{
							num = 19;
							continue;
						}
						goto IL_314;
					case 12:
						goto IL_2AD;
					case 13:
						(A_0 as Field).ᜎ();
						num = 7;
						continue;
					case 14:
						if (this.\u1719.Peek().End == A_0)
						{
							num = 20;
							continue;
						}
						goto IL_114;
					case 15:
						num = 14;
						continue;
					case 16:
						return result;
					case 17:
						if (this.\u1718)
						{
							num = 15;
							continue;
						}
						goto IL_114;
					case 18:
						if (this.\u1717)
						{
							num = 26;
							continue;
						}
						num = 11;
						continue;
					case 19:
						num = 9;
						continue;
					case 20:
						this.\u1718 = false;
						this.\u1719.Pop();
						num = 21;
						continue;
					case 21:
						return result;
					case 22:
						if (!base.Document.\u1757.Contains(A_0 as Field))
						{
							num = 13;
							continue;
						}
						goto IL_CB;
					case 23:
						if ((A_0 as Field).End != null)
						{
							num = 5;
							continue;
						}
						goto IL_2AD;
					case 24:
						if (A_0 is TextRange)
						{
							num = 4;
							continue;
						}
						return result;
					case 25:
						return result;
					case 26:
						return result;
					case 27:
						if (this.Separator == A_0)
						{
							num = 1;
							continue;
						}
						num = 17;
						continue;
					case 28:
						result = (A_0 as TextRange).Text;
						num = 25;
						continue;
					}
					break;
					IL_CB:
					result = (A_0 as Field).FieldResult;
					num = 16;
					continue;
					IL_114:
					num = 24;
					continue;
					IL_19B:
					if (!this.\u1718)
					{
						num = 28;
						continue;
					}
					return result;
					IL_2AD:
					num = 22;
					continue;
					IL_314:
					num = 27;
				}
			}
			return result;
		}

		// Token: 0x06003C36 RID: 15414 RVA: 0x0037E4AC File Offset: 0x0037D4AC
		internal string ᜐ()
		{
			switch (0)
			{
			default:
			{
				string text;
				DocumentObject documentObject;
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_4A8:
					text = text + (documentObject as Paragraph).ᜀ(num + 1, (documentObject as Paragraph).Items.Count - 1) + spr\u20E8.\u171F;
					base.Document.ᜅ = this.End.OwnerParagraph;
					num2 = 11;
					break;
				default:
					if (false)
					{
					}
					goto IL_F2;
				}
				for (;;)
				{
					IL_2F:
					int num3;
					int a_;
					int num4;
					switch (num2)
					{
					case 0:
						goto IL_527;
					case 1:
						num2 = 40;
						continue;
					case 2:
						if (documentObject is TextRange)
						{
							num2 = 6;
							continue;
						}
						num2 = 31;
						continue;
					case 3:
						num2 = 25;
						continue;
					case 4:
						goto IL_59A;
					case 5:
						if (this.Separator.OwnerParagraph == documentObject)
						{
							num2 = 43;
							continue;
						}
						goto IL_527;
					case 6:
						text += (documentObject as TextRange).Text;
						num2 = 14;
						continue;
					case 7:
						if (true)
						{
						}
						if (this.Separator.OwnerParagraph != null)
						{
							num2 = 19;
							continue;
						}
						return text;
					case 8:
						if (num < (documentObject as Paragraph).Items.Count - 1)
						{
							num2 = 21;
							continue;
						}
						goto IL_59A;
					case 9:
						goto IL_360;
					case 10:
						goto IL_59A;
					case 11:
						goto IL_59A;
					case 12:
						goto IL_3B8;
					case 13:
						if (this.Separator.OwnerParagraph == base.OwnerParagraph)
						{
							num2 = 27;
							continue;
						}
						num3 = this.Range.ᜁ().IndexOf(this.Separator.OwnerParagraph);
						num2 = 12;
						continue;
					case 14:
						goto IL_59A;
					case 15:
						goto IL_2B3;
					case 16:
						goto IL_21D;
					case 17:
						text += spr\u20E8.\u171F;
						num2 = 4;
						continue;
					case 18:
						goto IL_2B3;
					case 19:
						num2 = 23;
						continue;
					case 20:
						this.ᜄ();
						num2 = 9;
						continue;
					case 21:
						goto IL_4A8;
					case 22:
						text += (documentObject as TextRange).Text;
						num2 = 10;
						continue;
					case 23:
						if (this.End != null)
						{
							num2 = 1;
							continue;
						}
						return text;
					case 24:
						goto IL_3B8;
					case 25:
						if (!this.\u1715)
						{
							num2 = 20;
							continue;
						}
						goto IL_360;
					case 26:
						if (this.End.OwnerParagraph == documentObject)
						{
							num2 = 36;
							continue;
						}
						goto IL_21D;
					case 27:
						num3 = this.Range.ᜁ().IndexOf(this.Separator) + 1;
						num2 = 24;
						continue;
					case 28:
						num3 = 0;
						num2 = 38;
						continue;
					case 29:
						num2 = 7;
						continue;
					case 30:
						goto IL_477;
					case 31:
						if (documentObject is Break)
						{
							num2 = 17;
							continue;
						}
						goto IL_59A;
					case 32:
						if (this.End.OwnerParagraph != documentObject)
						{
							num2 = 42;
							continue;
						}
						goto IL_477;
					case 33:
						a_ = 0;
						num = (documentObject as Paragraph).Items.Count - 1;
						num2 = 5;
						continue;
					case 34:
						return text;
					case 35:
						if (documentObject is Paragraph)
						{
							num2 = 33;
							continue;
						}
						num2 = 37;
						continue;
					case 36:
						num = this.End.ឯ() - 1;
						num2 = 16;
						continue;
					case 37:
						if (documentObject is Table)
						{
							num2 = 44;
							continue;
						}
						num2 = 46;
						continue;
					case 38:
						if (this.Range.ᜁ().Count == 0)
						{
							num2 = 3;
							continue;
						}
						goto IL_360;
					case 39:
						if (this.Separator != null)
						{
							num2 = 29;
							continue;
						}
						return text;
					case 40:
						if (this.End.OwnerParagraph != null)
						{
							num2 = 28;
							continue;
						}
						return text;
					case 41:
						goto IL_59A;
					case 42:
						text += spr\u20E8.\u171F;
						num2 = 30;
						continue;
					case 43:
						a_ = this.Separator.ឯ() + 1;
						num2 = 0;
						continue;
					case 44:
						text += (documentObject as Table).ᜐ();
						num2 = 41;
						continue;
					case 45:
						if (num4 >= this.Range.ᜁ().Count)
						{
							num2 = 34;
							continue;
						}
						documentObject = (this.Range.ᜁ()[num4] as DocumentObject);
						num2 = 35;
						continue;
					case 46:
						if (documentObject is MergeField)
						{
							num2 = 22;
							continue;
						}
						num2 = 2;
						continue;
					}
					goto IL_F2;
					IL_21D:
					text += (documentObject as Paragraph).ᜀ(a_, num);
					num2 = 32;
					continue;
					IL_2B3:
					num2 = 45;
					continue;
					IL_360:
					num2 = 13;
					continue;
					IL_3B8:
					num4 = num3;
					num2 = 18;
					continue;
					IL_477:
					num2 = 8;
					continue;
					IL_527:
					num2 = 26;
					continue;
					IL_59A:
					num4++;
					num2 = 15;
				}
				return text;
				IL_F2:
				text = string.Empty;
				num2 = 39;
				goto IL_2F;
			}
			}
		}

		// Token: 0x06003C37 RID: 15415 RVA: 0x0037EABC File Offset: 0x0037DABC
		private void ᜅ(string A_0)
		{
			Match match = Field.ᜊ.Match(A_0.Trim());
			if (match.Groups[2].Length == 0)
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
					this.m_fieldValue = match.Groups[1].Value;
					return;
				}
			}
			this.m_fieldValue = match.Groups[2].Value;
		}

		// Token: 0x06003C38 RID: 15416 RVA: 0x0037EB4C File Offset: 0x0037DB4C
		private new static string ᜄ(string A_0)
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
			string text = A_0.Remove(0, 1);
			char[] trimChars = new char[]
			{
				'"'
			};
			return text.Trim(trimChars);
		}

		// Token: 0x06003C39 RID: 15417 RVA: 0x0037EBA8 File Offset: 0x0037DBA8
		protected void ParseField(string fieldCode)
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
			char[] separator = new char[]
			{
				'\\'
			};
			string[] array = fieldCode.Split(separator);
			this.ᜅ(array[0]);
			this.ParseFieldFormat(array);
		}

		// Token: 0x06003C3A RID: 15418 RVA: 0x0037EC0C File Offset: 0x0037DC0C
		protected void ParseFieldFormat(string[] fieldValues)
		{
			int a_ = 18;
			switch (0)
			{
			default:
				for (;;)
				{
					IL_B5:
					int num = 1;
					int num2 = 20;
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
							string text;
							switch (num2)
							{
							case 0:
							{
								string a;
								if (a == ClipboardData.b("㑷ᕹ୻᭽", a_))
								{
									num2 = 7;
									continue;
								}
								num2 = 26;
								continue;
							}
							case 1:
							{
								string a;
								if (a == ClipboardData.b("⵷੹౻᭽", a_))
								{
									num2 = 5;
									continue;
								}
								num2 = 0;
								continue;
							}
							case 2:
								goto IL_1CF;
							case 3:
								this.m_textFormat = TextFormat.Titlecase;
								num2 = 23;
								continue;
							case 4:
								num2 = 6;
								continue;
							case 5:
								this.m_textFormat = TextFormat.Uppercase;
								num2 = 9;
								continue;
							case 6:
								goto IL_154;
							case 7:
								this.m_textFormat = TextFormat.Lowercase;
								num2 = 12;
								continue;
							case 8:
								if (num >= fieldValues.Length)
								{
									num2 = 19;
									continue;
								}
								text = fieldValues[num];
								num2 = 21;
								continue;
							case 9:
								goto IL_1CF;
							case 10:
								if (true)
								{
								}
								goto IL_1CF;
							case 11:
								goto IL_1CF;
							case 12:
								goto IL_33B;
							case 13:
							{
								char c;
								if (c != '@')
								{
									num2 = 18;
									continue;
								}
								goto IL_154;
							}
							case 14:
							{
								string a;
								if (a == ClipboardData.b("㹷፹๻ൽ솁", a_))
								{
									num2 = 22;
									continue;
								}
								this.m_formattingString = this.m_formattingString + ClipboardData.b("塷♹", a_) + text;
								num2 = 10;
								continue;
							}
							case 15:
								num2 = 13;
								continue;
							case 16:
							{
								char c;
								if (c != '*')
								{
									num2 = 15;
									continue;
								}
								num2 = 1;
								continue;
							}
							case 17:
							{
								string a = Field.ᜄ(text);
								char c2 = text[0];
								char c = c2;
								num2 = 16;
								continue;
							}
							case 18:
								num2 = 24;
								continue;
							case 19:
								return;
							case 20:
								goto IL_264;
							case 21:
								if (text.Length > 0)
								{
									num2 = 17;
									continue;
								}
								goto IL_1CF;
							case 22:
								this.m_textFormat = TextFormat.FirstCapital;
								num2 = 2;
								continue;
							case 23:
								goto IL_1CF;
							case 24:
							{
								char c;
								if (c != '\\')
								{
									num2 = 4;
									continue;
								}
								goto IL_154;
							}
							case 25:
								goto IL_264;
							case 26:
							{
								string a;
								if (a == ClipboardData.b("㭷᭹౻ൽ", a_))
								{
									num2 = 3;
									continue;
								}
								num2 = 14;
								continue;
							}
							}
							goto IL_B5;
							IL_154:
							this.m_formattingString = this.m_formattingString + ClipboardData.b("塷♹", a_) + text;
							num2 = 11;
							continue;
							IL_264:
							num2 = 8;
							continue;
						}
						}
						IL_1CF:
						num++;
						num2 = 25;
						continue;
						IL_33B:
						goto IL_1CF;
					}
				}
				return;
			}
		}

		// Token: 0x06003C3B RID: 15419 RVA: 0x0037EF74 File Offset: 0x0037DF74
		private new void ᜁ(string A_0, int A_1)
		{
			int a_ = 4;
			for (;;)
			{
				A_1 += 2;
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
							goto IL_E2;
						default:
							if (false)
							{
							}
							if (A_0.Length > A_1)
							{
								num = 4;
								continue;
							}
							return;
						}
						break;
					case 1:
						goto IL_A7;
					case 2:
					{
						int num2;
						if (num2 == -1)
						{
							goto IL_E2;
						}
						string text;
						int num3 = text.IndexOf(ClipboardData.b("䡩", a_), num2 + 1);
						num = 3;
						continue;
					}
					case 3:
					{
						int num3;
						if (num3 == -1)
						{
							num = 1;
							continue;
						}
						int num2;
						string text;
						this.ᜐ = text.Substring(num2, num3 + 1 - num2);
						num = 5;
						continue;
					}
					case 4:
					{
						string text = A_0.Substring(A_1, A_0.Length - A_1).Trim();
						int num2 = text.IndexOf(ClipboardData.b("䡩", a_));
						num = 2;
						continue;
					}
					case 5:
						return;
					case 6:
						return;
					}
					break;
					IL_E2:
					num = 6;
				}
			}
			IL_A7:
			if (true)
			{
			}
		}

		// Token: 0x06003C3C RID: 15420 RVA: 0x0037F09C File Offset: 0x0037E09C
		private void ᜂ()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_24;
					}
					if (false)
					{
					}
					this.ᜑ = string.Empty;
					num = 2;
					continue;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					return;
				}
				IL_24:
				if (base.Document.ᜇ)
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x06003C3D RID: 15421 RVA: 0x0037F120 File Offset: 0x0037E120
		private new string ᜀ(string A_0, int A_1)
		{
			int a_ = 12;
			for (;;)
			{
				A_0 = A_0.Remove(0, A_1 + 2).Trim();
				if (true)
				{
				}
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return A_0;
					case 1:
						goto IL_D8;
					case 2:
						A_0 = A_0.Remove(0, A_0.IndexOf(ClipboardData.b("偱", a_)) + 1);
						A_0 = A_0.Remove(A_0.LastIndexOf(ClipboardData.b("偱", a_)));
						num = 0;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D8;
						default:
							if (false)
							{
							}
							if (A_0.StartsWith(ClipboardData.b("偱", a_)))
							{
								num = 4;
								continue;
							}
							return A_0;
						}
						break;
					case 4:
						num = 1;
						continue;
					}
					break;
					IL_D8:
					if (!A_0.EndsWith(ClipboardData.b("偱", a_)))
					{
						return A_0;
					}
					num = 2;
				}
			}
			return A_0;
		}

		// Token: 0x06003C3E RID: 15422 RVA: 0x0037F234 File Offset: 0x0037E234
		private new string ᜀ(string A_0, out bool A_1)
		{
			int a_ = 14;
			for (;;)
			{
				A_1 = false;
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						A_0 = A_0.Remove(A_0.IndexOf(ClipboardData.b("㕳㭵坷⩹ㅻ", a_)), A_0.Length - A_0.IndexOf(ClipboardData.b("㕳㭵坷⩹ㅻ", a_)));
						A_1 = true;
						num = 5;
						continue;
					case 1:
						goto IL_C5;
					case 2:
						if (A_0.Contains(ClipboardData.b("ᕳ᭵坷੹ᅻ", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_C5;
					case 3:
						A_0 = A_0.Remove(A_0.IndexOf(ClipboardData.b("ᕳ᭵坷੹ᅻ", a_)), A_0.Length - A_0.IndexOf(ClipboardData.b("ᕳ᭵坷੹ᅻ", a_)));
						A_1 = true;
						num = 1;
						continue;
					case 4:
						if (A_0.Contains(ClipboardData.b("㕳㭵坷⩹ㅻ", a_)))
						{
							num = 0;
							continue;
						}
						return A_0;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_BA;
						}
						break;
					}
					break;
					IL_C5:
					num = 4;
				}
			}
			IL_BA:
			if (false)
			{
			}
			return A_0;
		}

		// Token: 0x06003C3F RID: 15423 RVA: 0x0037F38C File Offset: 0x0037E38C
		private new string ᜁ(string A_0, DateTime A_1)
		{
			int a_ = 0;
			int num = 0;
			for (;;)
			{
				IL_1D:
				switch (num)
				{
				case 1:
					A_0 += ClipboardData.b("❥╧", a_);
					num = 3;
					continue;
				case 2:
					return A_0;
				case 3:
					return A_0;
				}
				while (!A_1.ToString().Contains(ClipboardData.b("❥╧", a_)))
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
						A_0 += ClipboardData.b("㙥╧", a_);
						num = 2;
						goto IL_1D;
					}
				}
				num = 1;
			}
			return A_0;
		}

		// Token: 0x06003C40 RID: 15424 RVA: 0x0037F464 File Offset: 0x0037E464
		private new string ᜀ(string A_0, DateTime A_1)
		{
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					int num = 0;
					text = string.Empty;
					int num2 = 0;
					int num3 = 60;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							num3 = 67;
							continue;
						case 1:
							if (A_0[num2] != 'y')
							{
								num3 = 43;
								continue;
							}
							goto IL_19A;
						case 2:
							goto IL_6B4;
						case 3:
							goto IL_76F;
						case 4:
						{
							char c;
							if (c != 's')
							{
								num3 = 50;
								continue;
							}
							goto IL_38B;
						}
						case 5:
							goto IL_76F;
						case 6:
							goto IL_2C9;
						case 7:
							goto IL_2F1;
						case 8:
							num3 = 12;
							continue;
						case 9:
							if (num2 < A_0.Length)
							{
								num3 = 62;
								continue;
							}
							goto IL_5F2;
						case 10:
						{
							char c;
							if (c != 'y')
							{
								num3 = 66;
								continue;
							}
							goto IL_6F6;
						}
						case 11:
							if (num2 < A_0.Length)
							{
								num3 = 52;
								continue;
							}
							goto IL_3B4;
						case 12:
						{
							char c;
							if (c <= 'D')
							{
								goto IL_57A;
							}
							num3 = 54;
							continue;
						}
						case 13:
							num3 = 1;
							continue;
						case 14:
							if (num2 < A_0.Length)
							{
								num3 = 0;
								continue;
							}
							goto IL_60F;
						case 15:
							num3 = 20;
							continue;
						case 16:
						{
							char c;
							if (c != 'Y')
							{
								num3 = 36;
								continue;
							}
							goto IL_6F6;
						}
						case 17:
							num3 = 61;
							continue;
						case 18:
							num3 = 16;
							continue;
						case 19:
						{
							if (num2 >= A_0.Length)
							{
								num3 = 51;
								continue;
							}
							char c = A_0[num2];
							num3 = 44;
							continue;
						}
						case 20:
						{
							char c;
							if (c != 'h')
							{
								num3 = 33;
								continue;
							}
							goto IL_171;
						}
						case 21:
							goto IL_7C0;
						case 22:
							goto IL_76F;
						case 23:
							goto IL_4A3;
						case 24:
						{
							char c;
							if (c != '\\')
							{
								num3 = 76;
								continue;
							}
							goto IL_669;
						}
						case 25:
							goto IL_2C9;
						case 26:
							goto IL_271;
						case 27:
						{
							char c;
							if (c != 'm')
							{
								num3 = 46;
								continue;
							}
							goto IL_4A3;
						}
						case 28:
						{
							char c;
							if (c <= 'h')
							{
								num3 = 49;
								continue;
							}
							num3 = 27;
							continue;
						}
						case 29:
							goto IL_76F;
						case 30:
							if (A_0[num2] != 'h')
							{
								num3 = 53;
								continue;
							}
							goto IL_230;
						case 31:
							if (A_0[num2] != 'd')
							{
								num3 = 77;
								continue;
							}
							goto IL_3D1;
						case 32:
							if (A_0[num2] != 'm')
							{
								num3 = 2;
								continue;
							}
							num2++;
							num++;
							num3 = 23;
							continue;
						case 33:
							num3 = 58;
							continue;
						case 34:
						{
							char c;
							if (c != 'M')
							{
								num3 = 18;
								continue;
							}
							goto IL_58B;
						}
						case 35:
							if (true)
							{
							}
							num3 = 32;
							continue;
						case 36:
							num3 = 59;
							continue;
						case 37:
							if (num2 < A_0.Length)
							{
								num3 = 17;
								continue;
							}
							goto IL_271;
						case 38:
							goto IL_58B;
						case 39:
							goto IL_3B4;
						case 40:
							num3 = 25;
							continue;
						case 41:
							num3 = 70;
							continue;
						case 42:
							goto IL_6F6;
						case 43:
							num3 = 65;
							continue;
						case 44:
						{
							char c;
							if (c <= 'Y')
							{
								num3 = 8;
								continue;
							}
							num3 = 28;
							continue;
						}
						case 45:
							goto IL_60F;
						case 46:
							num3 = 4;
							continue;
						case 47:
							goto IL_171;
						case 48:
						{
							char c;
							if (c != 'd')
							{
								num3 = 15;
								continue;
							}
							goto IL_2F1;
						}
						case 49:
							num3 = 24;
							continue;
						case 50:
							num3 = 10;
							continue;
						case 51:
							return text;
						case 52:
							num3 = 30;
							continue;
						case 53:
							num3 = 78;
							continue;
						case 54:
						{
							char c;
							if (c != 'H')
							{
								num3 = 72;
								continue;
							}
							goto IL_171;
						}
						case 55:
							goto IL_5F2;
						case 56:
							goto IL_76F;
						case 57:
							goto IL_76F;
						case 58:
							goto IL_2C9;
						case 59:
							goto IL_2C9;
						case 60:
							goto IL_76F;
						case 61:
							if (A_0[num2] != 'M')
							{
								num3 = 26;
								continue;
							}
							num2++;
							num++;
							num3 = 38;
							continue;
						case 62:
							num3 = 31;
							continue;
						case 63:
							goto IL_76F;
						case 64:
							if (A_0[num2] != 'D')
							{
								num3 = 55;
								continue;
							}
							goto IL_3D1;
						case 65:
							if (A_0[num2] != 'Y')
							{
								num3 = 21;
								continue;
							}
							goto IL_19A;
						case 66:
							num3 = 6;
							continue;
						case 67:
							if (A_0[num2] != 's')
							{
								num3 = 45;
								continue;
							}
							num2++;
							num++;
							num3 = 75;
							continue;
						case 68:
						{
							char c;
							if (c != 'D')
							{
								num3 = 40;
								continue;
							}
							goto IL_2F1;
						}
						case 69:
							if (num2 < A_0.Length)
							{
								num3 = 35;
								continue;
							}
							goto IL_6B4;
						case 70:
						{
							char c;
							if (c != '\'')
							{
								num3 = 73;
								continue;
							}
							goto IL_669;
						}
						case 71:
							if (num2 < A_0.Length)
							{
								num3 = 13;
								continue;
							}
							goto IL_7C0;
						case 72:
							num3 = 34;
							continue;
						case 73:
							num3 = 68;
							continue;
						case 74:
							goto IL_76F;
						case 75:
							goto IL_38B;
						case 76:
							num3 = 48;
							continue;
						case 77:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_57A;
							default:
								if (false)
								{
								}
								num3 = 64;
								continue;
							}
							break;
						case 78:
							if (A_0[num2] != 'H')
							{
								num3 = 39;
								continue;
							}
							goto IL_230;
						}
						break;
						IL_171:
						num3 = 11;
						continue;
						IL_19A:
						num2++;
						num++;
						num3 = 42;
						continue;
						IL_230:
						num2++;
						num++;
						num3 = 47;
						continue;
						IL_271:
						text = this.ᜄ(text, A_1, num);
						num = 0;
						num3 = 5;
						continue;
						IL_2C9:
						text += A_0[num2];
						num2++;
						num3 = 57;
						continue;
						IL_2F1:
						num3 = 9;
						continue;
						IL_38B:
						num3 = 14;
						continue;
						IL_3B4:
						text = this.ᜂ(text, A_1, num);
						num = 0;
						num3 = 3;
						continue;
						IL_3D1:
						num2++;
						num++;
						num3 = 7;
						continue;
						IL_4A3:
						num3 = 69;
						continue;
						IL_57A:
						num3 = 41;
						continue;
						IL_58B:
						num3 = 37;
						continue;
						IL_5F2:
						text = this.ᜅ(text, A_1, num);
						num = 0;
						num3 = 63;
						continue;
						IL_60F:
						text = this.ᜀ(text, A_1, num);
						num = 0;
						num3 = 29;
						continue;
						IL_669:
						num2++;
						num3 = 56;
						continue;
						IL_6B4:
						text = this.ᜁ(text, A_1, num);
						num = 0;
						num3 = 22;
						continue;
						IL_6F6:
						num3 = 71;
						continue;
						IL_76F:
						num3 = 19;
						continue;
						IL_7C0:
						text = this.ᜃ(text, A_1, num);
						num = 0;
						num3 = 74;
					}
				}
				return text;
			}
			}
		}

		// Token: 0x06003C41 RID: 15425 RVA: 0x0037FC68 File Offset: 0x0037EC68
		private string ᜅ(string A_0, DateTime A_1, int A_2)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				string str;
				for (;;)
				{
					str = string.Empty;
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 7;
							continue;
						case 1:
							str = ClipboardData.b("䥸", a_) + A_1.Day.ToString();
							num = 2;
							continue;
						case 2:
							goto IL_1AE;
						case 3:
							switch (A_2)
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
									str = A_1.Day.ToString();
									num = 4;
									continue;
								}
								break;
							case 2:
							{
								int day = A_1.Day;
								num = 9;
								continue;
							}
							case 3:
								break;
							case 4:
								str = A_1.DayOfWeek.ToString();
								num = 5;
								continue;
							default:
								num = 0;
								continue;
							}
							str = A_1.DayOfWeek.ToString().Remove(3, A_1.Day.ToString().Length);
							num = 8;
							continue;
						case 4:
							goto IL_11B;
						case 5:
							goto IL_17C;
						case 6:
							goto IL_13E;
						case 7:
							str = A_1.DayOfWeek.ToString();
							num = 6;
							continue;
						case 8:
							goto IL_BC;
						case 9:
						{
							int day;
							if (Convert.ToInt16(day.ToString()) < 10)
							{
								num = 1;
								continue;
							}
							str = A_1.Day.ToString();
							num = 10;
							continue;
						}
						case 10:
							goto IL_DE;
						}
						break;
					}
				}
				IL_BC:
				IL_DE:
				IL_11B:
				IL_13E:
				IL_17C:
				IL_1AE:
				if (true)
				{
				}
				A_0 += str;
				return A_0;
			}
			}
		}

		// Token: 0x06003C42 RID: 15426 RVA: 0x0037FE6C File Offset: 0x0037EE6C
		private new string ᜄ(string A_0, DateTime A_1, int A_2)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				string str;
				for (;;)
				{
					IL_4C:
					str = string.Empty;
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_1E1:
						num = 5;
						break;
					default:
						if (false)
						{
						}
						num = 7;
						break;
					}
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_1BB;
						case 1:
							goto IL_225;
						case 2:
							goto IL_10B;
						case 3:
							goto IL_16F;
						case 4:
						{
							int month;
							if (Convert.ToInt16(month.ToString()) < 10)
							{
								num = 1;
								continue;
							}
							str = A_1.Month.ToString();
							num = 2;
							continue;
						}
						case 5:
							goto IL_1ED;
						case 6:
							goto IL_12C;
						case 7:
							switch (A_2)
							{
							case 1:
								str = A_1.Month.ToString();
								num = 6;
								continue;
							case 2:
							{
								int month = A_1.Month;
								num = 4;
								continue;
							}
							case 3:
								str = Enum.GetName(typeof(Field.Month), Convert.ToInt32(A_1.Month.ToString())).Substring(0, 3);
								num = 9;
								continue;
							case 4:
								str = Enum.GetName(typeof(Field.Month), Convert.ToInt32(A_1.Month.ToString()));
								num = 0;
								continue;
							default:
								num = 10;
								continue;
							}
							break;
						case 8:
							str = Enum.GetName(typeof(Field.Month), Convert.ToInt32(A_1.Month.ToString()));
							num = 3;
							continue;
						case 9:
							goto IL_E9;
						case 10:
							num = 8;
							continue;
						}
						goto IL_4C;
					}
					IL_225:
					str = ClipboardData.b("奨", a_) + A_1.Month.ToString();
					goto IL_1E1;
				}
				IL_E9:
				IL_10B:
				goto IL_227;
				IL_12C:
				if (true)
				{
				}
				IL_16F:
				IL_1BB:
				IL_1ED:
				IL_227:
				A_0 += str;
				return A_0;
			}
			}
		}

		// Token: 0x06003C43 RID: 15427 RVA: 0x003800AC File Offset: 0x0037F0AC
		private new string ᜃ(string A_0, DateTime A_1, int A_2)
		{
			switch (0)
			{
			default:
			{
				string str;
				for (;;)
				{
					str = string.Empty;
					if (true)
					{
					}
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_E7;
						case 1:
							switch (A_2)
							{
							case 1:
								str = A_1.Year.ToString().Remove(0, 2);
								num = 4;
								continue;
							case 2:
								str = A_1.Year.ToString().Remove(0, 2);
								num = 3;
								continue;
							case 3:
								goto IL_E7;
							case 4:
								str = A_1.Year.ToString();
								goto IL_A6;
							default:
								num = 5;
								continue;
							}
							break;
						case 2:
							goto IL_B2;
						case 3:
							goto IL_D7;
						case 4:
							goto IL_129;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A6;
							default:
								if (false)
								{
								}
								num = 0;
								continue;
							}
							break;
						case 6:
							goto IL_104;
						}
						break;
						IL_A6:
						num = 2;
						continue;
						IL_E7:
						str = A_1.Year.ToString();
						num = 6;
					}
				}
				IL_B2:
				IL_D7:
				IL_104:
				IL_129:
				A_0 += str;
				return A_0;
			}
			}
		}

		// Token: 0x06003C44 RID: 15428 RVA: 0x003801F0 File Offset: 0x0037F1F0
		private string ᜂ(string A_0, DateTime A_1, int A_2)
		{
			int a_ = 8;
			switch (0)
			{
			default:
			{
				string str;
				for (;;)
				{
					str = string.Empty;
					int num = 7;
					for (;;)
					{
						int hour;
						switch (num)
						{
						case 0:
							num = 8;
							continue;
						case 1:
							goto IL_8A;
						case 2:
							goto IL_16A;
						case 3:
							if (Convert.ToInt16(hour.ToString()) < 10)
							{
								num = 6;
								continue;
							}
							str = A_1.Hour.ToString();
							num = 1;
							continue;
						case 4:
							goto IL_112;
						case 5:
							goto IL_138;
						case 6:
							str = ClipboardData.b("幭", a_) + A_1.Hour.ToString();
							num = 2;
							continue;
						case 7:
							switch (A_2)
							{
							case 1:
								if (true)
								{
								}
								str = A_1.Hour.ToString();
								num = 5;
								continue;
							case 2:
								goto IL_8F;
							default:
								num = 0;
								continue;
							}
							break;
						case 8:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8F;
							default:
								if (false)
								{
								}
								str = A_1.Hour.ToString();
								num = 4;
								continue;
							}
							break;
						}
						break;
						IL_8F:
						hour = A_1.Hour;
						num = 3;
					}
				}
				IL_8A:
				IL_112:
				IL_138:
				IL_16A:
				A_0 += str;
				return A_0;
			}
			}
		}

		// Token: 0x06003C45 RID: 15429 RVA: 0x00380374 File Offset: 0x0037F374
		private new string ᜁ(string A_0, DateTime A_1, int A_2)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				string str;
				for (;;)
				{
					str = string.Empty;
					int num = 1;
					for (;;)
					{
						int minute;
						switch (num)
						{
						case 0:
							num = 4;
							continue;
						case 1:
							switch (A_2)
							{
							case 1:
								str = A_1.Minute.ToString();
								num = 2;
								continue;
							case 2:
								goto IL_8F;
							default:
								num = 0;
								continue;
							}
							break;
						case 2:
							goto IL_138;
						case 3:
							if (Convert.ToInt16(minute.ToString()) < 10)
							{
								num = 5;
								continue;
							}
							str = A_1.Minute.ToString();
							num = 6;
							continue;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_8F;
							default:
								if (false)
								{
								}
								str = A_1.Minute.ToString();
								num = 8;
								continue;
							}
							break;
						case 5:
							str = ClipboardData.b("䥸", a_) + A_1.Minute.ToString();
							num = 7;
							continue;
						case 6:
							goto IL_8A;
						case 7:
							goto IL_16A;
						case 8:
							goto IL_112;
						}
						break;
						IL_8F:
						minute = A_1.Minute;
						num = 3;
					}
				}
				IL_8A:
				goto IL_16C;
				IL_112:
				if (true)
				{
				}
				IL_138:
				IL_16A:
				IL_16C:
				A_0 += str;
				return A_0;
			}
			}
		}

		// Token: 0x06003C46 RID: 15430 RVA: 0x003804F8 File Offset: 0x0037F4F8
		private new string ᜀ(string A_0, DateTime A_1, int A_2)
		{
			int a_ = 17;
			switch (0)
			{
			default:
			{
				string str;
				for (;;)
				{
					str = string.Empty;
					int num = 6;
					for (;;)
					{
						int second;
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_97;
							default:
								if (false)
								{
								}
								str = A_1.Second.ToString();
								num = 5;
								continue;
							}
							break;
						case 1:
							goto IL_16A;
						case 2:
							goto IL_92;
						case 3:
							num = 0;
							continue;
						case 4:
							if (Convert.ToInt16(second.ToString()) < 10)
							{
								num = 8;
								continue;
							}
							str = A_1.Second.ToString();
							if (true)
							{
							}
							num = 2;
							continue;
						case 5:
							goto IL_11A;
						case 6:
							switch (A_2)
							{
							case 1:
								str = A_1.Second.ToString();
								num = 7;
								continue;
							case 2:
								goto IL_97;
							default:
								num = 3;
								continue;
							}
							break;
						case 7:
							goto IL_138;
						case 8:
							str = ClipboardData.b("䝶", a_) + A_1.Second.ToString();
							num = 1;
							continue;
						}
						break;
						IL_97:
						second = A_1.Second;
						num = 4;
					}
				}
				IL_92:
				IL_11A:
				IL_138:
				IL_16A:
				A_0 += str;
				return A_0;
			}
			}
		}

		// Token: 0x06003C47 RID: 15431 RVA: 0x0038067C File Offset: 0x0037F67C
		private new void ᜃ(string A_0)
		{
			for (;;)
			{
				Paragraph ownerParagraph = base.OwnerParagraph;
				int num = base.ឯ();
				ParagraphBase paragraphBase = ownerParagraph.Items[++num];
				int num2 = 8;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return;
					case 1:
						if ((paragraphBase as FieldMark).Type == FieldMarkType.FieldSeparator)
						{
							num2 = 2;
							continue;
						}
						return;
					case 2:
						paragraphBase = ownerParagraph.Items[++num];
						num2 = 4;
						continue;
					case 3:
						goto IL_F5;
					case 4:
						if (paragraphBase is TextRange)
						{
							num2 = 7;
							continue;
						}
						return;
					case 5:
						if (true)
						{
						}
						num2 = 1;
						continue;
					case 6:
						goto IL_F5;
					case 7:
						(paragraphBase as TextRange).Text = A_0;
						num2 = 3;
						continue;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (!(paragraphBase is FieldMark))
							{
								return;
							}
							break;
						}
						num2 = 5;
						continue;
					case 9:
						if (!(ownerParagraph.Items[++num] is TextRange))
						{
							num2 = 0;
							continue;
						}
						ownerParagraph.Items.Remove(ownerParagraph.Items[num]);
						num2 = 6;
						continue;
					}
					break;
					IL_F5:
					num2 = 9;
				}
			}
		}

		// Token: 0x06003C48 RID: 15432 RVA: 0x003807F0 File Offset: 0x0037F7F0
		private new void ᜀ(string A_0, string A_1)
		{
			for (;;)
			{
				this.FieldResult = A_1;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						int num2 = 0;
						num = 1;
						continue;
					}
					case 1:
						IL_B4:
						goto IL_65;
					case 2:
						if (base.OwnerParagraph != null)
						{
							num = 12;
							continue;
						}
						return;
					case 3:
						goto IL_C2;
					case 4:
						if (this.End == null)
						{
							num = 9;
							continue;
						}
						this.ᜁ();
						this.ᜀ();
						this.ᜁ(A_1);
						num = 5;
						continue;
					case 5:
						if (this.\u171A.Count == 0)
						{
							num = 7;
							continue;
						}
						num = 14;
						continue;
					case 6:
						num = 3;
						continue;
					case 7:
						this.End.OwnerParagraph.Items.Insert(this.End.ឯ(), this.ᜀ(A_1));
						num = 11;
						continue;
					case 8:
						goto IL_141;
					case 9:
						goto IL_E8;
					case 10:
					{
						int num2;
						if (num2 >= this.\u171A.Count)
						{
							num = 6;
							continue;
						}
						base.OwnerParagraph.Items.Insert(this.End.ឯ(), this.\u171A[num2]);
						num2++;
						num = 13;
						continue;
					}
					case 11:
						goto IL_178;
					case 12:
						num = 4;
						continue;
					case 13:
						goto IL_65;
					case 14:
						if (base.OwnerParagraph == this.End.OwnerParagraph)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						this.ᜂ(A_1);
						num = 8;
						continue;
					}
					break;
					IL_65:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B4;
					default:
						if (false)
						{
						}
						num = 10;
						break;
					}
				}
			}
			return;
			IL_C2:
			goto IL_1EB;
			IL_E8:
			return;
			IL_141:
			IL_178:
			IL_1EB:
			this.\u171A.Clear();
			this.\u1715 = false;
		}

		// Token: 0x06003C49 RID: 15433 RVA: 0x003809FC File Offset: 0x0037F9FC
		private void ᜂ(string A_0)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				int num = 5;
				for (;;)
				{
					int num3;
					DocumentObject documentObject;
					Paragraph paragraph;
					int num4;
					switch (num)
					{
					case 0:
						goto IL_284;
					case 1:
						num = 0;
						continue;
					case 2:
					{
						int num2 = 0;
						num = 26;
						continue;
					}
					case 3:
						num3 = this.Range.ᜁ().Count - 1;
						goto IL_372;
					case 4:
					{
						int num2;
						if (num2 >= (documentObject as Paragraph).Items.Count)
						{
							num = 8;
							continue;
						}
						paragraph.Items.Insert(num2, (documentObject as Paragraph).Items[num2].Clone());
						num2++;
						num = 11;
						continue;
					}
					case 6:
						num = 21;
						continue;
					case 7:
						goto IL_284;
					case 8:
						num = 27;
						continue;
					case 9:
						goto IL_284;
					case 10:
						IL_1F1:
						num = 16;
						continue;
					case 11:
						goto IL_1F3;
					case 12:
						num = 23;
						continue;
					case 13:
						if (num4 == this.\u171A.Count - 1)
						{
							num = 30;
							continue;
						}
						goto IL_1CE;
					case 14:
						if (documentObject is Paragraph)
						{
							num = 10;
							continue;
						}
						goto IL_E3;
					case 15:
						goto IL_3D0;
					case 16:
						if (num4 == 0)
						{
							num = 12;
							continue;
						}
						goto IL_E3;
					case 17:
						if (!A_0.EndsWith(ClipboardData.b("慫", a_)))
						{
							num = 2;
							continue;
						}
						goto IL_1CE;
					case 18:
						goto IL_412;
					case 19:
						goto IL_3D0;
					case 20:
						if (true)
						{
						}
						num = 24;
						continue;
					case 21:
						num3 = 0;
						goto IL_372;
					case 22:
						if (documentObject is Body)
						{
							num = 20;
							continue;
						}
						num = 34;
						continue;
					case 23:
						if (!A_0.StartsWith(ClipboardData.b("慫", a_)))
						{
							num = 32;
							continue;
						}
						goto IL_E3;
					case 24:
						if (documentObject is Paragraph)
						{
							num = 25;
							continue;
						}
						goto IL_1CE;
					case 25:
						num = 13;
						continue;
					case 26:
						goto IL_1F3;
					case 27:
						goto IL_284;
					case 28:
						return;
					case 29:
						goto IL_412;
					case 30:
						num = 17;
						continue;
					case 31:
						this.Separator.OwnerParagraph.Items.Add(documentObject);
						num = 7;
						continue;
					case 32:
					{
						int num5 = 0;
						num = 15;
						continue;
					}
					case 33:
					{
						int num5;
						if (num5 >= (documentObject as Paragraph).Items.Count)
						{
							num = 1;
							continue;
						}
						this.Separator.OwnerParagraph.Items.Add((documentObject as Paragraph).Items[num5].Clone());
						num5++;
						num = 19;
						continue;
					}
					case 34:
						if (documentObject is ParagraphBase)
						{
							num = 31;
							continue;
						}
						goto IL_284;
					case 35:
						if (num4 >= this.\u171A.Count)
						{
							num = 28;
							continue;
						}
						documentObject = this.\u171A[num4];
						num = 22;
						continue;
					}
					if (this.Range.ᜁ().Count <= 0)
					{
						num = 6;
						continue;
					}
					num = 3;
					continue;
					IL_E3:
					Body ownerTextBody = paragraph.OwnerTextBody;
					ownerTextBody.Items.Insert(paragraph.ឯ(), documentObject);
					num = 9;
					continue;
					IL_1CE:
					num = 14;
					continue;
					IL_284:
					num4++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F1;
					default:
						if (false)
						{
						}
						num = 18;
						continue;
					}
					IL_1F3:
					num = 4;
					continue;
					IL_372:
					int index = num3;
					paragraph = (this.Range.ᜁ()[index] as Paragraph);
					num4 = 0;
					num = 29;
					continue;
					IL_3D0:
					num = 33;
					continue;
					IL_412:
					num = 35;
				}
				return;
			}
			}
		}

		// Token: 0x06003C4A RID: 15434 RVA: 0x00380E8C File Offset: 0x0037FE8C
		private new void ᜁ()
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				FieldMark fieldMark;
				for (;;)
				{
					Paragraph paragraph;
					switch (num)
					{
					case 0:
						if (this.Separator.NextSibling != null)
						{
							num = 5;
							continue;
						}
						return;
					case 1:
						goto IL_166;
					case 2:
						goto IL_1B5;
					case 3:
					{
						if (base.OwnerParagraph == this.End.OwnerParagraph)
						{
							num = 12;
							continue;
						}
						paragraph = (this.End.OwnerParagraph.Clone() as Paragraph);
						paragraph.\u170D();
						int index = this.End.OwnerParagraph.ឯ();
						this.End.OwnerParagraph.OwnerTextBody.Items.Insert(index, paragraph);
						int count = this.End.OwnerParagraph.Items.Count;
						int num2 = 0;
						num = 14;
						continue;
					}
					case 4:
					{
						paragraph.Items.Add(this.End.OwnerParagraph.Items[0]);
						int num2;
						num2++;
						num = 11;
						continue;
					}
					case 5:
						this.\u171D = ((this.Separator.NextSibling as ParagraphBase).ParaItemCharFormat.ឱ() as CharacterFormat);
						num = 2;
						continue;
					case 6:
						fieldMark = new FieldMark(base.Document, FieldMarkType.FieldSeparator);
						num = 3;
						continue;
					case 8:
					{
						int count;
						int num2;
						if (num2 >= count)
						{
							num = 1;
							continue;
						}
						num = 10;
						continue;
					}
					case 9:
						goto IL_75;
					case 10:
						if (this.End != this.End.OwnerParagraph.Items[0])
						{
							num = 4;
							continue;
						}
						goto IL_166;
					case 11:
						IL_2A4:
						goto IL_AA;
					case 12:
						base.OwnerParagraph.Items.Insert(this.End.ឯ(), fieldMark);
						num = 13;
						continue;
					case 13:
						goto IL_75;
					case 14:
						goto IL_AA;
					}
					if (this.Separator == null)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
					IL_AA:
					num = 8;
					continue;
					IL_75:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2A4;
					default:
						goto IL_95;
					}
					IL_166:
					paragraph.Items.Add(fieldMark);
					num = 9;
				}
				IL_95:
				if (false)
				{
				}
				this.Separator = fieldMark;
				this.\u1715 = false;
				return;
				IL_1B5:
				if (true)
				{
				}
				return;
			}
			}
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x00381144 File Offset: 0x00380144
		private new void ᜁ(string A_0)
		{
			int a_ = 7;
			switch (0)
			{
			default:
				for (;;)
				{
					this.\u171A.Clear();
					int num = 5;
					for (;;)
					{
						string text2;
						string text;
						int num2;
						string text3;
						DocumentObject documentObject2;
						DocumentObject documentObject3;
						DocumentObject documentObject;
						string str;
						int num3;
						switch (num)
						{
						case 0:
							text = text2;
							num = 12;
							continue;
						case 1:
							return;
						case 2:
							goto IL_2A0;
						case 3:
							goto IL_400;
						case 4:
							goto IL_DF;
						case 5:
						{
							if (A_0 == string.Empty)
							{
								num = 1;
								continue;
							}
							int a_2 = this.NestedFieldCode.IndexOf(A_0);
							text = string.Empty;
							num2 = this.ᜀ(a_2, ref text);
							text3 = string.Empty;
							text2 = A_0;
							num = 21;
							continue;
						}
						case 6:
							text3 += text2;
							documentObject = this.ᜂ(documentObject2, text2, ref documentObject3);
							num = 26;
							continue;
						case 7:
							goto IL_EB;
						case 8:
							str = this.ᜁ(documentObject2);
							num = 25;
							continue;
						case 9:
							text3 += text;
							num = 17;
							continue;
						case 10:
							goto IL_DF;
						case 11:
							goto IL_26F;
						case 12:
							goto IL_2D3;
						case 13:
							text = text.Replace(ClipboardData.b("恬", a_), string.Empty);
							num = 3;
							continue;
						case 14:
							this.\u171A.Add(documentObject);
							num = 19;
							continue;
						case 15:
							if (documentObject3 != null)
							{
								num = 22;
								continue;
							}
							goto IL_249;
						case 16:
							goto IL_26F;
						case 17:
							if (text.Contains(ClipboardData.b("恬", a_)))
							{
								num = 13;
								continue;
							}
							goto IL_400;
						case 18:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_EB;
							default:
								if (false)
								{
								}
								if (num3 == num2)
								{
									num = 29;
									continue;
								}
								goto IL_103;
							}
							break;
						case 19:
							if (true)
							{
							}
							goto IL_370;
						case 20:
							if (text != string.Empty)
							{
								num = 9;
								continue;
							}
							goto IL_103;
						case 21:
							if (text2.Length < text.Length)
							{
								num = 0;
								continue;
							}
							goto IL_2D3;
						case 22:
							num3 = this.Range.ᜁ().IndexOf(documentObject3);
							this.\u1719.Clear();
							this.\u1718 = false;
							documentObject3 = null;
							num = 32;
							continue;
						case 23:
							if (documentObject2 is ParagraphBase)
							{
								num = 8;
								continue;
							}
							str = this.ᜃ(documentObject2);
							num = 24;
							continue;
						case 24:
							goto IL_329;
						case 25:
							goto IL_329;
						case 26:
							goto IL_DF;
						case 27:
							if (A_0.Length < (text3 + str).Length)
							{
								num = 6;
								continue;
							}
							text3 += str;
							documentObject = this.ᜂ(documentObject2, null, ref documentObject3);
							num = 10;
							continue;
						case 28:
							if (!(text3 == A_0))
							{
								num = 31;
								continue;
							}
							goto IL_49B;
						case 29:
							num = 20;
							continue;
						case 30:
							if (num3 >= this.Range.ᜁ().Count - 1)
							{
								num = 2;
								continue;
							}
							documentObject2 = (this.Range.ᜁ()[num3] as DocumentObject);
							num = 23;
							continue;
						case 31:
							num3++;
							num = 11;
							continue;
						case 32:
							goto IL_249;
						}
						break;
						IL_DF:
						num = 7;
						continue;
						IL_EB:
						if (documentObject != null)
						{
							num = 14;
							continue;
						}
						goto IL_370;
						IL_103:
						num = 27;
						continue;
						IL_249:
						num = 28;
						continue;
						IL_26F:
						num = 30;
						continue;
						IL_2D3:
						str = string.Empty;
						this.\u1717 = false;
						this.\u1718 = false;
						this.\u1719.Clear();
						num3 = num2;
						num = 16;
						continue;
						IL_329:
						documentObject = null;
						documentObject3 = null;
						num = 18;
						continue;
						IL_370:
						text2 = A_0.Substring(text3.Length);
						num = 15;
						continue;
						IL_400:
						documentObject = this.ᜂ(documentObject2, text, ref documentObject3);
						num = 4;
					}
				}
				return;
				IL_2A0:
				IL_49B:
				this.\u1717 = false;
				this.\u1718 = false;
				this.\u1719.Clear();
				return;
			}
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x00381608 File Offset: 0x00380608
		private DocumentObject ᜂ(DocumentObject A_0, string A_1, ref DocumentObject A_2)
		{
			int num = 6;
			DocumentObject result;
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
						goto IL_7C;
					case 1:
						return result;
					case 2:
					{
						List<DocumentObject> list = this.ᜀ(A_0, A_1, ref A_2);
						int num2 = 0;
						num = 0;
						continue;
					}
					case 3:
						return result;
					case 4:
					{
						List<DocumentObject> list;
						result = list[list.Count - 1];
						num = 10;
						continue;
					}
					case 5:
						goto IL_7C;
					case 7:
						if (A_0 is Paragraph)
						{
							num = 2;
							continue;
						}
						result = this.ᜀ(A_0);
						num = 1;
						continue;
					case 8:
						result = this.ᜁ(A_0, A_1, ref A_2);
						num = 3;
						continue;
					case 9:
					{
						List<DocumentObject> list;
						int num2;
						if (num2 >= list.Count - 1)
						{
							num = 4;
							continue;
						}
						this.\u171A.Add(list[num2]);
						num2++;
						num = 5;
						continue;
					}
					case 10:
						return result;
					}
					break;
					IL_7C:
					num = 9;
					continue;
				}
				if (true)
				{
				}
				if (A_0 is ParagraphBase)
				{
					num = 8;
				}
				else
				{
					num = 7;
				}
			}
			return result;
		}

		// Token: 0x06003C4D RID: 15437 RVA: 0x0038175C File Offset: 0x0038075C
		private new DocumentObject ᜀ(DocumentObject A_0)
		{
			switch (0)
			{
			default:
			{
				DocumentObject documentObject;
				for (;;)
				{
					IL_37:
					documentObject = A_0.Clone();
					Table table = documentObject as Table;
					Table table2 = A_0 as Table;
					int num = 0;
					int num2;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_134:
						num2 = 6;
						break;
					default:
						if (false)
						{
						}
						num2 = 5;
						break;
					}
					int num3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_84;
						case 1:
							return documentObject;
						case 2:
							goto IL_D0;
						case 3:
							if (num >= table2.Rows.Count)
							{
								num2 = 1;
								continue;
							}
							num3 = 0;
							num2 = 0;
							continue;
						case 4:
							if (num3 >= table2.Rows[num].Cells.Count)
							{
								num2 = 7;
								continue;
							}
							goto IL_F8;
						case 5:
							goto IL_D0;
						case 6:
							if (true)
							{
							}
							goto IL_84;
						case 7:
							num++;
							num2 = 2;
							continue;
						}
						goto IL_37;
						IL_84:
						num2 = 4;
						continue;
						IL_D0:
						num2 = 3;
					}
					IL_F8:
					this.ᜀ(table2.Rows[num].Cells[num3], table.Rows[num].Cells[num3]);
					num3++;
					goto IL_134;
				}
				return documentObject;
			}
			}
		}

		// Token: 0x06003C4E RID: 15438 RVA: 0x003818B8 File Offset: 0x003808B8
		private new void ᜀ(Body A_0, Body A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					A_1.Items.InnerList.Clear();
					int num = 0;
					int num2 = 10;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							DocumentObject documentObject;
							if (documentObject != null)
							{
								num2 = 4;
								continue;
							}
							goto IL_E2;
						}
						case 1:
							return;
						case 2:
							goto IL_15D;
						case 3:
							goto IL_E2;
						case 4:
						{
							DocumentObject documentObject;
							num = A_0.Items.IndexOf(documentObject);
							this.\u1719.Clear();
							this.\u1718 = false;
							documentObject = null;
							if (true)
							{
							}
							num2 = 3;
							continue;
						}
						case 5:
						{
							if (num >= A_0.Items.Count)
							{
								num2 = 1;
								continue;
							}
							DocumentObject documentObject = null;
							num2 = 9;
							continue;
						}
						case 6:
							try
							{
								num2 = 0;
								for (;;)
								{
									switch (num2)
									{
									case 1:
										goto IL_1BC;
									case 2:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_1BC;
										default:
											goto IL_20F;
										}
										break;
									case 4:
										num2 = 2;
										continue;
									}
									IL_1B3:
									num2 = 1;
									continue;
									goto IL_1B3;
									IL_1BC:
									List<DocumentObject>.Enumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num2 = 4;
									}
									else
									{
										DocumentObject entity = enumerator.Current;
										A_1.Items.Add(entity);
										num2 = 3;
									}
								}
								IL_20F:
								if (false)
								{
								}
								goto IL_96;
							}
							finally
							{
								List<DocumentObject>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
							return;
							IL_96:
							num2 = 0;
							continue;
						case 7:
							goto IL_E2;
						case 8:
						{
							DocumentObject documentObject;
							List<DocumentObject> list = this.ᜀ(A_0.Items[num], null, ref documentObject);
							List<DocumentObject>.Enumerator enumerator = list.GetEnumerator();
							num2 = 6;
							continue;
						}
						case 9:
							if (A_0.Items[num] is Paragraph)
							{
								num2 = 8;
								continue;
							}
							A_1.Items.Add(this.ᜀ(A_0.Items[num]));
							num2 = 7;
							continue;
						case 10:
							goto IL_15D;
						}
						break;
						IL_E2:
						num++;
						num2 = 2;
						continue;
						IL_15D:
						num2 = 5;
					}
				}
				return;
			}
		}

		// Token: 0x06003C4F RID: 15439 RVA: 0x00381B00 File Offset: 0x00380B00
		private new DocumentObject ᜁ(DocumentObject A_0, string A_1, ref DocumentObject A_2)
		{
			switch (0)
			{
			default:
			{
				DocumentObject documentObject;
				for (;;)
				{
					IL_AE:
					documentObject = A_0.Clone();
					for (;;)
					{
						IL_B5:
						int num = 29;
						for (;;)
						{
							int num2;
							int num3;
							switch (num)
							{
							case 0:
								if (num2 >= (A_0 as Field).Range.ᜁ().Count - 1)
								{
									num = 13;
									continue;
								}
								num = 22;
								continue;
							case 1:
							{
								DocumentObject documentObject2;
								if (documentObject2 == null)
								{
									num = 14;
									continue;
								}
								Paragraph paragraph;
								paragraph.Items.Add(documentObject2.Clone());
								documentObject2 = ((A_0 as Field).Separator.NextSibling as DocumentObject);
								num = 16;
								continue;
							}
							case 2:
								if ((A_0 as Field).Range.ᜁ().Contains((A_0 as Field).Separator))
								{
									num = 35;
									continue;
								}
								num = 27;
								continue;
							case 3:
								if (num2 != (A_0 as Field).Range.ᜁ().Count - 2)
								{
									num = 28;
									continue;
								}
								goto IL_152;
							case 4:
								if ((A_0 as Field).Type != FieldType.FieldHyperlink)
								{
									num = 23;
									continue;
								}
								goto IL_2A9;
							case 5:
								num = 31;
								continue;
							case 6:
								goto IL_3F7;
							case 7:
							{
								Paragraph paragraph = ((A_0 as Field).Range.ᜁ()[num2] as DocumentObject).Clone() as Paragraph;
								paragraph.\u170D();
								DocumentObject documentObject2 = (A_0 as Field).Separator.NextSibling as DocumentObject;
								num = 9;
								continue;
							}
							case 8:
								num3 = (A_0 as Field).Range.ᜁ().IndexOf((A_0 as Field).Separator.OwnerParagraph);
								num = 6;
								continue;
							case 9:
								goto IL_FA;
							case 10:
								goto IL_40A;
							case 11:
								num = 26;
								continue;
							case 12:
								return documentObject;
							case 13:
								goto IL_152;
							case 14:
							{
								Paragraph paragraph;
								documentObject = paragraph;
								if (true)
								{
								}
								num = 33;
								continue;
							}
							case 15:
								if (((A_0 as Field).Range.ᜁ()[num2] as Paragraph).LastItem != (A_0 as Field).Separator)
								{
									num = 7;
									continue;
								}
								goto IL_55C;
							case 16:
								goto IL_FA;
							case 17:
								goto IL_40A;
							case 18:
								goto IL_3F7;
							case 19:
								num = 30;
								continue;
							case 20:
								num = 15;
								continue;
							case 21:
								(documentObject as TextRange).Text = A_1;
								num = 25;
								continue;
							case 22:
								if ((A_0 as Field).Range.ᜁ()[num2] == (A_0 as Field).Separator.OwnerParagraph)
								{
									num = 20;
									continue;
								}
								documentObject = ((A_0 as Field).Range.ᜁ()[num2] as DocumentObject).Clone();
								num = 36;
								continue;
							case 23:
								num3 = 0;
								num = 2;
								continue;
							case 24:
								num = 4;
								continue;
							case 25:
								return documentObject;
							case 26:
								if (documentObject is TextRange)
								{
									num = 21;
									continue;
								}
								return documentObject;
							case 27:
								if ((A_0 as Field).Range.ᜁ().Contains((A_0 as Field).Separator.OwnerParagraph))
								{
									num = 8;
									continue;
								}
								goto IL_3F7;
							case 28:
								this.\u171A.Add(documentObject);
								num = 34;
								continue;
							case 29:
								if (A_0 is Field)
								{
									num = 5;
									continue;
								}
								goto IL_2A9;
							case 30:
								if ((A_0 as Field).End != null)
								{
									num = 24;
									continue;
								}
								goto IL_2A9;
							case 31:
								if ((A_0 as Field).Separator == null)
								{
									goto IL_2A9;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_B5;
								default:
									if (false)
									{
									}
									num = 19;
									continue;
								}
								break;
							case 32:
								if (!string.IsNullOrEmpty(A_1))
								{
									num = 11;
									continue;
								}
								return documentObject;
							case 33:
								goto IL_1C5;
							case 34:
								goto IL_55C;
							case 35:
								num3 = (A_0 as Field).Range.ᜁ().IndexOf((A_0 as Field).Separator) + 1;
								num = 18;
								continue;
							case 36:
								goto IL_1C5;
							}
							goto IL_AE;
							IL_FA:
							num = 1;
							continue;
							IL_152:
							A_2 = ((A_0 as Field).Range.ᜁ()[(A_0 as Field).Range.ᜁ().Count - 1] as DocumentObject);
							num = 12;
							continue;
							IL_1C5:
							num = 3;
							continue;
							IL_2A9:
							num = 32;
							continue;
							IL_3F7:
							num2 = num3;
							num = 10;
							continue;
							IL_40A:
							num = 0;
							continue;
							IL_55C:
							num2++;
							num = 17;
						}
					}
				}
				return documentObject;
			}
			}
		}

		// Token: 0x06003C50 RID: 15440 RVA: 0x0038208C File Offset: 0x0038108C
		private new List<DocumentObject> ᜀ(DocumentObject A_0, string A_1, ref DocumentObject A_2)
		{
			int a_ = 6;
			switch (0)
			{
			default:
			{
				List<DocumentObject> list;
				Paragraph paragraph;
				for (;;)
				{
					list = new List<DocumentObject>();
					paragraph = (A_0.Clone() as Paragraph);
					paragraph.\u170D();
					int num = 20;
					for (;;)
					{
						DocumentObject documentObject;
						int num2;
						string text;
						string text2;
						int num3;
						switch (num)
						{
						case 0:
						{
							bool flag;
							if (!flag)
							{
								num = 37;
								continue;
							}
							goto IL_5DF;
						}
						case 1:
						{
							ParagraphBase paragraphBase;
							bool flag = this.ᜀ(paragraphBase, documentObject, paragraph, ref list);
							A_2 = ((paragraphBase as Field).Range.ᜁ()[(paragraphBase as Field).Range.ᜁ().Count - 1] as DocumentObject);
							num = 30;
							continue;
						}
						case 2:
						{
							ParagraphBase paragraphBase;
							if ((paragraphBase as Field).End != null)
							{
								num = 9;
								continue;
							}
							goto IL_434;
						}
						case 3:
							num = 14;
							continue;
						case 4:
							goto IL_300;
						case 5:
							goto IL_324;
						case 6:
							if (A_1 != null)
							{
								num = 24;
								continue;
							}
							goto IL_11A;
						case 7:
							num = 2;
							continue;
						case 8:
							num = 18;
							continue;
						case 9:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4EF;
							default:
								if (false)
								{
								}
								num = 36;
								continue;
							}
							break;
						case 10:
						{
							ParagraphBase paragraphBase;
							if (paragraphBase is Field)
							{
								num = 3;
								continue;
							}
							goto IL_434;
						}
						case 11:
							num2 = (A_0 as Paragraph).Items.IndexOf(A_2);
							this.\u1719.Clear();
							this.\u1718 = false;
							A_2 = null;
							num = 32;
							continue;
						case 12:
							if (A_1.EndsWith(ClipboardData.b("慫", a_)))
							{
								num = 19;
								continue;
							}
							goto IL_513;
						case 13:
							if (text != null)
							{
								num = 21;
								continue;
							}
							goto IL_300;
						case 14:
						{
							ParagraphBase paragraphBase;
							if ((paragraphBase as Field).Separator != null)
							{
								num = 7;
								continue;
							}
							goto IL_434;
						}
						case 15:
							text2 = text;
							num = 29;
							continue;
						case 16:
							text = text.Substring(text2.Length);
							num = 28;
							continue;
						case 17:
							if (text2.Contains(A_1))
							{
								num = 44;
								continue;
							}
							goto IL_14C;
						case 18:
							if (!text.Contains(text2))
							{
								num = 15;
								continue;
							}
							goto IL_5C1;
						case 19:
							A_1 = A_1.Remove(A_1.Length - 1);
							num = 39;
							continue;
						case 20:
							if (A_1 != null)
							{
								num = 46;
								continue;
							}
							goto IL_513;
						case 21:
							if (true)
							{
							}
							num = 35;
							continue;
						case 22:
						{
							if (num2 >= (A_0 as Paragraph).Items.Count)
							{
								num = 26;
								continue;
							}
							ParagraphBase paragraphBase = (A_0 as Paragraph).Items[num2];
							documentObject = paragraphBase.Clone();
							bool u = this.\u1717;
							this.\u1717 = false;
							text2 = this.ᜁ((A_0 as Paragraph).Items[num2]);
							this.\u1717 = u;
							bool flag = false;
							num = 10;
							continue;
						}
						case 23:
							if (documentObject is TextRange)
							{
								num = 43;
								continue;
							}
							goto IL_11A;
						case 24:
							num = 23;
							continue;
						case 25:
							if (!string.IsNullOrEmpty(text))
							{
								num = 16;
								continue;
							}
							goto IL_490;
						case 26:
							goto IL_353;
						case 27:
							if (num2 == num3)
							{
								num = 33;
								continue;
							}
							goto IL_14C;
						case 28:
							goto IL_490;
						case 29:
							goto IL_5C1;
						case 30:
							goto IL_11A;
						case 31:
							goto IL_324;
						case 32:
							goto IL_563;
						case 33:
							num = 17;
							continue;
						case 34:
							if (A_2 != null)
							{
								num = 11;
								continue;
							}
							goto IL_563;
						case 35:
							if (!(text == string.Empty))
							{
								num = 4;
								continue;
							}
							goto IL_5DF;
						case 36:
						{
							ParagraphBase paragraphBase;
							if ((paragraphBase as Field).Type != FieldType.FieldHyperlink)
							{
								num = 1;
								continue;
							}
							goto IL_434;
						}
						case 37:
							paragraph.Items.Add(documentObject);
							num = 13;
							continue;
						case 38:
							goto IL_11A;
						case 39:
							goto IL_513;
						case 40:
							text2 = A_1;
							num = 42;
							continue;
						case 41:
							if (text2.Contains(text))
							{
								num = 8;
								continue;
							}
							goto IL_5C1;
						case 42:
							goto IL_14C;
						case 43:
							num = 27;
							continue;
						case 44:
							num = 45;
							continue;
						case 45:
							if (!text2.StartsWith(A_1))
							{
								goto IL_4EF;
							}
							goto IL_14C;
						case 46:
							num = 12;
							continue;
						}
						break;
						IL_11A:
						num = 25;
						continue;
						IL_14C:
						num = 41;
						continue;
						IL_300:
						num = 34;
						continue;
						IL_324:
						num = 22;
						continue;
						IL_434:
						num = 6;
						continue;
						IL_490:
						num = 0;
						continue;
						IL_4EF:
						num = 40;
						continue;
						IL_513:
						text2 = string.Empty;
						text = A_1;
						num3 = this.ᜀ(A_0, A_1);
						num2 = num3;
						num = 5;
						continue;
						IL_563:
						num2++;
						num = 31;
						continue;
						IL_5C1:
						(documentObject as TextRange).Text = text2;
						num = 38;
					}
				}
				IL_353:
				IL_5DF:
				list.Add(paragraph);
				return list;
			}
			}
		}

		// Token: 0x06003C51 RID: 15441 RVA: 0x00382680 File Offset: 0x00381680
		private new bool ᜀ(ParagraphBase A_0, DocumentObject A_1, Paragraph A_2, ref List<DocumentObject> A_3)
		{
			switch (0)
			{
			default:
			{
				bool result;
				for (;;)
				{
					result = false;
					int num = 0;
					int num2 = 18;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							A_3.Add(A_2);
							A_3.Add(A_1);
							num2 = 23;
							continue;
						case 1:
							if (A_1 is Body)
							{
								num2 = 7;
								continue;
							}
							goto IL_D1;
						case 2:
							if ((A_0 as Field).Range.ᜁ()[num3] == (A_0 as Field).Separator.OwnerParagraph)
							{
								num2 = 14;
								continue;
							}
							A_1 = ((A_0 as Field).Range.ᜁ()[num3] as DocumentObject).Clone();
							num2 = 16;
							continue;
						case 3:
							if (true)
							{
							}
							goto IL_D1;
						case 4:
							num = (A_0 as Field).Range.ᜁ().IndexOf((A_0 as Field).Separator) + 1;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_366;
							default:
								if (false)
								{
								}
								num2 = 22;
								continue;
							}
							break;
						case 5:
							if ((A_0 as Field).Range.ᜁ().Contains((A_0 as Field).Separator.OwnerParagraph))
							{
								num2 = 8;
								continue;
							}
							goto IL_13E;
						case 6:
							goto IL_446;
						case 7:
							result = true;
							num2 = 3;
							continue;
						case 8:
							num = (A_0 as Field).Range.ᜁ().IndexOf((A_0 as Field).Separator.OwnerParagraph);
							num2 = 26;
							continue;
						case 9:
							if (num3 != (A_0 as Field).Range.ᜁ().Count - 2)
							{
								num2 = 10;
								continue;
							}
							return result;
						case 10:
							num2 = 27;
							continue;
						case 11:
							goto IL_1C0;
						case 12:
							if (((A_0 as Field).Range.ᜁ()[num3] as Paragraph).LastItem != (A_0 as Field).Separator)
							{
								goto IL_366;
							}
							goto IL_1C0;
						case 13:
							goto IL_41E;
						case 14:
							num2 = 12;
							continue;
						case 15:
						{
							Paragraph paragraph = ((A_0 as Field).Range.ᜁ()[num3] as DocumentObject).Clone() as Paragraph;
							A_2.\u170D();
							DocumentObject documentObject = (A_0 as Field).Separator.NextSibling as DocumentObject;
							num2 = 20;
							continue;
						}
						case 16:
							goto IL_41E;
						case 17:
							return result;
						case 18:
							if ((A_0 as Field).Range.ᜁ().Contains((A_0 as Field).Separator))
							{
								num2 = 4;
								continue;
							}
							num2 = 5;
							continue;
						case 19:
							if (num3 >= (A_0 as Field).Range.ᜁ().Count - 1)
							{
								num2 = 17;
								continue;
							}
							num2 = 2;
							continue;
						case 20:
							goto IL_446;
						case 21:
							goto IL_24C;
						case 22:
							goto IL_13E;
						case 23:
							goto IL_1C0;
						case 24:
						{
							Paragraph paragraph;
							A_1 = paragraph;
							num2 = 13;
							continue;
						}
						case 25:
						{
							DocumentObject documentObject;
							if (documentObject == null)
							{
								num2 = 24;
								continue;
							}
							Paragraph paragraph;
							paragraph.Items.Add(documentObject.Clone());
							documentObject = ((A_0 as Field).Separator.NextSibling as DocumentObject);
							num2 = 6;
							continue;
						}
						case 26:
							goto IL_13E;
						case 27:
							if (A_1 is Body)
							{
								num2 = 0;
								continue;
							}
							A_2.Items.Add(A_1);
							num2 = 11;
							continue;
						case 28:
							goto IL_24C;
						}
						break;
						IL_D1:
						num2 = 9;
						continue;
						IL_13E:
						num3 = num;
						num2 = 28;
						continue;
						IL_1C0:
						num3++;
						num2 = 21;
						continue;
						IL_24C:
						num2 = 19;
						continue;
						IL_366:
						num2 = 15;
						continue;
						IL_41E:
						num2 = 1;
						continue;
						IL_446:
						num2 = 25;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x00382B04 File Offset: 0x00381B04
		private new int ᜀ(DocumentObject A_0, string A_1)
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				int result;
				for (;;)
				{
					result = 0;
					string text = string.Empty;
					if (true)
					{
					}
					int num = 2;
					for (;;)
					{
						int num2;
						string text2;
						int num3;
						switch (num)
						{
						case 0:
							if (num2 >= (A_0 as Paragraph).Items.Count)
							{
								num = 13;
								continue;
							}
							text += this.ᜁ((A_0 as Paragraph).Items[num2]);
							num = 11;
							continue;
						case 1:
							result = num2 + 1;
							num = 16;
							continue;
						case 2:
							if (A_1 != null)
							{
								num = 10;
								continue;
							}
							return result;
						case 3:
							goto IL_196;
						case 4:
							goto IL_196;
						case 5:
							goto IL_9B;
						case 6:
							goto IL_1D8;
						case 7:
							if (text2.EndsWith(ClipboardData.b("祳", a_)))
							{
								num = 9;
								continue;
							}
							goto IL_1D8;
						case 8:
							return result;
						case 9:
							text2 = text2.Remove(text2.Length - 1);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9B;
							default:
								if (false)
								{
								}
								num = 6;
								continue;
							}
							break;
						case 10:
						{
							bool u = this.\u1717;
							this.\u1717 = false;
							text2 = this.ᜃ(A_0);
							this.\u1717 = u;
							num = 7;
							continue;
						}
						case 11:
							if (num3 == text.Length)
							{
								num = 1;
								continue;
							}
							num = 12;
							continue;
						case 12:
							if (num3 < text.Length)
							{
								num = 5;
								continue;
							}
							num2++;
							num = 4;
							continue;
						case 13:
							return result;
						case 14:
							num2 = 0;
							num = 3;
							continue;
						case 15:
							if (num3 > 0)
							{
								num = 14;
								continue;
							}
							return result;
						case 16:
							return result;
						}
						break;
						IL_9B:
						A_1 = text.Substring(num3);
						result = num2;
						num = 8;
						continue;
						IL_196:
						num = 0;
						continue;
						IL_1D8:
						num3 = text2.IndexOf(A_1);
						num = 15;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06003C53 RID: 15443 RVA: 0x00382D5C File Offset: 0x00381D5C
		private new int ᜀ(int A_0, ref string A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					string text = this.Code;
					this.\u1717 = false;
					this.\u1719.Clear();
					this.\u1718 = false;
					string str = string.Empty;
					int num = 0;
					int num2 = 10;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_180;
						case 1:
							if (this.\u1718)
							{
								num2 = 2;
								continue;
							}
							A_1 = text.Substring(A_0);
							A_0 = num;
							num2 = 3;
							continue;
						case 2:
						{
							if (true)
							{
							}
							Field field = this.\u1719.Pop();
							num2 = 19;
							continue;
						}
						case 3:
							goto IL_12F;
						case 4:
							num2 = 1;
							continue;
						case 5:
							A_0 = num + 1;
							num2 = 20;
							continue;
						case 6:
							goto IL_1B3;
						case 7:
						{
							DocumentObject documentObject;
							if (documentObject is ParagraphBase)
							{
								num2 = 16;
								continue;
							}
							str = this.ᜃ(documentObject);
							num2 = 13;
							continue;
						}
						case 8:
						{
							if (num >= this.Range.ᜁ().Count)
							{
								num2 = 6;
								continue;
							}
							DocumentObject documentObject = this.Range.ᜁ()[num] as DocumentObject;
							num2 = 7;
							continue;
						}
						case 9:
						{
							Field field;
							A_0 = this.Range.ᜁ().IndexOf(field.Separator.OwnerParagraph) + 1;
							num2 = 17;
							continue;
						}
						case 10:
							goto IL_185;
						case 11:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_185;
							default:
								if (false)
								{
								}
								num2 = 15;
								continue;
							}
							break;
						case 12:
							if (!this.\u1717)
							{
								num2 = 11;
								continue;
							}
							goto IL_31F;
						case 13:
							goto IL_1E1;
						case 14:
							goto IL_185;
						case 15:
							if (A_0 == text.Length)
							{
								num2 = 5;
								continue;
							}
							num2 = 21;
							continue;
						case 16:
						{
							DocumentObject documentObject;
							str = this.ᜁ(documentObject);
							num2 = 18;
							continue;
						}
						case 17:
							goto IL_D4;
						case 18:
							goto IL_1E1;
						case 19:
						{
							Field field;
							if (field.Separator.ឯ() == field.Separator.OwnerParagraph.Items.Count - 1)
							{
								num2 = 9;
								continue;
							}
							A_1 = text.Substring(A_0);
							A_0 = this.Range.ᜁ().IndexOf(field.Separator.OwnerParagraph);
							num2 = 0;
							continue;
						}
						case 20:
							goto IL_31A;
						case 21:
							if (A_0 < text.Length)
							{
								num2 = 4;
								continue;
							}
							num++;
							num2 = 14;
							continue;
						}
						break;
						IL_185:
						num2 = 8;
						continue;
						IL_1E1:
						text += str;
						num2 = 12;
					}
				}
				IL_D4:
				IL_12F:
				IL_180:
				IL_1B3:
				IL_31A:
				IL_31F:
				this.\u1717 = false;
				this.\u1719.Clear();
				this.\u1718 = false;
				return A_0;
			}
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x003830A4 File Offset: 0x003820A4
		private new void ᜀ()
		{
			for (;;)
			{
				int num = this.Range.Count - 1;
				int num2 = 20;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (!this.\u1717)
						{
							num2 = 22;
							continue;
						}
						goto IL_2DD;
					case 1:
					{
						DocumentObject documentObject;
						this.Range.ᜁ().Remove(documentObject);
						Body ownerTextBody;
						ownerTextBody.Items.Remove(documentObject);
						num2 = 15;
						continue;
					}
					case 2:
					{
						DocumentObject documentObject;
						if (documentObject != this.Separator)
						{
							num2 = 14;
							continue;
						}
						goto IL_31C;
					}
					case 3:
						goto IL_2DD;
					case 4:
					{
						DocumentObject documentObject;
						Body ownerTextBody = (documentObject as Paragraph).OwnerTextBody;
						num2 = 0;
						continue;
					}
					case 5:
						goto IL_192;
					case 6:
					{
						if (num < 0)
						{
							num2 = 18;
							continue;
						}
						DocumentObject documentObject = this.Range.ᜁ()[num] as DocumentObject;
						num2 = 11;
						continue;
					}
					case 7:
					{
						DocumentObject documentObject;
						if (documentObject is Paragraph)
						{
							num2 = 4;
							continue;
						}
						num2 = 19;
						continue;
					}
					case 8:
					{
						DocumentObject documentObject;
						if (documentObject is ParagraphBase)
						{
							num2 = 12;
							continue;
						}
						num2 = 7;
						continue;
					}
					case 9:
						IL_304:
						if (!this.\u1717)
						{
							num2 = 10;
							continue;
						}
						goto IL_31C;
					case 10:
						num2 = 13;
						continue;
					case 11:
					{
						DocumentObject documentObject;
						if (documentObject != this.End)
						{
							num2 = 16;
							continue;
						}
						goto IL_20A;
					}
					case 12:
					{
						DocumentObject documentObject;
						this.Range.ᜁ().Remove(documentObject);
						base.OwnerParagraph.Items.Remove(documentObject);
						num2 = 24;
						continue;
					}
					case 13:
					{
						DocumentObject documentObject;
						if ((documentObject as Paragraph).Items.Count == 0)
						{
							num2 = 23;
							continue;
						}
						goto IL_20A;
					}
					case 14:
						num2 = 8;
						continue;
					case 15:
						goto IL_20A;
					case 16:
						num2 = 2;
						continue;
					case 17:
						if (!this.\u1717)
						{
							num2 = 1;
							continue;
						}
						goto IL_20A;
					case 18:
						goto IL_1AF;
					case 19:
					{
						DocumentObject documentObject;
						if (documentObject is Table)
						{
							num2 = 25;
							continue;
						}
						goto IL_20A;
					}
					case 20:
						goto IL_192;
					case 21:
						goto IL_20A;
					case 22:
					{
						if (true)
						{
						}
						DocumentObject documentObject;
						this.ᜀ(documentObject as Paragraph);
						num2 = 3;
						continue;
					}
					case 23:
					{
						DocumentObject documentObject;
						this.Range.ᜁ().Remove(documentObject);
						Body ownerTextBody;
						ownerTextBody.Items.Remove(documentObject);
						num2 = 21;
						continue;
					}
					case 24:
						goto IL_20A;
					case 25:
					{
						DocumentObject documentObject;
						Body ownerTextBody = (documentObject as Table).OwnerTextBody;
						num2 = 17;
						continue;
					}
					}
					break;
					IL_192:
					num2 = 6;
					continue;
					IL_20A:
					num--;
					num2 = 5;
					continue;
					IL_2DD:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_304;
					default:
						if (false)
						{
						}
						num2 = 9;
						break;
					}
				}
			}
			IL_1AF:
			IL_31C:
			this.\u1715 = false;
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x003833D4 File Offset: 0x003823D4
		private new void ᜀ(Paragraph A_0)
		{
			for (;;)
			{
				int num = A_0.Items.Count - 1;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (this.End.OwnerParagraph == A_0)
						{
							num2 = 15;
							continue;
						}
						goto IL_14E;
					case 1:
						goto IL_BB;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_14E;
						default:
							goto IL_1CC;
						}
						break;
					case 3:
					{
						ParagraphBase paragraphBase;
						A_0.Items.Remove(paragraphBase);
						num2 = 10;
						continue;
					}
					case 4:
						goto IL_12F;
					case 5:
						num = A_0.Items.IndexOf(this.End);
						num2 = 1;
						continue;
					case 6:
					{
						if (num < 0)
						{
							num2 = 12;
							continue;
						}
						ParagraphBase paragraphBase = A_0.Items[num];
						num2 = 0;
						continue;
					}
					case 7:
						if (!this.\u1717)
						{
							num2 = 3;
							continue;
						}
						goto IL_BB;
					case 8:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase != this.End)
						{
							num2 = 9;
							continue;
						}
						goto IL_BB;
					}
					case 9:
						num2 = 14;
						continue;
					case 10:
						goto IL_BB;
					case 11:
						goto IL_12F;
					case 12:
						return;
					case 13:
						if (num > A_0.Items.IndexOf(this.End))
						{
							num2 = 5;
							continue;
						}
						goto IL_14E;
					case 14:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase == this.Separator)
						{
							num2 = 2;
							continue;
						}
						num2 = 7;
						continue;
					}
					case 15:
						if (true)
						{
						}
						num2 = 13;
						continue;
					}
					break;
					IL_BB:
					num--;
					num2 = 11;
					continue;
					IL_12F:
					num2 = 6;
					continue;
					IL_14E:
					num2 = 8;
				}
			}
			return;
			IL_1CC:
			if (false)
			{
			}
			this.\u1717 = true;
		}

		// Token: 0x06003C56 RID: 15446 RVA: 0x003835B8 File Offset: 0x003825B8
		private new void ᜀ(Paragraph A_0, bool A_1, ref string A_2)
		{
			for (;;)
			{
				IL_14:
				bool u = this.\u1717;
				this.\u1717 = false;
				A_2 = this.ᜃ(A_0) + A_2;
				this.\u1717 = u;
				for (;;)
				{
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							A_2 = A_2.TrimEnd(new char[]
							{
								'\r'
							});
							num = 1;
							continue;
						case 1:
							goto IL_7E;
						case 2:
							if (A_1)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							return;
						}
						goto IL_14;
					}
					IL_7E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_94;
					}
				}
			}
			IL_94:
			if (false)
			{
			}
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x00383664 File Offset: 0x00382664
		private new string ᜀ(ParagraphBase A_0)
		{
			string result;
			for (;;)
			{
				result = string.Empty;
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0 is TextRange)
						{
							num = 7;
							continue;
						}
						return result;
					case 1:
						if (A_0 != this)
						{
							num = 9;
							continue;
						}
						goto IL_63;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_18F;
						}
						if (false)
						{
						}
						num = 4;
						continue;
					case 3:
						return result;
					case 4:
						if ((A_0 as Field).Type == FieldType.FieldMergeField)
						{
							num = 13;
							continue;
						}
						num = 12;
						continue;
					case 5:
						num = 1;
						continue;
					case 6:
						if (true)
						{
						}
						goto IL_63;
					case 7:
						result = (A_0 as TextRange).Text;
						num = 11;
						continue;
					case 8:
						return result;
					case 9:
						(A_0 as Field).ᜎ();
						num = 6;
						continue;
					case 10:
						if (A_0 is Field)
						{
							num = 2;
							continue;
						}
						num = 0;
						continue;
					case 11:
						return result;
					case 12:
						if (!base.Document.\u1757.Contains(A_0 as Field))
						{
							goto IL_18F;
						}
						goto IL_63;
					case 13:
						result = (A_0 as TextRange).Text;
						num = 3;
						continue;
					}
					break;
					IL_63:
					result = (A_0 as Field).FieldResult;
					num = 8;
					continue;
					IL_18F:
					num = 5;
				}
			}
			return result;
		}

		// Token: 0x06003C58 RID: 15448 RVA: 0x00383810 File Offset: 0x00382810
		internal void \u1715(string A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.FieldResult = A_0;
					int num = 10;
					for (;;)
					{
						TextRange textRange;
						int num3;
						string[] array;
						switch (num)
						{
						case 0:
						{
							int num2;
							string a_ = A_0.Substring(num2);
							A_0 = A_0.Substring(0, num2);
							base.OwnerParagraph.Items.Insert(this.End.ឯ(), this.ᜀ(A_0));
							base.OwnerParagraph.Items.Insert(this.End.ឯ(), this.ᜀ(a_));
							num = 11;
							continue;
						}
						case 1:
							if (textRange.CharacterFormat.Sprms.ᜄ(2133))
							{
								num = 16;
								continue;
							}
							goto IL_1D8;
						case 2:
							if (num3 >= array.Length)
							{
								num = 9;
								continue;
							}
							textRange = new TextRange(base.Document);
							num = 7;
							continue;
						case 3:
							goto IL_462;
						case 4:
							goto IL_EA;
						case 5:
							if (base.OwnerParagraph == this.End.OwnerParagraph)
							{
								num = 20;
								continue;
							}
							A_0 = A_0.Replace(spr\u20E8.ᜉ, spr\u20E8.\u171F);
							A_0 = A_0.Replace(spr\u20E8.ᜏ, '\r');
							array = A_0.Split(new char[]
							{
								'\r'
							});
							num3 = 0;
							num = 24;
							continue;
						case 6:
							if (this.End == null)
							{
								num = 14;
								continue;
							}
							this.ᜁ();
							this.ᜀ();
							num = 5;
							continue;
						case 7:
							if (this.\u171D != null)
							{
								num = 13;
								continue;
							}
							textRange.CharacterFormat.ImportContainer(base.CharacterFormat);
							textRange.CharacterFormat.ᜃ(base.CharacterFormat);
							num = 19;
							continue;
						case 8:
							goto IL_3F3;
						case 9:
							goto IL_361;
						case 10:
							if (base.OwnerParagraph != null)
							{
								num = 29;
								continue;
							}
							return;
						case 11:
							goto IL_2CD;
						case 12:
							goto IL_340;
						case 13:
							textRange.CharacterFormat.ImportContainer(this.\u171D);
							textRange.CharacterFormat.ᜃ(this.\u171D);
							this.\u171D = null;
							num = 4;
							continue;
						case 14:
							goto IL_3BB;
						case 15:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_462;
							default:
								if (false)
								{
								}
								this.Separator.OwnerParagraph.Items.Add(textRange);
								num = 22;
								continue;
							}
							break;
						case 16:
							textRange.CharacterFormat.Sprms.ᜆ(2133);
							num = 23;
							continue;
						case 17:
							goto IL_221;
						case 18:
							goto IL_221;
						case 19:
							goto IL_EA;
						case 20:
						{
							A_0 = A_0.Replace(spr\u20E8.ᜉ, spr\u20E8.\u171F);
							A_0 = A_0.Replace(spr\u20E8.ᜏ, '\r');
							int num2 = A_0.IndexOf('\r');
							num = 21;
							continue;
						}
						case 21:
						{
							int num2;
							if (num2 != -1)
							{
								num = 0;
								continue;
							}
							base.OwnerParagraph.Items.Insert(this.End.ឯ(), this.ᜀ(A_0));
							if (true)
							{
							}
							num = 8;
							continue;
						}
						case 22:
							goto IL_221;
						case 23:
							goto IL_1D8;
						case 24:
							goto IL_340;
						case 25:
							if (textRange.CharacterFormat.Sprms != null)
							{
								num = 26;
								continue;
							}
							goto IL_1D8;
						case 26:
							num = 1;
							continue;
						case 27:
							if (num3 == 0)
							{
								num = 15;
								continue;
							}
							num = 28;
							continue;
						case 28:
						{
							if (num3 == array.Length - 1)
							{
								num = 3;
								continue;
							}
							Paragraph paragraph = this.End.OwnerParagraph.Clone() as Paragraph;
							paragraph.\u170D();
							int index = this.End.OwnerParagraph.ឯ();
							this.End.OwnerParagraph.OwnerTextBody.Items.Insert(index, paragraph);
							paragraph.Items.Add(textRange);
							num = 18;
							continue;
						}
						case 29:
							num = 6;
							continue;
						}
						break;
						IL_EA:
						num = 25;
						continue;
						IL_1D8:
						textRange.Text = array[num3];
						num = 27;
						continue;
						IL_221:
						num3++;
						num = 12;
						continue;
						IL_340:
						num = 2;
						continue;
						IL_462:
						this.End.OwnerParagraph.Items.Insert(this.End.ឯ(), textRange);
						num = 17;
					}
				}
				IL_2CD:
				IL_361:
				goto IL_511;
				IL_3BB:
				return;
				IL_3F3:
				IL_511:
				this.\u1715 = false;
				return;
			}
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x00383D38 File Offset: 0x00382D38
		private new TextRange ᜀ(string A_0)
		{
			TextRange textRange;
			for (;;)
			{
				textRange = new TextRange(base.Document);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_81;
					case 1:
						if (this.\u171D != null)
						{
							num = 3;
							continue;
						}
						goto IL_12F;
					case 2:
						goto IL_12D;
					case 3:
						textRange.CharacterFormat.ImportContainer(this.\u171D);
						textRange.CharacterFormat.ᜃ(this.\u171D);
						this.\u171D = null;
						num = 8;
						continue;
					case 4:
						if (textRange.CharacterFormat.Sprms != null)
						{
							num = 5;
							continue;
						}
						goto IL_161;
					case 5:
						num = 7;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_12F;
						}
						if (false)
						{
						}
						textRange.CharacterFormat.Sprms.ᜆ(2133);
						num = 2;
						continue;
					case 7:
						if (textRange.CharacterFormat.Sprms.ᜄ(2133))
						{
							num = 6;
							continue;
						}
						goto IL_161;
					case 8:
						if (true)
						{
						}
						goto IL_81;
					}
					break;
					IL_81:
					num = 4;
					continue;
					IL_12F:
					textRange.CharacterFormat.ImportContainer(base.CharacterFormat);
					textRange.CharacterFormat.ᜃ(base.CharacterFormat);
					num = 0;
				}
			}
			IL_12D:
			IL_161:
			textRange.Text = A_0;
			return textRange;
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x00383EB0 File Offset: 0x00382EB0
		SizeF spr\u2297.Measure(spr\u19E0 dc)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				SizeF result;
				for (;;)
				{
					result = SizeF.Empty;
					string text = "";
					CharacterFormat characterFormat = null;
					FieldType type = this.Type;
					int num = 132;
					for (;;)
					{
						CharacterFormat characterFormat2;
						DocumentObject documentObject;
						DocumentObject documentObject2;
						string text2;
						int num5;
						int num6;
						int num7;
						IEnumerator enumerator2;
						DocumentObject documentObject4;
						int num8;
						int num9;
						int num10;
						switch (num)
						{
						case 0:
							num = 156;
							continue;
						case 1:
							goto IL_4D0;
						case 2:
						{
							spr\u1AD2 spr_u1AD;
							int num2;
							TextRange textRange = spr_u1AD.ᜂ()[num2 + 2] as TextRange;
							num = 70;
							continue;
						}
						case 3:
						{
							IDocumentObject owner;
							if (!(owner is Section))
							{
								num = 45;
								continue;
							}
							num = 162;
							continue;
						}
						case 4:
						{
							Hyperlink hyperlink;
							if (hyperlink.Field.NextSibling is FieldMark)
							{
								num = 89;
								continue;
							}
							goto IL_412;
						}
						case 5:
						{
							spr\u1AD2 spr_u1AD2;
							int num3;
							TextRange textRange2 = spr_u1AD2.ᜂ()[num3 + 2] as TextRange;
							num = 26;
							continue;
						}
						case 6:
						{
							Hyperlink hyperlink;
							if ((hyperlink.Field.NextSibling as FieldMark).Type == FieldMarkType.FieldSeparator)
							{
								num = 28;
								continue;
							}
							goto IL_412;
						}
						case 7:
						{
							string a_2;
							result = dc.ᜀ(a_2, characterFormat2.Font, null, true);
							num = 12;
							continue;
						}
						case 8:
						{
							Hyperlink hyperlink;
							if (hyperlink.Field.NextSibling is sprẛ)
							{
								num = 95;
								continue;
							}
							num = 163;
							continue;
						}
						case 9:
							goto IL_1176;
						case 10:
							if ((base.NextSibling as FieldMark).Type != FieldMarkType.FieldSeparator)
							{
								num = 15;
								continue;
							}
							this.ᜀ.ᜁ(true);
							num = 157;
							continue;
						case 11:
							goto IL_EE8;
						case 12:
							return result;
						case 13:
							num = 130;
							continue;
						case 14:
							num = 137;
							continue;
						case 15:
							goto IL_17AD;
						case 16:
						{
							TextRange textRange3 = documentObject as TextRange;
							textRange3.ᜀ = new spr\u22A8();
							textRange3.ᜀ.ᜁ(true);
							num = 40;
							continue;
						}
						case 17:
						{
							IDocumentObject owner;
							if (owner is Section)
							{
								num = 66;
								continue;
							}
							owner = owner.Owner;
							num = 114;
							continue;
						}
						case 18:
							goto IL_1465;
						case 19:
							num = 146;
							continue;
						case 20:
							num = 120;
							continue;
						case 21:
							num = 88;
							continue;
						case 22:
							if (documentObject.NextSibling is FieldMark)
							{
								num = 138;
								continue;
							}
							goto IL_E5A;
						case 23:
							num = 121;
							continue;
						case 24:
							num = 3;
							continue;
						case 25:
							num = 111;
							continue;
						case 26:
						{
							TextRange textRange2;
							if (textRange2 != null)
							{
								num = 113;
								continue;
							}
							goto IL_A0A;
						}
						case 27:
						{
							if (characterFormat.IsSmallCaps)
							{
								num = 102;
								continue;
							}
							Hyperlink hyperlink;
							result = dc.ᜁ(hyperlink.TextToDisplay, characterFormat.Font, null);
							num = 179;
							continue;
						}
						case 28:
							num = 58;
							continue;
						case 29:
							goto IL_A0A;
						case 30:
							num = 149;
							continue;
						case 31:
							goto IL_1176;
						case 32:
							num = 64;
							continue;
						case 33:
							goto IL_6B2;
						case 34:
							if (base.NextSibling is FieldMark)
							{
								num = 183;
								continue;
							}
							goto IL_EE8;
						case 35:
							if (documentObject2 is TextRange)
							{
								num = 118;
								continue;
							}
							goto IL_E91;
						case 36:
						{
							int num4;
							if (num4 % 2 != 0)
							{
								num = 188;
								continue;
							}
							return result;
						}
						case 37:
							num = 187;
							continue;
						case 38:
							text2 = this.ᜀ(this as MergeField);
							goto IL_73F;
						case 39:
							goto IL_599;
						case 40:
							goto IL_BD4;
						case 41:
							if (base.NextSibling.NextSibling is TextRange)
							{
								num = 81;
								continue;
							}
							goto IL_A0A;
						case 42:
						{
							if (true)
							{
							}
							if (type != FieldType.FieldIf)
							{
								num = 94;
								continue;
							}
							num5 = -1;
							int num4 = 0;
							IEnumerator enumerator = this.Range.ᜁ().GetEnumerator();
							num = 142;
							continue;
						}
						case 43:
							goto IL_C2F;
						case 44:
							if (!(this.Value == string.Empty))
							{
								num = 84;
								continue;
							}
							num = 38;
							continue;
						case 45:
							goto IL_ED6;
						case 46:
						{
							TextRange textRange;
							base.CharacterFormat = textRange.CharacterFormat;
							num = 68;
							continue;
						}
						case 47:
							num6 = 1;
							goto IL_18DA;
						case 48:
						{
							Hyperlink hyperlink;
							characterFormat = (hyperlink.Field.NextSibling.NextSibling as TextRange).CharacterFormat;
							documentObject = (hyperlink.Field.NextSibling as DocumentObject);
							num = 170;
							continue;
						}
						case 49:
							if (!(documentObject.NextSibling is FieldMark))
							{
								num = 25;
								continue;
							}
							goto IL_1219;
						case 50:
						{
							Hyperlink hyperlink;
							if (hyperlink.PictureToDisplay.TextWrappingStyle != TextWrappingStyle.InFrontOfText)
							{
								num = 98;
								continue;
							}
							goto IL_353;
						}
						case 51:
							return result;
						case 52:
							return result;
						case 53:
							if (base.Owner is Paragraph)
							{
								num = 184;
								continue;
							}
							return result;
						case 54:
							return result;
						case 55:
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 5;
										continue;
									case 1:
									{
										IDocumentObject documentObject3;
										(documentObject3 as TextRange).Text = num7.ToString();
										num = 4;
										continue;
									}
									case 2:
									{
										if (!enumerator2.MoveNext())
										{
											num = 0;
											continue;
										}
										IDocumentObject documentObject3 = (IDocumentObject)enumerator2.Current;
										num = 6;
										continue;
									}
									case 5:
										goto IL_13C8;
									case 6:
									{
										IDocumentObject documentObject3;
										if (documentObject3.DocumentObjectType == DocumentObjectType.TextRange)
										{
											num = 1;
											continue;
										}
										break;
									}
									}
									IL_136F:
									num = 2;
									continue;
									goto IL_136F;
								}
								IL_13C8:
								return result;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator2 as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (disposable != null)
											{
												num = 2;
												continue;
											}
											goto IL_1431;
										case 1:
											goto IL_142F;
										case 2:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_1431;
											default:
												if (false)
												{
												}
												disposable.Dispose();
												num = 1;
												continue;
											}
											break;
										}
										break;
									}
								}
								IL_142F:
								IL_1431:;
							}
							goto IL_1432;
						case 56:
							num = 77;
							continue;
						case 57:
							num = 92;
							continue;
						case 58:
						{
							Hyperlink hyperlink;
							if (hyperlink.Field.NextSibling.NextSibling != null)
							{
								num = 37;
								continue;
							}
							goto IL_412;
						}
						case 59:
							text2 = this.Value;
							goto IL_73F;
						case 60:
							characterFormat = (base.NextSibling.NextSibling.NextSibling as TextRange).CharacterFormat;
							documentObject4 = (base.NextSibling.NextSibling as DocumentObject);
							num = 154;
							continue;
						case 61:
							goto IL_A0A;
						case 62:
							if (documentObject4 != null)
							{
								num = 185;
								continue;
							}
							return result;
						case 63:
						{
							DocumentObject documentObject5;
							if (documentObject5.DocumentObjectType == DocumentObjectType.FieldMark)
							{
								num = 148;
								continue;
							}
							goto IL_3B9;
						}
						case 64:
						{
							Hyperlink hyperlink;
							if (hyperlink.Field.NextSibling is sprẛ)
							{
								num = 57;
								continue;
							}
							goto IL_6B2;
						}
						case 65:
						{
							IDocumentObject owner;
							if (owner != null)
							{
								num = 165;
								continue;
							}
							goto IL_A82;
						}
						case 66:
							goto IL_A82;
						case 67:
							num = 90;
							continue;
						case 68:
							goto IL_A51;
						case 69:
						{
							Hyperlink hyperlink;
							if (!(hyperlink.Field.NextSibling.NextSibling.NextSibling is TextRange))
							{
								num = 33;
								continue;
							}
							goto IL_67D;
						}
						case 70:
						{
							TextRange textRange;
							if (textRange != null)
							{
								num = 46;
								continue;
							}
							goto IL_A51;
						}
						case 71:
						{
							spr\u1AD2 spr_u1AD2 = base.Owner as spr\u1AD2;
							int num3 = spr_u1AD2.ᜂ().IndexOf(this);
							num = 123;
							continue;
						}
						case 72:
							num = 73;
							continue;
						case 73:
							switch (type)
							{
							case FieldType.FieldDocVariable:
								num = 186;
								continue;
							case FieldType.FieldSection:
								return result;
							case FieldType.FieldSectionPages:
							{
								IDocumentObject owner = base.Owner;
								num = 128;
								continue;
							}
							default:
								num = 164;
								continue;
							}
							break;
						case 74:
							if (documentObject != null)
							{
								num = 83;
								continue;
							}
							goto IL_1219;
						case 75:
							num5 = num8;
							num = 119;
							continue;
						case 76:
							num = 115;
							continue;
						case 77:
							goto IL_ACC;
						case 78:
							return result;
						case 79:
						{
							int num4;
							if (num4 >= 0)
							{
								num = 21;
								continue;
							}
							goto IL_3B9;
						}
						case 80:
						{
							DocumentObject documentObject6;
							if (documentObject6 is TextRange)
							{
								num = 109;
								continue;
							}
							goto IL_FE0;
						}
						case 81:
							(base.NextSibling.NextSibling as TextRange).Text = "";
							num = 29;
							continue;
						case 82:
						{
							Hyperlink hyperlink;
							if (hyperlink != null)
							{
								num = 30;
								continue;
							}
							return result;
						}
						case 83:
							num = 22;
							continue;
						case 84:
							num = 59;
							continue;
						case 85:
						{
							if (characterFormat2.IsSmallCaps)
							{
								num = 7;
								continue;
							}
							string a_2;
							result = dc.ᜁ(a_2, characterFormat2.Font, null);
							num = 112;
							continue;
						}
						case 86:
						{
							Hyperlink hyperlink;
							if (hyperlink.Field.NextSibling.NextSibling.NextSibling != null)
							{
								num = 117;
								continue;
							}
							goto IL_6B2;
						}
						case 87:
							if (base.NextSibling is sprẛ)
							{
								num = 60;
								continue;
							}
							num = 34;
							continue;
						case 88:
						{
							int num4;
							if (num4 % 2 == 0)
							{
								num = 140;
								continue;
							}
							goto IL_3B9;
						}
						case 89:
							num = 6;
							continue;
						case 90:
						{
							if (type != FieldType.FieldPage)
							{
								num = 93;
								continue;
							}
							string a_2 = string.Empty;
							num = 104;
							continue;
						}
						case 91:
							num = 42;
							continue;
						case 92:
						{
							Hyperlink hyperlink;
							if ((hyperlink.Field.NextSibling.NextSibling as FieldMark).Type == FieldMarkType.FieldSeparator)
							{
								num = 136;
								continue;
							}
							goto IL_6B2;
						}
						case 93:
							num = 51;
							continue;
						case 94:
							num = 178;
							continue;
						case 95:
						{
							Hyperlink hyperlink;
							characterFormat = (hyperlink.Field.NextSibling.NextSibling.NextSibling as TextRange).CharacterFormat;
							documentObject = (hyperlink.Field.NextSibling.NextSibling as DocumentObject);
							num = 96;
							continue;
						}
						case 96:
							goto IL_6EE;
						case 97:
							if (type == FieldType.FieldHyperlink)
							{
								num = 158;
								continue;
							}
							return result;
						case 98:
						{
							Hyperlink hyperlink;
							result = dc.ᜁ(hyperlink.PictureToDisplay);
							num = 181;
							continue;
						}
						case 99:
						{
							if (num9 >= num5)
							{
								num = 168;
								continue;
							}
							DocumentObject documentObject6 = this.Range.ᜁ()[num9] as DocumentObject;
							num = 80;
							continue;
						}
						case 100:
						{
							Hyperlink hyperlink;
							if (hyperlink.Field.NextSibling.NextSibling != null)
							{
								num = 32;
								continue;
							}
							goto IL_412;
						}
						case 101:
						{
							TextRange textRange4 = documentObject4 as TextRange;
							textRange4.CharacterFormat = base.CharacterFormat;
							num = 129;
							continue;
						}
						case 102:
						{
							Hyperlink hyperlink;
							result = dc.ᜀ(hyperlink.TextToDisplay, characterFormat.Font, null, true);
							num = 43;
							continue;
						}
						case 103:
						{
							spr\u1AD2 spr_u1AD;
							int num2;
							if (spr_u1AD.ᜂ().Count > num2 + 2)
							{
								num = 2;
								continue;
							}
							goto IL_A51;
						}
						case 104:
						{
							if (spr\u1A69.ᜧ)
							{
								num = 108;
								continue;
							}
							string a_2 = this.Text;
							num = 171;
							continue;
						}
						case 105:
							return result;
						case 106:
							if ((documentObject4.NextSibling as FieldMark).Type == FieldMarkType.FieldEnd)
							{
								num = 0;
								continue;
							}
							goto IL_BF8;
						case 107:
							goto IL_FE0;
						case 108:
							this.Text = "";
							num = 144;
							continue;
						case 109:
						{
							DocumentObject documentObject6;
							TextRange textRange5 = documentObject6 as TextRange;
							textRange5.ᜀ = new spr\u22A8();
							textRange5.ᜀ.ᜁ(true);
							num = 107;
							continue;
						}
						case 110:
							num = 125;
							continue;
						case 111:
							goto IL_BD4;
						case 112:
							return result;
						case 113:
						{
							TextRange textRange2;
							textRange2.Text = "";
							num = 61;
							continue;
						}
						case 114:
							goto IL_9AD;
						case 115:
							if (documentObject4.NextSibling is FieldMark)
							{
								num = 126;
								continue;
							}
							goto IL_BF8;
						case 116:
							goto IL_1219;
						case 117:
							num = 69;
							continue;
						case 118:
							goto IL_1432;
						case 119:
							goto IL_3B9;
						case 120:
							if (documentObject4.DocumentObjectType == DocumentObjectType.TextRange)
							{
								num = 101;
								continue;
							}
							goto IL_ACC;
						case 121:
							if ((this.Range.ᜁ()[num8] as FieldMark).Type == FieldMarkType.FieldSeparator)
							{
								num = 75;
								continue;
							}
							goto IL_CA5;
						case 122:
							goto IL_1465;
						case 123:
						{
							spr\u1AD2 spr_u1AD2;
							int num3;
							if (spr_u1AD2.ᜂ().Count > num3 + 2)
							{
								num = 5;
								continue;
							}
							goto IL_A0A;
						}
						case 124:
							if (type != FieldType.FieldMergeField)
							{
								num = 72;
								continue;
							}
							num = 44;
							continue;
						case 125:
						{
							Hyperlink hyperlink;
							if (hyperlink.PictureToDisplay.TextWrappingStyle != TextWrappingStyle.Behind)
							{
								num = 166;
								continue;
							}
							goto IL_353;
						}
						case 126:
							num = 106;
							continue;
						case 127:
							goto IL_E91;
						case 128:
							goto IL_9AD;
						case 129:
							goto IL_ACC;
						case 130:
						{
							Hyperlink hyperlink;
							if (!hyperlink.BookmarkName.StartsWith(ClipboardData.b("⽯♱᭳ᕵ", a_)))
							{
								num = 1;
								continue;
							}
							return result;
						}
						case 131:
							goto IL_3B9;
						case 132:
							if (type <= FieldType.FieldPage)
							{
								num = 91;
								continue;
							}
							num = 124;
							continue;
						case 133:
							if ((documentObject.NextSibling as FieldMark).Type == FieldMarkType.FieldEnd)
							{
								num = 116;
								continue;
							}
							goto IL_E5A;
						case 134:
						{
							spr\u1AD2 spr_u1AD = base.Owner as spr\u1AD2;
							int num2 = spr_u1AD.ᜂ().IndexOf(this);
							num = 103;
							continue;
						}
						case 135:
							num10 = this.Range.ᜁ().Count;
							goto IL_18FF;
						case 136:
							num = 86;
							continue;
						case 137:
							if (base.NextSibling is FieldMark)
							{
								num = 153;
								continue;
							}
							goto IL_17AD;
						case 138:
							num = 133;
							continue;
						case 139:
						{
							Hyperlink hyperlink;
							if (hyperlink.PictureToDisplay != null)
							{
								num = 110;
								continue;
							}
							text = hyperlink.TextToDisplay;
							num = 100;
							continue;
						}
						case 140:
							num = 63;
							continue;
						case 141:
							num = 151;
							continue;
						case 142:
						{
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										object obj;
										if ((obj as DocumentObject).DocumentObjectType == DocumentObjectType.FieldMark)
										{
											num = 4;
											continue;
										}
										break;
									}
									case 1:
										goto IL_92F;
									case 4:
									{
										int num4;
										num4++;
										num = 3;
										continue;
									}
									case 5:
										num = 1;
										continue;
									case 6:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num = 5;
											continue;
										}
										object obj = enumerator.Current;
										num = 0;
										continue;
									}
									}
									IL_906:
									num = 6;
									continue;
									goto IL_906;
								}
								IL_92F:
								goto IL_1891;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator;
									IDisposable disposable2 = enumerator as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable2.Dispose();
											num = 1;
											continue;
										case 1:
											goto IL_97A;
										case 2:
											if (disposable2 != null)
											{
												num = 0;
												continue;
											}
											goto IL_97C;
										}
										break;
									}
								}
								IL_97A:
								IL_97C:;
							}
							goto IL_97D;
							IL_1891:
							DocumentObject documentObject5 = this.Range.ᜁ()[this.Range.Count - 1] as DocumentObject;
							num = 79;
							continue;
						}
						case 143:
							return result;
						case 144:
							if (base.Owner is spr\u1AD2)
							{
								num = 71;
								continue;
							}
							num = 41;
							continue;
						case 145:
							if (base.Owner is spr\u1AD2)
							{
								num = 134;
								continue;
							}
							num = 174;
							continue;
						case 146:
							if (documentObject.DocumentObjectType == DocumentObjectType.TextRange)
							{
								num = 16;
								continue;
							}
							goto IL_BD4;
						case 147:
							goto IL_599;
						case 148:
						{
							int count = this.Range.Count;
							num8 = count - 1;
							num = 39;
							continue;
						}
						case 149:
						{
							Hyperlink hyperlink;
							if (hyperlink.BookmarkName != null)
							{
								num = 13;
								continue;
							}
							goto IL_4D0;
						}
						case 150:
							goto IL_A51;
						case 151:
							num10 = num5;
							goto IL_18FF;
						case 152:
							if (documentObject != null)
							{
								num = 169;
								continue;
							}
							goto IL_1219;
						case 153:
							num = 10;
							continue;
						case 154:
							goto IL_EE8;
						case 155:
							base.CharacterFormat = (base.NextSibling.NextSibling as TextRange).CharacterFormat;
							num = 150;
							continue;
						case 156:
							return result;
						case 157:
							return result;
						case 158:
						{
							Hyperlink hyperlink = new Hyperlink(this);
							num = 82;
							continue;
						}
						case 159:
							if (documentObject4 is TextRange)
							{
								num = 20;
								continue;
							}
							goto IL_ACC;
						case 160:
							if (documentObject4 != null)
							{
								num = 76;
								continue;
							}
							return result;
						case 161:
							goto IL_67D;
						case 162:
						{
							IDocumentObject owner;
							num6 = (owner as Section).SectionCountPages;
							goto IL_18DA;
						}
						case 163:
						{
							Hyperlink hyperlink;
							if (hyperlink.Field.NextSibling is FieldMark)
							{
								num = 48;
								continue;
							}
							goto IL_6EE;
						}
						case 164:
							num = 97;
							continue;
						case 165:
							num = 17;
							continue;
						case 166:
							num = 50;
							continue;
						case 167:
						{
							IDocumentObject owner;
							if (owner != null)
							{
								num = 24;
								continue;
							}
							goto IL_ED6;
						}
						case 168:
							num = 36;
							continue;
						case 169:
							num = 49;
							continue;
						case 170:
							goto IL_6EE;
						case 171:
							goto IL_A0A;
						case 172:
							if (this.Range.ᜁ()[num8] is FieldMark)
							{
								num = 23;
								continue;
							}
							goto IL_CA5;
						case 173:
							this.Text = ((text != null) ? text : string.Empty);
							num = 54;
							continue;
						case 174:
							if (base.NextSibling.NextSibling is TextRange)
							{
								num = 155;
								continue;
							}
							goto IL_A51;
						case 175:
							this.Text = ((text != null) ? text : string.Empty);
							num = 78;
							continue;
						case 176:
							if (documentObject is TextRange)
							{
								num = 19;
								continue;
							}
							goto IL_BD4;
						case 177:
							if (num5 != -1)
							{
								num = 141;
								continue;
							}
							num = 135;
							continue;
						case 178:
							if (type != FieldType.FieldNumPages)
							{
								num = 67;
								continue;
							}
							goto IL_97D;
						case 179:
							goto IL_C2F;
						case 180:
							if (!(documentObject4.NextSibling is FieldMark))
							{
								num = 56;
								continue;
							}
							return result;
						case 181:
							goto IL_353;
						case 182:
							if (num8 < 0)
							{
								num = 131;
								continue;
							}
							num = 172;
							continue;
						case 183:
							characterFormat = (base.NextSibling.NextSibling as TextRange).CharacterFormat;
							documentObject4 = (base.NextSibling as DocumentObject);
							num = 11;
							continue;
						case 184:
						{
							Paragraph paragraph = base.Owner as Paragraph;
							int num11 = paragraph.Items.IndexOf(this.Range.ᜁ()[this.Range.Count - 1] as DocumentObject);
							TextRange textRange6 = new TextRange(base.Document);
							paragraph.Items.Insert(num11 + 1, textRange6);
							textRange6.Text = string.Empty;
							textRange6.ApplyCharacterFormat(base.CharacterFormat);
							num = 143;
							continue;
						}
						case 185:
							num = 180;
							continue;
						case 186:
							if (base.NextSibling != null)
							{
								num = 14;
								continue;
							}
							goto IL_17AD;
						case 187:
						{
							Hyperlink hyperlink;
							if (hyperlink.Field.NextSibling.NextSibling is TextRange)
							{
								num = 161;
								continue;
							}
							goto IL_412;
						}
						case 188:
							num = 53;
							continue;
						}
						break;
						IL_353:
						num = 175;
						continue;
						IL_3B9:
						num = 177;
						continue;
						IL_412:
						characterFormat = base.ឬ();
						num = 18;
						continue;
						IL_4D0:
						num = 139;
						continue;
						IL_599:
						num = 182;
						continue;
						IL_67D:
						documentObject = null;
						num = 8;
						continue;
						IL_6B2:
						num = 4;
						continue;
						IL_6EE:
						num = 152;
						continue;
						IL_73F:
						text = text2;
						result = dc.ᜁ(text, base.ឬ().Font, null);
						num = 105;
						continue;
						IL_97D:
						documentObject4 = null;
						num = 87;
						continue;
						IL_9AD:
						num = 65;
						continue;
						IL_A0A:
						num = 145;
						continue;
						IL_A51:
						characterFormat2 = base.ឬ();
						num = 85;
						continue;
						IL_A82:
						num = 167;
						continue;
						IL_ACC:
						num = 160;
						continue;
						IL_BD4:
						num = 74;
						continue;
						IL_BF8:
						documentObject4 = (documentObject4.NextSibling as DocumentObject);
						num = 159;
						continue;
						IL_C2F:
						num = 173;
						continue;
						IL_CA5:
						documentObject2 = (this.Range.ᜁ()[num8] as DocumentObject);
						num = 35;
						continue;
						IL_E5A:
						documentObject = (documentObject.NextSibling as DocumentObject);
						num = 176;
						continue;
						IL_E91:
						num8--;
						num = 147;
						continue;
						IL_ED6:
						num = 47;
						continue;
						IL_EE8:
						num = 62;
						continue;
						IL_FE0:
						num9++;
						num = 31;
						continue;
						IL_1176:
						num = 99;
						continue;
						IL_1219:
						base.CharacterFormat.ApplyBase(characterFormat);
						num = 122;
						continue;
						IL_1432:
						TextRange textRange7 = documentObject2 as TextRange;
						textRange7.ᜀ = new spr\u22A8();
						textRange7.ᜀ.ᜁ(false);
						num = 127;
						continue;
						IL_1465:
						num = 27;
						continue;
						IL_17AD:
						text = base.Document.Variables[this.Value];
						result = dc.ᜁ(text, base.ឬ().Font, null);
						num = 52;
						continue;
						IL_18DA:
						num7 = num6;
						enumerator2 = this.Range.ᜁ().GetEnumerator();
						num = 55;
						continue;
						IL_18FF:
						num5 = num10;
						num9 = 0;
						num = 9;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x003857F0 File Offset: 0x003847F0
		internal new string ᜀ(MergeField A_0)
		{
			int a_ = 2;
			switch (0)
			{
			default:
			{
				string text;
				for (;;)
				{
					text = A_0.Text;
					int num = 8;
					for (;;)
					{
						double num2;
						switch (num)
						{
						case 0:
							text = text.Replace(ClipboardData.b("䑧", a_), ClipboardData.b("䙧", a_)).Replace(ClipboardData.b("䡧", a_), "");
							num2 = double.Parse(text, CultureInfo.InvariantCulture);
							num = 1;
							continue;
						case 1:
							if (A_0.NumberFormat.Contains(ClipboardData.b("䵧", a_)))
							{
								num = 10;
								continue;
							}
							goto IL_97;
						case 2:
							if (A_0.NumberFormat != string.Empty)
							{
								num = 0;
								continue;
							}
							num = 12;
							continue;
						case 3:
						{
							string text2 = DateTime.Parse(text).ToString(A_0.DateFormat, DateTimeFormatInfo.CurrentInfo);
							text = text2;
							num = 13;
							continue;
						}
						case 4:
							goto IL_97;
						case 5:
							num = 9;
							continue;
						case 6:
							if (A_0.Text != null)
							{
								num = 5;
								continue;
							}
							goto IL_247;
						case 7:
							goto IL_247;
						case 8:
							if (A_0.ConvertedToText)
							{
								num = 14;
								continue;
							}
							goto IL_247;
						case 9:
							if (A_0.Text.Trim().Length > 0)
							{
								num = 11;
								continue;
							}
							goto IL_247;
						case 10:
							goto IL_BD;
						case 11:
							num = 2;
							continue;
						case 12:
							if (A_0.DateFormat != string.Empty)
							{
								num = 3;
								continue;
							}
							goto IL_247;
						case 13:
							goto IL_247;
						case 14:
							if (true)
							{
							}
							num = 6;
							continue;
						}
						break;
						IL_97:
						string text3 = num2.ToString(A_0.NumberFormat, CultureInfo.InvariantCulture);
						text = text3;
						num = 7;
						continue;
						IL_BD:
						num2 /= 100.0;
						num = 4;
						continue;
						IL_247:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD;
						default:
							goto IL_25D;
						}
					}
				}
				IL_25D:
				if (false)
				{
				}
				return text;
			}
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06003C5C RID: 15452 RVA: 0x00385A64 File Offset: 0x00384A64
		spr\u1D30 spr\u1AB8.LayoutInfo
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_3E;
					case 2:
						goto IL_2E;
					}
					if (this.ᜀ == null)
					{
						num = 2;
						continue;
					}
					goto IL_3E;
					IL_2E:
					this.CreateLayoutInfo();
					num = 1;
					continue;
					IL_3E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2E;
					}
					break;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x00385AE4 File Offset: 0x00384AE4
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
		{
			switch (0)
			{
			default:
			{
				for (;;)
				{
					FieldType type = this.Type;
					int num = 2;
					for (;;)
					{
						Paragraph paragraph;
						string text;
						switch (num)
						{
						case 0:
							return;
						case 1:
							if (!(this.Owner is spr\u1AD2))
							{
								num = 19;
								continue;
							}
							num = 11;
							continue;
						case 2:
							if (type <= FieldType.FieldPage)
							{
								num = 22;
								continue;
							}
							num = 7;
							continue;
						case 3:
							if (type != FieldType.FieldHyperlink)
							{
								num = 0;
								continue;
							}
							goto IL_2E1;
						case 4:
							if (!(this.Owner is spr\u1AD2))
							{
								num = 23;
								continue;
							}
							num = 18;
							continue;
						case 5:
							if (type != FieldType.FieldIf)
							{
								num = 13;
								continue;
							}
							return;
						case 6:
							goto IL_2D4;
						case 7:
							if (type != FieldType.FieldMergeField)
							{
								num = 26;
								continue;
							}
							goto IL_3C0;
						case 8:
							if (type != FieldType.FieldPage)
							{
								num = 25;
								continue;
							}
							num = 12;
							continue;
						case 9:
							num = 3;
							continue;
						case 10:
							paragraph = this.OwnerParagraph;
							goto IL_25A;
						case 11:
							paragraph = (this.Owner.Owner.Owner as Paragraph);
							goto IL_25A;
						case 12:
							if (!(this.Owner is spr\u1AD2))
							{
								num = 21;
								continue;
							}
							num = 20;
							continue;
						case 13:
							num = 14;
							continue;
						case 14:
							if (type != FieldType.FieldNumPages)
							{
								num = 15;
								continue;
							}
							text = this.Text;
							num = 1;
							continue;
						case 15:
							num = 8;
							continue;
						case 16:
							goto IL_BA;
						case 17:
							if (type != FieldType.FieldDocVariable)
							{
								num = 9;
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
						case 18:
							goto IL_3A5;
						case 19:
							num = 10;
							continue;
						case 20:
							goto IL_D2;
						case 21:
							num = 24;
							continue;
						case 22:
							num = 5;
							continue;
						case 23:
							num = 16;
							continue;
						case 24:
							goto IL_3DC;
						case 25:
							return;
						case 26:
							num = 17;
							continue;
						}
						break;
						IL_25A:
						Paragraph paragraph2 = paragraph;
						new RectangleF(ltWidget.ᜁ().Location, dc.ᜁ(text, this.CharacterFormat.Font, null));
						dc.ᜀ(this.Text, this.CharacterFormat, paragraph2.Format, ltWidget.ᜁ(), ltWidget.ᜁ().Width, new spr\u1AD7
						{
							ᜀ = DrawingTextDirection.Horizontal
						});
						num = 6;
					}
				}
				IL_BA:
				Paragraph paragraph3 = this.OwnerParagraph;
				goto IL_338;
				IL_D2:
				Paragraph paragraph4 = this.Owner.Owner.Owner as Paragraph;
				goto IL_3E4;
				IL_2D4:
				if (true)
				{
				}
				return;
				IL_2E1:
				Hyperlink a_ = new Hyperlink(this);
				dc.ᜀ(a_, ltWidget);
				return;
				IL_338:
				Paragraph paragraph5 = paragraph3;
				ltWidget.ᜀ(base.Document.Variables[this.Value]);
				dc.ᜀ(ltWidget.ᜅ(), this.CharacterFormat, paragraph5.Format, ltWidget.ᜁ(), ltWidget.ᜁ().Width, new spr\u1AD7
				{
					ᜀ = DrawingTextDirection.Horizontal
				});
				return;
				IL_3A5:
				paragraph3 = (this.Owner.Owner.Owner as Paragraph);
				goto IL_338;
				IL_3C0:
				MergeField a_2 = this as MergeField;
				dc.ᜀ(a_2, ltWidget);
				return;
				IL_3DC:
				paragraph4 = this.OwnerParagraph;
				IL_3E4:
				Paragraph paragraph6 = paragraph4;
				dc.ᜊ = null;
				dc.ᜀ(ltWidget.ᜅ(), this.CharacterFormat, paragraph6.Format, ltWidget.ᜁ(), ltWidget.ᜁ().Width, new spr\u1AD7
				{
					ᜀ = DrawingTextDirection.Horizontal
				});
				return;
			}
			}
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x00385F44 File Offset: 0x00384F44
		// Note: this type is marked as 'beforefieldinit'.
		static Field()
		{
			int a_ = 0;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			Field.ᜊ = new Regex(ClipboardData.b("乥㑧ᵩ䝫䝭Ɐű彳呵䝷剹❻⁽멿ꂁ\ud983궅ꆇ낉뎋ꚍ쮏첑뚓쮕늗뎙뺛ꆝ", a_));
			Field.ᜋ = new Regex(ClipboardData.b("乥㑧ᵩ䝫䝭Ɐű彳呵䝷剹❻⁽멿ꂁ\ud983궅ꆇ떉ꒋ햍캏낑즓법놗뢙ꎛ", a_));
			Field.ᜌ = new Regex(ClipboardData.b("⹥ㅧ㩩⥫㱭㱯㭱㩳㵵⑷ॹ坻噽\udc7f\ude81\uda85ﮇꆉꖋ농쮏낑즓ꦕ낗솙슛벝ﶟ覡趣躥誧횩貫螭", a_));
			Field.\u170D = new Regex(ClipboardData.b("乥㱧╩⽫㉭ͯ奱嵳幵䝷䙹㍻๽ﮇ뒉ꊋ꒍릏", a_));
			Field.ᜎ = new Regex(ClipboardData.b("⽥♧⥩⁫㭭㑯㝱⑳㽵㭷⹹⥻ⱽ앿\ude81궅ꪇꊉ힋킍늏쾑뾓뾕몗늙ꎛꊝ튡킣쾥잧쒩\udfab邭麯颱鶳", a_));
		}

		// Token: 0x04002BBF RID: 11199
		private new const char ᜀ = ',';

		// Token: 0x04002BC0 RID: 11200
		private new const char ᜁ = '(';

		// Token: 0x04002BC1 RID: 11201
		private const char ᜂ = ')';

		// Token: 0x04002BC2 RID: 11202
		private new const string ᜃ = "\r";

		// Token: 0x04002BC3 RID: 11203
		private new const string ᜄ = "\a";

		// Token: 0x04002BC4 RID: 11204
		private const string ᜅ = "\r\a";

		// Token: 0x04002BC5 RID: 11205
		private const char ᜆ = '\u0013';

		// Token: 0x04002BC6 RID: 11206
		private const char ᜇ = '\u0015';

		// Token: 0x04002BC7 RID: 11207
		private bool ᜈ;

		// Token: 0x04002BC8 RID: 11208
		private string ᜉ;

		// Token: 0x04002BC9 RID: 11209
		protected FieldType m_fieldType;

		// Token: 0x04002BCA RID: 11210
		protected bool m_bConvertedToText;

		// Token: 0x04002BCB RID: 11211
		private static Regex ᜊ;

		// Token: 0x04002BCC RID: 11212
		private static Regex ᜋ;

		// Token: 0x04002BCD RID: 11213
		private static Regex ᜌ;

		// Token: 0x04002BCE RID: 11214
		private static Regex \u170D;

		// Token: 0x04002BCF RID: 11215
		private static Regex ᜎ;

		// Token: 0x04002BD0 RID: 11216
		private bool ᜏ;

		// Token: 0x04002BD1 RID: 11217
		protected ParagraphItemType m_paraItemType;

		// Token: 0x04002BD2 RID: 11218
		protected internal string m_formattingString;

		// Token: 0x04002BD3 RID: 11219
		protected internal string m_fieldValue;

		// Token: 0x04002BD4 RID: 11220
		protected TextFormat m_textFormat;

		// Token: 0x04002BD5 RID: 11221
		private string ᜐ;

		// Token: 0x04002BD6 RID: 11222
		private string ᜑ;

		// Token: 0x04002BD7 RID: 11223
		private new int \u1712;

		// Token: 0x04002BD8 RID: 11224
		private new bool \u1713;

		// Token: 0x04002BD9 RID: 11225
		private spr\u24EF \u1714;

		// Token: 0x04002BDA RID: 11226
		private bool \u1715;

		// Token: 0x04002BDB RID: 11227
		private string \u1716;

		// Token: 0x04002BDC RID: 11228
		private bool \u1717;

		// Token: 0x04002BDD RID: 11229
		private bool \u1718;

		// Token: 0x04002BDE RID: 11230
		private Stack<Field> \u1719;

		// Token: 0x04002BDF RID: 11231
		private new List<DocumentObject> \u171A;

		// Token: 0x04002BE0 RID: 11232
		private FieldMark \u171B;

		// Token: 0x04002BE1 RID: 11233
		private FieldMark \u171C;

		// Token: 0x04002BE2 RID: 11234
		private CharacterFormat \u171D;

		// Token: 0x02000515 RID: 1301
		internal enum Month
		{
			// Token: 0x04003570 RID: 13680
			January = 1,
			// Token: 0x04003571 RID: 13681
			Febrauary,
			// Token: 0x04003572 RID: 13682
			March,
			// Token: 0x04003573 RID: 13683
			April,
			// Token: 0x04003574 RID: 13684
			May,
			// Token: 0x04003575 RID: 13685
			June,
			// Token: 0x04003576 RID: 13686
			July,
			// Token: 0x04003577 RID: 13687
			August,
			// Token: 0x04003578 RID: 13688
			September,
			// Token: 0x04003579 RID: 13689
			October,
			// Token: 0x0400357A RID: 13690
			November,
			// Token: 0x0400357B RID: 13691
			December
		}
	}
}
