using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc.Fields
{
	// Token: 0x02000516 RID: 1302
	public class TextBox : ParagraphBase, ITextBox, spr\u1AB8
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x003F0A04 File Offset: 0x003EFA04
		public DocumentObjectCollection ChildObjects
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
				return this.m_textBody.ChildObjects;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06004348 RID: 17224 RVA: 0x003F0A4C File Offset: 0x003EFA4C
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
				return DocumentObjectType.TextBox;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x003F0A8C File Offset: 0x003EFA8C
		// (set) Token: 0x0600434A RID: 17226 RVA: 0x003F0AD0 File Offset: 0x003EFAD0
		public TextBoxFormat Format
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
				return this.m_txbxFormat;
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
				this.m_txbxFormat = value;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x003F0B14 File Offset: 0x003EFB14
		public Body Body
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
				return this.m_textBody;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x0600434C RID: 17228 RVA: 0x003F0B58 File Offset: 0x003EFB58
		// (set) Token: 0x0600434D RID: 17229 RVA: 0x003F0B9C File Offset: 0x003EFB9C
		internal int Spid
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜀ = value;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x0600434E RID: 17230 RVA: 0x003F0BE0 File Offset: 0x003EFBE0
		internal CharacterFormat CharacterFormat
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
				return this.m_charFormat;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x0600434F RID: 17231 RVA: 0x003F0C24 File Offset: 0x003EFC24
		// (set) Token: 0x06004350 RID: 17232 RVA: 0x003F0C68 File Offset: 0x003EFC68
		internal spr\u248F ShapeInfo
		{
			[CompilerGenerated]
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
			[CompilerGenerated]
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
				this.ᜂ = value;
			}
		}

		// Token: 0x06004351 RID: 17233 RVA: 0x003F0CAC File Offset: 0x003EFCAC
		public TextBox(IDocument doc) : base((Document)doc)
		{
			this.m_charFormat = new CharacterFormat(doc);
			this.m_charFormat.ᜀ(this);
			this.m_txbxFormat = new TextBoxFormat();
			this.m_txbxFormat.ᜀ(this);
			this.m_textBody = new Body(base.Document, this);
		}

		// Token: 0x06004352 RID: 17234 RVA: 0x003F0D08 File Offset: 0x003EFD08
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			for (;;)
			{
				this.Body.CloneRelationsTo(doc, nextOwner);
				int num = 4;
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
							goto IL_D1;
						default:
							if (false)
							{
							}
							goto IL_5D;
						}
						break;
					case 1:
						goto IL_71;
					case 2:
						num = 3;
						continue;
					case 3:
						if (!(nextOwner.OwnerBase is HeaderFooter))
						{
							num = 5;
							continue;
						}
						goto IL_5D;
					case 4:
						if (nextOwner.OwnerBase != null)
						{
							num = 2;
							continue;
						}
						goto IL_73;
					case 5:
						goto IL_73;
					case 6:
						if (nextOwner is HeaderFooter)
						{
							num = 0;
							continue;
						}
						goto IL_D1;
					}
					break;
					IL_5D:
					this.Format.IsHeaderTextBox = true;
					num = 1;
					continue;
					IL_73:
					num = 6;
				}
			}
			IL_71:
			IL_D1:
			base.Document.ᜀ(doc, this);
			this.ᜁ = false;
		}

		// Token: 0x06004353 RID: 17235 RVA: 0x003F0DFC File Offset: 0x003EFDFC
		protected override object CloneImpl()
		{
			TextBox textBox;
			for (;;)
			{
				textBox = (TextBox)base.CloneImpl();
				textBox.m_textBody = (Body)this.Body.Clone();
				int num = 0;
				int count = textBox.m_textBody.Items.Count;
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
							goto IL_6B;
						default:
							goto IL_B8;
						}
						break;
					case 1:
						if (num >= count)
						{
							goto IL_6B;
						}
						textBox.m_textBody.Items[num].ᜀ(textBox.m_textBody);
						num++;
						num2 = 3;
						continue;
					case 2:
						if (true)
						{
						}
						goto IL_5F;
					case 3:
						goto IL_5F;
					}
					break;
					IL_5F:
					num2 = 1;
					continue;
					IL_6B:
					num2 = 0;
				}
			}
			IL_B8:
			if (false)
			{
			}
			textBox.m_txbxFormat = this.Format.Clone();
			textBox.m_textBody.ᜀ(textBox);
			textBox.m_txbxFormat.ᜀ(textBox);
			textBox.ᜁ = true;
			return textBox;
		}

		// Token: 0x06004354 RID: 17236 RVA: 0x003F0F04 File Offset: 0x003EFF04
		protected override void CreateLayoutInfo()
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
			this.ᜀ = new spr\u22A8(ChildrenLayoutDirection.Horizontal);
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x003F0F4C File Offset: 0x003EFF4C
		internal override void Attach(Paragraph owner, int itemPos)
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
			base.Attach(owner, itemPos);
			base.Document.TextBoxes.ᜁ(this);
		}

		// Token: 0x06004356 RID: 17238 RVA: 0x003F0FA0 File Offset: 0x003EFFA0
		internal override void Detach()
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
			base.Document.TextBoxes.ᜀ(this);
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x003F0FEC File Offset: 0x003EFFEC
		internal BodyRegion ᜄ()
		{
			if (true)
			{
			}
			if (base.OwnerParagraph != null)
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
				return base.OwnerParagraph.GetNextTextBodyItem();
			}
			return null;
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x003F1040 File Offset: 0x003F0040
		internal override void Close()
		{
			for (;;)
			{
				base.Close();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4E;
					case 1:
						if (this.m_textBody != null)
						{
							num = 2;
							continue;
						}
						goto IL_50;
					case 2:
						this.m_textBody.ᜅ();
						this.m_textBody = null;
						num = 0;
						continue;
					}
					break;
				}
			}
			IL_4E:
			IL_50:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4E;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				this.m_txbxFormat = null;
				return;
			}
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x003F10D4 File Offset: 0x003F00D4
		internal new void ᜀ(Body A_0)
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
			this.m_textBody = A_0;
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x003F1118 File Offset: 0x003F0118
		internal new Table ᜀ()
		{
			switch (0)
			{
			default:
				for (;;)
				{
					float num = 0f;
					float num2 = 0f;
					float num3 = 0f;
					float num4 = 0f;
					float num5 = 0f;
					Color backColor = default(Color);
					this.ᜁ = new Table(base.Document);
					this.ᜁ.ResetCells(1, 1);
					this.ᜁ.Rows[0].Cells[0].CellFormat.TextDirection = this.Format.TextDirection;
					backColor = this.Format.FillColor;
					this.ᜁ.ᜀ(base.Owner);
					float num6 = 0f;
					float num7 = 0f;
					float num8 = 0f;
					float num9 = 0f;
					bool flag = true;
					Section section = new Section(base.Document);
					int num10 = 96;
					for (;;)
					{
						DocumentObject documentObject;
						BodyRegion bodyRegion;
						Paragraph paragraph;
						HorizontalOrigin horizontalOrigin;
						TableCell tableCell;
						int num11;
						int num12;
						int count2;
						TableRow tableRow;
						switch (num10)
						{
						case 0:
							if (this.Format.TextWrappingStyle == TextWrappingStyle.InFrontOfText)
							{
								num10 = 123;
								continue;
							}
							backColor = Color.Transparent;
							num10 = 37;
							continue;
						case 1:
							goto IL_16D1;
						case 2:
							if (Math.Abs(this.Format.HorizontalRelativePercent) <= 1000f)
							{
								num10 = 41;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.HorizPosition = -(this.Format.Width + this.Format.InternalMargin.ᜃ());
							num10 = 143;
							continue;
						case 3:
							goto IL_1CD4;
						case 4:
							goto IL_1B7D;
						case 5:
							goto IL_1736;
						case 6:
							goto IL_16D1;
						case 7:
							this.ᜁ.TableFormat.Positioning.VertPosition = this.Format.VerticalPosition;
							num10 = 182;
							continue;
						case 8:
							goto IL_1A3C;
						case 9:
							if (this.ᜁ.\u1712.TextWrappingStyle != TextWrappingStyle.InFrontOfText)
							{
								num10 = 68;
								continue;
							}
							goto IL_20FC;
						case 10:
							goto IL_1B7D;
						case 11:
							goto IL_1CB5;
						case 12:
							if (Math.Abs(this.Format.VerticalRelativePercent) <= 1000f)
							{
								num10 = 61;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.VertPosition = num9 - this.Format.Height - num3 - num2;
							num10 = 55;
							continue;
						case 13:
							goto IL_16D1;
						case 14:
							goto IL_62B;
						case 15:
							goto IL_1C42;
						case 16:
							goto IL_1B7D;
						case 17:
							if (Math.Abs(this.Format.HorizontalRelativePercent) <= 1000f)
							{
								num10 = 135;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.HorizPosition = -(num - this.Format.InternalMargin.ᜄ());
							num10 = 42;
							continue;
						case 18:
							this.ᜁ.TableFormat.Positioning.HorizPosition = -(num / 2f) * (this.Format.HorizontalRelativePercent / 100f);
							num10 = 134;
							continue;
						case 19:
							num10 = 63;
							continue;
						case 20:
							this.ᜁ.Rows[0].HeightType = TableRowHeightType.AtLeast;
							num10 = 66;
							continue;
						case 21:
							goto IL_514;
						case 22:
							goto IL_1B7D;
						case 23:
							if (this.ᜁ.TableFormat.Positioning.VertPosition == 0f)
							{
								num10 = 166;
								continue;
							}
							goto IL_1B7D;
						case 24:
							num10 = 137;
							continue;
						case 25:
							if (section.Columns.Count > 1)
							{
								num10 = 180;
								continue;
							}
							goto IL_1237;
						case 26:
							if (Math.Abs(this.Format.VerticalRelativePercent) <= 1000f)
							{
								num10 = 144;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.VertPosition = this.Format.VerticalPosition;
							num10 = 38;
							continue;
						case 27:
							if (documentObject is sprờ)
							{
								num10 = 49;
								continue;
							}
							goto IL_E7B;
						case 28:
							goto IL_1B7D;
						case 29:
							goto IL_599;
						case 30:
						{
							IDocumentObject owner = base.Owner;
							num10 = 126;
							continue;
						}
						case 31:
							if (this.Format.FillEfects.Type == BackgroundType.Gradient)
							{
								num10 = 91;
								continue;
							}
							goto IL_1736;
						case 32:
							goto IL_1B7D;
						case 33:
							if (Math.Abs(this.Format.VerticalRelativePercent) <= 1000f)
							{
								num10 = 128;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.VertPosition = (num9 - this.Format.Height) / 2f;
							num10 = 67;
							continue;
						case 34:
							goto IL_19B8;
						case 35:
							goto IL_1736;
						case 36:
							if (this.Format.HorizontalOrigin == HorizontalOrigin.Column)
							{
								num10 = 148;
								continue;
							}
							goto IL_4E2;
						case 37:
							goto IL_1736;
						case 38:
							goto IL_16D1;
						case 39:
							goto IL_16D1;
						case 40:
						{
							ShapeVerticalAlignment verticalAlignment;
							switch (verticalAlignment)
							{
							case ShapeVerticalAlignment.None:
								num10 = 26;
								continue;
							case ShapeVerticalAlignment.Top:
								this.ᜁ.TableFormat.Positioning.VertPosition -= this.Format.InternalMargin.ᜂ();
								num10 = 57;
								continue;
							case ShapeVerticalAlignment.Center:
								this.ᜁ.TableFormat.Positioning.VertPosition = (num7 - this.Format.Height) / 2f;
								num10 = 6;
								continue;
							case ShapeVerticalAlignment.Bottom:
								this.ᜁ.TableFormat.Positioning.VertPosition = num7 - this.Format.Height - this.Format.InternalMargin.ᜀ();
								num10 = 50;
								continue;
							default:
								num10 = 76;
								continue;
							}
							break;
						}
						case 41:
							this.ᜁ.TableFormat.Positioning.HorizPosition = -(num - this.Format.InternalMargin.ᜃ()) * (this.Format.HorizontalRelativePercent / 100f);
							num10 = 130;
							continue;
						case 42:
							goto IL_1B7D;
						case 43:
							if (this.Format.IsAllowInCell)
							{
								num10 = 52;
								continue;
							}
							goto IL_14A8;
						case 44:
						{
							VerticalOrigin verticalOrigin;
							switch (verticalOrigin)
							{
							case VerticalOrigin.Margin:
							{
								this.ᜁ.TableFormat.Positioning.VertRelationTo = VerticalRelation.Margin;
								ShapeVerticalAlignment verticalAlignment2 = this.Format.VerticalAlignment;
								num10 = 58;
								continue;
							}
							case VerticalOrigin.Page:
							{
								this.ᜁ.TableFormat.Positioning.VertRelationTo = VerticalRelation.Page;
								ShapeVerticalAlignment verticalAlignment = this.Format.VerticalAlignment;
								num10 = 40;
								continue;
							}
							case VerticalOrigin.Paragraph:
							case VerticalOrigin.Line:
								this.ᜁ.TableFormat.Positioning.VertRelationTo = VerticalRelation.Paragraph;
								this.ᜁ.TableFormat.Positioning.VertPosition = this.Format.VerticalPosition;
								num10 = 1;
								continue;
							default:
								num10 = 78;
								continue;
							}
							break;
						}
						case 45:
							if (Math.Abs(this.Format.VerticalRelativePercent) <= 1000f)
							{
								num10 = 162;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.VertPosition = this.Format.VerticalPosition;
							num10 = 73;
							continue;
						case 46:
							goto IL_1CB5;
						case 47:
							if (this.ShapeInfo != null)
							{
								num10 = 106;
								continue;
							}
							goto IL_19B8;
						case 48:
							if (bodyRegion.DocumentObjectType == DocumentObjectType.Paragraph)
							{
								num10 = 147;
								continue;
							}
							goto IL_70E;
						case 49:
						{
							IEnumerator enumerator = (documentObject as sprờ).ᜇ().ᜂ().GetEnumerator();
							num10 = 53;
							continue;
						}
						case 50:
							goto IL_16D1;
						case 51:
							this.ᜁ.TableFormat.Positioning.HorizPosition = num6 * (this.Format.HorizontalRelativePercent / 100f);
							num10 = 163;
							continue;
						case 52:
							num10 = 71;
							continue;
						case 53:
							try
							{
								num10 = 3;
								for (;;)
								{
									switch (num10)
									{
									case 0:
										goto IL_E2D;
									case 1:
									{
										IEnumerator enumerator;
										if (!enumerator.MoveNext())
										{
											num10 = 2;
											continue;
										}
										DocumentObject documentObject2 = (DocumentObject)enumerator.Current;
										paragraph.Items.Add(documentObject2.Clone());
										num10 = 4;
										continue;
									}
									case 2:
										num10 = 0;
										continue;
									}
									IL_E07:
									num10 = 1;
									continue;
									goto IL_E07;
								}
								IL_E2D:
								goto IL_A8F;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator;
									IDisposable disposable = enumerator as IDisposable;
									num10 = 1;
									for (;;)
									{
										switch (num10)
										{
										case 0:
											disposable.Dispose();
											num10 = 2;
											continue;
										case 1:
											if (disposable != null)
											{
												num10 = 0;
												continue;
											}
											goto IL_E7A;
										case 2:
											goto IL_E78;
										}
										break;
									}
								}
								IL_E78:
								IL_E7A:;
							}
							goto IL_E7B;
							IL_A8F:
							paragraph.ᜋ = false;
							num10 = 29;
							continue;
						case 54:
							if (Math.Abs(this.Format.VerticalRelativePercent) <= 1000f)
							{
								num10 = 118;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.VertPosition = this.Format.VerticalPosition - this.Format.InternalMargin.ᜂ() + num2;
							num10 = 13;
							continue;
						case 55:
							goto IL_16D1;
						case 56:
							goto IL_1B7D;
						case 57:
							goto IL_16D1;
						case 58:
						{
							ShapeVerticalAlignment verticalAlignment2;
							switch (verticalAlignment2)
							{
							case ShapeVerticalAlignment.None:
								num10 = 45;
								continue;
							case ShapeVerticalAlignment.Top:
								num10 = 54;
								continue;
							case ShapeVerticalAlignment.Center:
								num10 = 33;
								continue;
							case ShapeVerticalAlignment.Bottom:
								num10 = 12;
								continue;
							default:
								num10 = 109;
								continue;
							}
							break;
						}
						case 59:
							goto IL_1B7D;
						case 60:
							num10 = 125;
							continue;
						case 61:
							this.ᜁ.TableFormat.Positioning.VertPosition = (num9 - this.Format.InternalMargin.ᜀ() - num3) * (this.Format.VerticalRelativePercent / 100f);
							num10 = 86;
							continue;
						case 62:
							if (this.Format.FillEfects.Type == BackgroundType.NoBackground)
							{
								num10 = 119;
								continue;
							}
							num10 = 31;
							continue;
						case 63:
							if (this.Format.IsInShape)
							{
								num10 = 152;
								continue;
							}
							goto IL_19B8;
						case 64:
						{
							ShapeHorizontalAlignment horizontalAlignment;
							switch (horizontalAlignment)
							{
							case ShapeHorizontalAlignment.Left:
								num10 = 17;
								continue;
							case ShapeHorizontalAlignment.Center:
								num10 = 83;
								continue;
							case ShapeHorizontalAlignment.Right:
								num10 = 2;
								continue;
							default:
								num10 = 172;
								continue;
							}
							break;
						}
						case 65:
							switch (horizontalOrigin)
							{
							case HorizontalOrigin.Margin:
							{
								this.ᜁ.TableFormat.Positioning.HorizRelationTo = HorizontalRelation.Margin;
								ShapeHorizontalAlignment horizontalAlignment2 = this.Format.HorizontalAlignment;
								num10 = 112;
								continue;
							}
							case HorizontalOrigin.Page:
							{
								this.ᜁ.TableFormat.Positioning.HorizRelationTo = HorizontalRelation.Page;
								ShapeHorizontalAlignment horizontalAlignment3 = this.Format.HorizontalAlignment;
								num10 = 104;
								continue;
							}
							case HorizontalOrigin.Column:
							{
								this.ᜁ.TableFormat.Positioning.HorizRelationTo = HorizontalRelation.Column;
								ShapeHorizontalAlignment horizontalAlignment4 = this.Format.HorizontalAlignment;
								num10 = 107;
								continue;
							}
							case HorizontalOrigin.Character:
								goto IL_1A3C;
							case HorizontalOrigin.LeftMarginArea:
							{
								this.ᜁ.TableFormat.Positioning.HorizRelationTo = HorizontalRelation.Margin;
								ShapeHorizontalAlignment horizontalAlignment = this.Format.HorizontalAlignment;
								num10 = 64;
								continue;
							}
							default:
								num10 = 150;
								continue;
							}
							break;
						case 66:
							goto IL_B8A;
						case 67:
							goto IL_16D1;
						case 68:
						{
							DocumentObject owner2 = base.Owner;
							num10 = 11;
							continue;
						}
						case 69:
							if (!this.ᜁ.IsTextBoxInTable)
							{
								num10 = 74;
								continue;
							}
							goto IL_4E2;
						case 70:
							goto IL_733;
						case 71:
						{
							IDocumentObject owner;
							if (!(owner as Table).IsSDTTable)
							{
								num10 = 75;
								continue;
							}
							goto IL_14A8;
						}
						case 72:
							num10 = 9;
							continue;
						case 73:
							goto IL_16D1;
						case 74:
							this.ᜁ.TableFormat.Positioning.HorizPosition += num;
							this.ᜁ.TableFormat.Positioning.HorizRelationTo = HorizontalRelation.Page;
							num10 = 115;
							continue;
						case 75:
							this.ᜁ.IsTextBoxInTable = true;
							num10 = 136;
							continue;
						case 76:
							num10 = 122;
							continue;
						case 77:
							goto IL_12AF;
						case 78:
							num10 = 24;
							continue;
						case 79:
							tableCell.Items.Add(paragraph);
							num10 = 21;
							continue;
						case 80:
							this.ᜁ.\u1712.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
							num10 = 185;
							continue;
						case 81:
							goto IL_1B7D;
						case 82:
							this.ᜁ.TableFormat.Positioning.HorizPosition = (num - this.Format.InternalMargin.ᜄ()) * (this.Format.HorizontalRelativePercent / 100f);
							num10 = 145;
							continue;
						case 83:
							if (Math.Abs(this.Format.HorizontalRelativePercent) <= 1000f)
							{
								num10 = 18;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.HorizPosition = -(num - this.Format.Width) / 2f;
							num10 = 4;
							continue;
						case 84:
						{
							int count;
							if (num11 >= count)
							{
								num10 = 79;
								continue;
							}
							documentObject = (bodyRegion as Paragraph).Items[num11];
							num10 = 27;
							continue;
						}
						case 85:
							num10 = 28;
							continue;
						case 86:
							goto IL_16D1;
						case 87:
							goto IL_514;
						case 88:
							goto IL_1B7D;
						case 89:
							goto IL_16D1;
						case 90:
							goto IL_1B7D;
						case 91:
							backColor = this.Format.FillEfects.Gradient.Color2;
							tableCell.CellFormat.TextureStyle = TextureStyle.Texture30Percent;
							num10 = 5;
							continue;
						case 92:
							goto IL_1B7D;
						case 93:
							this.ᜁ.TableFormat.Positioning.HorizPosition = (num8 - this.Format.InternalMargin.ᜃ()) * (this.Format.HorizontalRelativePercent / 100f);
							num10 = 97;
							continue;
						case 94:
							goto IL_1B7D;
						case 95:
							if ((bodyRegion as Paragraph).HasSDTInlineItem)
							{
								num10 = 164;
								continue;
							}
							goto IL_70E;
						case 96:
							if (base.Owner != null)
							{
								num10 = 30;
								continue;
							}
							goto IL_1237;
						case 97:
							goto IL_1B7D;
						case 98:
							this.ᜁ.TableFormat.Positioning.HorizPosition = num8 * (this.Format.HorizontalRelativePercent / 100f);
							num10 = 165;
							continue;
						case 99:
						{
							IDocumentObject owner;
							if (owner is Table)
							{
								num10 = 102;
								continue;
							}
							goto IL_14A8;
						}
						case 100:
							num10 = 117;
							continue;
						case 101:
							goto IL_1B7D;
						case 102:
							num10 = 43;
							continue;
						case 103:
							goto IL_198A;
						case 104:
						{
							ShapeHorizontalAlignment horizontalAlignment3;
							switch (horizontalAlignment3)
							{
							case ShapeHorizontalAlignment.None:
								num10 = 167;
								continue;
							case ShapeHorizontalAlignment.Left:
								this.ᜁ.TableFormat.Positioning.HorizPosition -= this.Format.InternalMargin.ᜄ();
								num10 = 101;
								continue;
							case ShapeHorizontalAlignment.Center:
								this.ᜁ.TableFormat.Positioning.HorizPosition = (num6 - this.Format.Width) / 2f;
								num10 = 168;
								continue;
							case ShapeHorizontalAlignment.Right:
								this.ᜁ.TableFormat.Positioning.HorizPosition = num6 - this.Format.Width - this.Format.InternalMargin.ᜃ();
								num10 = 22;
								continue;
							default:
								num10 = 121;
								continue;
							}
							break;
						}
						case 105:
						{
							IDocumentObject owner;
							if (owner is Section)
							{
								num10 = 111;
								continue;
							}
							num10 = 99;
							continue;
						}
						case 106:
							this.Format.StartPoint = this.ShapeInfo.\u1714();
							num10 = 34;
							continue;
						case 107:
						{
							ShapeHorizontalAlignment horizontalAlignment4;
							switch (horizontalAlignment4)
							{
							case ShapeHorizontalAlignment.None:
								this.ᜁ.TableFormat.Positioning.HorizPosition = this.Format.HorizontalPosition;
								num10 = 173;
								continue;
							case ShapeHorizontalAlignment.Left:
								this.ᜁ.TableFormat.Positioning.HorizPosition = this.ᜁ.TableFormat.LeftIndent - this.Format.InternalMargin.ᜄ();
								num10 = 32;
								continue;
							case ShapeHorizontalAlignment.Center:
								this.ᜁ.TableFormat.Positioning.HorizPosition = (num8 - this.Format.Width) / 2f;
								num10 = 10;
								continue;
							case ShapeHorizontalAlignment.Right:
								this.ᜁ.TableFormat.Positioning.HorizPosition = num8 - this.Format.Width - this.Format.InternalMargin.ᜃ();
								num10 = 141;
								continue;
							default:
								num10 = 85;
								continue;
							}
							break;
						}
						case 108:
							goto IL_12AF;
						case 109:
							num10 = 139;
							continue;
						case 110:
							goto IL_163D;
						case 111:
							goto IL_1C19;
						case 112:
						{
							ShapeHorizontalAlignment horizontalAlignment2;
							switch (horizontalAlignment2)
							{
							case ShapeHorizontalAlignment.None:
								num10 = 179;
								continue;
							case ShapeHorizontalAlignment.Left:
								num10 = 127;
								continue;
							case ShapeHorizontalAlignment.Center:
								num10 = 149;
								continue;
							case ShapeHorizontalAlignment.Right:
								num10 = 175;
								continue;
							default:
								num10 = 14;
								continue;
							}
							break;
						}
						case 113:
							if (this.Format.IsFitTextToShape)
							{
								num10 = 20;
								continue;
							}
							this.ᜁ.Rows[0].HeightType = TableRowHeightType.Exactly;
							num10 = 131;
							continue;
						case 114:
							goto IL_1B7D;
						case 115:
							goto IL_4E2;
						case 116:
							this.ᜁ.TableFormat.Borders.BorderType = BorderStyle.None;
							num10 = 184;
							continue;
						case 117:
							if (this.Format.IsInShape)
							{
								num10 = 116;
								continue;
							}
							goto IL_D85;
						case 118:
							this.ᜁ.TableFormat.Positioning.VertPosition = (num2 - this.Format.InternalMargin.ᜂ()) * (this.Format.VerticalRelativePercent / 100f);
							num10 = 39;
							continue;
						case 119:
							num10 = 0;
							continue;
						case 120:
							goto IL_163D;
						case 121:
							num10 = 142;
							continue;
						case 122:
							goto IL_16D1;
						case 123:
							backColor = this.Format.FillColor;
							num10 = 35;
							continue;
						case 124:
							goto IL_16D1;
						case 125:
							if (this.Format.NoLine)
							{
								num10 = 70;
								continue;
							}
							goto IL_1C42;
						case 126:
							goto IL_1B1C;
						case 127:
							if (Math.Abs(this.Format.HorizontalRelativePercent) <= 1000f)
							{
								num10 = 82;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.HorizPosition = this.ᜁ.TableFormat.LeftIndent - this.Format.InternalMargin.ᜄ();
							num10 = 92;
							continue;
						case 128:
							this.ᜁ.TableFormat.Positioning.VertPosition = num9 / 2f * (this.Format.VerticalRelativePercent / 100f);
							num10 = 89;
							continue;
						case 129:
							num10 = 174;
							continue;
						case 130:
							goto IL_1B7D;
						case 131:
							goto IL_B8A;
						case 132:
							if (num12 >= count2)
							{
								num10 = 19;
								continue;
							}
							bodyRegion = this.Body.Items[num12];
							num10 = 48;
							continue;
						case 133:
						{
							IDocumentObject owner;
							if (owner.Owner != null)
							{
								num10 = 170;
								continue;
							}
							goto IL_1C19;
						}
						case 134:
							goto IL_1B7D;
						case 135:
							this.ᜁ.TableFormat.Positioning.HorizPosition = -(num - this.Format.InternalMargin.ᜄ()) * (this.Format.HorizontalRelativePercent / 100f);
							num10 = 114;
							continue;
						case 136:
							goto IL_1C19;
						case 137:
							if (this.ᜁ.TableFormat.Positioning.VertPosition == 0f)
							{
								num10 = 7;
								continue;
							}
							goto IL_16D1;
						case 138:
						{
							IDocumentObject owner;
							section = (owner as Section);
							num = section.PageSetup.Margins.Left;
							num2 = section.PageSetup.Margins.Top;
							num3 = section.PageSetup.Margins.Bottom;
							num7 = section.PageSetup.PageSize.Height;
							num6 = section.PageSetup.PageSize.Width;
							num8 = section.PageSetup.ClientWidth;
							num9 = section.PageSetup.PageSize.Height - (num4 + num5);
							num5 = section.PageSetup.FooterDistance;
							num4 = section.PageSetup.HeaderDistance;
							num10 = 25;
							continue;
						}
						case 139:
							goto IL_16D1;
						case 140:
							goto IL_1237;
						case 141:
							goto IL_1B7D;
						case 142:
							goto IL_1B7D;
						case 143:
							goto IL_1B7D;
						case 144:
							this.ᜁ.TableFormat.Positioning.VertPosition = num7 * (this.Format.VerticalRelativePercent / 100f);
							num10 = 155;
							continue;
						case 145:
							goto IL_1B7D;
						case 146:
							if (this.Format.LineWidth >= 1f)
							{
								num10 = 60;
								continue;
							}
							goto IL_733;
						case 147:
							num10 = 95;
							continue;
						case 148:
							num10 = 153;
							continue;
						case 149:
							if (Math.Abs(this.Format.HorizontalRelativePercent) <= 1000f)
							{
								num10 = 157;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.HorizPosition = (num8 - this.Format.Width) / 2f;
							num10 = 56;
							continue;
						case 150:
							num10 = 8;
							continue;
						case 151:
							goto IL_1B1C;
						case 152:
							num10 = 47;
							continue;
						case 153:
							if (flag)
							{
								num10 = 178;
								continue;
							}
							goto IL_4E2;
						case 154:
							goto IL_20BA;
						case 155:
							goto IL_16D1;
						case 156:
							if (this.Format.TextWrappingStyle != TextWrappingStyle.Inline)
							{
								num10 = 169;
								continue;
							}
							goto IL_198A;
						case 157:
							this.ᜁ.TableFormat.Positioning.HorizPosition = num8 / 2f * (this.Format.HorizontalRelativePercent / 100f);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_62B;
							default:
								if (false)
								{
								}
								num10 = 90;
								continue;
							}
							break;
						case 158:
						{
							IDocumentObject owner;
							if (owner is Section)
							{
								num10 = 138;
								continue;
							}
							goto IL_1237;
						}
						case 159:
							tableRow.RowFormat.Borders.LineWidth = this.Format.LineWidth;
							tableCell.CellFormat.Borders.LineWidth = this.Format.LineWidth;
							tableRow.RowFormat.Borders.Color = this.Format.LineColor;
							tableCell.CellFormat.Borders.Color = this.Format.LineColor;
							tableRow.RowFormat.Borders.BorderType = this.ᜀ(this.Format.LineStyle);
							tableCell.CellFormat.Borders.BorderType = this.ᜀ(this.Format.LineStyle);
							num10 = 77;
							continue;
						case 160:
						{
							DocumentObject owner2;
							if (owner2 == null)
							{
								num10 = 3;
								continue;
							}
							num10 = 171;
							continue;
						}
						case 161:
							if (!this.Format.NoLine)
							{
								num10 = 159;
								continue;
							}
							tableCell.CellFormat.Borders.BorderType = BorderStyle.None;
							tableRow.RowFormat.Borders.BorderType = BorderStyle.None;
							num10 = 108;
							continue;
						case 162:
							this.ᜁ.TableFormat.Positioning.VertPosition = num9 * (this.Format.VerticalRelativePercent / 100f);
							num10 = 124;
							continue;
						case 163:
							goto IL_1B7D;
						case 164:
						{
							paragraph = (bodyRegion.Clone() as Paragraph);
							paragraph.Items.Clear();
							num11 = 0;
							int count = (bodyRegion as Paragraph).Items.Count;
							num10 = 110;
							continue;
						}
						case 165:
							goto IL_1B7D;
						case 166:
							this.ᜁ.TableFormat.Positioning.VertPosition = this.Format.VerticalPosition;
							num10 = 88;
							continue;
						case 167:
							if (Math.Abs(this.Format.HorizontalRelativePercent) <= 1000f)
							{
								num10 = 51;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.HorizPosition = this.Format.HorizontalPosition;
							num10 = 59;
							continue;
						case 168:
							goto IL_1B7D;
						case 169:
						{
							VerticalOrigin verticalOrigin = this.Format.VerticalOrigin;
							num10 = 44;
							continue;
						}
						case 170:
						{
							IDocumentObject owner = owner.Owner;
							num10 = 151;
							continue;
						}
						case 171:
						{
							DocumentObject owner2;
							if (owner2 is HeaderFooter)
							{
								num10 = 80;
								continue;
							}
							owner2 = owner2.Owner;
							num10 = 46;
							continue;
						}
						case 172:
							num10 = 16;
							continue;
						case 173:
							goto IL_1B7D;
						case 174:
							if (this.ᜁ.\u1712.TextWrappingStyle != TextWrappingStyle.Behind)
							{
								num10 = 72;
								continue;
							}
							goto IL_20FC;
						case 175:
							if (Math.Abs(this.Format.HorizontalRelativePercent) <= 1000f)
							{
								num10 = 93;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.HorizPosition = num8 - this.Format.Width - this.Format.InternalMargin.ᜃ();
							num10 = 81;
							continue;
						case 176:
							if (this.Format.LineWidth == 0f)
							{
								num10 = 100;
								continue;
							}
							goto IL_D85;
						case 177:
							if (this.ᜁ.\u1712.TextWrappingStyle != TextWrappingStyle.Inline)
							{
								num10 = 129;
								continue;
							}
							goto IL_20FC;
						case 178:
							num10 = 69;
							continue;
						case 179:
							if (Math.Abs(this.Format.HorizontalRelativePercent) <= 1000f)
							{
								num10 = 98;
								continue;
							}
							this.ᜁ.TableFormat.Positioning.HorizPosition = this.Format.HorizontalPosition;
							num10 = 94;
							continue;
						case 180:
							flag = false;
							num10 = 140;
							continue;
						case 181:
							goto IL_20BA;
						case 182:
							goto IL_16D1;
						case 183:
							goto IL_1B7D;
						case 184:
							goto IL_D85;
						case 185:
							goto IL_1BBC;
						case 186:
							goto IL_599;
						}
						break;
						IL_4E2:
						num10 = 62;
						continue;
						IL_514:
						num12++;
						num10 = 181;
						continue;
						IL_599:
						num11++;
						num10 = 120;
						continue;
						IL_62B:
						num10 = 183;
						continue;
						IL_70E:
						tableCell.Items.Add(bodyRegion.Clone());
						num10 = 87;
						continue;
						IL_733:
						tableRow.RowFormat.Borders.BorderType = BorderStyle.None;
						num10 = 15;
						continue;
						IL_B8A:
						num10 = 146;
						continue;
						IL_D85:
						num10 = 113;
						continue;
						IL_E7B:
						paragraph.Items.Add(documentObject.Clone());
						num10 = 186;
						continue;
						IL_1237:
						tableRow = this.ᜁ.Rows[0];
						tableCell = tableRow.Cells[0];
						tableRow.Height = this.Format.Height;
						tableCell.CellFormat.TextDirection = this.Format.LayoutFlowAlt;
						num10 = 161;
						continue;
						IL_12AF:
						num10 = 156;
						continue;
						IL_14A8:
						num10 = 133;
						continue;
						IL_163D:
						num10 = 84;
						continue;
						IL_16D1:
						horizontalOrigin = this.Format.HorizontalOrigin;
						num10 = 65;
						continue;
						IL_1736:
						this.ᜁ.TableFormat.BackColor = backColor;
						this.ᜁ.TableFormat.Paddings.Left = this.Format.InternalMargin.ᜄ();
						this.ᜁ.TableFormat.Paddings.Right = this.Format.InternalMargin.ᜃ();
						this.ᜁ.TableFormat.Paddings.Top = this.Format.InternalMargin.ᜂ();
						this.ᜁ.TableFormat.Paddings.Bottom = this.Format.InternalMargin.ᜀ();
						tableCell.Width = this.Format.Width;
						tableCell.CellFormat.BackColor = backColor;
						tableCell.CellFormat.VerticalAlignment = this.ᜀ(this.Format.TextAnchor);
						this.ᜁ.TableFormat.Borders.LineWidth = this.Format.LineWidth;
						num10 = 176;
						continue;
						IL_198A:
						num10 = 36;
						continue;
						IL_19B8:
						this.ᜁ.IsTextBox = true;
						this.ᜁ.\u1712 = this.Format;
						num10 = 177;
						continue;
						IL_1A3C:
						num10 = 23;
						continue;
						IL_1B1C:
						num10 = 105;
						continue;
						IL_1B7D:
						this.ᜁ.TableFormat.WrapTextAround = true;
						num10 = 103;
						continue;
						IL_1C19:
						num10 = 158;
						continue;
						IL_1C42:
						num12 = 0;
						count2 = this.Body.Items.Count;
						num10 = 154;
						continue;
						IL_1CB5:
						num10 = 160;
						continue;
						IL_20BA:
						if (true)
						{
						}
						num10 = 132;
					}
				}
				IL_1BBC:
				IL_1CD4:
				IL_20FC:
				return this.ᜁ;
			}
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x003F3238 File Offset: 0x003F2238
		private new RowAlignment ᜀ(ShapeHorizontalAlignment A_0)
		{
			for (;;)
			{
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						return RowAlignment.Left;
					case 2:
						for (;;)
						{
							switch (A_0)
							{
							case ShapeHorizontalAlignment.Center:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									goto IL_62;
								}
								break;
							case ShapeHorizontalAlignment.Right:
								return RowAlignment.Right;
							}
							break;
						}
						num = 0;
						continue;
					}
					break;
				}
			}
			return RowAlignment.Right;
			IL_62:
			if (false)
			{
			}
			return RowAlignment.Center;
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x003F32BC File Offset: 0x003F22BC
		private new BorderStyle ᜀ(TextBoxLineStyle A_0)
		{
			for (;;)
			{
				for (;;)
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							switch (A_0)
							{
							case TextBoxLineStyle.Simple:
								return BorderStyle.Single;
							case TextBoxLineStyle.Double:
								return BorderStyle.Double;
							case TextBoxLineStyle.ThickThin:
								goto IL_68;
							case TextBoxLineStyle.ThinThick:
								return BorderStyle.ThinThickMediumGap;
							case TextBoxLineStyle.Triple:
								goto IL_5B;
							default:
								num = 1;
								continue;
							}
							break;
						case 1:
							num = 2;
							continue;
						case 2:
							return BorderStyle.None;
						}
						break;
					}
				}
				IL_68:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_7E;
				}
			}
			return BorderStyle.ThinThickMediumGap;
			IL_5B:
			if (true)
			{
			}
			return BorderStyle.Triple;
			IL_7E:
			if (false)
			{
			}
			return BorderStyle.ThickThinMediumGap;
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x003F3354 File Offset: 0x003F2354
		private new VerticalAlignment ᜀ(ShapeVerticalAlignment A_0)
		{
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						for (;;)
						{
							switch (A_0)
							{
							case ShapeVerticalAlignment.Center:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									goto IL_5A;
								}
								break;
							case ShapeVerticalAlignment.Bottom:
								return VerticalAlignment.Bottom;
							}
							break;
						}
						num = 1;
						continue;
					case 1:
						if (true)
						{
						}
						num = 2;
						continue;
					case 2:
						return VerticalAlignment.Top;
					}
					break;
				}
			}
			return VerticalAlignment.Bottom;
			IL_5A:
			if (false)
			{
			}
			return VerticalAlignment.Middle;
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x003F33D8 File Offset: 0x003F23D8
		protected override void InitXDLSHolder()
		{
			int a_ = 5;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.XDLSHolder.AddElement(ClipboardData.b("४ɬ୮ࡰ", a_), this.Body);
			base.XDLSHolder.AddElement(ClipboardData.b("Ὢ࡬ᝮհᅲᩴྲྀ呸ᵺቼൾ", a_), this.Format);
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x003F345C File Offset: 0x003F245C
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 0;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.WriteXmlAttributes(writer);
			writer.WriteValue(ClipboardData.b("ብᅧᩩ५", a_), ParagraphItemType.TextBox);
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06004360 RID: 17248 RVA: 0x003F34C4 File Offset: 0x003F24C4
		spr\u1D30 spr\u1AB8.LayoutInfo
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_62;
					case 1:
						goto IL_54;
					}
					if (this.ᜀ != null)
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
					IL_54:
					this.CreateLayoutInfo();
					num = 0;
				}
				IL_62:
				if (true)
				{
				}
				return this.ᜀ;
			}
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x003F3544 File Offset: 0x003F2544
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
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
		}

		// Token: 0x0400357C RID: 13692
		protected Body m_textBody;

		// Token: 0x0400357D RID: 13693
		protected TextBoxFormat m_txbxFormat;

		// Token: 0x0400357E RID: 13694
		private bool \u2460\u0083\u009A\u009A;

		// Token: 0x0400357F RID: 13695
		private long \u2460\u0085\u00A9\u009D;

		// Token: 0x04003580 RID: 13696
		private new int ᜀ;

		// Token: 0x04003581 RID: 13697
		private new Table ᜁ;

		// Token: 0x04003582 RID: 13698
		private float[] \u2460\u008A\u0081\u0094;

		// Token: 0x04003583 RID: 13699
		private bool \u2460\u00A3\u00AE\u0087;

		// Token: 0x04003584 RID: 13700
		[CompilerGenerated]
		private spr\u248F ᜂ;
	}
}
