using System;

namespace Telerik.Web.Data
{
	// Token: 0x02001B9A RID: 7066
	internal class PredicateFilterDescription : FilterDescription
	{
		// Token: 0x060111A6 RID: 70054 RVA: 0x003C5D3D File Offset: 0x003C3F3D
		public PredicateFilterDescription(Delegate predicate)
		{
			this.predicate = predicate;
		}

		// Token: 0x060111A7 RID: 70055 RVA: 0x003C5D4C File Offset: 0x003C3F4C
		public override bool SatisfiesFilter(object dataItem)
		{
			return (bool)this.predicate.DynamicInvoke(new object[]
			{
				dataItem
			});
		}

		// Token: 0x04004C97 RID: 19607
		private readonly Delegate predicate;
	}
}
