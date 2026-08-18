using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace System.ComponentModel.Design
{
	// Token: 0x020005D8 RID: 1496
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class DesignerVerb : MenuCommand
	{
		// Token: 0x060037A5 RID: 14245 RVA: 0x000F0AEB File Offset: 0x000EECEB
		public DesignerVerb(string text, EventHandler handler) : base(handler, StandardCommands.VerbFirst)
		{
			this.Properties["Text"] = ((text == null) ? null : Regex.Replace(text, "\\(\\&.\\)", ""));
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000F0B1F File Offset: 0x000EED1F
		public DesignerVerb(string text, EventHandler handler, CommandID startCommandID) : base(handler, startCommandID)
		{
			this.Properties["Text"] = ((text == null) ? null : Regex.Replace(text, "\\(\\&.\\)", ""));
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x060037A7 RID: 14247 RVA: 0x000F0B50 File Offset: 0x000EED50
		// (set) Token: 0x060037A8 RID: 14248 RVA: 0x000F0B7D File Offset: 0x000EED7D
		public string Description
		{
			get
			{
				object obj = this.Properties["Description"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.Properties["Description"] = value;
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x060037A9 RID: 14249 RVA: 0x000F0B90 File Offset: 0x000EED90
		public string Text
		{
			get
			{
				object obj = this.Properties["Text"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
		}

		// Token: 0x060037AA RID: 14250 RVA: 0x000F0BBD File Offset: 0x000EEDBD
		public override string ToString()
		{
			return this.Text + " : " + base.ToString();
		}
	}
}
