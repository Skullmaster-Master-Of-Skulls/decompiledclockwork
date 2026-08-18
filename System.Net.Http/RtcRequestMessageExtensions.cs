using System;

namespace System.Net.Http
{
	// Token: 0x0200001F RID: 31
	internal static class RtcRequestMessageExtensions
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00006BC4 File Offset: 0x00004DC4
		internal static void SetRtcOptions(this HttpRequestMessage request, HttpWebRequest webRequest)
		{
			RtcRequestMessage rtcRequestMessage = request as RtcRequestMessage;
			if (rtcRequestMessage != null)
			{
				webRequest.RtcState = rtcRequestMessage.state;
				webRequest.ServicePoint.Expect100Continue = false;
				webRequest.PreAuthenticate = true;
				webRequest.KeepAlive = false;
				webRequest.AllowAutoRedirect = false;
				webRequest.Pipelined = false;
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006C10 File Offset: 0x00004E10
		internal static void MarkRtcFlushComplete(this HttpRequestMessage request)
		{
			RtcRequestMessage rtcRequestMessage = request as RtcRequestMessage;
			if (rtcRequestMessage != null)
			{
				rtcRequestMessage.state.flushComplete.Set();
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006C38 File Offset: 0x00004E38
		internal static void AbortRtcRequest(this HttpRequestMessage request)
		{
			RtcRequestMessage rtcRequestMessage = request as RtcRequestMessage;
			if (rtcRequestMessage != null)
			{
				rtcRequestMessage.state.Abort();
			}
		}
	}
}
