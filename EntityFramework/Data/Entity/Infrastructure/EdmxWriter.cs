using System;
using System.Data.Entity.Internal;
using System.Data.Entity.ModelConfiguration.Edm.Serialization;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Xml;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000758 RID: 1880
	public static class EdmxWriter
	{
		// Token: 0x06005529 RID: 21801 RVA: 0x001729C8 File Offset: 0x00170BC8
		public static void WriteEdmx(DbContext context, XmlWriter writer)
		{
			Check.NotNull<DbContext>(context, "context");
			Check.NotNull<XmlWriter>(writer, "writer");
			InternalContext internalContext = context.InternalContext;
			if (internalContext is EagerInternalContext)
			{
				throw Error.EdmxWriter_EdmxFromObjectContextNotSupported();
			}
			DbModel modelBeingInitialized = internalContext.ModelBeingInitialized;
			if (modelBeingInitialized != null)
			{
				EdmxWriter.WriteEdmx(modelBeingInitialized, writer);
				return;
			}
			DbCompiledModel codeFirstModel = internalContext.CodeFirstModel;
			if (codeFirstModel == null)
			{
				throw Error.EdmxWriter_EdmxFromModelFirstNotSupported();
			}
			DbModelBuilder dbModelBuilder = codeFirstModel.CachedModelBuilder.Clone();
			EdmxWriter.WriteEdmx((internalContext.ModelProviderInfo == null) ? dbModelBuilder.Build(internalContext.Connection) : dbModelBuilder.Build(internalContext.ModelProviderInfo), writer);
		}

		// Token: 0x0600552A RID: 21802 RVA: 0x00172A58 File Offset: 0x00170C58
		public static void WriteEdmx(DbModel model, XmlWriter writer)
		{
			Check.NotNull<DbModel>(model, "model");
			Check.NotNull<XmlWriter>(writer, "writer");
			new EdmxSerializer().Serialize(model.DatabaseMapping, writer);
		}
	}
}
