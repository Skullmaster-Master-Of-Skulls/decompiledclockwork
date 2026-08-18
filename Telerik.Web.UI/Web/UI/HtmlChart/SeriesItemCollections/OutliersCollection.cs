using System;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.SeriesItemCollections
{
	// Token: 0x020004F5 RID: 1269
	[ParseChildren(typeof(Outlier))]
	public class OutliersCollection : StronglyTypedStateManagedCollection<Outlier>
	{
		// Token: 0x06002D3D RID: 11581 RVA: 0x000949DF File Offset: 0x00092BDF
		public override void Add(Outlier outlier)
		{
			base.Add(outlier);
			this.SetDirtyObject(outlier);
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000949F0 File Offset: 0x00092BF0
		public void Add(decimal? value)
		{
			Outlier item = new Outlier(value);
			this.Add(item);
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x00094A0C File Offset: 0x00092C0C
		public void AddRange(decimal?[] values)
		{
			foreach (decimal value in values)
			{
				Outlier item = new Outlier(value);
				this.Add(item);
			}
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x00094A44 File Offset: 0x00092C44
		protected override void SetDirtyObject(object obj)
		{
			if (obj is Outlier)
			{
				((StateManager)obj).SetDirty();
			}
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x00094A5C File Offset: 0x00092C5C
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.List.Count > 0)
			{
				stringBuilder.Append("outliers:[");
				this.SerializeOutliers(stringBuilder);
				HtmlChartHelper.RemoveEndingComma(stringBuilder);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x00094AAC File Offset: 0x00092CAC
		private void SerializeOutliers(StringBuilder sb)
		{
			foreach (object obj in base.List)
			{
				Outlier outlier = (Outlier)obj;
				sb.AppendFormat("{0},", outlier.Serialize());
			}
		}
	}
}
