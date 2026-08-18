using System;
using System.Web.Http.ModelBinding;

namespace System.Web.Http
{
	// Token: 0x02000123 RID: 291
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class HttpBindNeverAttribute : HttpBindingBehaviorAttribute
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x0001745E File Offset: 0x0001565E
		public HttpBindNeverAttribute() : base(HttpBindingBehavior.Never)
		{
		}
	}
}
