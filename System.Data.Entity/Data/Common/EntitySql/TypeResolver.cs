using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000355 RID: 853
	internal sealed class TypeResolver
	{
		// Token: 0x0600319F RID: 12703 RVA: 0x000C2DB4 File Offset: 0x000C0FB4
		internal TypeResolver(Perspective perspective, ParserOptions parserOptions)
		{
			EntityUtil.CheckArgumentNull<Perspective>(perspective, "perspective");
			this._perspective = perspective;
			this._parserOptions = parserOptions;
			this._aliasedNamespaces = new Dictionary<string, MetadataNamespace>(parserOptions.NameComparer);
			this._namespaces = new HashSet<MetadataNamespace>(MetadataMember.CreateMetadataMemberNameEqualityComparer(parserOptions.NameComparer));
			this._functionDefinitions = new Dictionary<string, List<InlineFunctionInfo>>(parserOptions.NameComparer);
			this._includeInlineFunctions = true;
			this._resolveLeftMostUnqualifiedNameAsNamespaceOnly = false;
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060031A0 RID: 12704 RVA: 0x000C2E27 File Offset: 0x000C1027
		internal Perspective Perspective
		{
			get
			{
				return this._perspective;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000C2E2F File Offset: 0x000C102F
		internal ICollection<MetadataNamespace> NamespaceImports
		{
			get
			{
				return this._namespaces;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x000C2E37 File Offset: 0x000C1037
		internal TypeUsage StringType
		{
			get
			{
				return this._perspective.MetadataWorkspace.GetCanonicalModelTypeUsage(PrimitiveTypeKind.String);
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000C2E4B File Offset: 0x000C104B
		internal TypeUsage BooleanType
		{
			get
			{
				return this._perspective.MetadataWorkspace.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Boolean);
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060031A4 RID: 12708 RVA: 0x000C2E5E File Offset: 0x000C105E
		internal TypeUsage Int64Type
		{
			get
			{
				return this._perspective.MetadataWorkspace.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int64);
			}
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000C2E72 File Offset: 0x000C1072
		internal void AddAliasedNamespaceImport(string alias, MetadataNamespace @namespace, ErrorContext errCtx)
		{
			if (this._aliasedNamespaces.ContainsKey(alias))
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.NamespaceAliasAlreadyUsed(alias));
			}
			this._aliasedNamespaces.Add(alias, @namespace);
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000C2E9C File Offset: 0x000C109C
		internal void AddNamespaceImport(MetadataNamespace @namespace, ErrorContext errCtx)
		{
			if (this._namespaces.Contains(@namespace))
			{
				throw EntityUtil.EntitySqlError(errCtx, Strings.NamespaceAlreadyImported(@namespace.Name));
			}
			this._namespaces.Add(@namespace);
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000C2ECC File Offset: 0x000C10CC
		internal void DeclareInlineFunction(string name, InlineFunctionInfo functionInfo)
		{
			List<InlineFunctionInfo> list;
			if (!this._functionDefinitions.TryGetValue(name, out list))
			{
				list = new List<InlineFunctionInfo>();
				this._functionDefinitions.Add(name, list);
			}
			if (list.Exists((InlineFunctionInfo overload) => (from p in overload.Parameters
			select p.ResultType).SequenceEqual(from p in functionInfo.Parameters
			select p.ResultType, TypeResolver.TypeUsageStructuralComparer.Instance)))
			{
				throw EntityUtil.EntitySqlError(functionInfo.FunctionDefAst.ErrCtx, Strings.DuplicatedInlineFunctionOverload(name));
			}
			list.Add(functionInfo);
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000C2F48 File Offset: 0x000C1148
		internal IDisposable EnterFunctionNameResolution(bool includeInlineFunctions)
		{
			bool savedIncludeInlineFunctions = this._includeInlineFunctions;
			this._includeInlineFunctions = includeInlineFunctions;
			return new Disposer(delegate()
			{
				this._includeInlineFunctions = savedIncludeInlineFunctions;
			});
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000C2F86 File Offset: 0x000C1186
		internal IDisposable EnterBackwardCompatibilityResolution()
		{
			this._resolveLeftMostUnqualifiedNameAsNamespaceOnly = true;
			return new Disposer(delegate()
			{
				this._resolveLeftMostUnqualifiedNameAsNamespaceOnly = false;
			});
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000C2FA0 File Offset: 0x000C11A0
		internal MetadataMember ResolveMetadataMemberName(string[] name, ErrorContext errCtx)
		{
			MetadataMember result;
			if (name.Length == 1)
			{
				result = this.ResolveUnqualifiedName(name[0], false, errCtx);
			}
			else
			{
				result = this.ResolveFullyQualifiedName(name, name.Length, errCtx);
			}
			return result;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000C2FD0 File Offset: 0x000C11D0
		internal MetadataMember ResolveMetadataMemberAccess(MetadataMember qualifier, string name, ErrorContext errCtx)
		{
			string fullName = TypeResolver.GetFullName(new string[]
			{
				qualifier.Name,
				name
			});
			if (qualifier.MetadataMemberClass != MetadataMemberClass.Namespace)
			{
				if (qualifier.MetadataMemberClass == MetadataMemberClass.Type)
				{
					MetadataType metadataType = (MetadataType)qualifier;
					if (TypeSemantics.IsEnumerationType(metadataType.TypeUsage))
					{
						EnumMember enumMember;
						if (this._perspective.TryGetEnumMember((EnumType)metadataType.TypeUsage.EdmType, name, this._parserOptions.NameComparisonCaseInsensitive, out enumMember))
						{
							return new MetadataEnumMember(fullName, metadataType.TypeUsage, enumMember);
						}
						throw EntityUtil.EntitySqlError(errCtx, Strings.NotAMemberOfType(name, qualifier.Name));
					}
				}
				throw EntityUtil.EntitySqlError(errCtx, Strings.InvalidMetadataMemberClassResolution(qualifier.Name, qualifier.MetadataMemberClassName, MetadataNamespace.NamespaceClassName));
			}
			MetadataType result;
			if (this.TryGetTypeFromMetadata(fullName, out result))
			{
				return result;
			}
			MetadataFunctionGroup result2;
			if (this.TryGetFunctionFromMetadata(qualifier.Name, name, out result2))
			{
				return result2;
			}
			return new MetadataNamespace(fullName);
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000C30AC File Offset: 0x000C12AC
		internal MetadataMember ResolveUnqualifiedName(string name, bool partOfQualifiedName, ErrorContext errCtx)
		{
			bool flag = partOfQualifiedName && this._resolveLeftMostUnqualifiedNameAsNamespaceOnly;
			bool flag2 = !partOfQualifiedName;
			InlineFunctionGroup result;
			if (!flag && flag2 && this.TryGetInlineFunction(name, out result))
			{
				return result;
			}
			MetadataNamespace result2;
			if (this._aliasedNamespaces.TryGetValue(name, out result2))
			{
				return result2;
			}
			if (!flag)
			{
				MetadataType metadataType = null;
				MetadataFunctionGroup metadataFunctionGroup = null;
				if (!this.TryGetTypeFromMetadata(name, out metadataType) && flag2)
				{
					string[] array = name.Split(new char[]
					{
						'.'
					});
					if (array.Length > 1)
					{
						if (array.All((string p) => p.Length > 0))
						{
							string text = array[array.Length - 1];
							string namespaceName = name.Substring(0, name.Length - text.Length - 1);
							this.TryGetFunctionFromMetadata(namespaceName, text, out metadataFunctionGroup);
						}
					}
				}
				MetadataNamespace ns = null;
				foreach (MetadataNamespace metadataNamespace in this._namespaces)
				{
					string fullName = TypeResolver.GetFullName(new string[]
					{
						metadataNamespace.Name,
						name
					});
					MetadataType metadataType2;
					if (this.TryGetTypeFromMetadata(fullName, out metadataType2))
					{
						if (metadataType != null || metadataFunctionGroup != null)
						{
							throw TypeResolver.AmbiguousMetadataMemberName(errCtx, name, metadataNamespace, ns);
						}
						metadataType = metadataType2;
						ns = metadataNamespace;
					}
					MetadataFunctionGroup metadataFunctionGroup2;
					if (flag2 && this.TryGetFunctionFromMetadata(metadataNamespace.Name, name, out metadataFunctionGroup2))
					{
						if (metadataType != null || metadataFunctionGroup != null)
						{
							throw TypeResolver.AmbiguousMetadataMemberName(errCtx, name, metadataNamespace, ns);
						}
						metadataFunctionGroup = metadataFunctionGroup2;
						ns = metadataNamespace;
					}
				}
				if (metadataType != null)
				{
					return metadataType;
				}
				if (metadataFunctionGroup != null)
				{
					return metadataFunctionGroup;
				}
			}
			return new MetadataNamespace(name);
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000C3258 File Offset: 0x000C1458
		private MetadataMember ResolveFullyQualifiedName(string[] name, int length, ErrorContext errCtx)
		{
			MetadataMember qualifier;
			if (length == 2)
			{
				qualifier = this.ResolveUnqualifiedName(name[0], true, errCtx);
			}
			else
			{
				qualifier = this.ResolveFullyQualifiedName(name, length - 1, errCtx);
			}
			string name2 = name[length - 1];
			return this.ResolveMetadataMemberAccess(qualifier, name2, errCtx);
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000C3292 File Offset: 0x000C1492
		private static Exception AmbiguousMetadataMemberName(ErrorContext errCtx, string name, MetadataNamespace ns1, MetadataNamespace ns2)
		{
			throw EntityUtil.EntitySqlError(errCtx, Strings.AmbiguousMetadataMemberName(name, ns1.Name, (ns2 != null) ? ns2.Name : null));
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000C32B4 File Offset: 0x000C14B4
		private bool TryGetTypeFromMetadata(string typeFullName, out MetadataType type)
		{
			TypeUsage typeUsage;
			if (this._perspective.TryGetTypeByName(typeFullName, this._parserOptions.NameComparisonCaseInsensitive, out typeUsage))
			{
				type = new MetadataType(typeFullName, typeUsage);
				return true;
			}
			type = null;
			return false;
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000C32EC File Offset: 0x000C14EC
		internal bool TryGetFunctionFromMetadata(string namespaceName, string functionName, out MetadataFunctionGroup functionGroup)
		{
			IList<EdmFunction> functionMetadata;
			if (this._perspective.TryGetFunctionByName(namespaceName, functionName, this._parserOptions.NameComparisonCaseInsensitive, out functionMetadata))
			{
				functionGroup = new MetadataFunctionGroup(TypeResolver.GetFullName(new string[]
				{
					namespaceName,
					functionName
				}), functionMetadata);
				return true;
			}
			functionGroup = null;
			return false;
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x000C3338 File Offset: 0x000C1538
		private bool TryGetInlineFunction(string functionName, out InlineFunctionGroup inlineFunctionGroup)
		{
			List<InlineFunctionInfo> functionMetadata;
			if (this._includeInlineFunctions && this._functionDefinitions.TryGetValue(functionName, out functionMetadata))
			{
				inlineFunctionGroup = new InlineFunctionGroup(functionName, functionMetadata);
				return true;
			}
			inlineFunctionGroup = null;
			return false;
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x000C336C File Offset: 0x000C156C
		internal static string GetFullName(params string[] names)
		{
			return string.Join(".", names);
		}

		// Token: 0x04001595 RID: 5525
		private readonly Perspective _perspective;

		// Token: 0x04001596 RID: 5526
		private readonly ParserOptions _parserOptions;

		// Token: 0x04001597 RID: 5527
		private readonly Dictionary<string, MetadataNamespace> _aliasedNamespaces;

		// Token: 0x04001598 RID: 5528
		private readonly HashSet<MetadataNamespace> _namespaces;

		// Token: 0x04001599 RID: 5529
		private readonly Dictionary<string, List<InlineFunctionInfo>> _functionDefinitions;

		// Token: 0x0400159A RID: 5530
		private bool _includeInlineFunctions;

		// Token: 0x0400159B RID: 5531
		private bool _resolveLeftMostUnqualifiedNameAsNamespaceOnly;

		// Token: 0x02000666 RID: 1638
		private sealed class TypeUsageStructuralComparer : IEqualityComparer<TypeUsage>
		{
			// Token: 0x06004455 RID: 17493 RVA: 0x00002050 File Offset: 0x00000250
			private TypeUsageStructuralComparer()
			{
			}

			// Token: 0x06004456 RID: 17494 RVA: 0x000F77CD File Offset: 0x000F59CD
			public bool Equals(TypeUsage x, TypeUsage y)
			{
				return TypeSemantics.IsStructurallyEqual(x, y);
			}

			// Token: 0x06004457 RID: 17495 RVA: 0x000173E2 File Offset: 0x000155E2
			public int GetHashCode(TypeUsage obj)
			{
				return 0;
			}

			// Token: 0x04001F59 RID: 8025
			internal static readonly TypeResolver.TypeUsageStructuralComparer Instance = new TypeResolver.TypeUsageStructuralComparer();
		}
	}
}
