using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x02000F8A RID: 3978
	internal static class CallbackDeserializer
	{
		// Token: 0x06009864 RID: 39012 RVA: 0x002212A8 File Offset: 0x0021F4A8
		public static IList<ICallbackCommand> DeserializeCommands(string json)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new CallbackCommandConverter()
			});
			return javaScriptSerializer.Deserialize<ICallbackCommand[]>(json);
		}
	}
}
