using System;

namespace Microsoft.Practices.Unity
{
	// Token: 0x02000011 RID: 17
	public class DependencyOverride<T> : DependencyOverride
	{
		// Token: 0x06000037 RID: 55 RVA: 0x0000282E File Offset: 0x00000A2E
		public DependencyOverride(object dependencyValue) : base(typeof(T), dependencyValue)
		{
		}
	}
}
