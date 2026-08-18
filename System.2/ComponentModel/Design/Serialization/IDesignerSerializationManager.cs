using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x0200060B RID: 1547
	public interface IDesignerSerializationManager : IServiceProvider
	{
		// Token: 0x17000D95 RID: 3477
		// (get) Token: 0x060038C0 RID: 14528
		ContextStack Context { get; }

		// Token: 0x17000D96 RID: 3478
		// (get) Token: 0x060038C1 RID: 14529
		PropertyDescriptorCollection Properties { get; }

		// Token: 0x14000068 RID: 104
		// (add) Token: 0x060038C2 RID: 14530
		// (remove) Token: 0x060038C3 RID: 14531
		event ResolveNameEventHandler ResolveName;

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x060038C4 RID: 14532
		// (remove) Token: 0x060038C5 RID: 14533
		event EventHandler SerializationComplete;

		// Token: 0x060038C6 RID: 14534
		void AddSerializationProvider(IDesignerSerializationProvider provider);

		// Token: 0x060038C7 RID: 14535
		object CreateInstance(Type type, ICollection arguments, string name, bool addToContainer);

		// Token: 0x060038C8 RID: 14536
		object GetInstance(string name);

		// Token: 0x060038C9 RID: 14537
		string GetName(object value);

		// Token: 0x060038CA RID: 14538
		object GetSerializer(Type objectType, Type serializerType);

		// Token: 0x060038CB RID: 14539
		Type GetType(string typeName);

		// Token: 0x060038CC RID: 14540
		void RemoveSerializationProvider(IDesignerSerializationProvider provider);

		// Token: 0x060038CD RID: 14541
		void ReportError(object errorInformation);

		// Token: 0x060038CE RID: 14542
		void SetName(object instance, string name);
	}
}
