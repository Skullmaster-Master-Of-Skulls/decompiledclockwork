using System;
using System.Text;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009E3 RID: 2531
	internal class MtomMessageEncoderFactory : MessageEncoderFactory
	{
		// Token: 0x060063E7 RID: 25575 RVA: 0x00175052 File Offset: 0x00173252
		public MtomMessageEncoderFactory(MessageVersion version, Encoding writeEncoding, int maxReadPoolSize, int maxWritePoolSize, int maxBufferSize, XmlDictionaryReaderQuotas quotas)
		{
			this.messageEncoder = new MtomMessageEncoder(version, writeEncoding, maxReadPoolSize, maxWritePoolSize, maxBufferSize, quotas);
		}

		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x060063E8 RID: 25576 RVA: 0x0017506E File Offset: 0x0017326E
		public override MessageEncoder Encoder
		{
			get
			{
				return this.messageEncoder;
			}
		}

		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x060063E9 RID: 25577 RVA: 0x00175076 File Offset: 0x00173276
		public override MessageVersion MessageVersion
		{
			get
			{
				return this.messageEncoder.MessageVersion;
			}
		}

		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x060063EA RID: 25578 RVA: 0x00175083 File Offset: 0x00173283
		public int MaxWritePoolSize
		{
			get
			{
				return this.messageEncoder.MaxWritePoolSize;
			}
		}

		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x060063EB RID: 25579 RVA: 0x00175090 File Offset: 0x00173290
		public int MaxReadPoolSize
		{
			get
			{
				return this.messageEncoder.MaxReadPoolSize;
			}
		}

		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x060063EC RID: 25580 RVA: 0x0017509D File Offset: 0x0017329D
		public XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return this.messageEncoder.ReaderQuotas;
			}
		}

		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x060063ED RID: 25581 RVA: 0x001750AA File Offset: 0x001732AA
		public int MaxBufferSize
		{
			get
			{
				return this.messageEncoder.MaxBufferSize;
			}
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x001750B8 File Offset: 0x001732B8
		public static Encoding[] GetSupportedEncodings()
		{
			Encoding[] supportedEncodings = TextEncoderDefaults.SupportedEncodings;
			Encoding[] array = new Encoding[supportedEncodings.Length];
			Array.Copy(supportedEncodings, array, supportedEncodings.Length);
			return array;
		}

		// Token: 0x0400399F RID: 14751
		private MtomMessageEncoder messageEncoder;
	}
}
