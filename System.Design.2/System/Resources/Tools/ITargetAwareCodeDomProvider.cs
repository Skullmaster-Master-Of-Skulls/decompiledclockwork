using System;

namespace System.Resources.Tools
{
	// Token: 0x0200000A RID: 10
	public interface ITargetAwareCodeDomProvider
	{
		// Token: 0x06000012 RID: 18
		bool SupportsProperty(Type type, string propertyName, bool isWritable);
	}
}
