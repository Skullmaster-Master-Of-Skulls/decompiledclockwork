using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc
{
	// Token: 0x020000F9 RID: 249
	public class TableCell : Body, ICompositeObject, spr\u1AB8
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00041C00 File Offset: 0x00040C00
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x00041C44 File Offset: 0x00040C44
		public short GridSpan
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
				return this.ᜋ;
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
				this.ᜋ = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00041C88 File Offset: 0x00040C88
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x00041CCC File Offset: 0x00040CCC
		internal spr\u1AA4 SDTCell
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
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜏ = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00041D10 File Offset: 0x00040D10
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x00041D54 File Offset: 0x00040D54
		internal int Colspan
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00041D98 File Offset: 0x00040D98
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
				return DocumentObjectType.TableCell;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x00041DD8 File Offset: 0x00040DD8
		public TableRow OwnerRow
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
				return base.Owner as TableRow;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00041E20 File Offset: 0x00040E20
		public CellFormat CellFormat
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
				return this.ᜁ;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00041E64 File Offset: 0x00040E64
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x00041EAC File Offset: 0x00040EAC
		public float Width
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
				return this.CellFormat.CellWidth;
			}
			set
			{
				for (;;)
				{
					this.CellFormat.CellWidth = value;
					int num = 5;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_40;
							default:
								if (false)
								{
								}
								this.OwnerRow.OwnerTable.ᜢ = false;
								num = 4;
								continue;
							}
							break;
						case 1:
							num = 6;
							continue;
						case 2:
							num = 3;
							continue;
						case 3:
							if (this.OwnerRow.OwnerTable != null)
							{
								num = 0;
								continue;
							}
							return;
						case 4:
							return;
						case 5:
							goto IL_40;
						case 6:
							if (this.OwnerRow != null)
							{
								num = 2;
								continue;
							}
							return;
						}
						break;
						IL_40:
						if (base.Document.ᜇ)
						{
							return;
						}
						num = 1;
					}
				}
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00041F9C File Offset: 0x00040F9C
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00041FE0 File Offset: 0x00040FE0
		internal FtsWidth WidthType
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
				return this.ᜌ;
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
				this.ᜌ = value;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00042024 File Offset: 0x00041024
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x00042068 File Offset: 0x00041068
		public CellWidthType CellWidthType
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
				switch (value)
				{
				case CellWidthType.Auto:
					this.ᜌ = FtsWidth.Auto;
					this.CellFormat.IsAutoResized = true;
					return;
				case CellWidthType.Percentage:
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
						this.ᜌ = FtsWidth.Percentage;
						return;
					}
					break;
				case CellWidthType.Point:
					this.ᜌ = FtsWidth.Point;
					return;
				}
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x000420E0 File Offset: 0x000410E0
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0004212C File Offset: 0x0004112C
		internal byte WidthUnit
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
				this.ᜀ(13);
				return this.ᜆ;
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
				this.ᜆ = value;
				this.ᜀ(13, value);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0004217C File Offset: 0x0004117C
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x000421C4 File Offset: 0x000411C4
		internal Color ForeColor
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
				return this.CellFormat.ForeColor;
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
				this.CellFormat.ForeColor = value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0004220C File Offset: 0x0004120C
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x00042254 File Offset: 0x00041254
		internal TextureStyle TextureStyle
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
				return this.CellFormat.TextureStyle;
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
				this.CellFormat.TextureStyle = value;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0004229C File Offset: 0x0004129C
		internal CharacterFormat CharacterFormat
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
				return this.ᜂ;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000422E0 File Offset: 0x000412E0
		internal bool IsFixedWidth
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
				return this.Width > -1f;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x00042328 File Offset: 0x00041328
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0004236C File Offset: 0x0004136C
		internal float Scaling
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

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x000423B0 File Offset: 0x000413B0
		internal CellFormat TrackCellFormat
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_73;
					case 1:
						goto IL_54;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_54;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					if (this.ᜉ == null)
					{
						num = 1;
						continue;
					}
					goto IL_7D;
					IL_54:
					this.ᜉ = new CellFormat();
					this.ᜉ.ᜀ(this);
					num = 0;
				}
				IL_73:
				if (true)
				{
				}
				IL_7D:
				return this.ᜉ;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00042440 File Offset: 0x00041440
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x00042484 File Offset: 0x00041484
		internal RectangleF Bounds
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x000424C8 File Offset: 0x000414C8
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0004250C File Offset: 0x0004150C
		internal int HTMLColIndex
		{
			[CompilerGenerated]
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
				return this.ᜐ;
			}
			[CompilerGenerated]
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
				this.ᜐ = value;
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00042550 File Offset: 0x00041550
		public TableCell(IDocument document) : base((Document)document, null)
		{
			this.ᜁ = new CellFormat();
			this.ᜁ.ᜀ(this);
			this.ᜂ = new CharacterFormat(base.Document);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x000425C4 File Offset: 0x000415C4
		public new DocumentObject Clone()
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
			return (DocumentObject)this.CloneImpl();
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0004260C File Offset: 0x0004160C
		public int GetCellIndex()
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
			return base.ឯ();
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00042650 File Offset: 0x00041650
		public void SetCellWidth(float width, CellWidthType widthType)
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
						goto IL_D2;
					case 2:
						this.Width = 0f;
						num = 5;
						continue;
					case 3:
						goto IL_9B;
					case 4:
						if (widthType == CellWidthType.Percentage)
						{
							num = 7;
							continue;
						}
						goto IL_F0;
					case 5:
						goto IL_B3;
					case 6:
						this.Width = width;
						num = 3;
						continue;
					case 7:
						this.Width = 0f;
						this.Scaling = width;
						num = 0;
						continue;
					case 8:
						if (widthType == CellWidthType.Point)
						{
							num = 6;
							continue;
						}
						num = 4;
						continue;
					}
					if (widthType == CellWidthType.Auto)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num = 2;
							break;
						}
					}
					else
					{
						num = 8;
					}
				}
			}
			IL_9B:
			IL_B3:
			IL_D2:
			IL_F0:
			this.CellWidthType = widthType;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00042754 File Offset: 0x00041754
		internal new void ᜀ(CellFormat A_0, ParagraphFormat A_1, CharacterFormat A_2)
		{
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
					for (;;)
					{
						this.CellFormat.ApplyBase(A_0);
						int num = 0;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_17C;
							case 1:
							{
								Paragraph paragraph = base.Items[num] as Paragraph;
								paragraph.Format.TableStyleParagraphFormat = A_1;
								paragraph.BreakCharacterFormat.TableStyleCharacterFormat = A_2;
								IEnumerator enumerator = paragraph.Items.GetEnumerator();
								num2 = 5;
								continue;
							}
							case 2:
								if (num >= base.Items.Count)
								{
									num2 = 4;
									continue;
								}
								goto IL_14C;
							case 3:
								if (base.Items[num] is Paragraph)
								{
									num2 = 1;
									continue;
								}
								goto IL_59;
							case 4:
								return;
							case 5:
								try
								{
									num2 = 1;
									for (;;)
									{
										switch (num2)
										{
										case 0:
										{
											IEnumerator enumerator;
											if (!enumerator.MoveNext())
											{
												if (true)
												{
												}
												num2 = 2;
												continue;
											}
											ParagraphBase paragraphBase = (ParagraphBase)enumerator.Current;
											paragraphBase.ParaItemCharFormat.TableStyleCharacterFormat = A_2;
											num2 = 3;
											continue;
										}
										case 2:
											num2 = 4;
											continue;
										case 4:
											goto IL_FF;
										}
										IL_AF:
										num2 = 0;
										continue;
										goto IL_AF;
									}
									IL_FF:
									goto IL_59;
								}
								finally
								{
									for (;;)
									{
										IEnumerator enumerator;
										IDisposable disposable = enumerator as IDisposable;
										num2 = 2;
										for (;;)
										{
											switch (num2)
											{
											case 0:
												disposable.Dispose();
												num2 = 1;
												continue;
											case 1:
												goto IL_149;
											case 2:
												if (disposable != null)
												{
													num2 = 0;
													continue;
												}
												goto IL_14B;
											}
											break;
										}
									}
									IL_149:
									IL_14B:;
								}
								goto IL_14C;
							case 6:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_00;
								default:
									if (false)
									{
									}
									goto IL_17C;
								}
								break;
							}
							break;
							IL_59:
							num++;
							num2 = 6;
							continue;
							IL_14C:
							num2 = 3;
							continue;
							IL_17C:
							num2 = 2;
						}
					}
					break;
				}
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00042960 File Offset: 0x00041960
		protected override object CloneImpl()
		{
			TableCell tableCell;
			for (;;)
			{
				if (true)
				{
				}
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					tableCell = (TableCell)base.CloneImpl();
					tableCell.ᜁ = new CellFormat();
					tableCell.ᜁ.ᜀ(tableCell);
					tableCell.ᜁ.ImportContainer(this.ᜁ);
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						tableCell.ᜁ.Paddings.ᜀ(this.ᜁ.Paddings);
						num = 2;
						continue;
					case 1:
						if (this.ᜁ.HasValue(3))
						{
							num = 0;
							continue;
						}
						goto IL_C1;
					case 2:
						goto IL_BF;
					}
					break;
				}
			}
			IL_BF:
			IL_C1:
			tableCell.ᜂ = new CharacterFormat(base.Document);
			tableCell.ᜂ.ImportContainer(this.CharacterFormat);
			return tableCell;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00042A54 File Offset: 0x00041A54
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			for (;;)
			{
				int num = 0;
				int count = base.ChildObjects.Count;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
				{
					if (false)
					{
					}
					int num2 = 3;
					for (;;)
					{
						switch (num2)
						{
						case 0:
						{
							if (num >= count)
							{
								num2 = 2;
								continue;
							}
							DocumentObject documentObject = base.ChildObjects[num];
							documentObject.CloneRelationsTo(doc, nextOwner);
							num++;
							num2 = 1;
							continue;
						}
						case 1:
							goto IL_56;
						case 2:
							return;
						case 3:
							if (true)
							{
							}
							goto IL_56;
						}
						break;
						IL_56:
						num2 = 0;
					}
					break;
				}
				}
			}
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00042B00 File Offset: 0x00041B00
		private new void ᜀ(int A_0)
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
			this.ᜀ();
			this.CellFormat.ᜂ(A_0);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00042B50 File Offset: 0x00041B50
		private new void ᜀ(int A_0, object A_1)
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
			this.CellFormat.ᜃ(A_0);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00042B98 File Offset: 0x00041B98
		private new void ᜀ()
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
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					goto IL_6E;
				}
				if (this.CellFormat.OwnerBase != this)
				{
					num = 0;
					continue;
				}
				return;
				IL_5A:
				this.CellFormat.ᜀ(this);
				num = 2;
			}
			IL_6E:
			if (true)
			{
			}
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00042C20 File Offset: 0x00041C20
		internal BodyRegion ᜋ()
		{
			switch (0)
			{
			default:
			{
				int num = 6;
				TableCell tableCell;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1E8;
					case 1:
						if (this.OwnerRow.OwnerTable != null)
						{
							goto IL_231;
						}
						goto IL_2B2;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_231;
						default:
							goto IL_253;
						}
						break;
					case 3:
						if (tableCell.Items.Count > 0)
						{
							num = 0;
							continue;
						}
						goto IL_1EC;
					case 4:
						if (this.OwnerRow == null)
						{
							num = 12;
							continue;
						}
						goto IL_18F;
					case 5:
						num = 1;
						continue;
					case 7:
						try
						{
							num = 2;
							BodyRegion result;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_130;
								case 1:
									goto IL_141;
								case 3:
									num = 1;
									continue;
								case 4:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									TableCell tableCell2 = (TableCell)enumerator.Current;
									num = 5;
									continue;
								}
								case 5:
								{
									TableCell tableCell2;
									if (tableCell2.Items.Count > 0)
									{
										num = 6;
										continue;
									}
									break;
								}
								case 6:
								{
									TableCell tableCell2;
									result = tableCell2.Items[0];
									num = 0;
									continue;
								}
								}
								IL_FA:
								num = 4;
								continue;
								goto IL_FA;
							}
							IL_130:
							return result;
							IL_141:
							goto IL_79;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
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
										goto IL_18E;
									case 1:
										goto IL_18C;
									case 2:
										disposable.Dispose();
										num = 1;
										continue;
									}
									break;
								}
							}
							IL_18C:
							IL_18E:;
						}
						goto IL_18F;
					case 8:
						if (true)
						{
						}
						num = 4;
						continue;
					case 9:
						goto IL_79;
					case 10:
						goto IL_96;
					case 11:
					{
						TableRow tableRow;
						if (tableRow.NextSibling == null)
						{
							num = 10;
							continue;
						}
						tableRow = (tableRow.NextSibling as TableRow);
						IEnumerator enumerator = tableRow.Cells.GetEnumerator();
						num = 7;
						continue;
					}
					case 12:
						goto IL_2AD;
					case 13:
					{
						if (this.OwnerRow.NextSibling == null)
						{
							num = 5;
							continue;
						}
						TableRow tableRow = this.OwnerRow;
						num = 9;
						continue;
					}
					}
					if (base.NextSibling == null)
					{
						num = 8;
						continue;
					}
					tableCell = (base.NextSibling as TableCell);
					num = 3;
					continue;
					IL_79:
					num = 11;
					continue;
					IL_18F:
					num = 13;
					continue;
					IL_231:
					num = 2;
				}
				IL_96:
				return this.OwnerRow.OwnerTable.GetNextTextBodyItem();
				IL_1E8:
				return tableCell.Items[0];
				IL_1EC:
				return tableCell.ᜋ();
				IL_253:
				if (false)
				{
				}
				return this.OwnerRow.OwnerTable.GetNextTextBodyItem();
				IL_2AD:
				return null;
				IL_2B2:
				return null;
			}
			}
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00042F1C File Offset: 0x00041F1C
		internal new void ᜅ()
		{
			for (;;)
			{
				base.ᜅ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜂ != null)
						{
							goto IL_36;
						}
						goto IL_66;
					case 1:
						this.ᜂ.Close();
						this.ᜂ = null;
						num = 5;
						continue;
					case 2:
						return;
					case 3:
						this.ᜁ.Close();
						this.ᜁ = null;
						num = 2;
						continue;
					case 4:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_36;
						default:
							if (false)
							{
							}
							if (this.ᜁ != null)
							{
								num = 3;
								continue;
							}
							return;
						}
						break;
					case 5:
						goto IL_66;
					}
					break;
					IL_36:
					num = 1;
					continue;
					IL_66:
					num = 4;
				}
			}
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00042FF0 File Offset: 0x00041FF0
		protected override void InitXDLSHolder()
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
			base.InitXDLSHolder();
			base.XDLSHolder.AddElement(ClipboardData.b("ཫ୭ᱯṱ女ၵ᝷ࡹᅻώ", a_), this.CellFormat);
			base.XDLSHolder.AddElement(ClipboardData.b("ཫ٭ᅯqᕳᕵ౷ό๻卽ﺉ", a_), this.CharacterFormat);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00043078 File Offset: 0x00042078
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 11;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A2:
				num = 6;
				break;
			default:
				if (false)
				{
				}
				goto IL_4D;
			}
			for (;;)
			{
				IL_27:
				switch (num)
				{
				case 0:
					if (this.IsFixedWidth)
					{
						num = 1;
						continue;
					}
					goto IL_85;
				case 1:
					writer.WriteValue(ClipboardData.b("♰ᩲᅴͶᅸ", a_), this.Width);
					if (true)
					{
					}
					num = 3;
					continue;
				case 2:
					return;
				case 3:
					goto IL_85;
				case 4:
					if (this.ᜁ.OwnerRowFormat.ᜑ())
					{
						num = 2;
						continue;
					}
					num = 0;
					continue;
				case 5:
					goto IL_104;
				case 6:
					writer.WriteValue(ClipboardData.b("㝰ᱲݴቶ㩸ᑺᅼၾ", a_), this.ForeColor);
					num = 5;
					continue;
				case 7:
					goto IL_8D;
				}
				goto IL_4D;
				IL_85:
				num = 7;
			}
			return;
			IL_8D:
			if (this.ForeColor != Color.Empty)
			{
				goto IL_A2;
			}
			IL_104:
			writer.WriteValue(ClipboardData.b("╰ᙲ൴Ͷ౸ॺ᡼", a_), this.TextureStyle);
			return;
			IL_4D:
			base.WriteXmlAttributes(writer);
			num = 4;
			goto IL_27;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x000431D0 File Offset: 0x000421D0
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 16;
			for (;;)
			{
				IL_3F:
				base.ReadXmlAttributes(reader);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_104:
					num = 1;
					break;
				default:
					if (false)
					{
					}
					num = 4;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (reader.HasAttribute(ClipboardData.b("≵ᵷɹࡻ୽", a_)))
						{
							num = 6;
							continue;
						}
						return;
					case 1:
						goto IL_147;
					case 2:
						goto IL_8D;
					case 3:
						this.ForeColor = reader.ReadColor(ClipboardData.b("ふ᝷ࡹ᥻㵽", a_));
						num = 2;
						continue;
					case 4:
						if (reader.HasAttribute(ClipboardData.b("ⅵᅷṹࡻᙽ", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_147;
					case 5:
						goto IL_8B;
					case 6:
						if (true)
						{
						}
						this.TextureStyle = (TextureStyle)reader.ReadEnum(ClipboardData.b("≵ᵷɹࡻ୽", a_), typeof(TextureStyle));
						num = 8;
						continue;
					case 7:
						if (reader.HasAttribute(ClipboardData.b("ふ᝷ࡹ᥻㵽", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_8D;
					case 8:
						return;
					}
					goto IL_3F;
					IL_8D:
					num = 0;
					continue;
					IL_147:
					num = 7;
				}
				IL_8B:
				this.Width = reader.ReadFloat(ClipboardData.b("ⅵᅷṹࡻᙽ", a_));
				goto IL_104;
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00043358 File Offset: 0x00042358
		protected override void CreateLayoutInfo()
		{
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_61;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					goto IL_70;
				case 2:
					goto IL_61;
				}
				if (base.Paragraphs.Count == 0)
				{
					num = 2;
					continue;
				}
				break;
				IL_61:
				base.AddParagraph();
				num = 1;
			}
			IL_70:
			this.ᜀ = new TableCell.ᜀ(this);
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x000433E4 File Offset: 0x000423E4
		protected override IDocumentObjectCollection WidgetCollection
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
				return this.m_bodyItems;
			}
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00043428 File Offset: 0x00042428
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
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
			dc.ᜀ(this, ltWidget);
		}

		// Token: 0x04000DA5 RID: 3493
		internal new const int ᜀ = 13;

		// Token: 0x04000DA6 RID: 3494
		private new CellFormat ᜁ;

		// Token: 0x04000DA7 RID: 3495
		private new CharacterFormat ᜂ;

		// Token: 0x04000DA8 RID: 3496
		internal TextureStyle ᜃ;

		// Token: 0x04000DA9 RID: 3497
		internal new Color ᜄ = Color.Empty;

		// Token: 0x04000DAA RID: 3498
		internal new float ᜅ;

		// Token: 0x04000DAB RID: 3499
		internal byte ᜆ;

		// Token: 0x04000DAC RID: 3500
		internal int ᜇ;

		// Token: 0x04000DAD RID: 3501
		private float ᜈ = 100f;

		// Token: 0x04000DAE RID: 3502
		internal CellFormat ᜉ;

		// Token: 0x04000DAF RID: 3503
		private int ᜊ = 1;

		// Token: 0x04000DB0 RID: 3504
		private short ᜋ = 1;

		// Token: 0x04000DB1 RID: 3505
		private FtsWidth ᜌ = FtsWidth.Auto;

		// Token: 0x04000DB2 RID: 3506
		private CellWidthType \u170D = CellWidthType.Auto;

		// Token: 0x04000DB3 RID: 3507
		private RectangleF ᜎ;

		// Token: 0x04000DB4 RID: 3508
		private spr\u1AA4 ᜏ;

		// Token: 0x04000DB5 RID: 3509
		[CompilerGenerated]
		private new int ᜐ;

		// Token: 0x020000FA RID: 250
		internal new class ᜀ : spr\u2032
		{
			// Token: 0x0600064B RID: 1611 RVA: 0x0004346C File Offset: 0x0004246C
			internal bool ᜉ()
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
				return this.ᜁ;
			}

			// Token: 0x0600064C RID: 1612 RVA: 0x000434B0 File Offset: 0x000424B0
			internal void ᜀ(bool A_0)
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
				this.ᜁ = A_0;
			}

			// Token: 0x0600064D RID: 1613 RVA: 0x000434F4 File Offset: 0x000424F4
			internal bool ᜈ()
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
				return this.ᜂ;
			}

			// Token: 0x0600064E RID: 1614 RVA: 0x00043538 File Offset: 0x00042538
			internal void ᜂ(bool A_0)
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
				this.ᜂ = A_0;
			}

			// Token: 0x0600064F RID: 1615 RVA: 0x0004357C File Offset: 0x0004257C
			internal bool ᜇ()
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

			// Token: 0x06000650 RID: 1616 RVA: 0x000435C0 File Offset: 0x000425C0
			internal void ᜁ(bool A_0)
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
				this.ᜃ = A_0;
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x00043604 File Offset: 0x00042604
			internal bool ᜊ()
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
				return this.ᜄ;
			}

			// Token: 0x06000652 RID: 1618 RVA: 0x00043648 File Offset: 0x00042648
			internal void ᜃ(bool A_0)
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
				this.ᜄ = A_0;
			}

			// Token: 0x06000653 RID: 1619 RVA: 0x0004368C File Offset: 0x0004268C
			internal ᜀ(TableCell A_0) : base(ChildrenLayoutDirection.Vertical)
			{
				this.ᜀ = A_0;
				this.ᜄ();
				this.ᜅ();
				CellFormat cellFormat = this.ᜀ.CellFormat;
				if (cellFormat.TextDirection != TextDirection.LeftToRight)
				{
					base.\u1714(true);
				}
				base.ᜀ((byte)cellFormat.VerticalAlignment);
				base.\u1716(cellFormat.TextWrap);
			}

			// Token: 0x06000654 RID: 1620 RVA: 0x000436EC File Offset: 0x000426EC
			private void ᜆ()
			{
				for (;;)
				{
					int cellIndex = this.ᜀ.GetCellIndex();
					int rowIndex = this.ᜀ.OwnerRow.GetRowIndex();
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							base.ᜰ().ᜂ(0.0);
							num = 9;
							continue;
						case 1:
							base.ᜰ().ᜁ(0.0);
							if (true)
							{
							}
							num = 8;
							continue;
						case 2:
							if (cellIndex != 0)
							{
								num = 0;
								continue;
							}
							goto IL_C1;
						case 3:
							if (cellIndex != this.ᜀ.OwnerRow.Cells.Count - 1)
							{
								num = 11;
								continue;
							}
							goto IL_A3;
						case 4:
							return;
						case 5:
							if (rowIndex == this.ᜀ.OwnerRow.OwnerTable.Rows.Count - 1)
							{
								return;
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
								num = 6;
								continue;
							}
							break;
						case 6:
							base.ᜰ().ᜀ(0.0);
							num = 4;
							continue;
						case 7:
							if (rowIndex != 0)
							{
								num = 1;
								continue;
							}
							goto IL_13B;
						case 8:
							goto IL_13B;
						case 9:
							goto IL_C1;
						case 10:
							goto IL_A3;
						case 11:
							base.ᜰ().ᜃ(0.0);
							num = 10;
							continue;
						}
						break;
						IL_A3:
						num = 7;
						continue;
						IL_C1:
						num = 3;
						continue;
						IL_13B:
						num = 5;
					}
				}
			}

			// Token: 0x06000655 RID: 1621 RVA: 0x000438B0 File Offset: 0x000428B0
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
				CellFormat cellFormat = this.ᜀ.CellFormat;
				int num = this.ᜀ.OwnerRow.Cells.IndexOf(this.ᜀ);
				base.ᜉ(cellFormat.HorizontalMerge == CellMerge.Start && num < this.ᜀ.OwnerRow.Cells.Count - 1);
				base.ᜄ(cellFormat.HorizontalMerge == CellMerge.Continue);
				base.ᜊ(cellFormat.VerticalMerge == CellMerge.Start);
				base.ᜇ(cellFormat.VerticalMerge == CellMerge.Continue);
			}

			// Token: 0x06000656 RID: 1622 RVA: 0x00043970 File Offset: 0x00042970
			private void ᜄ()
			{
				switch (0)
				{
				default:
				{
					float num4;
					float num5;
					float num6;
					float num7;
					float num8;
					float num9;
					for (;;)
					{
						Paddings paddings = this.ᜀ.CellFormat.Paddings;
						float num = this.ᜀ.CellFormat.Paddings.Left;
						float num2 = this.ᜀ.CellFormat.Paddings.Right;
						float num3 = this.ᜀ.CellFormat.Paddings.Top;
						num4 = this.ᜀ.CellFormat.Paddings.Bottom;
						int cellIndex = this.ᜀ.GetCellIndex();
						int rowIndex = this.ᜀ.OwnerRow.GetRowIndex();
						int a_ = this.ᜀ.OwnerRow.Cells.Count - 1;
						int a_2 = this.ᜀ.OwnerRow.OwnerTable.Rows.Count - 1;
						num5 = this.ᜂ(cellIndex);
						num6 = this.ᜂ(cellIndex, rowIndex);
						num7 = this.ᜁ(cellIndex, a_);
						num8 = this.ᜀ(cellIndex, a_, rowIndex, a_2);
						num9 = 0f;
						int num10 = 40;
						for (;;)
						{
							int num11;
							int num12;
							switch (num10)
							{
							case 0:
								goto IL_5AF;
							case 1:
								this.ᜀ(ref num4, num11);
								num10 = 24;
								continue;
							case 2:
								if (this.ᜀ.CellFormat.SamePaddingsAsTable)
								{
									num10 = 21;
									continue;
								}
								num = this.ᜃ();
								num2 = this.ᜂ();
								num3 = this.ᜁ();
								num4 = this.ᜀ();
								num10 = 22;
								continue;
							case 3:
								goto IL_754;
							case 4:
								goto IL_6DA;
							case 5:
								goto IL_44C;
							case 6:
								base.ᜭ().ᜁ((double)((num3 > 0f) ? num3 : ((this.ᜀ.OwnerRow.RowFormat.Paddings.Top > 0f) ? this.ᜀ.OwnerRow.RowFormat.Paddings.Top : 0f)));
								base.ᜭ().ᜃ((double)(num2 - num7));
								num10 = 31;
								continue;
							case 7:
								num = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.Left;
								num10 = 35;
								continue;
							case 8:
								goto IL_44C;
							case 9:
								num3 = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.Top;
								num10 = 37;
								continue;
							case 10:
								num2 = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.Right;
								num10 = 5;
								continue;
							case 11:
								num4 = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.Bottom;
								num10 = 43;
								continue;
							case 12:
								goto IL_44C;
							case 13:
								if (true)
								{
								}
								num9 = this.ᜀ.OwnerRow.OwnerTable.TableFormat.CellSpacing * 2f;
								num10 = 49;
								continue;
							case 14:
								goto IL_36B;
							case 15:
								goto IL_754;
							case 16:
								if (this.ᜀ.OwnerRow.RowFormat.Paddings.HasKey(3))
								{
									num10 = 38;
									continue;
								}
								num10 = 20;
								continue;
							case 17:
								if (num11 >= this.ᜀ.OwnerRow.Cells.Count)
								{
									num10 = 48;
									continue;
								}
								num10 = 36;
								continue;
							case 18:
								goto IL_549;
							case 19:
								if (num12 >= this.ᜀ.OwnerRow.Cells.Count)
								{
									num10 = 47;
									continue;
								}
								num10 = 34;
								continue;
							case 20:
								if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.HasKey(3))
								{
									num10 = 11;
									continue;
								}
								num4 = 0f;
								num10 = 25;
								continue;
							case 21:
								num10 = 41;
								continue;
							case 22:
								goto IL_7C2;
							case 23:
								for (;;)
								{
									num3 = this.ᜀ.OwnerRow.RowFormat.Paddings.Top;
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										goto IL_4B4;
									}
								}
								IL_4B4:
								if (false)
								{
								}
								num10 = 18;
								continue;
							case 24:
								goto IL_740;
							case 25:
								goto IL_6DA;
							case 26:
								goto IL_549;
							case 27:
								goto IL_7C2;
							case 28:
								if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.HasKey(1))
								{
									num10 = 7;
									continue;
								}
								num = 5.4f;
								num10 = 42;
								continue;
							case 29:
								if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.HasKey(2))
								{
									num10 = 9;
									continue;
								}
								num3 = 0f;
								num10 = 26;
								continue;
							case 30:
								this.ᜁ(ref num3, num12);
								num10 = 0;
								continue;
							case 31:
								goto IL_859;
							case 32:
								num = this.ᜀ.OwnerRow.RowFormat.Paddings.Left;
								num10 = 14;
								continue;
							case 33:
								goto IL_5C3;
							case 34:
								if (!this.ᜀ.OwnerRow.Cells[num12].CellFormat.SamePaddingsAsTable)
								{
									num10 = 30;
									continue;
								}
								goto IL_5AF;
							case 35:
								goto IL_36B;
							case 36:
								if (!this.ᜀ.OwnerRow.Cells[num11].CellFormat.SamePaddingsAsTable)
								{
									num10 = 1;
									continue;
								}
								goto IL_740;
							case 37:
								goto IL_549;
							case 38:
								num4 = this.ᜀ.OwnerRow.RowFormat.Paddings.Bottom;
								num10 = 4;
								continue;
							case 39:
								goto IL_5C3;
							case 40:
								if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.CellSpacing > 0f)
								{
									num10 = 13;
									continue;
								}
								goto IL_251;
							case 41:
								if (this.ᜀ.OwnerRow.RowFormat.Paddings.HasKey(1))
								{
									num10 = 32;
									continue;
								}
								num10 = 28;
								continue;
							case 42:
								goto IL_36B;
							case 43:
								goto IL_6DA;
							case 44:
								num2 = this.ᜀ.OwnerRow.RowFormat.Paddings.Right;
								num10 = 12;
								continue;
							case 45:
								if (this.ᜀ.OwnerRow.RowFormat.Paddings.HasKey(4))
								{
									num10 = 44;
									continue;
								}
								num10 = 46;
								continue;
							case 46:
								if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.HasKey(4))
								{
									num10 = 10;
									continue;
								}
								num2 = 5.4f;
								num10 = 8;
								continue;
							case 47:
								num10 = 16;
								continue;
							case 48:
								num10 = 27;
								continue;
							case 49:
								goto IL_251;
							case 50:
								if (this.ᜀ.OwnerRow.RowFormat.Paddings.HasKey(2))
								{
									num10 = 23;
									continue;
								}
								num10 = 29;
								continue;
							}
							break;
							IL_251:
							num10 = 2;
							continue;
							IL_36B:
							num10 = 45;
							continue;
							IL_44C:
							num10 = 50;
							continue;
							IL_549:
							num12 = 0;
							num10 = 39;
							continue;
							IL_5AF:
							num12++;
							num10 = 33;
							continue;
							IL_5C3:
							num10 = 19;
							continue;
							IL_6DA:
							num11 = 0;
							num10 = 15;
							continue;
							IL_740:
							num11++;
							num10 = 3;
							continue;
							IL_754:
							num10 = 17;
							continue;
							IL_7C2:
							base.ᜭ().ᜂ((double)(num - num5));
							num10 = 6;
						}
					}
					IL_859:
					base.ᜭ().ᜀ((double)((num4 > 0f) ? num4 : ((this.ᜀ.OwnerRow.RowFormat.Paddings.Bottom > 0f) ? this.ᜀ.OwnerRow.RowFormat.Paddings.Bottom : 0f)));
					base.ᜰ().ᜂ((double)(num9 + num5));
					base.ᜰ().ᜁ((double)(num9 + num6));
					base.ᜰ().ᜃ((double)(num9 + num7));
					base.ᜰ().ᜀ((double)(num9 + num8));
					return;
				}
				}
			}

			// Token: 0x06000657 RID: 1623 RVA: 0x0004427C File Offset: 0x0004327C
			private float ᜂ(int A_0)
			{
				switch (0)
				{
				default:
				{
					float result;
					for (;;)
					{
						Borders borders = this.ᜀ.CellFormat.Borders;
						Borders borders2 = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Borders;
						Borders borders3 = this.ᜀ.OwnerRow.RowFormat.Borders;
						result = borders.Left.LineWidth / 2f;
						int num = 23;
						for (;;)
						{
							float lineWidth;
							float lineWidth2;
							switch (num)
							{
							case 0:
								return result;
							case 1:
								return result;
							case 2:
								if (A_0 > 0)
								{
									num = 19;
									continue;
								}
								num = 6;
								continue;
							case 3:
								num = 25;
								continue;
							case 4:
								return result;
							case 5:
								lineWidth = borders.Left.LineWidth;
								goto IL_48E;
							case 6:
								if (borders.Left.BorderType == BorderStyle.None)
								{
									num = 12;
									continue;
								}
								num = 5;
								continue;
							case 7:
								num = 9;
								continue;
							case 8:
								num = 27;
								continue;
							case 9:
								if (borders.Left.HasNoneStyle)
								{
									num = 32;
									continue;
								}
								goto IL_19D;
							case 10:
								goto IL_265;
							case 11:
							{
								TableCell tableCell;
								if (tableCell.CellFormat.Borders.Right.IsBorderDefined)
								{
									num = 30;
									continue;
								}
								result = borders2.Vertical.LineWidth / 2f;
								num = 10;
								continue;
							}
							case 12:
								num = 22;
								continue;
							case 13:
								this.ᜁ(this.ᜀ(borders.Left.LineWidth, borders2.Vertical.LineWidth, true, ref result));
								num = 20;
								continue;
							case 14:
								this.ᜁ(borders3.Vertical.BorderType == BorderStyle.None && borders2.Vertical.BorderType == BorderStyle.None);
								num = 24;
								continue;
							case 15:
								if (borders.Left.BorderType == BorderStyle.None)
								{
									num = 7;
									continue;
								}
								goto IL_19D;
							case 16:
								if (borders.Left.BorderType != BorderStyle.None)
								{
									num = 21;
									continue;
								}
								goto IL_2CE;
							case 17:
							{
								TableCell tableCell;
								if (tableCell.CellFormat.Borders.Right.IsBorderDefined)
								{
									num = 34;
									continue;
								}
								goto IL_2CE;
							}
							case 18:
								return result;
							case 19:
							{
								TableCell tableCell = this.ᜁ(A_0 - 1);
								num = 16;
								continue;
							}
							case 20:
								return result;
							case 21:
								num = 17;
								continue;
							case 22:
								lineWidth = borders2.Left.LineWidth;
								goto IL_48E;
							case 23:
								if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.CellSpacing > 0f)
								{
									num = 8;
									continue;
								}
								num = 2;
								continue;
							case 24:
								return result;
							case 25:
								lineWidth2 = borders2.Vertical.LineWidth;
								goto IL_4A6;
							case 26:
								lineWidth2 = borders.Left.LineWidth;
								goto IL_4A6;
							case 27:
								if (borders.Left.BorderType == BorderStyle.None)
								{
									num = 3;
									continue;
								}
								num = 26;
								continue;
							case 28:
							{
								TableCell tableCell;
								result = tableCell.CellFormat.Borders.Right.LineWidth / 2f;
								this.ᜁ(true);
								num = 1;
								continue;
							}
							case 29:
							{
								TableCell tableCell;
								if (borders2.Vertical.LineWidth < tableCell.CellFormat.Borders.Right.LineWidth)
								{
									num = 28;
									continue;
								}
								num = 15;
								continue;
							}
							case 30:
								num = 29;
								continue;
							case 31:
								if (borders.Left.BorderType != BorderStyle.None)
								{
									num = 13;
									continue;
								}
								num = 11;
								continue;
							case 32:
								this.ᜁ(true);
								result = 0f;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									return result;
								}
								if (false)
								{
								}
								num = 4;
								continue;
							case 33:
								goto IL_242;
							case 34:
							{
								TableCell tableCell;
								this.ᜁ(this.ᜀ(borders.Left.LineWidth, tableCell.CellFormat.Borders.Right.LineWidth, true, ref result));
								num = 33;
								continue;
							}
							}
							break;
							IL_19D:
							result = borders2.Vertical.LineWidth / 2f;
							num = 14;
							continue;
							IL_2CE:
							num = 31;
							continue;
							IL_48E:
							result = lineWidth / 2f;
							num = 0;
							continue;
							IL_4A6:
							result = lineWidth2 / 2f;
							num = 18;
						}
					}
					IL_242:
					return result;
					IL_265:
					if (true)
					{
					}
					return result;
				}
				}
			}

			// Token: 0x06000658 RID: 1624 RVA: 0x000447B8 File Offset: 0x000437B8
			private float ᜂ(int A_0, int A_1)
			{
				switch (0)
				{
				default:
				{
					float result;
					for (;;)
					{
						Borders borders = this.ᜀ.CellFormat.Borders;
						Borders borders2 = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Borders;
						Borders borders3 = this.ᜀ.OwnerRow.RowFormat.Borders;
						result = borders.Top.LineWidth / 2f;
						int num = 28;
						for (;;)
						{
							float lineWidth;
							float lineWidth2;
							switch (num)
							{
							case 0:
								if (borders.Top.BorderType == BorderStyle.None)
								{
									num = 3;
									continue;
								}
								num = 14;
								continue;
							case 1:
								if (borders.Top.BorderType != BorderStyle.None)
								{
									num = 10;
									continue;
								}
								goto IL_3A8;
							case 2:
								lineWidth = borders2.Horizontal.LineWidth;
								goto IL_3D2;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_382;
								default:
									if (false)
									{
									}
									num = 23;
									continue;
								}
								break;
							case 4:
								if (borders.Top.BorderType != BorderStyle.None)
								{
									num = 22;
									continue;
								}
								num = 18;
								continue;
							case 5:
							{
								TableCell tableCell;
								if (borders2.Horizontal.LineWidth < tableCell.CellFormat.Borders.Bottom.LineWidth)
								{
									num = 9;
									continue;
								}
								result = borders2.Horizontal.LineWidth / 2f;
								num = 15;
								continue;
							}
							case 6:
								if (borders.Top.BorderType == BorderStyle.None)
								{
									num = 29;
									continue;
								}
								num = 17;
								continue;
							case 7:
							{
								TableCell tableCell;
								if (tableCell.CellFormat.Borders.Bottom.IsBorderDefined)
								{
									num = 21;
									continue;
								}
								goto IL_3A8;
							}
							case 8:
								return result;
							case 9:
							{
								if (true)
								{
								}
								TableCell tableCell;
								result = tableCell.CellFormat.Borders.Bottom.LineWidth / 2f;
								this.ᜀ(true);
								num = 20;
								continue;
							}
							case 10:
								num = 7;
								continue;
							case 11:
								return result;
							case 12:
								return result;
							case 13:
								return result;
							case 14:
								lineWidth2 = borders.Top.LineWidth;
								goto IL_487;
							case 15:
								this.ᜀ(borders3.Horizontal.BorderType == BorderStyle.None && borders2.Horizontal.BorderType == BorderStyle.None);
								num = 8;
								continue;
							case 16:
							{
								TableCell tableCell = this.ᜀ(A_1 - 1);
								num = 1;
								continue;
							}
							case 17:
								lineWidth = borders.Top.LineWidth;
								goto IL_3D2;
							case 18:
							{
								TableCell tableCell;
								if (tableCell.CellFormat.Borders.Bottom.IsBorderDefined)
								{
									num = 26;
									continue;
								}
								result = borders2.Horizontal.LineWidth / 2f;
								num = 24;
								continue;
							}
							case 19:
								return result;
							case 20:
								return result;
							case 21:
							{
								TableCell tableCell;
								this.ᜀ(this.ᜀ(borders.Top.LineWidth, tableCell.CellFormat.Borders.Bottom.LineWidth, true, ref result));
								num = 19;
								continue;
							}
							case 22:
								this.ᜀ(this.ᜀ(borders.Top.LineWidth, borders2.Horizontal.LineWidth, true, ref result));
								num = 11;
								continue;
							case 23:
								goto IL_382;
							case 24:
								return result;
							case 25:
								num = 6;
								continue;
							case 26:
								num = 5;
								continue;
							case 27:
								if (A_1 > 0)
								{
									num = 16;
									continue;
								}
								num = 0;
								continue;
							case 28:
								if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.CellSpacing > 0f)
								{
									num = 25;
									continue;
								}
								num = 27;
								continue;
							case 29:
								num = 2;
								continue;
							}
							break;
							IL_3A8:
							num = 4;
							continue;
							IL_3D2:
							result = lineWidth / 2f;
							num = 13;
							continue;
							IL_487:
							result = lineWidth2 / 2f;
							num = 12;
							continue;
							IL_382:
							lineWidth2 = borders2.Top.LineWidth;
							goto IL_487;
						}
					}
					return result;
				}
				}
			}

			// Token: 0x06000659 RID: 1625 RVA: 0x00044C68 File Offset: 0x00043C68
			private float ᜁ(int A_0, int A_1)
			{
				switch (0)
				{
				default:
				{
					float result;
					for (;;)
					{
						Borders borders = this.ᜀ.CellFormat.Borders;
						Borders borders2 = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Borders;
						int num = this.ᜀ(A_0, A_1);
						int num2 = 40;
						for (;;)
						{
							TableCell tableCell;
							float lineWidth;
							int num3;
							int cellIndex;
							float lineWidth2;
							switch (num2)
							{
							case 0:
								num2 = 16;
								continue;
							case 1:
								if (tableCell.CellFormat.Borders.Left.IsBorderDefined)
								{
									num2 = 30;
									continue;
								}
								result = borders2.Vertical.LineWidth / 2f;
								this.ᜃ(true);
								num2 = 12;
								continue;
							case 2:
								lineWidth = borders.Right.LineWidth;
								goto IL_300;
							case 3:
							{
								int num4;
								(((spr\u1AB8)this.ᜀ.OwnerRow.OwnerTable.Rows[num3].Cells[num4 - 1]).ᜀ() as TableCell.ᜀ).ᜃ(false);
								num2 = 15;
								continue;
							}
							case 4:
								(((spr\u1AB8)tableCell).ᜀ() as TableCell.ᜀ).ᜁ(true);
								num2 = 8;
								continue;
							case 5:
								return result;
							case 6:
								if (borders.Right.BorderType == BorderStyle.None)
								{
									num2 = 31;
									continue;
								}
								num2 = 2;
								continue;
							case 7:
							{
								int num4;
								if (num4 > 0)
								{
									num2 = 3;
									continue;
								}
								goto IL_53A;
							}
							case 8:
								goto IL_20F;
							case 9:
								if (borders.Right.HasNoneStyle)
								{
									num2 = 19;
									continue;
								}
								goto IL_47F;
							case 10:
								if (tableCell.CellFormat.Borders.Left.BorderType == BorderStyle.Cleared)
								{
									num2 = 4;
									continue;
								}
								goto IL_20F;
							case 11:
								this.ᜃ(this.ᜀ(borders.Right.LineWidth, tableCell.CellFormat.Borders.Left.LineWidth, false, ref result));
								num2 = 37;
								continue;
							case 12:
								goto IL_259;
							case 13:
								if (borders.Right.BorderType == BorderStyle.Cleared)
								{
									if (true)
									{
									}
									num2 = 36;
									continue;
								}
								return result;
							case 14:
								goto IL_259;
							case 15:
								goto IL_53A;
							case 16:
								goto IL_1FF;
							case 17:
								if (tableCell.CellFormat.Borders.Left.IsBorderDefined)
								{
									num2 = 11;
									continue;
								}
								goto IL_47F;
							case 18:
								if (borders.Right.BorderType == BorderStyle.None)
								{
									num2 = 0;
									continue;
								}
								num2 = 27;
								continue;
							case 19:
								goto IL_3B3;
							case 20:
							{
								int rowIndex;
								num3 = rowIndex;
								num2 = 23;
								continue;
							}
							case 21:
								this.ᜃ(this.ᜀ(borders.Right.LineWidth, borders2.Vertical.LineWidth, false, ref result));
								num2 = 14;
								continue;
							case 22:
								return result;
							case 23:
								goto IL_1B1;
							case 24:
								goto IL_1B1;
							case 25:
								if (borders.Right.BorderType == BorderStyle.None)
								{
									num2 = 43;
									continue;
								}
								goto IL_3B3;
							case 26:
								if (cellIndex > 0)
								{
									num2 = 20;
									continue;
								}
								return result;
							case 27:
								lineWidth2 = borders.Right.LineWidth;
								goto IL_382;
							case 28:
							{
								int rowIndex2;
								if (num3 < rowIndex2)
								{
									int num4 = this.ᜀ(tableCell, cellIndex, num3);
									num2 = 7;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_1FF;
								default:
									if (false)
									{
									}
									num2 = 39;
									continue;
								}
								break;
							}
							case 29:
								tableCell = this.ᜁ(num);
								num2 = 25;
								continue;
							case 30:
								this.ᜃ(this.ᜀ(borders2.Vertical.LineWidth, tableCell.CellFormat.Borders.Left.LineWidth, false, ref result));
								num2 = 47;
								continue;
							case 31:
								num2 = 38;
								continue;
							case 32:
								borders = this.ᜀ.OwnerRow.Cells[num - 1].CellFormat.Borders;
								num2 = 34;
								continue;
							case 33:
								num2 = 18;
								continue;
							case 34:
								goto IL_61C;
							case 35:
							{
								int rowIndex;
								int rowIndex2;
								if (rowIndex < rowIndex2)
								{
									num2 = 42;
									continue;
								}
								return result;
							}
							case 36:
							{
								int rowIndex = tableCell.OwnerRow.GetRowIndex();
								int rowIndex2 = this.ᜀ.OwnerRow.GetRowIndex();
								num2 = 35;
								continue;
							}
							case 37:
								goto IL_259;
							case 38:
								lineWidth = borders2.Right.LineWidth;
								goto IL_300;
							case 39:
								num2 = 5;
								continue;
							case 40:
								if (num - 1 > A_0)
								{
									num2 = 32;
									continue;
								}
								goto IL_61C;
							case 41:
								if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.CellSpacing > 0f)
								{
									num2 = 33;
									continue;
								}
								num2 = 45;
								continue;
							case 42:
								num2 = 10;
								continue;
							case 43:
								num2 = 9;
								continue;
							case 44:
								return result;
							case 45:
								if (A_0 < A_1)
								{
									num2 = 29;
									continue;
								}
								num2 = 6;
								continue;
							case 46:
								if (borders.Right.BorderType != BorderStyle.None)
								{
									num2 = 21;
									continue;
								}
								num2 = 1;
								continue;
							case 47:
								goto IL_259;
							}
							break;
							IL_1B1:
							num2 = 28;
							continue;
							IL_20F:
							cellIndex = tableCell.GetCellIndex();
							num2 = 26;
							continue;
							IL_259:
							num2 = 13;
							continue;
							IL_300:
							result = lineWidth / 2f;
							num2 = 44;
							continue;
							IL_382:
							result = lineWidth2 / 2f;
							num2 = 22;
							continue;
							IL_1FF:
							lineWidth2 = borders2.Vertical.LineWidth;
							goto IL_382;
							IL_3B3:
							num2 = 17;
							continue;
							IL_47F:
							num2 = 46;
							continue;
							IL_53A:
							num3++;
							num2 = 24;
							continue;
							IL_61C:
							result = borders.Right.LineWidth / 2f;
							num2 = 41;
						}
					}
					return result;
				}
				}
			}

			// Token: 0x0600065A RID: 1626 RVA: 0x00045340 File Offset: 0x00044340
			private float ᜀ(int A_0, int A_1, int A_2, int A_3)
			{
				switch (0)
				{
				default:
				{
					float result;
					for (;;)
					{
						Borders borders = this.ᜀ.CellFormat.Borders;
						Borders borders2 = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Borders;
						result = borders.Bottom.LineWidth / 2f;
						int num = 28;
						for (;;)
						{
							float lineWidth;
							float lineWidth2;
							switch (num)
							{
							case 0:
								goto IL_13D;
							case 1:
								lineWidth = borders2.Horizontal.LineWidth;
								goto IL_387;
							case 2:
								if (borders.Bottom.BorderType == BorderStyle.None)
								{
									num = 32;
									continue;
								}
								num = 18;
								continue;
							case 3:
							{
								TableCell tableCell;
								if (tableCell.CellFormat.Borders.Top.IsBorderDefined)
								{
									num = 4;
									continue;
								}
								goto IL_39F;
							}
							case 4:
							{
								TableCell tableCell;
								this.ᜂ(this.ᜀ(borders.Bottom.LineWidth, tableCell.CellFormat.Borders.Top.LineWidth, false, ref result));
								num = 15;
								continue;
							}
							case 5:
								goto IL_5B7;
							case 6:
								num = 1;
								continue;
							case 7:
								this.ᜂ(borders.Bottom.BorderType == BorderStyle.Cleared);
								num = 0;
								continue;
							case 8:
								if (borders.Bottom.BorderType == BorderStyle.None)
								{
									num = 6;
									continue;
								}
								num = 30;
								continue;
							case 9:
								this.ᜂ(this.ᜀ(borders.Bottom.LineWidth, borders2.Horizontal.LineWidth, false, ref result));
								if (true)
								{
								}
								num = 5;
								continue;
							case 10:
								if (borders.Bottom.BorderType != BorderStyle.None)
								{
									num = 9;
									continue;
								}
								num = 29;
								continue;
							case 11:
							{
								int num2;
								int index = this.ᜀ(this.ᜀ, A_0, num2);
								TableCell tableCell = this.ᜀ(num2);
								num = 26;
								continue;
							}
							case 12:
								if (this.ᜈ())
								{
									num = 23;
									continue;
								}
								return result;
							case 13:
								this.ᜂ(true);
								result = 0f;
								num = 27;
								continue;
							case 14:
								return result;
							case 15:
								goto IL_5B7;
							case 16:
								num = 22;
								continue;
							case 17:
								if (borders.Bottom.BorderType != BorderStyle.None)
								{
									num = 40;
									continue;
								}
								return result;
							case 18:
								lineWidth2 = borders.Bottom.LineWidth;
								goto IL_2D4;
							case 19:
								if (borders.Bottom.BorderType != BorderStyle.None)
								{
									num = 7;
									continue;
								}
								goto IL_13D;
							case 20:
								return result;
							case 21:
								goto IL_5B7;
							case 22:
								if (borders.Bottom.HasNoneStyle)
								{
									num = 13;
									continue;
								}
								goto IL_4D2;
							case 23:
								num = 33;
								continue;
							case 24:
								num = 17;
								continue;
							case 25:
								return result;
							case 26:
								if (borders.Bottom.BorderType != BorderStyle.None)
								{
									num = 36;
									continue;
								}
								goto IL_39F;
							case 27:
								goto IL_5B7;
							case 28:
							{
								if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.CellSpacing > 0f)
								{
									num = 31;
									continue;
								}
								int num2 = this.ᜀ(A_0, A_2, A_3);
								num = 39;
								continue;
							}
							case 29:
							{
								TableCell tableCell;
								if (tableCell.CellFormat.Borders.Top.IsBorderDefined)
								{
									num = 34;
									continue;
								}
								num = 37;
								continue;
							}
							case 30:
								lineWidth = borders.Bottom.LineWidth;
								goto IL_387;
							case 31:
								num = 8;
								continue;
							case 32:
								num = 38;
								continue;
							case 33:
							{
								int num2;
								int index;
								if (this.ᜀ.OwnerRow.OwnerTable.Rows[num2].Cells[index].Width < this.ᜀ.Width)
								{
									num = 24;
									continue;
								}
								return result;
							}
							case 34:
							{
								TableCell tableCell;
								this.ᜂ(this.ᜀ(borders2.Horizontal.LineWidth, tableCell.CellFormat.Borders.Top.LineWidth, false, ref result));
								num = 35;
								continue;
							}
							case 35:
								goto IL_5B7;
							case 36:
								num = 3;
								continue;
							case 37:
								if (borders.Bottom.BorderType == BorderStyle.None)
								{
									num = 16;
									continue;
								}
								goto IL_4D2;
							case 38:
								lineWidth2 = borders2.Bottom.LineWidth;
								goto IL_2D4;
							case 39:
								if (A_2 < A_3)
								{
									num = 11;
									continue;
								}
								num = 2;
								continue;
							case 40:
								goto IL_1C9;
							}
							break;
							IL_13D:
							num = 12;
							continue;
							IL_1C9:
							this.ᜂ(false);
							num = 14;
							continue;
							IL_4D2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1C9;
							default:
								if (false)
								{
								}
								result = borders2.Horizontal.LineWidth / 2f;
								num = 21;
								continue;
							}
							IL_2D4:
							result = lineWidth2 / 2f;
							num = 20;
							continue;
							IL_387:
							result = lineWidth / 2f;
							num = 25;
							continue;
							IL_39F:
							num = 10;
							continue;
							IL_5B7:
							num = 19;
						}
					}
					return result;
				}
				}
			}

			// Token: 0x0600065B RID: 1627 RVA: 0x00045934 File Offset: 0x00044934
			private TableCell ᜁ(int A_0)
			{
				int rowIndex;
				int index;
				int num2;
				for (;;)
				{
					if (true)
					{
					}
					rowIndex = this.ᜀ.OwnerRow.GetRowIndex();
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (this.ᜀ.OwnerRow.OwnerTable.Rows[rowIndex - 1].Cells[index].CellFormat.VerticalMerge == CellMerge.Continue)
							{
								num = 9;
								continue;
							}
							num = 14;
							continue;
						case 1:
							if (this.ᜀ.CellFormat.VerticalMerge == CellMerge.Continue)
							{
								num = 13;
								continue;
							}
							goto IL_285;
						case 2:
							num = 1;
							continue;
						case 3:
							num = 5;
							continue;
						case 4:
							goto IL_1A5;
						case 5:
							goto IL_8E;
						case 6:
							if (num2 < 0)
							{
								num = 3;
								continue;
							}
							index = this.ᜀ(this.ᜀ, A_0, num2);
							num = 10;
							continue;
						case 7:
							goto IL_248;
						case 8:
							if (rowIndex > 0)
							{
								num = 2;
								continue;
							}
							goto IL_285;
						case 9:
							num2 = rowIndex - 1;
							num = 4;
							continue;
						case 10:
							if (this.ᜀ.OwnerRow.OwnerTable.Rows[num2].Cells[index].CellFormat.VerticalMerge == CellMerge.Start)
							{
								num = 7;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_172;
							default:
								if (false)
								{
								}
								num2--;
								num = 11;
								continue;
							}
							break;
						case 11:
							goto IL_1A5;
						case 12:
							goto IL_172;
						case 13:
							index = this.ᜀ(this.ᜀ, A_0, rowIndex - 1);
							num = 0;
							continue;
						case 14:
							if (this.ᜀ.OwnerRow.OwnerTable.Rows[rowIndex - 1].Cells[index].CellFormat.VerticalMerge == CellMerge.Start)
							{
								num = 12;
								continue;
							}
							goto IL_285;
						}
						break;
						IL_1A5:
						num = 6;
					}
				}
				IL_8E:
				goto IL_285;
				IL_172:
				return this.ᜀ.OwnerRow.OwnerTable.Rows[rowIndex - 1].Cells[index];
				IL_248:
				return this.ᜀ.OwnerRow.OwnerTable.Rows[num2].Cells[index];
				IL_285:
				return this.ᜀ.OwnerRow.Cells[A_0];
			}

			// Token: 0x0600065C RID: 1628 RVA: 0x00045BDC File Offset: 0x00044BDC
			private TableCell ᜀ(int A_0)
			{
				int num;
				int num3;
				for (;;)
				{
					int cellIndex = this.ᜀ.GetCellIndex();
					num = this.ᜀ(this.ᜀ, cellIndex, A_0);
					int num2 = 9;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_CB;
						case 1:
							goto IL_154;
						case 2:
							if (this.ᜀ.OwnerRow.OwnerTable.Rows[A_0].Cells[num - 1].CellFormat.HorizontalMerge == CellMerge.Continue)
							{
								num2 = 6;
								continue;
							}
							num2 = 12;
							continue;
						case 3:
							goto IL_E8;
						case 4:
							num2 = 8;
							continue;
						case 5:
							if (this.ᜀ.OwnerRow.OwnerTable.Rows[A_0].Cells[num3].CellFormat.HorizontalMerge == CellMerge.Start)
							{
								num2 = 7;
								continue;
							}
							num3--;
							num2 = 10;
							continue;
						case 6:
							num3 = num - 1;
							num2 = 0;
							continue;
						case 7:
							goto IL_279;
						case 8:
							if (this.ᜀ.OwnerRow.OwnerTable.Rows[A_0].Cells[num].CellFormat.HorizontalMerge != CellMerge.Start)
							{
								num2 = 1;
								continue;
							}
							goto IL_28F;
						case 9:
							if (num > 0)
							{
								num2 = 14;
								continue;
							}
							goto IL_28F;
						case 10:
							goto IL_CB;
						case 11:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_E8;
							default:
								goto IL_1CD;
							}
							break;
						case 12:
							if (this.ᜀ.OwnerRow.OwnerTable.Rows[A_0].Cells[num - 1].CellFormat.HorizontalMerge == CellMerge.Start)
							{
								num2 = 4;
								continue;
							}
							goto IL_28F;
						case 13:
							if (num3 < 0)
							{
								num2 = 3;
								continue;
							}
							if (true)
							{
							}
							num2 = 5;
							continue;
						case 14:
							num2 = 2;
							continue;
						}
						break;
						IL_CB:
						num2 = 13;
						continue;
						IL_E8:
						num2 = 11;
					}
				}
				IL_154:
				return this.ᜀ.OwnerRow.OwnerTable.Rows[A_0].Cells[num - 1];
				IL_1CD:
				if (false)
				{
				}
				goto IL_28F;
				IL_279:
				return this.ᜀ.OwnerRow.OwnerTable.Rows[A_0].Cells[num3];
				IL_28F:
				return this.ᜀ.OwnerRow.OwnerTable.Rows[A_0].Cells[num];
			}

			// Token: 0x0600065D RID: 1629 RVA: 0x00045EA0 File Offset: 0x00044EA0
			private bool ᜀ(float A_0, float A_1, bool A_2, ref float A_3)
			{
				bool result;
				for (;;)
				{
					result = false;
					int num = 0;
					for (;;)
					{
						bool flag;
						switch (num)
						{
						case 0:
							if (A_2)
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
									num = 5;
									continue;
								}
							}
							num = 6;
							continue;
						case 1:
							return result;
						case 2:
							if (true)
							{
							}
							A_3 = A_1 / 2f;
							result = true;
							num = 3;
							continue;
						case 3:
							return result;
						case 4:
							flag = (A_0 <= A_1);
							goto IL_B8;
						case 5:
							flag = (A_0 < A_1);
							goto IL_B8;
						case 6:
							num = 4;
							continue;
						}
						break;
						IL_B8:
						if (flag)
						{
							num = 2;
						}
						else
						{
							A_3 = A_0 / 2f;
							num = 1;
						}
					}
				}
				return result;
			}

			// Token: 0x0600065E RID: 1630 RVA: 0x00045F78 File Offset: 0x00044F78
			private int ᜀ(TableCell A_0, int A_1, int A_2)
			{
				switch (0)
				{
				default:
				{
					int result;
					for (;;)
					{
						result = 0;
						float num = 0f;
						int num2 = 0;
						int num3 = 2;
						for (;;)
						{
							switch (num3)
							{
							case 0:
							{
								float num4;
								if (num == num4)
								{
									num3 = 3;
									continue;
								}
								num3 = 9;
								continue;
							}
							case 1:
							{
								float num4 = 0f;
								int num5 = 0;
								num3 = 13;
								continue;
							}
							case 2:
								goto IL_B0;
							case 3:
								num3 = 15;
								continue;
							case 4:
								goto IL_124;
							case 5:
								goto IL_1D6;
							case 6:
								goto IL_163;
							case 7:
							{
								int num5;
								if (num5 >= A_0.OwnerRow.OwnerTable.Rows[A_2].Cells.Count)
								{
									num3 = 6;
									continue;
								}
								float num4;
								num4 += A_0.OwnerRow.OwnerTable.Rows[A_2].Cells[num5].Width;
								num3 = 0;
								continue;
							}
							case 8:
								goto IL_B0;
							case 9:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
								{
									int num5;
									return num5;
								}
								default:
								{
									if (false)
									{
									}
									float num4;
									if (num < num4)
									{
										num3 = 11;
										continue;
									}
									int num5;
									num5++;
									num3 = 4;
									continue;
								}
								}
								break;
							case 10:
								goto IL_11F;
							case 11:
							{
								int num5;
								result = num5;
								num3 = 10;
								continue;
							}
							case 12:
							{
								int num5;
								return num5;
							}
							case 13:
								goto IL_124;
							case 14:
								if (num2 >= A_1)
								{
									num3 = 1;
									continue;
								}
								num += A_0.OwnerRow.Cells[num2].Width;
								num2++;
								num3 = 8;
								continue;
							case 15:
							{
								int num5;
								if (num5 == A_0.OwnerRow.OwnerTable.Rows[A_2].Cells.Count - 1)
								{
									num3 = 12;
									continue;
								}
								result = num5 + 1;
								num3 = 5;
								continue;
							}
							}
							break;
							IL_B0:
							num3 = 14;
							continue;
							IL_124:
							num3 = 7;
						}
					}
					IL_11F:
					IL_163:
					IL_1D6:
					if (true)
					{
					}
					return result;
				}
				}
			}

			// Token: 0x0600065F RID: 1631 RVA: 0x000461B4 File Offset: 0x000451B4
			private int ᜀ(int A_0, int A_1)
			{
				int num;
				for (;;)
				{
					if (true)
					{
					}
					num = A_0 + 1;
					int num2 = 4;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (num >= A_1 + 1)
							{
								goto IL_99;
							}
							num2 = 2;
							continue;
						case 1:
							return num;
						case 2:
							if (this.ᜀ.OwnerRow.Cells[num].CellFormat.HorizontalMerge != CellMerge.Continue)
							{
								num2 = 1;
								continue;
							}
							num++;
							num2 = 3;
							continue;
						case 3:
							goto IL_88;
						case 4:
							goto IL_88;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_99;
							default:
								goto IL_BC;
							}
							break;
						}
						break;
						IL_88:
						num2 = 0;
						continue;
						IL_99:
						num2 = 5;
					}
				}
				return num;
				IL_BC:
				if (false)
				{
				}
				return A_0;
			}

			// Token: 0x06000660 RID: 1632 RVA: 0x00046284 File Offset: 0x00045284
			private int ᜀ(int A_0, int A_1, int A_2)
			{
				for (;;)
				{
					int num = A_1 + 1;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_90;
						case 1:
							if (A_0 < this.ᜀ.OwnerRow.OwnerTable.Rows[num].Cells.Count)
							{
								num2 = 3;
								continue;
							}
							return num;
						case 2:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_42;
							default:
								goto IL_CA;
							}
							break;
						case 3:
							num2 = 4;
							continue;
						case 4:
							if (this.ᜀ.OwnerRow.OwnerTable.Rows[num].Cells[A_0].CellFormat.VerticalMerge != CellMerge.Continue)
							{
								num2 = 5;
								continue;
							}
							num++;
							num2 = 6;
							continue;
						case 5:
							return num;
						case 6:
							goto IL_42;
						case 7:
							if (num >= A_2 + 1)
							{
								num2 = 2;
								continue;
							}
							num2 = 1;
							continue;
						}
						break;
						IL_90:
						num2 = 7;
						continue;
						IL_42:
						goto IL_90;
					}
				}
				IL_CA:
				if (false)
				{
				}
				return A_1;
			}

			// Token: 0x06000661 RID: 1633 RVA: 0x000463B8 File Offset: 0x000453B8
			private float ᜃ()
			{
				float result;
				for (;;)
				{
					result = this.ᜀ.CellFormat.Paddings.Left;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_14F;
						case 1:
							if (this.ᜀ.CellFormat.Paddings.Left == -0.05f)
							{
								num = 2;
								continue;
							}
							return result;
						case 2:
							num = 5;
							continue;
						case 3:
							return result;
						case 4:
							goto IL_C7;
						case 5:
							if (this.ᜀ.OwnerRow.RowFormat.Paddings.HasKey(1))
							{
								num = 7;
								continue;
							}
							num = 6;
							continue;
						case 6:
							if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.HasKey(1))
							{
								num = 8;
								continue;
							}
							result = 5.4f;
							num = 0;
							continue;
						case 7:
							goto IL_13C;
						case 8:
							result = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.Left;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_13C;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						}
						break;
						IL_13C:
						result = this.ᜀ.OwnerRow.RowFormat.Paddings.Left;
						num = 3;
					}
				}
				IL_C7:
				return result;
				IL_14F:
				if (true)
				{
				}
				return result;
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x00046548 File Offset: 0x00045548
			private float ᜂ()
			{
				float result;
				for (;;)
				{
					result = this.ᜀ.CellFormat.Paddings.Right;
					int num = 0;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							if (this.ᜀ.CellFormat.Paddings.Right == -0.05f)
							{
								num = 6;
								continue;
							}
							return result;
						case 1:
							if (this.ᜀ.OwnerRow.RowFormat.Paddings.HasKey(4))
							{
								num = 5;
								continue;
							}
							num = 8;
							continue;
						case 2:
							return result;
						case 3:
							return result;
						case 4:
							return result;
						case 5:
							goto IL_147;
						case 6:
							num = 1;
							continue;
						case 7:
							result = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.Right;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_147;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 8:
							if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.HasKey(4))
							{
								num = 7;
								continue;
							}
							result = 5.4f;
							num = 2;
							continue;
						}
						break;
						IL_147:
						result = this.ᜀ.OwnerRow.RowFormat.Paddings.Right;
						num = 3;
					}
				}
				return result;
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x000466DC File Offset: 0x000456DC
			private float ᜁ()
			{
				float result;
				for (;;)
				{
					result = this.ᜀ.CellFormat.Paddings.Top;
					int num = 3;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							if (num2 >= this.ᜀ.OwnerRow.Cells.Count)
							{
								num = 14;
								continue;
							}
							num = 8;
							continue;
						case 1:
							goto IL_179;
						case 2:
							if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.HasKey(2))
							{
								num = 5;
								continue;
							}
							result = 0f;
							num = 11;
							continue;
						case 3:
							if (this.ᜀ.CellFormat.Paddings.Top != -0.05f)
							{
								num = 15;
								continue;
							}
							goto IL_EB;
						case 4:
							if (!this.ᜀ.CellFormat.Paddings.HasValue(2))
							{
								num = 9;
								continue;
							}
							goto IL_16A;
						case 5:
							result = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.Top;
							num = 18;
							continue;
						case 6:
							if (this.ᜀ.CellFormat.Paddings.Top != 0f)
							{
								goto IL_16A;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_9D;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								num = 19;
								continue;
							}
							break;
						case 7:
							goto IL_126;
						case 8:
							if (this.ᜀ.GetCellIndex() != num2)
							{
								num = 17;
								continue;
							}
							goto IL_126;
						case 9:
							goto IL_EB;
						case 10:
							goto IL_179;
						case 11:
							goto IL_16A;
						case 12:
							result = this.ᜀ.OwnerRow.RowFormat.Paddings.Top;
							num = 13;
							continue;
						case 13:
							goto IL_16A;
						case 14:
							return result;
						case 15:
							goto IL_9D;
						case 16:
							if (this.ᜀ.OwnerRow.RowFormat.Paddings.HasKey(2))
							{
								num = 12;
								continue;
							}
							num = 2;
							continue;
						case 17:
							this.ᜁ(ref result, num2);
							num = 7;
							continue;
						case 18:
							goto IL_16A;
						case 19:
							num = 4;
							continue;
						}
						break;
						IL_9D:
						num = 6;
						continue;
						IL_EB:
						num = 16;
						continue;
						IL_126:
						num2++;
						num = 1;
						continue;
						IL_16A:
						num2 = 0;
						num = 10;
						continue;
						IL_179:
						num = 0;
					}
				}
				return result;
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x000469A8 File Offset: 0x000459A8
			private void ᜁ(ref float A_0, int A_1)
			{
				for (;;)
				{
					TableCell tableCell = this.ᜀ.OwnerRow.Cells[A_1];
					float num = tableCell.CellFormat.Paddings.Top;
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_154;
							default:
								if (false)
								{
								}
								goto IL_141;
							}
							break;
						case 1:
							if (tableCell.CellFormat.Paddings.Top == 0f)
							{
								num2 = 5;
								continue;
							}
							goto IL_141;
						case 2:
							if (tableCell.CellFormat.Paddings.Top != -0.05f)
							{
								num2 = 8;
								continue;
							}
							goto IL_1F1;
						case 3:
							num = tableCell.OwnerRow.OwnerTable.TableFormat.Paddings.Top;
							num2 = 14;
							continue;
						case 4:
							if (tableCell.OwnerRow.OwnerTable.TableFormat.Paddings.HasKey(2))
							{
								num2 = 3;
								continue;
							}
							num = 0f;
							num2 = 0;
							continue;
						case 5:
							num2 = 13;
							continue;
						case 6:
							goto IL_141;
						case 7:
							A_0 = num;
							num2 = 11;
							continue;
						case 8:
							num2 = 1;
							continue;
						case 9:
							if (true)
							{
							}
							num = tableCell.OwnerRow.RowFormat.Paddings.Top;
							num2 = 6;
							continue;
						case 10:
							if (tableCell.OwnerRow.RowFormat.Paddings.HasKey(2))
							{
								num2 = 9;
								continue;
							}
							num2 = 4;
							continue;
						case 11:
							return;
						case 12:
							if (num > A_0)
							{
								goto IL_154;
							}
							return;
						case 13:
							if (!tableCell.CellFormat.Paddings.HasValue(2))
							{
								num2 = 15;
								continue;
							}
							goto IL_141;
						case 14:
							goto IL_141;
						case 15:
							goto IL_1F1;
						}
						break;
						IL_141:
						num2 = 12;
						continue;
						IL_154:
						num2 = 7;
						continue;
						IL_1F1:
						num2 = 10;
					}
				}
			}

			// Token: 0x06000665 RID: 1637 RVA: 0x00046BE4 File Offset: 0x00045BE4
			private float ᜀ()
			{
				float result;
				for (;;)
				{
					result = this.ᜀ.CellFormat.Paddings.Bottom;
					int num = 17;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							if (this.ᜀ.GetCellIndex() != num2)
							{
								num = 4;
								continue;
							}
							goto IL_138;
						case 1:
							goto IL_17C;
						case 2:
							goto IL_17C;
						case 3:
							result = this.ᜀ.OwnerRow.RowFormat.Paddings.Bottom;
							num = 12;
							continue;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_A2;
							default:
								if (false)
								{
								}
								this.ᜀ(ref result, num2);
								num = 19;
								continue;
							}
							break;
						case 5:
							if (true)
							{
							}
							result = this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.Bottom;
							num = 2;
							continue;
						case 6:
							goto IL_FD;
						case 7:
							if (this.ᜀ.OwnerRow.OwnerTable.TableFormat.Paddings.HasKey(3))
							{
								num = 5;
								continue;
							}
							result = 0f;
							num = 1;
							continue;
						case 8:
							goto IL_18B;
						case 9:
							return result;
						case 10:
							if (num2 >= this.ᜀ.OwnerRow.Cells.Count)
							{
								num = 9;
								continue;
							}
							goto IL_A2;
						case 11:
							goto IL_18B;
						case 12:
							goto IL_17C;
						case 13:
							num = 14;
							continue;
						case 14:
							if (!this.ᜀ.CellFormat.Paddings.HasValue(3))
							{
								num = 6;
								continue;
							}
							goto IL_17C;
						case 15:
							num = 18;
							continue;
						case 16:
							if (this.ᜀ.OwnerRow.RowFormat.Paddings.HasKey(3))
							{
								num = 3;
								continue;
							}
							num = 7;
							continue;
						case 17:
							if (this.ᜀ.CellFormat.Paddings.Bottom != -0.05f)
							{
								num = 15;
								continue;
							}
							goto IL_FD;
						case 18:
							if (this.ᜀ.CellFormat.Paddings.Bottom == 0f)
							{
								num = 13;
								continue;
							}
							goto IL_17C;
						case 19:
							goto IL_138;
						}
						break;
						IL_A2:
						num = 0;
						continue;
						IL_FD:
						num = 16;
						continue;
						IL_138:
						num2++;
						num = 11;
						continue;
						IL_17C:
						num2 = 0;
						num = 8;
						continue;
						IL_18B:
						num = 10;
					}
				}
				return result;
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x00046EB0 File Offset: 0x00045EB0
			private void ᜀ(ref float A_0, int A_1)
			{
				for (;;)
				{
					IL_48:
					TableCell tableCell = this.ᜀ.OwnerRow.Cells[A_1];
					float num = tableCell.CellFormat.Paddings.Bottom;
					for (;;)
					{
						IL_70:
						int num2 = 9;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_70;
								default:
									if (false)
									{
									}
									num = tableCell.OwnerRow.OwnerTable.TableFormat.Paddings.Bottom;
									num2 = 7;
									continue;
								}
								break;
							case 1:
								return;
							case 2:
								if (true)
								{
								}
								if (tableCell.OwnerRow.OwnerTable.TableFormat.Paddings.HasKey(3))
								{
									num2 = 0;
									continue;
								}
								num = 0f;
								num2 = 4;
								continue;
							case 3:
								goto IL_1FF;
							case 4:
								goto IL_12D;
							case 5:
								if (!tableCell.CellFormat.Paddings.HasValue(3))
								{
									num2 = 3;
									continue;
								}
								goto IL_12D;
							case 6:
								A_0 = num;
								num2 = 1;
								continue;
							case 7:
								goto IL_12D;
							case 8:
								num = tableCell.OwnerRow.RowFormat.Paddings.Bottom;
								num2 = 14;
								continue;
							case 9:
								if (tableCell.CellFormat.Paddings.Bottom != -0.05f)
								{
									num2 = 12;
									continue;
								}
								goto IL_1FF;
							case 10:
								if (tableCell.CellFormat.Paddings.Bottom == 0f)
								{
									num2 = 13;
									continue;
								}
								goto IL_12D;
							case 11:
								if (num > A_0)
								{
									num2 = 6;
									continue;
								}
								return;
							case 12:
								num2 = 10;
								continue;
							case 13:
								num2 = 5;
								continue;
							case 14:
								goto IL_12D;
							case 15:
								if (tableCell.OwnerRow.RowFormat.Paddings.HasKey(3))
								{
									num2 = 8;
									continue;
								}
								num2 = 2;
								continue;
							}
							goto IL_48;
							IL_12D:
							num2 = 11;
							continue;
							IL_1FF:
							num2 = 15;
						}
					}
				}
			}

			// Token: 0x04000DB6 RID: 3510
			private TableCell ᜀ;

			// Token: 0x04000DB7 RID: 3511
			private bool ᜁ;

			// Token: 0x04000DB8 RID: 3512
			private bool ᜂ;

			// Token: 0x04000DB9 RID: 3513
			private bool ᜃ;

			// Token: 0x04000DBA RID: 3514
			private bool ᜄ;
		}
	}
}
