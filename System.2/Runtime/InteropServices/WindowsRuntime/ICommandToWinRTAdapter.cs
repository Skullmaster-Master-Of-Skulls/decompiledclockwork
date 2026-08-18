using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows.Input;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F6 RID: 1014
	[SecurityCritical]
	internal sealed class ICommandToWinRTAdapter
	{
		// Token: 0x06002650 RID: 9808 RVA: 0x000B0DD7 File Offset: 0x000AEFD7
		private ICommandToWinRTAdapter()
		{
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000B0DE0 File Offset: 0x000AEFE0
		private EventRegistrationToken add_CanExecuteChanged(EventHandler<object> value)
		{
			ICommand command = JitHelpers.UnsafeCast<ICommand>(this);
			EventRegistrationTokenTable<EventHandler> orCreateValue = ICommandToWinRTAdapter.m_weakTable.GetOrCreateValue(command);
			EventHandler eventHandler = ICommandAdapterHelpers.CreateWrapperHandler(value);
			EventRegistrationToken result = orCreateValue.AddEventHandler(eventHandler);
			command.CanExecuteChanged += eventHandler;
			return result;
		}

		// Token: 0x06002652 RID: 9810 RVA: 0x000B0E18 File Offset: 0x000AF018
		private void remove_CanExecuteChanged(EventRegistrationToken token)
		{
			ICommand command = JitHelpers.UnsafeCast<ICommand>(this);
			EventRegistrationTokenTable<EventHandler> orCreateValue = ICommandToWinRTAdapter.m_weakTable.GetOrCreateValue(command);
			EventHandler eventHandler = orCreateValue.ExtractHandler(token);
			if (eventHandler != null)
			{
				command.CanExecuteChanged -= eventHandler;
			}
		}

		// Token: 0x06002653 RID: 9811 RVA: 0x000B0E4C File Offset: 0x000AF04C
		private bool CanExecute(object parameter)
		{
			ICommand command = JitHelpers.UnsafeCast<ICommand>(this);
			return command.CanExecute(parameter);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000B0E68 File Offset: 0x000AF068
		private void Execute(object parameter)
		{
			ICommand command = JitHelpers.UnsafeCast<ICommand>(this);
			command.Execute(parameter);
		}

		// Token: 0x040020B1 RID: 8369
		private static ConditionalWeakTable<ICommand, EventRegistrationTokenTable<EventHandler>> m_weakTable = new ConditionalWeakTable<ICommand, EventRegistrationTokenTable<EventHandler>>();
	}
}
