using System;
using System.IO;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007ED RID: 2029
	internal class ViaStringDecoder : StringDecoder
	{
		// Token: 0x06004CB9 RID: 19641 RVA: 0x00117F70 File Offset: 0x00116170
		public ViaStringDecoder(int sizeQuota) : base(sizeQuota)
		{
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x00117F7C File Offset: 0x0011617C
		protected override Exception OnSizeQuotaExceeded(int size)
		{
			Exception ex = new InvalidDataException(SR.GetString("FramingViaTooLong", new object[]
			{
				size
			}));
			FramingEncodingString.AddFaultString(ex, "http://schemas.microsoft.com/ws/2006/05/framing/faults/ViaTooLong");
			return ex;
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x00117FB4 File Offset: 0x001161B4
		protected override void OnComplete(string value)
		{
			try
			{
				this.via = new Uri(value);
				base.OnComplete(value);
			}
			catch (UriFormatException innerException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataException(SR.GetString("FramingViaNotUri", new object[]
				{
					value
				}), innerException));
			}
		}

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06004CBC RID: 19644 RVA: 0x0011800C File Offset: 0x0011620C
		public Uri ValueAsUri
		{
			get
			{
				if (!base.IsValueDecoded)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("FramingValueNotAvailable")));
				}
				return this.via;
			}
		}

		// Token: 0x04002FC9 RID: 12233
		private Uri via;
	}
}
