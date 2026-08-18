using System;
using System.ComponentModel.Design;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x02000541 RID: 1345
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerAttribute : Attribute
	{
		// Token: 0x060032B3 RID: 12979 RVA: 0x000E281C File Offset: 0x000E0A1C
		public DesignerAttribute(string designerTypeName)
		{
			string text = designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = typeof(IDesigner).FullName;
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x000E2857 File Offset: 0x000E0A57
		public DesignerAttribute(Type designerType)
		{
			this.designerTypeName = designerType.AssemblyQualifiedName;
			this.designerBaseTypeName = typeof(IDesigner).FullName;
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x000E2880 File Offset: 0x000E0A80
		public DesignerAttribute(string designerTypeName, string designerBaseTypeName)
		{
			string text = designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = designerBaseTypeName;
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x000E28B0 File Offset: 0x000E0AB0
		public DesignerAttribute(string designerTypeName, Type designerBaseType)
		{
			string text = designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = designerBaseType.AssemblyQualifiedName;
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x000E28E2 File Offset: 0x000E0AE2
		public DesignerAttribute(Type designerType, Type designerBaseType)
		{
			this.designerTypeName = designerType.AssemblyQualifiedName;
			this.designerBaseTypeName = designerBaseType.AssemblyQualifiedName;
		}

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x060032B8 RID: 12984 RVA: 0x000E2902 File Offset: 0x000E0B02
		public string DesignerBaseTypeName
		{
			get
			{
				return this.designerBaseTypeName;
			}
		}

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x060032B9 RID: 12985 RVA: 0x000E290A File Offset: 0x000E0B0A
		public string DesignerTypeName
		{
			get
			{
				return this.designerTypeName;
			}
		}

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x060032BA RID: 12986 RVA: 0x000E2914 File Offset: 0x000E0B14
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.designerBaseTypeName;
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

		// Token: 0x060032BB RID: 12987 RVA: 0x000E2964 File Offset: 0x000E0B64
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerAttribute designerAttribute = obj as DesignerAttribute;
			return designerAttribute != null && designerAttribute.designerBaseTypeName == this.designerBaseTypeName && designerAttribute.designerTypeName == this.designerTypeName;
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000E29A7 File Offset: 0x000E0BA7
		public override int GetHashCode()
		{
			return this.designerTypeName.GetHashCode() ^ this.designerBaseTypeName.GetHashCode();
		}

		// Token: 0x04002989 RID: 10633
		private readonly string designerTypeName;

		// Token: 0x0400298A RID: 10634
		private readonly string designerBaseTypeName;

		// Token: 0x0400298B RID: 10635
		private string typeId;
	}
}
