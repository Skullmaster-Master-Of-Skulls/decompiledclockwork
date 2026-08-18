using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F5 RID: 1013
	[SecurityCritical]
	internal sealed class ICommandToManagedAdapter
	{
		// Token: 0x0600264A RID: 9802 RVA: 0x000B0CF6 File Offset: 0x000AEEF6
		private ICommandToManagedAdapter()
		{
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x0600264B RID: 9803 RVA: 0x000B0D00 File Offset: 0x000AEF00
		// (remove) Token: 0x0600264C RID: 9804 RVA: 0x000B0D50 File Offset: 0x000AEF50
		private event EventHandler CanExecuteChanged
		{
			add
			{
				ICommand_WinRT @object = JitHelpers.UnsafeCast<ICommand_WinRT>(this);
				Func<EventHandler<object>, EventRegistrationToken> addMethod = new Func<EventHandler<object>, EventRegistrationToken>(@object.add_CanExecuteChanged);
				Action<EventRegistrationToken> removeMethod = new Action<EventRegistrationToken>(@object.remove_CanExecuteChanged);
				EventHandler<object> value2 = ICommandToManagedAdapter.m_weakTable.GetValue(value, new ConditionalWeakTable<EventHandler, EventHandler<object>>.CreateValueCallback(ICommandAdapterHelpers.CreateWrapperHandler));
				WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(addMethod, removeMethod, value2);
			}
			remove
			{
				ICommand_WinRT @object = JitHelpers.UnsafeCast<ICommand_WinRT>(this);
				Action<EventRegistrationToken> removeMethod = new Action<EventRegistrationToken>(@object.remove_CanExecuteChanged);
				EventHandler<object> valueFromEquivalentKey = ICommandAdapterHelpers.GetValueFromEquivalentKey(ICommandToManagedAdapter.m_weakTable, value, new ConditionalWeakTable<EventHandler, EventHandler<object>>.CreateValueCallback(ICommandAdapterHelpers.CreateWrapperHandler));
				WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(removeMethod, valueFromEquivalentKey);
			}
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x000B0D94 File Offset: 0x000AEF94
		private bool CanExecute(object parameter)
		{
			ICommand_WinRT command_WinRT = JitHelpers.UnsafeCast<ICommand_WinRT>(this);
			return command_WinRT.CanExecute(parameter);
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000B0DB0 File Offset: 0x000AEFB0
		private void Execute(object parameter)
		{
			ICommand_WinRT command_WinRT = JitHelpers.UnsafeCast<ICommand_WinRT>(this);
			command_WinRT.Execute(parameter);
		}

		// Token: 0x040020B0 RID: 8368
		private static ConditionalWeakTable<EventHandler, EventHandler<object>> m_weakTable = new ConditionalWeakTable<EventHandler, EventHandler<object>>();
	}
}
