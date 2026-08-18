using System;

namespace System.Web.Mvc
{
	// Token: 0x02000113 RID: 275
	public sealed class QueryStringValueProviderFactory : ValueProviderFactory
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x00013BF8 File Offset: 0x00011DF8
		public QueryStringValueProviderFactory() : this(null)
		{
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00013C18 File Offset: 0x00011E18
		internal QueryStringValueProviderFactory(UnvalidatedRequestValuesAccessor unvalidatedValuesAccessor)
		{
			UnvalidatedRequestValuesAccessor unvalidatedValuesAccessor2 = unvalidatedValuesAccessor;
			if (unvalidatedValuesAccessor == null)
			{
				unvalidatedValuesAccessor2 = ((ControllerContext cc) => new UnvalidatedRequestValuesWrapper(cc.HttpContext.Request.Unvalidated));
			}
			this._unvalidatedValuesAccessor = unvalidatedValuesAccessor2;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00013C48 File Offset: 0x00011E48
		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			return new QueryStringValueProvider(controllerContext, this._unvalidatedValuesAccessor(controllerContext));
		}

		// Token: 0x0400020D RID: 525
		private readonly UnvalidatedRequestValuesAccessor _unvalidatedValuesAccessor;
	}
}
