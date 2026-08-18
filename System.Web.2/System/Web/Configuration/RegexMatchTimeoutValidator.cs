using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006C9 RID: 1737
	internal sealed class RegexMatchTimeoutValidator : TimeSpanValidator
	{
		// Token: 0x060053D3 RID: 21459 RVA: 0x0012693B File Offset: 0x00124B3B
		public RegexMatchTimeoutValidator() : base(RegexMatchTimeoutValidator._minValue, RegexMatchTimeoutValidator._maxValue)
		{
		}

		// Token: 0x04002C16 RID: 11286
		private static readonly TimeSpan _minValue = TimeSpan.Zero;

		// Token: 0x04002C17 RID: 11287
		private static readonly TimeSpan _maxValue = TimeSpan.FromMilliseconds(2147483646.0);
	}
}
