using System;
using System.Globalization;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000987 RID: 2439
	internal static class RedirectionUtility
	{
		// Token: 0x06005E73 RID: 24179 RVA: 0x0015D728 File Offset: 0x0015B928
		public static bool IsNamespaceAndValueMatch(string value1, string namespace1, string value2, string namespace2)
		{
			bool result = false;
			if (RedirectionUtility.IsNamespaceMatch(namespace1, namespace2))
			{
				result = string.Equals(value1, value2, StringComparison.Ordinal);
			}
			return result;
		}

		// Token: 0x06005E74 RID: 24180 RVA: 0x0015D74C File Offset: 0x0015B94C
		public static bool IsNamespaceMatch(string namespace1, string namespace2)
		{
			bool result = false;
			if (namespace1 == null && namespace2 == null)
			{
				result = true;
			}
			else if (namespace1 == null || namespace2 == null)
			{
				result = false;
			}
			else if (string.Equals(namespace1, namespace2, StringComparison.Ordinal))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x0015D77C File Offset: 0x0015B97C
		public static int ComputeHashCode(string value, string ns)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			string text = value + value.GetHashCode().ToString(CultureInfo.InvariantCulture);
			if (!string.IsNullOrEmpty(ns))
			{
				text += ns;
			}
			return text.GetHashCode();
		}
	}
}
