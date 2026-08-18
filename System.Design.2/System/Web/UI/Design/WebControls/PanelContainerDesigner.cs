using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000F3 RID: 243
	public class PanelContainerDesigner : ContainerControlDesigner
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0002FF88 File Offset: 0x0002E188
		internal override string DesignTimeHtml
		{
			get
			{
				if (this.FrameCaption.Length > 0)
				{
					return "<div style=\"{0}{2}{3}{4}{6}{10}\" class=\"{11}\">\r\n    <fieldset>\r\n        <legend>{5}</legend>\r\n        <div {7}=0></div>\r\n    </fieldset>\r\n</div>";
				}
				return "<div style=\"{0}{2}{3}{4}{6}{10}\" class=\"{11}\" {7}=0></div>";
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0002FFA3 File Offset: 0x0002E1A3
		public override string FrameCaption
		{
			get
			{
				return ((Panel)base.Component).GroupingText;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0002FFB5 File Offset: 0x0002E1B5
		public override Style FrameStyle
		{
			get
			{
				if (((Panel)base.Component).GroupingText.Length == 0)
				{
					return new Style();
				}
				return base.FrameStyle;
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002FFDC File Offset: 0x0002E1DC
		protected override void AddDesignTimeCssAttributes(IDictionary styleAttributes)
		{
			Panel panel = (Panel)base.Component;
			ContentDirection direction = panel.Direction;
			if (direction != ContentDirection.LeftToRight)
			{
				if (direction == ContentDirection.RightToLeft)
				{
					styleAttributes["direction"] = "rtl";
				}
			}
			else
			{
				styleAttributes["direction"] = "ltr";
			}
			string text = panel.BackImageUrl;
			if (text.Trim().Length > 0)
			{
				IUrlResolutionService urlResolutionService = (IUrlResolutionService)this.GetService(typeof(IUrlResolutionService));
				if (urlResolutionService != null)
				{
					text = urlResolutionService.ResolveClientUrl(text);
					styleAttributes["background-image"] = "url(" + text + ")";
				}
			}
			switch (panel.ScrollBars)
			{
			case ScrollBars.Horizontal:
				styleAttributes["overflow-x"] = "scroll";
				break;
			case ScrollBars.Vertical:
				styleAttributes["overflow-y"] = "scroll";
				break;
			case ScrollBars.Both:
				styleAttributes["overflow"] = "scroll";
				break;
			case ScrollBars.Auto:
				styleAttributes["overflow"] = "auto";
				break;
			}
			HorizontalAlign horizontalAlign = panel.HorizontalAlign;
			if (horizontalAlign != HorizontalAlign.NotSet)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(HorizontalAlign));
				styleAttributes["text-align"] = converter.ConvertToInvariantString(horizontalAlign).ToLowerInvariant();
			}
			if (!panel.Wrap)
			{
				styleAttributes["white-space"] = "nowrap";
			}
			base.AddDesignTimeCssAttributes(styleAttributes);
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0003013A File Offset: 0x0002E33A
		public override void Initialize(IComponent component)
		{
			ControlDesigner.VerifyInitializeArgument(component, typeof(Panel));
			base.Initialize(component);
		}

		// Token: 0x040004F1 RID: 1265
		private const string PanelWithCaptionDesignTimeHtml = "<div style=\"{0}{2}{3}{4}{6}{10}\" class=\"{11}\">\r\n    <fieldset>\r\n        <legend>{5}</legend>\r\n        <div {7}=0></div>\r\n    </fieldset>\r\n</div>";

		// Token: 0x040004F2 RID: 1266
		private const string PanelNoCaptionDesignTimeHtml = "<div style=\"{0}{2}{3}{4}{6}{10}\" class=\"{11}\" {7}=0></div>";
	}
}
