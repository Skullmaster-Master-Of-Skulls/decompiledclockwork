using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000323 RID: 803
	internal class RowTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x06002F55 RID: 12117 RVA: 0x000B304B File Offset: 0x000B124B
		internal RowTypeElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000B305F File Offset: 0x000B125F
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.CanHandleElement(reader, "Property"))
			{
				this.HandlePropertyElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000B307C File Offset: 0x000B127C
		protected void HandlePropertyElement(XmlReader reader)
		{
			RowTypePropertyElement rowTypePropertyElement = new RowTypePropertyElement(this);
			rowTypePropertyElement.Parse(reader);
			this._properties.Add(rowTypePropertyElement, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x000B30B0 File Offset: 0x000B12B0
		internal SchemaElementLookUpTable<RowTypePropertyElement> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000B30B8 File Offset: 0x000B12B8
		internal override void ResolveTopLevelNames()
		{
			foreach (RowTypePropertyElement rowTypePropertyElement in this._properties)
			{
				rowTypePropertyElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000B3104 File Offset: 0x000B1304
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Row[");
			bool flag = true;
			foreach (RowTypePropertyElement rowTypePropertyElement in this._properties)
			{
				if (flag)
				{
					flag = !flag;
				}
				else
				{
					builder.Append(", ");
				}
				rowTypePropertyElement.WriteIdentity(builder);
			}
			builder.Append("]");
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000B3184 File Offset: 0x000B1384
		internal override TypeUsage GetTypeUsage()
		{
			if (this._typeUsage == null)
			{
				List<EdmProperty> list = new List<EdmProperty>();
				foreach (RowTypePropertyElement rowTypePropertyElement in this._properties)
				{
					EdmProperty edmProperty = new EdmProperty(rowTypePropertyElement.FQName, rowTypePropertyElement.GetTypeUsage());
					edmProperty.AddMetadataProperties(rowTypePropertyElement.OtherContent);
					list.Add(edmProperty);
				}
				RowType rowType = new RowType(list);
				if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
				{
					rowType.DataSpace = DataSpace.CSpace;
				}
				else
				{
					rowType.DataSpace = DataSpace.SSpace;
				}
				rowType.AddMetadataProperties(base.OtherContent);
				this._typeUsage = TypeUsage.Create(rowType);
			}
			return this._typeUsage;
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000B3248 File Offset: 0x000B1448
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			bool result = true;
			if (this._typeUsage == null)
			{
				foreach (RowTypePropertyElement rowTypePropertyElement in this._properties)
				{
					if (!rowTypePropertyElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems))
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000B32A8 File Offset: 0x000B14A8
		internal override void Validate()
		{
			foreach (RowTypePropertyElement rowTypePropertyElement in this._properties)
			{
				rowTypePropertyElement.Validate();
			}
			if (this._properties.Count == 0)
			{
				base.AddError(ErrorCode.RowTypeWithoutProperty, EdmSchemaErrorSeverity.Error, Strings.RowTypeWithoutProperty);
			}
		}

		// Token: 0x0400145D RID: 5213
		private readonly SchemaElementLookUpTable<RowTypePropertyElement> _properties = new SchemaElementLookUpTable<RowTypePropertyElement>();
	}
}
