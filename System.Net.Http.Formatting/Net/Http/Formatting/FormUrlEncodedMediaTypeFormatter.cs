using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Formatting.Parsers;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000042 RID: 66
	public class FormUrlEncodedMediaTypeFormatter : MediaTypeFormatter
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00009380 File Offset: 0x00007580
		public FormUrlEncodedMediaTypeFormatter()
		{
			base.SupportedMediaTypes.Add(MediaTypeConstants.ApplicationFormUrlEncodedMediaType);
			this._isDerived = (base.GetType() != typeof(FormUrlEncodedMediaTypeFormatter));
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000093D4 File Offset: 0x000075D4
		protected FormUrlEncodedMediaTypeFormatter(FormUrlEncodedMediaTypeFormatter formatter) : base(formatter)
		{
			this.MaxDepth = formatter.MaxDepth;
			this.ReadBufferSize = formatter.ReadBufferSize;
			this._isDerived = (base.GetType() != typeof(FormUrlEncodedMediaTypeFormatter));
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00009431 File Offset: 0x00007631
		public static MediaTypeHeaderValue DefaultMediaType
		{
			get
			{
				return MediaTypeConstants.ApplicationFormUrlEncodedMediaType;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00009438 File Offset: 0x00007638
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00009440 File Offset: 0x00007640
		public int MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				this._maxDepth = value;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00009464 File Offset: 0x00007664
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000946C File Offset: 0x0000766C
		public int ReadBufferSize
		{
			get
			{
				return this._readBufferSize;
			}
			set
			{
				if (value < 256)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 256);
				}
				this._readBufferSize = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00009498 File Offset: 0x00007698
		internal override bool CanWriteAnyTypes
		{
			get
			{
				return this._isDerived;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000094A0 File Offset: 0x000076A0
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return type == typeof(FormDataCollection) || FormattingUtilities.IsJTokenType(type);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000094D0 File Offset: 0x000076D0
		public override bool CanWriteType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return false;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x000094E8 File Offset: 0x000076E8
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			Task<object> result;
			try
			{
				result = Task.FromResult<object>(this.ReadFromStream(type, readStream));
			}
			catch (Exception exception)
			{
				result = TaskHelpers.FromError<object>(exception);
			}
			return result;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009544 File Offset: 0x00007744
		private object ReadFromStream(Type type, Stream readStream)
		{
			IEnumerable<KeyValuePair<string, string>> enumerable = FormUrlEncodedMediaTypeFormatter.ReadFormUrlEncoded(readStream, this.ReadBufferSize);
			object result;
			if (type == typeof(FormDataCollection))
			{
				result = new FormDataCollection(enumerable);
			}
			else
			{
				if (!FormattingUtilities.IsJTokenType(type))
				{
					throw Error.InvalidOperation(Resources.SerializerCannotSerializeType, new object[]
					{
						base.GetType().Name,
						type.Name
					});
				}
				result = FormUrlEncodedJson.Parse(enumerable, this._maxDepth);
			}
			return result;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000095BC File Offset: 0x000077BC
		private static IEnumerable<KeyValuePair<string, string>> ReadFormUrlEncoded(Stream input, int bufferSize)
		{
			byte[] array = new byte[bufferSize];
			bool flag = false;
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			FormUrlEncodedParser formUrlEncodedParser = new FormUrlEncodedParser(list, long.MaxValue);
			int num2;
			for (;;)
			{
				int num;
				try
				{
					num = input.Read(array, 0, array.Length);
					if (num == 0)
					{
						flag = true;
					}
				}
				catch (Exception innerException)
				{
					throw Error.InvalidOperation(innerException, Resources.ErrorReadingFormUrlEncodedStream, new object[0]);
				}
				num2 = 0;
				ParserState parserState = formUrlEncodedParser.ParseBuffer(array, num, ref num2, flag);
				if (parserState != ParserState.NeedMoreData && parserState != ParserState.Done)
				{
					break;
				}
				if (flag)
				{
					return list;
				}
			}
			throw Error.InvalidOperation(Resources.FormUrlEncodedParseError, new object[]
			{
				num2
			});
		}

		// Token: 0x0400009F RID: 159
		private const int MinBufferSize = 256;

		// Token: 0x040000A0 RID: 160
		private const int DefaultBufferSize = 32768;

		// Token: 0x040000A1 RID: 161
		private int _readBufferSize = 32768;

		// Token: 0x040000A2 RID: 162
		private int _maxDepth = 256;

		// Token: 0x040000A3 RID: 163
		private readonly bool _isDerived;
	}
}
