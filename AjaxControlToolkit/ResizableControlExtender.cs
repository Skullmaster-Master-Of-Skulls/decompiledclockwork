using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x02000180 RID: 384
	[TargetControlType(typeof(HtmlControl))]
	[Designer(typeof(ResizableControlExtenderDesigner))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.ResizableControlBehavior", "ResizableControl")]
	[ToolboxBitmap(typeof(Accessor), "ResizableControl.bmp")]
	[TargetControlType(typeof(WebControl))]
	public class ResizableControlExtender : ExtenderControlBase
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x0001B9B9 File Offset: 0x00019BB9
		public ResizableControlExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x0001B9C8 File Offset: 0x00019BC8
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x0001B9DA File Offset: 0x00019BDA
		[ExtenderControlProperty]
		[ClientPropertyName("handleCssClass")]
		[DefaultValue("")]
		[RequiredProperty]
		public string HandleCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("HandleCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("HandleCssClass", value);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x0001B9E8 File Offset: 0x00019BE8
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0001B9FA File Offset: 0x00019BFA
		[ExtenderControlProperty]
		[ClientPropertyName("resizableCssClass")]
		[DefaultValue("")]
		public string ResizableCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("ResizableCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ResizableCssClass", value);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0001BA08 File Offset: 0x00019C08
		// (set) Token: 0x06000AA3 RID: 2723 RVA: 0x0001BA16 File Offset: 0x00019C16
		[ClientPropertyName("handleOffsetX")]
		[ExtenderControlProperty]
		[DefaultValue(0)]
		public int HandleOffsetX
		{
			get
			{
				return base.GetPropertyValue<int>("HandleOffsetX", 0);
			}
			set
			{
				base.SetPropertyValue<int>("HandleOffsetX", value);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x0001BA24 File Offset: 0x00019C24
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0001BA32 File Offset: 0x00019C32
		[ClientPropertyName("handleOffsetY")]
		[DefaultValue(0)]
		[ExtenderControlProperty]
		public int HandleOffsetY
		{
			get
			{
				return base.GetPropertyValue<int>("HandleOffsetY", 0);
			}
			set
			{
				base.SetPropertyValue<int>("HandleOffsetY", value);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0001BA40 File Offset: 0x00019C40
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0001BA4E File Offset: 0x00019C4E
		[ClientPropertyName("minimumWidth")]
		[DefaultValue(0)]
		[ExtenderControlProperty]
		public int MinimumWidth
		{
			get
			{
				return base.GetPropertyValue<int>("MinimumWidth", 0);
			}
			set
			{
				base.SetPropertyValue<int>("MinimumWidth", value);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0001BA5C File Offset: 0x00019C5C
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x0001BA6A File Offset: 0x00019C6A
		[DefaultValue(0)]
		[ClientPropertyName("minimumHeight")]
		[ExtenderControlProperty]
		public int MinimumHeight
		{
			get
			{
				return base.GetPropertyValue<int>("MinimumHeight", 0);
			}
			set
			{
				base.SetPropertyValue<int>("MinimumHeight", value);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0001BA78 File Offset: 0x00019C78
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x0001BA8A File Offset: 0x00019C8A
		[ExtenderControlProperty]
		[DefaultValue(100000)]
		[ClientPropertyName("maximumWidth")]
		public int MaximumWidth
		{
			get
			{
				return base.GetPropertyValue<int>("MaximumWidth", 100000);
			}
			set
			{
				base.SetPropertyValue<int>("MaximumWidth", value);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0001BA98 File Offset: 0x00019C98
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0001BAAA File Offset: 0x00019CAA
		[ClientPropertyName("maximumHeight")]
		[ExtenderControlProperty]
		[DefaultValue(100000)]
		public int MaximumHeight
		{
			get
			{
				return base.GetPropertyValue<int>("MaximumHeight", 100000);
			}
			set
			{
				base.SetPropertyValue<int>("MaximumHeight", value);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0001BAB8 File Offset: 0x00019CB8
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0001BACA File Offset: 0x00019CCA
		[DefaultValue("")]
		[ClientPropertyName("resize")]
		[ExtenderControlProperty]
		public string OnClientResize
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientResize", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientResize", value);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0001BAD8 File Offset: 0x00019CD8
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0001BAEA File Offset: 0x00019CEA
		[DefaultValue("")]
		[ExtenderControlProperty]
		[ClientPropertyName("resizing")]
		public string OnClientResizing
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientResizing", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientResizing", value);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x0001BAF8 File Offset: 0x00019CF8
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x0001BB0A File Offset: 0x00019D0A
		[ClientPropertyName("resizeBegin")]
		[DefaultValue("")]
		[ExtenderControlProperty]
		public string OnClientResizeBegin
		{
			get
			{
				return base.GetPropertyValue<string>("OnClientResizeBegin", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("OnClientResizeBegin", value);
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0001BB18 File Offset: 0x00019D18
		public override void EnsureValid()
		{
			base.EnsureValid();
			if (this.MaximumWidth < this.MinimumWidth)
			{
				throw new ArgumentException("Maximum width must not be less than minimum width");
			}
			if (this.MaximumHeight < this.MinimumHeight)
			{
				throw new ArgumentException("Maximum height must not be less than minimum height");
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0001BB54 File Offset: 0x00019D54
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x0001BBD0 File Offset: 0x00019DD0
		[ClientPropertyName("size")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Size Size
		{
			get
			{
				string[] array = (base.ClientState ?? string.Empty).Split(new char[]
				{
					','
				});
				int width;
				int height;
				if (array.Length < 2 || string.IsNullOrEmpty(array[0]) || string.IsNullOrEmpty(array[1]) || !int.TryParse(array[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out width) || !int.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out height))
				{
					return Size.Empty;
				}
				return new Size(width, height);
			}
			set
			{
				base.ClientState = string.Format(CultureInfo.InvariantCulture, "{0},{1}", new object[]
				{
					value.Width,
					value.Height
				});
			}
		}

		// Token: 0x0400040D RID: 1037
		private const int MaximumValue = 100000;
	}
}
