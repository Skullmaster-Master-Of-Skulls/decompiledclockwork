using System;
using System.Net.Http;
using System.Threading.Tasks;
using Google.Apis.Json;
using Google.Apis.Requests;
using Google.Apis.Services;
using Google.Apis.Util;
using Newtonsoft.Json;

namespace Google.Apis.Media
{
	// Token: 0x0200000A RID: 10
	internal static class MediaApiErrorHandling
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002BDC File Offset: 0x00000DDC
		internal static Task<GoogleApiException> ExceptionForResponseAsync(IClientService service, HttpResponseMessage response)
		{
			return MediaApiErrorHandling.ExceptionForResponseAsync(service.Serializer, service.Name, response);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BF0 File Offset: 0x00000DF0
		internal static async Task<GoogleApiException> ExceptionForResponseAsync(ISerializer serializer, string name, HttpResponseMessage response)
		{
			string text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
			RequestError requestError = null;
			string message = text;
			try
			{
				StandardResponse<object> standardResponse = (serializer ?? NewtonsoftJsonSerializer.Instance).Deserialize<StandardResponse<object>>(text);
				if (standardResponse != null && standardResponse.Error != null)
				{
					requestError = standardResponse.Error;
					message = requestError.ToString();
				}
			}
			catch (JsonException)
			{
			}
			return new GoogleApiException(name ?? "", message)
			{
				Error = requestError,
				HttpStatusCode = response.StatusCode
			};
		}
	}
}
