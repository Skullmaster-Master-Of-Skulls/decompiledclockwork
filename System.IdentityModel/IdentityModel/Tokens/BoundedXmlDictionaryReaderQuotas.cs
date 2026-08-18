using System;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000111 RID: 273
	internal static class BoundedXmlDictionaryReaderQuotas
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001FAB4 File Offset: 0x0001DCB4
		internal static XmlDictionaryReaderQuotas Quotas
		{
			get
			{
				if (LocalAppContextSwitches.AllowUnlimitedXmlRecursion)
				{
					return XmlDictionaryReaderQuotas.Max;
				}
				return BoundedXmlDictionaryReaderQuotas.BoundedQuotas;
			}
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		private static XmlDictionaryReaderQuotas CreateBoundedQuotas()
		{
			return new XmlDictionaryReaderQuotas
			{
				MaxDepth = 32,
				MaxStringContentLength = int.MaxValue,
				MaxArrayLength = int.MaxValue,
				MaxBytesPerRead = int.MaxValue,
				MaxNameTableCharCount = int.MaxValue
			};
		}

		// Token: 0x04000AC3 RID: 2755
		private static readonly XmlDictionaryReaderQuotas BoundedQuotas = BoundedXmlDictionaryReaderQuotas.CreateBoundedQuotas();
	}
}
