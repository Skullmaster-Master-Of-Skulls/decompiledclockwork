using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020005FA RID: 1530
	public interface ITypeDiscoveryService
	{
		// Token: 0x06003864 RID: 14436
		ICollection GetTypes(Type baseType, bool excludeGlobalTypes);
	}
}
