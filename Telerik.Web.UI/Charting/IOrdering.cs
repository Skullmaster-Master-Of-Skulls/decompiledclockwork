using System;

namespace Telerik.Charting
{
	// Token: 0x020016DC RID: 5852
	public interface IOrdering
	{
		// Token: 0x17004558 RID: 17752
		// (get) Token: 0x0600E2E8 RID: 58088
		// (set) Token: 0x0600E2E9 RID: 58089
		IContainer Container { get; set; }

		// Token: 0x0600E2EA RID: 58090
		int GetOrder();

		// Token: 0x0600E2EB RID: 58091
		void SetOrder(int index);

		// Token: 0x0600E2EC RID: 58092
		void Remove();

		// Token: 0x0600E2ED RID: 58093
		void BringForward();

		// Token: 0x0600E2EE RID: 58094
		void BringToFront();

		// Token: 0x0600E2EF RID: 58095
		void SendBackward();

		// Token: 0x0600E2F0 RID: 58096
		void SendToBack();
	}
}
