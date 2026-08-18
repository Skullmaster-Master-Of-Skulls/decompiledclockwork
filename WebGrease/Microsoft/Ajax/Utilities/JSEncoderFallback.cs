using System;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000D7 RID: 215
	public class JSEncoderFallback : EncoderFallback
	{
		// Token: 0x06000E46 RID: 3654 RVA: 0x0004251C File Offset: 0x0004071C
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new JSEncoderFallbackBuffer();
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00042523 File Offset: 0x00040723
		public override int MaxCharCount
		{
			get
			{
				return 12;
			}
		}
	}
}
