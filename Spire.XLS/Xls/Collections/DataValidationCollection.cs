using System;
using System.Collections.Generic;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x02000034 RID: 52
	public class DataValidationCollection : XlsDataValidationCollection
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0002112C File Offset: 0x0002012C
		internal DataValidationCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00021144 File Offset: 0x00020144
		internal DataValidationCollection(spr\u2158 A_0, object A_1, spr\u22CB A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0002115C File Offset: 0x0002015C
		internal DataValidationCollection(spr\u2158 A_0, object A_1, List<BiffRecordRaw> A_2, ref int A_3) : base(A_0, A_1, A_2, ref A_3)
		{
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00021174 File Offset: 0x00020174
		public void Add(Validation dv)
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
			base.Add(dv.Wrapped);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000211BC File Offset: 0x000201BC
		public void Remove(Validation dv)
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
			base.Remove(dv.Wrapped);
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00021204 File Offset: 0x00020204
		public new Workbook Workbook
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
				return base.Workbook.InnerWorkBook;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0002124C File Offset: 0x0002024C
		public new Worksheet Worksheet
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
				return (Worksheet)base.Worksheet;
			}
		}
	}
}
