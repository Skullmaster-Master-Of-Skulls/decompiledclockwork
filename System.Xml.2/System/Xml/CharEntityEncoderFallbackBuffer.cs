using System;
using System.Globalization;
using System.Text;

namespace System.Xml
{
	// Token: 0x0200009C RID: 156
	internal class CharEntityEncoderFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00014322 File Offset: 0x00012522
		internal CharEntityEncoderFallbackBuffer(CharEntityEncoderFallback parent)
		{
			this.parent = parent;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00014344 File Offset: 0x00012544
		public override bool Fallback(char charUnknown, int index)
		{
			if (this.charEntityIndex >= 0)
			{
				new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknown, index);
			}
			if (this.parent.CanReplaceAt(index))
			{
				this.charEntity = string.Format(CultureInfo.InvariantCulture, "&#x{0:X};", new object[]
				{
					(int)charUnknown
				});
				this.charEntityIndex = 0;
				return true;
			}
			EncoderFallbackBuffer encoderFallbackBuffer = new EncoderExceptionFallback().CreateFallbackBuffer();
			encoderFallbackBuffer.Fallback(charUnknown, index);
			return false;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000143BC File Offset: 0x000125BC
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsSurrogatePair(charUnknownHigh, charUnknownLow))
			{
				throw XmlConvert.CreateInvalidSurrogatePairException(charUnknownHigh, charUnknownLow);
			}
			if (this.charEntityIndex >= 0)
			{
				new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknownHigh, charUnknownLow, index);
			}
			if (this.parent.CanReplaceAt(index))
			{
				this.charEntity = string.Format(CultureInfo.InvariantCulture, "&#x{0:X};", new object[]
				{
					this.SurrogateCharToUtf32(charUnknownHigh, charUnknownLow)
				});
				this.charEntityIndex = 0;
				return true;
			}
			EncoderFallbackBuffer encoderFallbackBuffer = new EncoderExceptionFallback().CreateFallbackBuffer();
			encoderFallbackBuffer.Fallback(charUnknownHigh, charUnknownLow, index);
			return false;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00014450 File Offset: 0x00012650
		public override char GetNextChar()
		{
			if (this.charEntityIndex == this.charEntity.Length)
			{
				this.charEntityIndex = -1;
			}
			if (this.charEntityIndex == -1)
			{
				return '\0';
			}
			string text = this.charEntity;
			int num = this.charEntityIndex;
			this.charEntityIndex = num + 1;
			return text[num];
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x000144A0 File Offset: 0x000126A0
		public override bool MovePrevious()
		{
			if (this.charEntityIndex == -1)
			{
				return false;
			}
			if (this.charEntityIndex > 0)
			{
				this.charEntityIndex--;
				return true;
			}
			return false;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x000144C7 File Offset: 0x000126C7
		public override int Remaining
		{
			get
			{
				if (this.charEntityIndex == -1)
				{
					return 0;
				}
				return this.charEntity.Length - this.charEntityIndex;
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000144E6 File Offset: 0x000126E6
		public override void Reset()
		{
			this.charEntityIndex = -1;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000144EF File Offset: 0x000126EF
		private int SurrogateCharToUtf32(char highSurrogate, char lowSurrogate)
		{
			return XmlCharType.CombineSurrogateChar((int)lowSurrogate, (int)highSurrogate);
		}

		// Token: 0x04000250 RID: 592
		private CharEntityEncoderFallback parent;

		// Token: 0x04000251 RID: 593
		private string charEntity = string.Empty;

		// Token: 0x04000252 RID: 594
		private int charEntityIndex = -1;
	}
}
