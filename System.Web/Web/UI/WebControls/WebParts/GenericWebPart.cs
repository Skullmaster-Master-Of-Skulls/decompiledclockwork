using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020006C8 RID: 1736
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class GenericWebPart : WebPart
	{
		// Token: 0x06005565 RID: 21861 RVA: 0x0015A000 File Offset: 0x00159000
		protected internal GenericWebPart(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control is WebPart)
			{
				throw new ArgumentException(SR.GetString("GenericWebPart_CannotWrapWebPart"), "control");
			}
			if (control is BasePartialCachingControl)
			{
				throw new ArgumentException(SR.GetString("GenericWebPart_CannotWrapOutputCachedControl"), "control");
			}
			if (string.IsNullOrEmpty(control.ID))
			{
				throw new ArgumentException(SR.GetString("GenericWebPart_NoID", new object[]
				{
					control.GetType().FullName
				}));
			}
			this.ID = "gwp" + control.ID;
			this._childControl = control;
			this._childIWebPart = (this._childControl as IWebPart);
			this.CopyChildAttributes();
		}

		// Token: 0x170015F4 RID: 5620
		// (get) Token: 0x06005566 RID: 21862 RVA: 0x0015A0C2 File Offset: 0x001590C2
		// (set) Token: 0x06005567 RID: 21863 RVA: 0x0015A0DE File Offset: 0x001590DE
		public override string CatalogIconImageUrl
		{
			get
			{
				if (this._childIWebPart != null)
				{
					return this._childIWebPart.CatalogIconImageUrl;
				}
				return base.CatalogIconImageUrl;
			}
			set
			{
				if (this._childIWebPart != null)
				{
					this._childIWebPart.CatalogIconImageUrl = value;
					return;
				}
				base.CatalogIconImageUrl = value;
			}
		}

		// Token: 0x170015F5 RID: 5621
		// (get) Token: 0x06005568 RID: 21864 RVA: 0x0015A0FC File Offset: 0x001590FC
		public Control ChildControl
		{
			get
			{
				return this._childControl;
			}
		}

		// Token: 0x170015F6 RID: 5622
		// (get) Token: 0x06005569 RID: 21865 RVA: 0x0015A104 File Offset: 0x00159104
		// (set) Token: 0x0600556A RID: 21866 RVA: 0x0015A120 File Offset: 0x00159120
		public override string Description
		{
			get
			{
				if (this._childIWebPart != null)
				{
					return this._childIWebPart.Description;
				}
				return base.Description;
			}
			set
			{
				if (this._childIWebPart != null)
				{
					this._childIWebPart.Description = value;
					return;
				}
				base.Description = value;
			}
		}

		// Token: 0x170015F7 RID: 5623
		// (get) Token: 0x0600556B RID: 21867 RVA: 0x0015A140 File Offset: 0x00159140
		// (set) Token: 0x0600556C RID: 21868 RVA: 0x0015A16C File Offset: 0x0015916C
		public override Unit Height
		{
			get
			{
				WebControl webControl = this.ChildControl as WebControl;
				if (webControl != null)
				{
					return webControl.Height;
				}
				return base.Height;
			}
			set
			{
				WebControl webControl = this.ChildControl as WebControl;
				if (webControl != null)
				{
					webControl.Height = value;
					return;
				}
				base.Height = value;
			}
		}

		// Token: 0x170015F8 RID: 5624
		// (get) Token: 0x0600556D RID: 21869 RVA: 0x0015A197 File Offset: 0x00159197
		// (set) Token: 0x0600556E RID: 21870 RVA: 0x0015A19F File Offset: 0x0015919F
		public sealed override string ID
		{
			get
			{
				return base.ID;
			}
			set
			{
				base.ID = value;
			}
		}

		// Token: 0x170015F9 RID: 5625
		// (get) Token: 0x0600556F RID: 21871 RVA: 0x0015A1A8 File Offset: 0x001591A8
		public override string Subtitle
		{
			get
			{
				if (this._childIWebPart != null)
				{
					return this._childIWebPart.Subtitle;
				}
				if (this._subtitle == null)
				{
					return string.Empty;
				}
				return this._subtitle;
			}
		}

		// Token: 0x170015FA RID: 5626
		// (get) Token: 0x06005570 RID: 21872 RVA: 0x0015A1D2 File Offset: 0x001591D2
		// (set) Token: 0x06005571 RID: 21873 RVA: 0x0015A1EE File Offset: 0x001591EE
		public override string Title
		{
			get
			{
				if (this._childIWebPart != null)
				{
					return this._childIWebPart.Title;
				}
				return base.Title;
			}
			set
			{
				if (this._childIWebPart != null)
				{
					this._childIWebPart.Title = value;
					return;
				}
				base.Title = value;
			}
		}

		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06005572 RID: 21874 RVA: 0x0015A20C File Offset: 0x0015920C
		// (set) Token: 0x06005573 RID: 21875 RVA: 0x0015A228 File Offset: 0x00159228
		public override string TitleIconImageUrl
		{
			get
			{
				if (this._childIWebPart != null)
				{
					return this._childIWebPart.TitleIconImageUrl;
				}
				return base.TitleIconImageUrl;
			}
			set
			{
				if (this._childIWebPart != null)
				{
					this._childIWebPart.TitleIconImageUrl = value;
					return;
				}
				base.TitleIconImageUrl = value;
			}
		}

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06005574 RID: 21876 RVA: 0x0015A246 File Offset: 0x00159246
		// (set) Token: 0x06005575 RID: 21877 RVA: 0x0015A262 File Offset: 0x00159262
		public override string TitleUrl
		{
			get
			{
				if (this._childIWebPart != null)
				{
					return this._childIWebPart.TitleUrl;
				}
				return base.TitleUrl;
			}
			set
			{
				if (this._childIWebPart != null)
				{
					this._childIWebPart.TitleUrl = value;
					return;
				}
				base.TitleUrl = value;
			}
		}

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06005576 RID: 21878 RVA: 0x0015A280 File Offset: 0x00159280
		public override WebPartVerbCollection Verbs
		{
			get
			{
				if (this.ChildControl != null)
				{
					IWebActionable webActionable = this.ChildControl as IWebActionable;
					if (webActionable != null)
					{
						return new WebPartVerbCollection(base.Verbs, webActionable.Verbs);
					}
				}
				return base.Verbs;
			}
		}

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06005577 RID: 21879 RVA: 0x0015A2BC File Offset: 0x001592BC
		public override object WebBrowsableObject
		{
			get
			{
				IWebEditable webEditable = this.ChildControl as IWebEditable;
				if (webEditable != null)
				{
					return webEditable.WebBrowsableObject;
				}
				return this.ChildControl;
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06005578 RID: 21880 RVA: 0x0015A2E8 File Offset: 0x001592E8
		// (set) Token: 0x06005579 RID: 21881 RVA: 0x0015A314 File Offset: 0x00159314
		public override Unit Width
		{
			get
			{
				WebControl webControl = this.ChildControl as WebControl;
				if (webControl != null)
				{
					return webControl.Width;
				}
				return base.Width;
			}
			set
			{
				WebControl webControl = this.ChildControl as WebControl;
				if (webControl != null)
				{
					webControl.Width = value;
					return;
				}
				base.Width = value;
			}
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x0015A340 File Offset: 0x00159340
		private void CopyChildAttributes()
		{
			IAttributeAccessor attributeAccessor = this.ChildControl as IAttributeAccessor;
			if (attributeAccessor != null)
			{
				base.AuthorizationFilter = attributeAccessor.GetAttribute("AuthorizationFilter");
				base.CatalogIconImageUrl = attributeAccessor.GetAttribute("CatalogIconImageUrl");
				base.Description = attributeAccessor.GetAttribute("Description");
				string attribute = attributeAccessor.GetAttribute("ExportMode");
				if (attribute != null)
				{
					base.ExportMode = (WebPartExportMode)Util.GetEnumAttribute("ExportMode", attribute, typeof(WebPartExportMode));
				}
				this._subtitle = attributeAccessor.GetAttribute("Subtitle");
				base.Title = attributeAccessor.GetAttribute("Title");
				base.TitleIconImageUrl = attributeAccessor.GetAttribute("TitleIconImageUrl");
				base.TitleUrl = attributeAccessor.GetAttribute("TitleUrl");
			}
			WebControl webControl = this.ChildControl as WebControl;
			if (webControl != null)
			{
				webControl.Attributes.Remove("AuthorizationFilter");
				webControl.Attributes.Remove("CatalogIconImageUrl");
				webControl.Attributes.Remove("Description");
				webControl.Attributes.Remove("ExportMode");
				webControl.Attributes.Remove("Subtitle");
				webControl.Attributes.Remove("Title");
				webControl.Attributes.Remove("TitleIconImageUrl");
				webControl.Attributes.Remove("TitleUrl");
				return;
			}
			if (attributeAccessor != null)
			{
				attributeAccessor.SetAttribute("AuthorizationFilter", null);
				attributeAccessor.SetAttribute("CatalogIconImageUrl", null);
				attributeAccessor.SetAttribute("Description", null);
				attributeAccessor.SetAttribute("ExportMode", null);
				attributeAccessor.SetAttribute("Subtitle", null);
				attributeAccessor.SetAttribute("Title", null);
				attributeAccessor.SetAttribute("TitleIconImageUrl", null);
				attributeAccessor.SetAttribute("TitleUrl", null);
			}
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x0015A4FB File Offset: 0x001594FB
		protected internal override void CreateChildControls()
		{
			((GenericWebPart.GenericWebPartControlCollection)this.Controls).AddGenericControl(this.ChildControl);
		}

		// Token: 0x0600557C RID: 21884 RVA: 0x0015A513 File Offset: 0x00159513
		protected override ControlCollection CreateControlCollection()
		{
			return new GenericWebPart.GenericWebPartControlCollection(this);
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x0015A51C File Offset: 0x0015951C
		public override EditorPartCollection CreateEditorParts()
		{
			IWebEditable webEditable = this.ChildControl as IWebEditable;
			if (webEditable != null)
			{
				return new EditorPartCollection(base.CreateEditorParts(), webEditable.CreateEditorParts());
			}
			return base.CreateEditorParts();
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x0015A550 File Offset: 0x00159550
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.EnsureChildControls();
			}
			this.RenderContents(writer);
		}

		// Token: 0x04002F1B RID: 12059
		internal const string IDPrefix = "gwp";

		// Token: 0x04002F1C RID: 12060
		private Control _childControl;

		// Token: 0x04002F1D RID: 12061
		private IWebPart _childIWebPart;

		// Token: 0x04002F1E RID: 12062
		private string _subtitle;

		// Token: 0x020006C9 RID: 1737
		private sealed class GenericWebPartControlCollection : ControlCollection
		{
			// Token: 0x0600557F RID: 21887 RVA: 0x0015A567 File Offset: 0x00159567
			public GenericWebPartControlCollection(GenericWebPart owner) : base(owner)
			{
				base.SetCollectionReadOnly("GenericWebPart_CannotModify");
			}

			// Token: 0x06005580 RID: 21888 RVA: 0x0015A57C File Offset: 0x0015957C
			public void AddGenericControl(Control control)
			{
				string collectionReadOnly = base.SetCollectionReadOnly(null);
				try
				{
					try
					{
						this.Clear();
						this.Add(control);
					}
					finally
					{
						base.SetCollectionReadOnly(collectionReadOnly);
					}
				}
				catch
				{
					throw;
				}
			}
		}
	}
}
