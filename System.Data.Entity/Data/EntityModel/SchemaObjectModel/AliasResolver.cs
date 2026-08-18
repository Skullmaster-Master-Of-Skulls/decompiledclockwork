using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000310 RID: 784
	internal sealed class AliasResolver
	{
		// Token: 0x06002E88 RID: 11912 RVA: 0x000AFE7C File Offset: 0x000AE07C
		public AliasResolver(Schema schema)
		{
			this._definingSchema = schema;
			if (!string.IsNullOrEmpty(schema.Alias))
			{
				this._aliasToNamespaceMap.Add(schema.Alias, schema.Namespace);
			}
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000AFED8 File Offset: 0x000AE0D8
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

		// Token: 0x06002E8A RID: 11914 RVA: 0x000AFF4B File Offset: 0x000AE14B
		public bool TryResolveAlias(string alias, out string namespaceName)
		{
			return this._aliasToNamespaceMap.TryGetValue(alias, out namespaceName);
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000AFF5C File Offset: 0x000AE15C
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

		// Token: 0x06002E8C RID: 11916 RVA: 0x000AFFD8 File Offset: 0x000AE1D8
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

		// Token: 0x0400142B RID: 5163
		private Dictionary<string, string> _aliasToNamespaceMap = new Dictionary<string, string>(StringComparer.Ordinal);

		// Token: 0x0400142C RID: 5164
		private List<UsingElement> _usingElementCollection = new List<UsingElement>();

		// Token: 0x0400142D RID: 5165
		private Schema _definingSchema;

		// Token: 0x0200063E RID: 1598
		private enum NameKind
		{
			// Token: 0x04001ECE RID: 7886
			Alias,
			// Token: 0x04001ECF RID: 7887
			Namespace
		}
	}
}
