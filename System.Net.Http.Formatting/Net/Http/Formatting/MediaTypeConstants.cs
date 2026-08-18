using System;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000044 RID: 68
	internal static class MediaTypeConstants
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000282 RID: 642 RVA: 0x00009BAE File Offset: 0x00007DAE
		public static MediaTypeHeaderValue ApplicationOctetStreamMediaType
		{
			get
			{
				return MediaTypeConstants._defaultApplicationOctetStreamMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00009BBA File Offset: 0x00007DBA
		public static MediaTypeHeaderValue ApplicationXmlMediaType
		{
			get
			{
				return MediaTypeConstants._defaultApplicationXmlMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000284 RID: 644 RVA: 0x00009BC6 File Offset: 0x00007DC6
		public static MediaTypeHeaderValue ApplicationJsonMediaType
		{
			get
			{
				return MediaTypeConstants._defaultApplicationJsonMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009BD2 File Offset: 0x00007DD2
		public static MediaTypeHeaderValue TextXmlMediaType
		{
			get
			{
				return MediaTypeConstants._defaultTextXmlMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000286 RID: 646 RVA: 0x00009BDE File Offset: 0x00007DDE
		public static MediaTypeHeaderValue TextJsonMediaType
		{
			get
			{
				return MediaTypeConstants._defaultTextJsonMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00009BEA File Offset: 0x00007DEA
		public static MediaTypeHeaderValue ApplicationFormUrlEncodedMediaType
		{
			get
			{
				return MediaTypeConstants._defaultApplicationFormUrlEncodedMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00009BF6 File Offset: 0x00007DF6
		public static MediaTypeHeaderValue ApplicationBsonMediaType
		{
			get
			{
				return MediaTypeConstants._defaultApplicationBsonMediaType.Clone<MediaTypeHeaderValue>();
			}
		}

		// Token: 0x040000A9 RID: 169
		private static readonly MediaTypeHeaderValue _defaultApplicationXmlMediaType = new MediaTypeHeaderValue("application/xml");

		// Token: 0x040000AA RID: 170
		private static readonly MediaTypeHeaderValue _defaultTextXmlMediaType = new MediaTypeHeaderValue("text/xml");

		// Token: 0x040000AB RID: 171
		private static readonly MediaTypeHeaderValue _defaultApplicationJsonMediaType = new MediaTypeHeaderValue("application/json");

		// Token: 0x040000AC RID: 172
		private static readonly MediaTypeHeaderValue _defaultTextJsonMediaType = new MediaTypeHeaderValue("text/json");

		// Token: 0x040000AD RID: 173
		private static readonly MediaTypeHeaderValue _defaultApplicationOctetStreamMediaType = new MediaTypeHeaderValue("application/octet-stream");

		// Token: 0x040000AE RID: 174
		private static readonly MediaTypeHeaderValue _defaultApplicationFormUrlEncodedMediaType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

		// Token: 0x040000AF RID: 175
		private static readonly MediaTypeHeaderValue _defaultApplicationBsonMediaType = new MediaTypeHeaderValue("application/bson");
	}
}
