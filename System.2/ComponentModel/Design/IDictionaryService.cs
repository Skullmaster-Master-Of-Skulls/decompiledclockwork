using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020005EC RID: 1516
	public interface IDictionaryService
	{
		// Token: 0x06003827 RID: 14375
		object GetKey(object value);

		// Token: 0x06003828 RID: 14376
		object GetValue(object key);

		// Token: 0x06003829 RID: 14377
		void SetValue(object key, object value);
	}
}
