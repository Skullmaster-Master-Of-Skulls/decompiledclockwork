using System;

namespace System.Web.ModelBinding
{
	// Token: 0x02000630 RID: 1584
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class BindNeverAttribute : BindingBehaviorAttribute
	{
		// Token: 0x06004EE5 RID: 20197 RVA: 0x0011274F File Offset: 0x0011094F
		public BindNeverAttribute() : base(BindingBehavior.Never)
		{
		}
	}
}
