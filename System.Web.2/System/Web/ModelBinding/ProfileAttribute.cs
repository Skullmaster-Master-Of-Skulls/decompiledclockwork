using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000682 RID: 1666
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ProfileAttribute : ValueProviderSourceAttribute
	{
		// Token: 0x1700173F RID: 5951
		// (get) Token: 0x060050E6 RID: 20710 RVA: 0x001170E3 File Offset: 0x001152E3
		// (set) Token: 0x060050E7 RID: 20711 RVA: 0x001170EB File Offset: 0x001152EB
		public string Key { get; private set; }

		// Token: 0x060050E8 RID: 20712 RVA: 0x001170F4 File Offset: 0x001152F4
		public ProfileAttribute() : this(null)
		{
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x001170FD File Offset: 0x001152FD
		public ProfileAttribute(string key)
		{
			this.Key = key;
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x0011710C File Offset: 0x0011530C
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			return new ProfileValueProvider(modelBindingExecutionContext);
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x00117122 File Offset: 0x00115322
		public override string GetModelName()
		{
			return this.Key;
		}
	}
}
