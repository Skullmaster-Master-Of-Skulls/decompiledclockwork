using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000041 RID: 65
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	internal sealed class GlobalDataBindingHandler
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000362F File Offset: 0x0000182F
		private GlobalDataBindingHandler()
		{
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000F258 File Offset: 0x0000D458
		private static Hashtable DataBindingHandlerTable
		{
			get
			{
				if (GlobalDataBindingHandler.dataBindingHandlerTable == null)
				{
					GlobalDataBindingHandler.dataBindingHandlerTable = new Hashtable();
				}
				return GlobalDataBindingHandler.dataBindingHandlerTable;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F270 File Offset: 0x0000D470
		public static void OnDataBind(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			IDataBindingsAccessor dataBindingsAccessor = (IDataBindingsAccessor)sender;
			if (!dataBindingsAccessor.HasDataBindings)
			{
				return;
			}
			DataBindingHandlerAttribute dataBindingHandlerAttribute = (DataBindingHandlerAttribute)TypeDescriptor.GetAttributes(sender)[typeof(DataBindingHandlerAttribute)];
			if (dataBindingHandlerAttribute == null || dataBindingHandlerAttribute.HandlerTypeName.Length == 0)
			{
				return;
			}
			ISite site = control.Site;
			IDesignerHost designerHost = null;
			if (site == null)
			{
				Page page = control.Page;
				if (page != null)
				{
					site = page.Site;
				}
				else
				{
					Control parent = control.Parent;
					while (site == null && parent != null)
					{
						if (parent.Site != null)
						{
							site = parent.Site;
						}
						parent = parent.Parent;
					}
				}
			}
			if (site != null)
			{
				designerHost = (IDesignerHost)site.GetService(typeof(IDesignerHost));
			}
			if (designerHost == null)
			{
				return;
			}
			IDesigner designer = designerHost.GetDesigner(control);
			if (designer != null)
			{
				return;
			}
			DataBindingHandler dataBindingHandler = null;
			try
			{
				string handlerTypeName = dataBindingHandlerAttribute.HandlerTypeName;
				dataBindingHandler = (DataBindingHandler)GlobalDataBindingHandler.DataBindingHandlerTable[handlerTypeName];
				if (dataBindingHandler == null)
				{
					Type type = Type.GetType(handlerTypeName);
					if (type != null)
					{
						dataBindingHandler = (DataBindingHandler)Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, null, null, null);
						GlobalDataBindingHandler.DataBindingHandlerTable[handlerTypeName] = dataBindingHandler;
					}
				}
			}
			catch (Exception ex)
			{
				return;
			}
			if (dataBindingHandler != null)
			{
				dataBindingHandler.DataBindControl(designerHost, control);
			}
		}

		// Token: 0x04000158 RID: 344
		public static readonly EventHandler Handler = new EventHandler(GlobalDataBindingHandler.OnDataBind);

		// Token: 0x04000159 RID: 345
		private static Hashtable dataBindingHandlerTable;
	}
}
