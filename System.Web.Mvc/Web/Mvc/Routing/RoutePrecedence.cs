using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Mvc.Routing
{
	// Token: 0x02000010 RID: 16
	internal static class RoutePrecedence
	{
		// Token: 0x06000071 RID: 113 RVA: 0x000037DC File Offset: 0x000019DC
		internal static int ComputeDigit(PathContentSegment segment, IDictionary<string, object> constraints)
		{
			if (segment.Subsegments.Count > 1)
			{
				return 2;
			}
			PathSubsegment pathSubsegment = segment.Subsegments[0];
			if (pathSubsegment is PathLiteralSubsegment)
			{
				return 1;
			}
			PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
			int num = pathParameterSubsegment.IsCatchAll ? 5 : 3;
			if (constraints != null && constraints.ContainsKey(pathParameterSubsegment.ParameterName))
			{
				num--;
			}
			return num;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000383C File Offset: 0x00001A3C
		public static decimal Compute(ParsedRoute parsedRoute, IDictionary<string, object> constraints)
		{
			IList<PathContentSegment> list = parsedRoute.PathSegments.OfType<PathContentSegment>().ToArray<PathContentSegment>();
			decimal num = 0m;
			uint num2 = 1U;
			for (int i = 0; i < list.Count; i++)
			{
				PathContentSegment segment = list[i];
				int value = RoutePrecedence.ComputeDigit(segment, constraints);
				num += decimal.Divide(value, num2);
				num2 *= 10U;
			}
			return num;
		}
	}
}
