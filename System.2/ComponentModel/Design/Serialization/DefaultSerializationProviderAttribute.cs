using System;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000605 RID: 1541
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class DefaultSerializationProviderAttribute : Attribute
	{
		// Token: 0x060038A9 RID: 14505 RVA: 0x000F1EB5 File Offset: 0x000F00B5
		public DefaultSerializationProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this._providerTypeName = providerType.AssemblyQualifiedName;
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000F1EDD File Offset: 0x000F00DD
		public DefaultSerializationProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this._providerTypeName = providerTypeName;
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x060038AB RID: 14507 RVA: 0x000F1EFA File Offset: 0x000F00FA
		public string ProviderTypeName
		{
			get
			{
				return this._providerTypeName;
			}
		}

		// Token: 0x04002B78 RID: 11128
		private string _providerTypeName;
	}
}
