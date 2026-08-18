using System;
using System.IO;
using System.Runtime.Serialization;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x02000054 RID: 84
	[CLSCompliant(false)]
	public interface Asn1Decoder : ISerializable
	{
		// Token: 0x06000329 RID: 809
		Asn1Object decode(sbyte[] value_Renamed);

		// Token: 0x0600032A RID: 810
		Asn1Object decode(Stream in_Renamed);

		// Token: 0x0600032B RID: 811
		Asn1Object decode(Stream in_Renamed, int[] length);

		// Token: 0x0600032C RID: 812
		object decodeBoolean(Stream in_Renamed, int len);

		// Token: 0x0600032D RID: 813
		object decodeNumeric(Stream in_Renamed, int len);

		// Token: 0x0600032E RID: 814
		object decodeOctetString(Stream in_Renamed, int len);

		// Token: 0x0600032F RID: 815
		object decodeCharacterString(Stream in_Renamed, int len);
	}
}
