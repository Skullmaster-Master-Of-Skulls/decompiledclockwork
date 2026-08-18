using System;
using System.ComponentModel;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x020000DA RID: 218
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpResponseMessageExtensions
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x000112B8 File Offset: 0x0000F4B8
		public static bool TryGetContentValue<T>(this HttpResponseMessage response, out T value)
		{
			if (response == null)
			{
				throw Error.ArgumentNull("response");
			}
			ObjectContent objectContent = response.Content as ObjectContent;
			if (objectContent != null && objectContent.Value is T)
			{
				value = (T)((object)objectContent.Value);
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x0001130A File Offset: 0x0000F50A
		internal static void EnsureResponseHasRequest(this HttpResponseMessage response, HttpRequestMessage request)
		{
			if (response != null && response.RequestMessage == null)
			{
				response.RequestMessage = request;
			}
		}
	}
}
