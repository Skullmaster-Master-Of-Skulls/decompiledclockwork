using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200049E RID: 1182
	[ValidationProperty("Value")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputFile : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x0600372D RID: 14125 RVA: 0x000ED706 File Offset: 0x000EC706
		public HtmlInputFile() : base("file")
		{
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x000ED714 File Offset: 0x000EC714
		// (set) Token: 0x0600372F RID: 14127 RVA: 0x000ED73C File Offset: 0x000EC73C
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Accept
		{
			get
			{
				string text = base.Attributes["accept"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				base.Attributes["accept"] = HtmlControl.MapStringAttributeToString(value);
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x000ED754 File Offset: 0x000EC754
		// (set) Token: 0x06003731 RID: 14129 RVA: 0x000ED782 File Offset: 0x000EC782
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public int MaxLength
		{
			get
			{
				string text = base.Attributes["maxlength"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["maxlength"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x000ED79A File Offset: 0x000EC79A
		[WebCategory("Default")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public HttpPostedFile PostedFile
		{
			get
			{
				return this.Context.Request.Files[this.RenderedNameAttribute];
			}
		}

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06003733 RID: 14131 RVA: 0x000ED7B8 File Offset: 0x000EC7B8
		// (set) Token: 0x06003734 RID: 14132 RVA: 0x000ED7E6 File Offset: 0x000EC7E6
		[WebCategory("Appearance")]
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Size
		{
			get
			{
				string text = base.Attributes["size"];
				if (text == null)
				{
					return -1;
				}
				return int.Parse(text, CultureInfo.InvariantCulture);
			}
			set
			{
				base.Attributes["size"] = HtmlControl.MapIntegerAttributeToString(value);
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06003735 RID: 14133 RVA: 0x000ED800 File Offset: 0x000EC800
		// (set) Token: 0x06003736 RID: 14134 RVA: 0x000ED824 File Offset: 0x000EC824
		[Browsable(false)]
		public override string Value
		{
			get
			{
				HttpPostedFile postedFile = this.PostedFile;
				if (postedFile != null)
				{
					return postedFile.FileName;
				}
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException(SR.GetString("Value_Set_Not_Supported", new object[]
				{
					base.GetType().Name
				}));
			}
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000ED856 File Offset: 0x000EC856
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06003738 RID: 14136 RVA: 0x000ED860 File Offset: 0x000EC860
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return false;
		}

		// Token: 0x06003739 RID: 14137 RVA: 0x000ED863 File Offset: 0x000EC863
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x0600373A RID: 14138 RVA: 0x000ED86B File Offset: 0x000EC86B
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x0600373B RID: 14139 RVA: 0x000ED870 File Offset: 0x000EC870
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			HtmlForm form = this.Page.Form;
			if (form != null && form.Enctype.Length == 0)
			{
				form.Enctype = "multipart/form-data";
			}
		}
	}
}
