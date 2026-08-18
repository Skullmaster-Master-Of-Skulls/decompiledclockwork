using System;
using System.CodeDom.Compiler;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200051E RID: 1310
	[Bindable(false)]
	[ControlBuilder(typeof(WizardStepControlBuilder))]
	[ToolboxItem(false)]
	public abstract class WizardStepBase : View
	{
		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06004247 RID: 16967 RVA: 0x000D8710 File Offset: 0x000D6910
		// (set) Token: 0x06004248 RID: 16968 RVA: 0x000D8739 File Offset: 0x000D6939
		[WebCategory("Behavior")]
		[Themeable(false)]
		[Filterable(false)]
		[DefaultValue(true)]
		[WebSysDescription("WizardStep_AllowReturn")]
		public virtual bool AllowReturn
		{
			get
			{
				object obj = this.ViewState["AllowReturn"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["AllowReturn"] = value;
			}
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x000D8751 File Offset: 0x000D6951
		// (set) Token: 0x0600424A RID: 16970 RVA: 0x000D8759 File Offset: 0x000D6959
		[Browsable(true)]
		public override bool EnableTheming
		{
			get
			{
				return base.EnableTheming;
			}
			set
			{
				base.EnableTheming = value;
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x00069884 File Offset: 0x00067A84
		// (set) Token: 0x0600424C RID: 16972 RVA: 0x000D8764 File Offset: 0x000D6964
		public override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				if (this.Owner != null && this.Owner.DesignMode)
				{
					if (!CodeGenerator.IsValidLanguageIndependentIdentifier(value))
					{
						throw new ArgumentException(SR.GetString("Invalid_identifier", new object[]
						{
							value
						}));
					}
					if (value != null && value.Equals(this.Owner.ID, StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(SR.GetString("Id_already_used", new object[]
						{
							value
						}));
					}
					foreach (object obj in this.Owner.WizardSteps)
					{
						WizardStepBase wizardStepBase = (WizardStepBase)obj;
						if (wizardStepBase != this && wizardStepBase.ID != null && wizardStepBase.ID.Equals(value, StringComparison.OrdinalIgnoreCase))
						{
							throw new ArgumentException(SR.GetString("Id_already_used", new object[]
							{
								value
							}));
						}
					}
				}
				base.ID = value;
			}
		}

		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x000D8868 File Offset: 0x000D6A68
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Appearance")]
		[WebSysDescription("WizardStep_Name")]
		public virtual string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(this.Title))
				{
					return this.Title;
				}
				if (!string.IsNullOrEmpty(this.ID))
				{
					return this.ID;
				}
				return null;
			}
		}

		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x0600424E RID: 16974 RVA: 0x000D8893 File Offset: 0x000D6A93
		// (set) Token: 0x0600424F RID: 16975 RVA: 0x000D889B File Offset: 0x000D6A9B
		internal virtual Wizard Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x000D88A4 File Offset: 0x000D6AA4
		// (set) Token: 0x06004251 RID: 16977 RVA: 0x000D88D0 File Offset: 0x000D6AD0
		[WebCategory("Behavior")]
		[DefaultValue(WizardStepType.Auto)]
		[WebSysDescription("WizardStep_StepType")]
		public virtual WizardStepType StepType
		{
			get
			{
				object obj = this.ViewState["StepType"];
				if (obj != null)
				{
					return (WizardStepType)obj;
				}
				return WizardStepType.Auto;
			}
			set
			{
				if (value < WizardStepType.Auto || value > WizardStepType.Step)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (this.StepType != value)
				{
					this.ViewState["StepType"] = value;
					if (this.Owner != null)
					{
						this.Owner.OnWizardStepsChanged();
					}
				}
			}
		}

		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06004252 RID: 16978 RVA: 0x000D8924 File Offset: 0x000D6B24
		// (set) Token: 0x06004253 RID: 16979 RVA: 0x000D8951 File Offset: 0x000D6B51
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		[WebSysDescription("WizardStep_Title")]
		public virtual string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (this.Title != value)
				{
					this.ViewState["Title"] = value;
					if (this.Owner != null)
					{
						this.Owner.OnWizardStepsChanged();
					}
				}
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06004254 RID: 16980 RVA: 0x000D8985 File Offset: 0x000D6B85
		internal string TitleInternal
		{
			get
			{
				return (string)this.ViewState["Title"];
			}
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06004255 RID: 16981 RVA: 0x000D899C File Offset: 0x000D6B9C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[WebCategory("Appearance")]
		public Wizard Wizard
		{
			get
			{
				return this.Owner;
			}
		}

		// Token: 0x06004256 RID: 16982 RVA: 0x000D89A4 File Offset: 0x000D6BA4
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				base.LoadViewState(savedState);
				if (this.Owner != null && (this.ViewState["Title"] != null || this.ViewState["StepType"] != null))
				{
					this.Owner.OnWizardStepsChanged();
				}
			}
		}

		// Token: 0x06004257 RID: 16983 RVA: 0x000D89F2 File Offset: 0x000D6BF2
		protected internal override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (this.Owner == null && !base.DesignMode)
			{
				throw new InvalidOperationException(SR.GetString("WizardStep_WrongContainment"));
			}
		}

		// Token: 0x06004258 RID: 16984 RVA: 0x000D8A1B File Offset: 0x000D6C1B
		protected internal override void RenderChildren(HtmlTextWriter writer)
		{
			if (!this.Owner.ShouldRenderChildControl)
			{
				return;
			}
			base.RenderChildren(writer);
		}

		// Token: 0x04002560 RID: 9568
		private Wizard _owner;
	}
}
