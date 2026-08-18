using System;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x0200001D RID: 29
	public interface IIdentityValidator<in T>
	{
		// Token: 0x0600004A RID: 74
		Task<IdentityResult> ValidateAsync(T item);
	}
}
