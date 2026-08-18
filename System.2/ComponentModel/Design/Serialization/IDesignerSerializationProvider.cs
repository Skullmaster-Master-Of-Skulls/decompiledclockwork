using System;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200060C RID: 1548
	public interface IDesignerSerializationProvider
	{
		// Token: 0x060038CF RID: 14543
		object GetSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType);
	}
}
