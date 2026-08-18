using System;
using System.Collections;
using Org.BouncyCastle.Bcpg.Attr;

namespace Org.BouncyCastle.Bcpg.OpenPgp
{
	// Token: 0x02000604 RID: 1540
	public class PgpUserAttributeSubpacketVectorGenerator
	{
		// Token: 0x06003486 RID: 13446 RVA: 0x0014734B File Offset: 0x0014634B
		public virtual void SetImageAttribute(ImageAttrib.Format imageType, byte[] imageData)
		{
			if (imageData == null)
			{
				throw new ArgumentException("attempt to set null image", "imageData");
			}
			this.list.Add(new ImageAttrib(imageType, imageData));
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x00147373 File Offset: 0x00146373
		public virtual PgpUserAttributeSubpacketVector Generate()
		{
			return new PgpUserAttributeSubpacketVector((UserAttributeSubpacket[])this.list.ToArray(typeof(UserAttributeSubpacket)));
		}

		// Token: 0x04002353 RID: 9043
		private ArrayList list = new ArrayList();
	}
}
