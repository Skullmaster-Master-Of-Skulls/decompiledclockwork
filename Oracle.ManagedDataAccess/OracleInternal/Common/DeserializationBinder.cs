using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace OracleInternal.Common
{
	// Token: 0x02000034 RID: 52
	internal sealed class DeserializationBinder : SerializationBinder
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		public override Type BindToType(string assemblyName, string typeName)
		{
			string fullName = Assembly.GetExecutingAssembly().FullName;
			return Type.GetType(string.Format("{0}, {1}", typeName, fullName));
		}
	}
}
