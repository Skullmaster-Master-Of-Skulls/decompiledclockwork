using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using ClockWorkLogger;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.Core.Inventory.Adapters;
using TechnoPro.Common.DAO.Impl.Inventory;
using TechnoPro.Common.DAO.Inventory;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Inventory;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Inventory;
using TechnoPro.Common.Public.Exceptions.PermissionDenied;

namespace TechnoPro.Common.Core.Inventory
{
	// Token: 0x020000E2 RID: 226
	public class InventoryCatalogManager : IInventoryCatalogManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x0003959F File Offset: 0x0003779F
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x000395A7 File Offset: 0x000377A7
		public IInventoryCatalogDAO InventoryCatalogDAO { get; set; }

		// Token: 0x0600089E RID: 2206 RVA: 0x000395B0 File Offset: 0x000377B0
		public InventoryCatalogManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.InventoryCatalogDAO = new InventoryCatalogDAO(this.OpContext);
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x000395D4 File Offset: 0x000377D4
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x000395DC File Offset: 0x000377DC
		public OperationContext OpContext { get; set; }

		// Token: 0x060008A1 RID: 2209 RVA: 0x000395E8 File Offset: 0x000377E8
		public InventoryCatalog GetCatalogById(int catalogId)
		{
			bool flag = this.OpContext.IsCatalogAllowedForUser(catalogId);
			if (flag)
			{
				return this.InventoryCatalogDAO.GetCatalogById(catalogId);
			}
			throw new PermissionDeniedException(string.Format("User Id '{0}' does not have permission to read Catalog Id '{1}'", this.OpContext.WhoAmI, catalogId));
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0003963C File Offset: 0x0003783C
		public InventoryCatalog GetCatalogByName(string name)
		{
			IList<int> allowedCatalogIds = this.OpContext.GetAllowedCatalogIds();
			return this.InventoryCatalogDAO.GetCatalogByName(allowedCatalogIds, name);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00039668 File Offset: 0x00037868
		public IList<InventoryCatalog> GetCatalogs()
		{
			IList<int> allowedCatalogIds = this.OpContext.IsInventoryAdmin(false) ? null : this.OpContext.GetAllowedCatalogIds();
			return this.InventoryCatalogDAO.GetCatalogs(allowedCatalogIds);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000396A8 File Offset: 0x000378A8
		public string ExportToXML(int catalogId)
		{
			InventoryCatalog catalogById = this.GetCatalogById(catalogId);
			bool flag = catalogById == null;
			if (flag)
			{
				throw new ArgumentException(string.Format("Catalog {0} does not exist", catalogId));
			}
			int[] array = (from c in catalogById.Categories
			where c.DynamicFormId > 0
			select c.DynamicFormId).ToArray<int>();
			string text = string.Empty;
			bool flag2 = array.Length != 0;
			if (flag2)
			{
				IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
				text = dynamicFormManager.ExportFormsWithFieldsToXmlNew(false, array);
			}
			XDocument xdocument = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), Array.Empty<object>());
			xdocument.Add(new XComment(string.Format("Inventory catalog exported on {0}", DateTime.Today.ToString("MMMM dd, yyyy"))));
			XElement xelement = this.CatalogToXML(catalogById);
			XElement xelement2 = string.IsNullOrEmpty(text) ? null : XElement.Parse(text);
			xdocument.Add((xelement2 != null) ? new XElement("InventoryCatalogExport", new object[]
			{
				xelement,
				xelement2
			}) : new XElement("InventoryCatalogExport", xelement));
			return xdocument.ToString();
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0003980C File Offset: 0x00037A0C
		public InventoryCatalog GetTemplateCatalogByName(string templatesPath, string name)
		{
			string text = Path.Combine(templatesPath, string.Format("{0}.xml", name));
			return File.Exists(text) ? this.GetCatalogFromFile(text, true) : null;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00039844 File Offset: 0x00037A44
		public IList<InventoryCatalog> GetTemplateCatalogs(string templatesPath)
		{
			List<InventoryCatalog> list = new List<InventoryCatalog>();
			bool flag = Directory.Exists(templatesPath);
			if (flag)
			{
				string[] files = Directory.GetFiles(templatesPath, "*.xml");
				list.AddRange(from fn in files.Where(new Func<string, bool>(File.Exists))
				select this.GetCatalogFromFile(fn, false) into catalog
				where catalog != null
				select catalog);
			}
			return list;
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000398C4 File Offset: 0x00037AC4
		private InventoryCatalog GetCatalogFromFile(string fn, bool includeCategories = false)
		{
			InventoryCatalog result;
			try
			{
				XDocument xdocument = XDocument.Load(fn);
				InventoryCatalog inventoryCatalog = new InventoryCatalog
				{
					CreationDate = DateTime.Today
				};
				XElement xelement = xdocument.Descendants("Catalog").FirstOrDefault<XElement>();
				bool flag = xelement == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					XAttribute xattribute = xelement.Attribute("name");
					bool flag2 = xattribute == null || string.IsNullOrEmpty(xattribute.Value);
					if (flag2)
					{
						result = null;
					}
					else
					{
						inventoryCatalog.Name = xattribute.Value;
						XAttribute xattribute2 = xelement.Attribute("description");
						inventoryCatalog.Description = ((xattribute2 != null && !string.IsNullOrEmpty(xattribute2.Value)) ? xattribute2.Value : string.Empty);
						if (includeCategories)
						{
							XElement xelement2 = xelement.Element("Categories");
							bool flag3 = xelement2 != null;
							if (flag3)
							{
								inventoryCatalog.Categories = new List<InventoryCategory>();
								IEnumerable<XElement> enumerable = xelement2.Elements("Add");
								foreach (XElement xelement3 in enumerable)
								{
									XAttribute xattribute3 = xelement3.Attribute("name");
									bool flag4 = xattribute3 != null && !string.IsNullOrEmpty(xattribute3.Value);
									if (flag4)
									{
										inventoryCatalog.Categories.Add(new InventoryCategory
										{
											CatalogId = inventoryCatalog.InventoryCatalogId,
											CategoryName = xattribute3.Value
										});
									}
								}
							}
						}
						result = inventoryCatalog;
					}
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("InventoryCatalogManager::GetCatalogFromFile: {0}", ex.ToString()), ex);
				result = null;
			}
			return result;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00039AC4 File Offset: 0x00037CC4
		private XElement CatalogToXML(InventoryCatalog catalog)
		{
			int[] array = (from c in catalog.Categories
			where c.DynamicFormId > 0
			select c.DynamicFormId).ToArray<int>();
			IDictionary<int, string> mapping = null;
			bool flag = array.Length != 0;
			if (flag)
			{
				IDynamicFormManager dynamicFormManager = new DynamicFormManager(this.OpContext);
				mapping = dynamicFormManager.LoadScreenUniqueIdsByScreenNums(array);
			}
			XName name = "Catalog";
			object[] array2 = new object[3];
			array2[0] = new XAttribute("name", catalog.Name);
			array2[1] = new XAttribute("description", catalog.Description ?? string.Empty);
			array2[2] = new XElement("Categories", from cat in catalog.Categories
			let fuid = (cat.DynamicFormId > 0 && mapping != null && mapping.ContainsKey(cat.DynamicFormId)) ? mapping[cat.DynamicFormId] : string.Empty
			select new XElement("Add", new object[]
			{
				new XAttribute("name", cat.CategoryName),
				new XAttribute("dynamicForm_uid", fuid)
			}));
			return new XElement(name, array2);
		}
	}
}
