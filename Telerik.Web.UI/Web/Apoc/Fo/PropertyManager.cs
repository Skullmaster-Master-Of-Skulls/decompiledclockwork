using System;
using Telerik.Web.Apoc.Image;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Fo
{
	// Token: 0x020015BF RID: 5567
	internal class PropertyManager
	{
		// Token: 0x0600D930 RID: 55600 RVA: 0x002FA672 File Offset: 0x002F8872
		public PropertyManager(PropertyList pList)
		{
			this.properties = pList;
		}

		// Token: 0x0600D931 RID: 55601 RVA: 0x002FA684 File Offset: 0x002F8884
		private void InitDirections()
		{
			this.saTop = this.properties.wmAbsToRel(2);
			this.saBottom = this.properties.wmAbsToRel(3);
			this.saLeft = this.properties.wmAbsToRel(0);
			this.saRight = this.properties.wmAbsToRel(1);
		}

		// Token: 0x0600D932 RID: 55602 RVA: 0x002FA6DC File Offset: 0x002F88DC
		public FontState GetFontState(FontInfo fontInfo)
		{
			if (this.fontState == null)
			{
				string @string = this.properties.GetProperty("font-family").GetString();
				string string2 = this.properties.GetProperty("font-style").GetString();
				string string3 = this.properties.GetProperty("font-weight").GetString();
				int fontSize = this.properties.GetProperty("font-size").GetLength().MValue();
				int @enum = this.properties.GetProperty("font-variant").GetEnum();
				this.fontState = new FontState(fontInfo, @string, string2, string3, fontSize, @enum);
			}
			return this.fontState;
		}

		// Token: 0x0600D933 RID: 55603 RVA: 0x002FA780 File Offset: 0x002F8980
		public BorderAndPadding GetBorderAndPadding()
		{
			if (this.borderAndPadding == null)
			{
				this.borderAndPadding = new BorderAndPadding();
				this.InitDirections();
				this.InitBorderInfo(0, this.saTop);
				this.InitBorderInfo(2, this.saBottom);
				this.InitBorderInfo(3, this.saLeft);
				this.InitBorderInfo(1, this.saRight);
			}
			return this.borderAndPadding;
		}

		// Token: 0x0600D934 RID: 55604 RVA: 0x002FA7E0 File Offset: 0x002F89E0
		private void InitBorderInfo(int whichSide, string saSide)
		{
			this.borderAndPadding.setPadding(whichSide, this.properties.GetProperty(string.Format(PropertyManager.msgPaddingFmt, saSide)).GetCondLength());
			int @enum = this.properties.GetProperty(string.Format(PropertyManager.msgStyleFmt, saSide)).GetEnum();
			if (@enum != 51)
			{
				this.borderAndPadding.setBorder(whichSide, @enum, this.properties.GetProperty(string.Format(PropertyManager.msgWidthFmt, saSide)).GetCondLength(), this.properties.GetProperty(string.Format(PropertyManager.msgColorFmt, saSide)).GetColorType());
			}
		}

		// Token: 0x0600D935 RID: 55605 RVA: 0x002FA878 File Offset: 0x002F8A78
		public HyphenationProps GetHyphenationProps()
		{
			if (this.hyphProps == null)
			{
				this.hyphProps = new HyphenationProps();
				this.hyphProps.hyphenate = this.properties.GetProperty("hyphenate").GetEnum();
				this.hyphProps.hyphenationChar = this.properties.GetProperty("hyphenation-character").GetCharacter();
				this.hyphProps.hyphenationPushCharacterCount = this.properties.GetProperty("hyphenation-push-character-count").GetNumber().IntValue();
				this.hyphProps.hyphenationRemainCharacterCount = this.properties.GetProperty("hyphenation-remain-character-count").GetNumber().IntValue();
				this.hyphProps.language = this.properties.GetProperty("language").GetString();
				this.hyphProps.country = this.properties.GetProperty("country").GetString();
			}
			return this.hyphProps;
		}

		// Token: 0x0600D936 RID: 55606 RVA: 0x002FA96C File Offset: 0x002F8B6C
		public int CheckBreakBefore(Area area)
		{
			ColumnArea columnArea = area as ColumnArea;
			if (columnArea == null)
			{
				int @enum = this.properties.GetProperty("break-before").GetEnum();
				if (@enum <= 26)
				{
					if (@enum == 15)
					{
						return 7;
					}
					if (@enum == 26)
					{
						return 5;
					}
				}
				else
				{
					if (@enum == 55)
					{
						return 6;
					}
					if (@enum == 58)
					{
						return 4;
					}
				}
				return 1;
			}
			int enum2 = this.properties.GetProperty("break-before").GetEnum();
			if (enum2 <= 26)
			{
				if (enum2 != 15)
				{
					if (enum2 == 26)
					{
						if (!columnArea.hasChildren() && columnArea.getColumnIndex() == 1 && columnArea.getPage().getNumber() % 2 == 0)
						{
							return 1;
						}
						return 5;
					}
				}
				else
				{
					if (!area.hasChildren())
					{
						return 1;
					}
					return 7;
				}
			}
			else if (enum2 != 55)
			{
				if (enum2 == 58)
				{
					if (!columnArea.hasChildren() && columnArea.getColumnIndex() == 1)
					{
						return 1;
					}
					return 4;
				}
			}
			else
			{
				if (!columnArea.hasChildren() && columnArea.getColumnIndex() == 1 && columnArea.getPage().getNumber() % 2 != 0)
				{
					return 1;
				}
				return 6;
			}
			return 1;
		}

		// Token: 0x0600D937 RID: 55607 RVA: 0x002FAA5C File Offset: 0x002F8C5C
		public int CheckBreakAfter(Area area)
		{
			int @enum = this.properties.GetProperty("break-after").GetEnum();
			if (@enum <= 26)
			{
				if (@enum == 15)
				{
					return 7;
				}
				if (@enum == 26)
				{
					return 5;
				}
			}
			else
			{
				if (@enum == 55)
				{
					return 6;
				}
				if (@enum == 58)
				{
					return 4;
				}
			}
			return 1;
		}

		// Token: 0x0600D938 RID: 55608 RVA: 0x002FAAA4 File Offset: 0x002F8CA4
		public MarginProps GetMarginProps()
		{
			return new MarginProps
			{
				marginTop = this.properties.GetProperty("margin-top").GetLength().MValue(),
				marginBottom = this.properties.GetProperty("margin-bottom").GetLength().MValue(),
				marginLeft = this.properties.GetProperty("margin-left").GetLength().MValue(),
				marginRight = this.properties.GetProperty("margin-right").GetLength().MValue()
			};
		}

		// Token: 0x0600D939 RID: 55609 RVA: 0x002FAB38 File Offset: 0x002F8D38
		public BackgroundProps GetBackgroundProps()
		{
			if (this.bgProps == null)
			{
				this.bgProps = new BackgroundProps();
				this.bgProps.backColor = this.properties.GetProperty("background-color").GetColorType();
				string @string = this.properties.GetProperty("background-image").GetString();
				if (@string == "none")
				{
					this.bgProps.backImage = null;
				}
				else if (@string == "inherit")
				{
					this.bgProps.backImage = null;
				}
				else
				{
					try
					{
						this.bgProps.backImage = ApocImageFactory.Make(@string);
					}
					catch (ApocImageException ex)
					{
						this.bgProps.backImage = null;
						ApocDriver.ActiveDriver.FireApocError(ex.Message);
					}
				}
				this.bgProps.backRepeat = this.properties.GetProperty("background-repeat").GetEnum();
			}
			return this.bgProps;
		}

		// Token: 0x0600D93A RID: 55610 RVA: 0x002FAC30 File Offset: 0x002F8E30
		public MarginInlineProps GetMarginInlineProps()
		{
			return new MarginInlineProps();
		}

		// Token: 0x0600D93B RID: 55611 RVA: 0x002FAC44 File Offset: 0x002F8E44
		public AccessibilityProps GetAccessibilityProps()
		{
			AccessibilityProps accessibilityProps = new AccessibilityProps();
			string @string = this.properties.GetProperty("source-document").GetString();
			if (!"none".Equals(@string))
			{
				accessibilityProps.sourceDoc = @string;
			}
			@string = this.properties.GetProperty("role").GetString();
			if (!"none".Equals(@string))
			{
				accessibilityProps.role = @string;
			}
			return accessibilityProps;
		}

		// Token: 0x0600D93C RID: 55612 RVA: 0x002FACAC File Offset: 0x002F8EAC
		public AuralProps GetAuralProps()
		{
			return new AuralProps();
		}

		// Token: 0x0600D93D RID: 55613 RVA: 0x002FACC0 File Offset: 0x002F8EC0
		public RelativePositionProps GetRelativePositionProps()
		{
			return new RelativePositionProps();
		}

		// Token: 0x0600D93E RID: 55614 RVA: 0x002FACD4 File Offset: 0x002F8ED4
		public AbsolutePositionProps GetAbsolutePositionProps()
		{
			return new AbsolutePositionProps();
		}

		// Token: 0x0600D93F RID: 55615 RVA: 0x002FACE8 File Offset: 0x002F8EE8
		public TextState getTextDecoration(FObj parent)
		{
			TextState textState = null;
			bool flag = false;
			do
			{
				string name = parent.GetName();
				if (name.Equals("fo:flow") || name.Equals("fo:static-content"))
				{
					flag = true;
				}
				else if (name.Equals("fo:block") || name.Equals("fo:inline"))
				{
					FObjMixed fobjMixed = (FObjMixed)parent;
					textState = fobjMixed.getTextState();
					flag = true;
				}
				parent = parent.getParent();
			}
			while (!flag && parent != null);
			TextState textState2 = new TextState();
			if (textState != null)
			{
				textState2.setUnderlined(textState.getUnderlined());
				textState2.setOverlined(textState.getOverlined());
				textState2.setLineThrough(textState.getLineThrough());
			}
			int @enum = this.properties.GetProperty("text-decoration").GetEnum();
			if (@enum == 82)
			{
				textState2.setUnderlined(true);
			}
			if (@enum == 57)
			{
				textState2.setOverlined(true);
			}
			if (@enum == 40)
			{
				textState2.setLineThrough(true);
			}
			if (@enum == 48)
			{
				textState2.setUnderlined(false);
			}
			if (@enum == 47)
			{
				textState2.setOverlined(false);
			}
			if (@enum == 46)
			{
				textState2.setLineThrough(false);
			}
			return textState2;
		}

		// Token: 0x04003BFE RID: 15358
		private PropertyList properties;

		// Token: 0x04003BFF RID: 15359
		private FontState fontState;

		// Token: 0x04003C00 RID: 15360
		private BorderAndPadding borderAndPadding;

		// Token: 0x04003C01 RID: 15361
		private HyphenationProps hyphProps;

		// Token: 0x04003C02 RID: 15362
		private BackgroundProps bgProps;

		// Token: 0x04003C03 RID: 15363
		private string saLeft;

		// Token: 0x04003C04 RID: 15364
		private string saRight;

		// Token: 0x04003C05 RID: 15365
		private string saTop;

		// Token: 0x04003C06 RID: 15366
		private string saBottom;

		// Token: 0x04003C07 RID: 15367
		private static string msgColorFmt = "border-{0}-color";

		// Token: 0x04003C08 RID: 15368
		private static string msgStyleFmt = "border-{0}-style";

		// Token: 0x04003C09 RID: 15369
		private static string msgWidthFmt = "border-{0}-width";

		// Token: 0x04003C0A RID: 15370
		private static string msgPaddingFmt = "padding-{0}";
	}
}
