using System;
using System.Collections;

namespace System.Xml.Serialization.Advanced
{
	// Token: 0x020001D5 RID: 469
	public class SchemaImporterExtensionCollection : CollectionBase
	{
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001F83 RID: 8067 RVA: 0x000AAA31 File Offset: 0x000A8C31
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

		// Token: 0x06001F84 RID: 8068 RVA: 0x000AAA4C File Offset: 0x000A8C4C
		public int Add(SchemaImporterExtension extension)
		{
			return this.Add(extension.GetType().FullName, extension);
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x000AAA60 File Offset: 0x000A8C60
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

		// Token: 0x06001F86 RID: 8070 RVA: 0x000AAAA0 File Offset: 0x000A8CA0
		public void Remove(string name)
		{
			if (this.Names[name] != null)
			{
				base.List.Remove(this.Names[name]);
				this.Names[name] = null;
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x000AAAD4 File Offset: 0x000A8CD4
		public new void Clear()
		{
			this.Names.Clear();
			base.List.Clear();
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000AAAEC File Offset: 0x000A8CEC
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

		// Token: 0x17000683 RID: 1667
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

		// Token: 0x06001F8B RID: 8075 RVA: 0x000AAB88 File Offset: 0x000A8D88
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

		// Token: 0x06001F8C RID: 8076 RVA: 0x000AABF6 File Offset: 0x000A8DF6
		public void Insert(int index, SchemaImporterExtension extension)
		{
			base.List.Insert(index, extension);
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000AAC05 File Offset: 0x000A8E05
		public int IndexOf(SchemaImporterExtension extension)
		{
			return base.List.IndexOf(extension);
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000AAC13 File Offset: 0x000A8E13
		public bool Contains(SchemaImporterExtension extension)
		{
			return base.List.Contains(extension);
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x000AAC21 File Offset: 0x000A8E21
		public void Remove(SchemaImporterExtension extension)
		{
			base.List.Remove(extension);
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x000AAC2F File Offset: 0x000A8E2F
		public void CopyTo(SchemaImporterExtension[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x04000D45 RID: 3397
		private Hashtable exNames;
	}
}
