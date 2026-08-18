using System;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000065 RID: 101
	internal class InternetMessageFormatHeaderParser
	{
		// Token: 0x06000376 RID: 886 RVA: 0x0000E1B7 File Offset: 0x0000C3B7
		public InternetMessageFormatHeaderParser(HttpHeaders headers, int maxHeaderSize) : this(headers, maxHeaderSize, false)
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000E1C4 File Offset: 0x0000C3C4
		public InternetMessageFormatHeaderParser(HttpHeaders headers, int maxHeaderSize, bool ignoreHeaderValidation)
		{
			if (maxHeaderSize < 2)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxHeaderSize", maxHeaderSize, 2);
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			this._headers = headers;
			this._maxHeaderSize = maxHeaderSize;
			this._ignoreHeaderValidation = ignoreHeaderValidation;
			this._currentHeader = new InternetMessageFormatHeaderParser.CurrentHeaderFieldStore();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000E220 File Offset: 0x0000C420
		public ParserState ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			ParserState result = ParserState.NeedMoreData;
			if (bytesConsumed >= bytesReady)
			{
				return result;
			}
			try
			{
				result = InternetMessageFormatHeaderParser.ParseHeaderFields(buffer, bytesReady, ref bytesConsumed, ref this._headerState, this._maxHeaderSize, ref this._totalBytesConsumed, this._currentHeader, this._headers, this._ignoreHeaderValidation);
			}
			catch (Exception)
			{
				result = ParserState.Invalid;
			}
			return result;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000E28C File Offset: 0x0000C48C
		private static ParserState ParseHeaderFields(byte[] buffer, int bytesReady, ref int bytesConsumed, ref InternetMessageFormatHeaderParser.HeaderFieldState requestHeaderState, int maximumHeaderLength, ref int totalBytesConsumed, InternetMessageFormatHeaderParser.CurrentHeaderFieldStore currentField, HttpHeaders headers, bool ignoreHeaderValidation)
		{
			int num = bytesConsumed;
			ParserState result = ParserState.DataTooBig;
			int num2 = (maximumHeaderLength <= 0) ? int.MaxValue : (maximumHeaderLength - totalBytesConsumed + num);
			if (bytesReady < num2)
			{
				result = ParserState.NeedMoreData;
				num2 = bytesReady;
			}
			switch (requestHeaderState)
			{
			case InternetMessageFormatHeaderParser.HeaderFieldState.Name:
				break;
			case InternetMessageFormatHeaderParser.HeaderFieldState.Value:
				goto IL_EE;
			case InternetMessageFormatHeaderParser.HeaderFieldState.AfterCarriageReturn:
				goto IL_166;
			case InternetMessageFormatHeaderParser.HeaderFieldState.FoldingLine:
				goto IL_196;
			default:
				goto IL_1E1;
			}
			IL_42:
			int num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 58)
			{
				if (buffer[bytesConsumed] == 13)
				{
					if (!currentField.IsEmpty())
					{
						result = ParserState.Invalid;
						goto IL_1E1;
					}
					requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.AfterCarriageReturn;
					if (++bytesConsumed == num2)
					{
						goto IL_1E1;
					}
					goto IL_166;
				}
				else if (++bytesConsumed == num2)
				{
					string @string = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentField.Name.Append(@string);
					goto IL_1E1;
				}
			}
			if (bytesConsumed > num3)
			{
				string string2 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentField.Name.Append(string2);
			}
			requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.Value;
			if (++bytesConsumed == num2)
			{
				goto IL_1E1;
			}
			IL_EE:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 13)
			{
				if (++bytesConsumed == num2)
				{
					string string3 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentField.Value.Append(string3);
					goto IL_1E1;
				}
			}
			if (bytesConsumed > num3)
			{
				string string4 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentField.Value.Append(string4);
			}
			requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.AfterCarriageReturn;
			if (++bytesConsumed == num2)
			{
				goto IL_1E1;
			}
			IL_166:
			if (buffer[bytesConsumed] != 10)
			{
				result = ParserState.Invalid;
				goto IL_1E1;
			}
			if (currentField.IsEmpty())
			{
				result = ParserState.Done;
				bytesConsumed++;
				goto IL_1E1;
			}
			requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.FoldingLine;
			if (++bytesConsumed == num2)
			{
				goto IL_1E1;
			}
			IL_196:
			if (buffer[bytesConsumed] != 32 && buffer[bytesConsumed] != 9)
			{
				currentField.CopyTo(headers, ignoreHeaderValidation);
				requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.Name;
				if (bytesConsumed != num2)
				{
					goto IL_42;
				}
			}
			else
			{
				currentField.Value.Append(' ');
				requestHeaderState = InternetMessageFormatHeaderParser.HeaderFieldState.Value;
				if (++bytesConsumed != num2)
				{
					goto IL_EE;
				}
			}
			IL_1E1:
			totalBytesConsumed += bytesConsumed - num;
			return result;
		}

		// Token: 0x04000131 RID: 305
		internal const int MinHeaderSize = 2;

		// Token: 0x04000132 RID: 306
		private int _totalBytesConsumed;

		// Token: 0x04000133 RID: 307
		private int _maxHeaderSize;

		// Token: 0x04000134 RID: 308
		private InternetMessageFormatHeaderParser.HeaderFieldState _headerState;

		// Token: 0x04000135 RID: 309
		private HttpHeaders _headers;

		// Token: 0x04000136 RID: 310
		private InternetMessageFormatHeaderParser.CurrentHeaderFieldStore _currentHeader;

		// Token: 0x04000137 RID: 311
		private readonly bool _ignoreHeaderValidation;

		// Token: 0x02000066 RID: 102
		private enum HeaderFieldState
		{
			// Token: 0x04000139 RID: 313
			Name,
			// Token: 0x0400013A RID: 314
			Value,
			// Token: 0x0400013B RID: 315
			AfterCarriageReturn,
			// Token: 0x0400013C RID: 316
			FoldingLine
		}

		// Token: 0x02000067 RID: 103
		private class CurrentHeaderFieldStore
		{
			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x0600037A RID: 890 RVA: 0x0000E485 File Offset: 0x0000C685
			public StringBuilder Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x0600037B RID: 891 RVA: 0x0000E48D File Offset: 0x0000C68D
			public StringBuilder Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x0600037C RID: 892 RVA: 0x0000E498 File Offset: 0x0000C698
			public void CopyTo(HttpHeaders headers, bool ignoreHeaderValidation)
			{
				string name = this._name.ToString();
				string value = this._value.ToString().Trim(InternetMessageFormatHeaderParser.CurrentHeaderFieldStore._linearWhiteSpace);
				if (ignoreHeaderValidation)
				{
					headers.TryAddWithoutValidation(name, value);
				}
				else
				{
					headers.Add(name, value);
				}
				this.Clear();
			}

			// Token: 0x0600037D RID: 893 RVA: 0x0000E4E3 File Offset: 0x0000C6E3
			public bool IsEmpty()
			{
				return this._name.Length == 0 && this._value.Length == 0;
			}

			// Token: 0x0600037E RID: 894 RVA: 0x0000E502 File Offset: 0x0000C702
			private void Clear()
			{
				this._name.Clear();
				this._value.Clear();
			}

			// Token: 0x0400013D RID: 317
			private const int DefaultFieldNameAllocation = 128;

			// Token: 0x0400013E RID: 318
			private const int DefaultFieldValueAllocation = 2048;

			// Token: 0x0400013F RID: 319
			private static readonly char[] _linearWhiteSpace = new char[]
			{
				' ',
				'\t'
			};

			// Token: 0x04000140 RID: 320
			private readonly StringBuilder _name = new StringBuilder(128);

			// Token: 0x04000141 RID: 321
			private readonly StringBuilder _value = new StringBuilder(2048);
		}
	}
}
