using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001AB RID: 427
	public class DisplayText : Asn1Encodable, IAsn1Choice
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x0005DB8C File Offset: 0x0005CB8C
		public DisplayText(int type, string text)
		{
			if (text.Length > 200)
			{
				text = text.Substring(0, 200);
			}
			this.contentType = type;
			switch (type)
			{
			case 0:
				this.contents = new DerIA5String(text);
				return;
			case 1:
				this.contents = new DerBmpString(text);
				return;
			case 2:
				this.contents = new DerUtf8String(text);
				return;
			case 3:
				this.contents = new DerVisibleString(text);
				return;
			default:
				this.contents = new DerUtf8String(text);
				return;
			}
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0005DC1B File Offset: 0x0005CC1B
		public DisplayText(string text)
		{
			if (text.Length > 200)
			{
				text = text.Substring(0, 200);
			}
			this.contentType = 2;
			this.contents = new DerUtf8String(text);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0005DC51 File Offset: 0x0005CC51
		public DisplayText(IAsn1String contents)
		{
			this.contents = contents;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0005DC60 File Offset: 0x0005CC60
		public static DisplayText GetInstance(object obj)
		{
			if (obj is IAsn1String)
			{
				return new DisplayText((IAsn1String)obj);
			}
			if (obj is DisplayText)
			{
				return (DisplayText)obj;
			}
			throw new ArgumentException("unknown object in factory: " + obj.GetType().Name, "obj");
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0005DCAF File Offset: 0x0005CCAF
		public override Asn1Object ToAsn1Object()
		{
			return (Asn1Object)this.contents;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0005DCBC File Offset: 0x0005CCBC
		public string GetString()
		{
			return this.contents.GetString();
		}

		// Token: 0x04000BF4 RID: 3060
		public const int ContentTypeIA5String = 0;

		// Token: 0x04000BF5 RID: 3061
		public const int ContentTypeBmpString = 1;

		// Token: 0x04000BF6 RID: 3062
		public const int ContentTypeUtf8String = 2;

		// Token: 0x04000BF7 RID: 3063
		public const int ContentTypeVisibleString = 3;

		// Token: 0x04000BF8 RID: 3064
		public const int DisplayTextMaximumSize = 200;

		// Token: 0x04000BF9 RID: 3065
		internal readonly int contentType;

		// Token: 0x04000BFA RID: 3066
		internal readonly IAsn1String contents;
	}
}
