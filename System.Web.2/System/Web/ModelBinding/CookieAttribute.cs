using System;

namespace System.Web.ModelBinding
{
	// Token: 0x0200063B RID: 1595
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class CookieAttribute : ValueProviderSourceAttribute, IUnvalidatedValueProviderSource, IValueProviderSource
	{
		// Token: 0x170016DB RID: 5851
		// (get) Token: 0x06004F14 RID: 20244 RVA: 0x00112F8F File Offset: 0x0011118F
		// (set) Token: 0x06004F15 RID: 20245 RVA: 0x00112F97 File Offset: 0x00111197
		public string Name { get; private set; }

		// Token: 0x06004F16 RID: 20246 RVA: 0x00112FA0 File Offset: 0x001111A0
		public CookieAttribute() : this(null)
		{
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x00112FA9 File Offset: 0x001111A9
		public CookieAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x00112FBF File Offset: 0x001111BF
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			return new CookieValueProvider(modelBindingExecutionContext);
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x00112FD5 File Offset: 0x001111D5
		public override string GetModelName()
		{
			return this.Name;
		}

		// Token: 0x170016DC RID: 5852
		// (get) Token: 0x06004F1A RID: 20250 RVA: 0x00112FDD File Offset: 0x001111DD
		// (set) Token: 0x06004F1B RID: 20251 RVA: 0x00112FE5 File Offset: 0x001111E5
		public bool ValidateInput
		{
			get
			{
				return this._validateInput;
			}
			set
			{
				this._validateInput = value;
			}
		}

		// Token: 0x04002A63 RID: 10851
		private bool _validateInput = true;
	}
}
