using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.ButtonNS.JavaScriptSerialization
{
	// Token: 0x0200001E RID: 30
	public interface IJavaScriptConverterProvider
	{
		// Token: 0x060001B1 RID: 433
		IEnumerable<JavaScriptConverter> GetJsConverters();
	}
}
