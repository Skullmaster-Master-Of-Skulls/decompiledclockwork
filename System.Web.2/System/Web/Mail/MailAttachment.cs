using System;
using System.IO;

namespace System.Web.Mail
{
	// Token: 0x0200011A RID: 282
	[Obsolete("The recommended alternative is System.Net.Mail.Attachment. http://go.microsoft.com/fwlink/?linkid=14202")]
	public class MailAttachment
	{
		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00030858 File Offset: 0x0002EA58
		public string Filename
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x00030860 File Offset: 0x0002EA60
		public MailEncoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00030868 File Offset: 0x0002EA68
		public MailAttachment(string filename)
		{
			this._filename = filename;
			this._encoding = MailEncoding.Base64;
			this.VerifyFile();
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00030884 File Offset: 0x0002EA84
		public MailAttachment(string filename, MailEncoding encoding)
		{
			this._filename = filename;
			this._encoding = encoding;
			this.VerifyFile();
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000308A0 File Offset: 0x0002EAA0
		private void VerifyFile()
		{
			try
			{
				File.Open(this._filename, FileMode.Open, FileAccess.Read, FileShare.Read).Close();
			}
			catch
			{
				throw new HttpException(SR.GetString("Bad_attachment", new object[]
				{
					this._filename
				}));
			}
		}

		// Token: 0x040013C9 RID: 5065
		private string _filename;

		// Token: 0x040013CA RID: 5066
		private MailEncoding _encoding;
	}
}
