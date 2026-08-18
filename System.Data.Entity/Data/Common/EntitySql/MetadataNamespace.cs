using System;
using System.Data.Entity;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000350 RID: 848
	internal sealed class MetadataNamespace : MetadataMember
	{
		// Token: 0x06003190 RID: 12688 RVA: 0x000C2D18 File Offset: 0x000C0F18
		internal MetadataNamespace(string name) : base(MetadataMemberClass.Namespace, name)
		{
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x000C2D22 File Offset: 0x000C0F22
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataNamespace.NamespaceClassName;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x000C2D29 File Offset: 0x000C0F29
		internal static string NamespaceClassName
		{
			get
			{
				return Strings.LocalizedNamespace;
			}
		}
	}
}
