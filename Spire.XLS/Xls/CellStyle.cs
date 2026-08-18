using System;
using Spire.Xls.Collections;
using Spire.Xls.Core;

namespace Spire.Xls
{
	// Token: 0x020000FF RID: 255
	public class CellStyle : CellStyleObject
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x00071D68 File Offset: 0x00070D68
		internal CellStyle(IStyle A_0) : base(A_0)
		{
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x00071D7C File Offset: 0x00070D7C
		public new BordersCollection Borders
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
				return new BordersCollection(base.Borders);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x00071DC4 File Offset: 0x00070DC4
		public new ExcelFont Font
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
				return new ExcelFont(base.Font);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x00071E0C File Offset: 0x00070E0C
		public new ExcelInterior Interior
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
				return new ExcelInterior(base.Interior);
			}
		}
	}
}
