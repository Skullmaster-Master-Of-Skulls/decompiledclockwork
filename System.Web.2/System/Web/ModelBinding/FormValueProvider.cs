using System;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x0200064F RID: 1615
	public sealed class FormValueProvider : NameValueCollectionValueProvider
	{
		// Token: 0x06004F99 RID: 20377 RVA: 0x00114831 File Offset: 0x00112A31
		public FormValueProvider(ModelBindingExecutionContext modelBindingExecutionContext) : this(modelBindingExecutionContext, modelBindingExecutionContext.HttpContext.Request.Unvalidated)
		{
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x0011484A File Offset: 0x00112A4A
		internal FormValueProvider(ModelBindingExecutionContext modelBindingExecutionContext, UnvalidatedRequestValuesBase unvalidatedValues) : base(modelBindingExecutionContext.HttpContext.Request.Form, unvalidatedValues.Form, CultureInfo.CurrentCulture)
		{
		}
	}
}
