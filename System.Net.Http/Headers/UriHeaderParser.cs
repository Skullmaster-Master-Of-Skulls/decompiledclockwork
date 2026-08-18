using System;

namespace System.Net.Http.Headers
{
	// Token: 0x02000049 RID: 73
	internal class UriHeaderParser : HttpHeaderParser
	{
		// Token: 0x060003EC RID: 1004 RVA: 0x0000EA73 File Offset: 0x0000CC73
		private UriHeaderParser(UriKind uriKind) : base(false)
		{
			this.uriKind = uriKind;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public override bool TryParseValue(string value, object storeValue, ref int index, out object parsedValue)
		{
			parsedValue = null;
			if (string.IsNullOrEmpty(value) || index == value.Length)
			{
				return false;
			}
			string text = value;
			if (index > 0)
			{
				text = value.Substring(index);
			}
			Uri uri;
			if (!Uri.TryCreate(text, this.uriKind, out uri))
			{
				text = WebHeaderCollection.HeaderEncoding.DecodeUtf8FromString(text);
				if (!Uri.TryCreate(text, this.uriKind, out uri))
				{
					return false;
				}
			}
			index = value.Length;
			parsedValue = uri;
			return true;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		public override string ToString(object value)
		{
			Uri uri = (Uri)value;
			if (uri.IsAbsoluteUri)
			{
				return uri.AbsoluteUri;
			}
			return uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped);
		}

		// Token: 0x04000182 RID: 386
		private UriKind uriKind;

		// Token: 0x04000183 RID: 387
		internal static readonly UriHeaderParser RelativeOrAbsoluteUriParser = new UriHeaderParser(UriKind.RelativeOrAbsolute);
	}
}
