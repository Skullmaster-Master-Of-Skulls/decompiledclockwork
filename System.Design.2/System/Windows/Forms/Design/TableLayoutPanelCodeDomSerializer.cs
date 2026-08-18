using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200033F RID: 831
	internal class TableLayoutPanelCodeDomSerializer : CodeDomSerializer
	{
		// Token: 0x060020AD RID: 8365 RVA: 0x000C690C File Offset: 0x000C4B0C
		public override object Deserialize(IDesignerSerializationManager manager, object codeObject)
		{
			return this.GetBaseSerializer(manager).Deserialize(manager, codeObject);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000C691C File Offset: 0x000C4B1C
		private CodeDomSerializer GetBaseSerializer(IDesignerSerializationManager manager)
		{
			return (CodeDomSerializer)manager.GetSerializer(typeof(TableLayoutPanel).BaseType, typeof(CodeDomSerializer));
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x000C6944 File Offset: 0x000C4B44
		public override object Serialize(IDesignerSerializationManager manager, object value)
		{
			object result = this.GetBaseSerializer(manager).Serialize(manager, value);
			TableLayoutPanel tableLayoutPanel = value as TableLayoutPanel;
			if (tableLayoutPanel != null)
			{
				InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(tableLayoutPanel)[typeof(InheritanceAttribute)];
				if (inheritanceAttribute == null || inheritanceAttribute.InheritanceLevel != InheritanceLevel.InheritedReadOnly)
				{
					IDesignerHost host = (IDesignerHost)manager.GetService(typeof(IDesignerHost));
					if (this.IsLocalizable(host))
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(tableLayoutPanel)[TableLayoutPanelCodeDomSerializer.LayoutSettingsPropName];
						object obj = (propertyDescriptor != null) ? propertyDescriptor.GetValue(tableLayoutPanel) : null;
						if (obj != null)
						{
							string resourceName = manager.GetName(tableLayoutPanel) + "." + TableLayoutPanelCodeDomSerializer.LayoutSettingsPropName;
							base.SerializeResourceInvariant(manager, resourceName, obj);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x000C6A00 File Offset: 0x000C4C00
		private bool IsLocalizable(IDesignerHost host)
		{
			if (host != null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(host.RootComponent)["Localizable"];
				if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(bool))
				{
					return (bool)propertyDescriptor.GetValue(host.RootComponent);
				}
			}
			return false;
		}

		// Token: 0x040018FD RID: 6397
		private static readonly string LayoutSettingsPropName = "LayoutSettings";
	}
}
