using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Xml.Serialization
{
	// Token: 0x02000140 RID: 320
	public class ImportContext
	{
		// Token: 0x060016FA RID: 5882 RVA: 0x00066337 File Offset: 0x00064537
		public ImportContext(CodeIdentifiers identifiers, bool shareTypes)
		{
			this.typeIdentifiers = identifiers;
			this.shareTypes = shareTypes;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x0006634D File Offset: 0x0006454D
		internal ImportContext() : this(null, false)
		{
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x00066357 File Offset: 0x00064557
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

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060016FD RID: 5885 RVA: 0x00066372 File Offset: 0x00064572
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

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060016FE RID: 5886 RVA: 0x0006638D File Offset: 0x0006458D
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

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x000663A8 File Offset: 0x000645A8
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

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x000663C3 File Offset: 0x000645C3
		public bool ShareTypes
		{
			get
			{
				return this.shareTypes;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x000663CB File Offset: 0x000645CB
		public StringCollection Warnings
		{
			get
			{
				return this.Cache.Warnings;
			}
		}

		// Token: 0x04000AB0 RID: 2736
		private bool shareTypes;

		// Token: 0x04000AB1 RID: 2737
		private SchemaObjectCache cache;

		// Token: 0x04000AB2 RID: 2738
		private Hashtable mappings;

		// Token: 0x04000AB3 RID: 2739
		private Hashtable elements;

		// Token: 0x04000AB4 RID: 2740
		private CodeIdentifiers typeIdentifiers;
	}
}
