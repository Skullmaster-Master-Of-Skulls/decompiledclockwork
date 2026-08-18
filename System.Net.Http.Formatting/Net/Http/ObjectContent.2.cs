using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	// Token: 0x02000021 RID: 33
	public class ObjectContent<T> : ObjectContent
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00005183 File Offset: 0x00003383
		public ObjectContent(T value, MediaTypeFormatter formatter) : this(value, formatter, null)
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000518E File Offset: 0x0000338E
		public ObjectContent(T value, MediaTypeFormatter formatter, string mediaType) : this(value, formatter, ObjectContent.BuildHeaderValue(mediaType))
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000519E File Offset: 0x0000339E
		public ObjectContent(T value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType) : base(typeof(T), value, formatter, mediaType)
		{
		}
	}
}
