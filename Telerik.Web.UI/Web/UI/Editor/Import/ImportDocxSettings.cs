using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Editor.Import
{
	// Token: 0x020002A1 RID: 673
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ImportDocxSettings : ObjectWithState, IDplImportSettings
	{
		// Token: 0x060017CF RID: 6095 RVA: 0x0004F47A File Offset: 0x0004D67A
		public ImportDocxSettings(StateBag OwnerStateBag) : base("idocxs_", OwnerStateBag)
		{
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060017D0 RID: 6096 RVA: 0x0004F488 File Offset: 0x0004D688
		// (set) Token: 0x060017D1 RID: 6097 RVA: 0x0004F4B3 File Offset: 0x0004D6B3
		[NotifyParentProperty(true)]
		[DefaultValue(DocumentLevel.Fragment)]
		[Description("")]
		public DocumentLevel DocumentLevel
		{
			get
			{
				if (base.ViewState["_idocxdel"] == null)
				{
					return DocumentLevel.Fragment;
				}
				return (DocumentLevel)base.ViewState["_idocxdel"];
			}
			set
			{
				base.ViewState["_idocxdel"] = value;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x060017D2 RID: 6098 RVA: 0x0004F4CB File Offset: 0x0004D6CB
		// (set) Token: 0x060017D3 RID: 6099 RVA: 0x0004F4F6 File Offset: 0x0004D6F6
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue(StylesMode.Embedded)]
		public StylesMode StylesMode
		{
			get
			{
				if (base.ViewState["_idocxsem"] == null)
				{
					return StylesMode.Embedded;
				}
				return (StylesMode)base.ViewState["_idocxsem"];
			}
			set
			{
				base.ViewState["_idocxsem"] = value;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x060017D4 RID: 6100 RVA: 0x0004F50E File Offset: 0x0004D70E
		// (set) Token: 0x060017D5 RID: 6101 RVA: 0x0004F53D File Offset: 0x0004D73D
		[Description("")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public string StylesFilePath
		{
			get
			{
				if (base.ViewState["_idocxsfp"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_idocxsfp"];
			}
			set
			{
				base.ViewState["_idocxsfp"] = value;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x060017D6 RID: 6102 RVA: 0x0004F550 File Offset: 0x0004D750
		// (set) Token: 0x060017D7 RID: 6103 RVA: 0x0004F57F File Offset: 0x0004D77F
		[DefaultValue("")]
		[Description("")]
		[NotifyParentProperty(true)]
		public string StylesSourcePath
		{
			get
			{
				if (base.ViewState["_idocxssp"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_idocxssp"];
			}
			set
			{
				base.ViewState["_idocxssp"] = value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x060017D8 RID: 6104 RVA: 0x0004F592 File Offset: 0x0004D792
		// (set) Token: 0x060017D9 RID: 6105 RVA: 0x0004F5BD File Offset: 0x0004D7BD
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue(ImagesMode.Embedded)]
		public ImagesMode ImagesMode
		{
			get
			{
				if (base.ViewState["_idocxiem"] == null)
				{
					return ImagesMode.Embedded;
				}
				return (ImagesMode)base.ViewState["_idocxiem"];
			}
			set
			{
				base.ViewState["_idocxiem"] = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060017DA RID: 6106 RVA: 0x0004F5D5 File Offset: 0x0004D7D5
		// (set) Token: 0x060017DB RID: 6107 RVA: 0x0004F604 File Offset: 0x0004D804
		[Description("")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string ImagesFolderPath
		{
			get
			{
				if (base.ViewState["_idocxifp"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_idocxifp"];
			}
			set
			{
				base.ViewState["_idocxifp"] = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0004F617 File Offset: 0x0004D817
		// (set) Token: 0x060017DD RID: 6109 RVA: 0x0004F646 File Offset: 0x0004D846
		[DefaultValue("")]
		[Description("")]
		[NotifyParentProperty(true)]
		public string ImagesSourceBasePath
		{
			get
			{
				if (base.ViewState["_idocxisp"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_idocxisp"];
			}
			set
			{
				base.ViewState["_idocxisp"] = value;
			}
		}
	}
}
