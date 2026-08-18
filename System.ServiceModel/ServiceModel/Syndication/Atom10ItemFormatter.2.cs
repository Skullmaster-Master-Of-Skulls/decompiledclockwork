using System;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace System.ServiceModel.Syndication
{
	// Token: 0x02000185 RID: 389
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[XmlRoot(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
	public class Atom10ItemFormatter<TSyndicationItem> : Atom10ItemFormatter where TSyndicationItem : SyndicationItem, new()
	{
		// Token: 0x06000B91 RID: 2961 RVA: 0x0002B568 File Offset: 0x00029768
		public Atom10ItemFormatter() : base(typeof(TSyndicationItem))
		{
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0002B57A File Offset: 0x0002977A
		public Atom10ItemFormatter(TSyndicationItem itemToWrite) : base(itemToWrite)
		{
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0002B588 File Offset: 0x00029788
		protected override SyndicationItem CreateItemInstance()
		{
			return Activator.CreateInstance<TSyndicationItem>();
		}
	}
}
