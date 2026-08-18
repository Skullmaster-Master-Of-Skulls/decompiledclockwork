using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000650 RID: 1616
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class FormAttribute : ValueProviderSourceAttribute, IUnvalidatedValueProviderSource, IValueProviderSource
	{
		// Token: 0x170016F3 RID: 5875
		// (get) Token: 0x06004F9B RID: 20379 RVA: 0x0011486D File Offset: 0x00112A6D
		// (set) Token: 0x06004F9C RID: 20380 RVA: 0x00114875 File Offset: 0x00112A75
		public string FieldName { get; private set; }

		// Token: 0x06004F9D RID: 20381 RVA: 0x0011487E File Offset: 0x00112A7E
		public FormAttribute() : this(null)
		{
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x00114887 File Offset: 0x00112A87
		public FormAttribute(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x0011489D File Offset: 0x00112A9D
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			return new FormValueProvider(modelBindingExecutionContext);
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x001148B3 File Offset: 0x00112AB3
		public override string GetModelName()
		{
			return this.FieldName;
		}

		// Token: 0x170016F4 RID: 5876
		// (get) Token: 0x06004FA1 RID: 20385 RVA: 0x001148BB File Offset: 0x00112ABB
		// (set) Token: 0x06004FA2 RID: 20386 RVA: 0x001148C3 File Offset: 0x00112AC3
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

		// Token: 0x04002A8C RID: 10892
		private bool _validateInput = true;
	}
}
