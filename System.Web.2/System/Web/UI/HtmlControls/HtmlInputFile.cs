using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x0200034F RID: 847
	[ValidationProperty("Value")]
	public class HtmlInputFile : HtmlInputControl, IPostBackDataHandler
	{
		// Token: 0x060026F0 RID: 9968 RVA: 0x0007F46F File Offset: 0x0007D66F
		public HtmlInputFile() : base("file")
		{
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x0007F47C File Offset: 0x0007D67C
		// (set) Token: 0x060026F2 RID: 9970 RVA: 0x0007F4A4 File Offset: 0x0007D6A4
		[WebCategory("Behavior")]
		[DefaultValue("")]
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

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x0007F4BC File Offset: 0x0007D6BC
		// (set) Token: 0x060026F4 RID: 9972 RVA: 0x0007F4EA File Offset: 0x0007D6EA
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x0007F502 File Offset: 0x0007D702
		[WebCategory("Default")]
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public HttpPostedFile PostedFile
		{
			get
			{
				return this.Context.Request.Files[this.RenderedNameAttribute];
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x0007F520 File Offset: 0x0007D720
		// (set) Token: 0x060026F7 RID: 9975 RVA: 0x0007F54E File Offset: 0x0007D74E
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

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x0007F568 File Offset: 0x0007D768
		// (set) Token: 0x060026F9 RID: 9977 RVA: 0x0007F58B File Offset: 0x0007D78B
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

		// Token: 0x060026FA RID: 9978 RVA: 0x0007F5B0 File Offset: 0x0007D7B0
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x00007722 File Offset: 0x00005922
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return false;
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x0007F5BA File Offset: 0x0007D7BA
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x0007F5C4 File Offset: 0x0007D7C4
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
