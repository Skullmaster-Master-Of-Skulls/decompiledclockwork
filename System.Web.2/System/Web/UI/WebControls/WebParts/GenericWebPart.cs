using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x0200053F RID: 1343
	[ToolboxItem(false)]
	public class GenericWebPart : WebPart
	{
		// Token: 0x0600448F RID: 17551 RVA: 0x000E3110 File Offset: 0x000E1310
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

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x06004490 RID: 17552 RVA: 0x000E31D0 File Offset: 0x000E13D0
		// (set) Token: 0x06004491 RID: 17553 RVA: 0x000E31EC File Offset: 0x000E13EC
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

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x000E320A File Offset: 0x000E140A
		public Control ChildControl
		{
			get
			{
				return this._childControl;
			}
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x06004493 RID: 17555 RVA: 0x000E3212 File Offset: 0x000E1412
		// (set) Token: 0x06004494 RID: 17556 RVA: 0x000E322E File Offset: 0x000E142E
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

		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06004495 RID: 17557 RVA: 0x000E324C File Offset: 0x000E144C
		// (set) Token: 0x06004496 RID: 17558 RVA: 0x000E3278 File Offset: 0x000E1478
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

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06004497 RID: 17559 RVA: 0x00069884 File Offset: 0x00067A84
		// (set) Token: 0x06004498 RID: 17560 RVA: 0x0006988C File Offset: 0x00067A8C
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

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06004499 RID: 17561 RVA: 0x000E32A3 File Offset: 0x000E14A3
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

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x000E32CD File Offset: 0x000E14CD
		// (set) Token: 0x0600449B RID: 17563 RVA: 0x000E32E9 File Offset: 0x000E14E9
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

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x000E3307 File Offset: 0x000E1507
		// (set) Token: 0x0600449D RID: 17565 RVA: 0x000E3323 File Offset: 0x000E1523
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

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x0600449E RID: 17566 RVA: 0x000E3341 File Offset: 0x000E1541
		// (set) Token: 0x0600449F RID: 17567 RVA: 0x000E335D File Offset: 0x000E155D
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

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x060044A0 RID: 17568 RVA: 0x000E337C File Offset: 0x000E157C
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

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x000E33B8 File Offset: 0x000E15B8
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

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x060044A2 RID: 17570 RVA: 0x000E33E4 File Offset: 0x000E15E4
		// (set) Token: 0x060044A3 RID: 17571 RVA: 0x000E3410 File Offset: 0x000E1610
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

		// Token: 0x060044A4 RID: 17572 RVA: 0x000E343C File Offset: 0x000E163C
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

		// Token: 0x060044A5 RID: 17573 RVA: 0x000E35F7 File Offset: 0x000E17F7
		protected internal override void CreateChildControls()
		{
			((GenericWebPart.GenericWebPartControlCollection)this.Controls).AddGenericControl(this.ChildControl);
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x000E360F File Offset: 0x000E180F
		protected override ControlCollection CreateControlCollection()
		{
			return new GenericWebPart.GenericWebPartControlCollection(this);
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x000E3618 File Offset: 0x000E1818
		public override EditorPartCollection CreateEditorParts()
		{
			IWebEditable webEditable = this.ChildControl as IWebEditable;
			if (webEditable != null)
			{
				return new EditorPartCollection(base.CreateEditorParts(), webEditable.CreateEditorParts());
			}
			return base.CreateEditorParts();
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x000E364C File Offset: 0x000E184C
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				this.EnsureChildControls();
			}
			this.RenderContents(writer);
		}

		// Token: 0x04002638 RID: 9784
		internal const string IDPrefix = "gwp";

		// Token: 0x04002639 RID: 9785
		private Control _childControl;

		// Token: 0x0400263A RID: 9786
		private IWebPart _childIWebPart;

		// Token: 0x0400263B RID: 9787
		private string _subtitle;

		// Token: 0x020009EC RID: 2540
		private sealed class GenericWebPartControlCollection : ControlCollection
		{
			// Token: 0x06006D17 RID: 27927 RVA: 0x0018684A File Offset: 0x00184A4A
			public GenericWebPartControlCollection(GenericWebPart owner) : base(owner)
			{
				base.SetCollectionReadOnly("GenericWebPart_CannotModify");
			}

			// Token: 0x06006D18 RID: 27928 RVA: 0x00186860 File Offset: 0x00184A60
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
