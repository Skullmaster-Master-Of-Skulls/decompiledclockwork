using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Xml.Serialization
{
	// Token: 0x020002B9 RID: 697
	public class ImportContext
	{
		// Token: 0x06002150 RID: 8528 RVA: 0x0009E097 File Offset: 0x0009D097
		public ImportContext(CodeIdentifiers identifiers, bool shareTypes)
		{
			this.typeIdentifiers = identifiers;
			this.shareTypes = shareTypes;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0009E0AD File Offset: 0x0009D0AD
		internal ImportContext() : this(null, false)
		{
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002152 RID: 8530 RVA: 0x0009E0B7 File Offset: 0x0009D0B7
		internal SchemaObjectCache Cache
		{
			get
			{
				if (this.cache == null)
				{
					this.cache = new SchemaObjectCache();
				}
				return this.cache;
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002153 RID: 8531 RVA: 0x0009E0D2 File Offset: 0x0009D0D2
		internal Hashtable Elements
		{
			get
			{
				if (this.elements == null)
				{
					this.elements = new Hashtable();
				}
				return this.elements;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002154 RID: 8532 RVA: 0x0009E0ED File Offset: 0x0009D0ED
		internal Hashtable Mappings
		{
			get
			{
				if (this.mappings == null)
				{
					this.mappings = new Hashtable();
				}
				return this.mappings;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002155 RID: 8533 RVA: 0x0009E108 File Offset: 0x0009D108
		public CodeIdentifiers TypeIdentifiers
		{
			get
			{
				if (this.typeIdentifiers == null)
				{
					this.typeIdentifiers = new CodeIdentifiers();
				}
				return this.typeIdentifiers;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x0009E123 File Offset: 0x0009D123
		public bool ShareTypes
		{
			get
			{
				return this.shareTypes;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x0009E12B File Offset: 0x0009D12B
		public StringCollection Warnings
		{
			get
			{
				return this.Cache.Warnings;
			}
		}

		// Token: 0x0400144B RID: 5195
		private bool shareTypes;

		// Token: 0x0400144C RID: 5196
		private SchemaObjectCache cache;

		// Token: 0x0400144D RID: 5197
		private Hashtable mappings;

		// Token: 0x0400144E RID: 5198
		private Hashtable elements;

		// Token: 0x0400144F RID: 5199
		private CodeIdentifiers typeIdentifiers;
	}
}
