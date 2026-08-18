using System;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x0200061C RID: 1564
	public class ShapesGetter : IShapeGetter
	{
		// Token: 0x06005ED8 RID: 24280 RVA: 0x003B3F30 File Offset: 0x003B2F30
		public ShapeCollectionBase GetShapes(XlsWorksheetBase sheet)
		{
			int a_ = 8;
			while (sheet == null)
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
					throw new ArgumentNullException(RecordTableEnumerator.b("䴽⠿❁⅃㉅", a_));
				}
			}
			return sheet.InnerShapesBase;
		}

		// Token: 0x06005ED9 RID: 24281 RVA: 0x003B3F94 File Offset: 0x003B2F94
		public object Clone()
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
			return base.MemberwiseClone();
		}
	}
}
