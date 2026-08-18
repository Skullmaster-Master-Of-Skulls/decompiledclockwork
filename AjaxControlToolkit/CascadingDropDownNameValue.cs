using System;

namespace AjaxControlToolkit
{
	// Token: 0x02000067 RID: 103
	public class CascadingDropDownNameValue
	{
		// Token: 0x06000385 RID: 901 RVA: 0x0000AE58 File Offset: 0x00009058
		public CascadingDropDownNameValue()
		{
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000AE60 File Offset: 0x00009060
		public CascadingDropDownNameValue(string name, string value)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000AE76 File Offset: 0x00009076
		public CascadingDropDownNameValue(string name, string value, bool defaultValue)
		{
			this.name = name;
			this.value = value;
			this.isDefaultValue = defaultValue;
		}

		// Token: 0x0400011C RID: 284
		public string name;

		// Token: 0x0400011D RID: 285
		public string value;

		// Token: 0x0400011E RID: 286
		public bool isDefaultValue;

		// Token: 0x0400011F RID: 287
		public string optionTitle;
	}
}
