using System;
using System.Collections.Generic;
using Spire.Xls;
using Spire.Xls.Collections;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;
using Spire.Xls.Core.Spreadsheet.Shapes;

// Token: 0x02000465 RID: 1125
internal class spr\u22F9 : spr\u1D9B
{
	// Token: 0x060044A5 RID: 17573 RVA: 0x0028E6F0 File Offset: 0x0028D6F0
	internal spr\u22F9(spr\u2158 A_0, object A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x060044A6 RID: 17574 RVA: 0x0028E708 File Offset: 0x0028D708
	protected override void ᜀ()
	{
		for (;;)
		{
			base.ᜀ();
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_62;
				case 1:
					IL_3F:
					this.ᜆ = new CommentsCollection((spr\u2158)base.ReservedHandle, this);
					num = 0;
					continue;
				case 2:
					if (this.m_sheet is Worksheet)
					{
						num = 1;
						continue;
					}
					goto IL_62;
				}
				break;
				IL_62:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3F;
				default:
					goto IL_78;
				}
			}
		}
		IL_78:
		if (false)
		{
		}
	}

	// Token: 0x060044A7 RID: 17575 RVA: 0x0028E7A0 File Offset: 0x0028D7A0
	[CLSCompliant(false)]
	internal override XlsShape ᜀ(TObjType A_0, sprὙ A_1, ExcelParseOptions A_2, List<spr\u25AD> A_3, int A_4)
	{
		XlsShape xlsShape;
		for (;;)
		{
			xlsShape = null;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return xlsShape;
				case 1:
					return xlsShape;
				case 2:
					if (A_0 != TObjType.otComment)
					{
						num = 4;
						continue;
					}
					xlsShape = base.AppImplementation.ᜀ(this, A_1, A_2);
					this.ᜆ.ᜁ((ICommentShape)xlsShape);
					num = 1;
					continue;
				case 3:
					return xlsShape;
				case 4:
					goto IL_13B;
				case 5:
					switch (A_0)
					{
					case TObjType.otChart:
						xlsShape = new Chart((spr\u2158)base.ReservedHandle, this, A_1, A_2);
						if (true)
						{
						}
						num = 3;
						continue;
					case TObjType.otText:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_13B;
						default:
						{
							if (false)
							{
							}
							XlsTextBoxShape xlsTextBoxShape = new XlsTextBoxShape((spr\u2158)base.ReservedHandle, this, A_1, A_2);
							this.m_sheet.TypedTextBoxes.AddTextBox(xlsTextBoxShape);
							xlsShape = xlsTextBoxShape;
							num = 0;
							continue;
						}
						}
						break;
					case TObjType.otButton:
						return xlsShape;
					case TObjType.otPicture:
						xlsShape = new ExcelPicture((spr\u2158)base.ReservedHandle, this, A_1);
						(this.m_sheet.Pictures as XlsPicturesCollection).ᜀ(xlsShape as IPictureShape);
						num = 8;
						continue;
					default:
						num = 6;
						continue;
					}
					break;
				case 6:
					num = 2;
					continue;
				case 7:
					return xlsShape;
				case 8:
					return xlsShape;
				}
				break;
				IL_13B:
				num = 7;
			}
		}
		return xlsShape;
	}
}
