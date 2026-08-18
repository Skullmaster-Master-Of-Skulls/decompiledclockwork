using System;
using System.Collections;
using System.Drawing;
using System.Text;
using Telerik.Web.Apoc.Layout.Inline;
using Telerik.Web.Apoc.Render;
using Telerik.Web.Apoc.Render.Pdf;
using Telerik.Web.UI;

namespace Telerik.Web.Apoc.Layout
{
	// Token: 0x020015F2 RID: 5618
	internal class LineArea : Area
	{
		// Token: 0x17004325 RID: 17189
		// (get) Token: 0x0600DAE1 RID: 56033 RVA: 0x002FE095 File Offset: 0x002FC295
		internal PdfRendererOptions PdfOptions
		{
			get
			{
				if (this.options == null)
				{
					this.options = (ApocDriver.ActiveDriver.Options as PdfRendererOptions);
				}
				return this.options;
			}
		}

		// Token: 0x0600DAE2 RID: 56034 RVA: 0x002FE0BC File Offset: 0x002FC2BC
		public LineArea(FontState fontState, int lineHeight, int halfLeading, int allocationWidth, int startIndent, int endIndent, LineArea prevLineArea) : base(fontState)
		{
			this.currentFontState = fontState;
			this.lineHeight = lineHeight;
			this.nominalFontSize = fontState.FontSize;
			this.nominalGlyphHeight = fontState.Ascender - fontState.Descender;
			this.placementOffset = fontState.Ascender;
			this.contentRectangleWidth = allocationWidth - startIndent - endIndent;
			this.fontState = fontState;
			this.allocationHeight = this.nominalGlyphHeight;
			this.halfLeading = this.lineHeight - this.allocationHeight;
			this.startIndent = startIndent;
			this.endIndent = endIndent;
			if (prevLineArea != null)
			{
				IEnumerator enumerator = prevLineArea.pendingAreas.GetEnumerator();
				Box box = null;
				bool flag = true;
				int num = 0;
				while (flag)
				{
					if (enumerator.MoveNext())
					{
						box = (Box)enumerator.Current;
						InlineSpace inlineSpace = box as InlineSpace;
						if (inlineSpace != null)
						{
							if (inlineSpace.isEatable())
							{
								num += inlineSpace.getSize();
							}
							else
							{
								flag = false;
							}
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						flag = false;
						box = null;
					}
				}
				while (box != null)
				{
					this.pendingAreas.Add(box);
					if (enumerator.MoveNext())
					{
						box = (Box)enumerator.Current;
					}
					else
					{
						box = null;
					}
				}
				this.pendingWidth = prevLineArea.getPendingWidth() - num;
			}
		}

		// Token: 0x0600DAE3 RID: 56035 RVA: 0x002FE1F1 File Offset: 0x002FC3F1
		public override void render(Telerik.Web.Apoc.Render.IRenderer renderer)
		{
			renderer.RenderLineArea(this);
		}

		// Token: 0x0600DAE4 RID: 56036 RVA: 0x002FE1FC File Offset: 0x002FC3FC
		public int addPageNumberCitation(string refid, LinkSet ls)
		{
			int width = this.currentFontState.GetWidth(this.currentFontState.MapCharacter(' '));
			PageNumberInlineArea pageNumberInlineArea = new PageNumberInlineArea(this.currentFontState, this.red, this.green, this.blue, refid, width);
			pageNumberInlineArea.setYOffset(this.placementOffset);
			this.pendingAreas.Add(pageNumberInlineArea);
			this.pendingWidth += width;
			this.prev = 2;
			return -1;
		}

		// Token: 0x0600DAE5 RID: 56037 RVA: 0x002FE274 File Offset: 0x002FC474
		public int addText(char[] odata, int start, int end, LinkSet ls, TextState textState)
		{
			if (start == -1)
			{
				return -1;
			}
			bool flag = false;
			int num = start;
			int num2 = 0;
			int num3 = 0;
			int charWidth = this.getCharWidth(' ');
			char[] array = new char[odata.Length];
			char[] destinationArray = new char[odata.Length];
			Array.Copy(odata, array, odata.Length);
			Array.Copy(odata, destinationArray, odata.Length);
			for (int i = start; i < end; i++)
			{
				char c = array[i];
				int num4;
				bool flag2;
				bool flag3;
				if (!this.isSpace(c) && c != '\n' && c != '\r' && c != '\t' && c != '\u001f' && c != '\u2028')
				{
					num4 = this.getCharWidth(c);
					flag2 = true;
					flag3 = (c > '\u007f');
					if (num4 <= 0 && c != '​' && c != '﻿')
					{
						num4 = charWidth;
					}
				}
				else
				{
					if (c == '\n' || c == '\r' || c == '\t')
					{
						num4 = charWidth;
					}
					else if (c == '\u001f')
					{
						num4 = 0;
					}
					else
					{
						num4 = this.getCharWidth(c);
					}
					flag2 = false;
					flag3 = false;
					if (this.prev == 1)
					{
						bool flag4 = i > 0 && array[i - 1] == '\u001f';
						if ((this.PdfOptions.ForceTextWrap && flag4) || this.whiteSpaceCollapse == 27)
						{
							if (this.isSpace(c))
							{
								this.spaceWidth += this.getCharWidth(c);
							}
							else
							{
								if (c == '\n' || c == '\u2028')
								{
									if (this.spaceWidth > 0)
									{
										InlineSpace inlineSpace = new InlineSpace(this.spaceWidth);
										inlineSpace.setUnderlined(textState.getUnderlined());
										inlineSpace.setOverlined(textState.getOverlined());
										inlineSpace.setLineThrough(textState.getLineThrough());
										base.addChild(inlineSpace);
										this.finalWidth += this.spaceWidth;
										this.spaceWidth = 0;
									}
									return i + 1;
								}
								if (c == '\t')
								{
									this.spaceWidth += 8 * charWidth;
								}
							}
						}
						else if (c == '\u2028')
						{
							if (this.spaceWidth > 0)
							{
								InlineSpace inlineSpace2 = new InlineSpace(this.spaceWidth);
								inlineSpace2.setUnderlined(textState.getUnderlined());
								inlineSpace2.setOverlined(textState.getOverlined());
								inlineSpace2.setLineThrough(textState.getLineThrough());
								base.addChild(inlineSpace2);
								this.finalWidth += this.spaceWidth;
								this.spaceWidth = 0;
							}
							return i + 1;
						}
					}
					else if (this.prev == 2 || this.prev == 3)
					{
						if (this.spaceWidth > 0)
						{
							InlineSpace inlineSpace3 = new InlineSpace(this.spaceWidth);
							if (this.prevUlState)
							{
								inlineSpace3.setUnderlined(textState.getUnderlined());
							}
							if (this.prevOlState)
							{
								inlineSpace3.setOverlined(textState.getOverlined());
							}
							if (this.prevLTState)
							{
								inlineSpace3.setLineThrough(textState.getLineThrough());
							}
							base.addChild(inlineSpace3);
							this.finalWidth += this.spaceWidth;
							this.spaceWidth = 0;
						}
						foreach (object obj in this.pendingAreas)
						{
							Box box = (Box)obj;
							InlineArea inlineArea = box as InlineArea;
							if (inlineArea != null && ls != null)
							{
								Rectangle r = new Rectangle(this.finalWidth, 0, inlineArea.getContentWidth(), this.fontState.FontSize);
								ls.addRect(r, this, inlineArea);
							}
							base.addChild(box);
						}
						this.finalWidth += this.pendingWidth;
						this.pendingWidth = 0;
						this.pendingAreas = new ArrayList();
						if (num2 > 0)
						{
							this.addSpacedWord(new string(array, num, num2), ls, this.finalWidth, 0, textState, false);
							this.finalWidth += num3;
							num3 = 0;
						}
						this.prev = 1;
						this.embeddedLinkStart = 0;
						this.spaceWidth = this.getCharWidth(c);
						if (c == '\u001f')
						{
							this.spaceWidth = 0;
						}
						if (this.whiteSpaceCollapse == 27)
						{
							if (c == '\n' || c == '\u2028')
							{
								return i + 1;
							}
							if (c == '\t')
							{
								this.spaceWidth = charWidth;
							}
						}
						else if (c == '\u2028')
						{
							return i + 1;
						}
					}
					else if (this.whiteSpaceCollapse == 27)
					{
						if (this.isSpace(c))
						{
							this.prev = 1;
							this.spaceWidth = this.getCharWidth(c);
						}
						else
						{
							if (c == '\n')
							{
								InlineSpace child = new InlineSpace(this.spaceWidth);
								base.addChild(child);
								return i + 1;
							}
							if (c == '\t')
							{
								this.prev = 1;
								this.spaceWidth = 8 * charWidth;
							}
						}
					}
					else
					{
						num++;
					}
				}
				if (flag2)
				{
					int num5 = flag3 ? 3 : 2;
					if (this.prev == 1)
					{
						num3 = num4;
						if (this.finalWidth + this.spaceWidth + num3 > this.getContentWidth())
						{
							if (flag)
							{
								ApocDriver.ActiveDriver.FireApocWarning("Area contents overflows area");
							}
							if (this.wrapOption == 86)
							{
								return i;
							}
						}
						this.prev = num5;
						num = i;
						num2 = 1;
					}
					else if (this.prev == 2 || this.prev == 3)
					{
						if ((this.prev == 2 && num5 == 2) || !this.canBreakMidWord())
						{
							num2++;
							num3 += num4;
						}
						else
						{
							InlineSpace inlineSpace4 = new InlineSpace(this.spaceWidth);
							if (this.prevUlState)
							{
								inlineSpace4.setUnderlined(textState.getUnderlined());
							}
							if (this.prevOlState)
							{
								inlineSpace4.setOverlined(textState.getOverlined());
							}
							if (this.prevLTState)
							{
								inlineSpace4.setLineThrough(textState.getLineThrough());
							}
							base.addChild(inlineSpace4);
							this.finalWidth += this.spaceWidth;
							this.spaceWidth = 0;
							foreach (object obj2 in this.pendingAreas)
							{
								Box box2 = (Box)obj2;
								InlineArea inlineArea2 = box2 as InlineArea;
								if (inlineArea2 != null && ls != null)
								{
									Rectangle r2 = new Rectangle(this.finalWidth, 0, inlineArea2.getContentWidth(), this.fontState.FontSize);
									ls.addRect(r2, this, inlineArea2);
								}
								base.addChild(box2);
							}
							this.finalWidth += this.pendingWidth;
							this.pendingWidth = 0;
							this.pendingAreas = new ArrayList();
							if (num2 > 0)
							{
								this.addSpacedWord(new string(array, num, num2), ls, this.finalWidth, 0, textState, false);
								this.finalWidth += num3;
							}
							this.spaceWidth = 0;
							num = i;
							num2 = 1;
							num3 = num4;
						}
						this.prev = num5;
					}
					else
					{
						this.prev = num5;
						num = i;
						num2 = 1;
						num3 = num4;
					}
					if (this.finalWidth + this.spaceWidth + this.pendingWidth + num3 > this.getContentWidth() && this.wrapOption == 86)
					{
						if (num != start)
						{
							return num;
						}
						flag = true;
						if (this.finalWidth > 0)
						{
							return num;
						}
					}
				}
			}
			if (this.prev == 2 || this.prev == 3)
			{
				if (this.spaceWidth > 0)
				{
					InlineSpace inlineSpace5 = new InlineSpace(this.spaceWidth);
					inlineSpace5.setEatable(true);
					if (this.prevUlState)
					{
						inlineSpace5.setUnderlined(textState.getUnderlined());
					}
					if (this.prevOlState)
					{
						inlineSpace5.setOverlined(textState.getOverlined());
					}
					if (this.prevLTState)
					{
						inlineSpace5.setLineThrough(textState.getLineThrough());
					}
					this.pendingAreas.Add(inlineSpace5);
					this.pendingWidth += this.spaceWidth;
					this.spaceWidth = 0;
				}
				this.addSpacedWord(new string(array, num, num2), ls, this.finalWidth + this.pendingWidth, this.spaceWidth, textState, true);
				this.embeddedLinkStart += num3;
			}
			if (flag)
			{
				ApocDriver.ActiveDriver.FireApocWarning("Area contents overflows area");
			}
			return -1;
		}

		// Token: 0x0600DAE6 RID: 56038 RVA: 0x002FEA24 File Offset: 0x002FCC24
		public void AddLeader(int leaderPattern, int leaderLengthMinimum, int leaderLengthOptimum, int leaderLengthMaximum, int ruleStyle, int ruleThickness, int leaderPatternWidth, int leaderAlignment)
		{
			int num = 0;
			char c = '.';
			int width = this.currentFontState.GetWidth(this.currentFontState.MapCharacter(c));
			char c2 = ' ';
			this.currentFontState.GetWidth(this.currentFontState.MapCharacter(c2));
			int remainingWidth = this.getRemainingWidth();
			if (remainingWidth <= leaderLengthOptimum || remainingWidth <= leaderLengthMaximum)
			{
				num = remainingWidth;
			}
			else if (remainingWidth > leaderLengthOptimum && remainingWidth > leaderLengthMaximum)
			{
				num = leaderLengthMaximum;
			}
			else if (leaderLengthOptimum > leaderLengthMaximum && leaderLengthOptimum < remainingWidth)
			{
				num = leaderLengthOptimum;
			}
			if (num <= 0)
			{
				return;
			}
			if (leaderPattern <= 66)
			{
				if (leaderPattern != 19)
				{
					if (leaderPattern == 66)
					{
						LeaderArea leaderArea = new LeaderArea(this.fontState, this.red, this.green, this.blue, "", num, leaderPattern, ruleThickness, ruleStyle);
						leaderArea.setYOffset(this.placementOffset);
						this.pendingAreas.Add(leaderArea);
					}
				}
				else
				{
					if (leaderPatternWidth < width)
					{
						leaderPatternWidth = 0;
					}
					if (leaderPatternWidth == 0)
					{
						this.pendingAreas.Add(this.buildSimpleLeader(c, num));
					}
					else
					{
						if (leaderAlignment == 60)
						{
							int leaderAlignIndent = this.getLeaderAlignIndent(num, leaderPatternWidth);
							if (leaderAlignIndent != 0)
							{
								this.pendingAreas.Add(new InlineSpace(leaderAlignIndent, false));
								this.pendingWidth += leaderAlignIndent;
								num -= leaderAlignIndent;
							}
						}
						InlineSpace value = new InlineSpace(leaderPatternWidth - width, false);
						WordArea wordArea = new WordArea(this.currentFontState, this.red, this.green, this.blue, ".", width);
						wordArea.setYOffset(this.placementOffset);
						int num2 = (int)Math.Floor((double)num / (double)leaderPatternWidth);
						for (int i = 0; i < num2; i++)
						{
							this.pendingAreas.Add(wordArea);
							this.pendingAreas.Add(value);
						}
						this.pendingAreas.Add(new InlineSpace(num - num2 * leaderPatternWidth));
					}
				}
			}
			else if (leaderPattern != 71)
			{
				if (leaderPattern == 84)
				{
					ApocDriver.ActiveDriver.FireApocError("leader-pattern=\"use-content\" not supported by this version of Apoc");
					return;
				}
			}
			else
			{
				InlineSpace value2 = new InlineSpace(num);
				this.pendingAreas.Add(value2);
			}
			this.pendingWidth += num;
			this.prev = 2;
		}

		// Token: 0x0600DAE7 RID: 56039 RVA: 0x002FEC54 File Offset: 0x002FCE54
		public void addPending()
		{
			if (this.spaceWidth > 0)
			{
				base.addChild(new InlineSpace(this.spaceWidth));
				this.finalWidth += this.spaceWidth;
				this.spaceWidth = 0;
			}
			foreach (object obj in this.pendingAreas)
			{
				Box child = (Box)obj;
				base.addChild(child);
			}
			this.finalWidth += this.pendingWidth;
			this.pendingWidth = 0;
			this.pendingAreas = new ArrayList();
		}

		// Token: 0x0600DAE8 RID: 56040 RVA: 0x002FED08 File Offset: 0x002FCF08
		public void align(int type)
		{
			if (type <= 22)
			{
				int num;
				if (type == 13)
				{
					num = (this.getContentWidth() - this.finalWidth) / 2;
					this.startIndent += num;
					this.endIndent += num;
					return;
				}
				if (type != 22)
				{
					return;
				}
				num = this.getContentWidth() - this.finalWidth;
				this.startIndent += num;
				return;
			}
			else
			{
				int num;
				if (type == 37)
				{
					int num2 = 0;
					foreach (object obj in this.children)
					{
						Box box = (Box)obj;
						InlineSpace inlineSpace = box as InlineSpace;
						if (inlineSpace != null && inlineSpace.getResizeable())
						{
							num2++;
						}
					}
					if (num2 > 0)
					{
						num = (this.getContentWidth() - this.finalWidth) / num2;
					}
					else
					{
						num = 0;
					}
					num2 = 0;
					foreach (object obj2 in this.children)
					{
						Box box2 = (Box)obj2;
						InlineSpace inlineSpace2 = box2 as InlineSpace;
						InlineArea inlineArea = box2 as InlineArea;
						if (inlineSpace2 != null)
						{
							if (inlineSpace2.getResizeable())
							{
								inlineSpace2.setSize(inlineSpace2.getSize() + num);
								num2++;
							}
						}
						else if (inlineArea != null)
						{
							inlineArea.setXOffset(num2 * num);
						}
					}
					return;
				}
				if (type != 72)
				{
					return;
				}
				num = this.getContentWidth() - this.finalWidth;
				this.endIndent += num;
				return;
			}
		}

		// Token: 0x0600DAE9 RID: 56041 RVA: 0x002FEEAC File Offset: 0x002FD0AC
		public void verticalAlign()
		{
			int height = this.allocationHeight;
			foreach (object obj in this.children)
			{
				Box box = (Box)obj;
				InlineArea inlineArea = box as InlineArea;
				if (inlineArea != null)
				{
					if (inlineArea is WordArea)
					{
						inlineArea.setYOffset(this.placementOffset);
					}
					if (inlineArea.GetHeight() > height)
					{
						height = inlineArea.GetHeight();
					}
					int verticalAlign = inlineArea.getVerticalAlign();
					if (verticalAlign == 75)
					{
						int ascender = this.fontState.Ascender;
						inlineArea.setYOffset((int)((double)this.placementOffset - (double)(2 * ascender) / 3.0));
					}
					else if (verticalAlign == 74)
					{
						int ascender2 = this.fontState.Ascender;
						inlineArea.setYOffset((int)((double)this.placementOffset + (double)(2 * ascender2) / 3.0));
					}
				}
			}
			this.allocationHeight = height;
		}

		// Token: 0x0600DAEA RID: 56042 RVA: 0x002FEFB4 File Offset: 0x002FD1B4
		public void changeColor(float red, float green, float blue)
		{
			this.red = red;
			this.green = green;
			this.blue = blue;
		}

		// Token: 0x0600DAEB RID: 56043 RVA: 0x002FEFCB File Offset: 0x002FD1CB
		public void changeFont(FontState fontState)
		{
			this.currentFontState = fontState;
		}

		// Token: 0x0600DAEC RID: 56044 RVA: 0x002FEFD4 File Offset: 0x002FD1D4
		public void changeWhiteSpaceCollapse(int whiteSpaceCollapse)
		{
			this.whiteSpaceCollapse = whiteSpaceCollapse;
		}

		// Token: 0x0600DAED RID: 56045 RVA: 0x002FEFDD File Offset: 0x002FD1DD
		public void changeWrapOption(int wrapOption)
		{
			this.wrapOption = wrapOption;
		}

		// Token: 0x0600DAEE RID: 56046 RVA: 0x002FEFE6 File Offset: 0x002FD1E6
		public void changeVerticalAlign(int vAlign)
		{
			this.vAlign = vAlign;
		}

		// Token: 0x0600DAEF RID: 56047 RVA: 0x002FEFEF File Offset: 0x002FD1EF
		public int getEndIndent()
		{
			return this.endIndent;
		}

		// Token: 0x0600DAF0 RID: 56048 RVA: 0x002FEFF7 File Offset: 0x002FD1F7
		public override int GetHeight()
		{
			return this.allocationHeight;
		}

		// Token: 0x0600DAF1 RID: 56049 RVA: 0x002FEFFF File Offset: 0x002FD1FF
		public int getPlacementOffset()
		{
			return this.placementOffset;
		}

		// Token: 0x0600DAF2 RID: 56050 RVA: 0x002FF007 File Offset: 0x002FD207
		public int getStartIndent()
		{
			return this.startIndent;
		}

		// Token: 0x0600DAF3 RID: 56051 RVA: 0x002FF00F File Offset: 0x002FD20F
		public bool isEmpty()
		{
			return this.pendingAreas.Count <= 0 && this.children.Count <= 0;
		}

		// Token: 0x0600DAF4 RID: 56052 RVA: 0x002FF032 File Offset: 0x002FD232
		public ArrayList getPendingAreas()
		{
			return this.pendingAreas;
		}

		// Token: 0x0600DAF5 RID: 56053 RVA: 0x002FF03A File Offset: 0x002FD23A
		public int getPendingWidth()
		{
			return this.pendingWidth;
		}

		// Token: 0x0600DAF6 RID: 56054 RVA: 0x002FF042 File Offset: 0x002FD242
		public void setPendingAreas(ArrayList areas)
		{
			this.pendingAreas = areas;
		}

		// Token: 0x0600DAF7 RID: 56055 RVA: 0x002FF04B File Offset: 0x002FD24B
		public void setPendingWidth(int width)
		{
			this.pendingWidth = width;
		}

		// Token: 0x0600DAF8 RID: 56056 RVA: 0x002FF054 File Offset: 0x002FD254
		public void changeHyphenation(HyphenationProps hyphProps)
		{
			this.hyphProps = hyphProps;
		}

		// Token: 0x0600DAF9 RID: 56057 RVA: 0x002FF060 File Offset: 0x002FD260
		private InlineArea buildSimpleLeader(char c, int leaderLength)
		{
			int num = this.currentFontState.GetWidth(this.currentFontState.MapCharacter(c));
			if (num == 0)
			{
				ApocDriver.ActiveDriver.FireApocError("char '" + c + "' has width 0. Using width 100 instead.");
				num = 100;
			}
			int num2 = (int)Math.Floor((double)(leaderLength / num));
			char[] array = new char[num2];
			for (int i = 0; i < num2; i++)
			{
				array[i] = c;
			}
			WordArea wordArea = new WordArea(this.currentFontState, this.red, this.green, this.blue, new string(array), leaderLength);
			wordArea.setYOffset(this.placementOffset);
			return wordArea;
		}

		// Token: 0x0600DAFA RID: 56058 RVA: 0x002FF104 File Offset: 0x002FD304
		private int getLeaderAlignIndent(int leaderLength, int leaderPatternWidth)
		{
			double num = (double)this.getCurrentXPosition();
			double num2 = Math.Ceiling(num / (double)leaderPatternWidth);
			double num3 = (double)leaderPatternWidth * num2 - num;
			return (int)num3;
		}

		// Token: 0x0600DAFB RID: 56059 RVA: 0x002FF12C File Offset: 0x002FD32C
		private int getCurrentXPosition()
		{
			return this.finalWidth + this.spaceWidth + this.startIndent + this.pendingWidth;
		}

		// Token: 0x0600DAFC RID: 56060 RVA: 0x002FF14C File Offset: 0x002FD34C
		private string getHyphenationWord(char[] characters, int wordStart)
		{
			bool flag = false;
			int num = 0;
			char[] array = new char[characters.Length];
			while (!flag && wordStart + num < characters.Length)
			{
				char c = characters[wordStart + num];
				if (char.IsLetter(c))
				{
					array[num] = c;
					num++;
				}
				else
				{
					flag = true;
				}
			}
			return new string(array, 0, num);
		}

		// Token: 0x0600DAFD RID: 56061 RVA: 0x002FF198 File Offset: 0x002FD398
		private int getWordWidth(string word)
		{
			if (word == null)
			{
				return 0;
			}
			int num = 0;
			foreach (char c in word)
			{
				num += this.getCharWidth(c);
			}
			return num;
		}

		// Token: 0x0600DAFE RID: 56062 RVA: 0x002FF1D2 File Offset: 0x002FD3D2
		public int getRemainingWidth()
		{
			return this.getContentWidth() + this.startIndent - this.getCurrentXPosition();
		}

		// Token: 0x0600DAFF RID: 56063 RVA: 0x002FF1E8 File Offset: 0x002FD3E8
		public void setLinkSet(LinkSet ls)
		{
		}

		// Token: 0x0600DB00 RID: 56064 RVA: 0x002FF1EC File Offset: 0x002FD3EC
		public void addInlineArea(InlineArea box, LinkSet ls)
		{
			this.addPending();
			base.addChild(box);
			if (ls != null)
			{
				Rectangle r = new Rectangle(this.finalWidth, 0, box.getContentWidth(), box.getContentHeight());
				ls.addRect(r, this, box);
			}
			this.prev = 2;
			this.finalWidth += box.getContentWidth();
		}

		// Token: 0x0600DB01 RID: 56065 RVA: 0x002FF246 File Offset: 0x002FD446
		public void addInlineSpace(InlineSpace isp, int spaceWidth)
		{
			base.addChild(isp);
			this.finalWidth += spaceWidth;
		}

		// Token: 0x0600DB02 RID: 56066 RVA: 0x002FF25D File Offset: 0x002FD45D
		private bool isWhiteSpace(char c)
		{
			return char.IsWhiteSpace(c) || c == '\u001f';
		}

		// Token: 0x0600DB03 RID: 56067 RVA: 0x002FF270 File Offset: 0x002FD470
		public int addCharacter(char data, LinkSet ls, bool ul)
		{
			int remainingWidth = this.getRemainingWidth();
			int width = this.currentFontState.GetWidth(this.currentFontState.MapCharacter(data));
			if (width > remainingWidth)
			{
				return 1;
			}
			if (this.isWhiteSpace(data) && this.whiteSpaceCollapse == 81)
			{
				return 0;
			}
			WordArea wordArea = new WordArea(this.currentFontState, this.red, this.green, this.blue, data.ToString(), width);
			wordArea.setYOffset(this.placementOffset);
			wordArea.setUnderlined(ul);
			this.pendingAreas.Add(wordArea);
			if (this.isWhiteSpace(data))
			{
				this.spaceWidth = width;
				this.prev = 1;
			}
			else
			{
				this.pendingWidth += width;
				this.prev = 2;
			}
			return 0;
		}

		// Token: 0x0600DB04 RID: 56068 RVA: 0x002FF330 File Offset: 0x002FD530
		private void addMapWord(char startChar, StringBuilder wordBuf)
		{
			StringBuilder stringBuilder = new StringBuilder(wordBuf.Length);
			for (int i = 0; i < wordBuf.Length; i++)
			{
				stringBuilder.Append(this.currentFontState.MapCharacter(wordBuf[i]));
			}
			this.addWord(startChar, stringBuilder);
		}

		// Token: 0x0600DB05 RID: 56069 RVA: 0x002FF37C File Offset: 0x002FD57C
		private void addWord(char startChar, StringBuilder wordBuf)
		{
			string text = (wordBuf != null) ? wordBuf.ToString() : "";
			int charWidth = this.getCharWidth(startChar);
			WordArea wordArea;
			if (this.isAnySpace(startChar))
			{
				base.addChild(new InlineSpace(charWidth));
			}
			else
			{
				wordArea = new WordArea(this.currentFontState, this.red, this.green, this.blue, startChar.ToString(), 1);
				wordArea.setYOffset(this.placementOffset);
				base.addChild(wordArea);
			}
			int wordWidth = this.getWordWidth(text);
			wordArea = new WordArea(this.currentFontState, this.red, this.green, this.blue, text, text.Length);
			wordArea.setYOffset(this.placementOffset);
			base.addChild(wordArea);
			this.finalWidth += charWidth + wordWidth;
		}

		// Token: 0x0600DB06 RID: 56070 RVA: 0x002FF444 File Offset: 0x002FD644
		private bool canBreakMidWord()
		{
			bool result = false;
			if (this.hyphProps != null && this.hyphProps.language != null && !this.hyphProps.language.Equals("NONE"))
			{
				string value = this.hyphProps.language.ToLower();
				if ("zh".Equals(value) || "ja".Equals(value) || "ko".Equals(value) || "vi".Equals(value))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600DB07 RID: 56071 RVA: 0x002FF4C8 File Offset: 0x002FD6C8
		private int getCharWidth(char c)
		{
			int num;
			if (c == '\n' || c == '\r' || c == '\t' || c == '\u00a0')
			{
				num = this.getCharWidth(' ');
			}
			else
			{
				num = this.currentFontState.GetWidth(this.currentFontState.MapCharacter(c));
				if (num <= 0)
				{
					int num2 = this.currentFontState.GetWidth(this.currentFontState.MapCharacter('m'));
					int num3 = this.currentFontState.GetWidth(this.currentFontState.MapCharacter('n'));
					if (num2 <= 0)
					{
						num2 = 500 * this.currentFontState.FontSize;
					}
					if (num3 <= 0)
					{
						num3 = num2 - 10;
					}
					if (c == ' ')
					{
						num = num2;
					}
					if (c == '\u2000')
					{
						num = num3;
					}
					if (c == '\u2001')
					{
						num = num2;
					}
					if (c == '\u2002')
					{
						num = num2 / 2;
					}
					if (c == '\u2003')
					{
						num = this.currentFontState.FontSize;
					}
					if (c == '\u2004')
					{
						num = num2 / 3;
					}
					if (c == '\u2005')
					{
						num = num2 / 4;
					}
					if (c == '\u2006')
					{
						num = num2 / 6;
					}
					if (c == '\u2007')
					{
						num = this.getCharWidth(' ');
					}
					if (c == '\u2008')
					{
						num = this.getCharWidth('.');
					}
					if (c == '\u2009')
					{
						num = num2 / 5;
					}
					if (c == '\u200a')
					{
						num = 5;
					}
					if (c == '​')
					{
						num = 100;
					}
					if (c == '\u202f')
					{
						num = this.getCharWidth(' ') / 2;
					}
					if (c == '\u3000')
					{
						num = this.getCharWidth(' ') * 2;
					}
				}
			}
			return num;
		}

		// Token: 0x0600DB08 RID: 56072 RVA: 0x002FF634 File Offset: 0x002FD834
		private bool isSpace(char c)
		{
			return c == ' ' || c == '\u2000' || c == '\u2001' || c == '\u2002' || c == '\u2003' || c == '\u2004' || c == '\u2005' || c == '\u2006' || c == '\u2007' || c == '\u2008' || c == '\u2009' || c == '\u200a' || c == '​';
		}

		// Token: 0x0600DB09 RID: 56073 RVA: 0x002FF6A9 File Offset: 0x002FD8A9
		private bool isNBSP(char c)
		{
			return c == '\u00a0' || c == '\u202f' || c == '\u3000' || c == '﻿';
		}

		// Token: 0x0600DB0A RID: 56074 RVA: 0x002FF6D0 File Offset: 0x002FD8D0
		private bool isAnySpace(char c)
		{
			return this.isSpace(c) || this.isNBSP(c);
		}

		// Token: 0x0600DB0B RID: 56075 RVA: 0x002FF6F4 File Offset: 0x002FD8F4
		private void addSpacedWord(string word, LinkSet ls, int startw, int spacew, TextState textState, bool addToPending)
		{
			GridStringTokenizer gridStringTokenizer = new GridStringTokenizer(word, "\u00a0\u202f\u3000﻿", true);
			IEnumerator enumerator = gridStringTokenizer.GetEnumerator();
			int num = 0;
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				string text = (string)obj;
				if (text.Length == 1 && this.isNBSP(text[0]))
				{
					int charWidth = this.getCharWidth(text[0]);
					if (charWidth > 0)
					{
						InlineSpace inlineSpace = new InlineSpace(charWidth);
						num += charWidth;
						if (this.prevUlState)
						{
							inlineSpace.setUnderlined(textState.getUnderlined());
						}
						if (this.prevOlState)
						{
							inlineSpace.setOverlined(textState.getOverlined());
						}
						if (this.prevLTState)
						{
							inlineSpace.setLineThrough(textState.getLineThrough());
						}
						if (addToPending)
						{
							this.pendingAreas.Add(inlineSpace);
							this.pendingWidth += charWidth;
						}
						else
						{
							base.addChild(inlineSpace);
						}
					}
				}
				else
				{
					WordArea wordArea = new WordArea(this.currentFontState, this.red, this.green, this.blue, text, this.getWordWidth(text));
					wordArea.setYOffset(this.placementOffset);
					wordArea.setUnderlined(textState.getUnderlined());
					this.prevUlState = textState.getUnderlined();
					wordArea.setOverlined(textState.getOverlined());
					this.prevOlState = textState.getOverlined();
					wordArea.setLineThrough(textState.getLineThrough());
					this.prevLTState = textState.getLineThrough();
					wordArea.setVerticalAlign(this.vAlign);
					if (addToPending)
					{
						this.pendingAreas.Add(wordArea);
						this.pendingWidth += this.getWordWidth(text);
					}
					else
					{
						base.addChild(wordArea);
					}
					if (ls != null)
					{
						Rectangle r = new Rectangle(startw + num, spacew, wordArea.getContentWidth(), this.fontState.FontSize);
						ls.addRect(r, this, wordArea);
					}
				}
			}
		}

		// Token: 0x04003CFC RID: 15612
		protected const int NOTHING = 0;

		// Token: 0x04003CFD RID: 15613
		protected const int WHITESPACE = 1;

		// Token: 0x04003CFE RID: 15614
		protected const int TEXT = 2;

		// Token: 0x04003CFF RID: 15615
		protected const int MULTIBYTECHAR = 3;

		// Token: 0x04003D00 RID: 15616
		protected int lineHeight;

		// Token: 0x04003D01 RID: 15617
		protected int halfLeading;

		// Token: 0x04003D02 RID: 15618
		protected int nominalFontSize;

		// Token: 0x04003D03 RID: 15619
		protected int nominalGlyphHeight;

		// Token: 0x04003D04 RID: 15620
		protected int allocationHeight;

		// Token: 0x04003D05 RID: 15621
		protected int startIndent;

		// Token: 0x04003D06 RID: 15622
		protected int endIndent;

		// Token: 0x04003D07 RID: 15623
		private int placementOffset;

		// Token: 0x04003D08 RID: 15624
		private FontState currentFontState;

		// Token: 0x04003D09 RID: 15625
		private float red;

		// Token: 0x04003D0A RID: 15626
		private float green;

		// Token: 0x04003D0B RID: 15627
		private float blue;

		// Token: 0x04003D0C RID: 15628
		private int wrapOption;

		// Token: 0x04003D0D RID: 15629
		private int whiteSpaceCollapse;

		// Token: 0x04003D0E RID: 15630
		private int vAlign;

		// Token: 0x04003D0F RID: 15631
		private HyphenationProps hyphProps;

		// Token: 0x04003D10 RID: 15632
		protected int finalWidth;

		// Token: 0x04003D11 RID: 15633
		private PdfRendererOptions options;

		// Token: 0x04003D12 RID: 15634
		protected int embeddedLinkStart;

		// Token: 0x04003D13 RID: 15635
		protected int prev;

		// Token: 0x04003D14 RID: 15636
		protected int spaceWidth;

		// Token: 0x04003D15 RID: 15637
		protected ArrayList pendingAreas = new ArrayList();

		// Token: 0x04003D16 RID: 15638
		protected int pendingWidth;

		// Token: 0x04003D17 RID: 15639
		protected bool prevUlState;

		// Token: 0x04003D18 RID: 15640
		protected bool prevOlState;

		// Token: 0x04003D19 RID: 15641
		protected bool prevLTState;
	}
}
