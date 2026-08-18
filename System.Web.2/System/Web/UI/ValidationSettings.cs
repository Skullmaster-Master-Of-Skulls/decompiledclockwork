using System;
using System.Configuration;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000324 RID: 804
	public static class ValidationSettings
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060025B0 RID: 9648 RVA: 0x0007C78C File Offset: 0x0007A98C
		// (set) Token: 0x060025B1 RID: 9649 RVA: 0x0007C7FF File Offset: 0x0007A9FF
		public static UnobtrusiveValidationMode UnobtrusiveValidationMode
		{
			get
			{
				if (ValidationSettings._unobtrusiveValidationMode == null)
				{
					string value = ConfigurationManager.AppSettings["ValidationSettings:UnobtrusiveValidationMode"];
					object obj = PropertyConverter.EnumFromString(typeof(UnobtrusiveValidationMode), value);
					if (obj == null)
					{
						ValidationSettings._unobtrusiveValidationMode = new UnobtrusiveValidationMode?(BinaryCompatibility.Current.TargetsAtLeastFramework45 ? UnobtrusiveValidationMode.WebForms : UnobtrusiveValidationMode.None);
					}
					else
					{
						ValidationSettings._unobtrusiveValidationMode = new UnobtrusiveValidationMode?((UnobtrusiveValidationMode)obj);
					}
				}
				return ValidationSettings._unobtrusiveValidationMode.Value;
			}
			set
			{
				if (value < UnobtrusiveValidationMode.None || value > UnobtrusiveValidationMode.WebForms)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ValidationSettings._unobtrusiveValidationMode = new UnobtrusiveValidationMode?(value);
			}
		}

		// Token: 0x04001D7B RID: 7547
		private static UnobtrusiveValidationMode? _unobtrusiveValidationMode;
	}
}
