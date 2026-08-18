using System;
using System.ComponentModel;

namespace Telerik.Web
{
	// Token: 0x020000F2 RID: 242
	public class LocalizationStrings : StateManager
	{
		// Token: 0x06000A3B RID: 2619 RVA: 0x00025219 File Offset: 0x00023419
		internal LocalizationStrings(LocalizationProvider localization)
		{
			this._localization = localization;
			this._errorOnMissing = true;
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002522F File Offset: 0x0002342F
		internal LocalizationStrings(LocalizationProvider localization, bool errorOnMissing)
		{
			this._localization = localization;
			this._errorOnMissing = errorOnMissing;
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00025245 File Offset: 0x00023445
		protected LocalizationStrings()
		{
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0002524D File Offset: 0x0002344D
		public virtual string GetString(string key)
		{
			return this.GetString(key, this._errorOnMissing);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002525C File Offset: 0x0002345C
		public virtual string GetString(string key, bool throwErrorIfMissing)
		{
			string text = (base.ViewState[key] as string) ?? this._localization.GetString(key);
			if (text != null)
			{
				return text;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)[key];
			if (propertyDescriptor != null)
			{
				AttributeCollection attributes = propertyDescriptor.Attributes;
				DefaultValueAttribute defaultValueAttribute = attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
				if (defaultValueAttribute != null && defaultValueAttribute.Value != null)
				{
					return defaultValueAttribute.Value.ToString();
				}
			}
			if (throwErrorIfMissing)
			{
				throw new InvalidOperationException(string.Format("Cannot find a string resource with key '{0}' in App_GlobalResources/{1}.resx. Please, make sure that your custom localization has all needed resource strings, or copy the original localization resources from your installation folder to App_GlobalResources.", key, this._localization.ClassKey));
			}
			return key;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x000252F4 File Offset: 0x000234F4
		internal string GetStringSafe(string key)
		{
			string @string = this.GetString(key, false);
			if (@string == key)
			{
				return null;
			}
			return @string;
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00025316 File Offset: 0x00023516
		public virtual void SetString(string key, string value)
		{
			base.ViewState[key] = value;
		}

		// Token: 0x04000281 RID: 641
		private readonly LocalizationProvider _localization;

		// Token: 0x04000282 RID: 642
		private bool _errorOnMissing;
	}
}
