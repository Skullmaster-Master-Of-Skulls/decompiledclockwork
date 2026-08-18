using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000047 RID: 71
	internal class MediaTypeWithQualityHeaderValueComparer : IComparer<MediaTypeWithQualityHeaderValue>
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000A22C File Offset: 0x0000842C
		private MediaTypeWithQualityHeaderValueComparer()
		{
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x0000A234 File Offset: 0x00008434
		public static MediaTypeWithQualityHeaderValueComparer QualityComparer
		{
			get
			{
				return MediaTypeWithQualityHeaderValueComparer._mediaTypeComparer;
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000A23C File Offset: 0x0000843C
		public int Compare(MediaTypeWithQualityHeaderValue mediaType1, MediaTypeWithQualityHeaderValue mediaType2)
		{
			if (object.ReferenceEquals(mediaType1, mediaType2))
			{
				return 0;
			}
			int num = MediaTypeWithQualityHeaderValueComparer.CompareBasedOnQualityFactor(mediaType1, mediaType2);
			if (num == 0)
			{
				ParsedMediaTypeHeaderValue parsedMediaTypeHeaderValue = new ParsedMediaTypeHeaderValue(mediaType1);
				ParsedMediaTypeHeaderValue parsedMediaTypeHeaderValue2 = new ParsedMediaTypeHeaderValue(mediaType2);
				if (!parsedMediaTypeHeaderValue.TypesEqual(ref parsedMediaTypeHeaderValue2))
				{
					if (parsedMediaTypeHeaderValue.IsAllMediaRange)
					{
						return -1;
					}
					if (parsedMediaTypeHeaderValue2.IsAllMediaRange)
					{
						return 1;
					}
					if (parsedMediaTypeHeaderValue.IsSubtypeMediaRange && !parsedMediaTypeHeaderValue2.IsSubtypeMediaRange)
					{
						return -1;
					}
					if (!parsedMediaTypeHeaderValue.IsSubtypeMediaRange && parsedMediaTypeHeaderValue2.IsSubtypeMediaRange)
					{
						return 1;
					}
				}
				else if (!parsedMediaTypeHeaderValue.SubTypesEqual(ref parsedMediaTypeHeaderValue2))
				{
					if (parsedMediaTypeHeaderValue.IsSubtypeMediaRange)
					{
						return -1;
					}
					if (parsedMediaTypeHeaderValue2.IsSubtypeMediaRange)
					{
						return 1;
					}
				}
			}
			return num;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000A2E0 File Offset: 0x000084E0
		private static int CompareBasedOnQualityFactor(MediaTypeWithQualityHeaderValue mediaType1, MediaTypeWithQualityHeaderValue mediaType2)
		{
			double? quality = mediaType1.Quality;
			double num = (quality != null) ? quality.GetValueOrDefault() : 1.0;
			double? quality2 = mediaType2.Quality;
			double num2 = (quality2 != null) ? quality2.GetValueOrDefault() : 1.0;
			double num3 = num - num2;
			if (num3 < 0.0)
			{
				return -1;
			}
			if (num3 > 0.0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x040000B4 RID: 180
		private static readonly MediaTypeWithQualityHeaderValueComparer _mediaTypeComparer = new MediaTypeWithQualityHeaderValueComparer();
	}
}
