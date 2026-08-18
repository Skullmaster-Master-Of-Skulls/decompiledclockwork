using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000631 RID: 1585
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class BindRequiredAttribute : BindingBehaviorAttribute
	{
		// Token: 0x06004EE6 RID: 20198 RVA: 0x00112758 File Offset: 0x00110958
		public BindRequiredAttribute() : base(BindingBehavior.Required)
		{
		}
	}
}
