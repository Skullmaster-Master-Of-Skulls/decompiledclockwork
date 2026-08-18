using System;
using System.Globalization;
using System.Reflection;
using System.Web;

namespace System.ComponentModel.DataAnnotations
{
	// Token: 0x02000014 RID: 20
	internal class LocalizableString
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000034C7 File Offset: 0x000016C7
		public LocalizableString(string propertyName)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000034D6 File Offset: 0x000016D6
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000034DE File Offset: 0x000016DE
		public string Value
		{
			get
			{
				return this._propertyValue;
			}
			set
			{
				if (this._propertyValue != value)
				{
					this.ClearCache();
					this._propertyValue = value;
				}
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000034FB File Offset: 0x000016FB
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00003503 File Offset: 0x00001703
		public Type ResourceType
		{
			get
			{
				return this._resourceType;
			}
			set
			{
				if (this._resourceType != value)
				{
					this.ClearCache();
					this._resourceType = value;
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003520 File Offset: 0x00001720
		private void ClearCache()
		{
			this._cachedResult = null;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000352C File Offset: 0x0000172C
		public string GetLocalizableValue()
		{
			if (this._cachedResult == null)
			{
				if (this._propertyValue == null || this._resourceType == null)
				{
					this._cachedResult = (() => this._propertyValue);
				}
				else
				{
					PropertyInfo property = this._resourceType.GetProperty(this._propertyValue);
					bool flag = false;
					if (!this._resourceType.IsVisible || property == null || property.PropertyType != typeof(string))
					{
						flag = true;
					}
					else
					{
						MethodInfo getMethod = property.GetGetMethod();
						if (getMethod == null || !getMethod.IsPublic || !getMethod.IsStatic)
						{
							flag = true;
						}
					}
					if (flag)
					{
						string exceptionMessage = string.Format(CultureInfo.CurrentCulture, SR.GetString("LocalizableString_LocalizationFailed"), new object[]
						{
							this._propertyName,
							this._resourceType.FullName,
							this._propertyValue
						});
						this._cachedResult = delegate()
						{
							throw new InvalidOperationException(exceptionMessage);
						};
					}
					else
					{
						this._cachedResult = (() => (string)property.GetValue(null, null));
					}
				}
			}
			return this._cachedResult();
		}

		// Token: 0x0400006B RID: 107
		private string _propertyName;

		// Token: 0x0400006C RID: 108
		private string _propertyValue;

		// Token: 0x0400006D RID: 109
		private Type _resourceType;

		// Token: 0x0400006E RID: 110
		private Func<string> _cachedResult;
	}
}
