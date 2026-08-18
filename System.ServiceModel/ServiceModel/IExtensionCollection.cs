using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.ServiceModel
{
	// Token: 0x020000F7 RID: 247
	[__DynamicallyInvokable]
	public interface IExtensionCollection<T> : ICollection<IExtension<!0>>, IEnumerable<IExtension<T>>, IEnumerable where T : IExtensibleObject<T>
	{
		// Token: 0x06000530 RID: 1328
		[__DynamicallyInvokable]
		E Find<E>();

		// Token: 0x06000531 RID: 1329
		[__DynamicallyInvokable]
		Collection<E> FindAll<E>();
	}
}
