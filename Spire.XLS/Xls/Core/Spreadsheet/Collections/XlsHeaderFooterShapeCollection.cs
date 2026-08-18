using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Parser.Biff_Records.ObjRecords;
using Spire.Xls.Core.Spreadsheet.Shapes;

namespace Spire.Xls.Core.Spreadsheet.Collections
{
	// Token: 0x02000027 RID: 39
	public class XlsHeaderFooterShapeCollection : ShapeCollectionBase
	{
		// Token: 0x060002AA RID: 682 RVA: 0x00017F54 File Offset: 0x00016F54
		internal XlsHeaderFooterShapeCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00017F6C File Offset: 0x00016F6C
		internal XlsHeaderFooterShapeCollection(spr\u1DF5 A_0, object A_1, spr\u21EB A_2, ExcelParseOptions A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00017F84 File Offset: 0x00016F84
		internal override TBIFFRecord RecordCode
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
				return TBIFFRecord.HeaderFooterImage;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00017FC4 File Offset: 0x00016FC4
		internal override XlsShape CreateShape(TObjType objType, sprὙ shapeContainer, ExcelParseOptions options, List<spr\u25AD> subRecords, int cmoIndex)
		{
			XlsShape result;
			for (;;)
			{
				result = null;
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						if (objType == TObjType.otPicture)
						{
							num = 2;
							continue;
						}
						return result;
					case 2:
						result = new ExcelPicture((spr\u2158)base.ReservedHandle, this, shapeContainer);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0001804C File Offset: 0x0001704C
		internal override void CreateData(Stream stream, spr\u20A0 dgContainer, List<int> arrBreaks, List<List<BiffRecordRaw>> arrRecords)
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
			stream.Write(spr\u1976.ᜁ, 0, spr\u1976.ᜁ.Length);
			base.CreateData(stream, dgContainer, arrBreaks, arrRecords);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000180A8 File Offset: 0x000170A8
		internal override XlsShape AddShape(sprὙ shapeContainer, ExcelParseOptions options)
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
			shapeContainer.ᜀ();
			XlsShape newXlsShape = this.CreateShape(TObjType.otPicture, shapeContainer, options, null, -1);
			return base.AddShape(newXlsShape);
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00018100 File Offset: 0x00017100
		public override XlsWorkbookShapeData ShapeData
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
				return base.Workbook.HeaderFooterData;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00018148 File Offset: 0x00017148
		protected override void RegisterInWorksheet()
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
			base.WorksheetBase.InnerHeaderFooterShapes = this;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00018190 File Offset: 0x00017190
		internal new void ᜀ(spr\u1976 A_0, ExcelParseOptions A_1)
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
			base.ᜀ(A_0.ᜃ(), A_1);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000181D8 File Offset: 0x000171D8
		public XlsShape SetPicture(string strShapeName, Image image)
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
			return this.SetPicture(strShapeName, image, -1);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0001821C File Offset: 0x0001721C
		public XlsShape SetPicture(string strShapeName, Image image, int iIndex)
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
			return this.SetPicture(strShapeName, image, iIndex, true);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00018264 File Offset: 0x00017264
		public XlsShape SetPicture(string strShapeName, Image image, int iIndex, bool bIncludeOptions)
		{
			int a_ = 19;
			switch (0)
			{
			default:
			{
				int num = 10;
				XlsShape result;
				for (;;)
				{
					XlsBitmapShape xlsBitmapShape;
					int num2;
					switch (num)
					{
					case 0:
					{
						bool flag;
						if (flag)
						{
							num = 11;
							continue;
						}
						return result;
					}
					case 1:
						goto IL_2BE;
					case 2:
						xlsBitmapShape = new ExcelPicture((spr\u2158)base.ReservedHandle, this, bIncludeOptions);
						num = 8;
						continue;
					case 3:
						num2 = iIndex;
						goto IL_E4;
					case 4:
						return result;
					case 5:
					{
						XlsWorkbookShapeData shapeData;
						num2 = shapeData.AddPicture(image, ImageFormatType.Original, strShapeName);
						goto IL_E4;
					}
					case 6:
						num = 19;
						continue;
					case 7:
						if (image != null)
						{
							num = 6;
							continue;
						}
						num = 0;
						continue;
					case 8:
						goto IL_243;
					case 9:
						goto IL_88;
					case 11:
						base.Remove(xlsBitmapShape);
						num = 4;
						continue;
					case 12:
						goto IL_193;
					case 13:
						goto IL_16E;
					case 14:
						if (xlsBitmapShape != null)
						{
							num = 15;
							continue;
						}
						goto IL_2BE;
					case 15:
					{
						uint blipId = xlsBitmapShape.BlipId;
						XlsWorkbookShapeData shapeData;
						shapeData.RemovePicture(blipId, true);
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_262;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					case 16:
						goto IL_262;
					case 17:
						if (iIndex == -1)
						{
							num = 16;
							continue;
						}
						num = 3;
						continue;
					case 18:
					{
						if (strShapeName.Length == 0)
						{
							num = 12;
							continue;
						}
						result = null;
						xlsBitmapShape = (base[strShapeName] as XlsBitmapShape);
						bool flag = xlsBitmapShape != null;
						XlsWorkbookShapeData shapeData = this.ShapeData;
						num = 14;
						continue;
					}
					case 19:
					{
						bool flag;
						if (!flag)
						{
							num = 2;
							continue;
						}
						goto IL_243;
					}
					}
					if (strShapeName == null)
					{
						num = 9;
						continue;
					}
					num = 18;
					continue;
					IL_E4:
					int blipId2 = num2;
					xlsBitmapShape.BlipId = (uint)blipId2;
					xlsBitmapShape.SetName(strShapeName);
					xlsBitmapShape.IsShortVersion = true;
					double num3 = spr\u17FF.ᜁ(1.0, MeasureUnits.Inch);
					xlsBitmapShape.ClientAnchor.ᜆ((int)Math.Round((double)image.Height * num3 / (double)image.VerticalResolution));
					xlsBitmapShape.ClientAnchor.ᜇ((int)Math.Round((double)image.Width * num3 / (double)image.HorizontalResolution));
					xlsBitmapShape.VmlShape = true;
					result = base.AddShape(xlsBitmapShape);
					num = 13;
					continue;
					IL_243:
					num = 17;
					continue;
					IL_262:
					num = 5;
					continue;
					IL_2BE:
					num = 7;
				}
				IL_88:
				throw new ArgumentNullException(RecordTableEnumerator.b("㩈㽊㽌ᱎ㥐㉒╔㉖᝘㩚ぜ㩞", a_));
				IL_16E:
				return result;
				IL_193:
				throw new ArgumentException(RecordTableEnumerator.b("㩈㽊㽌ᱎ㥐㉒╔㉖᝘㩚ぜ㩞䅠乢䕤ᑦᵨᥪѬŮᙰ卲ᙴᙶ᝸ᕺቼ୾ꆀꞆﶌﮎ붒", a_));
			}
			}
		}
	}
}
