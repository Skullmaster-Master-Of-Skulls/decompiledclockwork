using System;
using System.IO;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200019A RID: 410
	public class CmsContentInfoParser
	{
		// Token: 0x06000FE3 RID: 4067 RVA: 0x0005C1DC File Offset: 0x0005B1DC
		protected CmsContentInfoParser(Stream data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.data = data;
			try
			{
				Asn1StreamParser asn1StreamParser = new Asn1StreamParser(data, CmsUtilities.MaximumMemory);
				this.contentInfo = new ContentInfoParser((Asn1SequenceParser)asn1StreamParser.ReadObject());
			}
			catch (IOException e)
			{
				throw new CmsException("IOException reading content.", e);
			}
			catch (InvalidCastException e2)
			{
				throw new CmsException("Unexpected object reading content.", e2);
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0005C260 File Offset: 0x0005B260
		public void Close()
		{
			this.data.Close();
		}

		// Token: 0x04000B85 RID: 2949
		protected ContentInfoParser contentInfo;

		// Token: 0x04000B86 RID: 2950
		protected Stream data;
	}
}
