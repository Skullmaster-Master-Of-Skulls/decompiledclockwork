using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003ED RID: 1005
	[SupportsEventValidation]
	[ValidationProperty("SelectedItem")]
	public class DropDownList : ListControl, IPostBackDataHandler
	{
		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x06003087 RID: 12423 RVA: 0x0009E7D8 File Offset: 0x0009C9D8
		// (set) Token: 0x06003088 RID: 12424 RVA: 0x0009E7E0 File Offset: 0x0009C9E0
		[Browsable(false)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003089 RID: 12425 RVA: 0x0009E7E9 File Offset: 0x0009C9E9
		// (set) Token: 0x0600308A RID: 12426 RVA: 0x0009E7F1 File Offset: 0x0009C9F1
		[Browsable(false)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x0600308B RID: 12427 RVA: 0x0009E7FA File Offset: 0x0009C9FA
		// (set) Token: 0x0600308C RID: 12428 RVA: 0x0009E802 File Offset: 0x0009CA02
		[Browsable(false)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x0009E80C File Offset: 0x0009CA0C
		// (set) Token: 0x0600308F RID: 12431 RVA: 0x0009E847 File Offset: 0x0009CA47
		[WebCategory("Behavior")]
		[DefaultValue(0)]
		[WebSysDescription("WebControl_SelectedIndex")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int SelectedIndex
		{
			get
			{
				int num = base.SelectedIndex;
				if (num < 0 && this.Items.Count > 0)
				{
					this.Items[0].Selected = true;
					num = 0;
				}
				return num;
			}
			set
			{
				base.SelectedIndex = value;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x0009E850 File Offset: 0x0009CA50
		internal override ArrayList SelectedIndicesInternal
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				return base.SelectedIndicesInternal;
			}
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x0009E86C File Offset: 0x0009CA6C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string uniqueID = this.UniqueID;
			if (uniqueID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x00060B2F File Offset: 0x0005ED2F
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x0009E893 File Offset: 0x0009CA93
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x0009E8A0 File Offset: 0x0009CAA0
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string[] values = postCollection.GetValues(postDataKey);
			base.EnsureDataBoundInLoadPostData();
			if (values != null)
			{
				base.ValidateEvent(postDataKey, values[0]);
				int num = this.Items.FindByValueInternal(values[0], false);
				if (this.SelectedIndex != num)
				{
					base.SetPostDataSelection(num);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x0009E8EB File Offset: 0x0009CAEB
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06003096 RID: 12438 RVA: 0x0009E8F4 File Offset: 0x0009CAF4
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.AutoPostBack && !this.Page.IsPostBackEventControlRegistered)
			{
				this.Page.AutoPostBackControl = this;
				if (this.CausesValidation)
				{
					this.Page.Validate(this.ValidationGroup);
				}
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x0009E946 File Offset: 0x0009CB46
		protected internal override void VerifyMultiSelect()
		{
			throw new HttpException(SR.GetString("Cant_Multiselect", new object[]
			{
				"DropDownList"
			}));
		}
	}
}
