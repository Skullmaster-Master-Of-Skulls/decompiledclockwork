using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000DD RID: 221
	internal class NamespaceCache
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x000173AB File Offset: 0x000155AB
		internal NamespaceCache(MetadataReader reader)
		{
			this._metadataReader = reader;
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x000173C5 File Offset: 0x000155C5
		internal bool CacheIsRealized
		{
			get
			{
				return this._namespaceTable != null;
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000173D0 File Offset: 0x000155D0
		internal string GetFullName(NamespaceDefinitionHandle handle)
		{
			return this.GetNamespaceData(handle).FullName;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x000173DE File Offset: 0x000155DE
		internal NamespaceData GetRootNamespace()
		{
			this.EnsureNamespaceTableIsPopulated();
			return this._rootNamespace;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x000173EC File Offset: 0x000155EC
		internal NamespaceData GetNamespaceData(NamespaceDefinitionHandle handle)
		{
			this.EnsureNamespaceTableIsPopulated();
			NamespaceData result;
			if (!this._namespaceTable.TryGetValue(handle, out result))
			{
				Throw.InvalidHandle();
			}
			return result;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00017418 File Offset: 0x00015618
		private StringHandle GetSimpleName(NamespaceDefinitionHandle fullNamespaceHandle, int segmentIndex = 2147483647)
		{
			fullNamespaceHandle.GetFullName();
			int num = fullNamespaceHandle.GetHeapOffset() - 1;
			for (int i = 0; i < segmentIndex; i++)
			{
				int num2 = this._metadataReader.StringStream.IndexOfRaw(num + 1, '.');
				if (num2 == -1)
				{
					break;
				}
				num = num2;
			}
			return StringHandle.FromOffset(num + 1).WithDotTermination();
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00017470 File Offset: 0x00015670
		private void PopulateNamespaceTable()
		{
			object namespaceTableAndListLock = this._namespaceTableAndListLock;
			lock (namespaceTableAndListLock)
			{
				if (this._namespaceTable == null)
				{
					Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> dictionary = new Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder>();
					NamespaceDefinitionHandle namespaceDefinitionHandle = NamespaceDefinitionHandle.FromFullNameOffset(0);
					Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> dictionary2 = dictionary;
					NamespaceDefinitionHandle namespaceDefinitionHandle2 = namespaceDefinitionHandle;
					dictionary2.Add(namespaceDefinitionHandle2, new NamespaceCache.NamespaceDataBuilder(namespaceDefinitionHandle2, namespaceDefinitionHandle.GetFullName(), string.Empty));
					this.PopulateTableWithTypeDefinitions(dictionary);
					this.PopulateTableWithExportedTypes(dictionary);
					Dictionary<string, NamespaceCache.NamespaceDataBuilder> namespaces;
					this.MergeDuplicateNamespaces(dictionary, out namespaces);
					List<NamespaceCache.NamespaceDataBuilder> list;
					this.ResolveParentChildRelationships(namespaces, out list);
					Dictionary<NamespaceDefinitionHandle, NamespaceData> dictionary3 = new Dictionary<NamespaceDefinitionHandle, NamespaceData>();
					foreach (KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> keyValuePair in dictionary)
					{
						dictionary3.Add(keyValuePair.Key, keyValuePair.Value.Freeze());
					}
					if (list != null)
					{
						foreach (NamespaceCache.NamespaceDataBuilder namespaceDataBuilder in list)
						{
							dictionary3.Add(namespaceDataBuilder.Handle, namespaceDataBuilder.Freeze());
						}
					}
					this._namespaceTable = dictionary3;
					this._rootNamespace = dictionary3[namespaceDefinitionHandle];
				}
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000175E4 File Offset: 0x000157E4
		private void MergeDuplicateNamespaces(Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> table, out Dictionary<string, NamespaceCache.NamespaceDataBuilder> stringTable)
		{
			Dictionary<string, NamespaceCache.NamespaceDataBuilder> dictionary = new Dictionary<string, NamespaceCache.NamespaceDataBuilder>();
			List<KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder>> list = null;
			foreach (KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> keyValuePair in table)
			{
				NamespaceCache.NamespaceDataBuilder value = keyValuePair.Value;
				NamespaceCache.NamespaceDataBuilder namespaceDataBuilder;
				if (dictionary.TryGetValue(value.FullName, out namespaceDataBuilder))
				{
					value.MergeInto(namespaceDataBuilder);
					if (list == null)
					{
						list = new List<KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder>>();
					}
					list.Add(new KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder>(keyValuePair.Key, namespaceDataBuilder));
				}
				else
				{
					dictionary.Add(value.FullName, value);
				}
			}
			if (list != null)
			{
				foreach (KeyValuePair<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> keyValuePair2 in list)
				{
					table[keyValuePair2.Key] = keyValuePair2.Value;
				}
			}
			stringTable = dictionary;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000176D8 File Offset: 0x000158D8
		private NamespaceCache.NamespaceDataBuilder SynthesizeNamespaceData(string fullName, NamespaceDefinitionHandle realChild)
		{
			int num = 0;
			for (int i = 0; i < fullName.Length; i++)
			{
				if (fullName[i] == '.')
				{
					num++;
				}
			}
			StringHandle simpleName = this.GetSimpleName(realChild, num);
			uint num2 = this._virtualNamespaceCounter + 1U;
			this._virtualNamespaceCounter = num2;
			return new NamespaceCache.NamespaceDataBuilder(NamespaceDefinitionHandle.FromVirtualIndex(num2), simpleName, fullName);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00017732 File Offset: 0x00015932
		private void LinkChildDataToParentData(NamespaceCache.NamespaceDataBuilder child, NamespaceCache.NamespaceDataBuilder parent)
		{
			child.Parent = parent.Handle;
			parent.Namespaces.Add(child.Handle);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00017754 File Offset: 0x00015954
		private void LinkChildToParentNamespace(Dictionary<string, NamespaceCache.NamespaceDataBuilder> existingNamespaces, NamespaceCache.NamespaceDataBuilder realChild, ref List<NamespaceCache.NamespaceDataBuilder> virtualNamespaces)
		{
			string fullName = realChild.FullName;
			NamespaceCache.NamespaceDataBuilder child = realChild;
			NamespaceCache.NamespaceDataBuilder parent;
			for (;;)
			{
				int num = fullName.LastIndexOf('.');
				string text;
				if (num == -1)
				{
					if (fullName.Length == 0)
					{
						break;
					}
					text = string.Empty;
				}
				else
				{
					text = fullName.Substring(0, num);
				}
				if (existingNamespaces.TryGetValue(text, out parent))
				{
					goto Block_3;
				}
				if (virtualNamespaces != null)
				{
					using (List<NamespaceCache.NamespaceDataBuilder>.Enumerator enumerator = virtualNamespaces.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							NamespaceCache.NamespaceDataBuilder namespaceDataBuilder = enumerator.Current;
							if (namespaceDataBuilder.FullName == text)
							{
								this.LinkChildDataToParentData(child, namespaceDataBuilder);
								return;
							}
						}
						goto IL_97;
					}
					goto IL_90;
				}
				goto IL_90;
				IL_97:
				NamespaceCache.NamespaceDataBuilder namespaceDataBuilder2 = this.SynthesizeNamespaceData(text, realChild.Handle);
				this.LinkChildDataToParentData(child, namespaceDataBuilder2);
				virtualNamespaces.Add(namespaceDataBuilder2);
				fullName = namespaceDataBuilder2.FullName;
				child = namespaceDataBuilder2;
				continue;
				IL_90:
				virtualNamespaces = new List<NamespaceCache.NamespaceDataBuilder>();
				goto IL_97;
			}
			return;
			Block_3:
			this.LinkChildDataToParentData(child, parent);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001783C File Offset: 0x00015A3C
		private void ResolveParentChildRelationships(Dictionary<string, NamespaceCache.NamespaceDataBuilder> namespaces, out List<NamespaceCache.NamespaceDataBuilder> virtualNamespaces)
		{
			virtualNamespaces = null;
			foreach (NamespaceCache.NamespaceDataBuilder realChild in namespaces.Values)
			{
				this.LinkChildToParentNamespace(namespaces, realChild, ref virtualNamespaces);
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00017894 File Offset: 0x00015A94
		private void PopulateTableWithTypeDefinitions(Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> table)
		{
			foreach (TypeDefinitionHandle typeDefinitionHandle in this._metadataReader.TypeDefinitions)
			{
				if (!this._metadataReader.GetTypeDefinition(typeDefinitionHandle).Attributes.IsNested())
				{
					NamespaceDefinitionHandle namespaceDefinition = this._metadataReader.TypeDefTable.GetNamespaceDefinition(typeDefinitionHandle);
					NamespaceCache.NamespaceDataBuilder namespaceDataBuilder;
					if (table.TryGetValue(namespaceDefinition, out namespaceDataBuilder))
					{
						namespaceDataBuilder.TypeDefinitions.Add(typeDefinitionHandle);
					}
					else
					{
						StringHandle simpleName = this.GetSimpleName(namespaceDefinition, int.MaxValue);
						string @string = this._metadataReader.GetString(namespaceDefinition);
						table.Add(namespaceDefinition, new NamespaceCache.NamespaceDataBuilder(namespaceDefinition, simpleName, @string)
						{
							TypeDefinitions = 
							{
								typeDefinitionHandle
							}
						});
					}
				}
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001797C File Offset: 0x00015B7C
		private void PopulateTableWithExportedTypes(Dictionary<NamespaceDefinitionHandle, NamespaceCache.NamespaceDataBuilder> table)
		{
			foreach (ExportedTypeHandle exportedTypeHandle in this._metadataReader.ExportedTypes)
			{
				ExportedType exportedType = this._metadataReader.GetExportedType(exportedTypeHandle);
				if (exportedType.Implementation.Kind != HandleKind.ExportedType)
				{
					NamespaceDefinitionHandle namespaceDefinition = exportedType.NamespaceDefinition;
					NamespaceCache.NamespaceDataBuilder namespaceDataBuilder;
					if (table.TryGetValue(namespaceDefinition, out namespaceDataBuilder))
					{
						namespaceDataBuilder.ExportedTypes.Add(exportedTypeHandle);
					}
					else
					{
						StringHandle simpleName = this.GetSimpleName(namespaceDefinition, int.MaxValue);
						string @string = this._metadataReader.GetString(namespaceDefinition);
						table.Add(namespaceDefinition, new NamespaceCache.NamespaceDataBuilder(namespaceDefinition, simpleName, @string)
						{
							ExportedTypes = 
							{
								exportedTypeHandle
							}
						});
					}
				}
			}
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00017A60 File Offset: 0x00015C60
		private void PopulateNamespaceList()
		{
			object namespaceTableAndListLock = this._namespaceTableAndListLock;
			lock (namespaceTableAndListLock)
			{
				if (!(new ImmutableArray<NamespaceDefinitionHandle>?(this._namespaceList) != null))
				{
					HashSet<string> hashSet = new HashSet<string>();
					ImmutableArray<NamespaceDefinitionHandle>.Builder builder = ImmutableArray.CreateBuilder<NamespaceDefinitionHandle>();
					foreach (KeyValuePair<NamespaceDefinitionHandle, NamespaceData> keyValuePair in this._namespaceTable)
					{
						NamespaceData value = keyValuePair.Value;
						if (hashSet.Add(value.FullName))
						{
							builder.Add(keyValuePair.Key);
						}
					}
					this._namespaceList = builder.ToImmutable();
				}
			}
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00017B34 File Offset: 0x00015D34
		private void EnsureNamespaceTableIsPopulated()
		{
			if (this._namespaceTable == null)
			{
				this.PopulateNamespaceTable();
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00017B44 File Offset: 0x00015D44
		private void EnsureNamespaceListIsPopulated()
		{
			if (new ImmutableArray<NamespaceDefinitionHandle>?(this._namespaceList) == null)
			{
				this.PopulateNamespaceList();
			}
		}

		// Token: 0x04000683 RID: 1667
		private readonly MetadataReader _metadataReader;

		// Token: 0x04000684 RID: 1668
		private readonly object _namespaceTableAndListLock = new object();

		// Token: 0x04000685 RID: 1669
		private Dictionary<NamespaceDefinitionHandle, NamespaceData> _namespaceTable;

		// Token: 0x04000686 RID: 1670
		private NamespaceData _rootNamespace;

		// Token: 0x04000687 RID: 1671
		private ImmutableArray<NamespaceDefinitionHandle> _namespaceList;

		// Token: 0x04000688 RID: 1672
		private uint _virtualNamespaceCounter;

		// Token: 0x020001D5 RID: 469
		private class NamespaceDataBuilder
		{
			// Token: 0x06000C54 RID: 3156 RVA: 0x000225D4 File Offset: 0x000207D4
			public NamespaceDataBuilder(NamespaceDefinitionHandle handle, StringHandle name, string fullName)
			{
				this.Handle = handle;
				this.Name = name;
				this.FullName = fullName;
				this.Namespaces = ImmutableArray.CreateBuilder<NamespaceDefinitionHandle>();
				this.TypeDefinitions = ImmutableArray.CreateBuilder<TypeDefinitionHandle>();
				this.ExportedTypes = ImmutableArray.CreateBuilder<ExportedTypeHandle>();
			}

			// Token: 0x06000C55 RID: 3157 RVA: 0x00022614 File Offset: 0x00020814
			public NamespaceData Freeze()
			{
				if (this._frozen == null)
				{
					ImmutableArray<NamespaceDefinitionHandle> namespaceDefinitions = this.Namespaces.ToImmutable();
					this.Namespaces = null;
					ImmutableArray<TypeDefinitionHandle> typeDefinitions = this.TypeDefinitions.ToImmutable();
					this.TypeDefinitions = null;
					ImmutableArray<ExportedTypeHandle> exportedTypes = this.ExportedTypes.ToImmutable();
					this.ExportedTypes = null;
					this._frozen = new NamespaceData(this.Name, this.FullName, this.Parent, namespaceDefinitions, typeDefinitions, exportedTypes);
				}
				return this._frozen;
			}

			// Token: 0x06000C56 RID: 3158 RVA: 0x00022688 File Offset: 0x00020888
			public void MergeInto(NamespaceCache.NamespaceDataBuilder other)
			{
				this.Parent = default(NamespaceDefinitionHandle);
				other.Namespaces.AddRange(this.Namespaces);
				other.TypeDefinitions.AddRange(this.TypeDefinitions);
				other.ExportedTypes.AddRange(this.ExportedTypes);
			}

			// Token: 0x04000B3F RID: 2879
			public readonly NamespaceDefinitionHandle Handle;

			// Token: 0x04000B40 RID: 2880
			public readonly StringHandle Name;

			// Token: 0x04000B41 RID: 2881
			public readonly string FullName;

			// Token: 0x04000B42 RID: 2882
			public NamespaceDefinitionHandle Parent;

			// Token: 0x04000B43 RID: 2883
			public ImmutableArray<NamespaceDefinitionHandle>.Builder Namespaces;

			// Token: 0x04000B44 RID: 2884
			public ImmutableArray<TypeDefinitionHandle>.Builder TypeDefinitions;

			// Token: 0x04000B45 RID: 2885
			public ImmutableArray<ExportedTypeHandle>.Builder ExportedTypes;

			// Token: 0x04000B46 RID: 2886
			private NamespaceData _frozen;
		}
	}
}
