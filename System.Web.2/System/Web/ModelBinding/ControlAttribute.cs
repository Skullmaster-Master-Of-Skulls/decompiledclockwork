using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000639 RID: 1593
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
	public sealed class ControlAttribute : ValueProviderSourceAttribute
	{
		// Token: 0x170016D8 RID: 5848
		// (get) Token: 0x06004F07 RID: 20231 RVA: 0x00112E55 File Offset: 0x00111055
		// (set) Token: 0x06004F08 RID: 20232 RVA: 0x00112E5D File Offset: 0x0011105D
		public string ControlID { get; private set; }

		// Token: 0x170016D9 RID: 5849
		// (get) Token: 0x06004F09 RID: 20233 RVA: 0x00112E66 File Offset: 0x00111066
		// (set) Token: 0x06004F0A RID: 20234 RVA: 0x00112E6E File Offset: 0x0011106E
		public string PropertyName { get; private set; }

		// Token: 0x06004F0B RID: 20235 RVA: 0x00112E77 File Offset: 0x00111077
		public ControlAttribute() : this(null, null)
		{
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x00112E81 File Offset: 0x00111081
		public ControlAttribute(string controlID) : this(controlID, null)
		{
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x00112E8B File Offset: 0x0011108B
		public ControlAttribute(string controlID, string propertyName)
		{
			this.ControlID = controlID;
			this.PropertyName = propertyName;
		}

		// Token: 0x06004F0E RID: 20238 RVA: 0x00112EA1 File Offset: 0x001110A1
		public override IValueProvider GetValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
		{
			if (modelBindingExecutionContext == null)
			{
				throw new ArgumentNullException("modelBindingExecutionContext");
			}
			return new ControlValueProvider(modelBindingExecutionContext, this.PropertyName);
		}

		// Token: 0x06004F0F RID: 20239 RVA: 0x00112EBD File Offset: 0x001110BD
		public override string GetModelName()
		{
			return this.ControlID;
		}
	}
}
