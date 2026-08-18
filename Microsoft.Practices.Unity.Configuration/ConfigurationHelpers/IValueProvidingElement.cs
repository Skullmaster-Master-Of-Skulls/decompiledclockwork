using System;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x0200002A RID: 42
	public interface IValueProvidingElement
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000145 RID: 325
		// (set) Token: 0x06000146 RID: 326
		ParameterValueElement Value { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000147 RID: 327
		string DestinationName { get; }
	}
}
