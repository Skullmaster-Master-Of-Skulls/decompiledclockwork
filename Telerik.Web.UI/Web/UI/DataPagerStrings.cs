using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F83 RID: 3971
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DataPagerStrings : LocalizationStrings
	{
		// Token: 0x0600981C RID: 38940 RVA: 0x00220BAF File Offset: 0x0021EDAF
		internal DataPagerStrings(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x1700301E RID: 12318
		// (get) Token: 0x0600981D RID: 38941 RVA: 0x00220BB8 File Offset: 0x0021EDB8
		// (set) Token: 0x0600981E RID: 38942 RVA: 0x00220BC5 File Offset: 0x0021EDC5
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		internal string NextButtonText
		{
			get
			{
				return this.GetString("NextButtonText");
			}
			set
			{
				this.SetString("NextButtonText", value);
			}
		}

		// Token: 0x1700301F RID: 12319
		// (get) Token: 0x0600981F RID: 38943 RVA: 0x00220BD3 File Offset: 0x0021EDD3
		// (set) Token: 0x06009820 RID: 38944 RVA: 0x00220BE0 File Offset: 0x0021EDE0
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		internal string PrevButtonText
		{
			get
			{
				return this.GetString("PrevButtonText");
			}
			set
			{
				this.SetString("PrevButtonText", value);
			}
		}

		// Token: 0x17003020 RID: 12320
		// (get) Token: 0x06009821 RID: 38945 RVA: 0x00220BEE File Offset: 0x0021EDEE
		// (set) Token: 0x06009822 RID: 38946 RVA: 0x00220BFB File Offset: 0x0021EDFB
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		internal string FirstButtonText
		{
			get
			{
				return this.GetString("FirstButtonText");
			}
			set
			{
				this.SetString("FirstButtonText", value);
			}
		}

		// Token: 0x17003021 RID: 12321
		// (get) Token: 0x06009823 RID: 38947 RVA: 0x00220C09 File Offset: 0x0021EE09
		// (set) Token: 0x06009824 RID: 38948 RVA: 0x00220C16 File Offset: 0x0021EE16
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		internal string LastButtonText
		{
			get
			{
				return this.GetString("LastButtonText");
			}
			set
			{
				this.SetString("LastButtonText", value);
			}
		}

		// Token: 0x17003022 RID: 12322
		// (get) Token: 0x06009825 RID: 38949 RVA: 0x00220C24 File Offset: 0x0021EE24
		// (set) Token: 0x06009826 RID: 38950 RVA: 0x00220C31 File Offset: 0x0021EE31
		[NotifyParentProperty(true)]
		[DefaultValue("Page")]
		internal string CurrentPageText
		{
			get
			{
				return this.GetString("CurrentPageText");
			}
			set
			{
				this.SetString("CurrentPageText", value);
			}
		}

		// Token: 0x17003023 RID: 12323
		// (get) Token: 0x06009827 RID: 38951 RVA: 0x00220C3F File Offset: 0x0021EE3F
		// (set) Token: 0x06009828 RID: 38952 RVA: 0x00220C4C File Offset: 0x0021EE4C
		[NotifyParentProperty(true)]
		[DefaultValue("of")]
		internal string TotalPageText
		{
			get
			{
				return this.GetString("TotalPageText");
			}
			set
			{
				this.SetString("TotalPageText", value);
			}
		}

		// Token: 0x17003024 RID: 12324
		// (get) Token: 0x06009829 RID: 38953 RVA: 0x00220C5A File Offset: 0x0021EE5A
		// (set) Token: 0x0600982A RID: 38954 RVA: 0x00220C67 File Offset: 0x0021EE67
		[DefaultValue("Go")]
		[NotifyParentProperty(true)]
		internal string SubmitButtonText
		{
			get
			{
				return this.GetString("SubmitButtonText");
			}
			set
			{
				this.SetString("SubmitButtonText", value);
			}
		}

		// Token: 0x17003025 RID: 12325
		// (get) Token: 0x0600982B RID: 38955 RVA: 0x00220C75 File Offset: 0x0021EE75
		// (set) Token: 0x0600982C RID: 38956 RVA: 0x00220C82 File Offset: 0x0021EE82
		[DefaultValue("Change")]
		[NotifyParentProperty(true)]
		internal string PageSizeSubmitButtonText
		{
			get
			{
				return this.GetString("PageSizeSubmitButtonText");
			}
			set
			{
				this.SetString("PageSizeSubmitButtonText", value);
			}
		}

		// Token: 0x17003026 RID: 12326
		// (get) Token: 0x0600982D RID: 38957 RVA: 0x00220C90 File Offset: 0x0021EE90
		// (set) Token: 0x0600982E RID: 38958 RVA: 0x00220C9D File Offset: 0x0021EE9D
		[NotifyParentProperty(true)]
		[DefaultValue("Page size")]
		internal string LabelText
		{
			get
			{
				return this.GetString("LabelText");
			}
			set
			{
				this.SetString("LabelText", value);
			}
		}

		// Token: 0x17003027 RID: 12327
		// (get) Token: 0x0600982F RID: 38959 RVA: 0x00220CAB File Offset: 0x0021EEAB
		// (set) Token: 0x06009830 RID: 38960 RVA: 0x00220CB8 File Offset: 0x0021EEB8
		[NotifyParentProperty(true)]
		[DefaultValue("Page size")]
		internal string PageSizeText
		{
			get
			{
				return this.GetString("PageSizeText");
			}
			set
			{
				this.SetString("PageSizeText", value);
			}
		}

		// Token: 0x17003028 RID: 12328
		// (get) Token: 0x06009831 RID: 38961 RVA: 0x00220CC6 File Offset: 0x0021EEC6
		// (set) Token: 0x06009832 RID: 38962 RVA: 0x00220CD3 File Offset: 0x0021EED3
		[DefaultValue("Drag")]
		[NotifyParentProperty(true)]
		internal string SliderDragText
		{
			get
			{
				return this.GetString("SliderDragText");
			}
			set
			{
				this.SetString("SliderDragText", value);
			}
		}

		// Token: 0x17003029 RID: 12329
		// (get) Token: 0x06009833 RID: 38963 RVA: 0x00220CE1 File Offset: 0x0021EEE1
		// (set) Token: 0x06009834 RID: 38964 RVA: 0x00220CEE File Offset: 0x0021EEEE
		[DefaultValue("Decrease")]
		[NotifyParentProperty(true)]
		internal string SliderDecreaseText
		{
			get
			{
				return this.GetString("SliderDecreaseText");
			}
			set
			{
				this.SetString("SliderDecreaseText", value);
			}
		}

		// Token: 0x1700302A RID: 12330
		// (get) Token: 0x06009835 RID: 38965 RVA: 0x00220CFC File Offset: 0x0021EEFC
		// (set) Token: 0x06009836 RID: 38966 RVA: 0x00220D09 File Offset: 0x0021EF09
		[NotifyParentProperty(true)]
		[DefaultValue("Increase")]
		internal string SliderIncreaseText
		{
			get
			{
				return this.GetString("SliderIncreaseText");
			}
			set
			{
				this.SetString("SliderIncreaseText", value);
			}
		}
	}
}
