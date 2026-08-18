using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using WebGrease.Common;
using WebGrease.Configuration;
using WebGrease.Extensions;

namespace WebGrease.Activities
{
	// Token: 0x020001BB RID: 443
	internal sealed class FileHasherActivity
	{
		// Token: 0x06001692 RID: 5778 RVA: 0x00081B84 File Offset: 0x0007FD84
		internal FileHasherActivity(IWebGreaseContext context)
		{
			this.context = context;
			this.SourceDirectories = new List<string>();
			this.ConfigType = context.Configuration.ConfigType;
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001693 RID: 5779 RVA: 0x00081BBA File Offset: 0x0007FDBA
		// (set) Token: 0x06001694 RID: 5780 RVA: 0x00081BC2 File Offset: 0x0007FDC2
		internal string ConfigType { get; set; }

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x00081BCB File Offset: 0x0007FDCB
		// (set) Token: 0x06001696 RID: 5782 RVA: 0x00081BD3 File Offset: 0x0007FDD3
		internal IList<string> SourceDirectories { get; private set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x00081BDC File Offset: 0x0007FDDC
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x00081BE4 File Offset: 0x0007FDE4
		internal string DestinationDirectory { private get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x00081BED File Offset: 0x0007FDED
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x00081BF5 File Offset: 0x0007FDF5
		internal bool CreateExtraDirectoryLevelFromHashes { private get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00081BFE File Offset: 0x0007FDFE
		// (set) Token: 0x0600169C RID: 5788 RVA: 0x00081C06 File Offset: 0x0007FE06
		internal string BasePrefixToAddToOutputPath { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x00081C0F File Offset: 0x0007FE0F
		// (set) Token: 0x0600169E RID: 5790 RVA: 0x00081C17 File Offset: 0x0007FE17
		internal FileTypes FileType { private get; set; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x00081C20 File Offset: 0x0007FE20
		// (set) Token: 0x060016A0 RID: 5792 RVA: 0x00081C28 File Offset: 0x0007FE28
		internal string BasePrefixToRemoveFromOutputPathInLog { get; set; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00081C31 File Offset: 0x0007FE31
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x00081C39 File Offset: 0x0007FE39
		internal string BasePrefixToRemoveFromInputPathInLog { get; set; }

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00081C42 File Offset: 0x0007FE42
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x00081C4A File Offset: 0x0007FE4A
		internal string LogFileName { get; set; }

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00081C53 File Offset: 0x0007FE53
		// (set) Token: 0x060016A6 RID: 5798 RVA: 0x00081C5B File Offset: 0x0007FE5B
		internal bool ShouldPreserveSourceDirectoryStructure { private get; set; }

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00081C64 File Offset: 0x0007FE64
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x00081C6C File Offset: 0x0007FE6C
		internal string FileTypeFilter { private get; set; }

		// Token: 0x060016A9 RID: 5801 RVA: 0x00081DA0 File Offset: 0x0007FFA0
		internal void Execute()
		{
			this.renamedFilesLog.Clear();
			this.context.SectionedAction(new string[]
			{
				"FileHasherActivity",
				this.FileType.ToString()
			}).Execute(delegate()
			{
				try
				{
					if (this.SourceDirectories == null || this.SourceDirectories.Count == 0)
					{
						Trace.TraceInformation("FileHasherActivity - No source directories passed and hence no action taken for the activity.");
					}
					else
					{
						if (string.IsNullOrWhiteSpace(this.DestinationDirectory))
						{
							this.DestinationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
						}
						IEnumerable<string> filters = FileHasherActivity.GetFilters(this.FileTypeFilter);
						if (string.IsNullOrWhiteSpace(this.BasePrefixToRemoveFromOutputPathInLog))
						{
							this.BasePrefixToRemoveFromOutputPathInLog = string.Empty;
						}
						if (string.IsNullOrWhiteSpace(this.BasePrefixToRemoveFromInputPathInLog))
						{
							this.BasePrefixToRemoveFromInputPathInLog = string.Empty;
						}
						foreach (string text in this.SourceDirectories)
						{
							if (!Directory.Exists(text))
							{
								Trace.TraceWarning(string.Format(CultureInfo.InvariantCulture, ResourceStrings.FileHasheActivityCouldNotLocateDirectory, new object[]
								{
									text
								}));
							}
							else
							{
								this.Hash(text, this.DestinationDirectory, filters, null);
							}
						}
						this.Save(true);
					}
				}
				catch (Exception inner)
				{
					throw new WorkflowException(ResourceStrings.FileHasherActivityErrorOccurred, inner);
				}
			});
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00081DF8 File Offset: 0x0007FFF8
		internal IEnumerable<ContentItem> Hash(ContentItem contentItem, IEnumerable<string> originalFiles)
		{
			List<ContentItem> list = new List<ContentItem>();
			if (originalFiles.Any<string>())
			{
				string relativeContentPath = originalFiles.FirstOrDefault<string>();
				ContentItem contentItem2 = this.Hash(ContentItem.FromContentItem(contentItem, relativeContentPath, null));
				list.Add(contentItem2);
				list.AddRange(this.AppendToWorkLog(contentItem2, originalFiles.Skip(1)));
			}
			return list;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00081E48 File Offset: 0x00080048
		internal IEnumerable<ContentItem> AppendToWorkLog(ContentItem hashedContentItem, IEnumerable<string> originalFiles)
		{
			List<ContentItem> list = new List<ContentItem>();
			foreach (string relativeContentPath in originalFiles)
			{
				ContentItem contentItem = ContentItem.FromContentItem(hashedContentItem, relativeContentPath, null);
				this.AppendToWorkLog(contentItem);
				list.Add(contentItem);
			}
			return list;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00081EA8 File Offset: 0x000800A8
		internal ContentItem Hash(ContentItem contentItem)
		{
			string relativeContentPath = contentItem.RelativeContentPath;
			string hashedFileName = contentItem.GetContentHash(this.context) + Path.GetExtension(relativeContentPath);
			string destinationFilePath = this.GetDestinationFilePath(this.DestinationDirectory, hashedFileName, contentItem.RelativeContentPath);
			string text = this.context.Configuration.DestinationDirectory ?? this.DestinationDirectory;
			string text2 = destinationFilePath;
			if (!string.IsNullOrWhiteSpace(text) && Path.IsPathRooted(text2))
			{
				text2 = text2.MakeRelativeToDirectory(text);
			}
			contentItem = ContentItem.FromContentItem(contentItem, null, text2);
			contentItem.WriteToRelativeHashedPath(text, false);
			this.AppendToWorkLog(contentItem);
			return contentItem;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00081F3C File Offset: 0x0008013C
		internal void Save(bool append = true)
		{
			this.WriteLog(append);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00081F48 File Offset: 0x00080148
		internal void AppendToWorkLog(IEnumerable<ContentItem> cacheResults)
		{
			foreach (ContentItem cacheResult in cacheResults)
			{
				this.AppendToWorkLog(cacheResult);
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00081F90 File Offset: 0x00080190
		internal void AppendToWorkLog(ContentItem cacheResult)
		{
			this.AppendToWorkLog(cacheResult.RelativeContentPath, cacheResult.RelativeHashedContentPath, false);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00081FA8 File Offset: 0x000801A8
		private static IEnumerable<string> GetFilters(string filterType)
		{
			if (!string.IsNullOrWhiteSpace(filterType))
			{
				return filterType.Split(Strings.FileFilterSeparator, StringSplitOptions.RemoveEmptyEntries);
			}
			return new string[]
			{
				"*"
			};
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00081FDA File Offset: 0x000801DA
		private static string GetUrlPath(string key)
		{
			return key.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00082044 File Offset: 0x00080244
		private IEnumerable<ContentItem> Hash(string sourceDirectory, string destinationDirectory, IEnumerable<string> filters, string rootSourceDirectory = null)
		{
			List<ContentItem> list = new List<ContentItem>();
			Directory.CreateDirectory(destinationDirectory);
			DirectoryInfo sourceDirectoryInfo = new DirectoryInfo(sourceDirectory);
			rootSourceDirectory = (rootSourceDirectory ?? sourceDirectoryInfo.FullName);
			list.AddRange(filters.SelectMany((string filter) => from sourceFileInfo in sourceDirectoryInfo.EnumerateFiles(filter, SearchOption.TopDirectoryOnly)
			select this.Hash(ContentItem.FromFile(sourceFileInfo.FullName, sourceFileInfo.FullName.MakeRelativeToDirectory(rootSourceDirectory), null, new ResourcePivotKey[0]))));
			foreach (DirectoryInfo directoryInfo in sourceDirectoryInfo.GetDirectories())
			{
				string destinationDirectory2 = this.ShouldPreserveSourceDirectoryStructure ? Path.Combine(destinationDirectory, directoryInfo.Name) : destinationDirectory;
				list.AddRange(this.Hash(directoryInfo.FullName, destinationDirectory2, filters, rootSourceDirectory));
			}
			return list;
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0008210C File Offset: 0x0008030C
		private string GetDestinationFilePath(string destination, string hashedFileName, string relativePath)
		{
			string text;
			if (this.CreateExtraDirectoryLevelFromHashes)
			{
				text = Path.Combine(destination, hashedFileName.Substring(0, 2)).ToLowerInvariant();
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				text = Path.Combine(text, hashedFileName.Remove(0, 2));
			}
			else if (this.ShouldPreserveSourceDirectoryStructure)
			{
				text = Path.Combine(destination, Path.GetDirectoryName(relativePath), hashedFileName);
			}
			else
			{
				text = Path.Combine(destination, hashedFileName);
			}
			return text.ToLowerInvariant();
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x000821A0 File Offset: 0x000803A0
		private void AppendToWorkLog(string fileBeforeHashing, string fileAfterHashing, bool skipIfExists = false)
		{
			fileAfterHashing = Path.Combine(this.context.Configuration.DestinationDirectory ?? this.DestinationDirectory, fileAfterHashing);
			fileBeforeHashing = this.NormalizeFileForWorkLog(fileBeforeHashing, this.BasePrefixToRemoveFromInputPathInLog);
			fileAfterHashing = this.NormalizeFileForWorkLog(fileAfterHashing, this.BasePrefixToRemoveFromOutputPathInLog);
			if (Path.IsPathRooted(fileBeforeHashing))
			{
				fileBeforeHashing = fileBeforeHashing.MakeRelativeToDirectory(this.BasePrefixToRemoveFromInputPathInLog);
			}
			if (!this.renamedFilesLog.ContainsKey(fileBeforeHashing) || this.renamedFilesLog[fileBeforeHashing].Equals(fileAfterHashing))
			{
				this.renamedFilesLog[fileBeforeHashing] = fileAfterHashing;
				return;
			}
			if (skipIfExists)
			{
				if (File.Exists(fileAfterHashing))
				{
					File.Delete(fileAfterHashing);
					string directoryName = Path.GetDirectoryName(fileAfterHashing);
					if (!Directory.EnumerateFiles(directoryName).Any<string>())
					{
						Directory.Delete(directoryName);
					}
				}
				return;
			}
			string format = "The renamed filename already has a rename to a different file: \r\nBeforehashing:{0} \r\nNewAfterHashing:{1} ExistingAfterhashing:{2}";
			object[] array = new object[3];
			array[0] = fileBeforeHashing;
			array[1] = fileAfterHashing;
			array[2] = string.Join(",", from rfl in this.renamedFilesLog
			where rfl.Key.Equals(fileBeforeHashing)
			select rfl into e
			select e.Key);
			throw new BuildWorkflowException(format.InvariantFormat(array));
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0008230C File Offset: 0x0008050C
		private string MakeOutputAbsolute(string output)
		{
			if (!string.IsNullOrWhiteSpace(this.BasePrefixToAddToOutputPath) && output.StartsWith(this.BasePrefixToAddToOutputPath, StringComparison.OrdinalIgnoreCase))
			{
				output = output.Substring(this.BasePrefixToAddToOutputPath.Length);
			}
			return Path.Combine(this.BasePrefixToRemoveFromOutputPathInLog ?? this.DestinationDirectory, output.NormalizeUrl());
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00082364 File Offset: 0x00080564
		private string NormalizeFileForWorkLog(string file, string preFixToRemoveFromWorkLog)
		{
			if (Path.IsPathRooted(file))
			{
				file = file.MakeRelativeToDirectory(preFixToRemoveFromWorkLog);
			}
			else if (!string.IsNullOrWhiteSpace(preFixToRemoveFromWorkLog))
			{
				string text = preFixToRemoveFromWorkLog.MakeRelativeToDirectory(this.DestinationDirectory);
				if (!string.IsNullOrWhiteSpace(text) && file.StartsWith(text, StringComparison.OrdinalIgnoreCase))
				{
					file = file.Substring(text.Length);
				}
			}
			return file.NormalizeUrl();
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00082508 File Offset: 0x00080708
		private void WriteLog(bool appendToLog = true)
		{
			if (string.IsNullOrWhiteSpace(this.LogFileName))
			{
				return;
			}
			if (appendToLog)
			{
				this.LoadBeforeWrite(this.LogFileName);
			}
			StringBuilder stringBuilder = new StringBuilder();
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Indent = true,
				OmitXmlDeclaration = true
			};
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings))
			{
				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement("RenamedFiles");
				xmlWriter.WriteAttributeString("configType", this.ConfigType);
				if (this.renamedFilesLog == null || this.renamedFilesLog.Keys.Count < 1)
				{
					xmlWriter.WriteComment(ResourceStrings.NoFilesProcessed);
				}
				else
				{
					var enumerable = (from rfl in this.renamedFilesLog
					orderby rfl.Value
					select rfl).GroupBy((KeyValuePair<string, string> rfl) => rfl.Value, (KeyValuePair<string, string> rfl) => rfl.Key, (string key, IEnumerable<string> g) => new
					{
						FileAfterHashing = key,
						FilesBeforeHashing = g.ToList<string>()
					});
					foreach (var <>f__AnonymousTypef in enumerable)
					{
						if (<>f__AnonymousTypef.FilesBeforeHashing.Any<string>())
						{
							xmlWriter.WriteStartElement("File");
							xmlWriter.WriteStartElement("Output");
							string text = FileHasherActivity.GetUrlPath(<>f__AnonymousTypef.FileAfterHashing);
							text = (this.BasePrefixToAddToOutputPath ?? Path.AltDirectorySeparatorChar.ToString(CultureInfo.InvariantCulture)) + text.TrimStart(new char[]
							{
								Path.AltDirectorySeparatorChar
							});
							xmlWriter.WriteValue(text);
							xmlWriter.WriteEndElement();
							foreach (string key2 in from r in <>f__AnonymousTypef.FilesBeforeHashing
							orderby r
							select r)
							{
								xmlWriter.WriteStartElement("Input");
								xmlWriter.WriteValue(Path.AltDirectorySeparatorChar + FileHasherActivity.GetUrlPath(key2).TrimStart(new char[]
								{
									Path.AltDirectorySeparatorChar
								}));
								xmlWriter.WriteEndElement();
							}
							xmlWriter.WriteEndElement();
						}
					}
				}
				xmlWriter.WriteEndElement();
			}
			FileHelper.WriteFile(this.LogFileName, stringBuilder.ToString());
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x000827FC File Offset: 0x000809FC
		private void LoadBeforeWrite(string logFileName)
		{
			string configTypeLogFile = FileHasherActivity.GetConfigTypeLogFile(logFileName, this.ConfigType);
			XElement xelement = null;
			if (!File.Exists(logFileName))
			{
				if (File.Exists(configTypeLogFile))
				{
					xelement = FileHasherActivity.GetLogRoot(configTypeLogFile);
				}
			}
			else
			{
				xelement = FileHasherActivity.GetLogRoot(logFileName);
			}
			if (xelement != null)
			{
				string text = (string)xelement.Attribute("configType");
				if (text != this.ConfigType)
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						string configTypeLogFile2 = FileHasherActivity.GetConfigTypeLogFile(logFileName, text);
						File.Copy(logFileName, configTypeLogFile2, true);
					}
					if (!File.Exists(configTypeLogFile))
					{
						return;
					}
					xelement = FileHasherActivity.GetLogRoot(configTypeLogFile);
				}
				IEnumerable<XElement> enumerable = xelement.Elements("File");
				foreach (XElement xelement2 in enumerable)
				{
					string text2 = (from e in xelement2.Elements("Output")
					select (string)e).FirstOrDefault<string>();
					if (!string.IsNullOrWhiteSpace(text2))
					{
						string text3 = this.MakeOutputAbsolute(text2);
						if (File.Exists(text3))
						{
							IEnumerable<string> enumerable2 = from e in xelement2.Elements("Input")
							select (string)e;
							foreach (string fileBeforeHashing in enumerable2)
							{
								this.AppendToWorkLog(fileBeforeHashing, text3, true);
							}
						}
					}
				}
			}
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x000829AC File Offset: 0x00080BAC
		private static XElement GetLogRoot(string logFileName)
		{
			XDocument xdocument = XDocument.Load(logFileName);
			return xdocument.Element("RenamedFiles");
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000829D4 File Offset: 0x00080BD4
		private static string GetConfigTypeLogFile(string logFileName, string configType)
		{
			if (string.IsNullOrWhiteSpace(configType))
			{
				return logFileName;
			}
			string extension = configType + "." + Path.GetExtension(logFileName);
			return Path.ChangeExtension(logFileName, extension);
		}

		// Token: 0x04000BEC RID: 3052
		private readonly IWebGreaseContext context;

		// Token: 0x04000BED RID: 3053
		private readonly ConcurrentDictionary<string, string> renamedFilesLog = new ConcurrentDictionary<string, string>();
	}
}
