using System;

namespace System.Web.UI.Design
{
	// Token: 0x0200003B RID: 59
	public class EditableDesignerRegion : DesignerRegion
	{
		// Token: 0x0600020C RID: 524 RVA: 0x0000DBE9 File Offset: 0x0000BDE9
		public EditableDesignerRegion(ControlDesigner owner, string name) : this(owner, name, false)
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		public EditableDesignerRegion(ControlDesigner owner, string name, bool serverControlsOnly) : base(owner, name)
		{
			this._serverControlsOnly = serverControlsOnly;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000DC05 File Offset: 0x0000BE05
		// (set) Token: 0x0600020F RID: 527 RVA: 0x0000DC13 File Offset: 0x0000BE13
		public virtual string Content
		{
			get
			{
				return base.Designer.GetEditableDesignerRegionContent(this);
			}
			set
			{
				base.Designer.SetEditableDesignerRegionContent(this, value);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000DC22 File Offset: 0x0000BE22
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000DC2A File Offset: 0x0000BE2A
		public bool ServerControlsOnly
		{
			get
			{
				return this._serverControlsOnly;
			}
			set
			{
				this._serverControlsOnly = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000DC33 File Offset: 0x0000BE33
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000DC3B File Offset: 0x0000BE3B
		public virtual bool SupportsDataBinding
		{
			get
			{
				return this._supportsDataBinding;
			}
			set
			{
				this._supportsDataBinding = value;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000DC44 File Offset: 0x0000BE44
		public virtual ViewRendering GetChildViewRendering(Control control)
		{
			return ControlDesigner.GetViewRendering(control);
		}

		// Token: 0x04000139 RID: 313
		private bool _serverControlsOnly;

		// Token: 0x0400013A RID: 314
		private bool _supportsDataBinding;
	}
}
