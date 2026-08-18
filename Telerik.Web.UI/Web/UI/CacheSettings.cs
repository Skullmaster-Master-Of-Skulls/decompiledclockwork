using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000E7B RID: 3707
	public class CacheSettings : ObjectWithState
	{
		// Token: 0x06008C94 RID: 35988 RVA: 0x001FE8DF File Offset: 0x001FCADF
		internal CacheSettings(string keyPrefix, StateBag ownerViewState) : base(keyPrefix, ownerViewState)
		{
		}

		// Token: 0x17002C66 RID: 11366
		// (get) Token: 0x06008C95 RID: 35989 RVA: 0x001FE8E9 File Offset: 0x001FCAE9
		// (set) Token: 0x06008C96 RID: 35990 RVA: 0x001FE90A File Offset: 0x001FCB0A
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Enables web resource cache. Cache is disabled by default")]
		[DefaultValue(false)]
		public bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? false);
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x17002C67 RID: 11367
		// (get) Token: 0x06008C97 RID: 35991 RVA: 0x001FE922 File Offset: 0x001FCB22
		// (set) Token: 0x06008C98 RID: 35992 RVA: 0x001FE942 File Offset: 0x001FCB42
		[Description("The unique key of the page. Combined web resources are associated with a page key.")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		public string PageKey
		{
			get
			{
				return (string)(base.ViewState["PageKey"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PageKey"] = value;
			}
		}
	}
}
