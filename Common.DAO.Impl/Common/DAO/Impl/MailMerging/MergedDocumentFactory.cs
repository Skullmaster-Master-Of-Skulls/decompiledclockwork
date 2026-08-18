using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ClockWorkLogger;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Public.Interfaces;

namespace TechnoPro.Common.DAO.Impl.MailMerging
{
	// Token: 0x02000096 RID: 150
	public static class MergedDocumentFactory
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x000220A8 File Offset: 0x000202A8
		public static IMergedDocument GetMergedDocument(string binPath, eAllowedExtensionGroup extensionGroup, bool isLicensed)
		{
			AllowedExtensionGroupAttribute attribute = extensionGroup.GetAttribute<AllowedExtensionGroupAttribute>();
			bool flag = string.IsNullOrEmpty((attribute != null) ? attribute.MergedDocumentImplementationClass : null);
			if (flag)
			{
				throw new Exception("MergedDocumentFactory" + ":No class specified:extensionGroup={0}" + extensionGroup.ToString());
			}
			CWLogger.Logger.Debug("MergedDocumentFactory::binpath={0}:mergedocimpdll={1}", binPath ?? "NULL", (attribute == null) ? "NULL" : (attribute.MergedDocumentImplementationClass ?? "null"));
			string assemblyFile = Path.Combine(binPath, attribute.MergedDocumentImplementationDll);
			Assembly assembly = Assembly.LoadFrom(assemblyFile);
			IMergedDocument mergedDocument = (IMergedDocument)AppDomain.CurrentDomain.CreateInstanceAndUnwrap(assembly.FullName, attribute.MergedDocumentImplementationClass);
			mergedDocument.IsLicensed = isLicensed;
			return mergedDocument;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00022170 File Offset: 0x00020370
		public static eAllowedExtensionGroup GetAllowedExtensionGroupForFilename(this string filename)
		{
			bool flag = string.IsNullOrWhiteSpace(filename);
			eAllowedExtensionGroup result;
			if (flag)
			{
				result = eAllowedExtensionGroup.Unknown;
			}
			else
			{
				result = Path.GetExtension(filename).GetAllowedExtensionGroupForExtension();
			}
			return result;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0002219C File Offset: 0x0002039C
		public static eAllowedExtensionGroup GetAllowedExtensionGroupForExtension(this string ext)
		{
			Func<string, bool> <>9__1;
			return ((eAllowedExtensionGroup[])Enum.GetValues(typeof(eAllowedExtensionGroup))).FirstOrDefault(delegate(eAllowedExtensionGroup g)
			{
				AllowedExtensionGroupAttribute attribute = g.GetAttribute<AllowedExtensionGroupAttribute>();
				IEnumerable<string> source = ((attribute != null) ? attribute.AllowedExtensions : null) ?? new string[0];
				Func<string, bool> predicate;
				if ((predicate = <>9__1) == null)
				{
					predicate = (<>9__1 = ((string h) => h.Equals(ext, StringComparison.OrdinalIgnoreCase)));
				}
				return source.Any(predicate);
			});
		}

		// Token: 0x040001C1 RID: 449
		private const string ExceptionTitlePrefix = "MergedDocumentFactory";
	}
}
