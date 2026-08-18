using System;
using System.ComponentModel;
using System.Drawing;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001730 RID: 5936
	public class ChartAxisItemsCollection : ChartingStateManagedCollection<ChartAxisItem>
	{
		// Token: 0x1700464C RID: 17996
		// (get) Token: 0x0600E6FC RID: 59132 RVA: 0x0033A98A File Offset: 0x00338B8A
		// (set) Token: 0x0600E6FD RID: 59133 RVA: 0x0033A992 File Offset: 0x00338B92
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ChartAxis Parent
		{
			get
			{
				return this.chartAxisItemsCollectionParent;
			}
			set
			{
				this.chartAxisItemsCollectionParent = value;
			}
		}

		// Token: 0x1700464D RID: 17997
		[NotifyParentProperty(false)]
		[Description("Gets or sets a chart axis item at the specified position.")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ChartAxisItem this[int index]
		{
			get
			{
				return base.List[index];
			}
			set
			{
				base.List[index] = value;
				base.List[index].Parent = this;
			}
		}

		// Token: 0x0600E700 RID: 59136 RVA: 0x0033A9CA File Offset: 0x00338BCA
		public ChartAxisItemsCollection()
		{
		}

		// Token: 0x0600E701 RID: 59137 RVA: 0x0033A9D2 File Offset: 0x00338BD2
		public ChartAxisItemsCollection(ChartAxis parent)
		{
			this.chartAxisItemsCollectionParent = parent;
		}

		// Token: 0x0600E702 RID: 59138 RVA: 0x0033A9E1 File Offset: 0x00338BE1
		[Description("Creates a new instance of the AxisItems class with the specified default item font.")]
		public ChartAxisItemsCollection(Font itemFont)
		{
			this.chartAxisItemsCollectionParent.Appearance.TextAppearance.TextProperties.Font = itemFont;
		}

		// Token: 0x0600E703 RID: 59139 RVA: 0x0033AA04 File Offset: 0x00338C04
		[Description("Creates a new instance of the AxisItems class with the specified default item color.")]
		public ChartAxisItemsCollection(Color itemColor)
		{
			this.chartAxisItemsCollectionParent.Appearance.TextAppearance.TextProperties.Color = itemColor;
		}

		// Token: 0x0600E704 RID: 59140 RVA: 0x0033AA27 File Offset: 0x00338C27
		[Description("Creates a new instance of the AxisItems class with the specified default item font and color.")]
		public ChartAxisItemsCollection(Font itemFont, Color itemColor) : this(itemFont)
		{
			this.chartAxisItemsCollectionParent.Appearance.TextAppearance.TextProperties.Color = itemColor;
		}

		// Token: 0x0600E705 RID: 59141 RVA: 0x0033AA4B File Offset: 0x00338C4B
		internal void DeleteItem(int itemIndex)
		{
			if (itemIndex >= 0 && itemIndex < base.List.Count)
			{
				base.List.RemoveAt(itemIndex);
			}
		}

		// Token: 0x0600E706 RID: 59142 RVA: 0x0033AA6C File Offset: 0x00338C6C
		internal float GetItemRotationAngle(ChartAxisItem item)
		{
			ChartAxisItem chartAxisItem = new ChartAxisItem();
			float rotationAngle = item.Appearance.RotationAngle;
			if (item.Appearance.RotationAngle == chartAxisItem.Appearance.RotationAngle)
			{
				rotationAngle = this.Parent.Appearance.LabelAppearance.RotationAngle;
			}
			return rotationAngle;
		}

		// Token: 0x0600E707 RID: 59143 RVA: 0x0033AABC File Offset: 0x00338CBC
		internal float GetWidth()
		{
			if (!this.Parent.IsVisible())
			{
				return 0f;
			}
			float num = 0f;
			foreach (ChartAxisItem chartAxisItem in this)
			{
				if (this.Parent.CheckAxisItemVisibility(chartAxisItem))
				{
					chartAxisItem.Parent = this;
					float width = chartAxisItem.GetWidth(true, true);
					if (width > num)
					{
						num = width;
					}
				}
			}
			return num;
		}

		// Token: 0x0600E708 RID: 59144 RVA: 0x0033AB3C File Offset: 0x00338D3C
		internal float GetHeight()
		{
			if (!this.Parent.IsVisible())
			{
				return 0f;
			}
			float num = 0f;
			foreach (ChartAxisItem chartAxisItem in this)
			{
				if (this.Parent.CheckAxisItemVisibility(chartAxisItem))
				{
					chartAxisItem.Parent = this;
					float height = chartAxisItem.GetHeight(true, true);
					if (height > num)
					{
						num = height;
					}
				}
			}
			return num;
		}

		// Token: 0x0600E709 RID: 59145 RVA: 0x0033ABBC File Offset: 0x00338DBC
		[Description("Adds a chart axis item to the collection.")]
		public override void Add(ChartAxisItem chartAxisItem)
		{
			chartAxisItem.Parent = this;
			if (chartAxisItem.Appearance == null)
			{
				chartAxisItem.appearance = (StyleAxisLabel)this.Parent.Appearance.LabelAppearance.Clone();
				chartAxisItem.TextBlock.appearance = (StyleAxisItemText)this.Parent.Appearance.TextAppearance.Clone();
			}
			base.Add(chartAxisItem);
		}

		// Token: 0x04004265 RID: 16997
		private ChartAxis chartAxisItemsCollectionParent;
	}
}
