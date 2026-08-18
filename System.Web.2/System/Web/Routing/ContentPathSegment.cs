using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Routing
{
	// Token: 0x0200013E RID: 318
	internal sealed class ContentPathSegment : PathSegment
	{
		// Token: 0x060012F6 RID: 4854 RVA: 0x0003683B File Offset: 0x00034A3B
		public ContentPathSegment(IList<PathSubsegment> subsegments)
		{
			this.Subsegments = subsegments;
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x0003684A File Offset: 0x00034A4A
		public bool IsCatchAll
		{
			get
			{
				return this.Subsegments.Any((PathSubsegment seg) => seg is ParameterSubsegment && ((ParameterSubsegment)seg).IsCatchAll);
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x00036876 File Offset: 0x00034A76
		// (set) Token: 0x060012F9 RID: 4857 RVA: 0x0003687E File Offset: 0x00034A7E
		public IList<PathSubsegment> Subsegments { get; private set; }
	}
}
