using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A2F RID: 6703
	public class ContextMenuSettings : ObjectWithState
	{
		// Token: 0x17004ED3 RID: 20179
		// (get) Token: 0x06010450 RID: 66640 RVA: 0x003A2C32 File Offset: 0x003A0E32
		// (set) Token: 0x06010451 RID: 66641 RVA: 0x003A2C3A File Offset: 0x003A0E3A
		private RadScheduler Owner { get; set; }

		// Token: 0x06010452 RID: 66642 RVA: 0x003A2C43 File Offset: 0x003A0E43
		internal ContextMenuSettings(RadScheduler owner, string keyPrefix, StateBag ownerViewState) : base(keyPrefix, ownerViewState)
		{
			this.Owner = owner;
		}

		// Token: 0x17004ED4 RID: 20180
		// (get) Token: 0x06010453 RID: 66643 RVA: 0x003A2C54 File Offset: 0x003A0E54
		// (set) Token: 0x06010454 RID: 66644 RVA: 0x003A2C75 File Offset: 0x003A0E75
		[Description("A value indicating whether to use the integrated context menu.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool EnableDefault
		{
			get
			{
				return (bool)(base.ViewState["EnableDefault"] ?? false);
			}
			set
			{
				base.ViewState["EnableDefault"] = value;
			}
		}

		// Token: 0x17004ED5 RID: 20181
		// (get) Token: 0x06010455 RID: 66645 RVA: 0x003A2C8D File Offset: 0x003A0E8D
		// (set) Token: 0x06010456 RID: 66646 RVA: 0x003A2CAD File Offset: 0x003A0EAD
		[NotifyParentProperty(true)]
		[Description("Specifies the skin that will be used by the context menu")]
		[Category("Appearance")]
		[DefaultValue("Default")]
		public string Skin
		{
			get
			{
				return (string)(base.ViewState["Skin"] ?? "Default");
			}
			set
			{
				base.ViewState["Skin"] = value;
			}
		}

		// Token: 0x17004ED6 RID: 20182
		// (get) Token: 0x06010457 RID: 66647 RVA: 0x003A2CC0 File Offset: 0x003A0EC0
		// (set) Token: 0x06010458 RID: 66648 RVA: 0x003A2CE1 File Offset: 0x003A0EE1
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("Whether to register the scripts automatically")]
		[Category("Appearance")]
		public virtual bool EnableEmbeddedScripts
		{
			get
			{
				return (bool)(base.ViewState["EnableEmbeddedScripts"] ?? true);
			}
			set
			{
				base.ViewState["EnableEmbeddedScripts"] = value;
			}
		}

		// Token: 0x17004ED7 RID: 20183
		// (get) Token: 0x06010459 RID: 66649 RVA: 0x003A2CF9 File Offset: 0x003A0EF9
		// (set) Token: 0x0601045A RID: 66650 RVA: 0x003A2D1A File Offset: 0x003A0F1A
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("Whether to register the selected skin automatically")]
		[Category("Appearance")]
		public virtual bool EnableEmbeddedSkins
		{
			get
			{
				return (bool)(base.ViewState["EnableEmbeddedSkins"] ?? true);
			}
			set
			{
				base.ViewState["EnableEmbeddedSkins"] = value;
			}
		}

		// Token: 0x17004ED8 RID: 20184
		// (get) Token: 0x0601045B RID: 66651 RVA: 0x003A2D32 File Offset: 0x003A0F32
		// (set) Token: 0x0601045C RID: 66652 RVA: 0x003A2D53 File Offset: 0x003A0F53
		[DefaultValue(true)]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Whether to register the base control skin file automatically")]
		public virtual bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return (bool)(base.ViewState["EnableEmbeddedBaseStylesheet"] ?? true);
			}
			set
			{
				base.ViewState["EnableEmbeddedBaseStylesheet"] = value;
			}
		}

		// Token: 0x17004ED9 RID: 20185
		// (get) Token: 0x0601045D RID: 66653 RVA: 0x003A2D6B File Offset: 0x003A0F6B
		internal string SkinResolved
		{
			get
			{
				if (base.ViewState["Skin"] == null)
				{
					return this.Owner.RuntimeSkin;
				}
				return this.Skin;
			}
		}

		// Token: 0x17004EDA RID: 20186
		// (get) Token: 0x0601045E RID: 66654 RVA: 0x003A2D91 File Offset: 0x003A0F91
		internal bool EnableEmbeddedScriptsResolved
		{
			get
			{
				if (base.ViewState["EnableEmbeddedScripts"] == null)
				{
					return this.Owner.EnableEmbeddedScripts;
				}
				return this.EnableEmbeddedScripts;
			}
		}

		// Token: 0x17004EDB RID: 20187
		// (get) Token: 0x0601045F RID: 66655 RVA: 0x003A2DB7 File Offset: 0x003A0FB7
		internal bool EnableEmbeddedSkinsResolved
		{
			get
			{
				if (base.ViewState["EnableEmbeddedSkins"] == null)
				{
					return this.Owner.EnableEmbeddedSkins;
				}
				return this.EnableEmbeddedSkins;
			}
		}

		// Token: 0x17004EDC RID: 20188
		// (get) Token: 0x06010460 RID: 66656 RVA: 0x003A2DDD File Offset: 0x003A0FDD
		internal bool EnableEmbeddedBaseStylesheetResolved
		{
			get
			{
				if (base.ViewState["EnableEmbeddedBaseStylesheet"] == null)
				{
					return this.Owner.EnableEmbeddedBaseStylesheet;
				}
				return this.EnableEmbeddedBaseStylesheet;
			}
		}
	}
}
