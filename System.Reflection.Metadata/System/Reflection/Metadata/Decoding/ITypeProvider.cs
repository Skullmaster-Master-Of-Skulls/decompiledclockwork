using System;

namespace System.Reflection.Metadata.Decoding
{
	// Token: 0x02000147 RID: 327
	internal interface ITypeProvider<TType>
	{
		// Token: 0x06000A70 RID: 2672
		TType GetTypeFromDefinition(MetadataReader reader, TypeDefinitionHandle handle, SignatureTypeHandleCode code);

		// Token: 0x06000A71 RID: 2673
		TType GetTypeFromReference(MetadataReader reader, TypeReferenceHandle handle, SignatureTypeHandleCode code);

		// Token: 0x06000A72 RID: 2674
		TType GetTypeFromSpecification(MetadataReader reader, TypeSpecificationHandle handle, SignatureTypeHandleCode code);
	}
}
