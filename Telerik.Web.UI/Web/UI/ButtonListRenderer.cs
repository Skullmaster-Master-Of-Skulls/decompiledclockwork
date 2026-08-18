using System;
using System.Text;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020000B8 RID: 184
	internal class ButtonListRenderer : RendererBase
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x0001C3F4 File Offset: 0x0001A5F4
		public ButtonListRenderer(RadButtonList buttonList)
		{
			this.buttonList = buttonList;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0001C403 File Offset: 0x0001A603
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (this.buttonList.Layout == ButtonListLayout.Flow)
			{
				this.RenderFlowLayout(writer);
				return;
			}
			this.RenderListLayout(writer);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0001C428 File Offset: 0x0001A628
		private void RenderFlowLayout(HtmlTextWriter writer)
		{
			if (this.buttonList.Direction == ButtonListDirection.Vertical)
			{
				this.RenderVerticalFlowLayout(writer);
				return;
			}
			this.RenderHorizontalFlowLayout(writer);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0001C446 File Offset: 0x0001A646
		private void RenderVerticalFlowLayout(HtmlTextWriter writer)
		{
			if (this.buttonList.Columns > 1)
			{
				this.RenderVerticalItemsInColumns(writer);
				return;
			}
			this.RenderVerticalItems(0, this.buttonList.Controls.Count, writer);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001C478 File Offset: 0x0001A678
		private void RenderVerticalItemsInColumns(HtmlTextWriter writer)
		{
			int num = 0;
			for (int i = 0; i < this.buttonList.Columns; i++)
			{
				int currentColumnItemCount = this.GetCurrentColumnItemCount(i);
				this.RenderColumn(writer, currentColumnItemCount, num);
				num += currentColumnItemCount;
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		private int GetCurrentColumnItemCount(int i)
		{
			int count = this.buttonList.Controls.Count;
			int columns = this.buttonList.Columns;
			int num = count / columns;
			int num2 = count % columns;
			if (i < num2)
			{
				num++;
			}
			return num;
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0001C4EF File Offset: 0x0001A6EF
		private void RenderColumn(HtmlTextWriter writer, int itemsForCurrentColumn, int startItemIndex)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rb{0}List", ButtonListDirection.Vertical.ToString()));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderVerticalItems(startItemIndex, itemsForCurrentColumn, writer);
			writer.RenderEndTag();
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x0001C528 File Offset: 0x0001A728
		public virtual void RenderVerticalItems(int startItemIndex, int itemsCountInGroup, HtmlTextWriter writer)
		{
			for (int i = startItemIndex; i < startItemIndex + itemsCountInGroup; i++)
			{
				this.buttonList.Controls[i].RenderControl(writer);
			}
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0001C55C File Offset: 0x0001A75C
		public virtual void RenderHorizontalFlowLayout(HtmlTextWriter writer)
		{
			ControlCollection controls = this.buttonList.Controls;
			for (int i = 0; i < controls.Count; i++)
			{
				controls[i].RenderControl(writer);
				if (this.ShouldRenderBrTag(i))
				{
					writer.WriteBreak();
				}
			}
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0001C5A4 File Offset: 0x0001A7A4
		private bool ShouldRenderBrTag(int itemIndex)
		{
			int columns = this.buttonList.Columns;
			return columns > 0 && (itemIndex + 1) % columns == 0 && itemIndex < this.buttonList.Controls.Count - 1;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
		private void RenderListLayout(HtmlTextWriter writer)
		{
			foreach (object obj in this.buttonList.Controls)
			{
				Control control = (Control)obj;
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				control.RenderControl(writer);
				writer.RenderEndTag();
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x0001C64C File Offset: 0x0001A84C
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				switch (this.buttonList.Layout)
				{
				case ButtonListLayout.OrderedList:
					return HtmlTextWriterTag.Ol;
				case ButtonListLayout.UnorderedList:
					return HtmlTextWriterTag.Ul;
				default:
					return HtmlTextWriterTag.Div;
				}
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001C67F File Offset: 0x0001A87F
		public override string CssClassFormatString
		{
			get
			{
				return this.CreateCssClassFormatString();
			}
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0001C688 File Offset: 0x0001A888
		private string CreateCssClassFormatString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			string name = this.buttonList.GetType().Name;
			stringBuilder.Append(name);
			stringBuilder.Append(" ");
			stringBuilder.Append(name + "_{0}");
			if (this.buttonList.Layout != ButtonListLayout.Flow)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format("rb{0}List", ButtonListDirection.Vertical.ToString()));
			}
			else if (!this.IsLayoutVerticalFlowWithColumns())
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(string.Format("rb{0}List", this.buttonList.Direction.ToString()));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001C749 File Offset: 0x0001A949
		private bool IsLayoutVerticalFlowWithColumns()
		{
			return this.buttonList.Columns > 1 && this.buttonList.Direction == ButtonListDirection.Vertical;
		}

		// Token: 0x04000181 RID: 385
		private const string CSS_CLASS_DIRECTION_FORMAT = "rb{0}List";

		// Token: 0x04000182 RID: 386
		private readonly RadButtonList buttonList;
	}
}
