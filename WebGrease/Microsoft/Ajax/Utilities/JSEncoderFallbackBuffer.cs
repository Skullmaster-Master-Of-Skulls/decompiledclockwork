using System;
using System.Text;

namespace Microsoft.Ajax.Utilities
{
	// Token: 0x020000D8 RID: 216
	internal sealed class JSEncoderFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00042527 File Offset: 0x00040727
		public override int Remaining
		{
			get
			{
				return this.m_fallbackString.Length - this.m_position;
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x0004253B File Offset: 0x0004073B
		public JSEncoderFallbackBuffer()
		{
			this.Reset();
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x0004254C File Offset: 0x0004074C
		private static string GetEncoding(int charValue)
		{
			return "\\u{0:x4}".FormatInvariant(new object[]
			{
				charValue
			});
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00042574 File Offset: 0x00040774
		public override bool Fallback(char charUnknown, int index)
		{
			if (this.m_position < this.m_fallbackString.Length)
			{
				throw new ArgumentException(CommonStrings.FallbackEncodingFailed);
			}
			this.m_fallbackString = JSEncoderFallbackBuffer.GetEncoding((int)charUnknown);
			this.m_position = 0;
			return this.m_fallbackString.Length > 0;
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000425C0 File Offset: 0x000407C0
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (this.m_position < this.m_fallbackString.Length)
			{
				throw new ArgumentException(CommonStrings.FallbackEncodingFailed);
			}
			this.m_fallbackString = JSEncoderFallbackBuffer.GetEncoding((int)charUnknownHigh) + JSEncoderFallbackBuffer.GetEncoding((int)charUnknownLow);
			this.m_position = 0;
			return this.m_fallbackString.Length > 0;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x00042618 File Offset: 0x00040818
		public override char GetNextChar()
		{
			if (this.m_position >= this.m_fallbackString.Length)
			{
				return '\0';
			}
			return this.m_fallbackString[this.m_position++];
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x00042658 File Offset: 0x00040858
		public override bool MovePrevious()
		{
			bool result = this.m_position > 0;
			if (this.m_position > 0)
			{
				this.m_position--;
			}
			return result;
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x00042687 File Offset: 0x00040887
		public override void Reset()
		{
			this.m_fallbackString = string.Empty;
			this.m_position = 0;
			base.Reset();
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000426A1 File Offset: 0x000408A1
		public override string ToString()
		{
			return this.m_fallbackString;
		}

		// Token: 0x0400058B RID: 1419
		private string m_fallbackString;

		// Token: 0x0400058C RID: 1420
		private int m_position;
	}
}
