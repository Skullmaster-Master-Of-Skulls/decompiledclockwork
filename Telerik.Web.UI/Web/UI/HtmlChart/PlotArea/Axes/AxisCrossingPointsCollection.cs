using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace Telerik.Web.UI.HtmlChart.PlotArea.Axes
{
	// Token: 0x020004F1 RID: 1265
	[ParseChildren(typeof(AxisCrossingPoint))]
	public class AxisCrossingPointsCollection : StronglyTypedStateManagedCollection<AxisCrossingPoint>
	{
		// Token: 0x06002D17 RID: 11543 RVA: 0x0009426D File Offset: 0x0009246D
		public override void Add(AxisCrossingPoint item)
		{
			base.Add(item);
			this.SetDirtyObject(item);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x00094280 File Offset: 0x00092480
		public void Add(decimal? value)
		{
			AxisCrossingPoint item = new AxisCrossingPoint(value);
			base.Add(item);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x0009429C File Offset: 0x0009249C
		public void Add(int? value)
		{
			AxisCrossingPoint item = new AxisCrossingPoint(value);
			base.Add(item);
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000942B8 File Offset: 0x000924B8
		public void AddRange(IEnumerable<decimal> values)
		{
			foreach (decimal value in values)
			{
				this.Add(new AxisCrossingPoint(new decimal?(value)));
			}
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x0009430C File Offset: 0x0009250C
		public void AddRange(IEnumerable<decimal?> values)
		{
			foreach (decimal? value in values)
			{
				this.Add(new AxisCrossingPoint(value));
			}
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x0009435C File Offset: 0x0009255C
		public void AddRange(IEnumerable<int> values)
		{
			foreach (int value in values)
			{
				this.Add(new AxisCrossingPoint(new int?(value)));
			}
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000943B0 File Offset: 0x000925B0
		public void AddRange(IEnumerable<int?> values)
		{
			foreach (int? value in values)
			{
				this.Add(new AxisCrossingPoint(value));
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x00094400 File Offset: 0x00092600
		protected override void SetDirtyObject(object obj)
		{
			if (obj is AxisCrossingPoint)
			{
				((StateManager)obj).SetDirty();
			}
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x00094418 File Offset: 0x00092618
		internal string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.List.Count > 0)
			{
				stringBuilder.Append("axisCrossingValue:[");
				this.SerializeAxisCrossingPoints(stringBuilder);
				HtmlChartHelper.RemoveEndingComma(stringBuilder);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x00094468 File Offset: 0x00092668
		private void SerializeAxisCrossingPoints(StringBuilder sb)
		{
			foreach (object obj in base.List)
			{
				AxisCrossingPoint point = (AxisCrossingPoint)obj;
				this.SerializeAxisCrossingPoint(sb, point);
			}
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000944C4 File Offset: 0x000926C4
		private void SerializeAxisCrossingPoint(StringBuilder sb, AxisCrossingPoint point)
		{
			if (point.Value != null)
			{
				sb.AppendFormat("{0},", HtmlChartHelper.ToStringInvariant(point.Value));
				return;
			}
			sb.Append("null,");
		}
	}
}
