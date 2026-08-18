using System;
using System.Globalization;

namespace System.Web.Http.ValueProviders.Providers
{
	// Token: 0x020001A0 RID: 416
	internal sealed class ElementalValueProvider : IValueProvider
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x000235C9 File Offset: 0x000217C9
		public ElementalValueProvider(string name, object rawValue, CultureInfo culture)
		{
			this.Name = name;
			this.RawValue = rawValue;
			this.Culture = culture;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x000235E6 File Offset: 0x000217E6
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x000235EE File Offset: 0x000217EE
		public CultureInfo Culture { get; private set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000235F7 File Offset: 0x000217F7
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x000235FF File Offset: 0x000217FF
		public string Name { get; private set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00023608 File Offset: 0x00021808
		// (set) Token: 0x06000A8E RID: 2702 RVA: 0x00023610 File Offset: 0x00021810
		public object RawValue { get; private set; }

		// Token: 0x06000A8F RID: 2703 RVA: 0x00023619 File Offset: 0x00021819
		public bool ContainsPrefix(string prefix)
		{
			return PrefixContainer.IsPrefixMatch(prefix, this.Name);
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00023627 File Offset: 0x00021827
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
