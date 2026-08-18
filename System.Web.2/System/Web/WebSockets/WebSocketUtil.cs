using System;

namespace System.Web.WebSockets
{
	// Token: 0x020001BB RID: 443
	internal static class WebSocketUtil
	{
		// Token: 0x060016F9 RID: 5881 RVA: 0x000482A4 File Offset: 0x000464A4
		public static bool IsSameOriginRequest(HttpWorkerRequest workerRequest)
		{
			string knownRequestHeader = workerRequest.GetKnownRequestHeader(28);
			if (string.IsNullOrEmpty(knownRequestHeader))
			{
				return false;
			}
			string unknownRequestHeader = workerRequest.GetUnknownRequestHeader("Origin");
			if (string.IsNullOrEmpty(unknownRequestHeader))
			{
				return false;
			}
			Uri uri = null;
			Uri uri2 = null;
			return Uri.TryCreate(workerRequest.GetProtocol() + "://" + knownRequestHeader.Trim(), UriKind.Absolute, out uri) && Uri.TryCreate(unknownRequestHeader.Trim(), UriKind.Absolute, out uri2) && (!(uri2.Scheme != "http") || !(uri2.Scheme != "https")) && (uri.Scheme == uri2.Scheme && uri.Host == uri2.Host) && uri.Port == uri2.Port;
		}
	}
}
