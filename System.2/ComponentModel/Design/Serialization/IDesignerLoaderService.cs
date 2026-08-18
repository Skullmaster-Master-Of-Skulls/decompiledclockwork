using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200060A RID: 1546
	public interface IDesignerLoaderService
	{
		// Token: 0x060038BD RID: 14525
		void AddLoadDependency();

		// Token: 0x060038BE RID: 14526
		void DependentLoadComplete(bool successful, ICollection errorCollection);

		// Token: 0x060038BF RID: 14527
		bool Reload();
	}
}
