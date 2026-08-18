using System;
using System.Collections;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

// Token: 0x02000144 RID: 324
internal class spr\u257C : spr\u2573
{
	// Token: 0x06000880 RID: 2176 RVA: 0x0005F19C File Offset: 0x0005E19C
	public spr\u2441 ᜑ()
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
		return this.\u1712().ᜂ();
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x0005F1E4 File Offset: 0x0005E1E4
	public spr\u1AE4 \u1712()
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
		return this.ᜂ as spr\u1AE4;
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0005F22C File Offset: 0x0005E22C
	public int ᜐ()
	{
		if (!this.ᜁ)
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
				return this.ᜃ;
			}
		}
		return this.ᜂ;
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0005F280 File Offset: 0x0005E280
	internal float \u1713()
	{
		float num;
		for (;;)
		{
			IL_14:
			num = (float)(((spr\u1AB8)this.ᜏ.Rows[0].Cells[0]).ᜀ().ᜊ().ᜃ() + ((spr\u1AB8)this.ᜏ.Rows[0].Cells[0]).ᜀ().ᜋ().ᜃ());
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_C1;
				case 1:
					if (this.ᜏ.TableFormat.CellSpacing > 0f)
					{
						num2 = 2;
						continue;
					}
					goto IL_C3;
				case 2:
					num += this.ᜏ.TableFormat.Borders.Left.LineWidth;
					num2 = 0;
					continue;
				}
				goto IL_14;
			}
			IL_C3:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_D9;
			}
			IL_C1:
			goto IL_C3;
		}
		IL_D9:
		if (true)
		{
		}
		if (false)
		{
		}
		return num;
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0005F380 File Offset: 0x0005E380
	public spr\u257C(sprᲲ A_0, sprᴉ A_1) : base(A_0.ᜄ(), A_1)
	{
		this.ᜁ = true;
		this.ᜃ = A_0.ᜀ() - 1;
		this.ᜎ = A_0;
		this.\u1712 = true;
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x0005F3D4 File Offset: 0x0005E3D4
	public spr\u257C(spr\u1AE4 A_0, sprᴉ A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0005F400 File Offset: 0x0005E400
	private new void ᜀ(Table A_0, MarginsF A_1, ref RectangleF A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 80;
			for (;;)
			{
				HorizontalRelation horizRelationTo;
				float y;
				float num3;
				float num4;
				switch (num)
				{
				case 0:
					goto IL_840;
				case 1:
					switch (horizRelationTo)
					{
					case HorizontalRelation.Column:
						num = 44;
						continue;
					case HorizontalRelation.Margin:
						num = 109;
						continue;
					case HorizontalRelation.Page:
						num = 56;
						continue;
					default:
						num = 34;
						continue;
					}
					break;
				case 2:
					num = 48;
					continue;
				case 3:
					if (this.ᜏ.TableFormat.Positioning.VertRelationTo == VerticalRelation.Page)
					{
						if (true)
						{
						}
						num = 31;
						continue;
					}
					return;
				case 4:
					if (!(A_0.Owner.Owner.Owner.Owner as Table).IsSDTTable)
					{
						num = 45;
						continue;
					}
					goto IL_31A;
				case 5:
				{
					Paragraph paragraph = A_0.\u1712.OwnerBase.OwnerBase as Paragraph;
					A_2.Y = (((spr\u1AB8)paragraph).ᜀ() as sprℐ).ᜦ() + A_0.TableFormat.Positioning.VertPosition;
					num = 36;
					continue;
				}
				case 6:
					num = 47;
					continue;
				case 7:
					goto IL_B9C;
				case 8:
					goto IL_840;
				case 9:
					num = 72;
					continue;
				case 10:
					num = 103;
					continue;
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1094;
					default:
						if (false)
						{
						}
						if (A_0.PreferredTableWidth.ᜀ() == FtsWidth.Percentage)
						{
							num = 108;
							continue;
						}
						num = 17;
						continue;
					}
					break;
				case 12:
					goto IL_B9C;
				case 13:
					if (!A_0.TextBoxFormat.IsInGroupShape)
					{
						num = 63;
						continue;
					}
					goto IL_A31;
				case 14:
					if (!(A_0.Owner.Owner.Owner as Table).IsSDTTable)
					{
						num = 118;
						continue;
					}
					goto IL_B47;
				case 15:
					if (A_0.IsTextBoxInTable)
					{
						num = 59;
						continue;
					}
					goto IL_91E;
				case 16:
					if (A_0.IsTextBox)
					{
						num = 81;
						continue;
					}
					goto IL_A31;
				case 17:
					if (A_0.Width != 0f)
					{
						num = 102;
						continue;
					}
					goto IL_B9C;
				case 18:
					goto IL_B9C;
				case 19:
					goto IL_840;
				case 20:
				{
					float num2 = A_2.X + A_0.TableFormat.Positioning.HorizPosition;
					num = 89;
					continue;
				}
				case 21:
					A_2.X += A_0.TableFormat.Positioning.HorizPosition;
					A_2.Width -= A_0.TableFormat.Positioning.HorizPosition;
					num = 7;
					continue;
				case 22:
					if (A_0.Owner is TableCell)
					{
						num = 76;
						continue;
					}
					goto IL_D2F;
				case 23:
					goto IL_B1A;
				case 24:
					num = 33;
					continue;
				case 25:
					num = 110;
					continue;
				case 26:
					goto IL_B9C;
				case 27:
					num = 4;
					continue;
				case 28:
					if (A_0.IsTextBox)
					{
						num = 5;
						continue;
					}
					A_2.Y += A_0.TableFormat.Positioning.VertPosition;
					A_2.Height -= A_0.TableFormat.Positioning.VertPosition;
					num = 96;
					continue;
				case 29:
					A_2.Height = (this.ᜆ as spr\u1DA4).ᜈ().Height;
					num = 119;
					continue;
				case 30:
					goto IL_B9C;
				case 31:
					num = 53;
					continue;
				case 32:
					num = 0;
					continue;
				case 33:
					if (!(A_0.Owner.Owner.Owner as Table).IsSDTTable)
					{
						num = 20;
						continue;
					}
					goto IL_505;
				case 34:
					num = 12;
					continue;
				case 35:
					if (y > A_2.Y)
					{
						num = 99;
						continue;
					}
					goto IL_B1A;
				case 36:
					goto IL_840;
				case 37:
					num = 40;
					continue;
				case 38:
					if (this.ᜏ.TableFormat.Positioning.VertPositionAbs != VerticalPosition.None)
					{
						num = 2;
						continue;
					}
					return;
				case 39:
					goto IL_532;
				case 40:
					if (A_0.TextBoxFormat.IsInGroupShape)
					{
						num = 116;
						continue;
					}
					goto IL_78F;
				case 41:
					A_2.X += A_0.TableFormat.Positioning.HorizPosition;
					A_2.Width -= A_0.TableFormat.Positioning.HorizPosition;
					num = 57;
					continue;
				case 42:
				{
					VerticalRelation vertRelationTo;
					switch (vertRelationTo)
					{
					case VerticalRelation.Margin:
					{
						VerticalPosition vertPositionAbs = A_0.TableFormat.Positioning.VertPositionAbs;
						num = 106;
						continue;
					}
					case VerticalRelation.Page:
					{
						VerticalPosition vertPositionAbs2 = A_0.TableFormat.Positioning.VertPositionAbs;
						num = 69;
						continue;
					}
					case VerticalRelation.Paragraph:
						num = 28;
						continue;
					default:
						num = 32;
						continue;
					}
					break;
				}
				case 43:
					goto IL_B9C;
				case 44:
					if (A_0.TableFormat.Positioning.HorizPosition != 0f)
					{
						num = 70;
						continue;
					}
					goto IL_B9C;
				case 45:
					A_2.Y = (A_0.Owner.Owner.Owner.Owner as Table).TableBounds.Y - A_1.Top + A_0.TableFormat.Positioning.VertPosition;
					num = 77;
					continue;
				case 46:
					if (A_0.IsTextBoxInTable)
					{
						num = 78;
						continue;
					}
					goto IL_31A;
				case 47:
					if (!(A_0.Owner.Owner.Owner as Table).IsSDTTable)
					{
						num = 100;
						continue;
					}
					goto IL_D57;
				case 48:
					if (this.ᜏ.TableFormat.Positioning.VertPosition == 0f)
					{
						num = 55;
						continue;
					}
					return;
				case 49:
					A_2.X -= this.\u1713();
					num = 18;
					continue;
				case 50:
					if (!(A_0.Owner is TableCell))
					{
						num = 9;
						continue;
					}
					num = 114;
					continue;
				case 51:
					if ((A_0.Owner.Owner.Owner as Table).IsSDTTable)
					{
						num = 79;
						continue;
					}
					goto IL_24A;
				case 52:
					if (!this.\u1712)
					{
						num = 83;
						continue;
					}
					goto IL_840;
				case 53:
					if (!(this.ᜏ.OwnerTextBody is TableCell))
					{
						num = 29;
						continue;
					}
					return;
				case 54:
					if ((A_0.Owner.Owner.Owner as Table).IsSDTTable)
					{
						num = 104;
						continue;
					}
					goto IL_D2F;
				case 55:
					num = 3;
					continue;
				case 56:
					if (A_0.Owner is TableCell)
					{
						num = 21;
						continue;
					}
					A_2.X = A_0.TableFormat.Positioning.HorizPosition;
					num = 43;
					continue;
				case 57:
					goto IL_B9C;
				case 58:
					if (A_0.Owner is TableCell)
					{
						num = 88;
						continue;
					}
					goto IL_24A;
				case 59:
					num = 67;
					continue;
				case 60:
					if (A_0.Owner is TableCell)
					{
						num = 65;
						continue;
					}
					goto IL_B47;
				case 61:
					A_2.Y = A_2.Y - A_1.Top + A_0.TableFormat.Positioning.VertPosition;
					num = 94;
					continue;
				case 62:
					if (this.ᜏ.OwnerSection != null)
					{
						num = 68;
						continue;
					}
					A_2.X = num3 + (A_2.Width - A_0.Width) / 2f;
					num = 98;
					continue;
				case 63:
					goto IL_E7C;
				case 64:
					if (A_0.IsTextBox)
					{
						num = 101;
						continue;
					}
					goto IL_31A;
				case 65:
					num = 14;
					continue;
				case 66:
					num = 95;
					continue;
				case 67:
					if (!(A_0.Owner.Owner.Owner.Owner as Table).IsSDTTable)
					{
						num = 61;
						continue;
					}
					goto IL_91E;
				case 68:
					A_2.X = num3 + (this.ᜏ.OwnerSection.PageSetup.ClientWidth - A_0.Width) / 2f;
					num = 91;
					continue;
				case 69:
				{
					VerticalPosition vertPositionAbs2;
					if (vertPositionAbs2 == VerticalPosition.None)
					{
						num = 111;
						continue;
					}
					goto IL_840;
				}
				case 70:
					A_2.X += A_0.TableFormat.Positioning.HorizPosition;
					A_2.Width -= A_0.TableFormat.Positioning.HorizPosition;
					num = 75;
					continue;
				case 71:
					goto IL_B9C;
				case 72:
					num4 = A_1.Left;
					goto IL_DAC;
				case 73:
				{
					HorizontalPosition horizPositionAbs;
					switch (horizPositionAbs)
					{
					case HorizontalPosition.Left:
						num = 84;
						continue;
					case HorizontalPosition.Center:
						num = 50;
						continue;
					default:
						num = 66;
						continue;
					}
					break;
				}
				case 74:
					goto IL_840;
				case 75:
					goto IL_B9C;
				case 76:
					num = 54;
					continue;
				case 77:
					goto IL_840;
				case 78:
					num = 107;
					continue;
				case 79:
					A_2.X += A_0.TableFormat.Positioning.HorizPosition;
					num = 26;
					continue;
				case 81:
					num = 90;
					continue;
				case 82:
					num = 38;
					continue;
				case 83:
				{
					VerticalRelation vertRelationTo = A_0.TableFormat.Positioning.VertRelationTo;
					num = 42;
					continue;
				}
				case 84:
					if (A_0.Owner is TableCell)
					{
						num = 24;
						continue;
					}
					goto IL_505;
				case 85:
					num = 60;
					continue;
				case 86:
					goto IL_B9C;
				case 87:
					if (!this.ᜏ.IsTextBox)
					{
						num = 82;
						continue;
					}
					return;
				case 88:
					num = 51;
					continue;
				case 89:
				{
					float num2;
					if (num2 < A_2.X - this.\u1713())
					{
						num = 49;
						continue;
					}
					A_2.X = num2;
					num = 30;
					continue;
				}
				case 90:
					if (A_0.TextBoxFormat.IsInShape)
					{
						num = 117;
						continue;
					}
					goto IL_A31;
				case 91:
					goto IL_B9C;
				case 92:
					if (A_0.IsTextBox)
					{
						num = 115;
						continue;
					}
					goto IL_91E;
				case 93:
					if (A_0.IsTextBox)
					{
						num = 25;
						continue;
					}
					goto IL_532;
				case 94:
					goto IL_840;
				case 95:
					goto IL_B9C;
				case 96:
					goto IL_840;
				case 97:
					goto IL_840;
				case 98:
					goto IL_B9C;
				case 99:
					A_2.Height += y - A_2.Y;
					num = 23;
					continue;
				case 100:
					A_2.Y = A_2.Y - A_1.Top + A_0.TableFormat.Positioning.VertPosition;
					num = 112;
					continue;
				case 101:
					num = 46;
					continue;
				case 102:
					num = 62;
					continue;
				case 103:
					if (A_0.TextBoxFormat.IsInShape)
					{
						num = 37;
						continue;
					}
					goto IL_78F;
				case 104:
					A_2.Y += A_0.TableFormat.Positioning.VertPosition;
					num = 74;
					continue;
				case 105:
					A_2.X = A_0.TableFormat.Positioning.HorizPosition;
					num = 39;
					continue;
				case 106:
				{
					VerticalPosition vertPositionAbs;
					if (vertPositionAbs == VerticalPosition.None)
					{
						num = 85;
						continue;
					}
					goto IL_840;
				}
				case 107:
					if (A_0.TextBoxFormat.IsAllowInCell)
					{
						num = 27;
						continue;
					}
					goto IL_31A;
				case 108:
					goto IL_1094;
				case 109:
				{
					if (A_0.IsTextBoxInTable)
					{
						num = 41;
						continue;
					}
					HorizontalPosition horizPositionAbs = A_0.TableFormat.Positioning.HorizPositionAbs;
					num = 73;
					continue;
				}
				case 110:
					if (A_0.\u1712.HorizontalOrigin == HorizontalOrigin.Character)
					{
						num = 105;
						continue;
					}
					goto IL_532;
				case 111:
					num = 113;
					continue;
				case 112:
					goto IL_840;
				case 113:
					if (A_0.Owner is TableCell)
					{
						num = 6;
						continue;
					}
					goto IL_D57;
				case 114:
					num4 = A_2.X;
					goto IL_DAC;
				case 115:
					num = 15;
					continue;
				case 116:
					goto IL_2CA;
				case 117:
					num = 13;
					continue;
				case 118:
					A_2.Y = A_2.Y - A_1.Top + A_0.TableFormat.Positioning.VertPosition;
					num = 19;
					continue;
				case 119:
					goto IL_9AC;
				}
				if (A_0.IsTextBox)
				{
					num = 10;
					continue;
				}
				goto IL_78F;
				IL_24A:
				A_2.X = A_1.Left + A_0.TableFormat.Positioning.HorizPosition;
				num = 86;
				continue;
				IL_31A:
				A_2.Y = A_0.TableFormat.Positioning.VertPosition;
				num = 97;
				continue;
				IL_505:
				num = 58;
				continue;
				IL_532:
				horizRelationTo = A_0.TableFormat.Positioning.HorizRelationTo;
				num = 1;
				continue;
				IL_78F:
				num = 16;
				continue;
				IL_840:
				num = 93;
				continue;
				IL_91E:
				A_2.Y = A_1.Top + A_0.TableFormat.Positioning.VertPosition;
				num = 8;
				continue;
				IL_A31:
				float x = A_2.X;
				y = A_2.Y;
				num = 52;
				continue;
				IL_B1A:
				num = 87;
				continue;
				IL_B47:
				num = 22;
				continue;
				IL_B9C:
				num = 35;
				continue;
				IL_D2F:
				num = 92;
				continue;
				IL_D57:
				num = 64;
				continue;
				IL_DAC:
				num3 = num4;
				num = 11;
				continue;
				IL_1094:
				A_2.X = num3 + A_2.Width * (float)(100 - A_0.PreferredTableWidth.ᜁ()) / 100f / 2f;
				num = 71;
			}
			IL_2CA:
			A_2 = new RectangleF(A_0.TextBoxFormat.StartPoint.X + A_0.TextBoxFormat.HorizontalPosition, A_0.TextBoxFormat.StartPoint.Y + A_0.TextBoxFormat.VerticalPosition, A_0.TextBoxFormat.Width, A_0.TextBoxFormat.Height);
			return;
			IL_9AC:
			return;
			IL_E7C:
			A_2 = new RectangleF(A_0.TextBoxFormat.StartPoint.X, A_0.TextBoxFormat.StartPoint.Y, A_0.TextBoxFormat.Width, A_0.TextBoxFormat.Height);
			this.ᜏ.TableFormat.Positioning.VertRelationTo = VerticalRelation.Paragraph;
			return;
		}
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x00060514 File Offset: 0x0005F514
	public override sprᦰ ᜀ(RectangleF A_0)
	{
		int a_ = 17;
		switch (0)
		{
		default:
			for (;;)
			{
				bool a_2 = true;
				int num = 60;
				for (;;)
				{
					int a_3;
					Paddings paddings;
					bool flag;
					IDocumentObject previousSibling;
					RectangleF rectangleF;
					Paddings paddings2;
					Font font;
					SizeF a_4;
					sprᡌ sprᡌ2;
					switch (num)
					{
					case 0:
						if (this.ᜏ.Owner.Owner.Owner.Owner != null)
						{
							num = 133;
							continue;
						}
						goto IL_B3E;
					case 1:
						num = 33;
						continue;
					case 2:
						num = 115;
						continue;
					case 3:
					{
						Table table;
						if (table.TableFormat.Paddings.IsEmpty)
						{
							num = 21;
							continue;
						}
						num = 130;
						continue;
					}
					case 4:
						goto IL_B3E;
					case 5:
						num = 102;
						continue;
					case 6:
						goto IL_12D0;
					case 7:
						num = 85;
						continue;
					case 8:
						if (this.ᜏ.Owner.Owner.Owner != null)
						{
							num = 22;
							continue;
						}
						goto IL_B3E;
					case 9:
						goto IL_4ED;
					case 10:
						if (this.ᜋ)
						{
							goto IL_F54;
						}
						goto IL_292;
					case 11:
						num = 19;
						continue;
					case 12:
						num = 83;
						continue;
					case 13:
						if (this.ᜏ.IsTextBox)
						{
							num = 68;
							continue;
						}
						goto IL_B3E;
					case 14:
						num = 25;
						continue;
					case 15:
						if (this.ᜏ.Rows[0].Cells[0].LastParagraph == null)
						{
							num = 7;
							continue;
						}
						num = 70;
						continue;
					case 16:
						num = 116;
						continue;
					case 17:
					{
						spr\u2591 spr_u = new spr\u2591();
						spr_u.ᜀ(null);
						spr_u.ᜀ(false);
						spr_u.ᜀ(this.ᜃ);
						spr_u.ᜁ(base.\u171E().ᜈ());
						spr_u.ᜀ(a_3);
						base.\u171E().\u171D().ᜀ(spr_u);
						num = 52;
						continue;
					}
					case 18:
						goto IL_5F5;
					case 19:
						if (this.ᜏ.Owner.Owner.Owner is Table)
						{
							num = 62;
							continue;
						}
						goto IL_1289;
					case 20:
						if (!(this.ᜏ.Owner is TableCell))
						{
							num = 18;
							continue;
						}
						goto IL_15FC;
					case 21:
						num = 65;
						continue;
					case 22:
						num = 0;
						continue;
					case 23:
						paddings = new Paddings();
						goto IL_AE0;
					case 24:
						goto IL_292;
					case 25:
						if (!flag)
						{
							num = 93;
							continue;
						}
						goto IL_15FC;
					case 26:
						if (this.ᜏ.IsTextBox)
						{
							num = 113;
							continue;
						}
						num = 43;
						continue;
					case 27:
						previousSibling = previousSibling.PreviousSibling;
						num = 59;
						continue;
					case 28:
						if (this.ᜏ.TextBoxFormat.TextWrappingStyle == TextWrappingStyle.Behind)
						{
							num = 122;
							continue;
						}
						goto IL_15FC;
					case 29:
						num = 125;
						continue;
					case 30:
						if (this.ᜏ.TextBoxFormat.TextWrappingStyle != TextWrappingStyle.Behind)
						{
							num = 109;
							continue;
						}
						goto IL_12D0;
					case 31:
						num = 111;
						continue;
					case 32:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F54;
						default:
							if (false)
							{
							}
							goto IL_E73;
						}
						break;
					case 33:
					{
						Table table2;
						if (table2.TableFormat.Paddings.IsEmpty)
						{
							num = 72;
							continue;
						}
						num = 99;
						continue;
					}
					case 34:
						if (this.ᜏ.Rows[0].Height > 0f)
						{
							num = 55;
							continue;
						}
						goto IL_154A;
					case 35:
						goto IL_1046;
					case 36:
						if ((this.ᜏ.Owner.Owner.Owner as Table).IsTextBox)
						{
							num = 35;
							continue;
						}
						goto IL_5F5;
					case 37:
						goto IL_F90;
					case 38:
						num = 143;
						continue;
					case 39:
						if (rectangleF.Height < this.ᜏ.Rows[0].Height)
						{
							num = 90;
							continue;
						}
						goto IL_154A;
					case 40:
						goto IL_652;
					case 41:
						if (!(this.ᜏ.Owner.Owner.Owner as Table).IsSDTTable)
						{
							num = 73;
							continue;
						}
						goto IL_1289;
					case 42:
						goto IL_292;
					case 43:
						if (!this.ᜏ.IsTextBox)
						{
							num = 16;
							continue;
						}
						goto IL_15FC;
					case 44:
						goto IL_CB2;
					case 45:
						num = 10;
						continue;
					case 46:
						(this.ᜂ as Table).ᜊ();
						num = 9;
						continue;
					case 47:
						num = 3;
						continue;
					case 48:
						A_0.Y = (previousSibling as Table).ᜤ.Bottom + 1f;
						A_0.X = (previousSibling as Table).ᜤ.X;
						a_2 = false;
						num = 137;
						continue;
					case 49:
						goto IL_CB2;
					case 50:
						if (this.ᜏ != null)
						{
							num = 5;
							continue;
						}
						goto IL_652;
					case 51:
						this.ᜏ.Rows[0].HeightType = TableRowHeightType.AtLeast;
						num = 40;
						continue;
					case 52:
						goto IL_1284;
					case 53:
						if (true)
						{
						}
						num = 30;
						continue;
					case 54:
						if (previousSibling is Table)
						{
							num = 38;
							continue;
						}
						num = 95;
						continue;
					case 55:
						num = 39;
						continue;
					case 56:
						if (!this.ᜏ.IsTextBox)
						{
							num = 12;
							continue;
						}
						goto IL_6AB;
					case 57:
						num = 91;
						continue;
					case 58:
						if (this.ᜏ.TextBoxFormat.VerticalOrigin == VerticalOrigin.Line)
						{
							num = 37;
							continue;
						}
						goto IL_A04;
					case 59:
						goto IL_710;
					case 60:
						if (this.ᜂ is Table)
						{
							num = 121;
							continue;
						}
						goto IL_4ED;
					case 61:
						if (this.ᜏ.TextBoxFormat.IsAllowInCell)
						{
							num = 117;
							continue;
						}
						goto IL_B3E;
					case 62:
						num = 41;
						continue;
					case 63:
					{
						sprᡌ sprᡌ;
						base.\u171E().\u171D().ᜀ(sprᡌ);
						num = 6;
						continue;
					}
					case 64:
						if (this.ᜀ != LayoutState.Unknown)
						{
							num = 42;
							continue;
						}
						goto IL_E73;
					case 65:
						paddings2 = new Paddings();
						goto IL_AAE;
					case 66:
						goto IL_90A;
					case 67:
						if (!(this.ᜏ.Owner.Owner.Owner.Owner as Table).IsSDTTable)
						{
							num = 114;
							continue;
						}
						goto IL_B3E;
					case 68:
						num = 120;
						continue;
					case 69:
						if (this.ᜏ.IsTextBox)
						{
							num = 106;
							continue;
						}
						goto IL_A04;
					case 70:
						font = this.ᜏ.Rows[0].Cells[0].LastParagraph.BreakCharacterFormat.Font;
						goto IL_461;
					case 71:
						a_4.Width = ((this.ᜏ.Rows[0].Cells[0].WidthType == FtsWidth.Percentage) ? (this.ᜏ.Width / 20f) : this.ᜏ.Width);
						this.ᜀ(a_4, ref A_0);
						this.ᜌ();
						base.ᜀ(A_0.Location);
						this.ᜉ = new double[this.ᜏ.Rows[this.\u1712().ᜃ()].Cells.Count];
						this.ᜊ = new int[this.ᜏ.Rows[this.\u1712().ᜃ()].Cells.Count];
						num = 32;
						continue;
					case 72:
						num = 23;
						continue;
					case 73:
					{
						TableCell tableCell = this.ᜏ.Owner as TableCell;
						Table table2 = this.ᜏ.Owner.Owner.Owner as Table;
						num = 78;
						continue;
					}
					case 74:
						if (this.ᜏ.Owner is TableCell)
						{
							num = 101;
							continue;
						}
						goto IL_1046;
					case 75:
						if (!this.ᜋ())
						{
							num = 45;
							continue;
						}
						this.ᜊ();
						this.ᜉ();
						num = 64;
						continue;
					case 76:
						goto IL_A04;
					case 77:
						if (this.ᜏ.Owner != null)
						{
							num = 87;
							continue;
						}
						goto IL_B3E;
					case 78:
					{
						TableCell tableCell;
						if (tableCell.CellFormat.Paddings.IsEmpty)
						{
							num = 1;
							continue;
						}
						num = 89;
						continue;
					}
					case 79:
						goto IL_6AB;
					case 80:
						if (this.ᜏ.TextBoxFormat.VerticalOrigin == VerticalOrigin.Line)
						{
							num = 105;
							continue;
						}
						goto IL_154A;
					case 81:
						if (this.ᜏ.TextBoxFormat.TextWrappingStyle != TextWrappingStyle.InFrontOfText)
						{
							num = 53;
							continue;
						}
						goto IL_12D0;
					case 82:
						num = 96;
						continue;
					case 83:
						if (this.ᜏ.Rows[0].Height > 0f)
						{
							num = 57;
							continue;
						}
						goto IL_6AB;
					case 84:
						rectangleF = A_0;
						num = 69;
						continue;
					case 85:
						font = this.ᜏ.Rows[0].Cells[0].CharacterFormat.Font;
						goto IL_461;
					case 86:
						if (this.ᜏ.TextBoxFormat.VerticalOrigin != VerticalOrigin.Paragraph)
						{
							num = 129;
							continue;
						}
						goto IL_F90;
					case 87:
						num = 98;
						continue;
					case 88:
						if (this.ᜏ.IsTextBox)
						{
							num = 94;
							continue;
						}
						goto IL_154A;
					case 89:
					{
						TableCell tableCell;
						paddings = tableCell.CellFormat.Paddings;
						goto IL_AE0;
					}
					case 90:
						num = 124;
						continue;
					case 91:
						if (A_0.Height >= this.ᜏ.Rows[0].Height)
						{
							num = 79;
							continue;
						}
						goto IL_59B;
					case 92:
					{
						TableCell tableCell2;
						paddings2 = tableCell2.CellFormat.Paddings;
						goto IL_AAE;
					}
					case 93:
					{
						sprᡌ2 = new sprᡌ();
						RectangleF a_5 = this.ᜃ.ᜁ();
						a_5 = new RectangleF(a_5.X - this.ᜏ.TableFormat.Positioning.DistanceFromLeft, a_5.Y - this.ᜏ.TableFormat.Positioning.DistanceFromTop, a_5.Width + this.ᜏ.TableFormat.Positioning.DistanceFromRight, a_5.Height + this.ᜏ.TableFormat.Positioning.DistanceFromBottom);
						sprᡌ2.ᜀ(a_5);
						sprᡌ2.ᜂ().ᜀ(TextWrappingStyle.Square);
						sprᡌ2.ᜂ().ᜀ(TextWrappingType.Both);
						num = 74;
						continue;
					}
					case 94:
						num = 34;
						continue;
					case 95:
						if (previousSibling is Paragraph)
						{
							num = 140;
							continue;
						}
						goto IL_CB2;
					case 96:
						if (this.ᜏ.Owner.Owner != null)
						{
							num = 123;
							continue;
						}
						goto IL_1289;
					case 97:
						if (base.\u171E().ᜢ().IsAtLast)
						{
							num = 51;
							continue;
						}
						goto IL_652;
					case 98:
						if (this.ᜏ.Owner.Owner != null)
						{
							num = 108;
							continue;
						}
						goto IL_B3E;
					case 99:
					{
						Table table2;
						paddings = table2.TableFormat.Paddings;
						goto IL_AE0;
					}
					case 100:
						num = 67;
						continue;
					case 101:
						num = 36;
						continue;
					case 102:
						if (this.ᜏ.IsTextBox)
						{
							num = 104;
							continue;
						}
						goto IL_652;
					case 103:
						if (this.ᜏ.TextBoxFormat.TextWrappingStyle == TextWrappingStyle.InFrontOfText)
						{
							num = 17;
							continue;
						}
						num = 28;
						continue;
					case 104:
						num = 97;
						continue;
					case 105:
						goto IL_35B;
					case 106:
						num = 86;
						continue;
					case 107:
						num = 61;
						continue;
					case 108:
						num = 8;
						continue;
					case 109:
						num = 112;
						continue;
					case 110:
					{
						TableCell tableCell2;
						if (tableCell2.CellFormat.Paddings.IsEmpty)
						{
							num = 47;
							continue;
						}
						num = 92;
						continue;
					}
					case 111:
						if (this.ᜏ.Owner != null)
						{
							num = 82;
							continue;
						}
						goto IL_1289;
					case 112:
						if (this.ᜏ.TextBoxFormat.TextWrappingStyle != TextWrappingStyle.Inline)
						{
							num = 63;
							continue;
						}
						goto IL_12D0;
					case 113:
					{
						sprᡌ sprᡌ = new sprᡌ();
						RectangleF a_6 = this.ᜃ.ᜁ();
						a_6 = new RectangleF(a_6.X - this.ᜏ.TableFormat.Positioning.DistanceFromLeft, a_6.Y - this.ᜏ.TableFormat.Positioning.DistanceFromTop, a_6.Width + this.ᜏ.TableFormat.Positioning.DistanceFromRight, a_6.Height + this.ᜏ.TableFormat.Positioning.DistanceFromBottom);
						sprᡌ.ᜁ(this.ᜏ.TextBoxFormat.IsAllowInCell);
						sprᡌ.ᜀ(a_6);
						sprᡌ.ᜂ().ᜀ(this.ᜏ.TextBoxFormat.TextWrappingStyle);
						sprᡌ.ᜂ().ᜀ(this.ᜏ.TextBoxFormat.TextWrappingType);
						num = 81;
						continue;
					}
					case 114:
					{
						TableCell tableCell2 = this.ᜏ.Owner.Owner as TableCell;
						Table table = this.ᜏ.Owner.Owner.Owner.Owner as Table;
						num = 110;
						continue;
					}
					case 115:
						if ((previousSibling as Table).TableFormat.HorizontalAlignment == RowAlignment.Left)
						{
							num = 29;
							continue;
						}
						goto IL_CB2;
					case 116:
						if (this.ᜏ.TableFormat.WrapTextAround)
						{
							num = 14;
							continue;
						}
						goto IL_15FC;
					case 117:
						num = 77;
						continue;
					case 118:
						this.ᜀ = LayoutState.Fitted;
						num = 24;
						continue;
					case 119:
						if (!(this.ᜂ as Table).IsHasCaculatedCellWidth)
						{
							num = 46;
							continue;
						}
						goto IL_4ED;
					case 120:
						if (this.ᜏ.IsTextBoxInTable)
						{
							num = 107;
							continue;
						}
						goto IL_B3E;
					case 121:
						num = 119;
						continue;
					case 122:
					{
						spr\u2591 spr_u2 = new spr\u2591();
						spr_u2.ᜀ(null);
						spr_u2.ᜀ(false);
						spr_u2.ᜀ(this.ᜃ);
						spr_u2.ᜁ(base.\u171E().ᜈ());
						spr_u2.ᜀ(a_3);
						base.\u171E().\u171D().ᜁ(spr_u2);
						num = 66;
						continue;
					}
					case 123:
						num = 141;
						continue;
					case 124:
						if (this.ᜏ.TextBoxFormat.VerticalOrigin != VerticalOrigin.Paragraph)
						{
							num = 126;
							continue;
						}
						goto IL_59B;
					case 125:
						if (A_0.Y < (previousSibling as Table).ᜤ.Bottom)
						{
							num = 48;
							continue;
						}
						goto IL_CB2;
					case 126:
						num = 80;
						continue;
					case 127:
						if (this.ᜏ.Owner.Owner.Owner.Owner is Table)
						{
							num = 100;
							continue;
						}
						goto IL_B3E;
					case 128:
						if (this.ᜑ().ᜈ())
						{
							num = 142;
							continue;
						}
						goto IL_BEA;
					case 129:
						num = 58;
						continue;
					case 130:
					{
						Table table;
						paddings2 = table.TableFormat.Paddings;
						goto IL_AAE;
					}
					case 131:
						if (previousSibling == null)
						{
							num = 44;
							continue;
						}
						num = 54;
						continue;
					case 132:
						if (!base.\u171E().ᜈ())
						{
							num = 84;
							continue;
						}
						goto IL_154A;
					case 133:
						num = 127;
						continue;
					case 134:
						if (!this.ᜑ().ᜇ())
						{
							num = 31;
							continue;
						}
						goto IL_BEA;
					case 135:
						goto IL_613;
					case 136:
						goto IL_710;
					case 137:
						goto IL_CB2;
					case 138:
						goto IL_B3E;
					case 139:
						if ((previousSibling as Paragraph).Text == string.Empty)
						{
							num = 27;
							continue;
						}
						goto IL_CB2;
					case 140:
						num = 139;
						continue;
					case 141:
						if (this.ᜏ.Owner.Owner.Owner != null)
						{
							num = 11;
							continue;
						}
						goto IL_1289;
					case 142:
						num = 134;
						continue;
					case 143:
						if ((previousSibling as Table).LastLayoutPage == base.\u171E().\u171D())
						{
							num = 2;
							continue;
						}
						goto IL_CB2;
					}
					break;
					IL_292:
					this.ᜅ();
					this.ᜏ();
					this.ᜏ.LastLayoutPage = base.\u171E().\u171D();
					flag = (this.ᜏ.Owner is HeaderFooter);
					num = 26;
					continue;
					IL_461:
					Font a_7 = font;
					a_4 = base.\u171E().ᜁ(ClipboardData.b("坶", a_), a_7, null);
					num = 71;
					continue;
					IL_4ED:
					this.ᜏ = (this.ᜂ as Table);
					MarginsF a_8 = this.\u170D();
					num = 132;
					continue;
					IL_5F5:
					base.\u171E().\u171D().ᜀ(sprᡌ2);
					num = 135;
					continue;
					IL_652:
					num = 15;
					continue;
					IL_6AB:
					num = 88;
					continue;
					IL_710:
					num = 131;
					continue;
					IL_A04:
					num = 56;
					continue;
					IL_AAE:
					Paddings paddings3 = paddings2;
					a_8 = new MarginsF(paddings3.Left, paddings3.Top, paddings3.Right, paddings3.Bottom);
					num = 138;
					continue;
					IL_AE0:
					Paddings paddings4 = paddings;
					a_8 = new MarginsF(paddings4.Left, paddings4.Top, paddings4.Right, paddings4.Bottom);
					num = 4;
					continue;
					IL_B3E:
					this.ᜀ(this.ᜏ, a_8, ref A_0);
					num = 49;
					continue;
					IL_BEA:
					previousSibling = this.ᜏ.PreviousSibling;
					num = 136;
					continue;
					IL_CB2:
					this.ᜀ(ref A_0, a_2);
					this.ᜏ.TableBounds = A_0;
					num = 50;
					continue;
					IL_E73:
					num = 75;
					continue;
					IL_F54:
					num = 118;
					continue;
					IL_F90:
					rectangleF.Y += this.ᜏ.TableFormat.Positioning.VertPosition;
					rectangleF.Height -= this.ᜏ.TableFormat.Positioning.VertPosition;
					num = 76;
					continue;
					IL_1046:
					num = 20;
					continue;
					IL_1289:
					num = 13;
					continue;
					IL_12D0:
					a_3 = this.ᜏ.TextBoxFormat.OrderIndex;
					num = 103;
					continue;
					IL_154A:
					num = 128;
				}
			}
			IL_35B:
			IL_59B:
			this.ᜏ.IsHasCaculatedCellWidth = false;
			this.ᜁ = new sprᲲ(this.\u1712(), 0);
			this.ᜀ = LayoutState.NotFitted;
			return null;
			IL_613:
			IL_90A:
			IL_1284:
			IL_15FC:
			return this.ᜃ;
		}
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x00061B24 File Offset: 0x00060B24
	private void ᜏ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				RectangleF rectangleF = this.ᜃ.ᜁ();
				float num = 0f;
				float num2 = 0f;
				float num3 = 0f;
				float num4 = 0f;
				int num5 = 4;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						if (!this.ᜑ().ᜇ())
						{
							num5 = 2;
							continue;
						}
						return;
					case 1:
					{
						if (true)
						{
						}
						num = this.ᜏ.TableFormat.CellSpacing * 2f;
						num2 = this.ᜏ.TableFormat.Borders.Left.LineWidth / 2f;
						num3 = this.ᜏ.TableFormat.Borders.Right.LineWidth / 2f;
						num4 = this.ᜏ.TableFormat.Borders.Top.LineWidth / 2f;
						float num6 = this.ᜏ.TableFormat.Borders.Bottom.LineWidth / 2f;
						num5 = 7;
						continue;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BE;
						default:
							if (false)
							{
							}
							num5 = 6;
							continue;
						}
						break;
					case 3:
						return;
					case 4:
						if (this.ᜏ.TableFormat.CellSpacing > 0f)
						{
							num5 = 1;
							continue;
						}
						goto IL_CF;
					case 5:
						this.ᜎ();
						num5 = 3;
						continue;
					case 6:
						if (this.ᜑ().ᜈ())
						{
							goto IL_BE;
						}
						return;
					case 7:
						goto IL_CF;
					}
					break;
					IL_BE:
					num5 = 5;
					continue;
					IL_CF:
					rectangleF.X -= num2;
					rectangleF.Y -= num4;
					rectangleF.Width += num + num2 + num3;
					rectangleF.Height += num + num4 + num3;
					this.ᜏ.ᜤ = rectangleF;
					this.ᜃ.ᜀ(rectangleF);
					num5 = 0;
				}
			}
			return;
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00061D6C File Offset: 0x00060D6C
	private void ᜎ()
	{
		switch (0)
		{
		default:
		{
			float num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_158:
				num = (this.ᜆ as spr\u1DA4).ᜈ().Height - this.ᜃ.ᜁ().Height;
				num2 = 18;
				break;
			default:
				if (false)
				{
				}
				goto IL_A3;
			}
			MarginsF marginsF;
			float num3;
			float num4;
			for (;;)
			{
				IL_2C:
				RectangleF rectangleF;
				RectangleF rectangleF2;
				switch (num2)
				{
				case 0:
					if (rectangleF.Height < (this.ᜆ as spr\u1DA4).ᜈ().Height)
					{
						num2 = 23;
						continue;
					}
					goto IL_38B;
				case 1:
					if (marginsF != null)
					{
						num2 = 2;
						continue;
					}
					goto IL_1A9;
				case 2:
					num3 = (this.ᜆ as spr\u1DA4).ᜈ().Height + marginsF.Top + marginsF.Bottom;
					num2 = 3;
					continue;
				case 3:
					goto IL_1A9;
				case 4:
					this.ᜃ.ᜀ((double)num4, (double)(num / 2f), false);
					num2 = 25;
					continue;
				case 5:
					goto IL_2A9;
				case 6:
					if (this.ᜏ.TableFormat.Positioning.VertPositionAbs != VerticalPosition.Bottom)
					{
						num2 = 12;
						continue;
					}
					goto IL_198;
				case 7:
					if (!this.ᜏ.IsTextBox)
					{
						num2 = 21;
						continue;
					}
					return;
				case 8:
					if (this.ᜏ.TableFormat.Positioning.VertRelationTo != VerticalRelation.Page)
					{
						num2 = 5;
						continue;
					}
					goto IL_38B;
				case 9:
					num2 = 6;
					continue;
				case 10:
					goto IL_328;
				case 11:
					goto IL_1F8;
				case 12:
					num2 = 13;
					continue;
				case 13:
					if (this.ᜏ.TableFormat.Positioning.VertPositionAbs == VerticalPosition.Outside)
					{
						num2 = 10;
						continue;
					}
					if (true)
					{
					}
					num2 = 15;
					continue;
				case 14:
					num = num3 - this.ᜃ.ᜁ().Height;
					num2 = 17;
					continue;
				case 15:
					if (this.ᜏ.TableFormat.Positioning.VertPositionAbs == VerticalPosition.Center)
					{
						num2 = 4;
						continue;
					}
					return;
				case 16:
					if (num != 0f)
					{
						num2 = 9;
						continue;
					}
					return;
				case 17:
					goto IL_3BF;
				case 18:
					goto IL_3BF;
				case 19:
					num2 = 24;
					continue;
				case 20:
					if (this.ᜏ.OwnerTextBody is TableCell)
					{
						num2 = 27;
						continue;
					}
					goto IL_1F8;
				case 21:
					num2 = 20;
					continue;
				case 22:
					if ((this.ᜏ.OwnerTextBody as TableCell).Owner.Owner is Table)
					{
						num2 = 19;
						continue;
					}
					goto IL_1F8;
				case 23:
					num2 = 8;
					continue;
				case 24:
					if (((this.ᜏ.OwnerTextBody as TableCell).Owner.Owner as Table).IsSDTTable)
					{
						num2 = 11;
						continue;
					}
					return;
				case 25:
					goto IL_242;
				case 26:
					if (rectangleF2.Height < num3)
					{
						num2 = 14;
						continue;
					}
					goto IL_3BF;
				case 27:
					num2 = 22;
					continue;
				}
				goto IL_A3;
				IL_1A9:
				rectangleF = this.ᜃ.ᜁ();
				num2 = 0;
				continue;
				IL_1F8:
				num2 = 16;
				continue;
				IL_38B:
				rectangleF2 = this.ᜃ.ᜁ();
				num2 = 26;
				continue;
				IL_3BF:
				num2 = 7;
			}
			IL_198:
			this.ᜃ.ᜀ((double)num4, (double)num, false);
			return;
			IL_242:
			return;
			IL_2A9:
			goto IL_158;
			IL_328:
			goto IL_198;
			IL_A3:
			num4 = 0f;
			num = 0f;
			marginsF = this.\u170D();
			num3 = 0f;
			num2 = 1;
			goto IL_2C;
		}
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x000621A8 File Offset: 0x000611A8
	private MarginsF \u170D()
	{
		MarginsF result;
		for (;;)
		{
			ISection section = null;
			result = null;
			IDocumentObject documentObject = this.ᜏ.Owner;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_70;
				case 1:
					if (documentObject.DocumentObjectType == DocumentObjectType.Section)
					{
						goto IL_14C;
					}
					num = 5;
					continue;
				case 2:
					goto IL_138;
				case 3:
					documentObject = documentObject.Owner;
					num = 2;
					continue;
				case 4:
					if (documentObject == null)
					{
						num = 13;
						continue;
					}
					goto IL_B0;
				case 5:
					if (documentObject.Owner != null)
					{
						num = 3;
						continue;
					}
					goto IL_70;
				case 6:
					num = 8;
					continue;
				case 7:
					return result;
				case 8:
					goto IL_138;
				case 9:
					if (section == null)
					{
						return result;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14C;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 10:
					result = section.PageSetup.Margins;
					num = 7;
					continue;
				case 11:
					if (true)
					{
					}
					section = (documentObject as Section);
					num = 9;
					continue;
				case 12:
					if (documentObject.DocumentObjectType == DocumentObjectType.Section)
					{
						num = 11;
						continue;
					}
					return result;
				case 13:
					documentObject = this.ᜏ.ClonedOwner;
					num = 15;
					continue;
				case 14:
					if (documentObject != null)
					{
						num = 6;
						continue;
					}
					return result;
				case 15:
					goto IL_B0;
				}
				break;
				IL_70:
				num = 12;
				continue;
				IL_B0:
				num = 14;
				continue;
				IL_138:
				num = 1;
				continue;
				IL_14C:
				num = 0;
			}
		}
		return result;
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x00062360 File Offset: 0x00061360
	private new void ᜀ(SizeF A_0, ref RectangleF A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 26;
			for (;;)
			{
				int num2;
				RectangleF rectangleF2;
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					if (this.ᜏ.TextBoxFormat.TextWrappingStyle == TextWrappingStyle.InFrontOfText)
					{
						num = 35;
						continue;
					}
					goto IL_B77;
				case 2:
					A_1.Width = spr\u25E5.ᜄ[num2].X - A_1.X;
					num = 81;
					continue;
				case 3:
					num = 32;
					continue;
				case 4:
					A_1.Y = spr\u25E5.ᜄ[num2].Bottom;
					A_1.Height -= spr\u25E5.ᜄ[num2].Height;
					base.ᜃ(A_1);
					num = 79;
					continue;
				case 5:
					if (A_1.Y >= spr\u25E5.ᜄ[num2].Y)
					{
						num = 41;
						continue;
					}
					goto IL_3CC;
				case 6:
					A_1.Width -= spr\u25E5.ᜄ[num2].Right - A_1.X;
					num = 29;
					continue;
				case 7:
					if (A_1.X > spr\u25E5.ᜄ[num2].X)
					{
						num = 50;
						continue;
					}
					goto IL_44C;
				case 8:
					if (spr\u25E5.ᜄ.Count > 0)
					{
						num = 0;
						continue;
					}
					goto IL_785;
				case 9:
					if (A_1.X > spr\u25E5.ᜄ[num2].X)
					{
						num = 58;
						continue;
					}
					goto IL_603;
				case 10:
					if (A_1.Y >= spr\u25E5.ᜄ[num2].Bottom)
					{
						num = 52;
						continue;
					}
					goto IL_9D1;
				case 11:
					if (A_1.Y + A_0.Height >= spr\u25E5.ᜄ[num2].Y)
					{
						num = 42;
						continue;
					}
					goto IL_DCB;
				case 12:
					if (spr\u25E5.ᜅ[num2] != TextWrappingStyle.InFrontOfText)
					{
						num = 36;
						continue;
					}
					goto IL_785;
				case 13:
					num = 19;
					continue;
				case 14:
					goto IL_44C;
				case 15:
					goto IL_3CC;
				case 16:
					num = 89;
					continue;
				case 17:
					goto IL_44C;
				case 18:
					num = 10;
					continue;
				case 19:
					if (A_1.Right > spr\u25E5.ᜄ[num2].X)
					{
						num = 2;
						continue;
					}
					goto IL_BE3;
				case 20:
					if (A_1.X > spr\u25E5.ᜄ[num2].Right)
					{
						num = 87;
						continue;
					}
					goto IL_603;
				case 21:
					goto IL_B77;
				case 22:
					if (A_1.Width < 16f)
					{
						num = 25;
						continue;
					}
					goto IL_44C;
				case 23:
					if (A_1.Y + A_0.Height >= spr\u25E5.ᜄ[num2].Y)
					{
						num = 80;
						continue;
					}
					goto IL_44C;
				case 24:
					num = 75;
					continue;
				case 25:
					A_1.Y = spr\u25E5.ᜄ[num2].Bottom;
					A_1.Height -= spr\u25E5.ᜄ[num2].Height;
					base.ᜃ(A_1);
					num = 47;
					continue;
				case 27:
					A_1.Width -= spr\u25E5.ᜄ[num2].Right - A_1.X;
					A_1.X = spr\u25E5.ᜄ[num2].Right;
					base.ᜃ(A_1);
					num = 62;
					continue;
				case 28:
					goto IL_44C;
				case 29:
					if (A_1.Width < 16f)
					{
						num = 34;
						continue;
					}
					A_1.X = spr\u25E5.ᜄ[num2].Right;
					base.ᜃ(A_1);
					num = 91;
					continue;
				case 30:
					if (A_1.Right - spr\u25E5.ᜄ[num2].Right > 0f)
					{
						num = 16;
						continue;
					}
					goto IL_DCB;
				case 31:
					if (A_1.X < spr\u25E5.ᜄ[num2].Right)
					{
						num = 6;
						continue;
					}
					goto IL_C1F;
				case 32:
					if (A_1.Y < spr\u25E5.ᜄ[num2].Y)
					{
						num = 39;
						continue;
					}
					goto IL_374;
				case 33:
					if (A_1.Y >= spr\u25E5.ᜄ[num2].Y)
					{
						num = 18;
						continue;
					}
					goto IL_5BF;
				case 34:
					A_1.Width = this.ᜅ.ᜇ().Right - spr\u25E5.ᜄ[num2].Right;
					num = 82;
					continue;
				case 35:
					goto IL_A2E;
				case 36:
					num = 63;
					continue;
				case 37:
				{
					RectangleF rectangleF;
					if (rectangleF.Right >= spr\u25E5.ᜄ[num2].X - 16f)
					{
						num = 90;
						continue;
					}
					goto IL_44C;
				}
				case 38:
					if (spr\u25E5.ᜄ.Count > 0)
					{
						num = 71;
						continue;
					}
					goto IL_44C;
				case 39:
					num = 11;
					continue;
				case 40:
					A_1.Width = this.ᜅ.ᜇ().Right - spr\u25E5.ᜄ[num2].Right;
					num = 22;
					continue;
				case 41:
					num = 45;
					continue;
				case 42:
					goto IL_374;
				case 43:
					if (spr\u25E5.ᜄ.Count > 0)
					{
						num = 65;
						continue;
					}
					return;
				case 44:
					goto IL_44C;
				case 45:
					if (A_1.Y >= spr\u25E5.ᜄ[num2].Bottom)
					{
						num = 15;
						continue;
					}
					goto IL_7E0;
				case 46:
					if (num2 >= spr\u25E5.ᜄ.Count)
					{
						num = 56;
						continue;
					}
					num = 77;
					continue;
				case 47:
					goto IL_44C;
				case 48:
					num = 1;
					continue;
				case 49:
					if (!this.ᜏ.IsTextBox)
					{
						num = 21;
						continue;
					}
					return;
				case 50:
					IL_63A:
					num = 59;
					continue;
				case 51:
					if (A_1.X >= spr\u25E5.ᜄ[num2].X)
					{
						num = 74;
						continue;
					}
					goto IL_C1F;
				case 52:
					goto IL_5BF;
				case 53:
					if (A_1.Y + A_0.Height < spr\u25E5.ᜄ[num2].Bottom)
					{
						num = 61;
						continue;
					}
					goto IL_44C;
				case 54:
					A_1.Y = spr\u25E5.ᜄ[num2].Bottom;
					A_1.Width = this.ᜅ.ᜆ().Width;
					A_1.Height -= spr\u25E5.ᜄ[num2].Bottom - A_1.Y;
					base.ᜃ(A_1);
					num = 28;
					continue;
				case 55:
					if (spr\u25E5.ᜅ[num2] != TextWrappingStyle.Inline)
					{
						num = 24;
						continue;
					}
					goto IL_785;
				case 56:
					return;
				case 57:
					if (rectangleF2.X > A_1.X)
					{
						num = 13;
						continue;
					}
					goto IL_BE3;
				case 58:
					num = 20;
					continue;
				case 59:
					if (A_1.X < spr\u25E5.ᜄ[num2].Right)
					{
						num = 27;
						continue;
					}
					goto IL_44C;
				case 60:
					if (spr\u25E5.ᜅ[num2] == TextWrappingStyle.TopAndBottom)
					{
						num = 4;
						continue;
					}
					goto IL_44C;
				case 61:
					goto IL_9D1;
				case 62:
					goto IL_44C;
				case 63:
					if (spr\u25E5.ᜅ[num2] != TextWrappingStyle.Behind)
					{
						num = 64;
						continue;
					}
					goto IL_785;
				case 64:
					num = 51;
					continue;
				case 65:
					num = 66;
					continue;
				case 66:
					if (!(this.ᜆ as spr\u1DA4).ᜁ())
					{
						num = 78;
						continue;
					}
					return;
				case 67:
					if (!this.ᜏ.IsFrame)
					{
						num = 86;
						continue;
					}
					return;
				case 68:
					num = 12;
					continue;
				case 69:
					num = 37;
					continue;
				case 70:
					if (A_1.Y + A_0.Height < spr\u25E5.ᜄ[num2].Bottom)
					{
						num = 92;
						continue;
					}
					goto IL_785;
				case 71:
					num = 33;
					continue;
				case 72:
					goto IL_CD9;
				case 73:
					num = 70;
					continue;
				case 74:
					num = 31;
					continue;
				case 75:
					if (spr\u25E5.ᜅ[num2] != TextWrappingStyle.TopAndBottom)
					{
						num = 68;
						continue;
					}
					goto IL_785;
				case 76:
					if (A_1.Y + A_0.Height >= spr\u25E5.ᜄ[num2].Y)
					{
						num = 73;
						continue;
					}
					goto IL_785;
				case 77:
				{
					RectangleF rectangleF;
					if (rectangleF.X <= spr\u25E5.ᜄ[num2].Right + 16f)
					{
						num = 69;
						continue;
					}
					goto IL_44C;
				}
				case 78:
					num = 67;
					continue;
				case 79:
					goto IL_44C;
				case 80:
					num = 53;
					continue;
				case 81:
					if (A_1.Width < 16f)
					{
						num = 40;
						continue;
					}
					base.ᜃ(A_1);
					num = 14;
					continue;
				case 82:
					if (A_1.Width < 16f)
					{
						num = 54;
						continue;
					}
					A_1.X = spr\u25E5.ᜄ[num2].Right;
					base.ᜃ(A_1);
					num = 17;
					continue;
				case 83:
					goto IL_CD9;
				case 84:
					goto IL_44C;
				case 85:
					num = 88;
					continue;
				case 86:
				{
					RectangleF rectangleF = (this.ᜆ as spr\u1DA4).ᜈ();
					num2 = 0;
					num = 72;
					continue;
				}
				case 87:
					A_1.Width = this.ᜅ.ᜆ().Width;
					base.ᜃ(A_1);
					num = 44;
					continue;
				case 88:
					if (this.ᜏ.TextBoxFormat.TextWrappingStyle != TextWrappingStyle.Behind)
					{
						num = 48;
						continue;
					}
					goto IL_A2E;
				case 89:
					if (A_1.Right - spr\u25E5.ᜄ[num2].Right < A_1.Width)
					{
						num = 3;
						continue;
					}
					goto IL_DCB;
				case 90:
					num = 8;
					continue;
				case 91:
					if (true)
					{
					}
					goto IL_44C;
				case 92:
					goto IL_7E0;
				}
				if (this.ᜏ.IsTextBox)
				{
					num = 85;
					continue;
				}
				goto IL_A2E;
				IL_374:
				A_1.Y = spr\u25E5.ᜄ[num2].Bottom;
				A_1.Height -= spr\u25E5.ᜄ[num2].Height;
				base.ᜃ(A_1);
				num = 84;
				continue;
				IL_3CC:
				num = 76;
				continue;
				IL_44C:
				num2++;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_63A;
				default:
					if (false)
					{
					}
					num = 83;
					continue;
				}
				IL_5BF:
				num = 23;
				continue;
				IL_603:
				num = 7;
				continue;
				IL_785:
				num = 38;
				continue;
				IL_7E0:
				num = 55;
				continue;
				IL_9D1:
				num = 60;
				continue;
				IL_A2E:
				num = 49;
				continue;
				IL_B77:
				num = 43;
				continue;
				IL_BE3:
				num = 9;
				continue;
				IL_C1F:
				num = 30;
				continue;
				IL_CD9:
				num = 46;
				continue;
				IL_DCB:
				rectangleF2 = spr\u25E5.ᜄ[num2];
				num = 57;
			}
			return;
		}
		}
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x00063174 File Offset: 0x00062174
	private void ᜌ()
	{
		ArrayList arrayList;
		for (;;)
		{
			int num = this.\u1712().ᜅ();
			this.ᜅ = new double[num];
			this.ᜆ = new double[num];
			arrayList = this.ᜁ(this.ᜏ);
			int num2 = this.ᜅ.Length - 1;
			int num3 = 0;
			for (;;)
			{
				if (true)
				{
				}
				switch (num3)
				{
				case 0:
					goto IL_D0;
				case 1:
					this.ᜅ[num2] = Convert.ToDouble(arrayList[num2]) - Convert.ToDouble(arrayList[num2 - 1]);
					goto IL_12B;
				case 2:
					goto IL_80;
				case 3:
					if (num2 < arrayList.Count)
					{
						num3 = 1;
						continue;
					}
					this.ᜅ[num2] = 0.0;
					num3 = 2;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_12B;
					default:
						goto IL_100;
					}
					break;
				case 5:
					if (num2 < 1)
					{
						num3 = 4;
						continue;
					}
					num3 = 3;
					continue;
				case 6:
					goto IL_D0;
				case 7:
					goto IL_80;
				}
				break;
				IL_80:
				num2--;
				num3 = 6;
				continue;
				IL_D0:
				num3 = 5;
				continue;
				IL_12B:
				num3 = 7;
			}
		}
		IL_100:
		if (false)
		{
		}
		this.ᜅ[0] = Convert.ToDouble(arrayList[0]);
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x000632D0 File Offset: 0x000622D0
	private bool ᜋ()
	{
		if (this.ᜐ() + 1 < this.\u1712().ᜄ())
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
			this.ᜄ = -1;
			this.ᜇ();
			this.ᜇ = new sprᦰ(this.\u1712().ᜁ(this.ᜐ()));
			this.ᜇ.ᜀ(new RectangleF(this.ᜅ.ᜇ().Location, default(SizeF)));
			return true;
		}
		return false;
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x00063380 File Offset: 0x00062380
	private void ᜊ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜌ = new sprᴛ[this.ᜏ.Rows[this.ᜐ()].Cells.Count];
				new ArrayList();
				int num = 13;
				for (;;)
				{
					spr\u25FC spr_u25FC;
					spr\u2573 spr_u2;
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (base.\u1717() != LayoutState.Unknown)
						{
							num = 0;
							continue;
						}
						goto IL_141;
					case 2:
						if (spr_u25FC.ᜂ() <= (double)this.ᜅ.ᜇ().Height)
						{
							goto IL_D8;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15C;
						default:
							if (false)
							{
							}
							num = 11;
							continue;
						}
						break;
					case 3:
					{
						RectangleF rectangleF;
						if (rectangleF.Height != this.ᜅ.ᜆ().Height)
						{
							num = 8;
							continue;
						}
						goto IL_D8;
					}
					case 4:
					{
						spr\u2032 spr_u;
						spr_u25FC = this.ᜀ(spr_u != null && spr_u.\u1714(), this.ᜐ(), this.ᜄ);
						num = 2;
						continue;
					}
					case 5:
					{
						if (spr_u2 == null)
						{
							goto IL_15C;
						}
						spr\u2032 spr_u = spr_u2.\u1718().ᜀ() as spr\u2032;
						TableCell tableCell = this.ᜏ.Rows[this.ᜐ()].Cells[this.ᜄ];
						num = 4;
						continue;
					}
					case 6:
						if (true)
						{
						}
						goto IL_228;
					case 7:
						(spr_u2.\u1718().ᜀ() as TableCell.ᜀ).ᜀ(false);
						this.ᜀ = LayoutState.NotFitted;
						num = 12;
						continue;
					case 8:
						num = 9;
						continue;
					case 9:
						if (!this.ᜏ.IsFrame)
						{
							num = 7;
							continue;
						}
						goto IL_D8;
					case 10:
						return;
					case 11:
					{
						RectangleF rectangleF = this.ᜅ.ᜇ();
						num = 3;
						continue;
					}
					case 12:
						goto IL_228;
					case 13:
						goto IL_141;
					}
					break;
					IL_D8:
					this.ᜀ(spr_u2, spr_u25FC.ᜆ());
					this.ᜃ(spr_u2);
					num = 6;
					continue;
					IL_141:
					spr_u2 = this.ᜃ();
					spr_u25FC = null;
					num = 5;
					continue;
					IL_15C:
					num = 10;
					continue;
					IL_228:
					num = 1;
				}
			}
			return;
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x00063608 File Offset: 0x00062608
	private void ᜉ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_96:
				this.ᜐ = 0;
				this.ᜑ = 0;
				bool flag = true;
				TableRow tableRow = this.\u1712().ᜁ(this.ᜐ()) as TableRow;
				Table table = tableRow.OwnerTable;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_22A:
					num = 19;
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
						goto IL_12D;
					case 1:
						if (this.\u170D == LayoutState.Splitted)
						{
							num = 7;
							continue;
						}
						goto IL_260;
					case 2:
						goto IL_165;
					case 3:
						if (this.ᜃ.ᜊ().Count > 0)
						{
							num = 22;
							continue;
						}
						return;
					case 4:
						if (this.ᜀ == LayoutState.Unknown)
						{
							num = 18;
							continue;
						}
						goto IL_1A5;
					case 5:
					{
						int num2;
						if (num2 >= this.ᜏ.Rows[this.ᜐ()].Cells.Count)
						{
							num = 8;
							continue;
						}
						this.ᜄ(num2);
						num2++;
						num = 11;
						continue;
					}
					case 6:
						if (table != null)
						{
							num = 28;
							continue;
						}
						return;
					case 7:
						num = 12;
						continue;
					case 8:
						num = 1;
						continue;
					case 9:
						this.ᜃ.ᜊ().ᜀ(this.ᜇ);
						this.ᜀ();
						this.ᜁ();
						num = 27;
						continue;
					case 10:
						if (this.ᜇ.ᜉ())
						{
							num = 16;
							continue;
						}
						return;
					case 11:
						goto IL_165;
					case 12:
						if (tableRow.RowFormat.IsBreakAcrossPages)
						{
							num = 24;
							continue;
						}
						goto IL_3F3;
					case 13:
					{
						this.ᜆ();
						int num2 = 0;
						num = 2;
						continue;
					}
					case 14:
						if (this.ᜋ)
						{
							num = 13;
							continue;
						}
						goto IL_1A5;
					case 15:
						if (flag)
						{
							num = 9;
							continue;
						}
						goto IL_3C9;
					case 16:
						num = 25;
						continue;
					case 17:
						goto IL_260;
					case 18:
						num = 14;
						continue;
					case 19:
						this.ᜁ = new sprᲲ(this.\u1712(), this.ᜃ + 1);
						num = 0;
						continue;
					case 20:
						goto IL_260;
					case 21:
						goto IL_2F8;
					case 22:
						if (true)
						{
						}
						num = 6;
						continue;
					case 23:
						if (!tableRow.IsHeader)
						{
							num = 29;
							continue;
						}
						goto IL_3F3;
					case 24:
						num = 23;
						continue;
					case 25:
						goto IL_212;
					case 26:
						this.ᜁ = new sprᲲ(this.\u1712(), this.ᜃ + 1);
						this.ᜀ = LayoutState.Splitted;
						num = 21;
						continue;
					case 27:
						goto IL_3C9;
					case 28:
						num = 30;
						continue;
					case 29:
						this.ᜁ = new sprᲲ(this.\u1712(), this.ᜐ() + 1, this.ᜌ);
						this.ᜀ = LayoutState.Splitted;
						num = 17;
						continue;
					case 30:
						if (!table.IsFrame)
						{
							num = 26;
							continue;
						}
						return;
					}
					goto IL_96;
					IL_165:
					num = 5;
					continue;
					IL_1A5:
					num = 3;
					continue;
					IL_260:
					num = 15;
					continue;
					IL_3C9:
					num = 10;
					continue;
					IL_3F3:
					this.ᜁ = new sprᲲ(this.\u1712(), this.ᜐ() + 1);
					tableRow.IsRowCanSplit = false;
					this.ᜀ = LayoutState.Splitted;
					flag = false;
					num = 20;
				}
				IL_212:
				if (this.ᜃ < this.\u1712().ᜄ() - 1)
				{
					goto IL_22A;
				}
				break;
			}
			IL_12D:
			goto IL_3C1;
			IL_2F8:
			return;
			IL_3C1:
			this.ᜀ = LayoutState.Breaked;
			return;
		}
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x00063A44 File Offset: 0x00062A44
	private new bool ᜅ(sprᦰ A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_125;
			case 1:
			{
				sprᦰ sprᦰ;
				if ((sprᦰ.ᜂ() as Break).BreakType == BreakType.PageBreak)
				{
					num = 8;
					continue;
				}
				return false;
			}
			case 2:
			{
				sprᦰ sprᦰ = A_0;
				num = 0;
				continue;
			}
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_125;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 5:
				goto IL_125;
			case 6:
				num = 10;
				continue;
			case 7:
			{
				if (true)
				{
				}
				sprᦰ sprᦰ;
				if (sprᦰ != null)
				{
					num = 6;
					continue;
				}
				return false;
			}
			case 8:
				return true;
			case 9:
				num = 1;
				continue;
			case 10:
			{
				sprᦰ sprᦰ;
				if (sprᦰ.ᜂ() is Break)
				{
					num = 9;
					continue;
				}
				return false;
			}
			case 11:
			{
				sprᦰ sprᦰ;
				if (sprᦰ.ᜊ().Count <= 0)
				{
					num = 4;
					continue;
				}
				sprᦰ = sprᦰ.ᜊ()[sprᦰ.ᜊ().Count - 1];
				num = 5;
				continue;
			}
			}
			if (A_0 != null)
			{
				num = 2;
				continue;
			}
			return false;
			IL_125:
			num = 11;
		}
		return true;
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x00063BA0 File Offset: 0x00062BA0
	private new void ᜄ(spr\u2573 A_0)
	{
		switch (0)
		{
		default:
		{
			RectangleF rectangleF;
			SizeF size;
			for (;;)
			{
				if (true)
				{
				}
				rectangleF = this.ᜇ.ᜁ();
				RectangleF rectangleF2 = this.ᜈ.ᜁ();
				int num = 9;
				for (;;)
				{
					double num2;
					double num4;
					double num3;
					double num6;
					switch (num)
					{
					case 0:
						goto IL_246;
					case 1:
						if (((A_0.\u1718() as TableCell).Owner.Owner as Table).FrameFormat.FrameHeightRule != FrameSizeRule.Auto)
						{
							num = 22;
							continue;
						}
						goto IL_1AA;
					case 2:
						if (((A_0.\u1718() as TableCell).Owner.Owner as Table).FrameFormat.FrameHeight != 0)
						{
							num = 7;
							continue;
						}
						goto IL_1AA;
					case 3:
						num2 = 0.0;
						goto IL_259;
					case 4:
						num2 = A_0.\u1715();
						goto IL_259;
					case 5:
						num = 14;
						continue;
					case 6:
					{
						FrameSizeRule frameSizeRule;
						switch (frameSizeRule)
						{
						case FrameSizeRule.AtLeast:
							num3 = Math.Max((double)rectangleF2.Bottom + num4, (double)rectangleF.Bottom);
							num = 16;
							continue;
						case FrameSizeRule.Exact:
						{
							float num5;
							num3 = (double)num5;
							num = 8;
							continue;
						}
						default:
							num = 5;
							continue;
						}
						break;
					}
					case 7:
					{
						float num5 = ((A_0.\u1718() as TableCell).Owner.Owner as Table).FrameFormat.FrameHeightEx;
						num3 = 0.0;
						FrameSizeRule frameSizeRule = ((A_0.\u1718() as TableCell).Owner.Owner as Table).FrameFormat.FrameHeightRule;
						num = 6;
						continue;
					}
					case 8:
						goto IL_149;
					case 9:
						if (!this.ᜄ)
						{
							num = 13;
							continue;
						}
						num = 3;
						continue;
					case 10:
						num6 = A_0.\u171D();
						goto IL_F9;
					case 11:
						num6 = 0.0;
						goto IL_F9;
					case 12:
						if (A_0.\u1718() is TableCell)
						{
							num = 21;
							continue;
						}
						goto IL_1AA;
					case 13:
						num = 4;
						continue;
					case 14:
						goto IL_149;
					case 15:
						goto IL_174;
					case 16:
						goto IL_149;
					case 17:
						num = 10;
						continue;
					case 18:
						if (!this.ᜄ)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_246;
							}
							if (false)
							{
							}
							num = 17;
							continue;
						}
						num = 11;
						continue;
					case 19:
						goto IL_1EE;
					case 20:
						if (((A_0.\u1718() as TableCell).Owner.Owner as Table).IsFrame)
						{
							num = 0;
							continue;
						}
						goto IL_1AA;
					case 21:
						num = 20;
						continue;
					case 22:
						num = 2;
						continue;
					}
					break;
					IL_F9:
					num4 = num6;
					size = rectangleF2.Size;
					double num8;
					double num7 = Math.Max((double)rectangleF2.Right + num8, (double)rectangleF.Right);
					num = 12;
					continue;
					IL_149:
					size = new SizeF((float)(num7 - (double)rectangleF.Left), (float)(num3 - (double)rectangleF.Top));
					num = 15;
					continue;
					IL_1AA:
					double num9 = Math.Max((double)rectangleF2.Bottom + num4, (double)rectangleF.Bottom);
					size = new SizeF((float)(num7 - (double)rectangleF.Left), (float)(num9 - (double)rectangleF.Top));
					num = 19;
					continue;
					IL_246:
					num = 1;
					continue;
					IL_259:
					num8 = num2;
					num = 18;
				}
			}
			IL_174:
			IL_1EE:
			this.ᜇ.ᜀ(new RectangleF(rectangleF.Location, size));
			return;
		}
		}
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x00063F98 File Offset: 0x00062F98
	private new void ᜇ()
	{
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
				{
					TableRow tableRow;
					if (tableRow != null)
					{
						num = 9;
						continue;
					}
					goto IL_133;
				}
				case 2:
				{
					Table table = this.\u1712() as Table;
					TableRow tableRow = table.Rows[this.ᜃ];
					num = 1;
					continue;
				}
				case 3:
				{
					TableRow tableRow2;
					if (tableRow2.IsHeader)
					{
						num = 12;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_12E;
					default:
						goto IL_AF;
					}
					break;
				}
				case 4:
					return;
				case 5:
				{
					Table table;
					int num2;
					if (num2 >= table.Rows.Count)
					{
						num = 4;
						continue;
					}
					TableRow tableRow2 = table.Rows[num2];
					num = 3;
					continue;
				}
				case 6:
				{
					int num2 = 0;
					num = 7;
					continue;
				}
				case 7:
					goto IL_6A;
				case 8:
					goto IL_12E;
				case 9:
					num = 10;
					continue;
				case 10:
				{
					TableRow tableRow;
					if (!tableRow.IsHeader)
					{
						num = 6;
						continue;
					}
					goto IL_133;
				}
				case 11:
				{
					int num2;
					num2++;
					num = 8;
					continue;
				}
				case 12:
				{
					if (true)
					{
					}
					TableRow tableRow2;
					this.ᜁ(tableRow2);
					num = 11;
					continue;
				}
				}
				if (this.ᜁ)
				{
					num = 2;
					continue;
				}
				goto IL_19D;
				IL_6A:
				num = 5;
				continue;
				IL_12E:
				goto IL_6A;
			}
			return;
			IL_AF:
			if (false)
			{
			}
			this.ᜁ = false;
			return;
			IL_133:
			this.ᜁ = false;
			return;
			IL_19D:
			this.ᜃ++;
			return;
		}
		}
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00064150 File Offset: 0x00063150
	private new void ᜁ(TableRow A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 24;
			for (;;)
			{
				int num2;
				int num4;
				switch (num)
				{
				case 0:
					goto IL_420;
				case 1:
				{
					Paragraph paragraph;
					if (num2 >= paragraph.Items.Count)
					{
						num = 22;
						continue;
					}
					num = 5;
					continue;
				}
				case 2:
					this.ᜂ++;
					num = 10;
					continue;
				case 3:
					num = 12;
					continue;
				case 4:
					goto IL_1BA;
				case 5:
				{
					Paragraph paragraph;
					if (paragraph.Items[num2] is TextRange)
					{
						num = 19;
						continue;
					}
					goto IL_399;
				}
				case 6:
				{
					this.ᜇ = new sprᦰ(this.\u1712().ᜁ(this.ᜐ()));
					this.ᜇ.ᜀ(new RectangleF(this.ᜅ.ᜇ().Location, default(SizeF)));
					int num3 = 0;
					num = 4;
					continue;
				}
				case 7:
					goto IL_1BA;
				case 8:
				{
					int num3;
					num3++;
					num = 7;
					continue;
				}
				case 9:
				{
					int num3;
					if (num4 >= (this.ᜇ.ᜂ() as TableRow).Cells[num3].Items.Count)
					{
						num = 8;
						continue;
					}
					num = 16;
					continue;
				}
				case 10:
					if (this.ᜃ != this.ᜂ)
					{
						num = 6;
						continue;
					}
					this.ᜃ++;
					num = 17;
					continue;
				case 11:
					goto IL_399;
				case 12:
					if (A_0.NextSibling is TableRow)
					{
						num = 18;
						continue;
					}
					goto IL_1F7;
				case 13:
					goto IL_1FF;
				case 14:
					num2 = 0;
					num = 21;
					continue;
				case 15:
					goto IL_420;
				case 16:
				{
					int num3;
					if ((this.ᜇ.ᜂ() as TableRow).Cells[num3].Items[num4] is Paragraph)
					{
						num = 20;
						continue;
					}
					goto IL_10E;
				}
				case 17:
					return;
				case 18:
					num = 25;
					continue;
				case 19:
				{
					Paragraph paragraph;
					(paragraph.Items[num2] as TextRange).TextToSplit = (paragraph.Items[num2] as TextRange).Text;
					num = 11;
					continue;
				}
				case 20:
				{
					int num3;
					Paragraph paragraph = (this.ᜇ.ᜂ() as TableRow).Cells[num3].Items[num4] as Paragraph;
					num = 27;
					continue;
				}
				case 21:
					goto IL_1FF;
				case 22:
					goto IL_10E;
				case 23:
					this.ᜊ();
					this.ᜉ();
					this.ᜄ = -1;
					num = 26;
					continue;
				case 25:
					if ((A_0.NextSibling as TableRow).IsHeader)
					{
						num = 29;
						continue;
					}
					goto IL_1F7;
				case 26:
					if (A_0.NextSibling != null)
					{
						num = 3;
						continue;
					}
					goto IL_1F7;
				case 27:
				{
					Paragraph paragraph;
					if (paragraph != null)
					{
						num = 14;
						continue;
					}
					goto IL_10E;
				}
				case 28:
				{
					int num3;
					if (num3 >= (this.ᜇ.ᜂ() as TableRow).Cells.Count)
					{
						num = 23;
						continue;
					}
					num4 = 0;
					if (true)
					{
					}
					num = 0;
					continue;
				}
				case 29:
					goto IL_19A;
				}
				if (!A_0.IsHeader)
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1BA;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				IL_10E:
				num4++;
				num = 15;
				continue;
				IL_1BA:
				num = 28;
				continue;
				IL_1FF:
				num = 1;
				continue;
				IL_399:
				num2++;
				num = 13;
				continue;
				IL_420:
				num = 9;
			}
			IL_19A:
			A_0 = (A_0.NextSibling as TableRow);
			return;
			IL_1F7:
			this.ᜁ = false;
			return;
		}
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x000645C8 File Offset: 0x000635C8
	private new void ᜆ()
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			RectangleF a_2;
			for (;;)
			{
				a_2 = this.ᜇ.ᜁ();
				this.ᜇ.ᜂ();
				double num = 0.0;
				int num2 = 0;
				int num3 = 43;
				for (;;)
				{
					double num5;
					int num6;
					int num8;
					switch (num3)
					{
					case 0:
					{
						spr\u2032 spr_u;
						if (spr_u.ᜌ())
						{
							num3 = 39;
							continue;
						}
						goto IL_2C0;
					}
					case 1:
						if (this.ᜐ() != this.\u1712().ᜄ() - 1)
						{
							num3 = 14;
							continue;
						}
						goto IL_7DA;
					case 2:
						goto IL_6E0;
					case 3:
						goto IL_746;
					case 4:
					{
						int num4;
						if (num4 >= this.ᜇ.ᜊ().Count)
						{
							num3 = 40;
							continue;
						}
						num3 = 49;
						continue;
					}
					case 5:
						num3 = 34;
						continue;
					case 6:
						if (num5 > 0.0)
						{
							num3 = 5;
							continue;
						}
						goto IL_297;
					case 7:
					{
						spr\u2032 spr_u2;
						if (spr_u2.ᜐ())
						{
							num3 = 30;
							continue;
						}
						goto IL_297;
					}
					case 8:
						goto IL_76B;
					case 9:
						goto IL_56D;
					case 10:
						num3 = 48;
						continue;
					case 11:
						goto IL_7B2;
					case 12:
						num6 = num2;
						num3 = 51;
						continue;
					case 13:
					{
						float num7;
						a_2.Height = (float)Math.Max((double)num7, num5);
						num3 = 31;
						continue;
					}
					case 14:
					{
						int a_3 = this.ᜅ(this.ᜐ(), num2);
						spr\u17C8 spr_u17C = this.\u1712().ᜀ(this.ᜐ() + 1, a_3);
						bool flag = (spr_u17C.ᜀ() as spr\u2032).\u1716();
						num3 = 57;
						continue;
					}
					case 15:
					{
						bool flag2;
						if (!flag2)
						{
							num3 = 21;
							continue;
						}
						goto IL_219;
					}
					case 16:
						goto IL_79C;
					case 17:
					{
						bool flag;
						if (!flag)
						{
							num3 = 12;
							continue;
						}
						goto IL_2C0;
					}
					case 18:
					{
						if (num8 >= this.ᜇ.ᜊ().Count)
						{
							num3 = 13;
							continue;
						}
						float height = this.ᜇ.ᜊ()[num8].ᜁ().Height;
						num3 = 45;
						continue;
					}
					case 19:
						goto IL_30A;
					case 20:
						goto IL_2C0;
					case 21:
						num3 = 0;
						continue;
					case 22:
						goto IL_280;
					case 23:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_746;
						default:
							if (false)
							{
							}
							num3 = 46;
							continue;
						}
						break;
					case 24:
					{
						bool flag3 = true;
						num3 = 25;
						continue;
					}
					case 25:
						goto IL_837;
					case 26:
					{
						sprᦰ sprᦰ;
						num6 = this.ᜁ(sprᦰ.ᜁ().X, num2);
						num3 = 52;
						continue;
					}
					case 27:
					{
						float height2 = base.\u171E().ᜁ(ClipboardData.b("䥨", a_), this.ᜏ.Rows[this.ᜐ()].Cells[0].LastParagraph.BreakCharacterFormat.Font, null).Height;
						a_2.Height = (float)num5 + height2;
						num3 = 2;
						continue;
					}
					case 28:
						num3 = 35;
						continue;
					case 29:
						goto IL_605;
					case 30:
						num3 = 6;
						continue;
					case 31:
						goto IL_7D5;
					case 32:
					{
						spr\u2032 spr_u2 = this.ᜇ.ᜂ().ᜀ() as spr\u2032;
						num5 = spr_u2.ᜎ();
						num3 = 42;
						continue;
					}
					case 33:
						num3 = 59;
						continue;
					case 34:
						if (this.ᜏ.\u1712 != null)
						{
							num3 = 28;
							continue;
						}
						goto IL_79C;
					case 35:
						if (this.ᜏ.\u1712.IsFitTextToShape)
						{
							num3 = 55;
							continue;
						}
						goto IL_79C;
					case 36:
						num5 += (double)((float)num);
						num3 = 29;
						continue;
					case 37:
						goto IL_4C4;
					case 38:
						goto IL_76B;
					case 39:
						goto IL_219;
					case 40:
						goto IL_837;
					case 41:
						if (num5 > (double)this.ᜈ.ᜁ().Height)
						{
							num3 = 3;
							continue;
						}
						goto IL_79C;
					case 42:
					{
						spr\u2032 spr_u2;
						if (!spr_u2.ᜐ())
						{
							num3 = 36;
							continue;
						}
						goto IL_605;
					}
					case 43:
						goto IL_5D2;
					case 44:
					{
						if (num2 >= this.ᜇ.ᜊ().Count)
						{
							num3 = 32;
							continue;
						}
						sprᦰ sprᦰ = this.ᜇ.ᜊ()[num2];
						spr\u2032 spr_u = sprᦰ.ᜂ().ᜀ() as spr\u2032;
						bool flag2 = spr_u.\u1716();
						bool flag = false;
						num = Math.Max(num, sprᦰ.ᜂ().ᜀ().ᜊ().ᜀ() + sprᦰ.ᜂ().ᜀ().ᜊ().ᜁ());
						num3 = 1;
						continue;
					}
					case 45:
						if (!this.ᜇ.ᜊ()[num8].ᜂ().ᜀ().ᜈ())
						{
							num3 = 23;
							continue;
						}
						goto IL_280;
					case 46:
					{
						float num7;
						float height;
						if (num7 < height)
						{
							num3 = 58;
							continue;
						}
						goto IL_280;
					}
					case 47:
					{
						spr\u2032 spr_u2;
						if (spr_u2.ᜫ())
						{
							num3 = 33;
							continue;
						}
						bool flag3 = false;
						int num4 = 0;
						num3 = 37;
						continue;
					}
					case 48:
						if (this.ᜃ.ᜊ().Count > 0)
						{
							num3 = 26;
							continue;
						}
						goto IL_588;
					case 49:
					{
						int num4;
						if (this.ᜇ.ᜊ()[num4].ᜂ().ᜀ().ᜈ())
						{
							num3 = 24;
							continue;
						}
						num4++;
						if (true)
						{
						}
						num3 = 53;
						continue;
					}
					case 50:
					{
						float num7 = 0f;
						num8 = 0;
						num3 = 38;
						continue;
					}
					case 51:
						if (this.ᜐ() > 0)
						{
							num3 = 10;
							continue;
						}
						goto IL_588;
					case 52:
						goto IL_588;
					case 53:
						goto IL_4C4;
					case 54:
						goto IL_5D2;
					case 55:
						num3 = 41;
						continue;
					case 56:
					{
						bool flag3;
						if (flag3)
						{
							num3 = 50;
							continue;
						}
						a_2.Height = (float)Math.Max((double)a_2.Height, num5);
						num3 = 19;
						continue;
					}
					case 57:
						goto IL_7DA;
					case 58:
					{
						float height;
						float num7 = height;
						num3 = 22;
						continue;
					}
					case 59:
					{
						spr\u2032 spr_u2;
						if (spr_u2.ᜎ() == 0.0)
						{
							num3 = 27;
							continue;
						}
						a_2.Height = (float)num5;
						num3 = 9;
						continue;
					}
					}
					break;
					IL_219:
					num3 = 17;
					continue;
					IL_280:
					num8++;
					num3 = 8;
					continue;
					IL_297:
					num3 = 47;
					continue;
					IL_2C0:
					num2++;
					num3 = 54;
					continue;
					IL_4C4:
					num3 = 4;
					continue;
					IL_588:
					a_2.Height = (float)Math.Max(this.ᜉ[num6], (double)a_2.Height);
					num3 = 20;
					continue;
					IL_5D2:
					num3 = 44;
					continue;
					IL_605:
					a_2.Width = this.ᜅ.ᜇ().Width;
					num3 = 7;
					continue;
					IL_746:
					num5 = (double)this.ᜈ.ᜁ().Height;
					num3 = 16;
					continue;
					IL_76B:
					num3 = 18;
					continue;
					IL_79C:
					a_2.Height = (float)num5;
					num3 = 11;
					continue;
					IL_7DA:
					num3 = 15;
					continue;
					IL_837:
					num3 = 56;
				}
			}
			IL_30A:
			IL_56D:
			IL_6E0:
			IL_7B2:
			IL_7D5:
			this.ᜇ.ᜀ(a_2);
			return;
		}
		}
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x00064E5C File Offset: 0x00063E5C
	private new int ᜅ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int result;
			int num5;
			for (;;)
			{
				result = 0;
				float num = 0f;
				int num2 = 0;
				int num3 = 12;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						float num4;
						if (num == num4)
						{
							num3 = 10;
							continue;
						}
						num5++;
						num3 = 8;
						continue;
					}
					case 1:
						if (num2 >= A_1)
						{
							num3 = 13;
							continue;
						}
						num += (this.\u1712() as Table).Rows[A_0].Cells[num2].Width;
						num2++;
						num3 = 11;
						continue;
					case 2:
						goto IL_163;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_191;
						default:
							if (false)
							{
							}
							result = num5;
							num3 = 9;
							continue;
						}
						break;
					case 4:
						if (num5 == (this.\u1712() as Table).Rows[A_0 + 1].Cells.Count - 1)
						{
							num3 = 6;
							continue;
						}
						result = num5 + 1;
						num3 = 7;
						continue;
					case 5:
						goto IL_191;
					case 6:
						goto IL_1D6;
					case 7:
						goto IL_11D;
					case 8:
						goto IL_122;
					case 9:
						goto IL_177;
					case 10:
						num3 = 4;
						continue;
					case 11:
						goto IL_92;
					case 12:
						goto IL_92;
					case 13:
					{
						float num4 = 0f;
						num5 = 0;
						num3 = 5;
						continue;
					}
					case 14:
					{
						float num4;
						if (num < num4)
						{
							num3 = 3;
							continue;
						}
						num3 = 0;
						continue;
					}
					case 15:
					{
						if (num5 >= (this.\u1712() as Table).Rows[A_0 + 1].Cells.Count)
						{
							num3 = 2;
							continue;
						}
						float num4;
						num4 += (this.\u1712() as Table).Rows[A_0 + 1].Cells[num5].Width;
						num3 = 14;
						continue;
					}
					}
					break;
					IL_92:
					num3 = 1;
					continue;
					IL_122:
					num3 = 15;
					continue;
					IL_191:
					goto IL_122;
				}
			}
			IL_11D:
			IL_163:
			IL_177:
			return result;
			IL_1D6:
			if (true)
			{
			}
			return num5;
		}
		}
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x000650B4 File Offset: 0x000640B4
	private new void ᜄ(int A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
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
					this.ᜃ(A_0);
					this.ᜂ(A_0);
					num = 2;
					continue;
				}
				break;
			case 2:
				return;
			}
			if (A_0 >= this.ᜇ.ᜊ().Count)
			{
				break;
			}
			num = 1;
		}
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00065140 File Offset: 0x00064140
	private new void ᜃ(int A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_167:
				sprᦰ sprᦰ = this.ᜇ.ᜊ()[A_0];
				RectangleF a_2 = sprᦰ.ᜁ();
				this.ᜄ = A_0;
				for (;;)
				{
					IL_187:
					int num = 37;
					for (;;)
					{
						TableCell tableCell;
						float num2;
						TableCell tableCell3;
						float num3;
						RectangleF rectangleF2;
						switch (num)
						{
						case 0:
						{
							float width = (sprᦰ.ᜂ() as TableCell).Width;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_187;
							default:
								if (false)
								{
								}
								num = 54;
								continue;
							}
							break;
						}
						case 1:
						{
							Section section;
							int index = this.ᜀ(section);
							RectangleF rectangleF = (this.ᜆ as spr\u1DA4).ᜈ();
							num = 39;
							continue;
						}
						case 2:
							a_2.Width = (float)(sprᦰ.ᜂ().ᜀ().ᜊ().ᜃ() + sprᦰ.ᜂ().ᜀ().ᜊ().ᜂ() + sprᦰ.ᜂ().ᜀ().ᜋ().ᜃ() + sprᦰ.ᜂ().ᜀ().ᜋ().ᜂ());
							num = 16;
							continue;
						case 3:
							if (this.ᜏ.TableGrid.Count - 1 > this.ᜄ)
							{
								num = 28;
								continue;
							}
							goto IL_210;
						case 4:
							if (A_0 - this.ᜑ == this.ᜅ.Length - 1)
							{
								num = 14;
								continue;
							}
							goto IL_857;
						case 5:
							if (a_2.Width == 0f)
							{
								num = 27;
								continue;
							}
							goto IL_210;
						case 6:
							num = 75;
							continue;
						case 7:
							if (sprᦰ.ᜂ() is TableCell)
							{
								num = 66;
								continue;
							}
							goto IL_23E;
						case 8:
							goto IL_9D8;
						case 9:
							if ((sprᦰ.ᜂ() as TableCell).Colspan > 1)
							{
								num = 74;
								continue;
							}
							goto IL_5D1;
						case 10:
						{
							float width;
							a_2.Width = width;
							num = 40;
							continue;
						}
						case 11:
							if (this.ᜏ.Rows[this.ᜐ()].Cells[this.ᜄ].GridSpan > 1)
							{
								num = 56;
								continue;
							}
							goto IL_9D8;
						case 12:
							goto IL_980;
						case 13:
							if (this.ᜏ.Rows[this.ᜐ()].Cells.Count < this.ᜅ.Length)
							{
								num = 47;
								continue;
							}
							goto IL_9D8;
						case 14:
							a_2.Width = this.ᜂ(this.ᜐ(), this.ᜄ);
							num = 59;
							continue;
						case 15:
						{
							Section section = tableCell.OwnerRow.OwnerTable.OwnerTextBody.OwnerBase as Section;
							num = 68;
							continue;
						}
						case 16:
							goto IL_410;
						case 17:
							A_0 += this.ᜑ;
							num = 50;
							continue;
						case 18:
							if (sprᦰ.ᜂ() is TableCell)
							{
								num = 20;
								continue;
							}
							goto IL_5D1;
						case 19:
							if (tableCell.OwnerRow.OwnerTable.OwnerTextBody.OwnerBase != null)
							{
								num = 38;
								continue;
							}
							goto IL_980;
						case 20:
							num = 22;
							continue;
						case 21:
							if (tableCell.OwnerRow.OwnerTable.OwnerTextBody != null)
							{
								num = 55;
								continue;
							}
							goto IL_980;
						case 22:
							if (a_2.Width != (sprᦰ.ᜂ() as TableCell).Width)
							{
								num = 26;
								continue;
							}
							goto IL_5D1;
						case 23:
							a_2.Width -= this.ᜏ.TableFormat.CellSpacing * 2f;
							num = 67;
							continue;
						case 24:
							num = 61;
							continue;
						case 25:
							goto IL_210;
						case 26:
							num = 9;
							continue;
						case 27:
							num = 57;
							continue;
						case 28:
							a_2.Width = (float)this.ᜁ(this.ᜐ(), this.ᜄ);
							num = 25;
							continue;
						case 29:
						{
							TableCell tableCell2;
							num2 = tableCell2.Width / 20f;
							goto IL_4AF;
						}
						case 30:
							num = 3;
							continue;
						case 31:
						{
							TableCell tableCell2 = (sprᦰ.ᜂ() as sprᴛ).ᜁ() as TableCell;
							num = 49;
							continue;
						}
						case 32:
							if (!(sprᦰ.ᜂ() is TableCell))
							{
								num = 53;
								continue;
							}
							num = 71;
							continue;
						case 33:
							if ((int)this.ᜏ.Rows[this.ᜐ()].Cells[this.ᜄ].GridSpan <= this.ᜅ.Length)
							{
								num = 35;
								continue;
							}
							goto IL_9D8;
						case 34:
							goto IL_857;
						case 35:
							num = 13;
							continue;
						case 36:
							goto IL_8C2;
						case 37:
							if (this.ᜑ > 0)
							{
								num = 17;
								continue;
							}
							goto IL_5FE;
						case 38:
							num = 69;
							continue;
						case 39:
						{
							Section section;
							int index;
							RectangleF rectangleF;
							if (rectangleF.Right + section.Columns[index].Space < sprᦰ.ᜁ().Right)
							{
								num = 79;
								continue;
							}
							goto IL_980;
						}
						case 40:
							goto IL_23E;
						case 41:
							a_2.Width = (((sprᦰ.ᜂ() as TableCell).WidthType == FtsWidth.Percentage) ? ((sprᦰ.ᜂ() as TableCell).Width / 20f) : (sprᦰ.ᜂ() as TableCell).Width);
							num = 36;
							continue;
						case 42:
						{
							TableCell tableCell2;
							num2 = tableCell2.Width;
							goto IL_4AF;
						}
						case 43:
						{
							TableCell tableCell2;
							if (tableCell2.Colspan > 1)
							{
								num = 63;
								continue;
							}
							goto IL_8C2;
						}
						case 44:
							tableCell3 = ((sprᦰ.ᜂ() as sprᴛ).ᜁ() as TableCell);
							goto IL_475;
						case 45:
							return;
						case 46:
							num = 43;
							continue;
						case 47:
							a_2.Width = (float)this.ᜁ(this.ᜐ(), this.ᜄ);
							num = 8;
							continue;
						case 48:
							if (a_2.Width >= num3)
							{
								num = 24;
								continue;
							}
							goto IL_45B;
						case 49:
						{
							TableCell tableCell2;
							if (tableCell2.WidthType != FtsWidth.Percentage)
							{
								num = 70;
								continue;
							}
							num = 29;
							continue;
						}
						case 50:
							goto IL_5FE;
						case 51:
							if (rectangleF2.Right < sprᦰ.ᜁ().Right)
							{
								num = 80;
								continue;
							}
							goto IL_980;
						case 52:
							if (sprᦰ.ᜂ() is sprᴛ)
							{
								num = 6;
								continue;
							}
							goto IL_8C2;
						case 53:
							num = 44;
							continue;
						case 54:
						{
							float width;
							if ((double)(a_2.Width - width) <= 0.5)
							{
								num = 10;
								continue;
							}
							goto IL_23E;
						}
						case 55:
							num = 19;
							continue;
						case 56:
							num = 33;
							continue;
						case 57:
							if (this.ᜏ.TableGrid.Count > 1)
							{
								num = 30;
								continue;
							}
							goto IL_210;
						case 58:
							if (a_2.Width == 0f)
							{
								num = 2;
								continue;
							}
							goto IL_410;
						case 59:
							goto IL_857;
						case 60:
							if (!((spr\u1AE4)tableCell.OwnerRow.OwnerTable).ᜂ().ᜈ())
							{
								num = 76;
								continue;
							}
							goto IL_980;
						case 61:
							if (a_2.Width > num3)
							{
								num = 46;
								continue;
							}
							goto IL_8C2;
						case 62:
							if (sprᦰ.ᜅ() == ClipboardData.b("ⅱέήࡷ⡹ᕻ᥽", a_))
							{
								num = 65;
								continue;
							}
							return;
						case 63:
							goto IL_45B;
						case 64:
							if (this.ᜏ.TableFormat.CellSpacing > 0f)
							{
								num = 23;
								continue;
							}
							goto IL_36A;
						case 65:
							this.ᜀ(sprᦰ, (float)((double)sprᦰ.ᜁ().Right - (sprᦰ.ᜂ().ᜀ().ᜋ().ᜃ() + sprᦰ.ᜂ().ᜀ().ᜋ().ᜂ())));
							num = 45;
							continue;
						case 66:
							num = 77;
							continue;
						case 67:
							goto IL_36A;
						case 68:
						{
							Section section;
							if (section.Columns.Count > 1)
							{
								num = 1;
								continue;
							}
							goto IL_980;
						}
						case 69:
							if (tableCell.OwnerRow.OwnerTable.OwnerTextBody.OwnerBase is Section)
							{
								num = 15;
								continue;
							}
							goto IL_980;
						case 70:
							num = 42;
							continue;
						case 71:
							tableCell3 = (sprᦰ.ᜂ() as TableCell);
							goto IL_475;
						case 72:
							goto IL_8C2;
						case 73:
							if (A_0 >= this.ᜅ.Length)
							{
								num = 78;
								continue;
							}
							a_2.Width = (float)((double)this.ᜂ(this.ᜐ(), this.ᜄ) + this.ᜄ());
							num = 34;
							continue;
						case 74:
							num = 41;
							continue;
						case 75:
							if ((sprᦰ.ᜂ() as sprᴛ).ᜁ() is TableCell)
							{
								num = 31;
								continue;
							}
							goto IL_8C2;
						case 76:
							num = 21;
							continue;
						case 77:
							if (a_2.Width > (sprᦰ.ᜂ() as TableCell).Width)
							{
								num = 0;
								continue;
							}
							goto IL_23E;
						case 78:
							num = 4;
							continue;
						case 79:
						{
							sprᦰ.ᜀ(ClipboardData.b("ㅱᡳήࡷ੹᥻᩽", a_));
							Section section;
							int index;
							sprᦰ.ᜀ((this.ᜆ as spr\u1DA4).ᜈ().Right + section.Columns[index].Space);
							num = 12;
							continue;
						}
						case 80:
							num = 32;
							continue;
						}
						goto IL_167;
						IL_210:
						num = 58;
						continue;
						IL_23E:
						num = 18;
						continue;
						IL_36A:
						sprᦰ.ᜀ(a_2);
						rectangleF2 = (this.ᜆ as spr\u1DA4).ᜈ();
						num = 51;
						continue;
						IL_410:
						num = 64;
						continue;
						IL_45B:
						a_2.Width = num3;
						num = 72;
						continue;
						IL_475:
						tableCell = tableCell3;
						num = 60;
						continue;
						IL_4AF:
						num3 = num2;
						num = 48;
						continue;
						IL_5D1:
						num = 52;
						continue;
						IL_5FE:
						num = 73;
						continue;
						IL_857:
						num = 11;
						continue;
						IL_8C2:
						num = 5;
						continue;
						IL_980:
						if (true)
						{
						}
						num = 62;
						continue;
						IL_9D8:
						num = 7;
					}
				}
			}
			return;
		}
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x00065D50 File Offset: 0x00064D50
	private new int ᜀ(Section A_0)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int result;
			for (;;)
			{
				ColumnCollection columns = A_0.Columns;
				result = 0;
				float num = 0f;
				int num2 = 0;
				int num3 = 1;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_E1;
					case 1:
						goto IL_E1;
					case 2:
						goto IL_10E;
					case 3:
					{
						RectangleF rectangleF;
						if (rectangleF.X - A_0.PageSetup.Margins.Left - num <= columns[num2].Width)
						{
							goto IL_D3;
						}
						num += columns[num2].Width + columns[num2].Space;
						num2++;
						num3 = 0;
						continue;
					}
					case 4:
						goto IL_10E;
					case 5:
					{
						if (num2 >= columns.Count)
						{
							num3 = 4;
							continue;
						}
						RectangleF rectangleF = this.ᜅ.ᜆ();
						num3 = 3;
						continue;
					}
					case 6:
						result = num2;
						num3 = 2;
						continue;
					}
					break;
					IL_D3:
					num3 = 6;
					continue;
					IL_10E:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D3;
					default:
						goto IL_124;
					}
					IL_E1:
					num3 = 5;
				}
			}
			IL_124:
			if (false)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00065E88 File Offset: 0x00064E88
	private new void ᜂ(int A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				sprᦰ sprᦰ = this.ᜇ.ᜊ()[A_0];
				RectangleF a_ = sprᦰ.ᜁ();
				RectangleF rectangleF = this.ᜇ.ᜁ();
				bool flag = (sprᦰ.ᜂ().ᜀ() as spr\u2032).\u1716();
				int num = 18;
				for (;;)
				{
					float num3;
					switch (num)
					{
					case 0:
						goto IL_3EC;
					case 1:
						if (this.ᜐ() != this.ᜏ.Rows.Count - 1)
						{
							num = 31;
							continue;
						}
						goto IL_3EC;
					case 2:
						num = 29;
						continue;
					case 3:
					{
						spr\u17C8 spr_u17C = this.\u1712().ᜀ(this.ᜐ(), A_0);
						(spr_u17C.ᜀ() as spr\u2032).\u1716();
						num = 27;
						continue;
					}
					case 4:
						num = 33;
						continue;
					case 5:
					{
						double num2 = this.ᜉ[this.ᜄ] - (double)rectangleF.Height;
						num = 6;
						continue;
					}
					case 6:
					{
						double num2;
						this.ᜉ[this.ᜄ] = ((num2 > 0.0) ? num2 : 0.0);
						num = 8;
						continue;
					}
					case 7:
						this.ᜄ(sprᦰ);
						num = 14;
						continue;
					case 8:
						goto IL_1DC;
					case 9:
						num = 24;
						continue;
					case 10:
						if (a_.Height < rectangleF.Height)
						{
							num = 17;
							continue;
						}
						goto IL_349;
					case 11:
						num = 30;
						continue;
					case 12:
						if (sprᦰ.ᜊ()[0].ᜊ().Count > 0)
						{
							num = 9;
							continue;
						}
						return;
					case 13:
						goto IL_30B;
					case 14:
						return;
					case 15:
						if (flag)
						{
							num = 32;
							continue;
						}
						goto IL_30B;
					case 16:
						goto IL_1DC;
					case 17:
						a_.Height = rectangleF.Height;
						num = 23;
						continue;
					case 18:
						if (this.ᜐ() != this.\u1712().ᜄ() - 1)
						{
							num = 3;
							continue;
						}
						goto IL_4FF;
					case 19:
						if (!this.\u1712)
						{
							goto IL_2A2;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D4;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 20:
						goto IL_475;
					case 21:
						num3 = this.ᜏ.TableFormat.CellSpacing * 2f;
						num = 20;
						continue;
					case 22:
						if (this.ᜐ() > 0)
						{
							num = 4;
							continue;
						}
						goto IL_2D4;
					case 23:
						goto IL_349;
					case 24:
						if ((this.ᜇ.ᜂ().ᜀ() as spr\u2032).ᜐ())
						{
							num = 7;
							continue;
						}
						return;
					case 25:
						this.ᜀ(sprᦰ.ᜁ().X, rectangleF.Height);
						num = 16;
						continue;
					case 26:
						goto IL_2A2;
					case 27:
						goto IL_4FF;
					case 28:
						flag = false;
						this.\u1714 = true;
						num = 26;
						continue;
					case 29:
						if (!this.\u1714)
						{
							num = 28;
							continue;
						}
						goto IL_2A2;
					case 30:
						if (!(sprᦰ.ᜂ().ᜀ() as spr\u2032).ᜌ())
						{
							num = 25;
							continue;
						}
						goto IL_2D4;
					case 31:
						if (true)
						{
						}
						a_.Height -= (float)(sprᦰ.ᜂ().ᜀ().ᜋ().ᜀ() - (double)num3);
						num = 0;
						continue;
					case 32:
						this.ᜀ(A_0);
						num = 13;
						continue;
					case 33:
						if (this.ᜃ.ᜊ().Count > 0)
						{
							num = 11;
							continue;
						}
						goto IL_2D4;
					case 34:
						if (this.ᜏ.TableFormat.CellSpacing > 0f)
						{
							num = 21;
							continue;
						}
						goto IL_475;
					case 35:
						if ((sprᦰ.ᜂ().ᜀ() as spr\u2032).ᜌ())
						{
							num = 5;
							continue;
						}
						goto IL_1DC;
					}
					break;
					IL_1DC:
					num = 12;
					continue;
					IL_2A2:
					num = 15;
					continue;
					IL_2D4:
					num = 35;
					continue;
					IL_30B:
					num3 = 0f;
					num = 34;
					continue;
					IL_349:
					sprᦰ.ᜀ(a_);
					num = 22;
					continue;
					IL_3EC:
					num = 10;
					continue;
					IL_475:
					a_.Height = rectangleF.Height - num3;
					num = 1;
					continue;
					IL_4FF:
					num = 19;
				}
			}
			return;
		}
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x000663D8 File Offset: 0x000653D8
	private new void ᜁ(int A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = this.ᜐ() - 1;
				sprᦰ sprᦰ = this.ᜇ.ᜊ()[A_0];
				RectangleF rectangleF = sprᦰ.ᜁ();
				bool flag = (sprᦰ.ᜂ().ᜀ() as spr\u2032).\u1716();
				bool flag2 = false;
				bool flag3 = false;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 13;
						continue;
					case 1:
						if (this.ᜃ.ᜊ().Count > num)
						{
							num2 = 7;
							continue;
						}
						goto IL_124;
					case 2:
						if (flag2)
						{
							num2 = 12;
							continue;
						}
						goto IL_E4;
					case 3:
						goto IL_124;
					case 4:
						if (flag3)
						{
							num2 = 6;
							continue;
						}
						goto IL_124;
					case 5:
						if (flag)
						{
							num2 = 0;
							continue;
						}
						return;
					case 6:
						num2 = 1;
						continue;
					case 7:
					{
						RectangleF rectangleF2 = this.ᜃ.ᜊ()[num].ᜊ()[A_0].ᜁ();
						float height;
						this.ᜃ.ᜊ()[num].ᜊ()[A_0].ᜀ(new RectangleF(rectangleF2.Location, new SizeF(rectangleF2.Width, rectangleF2.Height - height)));
						num2 = 3;
						continue;
					}
					case 8:
						if (!flag2)
						{
							num2 = 11;
							continue;
						}
						goto IL_201;
					case 9:
						goto IL_E4;
					case 10:
					{
						float height = rectangleF.Height;
						float height2 = rectangleF.Height;
						num2 = 14;
						continue;
					}
					case 11:
						return;
					case 12:
						if (true)
						{
						}
						num--;
						num2 = 9;
						continue;
					case 13:
						if (num != this.\u1712().ᜄ() - 1)
						{
							num2 = 10;
							continue;
						}
						return;
					case 14:
						IL_11F:
						goto IL_201;
					}
					break;
					IL_E4:
					num2 = 4;
					continue;
					IL_124:
					num2 = 8;
					continue;
					IL_201:
					spr\u17C8 spr_u17C = this.\u1712().ᜀ(num, A_0);
					flag3 = (spr_u17C.ᜀ() as spr\u2032).ᜌ();
					flag2 = (spr_u17C.ᜀ() as spr\u2032).\u1716();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11F;
					default:
						if (false)
						{
						}
						num2 = 2;
						break;
					}
				}
			}
			return;
		}
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x00066670 File Offset: 0x00065670
	private new int ᜁ(float A_0, int A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = this.ᜃ.ᜊ().Count - 1;
				int num2 = 10;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						if (!(this.ᜃ.ᜊ()[num].ᜊ()[num3].ᜂ().ᜀ() as spr\u2032).\u1716())
						{
							num2 = 15;
							continue;
						}
						goto IL_12D;
					}
					case 1:
					{
						float x;
						if (A_0 <= x)
						{
							num2 = 6;
							continue;
						}
						int num3;
						num3++;
						num2 = 13;
						continue;
					}
					case 2:
					{
						int num3;
						if (num3 >= this.ᜃ.ᜊ()[num].ᜊ().Count)
						{
							num2 = 3;
							continue;
						}
						float x = this.ᜃ.ᜊ()[num].ᜊ()[num3].ᜁ().X;
						num2 = 1;
						continue;
					}
					case 3:
						if (true)
						{
						}
						goto IL_12D;
					case 4:
					{
						if (num < 0)
						{
							num2 = 8;
							continue;
						}
						int num3 = 0;
						goto IL_1D4;
					}
					case 5:
						goto IL_1E5;
					case 6:
						num2 = 7;
						continue;
					case 7:
					{
						int num3;
						if ((this.ᜃ.ᜊ()[num].ᜊ()[num3].ᜂ().ᜀ() as spr\u2032).ᜌ())
						{
							num2 = 9;
							continue;
						}
						num2 = 0;
						continue;
					}
					case 8:
						return A_1;
					case 9:
						num2 = 14;
						continue;
					case 10:
						goto IL_1AC;
					case 11:
					{
						int num3;
						return num3;
					}
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1D4;
						default:
							if (false)
							{
							}
							goto IL_1AC;
						}
						break;
					case 13:
						goto IL_1E5;
					case 14:
					{
						int num3;
						if (this.ᜊ[num3] == num)
						{
							num2 = 11;
							continue;
						}
						return A_1;
					}
					case 15:
						return A_1;
					}
					break;
					IL_12D:
					num--;
					num2 = 12;
					continue;
					IL_1AC:
					num2 = 4;
					continue;
					IL_1D4:
					num2 = 5;
					continue;
					IL_1E5:
					num2 = 2;
				}
			}
			return A_1;
		}
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x000668E0 File Offset: 0x000658E0
	private new int ᜀ(float A_0, int A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = this.ᜐ() - 1;
				int num2 = 11;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						num2 = 7;
						continue;
					case 1:
						goto IL_60;
					case 2:
						return num3;
					case 3:
						return A_1;
					case 4:
						if (num < 0)
						{
							num2 = 3;
							continue;
						}
						num3 = 0;
						num2 = 1;
						continue;
					case 5:
						goto IL_60;
					case 6:
						goto IL_11B;
					case 7:
						if ((this.ᜏ.Rows[num].Cells[num3].ᜀ as spr\u2032).ᜌ())
						{
							num2 = 2;
							continue;
						}
						goto IL_A3;
					case 8:
					{
						float num4;
						if (A_0 == num4)
						{
							num2 = 0;
							continue;
						}
						goto IL_A3;
					}
					case 9:
						goto IL_96;
					case 10:
					{
						if (num3 >= this.ᜏ.Rows[num].Cells.Count)
						{
							num2 = 9;
							continue;
						}
						float num4 = (this.ᜏ.Rows[num].Cells[num3].ᜀ as spr\u2032).\u170D();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_96;
						default:
							if (false)
							{
							}
							num2 = 8;
							continue;
						}
						break;
					}
					case 11:
						goto IL_11B;
					}
					break;
					IL_60:
					num2 = 10;
					continue;
					IL_96:
					if (true)
					{
					}
					num--;
					num2 = 6;
					continue;
					IL_A3:
					num3++;
					num2 = 5;
					continue;
					IL_11B:
					num2 = 4;
				}
			}
			return A_1;
		}
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00066AA8 File Offset: 0x00065AA8
	private new void ᜀ(float A_0, float A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = this.ᜃ.ᜊ().Count - 1;
				int num2 = 11;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						if (!(this.ᜃ.ᜊ()[num].ᜊ()[num3].ᜂ().ᜀ() as spr\u2032).\u1716())
						{
							num2 = 16;
							continue;
						}
						goto IL_15E;
					}
					case 1:
					{
						float x;
						if (A_0 <= x)
						{
							num2 = 13;
							continue;
						}
						int num3;
						num3++;
						num2 = 7;
						continue;
					}
					case 2:
					{
						int num3;
						double num4 = this.ᜉ[num3] - (double)A_1;
						num2 = 15;
						continue;
					}
					case 3:
						goto IL_80;
					case 4:
						goto IL_1F2;
					case 5:
					{
						int num3;
						if (num3 >= this.ᜃ.ᜊ()[num].ᜊ().Count)
						{
							num2 = 8;
							continue;
						}
						float x = this.ᜃ.ᜊ()[num].ᜊ()[num3].ᜁ().X;
						num2 = 1;
						continue;
					}
					case 6:
						goto IL_FD;
					case 7:
						goto IL_FD;
					case 8:
						goto IL_15E;
					case 9:
						return;
					case 10:
					{
						IL_89:
						if (num < 0)
						{
							num2 = 9;
							continue;
						}
						int num3 = 0;
						num2 = 6;
						continue;
					}
					case 11:
						goto IL_80;
					case 12:
						num2 = 14;
						continue;
					case 13:
						num2 = 17;
						continue;
					case 14:
					{
						int num3;
						if (this.ᜊ[num3] == num)
						{
							num2 = 2;
							continue;
						}
						goto IL_15E;
					}
					case 15:
					{
						int num3;
						double num4;
						this.ᜉ[num3] = ((num4 > 0.0) ? num4 : 0.0);
						num2 = 4;
						continue;
					}
					case 16:
						return;
					case 17:
					{
						int num3;
						if ((this.ᜃ.ᜊ()[num].ᜊ()[num3].ᜂ().ᜀ() as spr\u2032).ᜌ())
						{
							num2 = 12;
							continue;
						}
						num2 = 0;
						continue;
					}
					}
					break;
					IL_80:
					num2 = 10;
					continue;
					IL_FD:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_89;
					}
					if (false)
					{
					}
					num2 = 5;
					continue;
					IL_15E:
					num--;
					num2 = 3;
				}
			}
			return;
			IL_1F2:
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00066D68 File Offset: 0x00065D68
	private new void ᜄ(sprᦰ A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
				{
					RectangleF rectangleF;
					if (rectangleF.Bottom < A_0.ᜁ().Bottom)
					{
						num = 6;
						continue;
					}
					sprᦰ sprᦰ;
					RectangleF rectangleF2 = sprᦰ.ᜁ();
					num = 15;
					continue;
				}
				case 1:
				{
					sprᦰ sprᦰ;
					sprᦰ.ᜊ().RemoveAt(num2);
					num = 19;
					continue;
				}
				case 3:
					goto IL_281;
				case 4:
					if ((A_0.ᜂ() as TableCell).OwnerRow.OwnerTable.\u1712 != null)
					{
						num = 5;
						continue;
					}
					goto IL_C5;
				case 5:
					return;
				case 6:
					goto IL_271;
				case 7:
					if (A_0.ᜊ()[0].ᜊ()[A_0.ᜊ().Count - 1].ᜂ() is Table)
					{
						num = 10;
						continue;
					}
					return;
				case 8:
				{
					sprᦰ sprᦰ;
					if (num2 < sprᦰ.ᜊ().Count)
					{
						num = 13;
						continue;
					}
					goto IL_271;
				}
				case 9:
					goto IL_19F;
				case 10:
				{
					sprᦰ sprᦰ2 = A_0.ᜊ()[0].ᜊ()[A_0.ᜊ().Count - 1];
					RectangleF rectangleF3 = sprᦰ2.ᜁ();
					num = 18;
					continue;
				}
				case 11:
				{
					sprᦰ sprᦰ2;
					if (num3 >= sprᦰ2.ᜊ().Count)
					{
						num = 21;
						continue;
					}
					sprᦰ sprᦰ = sprᦰ2.ᜊ()[num3];
					num2 = 0;
					num = 16;
					continue;
				}
				case 12:
					if (true)
					{
					}
					num3 = 0;
					num = 9;
					continue;
				case 13:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_281;
					default:
					{
						if (false)
						{
						}
						sprᦰ sprᦰ;
						RectangleF rectangleF = sprᦰ.ᜁ();
						num = 0;
						continue;
					}
					}
					break;
				case 14:
					goto IL_93;
				case 15:
				{
					RectangleF rectangleF2;
					if (rectangleF2.Top >= A_0.ᜁ().Bottom)
					{
						num = 1;
						continue;
					}
					sprᦰ sprᦰ;
					(sprᦰ.ᜊ()[num3].ᜂ().ᜀ() as TableCell.ᜀ).ᜂ(true);
					float num4 = (float)((double)A_0.ᜁ().Bottom - (A_0.ᜂ().ᜀ().ᜋ().ᜀ() + A_0.ᜂ().ᜀ().ᜋ().ᜁ()));
					sprᦰ.ᜀ(0f, num4 - sprᦰ.ᜊ()[num2].ᜁ().Y, 0f, num4 - sprᦰ.ᜊ()[num2].ᜁ().Y);
					num = 14;
					continue;
				}
				case 16:
					goto IL_155;
				case 17:
					goto IL_155;
				case 18:
				{
					RectangleF rectangleF3;
					if (rectangleF3.Bottom >= A_0.ᜁ().Bottom)
					{
						num = 12;
						continue;
					}
					return;
				}
				case 19:
					goto IL_93;
				case 20:
					num = 4;
					continue;
				case 21:
					goto IL_1C8;
				}
				if (A_0.ᜂ() is TableCell)
				{
					num = 20;
					continue;
				}
				goto IL_C5;
				IL_93:
				num2++;
				num = 17;
				continue;
				IL_C5:
				num = 7;
				continue;
				IL_155:
				num = 8;
				continue;
				IL_19F:
				num = 11;
				continue;
				IL_281:
				goto IL_19F;
				IL_271:
				num3++;
				num = 3;
			}
			IL_1C8:
			return;
		}
		}
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00067138 File Offset: 0x00066138
	private new int ᜄ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				float num = 0f;
				float num2 = 0f;
				int num3 = 15;
				for (;;)
				{
					int num5;
					switch (num3)
					{
					case 0:
					{
						int num4 = A_1 = num4 + 1;
						num3 = 8;
						continue;
					}
					case 1:
						goto IL_1F2;
					case 2:
						goto IL_1F2;
					case 3:
					{
						if (true)
						{
						}
						int num4 = A_1 = num4 + 1;
						num3 = 10;
						continue;
					}
					case 4:
					{
						int num4 = 0;
						num3 = 12;
						continue;
					}
					case 5:
						num3 = 9;
						continue;
					case 6:
					{
						if (Math.Round((double)num, 1) > Math.Round((double)num2, 1))
						{
							num3 = 5;
							continue;
						}
						int num4;
						num4++;
						num3 = 17;
						continue;
					}
					case 7:
						if (Math.Round((double)num, 1) == Math.Round((double)num2, 1))
						{
							num3 = 0;
							continue;
						}
						num3 = 6;
						continue;
					case 8:
						goto IL_BF;
					case 9:
					{
						if (Math.Ceiling((double)num) == Math.Ceiling((double)num2))
						{
							num3 = 3;
							continue;
						}
						int num4;
						A_1 = num4;
						num3 = 11;
						continue;
					}
					case 10:
						goto IL_A7;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11E;
						default:
							goto IL_238;
						}
						break;
					case 12:
						goto IL_161;
					case 13:
					{
						int num4;
						if (num4 >= this.ᜏ.Rows[A_0].Cells.Count)
						{
							num3 = 14;
							continue;
						}
						num += this.ᜏ.Rows[A_0].Cells[num4].Width;
						num3 = 7;
						continue;
					}
					case 14:
						goto IL_19A;
					case 15:
						if (A_1 == 0)
						{
							num3 = 16;
							continue;
						}
						goto IL_11E;
					case 16:
						return A_1;
					case 17:
						goto IL_161;
					case 18:
						if (num5 >= A_1)
						{
							num3 = 4;
							continue;
						}
						num2 += this.ᜏ.Rows[this.ᜐ()].Cells[num5].Width;
						num5++;
						num3 = 1;
						continue;
					}
					break;
					IL_11E:
					num5 = 0;
					num3 = 2;
					continue;
					IL_161:
					num3 = 13;
					continue;
					IL_1F2:
					num3 = 18;
				}
			}
			return A_1;
			IL_A7:
			IL_BF:
			IL_19A:
			return A_1;
			IL_238:
			if (false)
			{
			}
			return A_1;
		}
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x000673D0 File Offset: 0x000663D0
	private new void ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			RectangleF rectangleF;
			sprᦰ sprᦰ;
			sprᦰ sprᦰ2;
			for (;;)
			{
				int num = A_0;
				int num2 = this.ᜐ() - 1;
				int num3 = 0;
				rectangleF = this.ᜇ.ᜁ();
				bool isHeader = (this.ᜇ.ᜂ() as TableRow).IsHeader;
				int num4 = 37;
				for (;;)
				{
					bool flag2;
					switch (num4)
					{
					case 0:
						if (sprᦰ.ᜂ() is TableCell)
						{
							num4 = 54;
							continue;
						}
						num4 = 12;
						continue;
					case 1:
					{
						spr\u2032 spr_u;
						if (spr_u.ᜌ())
						{
							num4 = 14;
							continue;
						}
						goto IL_7EE;
					}
					case 2:
						num4 = 45;
						continue;
					case 3:
						num4 = 26;
						continue;
					case 4:
					{
						bool flag;
						if (isHeader == flag)
						{
							num4 = 31;
							continue;
						}
						return;
					}
					case 5:
						goto IL_7C8;
					case 6:
						num4 = 40;
						continue;
					case 7:
						goto IL_411;
					case 8:
						return;
					case 9:
						goto IL_768;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_569;
						default:
							if (false)
							{
							}
							num4 = 44;
							continue;
						}
						break;
					case 11:
					{
						sprᦰ = new sprᦰ(this.\u1712().ᜀ(num2, A_0));
						bool flag = false;
						num4 = 0;
						continue;
					}
					case 12:
						if (sprᦰ.ᜂ() is sprᴛ)
						{
							num4 = 51;
							continue;
						}
						goto IL_7A2;
					case 13:
						goto IL_7A2;
					case 14:
						num4 = 20;
						continue;
					case 15:
						goto IL_393;
					case 16:
					{
						spr\u2032 spr_u;
						if (spr_u.ᜐ())
						{
							num4 = 2;
							continue;
						}
						return;
					}
					case 17:
					{
						RectangleF a_ = sprᦰ2.ᜁ();
						a_.Height = rectangleF.Bottom - sprᦰ2.ᜁ().Top;
						sprᦰ2.ᜀ(a_);
						num4 = 16;
						continue;
					}
					case 18:
					{
						spr\u2032 spr_u;
						if (spr_u.ᜌ())
						{
							num4 = 10;
							continue;
						}
						goto IL_143;
					}
					case 19:
						goto IL_569;
					case 20:
						if (this.ᜃ.ᜊ().Count <= num2)
						{
							num4 = 11;
							continue;
						}
						goto IL_7EE;
					case 21:
						if (this.ᜐ() != this.ᜏ.Rows.Count - 1)
						{
							num4 = 35;
							continue;
						}
						goto IL_250;
					case 22:
						if (!this.\u1714)
						{
							num4 = 33;
							continue;
						}
						goto IL_64D;
					case 23:
						goto IL_540;
					case 24:
						if (sprᦰ2.ᜂ() is TableCell)
						{
							num4 = 19;
							continue;
						}
						num4 = 47;
						continue;
					case 25:
						goto IL_540;
					case 26:
						if (this.ᜃ.ᜊ().Count == 0)
						{
							num4 = 8;
							continue;
						}
						num4 = 27;
						continue;
					case 27:
						if (num3 != 0)
						{
							num4 = 34;
							continue;
						}
						sprᦰ2 = this.ᜃ.ᜊ()[num2].ᜊ()[A_0];
						num4 = 23;
						continue;
					case 28:
					{
						spr\u2032 spr_u;
						spr_u.ᜊ(this.ᜃ.ᜊ().Count == this.ᜐ() - num2);
						num4 = 53;
						continue;
					}
					case 29:
						if (this.\u1713 == num2)
						{
							num4 = 42;
							continue;
						}
						goto IL_411;
					case 30:
						if (this.\u1712)
						{
							num4 = 55;
							continue;
						}
						goto IL_411;
					case 31:
						goto IL_7C3;
					case 32:
						goto IL_7C8;
					case 33:
						num4 = 29;
						continue;
					case 34:
						sprᦰ2 = this.ᜃ.ᜊ()[this.ᜃ.ᜊ().Count - (this.ᜐ() - num2)].ᜊ()[A_0];
						num4 = 25;
						continue;
					case 35:
						num4 = 49;
						continue;
					case 36:
						num4 = 21;
						continue;
					case 37:
						goto IL_374;
					case 38:
						flag2 = (((sprᦰ2.ᜂ() as sprᴛ).ᜁ() as TableCell).Owner as TableRow).IsHeader;
						num4 = 5;
						continue;
					case 39:
						goto IL_7A2;
					case 40:
						if (this.ᜏ.Rows[this.ᜐ() + 1].Cells[this.ᜅ(this.ᜐ(), num)].CellFormat.VerticalMerge != CellMerge.Continue)
						{
							num4 = 9;
							continue;
						}
						return;
					case 41:
						num4 = 46;
						continue;
					case 42:
						num4 = 30;
						continue;
					case 43:
					{
						spr\u2032 spr_u;
						if (spr_u.\u1716())
						{
							num4 = 28;
							continue;
						}
						goto IL_456;
					}
					case 44:
						if (this.ᜃ.ᜊ().Count + num3 > num2)
						{
							num4 = 3;
							continue;
						}
						goto IL_143;
					case 45:
						if ((this.ᜇ.ᜊ()[num].ᜂ().ᜀ() as spr\u2032).\u1716())
						{
							num4 = 36;
							continue;
						}
						return;
					case 46:
						if ((sprᦰ2.ᜂ() as sprᴛ).ᜁ() is TableCell)
						{
							num4 = 38;
							continue;
						}
						goto IL_7C8;
					case 47:
						if (sprᦰ2.ᜂ() is sprᴛ)
						{
							num4 = 41;
							continue;
						}
						goto IL_7C8;
					case 48:
						goto IL_374;
					case 49:
						if (this.ᜐ() < this.ᜏ.Rows.Count - 1)
						{
							num4 = 6;
							continue;
						}
						return;
					case 50:
					{
						if (num2 <= -1)
						{
							num4 = 15;
							continue;
						}
						A_0 = this.ᜄ(num2, num);
						spr\u2032 spr_u = this.\u1712().ᜀ(num2, A_0).ᜀ() as spr\u2032;
						num4 = 22;
						continue;
					}
					case 51:
						num4 = 52;
						continue;
					case 52:
						if ((sprᦰ.ᜂ() as sprᴛ).ᜁ() is TableCell)
						{
							num4 = 56;
							continue;
						}
						goto IL_7A2;
					case 53:
						goto IL_456;
					case 54:
					{
						bool flag = ((sprᦰ.ᜂ() as TableCell).Owner as TableRow).IsHeader;
						num4 = 13;
						continue;
					}
					case 55:
						goto IL_64D;
					case 56:
					{
						bool flag = (((sprᦰ.ᜂ() as sprᴛ).ᜁ() as TableCell).Owner as TableRow).IsHeader;
						num4 = 39;
						continue;
					}
					case 57:
						if (isHeader == flag2)
						{
							num4 = 17;
							continue;
						}
						return;
					}
					break;
					IL_143:
					if (true)
					{
					}
					num4 = 1;
					continue;
					IL_374:
					num4 = 50;
					continue;
					IL_411:
					num4 = 18;
					continue;
					IL_456:
					num3 = this.ᜐ();
					this.\u1713 = num2;
					num4 = 7;
					continue;
					IL_540:
					flag2 = false;
					num4 = 24;
					continue;
					IL_569:
					flag2 = ((sprᦰ2.ᜂ() as TableCell).Owner as TableRow).IsHeader;
					num4 = 32;
					continue;
					IL_64D:
					num4 = 43;
					continue;
					IL_7A2:
					num4 = 4;
					continue;
					IL_7C8:
					num4 = 57;
					continue;
					IL_7EE:
					num2--;
					num4 = 48;
				}
			}
			IL_250:
			this.ᜃ(sprᦰ2);
			return;
			IL_393:
			return;
			IL_768:
			goto IL_250;
			IL_7C3:
			RectangleF a_2 = sprᦰ.ᜁ();
			a_2.Height = rectangleF.Bottom - sprᦰ.ᜁ().Top;
			a_2.Height -= (float)sprᦰ.ᜂ().ᜀ().ᜋ().ᜀ();
			sprᦰ.ᜀ(a_2);
			return;
		}
		}
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00067C48 File Offset: 0x00066C48
	private new void ᜃ(sprᦰ A_0)
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
		spr\u2573 spr_u = spr\u2573.ᜀ(A_0.ᜂ(), this.ᜆ, (float)this.ᜅ.ᜈ());
		sprᦰ value = spr_u.ᜀ(A_0.ᜁ());
		A_0.ᜊ()[0] = value;
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x00067CBC File Offset: 0x00066CBC
	private new void ᜅ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int count = this.ᜃ.ᜊ().Count;
				int num2 = 58;
				for (;;)
				{
					bool flag;
					Borders borders;
					sprᦰ sprᦰ;
					CellMerge cellMerge;
					int num3;
					RectangleF rectangleF;
					float num4;
					Borders borders2;
					Borders borders3;
					byte b;
					CellMerge cellMerge2;
					bool flag3;
					Borders borders4;
					switch (num2)
					{
					case 0:
						if (!flag)
						{
							num2 = 16;
							continue;
						}
						goto IL_3C5;
					case 1:
						num2 = 85;
						continue;
					case 2:
						num2 = 83;
						continue;
					case 3:
						if (borders.Right.BorderType == BorderStyle.Cleared)
						{
							num2 = 89;
							continue;
						}
						goto IL_3FE;
					case 4:
						goto IL_906;
					case 5:
						if (sprᦰ.ᜊ().Count != 0)
						{
							num2 = 84;
							continue;
						}
						goto IL_8A5;
					case 6:
						goto IL_BD4;
					case 7:
						num2 = 75;
						continue;
					case 8:
						goto IL_BAB;
					case 9:
						cellMerge = CellMerge.None;
						goto IL_66C;
					case 10:
					{
						sprᦰ sprᦰ2;
						cellMerge = (sprᦰ2.ᜂ() as TableCell).CellFormat.VerticalMerge;
						goto IL_66C;
					}
					case 11:
					{
						if (num3 <= -1)
						{
							num2 = 27;
							continue;
						}
						sprᦰ sprᦰ3;
						sprᦰ sprᦰ2 = sprᦰ3.ᜊ()[num3];
						spr\u2032 spr_u = sprᦰ2.ᜂ().ᜀ() as spr\u2032;
						bool flag2 = spr_u.\u1717();
						num2 = 67;
						continue;
					}
					case 12:
						if (num3 - 1 >= 0)
						{
							num2 = 57;
							continue;
						}
						goto IL_3FE;
					case 13:
						num4 = (rectangleF.Height - this.ᜂ(sprᦰ).Height) / 2f;
						num2 = 87;
						continue;
					case 14:
						num2 = 40;
						continue;
					case 15:
						num2 = 0;
						continue;
					case 16:
						num2 = 5;
						continue;
					case 17:
						goto IL_631;
					case 18:
						if (borders2.IsDefault)
						{
							num2 = 14;
							continue;
						}
						goto IL_1FE;
					case 19:
					{
						sprᦰ sprᦰ2;
						rectangleF = sprᦰ2.ᜁ();
						sprᦰ = sprᦰ2.ᜊ()[0];
						num4 = 0f;
						num2 = 92;
						continue;
					}
					case 20:
					{
						bool flag2;
						if (flag2)
						{
							num2 = 6;
							continue;
						}
						num2 = 31;
						continue;
					}
					case 21:
						goto IL_798;
					case 22:
					{
						spr\u1AB8 spr_u1AB;
						borders3 = (spr_u1AB as TableCell).CellFormat.Borders;
						goto IL_B20;
					}
					case 23:
					{
						spr\u1AB8 spr_u1AB2;
						if (!(spr_u1AB2 is sprᴛ))
						{
							num2 = 32;
							continue;
						}
						num2 = 91;
						continue;
					}
					case 24:
						num2 = 38;
						continue;
					case 25:
						return;
					case 26:
						num2 = 22;
						continue;
					case 27:
						num++;
						num2 = 34;
						continue;
					case 28:
						goto IL_3FE;
					case 29:
						num2 = 20;
						continue;
					case 30:
						num2 = 82;
						continue;
					case 31:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_426;
						default:
						{
							if (false)
							{
							}
							sprᦰ sprᦰ2;
							if (sprᦰ2.ᜊ().Count > 0)
							{
								num2 = 19;
								continue;
							}
							goto IL_906;
						}
						}
						break;
					case 32:
						num2 = 52;
						continue;
					case 33:
					{
						sprᦰ sprᦰ2;
						CellMerge horizontalMerge = (sprᦰ2.ᜂ() as TableCell).CellFormat.HorizontalMerge;
						num2 = 17;
						continue;
					}
					case 34:
						goto IL_339;
					case 35:
						switch (b)
						{
						case 1:
							num2 = 64;
							continue;
						case 2:
							num2 = 65;
							continue;
						default:
							num2 = 30;
							continue;
						}
						break;
					case 36:
						if (!this.ᜀ(num, num3))
						{
							num2 = 42;
							continue;
						}
						goto IL_81E;
					case 37:
						if (sprᦰ.ᜊ()[0].ᜂ() is Paragraph)
						{
							num2 = 13;
							continue;
						}
						goto IL_8A5;
					case 38:
						if (this.ᜃ.ᜊ()[num].ᜊ()[num3].ᜊ().Count > 0)
						{
							num2 = 44;
							continue;
						}
						goto IL_906;
					case 39:
						if (cellMerge2 == CellMerge.Continue)
						{
							num2 = 24;
							continue;
						}
						goto IL_906;
					case 40:
						if (!borders2.NoBorder)
						{
							num2 = 56;
							continue;
						}
						goto IL_3FE;
					case 41:
						goto IL_798;
					case 42:
						num2 = 68;
						continue;
					case 43:
						goto IL_906;
					case 44:
						this.ᜃ.ᜊ()[num].ᜊ()[num3].ᜊ().RemoveAt(0);
						num2 = 4;
						continue;
					case 45:
						if (sprᦰ.ᜊ().Count != 0)
						{
							num2 = 55;
							continue;
						}
						goto IL_B96;
					case 46:
						flag3 = false;
						goto IL_950;
					case 47:
					{
						spr\u1AB8 spr_u1AB = this.ᜃ.ᜊ()[num].ᜊ()[num3].ᜂ();
						spr\u1AB8 spr_u1AB2 = this.ᜃ.ᜊ()[num].ᜊ()[num3 - 1].ᜂ();
						num2 = 90;
						continue;
					}
					case 48:
					{
						sprᦰ sprᦰ2;
						flag3 = (sprᦰ2.ᜂ() as TableCell).OwnerRow.IsHeader;
						goto IL_950;
					}
					case 49:
						if (borders.Right.BorderType != BorderStyle.None)
						{
							num2 = 61;
							continue;
						}
						goto IL_36F;
					case 50:
						goto IL_BAB;
					case 51:
						goto IL_B96;
					case 52:
					{
						spr\u1AB8 spr_u1AB2;
						borders4 = (spr_u1AB2 as TableCell).CellFormat.Borders;
						goto IL_58A;
					}
					case 53:
					{
						bool flag2;
						if (flag2)
						{
							num2 = 47;
							continue;
						}
						goto IL_3FE;
					}
					case 54:
						sprᦰ.ᜀ(0.0, (double)num4, true);
						num2 = 78;
						continue;
					case 55:
						if (true)
						{
						}
						num2 = 59;
						continue;
					case 56:
						goto IL_1FE;
					case 57:
						num2 = 53;
						continue;
					case 58:
						goto IL_339;
					case 59:
						if (sprᦰ.ᜊ()[0].ᜂ() is Paragraph)
						{
							num2 = 66;
							continue;
						}
						goto IL_B96;
					case 60:
						num2 = 49;
						continue;
					case 61:
						num2 = 3;
						continue;
					case 62:
					{
						sprᦰ sprᦰ2;
						if (sprᦰ2.ᜂ() is TableCell)
						{
							num2 = 33;
							continue;
						}
						goto IL_631;
					}
					case 63:
						num2 = 74;
						continue;
					case 64:
						if (this.\u1712)
						{
							num2 = 15;
							continue;
						}
						goto IL_3C5;
					case 65:
						if (this.\u1712)
						{
							num2 = 1;
							continue;
						}
						goto IL_A8E;
					case 66:
					{
						sprᦰ sprᦰ4 = sprᦰ.ᜊ()[0];
						num4 = sprᦰ.ᜁ().Height - sprᦰ4.ᜁ().Height;
						num2 = 51;
						continue;
					}
					case 67:
					{
						sprᦰ sprᦰ2;
						if (!(sprᦰ2.ᜂ() is TableCell))
						{
							num2 = 69;
							continue;
						}
						num2 = 10;
						continue;
					}
					case 68:
					{
						bool flag2;
						if (flag2)
						{
							num2 = 77;
							continue;
						}
						num2 = 39;
						continue;
					}
					case 69:
						num2 = 9;
						continue;
					case 70:
						goto IL_426;
					case 71:
						goto IL_BAB;
					case 72:
						if (num4 > 0f)
						{
							num2 = 54;
							continue;
						}
						goto IL_490;
					case 73:
					{
						if (num >= count)
						{
							num2 = 25;
							continue;
						}
						sprᦰ sprᦰ3 = this.ᜃ.ᜊ()[num];
						num3 = sprᦰ3.ᜊ().Count - 1;
						num2 = 41;
						continue;
					}
					case 74:
						if (borders2.Right.BorderType != BorderStyle.None)
						{
							num2 = 2;
							continue;
						}
						goto IL_3FE;
					case 75:
						if (!borders.Right.IsDefault)
						{
							num2 = 60;
							continue;
						}
						goto IL_3FE;
					case 76:
						goto IL_BAB;
					case 77:
						goto IL_81E;
					case 78:
						goto IL_490;
					case 79:
						num2 = 46;
						continue;
					case 80:
					{
						spr\u1AB8 spr_u1AB;
						borders3 = ((spr_u1AB as sprᴛ).ᜁ() as TableCell).CellFormat.Borders;
						goto IL_B20;
					}
					case 81:
						if (!borders2.Right.IsDefault)
						{
							num2 = 63;
							continue;
						}
						goto IL_3FE;
					case 82:
						goto IL_BAB;
					case 83:
						if (borders2.Right.BorderType != BorderStyle.Cleared)
						{
							num2 = 7;
							continue;
						}
						goto IL_3FE;
					case 84:
						num2 = 37;
						continue;
					case 85:
						if (!flag)
						{
							num2 = 86;
							continue;
						}
						goto IL_A8E;
					case 86:
						num2 = 45;
						continue;
					case 87:
						goto IL_8A5;
					case 88:
					{
						spr\u2032 spr_u;
						if (!spr_u.\u1716())
						{
							num2 = 29;
							continue;
						}
						goto IL_BD4;
					}
					case 89:
						goto IL_36F;
					case 90:
					{
						spr\u1AB8 spr_u1AB;
						if (!(spr_u1AB is sprᴛ))
						{
							num2 = 26;
							continue;
						}
						num2 = 80;
						continue;
					}
					case 91:
					{
						spr\u1AB8 spr_u1AB2;
						borders4 = ((spr_u1AB2 as sprᴛ).ᜁ() as TableCell).CellFormat.Borders;
						goto IL_58A;
					}
					case 92:
					{
						sprᦰ sprᦰ2;
						if (!(sprᦰ2.ᜂ() is TableCell))
						{
							num2 = 79;
							continue;
						}
						num2 = 48;
						continue;
					}
					}
					break;
					IL_1FE:
					num2 = 81;
					continue;
					IL_339:
					num2 = 73;
					continue;
					IL_36F:
					borders.Right.Color = borders2.Right.Color;
					borders.Right.LineWidth = borders2.Right.LineWidth;
					borders.Right.Space = borders2.Right.Space;
					num2 = 28;
					continue;
					IL_3C5:
					num4 = (rectangleF.Height - this.ᜂ(sprᦰ).Height) / 2f;
					this.\u1712 = false;
					num2 = 71;
					continue;
					IL_3FE:
					this.ᜃ.ᜊ()[num].ᜊ().RemoveAt(num3);
					num2 = 70;
					continue;
					IL_490:
					sprᦰ.ᜀ(RectangleF.Empty);
					num2 = 43;
					continue;
					IL_58A:
					borders = borders4;
					num2 = 18;
					continue;
					IL_631:
					num2 = 88;
					continue;
					IL_66C:
					cellMerge2 = cellMerge;
					num2 = 62;
					continue;
					IL_798:
					num2 = 11;
					continue;
					IL_81E:
					num2 = 12;
					continue;
					IL_8A5:
					this.\u1712 = false;
					num2 = 50;
					continue;
					IL_906:
					num3--;
					num2 = 21;
					continue;
					IL_426:
					goto IL_906;
					IL_950:
					flag = flag3;
					b = (sprᦰ.ᜂ().ᜀ() as spr\u2032).ᜋ();
					num2 = 35;
					continue;
					IL_A8E:
					num4 = rectangleF.Height - sprᦰ.ᜁ().Height;
					this.\u1712 = false;
					num2 = 8;
					continue;
					IL_B20:
					borders2 = borders3;
					num2 = 23;
					continue;
					IL_B96:
					this.\u1712 = false;
					num2 = 76;
					continue;
					IL_BAB:
					num2 = 72;
					continue;
					IL_BD4:
					num2 = 36;
				}
			}
			return;
		}
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x000688C8 File Offset: 0x000678C8
	private new RectangleF ᜂ(sprᦰ A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 11;
			RectangleF result;
			for (;;)
			{
				sprᦰ sprᦰ;
				int num2;
				sprᦰ sprᦰ2;
				int num3;
				int count2;
				switch (num)
				{
				case 0:
					num = 9;
					continue;
				case 1:
					if ((sprᦰ.ᜊ()[num2].ᜂ() as DocPicture).TextWrappingStyle != TextWrappingStyle.Inline)
					{
						num = 10;
						continue;
					}
					goto IL_2BF;
				case 2:
					sprᦰ2 = sprᦰ;
					goto IL_20F;
				case 3:
					goto IL_1B5;
				case 4:
					if ((sprᦰ.ᜊ()[num2].ᜂ() as DocPicture).LayoutInCell)
					{
						num = 24;
						continue;
					}
					goto IL_2BF;
				case 5:
					if (sprᦰ.ᜊ().Count > 0)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_372;
				case 6:
					if (sprᦰ.ᜊ()[num2].ᜂ() is DocPicture)
					{
						num = 15;
						continue;
					}
					goto IL_2BF;
				case 7:
				{
					result = A_0.ᜁ();
					num3 = 0;
					int count = A_0.ᜊ().Count;
					num = 14;
					continue;
				}
				case 8:
					if (num2 >= count2)
					{
						num = 26;
						continue;
					}
					num = 6;
					continue;
				case 9:
					if (!(sprᦰ.ᜊ()[0].ᜂ() is Paragraph))
					{
						num = 16;
						continue;
					}
					num = 17;
					continue;
				case 10:
					num = 4;
					continue;
				case 12:
					goto IL_2BF;
				case 13:
					if (sprᦰ.ᜂ() is Paragraph)
					{
						num = 21;
						continue;
					}
					goto IL_372;
				case 14:
					goto IL_1B5;
				case 15:
					num = 1;
					continue;
				case 16:
					num = 2;
					continue;
				case 17:
					sprᦰ2 = sprᦰ.ᜊ()[0];
					goto IL_20F;
				case 18:
					goto IL_A3;
				case 19:
					if (A_0.ᜂ() is TableCell)
					{
						num = 7;
						continue;
					}
					goto IL_387;
				case 20:
				{
					int count;
					if (num3 >= count)
					{
						num = 23;
						continue;
					}
					sprᦰ = A_0.ᜊ()[num3];
					num = 13;
					continue;
				}
				case 21:
					num = 5;
					continue;
				case 22:
					num = 19;
					continue;
				case 23:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F8;
					default:
						goto IL_327;
					}
					break;
				case 24:
					goto IL_F8;
				case 25:
					goto IL_A3;
				case 26:
					goto IL_372;
				}
				if (A_0 != null)
				{
					num = 22;
					continue;
				}
				goto IL_387;
				IL_A3:
				num = 8;
				continue;
				IL_F8:
				result = new RectangleF(result.Location, new SizeF(result.Width, result.Height + sprᦰ.ᜊ()[num2].ᜁ().Height));
				num = 12;
				continue;
				IL_1B5:
				num = 20;
				continue;
				IL_20F:
				sprᦰ = sprᦰ2;
				num2 = 0;
				count2 = sprᦰ.ᜊ().Count;
				num = 18;
				continue;
				IL_2BF:
				num2++;
				num = 25;
				continue;
				IL_372:
				num3++;
				num = 3;
			}
			IL_327:
			if (false)
			{
			}
			return result;
			IL_387:
			return default(RectangleF);
		}
		}
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x00068C68 File Offset: 0x00067C68
	private new RectangleF ᜃ(int A_0, int A_1)
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
		spr\u2032 spr_u = this.\u1712().ᜀ(A_0, A_1).ᜀ() as spr\u2032;
		RectangleF a_ = default(RectangleF);
		a_.X = (float)((double)spr_u.\u170D() - spr_u.ᜰ().ᜃ() - spr_u.ᜭ().ᜃ());
		a_.Width = this.ᜀ(spr_u != null && spr_u.\u1714(), A_0, A_1).ᜆ().Width;
		sprᦰ sprᦰ = new sprᦰ(this.\u1712().ᜀ(A_0, A_1));
		spr\u2573 spr_u2 = spr\u2573.ᜀ(sprᦰ.ᜂ(), this.ᜆ, (float)this.ᜅ.ᜈ());
		sprᦰ sprᦰ2 = spr_u2.ᜀ(a_);
		sprᦰ2.ᜀ(a_);
		return sprᦰ2.ᜁ();
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00068D60 File Offset: 0x00067D60
	private new bool ᜁ(sprᦰ A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 16;
			for (;;)
			{
				bool flag;
				int num3;
				int num4;
				switch (num2)
				{
				case 0:
					flag = false;
					goto IL_28B;
				case 1:
					if (!(A_0.ᜊ()[num].ᜊ()[num3].ᜂ() is sprᴛ))
					{
						num2 = 12;
						continue;
					}
					num2 = 6;
					continue;
				case 2:
					if (num3 >= A_0.ᜊ()[num].ᜊ().Count)
					{
						num2 = 7;
						continue;
					}
					num2 = 4;
					continue;
				case 3:
					return true;
				case 4:
					if (!(A_0.ᜊ()[num].ᜊ()[num3].ᜂ() is Paragraph))
					{
						num2 = 20;
						continue;
					}
					goto IL_30C;
				case 5:
					if (num >= A_0.ᜊ().Count)
					{
						num2 = 3;
						continue;
					}
					num3 = 0;
					num2 = 11;
					continue;
				case 6:
					flag = ((A_0.ᜊ()[num].ᜊ()[num3].ᜂ() as sprᴛ).ᜁ() is Paragraph);
					goto IL_28B;
				case 7:
					num++;
					goto IL_1B4;
				case 8:
					num2 = 18;
					continue;
				case 9:
					if (A_0.ᜊ()[num].ᜊ()[num3].ᜊ()[num4].ᜂ() is DocPicture)
					{
						num2 = 8;
						continue;
					}
					goto IL_7B;
				case 10:
					goto IL_25F;
				case 11:
					goto IL_F6;
				case 12:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1B4;
					default:
						if (false)
						{
						}
						num2 = 0;
						continue;
					}
					break;
				case 13:
					goto IL_24B;
				case 14:
					if ((A_0.ᜊ()[num].ᜊ()[num3].ᜊ()[num4].ᜂ() as DocPicture).TextWrappingStyle != TextWrappingStyle.Inline)
					{
						num2 = 24;
						continue;
					}
					goto IL_7B;
				case 15:
					goto IL_30C;
				case 16:
					goto IL_25F;
				case 17:
					goto IL_16E;
				case 18:
					if ((A_0.ᜊ()[num].ᜊ()[num3].ᜊ()[num4].ᜂ() as DocPicture).LayoutInCell)
					{
						num2 = 21;
						continue;
					}
					goto IL_7B;
				case 19:
					goto IL_F6;
				case 20:
					num2 = 1;
					continue;
				case 21:
					num2 = 14;
					continue;
				case 22:
					if (num4 >= A_0.ᜊ()[num].ᜊ()[num3].ᜊ().Count)
					{
						num2 = 13;
						continue;
					}
					num2 = 9;
					continue;
				case 23:
					goto IL_16E;
				case 24:
					goto IL_D8;
				}
				break;
				IL_7B:
				num4++;
				num2 = 17;
				continue;
				IL_F6:
				num2 = 2;
				continue;
				IL_16E:
				num2 = 22;
				continue;
				IL_1B4:
				num2 = 10;
				continue;
				IL_24B:
				num3++;
				num2 = 19;
				continue;
				IL_28B:
				if (flag)
				{
					num2 = 15;
					continue;
				}
				goto IL_24B;
				IL_25F:
				num2 = 5;
				continue;
				IL_30C:
				num4 = 0;
				num2 = 23;
			}
		}
		IL_D8:
		if (true)
		{
		}
		return false;
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x000690EC File Offset: 0x000680EC
	private new ArrayList ᜁ(Table A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			ArrayList arrayList;
			for (;;)
			{
				float num2;
				int num3;
				float num4;
				int num5;
				int count2;
				switch (num)
				{
				case 0:
					goto IL_86;
				case 1:
					goto IL_198;
				case 2:
				{
					TableCell tableCell;
					if (tableCell.WidthType == FtsWidth.Percentage)
					{
						num = 10;
						continue;
					}
					num2 = (float)Math.Round((double)tableCell.Width);
					num = 12;
					continue;
				}
				case 3:
					if (arrayList.Count < num3 + 1)
					{
						num = 4;
						continue;
					}
					goto IL_8B;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_BA;
					default:
						if (false)
						{
						}
						goto IL_152;
					}
					break;
				case 5:
					goto IL_1F1;
				case 6:
					if (arrayList.Contains(num4))
					{
						num = 8;
						continue;
					}
					goto IL_152;
				case 8:
					num = 3;
					continue;
				case 9:
					goto IL_179;
				case 10:
				{
					TableCell tableCell;
					num2 = (float)Math.Round((double)(tableCell.Width / 20f));
					num = 16;
					continue;
				}
				case 11:
					goto IL_179;
				case 12:
					goto IL_AC;
				case 13:
					goto IL_1F1;
				case 14:
					num5++;
					num = 9;
					continue;
				case 15:
				{
					int count;
					if (num3 >= count)
					{
						num = 14;
						continue;
					}
					TableRow tableRow;
					TableCell tableCell = tableRow.Cells[num3];
					num = 2;
					continue;
				}
				case 16:
					goto IL_AC;
				case 17:
					if (true)
					{
					}
					goto IL_8B;
				case 18:
				{
					if (num5 >= count2)
					{
						num = 1;
						continue;
					}
					TableRow tableRow = A_0.Rows[num5];
					num4 = 0f;
					num3 = 0;
					int count = tableRow.Cells.Count;
					num = 13;
					continue;
				}
				}
				if (A_0.Offsets.Count > 0)
				{
					num = 0;
					continue;
				}
				arrayList = new ArrayList();
				num5 = 0;
				count2 = A_0.Rows.Count;
				num = 11;
				continue;
				IL_8B:
				num3++;
				num = 5;
				continue;
				IL_BA:
				num = 6;
				continue;
				IL_AC:
				num4 = (float)Math.Round((double)(num4 + num2));
				goto IL_BA;
				IL_152:
				arrayList.Add(num4);
				num = 17;
				continue;
				IL_179:
				num = 18;
				continue;
				IL_1F1:
				num = 15;
			}
			IL_86:
			return A_0.Offsets;
			IL_198:
			arrayList.Sort();
			A_0.Offsets = arrayList;
			return arrayList;
		}
		}
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x00069394 File Offset: 0x00068394
	private new void ᜀ(ArrayList A_0, float A_1, TableCell A_2)
	{
		if (true)
		{
		}
		for (;;)
		{
			int num;
			int num2;
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
				num = 1;
				short gridSpan = A_2.GridSpan;
				A_1 = (float)Math.Round((double)A_1);
				num2 = 2;
				break;
			}
			}
			for (;;)
			{
				float num3;
				switch (num2)
				{
				case 0:
					A_2.CellFormat.HorizontalMerge = CellMerge.Start;
					num2 = 6;
					continue;
				case 1:
					goto IL_80;
				case 2:
					if (A_2.WidthType == FtsWidth.Percentage)
					{
						num2 = 5;
						continue;
					}
					num3 = A_2.Width;
					num2 = 1;
					continue;
				case 3:
					if (num > 1)
					{
						num2 = 0;
						continue;
					}
					return;
				case 4:
					goto IL_80;
				case 5:
					num3 = A_2.Width / 20f;
					num2 = 4;
					continue;
				case 6:
					return;
				}
				break;
				IL_80:
				num = this.ᜀ(A_0, A_1, (float)Math.Round((double)num3));
				A_2.Colspan = num;
				num2 = 3;
			}
		}
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0006949C File Offset: 0x0006849C
	private new int ᜀ(ArrayList A_0, float A_1, float A_2)
	{
		switch (0)
		{
		default:
		{
			int num4;
			for (;;)
			{
				int num = -1;
				int num2 = 4;
				int num3 = 11;
				for (;;)
				{
					float num5;
					switch (num3)
					{
					case 0:
						num3 = 17;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11B;
						default:
							if (false)
							{
							}
							num3 = 6;
							continue;
						}
						break;
					case 2:
						goto IL_15C;
					case 3:
						goto IL_181;
					case 4:
						if (A_0.Count > num + num4)
						{
							num3 = 1;
							continue;
						}
						return num4;
					case 5:
						if (num < 0)
						{
							num3 = 0;
							continue;
						}
						goto IL_1A7;
					case 6:
						goto IL_B6;
					case 7:
						if (num == -1)
						{
							num3 = 9;
							continue;
						}
						goto IL_1D5;
					case 8:
						if (true)
						{
						}
						if (A_0.Count > num + num4)
						{
							num3 = 13;
							continue;
						}
						return num4;
					case 9:
						num2--;
						num3 = 10;
						continue;
					case 10:
						goto IL_186;
					case 11:
						goto IL_186;
					case 12:
						goto IL_1D5;
					case 13:
						num3 = 15;
						continue;
					case 14:
						goto IL_B6;
					case 15:
						if (num5 - (float)A_0[num + num4] <= 0.005f)
						{
							num3 = 2;
							continue;
						}
						goto IL_11B;
					case 16:
						if (num2 <= 0)
						{
							num3 = 12;
							continue;
						}
						num = A_0.IndexOf((float)Math.Round((double)A_1));
						num3 = 7;
						continue;
					case 17:
						if (A_1 > 0f)
						{
							num3 = 3;
							continue;
						}
						goto IL_1A7;
					}
					break;
					IL_B6:
					num3 = 8;
					continue;
					IL_11B:
					num4++;
					num3 = 14;
					continue;
					IL_186:
					num3 = 16;
					continue;
					IL_1A7:
					num4 = 1;
					num5 = A_1 + A_2;
					num3 = 4;
					continue;
					IL_1D5:
					num3 = 5;
				}
			}
			IL_15C:
			return num4;
			IL_181:
			throw new InvalidOperationException();
		}
		}
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x000696AC File Offset: 0x000686AC
	private new float ᜀ(ArrayList A_0, int A_1, float A_2)
	{
		switch (0)
		{
		default:
		{
			bool flag = false;
			float num = 0f;
			int num2 = 1;
			IEnumerator enumerator = A_0.GetEnumerator();
			try
			{
				int num3 = 10;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						flag = true;
						num3 = 4;
						continue;
					case 1:
						if (A_1 == num2)
						{
							num3 = 5;
							continue;
						}
						num2++;
						num3 = 12;
						continue;
					case 2:
					{
						object obj;
						num += (float)obj;
						num3 = 1;
						continue;
					}
					case 3:
						goto IL_12F;
					case 4:
						goto IL_112;
					case 5:
						flag = false;
						num3 = 7;
						continue;
					case 6:
						if (flag)
						{
							num3 = 2;
							continue;
						}
						break;
					case 7:
						goto IL_12F;
					case 8:
					{
						object obj;
						if ((float)obj == A_2)
						{
							num3 = 0;
							continue;
						}
						goto IL_112;
					}
					case 9:
						goto IL_13B;
					case 11:
					{
						if (!enumerator.MoveNext())
						{
							num3 = 3;
							continue;
						}
						object obj = enumerator.Current;
						num3 = 8;
						continue;
					}
					}
					IL_C8:
					num3 = 11;
					continue;
					goto IL_C8;
					IL_112:
					num3 = 6;
					continue;
					IL_12F:
					num3 = 9;
				}
				IL_13B:;
			}
			finally
			{
				for (;;)
				{
					IL_152:
					IDisposable disposable = enumerator as IDisposable;
					for (;;)
					{
						IL_15B:
						int num3 = 2;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_19F;
							case 1:
								disposable.Dispose();
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_15B;
								default:
									if (false)
									{
									}
									num3 = 0;
									continue;
								}
								break;
							case 2:
								if (disposable != null)
								{
									num3 = 1;
									continue;
								}
								goto IL_1A1;
							}
							goto IL_152;
						}
					}
				}
				IL_19F:
				IL_1A1:;
			}
			if (true)
			{
			}
			return num;
		}
		}
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x00069880 File Offset: 0x00068880
	private new ArrayList ᜀ(Table A_0)
	{
		switch (0)
		{
		default:
		{
			ArrayList arrayList;
			for (;;)
			{
				arrayList = new ArrayList();
				int num = 0;
				int count = A_0.Rows.Count;
				int num2 = 12;
				for (;;)
				{
					float num4;
					float num5;
					switch (num2)
					{
					case 0:
					{
						int num3;
						int count2;
						if (num3 >= count2)
						{
							num2 = 4;
							continue;
						}
						TableRow tableRow;
						TableCell tableCell = tableRow.Cells[num3];
						num2 = 2;
						continue;
					}
					case 1:
						goto IL_1CD;
					case 2:
					{
						TableCell tableCell;
						if (tableCell.WidthType == FtsWidth.Percentage)
						{
							num2 = 6;
							continue;
						}
						num4 = tableCell.Width;
						num2 = 1;
						continue;
					}
					case 3:
						if (!arrayList.Contains(num5))
						{
							num2 = 7;
							continue;
						}
						goto IL_139;
					case 4:
						num++;
						num2 = 11;
						continue;
					case 5:
						IL_1CB:
						goto IL_1CD;
					case 6:
					{
						TableCell tableCell;
						num4 = tableCell.Width / 20f;
						num2 = 5;
						continue;
					}
					case 7:
						arrayList.Add(num5);
						num2 = 10;
						continue;
					case 8:
						goto IL_75;
					case 9:
						goto IL_75;
					case 10:
						goto IL_139;
					case 11:
						goto IL_176;
					case 12:
						goto IL_176;
					case 13:
						goto IL_195;
					case 14:
					{
						if (num >= count)
						{
							num2 = 13;
							continue;
						}
						TableRow tableRow = A_0.Rows[num];
						num5 = 0f;
						int num3 = 0;
						int count2 = tableRow.Cells.Count;
						num2 = 9;
						continue;
					}
					}
					break;
					IL_75:
					if (true)
					{
					}
					num2 = 0;
					continue;
					IL_139:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1CB;
					default:
					{
						if (false)
						{
						}
						int num3;
						num3++;
						num2 = 8;
						continue;
					}
					}
					IL_176:
					num2 = 14;
					continue;
					IL_1CD:
					num5 += num4;
					num2 = 3;
				}
			}
			IL_195:
			arrayList.Sort();
			return arrayList;
		}
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x00069A98 File Offset: 0x00068A98
	private new float ᜀ(Table A_0, float A_1)
	{
		switch (0)
		{
		default:
		{
			spr\u22E1 spr_u22E;
			for (;;)
			{
				float num = A_0.TableFormat.Paddings.Right;
				float num2 = A_0.TableFormat.Paddings.Left;
				spr_u22E = new spr\u22E1();
				int num3 = 37;
				for (;;)
				{
					float num4;
					float num5;
					TableRow tableRow;
					int num6;
					float num7;
					int num8;
					sprᳮ sprᳮ;
					float a_;
					float num10;
					float num11;
					float num16;
					switch (num3)
					{
					case 0:
						goto IL_9F1;
					case 1:
						num4 = 2f;
						goto IL_51B;
					case 2:
						goto IL_971;
					case 3:
						num5 = 5.5f;
						goto IL_857;
					case 4:
						if (spr_u22E.ᜃ() == A_0.Rows.Count)
						{
							num3 = 48;
							continue;
						}
						goto IL_5C1;
					case 5:
						if (tableRow.Cells[num6].Paragraphs.Count > 0)
						{
							num3 = 13;
							continue;
						}
						goto IL_489;
					case 6:
						if (num + num2 >= 10f)
						{
							num3 = 28;
							continue;
						}
						num3 = 55;
						continue;
					case 7:
						goto IL_4F4;
					case 8:
						num3 = 18;
						continue;
					case 9:
						goto IL_A2A;
					case 10:
						num4 = num7;
						goto IL_51B;
					case 11:
						spr_u22E.ᜀ(num8.ToString(), sprᳮ);
						num3 = 7;
						continue;
					case 12:
						goto IL_751;
					case 13:
					{
						float num9 = 0f;
						num7 = 0f;
						a_ = 0f;
						num3 = 23;
						continue;
					}
					case 14:
						num5 = num2;
						goto IL_857;
					case 15:
						if (num > 1f)
						{
							num3 = 8;
							continue;
						}
						num3 = 34;
						continue;
					case 16:
						if (sprᳮ.ᜁ() > 0)
						{
							num3 = 11;
							continue;
						}
						goto IL_4F4;
					case 17:
					{
						int count;
						if (num6 >= count)
						{
							num3 = 49;
							continue;
						}
						num3 = 6;
						continue;
					}
					case 18:
						num10 = num;
						goto IL_9D7;
					case 19:
						goto IL_94C;
					case 20:
						num11 = num + num2;
						goto IL_99E;
					case 21:
					{
						float num9;
						try
						{
							num3 = 9;
							for (;;)
							{
								switch (num3)
								{
								case 0:
								{
									float num12;
									num9 = num12 / (float)tableRow.Cells.Count;
									num3 = 17;
									continue;
								}
								case 1:
								{
									object obj;
									num9 += (obj as DocPicture).Width;
									num3 = 8;
									continue;
								}
								case 3:
								{
									object obj;
									if (obj is DocPicture)
									{
										num3 = 1;
										continue;
									}
									num3 = 16;
									continue;
								}
								case 4:
								{
									object obj;
									num9 += this.ᜀ(obj as Table, A_1);
									num3 = 2;
									continue;
								}
								case 5:
								{
									object obj;
									if (obj is TextRange)
									{
										num3 = 7;
										continue;
									}
									num3 = 3;
									continue;
								}
								case 6:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num3 = 14;
										continue;
									}
									object obj = enumerator.Current;
									num3 = 5;
									continue;
								}
								case 7:
								{
									object obj;
									Font font = (obj as TextRange).CharacterFormat.Font;
									string text = (obj as TextRange).Text;
									SizeF sizeF = base.\u171E().ᜁ(text, font, null);
									float num12 = (obj as TextRange).Document.Sections[0].PageSetup.PageSize.Width - (obj as TextRange).Document.Sections[0].PageSetup.Margins.Left - (obj as TextRange).Document.Sections[0].PageSetup.Margins.Right;
									num9 += sizeF.Width;
									num3 = 10;
									continue;
								}
								case 10:
								{
									float num12;
									if (num9 > num12)
									{
										num3 = 0;
										continue;
									}
									break;
								}
								case 11:
								{
									object obj;
									if (obj is TextBox)
									{
										num3 = 13;
										continue;
									}
									break;
								}
								case 13:
								{
									object obj;
									num9 += (obj as TextBox).Format.Width;
									num3 = 12;
									continue;
								}
								case 14:
									num3 = 15;
									continue;
								case 15:
									goto IL_43B;
								case 16:
								{
									object obj;
									if (obj is Table)
									{
										num3 = 4;
										continue;
									}
									num3 = 11;
									continue;
								}
								}
								IL_399:
								num3 = 6;
								continue;
								goto IL_399;
							}
							IL_43B:
							goto IL_880;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num3 = 2;
								for (;;)
								{
									switch (num3)
									{
									case 0:
										goto IL_486;
									case 1:
										disposable.Dispose();
										num3 = 0;
										continue;
									case 2:
										if (disposable != null)
										{
											num3 = 1;
											continue;
										}
										goto IL_488;
									}
									break;
								}
							}
							IL_486:
							IL_488:;
						}
						goto IL_489;
						IL_880:
						num7 = Math.Max(num7, num9);
						int num13;
						num13++;
						num3 = 39;
						continue;
					}
					case 22:
					{
						int num14;
						if (num14 >= spr_u22E.ᜃ())
						{
							num3 = 2;
							continue;
						}
						sprᳮ sprᳮ2 = spr_u22E.ᜀ(num14.ToString());
						int num15 = 0;
						num3 = 31;
						continue;
					}
					case 23:
						if (tableRow.Cells[num6].Paragraphs[0].Items.Count > 0)
						{
							num3 = 43;
							continue;
						}
						goto IL_A2A;
					case 24:
					{
						int num13;
						if (num13 >= tableRow.Cells[num6].Paragraphs.Count)
						{
							num3 = 9;
							continue;
						}
						IEnumerator enumerator = tableRow.Cells[num6].Paragraphs[num13].Items.GetEnumerator();
						num3 = 21;
						continue;
					}
					case 25:
						if (true)
						{
						}
						num3 = 4;
						continue;
					case 26:
						if (spr_u22E.ᜃ() > 0)
						{
							num3 = 25;
							continue;
						}
						goto IL_5C1;
					case 27:
						goto IL_68F;
					case 28:
						num3 = 20;
						continue;
					case 29:
					{
						int num14;
						num14++;
						num3 = 19;
						continue;
					}
					case 30:
						num3 = 10;
						continue;
					case 31:
						goto IL_68F;
					case 32:
						if (tableRow.Cells[num6].Width <= num16)
						{
							num3 = 56;
							continue;
						}
						goto IL_489;
					case 33:
						goto IL_94C;
					case 34:
						num10 = 5.5f;
						goto IL_9D7;
					case 35:
						if (num7 != 0f)
						{
							num3 = 41;
							continue;
						}
						goto IL_751;
					case 36:
						goto IL_489;
					case 37:
						if (A_0.Width <= 11f)
						{
							num3 = 52;
							continue;
						}
						goto IL_A92;
					case 38:
						num3 = 14;
						continue;
					case 39:
						goto IL_9F1;
					case 40:
					{
						int count2;
						if (num8 >= count2)
						{
							num3 = 47;
							continue;
						}
						sprᳮ = new sprᳮ();
						tableRow = this.ᜏ.Rows[num8];
						num6 = 0;
						int count = tableRow.Cells.Count;
						num3 = 50;
						continue;
					}
					case 41:
						num3 = 53;
						continue;
					case 42:
						goto IL_8E9;
					case 43:
					{
						int num13 = 0;
						num3 = 0;
						continue;
					}
					case 44:
					{
						sprᳮ sprᳮ2;
						int num15;
						if (num15 >= sprᳮ2.ᜁ())
						{
							num3 = 29;
							continue;
						}
						sprᳮ2.ᜁ(num15.ToString());
						float num17 = spr_u22E.ᜁ(num15.ToString());
						int num14;
						A_0.Rows[num14].Cells[num15].Width = num17;
						A_0.Rows[num14].Cells[num15].WidthType = FtsWidth.Point;
						this.ᜑ().ᜂ()[num15] = num17;
						num15++;
						num3 = 27;
						continue;
					}
					case 45:
						goto IL_8C4;
					case 46:
						if (A_0.Rows[0].Cells[0].WidthType != FtsWidth.Percentage)
						{
							num3 = 51;
							continue;
						}
						return A_1;
					case 47:
						num3 = 26;
						continue;
					case 48:
					{
						int num14 = 0;
						num3 = 33;
						continue;
					}
					case 49:
						num3 = 16;
						continue;
					case 50:
						goto IL_8E9;
					case 51:
					{
						num8 = 0;
						int count2 = A_0.Rows.Count;
						num3 = 54;
						continue;
					}
					case 52:
						goto IL_A53;
					case 53:
						if (num2 > 1f)
						{
							num3 = 38;
							continue;
						}
						num3 = 3;
						continue;
					case 54:
						goto IL_8C4;
					case 55:
						num11 = 10f;
						goto IL_99E;
					case 56:
						num3 = 5;
						continue;
					case 57:
						if (num7 != 0f)
						{
							num3 = 30;
							continue;
						}
						num3 = 1;
						continue;
					}
					break;
					IL_489:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_A53:
						num3 = 46;
						continue;
					default:
						if (false)
						{
						}
						num6++;
						num3 = 42;
						continue;
					}
					IL_4F4:
					num8++;
					num3 = 45;
					continue;
					IL_51B:
					a_ = num4;
					num3 = 35;
					continue;
					IL_68F:
					num3 = 44;
					continue;
					IL_751:
					spr\u2569 spr_u = new spr\u2569();
					spr_u.ᜀ(a_);
					spr_u.ᜀ(tableRow.Cells[num6].CellFormat.HorizontalMerge);
					sprᳮ.ᜀ(num6.ToString(), spr_u);
					num3 = 36;
					continue;
					IL_857:
					num2 = num5;
					num3 = 15;
					continue;
					IL_8C4:
					num3 = 40;
					continue;
					IL_8E9:
					num3 = 17;
					continue;
					IL_94C:
					num3 = 22;
					continue;
					IL_99E:
					num16 = num11;
					num3 = 32;
					continue;
					IL_9D7:
					num = num10;
					a_ = num7 + num2 + num;
					num3 = 12;
					continue;
					IL_9F1:
					num3 = 24;
					continue;
					IL_A2A:
					num3 = 57;
				}
			}
			IL_5C1:
			return spr_u22E.ᜂ();
			IL_971:
			goto IL_5C1;
			IL_A92:
			return spr_u22E.ᜂ();
		}
		}
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0006A55C File Offset: 0x0006955C
	private new void ᜀ(ref RectangleF A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			Paddings a_2;
			for (;;)
			{
				spr\u2441 spr_u = this.ᜑ();
				int num = 16;
				for (;;)
				{
					bool flag;
					int num2;
					TableCell tableCell;
					float num4;
					int num7;
					float num9;
					float num8;
					float width;
					Paddings paddings;
					float num11;
					float num12;
					int num13;
					int count3;
					TableRow tableRow;
					int num15;
					ArrayList a_;
					Paddings paddings2;
					bool flag2;
					float num20;
					float num21;
					switch (num)
					{
					case 0:
						goto IL_710;
					case 1:
						if (!flag)
						{
							num = 33;
							continue;
						}
						goto IL_16DC;
					case 2:
						goto IL_1762;
					case 3:
						num2++;
						num = 143;
						continue;
					case 4:
						if (this.ᜏ.Owner is TableCell)
						{
							num = 172;
							continue;
						}
						goto IL_7C7;
					case 5:
						num = 20;
						continue;
					case 6:
					{
						float num3 = A_0.Width / this.ᜏ.Width;
						tableCell.Width *= num3;
						num = 109;
						continue;
					}
					case 7:
						goto IL_1000;
					case 8:
						if (num4 > this.ᜏ.Width)
						{
							goto IL_412;
						}
						goto IL_16F3;
					case 9:
						if (this.ᜏ.Width > A_0.Width)
						{
							num = 72;
							continue;
						}
						goto IL_152A;
					case 10:
						goto IL_16DC;
					case 11:
					{
						float num5;
						num4 = Math.Max(num4, num5);
						int num6;
						num6++;
						num = 74;
						continue;
					}
					case 12:
						if (this.ᜏ.TableFormat.RowIndent != -3.4028235E+38f)
						{
							num = 69;
							continue;
						}
						goto IL_775;
					case 13:
						if (this.ᜏ.Width + this.ᜏ.TableFormat.LeftIndent > A_0.Width)
						{
							num = 39;
							continue;
						}
						goto IL_52C;
					case 14:
						if (this.ᜏ.Rows[num2].Cells[num7].WidthType == FtsWidth.Percentage)
						{
							num = 169;
							continue;
						}
						num8 = num9 * (num8 / width);
						num = 38;
						continue;
					case 15:
						goto IL_16F3;
					case 16:
						if (!this.ᜏ.IsHasCaculatedCellWidth)
						{
							num = 99;
							continue;
						}
						goto IL_1217;
					case 17:
						goto IL_440;
					case 18:
						paddings = (this.ᜏ.Owner.Owner.Owner as Table).TableFormat.Paddings;
						goto IL_1572;
					case 19:
						if (this.ᜏ.Width != 0f)
						{
							num = 144;
							continue;
						}
						goto IL_8E6;
					case 20:
						if (flag)
						{
							num = 76;
							continue;
						}
						goto IL_142F;
					case 21:
					{
						int num10;
						int count;
						if (num10 >= count)
						{
							num = 11;
							continue;
						}
						float num5;
						int num6;
						num5 += this.ᜏ.Rows[num6].Cells[num10].Width;
						num10++;
						num = 150;
						continue;
					}
					case 22:
					{
						int num6 = 0;
						int count2 = this.ᜏ.Rows.Count;
						num = 25;
						continue;
					}
					case 23:
						num = 163;
						continue;
					case 24:
						num = 93;
						continue;
					case 25:
						goto IL_EFE;
					case 26:
						if (this.ᜏ.Rows[0].Cells[0].WidthType == FtsWidth.Point)
						{
							num = 5;
							continue;
						}
						goto IL_142F;
					case 27:
						goto IL_743;
					case 28:
						num11 = this.ᜏ.Width / num4;
						num = 84;
						continue;
					case 29:
						goto IL_14EE;
					case 30:
						if (this.ᜏ.TableFormat.RowIndent > 0f)
						{
							num = 51;
							continue;
						}
						goto IL_A2C;
					case 31:
						goto IL_B91;
					case 32:
						num12 = num9;
						goto IL_97C;
					case 33:
						num = 78;
						continue;
					case 34:
						num = 63;
						continue;
					case 35:
					{
						if (num13 >= count3)
						{
							num = 165;
							continue;
						}
						tableRow = this.ᜏ.Rows[num13];
						float num14 = 0f;
						num15 = 0;
						int count4 = tableRow.Cells.Count;
						num = 31;
						continue;
					}
					case 36:
						goto IL_C89;
					case 37:
						flag = true;
						num4 = A_0.Width - this.ᜏ.TableFormat.LeftIndent;
						num = 68;
						continue;
					case 38:
						goto IL_14EE;
					case 39:
						num = 88;
						continue;
					case 40:
						num = 135;
						continue;
					case 41:
						this.ᜏ.Width = A_0.Width;
						num = 7;
						continue;
					case 42:
						num = 48;
						continue;
					case 43:
					{
						if (tableCell.Width == 0f)
						{
							num = 108;
							continue;
						}
						tableCell.Width /= 20f;
						tableCell.WidthType = FtsWidth.Point;
						float num14;
						num14 += (float)Math.Round((double)tableCell.Width);
						num = 17;
						continue;
					}
					case 44:
					{
						int num17;
						int num16 = num17 + 1;
						num = 2;
						continue;
					}
					case 45:
						if (num4 < this.ᜏ.Width)
						{
							num = 96;
							continue;
						}
						num = 90;
						continue;
					case 46:
						if (flag)
						{
							num = 56;
							continue;
						}
						goto IL_16DC;
					case 47:
					{
						float num18 = this.ᜏ.Width / num4;
						tableCell.Width *= num18;
						num = 102;
						continue;
					}
					case 48:
						if (this.ᜏ.TableFormat.RowIndent < 0f)
						{
							num = 40;
							continue;
						}
						goto IL_6A3;
					case 49:
						num = 45;
						continue;
					case 50:
						num = 92;
						continue;
					case 51:
						num = 105;
						continue;
					case 52:
						if ((this.ᜏ.Owner as TableCell).Width > 11f)
						{
							num = 171;
							continue;
						}
						goto IL_7C7;
					case 53:
					{
						if (A_0.Width <= this.ᜏ.Width)
						{
							num = 132;
							continue;
						}
						float num19 = this.ᜏ.Width / A_0.Width * ((float)this.ᜏ.PreferredTableWidth.ᜁ() / 100f);
						tableCell.Width /= num19;
						num = 124;
						continue;
					}
					case 54:
						goto IL_E33;
					case 55:
					{
						int count4;
						if (num15 >= count4)
						{
							num = 170;
							continue;
						}
						tableCell = tableRow.Cells[num15];
						float num14;
						this.ᜀ(a_, num14, tableCell);
						num = 160;
						continue;
					}
					case 56:
						num = 158;
						continue;
					case 57:
						goto IL_7C7;
					case 58:
						if (!(this.ᜏ.Owner as TableCell).CellFormat.Paddings.IsEmpty)
						{
							num = 104;
							continue;
						}
						num = 147;
						continue;
					case 59:
						goto IL_6EC;
					case 60:
						if (spr_u.ᜁ() < A_0.Height)
						{
							num = 149;
							continue;
						}
						goto IL_181E;
					case 61:
						flag = (num4 != this.ᜏ.Width);
						num = 27;
						continue;
					case 62:
						if (width > num9)
						{
							num = 101;
							continue;
						}
						goto IL_903;
					case 63:
						num12 = num9 - paddings2.Left - paddings2.Right;
						goto IL_97C;
					case 64:
					{
						float num18 = num4 / this.ᜏ.Width;
						tableCell.Width *= num18;
						num = 100;
						continue;
					}
					case 65:
						if (this.ᜏ.Rows[0].Cells[0].WidthType == FtsWidth.Point)
						{
							num = 82;
							continue;
						}
						goto IL_16DC;
					case 66:
						num = 97;
						continue;
					case 67:
					{
						TableCell tableCell2;
						if (tableCell2.CellFormat.HorizontalMerge == CellMerge.Start)
						{
							num = 44;
							continue;
						}
						goto IL_1353;
					}
					case 68:
						goto IL_A2C;
					case 69:
						goto IL_152A;
					case 70:
						goto IL_8E6;
					case 71:
						goto IL_43B;
					case 72:
						num = 12;
						continue;
					case 73:
						num = 43;
						continue;
					case 74:
						goto IL_EFE;
					case 75:
						if (this.ᜏ.TableFormat.HasKey(107))
						{
							num = 42;
							continue;
						}
						goto IL_6A3;
					case 76:
						num11 = 1f;
						num = 153;
						continue;
					case 77:
						this.ᜏ.Width = A_0.Width * ((float)this.ᜏ.PreferredTableWidth.ᜁ() / 100f);
						if (true)
						{
						}
						num = 59;
						continue;
					case 78:
						if (A_0.Width < this.ᜏ.Width)
						{
							num = 6;
							continue;
						}
						goto IL_16DC;
					case 79:
						if (flag2)
						{
							num = 41;
							continue;
						}
						goto IL_1000;
					case 80:
						flag = true;
						num4 = A_0.Width - this.ᜏ.TableFormat.RowIndent;
						num = 114;
						continue;
					case 81:
						if (this.ᜏ.TableFormat.LeftIndent > 0f)
						{
							num = 154;
							continue;
						}
						goto IL_52C;
					case 82:
						num = 155;
						continue;
					case 83:
						this.ᜏ.Width = num20;
						num = 36;
						continue;
					case 84:
						goto IL_16F3;
					case 85:
						goto IL_16F3;
					case 86:
						num11 = this.ᜏ.Width / num4;
						num = 164;
						continue;
					case 87:
						num = 81;
						continue;
					case 88:
						if (this.ᜏ.TableFormat.RowIndent == -3.4028235E+38f)
						{
							num = 37;
							continue;
						}
						goto IL_52C;
					case 89:
						goto IL_C89;
					case 90:
						if (num4 > this.ᜏ.Width)
						{
							num = 86;
							continue;
						}
						goto IL_16F3;
					case 91:
						if (!paddings2.IsEmpty)
						{
							num = 34;
							continue;
						}
						num = 32;
						continue;
					case 92:
						this.ᜏ.PreferredTableWidth.ᜀ((this.ᜏ.PreferredTableWidth.ᜁ() == 0) ? 100 : this.ᜏ.PreferredTableWidth.ᜁ());
						num = 89;
						continue;
					case 93:
						if (this.ᜏ.PreferredTableWidth.ᜁ() == 100)
						{
							num = 161;
							continue;
						}
						tableCell.Width = A_0.Width * ((float)this.ᜏ.PreferredTableWidth.ᜁ() / 100f) * (tableCell.Width / this.ᜏ.Width);
						num = 10;
						continue;
					case 94:
						if (this.ᜏ.PreferredTableWidth.ᜀ() == FtsWidth.Percentage)
						{
							num = 24;
							continue;
						}
						num = 26;
						continue;
					case 95:
						if (this.ᜏ.TableFormat.HorizontalAlignment == RowAlignment.Left)
						{
							num = 87;
							continue;
						}
						goto IL_A2C;
					case 96:
						num11 = num4 / this.ᜏ.Width;
						num = 15;
						continue;
					case 97:
						if (this.ᜏ.PreferredWidth.Type == WidthType.Twip)
						{
							num = 146;
							continue;
						}
						goto IL_710;
					case 98:
						goto IL_440;
					case 99:
						num = 4;
						continue;
					case 100:
						goto IL_16DC;
					case 101:
						num2 = 0;
						num = 103;
						continue;
					case 102:
						goto IL_16DC;
					case 103:
						goto IL_D66;
					case 104:
						num = 111;
						continue;
					case 105:
						if (this.ᜏ.Width + this.ᜏ.TableFormat.RowIndent > A_0.Width)
						{
							num = 80;
							continue;
						}
						goto IL_A2C;
					case 106:
						goto IL_13ED;
					case 107:
						paddings = new Paddings();
						goto IL_1572;
					case 108:
						tableCell.Width = A_0.Width;
						tableCell.WidthType = FtsWidth.Point;
						num = 152;
						continue;
					case 109:
						goto IL_16DC;
					case 110:
						num = 60;
						continue;
					case 111:
						paddings = (this.ᜏ.Owner as TableCell).CellFormat.Paddings;
						goto IL_1572;
					case 112:
						goto IL_1762;
					case 113:
						goto IL_903;
					case 114:
						goto IL_A2C;
					case 115:
						if (this.ᜏ.TableFormat.HasKey(107))
						{
							num = 168;
							continue;
						}
						goto IL_A2C;
					case 116:
						if (this.ᜏ.Rows[0].Cells[0].WidthType == FtsWidth.Percentage)
						{
							num = 66;
							continue;
						}
						goto IL_710;
					case 117:
						if (this.ᜏ.Width > num9)
						{
							num = 70;
							continue;
						}
						goto IL_7C7;
					case 118:
						if (this.ᜏ.PreferredTableWidth.ᜀ() == FtsWidth.Percentage)
						{
							num = 50;
							continue;
						}
						goto IL_C89;
					case 119:
						goto IL_B91;
					case 120:
						num = 95;
						continue;
					case 121:
						if (num7 >= this.ᜏ.Rows[num2].Cells.Count)
						{
							num = 3;
							continue;
						}
						num8 = this.ᜏ.Rows[num2].Cells[num7].Width;
						num21 += num8;
						num = 14;
						continue;
					case 122:
						goto IL_563;
					case 123:
						if (this.ᜏ.Rows[0].Cells[0].WidthType == FtsWidth.Point)
						{
							num = 22;
							continue;
						}
						goto IL_743;
					case 124:
						goto IL_16DC;
					case 125:
						num = 9;
						continue;
					case 126:
						if (num4 < this.ᜏ.Width)
						{
							num = 28;
							continue;
						}
						num = 8;
						continue;
					case 127:
						num = 107;
						continue;
					case 128:
						num = 1;
						continue;
					case 129:
						if (this.ᜏ.TableFormat.LeftIndent < 0f)
						{
							num = 125;
							continue;
						}
						goto IL_152A;
					case 130:
						if (this.ᜏ.PreferredTableWidth.ᜀ() == FtsWidth.Percentage)
						{
							num = 77;
							continue;
						}
						goto IL_6EC;
					case 131:
						this.ᜀ(ref A_0);
						num = 174;
						continue;
					case 132:
					{
						float num18 = A_0.Width / this.ᜏ.Width * ((float)this.ᜏ.PreferredTableWidth.ᜁ() / 100f);
						tableCell.Width *= num18;
						num = 46;
						continue;
					}
					case 133:
						goto IL_775;
					case 134:
					{
						int num16;
						TableRow tableRow2;
						num9 += tableRow2.Cells[num16].Width;
						num16++;
						num = 112;
						continue;
					}
					case 135:
						if (this.ᜏ.Width > A_0.Width)
						{
							num = 133;
							continue;
						}
						goto IL_6A3;
					case 136:
						if (this.ᜏ.TableFormat.IsAutoResized)
						{
							num = 120;
							continue;
						}
						goto IL_A2C;
					case 137:
						goto IL_563;
					case 138:
						if (this.ᜑ().ᜁ() > 0f)
						{
							num = 110;
							continue;
						}
						goto IL_181E;
					case 139:
						num11 = num4 / this.ᜏ.Width;
						num = 85;
						continue;
					case 140:
						if (num4 > this.ᜏ.Width)
						{
							num = 64;
							continue;
						}
						goto IL_16DC;
					case 141:
						if (A_1)
						{
							num = 131;
							continue;
						}
						goto IL_5C1;
					case 142:
						goto IL_13ED;
					case 143:
						goto IL_D66;
					case 144:
						num = 117;
						continue;
					case 145:
						if (num20 > 0f)
						{
							num = 83;
							continue;
						}
						num = 116;
						continue;
					case 146:
						this.ᜏ.Width = this.ᜏ.Width / 20f;
						num = 0;
						continue;
					case 147:
						if (!(this.ᜏ.Owner.Owner.Owner is Table))
						{
							num = 127;
							continue;
						}
						num = 18;
						continue;
					case 148:
					{
						int num6;
						int count2;
						if (num6 >= count2)
						{
							num = 61;
							continue;
						}
						float num5 = 0f;
						int num10 = 0;
						int count = this.ᜏ.Rows[num6].Cells.Count;
						num = 54;
						continue;
					}
					case 149:
						A_0.Height = spr_u.ᜁ();
						num = 71;
						continue;
					case 150:
						goto IL_E33;
					case 151:
						goto IL_A2C;
					case 152:
						goto IL_440;
					case 153:
						if (this.ᜏ.TableFormat.IsAutoResized)
						{
							num = 49;
							continue;
						}
						num = 126;
						continue;
					case 154:
						num = 13;
						continue;
					case 155:
						if (this.ᜏ.PreferredWidth.Type == WidthType.Auto)
						{
							num = 128;
							continue;
						}
						goto IL_16DC;
					case 156:
						if (num2 >= this.ᜏ.Rows.Count)
						{
							num = 113;
							continue;
						}
						num21 = 0f;
						num7 = 0;
						num = 137;
						continue;
					case 157:
					{
						int num16;
						TableRow tableRow2;
						if (tableRow2.Cells[num16].CellFormat.HorizontalMerge == CellMerge.Continue)
						{
							num = 134;
							continue;
						}
						goto IL_1353;
					}
					case 158:
						if (num4 < this.ᜏ.Width)
						{
							num = 47;
							continue;
						}
						num = 140;
						continue;
					case 159:
						goto IL_440;
					case 160:
					{
						if (tableCell.WidthType == FtsWidth.Percentage)
						{
							num = 23;
							continue;
						}
						float num14;
						num14 += (float)Math.Round((double)tableCell.Width);
						num = 159;
						continue;
					}
					case 161:
						A_1 = false;
						num = 53;
						continue;
					case 162:
					{
						int num16;
						TableRow tableRow2;
						if (num16 >= tableRow2.Cells.Count)
						{
							num = 166;
							continue;
						}
						num = 157;
						continue;
					}
					case 163:
						if (tableCell.Scaling == 100f)
						{
							num = 73;
							continue;
						}
						tableCell.Width = A_0.Width * tableCell.Scaling / 100f;
						tableCell.WidthType = FtsWidth.Point;
						flag2 = true;
						num = 98;
						continue;
					case 164:
						goto IL_16F3;
					case 165:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_412;
						default:
							if (false)
							{
							}
							num = 130;
							continue;
						}
						break;
					case 166:
						goto IL_1353;
					case 167:
						goto IL_1217;
					case 168:
						num = 30;
						continue;
					case 169:
						num8 = num9 * (num8 / width) * 20f;
						num = 29;
						continue;
					case 170:
						num13++;
						num = 106;
						continue;
					case 171:
					{
						TableCell tableCell2 = this.ᜏ.Owner as TableCell;
						TableRow tableRow2 = tableCell2.Owner as TableRow;
						num9 = tableCell2.Width;
						int num17 = tableRow2.Cells.IndexOf(tableCell2);
						num = 67;
						continue;
					}
					case 172:
						num = 52;
						continue;
					case 173:
						goto IL_16DC;
					case 174:
						goto IL_5C1;
					}
					break;
					IL_412:
					num = 139;
					continue;
					IL_440:
					num = 94;
					continue;
					IL_52C:
					num = 129;
					continue;
					IL_563:
					num = 121;
					continue;
					IL_5C1:
					num = 138;
					continue;
					IL_6A3:
					num = 115;
					continue;
					IL_6EC:
					num = 79;
					continue;
					IL_710:
					num = 118;
					continue;
					IL_743:
					num = 136;
					continue;
					IL_775:
					flag = true;
					num4 = A_0.Width;
					num = 151;
					continue;
					IL_7C7:
					num20 = this.ᜀ(this.ᜏ, A_0.Width);
					num = 145;
					continue;
					IL_8E6:
					this.ᜏ.Width = num9;
					num = 57;
					continue;
					IL_903:
					num = 19;
					continue;
					IL_97C:
					num9 = num12;
					width = this.ᜏ.Width;
					num21 = 0f;
					num = 62;
					continue;
					IL_A2C:
					num13 = 0;
					count3 = this.ᜏ.Rows.Count;
					num = 142;
					continue;
					IL_B91:
					num = 55;
					continue;
					IL_C89:
					a_ = this.ᜁ(this.ᜏ);
					tableRow = null;
					tableCell = null;
					flag2 = false;
					flag = false;
					num4 = 0f;
					num = 123;
					continue;
					IL_D66:
					num = 156;
					continue;
					IL_E33:
					num = 21;
					continue;
					IL_EFE:
					num = 148;
					continue;
					IL_1000:
					this.ᜏ.IsHasCaculatedCellWidth = true;
					num = 167;
					continue;
					IL_1217:
					a_2 = new Paddings();
					num = 141;
					continue;
					IL_1353:
					num = 58;
					continue;
					IL_13ED:
					num = 35;
					continue;
					IL_142F:
					num = 65;
					continue;
					IL_14EE:
					this.ᜏ.Rows[num2].Cells[num7].Width = num8;
					num7++;
					num = 122;
					continue;
					IL_152A:
					num = 75;
					continue;
					IL_1572:
					paddings2 = paddings;
					num = 91;
					continue;
					IL_16DC:
					num15++;
					num = 119;
					continue;
					IL_16F3:
					tableCell.Width *= num11;
					num = 173;
					continue;
					IL_1762:
					num = 162;
				}
			}
			IL_43B:
			IL_181E:
			base.ᜀ(A_0, a_2);
			return;
		}
		}
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0006BD98 File Offset: 0x0006AD98
	private new void ᜀ(spr\u2573 A_0, RectangleF A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
			for (;;)
			{
				(A_0.\u171A() as spr\u2032).ᜈ((this.ᜇ.ᜂ().ᜀ() as spr\u2032).ᜐ());
				(A_0.\u171A() as spr\u2032).ᜁ((float)((double)A_1.X + A_0.\u171A().ᜊ().ᜃ() + A_0.\u171A().ᜋ().ᜃ()));
				(A_0.\u171A() as spr\u2032).ᜀ((float)((double)A_1.Y + A_0.\u171A().ᜊ().ᜁ() + A_0.\u171A().ᜋ().ᜁ()));
				sprᦰ sprᦰ = A_0.ᜀ(A_1);
				sprᦰ sprᦰ2 = sprᦰ;
				bool flag = false;
				int num = 0;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (num >= sprᦰ2.ᜊ().Count)
						{
							num2 = 1;
							continue;
						}
						flag = base.\u171E().\u171D().ᜀ(sprᦰ2.ᜊ()[num].ᜁ(), true);
						num2 = 5;
						continue;
					case 1:
						goto IL_157;
					case 2:
						goto IL_204;
					case 3:
						if (flag)
						{
							num2 = 10;
							continue;
						}
						goto IL_204;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_26A;
						default:
							if (false)
							{
							}
							goto IL_2A8;
						}
						break;
					case 5:
						if (!flag)
						{
							num2 = 11;
							continue;
						}
						goto IL_157;
					case 6:
						this.ᜈ.ᜀ(ClipboardData.b("㭧ũիṭ≯᭱፳ṵ౷", a_));
						num2 = 9;
						continue;
					case 7:
						goto IL_26A;
					case 8:
						goto IL_2A8;
					case 9:
						goto IL_1B1;
					case 10:
					{
						spr\u2573 spr_u = this.ᜂ();
						sprᦰ = spr_u.ᜀ(A_1);
						num2 = 2;
						continue;
					}
					case 11:
						num++;
						num2 = 8;
						continue;
					}
					break;
					IL_157:
					num2 = 3;
					continue;
					IL_204:
					this.ᜀ(sprᦰ);
					sprᦰ sprᦰ3 = new sprᦰ(A_0.\u1718(), sprᦰ.ᜁ().Location);
					sprᦰ3.ᜊ().ᜀ(sprᦰ);
					sprᦰ3.ᜁ(sprᦰ.ᜉ());
					sprᦰ3.ᜀ(sprᦰ.ᜁ());
					this.ᜈ = sprᦰ3;
					RectangleF rectangleF = sprᦰ.ᜁ();
					num2 = 7;
					continue;
					IL_26A:
					if (Math.Round((double)rectangleF.Right) > Math.Round((double)A_1.Right + A_0.\u171A().ᜊ().ᜂ()))
					{
						num2 = 6;
						continue;
					}
					return;
					IL_2A8:
					num2 = 0;
				}
			}
			IL_1B1:
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0006C07C File Offset: 0x0006B07C
	private new void ᜀ(sprᦰ A_0, float A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					sprᦰ sprᦰ;
					int num3;
					bool flag;
					RectangleF rectangleF2;
					int num4;
					RectangleF rectangleF4;
					switch (num2)
					{
					case 0:
						goto IL_293;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41C;
						default:
							if (false)
							{
							}
							sprᦰ.ᜊ()[num3].ᜀ(ClipboardData.b("㙴᭶ၸ୺ർ᩾", a_));
							sprᦰ.ᜊ()[num3].ᜀ(A_1);
							sprᦰ.ᜀ(A_1 - A_0.ᜁ().X, 0f, A_1 - A_0.ᜁ().X, 0f);
							num2 = 9;
							continue;
						}
						break;
					case 2:
					{
						sprᦰ sprᦰ2;
						flag = ((sprᦰ2.ᜂ() as Table).\u1712.TextWrappingStyle != TextWrappingStyle.Behind);
						goto IL_363;
					}
					case 3:
					{
						sprᦰ sprᦰ2 = A_0.ᜊ()[0].ᜊ()[num];
						num2 = 26;
						continue;
					}
					case 4:
						goto IL_216;
					case 5:
						flag = true;
						goto IL_363;
					case 6:
						goto IL_379;
					case 7:
						num3 = 0;
						num2 = 6;
						continue;
					case 8:
						sprᦰ.ᜊ()[num3].ᜀ(default(RectangleF));
						sprᦰ.ᜊ()[num3].ᜊ().RemoveAt(0);
						num2 = 32;
						continue;
					case 9:
						goto IL_3B2;
					case 10:
					{
						RectangleF rectangleF = sprᦰ.ᜊ()[num3].ᜁ();
						num2 = 12;
						continue;
					}
					case 11:
						if (true)
						{
						}
						if (num >= A_0.ᜊ()[0].ᜊ().Count)
						{
							num2 = 27;
							continue;
						}
						num2 = 29;
						continue;
					case 12:
					{
						RectangleF rectangleF;
						if (rectangleF.Left < A_1)
						{
							num2 = 1;
							continue;
						}
						goto IL_1D8;
					}
					case 13:
						num2 = 5;
						continue;
					case 14:
						flag = false;
						goto IL_363;
					case 15:
						goto IL_293;
					case 16:
						num2 = 2;
						continue;
					case 17:
						if (rectangleF2.Left > A_1)
						{
							num2 = 8;
							continue;
						}
						goto IL_3B2;
					case 18:
					{
						sprᦰ sprᦰ2;
						sprᦰ2.ᜀ(ClipboardData.b("㙴᭶ၸ୺ർ᩾", a_));
						sprᦰ2.ᜀ(A_1);
						num4 = 0;
						num2 = 33;
						continue;
					}
					case 19:
						goto IL_268;
					case 20:
						goto IL_179;
					case 21:
					{
						RectangleF rectangleF3;
						if (rectangleF3.Right < A_0.ᜊ()[0].ᜊ()[num].ᜁ().Right)
						{
							num2 = 3;
							continue;
						}
						goto IL_216;
					}
					case 22:
					{
						sprᦰ sprᦰ2;
						if (num4 >= sprᦰ2.ᜊ().Count)
						{
							num2 = 4;
							continue;
						}
						sprᦰ = sprᦰ2.ᜊ()[num4];
						sprᦰ.ᜀ(ClipboardData.b("㙴᭶ၸ୺ർ᩾", a_));
						sprᦰ.ᜀ(A_1);
						num2 = 31;
						continue;
					}
					case 23:
						if (num3 >= sprᦰ.ᜊ().Count)
						{
							num2 = 20;
							continue;
						}
						goto IL_41C;
					case 24:
						if (rectangleF4.Right > A_1)
						{
							num2 = 10;
							continue;
						}
						goto IL_1D8;
					case 25:
					{
						RectangleF rectangleF3 = A_0.ᜁ();
						num2 = 21;
						continue;
					}
					case 26:
					{
						sprᦰ sprᦰ2;
						if (!(sprᦰ2.ᜂ() as Table).IsTextBox)
						{
							num2 = 13;
							continue;
						}
						num2 = 30;
						continue;
					}
					case 27:
						return;
					case 28:
						goto IL_379;
					case 29:
						if (A_0.ᜊ()[0].ᜊ()[num].ᜂ() is Table)
						{
							num2 = 25;
							continue;
						}
						goto IL_216;
					case 30:
					{
						sprᦰ sprᦰ2;
						if ((sprᦰ2.ᜂ() as Table).\u1712.TextWrappingStyle != TextWrappingStyle.InFrontOfText)
						{
							num2 = 16;
							continue;
						}
						num2 = 14;
						continue;
					}
					case 31:
						if (sprᦰ.ᜊ().Count > 0)
						{
							num2 = 7;
							continue;
						}
						goto IL_179;
					case 32:
						goto IL_3B2;
					case 33:
						goto IL_268;
					}
					break;
					IL_179:
					num4++;
					num2 = 19;
					continue;
					IL_1D8:
					rectangleF2 = sprᦰ.ᜊ()[num3].ᜁ();
					num2 = 17;
					continue;
					IL_216:
					num++;
					num2 = 15;
					continue;
					IL_363:
					if (flag)
					{
						num2 = 18;
						continue;
					}
					goto IL_216;
					IL_268:
					num2 = 22;
					continue;
					IL_293:
					num2 = 11;
					continue;
					IL_379:
					num2 = 23;
					continue;
					IL_3B2:
					num3++;
					num2 = 28;
					continue;
					IL_41C:
					rectangleF4 = sprᦰ.ᜊ()[num3].ᜁ();
					num2 = 24;
				}
			}
			return;
		}
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x0006C5CC File Offset: 0x0006B5CC
	private new void ᜀ(sprᦰ A_0)
	{
		switch (0)
		{
		default:
		{
			RectangleF a_;
			for (;;)
			{
				a_ = A_0.ᜁ();
				int num = 3;
				for (;;)
				{
					float num2;
					float num3;
					float num4;
					TableCell tableCell;
					float num5;
					TableCell tableCell2;
					float num6;
					switch (num)
					{
					case 0:
						goto IL_137;
					case 1:
						goto IL_10D;
					case 2:
						a_.Height += num2;
						num = 6;
						continue;
					case 3:
						if (A_0.ᜂ() is TableCell)
						{
							num = 13;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10D;
						default:
							if (false)
							{
							}
							num = 16;
							continue;
						}
						break;
					case 4:
						goto IL_1BF;
					case 5:
						a_.Width += num3;
						num = 10;
						continue;
					case 6:
						goto IL_199;
					case 7:
						if (num4 > 0f)
						{
							num = 8;
							continue;
						}
						goto IL_22B;
					case 8:
						a_.X -= num4;
						a_.Width += num4;
						num = 15;
						continue;
					case 9:
						num5 = tableCell.OwnerRow.OwnerTable.TableFormat.CellSpacing * 2f;
						num = 0;
						continue;
					case 10:
						goto IL_2C0;
					case 11:
						tableCell2 = ((A_0.ᜂ() as sprᴛ).ᜁ() as TableCell);
						goto IL_FA;
					case 12:
						if (num6 > 0f)
						{
							num = 17;
							continue;
						}
						goto IL_1BF;
					case 13:
						tableCell2 = (A_0.ᜂ() as TableCell);
						goto IL_FA;
					case 14:
						if (num2 > 0f)
						{
							num = 2;
							continue;
						}
						goto IL_2FE;
					case 15:
						goto IL_22B;
					case 16:
						num = 11;
						continue;
					case 17:
						a_.Y -= num6;
						a_.Height += num6;
						num = 4;
						continue;
					case 18:
						if (num3 > 0f)
						{
							num = 5;
							continue;
						}
						goto IL_2C0;
					}
					break;
					IL_FA:
					tableCell = tableCell2;
					num5 = 0f;
					num = 1;
					continue;
					IL_10D:
					if (tableCell.OwnerRow.OwnerTable.TableFormat.CellSpacing > 0f)
					{
						num = 9;
						continue;
					}
					IL_137:
					num6 = (float)A_0.ᜂ().ᜀ().ᜋ().ᜁ() - num5;
					if (true)
					{
					}
					num = 12;
					continue;
					IL_1BF:
					num4 = (float)A_0.ᜂ().ᜀ().ᜋ().ᜃ() - num5;
					num = 7;
					continue;
					IL_22B:
					num3 = (float)A_0.ᜂ().ᜀ().ᜋ().ᜂ();
					num = 18;
					continue;
					IL_2C0:
					num2 = (float)A_0.ᜂ().ᜀ().ᜋ().ᜀ();
					num = 14;
				}
			}
			IL_199:
			IL_2FE:
			A_0.ᜀ(a_);
			return;
		}
		}
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0006C8E0 File Offset: 0x0006B8E0
	private new double ᜄ()
	{
		switch (0)
		{
		default:
		{
			double num;
			for (;;)
			{
				num = 0.0;
				TableCell tableCell = this.ᜏ.Rows[this.ᜐ()].Cells[this.ᜄ];
				TableCell tableCell2 = null;
				int num2 = this.ᜄ + 1;
				int num3 = 15;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_23F;
					case 1:
						if (tableCell2 != null)
						{
							num3 = 11;
							continue;
						}
						goto IL_2BB;
					case 2:
						goto IL_23F;
					case 3:
						if (num2 < this.ᜏ.Rows[this.ᜐ()].Cells.Count)
						{
							num3 = 17;
							continue;
						}
						tableCell2 = null;
						num3 = 14;
						continue;
					case 4:
						goto IL_26B;
					case 5:
						if (tableCell.Colspan > 1)
						{
							num3 = 9;
							continue;
						}
						goto IL_22E;
					case 6:
						if (tableCell2.CellFormat.HorizontalMerge != CellMerge.Continue)
						{
							num3 = 13;
							continue;
						}
						num += (double)tableCell2.Width;
						num2++;
						if (true)
						{
						}
						num3 = 3;
						continue;
					case 7:
						tableCell2 = this.ᜏ.Rows[this.ᜐ()].Cells[num2];
						num3 = 4;
						continue;
					case 8:
						goto IL_24B;
					case 9:
						num = 0.0;
						this.ᜐ = this.ᜄ + this.ᜑ + 1;
						num3 = 16;
						continue;
					case 10:
						return num;
					case 11:
						num3 = 6;
						continue;
					case 12:
						goto IL_26B;
					case 13:
						goto IL_2BB;
					case 14:
						goto IL_26B;
					case 15:
						if (this.ᜄ < this.ᜏ.Rows[this.ᜐ()].Cells.Count - 1)
						{
							num3 = 7;
							continue;
						}
						goto IL_26B;
					case 16:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_24B;
						default:
							if (false)
							{
							}
							goto IL_22E;
						}
						break;
					case 17:
						tableCell2 = this.ᜏ.Rows[this.ᜐ()].Cells[num2];
						num3 = 12;
						continue;
					}
					break;
					IL_24B:
					int num4;
					if (num4 >= tableCell.Colspan - 1)
					{
						num3 = 10;
						continue;
					}
					num += this.ᜅ[this.ᜐ++];
					this.ᜑ++;
					num4++;
					num3 = 0;
					continue;
					IL_22E:
					num4 = 0;
					num3 = 2;
					continue;
					IL_23F:
					num3 = 8;
					continue;
					IL_26B:
					num3 = 1;
					continue;
					IL_2BB:
					num3 = 5;
				}
			}
			return num;
		}
		}
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0006CBE8 File Offset: 0x0006BBE8
	private new spr\u2573 ᜃ()
	{
		int num = 0;
		spr\u17C8 spr_u17C;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_16A;
				default:
					if (false)
					{
					}
					this.ᜄ++;
					spr_u17C = null;
					num = 10;
					continue;
				}
				break;
			case 2:
				if (this.ᜎ.ᜁ() != null)
				{
					num = 11;
					continue;
				}
				goto IL_8C;
			case 3:
				goto IL_16A;
			case 4:
				spr_u17C = this.ᜎ.ᜁ()[this.ᜄ];
				num = 7;
				continue;
			case 5:
				if (this.ᜐ() == this.ᜎ.ᜀ() - 1)
				{
					num = 4;
					continue;
				}
				goto IL_8C;
			case 6:
				goto IL_8C;
			case 7:
				if (spr_u17C == null)
				{
					num = 9;
					continue;
				}
				goto IL_8C;
			case 8:
				if (spr_u17C == null)
				{
					num = 13;
					continue;
				}
				goto IL_104;
			case 9:
				spr_u17C = new sprᴛ(this.\u1712().ᜀ(this.ᜐ(), this.ᜄ));
				num = 6;
				continue;
			case 10:
				if (this.ᜎ != null)
				{
					num = 3;
					continue;
				}
				goto IL_8C;
			case 11:
				num = 5;
				continue;
			case 12:
				goto IL_192;
			case 13:
				spr_u17C = this.\u1712().ᜀ(this.ᜐ(), this.ᜄ);
				num = 12;
				continue;
			}
			if (this.ᜄ + 1 < this.ᜏ.Rows[this.ᜐ()].Cells.Count)
			{
				num = 1;
				continue;
			}
			goto IL_1FB;
			IL_8C:
			if (true)
			{
			}
			num = 8;
			continue;
			IL_16A:
			num = 2;
		}
		IL_104:
		return spr\u2573.ᜀ(spr_u17C, this.ᜆ, (float)this.ᜅ.ᜈ());
		IL_192:
		goto IL_104;
		IL_1FB:
		return null;
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0006CDF4 File Offset: 0x0006BDF4
	private new spr\u2573 ᜂ()
	{
		int num = 4;
		spr\u17C8 spr_u17C;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_86;
			case 1:
				if (true)
				{
				}
				num = 5;
				continue;
			case 2:
				if (spr_u17C == null)
				{
					num = 3;
					continue;
				}
				goto IL_EF;
			case 3:
				goto IL_A3;
			case 5:
				if (this.ᜄ < this.ᜏ.Rows[this.ᜐ()].Cells.Count)
				{
					num = 6;
					continue;
				}
				goto IL_108;
			case 6:
				spr_u17C = null;
				num = 2;
				continue;
			}
			if (this.ᜄ <= -1)
			{
				goto IL_108;
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
			IL_A3:
			spr_u17C = this.\u1712().ᜀ(this.ᜐ(), this.ᜄ);
			num = 0;
		}
		IL_86:
		IL_EF:
		return spr\u2573.ᜀ(spr_u17C, this.ᜆ, (float)this.ᜅ.ᜈ());
		IL_108:
		return null;
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0006CF0C File Offset: 0x0006BF0C
	private new void ᜃ(spr\u2573 A_0)
	{
		switch (A_0.\u1717())
		{
		case LayoutState.Unknown:
			this.ᜇ.ᜊ().ᜀ(this.ᜈ);
			this.ᜇ.ᜁ(this.ᜈ.ᜉ());
			this.ᜋ = true;
			return;
		case LayoutState.NotFitted:
			break;
		case LayoutState.Splitted:
			this.ᜂ(A_0);
			return;
		case LayoutState.Fitted:
			if (true)
			{
			}
			this.ᜀ(A_0);
			return;
		case LayoutState.Breaked:
			this.ᜅ(A_0);
			return;
		default:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				return;
			}
			break;
		}
		this.ᜁ(A_0);
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x0006CFC0 File Offset: 0x0006BFC0
	protected new virtual void ᜅ(spr\u2573 A_0)
	{
		for (;;)
		{
			spr\u2032 spr_u = this.ᜇ.ᜂ().ᜀ() as spr\u2032;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!spr_u.\u1716())
					{
						num = 4;
						continue;
					}
					goto IL_1F5;
				case 1:
					goto IL_103;
				case 2:
					goto IL_96;
				case 3:
					num = 6;
					continue;
				case 4:
					this.ᜄ(A_0);
					num = 9;
					continue;
				case 5:
					if (!spr_u.ᜌ())
					{
						num = 0;
						continue;
					}
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
						num = 2;
						continue;
					}
					break;
				case 6:
					this.ᜌ[this.ᜄ] = ((A_0.\u1716() is sprᴛ) ? (A_0.\u1716() as sprᴛ) : new sprᴛ(A_0.\u1718() as spr\u17C8));
					(this.ᜌ[this.ᜄ].ᜃ() as spr\u2032).ᜅ(true);
					num = 7;
					continue;
				case 7:
					goto IL_16D;
				case 8:
					if (spr_u.ᜐ())
					{
						num = 3;
						continue;
					}
					goto IL_16D;
				case 9:
					goto IL_145;
				}
				break;
				IL_96:
				this.ᜉ[this.ᜄ] = (double)this.ᜈ.ᜁ().Height + spr_u.ᜰ().ᜀ() + spr_u.ᜭ().ᜀ() + spr_u.ᜭ().ᜁ();
				this.ᜊ[this.ᜄ] = this.ᜐ();
				num = 1;
				continue;
				IL_16D:
				this.ᜇ.ᜊ().ᜀ(this.ᜈ);
				this.ᜇ.ᜁ(this.ᜈ.ᜉ());
				num = 5;
			}
		}
		IL_103:
		IL_145:
		IL_1F5:
		this.ᜋ = true;
		this.\u170D = LayoutState.Breaked;
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x0006D1D0 File Offset: 0x0006C1D0
	private new void ᜂ(spr\u2573 A_0)
	{
		for (;;)
		{
			IL_24:
			int num;
			spr\u2032 spr_u;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_66:
				num = 3;
				break;
			default:
				if (false)
				{
				}
				spr_u = (this.ᜇ.ᜂ().ᜀ() as spr\u2032);
				num = 2;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_100;
				case 1:
					if (A_0.\u171A().ᜈ())
					{
						num = 0;
						continue;
					}
					(this.ᜈ.ᜂ().ᜀ() as spr\u2032).ᜅ(true);
					spr_u.ᜅ(true);
					this.ᜀ(A_0);
					if (true)
					{
					}
					num = 6;
					continue;
				case 2:
					goto IL_5E;
				case 3:
					num = 1;
					continue;
				case 4:
					this.\u170D = LayoutState.Splitted;
					this.ᜑ().ᜀ(true);
					num = 5;
					continue;
				case 5:
					return;
				case 6:
					if (this.\u170D == LayoutState.Unknown)
					{
						num = 4;
						continue;
					}
					return;
				}
				goto IL_24;
			}
			IL_5E:
			if (!spr_u.ᜐ())
			{
				goto IL_66;
			}
			break;
		}
		IL_CB:
		this.ᜀ(A_0);
		this.ᜀ = LayoutState.Unknown;
		return;
		IL_100:
		goto IL_CB;
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x0006D300 File Offset: 0x0006C300
	private new void ᜁ(spr\u2573 A_0)
	{
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				spr\u2032 spr_u = this.ᜇ.ᜂ().ᜀ() as spr\u2032;
				num = 1;
				continue;
			}
			case 1:
			{
				spr\u2032 spr_u;
				if (spr_u.ᜐ())
				{
					num = 7;
					continue;
				}
				goto IL_51;
			}
			case 2:
			{
				RectangleF a_ = this.ᜈ.ᜁ();
				spr\u2032 spr_u;
				a_.Height = (float)Math.Abs(spr_u.ᜎ());
				this.ᜈ.ᜀ(a_);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6D;
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
				goto IL_110;
			case 4:
			{
				RectangleF rectangleF;
				if (rectangleF.Height == 0f)
				{
					num = 2;
					continue;
				}
				goto IL_59;
			}
			case 5:
				if (this.ᜐ() < this.\u1712().ᜄ() - 1)
				{
					num = 3;
					continue;
				}
				goto IL_169;
			case 6:
				goto IL_164;
			case 7:
			{
				RectangleF rectangleF = this.ᜈ.ᜁ();
				goto IL_6D;
			}
			}
			if (A_0.\u171C())
			{
				if (true)
				{
				}
				num = 0;
				continue;
			}
			num = 5;
			continue;
			IL_6D:
			num = 4;
		}
		IL_51:
		this.ᜀ = LayoutState.NotFitted;
		return;
		IL_59:
		this.ᜀ(A_0);
		return;
		IL_110:
		this.ᜂ(A_0);
		this.ᜀ = LayoutState.NotFitted;
		return;
		IL_164:
		goto IL_59;
		IL_169:
		this.ᜀ = LayoutState.NotFitted;
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x0006D480 File Offset: 0x0006C480
	private new void ᜀ(spr\u2573 A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			for (;;)
			{
				spr\u2032 spr_u;
				switch (num)
				{
				case 0:
					goto IL_158;
				case 1:
				{
					TextDirection textDirection = (this.ᜈ.ᜂ() as TableCell).CellFormat.TextDirection;
					TextDirection textDirection2 = textDirection;
					num = 2;
					continue;
				}
				case 2:
				{
					TextDirection textDirection2;
					switch (textDirection2)
					{
					case TextDirection.TopToBottom:
					case TextDirection.LeftToRightRotated:
					case TextDirection.RightToLeft:
					case TextDirection.RightToLeftRotated:
						this.ᜉ[this.ᜄ] = (double)this.ᜈ.ᜁ().Width + spr_u.ᜰ().ᜀ() + spr_u.ᜭ().ᜀ() + spr_u.ᜭ().ᜁ();
						num = 14;
						continue;
					case TextDirection.TopToBottomRotated:
						goto IL_158;
					default:
						num = 5;
						continue;
					}
					break;
				}
				case 4:
					if (spr_u.ᜌ())
					{
						num = 13;
						continue;
					}
					num = 11;
					continue;
				case 5:
					num = 0;
					continue;
				case 6:
					goto IL_AD;
				case 7:
					this.ᜄ(A_0);
					num = 12;
					continue;
				case 8:
					this.ᜌ[this.ᜄ] = ((A_0.\u1716() is sprᴛ) ? (A_0.\u1716() as sprᴛ) : new sprᴛ(A_0.\u1718() as spr\u17C8));
					(this.ᜌ[this.ᜄ].ᜃ() as spr\u2032).ᜅ(true);
					num = 15;
					continue;
				case 9:
					num = 8;
					continue;
				case 10:
					goto IL_25A;
				case 11:
					if (!spr_u.\u1716())
					{
						num = 7;
						continue;
					}
					goto IL_33D;
				case 12:
					goto IL_234;
				case 13:
					num = 10;
					continue;
				case 14:
					goto IL_8E;
				case 15:
					goto IL_27D;
				case 16:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_25A;
					default:
						if (false)
						{
						}
						goto IL_8E;
					}
					break;
				}
				if (!(this.ᜇ.ᜂ().ᜀ() as spr\u2032).ᜐ())
				{
					num = 9;
					continue;
				}
				goto IL_27D;
				IL_8E:
				this.ᜊ[this.ᜄ] = this.ᜐ();
				num = 6;
				continue;
				IL_25A:
				if (this.ᜈ.ᜂ() is TableCell)
				{
					num = 1;
					continue;
				}
				goto IL_8E;
				IL_158:
				this.ᜉ[this.ᜄ] = (double)this.ᜈ.ᜁ().Height + spr_u.ᜰ().ᜀ() + spr_u.ᜭ().ᜀ() + spr_u.ᜭ().ᜁ();
				num = 16;
				continue;
				IL_27D:
				this.ᜇ.ᜊ().ᜀ(this.ᜈ);
				this.ᜇ.ᜁ(this.ᜈ.ᜉ());
				spr_u = (this.ᜈ.ᜂ().ᜀ() as spr\u2032);
				num = 4;
			}
			IL_AD:
			IL_234:
			IL_33D:
			if (true)
			{
			}
			this.ᜋ = true;
			return;
		}
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x0006D7DC File Offset: 0x0006C7DC
	private new void ᜁ()
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
		this.ᜅ.ᜂ((double)this.ᜇ.ᜁ().Bottom);
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0006D838 File Offset: 0x0006C838
	private new spr\u25FC ᜀ(bool A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			double num4;
			double num5;
			double num15;
			double num17;
			for (;;)
			{
				RectangleF rectangleF = this.ᜅ.ᜇ();
				float num = 0f;
				int num2 = 25;
				for (;;)
				{
					float num3;
					float num7;
					float num9;
					float num12;
					spr\u2032 spr_u;
					int num16;
					bool flag;
					switch (num2)
					{
					case 0:
						num3 = this.ᜏ.Width / 20f;
						goto IL_89E;
					case 1:
						if (this.ᜏ.Width < Convert.ToSingle(num4) + Convert.ToSingle(num5))
						{
							num2 = 2;
							continue;
						}
						goto IL_68D;
					case 2:
					{
						float num6 = Convert.ToSingle((double)num7 - ((double)num + num5));
						num2 = 62;
						continue;
					}
					case 3:
						num2 = 32;
						continue;
					case 4:
						goto IL_305;
					case 5:
						if (this.ᜏ.Rows[A_1].Cells[A_2 + 1].CellFormat.HorizontalMerge == CellMerge.Continue)
						{
							num2 = 7;
							continue;
						}
						num5 = (double)this.ᜂ(A_1, A_2);
						num2 = 47;
						continue;
					case 6:
						if (true)
						{
						}
						if (this.ᜏ.Rows[A_1].Cells[A_2] != null)
						{
							num2 = 55;
							continue;
						}
						goto IL_C94;
					case 7:
					{
						int num8 = A_2;
						num9 = 0f;
						num2 = 36;
						continue;
					}
					case 8:
					{
						int num10 = A_1 + 1;
						num2 = 13;
						continue;
					}
					case 9:
						goto IL_7D3;
					case 10:
						if (this.ᜏ.Rows[A_1].Cells[A_2].CellFormat.VerticalMerge == CellMerge.Start)
						{
							num2 = 8;
							continue;
						}
						goto IL_8EE;
					case 11:
						goto IL_612;
					case 12:
						num2 = 57;
						continue;
					case 13:
						goto IL_AE8;
					case 14:
					{
						float num11;
						if (num11 < 0f)
						{
							num2 = 17;
							continue;
						}
						goto IL_917;
					}
					case 15:
						goto IL_590;
					case 16:
						num3 = this.ᜏ.Width;
						goto IL_89E;
					case 17:
					{
						float num11;
						num5 -= (double)num11;
						num2 = 64;
						continue;
					}
					case 18:
						if (this.ᜏ.Rows[A_1].Cells[A_2].CellFormat.HorizontalMerge == CellMerge.Start)
						{
							num2 = 53;
							continue;
						}
						num5 = (double)this.ᜂ(A_1, A_2);
						num2 = 78;
						continue;
					case 19:
						goto IL_1C4;
					case 20:
						if (num12 == 0f)
						{
							num2 = 30;
							continue;
						}
						goto IL_B58;
					case 21:
					{
						int num8;
						if (num8 + 1 < this.ᜏ.Rows[A_1].Cells.Count)
						{
							num2 = 12;
							continue;
						}
						goto IL_367;
					}
					case 22:
					{
						int num10;
						num12 += this.ᜏ.Rows[num10].Height;
						num10++;
						num2 = 76;
						continue;
					}
					case 23:
						goto IL_612;
					case 24:
					{
						int num8;
						if (this.ᜏ.Rows[A_1].Cells.Count > num8)
						{
							num2 = 41;
							continue;
						}
						goto IL_590;
					}
					case 25:
						if (this.ᜏ.Rows[A_1].Cells[A_2].WidthType != FtsWidth.Percentage)
						{
							num2 = 54;
							continue;
						}
						num2 = 0;
						continue;
					case 26:
					{
						int num13;
						if (num13 >= A_2)
						{
							num2 = 81;
							continue;
						}
						float num14;
						num14 += this.ᜂ(A_1, num13);
						num13++;
						num2 = 9;
						continue;
					}
					case 27:
					{
						RectangleF rectangleF2;
						if (rectangleF2.Height == this.ᜅ.ᜇ().Height)
						{
							num2 = 28;
							continue;
						}
						num15 = 0.0;
						num2 = 67;
						continue;
					}
					case 28:
						num15 = spr_u.ᜎ();
						num2 = 23;
						continue;
					case 29:
						goto IL_BA3;
					case 30:
						num12 = (float)num15;
						num15 = num5;
						num2 = 52;
						continue;
					case 31:
						num5 = this.ᜁ(A_1, A_2);
						num2 = 4;
						continue;
					case 32:
						goto IL_B53;
					case 33:
						if (this.ᜏ.Rows[A_1].Cells.Count == A_2 + 1)
						{
							num2 = 63;
							continue;
						}
						goto IL_917;
					case 34:
						if (this.ᜏ.Rows[A_1].Cells.Count == A_2 + 1)
						{
							num2 = 48;
							continue;
						}
						num2 = 5;
						continue;
					case 35:
					{
						if (num16 >= A_2)
						{
							num2 = 60;
							continue;
						}
						TableCell tableCell = this.ᜏ.Rows[A_1].Cells[num16];
						num += this.ᜂ(A_1, num16);
						num16++;
						num2 = 44;
						continue;
					}
					case 36:
						goto IL_BA3;
					case 37:
						goto IL_7D3;
					case 38:
						num2 = 14;
						continue;
					case 39:
						goto IL_8EE;
					case 40:
						num2 = 66;
						continue;
					case 41:
						num2 = 72;
						continue;
					case 42:
						if ((int)this.ᜏ.Rows[A_1].Cells[A_2].GridSpan <= this.ᜅ.Length)
						{
							num2 = 40;
							continue;
						}
						goto IL_305;
					case 43:
						num15 = spr_u.ᜎ();
						num2 = 11;
						continue;
					case 44:
						goto IL_467;
					case 45:
						if (num == 0f)
						{
							num2 = 61;
							continue;
						}
						num4 = (double)(num + rectangleF.X);
						num2 = 33;
						continue;
					case 46:
						if (num15 < spr_u.ᜎ())
						{
							num2 = 80;
							continue;
						}
						goto IL_612;
					case 47:
						goto IL_1C4;
					case 48:
					{
						float num14 = 0f;
						int num13 = 0;
						num2 = 37;
						continue;
					}
					case 49:
						if (this.ᜏ.Width > Convert.ToSingle(num4) + Convert.ToSingle(num5))
						{
							num2 = 51;
							continue;
						}
						goto IL_917;
					case 50:
						num2 = 18;
						continue;
					case 51:
					{
						float num11 = Convert.ToSingle((double)num7 - ((double)num + num5));
						num2 = 65;
						continue;
					}
					case 52:
						goto IL_B58;
					case 53:
						num2 = 34;
						continue;
					case 54:
						num2 = 16;
						continue;
					case 55:
					{
						IL_653:
						TextDirection textDirection = this.ᜏ.Rows[A_1].Cells[A_2].CellFormat.TextDirection;
						TextDirection textDirection2 = textDirection;
						num2 = 82;
						continue;
					}
					case 56:
						if (this.ᜏ.Rows[A_1].Cells[A_2].GridSpan > 1)
						{
							num2 = 73;
							continue;
						}
						goto IL_305;
					case 57:
					{
						int num8;
						if (this.ᜏ.Rows[A_1].Cells[num8 + 1].CellFormat.HorizontalMerge != CellMerge.Continue)
						{
							num2 = 74;
							continue;
						}
						num9 += this.ᜂ(A_1, num8);
						num8++;
						num2 = 29;
						continue;
					}
					case 58:
						if (A_0)
						{
							num2 = 50;
							continue;
						}
						goto IL_1C4;
					case 59:
						goto IL_1C4;
					case 60:
						num2 = 45;
						continue;
					case 61:
						num4 = (double)rectangleF.X + this.ᜆ[A_2];
						num2 = 83;
						continue;
					case 62:
					{
						float num6;
						if (num6 < 0f)
						{
							num2 = 70;
							continue;
						}
						goto IL_68D;
					}
					case 63:
						num2 = 1;
						continue;
					case 64:
						goto IL_917;
					case 65:
					{
						float num11;
						if (num5 > (double)num11)
						{
							num2 = 38;
							continue;
						}
						goto IL_917;
					}
					case 66:
						if (this.ᜏ.Rows[A_1].Cells.Count < this.ᜅ.Length)
						{
							num2 = 31;
							continue;
						}
						goto IL_305;
					case 67:
						goto IL_612;
					case 68:
					{
						int num10;
						if (num10 >= this.ᜏ.Rows.Count)
						{
							num2 = 39;
							continue;
						}
						num2 = 71;
						continue;
					}
					case 69:
						goto IL_467;
					case 70:
					{
						float num6;
						num5 += (double)num6;
						num2 = 79;
						continue;
					}
					case 71:
					{
						int num10;
						if (this.ᜏ.Rows[num10].Cells[A_2].CellFormat.VerticalMerge == CellMerge.Continue)
						{
							num2 = 22;
							continue;
						}
						goto IL_8EE;
					}
					case 72:
					{
						int num8;
						if (this.ᜏ.Rows[A_1].Cells[num8].CellFormat.HorizontalMerge == CellMerge.Continue)
						{
							num2 = 84;
							continue;
						}
						goto IL_590;
					}
					case 73:
						num2 = 42;
						continue;
					case 74:
						goto IL_367;
					case 75:
						goto IL_B68;
					case 76:
						goto IL_AE8;
					case 77:
						if (flag)
						{
							num2 = 43;
							continue;
						}
						num2 = 46;
						continue;
					case 78:
						goto IL_1C4;
					case 79:
						goto IL_68D;
					case 80:
					{
						RectangleF rectangleF2 = this.ᜅ.ᜆ();
						num2 = 27;
						continue;
					}
					case 81:
					{
						float num14;
						num5 = (double)(this.ᜅ.ᜇ().Width - num14 + this.ᜏ.Rows[A_1].RowFormat.Paddings.Left + this.ᜏ.Rows[A_1].RowFormat.Paddings.Right);
						this.ᜏ.Rows[A_1].Cells[A_2].Width = Convert.ToSingle(num5);
						num2 = 19;
						continue;
					}
					case 82:
					{
						TextDirection textDirection2;
						switch (textDirection2)
						{
						case TextDirection.TopToBottom:
						case TextDirection.LeftToRightRotated:
						case TextDirection.RightToLeft:
						case TextDirection.RightToLeftRotated:
						{
							num12 = this.ᜏ.Rows[this.ᜐ()].Height;
							float right = this.ᜏ.TableFormat.Paddings.Right;
							float left = this.ᜏ.TableFormat.Paddings.Left;
							num2 = 10;
							continue;
						}
						case TextDirection.TopToBottomRotated:
							goto IL_C94;
						default:
							num2 = 3;
							continue;
						}
						break;
					}
					case 83:
						goto IL_917;
					case 84:
					{
						int num8;
						num9 += this.ᜂ(A_1, num8);
						num2 = 15;
						continue;
					}
					}
					break;
					IL_1C4:
					spr_u = (this.ᜇ.ᜂ().ᜀ() as spr\u2032);
					flag = spr_u.ᜐ();
					num2 = 77;
					continue;
					IL_305:
					num16 = 0;
					num2 = 69;
					continue;
					IL_367:
					num2 = 24;
					continue;
					IL_467:
					num2 = 35;
					continue;
					IL_590:
					num5 = (double)num9;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_653;
					default:
						if (false)
						{
						}
						num2 = 59;
						continue;
					}
					IL_612:
					num2 = 6;
					continue;
					IL_68D:
					num2 = 49;
					continue;
					IL_7D3:
					num2 = 26;
					continue;
					IL_89E:
					num7 = num3;
					num5 = (double)this.ᜂ(A_1, A_2);
					num2 = 56;
					continue;
					IL_8EE:
					num2 = 20;
					continue;
					IL_917:
					num17 = (double)rectangleF.Y;
					num15 = (double)rectangleF.Height;
					num2 = 58;
					continue;
					IL_AE8:
					num2 = 68;
					continue;
					IL_B58:
					num5 = (double)num12;
					num2 = 75;
					continue;
					IL_BA3:
					num2 = 21;
				}
			}
			IL_B53:
			IL_B68:
			IL_C94:
			RectangleF a_ = new RectangleF((float)num4, (float)num17, (float)num5, (float)num15);
			return new spr\u25FC(a_);
		}
		}
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0006E4F4 File Offset: 0x0006D4F4
	private new float ᜀ(TableRow A_0)
	{
		int num = 12;
		for (;;)
		{
			float result;
			switch (num)
			{
			case 0:
				return result;
			case 1:
			{
				if (true)
				{
				}
				ushort num2 = (ushort)(A_0.Owner as Table).FrameFormat.FrameHeight;
				float num3 = (float)(num2 & 32767) / 20f;
				num = 8;
				continue;
			}
			case 2:
				num = 4;
				continue;
			case 3:
				num = 9;
				continue;
			case 4:
				if ((A_0.Owner as Table).IsFrame)
				{
					num = 14;
					continue;
				}
				return result;
			case 5:
				if ((A_0.Owner as Table).FrameFormat.FrameHeight != 0)
				{
					num = 1;
					continue;
				}
				return result;
			case 6:
				if ((A_0.Owner as Table).FrameFormat.FrameHeightRule == FrameSizeRule.Exact)
				{
					num = 10;
					continue;
				}
				return result;
			case 7:
			{
				float num3;
				result = num3;
				num = 0;
				continue;
			}
			case 8:
			{
				float num3;
				if (A_0.Height > num3)
				{
					num = 7;
					continue;
				}
				return result;
			}
			case 9:
				if (!(A_0.Owner is Table))
				{
					return result;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D5;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 10:
				num = 5;
				continue;
			case 11:
				goto IL_57;
			case 13:
				goto IL_D5;
			case 14:
				num = 6;
				continue;
			}
			if (A_0 == null)
			{
				num = 11;
				continue;
			}
			result = A_0.Height;
			num = 13;
			continue;
			IL_D5:
			if (A_0.HeightType != TableRowHeightType.Exactly)
			{
				return result;
			}
			num = 3;
		}
		IL_57:
		return 0f;
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x0006E6CC File Offset: 0x0006D6CC
	private new float ᜂ(int A_0, int A_1)
	{
		int num = 12;
		float num2;
		for (;;)
		{
			float num3;
			switch (num)
			{
			case 0:
				if (this.ᜏ.TableGrid.Count - 1 > A_1)
				{
					num = 5;
					continue;
				}
				goto IL_210;
			case 1:
				num2 = (float)(((spr\u1AB8)this.ᜏ.Rows[A_0].Cells[A_1]).ᜀ().ᜊ().ᜃ() + ((spr\u1AB8)this.ᜏ.Rows[A_0].Cells[A_1]).ᜀ().ᜊ().ᜂ() + ((spr\u1AB8)this.ᜏ.Rows[A_0].Cells[A_1]).ᜀ().ᜋ().ᜃ() + ((spr\u1AB8)this.ᜏ.Rows[A_0].Cells[A_1]).ᜀ().ᜋ().ᜂ());
				num = 13;
				continue;
			case 2:
				num3 = this.ᜏ.Rows[A_0].Cells[A_1].Width;
				goto IL_264;
			case 3:
				if (this.ᜏ.TableGrid.Count > 1)
				{
					num = 11;
					continue;
				}
				goto IL_210;
			case 4:
				num3 = this.ᜏ.Rows[A_0].Cells[A_1].Width / 20f;
				goto IL_264;
			case 5:
				goto IL_E8;
			case 6:
				if (num2 == 0f)
				{
					num = 8;
					continue;
				}
				goto IL_210;
			case 7:
				goto IL_210;
			case 8:
				num = 3;
				continue;
			case 9:
				num = 2;
				continue;
			case 10:
				if (num2 == 0f)
				{
					num = 1;
					continue;
				}
				return num2;
			case 11:
				num = 0;
				continue;
			case 12:
				if (true)
				{
				}
				break;
			case 13:
				return num2;
			}
			if (this.ᜏ.Rows[A_0].Cells[A_1].WidthType == FtsWidth.Percentage)
			{
				num = 4;
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
				num = 9;
				continue;
			}
			IL_E8:
			num2 = (float)this.ᜁ(A_0, A_1);
			num = 7;
			continue;
			IL_210:
			num = 10;
			continue;
			IL_264:
			num2 = num3;
			num = 6;
		}
		return num2;
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x0006E96C File Offset: 0x0006D96C
	private new double ᜁ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			double num;
			for (;;)
			{
				IL_6F:
				num = 0.0;
				int num2 = 0;
				for (;;)
				{
					IL_7B:
					int num3 = 17;
					for (;;)
					{
						int num4;
						int num7;
						switch (num3)
						{
						case 0:
							num3 = 1;
							continue;
						case 1:
							num4 = num2 + (int)this.ᜏ.Rows[A_0].Cells[A_1].GridSpan;
							goto IL_1A7;
						case 2:
						{
							float num5;
							if (num5 > this.ᜏ.Width)
							{
								num3 = 19;
								continue;
							}
							float num6 = this.ᜏ.Width / num5 * (float)((double)this.ᜏ.PreferredTableWidth.ᜁ() / 100.0);
							num *= (double)num6;
							num3 = 15;
							continue;
						}
						case 3:
							num7 = num2;
							goto IL_2D7;
						case 4:
							if (num2 - 1 >= 0)
							{
								num3 = 10;
								continue;
							}
							num3 = 6;
							continue;
						case 5:
							goto IL_1CE;
						case 6:
							num7 = 0;
							goto IL_2D7;
						case 7:
						{
							int num8 = 0;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7B;
							default:
								if (false)
								{
								}
								num3 = 5;
								continue;
							}
							break;
						}
						case 8:
						{
							int num8;
							if (num8 >= A_1)
							{
								num3 = 13;
								continue;
							}
							num2 += (int)this.ᜏ.Rows[A_0].Cells[num8].GridSpan;
							num8++;
							num3 = 14;
							continue;
						}
						case 9:
							num4 = this.ᜏ.TableGrid.Count - 1;
							goto IL_1A7;
						case 10:
							num3 = 3;
							continue;
						case 11:
						{
							float num5 = (this.ᜏ.TableGrid[this.ᜏ.TableGrid.Count - 1] - this.ᜏ.TableGrid[0]) / 20f;
							num3 = 2;
							continue;
						}
						case 12:
							goto IL_D5;
						case 13:
							num3 = 12;
							continue;
						case 14:
							goto IL_1CE;
						case 15:
							return num;
						case 16:
							goto IL_D5;
						case 17:
							if (this.ᜄ > 0)
							{
								num3 = 7;
								continue;
							}
							num2 = this.ᜄ;
							num3 = 16;
							continue;
						case 18:
							if (num2 + (int)this.ᜏ.Rows[A_0].Cells[A_1].GridSpan < this.ᜏ.TableGrid.Count)
							{
								num3 = 0;
								continue;
							}
							num3 = 9;
							continue;
						case 19:
						{
							float num5;
							float num9 = num5 / this.ᜏ.Width * (float)((double)this.ᜏ.PreferredTableWidth.ᜁ() / 100.0);
							num /= (double)num9;
							num3 = 20;
							continue;
						}
						case 20:
							return num;
						case 21:
							if (this.ᜏ.PreferredTableWidth.ᜀ() == FtsWidth.Percentage)
							{
								if (true)
								{
								}
								num3 = 11;
								continue;
							}
							return num;
						}
						goto IL_6F;
						IL_D5:
						num3 = 18;
						continue;
						IL_1A7:
						int index = num4;
						num3 = 4;
						continue;
						IL_1CE:
						num3 = 8;
						continue;
						IL_2D7:
						int index2 = num7;
						num = (double)((this.ᜏ.TableGrid[index] - this.ᜏ.TableGrid[index2]) / 20f);
						num3 = 21;
					}
				}
			}
			return num;
		}
		}
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x0006ED20 File Offset: 0x0006DD20
	private new void ᜀ()
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
		RectangleF a_ = this.ᜃ.ᜁ();
		a_.Width = this.ᜇ.ᜁ().Width + (float)(this.\u1712().ᜀ().ᜊ().ᜃ() + this.\u1712().ᜀ().ᜊ().ᜂ());
		a_.Height = this.ᜇ.ᜁ().Bottom - a_.Top + (float)this.\u1712().ᜀ().ᜊ().ᜀ();
		this.ᜃ.ᜀ(a_);
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x0006EDF4 File Offset: 0x0006DDF4
	private new bool ᜀ(int A_0, int A_1)
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				bool flag = A_0 > 0;
				int num = A_0;
				sprᦰ sprᦰ = this.ᜃ.ᜊ()[A_0];
				bool isHeader = (sprᦰ.ᜂ() as TableRow).IsHeader;
				int num2 = 7;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						if (!flag)
						{
							num2 = 8;
							continue;
						}
						sprᦰ sprᦰ2 = this.ᜃ.ᜊ()[num - 1];
						bool isHeader2 = (sprᦰ2.ᜂ() as TableRow).IsHeader;
						int num3 = this.ᜀ(A_0, A_1, num - 1);
						num2 = 3;
						continue;
					}
					case 1:
					{
						sprᦰ sprᦰ2;
						int num3;
						if ((sprᦰ2.ᜊ()[num3].ᜂ().ᜀ() as spr\u2032).ᜌ())
						{
							num2 = 5;
							continue;
						}
						goto IL_7F;
					}
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD;
						default:
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 3:
					{
						sprᦰ sprᦰ2;
						int num3;
						if (sprᦰ2.ᜊ().Count - 1 > num3)
						{
							num2 = 2;
							continue;
						}
						return true;
					}
					case 4:
						goto IL_126;
					case 5:
						num2 = 9;
						continue;
					case 6:
						goto IL_BD;
					case 7:
						goto IL_126;
					case 8:
						return false;
					case 9:
					{
						bool isHeader2;
						if (isHeader == isHeader2)
						{
							num2 = 6;
							continue;
						}
						goto IL_7F;
					}
					}
					break;
					IL_7F:
					num--;
					flag = (num > 0);
					num2 = 4;
					continue;
					IL_126:
					num2 = 0;
				}
			}
			IL_BD:
			return true;
		}
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x0006EFA8 File Offset: 0x0006DFA8
	private new int ᜀ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num2;
			for (;;)
			{
				float x = this.ᜃ.ᜊ()[A_0].ᜊ()[A_1].ᜁ().X;
				float num = 0f;
				num2 = 0;
				int num3 = 3;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_FA;
					case 1:
						if (x <= num)
						{
							num3 = 4;
							continue;
						}
						num2++;
						num3 = 0;
						continue;
					case 2:
						return A_1;
					case 3:
						goto IL_FA;
					case 4:
						return num2;
					case 5:
						if (num2 < this.ᜃ.ᜊ()[A_2].ᜊ().Count)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_127;
							}
							if (true)
							{
							}
							if (false)
							{
							}
							num = this.ᜃ.ᜊ()[A_2].ᜊ()[num2].ᜁ().X;
							num3 = 1;
							continue;
						}
						IL_127:
						num3 = 2;
						continue;
					}
					break;
					IL_FA:
					num3 = 5;
				}
			}
			return num2;
		}
		}
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x0006F0EC File Offset: 0x0006E0EC
	private new void ᜀ(ref RectangleF A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u2441 spr_u = this.ᜑ();
				bool flag = false;
				float num = 0f;
				int num2 = 0;
				int num3 = 0;
				int num4 = spr_u.ᜄ().Length;
				int num5 = 40;
				for (;;)
				{
					RowFormat tableFormat;
					MarginsF marginsF;
					int num6;
					float num7;
					int num8;
					switch (num5)
					{
					case 0:
						if (this.ᜏ.OwnerTextBody is TableCell)
						{
							num5 = 80;
							continue;
						}
						goto IL_B24;
					case 1:
						num5 = 61;
						continue;
					case 2:
						A_0.X += (A_0.Width - spr_u.ᜀ() - tableFormat.Paddings.Right) / 2f;
						num5 = 33;
						continue;
					case 3:
						num5 = 39;
						continue;
					case 4:
						goto IL_2E1;
					case 5:
						num5 = 41;
						continue;
					case 6:
						if (marginsF != null)
						{
							num5 = 3;
							continue;
						}
						goto IL_6A0;
					case 7:
						if (tableFormat.HorizontalAlignment == RowAlignment.Left)
						{
							num5 = 94;
							continue;
						}
						goto IL_32D;
					case 8:
						if (this.ᜏ.TableFormat.Positioning.HorizPosition == 0f)
						{
							num5 = 13;
							continue;
						}
						goto IL_79E;
					case 9:
						num5 = 15;
						continue;
					case 10:
						if (num3 >= num4)
						{
							num5 = 9;
							continue;
						}
						num5 = 28;
						continue;
					case 11:
						num5 = 47;
						continue;
					case 12:
						spr_u.ᜂ()[num6] = num7;
						num5 = 72;
						continue;
					case 13:
						num5 = 0;
						continue;
					case 14:
						return;
					case 15:
						if (!flag)
						{
							num5 = 62;
							continue;
						}
						goto IL_65B;
					case 16:
						goto IL_2E1;
					case 17:
						num5 = 85;
						continue;
					case 18:
						if (this.ᜑ().ᜈ())
						{
							num5 = 86;
							continue;
						}
						num5 = 23;
						continue;
					case 19:
						if (this.ᜑ().ᜈ())
						{
							num5 = 91;
							continue;
						}
						goto IL_712;
					case 20:
						num5 = 19;
						continue;
					case 21:
						goto IL_C4C;
					case 22:
						flag = true;
						num += spr_u.ᜂ()[num3];
						num2++;
						num5 = 58;
						continue;
					case 23:
						if (this.ᜏ.OwnerTextBody is TableCell)
						{
							num5 = 77;
							continue;
						}
						goto IL_83C;
					case 24:
						if (this.ᜏ.IndentFromLeft != -3.4028235E+38f)
						{
							num5 = 31;
							continue;
						}
						goto IL_32D;
					case 25:
						if (marginsF != null)
						{
							num5 = 67;
							continue;
						}
						goto IL_373;
					case 26:
						if (this.ᜏ.TableFormat.Positioning.HorizPositionAbs == HorizontalPosition.Right)
						{
							num5 = 54;
							continue;
						}
						goto IL_712;
					case 27:
						num5 = 68;
						continue;
					case 28:
						if (spr_u.ᜄ()[num3])
						{
							num5 = 22;
							continue;
						}
						goto IL_9DE;
					case 29:
						goto IL_307;
					case 30:
						if (!this.ᜑ().ᜈ())
						{
							goto IL_6A0;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BA3;
						default:
							if (false)
							{
							}
							num5 = 78;
							continue;
						}
						break;
					case 31:
						num5 = 7;
						continue;
					case 32:
						num5 = 8;
						continue;
					case 33:
						goto IL_443;
					case 34:
						goto IL_C4C;
					case 35:
						if (this.ᜏ.TableFormat.Positioning.HorizPositionAbs == HorizontalPosition.Left)
						{
							num5 = 32;
							continue;
						}
						goto IL_79E;
					case 36:
						if (A_0.Width > spr_u.ᜀ())
						{
							num5 = 90;
							continue;
						}
						goto IL_B24;
					case 37:
						if (!spr_u.ᜄ()[num6])
						{
							num5 = 12;
							continue;
						}
						goto IL_787;
					case 38:
						if (!(this.ᜏ.OwnerTextBody as TableCell).OwnerRow.OwnerTable.IsTextBox)
						{
							num5 = 64;
							continue;
						}
						goto IL_83C;
					case 39:
						if (spr_u.ᜀ() + tableFormat.Paddings.Right < marginsF.Right)
						{
							num5 = 83;
							continue;
						}
						A_0.X = (this.ᜆ as spr\u1DA4).ᜈ().Width + marginsF.Left + marginsF.Right - spr_u.ᜀ() - tableFormat.Paddings.Right;
						num5 = 87;
						continue;
					case 40:
						goto IL_307;
					case 41:
						if (this.ᜏ.TableFormat.Positioning.HorizRelationTo == HorizontalRelation.Page)
						{
							num5 = 71;
							continue;
						}
						goto IL_6A0;
					case 42:
						goto IL_32D;
					case 43:
						goto IL_443;
					case 44:
						goto IL_32D;
					case 45:
						goto IL_443;
					case 46:
						num5 = 25;
						continue;
					case 47:
						spr_u.ᜀ((spr_u.ᜀ() == 0f) ? this.ᜏ.Width : spr_u.ᜀ());
						num5 = 60;
						continue;
					case 48:
						if (this.ᜏ.TableFormat.Positioning.HorizPosition == 0f)
						{
							num5 = 1;
							continue;
						}
						goto IL_373;
					case 49:
						goto IL_32D;
					case 50:
						if (this.ᜑ().ᜈ())
						{
							num5 = 17;
							continue;
						}
						goto IL_373;
					case 51:
						num5 = 48;
						continue;
					case 52:
						num5 = 24;
						continue;
					case 53:
						goto IL_32D;
					case 54:
						goto IL_40E;
					case 55:
						if (num6 >= num8)
						{
							num5 = 14;
							continue;
						}
						num5 = 37;
						continue;
					case 56:
						if (this.ᜏ.TableFormat.Positioning.HorizPosition == 0f)
						{
							num5 = 73;
							continue;
						}
						goto IL_6A0;
					case 57:
						A_0.X = marginsF.Left - spr_u.ᜀ() - tableFormat.Paddings.Left - this.ᜏ.IndentFromLeft;
						num5 = 43;
						continue;
					case 58:
						goto IL_9DE;
					case 59:
						goto IL_32D;
					case 60:
						if (spr_u.ᜀ() > 0f)
						{
							num5 = 2;
							continue;
						}
						goto IL_443;
					case 61:
						if (this.ᜏ.TableFormat.Positioning.HorizRelationTo == HorizontalRelation.Page)
						{
							num5 = 27;
							continue;
						}
						goto IL_373;
					case 62:
						num5 = 69;
						continue;
					case 63:
						if (tableFormat.HorizontalAlignment == RowAlignment.Center)
						{
							num5 = 11;
							continue;
						}
						goto IL_443;
					case 64:
						A_0.X += this.ᜏ.IndentFromLeft;
						num5 = 53;
						continue;
					case 65:
						num5 = 6;
						continue;
					case 66:
						if (tableFormat.HorizontalAlignment != RowAlignment.Right)
						{
							num5 = 20;
							continue;
						}
						goto IL_40E;
					case 67:
						num5 = 89;
						continue;
					case 68:
						if (!(this.ᜏ.OwnerTextBody is TableCell))
						{
							num5 = 46;
							continue;
						}
						goto IL_373;
					case 69:
						if (spr_u.ᜀ() > A_0.Width)
						{
							num5 = 81;
							continue;
						}
						goto IL_65B;
					case 70:
						if (!(this.ᜏ.OwnerTextBody is TableCell))
						{
							num5 = 65;
							continue;
						}
						goto IL_6A0;
					case 71:
						num5 = 70;
						continue;
					case 72:
						goto IL_787;
					case 73:
						num5 = 75;
						continue;
					case 74:
						goto IL_443;
					case 75:
						if (this.ᜏ.TableFormat.Positioning.HorizPositionAbs == HorizontalPosition.Right)
						{
							num5 = 5;
							continue;
						}
						goto IL_6A0;
					case 76:
						if (num2 == this.ᜏ.Rows[this.ᜐ() + 1].Cells.Count)
						{
							num5 = 82;
							continue;
						}
						goto IL_C4C;
					case 77:
						if (true)
						{
						}
						num5 = 38;
						continue;
					case 78:
						num5 = 56;
						continue;
					case 79:
						goto IL_219;
					case 80:
						num5 = 36;
						continue;
					case 81:
						spr_u.ᜀ(A_0.Width - (float)spr_u.ᜅ() - (float)spr_u.ᜆ());
						num5 = 34;
						continue;
					case 82:
					{
						spr_u.ᜀ(0f);
						int num9 = 0;
						num5 = 79;
						continue;
					}
					case 83:
						A_0.X = (this.ᜆ as spr\u1DA4).ᜈ().Width + marginsF.Left;
						num5 = 74;
						continue;
					case 84:
						goto IL_219;
					case 85:
						if (this.ᜏ.TableFormat.Positioning.HorizPositionAbs == HorizontalPosition.Left)
						{
							num5 = 51;
							continue;
						}
						goto IL_373;
					case 86:
						num5 = 35;
						continue;
					case 87:
						goto IL_BA3;
					case 88:
						if (!this.ᜏ.IsTextBox)
						{
							num5 = 52;
							continue;
						}
						goto IL_443;
					case 89:
						if (spr_u.ᜀ() + tableFormat.Paddings.Left + this.ᜏ.IndentFromLeft < marginsF.Left)
						{
							num5 = 57;
							continue;
						}
						A_0.X = this.ᜏ.IndentFromLeft;
						num5 = 92;
						continue;
					case 90:
						A_0.X += this.ᜏ.IndentFromLeft;
						num5 = 59;
						continue;
					case 91:
						num5 = 26;
						continue;
					case 92:
						goto IL_443;
					case 93:
					{
						int num9;
						if (num9 >= num2)
						{
							num5 = 21;
							continue;
						}
						spr\u2441 spr_u2 = spr_u;
						spr_u2.ᜀ(spr_u2.ᜀ() + spr_u.ᜂ()[num9]);
						num9++;
						num5 = 84;
						continue;
					}
					case 94:
						num5 = 18;
						continue;
					}
					break;
					IL_219:
					num5 = 93;
					continue;
					IL_2E1:
					num5 = 55;
					continue;
					IL_307:
					num5 = 10;
					continue;
					IL_32D:
					num5 = 30;
					continue;
					IL_373:
					num5 = 66;
					continue;
					IL_40E:
					A_0.X += A_0.Width - spr_u.ᜀ() - tableFormat.Paddings.Right;
					num5 = 45;
					continue;
					IL_443:
					A_0.Width = spr_u.ᜀ();
					num7 = (spr_u.ᜀ() - num) / (float)(spr_u.ᜂ().Length - num2);
					num6 = 0;
					num8 = spr_u.ᜄ().Length;
					num5 = 4;
					continue;
					IL_BA3:
					goto IL_443;
					IL_65B:
					num5 = 76;
					continue;
					IL_6A0:
					num5 = 50;
					continue;
					IL_712:
					num5 = 63;
					continue;
					IL_787:
					num6++;
					num5 = 16;
					continue;
					IL_79E:
					A_0.X += this.ᜏ.IndentFromLeft;
					num5 = 44;
					continue;
					IL_83C:
					A_0.X += this.ᜏ.IndentFromLeft - this.\u1713();
					num5 = 42;
					continue;
					IL_9DE:
					num3++;
					num5 = 29;
					continue;
					IL_B24:
					A_0.X += this.ᜏ.IndentFromLeft - this.\u1713();
					num5 = 49;
					continue;
					IL_C4C:
					tableFormat = this.ᜏ.TableFormat;
					marginsF = this.\u170D();
					num5 = 88;
				}
			}
			return;
		}
	}

	// Token: 0x0400133D RID: 4925
	private new const float ᜀ = 16f;

	// Token: 0x0400133E RID: 4926
	private new bool ᜁ;

	// Token: 0x0400133F RID: 4927
	private new int ᜂ = -1;

	// Token: 0x04001340 RID: 4928
	private new int ᜃ = -1;

	// Token: 0x04001341 RID: 4929
	private new int ᜄ = -1;

	// Token: 0x04001342 RID: 4930
	private new double[] ᜅ;

	// Token: 0x04001343 RID: 4931
	private new double[] ᜆ;

	// Token: 0x04001344 RID: 4932
	private new sprᦰ ᜇ;

	// Token: 0x04001345 RID: 4933
	private new sprᦰ ᜈ;

	// Token: 0x04001346 RID: 4934
	private double[] ᜉ;

	// Token: 0x04001347 RID: 4935
	private int[] ᜊ;

	// Token: 0x04001348 RID: 4936
	protected bool ᜋ;

	// Token: 0x04001349 RID: 4937
	private sprᴛ[] ᜌ;

	// Token: 0x0400134A RID: 4938
	private LayoutState \u170D;

	// Token: 0x0400134B RID: 4939
	private sprᲲ ᜎ;

	// Token: 0x0400134C RID: 4940
	private Table ᜏ;

	// Token: 0x0400134D RID: 4941
	private int ᜐ;

	// Token: 0x0400134E RID: 4942
	private int ᜑ;

	// Token: 0x0400134F RID: 4943
	private bool \u1712;

	// Token: 0x04001350 RID: 4944
	private int \u1713;

	// Token: 0x04001351 RID: 4945
	private new bool \u1714;
}
