using System;
using System.Collections.Generic;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x020002D9 RID: 729
internal class sprថ : TextBoxShapeBase, ICheckBoxShape
{
	// Token: 0x06002CBB RID: 11451 RVA: 0x001928F0 File Offset: 0x001918F0
	internal sprថ(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
		base.ShapeType = ExcelShapeType.TextBox;
		base.Fill.BackColor = spr\u1D39.ᜂ;
		base.Fill.ForeColor = spr\u1D39.ᜁ;
		base.Line.ForeColor = spr\u1D39.ᜅ;
		base.Line.BackColor = spr\u1D39.ᜂ;
		base.HasFill = false;
		base.Fill.Transparency = 1.0;
		base.VmlShape = true;
	}

	// Token: 0x06002CBC RID: 11452 RVA: 0x00192970 File Offset: 0x00191970
	internal sprថ(spr\u1DF5 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3) : base(A_0, A_1, A_2, A_3)
	{
		base.ShapeType = ExcelShapeType.TextBox;
		base.VmlShape = true;
	}

	// Token: 0x06002CBD RID: 11453 RVA: 0x00192998 File Offset: 0x00191998
	private void ᜀ()
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
		base.ShapeType = ExcelShapeType.TextBox;
		this.m_bUpdateLineFill = true;
	}

	// Token: 0x06002CBE RID: 11454 RVA: 0x001929E4 File Offset: 0x001919E4
	protected virtual void ᜇ()
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
		this.ᜏ = (sprἼ)spr\u231F.ᜀ(MsoRecords.msofbtSp);
		this.ᜏ.ᜉ(2);
		this.ᜏ.ᜈ(201);
		this.ᜏ.ᜆ(true);
		this.ᜏ.ᜇ(true);
	}

	// Token: 0x06002CBF RID: 11455 RVA: 0x00192A68 File Offset: 0x00191A68
	internal virtual void ᜀ(spr\u21EB A_0)
	{
		int a_ = 9;
		switch (0)
		{
		default:
		{
			int num = 2;
			sprὙ sprὙ;
			spr᪙ spr᪙;
			for (;;)
			{
				spr᧗ spr᧗;
				spr\u2003 spr_u;
				spr\u2223 spr_u2;
				sprἨ sprἨ;
				sprទ a_3;
				switch (num)
				{
				case 0:
				{
					spr\u23E7 spr_u23E;
					if (spr_u23E.ᜀ().Length > 0)
					{
						num = 20;
						continue;
					}
					goto IL_307;
				}
				case 1:
					spr᧗.ᜀ((this.ᜁ() as spr\u1A8B).ᜀ());
					num = 15;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_110;
					default:
						if (false)
						{
						}
						goto IL_307;
					}
					break;
				case 4:
					goto IL_90;
				case 5:
					if (base.ChildAnchor != null)
					{
						if (true)
						{
						}
						num = 13;
						continue;
					}
					sprὙ.ᜀ(base.ClientAnchor);
					num = 21;
					continue;
				case 6:
				{
					if (spr_u == null)
					{
						num = 10;
						continue;
					}
					spr_u2 = (spr_u.ᜃ()[0] as spr\u2223);
					sprᯄ a_2 = (sprᯄ)this.ᜀ(spr_u.ᜃ(), TObjSubRecordType.ftCbls);
					sprἨ = (sprἨ)this.ᜀ(spr_u.ᜃ(), TObjSubRecordType.ftCblsData);
					num = 19;
					continue;
				}
				case 7:
					goto IL_110;
				case 8:
					if (this.ᜁ() != null)
					{
						num = 1;
						continue;
					}
					goto IL_95;
				case 9:
					goto IL_27C;
				case 10:
				{
					spr_u = (spr\u2003)spr\u175E.ᜀ(TBIFFRecord.OBJ);
					spr_u2 = new spr\u2223();
					spr_u2.ᜀ(TObjType.otCheckBox);
					spr_u2.ᜀ(true);
					spr_u2.ᜂ(true);
					spr_u2.ᜃ(false);
					a_3 = new sprទ();
					sprᯄ a_2 = new sprᯄ();
					sprἨ = new sprἨ();
					spr_u.ᜀ(spr_u2);
					spr_u.ᜀ(a_2);
					num = 7;
					continue;
				}
				case 11:
					spr᧗ = new spr᧗();
					spr_u.ᜀ(spr᧗);
					num = 9;
					continue;
				case 12:
					spr᧗ = (spr᧗)this.ᜀ(spr_u.ᜃ(), TObjSubRecordType.ftCblsFmla);
					num = 16;
					continue;
				case 13:
					sprὙ.ᜀ(base.ChildAnchor);
					num = 18;
					continue;
				case 14:
					goto IL_20D;
				case 15:
					goto IL_95;
				case 16:
					goto IL_20D;
				case 17:
				{
					spr_u2.ᜁ((base.OldObjId > 0U) ? ((ushort)base.OldObjId) : ((ushort)base.ParentWorkbook.CurrentObjectId));
					sprὙ.ᜀ(this.ᜏ);
					spr\u23E7 spr_u23E = this.SerializeOptions(sprὙ);
					num = 0;
					continue;
				}
				case 18:
					goto IL_277;
				case 19:
					if (this.ᜁ() != null)
					{
						num = 12;
						continue;
					}
					goto IL_20D;
				case 20:
				{
					spr\u23E7 spr_u23E;
					sprὙ.ᜀ(spr_u23E);
					num = 3;
					continue;
				}
				case 21:
					goto IL_3C3;
				}
				if (A_0 == null)
				{
					num = 4;
					continue;
				}
				sprὙ = (sprὙ)spr\u231F.ᜀ(MsoRecords.msofbtSpContainer);
				spr᪙ = (spr᪙)spr\u231F.ᜀ(MsoRecords.msofbtClientData);
				spr_u2 = null;
				spr_u = base.Obj;
				spr᧗ = null;
				num = 6;
				continue;
				IL_95:
				num = 17;
				continue;
				IL_110:
				if (this.ᜁ() != null)
				{
					num = 11;
					continue;
				}
				goto IL_27C;
				IL_20D:
				spr᪙.ᜀ(spr_u);
				sprἨ.ᜀ(this.ᜈ());
				sprἨ.ᜀ(this.ᜂ);
				num = 8;
				continue;
				IL_27C:
				spr_u.ᜀ(sprἨ);
				spr_u.ᜀ(a_3);
				num = 14;
				continue;
				IL_307:
				num = 5;
			}
			IL_90:
			throw new ArgumentNullException(RecordTableEnumerator.b("䰾ㅀ⑂㝄ц♈╊㥌⹎㡐㵒ご╖", a_));
			IL_277:
			IL_3C3:
			sprὙ.ᜀ(spr᪙);
			base.IsTextLocked = false;
			spr\u2016 a_4 = base.ᜁ(sprὙ);
			sprὙ.ᜀ(a_4);
			A_0.ᜀ(sprὙ);
			return;
		}
		}
	}

	// Token: 0x06002CC0 RID: 11456 RVA: 0x00192E60 File Offset: 0x00191E60
	private spr\u25AD ᜀ(List<spr\u25AD> A_0, TObjSubRecordType A_1)
	{
		switch (0)
		{
		default:
		{
			spr\u25AD result;
			for (;;)
			{
				result = null;
				int num = 0;
				int count = A_0.Count;
				int num2 = 6;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_51;
						default:
						{
							if (false)
							{
							}
							spr\u25AD spr_u25AD;
							result = spr_u25AD;
							num2 = 2;
							continue;
						}
						}
						break;
					case 1:
					{
						spr\u25AD spr_u25AD;
						if (spr_u25AD.ᜏ() == A_1)
						{
							num2 = 0;
							continue;
						}
						num++;
						num2 = 5;
						continue;
					}
					case 2:
						return result;
					case 3:
					{
						if (num >= count)
						{
							num2 = 4;
							continue;
						}
						spr\u25AD spr_u25AD = A_0[num];
						num2 = 1;
						continue;
					}
					case 4:
						return result;
					case 5:
						goto IL_B6;
					case 6:
						goto IL_51;
					}
					break;
					IL_B6:
					if (true)
					{
					}
					num2 = 3;
					continue;
					IL_51:
					goto IL_B6;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06002CC1 RID: 11457 RVA: 0x00192F4C File Offset: 0x00191F4C
	[CLSCompliant(false)]
	internal virtual spr\u23E7 ᜀ(spr\u1D3B A_0)
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
		spr\u23E7 spr_u23E = base.SerializeOptions(A_0);
		spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
		ᜀ = new spr\u23E7.ᜀ();
		ᜀ.ᜀ(MsoOptions.SizeTextToFitShape);
		ᜀ.ᜀ(1703944);
		spr_u23E.ᜁ(ᜀ);
		return spr_u23E;
	}

	// Token: 0x06002CC2 RID: 11458 RVA: 0x00192FBC File Offset: 0x00191FBC
	[CLSCompliant(false)]
	internal virtual spr\u23E7 ᜃ()
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
		spr\u23E7 spr_u23E = base.CreateDefaultOptions();
		spr_u23E.ᜉ(3);
		spr_u23E.ᜈ(2);
		return spr_u23E;
	}

	// Token: 0x06002CC3 RID: 11459 RVA: 0x00193010 File Offset: 0x00192010
	internal virtual void ᜀ(spr᪙ A_0, ExcelParseOptions A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				base.ParseClientData(A_0, A_1);
				spr\u2003 spr_u = A_0.ᜁ();
				List<spr\u25AD> list = spr_u.ᜃ();
				int num = 0;
				int count = list.Count;
				int num2 = 5;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						TObjSubRecordType tobjSubRecordType;
						if (tobjSubRecordType != TObjSubRecordType.ftCbls)
						{
							num2 = 9;
							continue;
						}
						goto IL_19D;
					}
					case 1:
					{
						TObjSubRecordType tobjSubRecordType;
						switch (tobjSubRecordType)
						{
						case TObjSubRecordType.ftCblsData:
						{
							spr\u25AD spr_u25AD;
							this.ᜂ = ((sprἨ)spr_u25AD).ᜀ();
							this.ᜄ = ((sprἨ)spr_u25AD).ᜂ();
							num2 = 10;
							continue;
						}
						case TObjSubRecordType.ftLbsData:
							goto IL_19D;
						case TObjSubRecordType.ftCblsFmla:
						{
							spr\u25AD spr_u25AD;
							sprỜ sprỜ = ((spr᧗)spr_u25AD).ᜀ()[0] as sprỜ;
							this.ᜃ = sprỜ.ᜀ(base.Workbook, base.Worksheet as IWorksheet);
							num2 = 2;
							continue;
						}
						default:
							num2 = 3;
							continue;
						}
						break;
					}
					case 2:
						goto IL_19D;
					case 3:
						goto IL_1AF;
					case 4:
						goto IL_147;
					case 5:
						goto IL_147;
					case 6:
						goto IL_19D;
					case 7:
					{
						if (num >= count)
						{
							num2 = 8;
							continue;
						}
						spr\u25AD spr_u25AD = list[num];
						TObjSubRecordType tobjSubRecordType = spr_u25AD.ᜏ();
						num2 = 0;
						continue;
					}
					case 8:
						return;
					case 9:
						num2 = 1;
						continue;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1AF;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_19D;
						}
						break;
					}
					break;
					IL_147:
					num2 = 7;
					continue;
					IL_19D:
					num++;
					num2 = 4;
					continue;
					IL_1AF:
					num2 = 6;
				}
			}
			return;
		}
	}

	// Token: 0x06002CC4 RID: 11460 RVA: 0x001931DC File Offset: 0x001921DC
	public virtual IShape ᜀ(object A_0, Dictionary<string, string> A_1, Dictionary<int, int> A_2, bool A_3)
	{
		sprថ sprថ;
		for (;;)
		{
			sprថ = (sprថ)base.Clone(A_0, A_1, A_2, A_3);
			XlsWorksheetBase worksheet = sprថ.Worksheet;
			XlsWorkbook parentWorkbook = worksheet.ParentWorkbook;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_83;
				case 1:
					if (this.ᜃ != null)
					{
						num = 4;
						continue;
					}
					goto IL_83;
				case 2:
					return sprថ;
				case 3:
					if (!A_3)
					{
						return sprថ;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_59;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 4:
					sprថ.ᜃ = (this.ᜃ as ICombinedRange).Clone(worksheet, A_1, parentWorkbook);
					num = 0;
					continue;
				case 5:
					goto IL_59;
				}
				break;
				IL_59:
				(sprថ.Worksheet.CheckBoxes as CheckBoxCollection).AddCheckBox(sprថ);
				num = 2;
				continue;
				IL_83:
				num = 3;
			}
		}
		return sprថ;
	}

	// Token: 0x06002CC5 RID: 11461 RVA: 0x001932D4 File Offset: 0x001922D4
	public new CommentHAlignType ᜆ()
	{
		int a_ = 5;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("稺儼嘾♀ⵂ⡄≆❈㽊", a_));
	}

	// Token: 0x06002CC6 RID: 11462 RVA: 0x0019332C File Offset: 0x0019232C
	public void ᜀ(CommentHAlignType A_0)
	{
		int a_ = 19;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("ࡈ❊⑌⡎㽐㹒ご㥖ⵘ", a_));
	}

	// Token: 0x06002CC7 RID: 11463 RVA: 0x00193384 File Offset: 0x00192384
	public new CommentVAlignType ᜅ()
	{
		int a_ = 19;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("ࡈ❊⑌⡎㽐㹒ご㥖ⵘ", a_));
	}

	// Token: 0x06002CC8 RID: 11464 RVA: 0x001933DC File Offset: 0x001923DC
	public void ᜀ(CommentVAlignType A_0)
	{
		int a_ = 10;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("Ŀ⹁ⵃⅅ♇❉⥋⁍⑏", a_));
	}

	// Token: 0x06002CC9 RID: 11465 RVA: 0x00193434 File Offset: 0x00192434
	public new TextRotationType ᜄ()
	{
		int a_ = 1;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("收嘸伺尼䬾⡀ⱂ⭄", a_));
	}

	// Token: 0x06002CCA RID: 11466 RVA: 0x0019348C File Offset: 0x0019248C
	public void ᜀ(TextRotationType A_0)
	{
		int a_ = 3;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		throw new NotSupportedException(RecordTableEnumerator.b("欸吺䤼帾㕀⩂⩄⥆", a_));
	}

	// Token: 0x06002CCB RID: 11467 RVA: 0x001934E4 File Offset: 0x001924E4
	public new CheckState ᜂ()
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

	// Token: 0x06002CCC RID: 11468 RVA: 0x00193528 File Offset: 0x00192528
	public new void ᜁ(CheckState A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				this.ᜃ.BooleanValue = Convert.ToBoolean(this.ᜀ(this.ᜂ));
				num = 2;
				continue;
			case 2:
				return;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					this.ᜂ = A_0;
					num = 4;
					continue;
				}
				break;
			case 4:
				if (this.ᜃ != null)
				{
					num = 1;
					continue;
				}
				return;
			}
			if (this.ᜂ == A_0)
			{
				break;
			}
			num = 3;
		}
	}

	// Token: 0x06002CCD RID: 11469 RVA: 0x001935E8 File Offset: 0x001925E8
	public new IXLSRange ᜁ()
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
		return this.ᜃ;
	}

	// Token: 0x06002CCE RID: 11470 RVA: 0x0019362C File Offset: 0x0019262C
	public void ᜀ(IXLSRange A_0)
	{
		int a_ = 1;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4A;
				default:
					if (false)
					{
					}
					if (A_0.Row == A_0.LastRow)
					{
						num = 6;
						continue;
					}
					goto IL_7A;
				}
				break;
			case 1:
				goto IL_111;
			case 2:
				if (A_0.Column != A_0.LastColumn)
				{
					num = 4;
					continue;
				}
				goto IL_4A;
			case 3:
			{
				string value = this.ᜀ(this.ᜂ);
				this.ᜃ = A_0;
				this.ᜃ.BooleanValue = Convert.ToBoolean(value);
				(this.ᜃ as spr\u1A8B).ᜀ();
				num = 1;
				continue;
			}
			case 4:
				goto IL_13A;
			case 5:
				num = 0;
				continue;
			case 6:
				num = 2;
				continue;
			case 7:
				if (A_0 != this.ᜃ)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				return;
			}
			if (A_0 != null)
			{
				num = 5;
				continue;
			}
			IL_4A:
			num = 7;
		}
		IL_7A:
		throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("笶倸唺嘼娾╀B⁄⭆╈歊⁌㩎≐❒畔㕖㱘筚⹜㙞འѢ।ɦ䥨ࡪ࡬ͮᵰ嵲", a_));
		IL_111:
		return;
		IL_13A:
		goto IL_7A;
	}

	// Token: 0x06002CCF RID: 11471 RVA: 0x00193778 File Offset: 0x00192778
	public bool ᜈ()
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
		return this.ᜄ;
	}

	// Token: 0x06002CD0 RID: 11472 RVA: 0x001937BC File Offset: 0x001927BC
	public void ᜀ(bool A_0)
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
		this.ᜄ = A_0;
	}

	// Token: 0x06002CD1 RID: 11473 RVA: 0x00193800 File Offset: 0x00192800
	private string ᜀ(CheckState A_0)
	{
		int a_ = 6;
		for (;;)
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					switch (A_0)
					{
					case CheckState.Unchecked:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_63;
						}
						break;
					case CheckState.Checked:
						goto IL_80;
					case CheckState.Mixed:
						goto IL_9C;
					}
					num = 0;
					continue;
				case 2:
					goto IL_9A;
				}
				break;
			}
		}
		IL_63:
		if (true)
		{
		}
		if (false)
		{
		}
		return RecordTableEnumerator.b("稻缽ిᅁŃ", a_);
		IL_80:
		return RecordTableEnumerator.b("栻氽ᔿ݁", a_);
		IL_9A:
		return null;
		IL_9C:
		return RecordTableEnumerator.b("Ἳ瀽漿́", a_);
	}

	// Token: 0x040014A9 RID: 5289
	public new const int ᜀ = 201;

	// Token: 0x040014AA RID: 5290
	private new const int ᜁ = 2;

	// Token: 0x040014AB RID: 5291
	private new CheckState ᜂ;

	// Token: 0x040014AC RID: 5292
	private new IXLSRange ᜃ;

	// Token: 0x040014AD RID: 5293
	private new bool ᜄ;
}
