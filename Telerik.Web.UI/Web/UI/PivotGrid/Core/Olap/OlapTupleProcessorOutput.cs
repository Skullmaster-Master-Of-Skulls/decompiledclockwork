using System;
using System.Collections.Generic;
using Telerik.Web.UI.PivotGrid.Core.Engine;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D18 RID: 3352
	internal class OlapTupleProcessorOutput
	{
		// Token: 0x06007D05 RID: 32005 RVA: 0x001CAFF0 File Offset: 0x001C91F0
		public OlapTupleProcessorOutput()
		{
			this.RootGroup = PivotEngine.OlapGrandTotal();
			this.tuples = new List<ProcessedTuple>();
		}

		// Token: 0x170027E0 RID: 10208
		// (get) Token: 0x06007D06 RID: 32006 RVA: 0x001CB00E File Offset: 0x001C920E
		// (set) Token: 0x06007D07 RID: 32007 RVA: 0x001CB016 File Offset: 0x001C9216
		public Group RootGroup { get; set; }

		// Token: 0x170027E1 RID: 10209
		// (get) Token: 0x06007D08 RID: 32008 RVA: 0x001CB01F File Offset: 0x001C921F
		public List<ProcessedTuple> ProcessedTuples
		{
			get
			{
				return this.tuples;
			}
		}

		// Token: 0x06007D09 RID: 32009 RVA: 0x001CB027 File Offset: 0x001C9227
		public virtual void AddTuple(ProcessedTuple pair)
		{
			this.tuples.Add(pair);
		}

		// Token: 0x06007D0A RID: 32010 RVA: 0x001CB038 File Offset: 0x001C9238
		public virtual Group FindTupleGroup(int tupleIndex)
		{
			int num = this.tuples.BinarySearch(new ProcessedTuple
			{
				SourceTupleIndex = tupleIndex
			});
			if (num >= 0)
			{
				return this.tuples[num].Group;
			}
			return null;
		}

		// Token: 0x04002240 RID: 8768
		private List<ProcessedTuple> tuples;
	}
}
