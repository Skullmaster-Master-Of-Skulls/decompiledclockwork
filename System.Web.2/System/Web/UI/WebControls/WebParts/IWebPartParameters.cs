using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200054A RID: 1354
	public interface IWebPartParameters
	{
		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x060044F0 RID: 17648
		PropertyDescriptorCollection Schema { get; }

		// Token: 0x060044F1 RID: 17649
		void GetParametersData(ParametersCallback callback);

		// Token: 0x060044F2 RID: 17650
		void SetConsumerSchema(PropertyDescriptorCollection schema);
	}
}
