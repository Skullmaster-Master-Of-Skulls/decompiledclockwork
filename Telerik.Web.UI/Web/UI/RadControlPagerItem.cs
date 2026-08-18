using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000548 RID: 1352
	internal abstract class RadControlPagerItem : CompositeControl
	{
		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x0009D242 File Offset: 0x0009B442
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x0009D246 File Offset: 0x0009B446
		// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x0009D24E File Offset: 0x0009B44E
		private RadControlPagerStyle PagerStyle { get; set; }

		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x0009D257 File Offset: 0x0009B457
		// (set) Token: 0x06002FDB RID: 12251 RVA: 0x0009D25F File Offset: 0x0009B45F
		private RadControlPagerButtonBuilder Builder { get; set; }

		// Token: 0x06002FDC RID: 12252 RVA: 0x0009D274 File Offset: 0x0009B474
		public void Recreate()
		{
			this.Controls.Clear();
			RadControlPagerItemProperties radControlPagerItemProperties = this.RequestRequriedProperties();
			this.PagerStyle = radControlPagerItemProperties.PagerStyle;
			if (!radControlPagerItemProperties.PagerStyle.AlwaysVisible && radControlPagerItemProperties.PagingSettings.PageCount == 1)
			{
				this.Visible = false;
				return;
			}
			this.Visible = true;
			this.Builder = new RadControlPagerButtonBuilder(this, radControlPagerItemProperties, delegate(string name, int value)
			{
				this.PagingPropertyChanged(name, value);
			});
			this.InitializePagerItem(this.Controls);
		}

		// Token: 0x06002FDD RID: 12253
		protected abstract void PagingPropertyChanged(string name, int value);

		// Token: 0x06002FDE RID: 12254
		protected abstract RadControlPagerItemProperties RequestRequriedProperties();

		// Token: 0x06002FDF RID: 12255 RVA: 0x0009D2EE File Offset: 0x0009B4EE
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, this.PagerStyle.Prefix + "Pager");
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x0009D314 File Offset: 0x0009B514
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.Recreate();
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x0009D324 File Offset: 0x0009B524
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (commandEventArgs != null)
			{
				this.PagingPropertyChanged(commandEventArgs.CommandName, int.Parse(commandEventArgs.CommandArgument.ToString()));
			}
			return base.OnBubbleEvent(source, args);
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x0009D360 File Offset: 0x0009B560
		private void InitializePagerItem(ControlCollection pagerContainer)
		{
			switch (this.PagerStyle.Mode)
			{
			case TreeListPagerMode.NextPrev:
			{
				Control control = this.Builder.BuildContainer("rtlArrPart1");
				control.Controls.Add(this.Builder.CreateFirstButton());
				control.Controls.Add(this.Builder.CreatePrevButton());
				control.Controls.Add(this.Builder.CreateNextButton());
				control.Controls.Add(this.Builder.CreateLastButton());
				pagerContainer.Add(control);
				return;
			}
			case TreeListPagerMode.NumericPages:
				pagerContainer.Add(this.Builder.CreateNumericPager());
				return;
			case TreeListPagerMode.NextPrevAndNumeric:
				this.CreateNextPrevAndNumeric(pagerContainer);
				if (this.PagerStyle.PageSizeControlType != PagerDropDownControlType.None)
				{
					pagerContainer.Add(this.Builder.CreatePageSize());
					return;
				}
				break;
			case TreeListPagerMode.NextPrevNumericAndAdvanced:
				this.CreateNextPrevAndNumeric(pagerContainer);
				pagerContainer.Add(this.Builder.CreateAdvancedPager());
				return;
			case TreeListPagerMode.Advanced:
				pagerContainer.Add(this.Builder.CreateAdvancedPager());
				return;
			case TreeListPagerMode.Slider:
				pagerContainer.Add(this.Builder.CreateSliderPager());
				break;
			default:
				return;
			}
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x0009D480 File Offset: 0x0009B680
		private void CreateNextPrevAndNumeric(ControlCollection pagerContainer)
		{
			Control control = this.Builder.BuildContainer("rtlArrPart1");
			control.Controls.Add(this.Builder.CreateFirstButton());
			control.Controls.Add(this.Builder.CreatePrevButton());
			pagerContainer.Add(control);
			pagerContainer.Add(this.Builder.CreateNumericPager());
			control = this.Builder.BuildContainer("rtlArrPart2");
			control.Controls.Add(this.Builder.CreateNextButton());
			control.Controls.Add(this.Builder.CreateLastButton());
			pagerContainer.Add(control);
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x0009D528 File Offset: 0x0009B728
		private Control GetButtonForArgument(string commandArgument)
		{
			if (commandArgument != null)
			{
				Control result;
				if (!(commandArgument == "First"))
				{
					if (!(commandArgument == "Next"))
					{
						if (!(commandArgument == "Prev"))
						{
							if (!(commandArgument == "Last"))
							{
								goto IL_73;
							}
							result = this.Builder.CreateLastButton();
						}
						else
						{
							result = this.Builder.CreatePrevButton();
						}
					}
					else
					{
						result = this.Builder.CreateNextButton();
					}
				}
				else
				{
					result = this.Builder.CreateFirstButton();
				}
				return result;
			}
			IL_73:
			throw new ArgumentOutOfRangeException("commandArgument");
		}
	}
}
