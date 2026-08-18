using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000013 RID: 19
	public class CsdlSerializer
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000AC RID: 172 RVA: 0x00004BEC File Offset: 0x00002DEC
		// (remove) Token: 0x060000AD RID: 173 RVA: 0x00004C24 File Offset: 0x00002E24
		public event EventHandler<DataModelErrorEventArgs> OnError;

		// Token: 0x060000AE RID: 174 RVA: 0x00004C9C File Offset: 0x00002E9C
		[SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
		public bool Serialize(EdmModel model, XmlWriter xmlWriter, string modelNamespace = null)
		{
			Check.NotNull<EdmModel>(model, "model");
			Check.NotNull<XmlWriter>(xmlWriter, "xmlWriter");
			bool modelIsValid = true;
			Action<DataModelErrorEventArgs> onErrorAction = delegate(DataModelErrorEventArgs e)
			{
				modelIsValid = false;
				if (this.OnError != null)
				{
					this.OnError(this, e);
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
			if (modelIsValid)
			{
				new EdmSerializationVisitor(xmlWriter, model.SchemaVersion, false).Visit(model, modelNamespace);
				return true;
			}
			return false;
		}
	}
}
