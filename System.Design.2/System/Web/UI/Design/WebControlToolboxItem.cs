using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing.Design;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.UI.Design
{
	// Token: 0x02000086 RID: 134
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	[Serializable]
	public class WebControlToolboxItem : ToolboxItem
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x00013287 File Offset: 0x00011487
		public WebControlToolboxItem()
		{
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013296 File Offset: 0x00011496
		public WebControlToolboxItem(Type type) : base(type)
		{
			this.BuildMetadataCache(type);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000132AD File Offset: 0x000114AD
		protected WebControlToolboxItem(SerializationInfo info, StreamingContext context)
		{
			this.Deserialize(info, context);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000132C4 File Offset: 0x000114C4
		private void BuildMetadataCache(Type type)
		{
			this.toolData = WebControlToolboxItem.ExtractToolboxData(type);
			this.persistChildren = WebControlToolboxItem.ExtractPersistChildrenAttribute(type);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000132DE File Offset: 0x000114DE
		protected override IComponent[] CreateComponentsCore(IDesignerHost host)
		{
			throw new Exception(SR.GetString("Toolbox_OnWebformsPage"));
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000132EF File Offset: 0x000114EF
		protected override void Deserialize(SerializationInfo info, StreamingContext context)
		{
			base.Deserialize(info, context);
			this.toolData = info.GetString("ToolData");
			this.persistChildren = info.GetInt32("PersistChildren");
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001331C File Offset: 0x0001151C
		private static int ExtractPersistChildrenAttribute(Type type)
		{
			if (type != null)
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(PersistChildrenAttribute), true);
				if (customAttributes != null && customAttributes.Length == 1)
				{
					PersistChildrenAttribute persistChildrenAttribute = (PersistChildrenAttribute)customAttributes[0];
					if (!persistChildrenAttribute.Persist)
					{
						return 0;
					}
					return 1;
				}
			}
			if (!PersistChildrenAttribute.Default.Persist)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00013374 File Offset: 0x00011574
		private static string ExtractToolboxData(Type type)
		{
			string result = string.Empty;
			if (type != null)
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(ToolboxDataAttribute), false);
				if (customAttributes != null && customAttributes.Length == 1)
				{
					ToolboxDataAttribute toolboxDataAttribute = (ToolboxDataAttribute)customAttributes[0];
					result = toolboxDataAttribute.Data;
				}
				else
				{
					string name = type.Name;
					result = string.Concat(new string[]
					{
						"<{0}:",
						name,
						" runat=\"server\"></{0}:",
						name,
						">"
					});
				}
			}
			return result;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000133F4 File Offset: 0x000115F4
		public object GetToolAttributeValue(IDesignerHost host, Type attributeType)
		{
			if (attributeType == typeof(PersistChildrenAttribute))
			{
				if (this.persistChildren == -1)
				{
					Type toolType = this.GetToolType(host);
					this.persistChildren = WebControlToolboxItem.ExtractPersistChildrenAttribute(toolType);
				}
				return this.persistChildren == 1;
			}
			throw new ArgumentException(SR.GetString("Toolbox_BadAttributeType"));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00013454 File Offset: 0x00011654
		public string GetToolHtml(IDesignerHost host)
		{
			if (this.toolData != null)
			{
				return this.toolData;
			}
			Type toolType = this.GetToolType(host);
			this.toolData = WebControlToolboxItem.ExtractToolboxData(toolType);
			return this.toolData;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001348A File Offset: 0x0001168A
		public Type GetToolType(IDesignerHost host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			return this.GetType(host, base.AssemblyName, base.TypeName, true);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000134AE File Offset: 0x000116AE
		public override void Initialize(Type type)
		{
			base.Initialize(type);
			this.BuildMetadataCache(type);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000134BE File Offset: 0x000116BE
		protected override void Serialize(SerializationInfo info, StreamingContext context)
		{
			base.Serialize(info, context);
			info.AddValue("ToolData", this.toolData);
			info.AddValue("PersistChildren", this.persistChildren);
		}

		// Token: 0x040001B1 RID: 433
		private string toolData;

		// Token: 0x040001B2 RID: 434
		private int persistChildren = -1;
	}
}
