using System;
using System.Drawing;

namespace Spire.Doc.Interface
{
	// Token: 0x02000504 RID: 1284
	public interface IXDLSAttributeReader
	{
		// Token: 0x06004246 RID: 16966
		bool HasAttribute(string name);

		// Token: 0x06004247 RID: 16967
		string ReadString(string name);

		// Token: 0x06004248 RID: 16968
		int ReadInt(string name);

		// Token: 0x06004249 RID: 16969
		short ReadShort(string name);

		// Token: 0x0600424A RID: 16970
		float ReadFloat(string name);

		// Token: 0x0600424B RID: 16971
		double ReadDouble(string name);

		// Token: 0x0600424C RID: 16972
		bool ReadBoolean(string name);

		// Token: 0x0600424D RID: 16973
		byte ReadByte(string name);

		// Token: 0x0600424E RID: 16974
		Enum ReadEnum(string name, Type enumType);

		// Token: 0x0600424F RID: 16975
		Color ReadColor(string name);

		// Token: 0x06004250 RID: 16976
		DateTime ReadDateTime(string s);
	}
}
