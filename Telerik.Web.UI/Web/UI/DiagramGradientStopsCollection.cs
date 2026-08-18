using System;
using System.Collections;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200023C RID: 572
	[ParseChildren(typeof(DiagramGradientStop))]
	public class DiagramGradientStopsCollection : StronglyTypedStateManagedCollection<DiagramGradientStop>
	{
		// Token: 0x17000708 RID: 1800
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00047FE6 File Offset: 0x000461E6
		internal IList ItemsList
		{
			get
			{
				return base.List;
			}
		}
	}
}
