using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.TabStrip.Rendering
{
	// Token: 0x02001ADE RID: 6878
	public class TabClassicRenderer : TabRendererBase
	{
		// Token: 0x06010AC3 RID: 68291 RVA: 0x003B7218 File Offset: 0x003B5418
		internal TabClassicRenderer(RadTab tab) : base(tab)
		{
		}

		// Token: 0x17005121 RID: 20769
		// (get) Token: 0x06010AC4 RID: 68292 RVA: 0x003B7224 File Offset: 0x003B5424
		protected override List<string> CurrentCssClass
		{
			get
			{
				List<string> currentCssClass = base.CurrentCssClass;
				if (!string.IsNullOrEmpty(base.Tab.CssClass))
				{
					currentCssClass.Add(base.Tab.CssClass);
				}
				if (base.Tab.Selected)
				{
					currentCssClass.Add("rtsSelected");
					if (!string.IsNullOrEmpty(base.Tab.SelectedCssClass))
					{
						currentCssClass.Add(base.Tab.SelectedCssClass);
					}
				}
				if (!base.Tab.Enabled)
				{
					currentCssClass.Add("rtsDisabled");
					if (!string.IsNullOrEmpty(base.Tab.DisabledCssClass))
					{
						currentCssClass.Add(base.Tab.DisabledCssClass);
					}
				}
				int visibleIndex = base.Tab.VisibleIndex;
				int num = -1;
				if (base.Owner.SelectedTab != null)
				{
					num = base.Owner.SelectedTab.VisibleIndex;
				}
				if (num > -1)
				{
					if (visibleIndex == num - 1)
					{
						currentCssClass.Insert(1, "rtsBefore");
					}
					if (visibleIndex == num + 1)
					{
						currentCssClass.Insert(1, "rtsAfter");
					}
				}
				return currentCssClass;
			}
		}

		// Token: 0x06010AC5 RID: 68293 RVA: 0x003B73B8 File Offset: 0x003B55B8
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			IList<ControlItem> visibleTabs = base.Tab.Owner.Tabs.VisibleItems;
			int index = visibleTabs.IndexOf(base.Tab);
			bool isBreakPreviousTab = false;
			this.AddAttributesToRender(writer, delegate(List<string> cssClass)
			{
				if (index > 0)
				{
					isBreakPreviousTab = (visibleTabs[index - 1] as RadTab).IsBreak;
				}
				if (index == 0 || isBreakPreviousTab)
				{
					cssClass.Add("rtsFirst");
				}
				if (index == visibleTabs.Count - 1 || this.Tab.IsBreak)
				{
					cssClass.Add("rtsLast");
				}
			});
		}

		// Token: 0x06010AC6 RID: 68294 RVA: 0x003B7534 File Offset: 0x003B5734
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (base.Tab.Templated)
			{
				this.RenderDiv(writer, delegate
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtsOut");
					this.RenderDiv(writer, delegate
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtsIn");
						this.RenderDiv(writer, delegate
						{
							this.RenderImage(writer);
							this.RenderTemplateContent(writer);
						});
					});
				});
				return;
			}
			this.RenderLink(writer, delegate
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtsOut");
				this.RenderSpan(writer, delegate
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Class, "rtsIn");
					this.RenderSpan(writer, delegate
					{
						this.RenderImage(writer);
						this.RenderText(writer);
					});
				});
			});
		}
	}
}
