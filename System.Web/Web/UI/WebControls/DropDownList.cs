using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200056E RID: 1390
	[ValidationProperty("SelectedItem")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DropDownList : ListControl, IPostBackDataHandler
	{
		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x00119B4C File Offset: 0x00118B4C
		// (set) Token: 0x0600445E RID: 17502 RVA: 0x00119B54 File Offset: 0x00118B54
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

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x00119B5D File Offset: 0x00118B5D
		// (set) Token: 0x06004460 RID: 17504 RVA: 0x00119B65 File Offset: 0x00118B65
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

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x00119B6E File Offset: 0x00118B6E
		// (set) Token: 0x06004462 RID: 17506 RVA: 0x00119B76 File Offset: 0x00118B76
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

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x00119B80 File Offset: 0x00118B80
		// (set) Token: 0x06004464 RID: 17508 RVA: 0x00119BBB File Offset: 0x00118BBB
		[DefaultValue(0)]
		[WebCategory("Behavior")]
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

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06004465 RID: 17509 RVA: 0x00119BC4 File Offset: 0x00118BC4
		internal override ArrayList SelectedIndicesInternal
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				return base.SelectedIndicesInternal;
			}
		}

		// Token: 0x06004466 RID: 17510 RVA: 0x00119BD4 File Offset: 0x00118BD4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string uniqueID = this.UniqueID;
			if (uniqueID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x00119BFB File Offset: 0x00118BFB
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06004468 RID: 17512 RVA: 0x00119C03 File Offset: 0x00118C03
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06004469 RID: 17513 RVA: 0x00119C10 File Offset: 0x00118C10
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string[] values = postCollection.GetValues(postDataKey);
			this.EnsureDataBound();
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

		// Token: 0x0600446A RID: 17514 RVA: 0x00119C5B File Offset: 0x00118C5B
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x0600446B RID: 17515 RVA: 0x00119C64 File Offset: 0x00118C64
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

		// Token: 0x0600446C RID: 17516 RVA: 0x00119CB8 File Offset: 0x00118CB8
		protected internal override void VerifyMultiSelect()
		{
			throw new HttpException(SR.GetString("Cant_Multiselect", new object[]
			{
				"DropDownList"
			}));
		}
	}
}
