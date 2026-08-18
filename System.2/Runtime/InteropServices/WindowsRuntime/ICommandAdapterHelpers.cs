using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020003F7 RID: 1015
	internal static class ICommandAdapterHelpers
	{
		// Token: 0x06002656 RID: 9814 RVA: 0x000B0E90 File Offset: 0x000AF090
		internal static EventHandler<object> CreateWrapperHandler(EventHandler handler)
		{
			return delegate(object sender, object e)
			{
				EventArgs eventArgs = e as EventArgs;
				handler(sender, (eventArgs == null) ? EventArgs.Empty : eventArgs);
			};
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000B0EB8 File Offset: 0x000AF0B8
		internal static EventHandler CreateWrapperHandler(EventHandler<object> handler)
		{
			return delegate(object sender, EventArgs e)
			{
				handler(sender, e);
			};
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000B0EE0 File Offset: 0x000AF0E0
		internal static EventHandler<object> GetValueFromEquivalentKey(ConditionalWeakTable<EventHandler, EventHandler<object>> table, EventHandler key, ConditionalWeakTable<EventHandler, EventHandler<object>>.CreateValueCallback callback)
		{
			EventHandler<object> eventHandler;
			if (table.FindEquivalentKeyUnsafe(key, out eventHandler) == null)
			{
				eventHandler = callback(key);
				table.Add(key, eventHandler);
			}
			return eventHandler;
		}
	}
}
