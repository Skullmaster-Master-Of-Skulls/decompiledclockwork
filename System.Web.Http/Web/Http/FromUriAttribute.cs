using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;
using System.Web.Http.ValueProviders;

namespace System.Web.Http
{
	// Token: 0x02000198 RID: 408
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
	public sealed class FromUriAttribute : ModelBinderAttribute
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x000230F8 File Offset: 0x000212F8
		public override IEnumerable<ValueProviderFactory> GetValueProviderFactories(HttpConfiguration configuration)
		{
			if (configuration == null)
			{
				throw Error.ArgumentNull("configuration");
			}
			foreach (ValueProviderFactory f in base.GetValueProviderFactories(configuration))
			{
				if (f is IUriValueProviderFactory)
				{
					yield return f;
				}
			}
			yield break;
		}
	}
}
