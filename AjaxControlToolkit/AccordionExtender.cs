using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using AjaxControlToolkit.Design;

namespace AjaxControlToolkit
{
	// Token: 0x02000008 RID: 8
	[ClientScriptResource("Sys.Extended.UI.AccordionBehavior", "Accordion")]
	[RequiredScript(typeof(AnimationScripts))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ToolboxItem(false)]
	[Designer(typeof(AccordionExtenderDesigner))]
	[TargetControlType(typeof(Accordion))]
	public class AccordionExtender : ExtenderControlBase
	{
		// Token: 0x06000079 RID: 121 RVA: 0x0000370A File Offset: 0x0000190A
		public AccordionExtender()
		{
			base.EnableClientState = true;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003719 File Offset: 0x00001919
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00003727 File Offset: 0x00001927
		[ClientPropertyName("autoSize")]
		[ExtenderControlProperty]
		[DefaultValue(AutoSize.None)]
		public AutoSize AutoSize
		{
			get
			{
				return base.GetPropertyValue<AutoSize>("AutoSize", AutoSize.None);
			}
			set
			{
				base.SetPropertyValue<AutoSize>("AutoSize", value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003735 File Offset: 0x00001935
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003747 File Offset: 0x00001947
		[ClientPropertyName("transitionDuration")]
		[ExtenderControlProperty]
		[DefaultValue(250)]
		public int TransitionDuration
		{
			get
			{
				return base.GetPropertyValue<int>("TransitionDuration", 250);
			}
			set
			{
				base.SetPropertyValue<int>("TransitionDuration", value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003755 File Offset: 0x00001955
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003763 File Offset: 0x00001963
		[ExtenderControlProperty]
		[DefaultValue(false)]
		[ClientPropertyName("fadeTransitions")]
		public bool FadeTransitions
		{
			get
			{
				return base.GetPropertyValue<bool>("FadeTransitions", false);
			}
			set
			{
				base.SetPropertyValue<bool>("FadeTransitions", value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003771 File Offset: 0x00001971
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00003780 File Offset: 0x00001980
		[ClientPropertyName("framesPerSecond")]
		[ExtenderControlProperty]
		[DefaultValue(30)]
		public int FramesPerSecond
		{
			get
			{
				return base.GetPropertyValue<int>("FramesPerSecond", 30);
			}
			set
			{
				base.SetPropertyValue<int>("FramesPerSecond", value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003790 File Offset: 0x00001990
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000037C2 File Offset: 0x000019C2
		[ClientPropertyName("selectedIndex")]
		[ExtenderControlProperty]
		[DefaultValue(0)]
		public int SelectedIndex
		{
			get
			{
				int result;
				if (string.IsNullOrEmpty(base.ClientState) || !int.TryParse(base.ClientState, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
				{
					return 0;
				}
				return result;
			}
			set
			{
				base.ClientState = value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000037D6 File Offset: 0x000019D6
		// (set) Token: 0x06000085 RID: 133 RVA: 0x000037E4 File Offset: 0x000019E4
		[ExtenderControlProperty]
		[ClientPropertyName("requireOpenedPane")]
		[DefaultValue(true)]
		public bool RequireOpenedPane
		{
			get
			{
				return base.GetPropertyValue<bool>("RequireOpenedPane", true);
			}
			set
			{
				base.SetPropertyValue<bool>("RequireOpenedPane", value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000037F2 File Offset: 0x000019F2
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003800 File Offset: 0x00001A00
		[ClientPropertyName("suppressHeaderPostbacks")]
		[ExtenderControlProperty]
		[DefaultValue(false)]
		public bool SuppressHeaderPostbacks
		{
			get
			{
				return base.GetPropertyValue<bool>("SuppressHeaderPostbacks", false);
			}
			set
			{
				base.SetPropertyValue<bool>("SuppressHeaderPostbacks", value);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000380E File Offset: 0x00001A0E
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003820 File Offset: 0x00001A20
		[ExtenderControlProperty]
		[ClientPropertyName("headerCssClass")]
		[DefaultValue("")]
		public string HeaderCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("HeaderCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("HeaderCssClass", value);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000382E File Offset: 0x00001A2E
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003840 File Offset: 0x00001A40
		[ExtenderControlProperty]
		[DefaultValue("")]
		[ClientPropertyName("headerSelectedCssClass")]
		public string HeaderSelectedCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("HeaderSelectedCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("HeaderSelectedCssClass", value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000384E File Offset: 0x00001A4E
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003860 File Offset: 0x00001A60
		[DefaultValue("")]
		public string ContentCssClass
		{
			get
			{
				return base.GetPropertyValue<string>("ContentCssClass", string.Empty);
			}
			set
			{
				base.SetPropertyValue<string>("ContentCssClass", value);
			}
		}
	}
}
