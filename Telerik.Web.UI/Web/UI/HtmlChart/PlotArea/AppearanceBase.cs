using System;
using System.ComponentModel;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.HtmlChart.JavaScriptConverters;

namespace Telerik.Web.UI.HtmlChart.PlotArea
{
	// Token: 0x02000056 RID: 86
	public class AppearanceBase : ObjectWithState, IJsConvertable, IDefaultCheck
	{
		// Token: 0x06000290 RID: 656 RVA: 0x00007051 File Offset: 0x00005251
		public AppearanceBase(string key, StateBag OwnerStateBag) : base("aap" + key, OwnerStateBag)
		{
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00007065 File Offset: 0x00005265
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000707C File Offset: 0x0000527C
		[DefaultValue(null)]
		public virtual bool? Visible
		{
			get
			{
				return (bool?)base.ViewState["Visible"];
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00007094 File Offset: 0x00005294
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000070A2 File Offset: 0x000052A2
		[DefaultValue(0)]
		public virtual int RotationAngle
		{
			get
			{
				return base.GetViewStateValue<int>("RotationAngle", 0);
			}
			set
			{
				base.ViewState["RotationAngle"] = value;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000070BC File Offset: 0x000052BC
		internal virtual string Serialize()
		{
			StringBuilder stringBuilder = new StringBuilder().AppendFormat("visible: {0}", (this.Visible == true) ? "true" : "false");
			if (this.RotationAngle != 0)
			{
				stringBuilder.Append(", rotation: ").Append(this.RotationAngle.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00007130 File Offset: 0x00005330
		public virtual void RegisterJSConverters(JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new AppearanceBaseConverter[]
			{
				new AppearanceBaseConverter()
			});
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00007154 File Offset: 0x00005354
		public virtual bool IsDefault
		{
			get
			{
				return this.Visible == null && this.RotationAngle == 0;
			}
		}
	}
}
