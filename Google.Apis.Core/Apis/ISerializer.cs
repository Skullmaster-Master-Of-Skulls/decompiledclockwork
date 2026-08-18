using System;
using System.IO;

namespace Google.Apis
{
	// Token: 0x02000004 RID: 4
	public interface ISerializer
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11
		string Format { get; }

		// Token: 0x0600000C RID: 12
		void Serialize(object obj, Stream target);

		// Token: 0x0600000D RID: 13
		string Serialize(object obj);

		// Token: 0x0600000E RID: 14
		T Deserialize<T>(string input);

		// Token: 0x0600000F RID: 15
		object Deserialize(string input, Type type);

		// Token: 0x06000010 RID: 16
		T Deserialize<T>(Stream input);
	}
}
