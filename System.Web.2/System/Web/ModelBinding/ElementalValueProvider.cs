using System;
using System.Globalization;

namespace System.Web.ModelBinding
{
	// Token: 0x0200063E RID: 1598
	internal sealed class ElementalValueProvider : IValueProvider
	{
		// Token: 0x06004F20 RID: 20256 RVA: 0x00113072 File Offset: 0x00111272
		public ElementalValueProvider(string name, object rawValue, CultureInfo culture)
		{
			this.Name = name;
			this.RawValue = rawValue;
			this.Culture = culture;
		}

		// Token: 0x170016DD RID: 5853
		// (get) Token: 0x06004F21 RID: 20257 RVA: 0x0011308F File Offset: 0x0011128F
		// (set) Token: 0x06004F22 RID: 20258 RVA: 0x00113097 File Offset: 0x00111297
		public CultureInfo Culture { get; private set; }

		// Token: 0x170016DE RID: 5854
		// (get) Token: 0x06004F23 RID: 20259 RVA: 0x001130A0 File Offset: 0x001112A0
		// (set) Token: 0x06004F24 RID: 20260 RVA: 0x001130A8 File Offset: 0x001112A8
		public string Name { get; private set; }

		// Token: 0x170016DF RID: 5855
		// (get) Token: 0x06004F25 RID: 20261 RVA: 0x001130B1 File Offset: 0x001112B1
		// (set) Token: 0x06004F26 RID: 20262 RVA: 0x001130B9 File Offset: 0x001112B9
		public object RawValue { get; private set; }

		// Token: 0x06004F27 RID: 20263 RVA: 0x001130C2 File Offset: 0x001112C2
		public bool ContainsPrefix(string prefix)
		{
			return PrefixContainer.IsPrefixMatch(this.Name, prefix);
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x001130D0 File Offset: 0x001112D0
		public ValueProviderResult GetValue(string key)
		{
			if (!string.Equals(key, this.Name, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			return new ValueProviderResult(this.RawValue, Convert.ToString(this.RawValue, this.Culture), this.Culture);
		}
	}
}
