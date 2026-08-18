using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000015 RID: 21
	public interface IScriptComponentDescriptor
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000E1 RID: 225
		string ClientID { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000E2 RID: 226
		// (set) Token: 0x060000E3 RID: 227
		string ID { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000E4 RID: 228
		// (set) Token: 0x060000E5 RID: 229
		string Type { get; set; }

		// Token: 0x060000E6 RID: 230
		void AddComponentProperty(string name, string componentID);

		// Token: 0x060000E7 RID: 231
		void AddElementProperty(string name, string elementID);

		// Token: 0x060000E8 RID: 232
		void AddEvent(string name, string handler);

		// Token: 0x060000E9 RID: 233
		void AddProperty(string name, object value);
	}
}
