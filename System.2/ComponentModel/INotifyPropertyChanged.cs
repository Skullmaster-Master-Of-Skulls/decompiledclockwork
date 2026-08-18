using System;

namespace System.ComponentModel
{
	// Token: 0x0200056A RID: 1386
	[__DynamicallyInvokable]
	public interface INotifyPropertyChanged
	{
		// Token: 0x1400004E RID: 78
		// (add) Token: 0x060033BB RID: 13243
		// (remove) Token: 0x060033BC RID: 13244
		[__DynamicallyInvokable]
		event PropertyChangedEventHandler PropertyChanged;
	}
}
