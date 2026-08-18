using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000F52 RID: 3922
	public interface IScriptDescriptor
	{
		// Token: 0x0600959A RID: 38298
		void AddComponentProperty(string name, string componentID);

		// Token: 0x0600959B RID: 38299
		void AddElementProperty(string name, string elementID);

		// Token: 0x0600959C RID: 38300
		void AddEvent(string name, string handler);

		// Token: 0x0600959D RID: 38301
		void AddProperty(string name, object value);

		// Token: 0x0600959E RID: 38302
		void AddScriptProperty(string name, string script);
	}
}
