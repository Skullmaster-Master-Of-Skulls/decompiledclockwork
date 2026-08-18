using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000551 RID: 1361
	[Designer("System.Web.UI.Design.WebControls.WebParts.PartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public abstract class Part : Panel, INamingContainer, ICompositeControlDesignerAccessor
	{
		// Token: 0x06004545 RID: 17733 RVA: 0x000E4A80 File Offset: 0x000E2C80
		internal Part()
		{
		}

		// Token: 0x17001471 RID: 5233
		// (get) Token: 0x06004546 RID: 17734 RVA: 0x000E4A88 File Offset: 0x000E2C88
		// (set) Token: 0x06004547 RID: 17735 RVA: 0x000E4AB1 File Offset: 0x000E2CB1
		[DefaultValue(PartChromeState.Normal)]
		[WebCategory("WebPartAppearance")]
		[WebSysDescription("Part_ChromeState")]
		public virtual PartChromeState ChromeState
		{
			get
			{
				object obj = this.ViewState["ChromeState"];
				if (obj == null)
				{
					return PartChromeState.Normal;
				}
				return (PartChromeState)obj;
			}
			set
			{
				if (value < PartChromeState.Normal || value > PartChromeState.Minimized)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ChromeState"] = value;
			}
		}

		// Token: 0x17001472 RID: 5234
		// (get) Token: 0x06004548 RID: 17736 RVA: 0x000E4ADC File Offset: 0x000E2CDC
		// (set) Token: 0x06004549 RID: 17737 RVA: 0x000E4B05 File Offset: 0x000E2D05
		[DefaultValue(PartChromeType.Default)]
		[WebCategory("WebPartAppearance")]
		[WebSysDescription("Part_ChromeType")]
		public virtual PartChromeType ChromeType
		{
			get
			{
				object obj = this.ViewState["ChromeType"];
				if (obj == null)
				{
					return PartChromeType.Default;
				}
				return (PartChromeType)((int)obj);
			}
			set
			{
				if (value < PartChromeType.Default || value > PartChromeType.BorderOnly)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["ChromeType"] = (int)value;
			}
		}

		// Token: 0x17001473 RID: 5235
		// (get) Token: 0x0600454A RID: 17738 RVA: 0x000856CA File Offset: 0x000838CA
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x000E4B30 File Offset: 0x000E2D30
		// (set) Token: 0x0600454C RID: 17740 RVA: 0x000E4B5D File Offset: 0x000E2D5D
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("WebPartAppearance")]
		[WebSysDescription("Part_Description")]
		public virtual string Description
		{
			get
			{
				string text = (string)this.ViewState["Description"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["Description"] = value;
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x0600454D RID: 17741 RVA: 0x000E4B70 File Offset: 0x000E2D70
		// (set) Token: 0x0600454E RID: 17742 RVA: 0x000D9EF2 File Offset: 0x000D80F2
		[Localizable(true)]
		[WebSysDefaultValue("")]
		[WebCategory("WebPartAppearance")]
		[WebSysDescription("Part_Title")]
		public virtual string Title
		{
			get
			{
				string text = (string)this.ViewState["Title"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["Title"] = value;
			}
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x000906F4 File Offset: 0x0008E8F4
		public override void DataBind()
		{
			this.OnDataBinding(EventArgs.Empty);
			this.EnsureChildControls();
			this.DataBindChildren();
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x0009070D File Offset: 0x0008E90D
		void ICompositeControlDesignerAccessor.RecreateChildControls()
		{
			base.ChildControlsCreated = false;
			this.EnsureChildControls();
		}
	}
}
