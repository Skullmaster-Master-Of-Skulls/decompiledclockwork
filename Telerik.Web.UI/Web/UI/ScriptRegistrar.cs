using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000F55 RID: 3925
	internal class ScriptRegistrar
	{
		// Token: 0x060095AC RID: 38316 RVA: 0x00216B1C File Offset: 0x00214D1C
		public static string GetClientControlType(Control control)
		{
			Attribute attribute = TypeDescriptor.GetAttributes(control)[typeof(ClientScriptResourceAttribute)];
			return ((ClientScriptResourceAttribute)attribute).ComponentType;
		}

		// Token: 0x060095AD RID: 38317 RVA: 0x00216B4C File Offset: 0x00214D4C
		public static ScriptManager GetScriptManager(Control control)
		{
			if (control.Page == null)
			{
				throw new InvalidOperationException("Page cannot be null. Please ensure that this operation is being performed in the context of an ASP.NET request.");
			}
			ScriptManager scriptManager = ScriptManager.GetCurrent(control.Page);
			IControl control2 = control as IControl;
			if (scriptManager == null && control2 != null && !control2.RegisterWithScriptManager)
			{
				scriptManager = new ScriptManager
				{
					ID = "dummyScriptManager"
				};
			}
			if (scriptManager == null)
			{
				throw new InvalidOperationException(string.Format("The control with ID '{0}' requires a ScriptManager on the page. The ScriptManager must appear before any controls that need it.", control.ID));
			}
			return scriptManager;
		}

		// Token: 0x060095AE RID: 38318 RVA: 0x00216BBA File Offset: 0x00214DBA
		public static List<ScriptDescriptor> GetScriptDescriptors(WebControl control)
		{
			return ScriptRegistrar.GetScriptDescriptors(control);
		}

		// Token: 0x060095AF RID: 38319 RVA: 0x00216BC4 File Offset: 0x00214DC4
		public static List<ScriptDescriptor> GetScriptDescriptors(Control control)
		{
			string clientControlType = ScriptRegistrar.GetClientControlType(control);
			if (clientControlType == null)
			{
				return new List<ScriptDescriptor>(new ScriptDescriptor[0]);
			}
			RadControlScriptDescriptor radControlScriptDescriptor = new RadControlScriptDescriptor(clientControlType, control.ClientID);
			((IControl)control).DescribeComponent(radControlScriptDescriptor);
			return new List<ScriptDescriptor>(new ScriptDescriptor[]
			{
				radControlScriptDescriptor
			});
		}

		// Token: 0x060095B0 RID: 38320 RVA: 0x00216C11 File Offset: 0x00214E11
		public static IEnumerable<ScriptReference> GetScriptReferences(Type type)
		{
			return ScriptRegistrar.GetScriptReferences(type, true);
		}

		// Token: 0x060095B1 RID: 38321 RVA: 0x00216C1C File Offset: 0x00214E1C
		public static IEnumerable<ScriptReference> GetScriptReferences(Control ctrl)
		{
			if (ctrl == null)
			{
				return null;
			}
			bool enableEmbeddedjQuery = true;
			RadScriptManager radScriptManager = ScriptManager.GetCurrent(ctrl.Page) as RadScriptManager;
			if (radScriptManager != null)
			{
				enableEmbeddedjQuery = radScriptManager.EnableEmbeddedjQuery;
			}
			return ScriptRegistrar.GetScriptReferences(ctrl.GetType(), enableEmbeddedjQuery);
		}

		// Token: 0x060095B2 RID: 38322 RVA: 0x00216C58 File Offset: 0x00214E58
		public static IEnumerable<ScriptReference> GetScriptReferences(Type type, bool enableEmbeddedjQuery)
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(ScriptObjectBuilder.GetScriptReferences(type));
			if (!enableEmbeddedjQuery)
			{
				ScriptRegistrar.DisableEmbeddedjQuery(list);
			}
			return list;
		}

		// Token: 0x060095B3 RID: 38323 RVA: 0x00216CA4 File Offset: 0x00214EA4
		private static void DisableEmbeddedjQuery(List<ScriptReference> scriptReferences)
		{
			string fullName = typeof(ScriptRegistrar).Assembly.FullName;
			ClientScriptResourceAttribute jQuery = (ClientScriptResourceAttribute)Attribute.GetCustomAttributes(typeof(jQuery), typeof(ClientScriptResourceAttribute), false)[0];
			ClientScriptResourceAttribute clientScriptResourceAttribute = (ClientScriptResourceAttribute)Attribute.GetCustomAttributes(typeof(jQueryExternal), typeof(ClientScriptResourceAttribute), false)[0];
			ScriptReference item = scriptReferences.Find((ScriptReference scriptRef) => scriptRef.Name == jQuery.ResourcePath);
			int num = scriptReferences.IndexOf(item);
			if (num < 0)
			{
				return;
			}
			scriptReferences.Remove(item);
			scriptReferences.Insert(num, new ScriptReference(clientScriptResourceAttribute.ResourcePath, fullName));
			for (int i = scriptReferences.Count - 1; i >= 0; i--)
			{
				ScriptReference scriptReference = scriptReferences[i];
				if (scriptReference.Name == jQuery.ResourcePath && scriptReference.Assembly == fullName)
				{
					scriptReferences.RemoveAt(i);
				}
			}
		}

		// Token: 0x060095B4 RID: 38324 RVA: 0x00216DA5 File Offset: 0x00214FA5
		public static void RegisterCssReferences(Control control)
		{
			ScriptObjectBuilder.RegisterCssReferences(control);
		}
	}
}
