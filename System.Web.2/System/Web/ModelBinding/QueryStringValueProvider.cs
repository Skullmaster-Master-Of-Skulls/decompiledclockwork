using System;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x02000665 RID: 1637
	public sealed class QueryStringValueProvider : NameValueCollectionValueProvider
	{
		// Token: 0x06005040 RID: 20544 RVA: 0x00115457 File Offset: 0x00113657
		public QueryStringValueProvider(ModelBindingExecutionContext modelBindingExecutionContext) : this(modelBindingExecutionContext, modelBindingExecutionContext.HttpContext.Request.Unvalidated)
		{
		}

		// Token: 0x06005041 RID: 20545 RVA: 0x00115470 File Offset: 0x00113670
		internal QueryStringValueProvider(ModelBindingExecutionContext modelBindingExecutionContext, UnvalidatedRequestValuesBase unvalidatedValues) : base(modelBindingExecutionContext.HttpContext.Request.QueryString, unvalidatedValues.QueryString, CultureInfo.InvariantCulture)
		{
		}
	}
}
