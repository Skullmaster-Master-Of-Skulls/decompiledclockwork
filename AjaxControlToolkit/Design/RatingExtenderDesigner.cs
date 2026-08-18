using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.Design
{
	// Token: 0x02000167 RID: 359
	internal class RatingExtenderDesigner : ControlDesigner
	{
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x000189C2 File Offset: 0x00016BC2
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._actionLists == null)
				{
					this._actionLists = new DesignerActionListCollection();
					this._actionLists.AddRange(base.ActionLists);
					this._actionLists.Add(new RatingExtenderDesigner.ActionList(this));
				}
				return this._actionLists;
			}
		}

		// Token: 0x040003C4 RID: 964
		private DesignerActionListCollection _actionLists;

		// Token: 0x02000168 RID: 360
		public class ActionList : DesignerActionList
		{
			// Token: 0x06000995 RID: 2453 RVA: 0x00018A00 File Offset: 0x00016C00
			public ActionList(RatingExtenderDesigner parent) : base(parent.Component)
			{
				this._parent = parent;
			}

			// Token: 0x170003A1 RID: 929
			// (get) Token: 0x06000996 RID: 2454 RVA: 0x00018A15 File Offset: 0x00016C15
			// (set) Token: 0x06000997 RID: 2455 RVA: 0x00018A2C File Offset: 0x00016C2C
			public int StartRating
			{
				get
				{
					return ((Rating)this._parent.Component).CurrentRating;
				}
				set
				{
					try
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._parent.Component)["CurrentRating"];
						propertyDescriptor.SetValue(this._parent.Component, value);
					}
					catch
					{
						throw;
					}
				}
			}

			// Token: 0x170003A2 RID: 930
			// (get) Token: 0x06000998 RID: 2456 RVA: 0x00018A80 File Offset: 0x00016C80
			// (set) Token: 0x06000999 RID: 2457 RVA: 0x00018A98 File Offset: 0x00016C98
			public int MaxRating
			{
				get
				{
					return ((Rating)this._parent.Component).MaxRating;
				}
				set
				{
					try
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._parent.Component)["MaxRating"];
						propertyDescriptor.SetValue(this._parent.Component, value);
					}
					catch
					{
						throw;
					}
				}
			}

			// Token: 0x170003A3 RID: 931
			// (get) Token: 0x0600099A RID: 2458 RVA: 0x00018AEC File Offset: 0x00016CEC
			// (set) Token: 0x0600099B RID: 2459 RVA: 0x00018B04 File Offset: 0x00016D04
			public bool RealOnly
			{
				get
				{
					return ((Rating)this._parent.Component).ReadOnly;
				}
				set
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._parent.Component)["ReadOnly"];
					propertyDescriptor.SetValue(this._parent.Component, value);
				}
			}

			// Token: 0x0600099C RID: 2460 RVA: 0x00018B44 File Offset: 0x00016D44
			public override DesignerActionItemCollection GetSortedActionItems()
			{
				if (this._items == null)
				{
					this._items = new DesignerActionItemCollection();
					this._items.Add(new DesignerActionPropertyItem("StartRating", "Initial Rating"));
					this._items.Add(new DesignerActionPropertyItem("MaxRating", "Maximum Rating"));
					this._items.Add(new DesignerActionPropertyItem("RealOnly", "Read-only"));
					this._items.Add(new DesignerActionMethodItem(this, "Alignment", "Switch Align"));
					this._items.Add(new DesignerActionMethodItem(this, "Direction", "Switch Direction"));
				}
				return this._items;
			}

			// Token: 0x0600099D RID: 2461 RVA: 0x00018BF8 File Offset: 0x00016DF8
			private void Alignment()
			{
				Rating rating = (Rating)this._parent.Component;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(rating)["RatingAlign"];
				if (rating.RatingAlign == Orientation.Horizontal)
				{
					propertyDescriptor.SetValue(rating, Orientation.Vertical);
					return;
				}
				propertyDescriptor.SetValue(rating, Orientation.Horizontal);
			}

			// Token: 0x0600099E RID: 2462 RVA: 0x00018C4C File Offset: 0x00016E4C
			private void Direction()
			{
				Rating rating = (Rating)this._parent.Component;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(rating)["RatingDirection"];
				if (rating.RatingDirection == RatingDirection.LeftToRightTopToBottom)
				{
					propertyDescriptor.SetValue(rating, RatingDirection.RightToLeftBottomToTop);
					return;
				}
				propertyDescriptor.SetValue(rating, RatingDirection.LeftToRightTopToBottom);
			}

			// Token: 0x040003C5 RID: 965
			private RatingExtenderDesigner _parent;

			// Token: 0x040003C6 RID: 966
			private DesignerActionItemCollection _items;
		}
	}
}
