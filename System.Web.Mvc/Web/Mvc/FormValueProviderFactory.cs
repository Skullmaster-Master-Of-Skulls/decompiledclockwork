using System;

namespace System.Web.Mvc
{
	// Token: 0x0200010F RID: 271
	public sealed class FormValueProviderFactory : ValueProviderFactory
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x000139E6 File Offset: 0x00011BE6
		public FormValueProviderFactory() : this(null)
		{
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00013A06 File Offset: 0x00011C06
		internal FormValueProviderFactory(UnvalidatedRequestValuesAccessor unvalidatedValuesAccessor)
		{
			UnvalidatedRequestValuesAccessor unvalidatedValuesAccessor2 = unvalidatedValuesAccessor;
			if (unvalidatedValuesAccessor == null)
			{
				unvalidatedValuesAccessor2 = ((ControllerContext cc) => new UnvalidatedRequestValuesWrapper(cc.HttpContext.Request.Unvalidated));
			}
			this._unvalidatedValuesAccessor = unvalidatedValuesAccessor2;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x00013A36 File Offset: 0x00011C36
		public override IValueProvider GetValueProvider(ControllerContext controllerContext)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			return new FormValueProvider(controllerContext, this._unvalidatedValuesAccessor(controllerContext));
		}

		// Token: 0x04000206 RID: 518
		private readonly UnvalidatedRequestValuesAccessor _unvalidatedValuesAccessor;
	}
}
