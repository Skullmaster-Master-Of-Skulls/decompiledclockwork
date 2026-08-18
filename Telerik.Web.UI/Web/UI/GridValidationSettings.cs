using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200119D RID: 4509
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridValidationSettings : ObjectWithState
	{
		// Token: 0x0600B92B RID: 47403 RVA: 0x0028F63A File Offset: 0x0028D83A
		public GridValidationSettings(StateBag OwnerStateBag, RadGrid owner) : base("gvls_", OwnerStateBag)
		{
			this.owner = owner;
		}

		// Token: 0x17003BD7 RID: 15319
		// (get) Token: 0x0600B92C RID: 47404 RVA: 0x0028F64F File Offset: 0x0028D84F
		// (set) Token: 0x0600B92D RID: 47405 RVA: 0x0028F67A File Offset: 0x0028D87A
		[DefaultValue(true)]
		[Description("Enable validation")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public bool EnableValidation
		{
			get
			{
				return base.ViewState["_ev"] == null || (bool)base.ViewState["_ev"];
			}
			set
			{
				base.ViewState["_ev"] = value;
			}
		}

		// Token: 0x17003BD8 RID: 15320
		// (get) Token: 0x0600B92E RID: 47406 RVA: 0x0028F692 File Offset: 0x0028D892
		// (set) Token: 0x0600B92F RID: 47407 RVA: 0x0028F6B3 File Offset: 0x0028D8B3
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Enable model validation")]
		public bool EnableModelValidation
		{
			get
			{
				return (bool)(base.ViewState["EnableModelValidation"] ?? true);
			}
			set
			{
				base.ViewState["EnableModelValidation"] = value;
			}
		}

		// Token: 0x17003BD9 RID: 15321
		// (get) Token: 0x0600B930 RID: 47408 RVA: 0x0028F6CB File Offset: 0x0028D8CB
		// (set) Token: 0x0600B931 RID: 47409 RVA: 0x0028F6FA File Offset: 0x0028D8FA
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

		// Token: 0x17003BDA RID: 15322
		// (get) Token: 0x0600B932 RID: 47410 RVA: 0x0028F710 File Offset: 0x0028D910
		// (set) Token: 0x0600B933 RID: 47411 RVA: 0x0028F75A File Offset: 0x0028D95A
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[NotifyParentProperty(true)]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("Comma delimited list of command names")]
		public virtual string[] CommandsToValidate
		{
			get
			{
				object obj = base.ViewState["_ctv"];
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
					base.ViewState["_ctv"] = (string[])value.Clone();
					return;
				}
				base.ViewState["_ctv"] = null;
			}
		}

		// Token: 0x0600B934 RID: 47412 RVA: 0x0028F78C File Offset: 0x0028D98C
		internal bool ValidateCommandName(string commandName)
		{
			if (this.EnableValidation)
			{
				StringCollection stringCollection = new StringCollection();
				stringCollection.AddRange(this.CommandsToValidate);
				if (stringCollection.Contains(commandName) && this.owner.Page != null)
				{
					this.owner.Page.Validate(this.ValidationGroup);
					return this.owner.Page.IsValid;
				}
			}
			return true;
		}

		// Token: 0x0600B935 RID: 47413 RVA: 0x0028F7F4 File Offset: 0x0028D9F4
		protected virtual bool ShouldSerializeCommandsToValidate()
		{
			StringCollection stringCollection = new StringCollection();
			stringCollection.AddRange(this.CommandsToValidate);
			return stringCollection.Count != 2 || !stringCollection.Contains("PerformInsert") || !stringCollection.Contains("Update");
		}

		// Token: 0x040030F4 RID: 12532
		private RadGrid owner;
	}
}
