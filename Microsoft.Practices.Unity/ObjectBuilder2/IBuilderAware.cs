using System;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000046 RID: 70
	public interface IBuilderAware
	{
		// Token: 0x06000137 RID: 311
		void OnBuiltUp(NamedTypeBuildKey buildKey);

		// Token: 0x06000138 RID: 312
		void OnTearingDown();
	}
}
