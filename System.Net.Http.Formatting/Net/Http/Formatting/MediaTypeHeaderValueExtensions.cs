using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000025 RID: 37
	internal static class MediaTypeHeaderValueExtensions
	{
		// Token: 0x06000123 RID: 291 RVA: 0x000055B0 File Offset: 0x000037B0
		public static bool IsSubsetOf(this MediaTypeHeaderValue mediaType1, MediaTypeHeaderValue mediaType2)
		{
			MediaTypeHeaderValueRange mediaTypeHeaderValueRange;
			return mediaType1.IsSubsetOf(mediaType2, out mediaTypeHeaderValueRange);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000055C8 File Offset: 0x000037C8
		public static bool IsSubsetOf(this MediaTypeHeaderValue mediaType1, MediaTypeHeaderValue mediaType2, out MediaTypeHeaderValueRange mediaType2Range)
		{
			if (mediaType2 == null)
			{
				mediaType2Range = MediaTypeHeaderValueRange.None;
				return false;
			}
			ParsedMediaTypeHeaderValue parsedMediaTypeHeaderValue = new ParsedMediaTypeHeaderValue(mediaType1);
			ParsedMediaTypeHeaderValue parsedMediaTypeHeaderValue2 = new ParsedMediaTypeHeaderValue(mediaType2);
			mediaType2Range = (parsedMediaTypeHeaderValue2.IsAllMediaRange ? MediaTypeHeaderValueRange.AllMediaRange : (parsedMediaTypeHeaderValue2.IsSubtypeMediaRange ? MediaTypeHeaderValueRange.SubtypeMediaRange : MediaTypeHeaderValueRange.None));
			if (!parsedMediaTypeHeaderValue.TypesEqual(ref parsedMediaTypeHeaderValue2))
			{
				if (mediaType2Range != MediaTypeHeaderValueRange.AllMediaRange)
				{
					return false;
				}
			}
			else if (!parsedMediaTypeHeaderValue.SubTypesEqual(ref parsedMediaTypeHeaderValue2) && mediaType2Range != MediaTypeHeaderValueRange.SubtypeMediaRange)
			{
				return false;
			}
			Collection<NameValueHeaderValue> collection = mediaType1.Parameters.AsCollection<NameValueHeaderValue>();
			int count = collection.Count;
			Collection<NameValueHeaderValue> collection2 = mediaType2.Parameters.AsCollection<NameValueHeaderValue>();
			int count2 = collection2.Count;
			for (int i = 0; i < count; i++)
			{
				NameValueHeaderValue nameValueHeaderValue = collection[i];
				bool flag = false;
				for (int j = 0; j < count2; j++)
				{
					NameValueHeaderValue obj = collection2[j];
					if (nameValueHeaderValue.Equals(obj))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}
	}
}
