using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Telerik.Web.Data
{
	// Token: 0x02001BA0 RID: 7072
	public class AggregateResultCollection : Collection<AggregateResult>
	{
		// Token: 0x1700538F RID: 21391
		public AggregateResult this[string functionName]
		{
			get
			{
				return this.FirstOrDefault((AggregateResult r) => r.FunctionName == functionName);
			}
		}
	}
}
