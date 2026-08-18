using System;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x0200001C RID: 28
	public class ChartsCollection : XlsChartsCollection
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00013D08 File Offset: 0x00012D08
		internal ChartsCollection(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x170000E7 RID: 231
		public Chart this[int index]
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
				return (Chart)base[index];
			}
		}

		// Token: 0x170000E8 RID: 232
		public Chart this[string name]
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
				return (Chart)base[name];
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00013DB0 File Offset: 0x00012DB0
		public new ChartSheet Add()
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return (ChartSheet)base.Add(new ChartSheet((spr\u2158)base.ReservedHandle, this)
			{
				Name = CollectionExtended<IChart>.GenerateDefaultName(base.List, RecordTableEnumerator.b("笷刹崻䰽㐿", a_))
			});
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00013E34 File Offset: 0x00012E34
		public new ChartSheet Add(string name)
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
			return (ChartSheet)base.Add(new ChartSheet((spr\u2158)base.ReservedHandle, this)
			{
				Name = name
			});
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00013E94 File Offset: 0x00012E94
		public ChartSheet Add(ChartSheet chart)
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
			return (ChartSheet)base.Add(chart);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00013EDC File Offset: 0x00012EDC
		public new ChartSheet Remove(string name)
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
			return (ChartSheet)base.Remove(name);
		}
	}
}
