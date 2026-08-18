using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B49 RID: 2889
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class EditorRtfSettings : ObjectWithState
	{
		// Token: 0x06006CE2 RID: 27874 RVA: 0x00194716 File Offset: 0x00192916
		public EditorRtfSettings(StateBag OwnerStateBag) : base("ertfs_", OwnerStateBag)
		{
		}

		// Token: 0x170023BB RID: 9147
		// (get) Token: 0x06006CE3 RID: 27875 RVA: 0x00194724 File Offset: 0x00192924
		// (set) Token: 0x06006CE4 RID: 27876 RVA: 0x00194753 File Offset: 0x00192953
		[DefaultValue("")]
		[Description("Sets the text in the header of the rtf document.")]
		[NotifyParentProperty(true)]
		public string PageHeader
		{
			get
			{
				if (base.ViewState["_pheadRtf"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_pheadRtf"];
			}
			set
			{
				base.ViewState["_pheadRtf"] = value;
			}
		}

		// Token: 0x170023BC RID: 9148
		// (get) Token: 0x06006CE5 RID: 27877 RVA: 0x00194766 File Offset: 0x00192966
		// (set) Token: 0x06006CE6 RID: 27878 RVA: 0x00194796 File Offset: 0x00192996
		[DefaultValue(typeof(decimal), "7")]
		[NotifyParentProperty(true)]
		[Description("Sets the font size of the header of the rtf document in points.")]
		public decimal HeaderFontSizeInPoints
		{
			get
			{
				if (base.ViewState["_phfsRtf"] == null)
				{
					return 7m;
				}
				return Convert.ToDecimal(base.ViewState["_phfsRtf"]);
			}
			set
			{
				base.ViewState["_phfsRtf"] = value;
			}
		}

		// Token: 0x170023BD RID: 9149
		// (get) Token: 0x06006CE7 RID: 27879 RVA: 0x001947AE File Offset: 0x001929AE
		// (set) Token: 0x06006CE8 RID: 27880 RVA: 0x001947DF File Offset: 0x001929DF
		[DefaultValue(typeof(decimal), "11")]
		[NotifyParentProperty(true)]
		[Description("Sets the default font size in the rtf document in points.")]
		public decimal DefaultFontSizeInPoints
		{
			get
			{
				if (base.ViewState["_dfsRtf"] == null)
				{
					return 11m;
				}
				return Convert.ToDecimal(base.ViewState["_dfsRtf"]);
			}
			set
			{
				base.ViewState["_dfsRtf"] = Convert.ToString(value);
			}
		}

		// Token: 0x170023BE RID: 9150
		// (get) Token: 0x06006CE9 RID: 27881 RVA: 0x001947F7 File Offset: 0x001929F7
		// (set) Token: 0x06006CEA RID: 27882 RVA: 0x00194826 File Offset: 0x00192A26
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Sets the default font name in the rtf document.")]
		public string DefaultFontName
		{
			get
			{
				if (base.ViewState["_dfnRtf"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_dfnRtf"];
			}
			set
			{
				base.ViewState["_dfnRtf"] = value;
			}
		}
	}
}
