using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.PageLayout
{
	// Token: 0x0200063A RID: 1594
	public abstract class BaseContainer : WebControl, IMutableRendering
	{
		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000BE149 File Offset: 0x000BC349
		// (set) Token: 0x060039FF RID: 14847 RVA: 0x000BE151 File Offset: 0x000BC351
		[Browsable(false)]
		public RadPageLayout Owner { get; internal set; }

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x06003A00 RID: 14848 RVA: 0x000BE15A File Offset: 0x000BC35A
		// (set) Token: 0x06003A01 RID: 14849 RVA: 0x000BE162 File Offset: 0x000BC362
		public TagName HtmlTag
		{
			get
			{
				return this._htmlTag;
			}
			set
			{
				this._htmlTag = value;
				this._tagName = this._htmlTag.ToString().ToLower();
			}
		}

		// Token: 0x17001310 RID: 4880
		// (get) Token: 0x06003A02 RID: 14850 RVA: 0x000BE186 File Offset: 0x000BC386
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Unknown;
			}
		}

		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x06003A03 RID: 14851 RVA: 0x000BE189 File Offset: 0x000BC389
		protected override string TagName
		{
			get
			{
				return this._tagName;
			}
		}

		// Token: 0x17001312 RID: 4882
		// (get) Token: 0x06003A04 RID: 14852 RVA: 0x000BE191 File Offset: 0x000BC391
		// (set) Token: 0x06003A05 RID: 14853 RVA: 0x000BE199 File Offset: 0x000BC399
		public string StaticId { get; set; }

		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x06003A06 RID: 14854 RVA: 0x000BE1A2 File Offset: 0x000BC3A2
		// (set) Token: 0x06003A07 RID: 14855 RVA: 0x000BE1AA File Offset: 0x000BC3AA
		public bool HiddenXs
		{
			get
			{
				return this._hiddenXs;
			}
			set
			{
				this._hiddenXs = value;
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x06003A08 RID: 14856 RVA: 0x000BE1B3 File Offset: 0x000BC3B3
		// (set) Token: 0x06003A09 RID: 14857 RVA: 0x000BE1BB File Offset: 0x000BC3BB
		public bool HiddenSm
		{
			get
			{
				return this._hiddenSm;
			}
			set
			{
				this._hiddenSm = value;
			}
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06003A0A RID: 14858 RVA: 0x000BE1C4 File Offset: 0x000BC3C4
		// (set) Token: 0x06003A0B RID: 14859 RVA: 0x000BE1CC File Offset: 0x000BC3CC
		public bool HiddenMd
		{
			get
			{
				return this._hiddenMd;
			}
			set
			{
				this._hiddenMd = value;
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06003A0C RID: 14860 RVA: 0x000BE1D5 File Offset: 0x000BC3D5
		// (set) Token: 0x06003A0D RID: 14861 RVA: 0x000BE1DD File Offset: 0x000BC3DD
		public bool HiddenLg
		{
			get
			{
				return this._hiddenLg;
			}
			set
			{
				this._hiddenLg = value;
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06003A0E RID: 14862 RVA: 0x000BE1E6 File Offset: 0x000BC3E6
		// (set) Token: 0x06003A0F RID: 14863 RVA: 0x000BE1EE File Offset: 0x000BC3EE
		public bool HiddenXl
		{
			get
			{
				return this._hiddenXl;
			}
			set
			{
				this._hiddenXl = value;
			}
		}

		// Token: 0x06003A10 RID: 14864 RVA: 0x000BE1F7 File Offset: 0x000BC3F7
		protected internal virtual void SetOwner(RadPageLayout owner)
		{
			this.Owner = owner;
		}

		// Token: 0x06003A11 RID: 14865 RVA: 0x000BE200 File Offset: 0x000BC400
		protected virtual List<string> GetTransformationClassNames()
		{
			return this.GetTransformationToggleClassNames();
		}

		// Token: 0x06003A12 RID: 14866 RVA: 0x000BE208 File Offset: 0x000BC408
		protected List<string> GetTransformationToggleClassNames()
		{
			List<string> list = new List<string>();
			if (this._hiddenXs)
			{
				list.Add("t-hidden-xs");
			}
			if (this._hiddenSm)
			{
				list.Add("t-hidden-sm");
			}
			if (this._hiddenMd)
			{
				list.Add("t-hidden-md");
			}
			if (this._hiddenLg)
			{
				list.Add("t-hidden-lg");
			}
			if (this._hiddenXl)
			{
				list.Add("t-hidden-xl");
			}
			return list;
		}

		// Token: 0x06003A13 RID: 14867 RVA: 0x000BE27B File Offset: 0x000BC47B
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			if (this.HtmlTag == Telerik.Web.UI.PageLayout.TagName.None)
			{
				return;
			}
			base.RenderBeginTag(writer);
		}

		// Token: 0x06003A14 RID: 14868 RVA: 0x000BE28F File Offset: 0x000BC48F
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (this.HtmlTag == Telerik.Web.UI.PageLayout.TagName.None)
			{
				return;
			}
			base.RenderEndTag(writer);
		}

		// Token: 0x06003A15 RID: 14869 RVA: 0x000BE2A4 File Offset: 0x000BC4A4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(this.StaticId))
			{
				string id = this.ID;
				this.ID = null;
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.StaticId);
				base.AddAttributesToRender(writer);
				this.ID = id;
				return;
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x000BE2F0 File Offset: 0x000BC4F0
		// (set) Token: 0x06003A17 RID: 14871 RVA: 0x000BE2F8 File Offset: 0x000BC4F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override string AccessKey { get; set; }

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06003A18 RID: 14872 RVA: 0x000BE301 File Offset: 0x000BC501
		// (set) Token: 0x06003A19 RID: 14873 RVA: 0x000BE309 File Offset: 0x000BC509
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override string ToolTip { get; set; }

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06003A1A RID: 14874 RVA: 0x000BE312 File Offset: 0x000BC512
		// (set) Token: 0x06003A1B RID: 14875 RVA: 0x000BE31A File Offset: 0x000BC51A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override short TabIndex { get; set; }

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06003A1C RID: 14876 RVA: 0x000BE323 File Offset: 0x000BC523
		// (set) Token: 0x06003A1D RID: 14877 RVA: 0x000BE32B File Offset: 0x000BC52B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override Unit Width { get; set; }

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06003A1E RID: 14878 RVA: 0x000BE334 File Offset: 0x000BC534
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override FontInfo Font
		{
			get
			{
				return base.Font;
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06003A1F RID: 14879 RVA: 0x000BE33C File Offset: 0x000BC53C
		// (set) Token: 0x06003A20 RID: 14880 RVA: 0x000BE344 File Offset: 0x000BC544
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor { get; set; }

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06003A21 RID: 14881 RVA: 0x000BE34D File Offset: 0x000BC54D
		// (set) Token: 0x06003A22 RID: 14882 RVA: 0x000BE355 File Offset: 0x000BC555
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override Color BackColor { get; set; }

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06003A23 RID: 14883 RVA: 0x000BE35E File Offset: 0x000BC55E
		// (set) Token: 0x06003A24 RID: 14884 RVA: 0x000BE366 File Offset: 0x000BC566
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override Color BorderColor { get; set; }

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06003A25 RID: 14885 RVA: 0x000BE36F File Offset: 0x000BC56F
		// (set) Token: 0x06003A26 RID: 14886 RVA: 0x000BE377 File Offset: 0x000BC577
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override BorderStyle BorderStyle { get; set; }

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06003A27 RID: 14887 RVA: 0x000BE380 File Offset: 0x000BC580
		// (set) Token: 0x06003A28 RID: 14888 RVA: 0x000BE388 File Offset: 0x000BC588
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override Unit BorderWidth { get; set; }

		// Token: 0x06003A29 RID: 14889 RVA: 0x000BE391 File Offset: 0x000BC591
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		protected override void OnDataBinding(EventArgs e)
		{
		}

		// Token: 0x04000F7C RID: 3964
		private TagName _htmlTag;

		// Token: 0x04000F7D RID: 3965
		private string _tagName = Telerik.Web.UI.PageLayout.TagName.Div.ToString().ToLower();

		// Token: 0x04000F7E RID: 3966
		private bool _hiddenXs;

		// Token: 0x04000F7F RID: 3967
		private bool _hiddenSm;

		// Token: 0x04000F80 RID: 3968
		private bool _hiddenMd;

		// Token: 0x04000F81 RID: 3969
		private bool _hiddenLg;

		// Token: 0x04000F82 RID: 3970
		private bool _hiddenXl;
	}
}
