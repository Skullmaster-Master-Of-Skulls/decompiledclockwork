using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.PivotTables
{
	// Token: 0x02000233 RID: 563
	public class PivotDataFields : CollectionExtended<PivotDataField>, IPivotDataFields
	{
		// Token: 0x17000C61 RID: 3169
		IPivotDataField IPivotDataFields.this[int index]
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
				return base.List[index];
			}
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x001328F0 File Offset: 0x001318F0
		internal PivotDataFields(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600224A RID: 8778 RVA: 0x00132908 File Offset: 0x00131908
		public IPivotDataField Add(IPivotField field, string name, SubtotalTypes subtotal)
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
			PivotDataField pivotDataField = new PivotDataField(name, subtotal, field as XlsPivotField);
			base.Add(pivotDataField);
			return pivotDataField;
		}
	}
}
