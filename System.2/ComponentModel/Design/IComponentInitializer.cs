using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020005E5 RID: 1509
	public interface IComponentInitializer
	{
		// Token: 0x060037F2 RID: 14322
		void InitializeExistingComponent(IDictionary defaultValues);

		// Token: 0x060037F3 RID: 14323
		void InitializeNewComponent(IDictionary defaultValues);
	}
}
