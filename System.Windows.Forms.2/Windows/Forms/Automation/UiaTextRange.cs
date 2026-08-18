using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Internal;

namespace System.Windows.Forms.Automation
{
	// Token: 0x020004F5 RID: 1269
	internal class UiaTextRange : UnsafeNativeMethods.UiaCore.ITextRangeProvider
	{
		// Token: 0x06005272 RID: 21106 RVA: 0x00155BF0 File Offset: 0x00153DF0
		public UiaTextRange(UnsafeNativeMethods.IRawElementProviderSimple enclosingElement, UiaTextProvider provider, int start, int end)
		{
			if (enclosingElement == null)
			{
				throw new ArgumentNullException("enclosingElement");
			}
			this._enclosingElement = enclosingElement;
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._provider = provider;
			if (start > 0)
			{
				this._start = start;
				this._end = start;
			}
			if (end > this._start)
			{
				this._end = end;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06005273 RID: 21107 RVA: 0x00155C53 File Offset: 0x00153E53
		// (set) Token: 0x06005274 RID: 21108 RVA: 0x00155C5B File Offset: 0x00153E5B
		internal int End
		{
			get
			{
				return this._end;
			}
			set
			{
				if (value < 0)
				{
					this._end = 0;
				}
				else
				{
					this._end = value;
				}
				if (this._end < this._start)
				{
					this._start = this._end;
				}
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06005275 RID: 21109 RVA: 0x00155C8B File Offset: 0x00153E8B
		internal int Length
		{
			get
			{
				if (this.Start < 0 || this.End < 0 || this.Start > this.End)
				{
					return 0;
				}
				return this.End - this.Start;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06005276 RID: 21110 RVA: 0x00155CBC File Offset: 0x00153EBC
		// (set) Token: 0x06005277 RID: 21111 RVA: 0x00155CC4 File Offset: 0x00153EC4
		internal int Start
		{
			get
			{
				return this._start;
			}
			set
			{
				if (value > this._end)
				{
					this._end = value;
				}
				if (value < 0)
				{
					this._start = 0;
					return;
				}
				this._start = value;
			}
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06005278 RID: 21112 RVA: 0x00155CE9 File Offset: 0x00153EE9
		private bool IsDegenerate
		{
			get
			{
				return this._start == this._end;
			}
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x00155CF9 File Offset: 0x00153EF9
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextRangeProvider.Clone()
		{
			return new UiaTextRange(this._enclosingElement, this._provider, this.Start, this.End);
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x00155D18 File Offset: 0x00153F18
		UnsafeNativeMethods.BOOL UnsafeNativeMethods.UiaCore.ITextRangeProvider.Compare(UnsafeNativeMethods.UiaCore.ITextRangeProvider range)
		{
			UiaTextRange uiaTextRange = range as UiaTextRange;
			if (uiaTextRange == null || uiaTextRange.Start != this.Start || uiaTextRange.End != this.End)
			{
				return UnsafeNativeMethods.BOOL.FALSE;
			}
			return UnsafeNativeMethods.BOOL.TRUE;
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x00155D50 File Offset: 0x00153F50
		int UnsafeNativeMethods.UiaCore.ITextRangeProvider.CompareEndpoints(UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint endpoint, UnsafeNativeMethods.UiaCore.ITextRangeProvider targetRange, UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint targetEndpoint)
		{
			UiaTextRange uiaTextRange = targetRange as UiaTextRange;
			if (uiaTextRange == null)
			{
				return -1;
			}
			int num = (endpoint == UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint.Start) ? this.Start : this.End;
			int num2 = (targetEndpoint == UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint.Start) ? uiaTextRange.Start : uiaTextRange.End;
			return num - num2;
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x00155D90 File Offset: 0x00153F90
		void UnsafeNativeMethods.UiaCore.ITextRangeProvider.ExpandToEnclosingUnit(UnsafeNativeMethods.UiaCore.TextUnit unit)
		{
			switch (unit)
			{
			case UnsafeNativeMethods.UiaCore.TextUnit.Character:
				if (this.IsDegenerate)
				{
					int num;
					this.End = this.MoveEndpointForward(this.End, UnsafeNativeMethods.UiaCore.TextUnit.Character, 1, out num);
					return;
				}
				return;
			case UnsafeNativeMethods.UiaCore.TextUnit.Format:
			case UnsafeNativeMethods.UiaCore.TextUnit.Page:
			case UnsafeNativeMethods.UiaCore.TextUnit.Document:
				this.MoveTo(0, this._provider.TextLength);
				return;
			case UnsafeNativeMethods.UiaCore.TextUnit.Word:
			{
				string text = this._provider.Text;
				this.ValidateEndpoints();
				while (!UiaTextRange.AtWordBoundary(text, this.Start))
				{
					int num2 = this.Start;
					this.Start = num2 - 1;
				}
				this.End = Math.Min(Math.Max(this.End, this.Start + 1), text.Length);
				while (!UiaTextRange.AtWordBoundary(text, this.End))
				{
					int num2 = this.End;
					this.End = num2 + 1;
				}
				return;
			}
			case UnsafeNativeMethods.UiaCore.TextUnit.Line:
				if (this._provider.LinesCount != 1)
				{
					int lineFromCharIndex = this._provider.GetLineFromCharIndex(this.Start);
					int lineIndex = this._provider.GetLineIndex(lineFromCharIndex);
					int num3 = this._provider.GetLineFromCharIndex(this.End);
					int end;
					if (num3 < this._provider.LinesCount - 1)
					{
						num3++;
						end = this._provider.GetLineIndex(num3);
					}
					else
					{
						end = this._provider.TextLength;
					}
					this.MoveTo(lineIndex, end);
					return;
				}
				this.MoveTo(0, this._provider.TextLength);
				return;
			case UnsafeNativeMethods.UiaCore.TextUnit.Paragraph:
			{
				string text2 = this._provider.Text;
				this.ValidateEndpoints();
				while (!UiaTextRange.AtParagraphBoundary(text2, this.Start))
				{
					int num2 = this.Start;
					this.Start = num2 - 1;
				}
				this.End = Math.Min(Math.Max(this.End, this.Start + 1), text2.Length);
				while (!UiaTextRange.AtParagraphBoundary(text2, this.End))
				{
					int num2 = this.End;
					this.End = num2 + 1;
				}
				return;
			}
			default:
				throw new InvalidEnumArgumentException("unit", (int)unit, typeof(UnsafeNativeMethods.UiaCore.TextUnit));
			}
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x00015ECC File Offset: 0x000140CC
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextRangeProvider.FindAttribute(int attributeId, object val, UnsafeNativeMethods.BOOL backwards)
		{
			return null;
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x00155F98 File Offset: 0x00154198
		UnsafeNativeMethods.UiaCore.ITextRangeProvider UnsafeNativeMethods.UiaCore.ITextRangeProvider.FindText(string text, UnsafeNativeMethods.BOOL backwards, UnsafeNativeMethods.BOOL ignoreCase)
		{
			if (text == null)
			{
				return null;
			}
			if (text.Length == 0)
			{
				return null;
			}
			this.ValidateEndpoints();
			string text2 = this._provider.Text;
			StringComparison comparisonType = (ignoreCase == UnsafeNativeMethods.BOOL.TRUE) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
			int num = (backwards == UnsafeNativeMethods.BOOL.TRUE) ? text2.LastIndexOf(text, comparisonType) : text2.IndexOf(text, comparisonType);
			if (num < 0)
			{
				return null;
			}
			return new UiaTextRange(this._enclosingElement, this._provider, this.Start + num, this.Start + num + text.Length);
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x00156014 File Offset: 0x00154214
		object UnsafeNativeMethods.UiaCore.ITextRangeProvider.GetAttributeValue(int attributeId)
		{
			return this.GetAttributeValue((UnsafeNativeMethods.UiaCore.TextAttributeIdentifier)attributeId);
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x00156020 File Offset: 0x00154220
		double[] UnsafeNativeMethods.UiaCore.ITextRangeProvider.GetBoundingRectangles()
		{
			Rectangle item = Rectangle.Empty;
			object propertyValue = this._enclosingElement.GetPropertyValue(30001);
			if (propertyValue is Rectangle)
			{
				Rectangle rectangle = (Rectangle)propertyValue;
				item = rectangle;
			}
			List<Rectangle> list = new List<Rectangle>();
			if (this._provider.TextLength == 0)
			{
				list.Add(item);
				return this._provider.RectListToDoubleArray(list);
			}
			if (this.Start == this._provider.TextLength)
			{
				Point pt;
				UnsafeNativeMethods.User32.GetCaretPos(out pt);
				pt = this._provider.PointToScreen(pt);
				Rectangle rectangle2 = new Rectangle(pt.X, pt.Y + 2, 2, Math.Abs(this._provider.Logfont.lfHeight) + 1);
				return new double[]
				{
					(double)rectangle2.X,
					(double)rectangle2.Y,
					(double)rectangle2.Width,
					(double)rectangle2.Height
				};
			}
			if (this.IsDegenerate)
			{
				return new double[0];
			}
			string text = this._provider.Text;
			this.ValidateEndpoints();
			Point mapClientToScreen = new Point(item.X, item.Y);
			Rectangle boundingRectangle = this._provider.BoundingRectangle;
			if (this._provider.IsMultiline)
			{
				list = this.GetMultilineBoundingRectangles(text, mapClientToScreen, boundingRectangle);
				return this._provider.RectListToDoubleArray(list);
			}
			Point positionFromChar = this._provider.GetPositionFromChar(this.Start);
			Point positionFromCharForUpperRightCorner = this._provider.GetPositionFromCharForUpperRightCorner(this.End - 1, text);
			Rectangle item2 = new Rectangle(positionFromChar.X, positionFromChar.Y + 2, positionFromCharForUpperRightCorner.X - positionFromChar.X, boundingRectangle.Height);
			item2.Intersect(boundingRectangle);
			if (item2.Width > 0 && item2.Height > 0)
			{
				item2.Offset(mapClientToScreen.X, mapClientToScreen.Y);
				list.Add(item2);
			}
			return this._provider.RectListToDoubleArray(list);
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x00156210 File Offset: 0x00154410
		UnsafeNativeMethods.IRawElementProviderSimple UnsafeNativeMethods.UiaCore.ITextRangeProvider.GetEnclosingElement()
		{
			return this._enclosingElement;
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x00156218 File Offset: 0x00154418
		string UnsafeNativeMethods.UiaCore.ITextRangeProvider.GetText(int maxLength)
		{
			if (maxLength == -1)
			{
				maxLength = this.End + 1;
			}
			string text = this._provider.Text;
			this.ValidateEndpoints();
			maxLength = ((maxLength >= 0) ? Math.Min(this.Length, maxLength) : this.Length);
			if (text.Length >= maxLength - this.Start)
			{
				return text.Substring(this.Start, maxLength);
			}
			return text.Substring(this.Start);
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x0015628C File Offset: 0x0015448C
		int UnsafeNativeMethods.UiaCore.ITextRangeProvider.Move(UnsafeNativeMethods.UiaCore.TextUnit unit, int count)
		{
			if (count > 0)
			{
				if (!this.IsDegenerate)
				{
					this.Start = this.End;
				}
				int start = this.Start;
				int result;
				this.Start = this.MoveEndpointForward(this.Start, unit, count, out result);
				if (start != this.Start)
				{
					return result;
				}
			}
			if (count < 0)
			{
				if (!this.IsDegenerate)
				{
					this.End = this.Start;
				}
				int end = this.End;
				int result;
				this.End = this.MoveEndpointBackward(this.End, unit, count, out result);
				if (end != this.End)
				{
					return result;
				}
			}
			return 0;
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x0015631C File Offset: 0x0015451C
		int UnsafeNativeMethods.UiaCore.ITextRangeProvider.MoveEndpointByUnit(UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint endpoint, UnsafeNativeMethods.UiaCore.TextUnit unit, int count)
		{
			bool flag = endpoint == UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint.Start;
			int start = this.Start;
			int end = this.End;
			if (count > 0)
			{
				if (flag)
				{
					int result;
					this.Start = this.MoveEndpointForward(this.Start, unit, count, out result);
					if (start != this.Start)
					{
						return result;
					}
					return 0;
				}
				else
				{
					int result;
					this.End = this.MoveEndpointForward(this.End, unit, count, out result);
					if (end != this.End)
					{
						return result;
					}
					return 0;
				}
			}
			else
			{
				if (count >= 0)
				{
					return 0;
				}
				if (flag)
				{
					int result;
					this.Start = this.MoveEndpointBackward(this.Start, unit, count, out result);
					if (start != this.Start)
					{
						return result;
					}
					return 0;
				}
				else
				{
					int result;
					this.End = this.MoveEndpointBackward(this.End, unit, count, out result);
					if (end != this.End)
					{
						return result;
					}
					return 0;
				}
			}
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x001563D8 File Offset: 0x001545D8
		void UnsafeNativeMethods.UiaCore.ITextRangeProvider.MoveEndpointByRange(UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint endpoint, UnsafeNativeMethods.UiaCore.ITextRangeProvider targetRange, UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint targetEndpoint)
		{
			UiaTextRange uiaTextRange = targetRange as UiaTextRange;
			if (uiaTextRange == null)
			{
				return;
			}
			int num = (targetEndpoint == UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint.Start) ? uiaTextRange.Start : uiaTextRange.End;
			if (endpoint == UnsafeNativeMethods.UiaCore.TextPatternRangeEndpoint.Start)
			{
				this.Start = num;
				return;
			}
			this.End = num;
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x00156414 File Offset: 0x00154614
		void UnsafeNativeMethods.UiaCore.ITextRangeProvider.Select()
		{
			this._provider.SetSelection(this.Start, this.End);
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.UiaCore.ITextRangeProvider.AddToSelection()
		{
		}

		// Token: 0x06005288 RID: 21128 RVA: 0x000072B6 File Offset: 0x000054B6
		void UnsafeNativeMethods.UiaCore.ITextRangeProvider.RemoveFromSelection()
		{
		}

		// Token: 0x06005289 RID: 21129 RVA: 0x00156430 File Offset: 0x00154630
		void UnsafeNativeMethods.UiaCore.ITextRangeProvider.ScrollIntoView(UnsafeNativeMethods.BOOL alignToTop)
		{
			if (this._provider.IsMultiline)
			{
				int num = (alignToTop == UnsafeNativeMethods.BOOL.TRUE) ? this._provider.GetLineFromCharIndex(this.Start) : Math.Max(0, this._provider.GetLineFromCharIndex(this.End) - this._provider.LinesPerPage + 1);
				this._provider.LineScroll(this.Start, num - this._provider.FirstVisibleLine);
				return;
			}
			if (this._provider.IsScrollable)
			{
				int num2;
				int num3;
				this._provider.GetVisibleRangePoints(out num2, out num3);
				short vk = (this.Start > num2) ? 39 : 37;
				if (this._provider.IsReadingRTL)
				{
					if (this.Start > num2 || this.Start < num3)
					{
						this._provider.SendKeyboardInputVK(vk, true);
						this._provider.GetVisibleRangePoints(out num2, out num3);
					}
					return;
				}
				if (this.Start < num2 || this.Start > num3)
				{
					this._provider.SendKeyboardInputVK(vk, true);
					this._provider.GetVisibleRangePoints(out num2, out num3);
				}
			}
		}

		// Token: 0x0600528A RID: 21130 RVA: 0x00156543 File Offset: 0x00154743
		UnsafeNativeMethods.IRawElementProviderSimple[] UnsafeNativeMethods.UiaCore.ITextRangeProvider.GetChildren()
		{
			return new UnsafeNativeMethods.IRawElementProviderSimple[0];
		}

		// Token: 0x0600528B RID: 21131 RVA: 0x0015654B File Offset: 0x0015474B
		private static bool AtParagraphBoundary(string text, int index)
		{
			return string.IsNullOrWhiteSpace(text) || index <= 0 || index >= text.Length || (text[index - 1] == '\n' && text[index] != '\n');
		}

		// Token: 0x0600528C RID: 21132 RVA: 0x00156584 File Offset: 0x00154784
		private static bool AtWordBoundary(string text, int index)
		{
			if (string.IsNullOrWhiteSpace(text) || index <= 0 || index >= text.Length || UiaTextRange.AtParagraphBoundary(text, index))
			{
				return true;
			}
			char c = text[index - 1];
			char c2 = text[index];
			return (!char.IsLetterOrDigit(c) || !UiaTextRange.IsApostrophe(c2)) && (!UiaTextRange.IsApostrophe(c) || !char.IsLetterOrDigit(c2) || index < 2 || !char.IsLetterOrDigit(text[index - 2])) && ((char.IsWhiteSpace(c) && !char.IsWhiteSpace(c2)) || (char.IsLetterOrDigit(c) && !char.IsLetterOrDigit(c2)) || (!char.IsLetterOrDigit(c) && char.IsLetterOrDigit(c2)) || (char.IsPunctuation(c) && char.IsWhiteSpace(c2)));
		}

		// Token: 0x0600528D RID: 21133 RVA: 0x0015663B File Offset: 0x0015483B
		private static bool IsApostrophe(char ch)
		{
			return ch == '\'' || ch == '’';
		}

		// Token: 0x0600528E RID: 21134 RVA: 0x0015664C File Offset: 0x0015484C
		private object GetAttributeValue(UnsafeNativeMethods.UiaCore.TextAttributeIdentifier textAttributeIdentifier)
		{
			switch (textAttributeIdentifier)
			{
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.BackgroundColorAttributeId:
				return UiaTextRange.GetBackgroundColor();
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.BulletStyleAttributeId:
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.CultureAttributeId:
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.IndentationFirstLineAttributeId:
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.IndentationLeadingAttributeId:
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.IndentationTrailingAttributeId:
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.IsHiddenAttributeId:
				break;
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.CapStyleAttributeId:
				return this.GetCapStyle(this._provider.EditStyle);
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.FontNameAttributeId:
				return UiaTextRange.GetFontName(this._provider.Logfont);
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.FontSizeAttributeId:
				return this.GetFontSize(this._provider.Logfont);
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.FontWeightAttributeId:
				return UiaTextRange.GetFontWeight(this._provider.Logfont);
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.ForegroundColorAttributeId:
				return UiaTextRange.GetForegroundColor();
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.HorizontalTextAlignmentAttributeId:
				return this.GetHorizontalTextAlignment(this._provider.EditStyle);
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.IsItalicAttributeId:
				return UiaTextRange.GetItalic(this._provider.Logfont);
			case UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.IsReadOnlyAttributeId:
				return this.GetReadOnly();
			default:
				if (textAttributeIdentifier == UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.StrikethroughStyleAttributeId)
				{
					return UiaTextRange.GetStrikethroughStyle(this._provider.Logfont);
				}
				if (textAttributeIdentifier == UnsafeNativeMethods.UiaCore.TextAttributeIdentifier.UnderlineStyleAttributeId)
				{
					return UiaTextRange.GetUnderlineStyle(this._provider.Logfont);
				}
				break;
			}
			return UnsafeNativeMethods.UiaCore.UiaGetReservedNotSupportedValue();
		}

		// Token: 0x0600528F RID: 21135 RVA: 0x00156794 File Offset: 0x00154994
		private List<Rectangle> GetMultilineBoundingRectangles(string text, Point mapClientToScreen, Rectangle clippingRectangle)
		{
			int num = Math.Abs(this._provider.Logfont.lfHeight);
			int num2 = this.Start;
			int num3 = this.End;
			int num4 = this._provider.GetLineFromCharIndex(num2);
			int num5 = this._provider.GetLineFromCharIndex(num3 - 1);
			int firstVisibleLine = this._provider.FirstVisibleLine;
			if (firstVisibleLine > num4)
			{
				num4 = firstVisibleLine;
				num2 = this._provider.GetLineIndex(num4);
			}
			int num6 = firstVisibleLine + this._provider.LinesPerPage - 1;
			if (num6 < num5)
			{
				num5 = num6;
				num3 = this._provider.GetLineIndex(num5) - 1;
			}
			List<Rectangle> list = new List<Rectangle>();
			int lineIndex = this._provider.GetLineIndex(num4);
			for (int i = num4; i <= num5; i++)
			{
				Point positionFromChar = this._provider.GetPositionFromChar((i == num4) ? num2 : lineIndex);
				Point point;
				if (i == num5)
				{
					point = this._provider.GetPositionFromCharForUpperRightCorner(num3 - 1, text);
				}
				else
				{
					lineIndex = this._provider.GetLineIndex(i + 1);
					point = this._provider.GetPositionFromChar(lineIndex - 1);
				}
				Rectangle item = new Rectangle(positionFromChar.X, positionFromChar.Y + 2, point.X - positionFromChar.X, num + 1);
				item.Intersect(clippingRectangle);
				if (item.Width > 0 && item.Height > 0)
				{
					item.Offset(mapClientToScreen.X, mapClientToScreen.Y);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06005290 RID: 21136 RVA: 0x00156913 File Offset: 0x00154B13
		private HorizontalTextAlignment GetHorizontalTextAlignment(int editStyle)
		{
			if (NativeMethods.HasFlag(editStyle, 1))
			{
				return HorizontalTextAlignment.Centered;
			}
			if (NativeMethods.HasFlag(editStyle, 2))
			{
				return HorizontalTextAlignment.Right;
			}
			return HorizontalTextAlignment.Left;
		}

		// Token: 0x06005291 RID: 21137 RVA: 0x0015692C File Offset: 0x00154B2C
		private CapStyle GetCapStyle(int editStyle)
		{
			if (!NativeMethods.HasFlag(editStyle, 8))
			{
				return CapStyle.None;
			}
			return CapStyle.AllCap;
		}

		// Token: 0x06005292 RID: 21138 RVA: 0x0015693A File Offset: 0x00154B3A
		private bool GetReadOnly()
		{
			return this._provider.IsReadOnly;
		}

		// Token: 0x06005293 RID: 21139 RVA: 0x00156947 File Offset: 0x00154B47
		private static int GetBackgroundColor()
		{
			return SafeNativeMethods.GetSysColor(5);
		}

		// Token: 0x06005294 RID: 21140 RVA: 0x0015694F File Offset: 0x00154B4F
		private static string GetFontName(NativeMethods.LOGFONT logfont)
		{
			return logfont.lfFaceName;
		}

		// Token: 0x06005295 RID: 21141 RVA: 0x00156958 File Offset: 0x00154B58
		private double GetFontSize(NativeMethods.LOGFONT logfont)
		{
			IntPtr dc = IntUnsafeNativeMethods.GetDC(new HandleRef(null, IntPtr.Zero));
			int deviceCaps = UnsafeNativeMethods.GetDeviceCaps(new HandleRef(null, dc), 90);
			return Math.Round((double)(-(double)logfont.lfHeight) * 72.0 / (double)deviceCaps);
		}

		// Token: 0x06005296 RID: 21142 RVA: 0x0015699F File Offset: 0x00154B9F
		private static int GetFontWeight(NativeMethods.LOGFONT logfont)
		{
			return logfont.lfWeight;
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x001569A7 File Offset: 0x00154BA7
		private static int GetForegroundColor()
		{
			return SafeNativeMethods.GetSysColor(8);
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x001569AF File Offset: 0x00154BAF
		private static bool GetItalic(NativeMethods.LOGFONT logfont)
		{
			return logfont.lfItalic > 0;
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x001569BA File Offset: 0x00154BBA
		private static TextDecorationLineStyle GetStrikethroughStyle(NativeMethods.LOGFONT logfont)
		{
			if (logfont.lfStrikeOut == 0)
			{
				return TextDecorationLineStyle.None;
			}
			return TextDecorationLineStyle.Single;
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x001569C7 File Offset: 0x00154BC7
		private static TextDecorationLineStyle GetUnderlineStyle(NativeMethods.LOGFONT logfont)
		{
			if (logfont.lfUnderline == 0)
			{
				return TextDecorationLineStyle.None;
			}
			return TextDecorationLineStyle.Single;
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x001569D4 File Offset: 0x00154BD4
		private int MoveEndpointForward(int index, UnsafeNativeMethods.UiaCore.TextUnit unit, int count, out int moved)
		{
			switch (unit)
			{
			case UnsafeNativeMethods.UiaCore.TextUnit.Character:
			{
				int textLength = this._provider.TextLength;
				this.ValidateEndpoints();
				moved = Math.Min(count, textLength - index);
				index += moved;
				index = ((index > textLength) ? textLength : index);
				break;
			}
			case UnsafeNativeMethods.UiaCore.TextUnit.Format:
			case UnsafeNativeMethods.UiaCore.TextUnit.Page:
			case UnsafeNativeMethods.UiaCore.TextUnit.Document:
			{
				int textLength2 = this._provider.TextLength;
				this.ValidateEndpoints();
				moved = ((index < textLength2) ? 1 : 0);
				index = textLength2;
				break;
			}
			case UnsafeNativeMethods.UiaCore.TextUnit.Word:
			{
				string text = this._provider.Text;
				this.ValidateEndpoints();
				for (moved = 0; moved < count; moved++)
				{
					if (index >= text.Length)
					{
						break;
					}
					index++;
					while (!UiaTextRange.AtWordBoundary(text, index))
					{
						index++;
					}
				}
				break;
			}
			case UnsafeNativeMethods.UiaCore.TextUnit.Line:
			{
				int lineFromCharIndex = this._provider.GetLineFromCharIndex(index);
				int linesCount = this._provider.LinesCount;
				moved = Math.Min(count, linesCount - lineFromCharIndex - 1);
				if (moved > 0)
				{
					index = this._provider.GetLineIndex(lineFromCharIndex + moved);
				}
				else if (moved == 0 && linesCount == 1)
				{
					index = this._provider.TextLength;
					moved = 1;
				}
				break;
			}
			case UnsafeNativeMethods.UiaCore.TextUnit.Paragraph:
			{
				string text2 = this._provider.Text;
				this.ValidateEndpoints();
				for (moved = 0; moved < count; moved++)
				{
					if (index >= text2.Length)
					{
						break;
					}
					index++;
					while (!UiaTextRange.AtParagraphBoundary(text2, index))
					{
						index++;
					}
				}
				break;
			}
			default:
				throw new InvalidEnumArgumentException("unit", (int)unit, typeof(UnsafeNativeMethods.UiaCore.TextUnit));
			}
			return index;
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x00156B78 File Offset: 0x00154D78
		private int MoveEndpointBackward(int index, UnsafeNativeMethods.UiaCore.TextUnit unit, int count, out int moved)
		{
			switch (unit)
			{
			case UnsafeNativeMethods.UiaCore.TextUnit.Character:
			{
				this.ValidateEndpoints();
				int num = index + 1;
				moved = Math.Max(count, -num);
				index += moved;
				index = ((index < 0) ? 0 : index);
				break;
			}
			case UnsafeNativeMethods.UiaCore.TextUnit.Format:
			case UnsafeNativeMethods.UiaCore.TextUnit.Page:
			case UnsafeNativeMethods.UiaCore.TextUnit.Document:
				moved = ((index > 0) ? -1 : 0);
				index = 0;
				break;
			case UnsafeNativeMethods.UiaCore.TextUnit.Word:
			{
				string text = this._provider.Text;
				this.ValidateEndpoints();
				for (moved = 0; moved > count; moved--)
				{
					if (index <= 0)
					{
						break;
					}
					index--;
					while (!UiaTextRange.AtWordBoundary(text, index))
					{
						index--;
					}
				}
				break;
			}
			case UnsafeNativeMethods.UiaCore.TextUnit.Line:
			{
				int num2 = this._provider.GetLineFromCharIndex(index) + 1;
				int linesCount = this._provider.LinesCount;
				int num3 = Math.Max(count, -num2);
				moved = num3;
				if (num3 == -num2)
				{
					index = 0;
					bool flag = linesCount == 0 || (linesCount > 1 && this._provider.GetLineIndex(1) == "\r\n".Length);
					if (moved < 0 && flag)
					{
						moved++;
					}
				}
				else
				{
					index = this._provider.GetLineIndex(num2 + num3) - "\r\n".Length;
				}
				break;
			}
			case UnsafeNativeMethods.UiaCore.TextUnit.Paragraph:
			{
				string text2 = this._provider.Text;
				this.ValidateEndpoints();
				for (moved = 0; moved > count; moved--)
				{
					if (index <= 0)
					{
						break;
					}
					index--;
					while (!UiaTextRange.AtParagraphBoundary(text2, index))
					{
						index--;
					}
				}
				break;
			}
			default:
				throw new InvalidEnumArgumentException("unit", (int)unit, typeof(UnsafeNativeMethods.UiaCore.TextUnit));
			}
			return index;
		}

		// Token: 0x0600529D RID: 21149 RVA: 0x00156D1B File Offset: 0x00154F1B
		private void MoveTo(int start, int end)
		{
			if (start < 0)
			{
				throw new ArgumentOutOfRangeException("start");
			}
			this._start = start;
			if (end < start)
			{
				throw new ArgumentOutOfRangeException("end");
			}
			this._end = end;
		}

		// Token: 0x0600529E RID: 21150 RVA: 0x00156D4C File Offset: 0x00154F4C
		private void ValidateEndpoints()
		{
			int textLength = this._provider.TextLength;
			if (this.Start > textLength && textLength > 0)
			{
				this.Start = textLength;
			}
			if (this.End > textLength && textLength > 0)
			{
				this.End = textLength;
			}
		}

		// Token: 0x0400364E RID: 13902
		private const string LineSeparator = "\r\n";

		// Token: 0x0400364F RID: 13903
		private readonly UnsafeNativeMethods.IRawElementProviderSimple _enclosingElement;

		// Token: 0x04003650 RID: 13904
		private readonly UiaTextProvider _provider;

		// Token: 0x04003651 RID: 13905
		private int _start;

		// Token: 0x04003652 RID: 13906
		private int _end;
	}
}
