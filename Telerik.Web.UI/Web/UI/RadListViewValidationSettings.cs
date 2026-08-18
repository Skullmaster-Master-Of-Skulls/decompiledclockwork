using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020019C9 RID: 6601
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadListViewValidationSettings : ObjectWithState
	{
		// Token: 0x0600FEF2 RID: 65266 RVA: 0x00393A1E File Offset: 0x00391C1E
		public RadListViewValidationSettings(StateBag ownerStateBag, RadListView ownerListView) : base("lvvs_", ownerStateBag)
		{
			this._ownerListView = ownerListView;
		}

		// Token: 0x17004CED RID: 19693
		// (get) Token: 0x0600FEF3 RID: 65267 RVA: 0x00393A33 File Offset: 0x00391C33
		// (set) Token: 0x0600FEF4 RID: 65268 RVA: 0x00393A5E File Offset: 0x00391C5E
		[DefaultValue(true)]
		[Description("Enable validation")]
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

		// Token: 0x17004CEE RID: 19694
		// (get) Token: 0x0600FEF5 RID: 65269 RVA: 0x00393A76 File Offset: 0x00391C76
		// (set) Token: 0x0600FEF6 RID: 65270 RVA: 0x00393AA1 File Offset: 0x00391CA1
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Enable model validation")]
		[DefaultValue(true)]
		public bool EnableModelValidation
		{
			get
			{
				return base.ViewState["EnableModelValidation"] == null || (bool)base.ViewState["EnableModelValidation"];
			}
			set
			{
				base.ViewState["EnableModelValidation"] = value;
			}
		}

		// Token: 0x17004CEF RID: 19695
		// (get) Token: 0x0600FEF7 RID: 65271 RVA: 0x00393AB9 File Offset: 0x00391CB9
		// (set) Token: 0x0600FEF8 RID: 65272 RVA: 0x00393AE8 File Offset: 0x00391CE8
		[DefaultValue("")]
		[Description("Validation group")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public string ValidationGroup
		{
			get
			{
				if (base.ViewState["ValidationGroup"] == null)
				{
					return string.Empty;
				}
				return (string)base.ViewState["ValidationGroup"];
			}
			set
			{
				base.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x17004CF0 RID: 19696
		// (get) Token: 0x0600FEF9 RID: 65273 RVA: 0x00393AFC File Offset: 0x00391CFC
		// (set) Token: 0x0600FEFA RID: 65274 RVA: 0x00393B46 File Offset: 0x00391D46
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Category("Behavior")]
		[Description("Comma delimited list of command names")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(RadListViewStringArrayConverter))]
		[DefaultValue(null)]
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
					base.ViewState["CommandsToValidate"] = value.Clone();
					return;
				}
				base.ViewState["CommandsToValidate"] = null;
			}
		}

		// Token: 0x0600FEFB RID: 65275 RVA: 0x00393B74 File Offset: 0x00391D74
		internal bool ValidateCommand(string commandName)
		{
			if (this.EnableValidation)
			{
				StringCollection stringCollection = new StringCollection();
				stringCollection.AddRange(this.CommandsToValidate);
				if (stringCollection.Contains(commandName))
				{
					Page page = this._ownerListView.Page;
					if (page != null)
					{
						page.Validate(this.ValidationGroup);
						return page.IsValid;
					}
				}
			}
			return true;
		}

		// Token: 0x0600FEFC RID: 65276 RVA: 0x00393BC8 File Offset: 0x00391DC8
		protected virtual bool ShouldSerializeCommandsToValidate()
		{
			StringCollection stringCollection = new StringCollection();
			stringCollection.AddRange(this.CommandsToValidate);
			return stringCollection.Count != 2 || !stringCollection.Contains("PerformInsert") || !stringCollection.Contains("Update");
		}

		// Token: 0x0400484F RID: 18511
		private readonly RadListView _ownerListView;
	}
}
