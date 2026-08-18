using System;

namespace WCFExtras.Soap
{
	// Token: 0x02000008 RID: 8
	public class SoapHeaderHelper<T>
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002C04 File Offset: 0x00000E04
		public static T GetInputHeader(string name)
		{
			return (T)((object)SoapHeaderHelper<T>.soapHeaderHelper.GetInputHeader(name));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002C26 File Offset: 0x00000E26
		public static void SetOutputHeader(string name, T value)
		{
			SoapHeaderHelper<T>.soapHeaderHelper.SetOutputHeader(name, value);
		}

		// Token: 0x04000008 RID: 8
		private static SoapHeaderHelper soapHeaderHelper = new SoapHeaderHelper(typeof(T));
	}
}
