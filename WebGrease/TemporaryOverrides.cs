using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x0200010A RID: 266
	public class TemporaryOverrides
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0004AA2C File Offset: 0x00048C2C
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x0004AA34 File Offset: 0x00048C34
		public bool SkipAll { get; set; }

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x0004AA3D File Offset: 0x00048C3D
		public string UniqueKey
		{
			get
			{
				return this.uniqueKey;
			}
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x0004AA54 File Offset: 0x00048C54
		public static TemporaryOverrides Load(string overrideFile)
		{
			TemporaryOverrides temporaryOverrides = new TemporaryOverrides();
			temporaryOverrides.LoadFromFile(overrideFile);
			temporaryOverrides.uniqueKey = temporaryOverrides.ToJson(true);
			if (!temporaryOverrides.resourcePivots.Any((KeyValuePair<string, List<string>> rp) => rp.Value.Any<string>()) && !temporaryOverrides.outputs.Any<string>() && !temporaryOverrides.SkipAll)
			{
				return null;
			}
			return temporaryOverrides;
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0004AABD File Offset: 0x00048CBD
		public bool ShouldIgnore(ContentItem contentItem)
		{
			return contentItem != null && this.ShouldIgnore(contentItem.ResourcePivotKeys);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x0004AAEC File Offset: 0x00048CEC
		public bool ShouldIgnore(IEnumerable<ResourcePivotKey> resourcePivotKeys)
		{
			if (resourcePivotKeys != null && resourcePivotKeys.Any<ResourcePivotKey>())
			{
				return (from rpk in resourcePivotKeys
				group rpk by rpk.GroupKey).Any((IGrouping<string, ResourcePivotKey> rpk) => rpk.All(new Func<ResourcePivotKey, bool>(this.ShouldIgnore)));
			}
			return false;
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x0004AB3A File Offset: 0x00048D3A
		public bool ShouldIgnore(IFileSet fileSet)
		{
			return fileSet != null && !string.IsNullOrWhiteSpace(fileSet.Output) && (this.ShouldIgnoreOutputs(fileSet) || this.ShouldIgnoreOutputExtensions(fileSet));
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0004AB80 File Offset: 0x00048D80
		private bool ShouldIgnore(ResourcePivotKey resourcePivotKey)
		{
			return this.resourcePivots.ContainsKey(resourcePivotKey.GroupKey) && this.resourcePivots[resourcePivotKey.GroupKey].Any<string>() && this.resourcePivots[resourcePivotKey.GroupKey].All((string pivotToIgnore) => resourcePivotKey.Key.IndexOf(pivotToIgnore, StringComparison.OrdinalIgnoreCase) == -1);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x0004ABF8 File Offset: 0x00048DF8
		private static IEnumerable<string> GetItems(string items)
		{
			if (items != null)
			{
				return items.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries);
			}
			return new string[0];
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x0004AC40 File Offset: 0x00048E40
		private static IEnumerable<string> GetElementItems(IEnumerable<XElement> elements, string elementName)
		{
			return from i in TemporaryOverrides.GetItems((from e in elements.Elements(elementName)
			select (string)e).FirstOrDefault<string>())
			where !i.IsNullOrWhitespace()
			select i.Trim();
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x0004AD40 File Offset: 0x00048F40
		private bool ShouldIgnoreOutputs(IFileSet fileSet)
		{
			return this.outputs.Any<string>() && !this.outputs.Any(delegate(string output)
			{
				if (fileSet.Output.IndexOf(output, StringComparison.OrdinalIgnoreCase) < 0)
				{
					return false;
				}
				if (output.IndexOf(".", StringComparison.OrdinalIgnoreCase) != -1)
				{
					return fileSet.Output.Count((char o) => o == '.') > 1;
				}
				return true;
			});
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x0004ADA0 File Offset: 0x00048FA0
		private bool ShouldIgnoreOutputExtensions(IFileSet fileSet)
		{
			return this.outputExtensions.Any<string>() && !this.outputExtensions.Any((string outputExtension) => fileSet.Output.EndsWith(outputExtension, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x0004ADEC File Offset: 0x00048FEC
		private void LoadFromFile(string overrideFile)
		{
			if (File.Exists(overrideFile))
			{
				try
				{
					XDocument xdocument = XDocument.Load(overrideFile);
					IEnumerable<XElement> enumerable = xdocument.Elements("Overrides");
					this.SkipAll = ((from a in enumerable.Attributes("SkipAll")
					select (bool?)a).FirstOrDefault<bool?>() == true);
					this.resourcePivots.Add("locales", TemporaryOverrides.GetElementItems(enumerable, "Locales").ToList<string>());
					this.resourcePivots.Add("themes", TemporaryOverrides.GetElementItems(enumerable, "Themes").ToList<string>());
					this.resourcePivots.Add("dpi", TemporaryOverrides.GetElementItems(enumerable, "Dpi").ToList<string>());
					this.outputs.AddRange(TemporaryOverrides.GetElementItems(enumerable, "Outputs"));
					this.outputExtensions.AddRange(TemporaryOverrides.GetElementItems(enumerable, "OutputExtensions"));
					foreach (XElement xelement in enumerable.Elements("ResourcePivot"))
					{
						this.resourcePivots.Add((string)xelement.Attribute("key"), ((string)xelement).SafeSplitSemiColonSeperatedValue().ToList<string>());
					}
				}
				catch (Exception inner)
				{
					throw new ConfigurationErrorsException(ResourceStrings.OverrideFileLoadErrorMessage.InvariantFormat(new object[]
					{
						overrideFile
					}), inner);
				}
			}
		}

		// Token: 0x0400068D RID: 1677
		private readonly IDictionary<string, List<string>> resourcePivots = new Dictionary<string, List<string>>();

		// Token: 0x0400068E RID: 1678
		private readonly List<string> outputs = new List<string>();

		// Token: 0x0400068F RID: 1679
		private readonly List<string> outputExtensions = new List<string>();

		// Token: 0x04000690 RID: 1680
		private string uniqueKey;
	}
}
