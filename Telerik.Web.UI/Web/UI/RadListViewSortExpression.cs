using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020019C6 RID: 6598
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadListViewSortExpression : IStateManager
	{
		// Token: 0x0600FEB1 RID: 65201 RVA: 0x00392F4D File Offset: 0x0039114D
		public RadListViewSortExpression()
		{
			this.StateManager = new ListViewControlStateManager();
			((IStateManager)this).TrackViewState();
		}

		// Token: 0x17004CE0 RID: 19680
		// (get) Token: 0x0600FEB2 RID: 65202 RVA: 0x00392F68 File Offset: 0x00391168
		// (set) Token: 0x0600FEB3 RID: 65203 RVA: 0x00392F95 File Offset: 0x00391195
		[Localizable(true)]
		[NotifyParentProperty(true)]
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

		// Token: 0x17004CE1 RID: 19681
		// (get) Token: 0x0600FEB4 RID: 65204 RVA: 0x00392FB0 File Offset: 0x003911B0
		// (set) Token: 0x0600FEB5 RID: 65205 RVA: 0x00392FDE File Offset: 0x003911DE
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(RadListViewSortOrder), "Ascending")]
		public RadListViewSortOrder SortOrder
		{
			get
			{
				object obj = this.StateManager["SortOrder"] ?? RadListViewSortOrder.Ascending;
				return (RadListViewSortOrder)obj;
			}
			set
			{
				this.StateManager["SortOrder"] = value;
			}
		}

		// Token: 0x0600FEB6 RID: 65206 RVA: 0x00392FF6 File Offset: 0x003911F6
		void IStateManager.LoadViewState(object state)
		{
			((IStateManager)this.StateManager).LoadViewState(state);
		}

		// Token: 0x0600FEB7 RID: 65207 RVA: 0x00393004 File Offset: 0x00391204
		object IStateManager.SaveViewState()
		{
			return ((IStateManager)this.StateManager).SaveViewState();
		}

		// Token: 0x0600FEB8 RID: 65208 RVA: 0x00393011 File Offset: 0x00391211
		void IStateManager.TrackViewState()
		{
			((IStateManager)this.StateManager).TrackViewState();
		}

		// Token: 0x17004CE2 RID: 19682
		// (get) Token: 0x0600FEB9 RID: 65209 RVA: 0x0039301E File Offset: 0x0039121E
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return ((IStateManager)this.StateManager).IsTrackingViewState;
			}
		}

		// Token: 0x0600FEBA RID: 65210 RVA: 0x0039302B File Offset: 0x0039122B
		public string SortOrderAsString()
		{
			return RadListViewSortExpression.SortOrderAsString(this.SortOrder);
		}

		// Token: 0x0600FEBB RID: 65211 RVA: 0x00393038 File Offset: 0x00391238
		public static RadListViewSortOrder SortOrderFromString(string sortOrder)
		{
			if (sortOrder == null)
			{
				return RadListViewSortOrder.None;
			}
			string a;
			if ((a = sortOrder.ToUpper()) != null)
			{
				if (a == "ASC")
				{
					return RadListViewSortOrder.Ascending;
				}
				if (a == "DESC")
				{
					return RadListViewSortOrder.Descending;
				}
			}
			return RadListViewSortOrder.None;
		}

		// Token: 0x0600FEBC RID: 65212 RVA: 0x00393078 File Offset: 0x00391278
		public static string SortOrderAsString(RadListViewSortOrder sortOrder)
		{
			switch (sortOrder)
			{
			case RadListViewSortOrder.Ascending:
				return "ASC";
			case RadListViewSortOrder.Descending:
				return "DESC";
			default:
				return "";
			}
		}

		// Token: 0x0600FEBD RID: 65213 RVA: 0x003930AC File Offset: 0x003912AC
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
						this.SortOrder = RadListViewSortOrder.Ascending;
						text = text.Substring(0, num);
					}
					else if (text2.Trim().ToUpper() == "DESC")
					{
						this.SortOrder = RadListViewSortOrder.Descending;
						text = text.Substring(0, num);
					}
				}
			}
			return text;
		}

		// Token: 0x0600FEBE RID: 65214 RVA: 0x00393130 File Offset: 0x00391330
		public override bool Equals(object obj)
		{
			RadListViewSortExpression radListViewSortExpression = obj as RadListViewSortExpression;
			return radListViewSortExpression != null && this.FieldName == radListViewSortExpression.FieldName;
		}

		// Token: 0x0600FEBF RID: 65215 RVA: 0x0039315A File Offset: 0x0039135A
		public override int GetHashCode()
		{
			return this.FieldName.GetHashCode();
		}

		// Token: 0x0600FEC0 RID: 65216 RVA: 0x00393168 File Offset: 0x00391368
		public void SetSortOrder(string SortOrder)
		{
			try
			{
				this.SortOrder = (RadListViewSortOrder)Enum.Parse(typeof(RadListViewSortOrder), SortOrder);
			}
			catch
			{
				throw new ArgumentException("Sort order " + SortOrder + " is unknown. Please check the expression syntax.");
			}
		}

		// Token: 0x0600FEC1 RID: 65217 RVA: 0x003931BC File Offset: 0x003913BC
		public override string ToString()
		{
			return this.FieldName + " " + this.SortOrderAsString();
		}

		// Token: 0x0600FEC2 RID: 65218 RVA: 0x003931D4 File Offset: 0x003913D4
		public static RadListViewSortExpression Parse(string expression)
		{
			return new RadListViewSortExpression
			{
				FieldName = expression
			};
		}

		// Token: 0x0600FEC3 RID: 65219 RVA: 0x003931F1 File Offset: 0x003913F1
		internal object Clone()
		{
			return RadListViewSortExpression.Parse(this.ToString());
		}

		// Token: 0x0400484C RID: 18508
		private ListViewControlStateManager StateManager;
	}
}
