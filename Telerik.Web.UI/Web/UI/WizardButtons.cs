using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000995 RID: 2453
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class WizardButtons : LocalizationStrings
	{
		// Token: 0x06005D45 RID: 23877 RVA: 0x0011C9ED File Offset: 0x0011ABED
		internal WizardButtons(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x17001EC1 RID: 7873
		// (get) Token: 0x06005D46 RID: 23878 RVA: 0x0011C9F6 File Offset: 0x0011ABF6
		// (set) Token: 0x06005D47 RID: 23879 RVA: 0x0011CA03 File Offset: 0x0011AC03
		[DefaultValue("Previous")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Previous
		{
			get
			{
				return this.GetString("Previous");
			}
			set
			{
				this.SetString("Previous", value);
			}
		}

		// Token: 0x17001EC2 RID: 7874
		// (get) Token: 0x06005D48 RID: 23880 RVA: 0x0011CA11 File Offset: 0x0011AC11
		// (set) Token: 0x06005D49 RID: 23881 RVA: 0x0011CA1E File Offset: 0x0011AC1E
		[NotifyParentProperty(true)]
		[DefaultValue("Cancel")]
		[Localizable(true)]
		public string Cancel
		{
			get
			{
				return this.GetString("Cancel");
			}
			set
			{
				this.SetString("Cancel", value);
			}
		}

		// Token: 0x17001EC3 RID: 7875
		// (get) Token: 0x06005D4A RID: 23882 RVA: 0x0011CA2C File Offset: 0x0011AC2C
		// (set) Token: 0x06005D4B RID: 23883 RVA: 0x0011CA39 File Offset: 0x0011AC39
		[Localizable(true)]
		[DefaultValue("Next")]
		[NotifyParentProperty(true)]
		public string Next
		{
			get
			{
				return this.GetString("Next");
			}
			set
			{
				this.SetString("Next", value);
			}
		}

		// Token: 0x17001EC4 RID: 7876
		// (get) Token: 0x06005D4C RID: 23884 RVA: 0x0011CA47 File Offset: 0x0011AC47
		// (set) Token: 0x06005D4D RID: 23885 RVA: 0x0011CA54 File Offset: 0x0011AC54
		[DefaultValue("Finish")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string Finish
		{
			get
			{
				return this.GetString("Finish");
			}
			set
			{
				this.SetString("Finish", value);
			}
		}
	}
}
