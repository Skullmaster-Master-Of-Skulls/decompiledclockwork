using System;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004D6 RID: 1238
	internal static class DeviceContexts
	{
		// Token: 0x06005140 RID: 20800 RVA: 0x001531E8 File Offset: 0x001513E8
		internal static void AddDeviceContext(DeviceContext dc)
		{
			if (DeviceContexts.activeDeviceContexts == null)
			{
				DeviceContexts.activeDeviceContexts = new ClientUtils.WeakRefCollection();
				DeviceContexts.activeDeviceContexts.RefCheckThreshold = 20;
			}
			if (!DeviceContexts.activeDeviceContexts.Contains(dc))
			{
				dc.Disposing += DeviceContexts.OnDcDisposing;
				DeviceContexts.activeDeviceContexts.Add(dc);
			}
		}

		// Token: 0x06005141 RID: 20801 RVA: 0x00153240 File Offset: 0x00151440
		private static void OnDcDisposing(object sender, EventArgs e)
		{
			DeviceContext deviceContext = sender as DeviceContext;
			if (deviceContext != null)
			{
				deviceContext.Disposing -= DeviceContexts.OnDcDisposing;
				DeviceContexts.RemoveDeviceContext(deviceContext);
			}
		}

		// Token: 0x06005142 RID: 20802 RVA: 0x0015326F File Offset: 0x0015146F
		internal static void RemoveDeviceContext(DeviceContext dc)
		{
			if (DeviceContexts.activeDeviceContexts == null)
			{
				return;
			}
			DeviceContexts.activeDeviceContexts.RemoveByHashCode(dc);
		}

		// Token: 0x06005143 RID: 20803 RVA: 0x00153284 File Offset: 0x00151484
		internal static bool IsFontInUse(WindowsFont wf)
		{
			if (wf == null)
			{
				return false;
			}
			for (int i = 0; i < DeviceContexts.activeDeviceContexts.Count; i++)
			{
				DeviceContext deviceContext = DeviceContexts.activeDeviceContexts[i] as DeviceContext;
				if (deviceContext != null && (deviceContext.ActiveFont == wf || deviceContext.IsFontOnContextStack(wf)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400351C RID: 13596
		[ThreadStatic]
		private static ClientUtils.WeakRefCollection activeDeviceContexts;
	}
}
