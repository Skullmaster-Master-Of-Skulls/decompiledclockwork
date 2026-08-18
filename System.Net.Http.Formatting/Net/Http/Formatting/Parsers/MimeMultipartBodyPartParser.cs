using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000069 RID: 105
	internal class MimeMultipartBodyPartParser : IDisposable
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0000E929 File Offset: 0x0000CB29
		public MimeMultipartBodyPartParser(HttpContent content, MultipartStreamProvider streamProvider) : this(content, streamProvider, long.MaxValue, 4096)
		{
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000E944 File Offset: 0x0000CB44
		public MimeMultipartBodyPartParser(HttpContent content, MultipartStreamProvider streamProvider, long maxMessageSize, int maxBodyPartHeaderSize)
		{
			string boundary = MimeMultipartBodyPartParser.ValidateArguments(content, maxMessageSize, true);
			this._mimeParser = new MimeMultipartParser(boundary, maxMessageSize);
			this._currentBodyPart = new MimeBodyPart(streamProvider, maxBodyPartHeaderSize, content);
			this._content = content;
			this._maxBodyPartHeaderSize = maxBodyPartHeaderSize;
			this._streamProvider = streamProvider;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000E9A8 File Offset: 0x0000CBA8
		public static bool IsMimeMultipartContent(HttpContent content)
		{
			bool result;
			try
			{
				string text = MimeMultipartBodyPartParser.ValidateArguments(content, long.MaxValue, false);
				result = (text != null);
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000E9E8 File Offset: 0x0000CBE8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000EE14 File Offset: 0x0000D014
		public IEnumerable<MimeBodyPart> ParseBuffer(byte[] data, int bytesRead)
		{
			int bytesConsumed = 0;
			bool isFinal = false;
			if (bytesRead == 0 && !this._mimeParser.IsWaitingForEndOfMessage)
			{
				this.CleanupCurrentBodyPart();
				throw new IOException(Resources.ReadAsMimeMultipartUnexpectedTermination);
			}
			this._currentBodyPart.Segments.Clear();
			while (this._mimeParser.CanParseMore(bytesRead, bytesConsumed))
			{
				this._mimeStatus = this._mimeParser.ParseBuffer(data, bytesRead, ref bytesConsumed, out this._parsedBodyPart[0], out this._parsedBodyPart[1], out isFinal);
				if (this._mimeStatus != MimeMultipartParser.State.BodyPartCompleted && this._mimeStatus != MimeMultipartParser.State.NeedMoreData)
				{
					this.CleanupCurrentBodyPart();
					throw Error.InvalidOperation(Resources.ReadAsMimeMultipartParseError, new object[]
					{
						bytesConsumed,
						data
					});
				}
				if (this._isFirst)
				{
					if (this._mimeStatus == MimeMultipartParser.State.BodyPartCompleted)
					{
						this._isFirst = false;
					}
				}
				else
				{
					foreach (ArraySegment<byte> item in this._parsedBodyPart)
					{
						if (item.Count != 0)
						{
							if (this._bodyPartHeaderStatus != ParserState.Done)
							{
								int offset = item.Offset;
								this._bodyPartHeaderStatus = this._currentBodyPart.HeaderParser.ParseBuffer(item.Array, item.Count + item.Offset, ref offset);
								if (this._bodyPartHeaderStatus == ParserState.Done)
								{
									this._currentBodyPart.Segments.Add(new ArraySegment<byte>(item.Array, offset, item.Count + item.Offset - offset));
								}
								else if (this._bodyPartHeaderStatus != ParserState.NeedMoreData)
								{
									this.CleanupCurrentBodyPart();
									throw Error.InvalidOperation(Resources.ReadAsMimeMultipartHeaderParseError, new object[]
									{
										offset,
										item.Array
									});
								}
							}
							else
							{
								this._currentBodyPart.Segments.Add(item);
							}
						}
					}
					if (this._mimeStatus == MimeMultipartParser.State.BodyPartCompleted)
					{
						MimeBodyPart completed = this._currentBodyPart;
						completed.IsComplete = true;
						completed.IsFinal = isFinal;
						this._currentBodyPart = new MimeBodyPart(this._streamProvider, this._maxBodyPartHeaderSize, this._content);
						this._mimeStatus = MimeMultipartParser.State.NeedMoreData;
						this._bodyPartHeaderStatus = ParserState.NeedMoreData;
						yield return completed;
					}
					else
					{
						yield return this._currentBodyPart;
					}
				}
			}
			yield break;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000EE3F File Offset: 0x0000D03F
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._mimeParser = null;
				this.CleanupCurrentBodyPart();
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000EE54 File Offset: 0x0000D054
		private static string ValidateArguments(HttpContent content, long maxMessageSize, bool throwOnError)
		{
			if (maxMessageSize < 10L)
			{
				if (throwOnError)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxMessageSize", maxMessageSize, 10);
				}
				return null;
			}
			else
			{
				MediaTypeHeaderValue contentType = content.Headers.ContentType;
				if (contentType == null)
				{
					if (throwOnError)
					{
						throw Error.Argument("content", Resources.ReadAsMimeMultipartArgumentNoContentType, new object[]
						{
							typeof(HttpContent).Name,
							"multipart/"
						});
					}
					return null;
				}
				else if (!contentType.MediaType.StartsWith("multipart", StringComparison.OrdinalIgnoreCase))
				{
					if (throwOnError)
					{
						throw Error.Argument("content", Resources.ReadAsMimeMultipartArgumentNoMultipart, new object[]
						{
							typeof(HttpContent).Name,
							"multipart/"
						});
					}
					return null;
				}
				else
				{
					string text = null;
					foreach (NameValueHeaderValue nameValueHeaderValue in contentType.Parameters)
					{
						if (nameValueHeaderValue.Name.Equals("boundary", StringComparison.OrdinalIgnoreCase))
						{
							text = FormattingUtilities.UnquoteToken(nameValueHeaderValue.Value);
							break;
						}
					}
					if (text != null)
					{
						return text;
					}
					if (throwOnError)
					{
						throw Error.Argument("content", Resources.ReadAsMimeMultipartArgumentNoBoundary, new object[]
						{
							typeof(HttpContent).Name,
							"multipart",
							"boundary"
						});
					}
					return null;
				}
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000EFC4 File Offset: 0x0000D1C4
		private void CleanupCurrentBodyPart()
		{
			if (this._currentBodyPart != null)
			{
				this._currentBodyPart.Dispose();
				this._currentBodyPart = null;
			}
		}

		// Token: 0x0400014C RID: 332
		internal const long DefaultMaxMessageSize = 9223372036854775807L;

		// Token: 0x0400014D RID: 333
		private const int DefaultMaxBodyPartHeaderSize = 4096;

		// Token: 0x0400014E RID: 334
		private MimeMultipartParser _mimeParser;

		// Token: 0x0400014F RID: 335
		private MimeMultipartParser.State _mimeStatus;

		// Token: 0x04000150 RID: 336
		private ArraySegment<byte>[] _parsedBodyPart = new ArraySegment<byte>[2];

		// Token: 0x04000151 RID: 337
		private MimeBodyPart _currentBodyPart;

		// Token: 0x04000152 RID: 338
		private bool _isFirst = true;

		// Token: 0x04000153 RID: 339
		private ParserState _bodyPartHeaderStatus;

		// Token: 0x04000154 RID: 340
		private int _maxBodyPartHeaderSize;

		// Token: 0x04000155 RID: 341
		private MultipartStreamProvider _streamProvider;

		// Token: 0x04000156 RID: 342
		private HttpContent _content;
	}
}
