using System;
using System.Globalization;
using System.Text;
using System.Web.Razor.Parser.SyntaxTree;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Razor.Text
{
	// Token: 0x02000096 RID: 150
	public struct TextChange
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x00018406 File Offset: 0x00016606
		internal TextChange(int position, int oldLength, ITextBuffer oldBuffer, int newLength, ITextBuffer newBuffer)
		{
			this = new TextChange(position, oldLength, oldBuffer, position, newLength, newBuffer);
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00018418 File Offset: 0x00016618
		public TextChange(int oldPosition, int oldLength, ITextBuffer oldBuffer, int newPosition, int newLength, ITextBuffer newBuffer)
		{
			this = default(TextChange);
			if (oldPosition < 0)
			{
				throw new ArgumentOutOfRangeException("oldPosition", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					"0"
				}));
			}
			if (newPosition < 0)
			{
				throw new ArgumentOutOfRangeException("newPosition", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					"0"
				}));
			}
			if (oldLength < 0)
			{
				throw new ArgumentOutOfRangeException("oldLength", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					"0"
				}));
			}
			if (newLength < 0)
			{
				throw new ArgumentOutOfRangeException("newLength", string.Format(CultureInfo.CurrentCulture, CommonResources.Argument_Must_Be_GreaterThanOrEqualTo, new object[]
				{
					"0"
				}));
			}
			if (oldBuffer == null)
			{
				throw new ArgumentNullException("oldBuffer");
			}
			if (newBuffer == null)
			{
				throw new ArgumentNullException("newBuffer");
			}
			this.OldPosition = oldPosition;
			this.NewPosition = newPosition;
			this.OldLength = oldLength;
			this.NewLength = newLength;
			this.NewBuffer = newBuffer;
			this.OldBuffer = oldBuffer;
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060006AC RID: 1708 RVA: 0x00018530 File Offset: 0x00016730
		// (set) Token: 0x060006AD RID: 1709 RVA: 0x00018538 File Offset: 0x00016738
		public int OldPosition { get; private set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006AE RID: 1710 RVA: 0x00018541 File Offset: 0x00016741
		// (set) Token: 0x060006AF RID: 1711 RVA: 0x00018549 File Offset: 0x00016749
		public int NewPosition { get; private set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00018552 File Offset: 0x00016752
		// (set) Token: 0x060006B1 RID: 1713 RVA: 0x0001855A File Offset: 0x0001675A
		public int OldLength { get; private set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00018563 File Offset: 0x00016763
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0001856B File Offset: 0x0001676B
		public int NewLength { get; private set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00018574 File Offset: 0x00016774
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0001857C File Offset: 0x0001677C
		public ITextBuffer NewBuffer { get; private set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00018585 File Offset: 0x00016785
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0001858D File Offset: 0x0001678D
		public ITextBuffer OldBuffer { get; private set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00018596 File Offset: 0x00016796
		public string OldText
		{
			get
			{
				if (this._oldText == null && this.OldBuffer != null)
				{
					this._oldText = TextChange.GetText(this.OldBuffer, this.OldPosition, this.OldLength);
				}
				return this._oldText;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x000185CB File Offset: 0x000167CB
		public string NewText
		{
			get
			{
				if (this._newText == null)
				{
					this._newText = TextChange.GetText(this.NewBuffer, this.NewPosition, this.NewLength);
				}
				return this._newText;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x000185F8 File Offset: 0x000167F8
		public bool IsInsert
		{
			get
			{
				return this.OldLength == 0 && this.NewLength > 0;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0001860D File Offset: 0x0001680D
		public bool IsDelete
		{
			get
			{
				return this.OldLength > 0 && this.NewLength == 0;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x00018623 File Offset: 0x00016823
		public bool IsReplace
		{
			get
			{
				return this.OldLength > 0 && this.NewLength > 0;
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001863C File Offset: 0x0001683C
		public override bool Equals(object obj)
		{
			if (!(obj is TextChange))
			{
				return false;
			}
			TextChange textChange = (TextChange)obj;
			return textChange.OldPosition == this.OldPosition && textChange.NewPosition == this.NewPosition && textChange.OldLength == this.OldLength && textChange.NewLength == this.NewLength && this.OldBuffer.Equals(textChange.OldBuffer) && this.NewBuffer.Equals(textChange.NewBuffer);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x000186C0 File Offset: 0x000168C0
		public string ApplyChange(string content, int changeOffset)
		{
			int startIndex = this.OldPosition - changeOffset;
			return content.Remove(startIndex, this.OldLength).Insert(startIndex, this.NewText);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000186F0 File Offset: 0x000168F0
		public string ApplyChange(Span span)
		{
			return this.ApplyChange(span.Content, span.Start.AbsoluteIndex);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00018717 File Offset: 0x00016917
		public override int GetHashCode()
		{
			return this.OldPosition ^ this.NewPosition ^ this.OldLength ^ this.NewLength ^ this.NewBuffer.GetHashCode() ^ this.OldBuffer.GetHashCode();
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001874C File Offset: 0x0001694C
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "({0}:{1}) \"{3}\" -> ({0}:{2}) \"{4}\"", new object[]
			{
				this.OldPosition,
				this.OldLength,
				this.NewLength,
				this.OldText,
				this.NewText
			});
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000187AC File Offset: 0x000169AC
		public TextChange Normalize()
		{
			if (this.OldBuffer != null && this.IsReplace && this.NewLength > this.OldLength && this.NewText.StartsWith(this.OldText, StringComparison.Ordinal) && this.NewPosition == this.OldPosition)
			{
				return new TextChange(this.OldPosition + this.OldLength, 0, this.OldBuffer, this.OldPosition + this.OldLength, this.NewLength - this.OldLength, this.NewBuffer);
			}
			return this;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001883C File Offset: 0x00016A3C
		private static string GetText(ITextBuffer buffer, int position, int length)
		{
			if (length == 0)
			{
				return string.Empty;
			}
			int position2 = buffer.Position;
			string result;
			try
			{
				buffer.Position = position;
				if (length == 1)
				{
					result = ((char)buffer.Read()).ToString();
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < length; i++)
					{
						char c = (char)buffer.Read();
						stringBuilder.Append(c);
						if (char.IsHighSurrogate(c))
						{
							stringBuilder.Append((char)buffer.Read());
						}
					}
					result = stringBuilder.ToString();
				}
			}
			finally
			{
				buffer.Position = position2;
			}
			return result;
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x000188D4 File Offset: 0x00016AD4
		public static bool operator ==(TextChange left, TextChange right)
		{
			return left.Equals(right);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000188E9 File Offset: 0x00016AE9
		public static bool operator !=(TextChange left, TextChange right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000334 RID: 820
		private string _newText;

		// Token: 0x04000335 RID: 821
		private string _oldText;
	}
}
