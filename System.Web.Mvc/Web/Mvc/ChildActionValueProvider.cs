using System;
using System.Globalization;

namespace System.Web.Mvc
{
	// Token: 0x02000084 RID: 132
	public sealed class ChildActionValueProvider : DictionaryValueProvider<object>
	{
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000BB4D File Offset: 0x00009D4D
		public ChildActionValueProvider(ControllerContext controllerContext) : base(controllerContext.RouteData.Values, CultureInfo.InvariantCulture)
		{
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000BB65 File Offset: 0x00009D65
		internal static string ChildActionValuesKey
		{
			get
			{
				return ChildActionValueProvider._childActionValuesKey;
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000BB6C File Offset: 0x00009D6C
		public override ValueProviderResult GetValue(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			ValueProviderResult value = base.GetValue(ChildActionValueProvider.ChildActionValuesKey);
			if (value != null)
			{
				DictionaryValueProvider<object> dictionaryValueProvider = value.RawValue as DictionaryValueProvider<object>;
				if (dictionaryValueProvider != null)
				{
					return dictionaryValueProvider.GetValue(key);
				}
			}
			return null;
		}

		// Token: 0x04000110 RID: 272
		private static string _childActionValuesKey = Guid.NewGuid().ToString();
	}
}
