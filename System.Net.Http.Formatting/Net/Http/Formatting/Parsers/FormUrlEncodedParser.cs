using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000051 RID: 81
	internal class FormUrlEncodedParser
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0000B0FC File Offset: 0x000092FC
		public FormUrlEncodedParser(ICollection<KeyValuePair<string, string>> nameValuePairs, long maxMessageSize)
		{
			if (maxMessageSize < 1L)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxMessageSize", maxMessageSize, 1);
			}
			if (nameValuePairs == null)
			{
				throw Error.ArgumentNull("nameValuePairs");
			}
			this._nameValuePairs = nameValuePairs;
			this._maxMessageSize = maxMessageSize;
			this._currentNameValuePair = new FormUrlEncodedParser.CurrentNameValuePair();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000B154 File Offset: 0x00009354
		public ParserState ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed, bool isFinal)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			ParserState parserState = ParserState.NeedMoreData;
			if (bytesConsumed >= bytesReady)
			{
				if (isFinal)
				{
					parserState = this.CopyCurrent(parserState);
				}
				return parserState;
			}
			try
			{
				parserState = FormUrlEncodedParser.ParseNameValuePairs(buffer, bytesReady, ref bytesConsumed, ref this._nameValueState, this._maxMessageSize, ref this._totalBytesConsumed, this._currentNameValuePair, this._nameValuePairs);
				if (isFinal)
				{
					parserState = this.CopyCurrent(parserState);
				}
			}
			catch (Exception)
			{
				parserState = ParserState.Invalid;
			}
			return parserState;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000B1D0 File Offset: 0x000093D0
		private static ParserState ParseNameValuePairs(byte[] buffer, int bytesReady, ref int bytesConsumed, ref FormUrlEncodedParser.NameValueState nameValueState, long maximumLength, ref long totalBytesConsumed, FormUrlEncodedParser.CurrentNameValuePair currentNameValuePair, ICollection<KeyValuePair<string, string>> nameValuePairs)
		{
			int num = bytesConsumed;
			ParserState result = ParserState.DataTooBig;
			long num2 = (maximumLength <= 0L) ? long.MaxValue : (maximumLength - totalBytesConsumed + (long)num);
			if ((long)bytesReady < num2)
			{
				result = ParserState.NeedMoreData;
				num2 = (long)bytesReady;
			}
			switch (nameValueState)
			{
			case FormUrlEncodedParser.NameValueState.Name:
				break;
			case FormUrlEncodedParser.NameValueState.Value:
				goto IL_F1;
			default:
				goto IL_174;
			}
			int num3;
			for (;;)
			{
				IL_42:
				num3 = bytesConsumed;
				while (buffer[bytesConsumed] != 61 && buffer[bytesConsumed] != 38)
				{
					if ((long)(++bytesConsumed) == num2)
					{
						goto Block_4;
					}
				}
				if (bytesConsumed > num3)
				{
					string @string = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentNameValuePair.Name.Append(@string);
				}
				if (buffer[bytesConsumed] == 61)
				{
					goto Block_7;
				}
				currentNameValuePair.CopyNameOnlyTo(nameValuePairs);
				if ((long)(++bytesConsumed) == num2)
				{
					goto Block_9;
				}
			}
			Block_4:
			string string2 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
			currentNameValuePair.Name.Append(string2);
			goto IL_174;
			Block_7:
			nameValueState = FormUrlEncodedParser.NameValueState.Value;
			if ((long)(++bytesConsumed) != num2)
			{
				goto IL_F1;
			}
			Block_9:
			goto IL_174;
			IL_F1:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 38)
			{
				if ((long)(++bytesConsumed) == num2)
				{
					string string3 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentNameValuePair.Value.Append(string3);
					goto IL_174;
				}
			}
			if (bytesConsumed > num3)
			{
				string string4 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentNameValuePair.Value.Append(string4);
			}
			currentNameValuePair.CopyTo(nameValuePairs);
			nameValueState = FormUrlEncodedParser.NameValueState.Name;
			if ((long)(++bytesConsumed) != num2)
			{
				goto IL_42;
			}
			IL_174:
			totalBytesConsumed += (long)(bytesConsumed - num);
			return result;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000B35D File Offset: 0x0000955D
		private ParserState CopyCurrent(ParserState parseState)
		{
			if (this._nameValueState == FormUrlEncodedParser.NameValueState.Name)
			{
				if (this._totalBytesConsumed > 0L)
				{
					this._currentNameValuePair.CopyNameOnlyTo(this._nameValuePairs);
				}
			}
			else
			{
				this._currentNameValuePair.CopyTo(this._nameValuePairs);
			}
			if (parseState != ParserState.NeedMoreData)
			{
				return parseState;
			}
			return ParserState.Done;
		}

		// Token: 0x040000D3 RID: 211
		private const int MinMessageSize = 1;

		// Token: 0x040000D4 RID: 212
		private long _totalBytesConsumed;

		// Token: 0x040000D5 RID: 213
		private long _maxMessageSize;

		// Token: 0x040000D6 RID: 214
		private FormUrlEncodedParser.NameValueState _nameValueState;

		// Token: 0x040000D7 RID: 215
		private ICollection<KeyValuePair<string, string>> _nameValuePairs;

		// Token: 0x040000D8 RID: 216
		private readonly FormUrlEncodedParser.CurrentNameValuePair _currentNameValuePair;

		// Token: 0x02000052 RID: 82
		private enum NameValueState
		{
			// Token: 0x040000DA RID: 218
			Name,
			// Token: 0x040000DB RID: 219
			Value
		}

		// Token: 0x02000053 RID: 83
		private class CurrentNameValuePair
		{
			// Token: 0x170000BE RID: 190
			// (get) Token: 0x06000303 RID: 771 RVA: 0x0000B39B File Offset: 0x0000959B
			public StringBuilder Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x170000BF RID: 191
			// (get) Token: 0x06000304 RID: 772 RVA: 0x0000B3A3 File Offset: 0x000095A3
			public StringBuilder Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x06000305 RID: 773 RVA: 0x0000B3AC File Offset: 0x000095AC
			public void CopyTo(ICollection<KeyValuePair<string, string>> nameValuePairs)
			{
				string key = UriQueryUtility.UrlDecode(this._name.ToString());
				string str = this._value.ToString();
				string value = UriQueryUtility.UrlDecode(str);
				nameValuePairs.Add(new KeyValuePair<string, string>(key, value));
				this.Clear();
			}

			// Token: 0x06000306 RID: 774 RVA: 0x0000B3F0 File Offset: 0x000095F0
			public void CopyNameOnlyTo(ICollection<KeyValuePair<string, string>> nameValuePairs)
			{
				string key = UriQueryUtility.UrlDecode(this._name.ToString());
				string empty = string.Empty;
				nameValuePairs.Add(new KeyValuePair<string, string>(key, empty));
				this.Clear();
			}

			// Token: 0x06000307 RID: 775 RVA: 0x0000B427 File Offset: 0x00009627
			private void Clear()
			{
				this._name.Clear();
				this._value.Clear();
			}

			// Token: 0x040000DC RID: 220
			private const int DefaultNameAllocation = 128;

			// Token: 0x040000DD RID: 221
			private const int DefaultValueAllocation = 2048;

			// Token: 0x040000DE RID: 222
			private readonly StringBuilder _name = new StringBuilder(128);

			// Token: 0x040000DF RID: 223
			private readonly StringBuilder _value = new StringBuilder(2048);
		}
	}
}
