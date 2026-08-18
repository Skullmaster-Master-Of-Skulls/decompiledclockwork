using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000002 RID: 2
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = false, Inherited = true)]
	internal sealed class CanBeNullAttribute : Attribute
	{
	}
}
