using System;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000799 RID: 1945
	internal static class BasicHttpBindingDefaults
	{
		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x060049BD RID: 18877 RVA: 0x0010ED5E File Offset: 0x0010CF5E
		internal static Encoding TextEncoding
		{
			get
			{
				return TextEncoderDefaults.Encoding;
			}
		}

		// Token: 0x04002EBC RID: 11964
		internal const BasicHttpMessageCredentialType MessageSecurityClientCredentialType = BasicHttpMessageCredentialType.UserName;

		// Token: 0x04002EBD RID: 11965
		internal const WSMessageEncoding MessageEncoding = WSMessageEncoding.Text;

		// Token: 0x04002EBE RID: 11966
		internal const TransferMode TransferMode = TransferMode.Buffered;
	}
}
