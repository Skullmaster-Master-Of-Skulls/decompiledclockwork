using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004BA RID: 1210
	internal class TypedElement : ConfigurationElement
	{
		// Token: 0x06002D38 RID: 11576 RVA: 0x000CBA90 File Offset: 0x000C9C90
		public TypedElement(Type baseType)
		{
			this._properties = new ConfigurationPropertyCollection();
			this._properties.Add(TypedElement._propTypeName);
			this._properties.Add(TypedElement._propInitData);
			this._baseType = baseType;
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000CBACA File Offset: 0x000C9CCA
		// (set) Token: 0x06002D3A RID: 11578 RVA: 0x000CBADC File Offset: 0x000C9CDC
		[ConfigurationProperty("initializeData", DefaultValue = "")]
		public string InitData
		{
			get
			{
				return (string)base[TypedElement._propInitData];
			}
			set
			{
				base[TypedElement._propInitData] = value;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000CBAEA File Offset: 0x000C9CEA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x000CBAF2 File Offset: 0x000C9CF2
		// (set) Token: 0x06002D3D RID: 11581 RVA: 0x000CBB04 File Offset: 0x000C9D04
		[ConfigurationProperty("type", IsRequired = true, DefaultValue = "")]
		public virtual string TypeName
		{
			get
			{
				return (string)base[TypedElement._propTypeName];
			}
			set
			{
				base[TypedElement._propTypeName] = value;
			}
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x000CBB12 File Offset: 0x000C9D12
		protected object BaseGetRuntimeObject()
		{
			if (this._runtimeObject == null)
			{
				this._runtimeObject = TraceUtils.GetRuntimeObject(this.TypeName, this._baseType, this.InitData);
			}
			return this._runtimeObject;
		}

		// Token: 0x0400270F RID: 9999
		protected static readonly ConfigurationProperty _propTypeName = new ConfigurationProperty("type", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsTypeStringTransformationRequired);

		// Token: 0x04002710 RID: 10000
		protected static readonly ConfigurationProperty _propInitData = new ConfigurationProperty("initializeData", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002711 RID: 10001
		protected ConfigurationPropertyCollection _properties;

		// Token: 0x04002712 RID: 10002
		protected object _runtimeObject;

		// Token: 0x04002713 RID: 10003
		private Type _baseType;
	}
}
