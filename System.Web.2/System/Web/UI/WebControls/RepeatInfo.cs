using System;
using System.Globalization;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004B9 RID: 1209
	public sealed class RepeatInfo
	{
		// Token: 0x06003C63 RID: 15459 RVA: 0x000C39DC File Offset: 0x000C1BDC
		public RepeatInfo()
		{
			this.repeatDirection = RepeatDirection.Vertical;
			this.repeatLayout = RepeatLayout.Table;
			this.repeatColumns = 0;
			this.outerTableImplied = false;
		}

		// Token: 0x170011A6 RID: 4518
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x000C3A00 File Offset: 0x000C1C00
		// (set) Token: 0x06003C65 RID: 15461 RVA: 0x000C3A16 File Offset: 0x000C1C16
		public string Caption
		{
			get
			{
				if (this.caption != null)
				{
					return this.caption;
				}
				return string.Empty;
			}
			set
			{
				this.caption = value;
			}
		}

		// Token: 0x170011A7 RID: 4519
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x000C3A1F File Offset: 0x000C1C1F
		// (set) Token: 0x06003C67 RID: 15463 RVA: 0x000C3A27 File Offset: 0x000C1C27
		public TableCaptionAlign CaptionAlign
		{
			get
			{
				return this.captionAlign;
			}
			set
			{
				if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.captionAlign = value;
			}
		}

		// Token: 0x170011A8 RID: 4520
		// (get) Token: 0x06003C68 RID: 15464 RVA: 0x000C3A43 File Offset: 0x000C1C43
		// (set) Token: 0x06003C69 RID: 15465 RVA: 0x000C3A4B File Offset: 0x000C1C4B
		internal bool EnableLegacyRendering
		{
			get
			{
				return this.enableLegacyRendering;
			}
			set
			{
				this.enableLegacyRendering = value;
			}
		}

		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x000C3A54 File Offset: 0x000C1C54
		private bool IsListLayout
		{
			get
			{
				return this.RepeatLayout == RepeatLayout.UnorderedList || this.RepeatLayout == RepeatLayout.OrderedList;
			}
		}

		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06003C6B RID: 15467 RVA: 0x000C3A6A File Offset: 0x000C1C6A
		// (set) Token: 0x06003C6C RID: 15468 RVA: 0x000C3A72 File Offset: 0x000C1C72
		public bool OuterTableImplied
		{
			get
			{
				return this.outerTableImplied;
			}
			set
			{
				this.outerTableImplied = value;
			}
		}

		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x000C3A7B File Offset: 0x000C1C7B
		// (set) Token: 0x06003C6E RID: 15470 RVA: 0x000C3A83 File Offset: 0x000C1C83
		public int RepeatColumns
		{
			get
			{
				return this.repeatColumns;
			}
			set
			{
				this.repeatColumns = value;
			}
		}

		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x06003C6F RID: 15471 RVA: 0x000C3A8C File Offset: 0x000C1C8C
		// (set) Token: 0x06003C70 RID: 15472 RVA: 0x000C3A94 File Offset: 0x000C1C94
		public RepeatDirection RepeatDirection
		{
			get
			{
				return this.repeatDirection;
			}
			set
			{
				if (value < RepeatDirection.Horizontal || value > RepeatDirection.Vertical)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.repeatDirection = value;
			}
		}

		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06003C71 RID: 15473 RVA: 0x000C3AB0 File Offset: 0x000C1CB0
		// (set) Token: 0x06003C72 RID: 15474 RVA: 0x000C3AB8 File Offset: 0x000C1CB8
		public RepeatLayout RepeatLayout
		{
			get
			{
				return this.repeatLayout;
			}
			set
			{
				EnumerationRangeValidationUtil.ValidateRepeatLayout(value);
				this.repeatLayout = value;
			}
		}

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x000C3AC7 File Offset: 0x000C1CC7
		// (set) Token: 0x06003C74 RID: 15476 RVA: 0x000C3ACF File Offset: 0x000C1CCF
		public bool UseAccessibleHeader
		{
			get
			{
				return this.useAccessibleHeader;
			}
			set
			{
				this.useAccessibleHeader = value;
			}
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x000C3AD8 File Offset: 0x000C1CD8
		private void RenderHorizontalRepeater(HtmlTextWriter writer, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
		{
			int repeatedItemCount = user.RepeatedItemCount;
			int num = this.repeatColumns;
			int num2 = 0;
			if (num == 0)
			{
				num = repeatedItemCount;
			}
			WebControl webControl = null;
			bool flag = false;
			RepeatLayout repeatLayout = this.repeatLayout;
			if (repeatLayout != RepeatLayout.Table)
			{
				if (repeatLayout == RepeatLayout.Flow)
				{
					webControl = new WebControl(HtmlTextWriterTag.Span);
				}
			}
			else
			{
				webControl = new Table();
				if (this.Caption.Length != 0)
				{
					((Table)webControl).Caption = this.Caption;
					((Table)webControl).CaptionAlign = this.CaptionAlign;
				}
				flag = true;
			}
			bool hasSeparators = user.HasSeparators;
			webControl.ID = baseControl.ClientID;
			webControl.CopyBaseAttributes(baseControl);
			webControl.ApplyStyle(controlStyle);
			webControl.RenderBeginTag(writer);
			if (user.HasHeader)
			{
				if (flag)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
					if (num != 1 || hasSeparators)
					{
						int num3 = num;
						if (hasSeparators)
						{
							num3 += num;
						}
						writer.AddAttribute(HtmlTextWriterAttribute.Colspan, num3.ToString(NumberFormatInfo.InvariantInfo));
					}
					if (this.useAccessibleHeader)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
					}
					Style itemStyle = user.GetItemStyle(ListItemType.Header, -1);
					if (itemStyle != null)
					{
						itemStyle.AddAttributesToRender(writer);
					}
					if (this.useAccessibleHeader)
					{
						writer.RenderBeginTag(HtmlTextWriterTag.Th);
					}
					else
					{
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
					}
				}
				user.RenderItem(ListItemType.Header, -1, this, writer);
				if (flag)
				{
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				else if (num < repeatedItemCount)
				{
					if (this.EnableLegacyRendering)
					{
						writer.WriteObsoleteBreak();
					}
					else
					{
						writer.WriteBreak();
					}
				}
			}
			for (int i = 0; i < repeatedItemCount; i++)
			{
				if (flag && num2 == 0)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				if (flag)
				{
					Style itemStyle2 = user.GetItemStyle(ListItemType.Item, i);
					if (itemStyle2 != null)
					{
						itemStyle2.AddAttributesToRender(writer);
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
				}
				user.RenderItem(ListItemType.Item, i, this, writer);
				if (flag)
				{
					writer.RenderEndTag();
				}
				if (hasSeparators && i != repeatedItemCount - 1)
				{
					if (flag)
					{
						Style itemStyle3 = user.GetItemStyle(ListItemType.Separator, i);
						if (itemStyle3 != null)
						{
							itemStyle3.AddAttributesToRender(writer);
						}
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
					}
					user.RenderItem(ListItemType.Separator, i, this, writer);
					if (flag)
					{
						writer.RenderEndTag();
					}
				}
				num2++;
				if (flag && i == repeatedItemCount - 1)
				{
					int num4 = num - num2;
					if (hasSeparators)
					{
						int num5 = num4 * 2 + 1;
						if (num5 > num4)
						{
							num4 = num5;
						}
					}
					for (int j = 0; j < num4; j++)
					{
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
						writer.RenderEndTag();
					}
				}
				if (num2 == num || i == repeatedItemCount - 1)
				{
					if (flag)
					{
						writer.RenderEndTag();
					}
					else if (num < repeatedItemCount)
					{
						if (this.EnableLegacyRendering)
						{
							writer.WriteObsoleteBreak();
						}
						else
						{
							writer.WriteBreak();
						}
					}
					num2 = 0;
				}
			}
			if (user.HasFooter)
			{
				if (flag)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
					if (num != 1 || hasSeparators)
					{
						int num6 = num;
						if (hasSeparators)
						{
							num6 += num;
						}
						writer.AddAttribute(HtmlTextWriterAttribute.Colspan, num6.ToString(NumberFormatInfo.InvariantInfo));
					}
					Style itemStyle4 = user.GetItemStyle(ListItemType.Footer, -1);
					if (itemStyle4 != null)
					{
						itemStyle4.AddAttributesToRender(writer);
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
				}
				user.RenderItem(ListItemType.Footer, -1, this, writer);
				if (flag)
				{
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
			}
			webControl.RenderEndTag(writer);
		}

		// Token: 0x06003C76 RID: 15478 RVA: 0x000C3DDC File Offset: 0x000C1FDC
		public void RenderRepeater(HtmlTextWriter writer, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
		{
			if (this.IsListLayout)
			{
				if (user.HasFooter || user.HasHeader || user.HasSeparators)
				{
					throw new InvalidOperationException(SR.GetString("RepeatInfo_ListLayoutDoesNotSupportHeaderFooterSeparator"));
				}
				if (this.RepeatDirection != RepeatDirection.Vertical)
				{
					throw new InvalidOperationException(SR.GetString("RepeatInfo_ListLayoutOnlySupportsVerticalLayout"));
				}
				if (this.RepeatColumns != 0 && this.RepeatColumns != 1)
				{
					throw new InvalidOperationException(SR.GetString("RepeatInfo_ListLayoutDoesNotSupportMultipleColumn"));
				}
				if (this.OuterTableImplied)
				{
					throw new InvalidOperationException(SR.GetString("RepeatInfo_ListLayoutDoesNotSupportImpliedOuterTable"));
				}
			}
			if (this.repeatDirection == RepeatDirection.Vertical)
			{
				this.RenderVerticalRepeater(writer, user, controlStyle, baseControl);
				return;
			}
			this.RenderHorizontalRepeater(writer, user, controlStyle, baseControl);
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x000C3E8C File Offset: 0x000C208C
		private void RenderVerticalRepeater(HtmlTextWriter writer, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
		{
			int repeatedItemCount = user.RepeatedItemCount;
			int num;
			int num2;
			int num3;
			if (this.repeatColumns == 0 || this.repeatColumns == 1)
			{
				num = 1;
				num2 = 1;
				num3 = repeatedItemCount;
			}
			else
			{
				num = this.repeatColumns;
				num3 = (repeatedItemCount + this.repeatColumns - 1) / this.repeatColumns;
				if (num3 == 0 && repeatedItemCount != 0)
				{
					num3 = 1;
				}
				num2 = repeatedItemCount % num;
				if (num2 == 0)
				{
					num2 = num;
				}
			}
			WebControl webControl = null;
			bool flag = false;
			if (!this.outerTableImplied)
			{
				switch (this.repeatLayout)
				{
				case RepeatLayout.Table:
					webControl = new Table();
					if (this.Caption.Length != 0)
					{
						((Table)webControl).Caption = this.Caption;
						((Table)webControl).CaptionAlign = this.CaptionAlign;
					}
					flag = true;
					break;
				case RepeatLayout.Flow:
					webControl = new WebControl(HtmlTextWriterTag.Span);
					break;
				case RepeatLayout.UnorderedList:
					webControl = new WebControl(HtmlTextWriterTag.Ul);
					break;
				case RepeatLayout.OrderedList:
					webControl = new WebControl(HtmlTextWriterTag.Ol);
					break;
				}
			}
			bool hasSeparators = user.HasSeparators;
			if (webControl != null)
			{
				webControl.ID = baseControl.ClientID;
				webControl.CopyBaseAttributes(baseControl);
				webControl.ApplyStyle(controlStyle);
				webControl.RenderBeginTag(writer);
			}
			if (user.HasHeader)
			{
				if (flag)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
					if (num != 1)
					{
						int num4 = num;
						if (hasSeparators)
						{
							num4 += num;
						}
						writer.AddAttribute(HtmlTextWriterAttribute.Colspan, num4.ToString(NumberFormatInfo.InvariantInfo));
					}
					if (this.useAccessibleHeader)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col");
					}
					Style itemStyle = user.GetItemStyle(ListItemType.Header, -1);
					if (itemStyle != null)
					{
						itemStyle.AddAttributesToRender(writer);
					}
					if (this.useAccessibleHeader)
					{
						writer.RenderBeginTag(HtmlTextWriterTag.Th);
					}
					else
					{
						writer.RenderBeginTag(HtmlTextWriterTag.Td);
					}
				}
				user.RenderItem(ListItemType.Header, -1, this, writer);
				if (flag)
				{
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
				else if (!this.outerTableImplied)
				{
					if (this.EnableLegacyRendering)
					{
						writer.WriteObsoleteBreak();
					}
					else
					{
						writer.WriteBreak();
					}
				}
			}
			int num5 = 0;
			for (int i = 0; i < num3; i++)
			{
				if (flag)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				}
				int num6 = i;
				int num7 = 0;
				while (num7 < num && num5 < repeatedItemCount)
				{
					if (num7 != 0)
					{
						num6 += num3;
						if (num7 - 1 >= num2)
						{
							num6--;
						}
					}
					if (num6 < repeatedItemCount)
					{
						num5++;
						if (flag)
						{
							Style itemStyle2 = user.GetItemStyle(ListItemType.Item, num6);
							if (itemStyle2 != null)
							{
								itemStyle2.AddAttributesToRender(writer);
							}
							writer.RenderBeginTag(HtmlTextWriterTag.Td);
						}
						if (this.IsListLayout)
						{
							writer.RenderBeginTag(HtmlTextWriterTag.Li);
						}
						user.RenderItem(ListItemType.Item, num6, this, writer);
						if (this.IsListLayout)
						{
							writer.RenderEndTag();
							writer.WriteLine();
						}
						if (flag)
						{
							writer.RenderEndTag();
						}
						if (hasSeparators)
						{
							if (num6 != repeatedItemCount - 1)
							{
								if (num == 1)
								{
									if (flag)
									{
										writer.RenderEndTag();
										writer.RenderBeginTag(HtmlTextWriterTag.Tr);
									}
									else if (!this.outerTableImplied)
									{
										if (this.EnableLegacyRendering)
										{
											writer.WriteObsoleteBreak();
										}
										else
										{
											writer.WriteBreak();
										}
									}
								}
								if (flag)
								{
									Style itemStyle3 = user.GetItemStyle(ListItemType.Separator, num6);
									if (itemStyle3 != null)
									{
										itemStyle3.AddAttributesToRender(writer);
									}
									writer.RenderBeginTag(HtmlTextWriterTag.Td);
								}
								if (num6 < repeatedItemCount)
								{
									user.RenderItem(ListItemType.Separator, num6, this, writer);
								}
								if (flag)
								{
									writer.RenderEndTag();
								}
							}
							else if (flag && num > 1)
							{
								writer.RenderBeginTag(HtmlTextWriterTag.Td);
								writer.RenderEndTag();
							}
						}
					}
					num7++;
				}
				if (flag)
				{
					if (i == num3 - 1)
					{
						int num8 = num - num2;
						if (hasSeparators)
						{
							int num9 = num8 * 2;
							if (num9 >= num8)
							{
								num8 = num9;
							}
						}
						if (num8 != 0)
						{
							for (int j = 0; j < num8; j++)
							{
								writer.RenderBeginTag(HtmlTextWriterTag.Td);
								writer.RenderEndTag();
							}
						}
					}
					writer.RenderEndTag();
				}
				else if ((i != num3 - 1 || user.HasFooter) && !this.outerTableImplied && !this.IsListLayout)
				{
					if (this.EnableLegacyRendering)
					{
						writer.WriteObsoleteBreak();
					}
					else
					{
						writer.WriteBreak();
					}
				}
			}
			if (user.HasFooter)
			{
				if (flag)
				{
					writer.RenderBeginTag(HtmlTextWriterTag.Tr);
					if (num != 1)
					{
						int num10 = num;
						if (hasSeparators)
						{
							num10 += num;
						}
						writer.AddAttribute(HtmlTextWriterAttribute.Colspan, num10.ToString(NumberFormatInfo.InvariantInfo));
					}
					Style itemStyle4 = user.GetItemStyle(ListItemType.Footer, -1);
					if (itemStyle4 != null)
					{
						itemStyle4.AddAttributesToRender(writer);
					}
					writer.RenderBeginTag(HtmlTextWriterTag.Td);
				}
				user.RenderItem(ListItemType.Footer, -1, this, writer);
				if (flag)
				{
					writer.RenderEndTag();
					writer.RenderEndTag();
				}
			}
			if (webControl != null)
			{
				webControl.RenderEndTag(writer);
			}
		}

		// Token: 0x0400237A RID: 9082
		private RepeatDirection repeatDirection;

		// Token: 0x0400237B RID: 9083
		private RepeatLayout repeatLayout;

		// Token: 0x0400237C RID: 9084
		private int repeatColumns;

		// Token: 0x0400237D RID: 9085
		private string caption;

		// Token: 0x0400237E RID: 9086
		private TableCaptionAlign captionAlign;

		// Token: 0x0400237F RID: 9087
		private bool useAccessibleHeader;

		// Token: 0x04002380 RID: 9088
		private bool outerTableImplied;

		// Token: 0x04002381 RID: 9089
		private bool enableLegacyRendering;
	}
}
