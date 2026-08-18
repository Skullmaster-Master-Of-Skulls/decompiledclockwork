using System;
using System.Collections.Generic;
using System.Runtime;
using System.Text;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200004D RID: 77
	internal static class ScopeCompiler
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0000B40C File Offset: 0x0000960C
		public static string[] Compile(ICollection<Uri> scopes)
		{
			if (scopes == null || scopes.Count == 0)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (Uri scope in scopes)
			{
				ScopeCompiler.Compile(scope, list);
			}
			return list.ToArray();
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000B470 File Offset: 0x00009670
		public static CompiledScopeCriteria[] CompileMatchCriteria(ICollection<Uri> scopes, Uri matchBy)
		{
			if (scopes == null || scopes.Count == 0)
			{
				return null;
			}
			List<CompiledScopeCriteria> list = new List<CompiledScopeCriteria>();
			foreach (Uri scope in scopes)
			{
				list.Add(ScopeCompiler.CompileCriteria(scope, matchBy));
			}
			return list.ToArray();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000B4D8 File Offset: 0x000096D8
		public static bool IsSupportedMatchingRule(Uri matchBy)
		{
			return matchBy.Equals(FindCriteria.ScopeMatchByPrefix) || matchBy.Equals(FindCriteria.ScopeMatchByUuid) || matchBy.Equals(FindCriteria.ScopeMatchByLdap) || matchBy.Equals(FindCriteria.ScopeMatchByExact) || matchBy.Equals(FindCriteria.ScopeMatchByNone);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000B528 File Offset: 0x00009728
		public static bool IsMatch(CompiledScopeCriteria compiledScopeMatchCriteria, string[] compiledScopes)
		{
			if (compiledScopeMatchCriteria.MatchBy == CompiledScopeCriteriaMatchBy.Exact)
			{
				for (int i = 0; i < compiledScopes.Length; i++)
				{
					if (string.CompareOrdinal(compiledScopes[i], compiledScopeMatchCriteria.CompiledScope) == 0)
					{
						return true;
					}
				}
			}
			else if (compiledScopeMatchCriteria.MatchBy == CompiledScopeCriteriaMatchBy.StartsWith)
			{
				for (int j = 0; j < compiledScopes.Length; j++)
				{
					if (compiledScopes[j].StartsWith(compiledScopeMatchCriteria.CompiledScope, StringComparison.Ordinal))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000B58C File Offset: 0x0000978C
		private static void Compile(Uri scope, List<string> compiledScopes)
		{
			compiledScopes.Add(ScopeCompiler.CompileForMatchByRfc2396(scope));
			Guid guid;
			if (ScopeCompiler.TryGetUuidGuid(scope, out guid))
			{
				compiledScopes.Add(ScopeCompiler.CompileForMatchByUuid(guid));
			}
			compiledScopes.Add(ScopeCompiler.CompileForMatchByStrcmp0(scope));
			if (string.Compare(scope.Scheme, "ldap", StringComparison.OrdinalIgnoreCase) == 0)
			{
				compiledScopes.Add(ScopeCompiler.CompileForMatchByLdap(scope));
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000B5E8 File Offset: 0x000097E8
		private static CompiledScopeCriteria CompileCriteria(Uri scope, Uri matchBy)
		{
			string compiledScope;
			CompiledScopeCriteriaMatchBy matchBy2;
			if (matchBy.Equals(FindCriteria.ScopeMatchByPrefix))
			{
				compiledScope = ScopeCompiler.CompileForMatchByRfc2396(scope);
				matchBy2 = CompiledScopeCriteriaMatchBy.StartsWith;
			}
			else if (matchBy.Equals(FindCriteria.ScopeMatchByUuid))
			{
				Guid guid;
				if (!ScopeCompiler.TryGetUuidGuid(scope, out guid))
				{
					throw FxTrace.Exception.AsError(new FormatException(SR.DiscoveryFormatInvalidScopeUuidUri(scope.ToString())));
				}
				compiledScope = ScopeCompiler.CompileForMatchByUuid(guid);
				matchBy2 = CompiledScopeCriteriaMatchBy.Exact;
			}
			else if (matchBy.Equals(FindCriteria.ScopeMatchByLdap))
			{
				if (string.Compare(scope.Scheme, "ldap", StringComparison.OrdinalIgnoreCase) != 0)
				{
					throw FxTrace.Exception.AsError(new FormatException(SR.DiscoveryFormatInvalidScopeLdapUri(scope.ToString())));
				}
				compiledScope = ScopeCompiler.CompileForMatchByLdap(scope);
				matchBy2 = CompiledScopeCriteriaMatchBy.StartsWith;
			}
			else
			{
				if (!matchBy.Equals(FindCriteria.ScopeMatchByExact))
				{
					throw FxTrace.Exception.ArgumentOutOfRange("matchBy", matchBy, SR.DiscoveryMatchingRuleNotSupported(FindCriteria.ScopeMatchByExact, FindCriteria.ScopeMatchByPrefix, FindCriteria.ScopeMatchByUuid, FindCriteria.ScopeMatchByLdap));
				}
				compiledScope = ScopeCompiler.CompileForMatchByStrcmp0(scope);
				matchBy2 = CompiledScopeCriteriaMatchBy.Exact;
			}
			return new CompiledScopeCriteria(compiledScope, matchBy2);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000B6E0 File Offset: 0x000098E0
		private static string CompileForMatchByRfc2396(Uri scope)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("rfc2396match::");
			string text = scope.GetComponents(UriComponents.Scheme, UriFormat.UriEscaped);
			if (text != null)
			{
				text = text.ToUpperInvariant();
			}
			else
			{
				text = string.Empty;
			}
			stringBuilder.Append(text);
			stringBuilder.Append(":");
			string text2 = scope.GetComponents(UriComponents.StrongAuthority, UriFormat.UriEscaped);
			if (text2 != null)
			{
				text2 = text2.ToUpperInvariant();
			}
			else
			{
				text2 = string.Empty;
			}
			stringBuilder.Append(text2);
			stringBuilder.Append(":");
			foreach (string segment in scope.Segments)
			{
				stringBuilder.Append(ScopeCompiler.ProcessUriSegment(segment));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000B794 File Offset: 0x00009994
		private static string ProcessUriSegment(string segment)
		{
			int num = segment.IndexOf(';');
			if (num != -1)
			{
				segment = segment.Substring(0, num);
			}
			if (!segment.EndsWith("/", StringComparison.Ordinal))
			{
				segment += "/";
			}
			return segment;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000B7D4 File Offset: 0x000099D4
		private static bool TryGetUuidGuid(Uri scope, out Guid guid)
		{
			string guidString = null;
			if (string.Compare(scope.Scheme, "uuid", StringComparison.OrdinalIgnoreCase) == 0)
			{
				guidString = scope.GetComponents(UriComponents.Path, UriFormat.UriEscaped);
			}
			else if (string.Compare(scope.Scheme, "urn", StringComparison.OrdinalIgnoreCase) == 0)
			{
				string text = scope.ToString();
				if (string.Compare(text, 4, "uuid:", 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
				{
					guidString = text.Substring(9);
				}
			}
			return Fx.TryCreateGuid(guidString, out guid);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000B83D File Offset: 0x00009A3D
		private static string CompileForMatchByUuid(Guid guid)
		{
			return "uuidmatch::" + guid.ToString();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000B856 File Offset: 0x00009A56
		private static string CompileForMatchByStrcmp0(Uri scope)
		{
			return "strcmp0match::" + scope.ToString();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000B868 File Offset: 0x00009A68
		private static string CompileForMatchByLdap(Uri scope)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("ldapmatch::");
			stringBuilder.Append("ldap:");
			string text = scope.GetComponents(UriComponents.HostAndPort, UriFormat.UriEscaped);
			if (text != null)
			{
				text = text.ToUpperInvariant();
			}
			else
			{
				text = string.Empty;
			}
			stringBuilder.Append(text);
			stringBuilder.Append(":");
			string components = scope.GetComponents(UriComponents.Path, UriFormat.Unescaped);
			stringBuilder.Append(ScopeCompiler.ParseLdapRDNSequence(components));
			return stringBuilder.ToString();
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		private static string ParseLdapRDNSequence(string dn)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = dn.Split(new char[]
			{
				','
			});
			StringBuilder stringBuilder2 = new StringBuilder();
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text.Trim()))
				{
					if (text.EndsWith("\\", StringComparison.Ordinal))
					{
						stringBuilder2.Append(text.Substring(0, text.Length - 1));
						stringBuilder2.Append(',');
					}
					else
					{
						stringBuilder2.Append(text);
						stringBuilder.Insert(0, "/");
						stringBuilder.Insert(0, ScopeCompiler.ParseAndSortRDNAttributes(stringBuilder2.ToString()));
						stringBuilder2 = new StringBuilder();
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		private static string ParseAndSortRDNAttributes(string rdn)
		{
			if (rdn.IndexOf('+') == -1)
			{
				return rdn;
			}
			string[] array = rdn.Split(new char[]
			{
				'+'
			});
			StringBuilder stringBuilder = new StringBuilder();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> list = new List<string>();
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text.Trim()))
				{
					if (text.EndsWith("\\", StringComparison.Ordinal))
					{
						stringBuilder.Append(text.Substring(0, text.Length - 1));
						stringBuilder.Append('+');
					}
					else
					{
						stringBuilder.Append(text);
						string text2 = stringBuilder.ToString();
						string text3 = text2;
						string value = null;
						int num = text2.IndexOf('=');
						if (num != -1)
						{
							text3 = text2.Substring(0, num);
							value = text2.Substring(num + 1);
						}
						list.Add(text3);
						dictionary.Add(text3, value);
						stringBuilder = new StringBuilder();
					}
				}
			}
			list.Sort();
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int j = 0; j < list.Count - 1; j++)
			{
				stringBuilder2.Append(list[j]);
				if (dictionary[list[j]] != null)
				{
					stringBuilder2.Append("=");
					stringBuilder2.Append(dictionary[list[j]]);
				}
				stringBuilder2.Append("+");
			}
			if (list.Count > 1)
			{
				stringBuilder2.Append(list[list.Count - 1]);
				if (dictionary[list[list.Count - 1]] != null)
				{
					stringBuilder2.Append("=");
					stringBuilder2.Append(dictionary[list[list.Count - 1]]);
				}
				stringBuilder2.Append("+");
			}
			return stringBuilder2.ToString();
		}
	}
}
