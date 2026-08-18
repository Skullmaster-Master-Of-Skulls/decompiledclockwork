using System;
using System.Collections;
using System.Resources;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001D5 RID: 469
	internal sealed class CodeDomSerializationProvider : IDesignerSerializationProvider
	{
		// Token: 0x06001187 RID: 4487 RVA: 0x00060F34 File Offset: 0x0005F134
		private object GetCodeDomSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
		{
			if (currentSerializer != null)
			{
				return null;
			}
			if (objectType == null)
			{
				return PrimitiveCodeDomSerializer.Default;
			}
			if (typeof(IComponent).IsAssignableFrom(objectType))
			{
				return ComponentCodeDomSerializer.Default;
			}
			if (typeof(Enum).IsAssignableFrom(objectType))
			{
				return EnumCodeDomSerializer.Default;
			}
			if (objectType.IsPrimitive || objectType.IsEnum || objectType == typeof(string))
			{
				return PrimitiveCodeDomSerializer.Default;
			}
			if (typeof(ICollection).IsAssignableFrom(objectType))
			{
				return CollectionCodeDomSerializer.Default;
			}
			if (typeof(IContainer).IsAssignableFrom(objectType))
			{
				return ContainerCodeDomSerializer.Default;
			}
			if (typeof(ResourceManager).IsAssignableFrom(objectType))
			{
				return ResourceCodeDomSerializer.Default;
			}
			return CodeDomSerializer.Default;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00060FFA File Offset: 0x0005F1FA
		private object GetMemberCodeDomSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
		{
			if (currentSerializer != null)
			{
				return null;
			}
			if (typeof(PropertyDescriptor).IsAssignableFrom(objectType))
			{
				return PropertyMemberCodeDomSerializer.Default;
			}
			if (typeof(EventDescriptor).IsAssignableFrom(objectType))
			{
				return EventMemberCodeDomSerializer.Default;
			}
			return null;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00061032 File Offset: 0x0005F232
		private object GetTypeCodeDomSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
		{
			if (currentSerializer != null)
			{
				return null;
			}
			if (typeof(IComponent).IsAssignableFrom(objectType))
			{
				return ComponentTypeCodeDomSerializer.Default;
			}
			return TypeCodeDomSerializer.Default;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00061058 File Offset: 0x0005F258
		object IDesignerSerializationProvider.GetSerializer(IDesignerSerializationManager manager, object currentSerializer, Type objectType, Type serializerType)
		{
			if (serializerType == typeof(CodeDomSerializer))
			{
				return this.GetCodeDomSerializer(manager, currentSerializer, objectType, serializerType);
			}
			if (serializerType == typeof(MemberCodeDomSerializer))
			{
				return this.GetMemberCodeDomSerializer(manager, currentSerializer, objectType, serializerType);
			}
			if (serializerType == typeof(TypeCodeDomSerializer))
			{
				return this.GetTypeCodeDomSerializer(manager, currentSerializer, objectType, serializerType);
			}
			return null;
		}
	}
}
