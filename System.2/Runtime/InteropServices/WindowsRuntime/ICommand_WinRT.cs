using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003EC RID: 1004
	[Guid("e5af3542-ca67-4081-995b-709dd13792df")]
	[ComImport]
	internal interface ICommand_WinRT
	{
		// Token: 0x0600262C RID: 9772
		EventRegistrationToken add_CanExecuteChanged(EventHandler<object> value);

		// Token: 0x0600262D RID: 9773
		void remove_CanExecuteChanged(EventRegistrationToken token);

		// Token: 0x0600262E RID: 9774
		bool CanExecute(object parameter);

		// Token: 0x0600262F RID: 9775
		void Execute(object parameter);
	}
}
