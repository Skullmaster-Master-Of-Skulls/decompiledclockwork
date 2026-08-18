using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000692 RID: 1682
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ViewStateAttribute : ValueProviderSourceAttribute
	{
		// Token: 0x17001748 RID: 5960
		// (get) Token: 0x06005126 RID: 20774 RVA: 0x0011787A File Offset: 0x00115A7A
		// (set) Token: 0x06005127 RID: 20775 RVA: 0x00117882 File Offset: 0x00115A82
		public string Key { get; private set; }

		// Token: 0x06005128 RID: 20776 RVA: 0x0011788B File Offset: 0x00115A8B
		public ViewStateAttribute() : this(null)
		{
		}

		// Token: 0x06005129 RID: 20777 RVA: 0x00117894 File Offset: 0x00115A94
		public ViewStateAttribute(string key)
		{
			this.Key = key;
		}

		// Token: 0x0600512A RID: 20778 RVA: 0x001178A3 File Offset: 0x00115AA3
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			return new ViewStateValueProvider(modelBindingExecutionContext);
		}

		// Token: 0x0600512B RID: 20779 RVA: 0x001178B9 File Offset: 0x00115AB9
		public override string GetModelName()
		{
			return this.Key;
		}
	}
}
