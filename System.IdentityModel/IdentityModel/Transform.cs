using System;
using System.Xml;

namespace System.IdentityModel
{
	// Token: 0x020000B0 RID: 176
	internal abstract class Transform
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600055C RID: 1372
		public abstract string Algorithm { get; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00002D09 File Offset: 0x00000F09
		public virtual bool NeedsInclusiveContext
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600055E RID: 1374
		public abstract object Process(object input, SignatureResourcePool resourcePool, DictionaryManager dictionaryManager);

		// Token: 0x0600055F RID: 1375
		public abstract byte[] ProcessAndDigest(object input, SignatureResourcePool resourcePool, string digestAlgorithm, DictionaryManager dictionaryManager);

		// Token: 0x06000560 RID: 1376
		public abstract void ReadFrom(XmlDictionaryReader reader, DictionaryManager dictionaryManager, bool preserveComments);

		// Token: 0x06000561 RID: 1377
		public abstract void WriteTo(XmlDictionaryWriter writer, DictionaryManager dictionaryManager);
	}
}
