using System;

namespace System.ComponentModel
{
	// Token: 0x02000563 RID: 1379
	public interface IIntellisenseBuilder
	{
		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060033A9 RID: 13225
		string Name { get; }

		// Token: 0x060033AA RID: 13226
		bool Show(string language, string value, ref string newValue);
	}
}
