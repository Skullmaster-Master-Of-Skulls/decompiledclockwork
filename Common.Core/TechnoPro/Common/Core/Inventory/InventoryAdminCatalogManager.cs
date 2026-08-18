using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.DAO.Impl.Inventory;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Inventory;

namespace TechnoPro.Common.Core.Inventory
{
	// Token: 0x020000E0 RID: 224
	public class InventoryAdminCatalogManager : IInventoryAdminCatalogManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00039004 File Offset: 0x00037204
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x0003900C File Offset: 0x0003720C
		public IInventoryCatalogDAO InventoryCatalogDAO { get; set; }

		// Token: 0x06000885 RID: 2181 RVA: 0x00039015 File Offset: 0x00037215
		public InventoryAdminCatalogManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.InventoryCatalogDAO = new InventoryCatalogDAO(this.OpContext);
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x00039039 File Offset: 0x00037239
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x00039041 File Offset: 0x00037241
		public OperationContext OpContext { get; set; }

		// Token: 0x06000888 RID: 2184 RVA: 0x0003904C File Offset: 0x0003724C
		public IList<InventoryCatalog> GetFullCatalogList()
		{
			return this.InventoryCatalogDAO.GetCatalogs(null);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0003906C File Offset: 0x0003726C
		public int CreateCatalog(InventoryCatalog catalog)
		{
			return this.InventoryCatalogDAO.CreateCatalog(catalog);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0003908A File Offset: 0x0003728A
		public void UpdateCatalog(InventoryCatalog catalog)
		{
			this.InventoryCatalogDAO.UpdateCatalog(catalog);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x0003909C File Offset: 0x0003729C
		public bool DeleteEmptyCatalog(int catalogId)
		{
			IInventoryCategoryDAO inventoryCategoryDAO = new InventoryCategoryDAO(this.OpContext);
			inventoryCategoryDAO.DeleteRootCategory(catalogId);
			return this.InventoryCatalogDAO.DeleteEmptyCatalog(catalogId);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x000390D0 File Offset: 0x000372D0
		public int ImportFromXML(string catalogXmlDoc, string catalogName = null, string catalogDescription = null)
		{
			IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
			IList<DynamicFormWithFields> source = dynamicFormManager.ImportFormsFromXmlNew(catalogXmlDoc, true);
			InventoryCatalog inventoryCatalog = new InventoryCatalog
			{
				Name = catalogName,
				Description = catalogDescription,
				CreationDate = DateTime.Today
			};
			XDocument xdocument = XDocument.Parse(catalogXmlDoc);
			XElement xelement = xdocument.Descendants("Catalog").FirstOrDefault<XElement>();
			bool flag = xelement == null;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				XAttribute xattribute = xelement.Attribute("name");
				bool flag2 = xattribute == null || string.IsNullOrEmpty(xattribute.Value);
				if (flag2)
				{
					result = 0;
				}
				else
				{
					bool flag3 = string.IsNullOrEmpty(inventoryCatalog.Name);
					if (flag3)
					{
						inventoryCatalog.Name = ((xattribute != null && !string.IsNullOrEmpty(xattribute.Value)) ? xattribute.Value : string.Empty);
					}
					bool flag4 = string.IsNullOrEmpty(inventoryCatalog.Description);
					if (flag4)
					{
						XAttribute xattribute2 = xelement.Attribute("description");
						inventoryCatalog.Description = ((xattribute2 != null && !string.IsNullOrEmpty(xattribute2.Value)) ? xattribute2.Value : string.Empty);
					}
					inventoryCatalog.InventoryCatalogId = this.CreateCatalog(inventoryCatalog);
					bool flag5 = inventoryCatalog.InventoryCatalogId > 0;
					if (flag5)
					{
						XElement xelement2 = xelement.Element("Categories");
						bool flag6 = xelement2 != null;
						if (flag6)
						{
							IInventoryCategoryManager inventoryCategoryManager = new InventoryCategoryManager(this.OpContext);
							IEnumerable<XElement> enumerable = xelement2.Elements("Add");
							foreach (XElement xelement3 in enumerable)
							{
								XAttribute xattribute3 = xelement3.Attribute("name");
								XAttribute dynamicFormAtt = xelement3.Attribute("dynamicForm_uid");
								bool flag7 = xattribute3 != null && !string.IsNullOrEmpty(xattribute3.Value);
								if (flag7)
								{
									DynamicFormWithFields dynamicFormWithFields;
									if (dynamicFormAtt == null || string.IsNullOrEmpty(dynamicFormAtt.Value))
									{
										dynamicFormWithFields = null;
									}
									else
									{
										dynamicFormWithFields = (from f in source
										where f.Form != null
										select f).FirstOrDefault((DynamicFormWithFields f) => f.Form.UniqueId == dynamicFormAtt.Value);
									}
									DynamicFormWithFields dynamicFormWithFields2 = dynamicFormWithFields;
									inventoryCategoryManager.CreateCategory(new InventoryCategory
									{
										CatalogId = inventoryCatalog.InventoryCatalogId,
										DynamicFormId = ((dynamicFormWithFields2 != null) ? dynamicFormWithFields2.Form.ScreenNum : 0),
										CategoryName = (string.IsNullOrEmpty(catalogName) ? xattribute3.Value : string.Format("{0}{1}", inventoryCatalog.Name, (xattribute3.Value.Length > xattribute.Value.Length) ? xattribute3.Value.Substring(xattribute.Value.Length) : string.Empty))
									});
								}
							}
						}
					}
					result = inventoryCatalog.InventoryCatalogId;
				}
			}
			return result;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00039404 File Offset: 0x00037604
		public int ImportFromTemplate(string templatesPath, string templateName, string catalogName = null, string catalogDescription = null)
		{
			string text = Path.Combine(templatesPath, string.Format("{0}.xml", templateName));
			bool flag = File.Exists(text);
			int result;
			if (flag)
			{
				XDocument xdocument = XDocument.Load(text);
				result = this.ImportFromXML(xdocument.ToString(), catalogName, catalogDescription);
			}
			else
			{
				result = 0;
			}
			return result;
		}
	}
}
