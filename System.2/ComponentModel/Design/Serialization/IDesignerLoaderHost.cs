using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000608 RID: 1544
	public interface IDesignerLoaderHost : IDesignerHost, IServiceContainer, IServiceProvider
	{
		// Token: 0x060038B7 RID: 14519
		void EndLoad(string baseClassName, bool successful, ICollection errorCollection);

		// Token: 0x060038B8 RID: 14520
		void Reload();
	}
}
