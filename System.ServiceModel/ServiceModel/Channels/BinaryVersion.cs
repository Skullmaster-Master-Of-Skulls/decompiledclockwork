using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009DB RID: 2523
	internal class BinaryVersion
	{
		// Token: 0x060063BD RID: 25533 RVA: 0x00174A36 File Offset: 0x00172C36
		private BinaryVersion(string contentType, string sessionContentType, IXmlDictionary dictionary)
		{
			this.contentType = contentType;
			this.sessionContentType = sessionContentType;
			this.dictionary = dictionary;
		}

		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x060063BE RID: 25534 RVA: 0x00174A53 File Offset: 0x00172C53
		public static BinaryVersion CurrentVersion
		{
			get
			{
				return BinaryVersion.Version1;
			}
		}

		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x060063BF RID: 25535 RVA: 0x00174A5A File Offset: 0x00172C5A
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x060063C0 RID: 25536 RVA: 0x00174A62 File Offset: 0x00172C62
		public string SessionContentType
		{
			get
			{
				return this.sessionContentType;
			}
		}

		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x060063C1 RID: 25537 RVA: 0x00174A6A File Offset: 0x00172C6A
		public IXmlDictionary Dictionary
		{
			get
			{
				return this.dictionary;
			}
		}

		// Token: 0x04003991 RID: 14737
		public static readonly BinaryVersion Version1 = new BinaryVersion("application/soap+msbin1", "application/soap+msbinsession1", ServiceModelDictionary.Version1);

		// Token: 0x04003992 RID: 14738
		public static readonly BinaryVersion GZipVersion1 = new BinaryVersion("application/soap+msbin1+gzip", "application/soap+msbinsession1+gzip", ServiceModelDictionary.Version1);

		// Token: 0x04003993 RID: 14739
		public static readonly BinaryVersion DeflateVersion1 = new BinaryVersion("application/soap+msbin1+deflate", "application/soap+msbinsession1+deflate", ServiceModelDictionary.Version1);

		// Token: 0x04003994 RID: 14740
		private string contentType;

		// Token: 0x04003995 RID: 14741
		private string sessionContentType;

		// Token: 0x04003996 RID: 14742
		private IXmlDictionary dictionary;
	}
}
