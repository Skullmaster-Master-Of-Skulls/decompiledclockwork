using System;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram.DataBinding
{
	// Token: 0x02000229 RID: 553
	[ParseChildren(ChildrenAsProperties = true)]
	public class ConnectionSettings : BaseBindingSettings
	{
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00046837 File Offset: 0x00044A37
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x00046857 File Offset: 0x00044A57
		public string DataStartCapField
		{
			get
			{
				return (string)(base.ViewState["DataStartCapField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataStartCapField"] = value;
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0004686A File Offset: 0x00044A6A
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0004688A File Offset: 0x00044A8A
		public string DataEndCapField
		{
			get
			{
				return (string)(base.ViewState["DataEndCapField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataEndCapField"] = value;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0004689D File Offset: 0x00044A9D
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x000468BD File Offset: 0x00044ABD
		public string DataFromConnectorField
		{
			get
			{
				return (string)(base.ViewState["DataFromConnectorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFromConnectorField"] = value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x000468D0 File Offset: 0x00044AD0
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x000468F0 File Offset: 0x00044AF0
		public string DataToConnectorField
		{
			get
			{
				return (string)(base.ViewState["DataToConnectorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataToConnectorField"] = value;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00046903 File Offset: 0x00044B03
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x00046923 File Offset: 0x00044B23
		public string DataFromShapeIdField
		{
			get
			{
				return (string)(base.ViewState["DataFromShapeIdField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataFromShapeIdField"] = value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00046936 File Offset: 0x00044B36
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00046956 File Offset: 0x00044B56
		public string DataToShapeIdField
		{
			get
			{
				return (string)(base.ViewState["DataToShapeIdField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataToShapeIdField"] = value;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00046969 File Offset: 0x00044B69
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x00046989 File Offset: 0x00044B89
		public string DataStrokeColorField
		{
			get
			{
				return (string)(base.ViewState["DataStrokeColorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataStrokeColorField"] = value;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0004699C File Offset: 0x00044B9C
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x000469BC File Offset: 0x00044BBC
		public string DataHoverStrokeColorField
		{
			get
			{
				return (string)(base.ViewState["DataHoverStrokeColorField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataHoverStrokeColorField"] = value;
			}
		}
	}
}
