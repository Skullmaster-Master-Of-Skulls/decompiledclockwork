using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x0200005A RID: 90
	public class XlsComment : TextBoxShapeBase, ICommentShape
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x00058124 File Offset: 0x00057124
		internal XlsComment(spr\u1DF5 A_0, object A_1) : this(A_0, A_1, true)
		{
			this.InitializeVariables();
			base.ShapeType = ExcelShapeType.Comment;
			this.m_bUpdateLineFill = true;
			base.Fill.ForeColor = XlsShapeFill.DEF_COMENT_PARSE_COLOR;
			base.Line.ForeColor = spr\u1D39.ᜀ;
			base.Fill.BackColor = spr\u1D39.ᜂ;
			base.Line.BackColor = spr\u1D39.ᜂ;
			base.Fill.Transparency = 1.0;
			this.ᜋ = this.m_shapes.Worksheet.Workbook.Author;
			base.IsMoveWithCell = false;
			base.IsSizeWithCell = false;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000581CC File Offset: 0x000571CC
		internal XlsComment(spr\u1DF5 A_0, object A_1, bool A_2) : base(A_0, A_1)
		{
			this.InitializeVariables();
			base.ShapeType = ExcelShapeType.Comment;
			if (A_2)
			{
				this.m_bUpdateLineFill = true;
				base.Fill.ForeColor = XlsShapeFill.DEF_COMENT_PARSE_COLOR;
				base.Line.ForeColor = spr\u1D39.ᜀ;
				base.Fill.BackColor = spr\u1D39.ᜂ;
				base.Line.BackColor = spr\u1D39.ᜂ;
			}
			this.ᜋ = this.m_shapes.Worksheet.Workbook.Author;
			base.IsMoveWithCell = false;
			base.IsSizeWithCell = false;
			base.FillColor = Color.FromArgb(255, 255, 255, 225);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00058284 File Offset: 0x00057284
		internal XlsComment(spr\u1DF5 A_0, object A_1, string A_2) : this(A_0, A_1)
		{
			base.RichText.Text = A_2;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000582A8 File Offset: 0x000572A8
		internal XlsComment(spr\u1DF5 A_0, object A_1, sprὙ A_2) : this(A_0, A_1, A_2, ExcelParseOptions.Default)
		{
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000582C0 File Offset: 0x000572C0
		internal XlsComment(spr\u1DF5 A_0, object A_1, sprὙ A_2, ExcelParseOptions A_3) : base(A_0, A_1, A_2, A_3)
		{
			base.ShapeType = ExcelShapeType.Comment;
			this.m_bSupportOptions = true;
			this.m_bUpdateLineFill = true;
			List<spr\u1D3B> list = A_2.ᜀ();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (list[i] is spr᪙)
				{
					spr\u2003 spr_u = (list[i] as spr᪙).ᜁ();
					spr\u2223 spr_u2 = spr_u.ᜃ()[0] as spr\u2223;
					this.ᜀ((int)spr_u2.ᜈ());
					return;
				}
				i++;
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00058350 File Offset: 0x00057350
		protected override void InitializeVariables()
		{
			for (;;)
			{
				base.InitializeVariables();
				base.VmlShape = true;
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜀ();
						num = 0;
						continue;
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
							if (!base.Worksheet.IsParsed)
							{
								return;
							}
							break;
						}
						num = 1;
						continue;
					}
					break;
				}
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000583DC File Offset: 0x000573DC
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
			base.ClientAnchor.ᜀ(3);
			base.ClientAnchor.ᜇ(this.Column - 1);
			base.ClientAnchor.ᜂ(this.Column + 1);
			base.ClientAnchor.ᜆ(this.Row - 1);
			base.ClientAnchor.ᜅ(this.Row + 3);
			base.ClientAnchor.ᜀ(240);
			base.ClientAnchor.ᜃ(240);
			base.ClientAnchor.ᜁ(240);
			base.ClientAnchor.ᜄ(240);
			base.UpdateWidth();
			base.UpdateHeight();
			base.EvaluateTopLeftPosition();
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x000584C4 File Offset: 0x000574C4
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x00058508 File Offset: 0x00057508
		public int Row
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
				return this.ᜈ + 1;
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
				this.ᜈ = value - 1;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0005854C File Offset: 0x0005754C
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x00058590 File Offset: 0x00057590
		public int Column
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
				return this.ᜉ + 1;
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
				this.ᜉ = value - 1;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x000585D4 File Offset: 0x000575D4
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00058618 File Offset: 0x00057618
		public bool IsVisible
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

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0005865C File Offset: 0x0005765C
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x000586A0 File Offset: 0x000576A0
		public string Author
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
				this.ᜋ = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x000586E4 File Offset: 0x000576E4
		public override int Instance
		{
			get
			{
				if (true)
				{
				}
				if (this.ᜏ != null)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return 202;
					}
					if (false)
					{
					}
					return this.ᜏ.\u1714();
				}
				return 202;
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0005873C File Offset: 0x0005773C
		internal override void RegisterInSubCollection()
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
			this.m_shapes.WorksheetBase.InnerComments.ᜁ(this);
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00058790 File Offset: 0x00057790
		public override IShape Clone(object parent, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes, bool addToCollections)
		{
			XlsComment xlsComment;
			for (;;)
			{
				xlsComment = (XlsComment)base.Clone(parent, hashNewNames, dicFontIndexes, addToCollections);
				xlsComment.IsVisible = this.IsVisible;
				xlsComment.CopyFrom(this, hashNewNames, dicFontIndexes);
				xlsComment.CopyCommentOptions(this, dicFontIndexes);
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return xlsComment;
					case 1:
						xlsComment.Worksheet.InnerComments.ᜁ(xlsComment);
						num = 0;
						continue;
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
							if (!addToCollections)
							{
								return xlsComment;
							}
							break;
						}
						num = 1;
						continue;
					}
					break;
				}
			}
			return xlsComment;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00058848 File Offset: 0x00057848
		internal override void SerializeShape(spr\u21EB spgrContainer)
		{
			int a_ = 10;
			switch (0)
			{
			default:
			{
				int num = 4;
				spr\u2223 spr_u;
				spr᪙ spr᪙;
				sprὙ sprὙ;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9E;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17D;
						default:
							if (false)
							{
							}
							if (base.Obj == null)
							{
								num = 9;
								continue;
							}
							spr_u = (base.Obj.ᜃ()[0] as spr\u2223);
							spr᪙.ᜀ(base.Obj);
							num = 6;
							continue;
						}
						break;
					case 2:
					{
						spr\u23E7 spr_u23E;
						if (spr_u23E.ᜀ().Length > 0)
						{
							num = 7;
							continue;
						}
						goto IL_215;
					}
					case 3:
						goto IL_1B0;
					case 5:
						goto IL_1A2;
					case 6:
						goto IL_9E;
					case 7:
					{
						spr\u23E7 spr_u23E;
						sprὙ.ᜀ(spr_u23E);
						num = 5;
						continue;
					}
					case 8:
					{
						if (true)
						{
						}
						spr_u.ᜁ((base.OldObjId > 0U) ? ((ushort)base.OldObjId) : ((ushort)base.ParentWorkbook.CurrentObjectId));
						sprὙ.ᜀ(this.ᜏ);
						spr\u23E7 spr_u23E = this.SerializeOptions(sprὙ);
						num = 2;
						continue;
					}
					case 9:
					{
						spr\u2003 spr_u2 = (spr\u2003)spr\u175E.ᜀ(TBIFFRecord.OBJ);
						spr_u = new spr\u2223();
						spr_u.ᜀ(TObjType.otComment);
						spr_u.ᜀ(true);
						spr_u.ᜂ(true);
						spr_u.ᜃ(true);
						sprទ a_2 = new sprទ();
						spr_u2.ᜀ(spr_u);
						spr_u2.ᜀ(a_2);
						spr᪙.ᜀ(spr_u2);
						goto IL_17D;
					}
					case 10:
						num = 3;
						continue;
					}
					if (spgrContainer == null)
					{
						num = 10;
						continue;
					}
					sprὙ = (sprὙ)spr\u231F.ᜀ(MsoRecords.msofbtSpContainer);
					spr᪙ = (spr᪙)spr\u231F.ᜀ(MsoRecords.msofbtClientData);
					spr_u = null;
					num = 1;
					continue;
					IL_9E:
					num = 8;
					continue;
					IL_17D:
					num = 0;
				}
				IL_1A2:
				goto IL_215;
				IL_1B0:
				throw new ArgumentNullException(RecordTableEnumerator.b("㌿㉁⍃㑅େ╉≋㩍ㅏ㭑㩓㍕⩗", a_));
				IL_215:
				sprὙ.ᜀ(base.ClientAnchor);
				sprὙ.ᜀ(spr᪙);
				sprὙ.ᜀ(base.ᜁ(sprὙ));
				spgrContainer.ᜀ(sprὙ);
				this.ᜀ(spr_u.ᜈ());
				return;
			}
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00058AA0 File Offset: 0x00057AA0
		private void ᜀ(ushort A_0)
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
			spr\u2114 spr_u = (spr\u2114)spr\u175E.ᜀ(TBIFFRecord.Note);
			spr_u.ᜁ((ushort)this.ᜈ);
			spr_u.ᜂ((ushort)this.ᜉ);
			spr_u.ᜀ(this.Author);
			spr_u.ᜀ(this.IsVisible);
			spr_u.ᜀ(A_0);
			this.m_shapes.Worksheet.ᜀ(spr_u);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00058B34 File Offset: 0x00057B34
		internal void ᜀ(spr\u23E7 A_0)
		{
			int a_ = 19;
			if (true)
			{
			}
			if (A_0 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("♈㭊㥌♎㹐㵒♔", a_));
				}
			}
			spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
			ᜀ.ᜀ(MsoOptions.TextId);
			ᜀ.ᜀ(19990000U);
			ᜀ.ᜀ(false);
			ᜀ.ᜁ(false);
			A_0.ᜁ(ᜀ);
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00058BC4 File Offset: 0x00057BC4
		internal new void ᜁ(spr\u23E7 A_0)
		{
			int a_ = 18;
			if (A_0 == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("❇㩉㡋❍㽏㱑❓", a_));
				}
			}
			spr\u23E7.ᜀ ᜀ = new spr\u23E7.ᜀ();
			ᜀ.ᜀ((MsoOptions)344);
			ᜀ.ᜀ(0U);
			ᜀ.ᜀ(false);
			ᜀ.ᜁ(false);
			A_0.ᜁ(ᜀ);
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00058C50 File Offset: 0x00057C50
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

		// Token: 0x06000899 RID: 2201 RVA: 0x00058CA4 File Offset: 0x00057CA4
		[CLSCompliant(false)]
		internal override void SerializeCommentShadow(spr\u23E7 option)
		{
			int a_ = 12;
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_D3;
				case 1:
					if (!this.IsVisible)
					{
						num = 5;
						continue;
					}
					num = 0;
					continue;
				case 2:
					goto IL_44;
				case 4:
					goto IL_4E;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_74;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				if (option == null)
				{
					num = 2;
					continue;
				}
				XlsShape.ᜀ(option, MsoOptions.ShadowObscured, 196611);
				XlsShape.ᜀ(option, MsoOptions.ForeShadowColor, 0);
				IL_74:
				num = 1;
			}
			IL_44:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⵁ㑃㉅ⅇ╉≋", a_));
			IL_4E:
			int num2 = 131074;
			goto IL_DA;
			IL_D3:
			num2 = 131072;
			IL_DA:
			int a_2 = num2;
			XlsShape.ᜀ(option, MsoOptions.CommentShowAlways, a_2);
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00058D98 File Offset: 0x00057D98
		internal override bool CanCopyShapesOnRangeCopy(Rectangle sourceRec, Rectangle destRec, out Rectangle newPosition)
		{
			int row;
			int column;
			for (;;)
			{
				if (true)
				{
				}
				newPosition = new Rectangle(0, 0, 0, 0);
				row = this.Row;
				column = this.Column;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (row <= sourceRec.Bottom)
						{
							num = 3;
							continue;
						}
						return false;
					case 1:
						if (row >= sourceRec.Top)
						{
							num = 7;
							continue;
						}
						return false;
					case 2:
						num = 4;
						continue;
					case 3:
						num = 6;
						continue;
					case 4:
						if (column > sourceRec.Right)
						{
							num = 5;
							continue;
						}
						goto IL_F7;
					case 5:
						return false;
					case 6:
						if (column < sourceRec.Left)
						{
							return false;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return false;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 7:
						num = 0;
						continue;
					}
					break;
				}
			}
			return false;
			IL_F7:
			newPosition.Y = row - sourceRec.Top + destRec.Top;
			newPosition.X = column - sourceRec.Left + destRec.Left;
			newPosition.Width = base.Width;
			newPosition.Height = base.Height;
			return true;
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00058EE4 File Offset: 0x00057EE4
		public override XlsShape CopyMoveShape(XlsWorksheet sheet, Rectangle destRec, bool bIsCopy)
		{
			int a_ = 10;
			int num = 3;
			XlsComment xlsComment;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_132;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_132;
					default:
					{
						if (true)
						{
						}
						if (false)
						{
						}
						XlsWorksheet worksheet;
						worksheet.InnerComments.Remove(this);
						num = 2;
						continue;
					}
					}
					break;
				case 2:
					goto IL_FC;
				case 4:
					goto IL_4D;
				case 5:
				{
					if (sheet.InnerComments[destRec.Y, destRec.X] != null)
					{
						num = 0;
						continue;
					}
					XlsWorksheet worksheet = base.ParentShapes.Worksheet;
					IXLSRange ixlsrange = sheet[destRec.Y, destRec.X];
					xlsComment = (XlsComment)ixlsrange.AddComment();
					xlsComment.CopyCommentOptions(this, null);
					xlsComment.IsVisible = this.IsVisible;
					num = 6;
					continue;
				}
				case 6:
					if (!bIsCopy)
					{
						num = 1;
						continue;
					}
					goto IL_137;
				}
				if (sheet == null)
				{
					num = 4;
				}
				else
				{
					num = 5;
				}
			}
			IL_4D:
			throw new ArgumentNullException(RecordTableEnumerator.b("㌿⩁⅃⍅㱇", a_));
			IL_FC:
			goto IL_137;
			IL_132:
			return null;
			IL_137:
			xlsComment.ᜀ();
			xlsComment.Height = base.Height;
			xlsComment.Width = base.Width;
			xlsComment.UpdateRightColumn();
			xlsComment.UpdateBottomRow();
			return xlsComment;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00059054 File Offset: 0x00058054
		protected override void UpdateNotSizeNotMoveShape(bool bRow, int index, int iCount)
		{
			switch (0)
			{
			default:
			{
				int num = 21;
				for (;;)
				{
					int num3;
					int num2;
					int num5;
					int num4;
					switch (num)
					{
					case 0:
						num2 = num3;
						goto IL_293;
					case 1:
						if (!bRow)
						{
							num = 29;
							continue;
						}
						goto IL_1AC;
					case 2:
						num4 = num5;
						goto IL_307;
					case 3:
						if (index > this.ᜈ)
						{
							num = 19;
							continue;
						}
						goto IL_17B;
					case 4:
						if (index - iCount >= this.ᜉ)
						{
							num = 15;
							continue;
						}
						goto IL_1AC;
					case 5:
						num = 2;
						continue;
					case 6:
						if (!bRow)
						{
							num = 22;
							continue;
						}
						num = 0;
						continue;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2DC;
						default:
							goto IL_3F6;
						}
						break;
					case 8:
						goto IL_2DC;
					case 9:
						if (index <= this.ᜈ)
						{
							num = 14;
							continue;
						}
						goto IL_2DC;
					case 10:
						num = 9;
						continue;
					case 11:
						if (index <= this.ᜉ)
						{
							num = 13;
							continue;
						}
						return;
					case 12:
						goto IL_2D7;
					case 13:
						goto IL_17B;
					case 14:
						num = 20;
						continue;
					case 15:
						goto IL_11A;
					case 16:
						if (iCount < 0)
						{
							num = 36;
							continue;
						}
						goto IL_1AC;
					case 17:
						num = 11;
						continue;
					case 18:
						num4 = num5 + iCount;
						goto IL_307;
					case 19:
						goto IL_11F;
					case 20:
						if (index - iCount < this.ᜈ)
						{
							num = 8;
							continue;
						}
						goto IL_1CF;
					case 22:
						num = 25;
						continue;
					case 23:
					{
						if (bRow)
						{
							num = 12;
							continue;
						}
						sprᮋ sprᮋ = base.ClientAnchor;
						sprᮋ.ᜇ(sprᮋ.ᜃ() + iCount);
						num = 31;
						continue;
					}
					case 24:
						if (bRow)
						{
							num = 26;
							continue;
						}
						goto IL_11F;
					case 25:
						num2 = num3 + iCount;
						goto IL_293;
					case 26:
						num = 3;
						continue;
					case 27:
						if (index <= this.ᜉ)
						{
							num = 35;
							continue;
						}
						goto IL_1AC;
					case 28:
						if (!bRow)
						{
							num = 5;
							continue;
						}
						num = 18;
						continue;
					case 29:
						num = 27;
						continue;
					case 30:
						return;
					case 31:
						if (iCount == 1)
						{
							num = 32;
							continue;
						}
						base.UpdateRightColumn();
						num = 7;
						continue;
					case 32:
						goto IL_280;
					case 33:
						if (!bRow)
						{
							num = 17;
							continue;
						}
						return;
					case 34:
						if (bRow)
						{
							num = 10;
							continue;
						}
						goto IL_2DC;
					case 35:
						num = 4;
						continue;
					case 36:
						num = 34;
						continue;
					}
					if (index == 0)
					{
						num = 30;
						continue;
					}
					index--;
					num = 16;
					continue;
					IL_11F:
					num = 33;
					continue;
					IL_17B:
					num5 = this.ᜈ;
					num3 = this.ᜉ;
					num = 28;
					continue;
					IL_1AC:
					num = 24;
					continue;
					IL_293:
					int num6 = num2;
					int num7;
					this.ᜈ = num7;
					this.ᜉ = num6;
					spr\u1D9B spr_u1D9B = (spr\u1D9B)this.m_shapes;
					spr_u1D9B.ᜌ().ReRegisterOnAccess = true;
					num = 23;
					continue;
					IL_2DC:
					num = 1;
					continue;
					IL_307:
					num7 = num4;
					num = 6;
				}
				return;
				IL_11A:
				IL_1CF:
				base.Remove();
				return;
				IL_280:
				base.UpdateRightColumn(iCount);
				return;
				IL_2D7:
				if (true)
				{
				}
				sprᮋ sprᮋ2 = base.ClientAnchor;
				sprᮋ2.ᜆ(sprᮋ2.ᜉ() + iCount);
				base.UpdateBottomRow();
				return;
				IL_3F6:
				if (false)
				{
				}
				return;
			}
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00059460 File Offset: 0x00058460
		protected override void OnDelete()
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
			base.OnDelete();
			this.m_shapes.WorksheetBase.InnerComments.ᜀ(this);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x000594B8 File Offset: 0x000584B8
		protected override void CreateDefaultFillLineFormats()
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
			this.m_bSupportOptions = true;
			base.CreateDefaultFillLineFormats();
			base.Fill.ForeColor = XlsShapeFill.DEF_COMENT_PARSE_COLOR;
			base.Line.ForeColor = spr\u1D39.ᜀ;
			base.Fill.BackColor = spr\u1D39.ᜂ;
			base.Line.BackColor = spr\u1D39.ᜂ;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00059540 File Offset: 0x00058540
		public void CopyCommentOptions(XlsComment sourceComment, Dictionary<int, int> dicFontIndexes)
		{
			int a_ = 15;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					base.ᜀ(sourceComment, dicFontIndexes);
					num = 4;
					continue;
				case 2:
					goto IL_38;
				case 3:
					if (!this.m_bUpdateLineFill)
					{
						goto IL_C5;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4C;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 4:
					goto IL_4A;
				}
				if (sourceComment == null)
				{
					num = 2;
					continue;
				}
				IL_4C:
				RichTextString richTextString = (RichTextString)base.RichText;
				RichTextString source = (RichTextString)sourceComment.RichText;
				richTextString.CopyFrom(source, dicFontIndexes);
				this.Name = sourceComment.Name;
				num = 3;
			}
			IL_38:
			throw new ArgumentNullException(RecordTableEnumerator.b("㙄⡆㱈㥊⹌⩎ቐ㱒㡔㩖㱘㕚⥜", a_));
			IL_4A:
			IL_C5:
			if (true)
			{
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00059624 File Offset: 0x00058624
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

		// Token: 0x060008A1 RID: 2209 RVA: 0x000596A8 File Offset: 0x000586A8
		private void ᜀ(int A_0)
		{
			spr\u2114 spr_u = (base.Worksheet as XlsWorksheet).ᜌ(A_0);
			if (spr_u == null)
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
					return;
				}
			}
			this.Author = spr_u.ᜆ();
			this.ᜈ = (int)spr_u.ᜀ();
			this.ᜉ = (int)spr_u.ᜁ();
			this.IsVisible = spr_u.ᜅ();
		}

		// Token: 0x0400018B RID: 395
		internal new const int ᜀ = 202;

		// Token: 0x0400018C RID: 396
		private long[] \u2460\u008D\u0090\u008C;

		// Token: 0x0400018D RID: 397
		private new const int ᜁ = 2;

		// Token: 0x0400018E RID: 398
		private new const int ᜂ = 3;

		// Token: 0x0400018F RID: 399
		private new const int ᜃ = 10;

		// Token: 0x04000190 RID: 400
		internal new const int ᜄ = 240;

		// Token: 0x04000191 RID: 401
		private bool \u25D8\u00A5\u00AF\u008B;

		// Token: 0x04000192 RID: 402
		private new const int ᜅ = 196611;

		// Token: 0x04000193 RID: 403
		private new const int ᜆ = 131072;

		// Token: 0x04000194 RID: 404
		private long \u2593\u0093\u00A3\u0090;

		// Token: 0x04000195 RID: 405
		private new const int ᜇ = 131074;

		// Token: 0x04000196 RID: 406
		private int ᜈ;

		// Token: 0x04000197 RID: 407
		private int ᜉ;

		// Token: 0x04000198 RID: 408
		private string \u2593\u0089\u0092\u00A3;

		// Token: 0x04000199 RID: 409
		private bool ᜊ;

		// Token: 0x0400019A RID: 410
		private string ᜋ;
	}
}
