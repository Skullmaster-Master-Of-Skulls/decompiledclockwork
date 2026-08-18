using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
	// Token: 0x02000006 RID: 6
	[__DynamicallyInvokable]
	public class FormUrlEncodedContent : ByteArrayContent
	{
		// Token: 0x0600003A RID: 58 RVA: 0x0000252E File Offset: 0x0000072E
		[__DynamicallyInvokable]
		public FormUrlEncodedContent(IEnumerable<KeyValuePair<string, string>> nameValueCollection) : base(FormUrlEncodedContent.GetContentByteArray(nameValueCollection))
		{
			base.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002554 File Offset: 0x00000754
		private static byte[] GetContentByteArray(IEnumerable<KeyValuePair<string, string>> nameValueCollection)
		{
			if (nameValueCollection == null)
			{
				throw new ArgumentNullException("nameValueCollection");
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, string> keyValuePair in nameValueCollection)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append('&');
				}
				stringBuilder.Append(FormUrlEncodedContent.Encode(keyValuePair.Key));
				stringBuilder.Append('=');
				stringBuilder.Append(FormUrlEncodedContent.Encode(keyValuePair.Value));
			}
			return HttpRuleParser.DefaultHttpEncoding.GetBytes(stringBuilder.ToString());
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000025FC File Offset: 0x000007FC
		private static string Encode(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				return string.Empty;
			}
			return Uri.EscapeDataString(data).Replace("%20", "+");
		}
	}
}
