using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.Common.Core.People;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicLists;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.DynamicForms
{
	// Token: 0x020000FE RID: 254
	public class DynamicFileStorageManager : IDynamicFileStorageManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000425C4 File Offset: 0x000407C4
		private IDynamicDataManager dynamicDataManager
		{
			get
			{
				IDynamicDataManager result;
				if ((result = this._dynamicDataManager) == null)
				{
					result = (this._dynamicDataManager = new DynamicDataManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x000425F0 File Offset: 0x000407F0
		private IDynamicFieldManager dynamicFieldManager
		{
			get
			{
				IDynamicFieldManager result;
				if ((result = this._dynamicFieldManager) == null)
				{
					result = (this._dynamicFieldManager = new DynamicFieldManager(this.OpContext));
				}
				return result;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x0004261B File Offset: 0x0004081B
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x00042623 File Offset: 0x00040823
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A47 RID: 2631 RVA: 0x0004262C File Offset: 0x0004082C
		public DynamicFileStorageManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new DynamicFileStorageDAO(opContext);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0004264C File Offset: 0x0004084C
		private DynamicListTable LoadEmptyTable(int ControlId)
		{
			DynamicField field = this.dynamicFieldManager.LoadFieldByControlId(ControlId);
			IList<DynamicListColumn> columns = this.LoadColumnsByControlId(field);
			return new DynamicListTable
			{
				Columns = columns,
				Field = field,
				Rows = new List<DynamicListRow>()
			};
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00042694 File Offset: 0x00040894
		private IList<DynamicListRow> GetRowsFromDataItem(DynamicData dataItem, bool LoadFileContents = false)
		{
			string list = dataItem.Value.ToString();
			List<string[]> list2 = DynamicDataManager.DecodeDocumentsList(list);
			List<DynamicListRow> list3 = new List<DynamicListRow>();
			foreach (string[] array in list2)
			{
				bool flag = array != null && array.Length != 0;
				if (flag)
				{
					string fileName = "";
					string date = "";
					byte[] byteArray = null;
					int fileId = 0;
					string text = array[array.Length - 1] ?? "";
					bool flag2 = !string.IsNullOrEmpty(text);
					if (flag2)
					{
						int num = text.LastIndexOf(':');
						bool flag3 = num > 0;
						if (flag3)
						{
							fileName = Path.GetFileName(text.Substring(0, num));
							string s = text.Substring(num + 1);
							int num2;
							bool flag4 = int.TryParse(s, out num2) && num2 > 0;
							if (flag4)
							{
								fileId = num2;
								if (LoadFileContents)
								{
									DynamicFile dynamicFile = this.LoadDynamicFileById(fileId, true);
									bool flag5 = dynamicFile != null;
									if (flag5)
									{
										byteArray = dynamicFile.FileContents.ByteArray;
									}
								}
							}
						}
						else
						{
							fileName = Path.GetFileName(text);
						}
					}
					bool flag6 = array.Length > 1;
					if (flag6)
					{
						date = (array[array.Length - 2] ?? "");
					}
					DynamicListRow item = new DynamicListRow
					{
						File = new BinaryFile
						{
							FileName = fileName,
							ByteArray = byteArray
						},
						CellValues = array.ToList<string>(),
						Date = date,
						FileId = fileId
					};
					list3.Add(item);
				}
			}
			return list3;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00042864 File Offset: 0x00040A64
		public DynamicFile LoadDynamicFileById(int FileId, bool LoadFileContents)
		{
			return this.dao.LoadDynamicFileById(FileId, LoadFileContents);
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x00042884 File Offset: 0x00040A84
		public DynamicListTable LoadFilesByStudent(int PersonId, int ControlId, bool LoadFileContents, eDynamicFormType DataType)
		{
			DynamicListTable dynamicListTable = this.LoadEmptyTable(ControlId);
			DynamicDataContext context = new DynamicDataContext
			{
				PrimaryId = PersonId
			};
			List<DynamicData> list = this.dynamicDataManager.LoadDataByFields(context, new List<int>
			{
				ControlId
			}, DataType);
			bool flag = list == null || list.Count < 1 || list[0].Value == null;
			DynamicListTable result;
			if (flag)
			{
				result = dynamicListTable;
			}
			else
			{
				DynamicData dataItem = list[0];
				dynamicListTable.Rows = this.GetRowsFromDataItem(dataItem, false);
				result = dynamicListTable;
			}
			return result;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0004290C File Offset: 0x00040B0C
		public IDictionary<int, DynamicListTable> LoadPerStudentFilesByStudents(int ControlId, params int[] PersonIds)
		{
			DynamicListTable dynamicListTable = this.LoadEmptyTable(ControlId);
			Dictionary<int, DynamicListTable> dictionary = new Dictionary<int, DynamicListTable>();
			List<DynamicDataSet> list = this.dynamicDataManager.LoadPerStudentDataForMultipleStudents(PersonIds.ToList<int>(), new List<int>
			{
				ControlId
			});
			bool flag = list == null || list.Count < 1;
			IDictionary<int, DynamicListTable> result;
			if (flag)
			{
				result = dictionary;
			}
			else
			{
				foreach (DynamicDataSet dynamicDataSet in list)
				{
					bool flag2 = dynamicDataSet.Data != null && dynamicDataSet.Data.Count > 0;
					if (flag2)
					{
						IList<DynamicListRow> rowsFromDataItem = this.GetRowsFromDataItem(dynamicDataSet.Data[0], false);
						DynamicListTable value = new DynamicListTable
						{
							Columns = dynamicListTable.Columns,
							Field = dynamicListTable.Field,
							Rows = rowsFromDataItem
						};
						dictionary.Add(dynamicDataSet.Context.PrimaryId, value);
					}
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00042A28 File Offset: 0x00040C28
		public IList<DynamicListColumn> LoadColumnsByControlId(DynamicField Field)
		{
			bool flag = Field == null;
			IList<DynamicListColumn> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<DynamicListItem> list = this.dynamicFieldManager.LoadListItems(Field.Setting1);
				List<DynamicListColumn> list2 = new List<DynamicListColumn>();
				foreach (DynamicListItem dynamicListItem in list)
				{
					string text = dynamicListItem.LookupText;
					int num = text.IndexOf('`');
					bool flag2 = num > 0;
					if (flag2)
					{
						text = text.Substring(0, num);
					}
					else
					{
						bool flag3 = num == 0;
						if (flag3)
						{
							text = "";
						}
					}
					list2.Add(new DynamicListColumn
					{
						OriginalName = dynamicListItem.LookupText,
						Name = text
					});
				}
				result = list2;
			}
			return result;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00042B04 File Offset: 0x00040D04
		public IList<SyncDocumentAction> SyncDocuments(string ExternalFolderPath, int DocumentsControlId, eDynamicFormType DataType)
		{
			OperationContext opContext = new OperationContext
			{
				WhoAmI = 0
			};
			IDynamicFileStorageManager dynamicFileStorageManager = new DynamicFileStorageManager(opContext);
			IPeopleManager peopleManager = new PeopleManager(this.OpContext);
			string[] directories = Directory.GetDirectories(ExternalFolderPath);
			List<SyncDocumentAction> list = new List<SyncDocumentAction>();
			foreach (string path in directories)
			{
				string fileName = Path.GetFileName(path);
				string[] files = Directory.GetFiles(path);
				bool flag = files.Length != 0;
				if (flag)
				{
					PersonBase personBase = peopleManager.LoadPersonByStudentNumber(fileName);
					bool flag2 = personBase != null;
					if (flag2)
					{
						int personId = personBase.PersonId;
						DynamicListTable dynamicListTable = dynamicFileStorageManager.LoadFilesByStudent(personId, DocumentsControlId, false, eDynamicFormType.PerStudent);
						List<string> existingFilenames = dynamicListTable.Rows.ToList<DynamicListRow>().ConvertAll<string>((DynamicListRow g) => (g.File.FileName.IndexOf(":") < 0) ? g.File.FileName : g.File.FileName.Substring(0, g.File.FileName.IndexOf(":")));
						IEnumerable<string> enumerable = from g in files
						where existingFilenames.FirstOrDefault((string h) => h.Equals(Path.GetFileName(g), StringComparison.OrdinalIgnoreCase)) == null
						select g;
						foreach (string text in enumerable)
						{
							string fileName2 = Path.GetFileName(text);
							BinaryFile file = new BinaryFile
							{
								FileName = fileName2,
								ByteArray = File.ReadAllBytes(text)
							};
							int clockWorkFileId = this.AddFile(DocumentsControlId, new DynamicDataContext
							{
								PrimaryId = personId
							}, DataType, "Document", "Synced in from " + ExternalFolderPath, file, 1000);
							list.Add(new SyncDocumentAction
							{
								ClockWorkFileId = clockWorkFileId,
								ExternalFileName = text,
								PersonId = personId
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00042CDC File Offset: 0x00040EDC
		public IList<T> LoadPerStudentSingleFileDescriptionsByStudentAndControls<T>(int PersonId, params int[] cids) where T : DynamicFileDescription
		{
			return this.dao.LoadPerStudentSingleFileDescriptionsByStudentAndControls<T>(PersonId, cids);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00042CFC File Offset: 0x00040EFC
		[DebuggerStepThrough]
		public Task<IList<T>> LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync<T>(int PersonId, params int[] cids) where T : DynamicFileDescription
		{
			DynamicFileStorageManager.<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__20<T> <LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__ = new DynamicFileStorageManager.<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__20<T>();
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<T>>.Create();
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.<>4__this = this;
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.PersonId = PersonId;
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.cids = cids;
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.<>1__state = -1;
			<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder.Start<DynamicFileStorageManager.<LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__20<T>>(ref <LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__);
			return <LoadPerStudentSingleFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00042D50 File Offset: 0x00040F50
		public IList<DynamicFileDescription> LoadPerStudentFileListFileDescriptionsByStudentAndControls(int PersonId, params int[] cids)
		{
			return this.dao.LoadPerStudentFileListFileDescriptionsByStudentAndControls(PersonId, cids);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00042D70 File Offset: 0x00040F70
		[DebuggerStepThrough]
		public Task<IList<DynamicFileDescription>> LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync(int PersonId, params int[] cids)
		{
			DynamicFileStorageManager.<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__22 <LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__ = new DynamicFileStorageManager.<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__22();
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicFileDescription>>.Create();
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.<>4__this = this;
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.PersonId = PersonId;
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.cids = cids;
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.<>1__state = -1;
			<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder.Start<DynamicFileStorageManager.<LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__22>(ref <LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__);
			return <LoadPerStudentFileListFileDescriptionsByStudentAndControlsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00042DC4 File Offset: 0x00040FC4
		public IList<DynamicFileDescriptionWithColData> LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControls(int PersonId, params int[] cids)
		{
			return this.dao.LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControls(PersonId, cids);
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00042DE4 File Offset: 0x00040FE4
		[DebuggerStepThrough]
		public Task<IList<DynamicFileDescriptionWithColData>> LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync(int PersonId, params int[] cids)
		{
			DynamicFileStorageManager.<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__24 <LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__ = new DynamicFileStorageManager.<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__24();
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<DynamicFileDescriptionWithColData>>.Create();
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.<>4__this = this;
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.PersonId = PersonId;
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.cids = cids;
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.<>1__state = -1;
			<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.<>t__builder.Start<DynamicFileStorageManager.<LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__24>(ref <LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__);
			return <LoadPerStudentFileListFileDescriptionsWithColDataByStudentAndControlsAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x00042E38 File Offset: 0x00041038
		public BinaryFile LoadFileFromDynamicFileDescription(int studentPersonId, DynamicFileDescription dynamicFileDescription)
		{
			bool flag = dynamicFileDescription == null;
			BinaryFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
				bool flag2 = dynamicFileDescription.FileId > 0;
				if (flag2)
				{
					result = dynamicDataManager.LoadFileFromDocuments(studentPersonId, dynamicFileDescription.FileId);
				}
				else
				{
					bool flag3 = dynamicFileDescription.DataId < 1;
					if (flag3)
					{
						result = null;
					}
					else
					{
						new DynamicDataContext().PrimaryId = studentPersonId;
						result = dynamicDataManager.LoadFileFromImageInfo(dynamicFileDescription.DataId, dynamicFileDescription.ControlId, null);
					}
				}
			}
			return result;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00042EB8 File Offset: 0x000410B8
		[DebuggerStepThrough]
		public Task<BinaryFile> LoadFileFromDynamicFileDescriptionAsync(int studentPersonId, DynamicFileDescription dynamicFileDescription)
		{
			DynamicFileStorageManager.<LoadFileFromDynamicFileDescriptionAsync>d__26 <LoadFileFromDynamicFileDescriptionAsync>d__ = new DynamicFileStorageManager.<LoadFileFromDynamicFileDescriptionAsync>d__26();
			<LoadFileFromDynamicFileDescriptionAsync>d__.<>t__builder = AsyncTaskMethodBuilder<BinaryFile>.Create();
			<LoadFileFromDynamicFileDescriptionAsync>d__.<>4__this = this;
			<LoadFileFromDynamicFileDescriptionAsync>d__.studentPersonId = studentPersonId;
			<LoadFileFromDynamicFileDescriptionAsync>d__.dynamicFileDescription = dynamicFileDescription;
			<LoadFileFromDynamicFileDescriptionAsync>d__.<>1__state = -1;
			<LoadFileFromDynamicFileDescriptionAsync>d__.<>t__builder.Start<DynamicFileStorageManager.<LoadFileFromDynamicFileDescriptionAsync>d__26>(ref <LoadFileFromDynamicFileDescriptionAsync>d__);
			return <LoadFileFromDynamicFileDescriptionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00042F0C File Offset: 0x0004110C
		public int AddFile(int ControlId, DynamicDataContext Context, eDynamicFormType DataType, string Title, string Notes, BinaryFile File, int fileTypeCode = 1000)
		{
			IDynamicDataManager dynamicDataManager = new DynamicDataManager(this.OpContext);
			return dynamicDataManager.StoreFileInDocuments(Title, Notes, File, Context, DataType, ControlId, fileTypeCode);
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00042F3C File Offset: 0x0004113C
		[DebuggerStepThrough]
		public Task<int> AddFileAsync(int ControlId, DynamicDataContext Context, eDynamicFormType DataType, string Title, string Notes, BinaryFile File, int fileTypeCode = 1000)
		{
			DynamicFileStorageManager.<AddFileAsync>d__28 <AddFileAsync>d__ = new DynamicFileStorageManager.<AddFileAsync>d__28();
			<AddFileAsync>d__.<>t__builder = AsyncTaskMethodBuilder<int>.Create();
			<AddFileAsync>d__.<>4__this = this;
			<AddFileAsync>d__.ControlId = ControlId;
			<AddFileAsync>d__.Context = Context;
			<AddFileAsync>d__.DataType = DataType;
			<AddFileAsync>d__.Title = Title;
			<AddFileAsync>d__.Notes = Notes;
			<AddFileAsync>d__.File = File;
			<AddFileAsync>d__.fileTypeCode = fileTypeCode;
			<AddFileAsync>d__.<>1__state = -1;
			<AddFileAsync>d__.<>t__builder.Start<DynamicFileStorageManager.<AddFileAsync>d__28>(ref <AddFileAsync>d__);
			return <AddFileAsync>d__.<>t__builder.Task;
		}

		// Token: 0x040001C0 RID: 448
		private IDynamicFileStorageDAO dao;

		// Token: 0x040001C1 RID: 449
		private IDynamicDataManager _dynamicDataManager;

		// Token: 0x040001C2 RID: 450
		private IDynamicFieldManager _dynamicFieldManager;
	}
}
