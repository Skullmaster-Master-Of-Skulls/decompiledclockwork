using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200116C RID: 4460
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridSelfHierarchySettings : ObjectWithState
	{
		// Token: 0x0600B5D6 RID: 46550 RVA: 0x00280763 File Offset: 0x0027E963
		public GridSelfHierarchySettings(StateBag OwnerStateBag, GridTableView owner) : base("shs_", OwnerStateBag)
		{
			this.owner = owner;
		}

		// Token: 0x0600B5D7 RID: 46551 RVA: 0x00280778 File Offset: 0x0027E978
		public bool IsSet()
		{
			return !new GridDefaultValueChecker(this).IsDefault;
		}

		// Token: 0x17003AD2 RID: 15058
		// (get) Token: 0x0600B5D8 RID: 46552 RVA: 0x00280788 File Offset: 0x0027E988
		// (set) Token: 0x0600B5D9 RID: 46553 RVA: 0x002807B5 File Offset: 0x0027E9B5
		[DefaultValue("")]
		[Description("")]
		[NotifyParentProperty(true)]
		[Category("SelfHierarchy")]
		public virtual string ParentKeyName
		{
			get
			{
				object obj = base.ViewState["ParentKeyName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["ParentKeyName"] = value;
			}
		}

		// Token: 0x17003AD3 RID: 15059
		// (get) Token: 0x0600B5DA RID: 46554 RVA: 0x002807C8 File Offset: 0x0027E9C8
		// (set) Token: 0x0600B5DB RID: 46555 RVA: 0x002807F5 File Offset: 0x0027E9F5
		[Description("")]
		[Category("SelfHierarchy")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string KeyName
		{
			get
			{
				object obj = base.ViewState["KeyName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["KeyName"] = value;
			}
		}

		// Token: 0x17003AD4 RID: 15060
		// (get) Token: 0x0600B5DC RID: 46556 RVA: 0x00280808 File Offset: 0x0027EA08
		// (set) Token: 0x0600B5DD RID: 46557 RVA: 0x00280832 File Offset: 0x0027EA32
		[NotifyParentProperty(true)]
		[Category("SelfHierarchy")]
		[Description("")]
		[DefaultValue(10)]
		public virtual int MaximumDepth
		{
			get
			{
				object obj = base.ViewState["MaximumDepth"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				base.ViewState["MaximumDepth"] = value;
			}
		}

		// Token: 0x04002FF0 RID: 12272
		private readonly GridTableView owner;
	}
}
