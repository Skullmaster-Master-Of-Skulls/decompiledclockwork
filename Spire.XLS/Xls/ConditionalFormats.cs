using System;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls
{
	// Token: 0x02000170 RID: 368
	public class ConditionalFormats : CondFormatCollectionWrapper
	{
		// Token: 0x060011A2 RID: 4514 RVA: 0x000AD7C4 File Offset: 0x000AC7C4
		internal ConditionalFormats(ICombinedRange A_0) : base(A_0)
		{
		}

		// Token: 0x17000637 RID: 1591
		public ConditionalFormatWrapper this[int index]
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
				return base[index] as ConditionalFormatWrapper;
			}
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000AD820 File Offset: 0x000AC820
		public new ConditionalFormatWrapper AddCondition()
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
			return base.AddCondition() as ConditionalFormatWrapper;
		}
	}
}
