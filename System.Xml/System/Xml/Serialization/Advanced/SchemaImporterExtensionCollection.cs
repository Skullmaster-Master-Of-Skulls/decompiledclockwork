using System;
using System.Collections;

namespace System.Xml.Serialization.Advanced
{
	// Token: 0x02000349 RID: 841
	public class SchemaImporterExtensionCollection : CollectionBase
	{
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x000D1F5C File Offset: 0x000D0F5C
		internal Hashtable Names
		{
			get
			{
				if (this.exNames == null)
				{
					this.exNames = new Hashtable();
				}
				return this.exNames;
			}
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x000D1F77 File Offset: 0x000D0F77
		public int Add(SchemaImporterExtension extension)
		{
			return this.Add(extension.GetType().FullName, extension);
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x000D1F8C File Offset: 0x000D0F8C
		public int Add(string name, Type type)
		{
			if (type.IsSubclassOf(typeof(SchemaImporterExtension)))
			{
				return this.Add(name, (SchemaImporterExtension)Activator.CreateInstance(type));
			}
			throw new ArgumentException(Res.GetString("XmlInvalidSchemaExtension", new object[]
			{
				type
			}));
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000D1FD9 File Offset: 0x000D0FD9
		public void Remove(string name)
		{
			if (this.Names[name] != null)
			{
				base.List.Remove(this.Names[name]);
				this.Names[name] = null;
			}
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x000D200D File Offset: 0x000D100D
		public new void Clear()
		{
			this.Names.Clear();
			base.List.Clear();
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x000D2028 File Offset: 0x000D1028
		internal SchemaImporterExtensionCollection Clone()
		{
			SchemaImporterExtensionCollection schemaImporterExtensionCollection = new SchemaImporterExtensionCollection();
			schemaImporterExtensionCollection.exNames = (Hashtable)this.Names.Clone();
			foreach (object value in base.List)
			{
				schemaImporterExtensionCollection.List.Add(value);
			}
			return schemaImporterExtensionCollection;
		}

		// Token: 0x170009B4 RID: 2484
		public SchemaImporterExtension this[int index]
		{
			get
			{
				return (SchemaImporterExtension)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000D20C4 File Offset: 0x000D10C4
		internal int Add(string name, SchemaImporterExtension extension)
		{
			if (this.Names[name] == null)
			{
				this.Names[name] = extension;
				return base.List.Add(extension);
			}
			if (this.Names[name].GetType() != extension.GetType())
			{
				throw new InvalidOperationException(Res.GetString("XmlConfigurationDuplicateExtension", new object[]
				{
					name
				}));
			}
			return -1;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x000D212F File Offset: 0x000D112F
		public void Insert(int index, SchemaImporterExtension extension)
		{
			base.List.Insert(index, extension);
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000D213E File Offset: 0x000D113E
		public int IndexOf(SchemaImporterExtension extension)
		{
			return base.List.IndexOf(extension);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000D214C File Offset: 0x000D114C
		public bool Contains(SchemaImporterExtension extension)
		{
			return base.List.Contains(extension);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000D215A File Offset: 0x000D115A
		public void Remove(SchemaImporterExtension extension)
		{
			base.List.Remove(extension);
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000D2168 File Offset: 0x000D1168
		public void CopyTo(SchemaImporterExtension[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x0400169D RID: 5789
		private Hashtable exNames;
	}
}
