using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Http.Routing
{
	// Token: 0x02000016 RID: 22
	internal static class RoutePrecedence
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003E80 File Offset: 0x00002080
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

		// Token: 0x0600009A RID: 154 RVA: 0x00003EE0 File Offset: 0x000020E0
		public static decimal Compute(HttpParsedRoute parsedRoute, IDictionary<string, object> constraints)
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
