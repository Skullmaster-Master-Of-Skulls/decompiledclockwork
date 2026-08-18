using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020002A4 RID: 676
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class EditorDocxSettings : ObjectWithState
	{
		// Token: 0x060017E6 RID: 6118 RVA: 0x0004F6E1 File Offset: 0x0004D8E1
		public EditorDocxSettings(StateBag OwnerStateBag) : base("edocxs_", OwnerStateBag)
		{
		}

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x060017E7 RID: 6119 RVA: 0x0004F6EF File Offset: 0x0004D8EF
		// (set) Token: 0x060017E8 RID: 6120 RVA: 0x0004F71E File Offset: 0x0004D91E
		[NotifyParentProperty(true)]
		[Description("Sets the text in the header of the docx document.")]
		[DefaultValue("")]
		public string PageHeader
		{
			get
			{
				if (base.ViewState["_pheadDocx"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_pheadDocx"];
			}
			set
			{
				base.ViewState["_pheadDocx"] = value;
			}
		}

		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x060017E9 RID: 6121 RVA: 0x0004F731 File Offset: 0x0004D931
		// (set) Token: 0x060017EA RID: 6122 RVA: 0x0004F761 File Offset: 0x0004D961
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(decimal), "7")]
		[Description("Sets the font size of the header of the docx document in points.")]
		public decimal HeaderFontSizeInPoints
		{
			get
			{
				if (base.ViewState["_phfsDocx"] == null)
				{
					return 7m;
				}
				return Convert.ToDecimal(base.ViewState["_phfsDocx"]);
			}
			set
			{
				base.ViewState["_phfsDocx"] = value;
			}
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x0004F779 File Offset: 0x0004D979
		// (set) Token: 0x060017EC RID: 6124 RVA: 0x0004F7AA File Offset: 0x0004D9AA
		[DefaultValue(typeof(decimal), "11")]
		[NotifyParentProperty(true)]
		[Description("Sets the default font size in the docx document in points.")]
		public decimal DefaultFontSizeInPoints
		{
			get
			{
				if (base.ViewState["_dfsDocx"] == null)
				{
					return 11m;
				}
				return Convert.ToDecimal(base.ViewState["_dfsDocx"]);
			}
			set
			{
				base.ViewState["_dfsDocx"] = Convert.ToString(value);
			}
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x0004F7C2 File Offset: 0x0004D9C2
		// (set) Token: 0x060017EE RID: 6126 RVA: 0x0004F7F1 File Offset: 0x0004D9F1
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Sets the default font name in the docx document.")]
		public string DefaultFontName
		{
			get
			{
				if (base.ViewState["_dfnDocx"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_dfnDocx"];
			}
			set
			{
				base.ViewState["_dfnDocx"] = value;
			}
		}
	}
}
