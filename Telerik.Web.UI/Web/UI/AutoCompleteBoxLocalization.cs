using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000192 RID: 402
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class AutoCompleteBoxLocalization : LocalizationStrings
	{
		// Token: 0x06000DB2 RID: 3506 RVA: 0x00034098 File Offset: 0x00032298
		internal AutoCompleteBoxLocalization(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x000340A1 File Offset: 0x000322A1
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x000340AE File Offset: 0x000322AE
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Remove token")]
		public string RemoveTokenTitle
		{
			get
			{
				return this.GetString("RemoveTokenTitle");
			}
			set
			{
				this.SetString("RemoveTokenTitle", value);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x000340BC File Offset: 0x000322BC
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x000340C9 File Offset: 0x000322C9
		[NotifyParentProperty(true)]
		[DefaultValue("Show All Results")]
		[Localizable(true)]
		public string ShowAllResults
		{
			get
			{
				return this.GetString("ShowAllResults");
			}
			set
			{
				this.SetString("ShowAllResults", value);
			}
		}
	}
}
