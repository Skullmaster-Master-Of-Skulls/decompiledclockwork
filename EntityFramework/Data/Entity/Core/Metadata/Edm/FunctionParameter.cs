using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E8 RID: 1256
	public sealed class FunctionParameter : MetadataItem, INamedDataModelItem
	{
		// Token: 0x06002EB3 RID: 11955 RVA: 0x000DF98C File Offset: 0x000DDB8C
		internal FunctionParameter()
		{
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000DF99F File Offset: 0x000DDB9F
		internal FunctionParameter(string name, TypeUsage typeUsage, ParameterMode parameterMode)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<TypeUsage>(typeUsage, "typeUsage");
			this._name = name;
			this._typeUsage = typeUsage;
			base.SetParameterMode(parameterMode);
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002EB5 RID: 11957 RVA: 0x000DF9DF File Offset: 0x000DDBDF
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.FunctionParameter;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002EB6 RID: 11958 RVA: 0x000DF9E3 File Offset: 0x000DDBE3
		[MetadataProperty(BuiltInTypeKind.ParameterMode, false)]
		public ParameterMode Mode
		{
			get
			{
				return base.GetParameterMode();
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002EB7 RID: 11959 RVA: 0x000DF9EB File Offset: 0x000DDBEB
		string INamedDataModelItem.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002EB8 RID: 11960 RVA: 0x000DF9F3 File Offset: 0x000DDBF3
		internal override string Identity
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06002EB9 RID: 11961 RVA: 0x000DF9FB File Offset: 0x000DDBFB
		// (set) Token: 0x06002EBA RID: 11962 RVA: 0x000DFA03 File Offset: 0x000DDC03
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				Check.NotEmpty(value, "value");
				this.SetName(value);
			}
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000DFA18 File Offset: 0x000DDC18
		private void SetName(string name)
		{
			this._name = name;
			if (this.DeclaringFunction == null)
			{
				return;
			}
			MetadataCollection<FunctionParameter> metadataCollection = (this.Mode == ParameterMode.ReturnValue) ? this.DeclaringFunction.ReturnParameters.Source : this.DeclaringFunction.Parameters.Source;
			metadataCollection.InvalidateCache();
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x000DFA67 File Offset: 0x000DDC67
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06002EBD RID: 11965 RVA: 0x000DFA6F File Offset: 0x000DDC6F
		public string TypeName
		{
			get
			{
				return this.TypeUsage.EdmType.Name;
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x000DFA84 File Offset: 0x000DDC84
		public bool IsMaxLengthConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x000DFAB8 File Offset: 0x000DDCB8
		public int? MaxLength
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet))
				{
					return null;
				}
				return facet.Value as int?;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x000DFAFC File Offset: 0x000DDCFC
		public bool IsMaxLength
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("MaxLength", false, out facet) && facet.IsUnbounded;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000DFB2C File Offset: 0x000DDD2C
		public bool IsPrecisionConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("Precision", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x000DFB60 File Offset: 0x000DDD60
		public byte? Precision
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("Precision", false, out facet))
				{
					return null;
				}
				return facet.Value as byte?;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x000DFBA4 File Offset: 0x000DDDA4
		public bool IsScaleConstant
		{
			get
			{
				Facet facet;
				return this.TypeUsage.Facets.TryGetValue("Scale", false, out facet) && facet.Description.IsConstant;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x000DFBD8 File Offset: 0x000DDDD8
		public byte? Scale
		{
			get
			{
				Facet facet;
				if (!this.TypeUsage.Facets.TryGetValue("Scale", false, out facet))
				{
					return null;
				}
				return facet.Value as byte?;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002EC5 RID: 11973 RVA: 0x000DFC19 File Offset: 0x000DDE19
		public EdmFunction DeclaringFunction
		{
			get
			{
				return this._declaringFunction.Value;
			}
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000DFC26 File Offset: 0x000DDE26
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000DFC2E File Offset: 0x000DDE2E
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
			}
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000DFC40 File Offset: 0x000DDE40
		public static FunctionParameter Create(string name, EdmType edmType, ParameterMode parameterMode)
		{
			Check.NotEmpty(name, "name");
			Check.NotNull<EdmType>(edmType, "edmType");
			FunctionParameter functionParameter = new FunctionParameter(name, TypeUsage.Create(edmType, FacetValues.NullFacetValues), parameterMode);
			functionParameter.SetReadOnly();
			return functionParameter;
		}

		// Token: 0x040011C8 RID: 4552
		internal static Func<FunctionParameter, SafeLink<EdmFunction>> DeclaringFunctionLinker = (FunctionParameter fp) => fp._declaringFunction;

		// Token: 0x040011C9 RID: 4553
		private readonly SafeLink<EdmFunction> _declaringFunction = new SafeLink<EdmFunction>();

		// Token: 0x040011CA RID: 4554
		private readonly TypeUsage _typeUsage;

		// Token: 0x040011CB RID: 4555
		private string _name;
	}
}
