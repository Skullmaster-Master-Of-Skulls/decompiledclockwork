using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000684 RID: 1668
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class RouteDataAttribute : ValueProviderSourceAttribute
	{
		// Token: 0x17001740 RID: 5952
		// (get) Token: 0x060050EE RID: 20718 RVA: 0x00117170 File Offset: 0x00115370
		// (set) Token: 0x060050EF RID: 20719 RVA: 0x00117178 File Offset: 0x00115378
		public string Key { get; private set; }

		// Token: 0x060050F0 RID: 20720 RVA: 0x00117181 File Offset: 0x00115381
		public RouteDataAttribute() : this(null)
		{
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x0011718A File Offset: 0x0011538A
		public RouteDataAttribute(string key)
		{
			this.Key = key;
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x00117199 File Offset: 0x00115399
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			return new RouteDataValueProvider(modelBindingExecutionContext);
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x001171AF File Offset: 0x001153AF
		public override string GetModelName()
		{
			return this.Key;
		}
	}
}
