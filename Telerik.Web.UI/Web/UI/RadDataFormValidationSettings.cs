using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000211 RID: 529
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadDataFormValidationSettings : ObjectWithState
	{
		// Token: 0x06001371 RID: 4977 RVA: 0x000448D5 File Offset: 0x00042AD5
		public RadDataFormValidationSettings(StateBag ownerStateBag, RadDataForm ownerDataForm) : base("lvvs_", ownerStateBag)
		{
			this._ownerDataForm = ownerDataForm;
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x000448EA File Offset: 0x00042AEA
		// (set) Token: 0x06001373 RID: 4979 RVA: 0x00044915 File Offset: 0x00042B15
		[Description("Enable validation")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Behavior")]
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

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001374 RID: 4980 RVA: 0x0004492D File Offset: 0x00042B2D
		// (set) Token: 0x06001375 RID: 4981 RVA: 0x00044958 File Offset: 0x00042B58
		[DefaultValue(true)]
		[Description("Enable model validation")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
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

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001376 RID: 4982 RVA: 0x00044970 File Offset: 0x00042B70
		// (set) Token: 0x06001377 RID: 4983 RVA: 0x0004499F File Offset: 0x00042B9F
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Validation group")]
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

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x000449B4 File Offset: 0x00042BB4
		// (set) Token: 0x06001379 RID: 4985 RVA: 0x000449FE File Offset: 0x00042BFE
		[DefaultValue(null)]
		[Description("Comma delimited list of command names")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Category("Behavior")]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(RadDataFormStringArrayConverter))]
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

		// Token: 0x0600137A RID: 4986 RVA: 0x00044A2C File Offset: 0x00042C2C
		internal bool ValidateCommand(string commandName)
		{
			if (this.EnableValidation)
			{
				StringCollection stringCollection = new StringCollection();
				stringCollection.AddRange(this.CommandsToValidate);
				if (stringCollection.Contains(commandName))
				{
					Page page = this._ownerDataForm.Page;
					if (page != null)
					{
						page.Validate(this.ValidationGroup);
						return page.IsValid;
					}
				}
			}
			return true;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00044A80 File Offset: 0x00042C80
		protected virtual bool ShouldSerializeCommandsToValidate()
		{
			StringCollection stringCollection = new StringCollection();
			stringCollection.AddRange(this.CommandsToValidate);
			return stringCollection.Count != 2 || !stringCollection.Contains("PerformInsert") || !stringCollection.Contains("Update");
		}

		// Token: 0x0400057A RID: 1402
		private readonly RadDataForm _ownerDataForm;
	}
}
