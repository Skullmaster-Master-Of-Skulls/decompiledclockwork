using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x02000271 RID: 625
	internal class MailWriter : BaseWriter
	{
		// Token: 0x0600178F RID: 6031 RVA: 0x000781C8 File Offset: 0x000763C8
		internal MailWriter(Stream stream) : base(stream, true)
		{
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000781D4 File Offset: 0x000763D4
		internal override void WriteHeaders(NameValueCollection headers, bool allowUnicode)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			foreach (object obj in headers)
			{
				string name = (string)obj;
				string[] values = headers.GetValues(name);
				foreach (string value in values)
				{
					base.WriteHeader(name, value, allowUnicode);
				}
			}
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00078260 File Offset: 0x00076460
		internal override void Close()
		{
			this.bufferBuilder.Append(BaseWriter.CRLF);
			base.Flush(null);
			this.stream.Close();
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00078284 File Offset: 0x00076484
		protected override void OnClose(object sender, EventArgs args)
		{
			this.contentStream.Flush();
			this.contentStream = null;
		}
	}
}
