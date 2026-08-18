using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x020005C5 RID: 1477
	[AttributeUsage(AttributeTargets.All)]
	public class ToolboxItemAttribute : Attribute
	{
		// Token: 0x06003746 RID: 14150 RVA: 0x000F038F File Offset: 0x000EE58F
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ToolboxItemAttribute.Default);
		}

		// Token: 0x06003747 RID: 14151 RVA: 0x000F039C File Offset: 0x000EE59C
		public ToolboxItemAttribute(bool defaultType)
		{
			if (defaultType)
			{
				this.toolboxItemTypeName = "System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
			}
		}

		// Token: 0x06003748 RID: 14152 RVA: 0x000F03B4 File Offset: 0x000EE5B4
		public ToolboxItemAttribute(string toolboxItemTypeName)
		{
			string text = toolboxItemTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.toolboxItemTypeName = toolboxItemTypeName;
		}

		// Token: 0x06003749 RID: 14153 RVA: 0x000F03DA File Offset: 0x000EE5DA
		public ToolboxItemAttribute(Type toolboxItemType)
		{
			this.toolboxItemType = toolboxItemType;
			this.toolboxItemTypeName = toolboxItemType.AssemblyQualifiedName;
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x0600374A RID: 14154 RVA: 0x000F03F8 File Offset: 0x000EE5F8
		public Type ToolboxItemType
		{
			get
			{
				if (this.toolboxItemType == null && this.toolboxItemTypeName != null)
				{
					try
					{
						this.toolboxItemType = Type.GetType(this.toolboxItemTypeName, true);
					}
					catch (Exception innerException)
					{
						throw new ArgumentException(SR.GetString("ToolboxItemAttributeFailedGetType", new object[]
						{
							this.toolboxItemTypeName
						}), innerException);
					}
				}
				return this.toolboxItemType;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x0600374B RID: 14155 RVA: 0x000F0468 File Offset: 0x000EE668
		public string ToolboxItemTypeName
		{
			get
			{
				if (this.toolboxItemTypeName == null)
				{
					return string.Empty;
				}
				return this.toolboxItemTypeName;
			}
		}

		// Token: 0x0600374C RID: 14156 RVA: 0x000F0480 File Offset: 0x000EE680
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemAttribute toolboxItemAttribute = obj as ToolboxItemAttribute;
			return toolboxItemAttribute != null && toolboxItemAttribute.ToolboxItemTypeName == this.ToolboxItemTypeName;
		}

		// Token: 0x0600374D RID: 14157 RVA: 0x000F04B0 File Offset: 0x000EE6B0
		public override int GetHashCode()
		{
			if (this.toolboxItemTypeName != null)
			{
				return this.toolboxItemTypeName.GetHashCode();
			}
			return base.GetHashCode();
		}

		// Token: 0x04002AE6 RID: 10982
		private Type toolboxItemType;

		// Token: 0x04002AE7 RID: 10983
		private string toolboxItemTypeName;

		// Token: 0x04002AE8 RID: 10984
		public static readonly ToolboxItemAttribute Default = new ToolboxItemAttribute("System.Drawing.Design.ToolboxItem, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");

		// Token: 0x04002AE9 RID: 10985
		public static readonly ToolboxItemAttribute None = new ToolboxItemAttribute(false);
	}
}
