using System;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001DC RID: 476
	public sealed class FunctionParameter : MetadataItem
	{
		// Token: 0x06002019 RID: 8217 RVA: 0x00070272 File Offset: 0x0006E472
		internal FunctionParameter(string name, TypeUsage typeUsage, ParameterMode parameterMode)
		{
			EntityUtil.CheckStringArgument(name, "name");
			EntityUtil.GenericCheckArgumentNull<TypeUsage>(typeUsage, "typeUsage");
			this._name = name;
			this._typeUsage = typeUsage;
			base.SetParameterMode(parameterMode);
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x000702B1 File Offset: 0x0006E4B1
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.FunctionParameter;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x000702B5 File Offset: 0x0006E4B5
		[MetadataProperty(BuiltInTypeKind.ParameterMode, false)]
		public ParameterMode Mode
		{
			get
			{
				return base.GetParameterMode();
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x000702BD File Offset: 0x0006E4BD
		internal override string Identity
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x000702BD File Offset: 0x0006E4BD
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x0600201E RID: 8222 RVA: 0x000702C5 File Offset: 0x0006E4C5
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x000702CD File Offset: 0x0006E4CD
		public EdmFunction DeclaringFunction
		{
			get
			{
				return this._declaringFunction.Value;
			}
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x000702DA File Offset: 0x0006E4DA
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x000702E2 File Offset: 0x0006E4E2
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
			}
		}

		// Token: 0x04000E38 RID: 3640
		internal static Func<FunctionParameter, SafeLink<EdmFunction>> DeclaringFunctionLinker = (FunctionParameter fp) => fp._declaringFunction;

		// Token: 0x04000E39 RID: 3641
		private readonly TypeUsage _typeUsage;

		// Token: 0x04000E3A RID: 3642
		private readonly string _name;

		// Token: 0x04000E3B RID: 3643
		private readonly SafeLink<EdmFunction> _declaringFunction = new SafeLink<EdmFunction>();
	}
}
