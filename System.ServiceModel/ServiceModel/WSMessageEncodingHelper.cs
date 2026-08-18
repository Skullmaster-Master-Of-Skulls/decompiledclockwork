using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x02000159 RID: 345
	internal static class WSMessageEncodingHelper
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x0002655B File Offset: 0x0002475B
		internal static bool IsDefined(WSMessageEncoding value)
		{
			return value == WSMessageEncoding.Text || value == WSMessageEncoding.Mtom;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00026566 File Offset: 0x00024766
		internal static void SyncUpEncodingBindingElementProperties(TextMessageEncodingBindingElement textEncoding, MtomMessageEncodingBindingElement mtomEncoding)
		{
			textEncoding.ReaderQuotas.CopyTo(mtomEncoding.ReaderQuotas);
			mtomEncoding.WriteEncoding = textEncoding.WriteEncoding;
		}
	}
}
