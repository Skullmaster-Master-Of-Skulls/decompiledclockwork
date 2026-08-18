using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000384 RID: 900
	internal class RowTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x06002090 RID: 8336 RVA: 0x00099ABD File Offset: 0x00097CBD
		internal RowTypeElement(SchemaElement parentElement) : base(parentElement)
		{
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x00099AD1 File Offset: 0x00097CD1
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.CanHandleElement(reader, "Property"))
			{
				this.HandlePropertyElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x00099AEC File Offset: 0x00097CEC
		protected void HandlePropertyElement(XmlReader reader)
		{
			RowTypePropertyElement rowTypePropertyElement = new RowTypePropertyElement(this);
			rowTypePropertyElement.Parse(reader);
			this._properties.Add(rowTypePropertyElement, true, new Func<object, string>(Strings.DuplicateEntityContainerMemberName));
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x00099B20 File Offset: 0x00097D20
		internal SchemaElementLookUpTable<RowTypePropertyElement> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00099B28 File Offset: 0x00097D28
		internal override void ResolveTopLevelNames()
		{
			foreach (RowTypePropertyElement rowTypePropertyElement in this._properties)
			{
				rowTypePropertyElement.ResolveTopLevelNames();
			}
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00099B74 File Offset: 0x00097D74
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

		// Token: 0x06002096 RID: 8342 RVA: 0x00099BF4 File Offset: 0x00097DF4
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

		// Token: 0x06002097 RID: 8343 RVA: 0x00099CB8 File Offset: 0x00097EB8
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

		// Token: 0x06002098 RID: 8344 RVA: 0x00099D18 File Offset: 0x00097F18
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

		// Token: 0x04000B8B RID: 2955
		private readonly SchemaElementLookUpTable<RowTypePropertyElement> _properties = new SchemaElementLookUpTable<RowTypePropertyElement>();
	}
}
