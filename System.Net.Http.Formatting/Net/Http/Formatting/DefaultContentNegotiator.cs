using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000041 RID: 65
	public class DefaultContentNegotiator : IContentNegotiator
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00008BE4 File Offset: 0x00006DE4
		public DefaultContentNegotiator() : this(false)
		{
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008BED File Offset: 0x00006DED
		public DefaultContentNegotiator(bool excludeMatchOnTypeOnly)
		{
			this.ExcludeMatchOnTypeOnly = excludeMatchOnTypeOnly;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00008BFC File Offset: 0x00006DFC
		// (set) Token: 0x06000250 RID: 592 RVA: 0x00008C04 File Offset: 0x00006E04
		public bool ExcludeMatchOnTypeOnly { get; private set; }

		// Token: 0x06000251 RID: 593 RVA: 0x00008C10 File Offset: 0x00006E10
		public virtual ContentNegotiationResult Negotiate(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatters == null)
			{
				throw Error.ArgumentNull("formatters");
			}
			Collection<MediaTypeFormatterMatch> matches = this.ComputeFormatterMatches(type, request, formatters);
			MediaTypeFormatterMatch mediaTypeFormatterMatch = this.SelectResponseMediaTypeFormatter(matches);
			if (mediaTypeFormatterMatch != null)
			{
				Encoding encoding = this.SelectResponseCharacterEncoding(request, mediaTypeFormatterMatch.Formatter);
				if (encoding != null)
				{
					mediaTypeFormatterMatch.MediaType.CharSet = encoding.WebName;
				}
				MediaTypeHeaderValue mediaType = mediaTypeFormatterMatch.MediaType;
				MediaTypeFormatter perRequestFormatterInstance = mediaTypeFormatterMatch.Formatter.GetPerRequestFormatterInstance(type, request, mediaType);
				return new ContentNegotiationResult(perRequestFormatterInstance, mediaType);
			}
			return null;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008CA8 File Offset: 0x00006EA8
		protected virtual Collection<MediaTypeFormatterMatch> ComputeFormatterMatches(Type type, HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatters == null)
			{
				throw Error.ArgumentNull("formatters");
			}
			IEnumerable<MediaTypeWithQualityHeaderValue> enumerable = null;
			ListWrapperCollection<MediaTypeFormatterMatch> listWrapperCollection = new ListWrapperCollection<MediaTypeFormatterMatch>();
			foreach (MediaTypeFormatter mediaTypeFormatter in DefaultContentNegotiator.GetWritingFormatters(formatters))
			{
				if (mediaTypeFormatter.CanWriteType(type))
				{
					MediaTypeFormatterMatch item;
					if ((item = this.MatchMediaTypeMapping(request, mediaTypeFormatter)) != null)
					{
						listWrapperCollection.Add(item);
					}
					else
					{
						if (enumerable == null)
						{
							enumerable = this.SortMediaTypeWithQualityHeaderValuesByQFactor(request.Headers.Accept);
						}
						if ((item = this.MatchAcceptHeader(enumerable, mediaTypeFormatter)) != null)
						{
							listWrapperCollection.Add(item);
						}
						else if ((item = this.MatchRequestMediaType(request, mediaTypeFormatter)) != null)
						{
							listWrapperCollection.Add(item);
						}
						else
						{
							bool flag = this.ShouldMatchOnType(enumerable);
							if (flag && (item = this.MatchType(type, mediaTypeFormatter)) != null)
							{
								listWrapperCollection.Add(item);
							}
						}
					}
				}
			}
			return listWrapperCollection;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008DA0 File Offset: 0x00006FA0
		protected virtual MediaTypeFormatterMatch SelectResponseMediaTypeFormatter(ICollection<MediaTypeFormatterMatch> matches)
		{
			if (matches == null)
			{
				throw Error.ArgumentNull("matches");
			}
			List<MediaTypeFormatterMatch> list = matches.AsList<MediaTypeFormatterMatch>();
			MediaTypeFormatterMatch mediaTypeFormatterMatch = null;
			MediaTypeFormatterMatch mediaTypeFormatterMatch2 = null;
			MediaTypeFormatterMatch mediaTypeFormatterMatch3 = null;
			MediaTypeFormatterMatch mediaTypeFormatterMatch4 = null;
			MediaTypeFormatterMatch mediaTypeFormatterMatch5 = null;
			MediaTypeFormatterMatch mediaTypeFormatterMatch6 = null;
			for (int i = 0; i < list.Count; i++)
			{
				MediaTypeFormatterMatch mediaTypeFormatterMatch7 = list[i];
				switch (mediaTypeFormatterMatch7.Ranking)
				{
				case MediaTypeFormatterMatchRanking.MatchOnCanWriteType:
					if (mediaTypeFormatterMatch == null)
					{
						mediaTypeFormatterMatch = mediaTypeFormatterMatch7;
					}
					break;
				case MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderLiteral:
					mediaTypeFormatterMatch2 = this.UpdateBestMatch(mediaTypeFormatterMatch2, mediaTypeFormatterMatch7);
					break;
				case MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderSubtypeMediaRange:
					mediaTypeFormatterMatch3 = this.UpdateBestMatch(mediaTypeFormatterMatch3, mediaTypeFormatterMatch7);
					break;
				case MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderAllMediaRange:
					mediaTypeFormatterMatch4 = this.UpdateBestMatch(mediaTypeFormatterMatch4, mediaTypeFormatterMatch7);
					break;
				case MediaTypeFormatterMatchRanking.MatchOnRequestWithMediaTypeMapping:
					mediaTypeFormatterMatch5 = this.UpdateBestMatch(mediaTypeFormatterMatch5, mediaTypeFormatterMatch7);
					break;
				case MediaTypeFormatterMatchRanking.MatchOnRequestMediaType:
					if (mediaTypeFormatterMatch6 == null)
					{
						mediaTypeFormatterMatch6 = mediaTypeFormatterMatch7;
					}
					break;
				}
			}
			if (mediaTypeFormatterMatch5 != null)
			{
				MediaTypeFormatterMatch mediaTypeFormatterMatch8 = mediaTypeFormatterMatch5;
				mediaTypeFormatterMatch8 = this.UpdateBestMatch(mediaTypeFormatterMatch8, mediaTypeFormatterMatch2);
				mediaTypeFormatterMatch8 = this.UpdateBestMatch(mediaTypeFormatterMatch8, mediaTypeFormatterMatch3);
				mediaTypeFormatterMatch8 = this.UpdateBestMatch(mediaTypeFormatterMatch8, mediaTypeFormatterMatch4);
				if (mediaTypeFormatterMatch8 != mediaTypeFormatterMatch5)
				{
					mediaTypeFormatterMatch5 = null;
				}
			}
			MediaTypeFormatterMatch mediaTypeFormatterMatch9 = null;
			if (mediaTypeFormatterMatch5 != null)
			{
				mediaTypeFormatterMatch9 = mediaTypeFormatterMatch5;
			}
			else if (mediaTypeFormatterMatch2 != null || mediaTypeFormatterMatch3 != null || mediaTypeFormatterMatch4 != null)
			{
				mediaTypeFormatterMatch9 = this.UpdateBestMatch(mediaTypeFormatterMatch9, mediaTypeFormatterMatch2);
				mediaTypeFormatterMatch9 = this.UpdateBestMatch(mediaTypeFormatterMatch9, mediaTypeFormatterMatch3);
				mediaTypeFormatterMatch9 = this.UpdateBestMatch(mediaTypeFormatterMatch9, mediaTypeFormatterMatch4);
			}
			else if (mediaTypeFormatterMatch6 != null)
			{
				mediaTypeFormatterMatch9 = mediaTypeFormatterMatch6;
			}
			else if (mediaTypeFormatterMatch != null)
			{
				mediaTypeFormatterMatch9 = mediaTypeFormatterMatch;
			}
			return mediaTypeFormatterMatch9;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00008EE8 File Offset: 0x000070E8
		protected virtual Encoding SelectResponseCharacterEncoding(HttpRequestMessage request, MediaTypeFormatter formatter)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			List<Encoding> supportedEncodingsInternal = formatter.SupportedEncodingsInternal;
			if (supportedEncodingsInternal.Count > 0)
			{
				IEnumerable<StringWithQualityHeaderValue> enumerable = this.SortStringWithQualityHeaderValuesByQFactor(request.Headers.AcceptCharset);
				foreach (StringWithQualityHeaderValue stringWithQualityHeaderValue in enumerable)
				{
					for (int i = 0; i < supportedEncodingsInternal.Count; i++)
					{
						Encoding encoding = supportedEncodingsInternal[i];
						if (encoding != null)
						{
							double? quality = stringWithQualityHeaderValue.Quality;
							if ((quality.GetValueOrDefault() != 0.0 || quality == null) && (stringWithQualityHeaderValue.Value.Equals(encoding.WebName, StringComparison.OrdinalIgnoreCase) || stringWithQualityHeaderValue.Value.Equals("*", StringComparison.OrdinalIgnoreCase)))
							{
								return encoding;
							}
						}
					}
				}
				return formatter.SelectCharacterEncoding((request.Content != null) ? request.Content.Headers : null);
			}
			return null;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000900C File Offset: 0x0000720C
		protected virtual MediaTypeFormatterMatch MatchMediaTypeMapping(HttpRequestMessage request, MediaTypeFormatter formatter)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			List<MediaTypeMapping> mediaTypeMappingsInternal = formatter.MediaTypeMappingsInternal;
			for (int i = 0; i < mediaTypeMappingsInternal.Count; i++)
			{
				MediaTypeMapping mediaTypeMapping = mediaTypeMappingsInternal[i];
				double value;
				if (mediaTypeMapping != null && (value = mediaTypeMapping.TryMatchMediaType(request)) > 0.0)
				{
					return new MediaTypeFormatterMatch(formatter, mediaTypeMapping.MediaType, new double?(value), MediaTypeFormatterMatchRanking.MatchOnRequestWithMediaTypeMapping);
				}
			}
			return null;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009084 File Offset: 0x00007284
		protected virtual MediaTypeFormatterMatch MatchAcceptHeader(IEnumerable<MediaTypeWithQualityHeaderValue> sortedAcceptValues, MediaTypeFormatter formatter)
		{
			if (sortedAcceptValues == null)
			{
				throw Error.ArgumentNull("sortedAcceptValues");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			foreach (MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValue in sortedAcceptValues)
			{
				List<MediaTypeHeaderValue> supportedMediaTypesInternal = formatter.SupportedMediaTypesInternal;
				for (int i = 0; i < supportedMediaTypesInternal.Count; i++)
				{
					MediaTypeHeaderValue mediaTypeHeaderValue = supportedMediaTypesInternal[i];
					if (mediaTypeHeaderValue != null)
					{
						double? quality = mediaTypeWithQualityHeaderValue.Quality;
						MediaTypeHeaderValueRange mediaTypeHeaderValueRange;
						if ((quality.GetValueOrDefault() != 0.0 || quality == null) && mediaTypeHeaderValue.IsSubsetOf(mediaTypeWithQualityHeaderValue, out mediaTypeHeaderValueRange))
						{
							MediaTypeFormatterMatchRanking ranking;
							switch (mediaTypeHeaderValueRange)
							{
							case MediaTypeHeaderValueRange.SubtypeMediaRange:
								ranking = MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderSubtypeMediaRange;
								break;
							case MediaTypeHeaderValueRange.AllMediaRange:
								ranking = MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderAllMediaRange;
								break;
							default:
								ranking = MediaTypeFormatterMatchRanking.MatchOnRequestAcceptHeaderLiteral;
								break;
							}
							return new MediaTypeFormatterMatch(formatter, mediaTypeHeaderValue, mediaTypeWithQualityHeaderValue.Quality, ranking);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009184 File Offset: 0x00007384
		protected virtual MediaTypeFormatterMatch MatchRequestMediaType(HttpRequestMessage request, MediaTypeFormatter formatter)
		{
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			if (request.Content != null)
			{
				MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;
				if (contentType != null)
				{
					List<MediaTypeHeaderValue> supportedMediaTypesInternal = formatter.SupportedMediaTypesInternal;
					for (int i = 0; i < supportedMediaTypesInternal.Count; i++)
					{
						MediaTypeHeaderValue mediaTypeHeaderValue = supportedMediaTypesInternal[i];
						if (mediaTypeHeaderValue != null && mediaTypeHeaderValue.IsSubsetOf(contentType))
						{
							return new MediaTypeFormatterMatch(formatter, mediaTypeHeaderValue, new double?(1.0), MediaTypeFormatterMatchRanking.MatchOnRequestMediaType);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000920D File Offset: 0x0000740D
		protected virtual bool ShouldMatchOnType(IEnumerable<MediaTypeWithQualityHeaderValue> sortedAcceptValues)
		{
			if (sortedAcceptValues == null)
			{
				throw Error.ArgumentNull("sortedAcceptValues");
			}
			return !this.ExcludeMatchOnTypeOnly || !sortedAcceptValues.Any<MediaTypeWithQualityHeaderValue>();
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009230 File Offset: 0x00007430
		protected virtual MediaTypeFormatterMatch MatchType(Type type, MediaTypeFormatter formatter)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			MediaTypeHeaderValue mediaType = null;
			List<MediaTypeHeaderValue> supportedMediaTypesInternal = formatter.SupportedMediaTypesInternal;
			if (supportedMediaTypesInternal.Count > 0)
			{
				mediaType = supportedMediaTypesInternal[0];
			}
			return new MediaTypeFormatterMatch(formatter, mediaType, new double?(1.0), MediaTypeFormatterMatchRanking.MatchOnCanWriteType);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009294 File Offset: 0x00007494
		protected virtual IEnumerable<MediaTypeWithQualityHeaderValue> SortMediaTypeWithQualityHeaderValuesByQFactor(ICollection<MediaTypeWithQualityHeaderValue> headerValues)
		{
			if (headerValues == null)
			{
				throw Error.ArgumentNull("headerValues");
			}
			if (headerValues.Count > 1)
			{
				return headerValues.OrderByDescending((MediaTypeWithQualityHeaderValue m) => m, MediaTypeWithQualityHeaderValueComparer.QualityComparer).ToArray<MediaTypeWithQualityHeaderValue>();
			}
			return headerValues;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000092EC File Offset: 0x000074EC
		protected virtual IEnumerable<StringWithQualityHeaderValue> SortStringWithQualityHeaderValuesByQFactor(ICollection<StringWithQualityHeaderValue> headerValues)
		{
			if (headerValues == null)
			{
				throw Error.ArgumentNull("headerValues");
			}
			if (headerValues.Count > 1)
			{
				return headerValues.OrderByDescending((StringWithQualityHeaderValue m) => m, StringWithQualityHeaderValueComparer.QualityComparer).ToArray<StringWithQualityHeaderValue>();
			}
			return headerValues;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000933F File Offset: 0x0000753F
		protected virtual MediaTypeFormatterMatch UpdateBestMatch(MediaTypeFormatterMatch current, MediaTypeFormatterMatch potentialReplacement)
		{
			if (potentialReplacement == null)
			{
				return current;
			}
			if (current == null)
			{
				return potentialReplacement;
			}
			if (potentialReplacement.Quality <= current.Quality)
			{
				return current;
			}
			return potentialReplacement;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000935C File Offset: 0x0000755C
		private static MediaTypeFormatter[] GetWritingFormatters(IEnumerable<MediaTypeFormatter> formatters)
		{
			MediaTypeFormatterCollection mediaTypeFormatterCollection = formatters as MediaTypeFormatterCollection;
			if (mediaTypeFormatterCollection != null)
			{
				return mediaTypeFormatterCollection.WritingFormatters;
			}
			return formatters.AsArray<MediaTypeFormatter>();
		}
	}
}
