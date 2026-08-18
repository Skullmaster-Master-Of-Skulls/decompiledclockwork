using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net.Http
{
	// Token: 0x0200001E RID: 30
	[ComVisible(true)]
	internal class RtcRequestMessage : HttpRequestMessage, INetworkTransportSettings, INotificationTransportSync
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00006A66 File Offset: 0x00004C66
		internal RtcRequestMessage(HttpMethod method, Uri uri) : base(method, uri)
		{
			this.state = new RtcState();
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006A7C File Offset: 0x00004C7C
		[SecuritySafeCritical]
		public void ApplySetting([In] ref TRANSPORT_SETTING_ID settingId, [In] int lengthIn, [In] IntPtr valueIn, out int lengthOut, out IntPtr valueOut)
		{
			if (!RtcRequestMessage.TransportSettingsId.Equals(settingId.Guid))
			{
				throw new NotSupportedException();
			}
			if (valueIn == IntPtr.Zero)
			{
				throw new ArgumentNullException("valueIn");
			}
			byte[] array = settingId.Guid.ToByteArray();
			byte[] array2 = new byte[array.Length + lengthIn];
			array.CopyTo(array2, 0);
			if (lengthIn > 0)
			{
				Marshal.Copy(valueIn, array2, array.Length, lengthIn);
			}
			this.state.inputData = array2;
			lengthOut = 0;
			valueOut = IntPtr.Zero;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00006B04 File Offset: 0x00004D04
		[SecuritySafeCritical]
		public void QuerySetting([In] ref TRANSPORT_SETTING_ID settingId, [In] int lengthIn, [In] IntPtr valueIn, out int lengthOut, out IntPtr valueOut)
		{
			if (!RtcRequestMessage.TransportSettingsId.Equals(settingId.Guid))
			{
				throw new NotSupportedException();
			}
			this.state.connectComplete.WaitOne();
			if (this.state.result != 0)
			{
				throw new Win32Exception(this.state.result);
			}
			byte[] array;
			if (this.state.outputData != null)
			{
				array = this.state.outputData;
			}
			else
			{
				array = BitConverter.GetBytes(5);
			}
			lengthOut = array.Length;
			valueOut = Marshal.AllocCoTaskMem(lengthOut);
			Marshal.Copy(array, 0, valueOut, lengthOut);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006B9B File Offset: 0x00004D9B
		public void CompleteDelivery()
		{
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006B9D File Offset: 0x00004D9D
		public void Flush()
		{
			this.state.flushComplete.WaitOne();
		}

		// Token: 0x040000D2 RID: 210
		private static readonly Guid TransportSettingsId = new Guid("6B59819A-5CAE-492D-A901-2A3C2C50164F");

		// Token: 0x040000D3 RID: 211
		internal RtcState state;
	}
}
