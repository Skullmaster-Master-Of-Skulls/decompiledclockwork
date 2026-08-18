using System;
using System.Collections;

namespace Telerik.Charting
{
	// Token: 0x02001725 RID: 5925
	internal class AxisSegmentComparer : IComparer
	{
		// Token: 0x0600E62A RID: 58922 RVA: 0x00332EBC File Offset: 0x003310BC
		int IComparer.Compare(object x, object y)
		{
			AxisSegment axisSegment = (AxisSegment)x;
			AxisSegment axisSegment2 = (AxisSegment)y;
			if (axisSegment2.MinValue > axisSegment.MaxValue)
			{
				return 1;
			}
			if (axisSegment.MinValue > axisSegment2.MaxValue)
			{
				return -1;
			}
			return 0;
		}
	}
}
