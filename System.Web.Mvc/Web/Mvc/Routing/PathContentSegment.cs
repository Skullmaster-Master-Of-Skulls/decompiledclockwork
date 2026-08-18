using System;
using System.Collections.Generic;

namespace System.Web.Mvc.Routing
{
	// Token: 0x0200004E RID: 78
	internal sealed class PathContentSegment : PathSegment
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00007A85 File Offset: 0x00005C85
		public PathContentSegment(List<PathSubsegment> subsegments)
		{
			this.Subsegments = subsegments;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00007A94 File Offset: 0x00005C94
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00007ADB File Offset: 0x00005CDB
		// (set) Token: 0x0600021D RID: 541 RVA: 0x00007AE3 File Offset: 0x00005CE3
		public List<PathSubsegment> Subsegments { get; private set; }
	}
}
