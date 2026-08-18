using System;
using System.ComponentModel;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003EB RID: 1003
	[Guid("cf75d69c-f2f4-486b-b302-bb4c09baebfa")]
	[ComImport]
	internal interface INotifyPropertyChanged_WinRT
	{
		// Token: 0x0600262A RID: 9770
		EventRegistrationToken add_PropertyChanged(PropertyChangedEventHandler value);

		// Token: 0x0600262B RID: 9771
		void remove_PropertyChanged(EventRegistrationToken token);
	}
}
