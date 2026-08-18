using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduler
{
	// Token: 0x02000F88 RID: 3976
	internal class CallbackCommandFactory<T> : ICallbackCommandFactory where T : ICallbackCommand
	{
		// Token: 0x0600985B RID: 39003 RVA: 0x00221178 File Offset: 0x0021F378
		public ICallbackCommand FromDictionary(IDictionary<string, object> dictionary, JavaScriptSerializer serializer)
		{
			this.ParseDate(dictionary, "Start");
			this.ParseDate(dictionary, "End");
			return serializer.ConvertToType<T>(dictionary);
		}

		// Token: 0x0600985C RID: 39004 RVA: 0x0022119E File Offset: 0x0021F39E
		private void ParseDate(IDictionary<string, object> dictionary, string key)
		{
			if (dictionary.ContainsKey(key))
			{
				dictionary[key] = this.ParseJavaScriptTime(dictionary[key].ToString());
			}
		}

		// Token: 0x0600985D RID: 39005 RVA: 0x002211C8 File Offset: 0x0021F3C8
		private DateTime ParseJavaScriptTime(string jsTime)
		{
			return DateTime.ParseExact(jsTime, "yyyyMMddHHmm", null, DateTimeStyles.AssumeUniversal).ToUniversalTime();
		}
	}
}
