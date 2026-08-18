using System;
using System.Web.Http.ModelBinding;

namespace System.Web.Http
{
	// Token: 0x02000124 RID: 292
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class HttpBindRequiredAttribute : HttpBindingBehaviorAttribute
	{
		// Token: 0x0600070E RID: 1806 RVA: 0x00017467 File Offset: 0x00015667
		public HttpBindRequiredAttribute() : base(HttpBindingBehavior.Required)
		{
		}
	}
}
