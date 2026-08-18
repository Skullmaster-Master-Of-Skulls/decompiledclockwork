using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009BB RID: 2491
	internal class XmlReaderBodyWriter : BodyWriter
	{
		// Token: 0x060061DD RID: 25053 RVA: 0x0016C6F4 File Offset: 0x0016A8F4
		public XmlReaderBodyWriter(XmlDictionaryReader reader, EnvelopeVersion version) : base(false)
		{
			this.reader = reader;
			if (reader.MoveToContent() != XmlNodeType.Element)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidReaderPositionOnCreateMessage"), "reader"));
			}
			this.isFault = Message.IsFaultStartElement(reader, version);
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x060061DE RID: 25054 RVA: 0x0016C744 File Offset: 0x0016A944
		internal override bool IsFault
		{
			get
			{
				return this.isFault;
			}
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x0016C74C File Offset: 0x0016A94C
		protected override BodyWriter OnCreateBufferedCopy(int maxBufferSize)
		{
			return base.OnCreateBufferedCopy(maxBufferSize, this.reader.Quotas);
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x0016C760 File Offset: 0x0016A960
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			using (this.reader)
			{
				XmlNodeType xmlNodeType = this.reader.MoveToContent();
				while (!this.reader.EOF && xmlNodeType != XmlNodeType.EndElement)
				{
					if (xmlNodeType != XmlNodeType.Element)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("InvalidReaderPositionOnCreateMessage"), "reader"));
					}
					writer.WriteNode(this.reader, false);
					xmlNodeType = this.reader.MoveToContent();
				}
			}
		}

		// Token: 0x040038DF RID: 14559
		private XmlDictionaryReader reader;

		// Token: 0x040038E0 RID: 14560
		private bool isFault;
	}
}
