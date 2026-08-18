using System;

namespace System.Web.Mvc
{
	// Token: 0x0200003C RID: 60
	public sealed class JQueryFormValueProviderFactory : ValueProviderFactory
	{
		// Token: 0x06000123 RID: 291 RVA: 0x0000563B File Offset: 0x0000383B
		public JQueryFormValueProviderFactory() : this(null)
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000565B File Offset: 0x0000385B
		internal JQueryFormValueProviderFactory(UnvalidatedRequestValuesAccessor unvalidatedValuesAccessor)
		{
			UnvalidatedRequestValuesAccessor unvalidatedValuesAccessor2 = unvalidatedValuesAccessor;
			if (unvalidatedValuesAccessor == null)
			{
				unvalidatedValuesAccessor2 = ((ControllerContext cc) => new UnvalidatedRequestValuesWrapper(cc.HttpContext.Request.Unvalidated));
			}
			this._unvalidatedValuesAccessor = unvalidatedValuesAccessor2;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000568B File Offset: 0x0000388B
		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			return new JQueryFormValueProvider(controllerContext, this._unvalidatedValuesAccessor(controllerContext));
		}

		// Token: 0x0400004E RID: 78
		private readonly UnvalidatedRequestValuesAccessor _unvalidatedValuesAccessor;
	}
}
