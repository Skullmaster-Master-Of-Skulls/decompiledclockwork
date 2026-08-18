using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003A1 RID: 929
	[ControlBuilder(typeof(ContentBuilderInternal))]
	[Designer("System.Web.UI.Design.WebControls.ContentDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false)]
	public class Content : Control, INonBindingContainer, INamingContainer
	{
		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x00090B85 File Offset: 0x0008ED85
		// (set) Token: 0x06002C5C RID: 11356 RVA: 0x00090B9B File Offset: 0x0008ED9B
		[DefaultValue("")]
		[IDReferenceProperty(typeof(ContentPlaceHolder))]
		[Themeable(false)]
		[WebCategory("Behavior")]
		[WebSysDescription("Content_ContentPlaceHolderID")]
		public string ContentPlaceHolderID
		{
			get
			{
				if (this._contentPlaceHolderID == null)
				{
					return string.Empty;
				}
				return this._contentPlaceHolderID;
			}
			set
			{
				if (!base.DesignMode)
				{
					throw new NotSupportedException(SR.GetString("Property_Set_Not_Supported", new object[]
					{
						"ContentPlaceHolderID",
						base.GetType().ToString()
					}));
				}
				this._contentPlaceHolderID = value;
			}
		}

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x06002C5D RID: 11357 RVA: 0x00090BD8 File Offset: 0x0008EDD8
		// (remove) Token: 0x06002C5E RID: 11358 RVA: 0x00090BE1 File Offset: 0x0008EDE1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler DataBinding
		{
			add
			{
				base.DataBinding += value;
			}
			remove
			{
				base.DataBinding -= value;
			}
		}

		// Token: 0x1400005C RID: 92
		// (add) Token: 0x06002C5F RID: 11359 RVA: 0x00090BEA File Offset: 0x0008EDEA
		// (remove) Token: 0x06002C60 RID: 11360 RVA: 0x00090BF3 File Offset: 0x0008EDF3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Disposed
		{
			add
			{
				base.Disposed += value;
			}
			remove
			{
				base.Disposed -= value;
			}
		}

		// Token: 0x1400005D RID: 93
		// (add) Token: 0x06002C61 RID: 11361 RVA: 0x00090BFC File Offset: 0x0008EDFC
		// (remove) Token: 0x06002C62 RID: 11362 RVA: 0x00090C05 File Offset: 0x0008EE05
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Init
		{
			add
			{
				base.Init += value;
			}
			remove
			{
				base.Init -= value;
			}
		}

		// Token: 0x1400005E RID: 94
		// (add) Token: 0x06002C63 RID: 11363 RVA: 0x00090C0E File Offset: 0x0008EE0E
		// (remove) Token: 0x06002C64 RID: 11364 RVA: 0x00090C17 File Offset: 0x0008EE17
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Load
		{
			add
			{
				base.Load += value;
			}
			remove
			{
				base.Load -= value;
			}
		}

		// Token: 0x1400005F RID: 95
		// (add) Token: 0x06002C65 RID: 11365 RVA: 0x00090C20 File Offset: 0x0008EE20
		// (remove) Token: 0x06002C66 RID: 11366 RVA: 0x00090C29 File Offset: 0x0008EE29
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler PreRender
		{
			add
			{
				base.PreRender += value;
			}
			remove
			{
				base.PreRender -= value;
			}
		}

		// Token: 0x14000060 RID: 96
		// (add) Token: 0x06002C67 RID: 11367 RVA: 0x00090C32 File Offset: 0x0008EE32
		// (remove) Token: 0x06002C68 RID: 11368 RVA: 0x00090C3B File Offset: 0x0008EE3B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new event EventHandler Unload
		{
			add
			{
				base.Unload += value;
			}
			remove
			{
				base.Unload -= value;
			}
		}

		// Token: 0x04001F34 RID: 7988
		private string _contentPlaceHolderID;
	}
}
