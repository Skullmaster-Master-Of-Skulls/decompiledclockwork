using System;
using System.Web.UI;

namespace System.Web.ModelBinding
{
	// Token: 0x02000693 RID: 1683
	public sealed class ViewStateValueProvider : SimpleValueProvider
	{
		// Token: 0x0600512C RID: 20780 RVA: 0x0011712A File Offset: 0x0011532A
		public ViewStateValueProvider(ModelBindingExecutionContext modelBindingExecutionContext) : base(modelBindingExecutionContext)
		{
		}

		// Token: 0x0600512D RID: 20781 RVA: 0x001178C4 File Offset: 0x00115AC4
		protected override object FetchValue(string key)
		{
			StateBag service = base.ModelBindingExecutionContext.GetService<StateBag>();
			if (service != null)
			{
				return service[key];
			}
			return null;
		}
	}
}
