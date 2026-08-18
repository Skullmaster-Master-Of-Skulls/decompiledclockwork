using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001173 RID: 4467
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[Serializable]
	public class GridSortExpression : IStateManager
	{
		// Token: 0x0600B5F3 RID: 46579 RVA: 0x00280D02 File Offset: 0x0027EF02
		public GridSortExpression()
		{
			this.StateManager = new GridStateManager();
			((IStateManager)this).TrackViewState();
		}

		// Token: 0x17003ADC RID: 15068
		// (get) Token: 0x0600B5F4 RID: 46580 RVA: 0x00280D1B File Offset: 0x0027EF1B
		// (set) Token: 0x0600B5F5 RID: 46581 RVA: 0x00280D2D File Offset: 0x0027EF2D
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string FieldName
		{
			get
			{
				return this.StateManager.ViewStateGetString("_fn");
			}
			set
			{
				this.StateManager.ViewState["_fn"] = this.ParseExpression(value);
			}
		}

		// Token: 0x0600B5F6 RID: 46582 RVA: 0x00280D4B File Offset: 0x0027EF4B
		public string SortOrderAsString()
		{
			return GridSortExpression.SortOrderAsString(this.SortOrder);
		}

		// Token: 0x0600B5F7 RID: 46583 RVA: 0x00280D58 File Offset: 0x0027EF58
		public static GridSortOrder SortOrderFromString(string sortOrder)
		{
			if (sortOrder == null)
			{
				return GridSortOrder.None;
			}
			string a;
			if ((a = sortOrder.ToUpper()) != null)
			{
				if (a == "ASC")
				{
					return GridSortOrder.Ascending;
				}
				if (a == "DESC")
				{
					return GridSortOrder.Descending;
				}
			}
			return GridSortOrder.None;
		}

		// Token: 0x0600B5F8 RID: 46584 RVA: 0x00280D98 File Offset: 0x0027EF98
		public static string SortOrderAsString(GridSortOrder sortOrder)
		{
			switch (sortOrder)
			{
			case GridSortOrder.Ascending:
				return "ASC";
			case GridSortOrder.Descending:
				return "DESC";
			default:
				return "";
			}
		}

		// Token: 0x0600B5F9 RID: 46585 RVA: 0x00280DCC File Offset: 0x0027EFCC
		private string ParseExpression(string value)
		{
			string text = value;
			if (value != null)
			{
				text = value.Trim();
				int num = text.LastIndexOf(" ");
				if (num > 0)
				{
					string text2 = text.Substring(num);
					if (text2.Trim().ToUpper() == "ASC")
					{
						this.SortOrder = GridSortOrder.Ascending;
						text = text.Substring(0, num);
					}
					else if (text2.Trim().ToUpper() == "DESC")
					{
						this.SortOrder = GridSortOrder.Descending;
						text = text.Substring(0, num);
					}
				}
			}
			return text;
		}

		// Token: 0x17003ADD RID: 15069
		// (get) Token: 0x0600B5FA RID: 46586 RVA: 0x00280E4E File Offset: 0x0027F04E
		// (set) Token: 0x0600B5FB RID: 46587 RVA: 0x00280E6B File Offset: 0x0027F06B
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(GridSortOrder), "Ascending")]
		public GridSortOrder SortOrder
		{
			get
			{
				return (GridSortOrder)this.StateManager.ViewStateGetObject("_so", GridSortOrder.Ascending);
			}
			set
			{
				this.StateManager.ViewState["_so"] = value;
			}
		}

		// Token: 0x0600B5FC RID: 46588 RVA: 0x00280E88 File Offset: 0x0027F088
		public override bool Equals(object obj)
		{
			GridSortExpression gridSortExpression = obj as GridSortExpression;
			return gridSortExpression != null && this.FieldName == gridSortExpression.FieldName;
		}

		// Token: 0x0600B5FD RID: 46589 RVA: 0x00280EB2 File Offset: 0x0027F0B2
		public override int GetHashCode()
		{
			return this.FieldName.GetHashCode();
		}

		// Token: 0x0600B5FE RID: 46590 RVA: 0x00280EC0 File Offset: 0x0027F0C0
		public void SetSortOrder(string SortOrder)
		{
			try
			{
				this.SortOrder = (GridSortOrder)Enum.Parse(typeof(GridSortOrder), SortOrder);
			}
			catch
			{
				throw new GridGroupByException("Sort order " + SortOrder + " is unknown. Please check the expression syntax.");
			}
		}

		// Token: 0x0600B5FF RID: 46591 RVA: 0x00280F14 File Offset: 0x0027F114
		void IStateManager.LoadViewState(object state)
		{
			this.StateManager.LoadViewState(state);
		}

		// Token: 0x0600B600 RID: 46592 RVA: 0x00280F22 File Offset: 0x0027F122
		object IStateManager.SaveViewState()
		{
			return this.StateManager.SaveViewState();
		}

		// Token: 0x0600B601 RID: 46593 RVA: 0x00280F2F File Offset: 0x0027F12F
		void IStateManager.TrackViewState()
		{
			this.StateManager.TrackViewState();
		}

		// Token: 0x17003ADE RID: 15070
		// (get) Token: 0x0600B602 RID: 46594 RVA: 0x00280F3C File Offset: 0x0027F13C
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.StateManager.IsTrackingViewState;
			}
		}

		// Token: 0x0600B603 RID: 46595 RVA: 0x00280F49 File Offset: 0x0027F149
		public override string ToString()
		{
			return this.FieldName + " " + this.SortOrderAsString();
		}

		// Token: 0x0600B604 RID: 46596 RVA: 0x00280F64 File Offset: 0x0027F164
		public static GridSortExpression Parse(string expression)
		{
			return new GridSortExpression
			{
				FieldName = expression
			};
		}

		// Token: 0x0600B605 RID: 46597 RVA: 0x00280F7F File Offset: 0x0027F17F
		internal object Clone()
		{
			return GridSortExpression.Parse(this.ToString());
		}

		// Token: 0x04002FFB RID: 12283
		private GridStateManager StateManager;
	}
}
