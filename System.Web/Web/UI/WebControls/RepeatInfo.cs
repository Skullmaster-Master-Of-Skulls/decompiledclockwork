using System;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000632 RID: 1586
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RepeatInfo
	{
		// Token: 0x06004E79 RID: 20089 RVA: 0x0013D52C File Offset: 0x0013C52C
		public RepeatInfo()
		{
			this.repeatDirection = RepeatDirection.Vertical;
			this.repeatLayout = RepeatLayout.Table;
			this.repeatColumns = 0;
			this.outerTableImplied = false;
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06004E7A RID: 20090 RVA: 0x0013D550 File Offset: 0x0013C550
		// (set) Token: 0x06004E7B RID: 20091 RVA: 0x0013D566 File Offset: 0x0013C566
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

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x06004E7C RID: 20092 RVA: 0x0013D56F File Offset: 0x0013C56F
		// (set) Token: 0x06004E7D RID: 20093 RVA: 0x0013D577 File Offset: 0x0013C577
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

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x06004E7E RID: 20094 RVA: 0x0013D593 File Offset: 0x0013C593
		// (set) Token: 0x06004E7F RID: 20095 RVA: 0x0013D59B File Offset: 0x0013C59B
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

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x06004E80 RID: 20096 RVA: 0x0013D5A4 File Offset: 0x0013C5A4
		// (set) Token: 0x06004E81 RID: 20097 RVA: 0x0013D5AC File Offset: 0x0013C5AC
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

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x06004E82 RID: 20098 RVA: 0x0013D5B5 File Offset: 0x0013C5B5
		// (set) Token: 0x06004E83 RID: 20099 RVA: 0x0013D5BD File Offset: 0x0013C5BD
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

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x06004E84 RID: 20100 RVA: 0x0013D5C6 File Offset: 0x0013C5C6
		// (set) Token: 0x06004E85 RID: 20101 RVA: 0x0013D5CE File Offset: 0x0013C5CE
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

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x06004E86 RID: 20102 RVA: 0x0013D5EA File Offset: 0x0013C5EA
		// (set) Token: 0x06004E87 RID: 20103 RVA: 0x0013D5F2 File Offset: 0x0013C5F2
		public RepeatLayout RepeatLayout
		{
			get
			{
				return this.repeatLayout;
			}
			set
			{
				if (value < RepeatLayout.Table || value > RepeatLayout.Flow)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.repeatLayout = value;
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x0013D60E File Offset: 0x0013C60E
		// (set) Token: 0x06004E89 RID: 20105 RVA: 0x0013D616 File Offset: 0x0013C616
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

		// Token: 0x06004E8A RID: 20106 RVA: 0x0013D620 File Offset: 0x0013C620
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

		// Token: 0x06004E8B RID: 20107 RVA: 0x0013D920 File Offset: 0x0013C920
		public void RenderRepeater(HtmlTextWriter writer, IRepeatInfoUser user, Style controlStyle, WebControl baseControl)
		{
			if (this.repeatDirection == RepeatDirection.Vertical)
			{
				this.RenderVerticalRepeater(writer, user, controlStyle, baseControl);
				return;
			}
			this.RenderHorizontalRepeater(writer, user, controlStyle, baseControl);
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x0013D944 File Offset: 0x0013C944
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
						user.RenderItem(ListItemType.Item, num6, this, writer);
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
				else if ((i != num3 - 1 || user.HasFooter) && !this.outerTableImplied)
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

		// Token: 0x04002C9A RID: 11418
		private RepeatDirection repeatDirection;

		// Token: 0x04002C9B RID: 11419
		private RepeatLayout repeatLayout;

		// Token: 0x04002C9C RID: 11420
		private int repeatColumns;

		// Token: 0x04002C9D RID: 11421
		private string caption;

		// Token: 0x04002C9E RID: 11422
		private TableCaptionAlign captionAlign;

		// Token: 0x04002C9F RID: 11423
		private bool useAccessibleHeader;

		// Token: 0x04002CA0 RID: 11424
		private bool outerTableImplied;

		// Token: 0x04002CA1 RID: 11425
		private bool enableLegacyRendering;
	}
}
