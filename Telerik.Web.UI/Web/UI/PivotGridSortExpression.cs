using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000E10 RID: 3600
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[Serializable]
	public class PivotGridSortExpression : IStateManager
	{
		// Token: 0x060085BA RID: 34234 RVA: 0x001E7A0F File Offset: 0x001E5C0F
		public PivotGridSortExpression()
		{
			this.StateManager = new PivotGridControlStateManager();
			((IStateManager)this).TrackViewState();
		}

		// Token: 0x17002A5B RID: 10843
		// (get) Token: 0x060085BB RID: 34235 RVA: 0x001E7A28 File Offset: 0x001E5C28
		// (set) Token: 0x060085BC RID: 34236 RVA: 0x001E7A55 File Offset: 0x001E5C55
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Localizable(true)]
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

		// Token: 0x17002A5C RID: 10844
		// (get) Token: 0x060085BD RID: 34237 RVA: 0x001E7A70 File Offset: 0x001E5C70
		// (set) Token: 0x060085BE RID: 34238 RVA: 0x001E7A9E File Offset: 0x001E5C9E
		[DefaultValue(typeof(PivotGridSortOrder), "Ascending")]
		[NotifyParentProperty(true)]
		public PivotGridSortOrder SortOrder
		{
			get
			{
				object obj = this.StateManager["SortOrder"] ?? PivotGridSortOrder.Ascending;
				return (PivotGridSortOrder)obj;
			}
			set
			{
				this.StateManager["SortOrder"] = value;
			}
		}

		// Token: 0x060085BF RID: 34239 RVA: 0x001E7AB8 File Offset: 0x001E5CB8
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
		[SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", MessageId = "System.String.LastIndexOf(System.String)")]
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
						this.SortOrder = PivotGridSortOrder.Ascending;
						text = text.Substring(0, num);
					}
					else if (text2.Trim().ToUpper() == "DESC")
					{
						this.SortOrder = PivotGridSortOrder.Descending;
						text = text.Substring(0, num);
					}
					else if (text2.Trim().ToUpper() == "NONE")
					{
						this.SortOrder = PivotGridSortOrder.None;
						text = text.Substring(0, num);
					}
				}
			}
			return text;
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x001E7B6C File Offset: 0x001E5D6C
		public override bool Equals(object obj)
		{
			PivotGridSortExpression pivotGridSortExpression = obj as PivotGridSortExpression;
			return pivotGridSortExpression != null && this.FieldName == pivotGridSortExpression.FieldName;
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x001E7B96 File Offset: 0x001E5D96
		public override int GetHashCode()
		{
			return this.FieldName.GetHashCode();
		}

		// Token: 0x060085C2 RID: 34242 RVA: 0x001E7BA4 File Offset: 0x001E5DA4
		public void SetSortOrder(string SortOrder)
		{
			try
			{
				this.SortOrder = (PivotGridSortOrder)Enum.Parse(typeof(PivotGridSortOrder), SortOrder);
			}
			catch
			{
				throw new ArgumentException("Sort order " + SortOrder + " is unknown. Please check the expression syntax.");
			}
		}

		// Token: 0x060085C3 RID: 34243 RVA: 0x001E7BF8 File Offset: 0x001E5DF8
		public override string ToString()
		{
			return this.FieldName + " " + this.SortOrderAsString();
		}

		// Token: 0x060085C4 RID: 34244 RVA: 0x001E7C10 File Offset: 0x001E5E10
		public static string SortOrderAsString(PivotGridSortOrder sortOrder)
		{
			switch (sortOrder)
			{
			case PivotGridSortOrder.Ascending:
				return "ASC";
			case PivotGridSortOrder.Descending:
				return "DESC";
			case PivotGridSortOrder.None:
				return "NONE";
			default:
				return "ASC";
			}
		}

		// Token: 0x060085C5 RID: 34245 RVA: 0x001E7C4A File Offset: 0x001E5E4A
		public string SortOrderAsString()
		{
			return PivotGridSortExpression.SortOrderAsString(this.SortOrder);
		}

		// Token: 0x060085C6 RID: 34246 RVA: 0x001E7C58 File Offset: 0x001E5E58
		[SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToUpper")]
		public static PivotGridSortOrder SortOrderFromString(string sortOrder)
		{
			if (sortOrder == null)
			{
				return PivotGridSortOrder.Ascending;
			}
			string a;
			if ((a = sortOrder.ToUpper()) != null)
			{
				if (a == "ASC")
				{
					return PivotGridSortOrder.Ascending;
				}
				if (a == "DESC")
				{
					return PivotGridSortOrder.Descending;
				}
				if (a == "NONE")
				{
					return PivotGridSortOrder.None;
				}
			}
			return PivotGridSortOrder.Ascending;
		}

		// Token: 0x060085C7 RID: 34247 RVA: 0x001E7CA4 File Offset: 0x001E5EA4
		public static PivotGridSortExpression Parse(string expression)
		{
			return new PivotGridSortExpression
			{
				FieldName = expression
			};
		}

		// Token: 0x060085C8 RID: 34248 RVA: 0x001E7CC1 File Offset: 0x001E5EC1
		internal object Clone()
		{
			return PivotGridSortExpression.Parse(this.ToString());
		}

		// Token: 0x060085C9 RID: 34249 RVA: 0x001E7CCE File Offset: 0x001E5ECE
		void IStateManager.LoadViewState(object state)
		{
			((IStateManager)this.StateManager).LoadViewState(state);
		}

		// Token: 0x060085CA RID: 34250 RVA: 0x001E7CDC File Offset: 0x001E5EDC
		object IStateManager.SaveViewState()
		{
			return ((IStateManager)this.StateManager).SaveViewState();
		}

		// Token: 0x060085CB RID: 34251 RVA: 0x001E7CE9 File Offset: 0x001E5EE9
		void IStateManager.TrackViewState()
		{
			((IStateManager)this.StateManager).TrackViewState();
		}

		// Token: 0x17002A5D RID: 10845
		// (get) Token: 0x060085CC RID: 34252 RVA: 0x001E7CF6 File Offset: 0x001E5EF6
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return ((IStateManager)this.StateManager).IsTrackingViewState;
			}
		}

		// Token: 0x04002547 RID: 9543
		private PivotGridControlStateManager StateManager;
	}
}
