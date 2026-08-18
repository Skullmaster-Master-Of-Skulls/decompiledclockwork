using System;
using System.Collections.Generic;
using System.Text;

namespace iTextSharp.text.pdf
{
	// Token: 0x020004A9 RID: 1193
	public class BidiLine
	{
		// Token: 0x0600284D RID: 10317 RVA: 0x000F3314 File Offset: 0x000F2314
		public BidiLine()
		{
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000F33B0 File Offset: 0x000F23B0
		public BidiLine(BidiLine org)
		{
			this.runDirection = org.runDirection;
			this.pieceSize = org.pieceSize;
			this.text = (char[])org.text.Clone();
			this.detailChunks = (PdfChunk[])org.detailChunks.Clone();
			this.totalTextLength = org.totalTextLength;
			this.orderLevels = (byte[])org.orderLevels.Clone();
			this.indexChars = (int[])org.indexChars.Clone();
			this.chunks = new List<PdfChunk>(org.chunks);
			this.indexChunk = org.indexChunk;
			this.indexChunkChar = org.indexChunkChar;
			this.currentChar = org.currentChar;
			this.storedRunDirection = org.storedRunDirection;
			this.storedText = (char[])org.storedText.Clone();
			this.storedDetailChunks = (PdfChunk[])org.storedDetailChunks.Clone();
			this.storedTotalTextLength = org.storedTotalTextLength;
			this.storedOrderLevels = (byte[])org.storedOrderLevels.Clone();
			this.storedIndexChars = (int[])org.storedIndexChars.Clone();
			this.storedIndexChunk = org.storedIndexChunk;
			this.storedIndexChunkChar = org.storedIndexChunkChar;
			this.storedCurrentChar = org.storedCurrentChar;
			this.shortStore = org.shortStore;
			this.arabicOptions = org.arabicOptions;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000F35A6 File Offset: 0x000F25A6
		public bool IsEmpty()
		{
			return this.currentChar >= this.totalTextLength && this.indexChunk >= this.chunks.Count;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000F35CE File Offset: 0x000F25CE
		public void ClearChunks()
		{
			this.chunks.Clear();
			this.totalTextLength = 0;
			this.currentChar = 0;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000F35EC File Offset: 0x000F25EC
		public bool GetParagraph(int runDirection)
		{
			this.runDirection = runDirection;
			this.currentChar = 0;
			this.totalTextLength = 0;
			bool flag = false;
			while (this.indexChunk < this.chunks.Count)
			{
				PdfChunk pdfChunk = this.chunks[this.indexChunk];
				BaseFont font = pdfChunk.Font.Font;
				string text = pdfChunk.ToString();
				int length = text.Length;
				while (this.indexChunkChar < length)
				{
					char c = text[this.indexChunkChar];
					char c2 = (char)font.GetUnicodeEquivalent((int)c);
					if (c2 == '\r' || c2 == '\n')
					{
						if (c2 == '\r' && this.indexChunkChar + 1 < length && text[this.indexChunkChar + 1] == '\n')
						{
							this.indexChunkChar++;
						}
						this.indexChunkChar++;
						if (this.indexChunkChar >= length)
						{
							this.indexChunkChar = 0;
							this.indexChunk++;
						}
						flag = true;
						if (this.totalTextLength == 0)
						{
							this.detailChunks[0] = pdfChunk;
							break;
						}
						break;
					}
					else
					{
						this.AddPiece(c, pdfChunk);
						this.indexChunkChar++;
					}
				}
				if (flag)
				{
					break;
				}
				this.indexChunkChar = 0;
				this.indexChunk++;
			}
			if (this.totalTextLength == 0)
			{
				return flag;
			}
			this.totalTextLength = this.TrimRight(0, this.totalTextLength - 1) + 1;
			if (this.totalTextLength == 0)
			{
				return true;
			}
			if (runDirection == 2 || runDirection == 3)
			{
				if (this.orderLevels.Length < this.totalTextLength)
				{
					this.orderLevels = new byte[this.pieceSize];
					this.indexChars = new int[this.pieceSize];
				}
				ArabicLigaturizer.ProcessNumbers(this.text, 0, this.totalTextLength, this.arabicOptions);
				BidiOrder bidiOrder = new BidiOrder(this.text, 0, this.totalTextLength, (runDirection == 3) ? 1 : 0);
				byte[] levels = bidiOrder.GetLevels();
				for (int i = 0; i < this.totalTextLength; i++)
				{
					this.orderLevels[i] = levels[i];
					this.indexChars[i] = i;
				}
				this.DoArabicShapping();
				this.MirrorGlyphs();
			}
			this.totalTextLength = this.TrimRightEx(0, this.totalTextLength - 1) + 1;
			return true;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000F382C File Offset: 0x000F282C
		public void AddChunk(PdfChunk chunk)
		{
			this.chunks.Add(chunk);
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000F383A File Offset: 0x000F283A
		public void AddChunks(List<PdfChunk> chunks)
		{
			this.chunks.AddRange(chunks);
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000F3848 File Offset: 0x000F2848
		public void AddPiece(char c, PdfChunk chunk)
		{
			if (this.totalTextLength >= this.pieceSize)
			{
				char[] sourceArray = this.text;
				PdfChunk[] sourceArray2 = this.detailChunks;
				this.pieceSize *= 2;
				this.text = new char[this.pieceSize];
				this.detailChunks = new PdfChunk[this.pieceSize];
				Array.Copy(sourceArray, 0, this.text, 0, this.totalTextLength);
				Array.Copy(sourceArray2, 0, this.detailChunks, 0, this.totalTextLength);
			}
			this.text[this.totalTextLength] = c;
			this.detailChunks[this.totalTextLength++] = chunk;
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000F38F0 File Offset: 0x000F28F0
		public void Save()
		{
			if (this.indexChunk > 0)
			{
				if (this.indexChunk >= this.chunks.Count)
				{
					this.chunks.Clear();
				}
				else
				{
					this.indexChunk--;
					while (this.indexChunk >= 0)
					{
						this.chunks.RemoveAt(this.indexChunk);
						this.indexChunk--;
					}
				}
				this.indexChunk = 0;
			}
			this.storedRunDirection = this.runDirection;
			this.storedTotalTextLength = this.totalTextLength;
			this.storedIndexChunk = this.indexChunk;
			this.storedIndexChunkChar = this.indexChunkChar;
			this.storedCurrentChar = this.currentChar;
			this.shortStore = (this.currentChar < this.totalTextLength);
			if (!this.shortStore)
			{
				if (this.storedText.Length < this.totalTextLength)
				{
					this.storedText = new char[this.totalTextLength];
					this.storedDetailChunks = new PdfChunk[this.totalTextLength];
				}
				Array.Copy(this.text, 0, this.storedText, 0, this.totalTextLength);
				Array.Copy(this.detailChunks, 0, this.storedDetailChunks, 0, this.totalTextLength);
			}
			if (this.runDirection == 2 || this.runDirection == 3)
			{
				if (this.storedOrderLevels.Length < this.totalTextLength)
				{
					this.storedOrderLevels = new byte[this.totalTextLength];
					this.storedIndexChars = new int[this.totalTextLength];
				}
				Array.Copy(this.orderLevels, this.currentChar, this.storedOrderLevels, this.currentChar, this.totalTextLength - this.currentChar);
				Array.Copy(this.indexChars, this.currentChar, this.storedIndexChars, this.currentChar, this.totalTextLength - this.currentChar);
			}
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000F3ABC File Offset: 0x000F2ABC
		public void Restore()
		{
			this.runDirection = this.storedRunDirection;
			this.totalTextLength = this.storedTotalTextLength;
			this.indexChunk = this.storedIndexChunk;
			this.indexChunkChar = this.storedIndexChunkChar;
			this.currentChar = this.storedCurrentChar;
			if (!this.shortStore)
			{
				Array.Copy(this.storedText, 0, this.text, 0, this.totalTextLength);
				Array.Copy(this.storedDetailChunks, 0, this.detailChunks, 0, this.totalTextLength);
			}
			if (this.runDirection == 2 || this.runDirection == 3)
			{
				Array.Copy(this.storedOrderLevels, this.currentChar, this.orderLevels, this.currentChar, this.totalTextLength - this.currentChar);
				Array.Copy(this.storedIndexChars, this.currentChar, this.indexChars, this.currentChar, this.totalTextLength - this.currentChar);
			}
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x000F3BA8 File Offset: 0x000F2BA8
		public void MirrorGlyphs()
		{
			for (int i = 0; i < this.totalTextLength; i++)
			{
				if ((this.orderLevels[i] & 1) == 1)
				{
					int num = BidiLine.mirrorChars[(int)this.text[i]];
					if (num != 0)
					{
						this.text[i] = (char)num;
					}
				}
			}
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000F3BF4 File Offset: 0x000F2BF4
		public void DoArabicShapping()
		{
			int i = 0;
			int num = 0;
			for (;;)
			{
				if (i < this.totalTextLength)
				{
					char c = this.text[i];
					if (c < '؀' || c > 'ۿ')
					{
						if (i != num)
						{
							this.text[num] = this.text[i];
							this.detailChunks[num] = this.detailChunks[i];
							this.orderLevels[num] = this.orderLevels[i];
						}
						i++;
						num++;
						continue;
					}
				}
				if (i >= this.totalTextLength)
				{
					break;
				}
				int num2 = i;
				for (i++; i < this.totalTextLength; i++)
				{
					char c2 = this.text[i];
					if (c2 < '؀' || c2 > 'ۿ')
					{
						break;
					}
				}
				int num3 = i - num2;
				int num4 = ArabicLigaturizer.Arabic_shape(this.text, num2, num3, this.text, num, num3, this.arabicOptions);
				if (num2 != num)
				{
					for (int j = 0; j < num4; j++)
					{
						this.detailChunks[num] = this.detailChunks[num2];
						this.orderLevels[num++] = this.orderLevels[num2++];
					}
				}
				else
				{
					num += num4;
				}
			}
			this.totalTextLength = num;
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000F3D18 File Offset: 0x000F2D18
		public PdfLine ProcessLine(float leftX, float width, int alignment, int runDirection, int arabicOptions)
		{
			this.arabicOptions = arabicOptions;
			this.Save();
			bool isRTL = runDirection == 3;
			if (this.currentChar >= this.totalTextLength)
			{
				if (!this.GetParagraph(runDirection))
				{
					return null;
				}
				if (this.totalTextLength == 0)
				{
					List<PdfChunk> list = new List<PdfChunk>();
					PdfChunk item = new PdfChunk("", this.detailChunks[0]);
					list.Add(item);
					return new PdfLine(0f, 0f, 0f, alignment, true, list, isRTL);
				}
			}
			float num = width;
			int num2 = -1;
			if (this.currentChar != 0)
			{
				this.currentChar = this.TrimLeftEx(this.currentChar, this.totalTextLength - 1);
			}
			int num3 = this.currentChar;
			PdfChunk pdfChunk = null;
			bool flag = false;
			while (this.currentChar < this.totalTextLength)
			{
				PdfChunk pdfChunk2 = this.detailChunks[this.currentChar];
				flag = Utilities.IsSurrogatePair(this.text, this.currentChar);
				int unicodeEquivalent;
				if (flag)
				{
					unicodeEquivalent = pdfChunk2.GetUnicodeEquivalent(Utilities.ConvertToUtf32(this.text, this.currentChar));
				}
				else
				{
					unicodeEquivalent = pdfChunk2.GetUnicodeEquivalent((int)this.text[this.currentChar]);
				}
				if (!PdfChunk.NoPrint(unicodeEquivalent))
				{
					float charWidth;
					if (flag)
					{
						charWidth = pdfChunk2.GetCharWidth(unicodeEquivalent);
					}
					else
					{
						charWidth = pdfChunk2.GetCharWidth((int)this.text[this.currentChar]);
					}
					bool flag2 = pdfChunk2.IsExtSplitCharacter(num3, this.currentChar, this.totalTextLength, this.text, this.detailChunks);
					if (flag2 && char.IsWhiteSpace((char)unicodeEquivalent))
					{
						num2 = this.currentChar;
					}
					if (width - charWidth < 0f)
					{
						break;
					}
					if (flag2)
					{
						num2 = this.currentChar;
					}
					width -= charWidth;
					pdfChunk = pdfChunk2;
					if (flag)
					{
						this.currentChar++;
					}
					if (pdfChunk2.IsTab())
					{
						object[] array = (object[])pdfChunk2.GetAttribute("TAB");
						float num4 = (float)array[1];
						bool flag3 = (bool)array[2];
						if (flag3 && num4 < num - width)
						{
							return new PdfLine(0f, num, width, alignment, true, this.CreateArrayOfPdfChunks(num3, this.currentChar - 1), isRTL);
						}
						this.detailChunks[this.currentChar].AdjustLeft(leftX);
						width = num - num4;
					}
				}
				this.currentChar++;
			}
			if (pdfChunk == null)
			{
				this.currentChar++;
				if (flag)
				{
					this.currentChar++;
				}
				return new PdfLine(0f, num, 0f, alignment, false, this.CreateArrayOfPdfChunks(this.currentChar - 1, this.currentChar - 1), isRTL);
			}
			if (this.currentChar >= this.totalTextLength)
			{
				return new PdfLine(0f, num, width, alignment, true, this.CreateArrayOfPdfChunks(num3, this.totalTextLength - 1), isRTL);
			}
			int num5 = this.TrimRightEx(num3, this.currentChar - 1);
			if (num5 < num3)
			{
				return new PdfLine(0f, num, width, alignment, false, this.CreateArrayOfPdfChunks(num3, this.currentChar - 1), isRTL);
			}
			if (num5 == this.currentChar - 1)
			{
				IHyphenationEvent hyphenationEvent = (IHyphenationEvent)pdfChunk.GetAttribute("HYPHENATION");
				if (hyphenationEvent != null)
				{
					int[] word = this.GetWord(num3, num5);
					if (word != null)
					{
						float num6 = width + this.GetWidth(word[0], this.currentChar - 1);
						string hyphenatedWordPre = hyphenationEvent.GetHyphenatedWordPre(new string(this.text, word[0], word[1] - word[0]), pdfChunk.Font.Font, pdfChunk.Font.Size, num6);
						string hyphenatedWordPost = hyphenationEvent.HyphenatedWordPost;
						if (hyphenatedWordPre.Length > 0)
						{
							PdfChunk extraPdfChunk = new PdfChunk(hyphenatedWordPre, pdfChunk);
							this.currentChar = word[1] - hyphenatedWordPost.Length;
							return new PdfLine(0f, num, num6 - pdfChunk.Font.Width(hyphenatedWordPre), alignment, false, this.CreateArrayOfPdfChunks(num3, word[0] - 1, extraPdfChunk), isRTL);
						}
					}
				}
			}
			if (num2 == -1 || num2 >= num5)
			{
				return new PdfLine(0f, num, width + this.GetWidth(num5 + 1, this.currentChar - 1), alignment, false, this.CreateArrayOfPdfChunks(num3, num5), isRTL);
			}
			this.currentChar = num2 + 1;
			num5 = this.TrimRightEx(num3, num2);
			if (num5 < num3)
			{
				num5 = this.currentChar - 1;
			}
			return new PdfLine(0f, num, num - this.GetWidth(num3, num5), alignment, false, this.CreateArrayOfPdfChunks(num3, num5), isRTL);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000F41A0 File Offset: 0x000F31A0
		public float GetWidth(int startIdx, int lastIdx)
		{
			float num = 0f;
			while (startIdx <= lastIdx)
			{
				bool flag = Utilities.IsSurrogatePair(this.text, startIdx);
				if (flag)
				{
					num += this.detailChunks[startIdx].GetCharWidth(Utilities.ConvertToUtf32(this.text, startIdx));
					startIdx++;
				}
				else
				{
					char c = this.text[startIdx];
					PdfChunk pdfChunk = this.detailChunks[startIdx];
					if (!PdfChunk.NoPrint(pdfChunk.GetUnicodeEquivalent((int)c)))
					{
						num += this.detailChunks[startIdx].GetCharWidth((int)c);
					}
				}
				startIdx++;
			}
			return num;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x000F4227 File Offset: 0x000F3227
		public List<PdfChunk> CreateArrayOfPdfChunks(int startIdx, int endIdx)
		{
			return this.CreateArrayOfPdfChunks(startIdx, endIdx, null);
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x000F4234 File Offset: 0x000F3234
		public List<PdfChunk> CreateArrayOfPdfChunks(int startIdx, int endIdx, PdfChunk extraPdfChunk)
		{
			bool flag = this.runDirection == 2 || this.runDirection == 3;
			if (flag)
			{
				this.Reorder(startIdx, endIdx);
			}
			List<PdfChunk> list = new List<PdfChunk>();
			PdfChunk pdfChunk = this.detailChunks[startIdx];
			StringBuilder stringBuilder = new StringBuilder();
			while (startIdx <= endIdx)
			{
				int num = flag ? this.indexChars[startIdx] : startIdx;
				char c = this.text[num];
				PdfChunk pdfChunk2 = this.detailChunks[num];
				if (!PdfChunk.NoPrint(pdfChunk2.GetUnicodeEquivalent((int)c)))
				{
					if (pdfChunk2.IsImage() || pdfChunk2.IsSeparator() || pdfChunk2.IsTab())
					{
						if (stringBuilder.Length > 0)
						{
							list.Add(new PdfChunk(stringBuilder.ToString(), pdfChunk));
							stringBuilder = new StringBuilder();
						}
						list.Add(pdfChunk2);
					}
					else if (pdfChunk2 == pdfChunk)
					{
						stringBuilder.Append(c);
					}
					else
					{
						if (stringBuilder.Length > 0)
						{
							list.Add(new PdfChunk(stringBuilder.ToString(), pdfChunk));
							stringBuilder = new StringBuilder();
						}
						if (!pdfChunk2.IsImage() && !pdfChunk2.IsSeparator() && !pdfChunk2.IsTab())
						{
							stringBuilder.Append(c);
						}
						pdfChunk = pdfChunk2;
					}
				}
				startIdx++;
			}
			if (stringBuilder.Length > 0)
			{
				list.Add(new PdfChunk(stringBuilder.ToString(), pdfChunk));
			}
			if (extraPdfChunk != null)
			{
				list.Add(extraPdfChunk);
			}
			return list;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000F438C File Offset: 0x000F338C
		public int[] GetWord(int startIdx, int idx)
		{
			int num = idx;
			int num2 = idx;
			while (num < this.totalTextLength && char.IsLetter(this.text[num]))
			{
				num++;
			}
			if (num == idx)
			{
				return null;
			}
			while (num2 >= startIdx && char.IsLetter(this.text[num2]))
			{
				num2--;
			}
			num2++;
			return new int[]
			{
				num2,
				num
			};
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000F43EC File Offset: 0x000F33EC
		public int TrimRight(int startIdx, int endIdx)
		{
			int i;
			for (i = endIdx; i >= startIdx; i--)
			{
				char c = (char)this.detailChunks[i].GetUnicodeEquivalent((int)this.text[i]);
				if (!BidiLine.IsWS(c))
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000F4428 File Offset: 0x000F3428
		public int TrimLeft(int startIdx, int endIdx)
		{
			int i;
			for (i = startIdx; i <= endIdx; i++)
			{
				char c = (char)this.detailChunks[i].GetUnicodeEquivalent((int)this.text[i]);
				if (!BidiLine.IsWS(c))
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x000F4464 File Offset: 0x000F3464
		public int TrimRightEx(int startIdx, int endIdx)
		{
			int i;
			for (i = endIdx; i >= startIdx; i--)
			{
				char c = (char)this.detailChunks[i].GetUnicodeEquivalent((int)this.text[i]);
				if (!BidiLine.IsWS(c) && !PdfChunk.NoPrint((int)c))
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x000F44A8 File Offset: 0x000F34A8
		public int TrimLeftEx(int startIdx, int endIdx)
		{
			int i;
			for (i = startIdx; i <= endIdx; i++)
			{
				char c = (char)this.detailChunks[i].GetUnicodeEquivalent((int)this.text[i]);
				if (!BidiLine.IsWS(c) && !PdfChunk.NoPrint((int)c))
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x000F44EC File Offset: 0x000F34EC
		public void Reorder(int start, int end)
		{
			byte b = this.orderLevels[start];
			byte b2 = b;
			byte b3 = b;
			byte b4 = b;
			for (int i = start + 1; i <= end; i++)
			{
				byte b5 = this.orderLevels[i];
				if (b5 > b)
				{
					b = b5;
				}
				else if (b5 < b2)
				{
					b2 = b5;
				}
				b3 &= b5;
				b4 |= b5;
			}
			if ((b4 & 1) == 0)
			{
				return;
			}
			if ((b3 & 1) == 1)
			{
				this.Flip(start, end + 1);
				return;
			}
			b2 |= 1;
			while (b >= b2)
			{
				int num = start;
				for (;;)
				{
					if (num <= end && this.orderLevels[num] < b)
					{
						num++;
					}
					else
					{
						if (num > end)
						{
							break;
						}
						int num2 = num + 1;
						while (num2 <= end && this.orderLevels[num2] >= b)
						{
							num2++;
						}
						this.Flip(num, num2);
						num = num2 + 1;
					}
				}
				b -= 1;
			}
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000F45BC File Offset: 0x000F35BC
		public void Flip(int start, int end)
		{
			int num = (start + end) / 2;
			end--;
			while (start < num)
			{
				int num2 = this.indexChars[start];
				this.indexChars[start] = this.indexChars[end];
				this.indexChars[end] = num2;
				start++;
				end--;
			}
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000F4606 File Offset: 0x000F3606
		public static bool IsWS(char c)
		{
			return c <= ' ';
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000F4610 File Offset: 0x000F3610
		static BidiLine()
		{
			BidiLine.mirrorChars[40] = 41;
			BidiLine.mirrorChars[41] = 40;
			BidiLine.mirrorChars[60] = 62;
			BidiLine.mirrorChars[62] = 60;
			BidiLine.mirrorChars[91] = 93;
			BidiLine.mirrorChars[93] = 91;
			BidiLine.mirrorChars[123] = 125;
			BidiLine.mirrorChars[125] = 123;
			BidiLine.mirrorChars[171] = 187;
			BidiLine.mirrorChars[187] = 171;
			BidiLine.mirrorChars[8249] = 8250;
			BidiLine.mirrorChars[8250] = 8249;
			BidiLine.mirrorChars[8261] = 8262;
			BidiLine.mirrorChars[8262] = 8261;
			BidiLine.mirrorChars[8317] = 8318;
			BidiLine.mirrorChars[8318] = 8317;
			BidiLine.mirrorChars[8333] = 8334;
			BidiLine.mirrorChars[8334] = 8333;
			BidiLine.mirrorChars[8712] = 8715;
			BidiLine.mirrorChars[8713] = 8716;
			BidiLine.mirrorChars[8714] = 8717;
			BidiLine.mirrorChars[8715] = 8712;
			BidiLine.mirrorChars[8716] = 8713;
			BidiLine.mirrorChars[8717] = 8714;
			BidiLine.mirrorChars[8725] = 10741;
			BidiLine.mirrorChars[8764] = 8765;
			BidiLine.mirrorChars[8765] = 8764;
			BidiLine.mirrorChars[8771] = 8909;
			BidiLine.mirrorChars[8786] = 8787;
			BidiLine.mirrorChars[8787] = 8786;
			BidiLine.mirrorChars[8788] = 8789;
			BidiLine.mirrorChars[8789] = 8788;
			BidiLine.mirrorChars[8804] = 8805;
			BidiLine.mirrorChars[8805] = 8804;
			BidiLine.mirrorChars[8806] = 8807;
			BidiLine.mirrorChars[8807] = 8806;
			BidiLine.mirrorChars[8808] = 8809;
			BidiLine.mirrorChars[8809] = 8808;
			BidiLine.mirrorChars[8810] = 8811;
			BidiLine.mirrorChars[8811] = 8810;
			BidiLine.mirrorChars[8814] = 8815;
			BidiLine.mirrorChars[8815] = 8814;
			BidiLine.mirrorChars[8816] = 8817;
			BidiLine.mirrorChars[8817] = 8816;
			BidiLine.mirrorChars[8818] = 8819;
			BidiLine.mirrorChars[8819] = 8818;
			BidiLine.mirrorChars[8820] = 8821;
			BidiLine.mirrorChars[8821] = 8820;
			BidiLine.mirrorChars[8822] = 8823;
			BidiLine.mirrorChars[8823] = 8822;
			BidiLine.mirrorChars[8824] = 8825;
			BidiLine.mirrorChars[8825] = 8824;
			BidiLine.mirrorChars[8826] = 8827;
			BidiLine.mirrorChars[8827] = 8826;
			BidiLine.mirrorChars[8828] = 8829;
			BidiLine.mirrorChars[8829] = 8828;
			BidiLine.mirrorChars[8830] = 8831;
			BidiLine.mirrorChars[8831] = 8830;
			BidiLine.mirrorChars[8832] = 8833;
			BidiLine.mirrorChars[8833] = 8832;
			BidiLine.mirrorChars[8834] = 8835;
			BidiLine.mirrorChars[8835] = 8834;
			BidiLine.mirrorChars[8836] = 8837;
			BidiLine.mirrorChars[8837] = 8836;
			BidiLine.mirrorChars[8838] = 8839;
			BidiLine.mirrorChars[8839] = 8838;
			BidiLine.mirrorChars[8840] = 8841;
			BidiLine.mirrorChars[8841] = 8840;
			BidiLine.mirrorChars[8842] = 8843;
			BidiLine.mirrorChars[8843] = 8842;
			BidiLine.mirrorChars[8847] = 8848;
			BidiLine.mirrorChars[8848] = 8847;
			BidiLine.mirrorChars[8849] = 8850;
			BidiLine.mirrorChars[8850] = 8849;
			BidiLine.mirrorChars[8856] = 10680;
			BidiLine.mirrorChars[8866] = 8867;
			BidiLine.mirrorChars[8867] = 8866;
			BidiLine.mirrorChars[8870] = 10974;
			BidiLine.mirrorChars[8872] = 10980;
			BidiLine.mirrorChars[8873] = 10979;
			BidiLine.mirrorChars[8875] = 10981;
			BidiLine.mirrorChars[8880] = 8881;
			BidiLine.mirrorChars[8881] = 8880;
			BidiLine.mirrorChars[8882] = 8883;
			BidiLine.mirrorChars[8883] = 8882;
			BidiLine.mirrorChars[8884] = 8885;
			BidiLine.mirrorChars[8885] = 8884;
			BidiLine.mirrorChars[8886] = 8887;
			BidiLine.mirrorChars[8887] = 8886;
			BidiLine.mirrorChars[8905] = 8906;
			BidiLine.mirrorChars[8906] = 8905;
			BidiLine.mirrorChars[8907] = 8908;
			BidiLine.mirrorChars[8908] = 8907;
			BidiLine.mirrorChars[8909] = 8771;
			BidiLine.mirrorChars[8912] = 8913;
			BidiLine.mirrorChars[8913] = 8912;
			BidiLine.mirrorChars[8918] = 8919;
			BidiLine.mirrorChars[8919] = 8918;
			BidiLine.mirrorChars[8920] = 8921;
			BidiLine.mirrorChars[8921] = 8920;
			BidiLine.mirrorChars[8922] = 8923;
			BidiLine.mirrorChars[8923] = 8922;
			BidiLine.mirrorChars[8924] = 8925;
			BidiLine.mirrorChars[8925] = 8924;
			BidiLine.mirrorChars[8926] = 8927;
			BidiLine.mirrorChars[8927] = 8926;
			BidiLine.mirrorChars[8928] = 8929;
			BidiLine.mirrorChars[8929] = 8928;
			BidiLine.mirrorChars[8930] = 8931;
			BidiLine.mirrorChars[8931] = 8930;
			BidiLine.mirrorChars[8932] = 8933;
			BidiLine.mirrorChars[8933] = 8932;
			BidiLine.mirrorChars[8934] = 8935;
			BidiLine.mirrorChars[8935] = 8934;
			BidiLine.mirrorChars[8936] = 8937;
			BidiLine.mirrorChars[8937] = 8936;
			BidiLine.mirrorChars[8938] = 8939;
			BidiLine.mirrorChars[8939] = 8938;
			BidiLine.mirrorChars[8940] = 8941;
			BidiLine.mirrorChars[8941] = 8940;
			BidiLine.mirrorChars[8944] = 8945;
			BidiLine.mirrorChars[8945] = 8944;
			BidiLine.mirrorChars[8946] = 8954;
			BidiLine.mirrorChars[8947] = 8955;
			BidiLine.mirrorChars[8948] = 8956;
			BidiLine.mirrorChars[8950] = 8957;
			BidiLine.mirrorChars[8951] = 8958;
			BidiLine.mirrorChars[8954] = 8946;
			BidiLine.mirrorChars[8955] = 8947;
			BidiLine.mirrorChars[8956] = 8948;
			BidiLine.mirrorChars[8957] = 8950;
			BidiLine.mirrorChars[8958] = 8951;
			BidiLine.mirrorChars[8968] = 8969;
			BidiLine.mirrorChars[8969] = 8968;
			BidiLine.mirrorChars[8970] = 8971;
			BidiLine.mirrorChars[8971] = 8970;
			BidiLine.mirrorChars[9001] = 9002;
			BidiLine.mirrorChars[9002] = 9001;
			BidiLine.mirrorChars[10088] = 10089;
			BidiLine.mirrorChars[10089] = 10088;
			BidiLine.mirrorChars[10090] = 10091;
			BidiLine.mirrorChars[10091] = 10090;
			BidiLine.mirrorChars[10092] = 10093;
			BidiLine.mirrorChars[10093] = 10092;
			BidiLine.mirrorChars[10094] = 10095;
			BidiLine.mirrorChars[10095] = 10094;
			BidiLine.mirrorChars[10096] = 10097;
			BidiLine.mirrorChars[10097] = 10096;
			BidiLine.mirrorChars[10098] = 10099;
			BidiLine.mirrorChars[10099] = 10098;
			BidiLine.mirrorChars[10100] = 10101;
			BidiLine.mirrorChars[10101] = 10100;
			BidiLine.mirrorChars[10197] = 10198;
			BidiLine.mirrorChars[10198] = 10197;
			BidiLine.mirrorChars[10205] = 10206;
			BidiLine.mirrorChars[10206] = 10205;
			BidiLine.mirrorChars[10210] = 10211;
			BidiLine.mirrorChars[10211] = 10210;
			BidiLine.mirrorChars[10212] = 10213;
			BidiLine.mirrorChars[10213] = 10212;
			BidiLine.mirrorChars[10214] = 10215;
			BidiLine.mirrorChars[10215] = 10214;
			BidiLine.mirrorChars[10216] = 10217;
			BidiLine.mirrorChars[10217] = 10216;
			BidiLine.mirrorChars[10218] = 10219;
			BidiLine.mirrorChars[10219] = 10218;
			BidiLine.mirrorChars[10627] = 10628;
			BidiLine.mirrorChars[10628] = 10627;
			BidiLine.mirrorChars[10629] = 10630;
			BidiLine.mirrorChars[10630] = 10629;
			BidiLine.mirrorChars[10631] = 10632;
			BidiLine.mirrorChars[10632] = 10631;
			BidiLine.mirrorChars[10633] = 10634;
			BidiLine.mirrorChars[10634] = 10633;
			BidiLine.mirrorChars[10635] = 10636;
			BidiLine.mirrorChars[10636] = 10635;
			BidiLine.mirrorChars[10637] = 10640;
			BidiLine.mirrorChars[10638] = 10639;
			BidiLine.mirrorChars[10639] = 10638;
			BidiLine.mirrorChars[10640] = 10637;
			BidiLine.mirrorChars[10641] = 10642;
			BidiLine.mirrorChars[10642] = 10641;
			BidiLine.mirrorChars[10643] = 10644;
			BidiLine.mirrorChars[10644] = 10643;
			BidiLine.mirrorChars[10645] = 10646;
			BidiLine.mirrorChars[10646] = 10645;
			BidiLine.mirrorChars[10647] = 10648;
			BidiLine.mirrorChars[10648] = 10647;
			BidiLine.mirrorChars[10680] = 8856;
			BidiLine.mirrorChars[10688] = 10689;
			BidiLine.mirrorChars[10689] = 10688;
			BidiLine.mirrorChars[10692] = 10693;
			BidiLine.mirrorChars[10693] = 10692;
			BidiLine.mirrorChars[10703] = 10704;
			BidiLine.mirrorChars[10704] = 10703;
			BidiLine.mirrorChars[10705] = 10706;
			BidiLine.mirrorChars[10706] = 10705;
			BidiLine.mirrorChars[10708] = 10709;
			BidiLine.mirrorChars[10709] = 10708;
			BidiLine.mirrorChars[10712] = 10713;
			BidiLine.mirrorChars[10713] = 10712;
			BidiLine.mirrorChars[10714] = 10715;
			BidiLine.mirrorChars[10715] = 10714;
			BidiLine.mirrorChars[10741] = 8725;
			BidiLine.mirrorChars[10744] = 10745;
			BidiLine.mirrorChars[10745] = 10744;
			BidiLine.mirrorChars[10748] = 10749;
			BidiLine.mirrorChars[10749] = 10748;
			BidiLine.mirrorChars[10795] = 10796;
			BidiLine.mirrorChars[10796] = 10795;
			BidiLine.mirrorChars[10797] = 10796;
			BidiLine.mirrorChars[10798] = 10797;
			BidiLine.mirrorChars[10804] = 10805;
			BidiLine.mirrorChars[10805] = 10804;
			BidiLine.mirrorChars[10812] = 10813;
			BidiLine.mirrorChars[10813] = 10812;
			BidiLine.mirrorChars[10852] = 10853;
			BidiLine.mirrorChars[10853] = 10852;
			BidiLine.mirrorChars[10873] = 10874;
			BidiLine.mirrorChars[10874] = 10873;
			BidiLine.mirrorChars[10877] = 10878;
			BidiLine.mirrorChars[10878] = 10877;
			BidiLine.mirrorChars[10879] = 10880;
			BidiLine.mirrorChars[10880] = 10879;
			BidiLine.mirrorChars[10881] = 10882;
			BidiLine.mirrorChars[10882] = 10881;
			BidiLine.mirrorChars[10883] = 10884;
			BidiLine.mirrorChars[10884] = 10883;
			BidiLine.mirrorChars[10891] = 10892;
			BidiLine.mirrorChars[10892] = 10891;
			BidiLine.mirrorChars[10897] = 10898;
			BidiLine.mirrorChars[10898] = 10897;
			BidiLine.mirrorChars[10899] = 10900;
			BidiLine.mirrorChars[10900] = 10899;
			BidiLine.mirrorChars[10901] = 10902;
			BidiLine.mirrorChars[10902] = 10901;
			BidiLine.mirrorChars[10903] = 10904;
			BidiLine.mirrorChars[10904] = 10903;
			BidiLine.mirrorChars[10905] = 10906;
			BidiLine.mirrorChars[10906] = 10905;
			BidiLine.mirrorChars[10907] = 10908;
			BidiLine.mirrorChars[10908] = 10907;
			BidiLine.mirrorChars[10913] = 10914;
			BidiLine.mirrorChars[10914] = 10913;
			BidiLine.mirrorChars[10918] = 10919;
			BidiLine.mirrorChars[10919] = 10918;
			BidiLine.mirrorChars[10920] = 10921;
			BidiLine.mirrorChars[10921] = 10920;
			BidiLine.mirrorChars[10922] = 10923;
			BidiLine.mirrorChars[10923] = 10922;
			BidiLine.mirrorChars[10924] = 10925;
			BidiLine.mirrorChars[10925] = 10924;
			BidiLine.mirrorChars[10927] = 10928;
			BidiLine.mirrorChars[10928] = 10927;
			BidiLine.mirrorChars[10931] = 10932;
			BidiLine.mirrorChars[10932] = 10931;
			BidiLine.mirrorChars[10939] = 10940;
			BidiLine.mirrorChars[10940] = 10939;
			BidiLine.mirrorChars[10941] = 10942;
			BidiLine.mirrorChars[10942] = 10941;
			BidiLine.mirrorChars[10943] = 10944;
			BidiLine.mirrorChars[10944] = 10943;
			BidiLine.mirrorChars[10945] = 10946;
			BidiLine.mirrorChars[10946] = 10945;
			BidiLine.mirrorChars[10947] = 10948;
			BidiLine.mirrorChars[10948] = 10947;
			BidiLine.mirrorChars[10949] = 10950;
			BidiLine.mirrorChars[10950] = 10949;
			BidiLine.mirrorChars[10957] = 10958;
			BidiLine.mirrorChars[10958] = 10957;
			BidiLine.mirrorChars[10959] = 10960;
			BidiLine.mirrorChars[10960] = 10959;
			BidiLine.mirrorChars[10961] = 10962;
			BidiLine.mirrorChars[10962] = 10961;
			BidiLine.mirrorChars[10963] = 10964;
			BidiLine.mirrorChars[10964] = 10963;
			BidiLine.mirrorChars[10965] = 10966;
			BidiLine.mirrorChars[10966] = 10965;
			BidiLine.mirrorChars[10974] = 8870;
			BidiLine.mirrorChars[10979] = 8873;
			BidiLine.mirrorChars[10980] = 8872;
			BidiLine.mirrorChars[10981] = 8875;
			BidiLine.mirrorChars[10988] = 10989;
			BidiLine.mirrorChars[10989] = 10988;
			BidiLine.mirrorChars[10999] = 11000;
			BidiLine.mirrorChars[11000] = 10999;
			BidiLine.mirrorChars[11001] = 11002;
			BidiLine.mirrorChars[11002] = 11001;
			BidiLine.mirrorChars[12296] = 12297;
			BidiLine.mirrorChars[12297] = 12296;
			BidiLine.mirrorChars[12298] = 12299;
			BidiLine.mirrorChars[12299] = 12298;
			BidiLine.mirrorChars[12300] = 12301;
			BidiLine.mirrorChars[12301] = 12300;
			BidiLine.mirrorChars[12302] = 12303;
			BidiLine.mirrorChars[12303] = 12302;
			BidiLine.mirrorChars[12304] = 12305;
			BidiLine.mirrorChars[12305] = 12304;
			BidiLine.mirrorChars[12308] = 12309;
			BidiLine.mirrorChars[12309] = 12308;
			BidiLine.mirrorChars[12310] = 12311;
			BidiLine.mirrorChars[12311] = 12310;
			BidiLine.mirrorChars[12312] = 12313;
			BidiLine.mirrorChars[12313] = 12312;
			BidiLine.mirrorChars[12314] = 12315;
			BidiLine.mirrorChars[12315] = 12314;
			BidiLine.mirrorChars[65288] = 65289;
			BidiLine.mirrorChars[65289] = 65288;
			BidiLine.mirrorChars[65308] = 65310;
			BidiLine.mirrorChars[65310] = 65308;
			BidiLine.mirrorChars[65339] = 65341;
			BidiLine.mirrorChars[65341] = 65339;
			BidiLine.mirrorChars[65371] = 65373;
			BidiLine.mirrorChars[65373] = 65371;
			BidiLine.mirrorChars[65375] = 65376;
			BidiLine.mirrorChars[65376] = 65375;
			BidiLine.mirrorChars[65378] = 65379;
			BidiLine.mirrorChars[65379] = 65378;
		}

		// Token: 0x04001C90 RID: 7312
		private const int pieceSizeStart = 256;

		// Token: 0x04001C91 RID: 7313
		protected int runDirection;

		// Token: 0x04001C92 RID: 7314
		protected int pieceSize = 256;

		// Token: 0x04001C93 RID: 7315
		protected char[] text = new char[256];

		// Token: 0x04001C94 RID: 7316
		protected PdfChunk[] detailChunks = new PdfChunk[256];

		// Token: 0x04001C95 RID: 7317
		protected int totalTextLength;

		// Token: 0x04001C96 RID: 7318
		protected byte[] orderLevels = new byte[256];

		// Token: 0x04001C97 RID: 7319
		protected int[] indexChars = new int[256];

		// Token: 0x04001C98 RID: 7320
		protected List<PdfChunk> chunks = new List<PdfChunk>();

		// Token: 0x04001C99 RID: 7321
		protected int indexChunk;

		// Token: 0x04001C9A RID: 7322
		protected int indexChunkChar;

		// Token: 0x04001C9B RID: 7323
		protected int currentChar;

		// Token: 0x04001C9C RID: 7324
		protected int storedRunDirection;

		// Token: 0x04001C9D RID: 7325
		protected char[] storedText = new char[0];

		// Token: 0x04001C9E RID: 7326
		protected PdfChunk[] storedDetailChunks = new PdfChunk[0];

		// Token: 0x04001C9F RID: 7327
		protected int storedTotalTextLength;

		// Token: 0x04001CA0 RID: 7328
		protected byte[] storedOrderLevels = new byte[0];

		// Token: 0x04001CA1 RID: 7329
		protected int[] storedIndexChars = new int[0];

		// Token: 0x04001CA2 RID: 7330
		protected int storedIndexChunk;

		// Token: 0x04001CA3 RID: 7331
		protected int storedIndexChunkChar;

		// Token: 0x04001CA4 RID: 7332
		protected int storedCurrentChar;

		// Token: 0x04001CA5 RID: 7333
		protected bool shortStore;

		// Token: 0x04001CA6 RID: 7334
		protected static IntHashtable mirrorChars = new IntHashtable();

		// Token: 0x04001CA7 RID: 7335
		protected int arabicOptions;
	}
}
