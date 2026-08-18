using System;
using System.ComponentModel;

namespace System.ServiceModel
{
	// Token: 0x02000039 RID: 57
	internal static class TransferModeHelper
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x000090CA File Offset: 0x000072CA
		public static bool IsDefined(TransferMode v)
		{
			return v == TransferMode.Buffered || v == TransferMode.Streamed || v == TransferMode.StreamedRequest || v == TransferMode.StreamedResponse;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000090DD File Offset: 0x000072DD
		public static bool IsRequestStreamed(TransferMode v)
		{
			return v == TransferMode.StreamedRequest || v == TransferMode.Streamed;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000090E9 File Offset: 0x000072E9
		public static bool IsResponseStreamed(TransferMode v)
		{
			return v == TransferMode.StreamedResponse || v == TransferMode.Streamed;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000090F5 File Offset: 0x000072F5
		public static void Validate(TransferMode value)
		{
			if (!TransferModeHelper.IsDefined(value))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, typeof(TransferMode)));
			}
		}
	}
}
