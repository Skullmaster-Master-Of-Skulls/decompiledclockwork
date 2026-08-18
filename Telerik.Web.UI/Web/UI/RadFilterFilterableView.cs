using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x020018C6 RID: 6342
	public class RadFilterFilterableView
	{
		// Token: 0x0600F57C RID: 62844 RVA: 0x0037BFC9 File Offset: 0x0037A1C9
		public RadFilterFilterableView() : this(new List<RadFilterFieldDescriptor>(), new List<RadFilterGroupOperation>(), new List<RadFilterFunction>())
		{
		}

		// Token: 0x0600F57D RID: 62845 RVA: 0x0037BFE0 File Offset: 0x0037A1E0
		public RadFilterFilterableView(IList<RadFilterFieldDescriptor> dataFields, IList<RadFilterGroupOperation> groupTypes, IList<RadFilterFunction> filterFunctions)
		{
			this.DataFields = dataFields;
			this.SupportedGroupTypes = groupTypes;
			this.SupportedFilterFunctions = filterFunctions;
		}

		// Token: 0x170049FD RID: 18941
		// (get) Token: 0x0600F57E RID: 62846 RVA: 0x0037BFFD File Offset: 0x0037A1FD
		// (set) Token: 0x0600F57F RID: 62847 RVA: 0x0037C005 File Offset: 0x0037A205
		public IList<RadFilterFieldDescriptor> DataFields { get; set; }

		// Token: 0x170049FE RID: 18942
		// (get) Token: 0x0600F580 RID: 62848 RVA: 0x0037C00E File Offset: 0x0037A20E
		// (set) Token: 0x0600F581 RID: 62849 RVA: 0x0037C016 File Offset: 0x0037A216
		public IList<RadFilterGroupOperation> SupportedGroupTypes { get; set; }

		// Token: 0x170049FF RID: 18943
		// (get) Token: 0x0600F582 RID: 62850 RVA: 0x0037C01F File Offset: 0x0037A21F
		// (set) Token: 0x0600F583 RID: 62851 RVA: 0x0037C027 File Offset: 0x0037A227
		public IList<RadFilterFunction> SupportedFilterFunctions { get; set; }
	}
}
