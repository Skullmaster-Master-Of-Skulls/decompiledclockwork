using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000261 RID: 609
	internal sealed class MetadataNamespace : MetadataMember
	{
		// Token: 0x060014F3 RID: 5363 RVA: 0x0006306D File Offset: 0x0006126D
		internal MetadataNamespace(string name) : base(MetadataMemberClass.Namespace, name)
		{
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00063077 File Offset: 0x00061277
		internal override string MetadataMemberClassName
		{
			get
			{
				return MetadataNamespace.NamespaceClassName;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0006307E File Offset: 0x0006127E
		internal static string NamespaceClassName
		{
			get
			{
				return Strings.LocalizedNamespace;
			}
		}
	}
}
