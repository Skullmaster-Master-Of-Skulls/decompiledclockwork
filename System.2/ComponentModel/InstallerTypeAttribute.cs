using System;

namespace System.ComponentModel
{
	// Token: 0x0200056C RID: 1388
	[AttributeUsage(AttributeTargets.Class)]
	public class InstallerTypeAttribute : Attribute
	{
		// Token: 0x060033BF RID: 13247 RVA: 0x000E4167 File Offset: 0x000E2367
		public InstallerTypeAttribute(Type installerType)
		{
			this._typeName = installerType.AssemblyQualifiedName;
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000E417B File Offset: 0x000E237B
		public InstallerTypeAttribute(string typeName)
		{
			this._typeName = typeName;
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060033C1 RID: 13249 RVA: 0x000E418A File Offset: 0x000E238A
		public virtual Type InstallerType
		{
			get
			{
				return Type.GetType(this._typeName);
			}
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000E4198 File Offset: 0x000E2398
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			InstallerTypeAttribute installerTypeAttribute = obj as InstallerTypeAttribute;
			return installerTypeAttribute != null && installerTypeAttribute._typeName == this._typeName;
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000E41C8 File Offset: 0x000E23C8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040029C6 RID: 10694
		private string _typeName;
	}
}
