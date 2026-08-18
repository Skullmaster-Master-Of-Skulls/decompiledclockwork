using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000054 RID: 84
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpContentExtensions
	{
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000B469 File Offset: 0x00009669
		private static MediaTypeFormatterCollection DefaultMediaTypeFormatterCollection
		{
			get
			{
				if (HttpContentExtensions._defaultMediaTypeFormatterCollection == null)
				{
					HttpContentExtensions._defaultMediaTypeFormatterCollection = new MediaTypeFormatterCollection();
				}
				return HttpContentExtensions._defaultMediaTypeFormatterCollection;
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000B481 File Offset: 0x00009681
		public static Task<object> ReadAsAsync(this HttpContent content, Type type)
		{
			return content.ReadAsAsync(type, HttpContentExtensions.DefaultMediaTypeFormatterCollection);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B48F File Offset: 0x0000968F
		public static Task<object> ReadAsAsync(this HttpContent content, Type type, CancellationToken cancellationToken)
		{
			return content.ReadAsAsync(type, HttpContentExtensions.DefaultMediaTypeFormatterCollection, cancellationToken);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B49E File Offset: 0x0000969E
		public static Task<object> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters)
		{
			return HttpContentExtensions.ReadAsAsync<object>(content, type, formatters, null);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B4A9 File Offset: 0x000096A9
		public static Task<object> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, CancellationToken cancellationToken)
		{
			return HttpContentExtensions.ReadAsAsync<object>(content, type, formatters, null, cancellationToken);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B4B5 File Offset: 0x000096B5
		public static Task<object> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
		{
			return HttpContentExtensions.ReadAsAsync<object>(content, type, formatters, formatterLogger);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public static Task<object> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return HttpContentExtensions.ReadAsAsync<object>(content, type, formatters, formatterLogger, cancellationToken);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B4CD File Offset: 0x000096CD
		public static Task<T> ReadAsAsync<T>(this HttpContent content)
		{
			return content.ReadAsAsync(HttpContentExtensions.DefaultMediaTypeFormatterCollection);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B4DA File Offset: 0x000096DA
		public static Task<T> ReadAsAsync<T>(this HttpContent content, CancellationToken cancellationToken)
		{
			return content.ReadAsAsync(HttpContentExtensions.DefaultMediaTypeFormatterCollection, cancellationToken);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B4E8 File Offset: 0x000096E8
		public static Task<T> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters)
		{
			return HttpContentExtensions.ReadAsAsync<T>(content, typeof(T), formatters, null);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B4FC File Offset: 0x000096FC
		public static Task<T> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters, CancellationToken cancellationToken)
		{
			return HttpContentExtensions.ReadAsAsync<T>(content, typeof(T), formatters, null, cancellationToken);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B511 File Offset: 0x00009711
		public static Task<T> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
		{
			return HttpContentExtensions.ReadAsAsync<T>(content, typeof(T), formatters, formatterLogger);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B525 File Offset: 0x00009725
		public static Task<T> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return HttpContentExtensions.ReadAsAsync<T>(content, typeof(T), formatters, formatterLogger, cancellationToken);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000B53A File Offset: 0x0000973A
		private static Task<T> ReadAsAsync<T>(HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
		{
			return HttpContentExtensions.ReadAsAsync<T>(content, type, formatters, formatterLogger, CancellationToken.None);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B54C File Offset: 0x0000974C
		private static Task<T> ReadAsAsync<T>(HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (formatters == null)
			{
				throw Error.ArgumentNull("formatters");
			}
			ObjectContent objectContent = content as ObjectContent;
			if (objectContent != null && objectContent.Value != null && type.IsAssignableFrom(objectContent.Value.GetType()))
			{
				return Task.FromResult<T>((T)((object)objectContent.Value));
			}
			MediaTypeHeaderValue mediaTypeHeaderValue = content.Headers.ContentType ?? MediaTypeConstants.ApplicationOctetStreamMediaType;
			MediaTypeFormatter mediaTypeFormatter = new MediaTypeFormatterCollection(formatters).FindReader(type, mediaTypeHeaderValue);
			if (mediaTypeFormatter != null)
			{
				return HttpContentExtensions.ReadAsAsyncCore<T>(content, type, formatterLogger, mediaTypeFormatter, cancellationToken);
			}
			if (content.Headers.ContentLength == 0L)
			{
				T result = (T)((object)MediaTypeFormatter.GetDefaultValueForType(type));
				return Task.FromResult<T>(result);
			}
			throw new UnsupportedMediaTypeException(Error.Format(Resources.NoReadSerializerAvailable, new object[]
			{
				type.Name,
				mediaTypeHeaderValue.MediaType
			}), mediaTypeHeaderValue);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000B7F8 File Offset: 0x000099F8
		private static async Task<T> ReadAsAsyncCore<T>(HttpContent content, Type type, IFormatterLogger formatterLogger, MediaTypeFormatter formatter, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Stream stream = await content.ReadAsStreamAsync();
			object result = await formatter.ReadFromStreamAsync(type, stream, content, formatterLogger, cancellationToken);
			return (T)((object)result);
		}

		// Token: 0x040000E0 RID: 224
		private static MediaTypeFormatterCollection _defaultMediaTypeFormatterCollection;
	}
}
