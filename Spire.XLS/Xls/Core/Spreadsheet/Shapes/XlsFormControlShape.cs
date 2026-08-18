using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.MsoDrawing;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Shapes
{
	// Token: 0x0200021A RID: 538
	public class XlsFormControlShape : XlsShape, IShape
	{
		// Token: 0x06002036 RID: 8246 RVA: 0x00121AE0 File Offset: 0x00120AE0
		internal XlsFormControlShape(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
			this.ᜀ();
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00121B08 File Offset: 0x00120B08
		internal XlsFormControlShape(spr\u1DF5 A_0, object A_1, spr\u1D3B[] A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
			this.ᜀ();
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00121B34 File Offset: 0x00120B34
		internal XlsFormControlShape(spr\u1DF5 A_0, object A_1, sprὙ A_2) : base(A_0, A_1, A_2)
		{
			this.ᜀ();
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x00121B5C File Offset: 0x00120B5C
		internal XlsFormControlShape(spr\u1DF5 A_0, object A_1, spr\u1D3B A_2) : base(A_0, A_1, A_2, ExcelParseOptions.Default)
		{
			this.ᜀ();
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00121B84 File Offset: 0x00120B84
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
			base.ShapeType = ExcelShapeType.FormControl;
			base.ClientAnchor.ᜃ(true);
			this.ᜅ.ᜀ(ExcelComboType.AutoFilter);
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00121BE0 File Offset: 0x00120BE0
		public override IShape Clone(object parent, Dictionary<string, string> hashNewNames, Dictionary<int, int> dicFontIndexes, bool addToCollection)
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
			XlsFormControlShape xlsFormControlShape = (XlsFormControlShape)base.Clone(parent, hashNewNames, dicFontIndexes, addToCollection);
			xlsFormControlShape.ᜅ = (spr\u2471)this.ᜅ.ᜁ();
			return xlsFormControlShape;
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00121C44 File Offset: 0x00120C44
		protected override void OnPrepareForSerialization()
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
						if (false)
						{
						}
						this.ᜏ = (sprἼ)spr\u231F.ᜀ(MsoRecords.msofbtSp);
						this.ᜏ.ᜈ(201);
						this.ᜏ.ᜆ(true);
						this.ᜏ.ᜇ(true);
						if (true)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					return;
				}
				if (this.ᜏ != null)
				{
					break;
				}
				num = 1;
			}
		}

		// Token: 0x0600203D RID: 8253 RVA: 0x00121CFC File Offset: 0x00120CFC
		internal override void SerializeShape(spr\u21EB spgrContainer)
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
			sprὙ sprὙ = new sprὙ(spgrContainer);
			new sprᮋ(sprὙ);
			spr᪙ spr᪙ = new spr᪙(sprὙ);
			spr\u2003 spr_u = (spr\u2003)spr\u175E.ᜀ(TBIFFRecord.OBJ);
			spr\u2223 spr_u2 = new spr\u2223();
			spr_u2.ᜀ(TObjType.otComboBox);
			spr_u2.ᜀ(false);
			spr_u2.ᜂ(true);
			spr_u2.ᜄ(true);
			spr_u2.ᜃ(false);
			spr_u2.ᜁ(true);
			spr_u2.ᜁ((base.OldObjId > 0U) ? ((ushort)base.OldObjId) : ((ushort)base.ParentWorkbook.CurrentObjectId));
			XlsFormControlShape.ᜄ.CopyTo(spr_u2.ᜇ(), 0);
			sprᢛ a_ = new sprᢛ();
			sprទ a_2 = new sprទ();
			spr_u.ᜀ(spr_u2);
			spr_u.ᜀ(a_);
			spr_u.ᜀ(this.ᜅ);
			spr_u.ᜀ(a_2);
			spr᪙.ᜀ(spr_u);
			spr\u23E7 a_3 = this.SerializeOptions(this.ᜏ);
			sprὙ.ᜀ(this.ᜏ);
			sprὙ.ᜀ(a_3);
			base.ClientAnchor.ᜀ(1);
			sprὙ.ᜀ(base.ClientAnchor);
			sprὙ.ᜀ(spr᪙);
			spgrContainer.ᜀ(sprὙ);
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00121E44 File Offset: 0x00120E44
		internal override spr\u23E7 SerializeOptions(spr\u1D3B parent)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					goto IL_D7;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_D7;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (this.\u1712 == null)
						{
							num = 1;
							continue;
						}
						goto IL_DC;
					}
					break;
				}
				if (this.m_bUpdateLineFill)
				{
					break;
				}
				num = 0;
			}
			IL_32:
			spr\u23E7 spr_u23E = base.SerializeOptions(parent);
			base.ᜀ(spr_u23E, MsoOptions.LockAgainstGrouping, 17039620);
			base.ᜀ(spr_u23E, MsoOptions.SizeTextToFitShape, 524296);
			base.ᜀ(spr_u23E, MsoOptions.NoLineDrawDash, 524288);
			base.ᜀ(spr_u23E, MsoOptions.CommentShowAlways, 131072);
			spr_u23E.ᜉ(3);
			spr_u23E.ᜈ(2);
			return spr_u23E;
			IL_D7:
			goto IL_32;
			IL_DC:
			return this.\u1712;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x00121F34 File Offset: 0x00120F34
		internal override void ParseClientData(spr᪙ clientData, ExcelParseOptions options)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_D8:
					num = 3;
					break;
				default:
					if (false)
					{
					}
					num = 6;
					break;
				}
				for (;;)
				{
					int num2;
					int count;
					List<spr\u25AD> list;
					switch (num)
					{
					case 0:
						goto IL_78;
					case 1:
						goto IL_8F;
					case 2:
					{
						spr\u25AD spr_u25AD;
						if (spr_u25AD.ᜏ() == TObjSubRecordType.ftLbsData)
						{
							num = 7;
							continue;
						}
						goto IL_82;
					}
					case 3:
					{
						if (num2 >= count)
						{
							num = 8;
							continue;
						}
						spr\u25AD spr_u25AD = list[num2];
						num = 2;
						continue;
					}
					case 4:
						goto IL_82;
					case 5:
						goto IL_11F;
					case 7:
					{
						spr\u25AD spr_u25AD;
						this.ᜅ = (spr\u2471)spr_u25AD;
						num = 4;
						continue;
					}
					case 8:
						return;
					}
					if (clientData == null)
					{
						num = 0;
						continue;
					}
					base.ParseClientData(clientData, options);
					list = base.Obj.ᜃ();
					num2 = 0;
					count = list.Count;
					num = 5;
					continue;
					IL_82:
					num2++;
					num = 1;
				}
				IL_78:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("⁂⥄⹆ⱈ╊㥌୎ぐ❒㑔", a_));
				IL_8F:
				IL_11F:
				goto IL_D8;
			}
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x00122080 File Offset: 0x00121080
		// (set) Token: 0x06002041 RID: 8257 RVA: 0x001220C8 File Offset: 0x001210C8
		public bool IsArrowSelectedColor
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
				return this.ᜅ.ᜀ();
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
				this.ᜅ.ᜀ(value);
			}
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x00122110 File Offset: 0x00121110
		// Note: this type is marked as 'beforefieldinit'.
		static XlsFormControlShape()
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
			XlsFormControlShape.ᜄ = new byte[]
			{
				0,
				0,
				0,
				0,
				108,
				25,
				42,
				1,
				0,
				0,
				0,
				0
			};
		}

		// Token: 0x04001125 RID: 4389
		private new const int ᜀ = 17039620;

		// Token: 0x04001126 RID: 4390
		private long[] \u2609\u009E\u0086ª;

		// Token: 0x04001127 RID: 4391
		internal new const int ᜁ = 524296;

		// Token: 0x04001128 RID: 4392
		private new const int ᜂ = 524288;

		// Token: 0x04001129 RID: 4393
		private bool \u2593\u009E\u0097\u009B;

		// Token: 0x0400112A RID: 4394
		private new const int ᜃ = 131072;

		// Token: 0x0400112B RID: 4395
		private new static readonly byte[] ᜄ;

		// Token: 0x0400112C RID: 4396
		private new spr\u2471 ᜅ = new spr\u2471();
	}
}
