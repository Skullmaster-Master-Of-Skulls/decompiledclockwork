using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Editor.Import
{
	// Token: 0x020002B7 RID: 695
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ImportRtfSettings : ObjectWithState, IDplImportSettings
	{
		// Token: 0x06001858 RID: 6232 RVA: 0x000504EA File Offset: 0x0004E6EA
		public ImportRtfSettings(StateBag OwnerStateBag) : base("irtfs_", OwnerStateBag)
		{
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001859 RID: 6233 RVA: 0x000504F8 File Offset: 0x0004E6F8
		// (set) Token: 0x0600185A RID: 6234 RVA: 0x00050523 File Offset: 0x0004E723
		[Description("")]
		[DefaultValue(DocumentLevel.Fragment)]
		[NotifyParentProperty(true)]
		public DocumentLevel DocumentLevel
		{
			get
			{
				if (base.ViewState["_irtfdel"] == null)
				{
					return DocumentLevel.Fragment;
				}
				return (DocumentLevel)base.ViewState["_irtfdel"];
			}
			set
			{
				base.ViewState["_irtfdel"] = value;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600185B RID: 6235 RVA: 0x0005053B File Offset: 0x0004E73B
		// (set) Token: 0x0600185C RID: 6236 RVA: 0x00050566 File Offset: 0x0004E766
		[DefaultValue(StylesMode.Embedded)]
		[Description("")]
		[NotifyParentProperty(true)]
		public StylesMode StylesMode
		{
			get
			{
				if (base.ViewState["_irtfsem"] == null)
				{
					return StylesMode.Embedded;
				}
				return (StylesMode)base.ViewState["_irtfsem"];
			}
			set
			{
				base.ViewState["_irtfsem"] = value;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x0005057E File Offset: 0x0004E77E
		// (set) Token: 0x0600185E RID: 6238 RVA: 0x000505AD File Offset: 0x0004E7AD
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue("")]
		public string StylesFilePath
		{
			get
			{
				if (base.ViewState["_irtfsfp"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_irtfsfp"];
			}
			set
			{
				base.ViewState["_irtfsfp"] = value;
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x000505C0 File Offset: 0x0004E7C0
		// (set) Token: 0x06001860 RID: 6240 RVA: 0x000505EF File Offset: 0x0004E7EF
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue("")]
		public string StylesSourcePath
		{
			get
			{
				if (base.ViewState["_irtfssp"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_irtfssp"];
			}
			set
			{
				base.ViewState["_irtfssp"] = value;
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x00050602 File Offset: 0x0004E802
		// (set) Token: 0x06001862 RID: 6242 RVA: 0x0005062D File Offset: 0x0004E82D
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue(ImagesMode.Embedded)]
		public ImagesMode ImagesMode
		{
			get
			{
				if (base.ViewState["_irtfiem"] == null)
				{
					return ImagesMode.Embedded;
				}
				return (ImagesMode)base.ViewState["_irtfiem"];
			}
			set
			{
				base.ViewState["_irtfiem"] = value;
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x00050645 File Offset: 0x0004E845
		// (set) Token: 0x06001864 RID: 6244 RVA: 0x00050674 File Offset: 0x0004E874
		[DefaultValue("")]
		[Description("")]
		[NotifyParentProperty(true)]
		public string ImagesFolderPath
		{
			get
			{
				if (base.ViewState["_irtfifp"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_irtfifp"];
			}
			set
			{
				base.ViewState["_irtfifp"] = value;
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001865 RID: 6245 RVA: 0x00050687 File Offset: 0x0004E887
		// (set) Token: 0x06001866 RID: 6246 RVA: 0x000506B6 File Offset: 0x0004E8B6
		[DefaultValue("")]
		[Description("")]
		[NotifyParentProperty(true)]
		public string ImagesSourceBasePath
		{
			get
			{
				if (base.ViewState["_irtfisp"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_irtfisp"];
			}
			set
			{
				base.ViewState["_irtfisp"] = value;
			}
		}
	}
}
