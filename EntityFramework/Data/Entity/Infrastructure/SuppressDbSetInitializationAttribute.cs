using System;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000760 RID: 1888
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false)]
	public sealed class SuppressDbSetInitializationAttribute : Attribute
	{
	}
}
