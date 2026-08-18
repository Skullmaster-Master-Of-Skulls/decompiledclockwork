using System;
using System.ComponentModel.Design;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200004C RID: 76
	public class ChartBaseSeriesEditor<T> : CollectionEditor
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00008CF6 File Offset: 0x00006EF6
		public ChartBaseSeriesEditor(Type type) : base(type)
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00008D00 File Offset: 0x00006F00
		protected override Type[] CreateNewItemTypes()
		{
			return new Type[]
			{
				typeof(T)
			};
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00008D22 File Offset: 0x00006F22
		protected override bool CanSelectMultipleInstances()
		{
			return false;
		}
	}
}
