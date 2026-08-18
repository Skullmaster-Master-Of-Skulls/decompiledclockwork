using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000312 RID: 786
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ResourcesDataBindings : BaseDataBindings, IResourcesDataBindings
	{
		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001A95 RID: 6805 RVA: 0x00056A20 File Offset: 0x00054C20
		// (set) Token: 0x06001A96 RID: 6806 RVA: 0x00056A40 File Offset: 0x00054C40
		[RequiredProperty]
		[DefaultValue("")]
		public string IdField
		{
			get
			{
				return (string)(base.ViewState["IdField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["IdField"] = value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001A97 RID: 6807 RVA: 0x00056A53 File Offset: 0x00054C53
		// (set) Token: 0x06001A98 RID: 6808 RVA: 0x00056A73 File Offset: 0x00054C73
		[RequiredProperty]
		[DefaultValue("")]
		public string TextField
		{
			get
			{
				return (string)(base.ViewState["TextField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["TextField"] = value;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x00056A86 File Offset: 0x00054C86
		// (set) Token: 0x06001A9A RID: 6810 RVA: 0x00056AA6 File Offset: 0x00054CA6
		[DefaultValue("")]
		public string ColorField
		{
			get
			{
				return (string)(base.ViewState["ColorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ColorField"] = value;
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x00056AB9 File Offset: 0x00054CB9
		// (set) Token: 0x06001A9C RID: 6812 RVA: 0x00056AD9 File Offset: 0x00054CD9
		[DefaultValue("")]
		public string FormatField
		{
			get
			{
				return (string)(base.ViewState["FormatField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["FormatField"] = value;
			}
		}
	}
}
