using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200190F RID: 6415
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class InputSettingValidation
	{
		// Token: 0x0600F8F1 RID: 63729 RVA: 0x003833A9 File Offset: 0x003815A9
		internal static string ToLower(Match m)
		{
			return m.ToString().ToLower();
		}

		// Token: 0x0600F8F2 RID: 63730 RVA: 0x003833B6 File Offset: 0x003815B6
		public InputSettingValidation(StateBag viewStateOwner)
		{
			this._viewStateOwner = new InputStateBag("inputM_validation_", viewStateOwner);
		}

		// Token: 0x17004B38 RID: 19256
		// (get) Token: 0x0600F8F3 RID: 63731 RVA: 0x003833CF File Offset: 0x003815CF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public InputStateBag ViewState
		{
			get
			{
				return this._viewStateOwner;
			}
		}

		// Token: 0x0600F8F4 RID: 63732 RVA: 0x003833D7 File Offset: 0x003815D7
		public override string ToString()
		{
			return "";
		}

		// Token: 0x0600F8F5 RID: 63733 RVA: 0x003833E0 File Offset: 0x003815E0
		internal void Describe(IScriptDescriptor descriptor)
		{
			if (this.IsRequired)
			{
				descriptor.AddProperty("isRequired", this.IsRequired);
			}
			if (!string.IsNullOrEmpty(this.ValidationGroup))
			{
				descriptor.AddProperty("validationGroup", this.ValidationGroup);
			}
			if (this.ValidateOnEvent != InputSettingValidateOnEvent.All)
			{
				descriptor.AddProperty("validateOnEvent", this.ValidateOnEvent);
			}
			if (!string.IsNullOrEmpty(this.Location))
			{
				descriptor.AddProperty("location", HttpContext.Current.Response.ApplyAppPathModifier(this.Location));
			}
			if (!string.IsNullOrEmpty(this.Method))
			{
				descriptor.AddProperty("method", this.Method);
			}
		}

		// Token: 0x17004B39 RID: 19257
		// (get) Token: 0x0600F8F6 RID: 63734 RVA: 0x00383493 File Offset: 0x00381693
		// (set) Token: 0x0600F8F7 RID: 63735 RVA: 0x003834BE File Offset: 0x003816BE
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating the control should be required on client or not")]
		[DefaultValue(false)]
		public virtual bool IsRequired
		{
			get
			{
				return this.ViewState["IsRequired"] != null && (bool)this.ViewState["IsRequired"];
			}
			set
			{
				this.ViewState["IsRequired"] = value;
				if (this.AssignedValidator != null)
				{
					this.AssignedValidator.ValidateEmptyText = value;
				}
			}
		}

		// Token: 0x17004B3A RID: 19258
		// (get) Token: 0x0600F8F8 RID: 63736 RVA: 0x003834EA File Offset: 0x003816EA
		// (set) Token: 0x0600F8F9 RID: 63737 RVA: 0x00383519 File Offset: 0x00381719
		[Category("Behavior")]
		[Description("Gets or sets the name of the validation group to wich this setting belongs.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string ValidationGroup
		{
			get
			{
				if (this.ViewState["ValidationGroup"] == null)
				{
					return string.Empty;
				}
				return (string)this.ViewState["ValidationGroup"];
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
				if (this.AssignedValidator != null)
				{
					this.AssignedValidator.ValidationGroup = value;
				}
			}
		}

		// Token: 0x17004B3B RID: 19259
		// (get) Token: 0x0600F8FA RID: 63738 RVA: 0x00383540 File Offset: 0x00381740
		// (set) Token: 0x0600F8FB RID: 63739 RVA: 0x00383569 File Offset: 0x00381769
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[Description("ValidateOnEvent")]
		[DefaultValue(typeof(InputSettingValidateOnEvent), "All")]
		public virtual InputSettingValidateOnEvent ValidateOnEvent
		{
			get
			{
				object obj = this.ViewState["ValidateOnEvent"];
				if (obj != null)
				{
					return (InputSettingValidateOnEvent)obj;
				}
				return InputSettingValidateOnEvent.All;
			}
			set
			{
				this.ViewState["ValidateOnEvent"] = value;
			}
		}

		// Token: 0x17004B3C RID: 19260
		// (get) Token: 0x0600F8FC RID: 63740 RVA: 0x00383581 File Offset: 0x00381781
		// (set) Token: 0x0600F8FD RID: 63741 RVA: 0x003835A1 File Offset: 0x003817A1
		[Description("Gets or sets url for the WebService or Page which will be requested to validate data.")]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("")]
		[UrlProperty]
		public virtual string Location
		{
			get
			{
				return ((string)this.ViewState["Location"]) ?? "";
			}
			set
			{
				this.ViewState["Location"] = value;
			}
		}

		// Token: 0x17004B3D RID: 19261
		// (get) Token: 0x0600F8FE RID: 63742 RVA: 0x003835B4 File Offset: 0x003817B4
		// (set) Token: 0x0600F8FF RID: 63743 RVA: 0x003835D4 File Offset: 0x003817D4
		[Description("Gets or sets method name in the WebService or Page which will be requested to validate data.")]
		[NotifyParentProperty(true)]
		[Category("Validation")]
		[DefaultValue("")]
		public virtual string Method
		{
			get
			{
				return ((string)this.ViewState["Method"]) ?? "";
			}
			set
			{
				this.ViewState["Method"] = value;
			}
		}

		// Token: 0x040046D3 RID: 18131
		private InputStateBag _viewStateOwner;

		// Token: 0x040046D4 RID: 18132
		internal InputSettingCustomValidator AssignedValidator;
	}
}
