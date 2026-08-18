using System;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200001C RID: 28
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class ControlParser
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x0000362F File Offset: 0x0000182F
		private ControlParser()
		{
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00006D1C File Offset: 0x00004F1C
		public static Control ParseControl(IDesignerHost designerHost, string controlText)
		{
			if (designerHost == null)
			{
				throw new ArgumentNullException("designerHost");
			}
			if (controlText == null || controlText.Length == 0)
			{
				throw new ArgumentNullException("controlText");
			}
			return ControlSerializer.DeserializeControl(controlText, designerHost);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00006D49 File Offset: 0x00004F49
		internal static Control ParseControl(IDesignerHost designerHost, string controlText, bool applyTheme)
		{
			if (designerHost == null)
			{
				throw new ArgumentNullException("designerHost");
			}
			if (controlText == null || controlText.Length == 0)
			{
				throw new ArgumentNullException("controlText");
			}
			return ControlSerializer.DeserializeControlInternal(controlText, designerHost, applyTheme);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00006D78 File Offset: 0x00004F78
		public static Control ParseControl(IDesignerHost designerHost, string controlText, string directives)
		{
			if (designerHost == null)
			{
				throw new ArgumentNullException("designerHost");
			}
			if (controlText == null || controlText.Length == 0)
			{
				throw new ArgumentNullException("controlText");
			}
			if (directives != null && directives.Length != 0)
			{
				controlText = directives + controlText;
			}
			return ControlSerializer.DeserializeControl(controlText, designerHost);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00006DC4 File Offset: 0x00004FC4
		public static Control[] ParseControls(IDesignerHost designerHost, string controlText)
		{
			if (designerHost == null)
			{
				throw new ArgumentNullException("designerHost");
			}
			if (controlText == null || controlText.Length == 0)
			{
				throw new ArgumentNullException("controlText");
			}
			return ControlSerializer.DeserializeControls(controlText, designerHost);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006DF1 File Offset: 0x00004FF1
		public static ITemplate ParseTemplate(IDesignerHost designerHost, string templateText)
		{
			if (designerHost == null)
			{
				throw new ArgumentNullException("designerHost");
			}
			return ControlSerializer.DeserializeTemplate(templateText, designerHost);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006DF1 File Offset: 0x00004FF1
		public static ITemplate ParseTemplate(IDesignerHost designerHost, string templateText, string directives)
		{
			if (designerHost == null)
			{
				throw new ArgumentNullException("designerHost");
			}
			return ControlSerializer.DeserializeTemplate(templateText, designerHost);
		}
	}
}
