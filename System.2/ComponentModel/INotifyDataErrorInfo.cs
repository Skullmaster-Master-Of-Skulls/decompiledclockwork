using System;
using System.Collections;

namespace System.ComponentModel
{
	// Token: 0x02000569 RID: 1385
	[__DynamicallyInvokable]
	public interface INotifyDataErrorInfo
	{
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060033B7 RID: 13239
		[__DynamicallyInvokable]
		bool HasErrors { [__DynamicallyInvokable] get; }

		// Token: 0x060033B8 RID: 13240
		[__DynamicallyInvokable]
		IEnumerable GetErrors(string propertyName);

		// Token: 0x1400004D RID: 77
		// (add) Token: 0x060033B9 RID: 13241
		// (remove) Token: 0x060033BA RID: 13242
		[__DynamicallyInvokable]
		event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
	}
}
