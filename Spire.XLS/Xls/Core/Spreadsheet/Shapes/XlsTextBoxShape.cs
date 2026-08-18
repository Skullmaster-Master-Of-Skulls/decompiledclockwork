using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x02000221 RID: 545
	public class XlsTextBoxShape : TextBoxShapeBase, ITextBoxShape
	{
		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x00126CB0 File Offset: 0x00125CB0
		// (set) Token: 0x060020B4 RID: 8372 RVA: 0x00126CF4 File Offset: 0x00125CF4
		public Rectangle Coordinates2007
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
				return this.ᜃ;
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
				this.ᜃ = value;
			}
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00126D38 File Offset: 0x00125D38
		internal XlsTextBoxShape(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			base.ShapeType = ExcelShapeType.TextBox;
			base.Fill.ForeColor = spr\u1D39.ᜁ;
			base.Line.ForeColor = spr\u1D39.ᜅ;
			base.Line.BackColor = spr\u1D39.ᜅ;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x00126D9C File Offset: 0x00125D9C
		[CLSCompliant(false)]
		internal XlsTextBoxShape(spr\u1DF5 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3) : base(A_0, A_1, A_2, A_3)
		{
			base.ShapeType = ExcelShapeType.TextBox;
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00126DD4 File Offset: 0x00125DD4
		private void ᜀ()
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
			base.ShapeType = ExcelShapeType.TextBox;
			this.m_bUpdateLineFill = true;
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00126E20 File Offset: 0x00125E20
		protected override void OnPrepareForSerialization()
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
			this.ᜏ = (sprἼ)spr\u231F.ᜀ(MsoRecords.msofbtSp);
			this.ᜏ.ᜉ(2);
			this.ᜏ.ᜈ(202);
			this.ᜏ.ᜆ(true);
			this.ᜏ.ᜇ(true);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x00126EA4 File Offset: 0x00125EA4
		[CLSCompliant(false)]
		internal override void SerializeShape(spr\u21EB spgrContainer)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 7;
				sprὙ sprὙ;
				for (;;)
				{
					spr᪙ spr᪙;
					switch (num)
					{
					case 0:
					{
						spr\u2003 spr_u = (spr\u2003)spr\u175E.ᜀ(TBIFFRecord.OBJ);
						spr\u2223 spr_u2 = new spr\u2223();
						spr_u2.ᜀ(TObjType.otText);
						spr_u2.ᜀ(true);
						spr_u2.ᜂ(true);
						spr_u2.ᜃ(true);
						sprទ a_2 = new sprទ();
						spr_u.ᜀ(spr_u2);
						spr_u.ᜀ(a_2);
						spr᪙.ᜀ(spr_u);
						num = 6;
						continue;
					}
					case 1:
					{
						spr\u2223 spr_u2;
						spr_u2.ᜁ((base.OldObjId > 0U) ? ((ushort)base.OldObjId) : ((ushort)base.ParentWorkbook.CurrentObjectId));
						sprὙ.ᜀ(this.ᜏ);
						spr\u23E7 spr_u23E = this.SerializeOptions(sprὙ);
						num = 5;
						continue;
					}
					case 2:
						goto IL_92;
					case 3:
						if (base.Text.Length > 0)
						{
							num = 11;
							continue;
						}
						goto IL_275;
					case 4:
						goto IL_1D9;
					case 5:
					{
						spr\u23E7 spr_u23E;
						if (spr_u23E.ᜀ().Length > 0)
						{
							num = 10;
							continue;
						}
						goto IL_1D9;
					}
					case 6:
						goto IL_97;
					case 8:
						goto IL_97;
					case 9:
					{
						if (base.Obj == null)
						{
							num = 0;
							continue;
						}
						spr\u2223 spr_u2 = base.Obj.ᜃ()[0] as spr\u2223;
						spr᪙.ᜀ(base.Obj);
						num = 8;
						continue;
					}
					case 10:
					{
						spr\u23E7 spr_u23E;
						sprὙ.ᜀ(spr_u23E);
						if (true)
						{
						}
						num = 4;
						continue;
					}
					case 11:
						sprὙ.ᜀ(base.ᜁ(sprὙ));
						num = 12;
						continue;
					case 12:
						goto IL_14D;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_97:
						num = 1;
						continue;
					default:
					{
						if (false)
						{
						}
						if (spgrContainer == null)
						{
							num = 2;
							continue;
						}
						sprὙ = (sprὙ)spr\u231F.ᜀ(MsoRecords.msofbtSpContainer);
						spr᪙ = (spr᪙)spr\u231F.ᜀ(MsoRecords.msofbtClientData);
						spr\u2223 spr_u2 = null;
						num = 9;
						continue;
					}
					}
					IL_1D9:
					sprὙ.ᜀ(base.ClientAnchor);
					sprὙ.ᜀ(spr᪙);
					num = 3;
				}
				IL_92:
				throw new ArgumentNullException(RecordTableEnumerator.b("㭇㩉⭋㱍ፏ㵑㩓≕㥗㍙㉛㭝቟", a_));
				IL_14D:
				IL_275:
				spgrContainer.ᜀ(sprὙ);
				return;
			}
			}
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00127130 File Offset: 0x00126130
		[CLSCompliant(false)]
		internal override spr\u23E7 CreateDefaultOptions()
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

		// Token: 0x060020BB RID: 8379 RVA: 0x00127184 File Offset: 0x00126184
		public override IShape Clone(object parent, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes, bool addToCollections)
		{
			XlsTextBoxShape xlsTextBoxShape;
			for (;;)
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_51:
					if (!addToCollections)
					{
						return xlsTextBoxShape;
					}
					num = 1;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					xlsTextBoxShape = (XlsTextBoxShape)base.Clone(parent, hashNewNames, dicFontIndexes, addToCollections);
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_51;
					case 1:
						(xlsTextBoxShape.Worksheet.TextBoxes as TextBoxCollection).AddTextBox(xlsTextBoxShape);
						num = 2;
						continue;
					case 2:
						return xlsTextBoxShape;
					}
					break;
				}
			}
			return xlsTextBoxShape;
		}

		// Token: 0x04001151 RID: 4433
		private new const int ᜀ = 202;

		// Token: 0x04001152 RID: 4434
		private float[] \u25D8\u0092\u009Dª;

		// Token: 0x04001153 RID: 4435
		private int[] \u2609\u0083\u0085\u0081;

		// Token: 0x04001154 RID: 4436
		private new const int ᜁ = 2;

		// Token: 0x04001155 RID: 4437
		private string[] \u2460\u00AE\u009E\u0092;

		// Token: 0x04001156 RID: 4438
		private bool \u2609\u00A4\u0089\u0097;

		// Token: 0x04001157 RID: 4439
		internal new const string ᜂ = "Forms.TextBox.1";

		// Token: 0x04001158 RID: 4440
		private float \u25D8\u0098\u008A\u0095;

		// Token: 0x04001159 RID: 4441
		private new Rectangle ᜃ = new Rectangle(0, 1, 2076450, 1557338);
	}
}
