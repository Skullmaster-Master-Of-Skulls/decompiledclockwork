using System;
using System.ComponentModel;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000057 RID: 87
	public class SeriesHighlightAppearance : AppearanceBase, IJsConvertable, IDefaultCheck
	{
		// Token: 0x06000298 RID: 664 RVA: 0x0000717C File Offset: 0x0000537C
		public SeriesHighlightAppearance(string prefix, StateBag OwnerStateBag) : base("sha" + prefix, OwnerStateBag)
		{
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00007190 File Offset: 0x00005390
		// (set) Token: 0x0600029A RID: 666 RVA: 0x000071B1 File Offset: 0x000053B1
		[DefaultValue(true)]
		public override bool? Visible
		{
			get
			{
				return (bool?)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600029B RID: 667 RVA: 0x000071C9 File Offset: 0x000053C9
		// (set) Token: 0x0600029C RID: 668 RVA: 0x000071E9 File Offset: 0x000053E9
		[DefaultValue("")]
		public string Visual
		{
			get
			{
				return (string)(base.ViewState["Visual"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Visual"] = value;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600029D RID: 669 RVA: 0x000071FC File Offset: 0x000053FC
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000721C File Offset: 0x0000541C
		[DefaultValue("")]
		public string Toggle
		{
			get
			{
				return (string)(base.ViewState["Toggle"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Toggle"] = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000722F File Offset: 0x0000542F
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x00007250 File Offset: 0x00005450
		[DefaultValue(0)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Bindable(false)]
		public override int RotationAngle
		{
			get
			{
				return (int)(base.ViewState["RotationAngle"] ?? 0);
			}
			set
			{
				base.ViewState["RotationAngle"] = value;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00007268 File Offset: 0x00005468
		internal override string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder(", highlight: {");
			bool flag = this.Visible ?? false;
			string str = string.Empty;
			if (!flag)
			{
				stringBuilder.Append("visible: false");
				str = ", ";
			}
			if (!string.IsNullOrEmpty(this.Visual))
			{
				stringBuilder.AppendFormat(str + "visual: {0}", this.Visual);
				str = ", ";
			}
			if (!string.IsNullOrEmpty(this.Toggle))
			{
				stringBuilder.AppendFormat(str + "toggle: {0}", this.Toggle);
			}
			stringBuilder.Append("}");
			string text = stringBuilder.ToString();
			if (!(text != ", highlight: {}"))
			{
				return "";
			}
			return text;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000732E File Offset: 0x0000552E
		protected virtual void SerializeSharedProperties(StringBuilder sb)
		{
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007330 File Offset: 0x00005530
		public override void RegisterJSConverters(JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new SeriesHighlightConverter[]
			{
				new SeriesHighlightConverter()
			});
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00007354 File Offset: 0x00005554
		public override bool IsDefault
		{
			get
			{
				return this.Visible == null && this.RotationAngle == 0;
			}
		}
	}
}
