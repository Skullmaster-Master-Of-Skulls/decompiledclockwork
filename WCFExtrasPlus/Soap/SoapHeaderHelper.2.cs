using System;

namespace WCFExtrasPlus.Soap
{
	// Token: 0x02000009 RID: 9
	public class SoapHeaderHelper<T>
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000265D File Offset: 0x0000085D
		public static T GetInputHeader(string name)
		{
			return (T)((object)SoapHeaderHelper<T>.soapHeaderHelper.GetInputHeader(name));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000266F File Offset: 0x0000086F
		public static void SetOutputHeader(string name, T value)
		{
			SoapHeaderHelper<T>.soapHeaderHelper.SetOutputHeader(name, value);
		}

		// Token: 0x0400000C RID: 12
		private static SoapHeaderHelper soapHeaderHelper = new SoapHeaderHelper(typeof(T));
	}
}
