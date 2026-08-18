using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web.Resources;

namespace System.Web.UI
{
	// Token: 0x0200004F RID: 79
	[DefaultProperty("TargetControlID")]
	[Designer("System.Web.UI.Design.ExtenderControlDesigner, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[NonVisualControl]
	[ParseChildren(true)]
	[PersistChildren(false)]
	[ToolboxItem("System.Web.UI.Design.ExtenderControlToolboxItem, System.Web.Extensions.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class ExtenderControl : Control, IExtenderControl
	{
		// Token: 0x060002EA RID: 746 RVA: 0x00011E41 File Offset: 0x00010041
		protected ExtenderControl()
		{
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00011E49 File Offset: 0x00010049
		internal ExtenderControl(IScriptManagerInternal scriptManager, IPage page)
		{
			this._scriptManager = scriptManager;
			this._page = page;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00011E60 File Offset: 0x00010060
		private IPage IPage
		{
			get
			{
				if (this._page != null)
				{
					return this._page;
				}
				Page page = this.Page;
				if (page == null)
				{
					throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
				}
				return new PageWrapper(page);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00011E98 File Offset: 0x00010098
		private IScriptManagerInternal ScriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					Page page = this.Page;
					if (page == null)
					{
						throw new InvalidOperationException(AtlasWeb.Common_PageCannotBeNull);
					}
					this._scriptManager = System.Web.UI.ScriptManager.GetCurrent(page);
					if (this._scriptManager == null)
					{
						throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.Common_ScriptManagerRequired, new object[]
						{
							this.ID
						}));
					}
				}
				return this._scriptManager;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00011F00 File Offset: 0x00010100
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00011F16 File Offset: 0x00010116
		[Category("Behavior")]
		[DefaultValue("")]
		[IDReferenceProperty]
		[ResourceDescription("ExtenderControl_TargetControlID")]
		public string TargetControlID
		{
			get
			{
				if (this._targetControlID != null)
				{
					return this._targetControlID;
				}
				return string.Empty;
			}
			set
			{
				this._targetControlID = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00011F1F File Offset: 0x0001011F
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x00002058 File Offset: 0x00000258
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00011F28 File Offset: 0x00010128
		private static UpdatePanel FindUpdatePanel(Control control)
		{
			for (Control parent = control.Parent; parent != null; parent = parent.Parent)
			{
				UpdatePanel updatePanel = parent as UpdatePanel;
				if (updatePanel != null)
				{
					return updatePanel;
				}
			}
			return null;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00011F55 File Offset: 0x00010155
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.RegisterWithScriptManager();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00011F64 File Offset: 0x00010164
		private void RegisterWithScriptManager()
		{
			if (string.IsNullOrEmpty(this.TargetControlID))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ExtenderControl_TargetControlIDEmpty, new object[]
				{
					this.ID
				}));
			}
			Control control = this.FindControl(this.TargetControlID);
			if (control == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.ExtenderControl_TargetControlIDInvalid, new object[]
				{
					this.ID,
					this.TargetControlID
				}));
			}
			if (ExtenderControl.FindUpdatePanel(this) != ExtenderControl.FindUpdatePanel(control))
			{
				throw new InvalidOperationException(AtlasWeb.ExtenderControl_TargetControlDifferentUpdatePanel);
			}
			this.ScriptManager.RegisterExtenderControl<ExtenderControl>(this, control);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00012005 File Offset: 0x00010205
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			this.IPage.VerifyRenderingInServerForm(this);
			if (!base.DesignMode)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x060002F6 RID: 758
		protected abstract IEnumerable<ScriptDescriptor> GetScriptDescriptors(Control targetControl);

		// Token: 0x060002F7 RID: 759
		protected abstract IEnumerable<ScriptReference> GetScriptReferences();

		// Token: 0x060002F8 RID: 760 RVA: 0x0001202E File Offset: 0x0001022E
		IEnumerable<ScriptDescriptor> IExtenderControl.GetScriptDescriptors(Control targetControl)
		{
			return this.GetScriptDescriptors(targetControl);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00012037 File Offset: 0x00010237
		IEnumerable<ScriptReference> IExtenderControl.GetScriptReferences()
		{
			return this.GetScriptReferences();
		}

		// Token: 0x04000116 RID: 278
		private string _targetControlID;

		// Token: 0x04000117 RID: 279
		private IScriptManagerInternal _scriptManager;

		// Token: 0x04000118 RID: 280
		private new IPage _page;
	}
}
