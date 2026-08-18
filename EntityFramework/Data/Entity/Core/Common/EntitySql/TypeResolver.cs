using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000275 RID: 629
	internal sealed class TypeResolver
	{
		// Token: 0x06001612 RID: 5650 RVA: 0x0006B184 File Offset: 0x00069384
		internal TypeResolver(Perspective perspective, ParserOptions parserOptions)
		{
			this._perspective = perspective;
			this._parserOptions = parserOptions;
			this._aliasedNamespaces = new Dictionary<string, MetadataNamespace>(parserOptions.NameComparer);
			this._namespaces = new HashSet<MetadataNamespace>(MetadataMember.CreateMetadataMemberNameEqualityComparer(parserOptions.NameComparer));
			this._functionDefinitions = new Dictionary<string, List<InlineFunctionInfo>>(parserOptions.NameComparer);
			this._includeInlineFunctions = true;
			this._resolveLeftMostUnqualifiedNameAsNamespaceOnly = false;
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x0006B1EB File Offset: 0x000693EB
		internal Perspective Perspective
		{
			get
			{
				return this._perspective;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0006B1F3 File Offset: 0x000693F3
		internal ICollection<MetadataNamespace> NamespaceImports
		{
			get
			{
				return this._namespaces;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0006B1FB File Offset: 0x000693FB
		internal static TypeUsage StringType
		{
			get
			{
				return MetadataWorkspace.GetCanonicalModelTypeUsage(PrimitiveTypeKind.String);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0006B204 File Offset: 0x00069404
		internal static TypeUsage BooleanType
		{
			get
			{
				return MetadataWorkspace.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Boolean);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0006B20C File Offset: 0x0006940C
		internal static TypeUsage Int64Type
		{
			get
			{
				return MetadataWorkspace.GetCanonicalModelTypeUsage(PrimitiveTypeKind.Int64);
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0006B218 File Offset: 0x00069418
		internal void AddAliasedNamespaceImport(string alias, MetadataNamespace @namespace, ErrorContext errCtx)
		{
			if (this._aliasedNamespaces.ContainsKey(alias))
			{
				string errorMessage = Strings.NamespaceAliasAlreadyUsed(alias);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			this._aliasedNamespaces.Add(alias, @namespace);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0006B250 File Offset: 0x00069450
		internal void AddNamespaceImport(MetadataNamespace @namespace, ErrorContext errCtx)
		{
			if (this._namespaces.Contains(@namespace))
			{
				string errorMessage = Strings.NamespaceAlreadyImported(@namespace.Name);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			this._namespaces.Add(@namespace);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0006B314 File Offset: 0x00069514
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
				ErrorContext errCtx = functionInfo.FunctionDefAst.ErrCtx;
				string errorMessage = Strings.DuplicatedInlineFunctionOverload(name);
				throw EntitySqlException.Create(errCtx, errorMessage, null);
			}
			list.Add(functionInfo);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0006B3B0 File Offset: 0x000695B0
		internal IDisposable EnterFunctionNameResolution(bool includeInlineFunctions)
		{
			bool savedIncludeInlineFunctions = this._includeInlineFunctions;
			this._includeInlineFunctions = includeInlineFunctions;
			return new Disposer(delegate()
			{
				this._includeInlineFunctions = savedIncludeInlineFunctions;
			});
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x0006B3F7 File Offset: 0x000695F7
		internal IDisposable EnterBackwardCompatibilityResolution()
		{
			this._resolveLeftMostUnqualifiedNameAsNamespaceOnly = true;
			return new Disposer(delegate()
			{
				this._resolveLeftMostUnqualifiedNameAsNamespaceOnly = false;
			});
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0006B414 File Offset: 0x00069614
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

		// Token: 0x0600161E RID: 5662 RVA: 0x0006B444 File Offset: 0x00069644
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
						string errorMessage = Strings.NotAMemberOfType(name, qualifier.Name);
						throw EntitySqlException.Create(errCtx, errorMessage, null);
					}
				}
				string errorMessage2 = Strings.InvalidMetadataMemberClassResolution(qualifier.Name, qualifier.MetadataMemberClassName, MetadataNamespace.NamespaceClassName);
				throw EntitySqlException.Create(errCtx, errorMessage2, null);
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

		// Token: 0x0600161F RID: 5663 RVA: 0x0006B53C File Offset: 0x0006973C
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

		// Token: 0x06001620 RID: 5664 RVA: 0x0006B6EC File Offset: 0x000698EC
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

		// Token: 0x06001621 RID: 5665 RVA: 0x0006B728 File Offset: 0x00069928
		private static Exception AmbiguousMetadataMemberName(ErrorContext errCtx, string name, MetadataNamespace ns1, MetadataNamespace ns2)
		{
			string errorMessage = Strings.AmbiguousMetadataMemberName(name, ns1.Name, (ns2 != null) ? ns2.Name : null);
			throw EntitySqlException.Create(errCtx, errorMessage, null);
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0006B758 File Offset: 0x00069958
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

		// Token: 0x06001623 RID: 5667 RVA: 0x0006B790 File Offset: 0x00069990
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

		// Token: 0x06001624 RID: 5668 RVA: 0x0006B7DC File Offset: 0x000699DC
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

		// Token: 0x06001625 RID: 5669 RVA: 0x0006B810 File Offset: 0x00069A10
		internal static string GetFullName(params string[] names)
		{
			return string.Join(".", names);
		}

		// Token: 0x040007BD RID: 1981
		private readonly Perspective _perspective;

		// Token: 0x040007BE RID: 1982
		private readonly ParserOptions _parserOptions;

		// Token: 0x040007BF RID: 1983
		private readonly Dictionary<string, MetadataNamespace> _aliasedNamespaces;

		// Token: 0x040007C0 RID: 1984
		private readonly HashSet<MetadataNamespace> _namespaces;

		// Token: 0x040007C1 RID: 1985
		private readonly Dictionary<string, List<InlineFunctionInfo>> _functionDefinitions;

		// Token: 0x040007C2 RID: 1986
		private bool _includeInlineFunctions;

		// Token: 0x040007C3 RID: 1987
		private bool _resolveLeftMostUnqualifiedNameAsNamespaceOnly;

		// Token: 0x02000276 RID: 630
		private sealed class TypeUsageStructuralComparer : IEqualityComparer<TypeUsage>
		{
			// Token: 0x06001628 RID: 5672 RVA: 0x0006B81D File Offset: 0x00069A1D
			private TypeUsageStructuralComparer()
			{
			}

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06001629 RID: 5673 RVA: 0x0006B825 File Offset: 0x00069A25
			public static TypeResolver.TypeUsageStructuralComparer Instance
			{
				get
				{
					return TypeResolver.TypeUsageStructuralComparer._instance;
				}
			}

			// Token: 0x0600162A RID: 5674 RVA: 0x0006B82C File Offset: 0x00069A2C
			public bool Equals(TypeUsage x, TypeUsage y)
			{
				return TypeSemantics.IsStructurallyEqual(x, y);
			}

			// Token: 0x0600162B RID: 5675 RVA: 0x0006B835 File Offset: 0x00069A35
			public int GetHashCode(TypeUsage obj)
			{
				return 0;
			}

			// Token: 0x040007C5 RID: 1989
			private static readonly TypeResolver.TypeUsageStructuralComparer _instance = new TypeResolver.TypeUsageStructuralComparer();
		}
	}
}
