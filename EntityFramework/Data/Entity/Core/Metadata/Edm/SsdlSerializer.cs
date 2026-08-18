using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200002E RID: 46
	public class SsdlSerializer
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001FE RID: 510 RVA: 0x0000BB30 File Offset: 0x00009D30
		// (remove) Token: 0x060001FF RID: 511 RVA: 0x0000BB68 File Offset: 0x00009D68
		public event EventHandler<DataModelErrorEventArgs> OnError;

		// Token: 0x06000200 RID: 512 RVA: 0x0000BBA0 File Offset: 0x00009DA0
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullability")]
		public virtual bool Serialize(EdmModel dbDatabase, string provider, string providerManifestToken, XmlWriter xmlWriter, bool serializeDefaultNullability = true)
		{
			Check.NotNull<EdmModel>(dbDatabase, "dbDatabase");
			Check.NotEmpty(provider, "provider");
			Check.NotEmpty(providerManifestToken, "providerManifestToken");
			Check.NotNull<XmlWriter>(xmlWriter, "xmlWriter");
			if (this.ValidateModel(dbDatabase))
			{
				SsdlSerializer.CreateVisitor(xmlWriter, dbDatabase, serializeDefaultNullability).Visit(dbDatabase, provider, providerManifestToken);
				return true;
			}
			return false;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000BBFC File Offset: 0x00009DFC
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Nullability")]
		public virtual bool Serialize(EdmModel dbDatabase, string namespaceName, string provider, string providerManifestToken, XmlWriter xmlWriter, bool serializeDefaultNullability = true)
		{
			Check.NotNull<EdmModel>(dbDatabase, "dbDatabase");
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotEmpty(provider, "provider");
			Check.NotEmpty(providerManifestToken, "providerManifestToken");
			Check.NotNull<XmlWriter>(xmlWriter, "xmlWriter");
			if (this.ValidateModel(dbDatabase))
			{
				SsdlSerializer.CreateVisitor(xmlWriter, dbDatabase, serializeDefaultNullability).Visit(dbDatabase, namespaceName, provider, providerManifestToken);
				return true;
			}
			return false;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		private bool ValidateModel(EdmModel model)
		{
			bool modelIsValid = true;
			Action<DataModelErrorEventArgs> onErrorAction = delegate(DataModelErrorEventArgs e)
			{
				MetadataItem item = e.Item;
				if (item == null || !MetadataItemHelper.IsInvalid(item))
				{
					modelIsValid = false;
					if (this.OnError != null)
					{
						this.OnError(this, e);
					}
				}
			};
			if (model.NamespaceNames.Count<string>() > 1 || model.Containers.Count<EntityContainer>() != 1)
			{
				onErrorAction(new DataModelErrorEventArgs
				{
					ErrorMessage = Strings.Serializer_OneNamespaceAndOneContainer
				});
			}
			DataModelValidator dataModelValidator = new DataModelValidator();
			dataModelValidator.OnError += delegate(object _, DataModelErrorEventArgs e)
			{
				onErrorAction(e);
			};
			dataModelValidator.Validate(model, true);
			return modelIsValid;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000BD5A File Offset: 0x00009F5A
		private static EdmSerializationVisitor CreateVisitor(XmlWriter xmlWriter, EdmModel dbDatabase, bool serializeDefaultNullability)
		{
			return new EdmSerializationVisitor(xmlWriter, dbDatabase.SchemaVersion, serializeDefaultNullability);
		}
	}
}
