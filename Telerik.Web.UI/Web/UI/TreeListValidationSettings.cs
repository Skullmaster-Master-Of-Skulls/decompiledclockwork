using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001299 RID: 4761
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class TreeListValidationSettings : StateManager
	{
		// Token: 0x0600C65B RID: 50779 RVA: 0x002C3F37 File Offset: 0x002C2137
		public TreeListValidationSettings(RadTreeList owner)
		{
			this.Owner = owner;
		}

		// Token: 0x1700400E RID: 16398
		// (get) Token: 0x0600C65C RID: 50780 RVA: 0x002C3F46 File Offset: 0x002C2146
		// (set) Token: 0x0600C65D RID: 50781 RVA: 0x002C3F4E File Offset: 0x002C214E
		private protected RadTreeList Owner { protected get; private set; }

		// Token: 0x1700400F RID: 16399
		// (get) Token: 0x0600C65E RID: 50782 RVA: 0x002C3F57 File Offset: 0x002C2157
		// (set) Token: 0x0600C65F RID: 50783 RVA: 0x002C3F82 File Offset: 0x002C2182
		[Description("Enable validation")]
		[DefaultValue(true)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public bool EnableValidation
		{
			get
			{
				return base.ViewState["EnableValidation"] == null || (bool)base.ViewState["EnableValidation"];
			}
			set
			{
				base.ViewState["EnableValidation"] = value;
			}
		}

		// Token: 0x17004010 RID: 16400
		// (get) Token: 0x0600C660 RID: 50784 RVA: 0x002C3F9A File Offset: 0x002C219A
		// (set) Token: 0x0600C661 RID: 50785 RVA: 0x002C3FC9 File Offset: 0x002C21C9
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Validation group")]
		[NotifyParentProperty(true)]
		public string ValidationGroup
		{
			get
			{
				if (base.ViewState["_vg"] == null)
				{
					return "";
				}
				return (string)base.ViewState["_vg"];
			}
			set
			{
				base.ViewState["_vg"] = value;
			}
		}

		// Token: 0x17004011 RID: 16401
		// (get) Token: 0x0600C662 RID: 50786 RVA: 0x002C3FDC File Offset: 0x002C21DC
		// (set) Token: 0x0600C663 RID: 50787 RVA: 0x002C4026 File Offset: 0x002C2226
		[TypeConverter(typeof(GridStringArrayConverter))]
		[Category("Behavior")]
		[DefaultValue("PerformInsert,Update")]
		[Description("Comma delimited list of command names")]
		[NotifyParentProperty(true)]
		public virtual string[] CommandsToValidate
		{
			get
			{
				object obj = base.ViewState["CommandsToValidate"];
				if (obj != null)
				{
					return (string[])((string[])obj).Clone();
				}
				return new string[]
				{
					"PerformInsert",
					"Update"
				};
			}
			set
			{
				if (value != null)
				{
					base.ViewState["CommandsToValidate"] = (string[])value.Clone();
					return;
				}
				base.ViewState["CommandsToValidate"] = null;
			}
		}

		// Token: 0x0600C664 RID: 50788 RVA: 0x002C4058 File Offset: 0x002C2258
		internal bool ValidateCommandName(string commandName)
		{
			if (this.EnableValidation)
			{
				StringCollection stringCollection = new StringCollection();
				stringCollection.AddRange(this.CommandsToValidate);
				if (stringCollection.Contains(commandName) && this.Owner.Page != null)
				{
					this.Owner.Page.Validate(this.ValidationGroup);
					return this.Owner.Page.IsValid;
				}
			}
			return true;
		}
	}
}
