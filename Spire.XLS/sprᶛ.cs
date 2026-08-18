using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls;
using Spire.Xls.Collections;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x020003F0 RID: 1008
internal class spr\u1D9B : ShapeCollectionBase, IShapes
{
	// Token: 0x06003C86 RID: 15494 RVA: 0x0021D44C File Offset: 0x0021C44C
	internal spr\u1D9B(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06003C87 RID: 15495 RVA: 0x0021D464 File Offset: 0x0021C464
	internal spr\u1D9B(spr\u1DF5 A_0, object A_1, spr\u21EB A_2, ExcelParseOptions A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x06003C88 RID: 15496 RVA: 0x0021D47C File Offset: 0x0021C47C
	protected virtual void ᜀ()
	{
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				base.InitializeCollection();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.ᜆ = new CommentsCollection((spr\u2158)base.ReservedHandle, this);
						num = 1;
						continue;
					case 1:
						return;
					case 2:
						if (this.m_sheet is XlsWorksheet)
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
				}
				break;
			}
			}
		}
	}

	// Token: 0x06003C89 RID: 15497 RVA: 0x0021D514 File Offset: 0x0021C514
	public IComments ᜅ()
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

	// Token: 0x06003C8A RID: 15498 RVA: 0x0021D558 File Offset: 0x0021C558
	internal XlsCommentsCollection ᜌ()
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
		return this.ᜆ;
	}

	// Token: 0x06003C8B RID: 15499 RVA: 0x0021D59C File Offset: 0x0021C59C
	internal virtual TBIFFRecord ᜁ()
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
		return TBIFFRecord.MSODrawing;
	}

	// Token: 0x06003C8C RID: 15500 RVA: 0x0021D5DC File Offset: 0x0021C5DC
	public virtual XlsWorkbookShapeData ᜂ()
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
		return base.Workbook.ShapesData;
	}

	// Token: 0x06003C8D RID: 15501 RVA: 0x0021D624 File Offset: 0x0021C624
	public new IPictureShape ᜀ(Image A_0, string A_1, ImageFormatType A_2)
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
		int a_ = this.ShapeData.AddPicture(A_0, A_2, A_1);
		IPictureShape pictureShape = this.ᜀ(a_, A_1);
		pictureShape.Height = (int)Math.Round((double)A_0.Height * spr\u17FF.ᜁ(1.0, MeasureUnits.Inch) / (double)A_0.VerticalResolution);
		pictureShape.Width = (int)Math.Round((double)A_0.Width * spr\u17FF.ᜁ(1.0, MeasureUnits.Inch) / (double)A_0.HorizontalResolution);
		pictureShape.Name = A_1;
		return pictureShape;
	}

	// Token: 0x06003C8E RID: 15502 RVA: 0x0021D6D8 File Offset: 0x0021C6D8
	public new IPictureShape ᜀ(string A_0)
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
		Image a_ = Image.FromFile(A_0);
		return this.ᜀ(a_, Path.GetFileNameWithoutExtension(A_0), ImageFormatType.Original);
	}

	// Token: 0x06003C8F RID: 15503 RVA: 0x0021D728 File Offset: 0x0021C728
	public new ICommentShape ᜁ(string A_0)
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
		return this.ᜀ(A_0, true);
	}

	// Token: 0x06003C90 RID: 15504 RVA: 0x0021D76C File Offset: 0x0021C76C
	public new ICommentShape ᜀ(string A_0, bool A_1)
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
		XlsComment xlsComment = base.AppImplementation.ᜀ(this, A_1);
		xlsComment.RichText.Text = A_0;
		return base.AddShape(xlsComment) as ICommentShape;
	}

	// Token: 0x06003C91 RID: 15505 RVA: 0x0021D7D0 File Offset: 0x0021C7D0
	public ICommentShape ᜋ()
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
		return this.ᜁ(string.Empty);
	}

	// Token: 0x06003C92 RID: 15506 RVA: 0x0021D818 File Offset: 0x0021C818
	public IChartShape ᜊ()
	{
		int a_ = 3;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		Chart chart = new Chart((spr\u2158)base.ReservedHandle, this);
		chart.Name = CollectionExtended<IShape>.GenerateDefaultName(base.List, RecordTableEnumerator.b("稸区尼䴾㕀捂", a_));
		base.AddShape(chart);
		return chart;
	}

	// Token: 0x06003C93 RID: 15507 RVA: 0x0021D898 File Offset: 0x0021C898
	public ITextBoxShape ᜉ()
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		XlsTextBoxShape xlsTextBoxShape = base.AppImplementation.ᜀ(this);
		base.AddShape(xlsTextBoxShape);
		this.m_sheet.TypedTextBoxes.AddTextBox(xlsTextBoxShape);
		xlsTextBoxShape.Name = CollectionExtended<IShape>.GenerateDefaultName(this, RecordTableEnumerator.b("ᅄ≆ㅈ㽊ཌ⁎⥐獒", a_));
		return xlsTextBoxShape;
	}

	// Token: 0x06003C94 RID: 15508 RVA: 0x0021D920 File Offset: 0x0021C920
	public ICheckBoxShape ᜇ()
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		sprថ sprថ = base.AppImplementation.ᜉ(this);
		base.AddShape(sprថ);
		this.m_sheet.TypedCheckBoxes.AddCheckBox(sprថ);
		sprថ.Name = CollectionExtended<IShape>.GenerateDefaultName(this, RecordTableEnumerator.b("笷刹夻崽⬿A⭃㹅桇", a_));
		return sprថ;
	}

	// Token: 0x06003C95 RID: 15509 RVA: 0x0021D9A8 File Offset: 0x0021C9A8
	public IRadioButton ᜃ()
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
		RadioButton radioButton = base.AppImplementation.ᜋ(this);
		base.AddShape(radioButton);
		this.m_sheet.TypedOptionButtons.AddRadioButton(radioButton);
		radioButton.Name = CollectionExtended<IShape>.GenerateDefaultName(this, RecordTableEnumerator.b("݇㩉㡋❍㽏㱑瑓ᑕⵗ⹙⡛ㅝ๟䉡", a_));
		return radioButton;
	}

	// Token: 0x06003C96 RID: 15510 RVA: 0x0021DA30 File Offset: 0x0021CA30
	public IComboBoxShape ᜄ()
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
		XlsComboBoxShape xlsComboBoxShape = base.AppImplementation.ᜊ(this);
		base.AddShape(xlsComboBoxShape);
		this.m_sheet.TypedComboBoxes.AddComboBox(xlsComboBoxShape);
		xlsComboBoxShape.Name = CollectionExtended<IShape>.GenerateDefaultName(this, RecordTableEnumerator.b("ൈ㥊≌㽎煐ᝒ㩔⁖㝘筚", a_));
		return xlsComboBoxShape;
	}

	// Token: 0x06003C97 RID: 15511 RVA: 0x0021DAB8 File Offset: 0x0021CAB8
	internal void ᜆ()
	{
		int a_ = 0;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_60:
				int num = 0;
				int count = base.Count;
				int num2 = 0;
				for (;;)
				{
					if (true)
					{
					}
					XlsComboBoxShape xlsComboBoxShape;
					XlsShape xlsShape;
					switch (num2)
					{
					case 0:
						goto IL_E2;
					case 1:
						if (num >= count)
						{
							num2 = 9;
							continue;
						}
						xlsComboBoxShape = (base[num] as XlsComboBoxShape);
						num2 = 13;
						continue;
					case 2:
						goto IL_161;
					case 3:
					{
						string name;
						if (name.Length == 0)
						{
							num2 = 2;
							continue;
						}
						goto IL_89;
					}
					case 4:
						if (xlsShape != null)
						{
							num2 = 7;
							continue;
						}
						goto IL_14F;
					case 5:
					{
						string name;
						if (name != null)
						{
							num2 = 8;
							continue;
						}
						goto IL_161;
					}
					case 6:
						goto IL_89;
					case 7:
						xlsShape.ᜐ();
						num2 = 12;
						continue;
					case 8:
						num2 = 3;
						continue;
					case 9:
						goto IL_FE;
					case 10:
					{
						string name = xlsComboBoxShape.Name;
						num2 = 5;
						continue;
					}
					case 11:
						goto IL_E2;
					case 12:
						goto IL_14F;
					case 13:
						if (xlsComboBoxShape != null)
						{
							num2 = 10;
							continue;
						}
						goto IL_89;
					}
					goto IL_60;
					IL_89:
					xlsShape = (base[num] as XlsComment);
					num2 = 4;
					continue;
					IL_E2:
					num2 = 1;
					continue;
					IL_14F:
					num++;
					num2 = 11;
					continue;
					IL_161:
					xlsComboBoxShape.Name = CollectionExtended<IShape>.GenerateDefaultName(this, RecordTableEnumerator.b("爵䨷唹䰻ḽпⵁ㍃⡅桇", a_));
					num2 = 6;
				}
				IL_FE:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_114;
				}
			}
			IL_114:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06003C98 RID: 15512 RVA: 0x0021DC80 File Offset: 0x0021CC80
	internal virtual XlsShape ᜀ(TObjType A_0, sprὙ A_1, ExcelParseOptions A_2, List<spr\u25AD> A_3, int A_4)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			XlsShape xlsShape;
			for (;;)
			{
				xlsShape = null;
				int num = 9;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return xlsShape;
					case 1:
						return xlsShape;
					case 2:
						xlsShape = new ExcelPicture((spr\u2158)base.ReservedHandle, this, A_1);
						(this.m_sheet.Pictures as XlsPicturesCollection).ᜀ(xlsShape as IPictureShape);
						num = 10;
						continue;
					case 3:
						goto IL_197;
					case 4:
					{
						string name;
						if (name != null)
						{
							num = 12;
							continue;
						}
						goto IL_1AB;
					}
					case 5:
					{
						if (true)
						{
						}
						string name;
						if (name.Length == 0)
						{
							num = 11;
							continue;
						}
						return xlsShape;
					}
					case 6:
						num = 3;
						continue;
					case 7:
						num = 18;
						continue;
					case 8:
						num = 16;
						continue;
					case 9:
						switch (A_0)
						{
						case TObjType.otChart:
						{
							xlsShape = new Chart((spr\u2158)base.ReservedHandle, this, A_1, A_2);
							string name = xlsShape.Name;
							num = 4;
							continue;
						}
						case TObjType.otText:
						{
							XlsTextBoxShape xlsTextBoxShape = new XlsTextBoxShape(base.AppImplementation, this, A_1, A_2);
							this.m_sheet.TypedTextBoxes.AddTextBox(xlsTextBoxShape);
							xlsShape = xlsTextBoxShape;
							num = 15;
							continue;
						}
						case TObjType.otButton:
						case TObjType.otPolygon:
						case TObjType.otReserved0:
							return xlsShape;
						case TObjType.otPicture:
							num = 14;
							continue;
						case TObjType.otCheckBox:
						{
							sprថ sprថ = new sprថ(base.AppImplementation, this, A_1, A_2);
							this.m_sheet.TypedCheckBoxes.AddCheckBox(sprថ);
							xlsShape = sprថ;
							num = 19;
							continue;
						}
						case TObjType.otOptionBtn:
						{
							RadioButton radioButton = new RadioButton(base.AppImplementation, this, A_1, A_2, A_4);
							this.m_sheet.TypedOptionButtons.AddRadioButton(radioButton);
							xlsShape = radioButton;
							num = 0;
							continue;
						}
						default:
							num = 7;
							continue;
						}
						break;
					case 10:
						return xlsShape;
					case 11:
						goto IL_1AB;
					case 12:
						num = 5;
						continue;
					case 13:
						return xlsShape;
					case 14:
						if (xlsShape == null)
						{
							num = 2;
							continue;
						}
						return xlsShape;
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_197;
						default:
							goto IL_FC;
						}
						break;
					case 16:
						return xlsShape;
					case 17:
						return xlsShape;
					case 18:
					{
						if (A_0 != TObjType.otComboBox)
						{
							num = 6;
							continue;
						}
						XlsComboBoxShape xlsComboBoxShape = new XlsComboBoxShape(base.AppImplementation, this, A_1, A_2, A_3);
						this.m_sheet.TypedComboBoxes.AddComboBox(xlsComboBoxShape);
						xlsShape = xlsComboBoxShape;
						num = 13;
						continue;
					}
					case 19:
						return xlsShape;
					}
					break;
					IL_197:
					if (A_0 != TObjType.otComment)
					{
						num = 8;
						continue;
					}
					xlsShape = base.AppImplementation.ᜀ(this, A_1, A_2);
					this.ᜆ.ᜁ((ICommentShape)xlsShape);
					num = 17;
					continue;
					IL_1AB:
					xlsShape.Name = CollectionExtended<IShape>.GenerateDefaultName(this, RecordTableEnumerator.b("縼圾⁀ㅂㅄ杆", a_));
					num = 1;
				}
			}
			IL_FC:
			if (false)
			{
			}
			return xlsShape;
		}
		}
	}

	// Token: 0x06003C99 RID: 15513 RVA: 0x0021DFC4 File Offset: 0x0021CFC4
	private new XlsShape ᜀ(sprὙ A_0, ExcelParseOptions A_1, List<spr\u25AD> A_2, int A_3)
	{
		int a_ = 2;
		int num = 0;
		switch (num)
		{
		default:
		{
			XlsShape result;
			for (;;)
			{
				IL_4C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_6C;
				default:
					goto IL_6C;
				}
				int num2;
				int count;
				for (;;)
				{
					IL_19:
					switch (num)
					{
					case 0:
					{
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						spr\u25AD spr_u25AD = A_2[num2];
						num = 1;
						continue;
					}
					case 1:
					{
						spr\u25AD spr_u25AD;
						if (spr_u25AD.ᜏ() == TObjSubRecordType.ftPictFmla)
						{
							num = 7;
							continue;
						}
						num2++;
						num = 4;
						continue;
					}
					case 2:
					{
						XlsTextBoxShape xlsTextBoxShape = new XlsTextBoxShape(base.AppImplementation, this, A_0, A_1);
						this.m_sheet.TypedTextBoxes.AddTextBox(xlsTextBoxShape);
						result = xlsTextBoxShape;
						num = 9;
						continue;
					}
					case 3:
						return result;
					case 4:
						if (true)
						{
						}
						goto IL_100;
					case 5:
					{
						spr\u2285 spr_u;
						string a;
						if ((a = spr_u.ᜀ()) != null)
						{
							num = 6;
							continue;
						}
						return result;
					}
					case 6:
						num = 10;
						continue;
					case 7:
					{
						spr\u25AD spr_u25AD;
						spr\u2285 spr_u = (spr\u2285)spr_u25AD;
						num = 5;
						continue;
					}
					case 8:
						goto IL_100;
					case 9:
						return result;
					case 10:
					{
						string a;
						if (a == RecordTableEnumerator.b("縷唹主匽㌿汁၃⍅ぇ㹉๋⅍⡏籑敓", a_))
						{
							num = 2;
							continue;
						}
						return result;
					}
					}
					goto IL_4C;
					IL_100:
					num = 0;
				}
				IL_6C:
				if (false)
				{
				}
				result = null;
				num2 = A_3;
				count = A_2.Count;
				num = 8;
				goto IL_19;
			}
			return result;
		}
		}
	}

	// Token: 0x06003C9A RID: 15514 RVA: 0x0021E154 File Offset: 0x0021D154
	public XlsFormControlShape ᜈ()
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
		XlsFormControlShape xlsFormControlShape = new XlsFormControlShape(base.ReservedHandle, this);
		base.AddShape(xlsFormControlShape);
		return xlsFormControlShape;
	}

	// Token: 0x06003C9B RID: 15515 RVA: 0x0021E1A8 File Offset: 0x0021D1A8
	protected internal new void ᜀ(ICommentShape A_0)
	{
		int a_ = 16;
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
			if (A_0 != null)
			{
				base.InnerList.Remove(A_0);
				return;
			}
			break;
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("╅❇❉⅋⭍㹏♑", a_));
	}

	// Token: 0x06003C9C RID: 15516 RVA: 0x0021E214 File Offset: 0x0021D214
	public new bool ᜀ(int A_0, int A_1, bool A_2, int A_3)
	{
		for (;;)
		{
			int num = base.Count - 1;
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return false;
					default:
					{
						if (false)
						{
						}
						if (num < 0)
						{
							num2 = 4;
							continue;
						}
						XlsShape xlsShape = (XlsShape)base.InnerList[num];
						num2 = 2;
						continue;
					}
					}
					break;
				case 1:
					goto IL_7C;
				case 2:
				{
					XlsShape xlsShape;
					if (!xlsShape.CanInsertRowColumn(A_0, A_1, A_2, A_3))
					{
						num2 = 1;
						continue;
					}
					num--;
					num2 = 3;
					continue;
				}
				case 3:
					goto IL_86;
				case 4:
					return true;
				case 5:
					goto IL_86;
				}
				break;
				IL_86:
				num2 = 0;
			}
		}
		return false;
		IL_7C:
		if (true)
		{
		}
		return false;
	}

	// Token: 0x06003C9D RID: 15517 RVA: 0x0021E2E0 File Offset: 0x0021D2E0
	public new void ᜀ(int A_0, int A_1, bool A_2, bool A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = base.Count - 1;
				int num2 = 8;
				for (;;)
				{
					XlsWorksheet worksheet;
					switch (num2)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9C;
						default:
						{
							if (false)
							{
							}
							XlsShape xlsShape;
							xlsShape.Remove(A_0, A_1, A_2);
							num2 = 11;
							continue;
						}
						}
						break;
					case 2:
					{
						XlsAutoFiltersCollection xlsAutoFiltersCollection;
						if (xlsAutoFiltersCollection != null)
						{
							num2 = 3;
							continue;
						}
						return;
					}
					case 3:
					{
						if (true)
						{
						}
						XlsAutoFiltersCollection xlsAutoFiltersCollection;
						xlsAutoFiltersCollection.ᜂ();
						num2 = 0;
						continue;
					}
					case 4:
						goto IL_83;
					case 5:
					{
						if (num < 0)
						{
							num2 = 6;
							continue;
						}
						XlsShape xlsShape = (XlsShape)base.InnerList[num];
						num2 = 12;
						continue;
					}
					case 6:
						goto IL_9C;
					case 7:
						goto IL_15F;
					case 8:
						goto IL_83;
					case 9:
					{
						XlsAutoFiltersCollection xlsAutoFiltersCollection = (XlsAutoFiltersCollection)worksheet.AutoFilters;
						num2 = 2;
						continue;
					}
					case 10:
						if (worksheet != null)
						{
							num2 = 9;
							continue;
						}
						return;
					case 11:
						goto IL_15F;
					case 12:
					{
						if (A_3)
						{
							num2 = 1;
							continue;
						}
						XlsShape xlsShape;
						xlsShape.InsertRowColumn(A_0, A_1, A_2);
						num2 = 7;
						continue;
					}
					}
					break;
					IL_83:
					num2 = 5;
					continue;
					IL_9C:
					worksheet = base.Worksheet;
					num2 = 10;
					continue;
					IL_15F:
					num--;
					num2 = 4;
				}
			}
			return;
		}
	}

	// Token: 0x06003C9E RID: 15518 RVA: 0x0021E478 File Offset: 0x0021D478
	public new XlsBitmapShape ᜀ(int A_0, string A_1)
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
		XlsBitmapShape xlsBitmapShape = new ExcelPicture((spr\u2158)base.ReservedHandle, this);
		xlsBitmapShape.FileName = A_1;
		xlsBitmapShape.ShapeType = ExcelShapeType.Picture;
		xlsBitmapShape.BlipId = (uint)A_0;
		base.Add(xlsBitmapShape);
		xlsBitmapShape.IsSizeWithCell = false;
		xlsBitmapShape.IsMoveWithCell = true;
		(this.m_sheet.Pictures as XlsPicturesCollection).ᜀ(xlsBitmapShape);
		return xlsBitmapShape;
	}

	// Token: 0x06003C9F RID: 15519 RVA: 0x0021E508 File Offset: 0x0021D508
	public new void ᜀ(XlsBitmapShape A_0)
	{
		int a_ = 14;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			if (A_0 != null)
			{
				base.Add(A_0);
				(this.m_sheet.Pictures as XlsPicturesCollection).ᜀ(A_0);
				return;
			}
			break;
		}
		if (true)
		{
		}
		throw new ArgumentNullException(RecordTableEnumerator.b("㝃⹅⥇㩉⥋", a_));
	}

	// Token: 0x06003CA0 RID: 15520 RVA: 0x0021E584 File Offset: 0x0021D584
	public new void ᜀ(int A_0, int A_1, Rectangle A_2, int A_3, Rectangle A_4)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IList<IShape> innerList = base.InnerList;
				int num = 0;
				int count = innerList.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_68;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							if (false)
							{
							}
							goto IL_68;
						}
						break;
					case 2:
						goto IL_7E;
					case 3:
					{
						if (num >= count)
						{
							num2 = 2;
							continue;
						}
						XlsShape xlsShape = (XlsShape)innerList[num];
						xlsShape.UpdateFormula(A_0, A_1, A_2, A_3, A_4);
						num++;
						goto IL_9E;
					}
					}
					break;
					IL_68:
					num2 = 3;
					continue;
					IL_9E:
					num2 = 0;
				}
			}
			IL_7E:
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x06003CA1 RID: 15521 RVA: 0x0021E648 File Offset: 0x0021D648
	public virtual object ᜀ(object A_0)
	{
		int a_ = 18;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_17:
				int num = 5;
				for (;;)
				{
					spr\u1D9B spr_u1D9B;
					int num2;
					int count;
					List<IShape> innerList;
					switch (num)
					{
					case 0:
						goto IL_66;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17;
						default:
							if (false)
							{
							}
							spr_u1D9B.ᜆ = new XlsCommentsCollection(base.ReservedHandle, this);
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_12F;
					case 3:
						goto IL_C3;
					case 4:
						goto IL_C3;
					case 6:
					{
						if (num2 >= count)
						{
							num = 7;
							continue;
						}
						XlsShape xlsShape = (XlsShape)innerList[num2];
						xlsShape.RegisterInSubCollection();
						num2++;
						num = 4;
						continue;
					}
					case 7:
						return spr_u1D9B;
					case 8:
						if (this.ᜆ != null)
						{
							num = 1;
							continue;
						}
						goto IL_12F;
					}
					if (A_0 == null)
					{
						num = 0;
						continue;
					}
					spr_u1D9B = (spr\u1D9B)base.Clone(A_0);
					num = 8;
					continue;
					IL_C3:
					num = 6;
					continue;
					IL_12F:
					if (true)
					{
					}
					innerList = spr_u1D9B.InnerList;
					num2 = 0;
					count = base.Count;
					num = 3;
				}
			}
			IL_66:
			throw new ArgumentNullException(RecordTableEnumerator.b("㡇⭉㹋⭍㹏♑", a_));
		}
	}

	// Token: 0x06003CA2 RID: 15522 RVA: 0x0021E7B0 File Offset: 0x0021D7B0
	public new void ᜀ(XlsWorksheet A_0, Rectangle A_1, Rectangle A_2, bool A_3)
	{
		int a_ = 14;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_17:
				int num = 5;
				for (;;)
				{
					int num2;
					IList<IShape> list;
					switch (num)
					{
					case 0:
					{
						bool flag;
						if (flag)
						{
							num = 6;
							continue;
						}
						goto IL_68;
					}
					case 1:
					{
						if (num2 < 0)
						{
							num = 7;
							continue;
						}
						XlsShape xlsShape = (XlsShape)list[num2];
						Rectangle destRec;
						bool flag = xlsShape.CanCopyShapesOnRangeCopy(A_1, A_2, out destRec);
						num = 0;
						continue;
					}
					case 2:
						goto IL_68;
					case 3:
						goto IL_C7;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17;
						default:
							if (false)
							{
							}
							goto IL_C7;
						}
						break;
					case 6:
					{
						XlsShape xlsShape;
						Rectangle destRec;
						xlsShape.CopyMoveShape(A_0, destRec, A_3);
						num = 2;
						continue;
					}
					case 7:
						return;
					case 8:
						goto IL_66;
					}
					if (A_0 == null)
					{
						num = 8;
						continue;
					}
					list = base.List;
					num2 = list.Count - 1;
					num = 4;
					continue;
					IL_68:
					if (true)
					{
					}
					num2--;
					num = 3;
					continue;
					IL_C7:
					num = 1;
				}
			}
			IL_66:
			throw new ArgumentNullException(RecordTableEnumerator.b("⁃⍅㭇㹉Ὃ♍㕏㝑⁓", a_));
		}
	}

	// Token: 0x06003CA3 RID: 15523 RVA: 0x0021E8F8 File Offset: 0x0021D8F8
	internal new void ᜀ(ExcelVersion A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num;
				int num2;
				UtilityMethods.ᜀ(out num, out num2, A_0);
				int num3 = base.Count - 1;
				int num4 = 2;
				for (;;)
				{
					XlsShape xlsShape;
					switch (num4)
					{
					case 0:
						goto IL_186;
					case 1:
						num4 = 17;
						continue;
					case 2:
						goto IL_167;
					case 3:
						num4 = 11;
						continue;
					case 4:
						num4 = 9;
						continue;
					case 5:
						num4 = 7;
						continue;
					case 6:
						if (xlsShape.Name != null)
						{
							num4 = 4;
							continue;
						}
						goto IL_80;
					case 7:
						if (xlsShape.TopRow <= num)
						{
							num4 = 1;
							continue;
						}
						goto IL_1D9;
					case 8:
						goto IL_1D9;
					case 9:
						if (xlsShape.Name.Length != 0)
						{
							goto IL_DC;
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
							num4 = 12;
							continue;
						}
						break;
					case 10:
						if (num3 < 0)
						{
							num4 = 0;
							continue;
						}
						xlsShape = (XlsShape)base[num3];
						num4 = 13;
						continue;
					case 11:
						if (xlsShape.RightColumn <= num2)
						{
							num4 = 5;
							continue;
						}
						goto IL_1D9;
					case 12:
						goto IL_80;
					case 13:
						if (xlsShape.LeftColumn <= num2)
						{
							num4 = 3;
							continue;
						}
						goto IL_1D9;
					case 14:
						goto IL_DC;
					case 15:
						goto IL_DC;
					case 16:
						goto IL_167;
					case 17:
						if (xlsShape.BottomRow > num)
						{
							num4 = 8;
							continue;
						}
						num4 = 6;
						continue;
					}
					break;
					IL_80:
					xlsShape.GenerateDefaultName();
					num4 = 15;
					continue;
					IL_DC:
					num3--;
					num4 = 16;
					continue;
					IL_167:
					num4 = 10;
					continue;
					IL_1D9:
					xlsShape.Remove();
					num4 = 14;
				}
			}
			IL_186:
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x06003CA4 RID: 15524 RVA: 0x0021EB00 File Offset: 0x0021DB00
	internal new void ᜀ(int[] A_0)
	{
		for (;;)
		{
			int num = 0;
			int count = base.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_2B;
				case 1:
					return;
				case 2:
					goto IL_2B;
				case 3:
				{
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					XlsShape xlsShape = base[num] as XlsShape;
					xlsShape.ᜀ(A_0);
					num++;
					if (true)
					{
					}
					num2 = 0;
					continue;
				}
				}
				break;
				IL_2B:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					num2 = 3;
					break;
				}
			}
		}
	}

	// Token: 0x06003CA5 RID: 15525 RVA: 0x0021EBA4 File Offset: 0x0021DBA4
	internal new void ᜀ(IDictionary<int, int> A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			int num = 0;
			int count = base.Count;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
				{
					if (num >= count)
					{
						num2 = 0;
						continue;
					}
					XlsShape xlsShape = base[num] as XlsShape;
					xlsShape.ᜀ(A_0);
					num++;
					num2 = 3;
					continue;
				}
				case 2:
					goto IL_33;
				case 3:
					goto IL_33;
				}
				break;
				IL_33:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				num2 = 1;
			}
		}
	}

	// Token: 0x06003CA6 RID: 15526 RVA: 0x0021EC48 File Offset: 0x0021DC48
	internal new XlsShape ᜀ(int A_0)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			XlsShape result;
			for (;;)
			{
				result = null;
				int num = 0;
				int count = base.Count;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						return result;
					case 1:
						goto IL_B6;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B6;
						default:
						{
							if (false)
							{
							}
							XlsShape xlsShape;
							if (xlsShape.ShapeId == A_0)
							{
								num2 = 4;
								continue;
							}
							num++;
							num2 = 5;
							continue;
						}
						}
						break;
					case 3:
					{
						if (num >= count)
						{
							num2 = 6;
							continue;
						}
						XlsShape xlsShape = (XlsShape)base[num];
						num2 = 2;
						continue;
					}
					case 4:
					{
						XlsShape xlsShape;
						result = xlsShape;
						num2 = 0;
						continue;
					}
					case 5:
						goto IL_B6;
					case 6:
						return result;
					}
					break;
					IL_B6:
					num2 = 3;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x04001A32 RID: 6706
	internal new const string ᜀ = "Chart ";

	// Token: 0x04001A33 RID: 6707
	internal new const string ᜁ = "TextBox ";

	// Token: 0x04001A34 RID: 6708
	internal new const string ᜂ = "Option Button ";

	// Token: 0x04001A35 RID: 6709
	internal const string ᜃ = "CheckBox ";

	// Token: 0x04001A36 RID: 6710
	internal const string ᜄ = "Drop Down ";

	// Token: 0x04001A37 RID: 6711
	internal const string ᜅ = "Picture ";

	// Token: 0x04001A38 RID: 6712
	protected XlsCommentsCollection ᜆ;
}
