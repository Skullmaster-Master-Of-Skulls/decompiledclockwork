using System;
using System.ComponentModel;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000037 RID: 55
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpClientExtensions
	{
		// Token: 0x060001AD RID: 429 RVA: 0x000079BA File Offset: 0x00005BBA
		public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
		{
			return client.PostAsJsonAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000079C9 File Offset: 0x00005BC9
		public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000079D9 File Offset: 0x00005BD9
		public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value)
		{
			return client.PostAsJsonAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000079E8 File Offset: 0x00005BE8
		public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000079F8 File Offset: 0x00005BF8
		public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, string requestUri, T value)
		{
			return client.PostAsXmlAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007A07 File Offset: 0x00005C07
		public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, new XmlMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007A17 File Offset: 0x00005C17
		public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value)
		{
			return client.PostAsXmlAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00007A26 File Offset: 0x00005C26
		public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, new XmlMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007A36 File Offset: 0x00005C36
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter)
		{
			return client.PostAsync(requestUri, value, formatter, CancellationToken.None);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00007A46 File Offset: 0x00005C46
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, formatter, null, cancellationToken);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007A54 File Offset: 0x00005C54
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, string mediaType)
		{
			return client.PostAsync(requestUri, value, formatter, mediaType, CancellationToken.None);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007A66 File Offset: 0x00005C66
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, string mediaType, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, formatter, ObjectContent.BuildHeaderValue(mediaType), cancellationToken);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007A7C File Offset: 0x00005C7C
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, CancellationToken cancellationToken)
		{
			if (client == null)
			{
				throw Error.ArgumentNull("client");
			}
			ObjectContent<T> content = new ObjectContent<T>(value, formatter, mediaType);
			return client.PostAsync(requestUri, content, cancellationToken);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007AAB File Offset: 0x00005CAB
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter)
		{
			return client.PostAsync(requestUri, value, formatter, CancellationToken.None);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007ABB File Offset: 0x00005CBB
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, formatter, null, cancellationToken);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007AC9 File Offset: 0x00005CC9
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, string mediaType)
		{
			return client.PostAsync(requestUri, value, formatter, mediaType, CancellationToken.None);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007ADB File Offset: 0x00005CDB
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, string mediaType, CancellationToken cancellationToken)
		{
			return client.PostAsync(requestUri, value, formatter, ObjectContent.BuildHeaderValue(mediaType), cancellationToken);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, CancellationToken cancellationToken)
		{
			if (client == null)
			{
				throw Error.ArgumentNull("client");
			}
			ObjectContent<T> content = new ObjectContent<T>(value, formatter, mediaType);
			return client.PostAsync(requestUri, content, cancellationToken);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007B1F File Offset: 0x00005D1F
		public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string requestUri, T value)
		{
			return client.PutAsJsonAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007B2E File Offset: 0x00005D2E
		public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007B3E File Offset: 0x00005D3E
		public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value)
		{
			return client.PutAsJsonAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007B4D File Offset: 0x00005D4D
		public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, new JsonMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007B5D File Offset: 0x00005D5D
		public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, string requestUri, T value)
		{
			return client.PutAsXmlAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007B6C File Offset: 0x00005D6C
		public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, string requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, new XmlMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00007B7C File Offset: 0x00005D7C
		public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value)
		{
			return client.PutAsXmlAsync(requestUri, value, CancellationToken.None);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00007B8B File Offset: 0x00005D8B
		public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, new XmlMediaTypeFormatter(), cancellationToken);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00007B9B File Offset: 0x00005D9B
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter)
		{
			return client.PutAsync(requestUri, value, formatter, CancellationToken.None);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007BAB File Offset: 0x00005DAB
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, formatter, null, cancellationToken);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007BB9 File Offset: 0x00005DB9
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, string mediaType)
		{
			return client.PutAsync(requestUri, value, formatter, mediaType, CancellationToken.None);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007BCB File Offset: 0x00005DCB
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, string mediaType, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, formatter, ObjectContent.BuildHeaderValue(mediaType), cancellationToken);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007BE0 File Offset: 0x00005DE0
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, string requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, CancellationToken cancellationToken)
		{
			if (client == null)
			{
				throw Error.ArgumentNull("client");
			}
			ObjectContent<T> content = new ObjectContent<T>(value, formatter, mediaType);
			return client.PutAsync(requestUri, content, cancellationToken);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007C0F File Offset: 0x00005E0F
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter)
		{
			return client.PutAsync(requestUri, value, formatter, CancellationToken.None);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007C1F File Offset: 0x00005E1F
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, formatter, null, cancellationToken);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007C2D File Offset: 0x00005E2D
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, string mediaType)
		{
			return client.PutAsync(requestUri, value, formatter, mediaType, CancellationToken.None);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007C3F File Offset: 0x00005E3F
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, string mediaType, CancellationToken cancellationToken)
		{
			return client.PutAsync(requestUri, value, formatter, ObjectContent.BuildHeaderValue(mediaType), cancellationToken);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007C54 File Offset: 0x00005E54
		public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType, CancellationToken cancellationToken)
		{
			if (client == null)
			{
				throw Error.ArgumentNull("client");
			}
			ObjectContent<T> content = new ObjectContent<T>(value, formatter, mediaType);
			return client.PutAsync(requestUri, content, cancellationToken);
		}
	}
}
