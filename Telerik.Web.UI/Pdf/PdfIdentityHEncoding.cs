using System;
using System.Text;
using Telerik.Pdf.Gdi;

namespace Telerik.Pdf
{
	// Token: 0x0200165D RID: 5725
	public class PdfIdentityHEncoding : Encoding
	{
		// Token: 0x0600DDC7 RID: 56775 RVA: 0x0030747A File Offset: 0x0030567A
		public PdfIdentityHEncoding(GdiFontMetrics metrics)
		{
			this.metrics = metrics;
		}

		// Token: 0x170043E4 RID: 17380
		// (set) Token: 0x0600DDC8 RID: 56776 RVA: 0x00307489 File Offset: 0x00305689
		public GdiFontMetrics Metrics
		{
			set
			{
				this.metrics = value;
			}
		}

		// Token: 0x0600DDC9 RID: 56777 RVA: 0x00307492 File Offset: 0x00305692
		public override int GetByteCount(char[] chars)
		{
			return this.GetByteCount(chars, 0, chars.Length);
		}

		// Token: 0x0600DDCA RID: 56778 RVA: 0x0030749F File Offset: 0x0030569F
		public override int GetByteCount(string s)
		{
			return this.GetByteCount(s.ToCharArray(), 0, s.Length);
		}

		// Token: 0x0600DDCB RID: 56779 RVA: 0x003074B4 File Offset: 0x003056B4
		public override int GetByteCount(char[] chars, int index, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", "Array cannot be null");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Negative index or count");
			}
			if (chars.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("chars", "Index is not within chars array");
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = (char)this.metrics.MapCharacter(chars[index + i]);
			}
			return PdfIdentityHEncoding.BigEndianEncoding.GetByteCount(array, 0, array.Length);
		}

		// Token: 0x0600DDCC RID: 56780 RVA: 0x0030753C File Offset: 0x0030573C
		public override byte[] GetBytes(char[] chars)
		{
			byte[] array = new byte[this.GetByteCount(chars)];
			this.GetBytesInternal(chars, 0, chars.Length, array, 0);
			return array;
		}

		// Token: 0x0600DDCD RID: 56781 RVA: 0x00307568 File Offset: 0x00305768
		public override byte[] GetBytes(string s)
		{
			byte[] array = new byte[this.GetByteCount(s)];
			this.GetBytesInternal(s, 0, s.Length, array, 0);
			return array;
		}

		// Token: 0x0600DDCE RID: 56782 RVA: 0x00307594 File Offset: 0x00305794
		public override byte[] GetBytes(char[] chars, int index, int count)
		{
			string text = new string(chars, index, count);
			byte[] array = new byte[this.GetByteCount(text)];
			this.GetBytesInternal(text, 0, text.Length, array, 0);
			return array;
		}

		// Token: 0x0600DDCF RID: 56783 RVA: 0x003075C9 File Offset: 0x003057C9
		public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600DDD0 RID: 56784 RVA: 0x003075D0 File Offset: 0x003057D0
		public override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600DDD1 RID: 56785 RVA: 0x003075D7 File Offset: 0x003057D7
		private int GetBytesInternal(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return PdfIdentityHEncoding.BigEndianEncoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x0600DDD2 RID: 56786 RVA: 0x003075EA File Offset: 0x003057EA
		private int GetBytesInternal(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			return PdfIdentityHEncoding.BigEndianEncoding.GetBytes(s, charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x0600DDD3 RID: 56787 RVA: 0x003075FD File Offset: 0x003057FD
		public override int GetCharCount(byte[] bytes)
		{
			return PdfIdentityHEncoding.BigEndianEncoding.GetCharCount(bytes);
		}

		// Token: 0x0600DDD4 RID: 56788 RVA: 0x0030760A File Offset: 0x0030580A
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return PdfIdentityHEncoding.BigEndianEncoding.GetCharCount(bytes, index, count);
		}

		// Token: 0x0600DDD5 RID: 56789 RVA: 0x00307619 File Offset: 0x00305819
		public override char[] GetChars(byte[] bytes)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600DDD6 RID: 56790 RVA: 0x00307620 File Offset: 0x00305820
		public override char[] GetChars(byte[] bytes, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600DDD7 RID: 56791 RVA: 0x00307627 File Offset: 0x00305827
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600DDD8 RID: 56792 RVA: 0x0030762E File Offset: 0x0030582E
		public override int GetMaxByteCount(int charCount)
		{
			return PdfIdentityHEncoding.BigEndianEncoding.GetMaxByteCount(charCount);
		}

		// Token: 0x0600DDD9 RID: 56793 RVA: 0x0030763B File Offset: 0x0030583B
		public override int GetMaxCharCount(int byteCount)
		{
			return PdfIdentityHEncoding.BigEndianEncoding.GetMaxCharCount(byteCount);
		}

		// Token: 0x0600DDDA RID: 56794 RVA: 0x00307648 File Offset: 0x00305848
		public override byte[] GetPreamble()
		{
			return PdfIdentityHEncoding.BigEndianEncoding.GetPreamble();
		}

		// Token: 0x04003F20 RID: 16160
		private GdiFontMetrics metrics;

		// Token: 0x04003F21 RID: 16161
		private static readonly byte[] EmptyByteArray = new byte[0];

		// Token: 0x04003F22 RID: 16162
		private static readonly Encoding BigEndianEncoding = Encoding.BigEndianUnicode;
	}
}
