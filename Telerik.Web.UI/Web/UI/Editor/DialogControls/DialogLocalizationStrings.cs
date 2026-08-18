using System;
using System.Collections.Generic;
using System.Reflection;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x0200104F RID: 4175
	public class DialogLocalizationStrings : LocalizationStrings
	{
		// Token: 0x0600A3F2 RID: 41970 RVA: 0x0024703B File Offset: 0x0024523B
		internal DialogLocalizationStrings()
		{
		}

		// Token: 0x0600A3F3 RID: 41971 RVA: 0x0024704E File Offset: 0x0024524E
		internal DialogLocalizationStrings(LocalizationProvider localization, string dialogName, bool errorOnMissing) : base(localization, errorOnMissing)
		{
			this._dialogName = dialogName;
			this.InitializeDialogStrings();
		}

		// Token: 0x0600A3F4 RID: 41972 RVA: 0x00247070 File Offset: 0x00245270
		internal DialogLocalizationStrings(LocalizationProvider localization, string[] dialogNames, bool errorOnMissing) : base(localization, errorOnMissing)
		{
			foreach (string dialogName in dialogNames)
			{
				this._dialogName = dialogName;
				this.InitializeDialogStrings();
			}
		}

		// Token: 0x0600A3F5 RID: 41973 RVA: 0x002470B4 File Offset: 0x002452B4
		private void InitializeDialogStrings()
		{
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (propertyInfo.PropertyType == typeof(string) && this.CheckDialogString(propertyInfo))
				{
					this._dialogStrings[propertyInfo.Name] = base.GetString(propertyInfo.Name);
				}
			}
		}

		// Token: 0x0600A3F6 RID: 41974 RVA: 0x0024711E File Offset: 0x0024531E
		private bool CheckDialogString(PropertyInfo property)
		{
			return property.Name.StartsWith(this._dialogName) || property.Name.StartsWith("Common");
		}

		// Token: 0x0600A3F7 RID: 41975 RVA: 0x00247145 File Offset: 0x00245345
		public override void SetString(string key, string value)
		{
			this._dialogStrings[key] = value;
			base.SetString(key, value);
		}

		// Token: 0x0600A3F8 RID: 41976 RVA: 0x0024715C File Offset: 0x0024535C
		public override string GetString(string key)
		{
			string key2 = key;
			if (!this._dialogStrings.ContainsKey(key2))
			{
				key2 = this._dialogName + "_" + key;
			}
			if (!this._dialogStrings.ContainsKey(key2))
			{
				key2 = "Common_" + key;
			}
			if (!this._dialogStrings.ContainsKey(key2))
			{
				return key;
			}
			return this._dialogStrings[key2];
		}

		// Token: 0x0600A3F9 RID: 41977 RVA: 0x002471C4 File Offset: 0x002453C4
		public string GetJavaScriptString(string key)
		{
			return this.GetString(key).Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
		}

		// Token: 0x0600A3FA RID: 41978 RVA: 0x00247223 File Offset: 0x00245423
		public Dictionary<string, string>.KeyCollection GetStringKeys()
		{
			return this._dialogStrings.Keys;
		}

		// Token: 0x04002DAD RID: 11693
		private readonly string _dialogName;

		// Token: 0x04002DAE RID: 11694
		private readonly Dictionary<string, string> _dialogStrings = new Dictionary<string, string>();
	}
}
