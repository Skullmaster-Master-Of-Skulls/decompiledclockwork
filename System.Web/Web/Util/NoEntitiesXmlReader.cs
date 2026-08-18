using System;
using System.IO;
using System.Xml;

namespace System.Web.Util
{
	// Token: 0x0200076F RID: 1903
	internal sealed class NoEntitiesXmlReader : XmlTextReader
	{
		// Token: 0x06005C4E RID: 23630 RVA: 0x001724A4 File Offset: 0x001714A4
		public NoEntitiesXmlReader(string filepath) : base(filepath)
		{
			this.Initialize();
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x001724B3 File Offset: 0x001714B3
		public NoEntitiesXmlReader(Stream datastream) : base(datastream)
		{
			this.Initialize();
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x001724C2 File Offset: 0x001714C2
		public NoEntitiesXmlReader(TextReader reader) : base(reader)
		{
			this.Initialize();
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x001724D1 File Offset: 0x001714D1
		public NoEntitiesXmlReader(string baseURI, Stream contentStream) : base(baseURI, contentStream)
		{
			this.Initialize();
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x001724E1 File Offset: 0x001714E1
		private void Initialize()
		{
			base.EntityHandling = EntityHandling.ExpandCharEntities;
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x001724EA File Offset: 0x001714EA
		public override void ResolveEntity()
		{
		}
	}
}
