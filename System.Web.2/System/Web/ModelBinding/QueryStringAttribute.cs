using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000666 RID: 1638
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class QueryStringAttribute : ValueProviderSourceAttribute, IUnvalidatedValueProviderSource, IValueProviderSource
	{
		// Token: 0x17001729 RID: 5929
		// (get) Token: 0x06005042 RID: 20546 RVA: 0x00115493 File Offset: 0x00113693
		// (set) Token: 0x06005043 RID: 20547 RVA: 0x0011549B File Offset: 0x0011369B
		public string Key { get; private set; }

		// Token: 0x06005044 RID: 20548 RVA: 0x001154A4 File Offset: 0x001136A4
		public QueryStringAttribute() : this(null)
		{
		}

		// Token: 0x06005045 RID: 20549 RVA: 0x001154AD File Offset: 0x001136AD
		public QueryStringAttribute(string key)
		{
			this.Key = key;
		}

		// Token: 0x06005046 RID: 20550 RVA: 0x001154C3 File Offset: 0x001136C3
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			return new QueryStringValueProvider(modelBindingExecutionContext);
		}

		// Token: 0x06005047 RID: 20551 RVA: 0x001154D9 File Offset: 0x001136D9
		public override string GetModelName()
		{
			return this.Key;
		}

		// Token: 0x1700172A RID: 5930
		// (get) Token: 0x06005048 RID: 20552 RVA: 0x001154E1 File Offset: 0x001136E1
		// (set) Token: 0x06005049 RID: 20553 RVA: 0x001154E9 File Offset: 0x001136E9
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

		// Token: 0x04002ABE RID: 10942
		private bool _validateInput = true;
	}
}
