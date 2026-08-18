using System;
using System.ComponentModel.Design;
using System.IO;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x0200001D RID: 29
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class ControlPersister
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000362F File Offset: 0x0000182F
		private ControlPersister()
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006E08 File Offset: 0x00005008
		public static string PersistInnerProperties(object component, IDesignerHost host)
		{
			return ControlSerializer.SerializeInnerProperties(component, host);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006E11 File Offset: 0x00005011
		public static void PersistInnerProperties(TextWriter sw, object component, IDesignerHost host)
		{
			ControlSerializer.SerializeInnerProperties(component, host, sw);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006E1B File Offset: 0x0000501B
		public static string PersistControl(Control control)
		{
			return ControlSerializer.SerializeControl(control);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00006E23 File Offset: 0x00005023
		public static string PersistControl(Control control, IDesignerHost host)
		{
			return ControlSerializer.SerializeControl(control, host);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00006E2C File Offset: 0x0000502C
		public static void PersistControl(TextWriter sw, Control control)
		{
			ControlSerializer.SerializeControl(control, sw);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006E35 File Offset: 0x00005035
		public static void PersistControl(TextWriter sw, Control control, IDesignerHost host)
		{
			ControlSerializer.SerializeControl(control, host, sw);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006E3F File Offset: 0x0000503F
		public static string PersistTemplate(ITemplate template, IDesignerHost host)
		{
			return ControlSerializer.SerializeTemplate(template, host);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006E48 File Offset: 0x00005048
		public static void PersistTemplate(TextWriter writer, ITemplate template, IDesignerHost host)
		{
			ControlSerializer.SerializeTemplate(template, writer, host);
		}
	}
}
