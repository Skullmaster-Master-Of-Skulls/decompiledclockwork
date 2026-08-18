using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x0200054B RID: 1355
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public sealed class EditorAttribute : Attribute
	{
		// Token: 0x060032F0 RID: 13040 RVA: 0x000E2D75 File Offset: 0x000E0F75
		public EditorAttribute()
		{
			this.typeName = string.Empty;
			this.baseTypeName = string.Empty;
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000E2D94 File Offset: 0x000E0F94
		public EditorAttribute(string typeName, string baseTypeName)
		{
			string text = typeName.ToUpper(CultureInfo.InvariantCulture);
			this.typeName = typeName;
			this.baseTypeName = baseTypeName;
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000E2DC4 File Offset: 0x000E0FC4
		public EditorAttribute(string typeName, Type baseType)
		{
			string text = typeName.ToUpper(CultureInfo.InvariantCulture);
			this.typeName = typeName;
			this.baseTypeName = baseType.AssemblyQualifiedName;
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000E2DF6 File Offset: 0x000E0FF6
		public EditorAttribute(Type type, Type baseType)
		{
			this.typeName = type.AssemblyQualifiedName;
			this.baseTypeName = baseType.AssemblyQualifiedName;
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000E2E16 File Offset: 0x000E1016
		public string EditorBaseTypeName
		{
			get
			{
				return this.baseTypeName;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000E2E1E File Offset: 0x000E101E
		public string EditorTypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000E2E28 File Offset: 0x000E1028
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.baseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this.typeId = base.GetType().FullName + text;
				}
				return this.typeId;
			}
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000E2E78 File Offset: 0x000E1078
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorAttribute editorAttribute = obj as EditorAttribute;
			return editorAttribute != null && editorAttribute.typeName == this.typeName && editorAttribute.baseTypeName == this.baseTypeName;
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000E2EBB File Offset: 0x000E10BB
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040029A7 RID: 10663
		private string baseTypeName;

		// Token: 0x040029A8 RID: 10664
		private string typeName;

		// Token: 0x040029A9 RID: 10665
		private string typeId;
	}
}
