using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200060D RID: 1549
	public interface IDesignerSerializationService
	{
		// Token: 0x060038D0 RID: 14544
		ICollection Deserialize(object serializationData);

		// Token: 0x060038D1 RID: 14545
		object Serialize(ICollection objects);
	}
}
