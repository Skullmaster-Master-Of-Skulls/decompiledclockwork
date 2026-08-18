using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200038D RID: 909
	internal sealed class AliasResolver
	{
		// Token: 0x060020E4 RID: 8420 RVA: 0x0009AD28 File Offset: 0x00098F28
		public AliasResolver(Schema schema)
		{
			this._definingSchema = schema;
			if (!string.IsNullOrEmpty(schema.Alias))
			{
				this._aliasToNamespaceMap.Add(schema.Alias, schema.Namespace);
			}
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x0009AD84 File Offset: 0x00098F84
		public void Add(UsingElement usingElement)
		{
			string text = usingElement.NamespaceName;
			string text2 = usingElement.Alias;
			if (this.CheckForSystemNamespace(usingElement, text2, AliasResolver.NameKind.Alias))
			{
				text2 = null;
			}
			if (this.CheckForSystemNamespace(usingElement, text, AliasResolver.NameKind.Namespace))
			{
				text = null;
			}
			if (text2 != null && this._aliasToNamespaceMap.ContainsKey(text2))
			{
				usingElement.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, Strings.AliasNameIsAlreadyDefined(text2));
				text2 = null;
			}
			if (text2 != null)
			{
				this._aliasToNamespaceMap.Add(text2, text);
				this._usingElementCollection.Add(usingElement);
			}
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x0009ADF7 File Offset: 0x00098FF7
		public bool TryResolveAlias(string alias, out string namespaceName)
		{
			return this._aliasToNamespaceMap.TryGetValue(alias, out namespaceName);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x0009AE08 File Offset: 0x00099008
		public void ResolveNamespaces()
		{
			foreach (UsingElement usingElement in this._usingElementCollection)
			{
				if (!this._definingSchema.SchemaManager.IsValidNamespaceName(usingElement.NamespaceName))
				{
					usingElement.AddError(ErrorCode.InvalidNamespaceInUsing, EdmSchemaErrorSeverity.Error, Strings.InvalidNamespaceInUsing(usingElement.NamespaceName));
				}
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x0009AE84 File Offset: 0x00099084
		private bool CheckForSystemNamespace(UsingElement refSchema, string name, AliasResolver.NameKind nameKind)
		{
			if (EdmItemCollection.IsSystemNamespace(this._definingSchema.ProviderManifest, name))
			{
				if (nameKind == AliasResolver.NameKind.Alias)
				{
					refSchema.AddError(ErrorCode.CannotUseSystemNamespaceAsAlias, EdmSchemaErrorSeverity.Error, Strings.CannotUseSystemNamespaceAsAlias(name));
				}
				else
				{
					refSchema.AddError(ErrorCode.NeedNotUseSystemNamespaceInUsing, EdmSchemaErrorSeverity.Error, Strings.NeedNotUseSystemNamespaceInUsing(name));
				}
				return true;
			}
			return false;
		}

		// Token: 0x04000BA5 RID: 2981
		private readonly Dictionary<string, string> _aliasToNamespaceMap = new Dictionary<string, string>(StringComparer.Ordinal);

		// Token: 0x04000BA6 RID: 2982
		private readonly List<UsingElement> _usingElementCollection = new List<UsingElement>();

		// Token: 0x04000BA7 RID: 2983
		private readonly Schema _definingSchema;

		// Token: 0x0200038E RID: 910
		private enum NameKind
		{
			// Token: 0x04000BA9 RID: 2985
			Alias,
			// Token: 0x04000BAA RID: 2986
			Namespace
		}
	}
}
