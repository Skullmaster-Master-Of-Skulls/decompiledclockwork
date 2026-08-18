using System;
using System.ComponentModel.Design;
using System.Design;
using System.Security.Permissions;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x0200006F RID: 111
	[Obsolete("Use of this type is not recommended because template editing is handled in ControlDesigner. To support template editing expose template data in the TemplateGroups property and call SetViewFlags(ViewFlags.TemplateEditing, true). http://go.microsoft.com/fwlink/?linkid=14202")]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public sealed class TemplateEditingService : ITemplateEditingService, IDisposable
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00011AF7 File Offset: 0x0000FCF7
		public TemplateEditingService(IDesignerHost designerHost)
		{
			if (designerHost == null)
			{
				throw new ArgumentNullException("designerHost");
			}
			this.designerHost = designerHost;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000445B File Offset: 0x0000265B
		public bool SupportsNestedTemplateEditing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00011B14 File Offset: 0x0000FD14
		public ITemplateEditingFrame CreateFrame(TemplatedControlDesigner designer, string frameName, string[] templateNames)
		{
			return this.CreateFrame(designer, frameName, templateNames, null, null);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00011B24 File Offset: 0x0000FD24
		public ITemplateEditingFrame CreateFrame(TemplatedControlDesigner designer, string frameName, string[] templateNames, Style controlStyle, Style[] templateStyles)
		{
			if (designer == null)
			{
				throw new ArgumentNullException("designer");
			}
			if (frameName == null || frameName.Length == 0)
			{
				throw new ArgumentNullException("frameName");
			}
			if (templateNames == null || templateNames.Length == 0)
			{
				throw new ArgumentException("templateNames");
			}
			if (templateStyles != null && templateStyles.Length != templateNames.Length)
			{
				throw new ArgumentException("templateStyles");
			}
			frameName = this.CreateFrameName(frameName);
			return new TemplateEditingFrame(designer, frameName, templateNames, controlStyle, templateStyles);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00011B94 File Offset: 0x0000FD94
		private string CreateFrameName(string frameName)
		{
			int num = frameName.IndexOf('&');
			if (num < 0)
			{
				return frameName;
			}
			if (num == 0)
			{
				return frameName.Substring(num + 1);
			}
			return frameName.Substring(0, num) + frameName.Substring(num + 1);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00011BD3 File Offset: 0x0000FDD3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00011BE4 File Offset: 0x0000FDE4
		~TemplateEditingService()
		{
			this.Dispose(false);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011C14 File Offset: 0x0000FE14
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.designerHost = null;
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00011C20 File Offset: 0x0000FE20
		public string GetContainingTemplateName(Control control)
		{
			string result = string.Empty;
			HtmlControlDesigner htmlControlDesigner = (HtmlControlDesigner)this.designerHost.GetDesigner(control);
			if (htmlControlDesigner != null)
			{
				IHtmlControlDesignerBehavior behaviorInternal = htmlControlDesigner.BehaviorInternal;
				NativeMethods.IHTMLElement ihtmlelement = (NativeMethods.IHTMLElement)behaviorInternal.DesignTimeElement;
				if (ihtmlelement != null)
				{
					object[] array = new object[1];
					NativeMethods.IHTMLElement parentElement;
					for (NativeMethods.IHTMLElement ihtmlelement2 = ihtmlelement.GetParentElement(); ihtmlelement2 != null; ihtmlelement2 = parentElement)
					{
						ihtmlelement2.GetAttribute("templatename", 0, array);
						if (array[0] != null && array[0].GetType() == typeof(string))
						{
							result = array[0].ToString();
							break;
						}
						parentElement = ihtmlelement2.GetParentElement();
					}
				}
			}
			return result;
		}

		// Token: 0x0400018B RID: 395
		private IDesignerHost designerHost;
	}
}
