using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.Common.UI.Web.DynamicControls.Entity
{
	// Token: 0x02000002 RID: 2
	public interface IDynamicWebControl
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		// (set) Token: 0x06000002 RID: 2
		DynamicFieldDTO DynamicField { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3
		// (set) Token: 0x06000004 RID: 4
		DynamicDataDTO DynamicData { get; set; }

		// Token: 0x06000005 RID: 5
		void ChildLoadViewState(object dataFromViewState);

		// Token: 0x06000006 RID: 6
		object ChildSaveViewState();

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7
		string ViewStateKey { get; }

		// Token: 0x06000008 RID: 8
		void ClearData();

		// Token: 0x06000009 RID: 9
		void ShowData(DynamicDataDTO data);

		// Token: 0x0600000A RID: 10
		DynamicDataDTO GetCurrentData(out bool isEmpty);
	}
}
