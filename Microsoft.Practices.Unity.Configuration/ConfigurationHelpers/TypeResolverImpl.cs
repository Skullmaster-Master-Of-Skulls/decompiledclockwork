using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Microsoft.Practices.Unity.Configuration.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity.Configuration.ConfigurationHelpers
{
	// Token: 0x0200003D RID: 61
	[SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Justification = "Impl is common suffix for implementation class")]
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Impl", Justification = "Impl is common suffix for implementation class")]
	public class TypeResolverImpl
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x000067F0 File Offset: 0x000049F0
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Validation done by Guard class")]
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Use of nested generic types is appropriate here")]
		public TypeResolverImpl(IEnumerable<KeyValuePair<string, string>> aliasesSequence, IEnumerable<string> namespaces, IEnumerable<string> assemblies)
		{
			Guard.ArgumentNotNull(aliasesSequence, "aliasesSequence");
			this.aliases = new Dictionary<string, string>();
			foreach (KeyValuePair<string, string> keyValuePair in aliasesSequence)
			{
				this.aliases.Add(keyValuePair.Key, keyValuePair.Value);
			}
			this.namespaces = new List<string>(namespaces);
			this.assemblies = new List<string>(assemblies);
			this.assemblies.Add(typeof(object).Assembly.FullName);
			this.assemblies.Add(typeof(Uri).Assembly.FullName);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000068BC File Offset: 0x00004ABC
		public Type ResolveType(string typeNameOrAlias, bool throwIfResolveFails)
		{
			Type type = this.ResolveTypeInternal(typeNameOrAlias) ?? this.ResolveGenericTypeShorthand(typeNameOrAlias);
			if (type == null && throwIfResolveFails)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Resources.CouldNotResolveType, new object[]
				{
					typeNameOrAlias
				}));
			}
			return type;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000690A File Offset: 0x00004B0A
		private Type ResolveTypeInternal(string typeNameOrAlias)
		{
			Type result;
			if ((result = TypeResolverImpl.ResolveTypeDirectly(typeNameOrAlias)) == null && (result = this.ResolveAlias(typeNameOrAlias)) == null)
			{
				result = (TypeResolverImpl.ResolveDefaultAlias(typeNameOrAlias) ?? this.ResolveTypeThroughSearch(typeNameOrAlias));
			}
			return result;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00006932 File Offset: 0x00004B32
		public Type ResolveTypeWithDefault(string typeNameOrAlias, Type defaultValue, bool throwIfResolveFails)
		{
			if (string.IsNullOrEmpty(typeNameOrAlias))
			{
				return defaultValue;
			}
			return this.ResolveType(typeNameOrAlias, throwIfResolveFails);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00006946 File Offset: 0x00004B46
		private static Type ResolveTypeDirectly(string typeNameOrAlias)
		{
			return Type.GetType(typeNameOrAlias);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00006950 File Offset: 0x00004B50
		private Type ResolveAlias(string typeNameOrAlias)
		{
			string text = this.aliases.GetOrNull(typeNameOrAlias) ?? this.aliases.GetOrNull(TypeResolverImpl.RemoveGenericWart(typeNameOrAlias));
			if (text != null)
			{
				return Type.GetType(text);
			}
			return null;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000698C File Offset: 0x00004B8C
		private static Type ResolveDefaultAlias(string typeNameOrAlias)
		{
			Type result;
			if (TypeResolverImpl.DefaultAliases.TryGetValue(typeNameOrAlias, out result) || TypeResolverImpl.DefaultAliases.TryGetValue(TypeResolverImpl.RemoveGenericWart(typeNameOrAlias), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000069C0 File Offset: 0x00004BC0
		private static string RemoveGenericWart(string typeNameOrAlias)
		{
			string result = typeNameOrAlias;
			int num = typeNameOrAlias.IndexOf('`');
			if (num != -1)
			{
				result = typeNameOrAlias.Substring(0, num);
			}
			return result;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000069E6 File Offset: 0x00004BE6
		private Type ResolveTypeThroughSearch(string typeNameOrAlias)
		{
			if (this.namespaces.Count == 0)
			{
				return this.SearchAssemblies(typeNameOrAlias);
			}
			return this.SearchAssembliesAndNamespaces(typeNameOrAlias);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00006A04 File Offset: 0x00004C04
		private Type ResolveGenericTypeShorthand(string typeNameOrAlias)
		{
			Type type = null;
			TypeNameInfo typeNameInfo = TypeNameParser.Parse(typeNameOrAlias);
			if (typeNameInfo != null && typeNameInfo.IsGenericType)
			{
				type = this.ResolveTypeInternal(typeNameInfo.FullName);
				if (type == null)
				{
					return null;
				}
				List<Type> list = new List<Type>(typeNameInfo.NumGenericParameters);
				if (typeNameInfo.GenericParameters[0] != null)
				{
					foreach (TypeNameInfo typeNameInfo2 in typeNameInfo.GenericParameters)
					{
						Type type2 = this.ResolveType(typeNameInfo2.FullName, false);
						if (type2 == null)
						{
							return null;
						}
						list.Add(type2);
					}
					if (list.Count > 0)
					{
						return type.MakeGenericType(list.ToArray());
					}
					return type;
				}
			}
			return type;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00006AE4 File Offset: 0x00004CE4
		private Type SearchAssembliesAndNamespaces(string typeNameOrAlias)
		{
			foreach (string assembly in this.assemblies)
			{
				foreach (string ns in this.namespaces)
				{
					try
					{
						Type type = Type.GetType(TypeResolverImpl.MakeTypeName(ns, typeNameOrAlias, assembly));
						if (type != null)
						{
							return type;
						}
					}
					catch (FileLoadException)
					{
					}
				}
			}
			return null;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00006B9C File Offset: 0x00004D9C
		private Type SearchAssemblies(string typeNameOrAlias)
		{
			foreach (string assembly in this.assemblies)
			{
				try
				{
					Type type = Type.GetType(TypeResolverImpl.MakeAssemblyQualifiedName(typeNameOrAlias, assembly));
					if (type != null)
					{
						return type;
					}
				}
				catch (FileLoadException)
				{
				}
			}
			return null;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006C18 File Offset: 0x00004E18
		private static string MakeTypeName(string ns, string typename, string assembly)
		{
			return TypeResolverImpl.MakeAssemblyQualifiedName(ns + "." + typename, assembly);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006C2C File Offset: 0x00004E2C
		private static string MakeAssemblyQualifiedName(string typename, string assembly)
		{
			return typename + ", " + assembly;
		}

		// Token: 0x0400006E RID: 110
		private readonly Dictionary<string, string> aliases;

		// Token: 0x0400006F RID: 111
		private readonly List<string> namespaces;

		// Token: 0x04000070 RID: 112
		private readonly List<string> assemblies;

		// Token: 0x04000071 RID: 113
		private static readonly Dictionary<string, Type> DefaultAliases = new Dictionary<string, Type>
		{
			{
				"sbyte",
				typeof(sbyte)
			},
			{
				"short",
				typeof(short)
			},
			{
				"int",
				typeof(int)
			},
			{
				"integer",
				typeof(int)
			},
			{
				"long",
				typeof(long)
			},
			{
				"byte",
				typeof(byte)
			},
			{
				"ushort",
				typeof(ushort)
			},
			{
				"uint",
				typeof(uint)
			},
			{
				"ulong",
				typeof(ulong)
			},
			{
				"float",
				typeof(float)
			},
			{
				"single",
				typeof(float)
			},
			{
				"double",
				typeof(double)
			},
			{
				"decimal",
				typeof(decimal)
			},
			{
				"char",
				typeof(char)
			},
			{
				"bool",
				typeof(bool)
			},
			{
				"object",
				typeof(object)
			},
			{
				"string",
				typeof(string)
			},
			{
				"datetime",
				typeof(DateTime)
			},
			{
				"DateTime",
				typeof(DateTime)
			},
			{
				"date",
				typeof(DateTime)
			},
			{
				"singleton",
				typeof(ContainerControlledLifetimeManager)
			},
			{
				"ContainerControlledLifetimeManager",
				typeof(ContainerControlledLifetimeManager)
			},
			{
				"transient",
				typeof(TransientLifetimeManager)
			},
			{
				"TransientLifetimeManager",
				typeof(TransientLifetimeManager)
			},
			{
				"perthread",
				typeof(PerThreadLifetimeManager)
			},
			{
				"PerThreadLifetimeManager",
				typeof(PerThreadLifetimeManager)
			},
			{
				"external",
				typeof(ExternallyControlledLifetimeManager)
			},
			{
				"ExternallyControlledLifetimeManager",
				typeof(ExternallyControlledLifetimeManager)
			},
			{
				"hierarchical",
				typeof(HierarchicalLifetimeManager)
			},
			{
				"HierarchicalLifetimeManager",
				typeof(HierarchicalLifetimeManager)
			},
			{
				"resolve",
				typeof(PerResolveLifetimeManager)
			},
			{
				"perresolve",
				typeof(PerResolveLifetimeManager)
			},
			{
				"PerResolveLifetimeManager",
				typeof(PerResolveLifetimeManager)
			}
		};
	}
}
