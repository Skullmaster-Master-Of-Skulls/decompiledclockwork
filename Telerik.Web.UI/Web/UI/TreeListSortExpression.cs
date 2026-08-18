using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200127F RID: 4735
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[Serializable]
	public class TreeListSortExpression : IStateManager
	{
		// Token: 0x0600C55E RID: 50526 RVA: 0x002C1299 File Offset: 0x002BF499
		public TreeListSortExpression()
		{
			this.StateManager = new TreeListControlStateManager();
			((IStateManager)this).TrackViewState();
		}

		// Token: 0x17003FBC RID: 16316
		// (get) Token: 0x0600C55F RID: 50527 RVA: 0x002C12B4 File Offset: 0x002BF4B4
		// (set) Token: 0x0600C560 RID: 50528 RVA: 0x002C12E1 File Offset: 0x002BF4E1
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("")]
		public string FieldName
		{
			get
			{
				object obj = this.StateManager["FieldName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.StateManager["FieldName"] = this.ParseExpression(value);
			}
		}

		// Token: 0x17003FBD RID: 16317
		// (get) Token: 0x0600C561 RID: 50529 RVA: 0x002C12FC File Offset: 0x002BF4FC
		// (set) Token: 0x0600C562 RID: 50530 RVA: 0x002C132A File Offset: 0x002BF52A
		[DefaultValue(typeof(TreeListSortOrder), "Ascending")]
		[NotifyParentProperty(true)]
		public TreeListSortOrder SortOrder
		{
			get
			{
				object obj = this.StateManager["SortOrder"] ?? TreeListSortOrder.Ascending;
				return (TreeListSortOrder)obj;
			}
			set
			{
				this.StateManager["SortOrder"] = value;
			}
		}

		// Token: 0x0600C563 RID: 50531 RVA: 0x002C1344 File Offset: 0x002BF544
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.LastIndexOf(System.String)")]
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
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
						this.SortOrder = TreeListSortOrder.Ascending;
						text = text.Substring(0, num);
					}
					else if (text2.Trim().ToUpper() == "DESC")
					{
						this.SortOrder = TreeListSortOrder.Descending;
						text = text.Substring(0, num);
					}
				}
			}
			return text;
		}

		// Token: 0x0600C564 RID: 50532 RVA: 0x002C13C8 File Offset: 0x002BF5C8
		public override bool Equals(object obj)
		{
			TreeListSortExpression treeListSortExpression = obj as TreeListSortExpression;
			return treeListSortExpression != null && this.FieldName == treeListSortExpression.FieldName;
		}

		// Token: 0x0600C565 RID: 50533 RVA: 0x002C13F2 File Offset: 0x002BF5F2
		public override int GetHashCode()
		{
			return this.FieldName.GetHashCode();
		}

		// Token: 0x0600C566 RID: 50534 RVA: 0x002C1400 File Offset: 0x002BF600
		public void SetSortOrder(string SortOrder)
		{
			try
			{
				this.SortOrder = (TreeListSortOrder)Enum.Parse(typeof(TreeListSortOrder), SortOrder);
			}
			catch
			{
				throw new ArgumentException("Sort order " + SortOrder + " is unknown. Please check the expression syntax.");
			}
		}

		// Token: 0x0600C567 RID: 50535 RVA: 0x002C1454 File Offset: 0x002BF654
		public override string ToString()
		{
			return this.FieldName + " " + this.SortOrderAsString();
		}

		// Token: 0x0600C568 RID: 50536 RVA: 0x002C146C File Offset: 0x002BF66C
		public static string SortOrderAsString(TreeListSortOrder sortOrder)
		{
			switch (sortOrder)
			{
			case TreeListSortOrder.Ascending:
				return "ASC";
			case TreeListSortOrder.Descending:
				return "DESC";
			default:
				return "";
			}
		}

		// Token: 0x0600C569 RID: 50537 RVA: 0x002C149E File Offset: 0x002BF69E
		public string SortOrderAsString()
		{
			return TreeListSortExpression.SortOrderAsString(this.SortOrder);
		}

		// Token: 0x0600C56A RID: 50538 RVA: 0x002C14AC File Offset: 0x002BF6AC
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
		public static TreeListSortOrder SortOrderFromString(string sortOrder)
		{
			if (sortOrder == null)
			{
				return TreeListSortOrder.None;
			}
			string a;
			if ((a = sortOrder.ToUpper()) != null)
			{
				if (a == "ASC")
				{
					return TreeListSortOrder.Ascending;
				}
				if (a == "DESC")
				{
					return TreeListSortOrder.Descending;
				}
			}
			return TreeListSortOrder.None;
		}

		// Token: 0x0600C56B RID: 50539 RVA: 0x002C14EC File Offset: 0x002BF6EC
		public static TreeListSortExpression Parse(string expression)
		{
			return new TreeListSortExpression
			{
				FieldName = expression
			};
		}

		// Token: 0x0600C56C RID: 50540 RVA: 0x002C1509 File Offset: 0x002BF709
		internal object Clone()
		{
			return TreeListSortExpression.Parse(this.ToString());
		}

		// Token: 0x0600C56D RID: 50541 RVA: 0x002C1516 File Offset: 0x002BF716
		void IStateManager.LoadViewState(object state)
		{
			((IStateManager)this.StateManager).LoadViewState(state);
		}

		// Token: 0x0600C56E RID: 50542 RVA: 0x002C1524 File Offset: 0x002BF724
		object IStateManager.SaveViewState()
		{
			return ((IStateManager)this.StateManager).SaveViewState();
		}

		// Token: 0x0600C56F RID: 50543 RVA: 0x002C1531 File Offset: 0x002BF731
		void IStateManager.TrackViewState()
		{
			((IStateManager)this.StateManager).TrackViewState();
		}

		// Token: 0x17003FBE RID: 16318
		// (get) Token: 0x0600C570 RID: 50544 RVA: 0x002C153E File Offset: 0x002BF73E
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return ((IStateManager)this.StateManager).IsTrackingViewState;
			}
		}

		// Token: 0x04003432 RID: 13362
		private TreeListControlStateManager StateManager;
	}
}
