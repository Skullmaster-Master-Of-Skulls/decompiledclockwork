using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using Telerik.Web.UI.PageLayout;
using Telerik.Web.UI.PageLayout.Utils;

namespace Telerik.Web.UI
{
	// Token: 0x0200063D RID: 1597
	[ToolboxItem(false)]
	[ParseChildren(false)]
	public class LayoutColumn : BaseContainer
	{
		// Token: 0x06003A42 RID: 14914 RVA: 0x000BE6F0 File Offset: 0x000BC8F0
		public LayoutColumn()
		{
			this.Offset = -1;
			this.Push = -1;
			this.Pull = -1;
			this.OffsetXs = -1;
			this.OffsetSm = -1;
			this.OffsetMd = -1;
			this.OffsetLg = -1;
			this.OffsetXl = -1;
			this.PullXs = -1;
			this.PullSm = -1;
			this.PullMd = -1;
			this.PullLg = -1;
			this.PullXl = -1;
			this.PushXs = -1;
			this.PushSm = -1;
			this.PushMd = -1;
			this.PushLg = -1;
			this.PushXl = -1;
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06003A43 RID: 14915 RVA: 0x000BE789 File Offset: 0x000BC989
		// (set) Token: 0x06003A44 RID: 14916 RVA: 0x000BE791 File Offset: 0x000BC991
		[PersistenceMode(PersistenceMode.Attribute)]
		public int Span
		{
			get
			{
				return this._span;
			}
			set
			{
				this._span = value;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x000BE79A File Offset: 0x000BC99A
		// (set) Token: 0x06003A46 RID: 14918 RVA: 0x000BE7A2 File Offset: 0x000BC9A2
		public int SpanXs
		{
			get
			{
				return this._spanXs;
			}
			set
			{
				this._spanXs = value;
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x000BE7AB File Offset: 0x000BC9AB
		// (set) Token: 0x06003A48 RID: 14920 RVA: 0x000BE7B3 File Offset: 0x000BC9B3
		public int SpanSm
		{
			get
			{
				return this._spanSm;
			}
			set
			{
				this._spanSm = value;
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06003A49 RID: 14921 RVA: 0x000BE7BC File Offset: 0x000BC9BC
		// (set) Token: 0x06003A4A RID: 14922 RVA: 0x000BE7C4 File Offset: 0x000BC9C4
		public int SpanMd
		{
			get
			{
				return this._spanMd;
			}
			set
			{
				this._spanMd = value;
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06003A4B RID: 14923 RVA: 0x000BE7CD File Offset: 0x000BC9CD
		// (set) Token: 0x06003A4C RID: 14924 RVA: 0x000BE7D5 File Offset: 0x000BC9D5
		public int SpanLg
		{
			get
			{
				return this._spanLg;
			}
			set
			{
				this._spanLg = value;
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000BE7DE File Offset: 0x000BC9DE
		// (set) Token: 0x06003A4E RID: 14926 RVA: 0x000BE7E6 File Offset: 0x000BC9E6
		public int SpanXl
		{
			get
			{
				return this._spanXl;
			}
			set
			{
				this._spanXl = value;
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x000BE7EF File Offset: 0x000BC9EF
		// (set) Token: 0x06003A50 RID: 14928 RVA: 0x000BE7F7 File Offset: 0x000BC9F7
		public int Offset { get; set; }

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06003A51 RID: 14929 RVA: 0x000BE800 File Offset: 0x000BCA00
		// (set) Token: 0x06003A52 RID: 14930 RVA: 0x000BE808 File Offset: 0x000BCA08
		public int Push { get; set; }

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06003A53 RID: 14931 RVA: 0x000BE811 File Offset: 0x000BCA11
		// (set) Token: 0x06003A54 RID: 14932 RVA: 0x000BE819 File Offset: 0x000BCA19
		public int Pull { get; set; }

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06003A55 RID: 14933 RVA: 0x000BE822 File Offset: 0x000BCA22
		// (set) Token: 0x06003A56 RID: 14934 RVA: 0x000BE82A File Offset: 0x000BCA2A
		public int OffsetXs { get; set; }

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x000BE833 File Offset: 0x000BCA33
		// (set) Token: 0x06003A58 RID: 14936 RVA: 0x000BE83B File Offset: 0x000BCA3B
		public int PushXs { get; set; }

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06003A59 RID: 14937 RVA: 0x000BE844 File Offset: 0x000BCA44
		// (set) Token: 0x06003A5A RID: 14938 RVA: 0x000BE84C File Offset: 0x000BCA4C
		public int PullXs { get; set; }

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x06003A5B RID: 14939 RVA: 0x000BE855 File Offset: 0x000BCA55
		// (set) Token: 0x06003A5C RID: 14940 RVA: 0x000BE85D File Offset: 0x000BCA5D
		public int OffsetSm { get; set; }

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x000BE866 File Offset: 0x000BCA66
		// (set) Token: 0x06003A5E RID: 14942 RVA: 0x000BE86E File Offset: 0x000BCA6E
		public int PushSm { get; set; }

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x06003A5F RID: 14943 RVA: 0x000BE877 File Offset: 0x000BCA77
		// (set) Token: 0x06003A60 RID: 14944 RVA: 0x000BE87F File Offset: 0x000BCA7F
		public int PullSm { get; set; }

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x06003A61 RID: 14945 RVA: 0x000BE888 File Offset: 0x000BCA88
		// (set) Token: 0x06003A62 RID: 14946 RVA: 0x000BE890 File Offset: 0x000BCA90
		public int OffsetMd { get; set; }

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x000BE899 File Offset: 0x000BCA99
		// (set) Token: 0x06003A64 RID: 14948 RVA: 0x000BE8A1 File Offset: 0x000BCAA1
		public int PushMd { get; set; }

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000BE8AA File Offset: 0x000BCAAA
		// (set) Token: 0x06003A66 RID: 14950 RVA: 0x000BE8B2 File Offset: 0x000BCAB2
		public int PullMd { get; set; }

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x000BE8BB File Offset: 0x000BCABB
		// (set) Token: 0x06003A68 RID: 14952 RVA: 0x000BE8C3 File Offset: 0x000BCAC3
		public int OffsetLg { get; set; }

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x000BE8CC File Offset: 0x000BCACC
		// (set) Token: 0x06003A6A RID: 14954 RVA: 0x000BE8D4 File Offset: 0x000BCAD4
		public int PushLg { get; set; }

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06003A6B RID: 14955 RVA: 0x000BE8DD File Offset: 0x000BCADD
		// (set) Token: 0x06003A6C RID: 14956 RVA: 0x000BE8E5 File Offset: 0x000BCAE5
		public int PullLg { get; set; }

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x000BE8EE File Offset: 0x000BCAEE
		// (set) Token: 0x06003A6E RID: 14958 RVA: 0x000BE8F6 File Offset: 0x000BCAF6
		public int OffsetXl { get; set; }

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x06003A6F RID: 14959 RVA: 0x000BE8FF File Offset: 0x000BCAFF
		// (set) Token: 0x06003A70 RID: 14960 RVA: 0x000BE907 File Offset: 0x000BCB07
		public int PushXl { get; set; }

		// Token: 0x1700133D RID: 4925
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x000BE910 File Offset: 0x000BCB10
		// (set) Token: 0x06003A72 RID: 14962 RVA: 0x000BE918 File Offset: 0x000BCB18
		public int PullXl { get; set; }

		// Token: 0x06003A73 RID: 14963 RVA: 0x000BE924 File Offset: 0x000BCB24
		protected virtual List<string> GetOffsetClassNames()
		{
			if (this.Push > -1 && this.Pull > -1)
			{
				throw new Exception("Can not set Push && Pull at the same time.");
			}
			List<string> list = new List<string>();
			if (this.Offset > -1)
			{
				list.Add("t-offset-" + this.Offset);
			}
			if (this.Push > -1)
			{
				list.Add("t-push-" + this.Push);
			}
			if (this.Pull > -1)
			{
				list.Add("t-pull-" + this.Pull);
			}
			return list;
		}

		// Token: 0x06003A74 RID: 14964 RVA: 0x000BE9C1 File Offset: 0x000BCBC1
		protected override List<string> GetTransformationClassNames()
		{
			return new List<string>().Concat(this.GetTransformationReflowClassNames()).Concat(base.GetTransformationClassNames()).ToList<string>();
		}

		// Token: 0x06003A75 RID: 14965 RVA: 0x000BE9E4 File Offset: 0x000BCBE4
		protected virtual List<string> GetTransformationReflowClassNames()
		{
			List<string> list = new List<string>();
			if (this._spanXs != 0)
			{
				list.Add("t-col-xs-" + this._spanXs);
			}
			if (this._spanSm != 0)
			{
				list.Add("t-col-sm-" + this._spanSm);
			}
			if (this._spanMd != 0)
			{
				list.Add("t-col-md-" + this._spanMd);
			}
			if (this._spanLg != 0)
			{
				list.Add("t-col-lg-" + this._spanLg);
			}
			if (this._spanXl != 0)
			{
				list.Add("t-col-xl-" + this._spanXl);
			}
			return list;
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000BEAA8 File Offset: 0x000BCCA8
		protected virtual List<string> GetTransformationOffsetClassNames()
		{
			if (this.PushXs > -1 && this.PullXs > -1)
			{
				throw new Exception("Can not set PushXs && PullXs");
			}
			if (this.PushSm > -1 && this.PullSm > -1)
			{
				throw new Exception("Can not set PushSm && PullSm");
			}
			if (this.PushMd > -1 && this.PullMd > -1)
			{
				throw new Exception("Can not set PushMd && PullMd");
			}
			if (this.PushLg > -1 && this.PullLg > -1)
			{
				throw new Exception("Can not set PushLg && PullLg");
			}
			if (this.PushXl > -1 && this.PullXl > -1)
			{
				throw new Exception("Can not set PushXl && PullXl");
			}
			List<string> list = new List<string>();
			if (this.OffsetXs > -1)
			{
				list.Add("t-offset-xs-" + this.OffsetXs);
			}
			if (this.OffsetSm > -1)
			{
				list.Add("t-offset-sm-" + this.OffsetSm);
			}
			if (this.OffsetMd > -1)
			{
				list.Add("t-offset-md-" + this.OffsetMd);
			}
			if (this.OffsetLg > -1)
			{
				list.Add("t-offset-lg-" + this.OffsetLg);
			}
			if (this.OffsetXl > -1)
			{
				list.Add("t-offset-xl-" + this.OffsetXl);
			}
			if (this.PushXs > -1)
			{
				list.Add("t-push-xs-" + this.PushXs);
			}
			if (this.PushSm > -1)
			{
				list.Add("t-push-sm-" + this.PushSm);
			}
			if (this.PushMd > -1)
			{
				list.Add("t-push-md-" + this.PushMd);
			}
			if (this.PushLg > -1)
			{
				list.Add("t-push-lg-" + this.PushLg);
			}
			if (this.PushXl > -1)
			{
				list.Add("t-push-xl-" + this.PushXl);
			}
			if (this.PullXs > -1)
			{
				list.Add("t-pull-xs-" + this.PullXs);
			}
			if (this.PullSm > -1)
			{
				list.Add("t-pull-sm-" + this.PullSm);
			}
			if (this.PullMd > -1)
			{
				list.Add("t-pull-md-" + this.PullMd);
			}
			if (this.PullLg > -1)
			{
				list.Add("t-pull-lg-" + this.PullLg);
			}
			if (this.PullXl > -1)
			{
				list.Add("t-pull-xl-" + this.PullXl);
			}
			return list;
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000BED6C File Offset: 0x000BCF6C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.CssClass;
			List<string> list = new List<string>
			{
				"t-col",
				string.Format("{0}{1}", "t-col-", this.Span)
			};
			list.AddRange(this.GetOffsetClassNames());
			list.AddRange(this.GetTransformationReflowClassNames());
			list.AddRange(this.GetTransformationOffsetClassNames());
			list.AddRange(base.GetTransformationToggleClassNames());
			list.AddRange(cssClass.Split(null, StringSplitOptions.RemoveEmptyEntries));
			this.CssClass = CssUtils.NormalizeClassNames(list);
			base.AddAttributesToRender(writer);
			this.CssClass = cssClass;
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x000BEE0C File Offset: 0x000BD00C
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			base.RenderBeginTag(writer);
			if (!base.DesignMode && this.Page.Request != null && this.Page.Request.Browser.Browser == "IE" && this.Page.Request.Browser.MajorVersion <= 7)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "t-col-inner");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x000BEE84 File Offset: 0x000BD084
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			if (!base.DesignMode && this.Page.Request != null && this.Page.Request.Browser.Browser == "IE" && this.Page.Request.Browser.MajorVersion <= 7)
			{
				writer.RenderEndTag();
			}
			base.RenderEndTag(writer);
		}

		// Token: 0x04000F90 RID: 3984
		private int _span = 12;

		// Token: 0x04000F91 RID: 3985
		private int _spanXs;

		// Token: 0x04000F92 RID: 3986
		private int _spanSm;

		// Token: 0x04000F93 RID: 3987
		private int _spanMd;

		// Token: 0x04000F94 RID: 3988
		private int _spanLg;

		// Token: 0x04000F95 RID: 3989
		private int _spanXl;
	}
}
