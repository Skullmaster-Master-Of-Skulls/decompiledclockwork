using System;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Interfaces
{
	// Token: 0x02000010 RID: 16
	public interface IShapeGetter : ICloneable
	{
		// Token: 0x060000EF RID: 239
		ShapeCollectionBase GetShapes(XlsWorksheetBase sheet);
	}
}
