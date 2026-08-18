using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000691 RID: 1681
	public sealed class UserProfileValueProvider : SimpleValueProvider
	{
		// Token: 0x06005124 RID: 20772 RVA: 0x0011712A File Offset: 0x0011532A
		public UserProfileValueProvider(ModelBindingExecutionContext modelBindingExecutionContext) : base(modelBindingExecutionContext)
		{
		}

		// Token: 0x06005125 RID: 20773 RVA: 0x00117868 File Offset: 0x00115A68
		protected override object FetchValue(string key)
		{
			return base.ModelBindingExecutionContext.HttpContext.Profile;
		}
	}
}
