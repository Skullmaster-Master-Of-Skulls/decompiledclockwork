using System;
using Spire.DataExport.Collections;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001B4 RID: 436
	public class ChartSeriesList : Collection
	{
		// Token: 0x06000C4B RID: 3147 RVA: 0x00080F94 File Offset: 0x0007FF94
		public ChartSeriesList(object Holder)
		{
			this.m_holder = Holder;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00080FB0 File Offset: 0x0007FFB0
		public ChartSeries Add(ChartSeries Item)
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
			base.Add(Item);
			return Item;
		}

		// Token: 0x17000137 RID: 311
		public ChartSeries this[int Index]
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
				return base[Index] as ChartSeries;
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
				base[Index] = value;
			}
		}
	}
}
