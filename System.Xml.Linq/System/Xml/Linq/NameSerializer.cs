using System;
using System.Runtime.Serialization;

namespace System.Xml.Linq
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	internal sealed class NameSerializer : IObjectReference, ISerializable
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003DC2 File Offset: 0x00001FC2
		private NameSerializer(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.expandedName = info.GetString("name");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003DE9 File Offset: 0x00001FE9
		object IObjectReference.GetRealObject(StreamingContext context)
		{
			return XName.Get(this.expandedName);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003DF6 File Offset: 0x00001FF6
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000064 RID: 100
		private string expandedName;
	}
}
