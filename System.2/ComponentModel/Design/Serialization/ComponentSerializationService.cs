using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x02000603 RID: 1539
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class ComponentSerializationService
	{
		// Token: 0x06003896 RID: 14486
		public abstract SerializationStore CreateStore();

		// Token: 0x06003897 RID: 14487
		public abstract SerializationStore LoadStore(Stream stream);

		// Token: 0x06003898 RID: 14488
		public abstract void Serialize(SerializationStore store, object value);

		// Token: 0x06003899 RID: 14489
		public abstract void SerializeAbsolute(SerializationStore store, object value);

		// Token: 0x0600389A RID: 14490
		public abstract void SerializeMember(SerializationStore store, object owningObject, MemberDescriptor member);

		// Token: 0x0600389B RID: 14491
		public abstract void SerializeMemberAbsolute(SerializationStore store, object owningObject, MemberDescriptor member);

		// Token: 0x0600389C RID: 14492
		public abstract ICollection Deserialize(SerializationStore store);

		// Token: 0x0600389D RID: 14493
		public abstract ICollection Deserialize(SerializationStore store, IContainer container);

		// Token: 0x0600389E RID: 14494
		public abstract void DeserializeTo(SerializationStore store, IContainer container, bool validateRecycledTypes, bool applyDefaults);

		// Token: 0x0600389F RID: 14495 RVA: 0x000F1D05 File Offset: 0x000EFF05
		public void DeserializeTo(SerializationStore store, IContainer container)
		{
			this.DeserializeTo(store, container, true, true);
		}

		// Token: 0x060038A0 RID: 14496 RVA: 0x000F1D11 File Offset: 0x000EFF11
		public void DeserializeTo(SerializationStore store, IContainer container, bool validateRecycledTypes)
		{
			this.DeserializeTo(store, container, validateRecycledTypes, true);
		}
	}
}
