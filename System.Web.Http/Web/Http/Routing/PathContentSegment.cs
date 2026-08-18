using System;
using System.Collections.Generic;

namespace System.Web.Http.Routing
{
	// Token: 0x0200010D RID: 269
	internal sealed class PathContentSegment : PathSegment
	{
		// Token: 0x0600067E RID: 1662 RVA: 0x00015D6B File Offset: 0x00013F6B
		public PathContentSegment(List<PathSubsegment> subsegments)
		{
			this.Subsegments = subsegments;
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00015D7C File Offset: 0x00013F7C
		public bool IsCatchAll
		{
			get
			{
				int count = this.Subsegments.Count;
				for (int i = 0; i < count; i++)
				{
					PathSubsegment pathSubsegment = this.Subsegments[i];
					PathParameterSubsegment pathParameterSubsegment = pathSubsegment as PathParameterSubsegment;
					if (pathParameterSubsegment != null && pathParameterSubsegment.IsCatchAll)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x00015DC3 File Offset: 0x00013FC3
		// (set) Token: 0x06000681 RID: 1665 RVA: 0x00015DCB File Offset: 0x00013FCB
		public List<PathSubsegment> Subsegments { get; private set; }
	}
}
