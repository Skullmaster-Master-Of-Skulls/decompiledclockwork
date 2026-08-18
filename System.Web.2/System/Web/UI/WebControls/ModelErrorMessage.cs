using System;
using System.ComponentModel;
using System.Linq;
using System.Web.ModelBinding;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200036B RID: 875
	[ToolboxData("<{0}:ModelErrorMessage runat=\"server\" Key=\"ModelStateKey\"></{0}:ModelErrorMessage>")]
	[DefaultProperty("Key")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class ModelErrorMessage : Label
	{
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002865 RID: 10341 RVA: 0x00082A70 File Offset: 0x00080C70
		// (set) Token: 0x06002866 RID: 10342 RVA: 0x00082A9D File Offset: 0x00080C9D
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("ModelErrorMessage_ModelStateKey")]
		[DefaultValue("")]
		public string ModelStateKey
		{
			get
			{
				object obj = this.ViewState["ModelStateKey"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ModelStateKey"] = value;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x00082AB0 File Offset: 0x00080CB0
		// (set) Token: 0x06002868 RID: 10344 RVA: 0x00082AB8 File Offset: 0x00080CB8
		[DefaultValue("")]
		[IDReferenceProperty]
		[WebCategory("Behavior")]
		[WebSysDescription("ModelErrorMessage_AssociatedControlID")]
		[Themeable(false)]
		public override string AssociatedControlID
		{
			get
			{
				return base.AssociatedControlID;
			}
			set
			{
				base.AssociatedControlID = value;
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x00082AC4 File Offset: 0x00080CC4
		// (set) Token: 0x0600286A RID: 10346 RVA: 0x00082AED File Offset: 0x00080CED
		[WebCategory("Behavior")]
		[Themeable(false)]
		[DefaultValue(false)]
		[WebSysDescription("ModelErrorMessage_SetFocusOnError")]
		public bool SetFocusOnError
		{
			get
			{
				object obj = this.ViewState["SetFocusOnError"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["SetFocusOnError"] = value;
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x0600286B RID: 10347 RVA: 0x00082B05 File Offset: 0x00080D05
		// (set) Token: 0x0600286C RID: 10348 RVA: 0x00082B0D File Offset: 0x00080D0D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[PersistenceMode(PersistenceMode.Attribute)]
		public override string Text { get; set; }

		// Token: 0x0600286D RID: 10349 RVA: 0x00082B18 File Offset: 0x00080D18
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ModelState modelState;
			if (this.Page != null && this.Page.ModelState.TryGetValue(this.ModelStateKey, out modelState))
			{
				ModelError modelError2 = modelState.Errors.FirstOrDefault((ModelError modelError) => !string.IsNullOrEmpty(modelError.ErrorMessage));
				if (modelError2 != null)
				{
					this.Text = HttpUtility.HtmlEncode(modelError2.ErrorMessage);
					if (this.SetFocusOnError)
					{
						string text = this.AssociatedControlID;
						if (!string.IsNullOrEmpty(text))
						{
							Control control = this.FindControl(text);
							if (control != null)
							{
								text = control.ClientID;
							}
							this.Page.SetValidatorInvalidControlFocus(text);
						}
					}
				}
			}
		}
	}
}
