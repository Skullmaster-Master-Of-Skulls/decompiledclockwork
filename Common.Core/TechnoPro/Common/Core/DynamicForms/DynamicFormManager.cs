using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TechnoPro.Common.DAO.DynamicForms;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DataStructure.Tree;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.Core.DynamicForms
{
	// Token: 0x02000100 RID: 256
	public class DynamicFormManager : IDynamicFormManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00043129 File Offset: 0x00041329
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00043131 File Offset: 0x00041331
		public IDynamicFormsDAO dao { get; set; }

		// Token: 0x06000A64 RID: 2660 RVA: 0x0004313A File Offset: 0x0004133A
		public DynamicFormManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new DynamicFormsDAO(opContext);
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x0004315C File Offset: 0x0004135C
		private string FixFilename(string fn)
		{
			char[] invalidFileNameChars = Path.GetInvalidFileNameChars();
			string text = fn.Replace(" ", "_");
			foreach (char oldChar in invalidFileNameChars)
			{
				text = text.Replace(oldChar, '_');
			}
			return text;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x000431AB File Offset: 0x000413AB
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x000431B3 File Offset: 0x000413B3
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A68 RID: 2664 RVA: 0x000431BC File Offset: 0x000413BC
		public DynamicForm LoadDynamicFormById(int ScreenNum)
		{
			IList<DynamicForm> list = this.dao.LoadDynamicFormsByIds(new int[]
			{
				ScreenNum
			});
			bool flag = list == null || list.Count < 1;
			DynamicForm result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = list[0];
			}
			return result;
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00043204 File Offset: 0x00041404
		[DebuggerStepThrough]
		public Task<DynamicForm> LoadDynamicFormByIdAsync(int ScreenNum)
		{
			DynamicFormManager.<LoadDynamicFormByIdAsync>d__11 <LoadDynamicFormByIdAsync>d__ = new DynamicFormManager.<LoadDynamicFormByIdAsync>d__11();
			<LoadDynamicFormByIdAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DynamicForm>.Create();
			<LoadDynamicFormByIdAsync>d__.<>4__this = this;
			<LoadDynamicFormByIdAsync>d__.ScreenNum = ScreenNum;
			<LoadDynamicFormByIdAsync>d__.<>1__state = -1;
			<LoadDynamicFormByIdAsync>d__.<>t__builder.Start<DynamicFormManager.<LoadDynamicFormByIdAsync>d__11>(ref <LoadDynamicFormByIdAsync>d__);
			return <LoadDynamicFormByIdAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00043250 File Offset: 0x00041450
		public IList<DynamicForm> LoadActiveFormsByFormType(eDynamicFormType[] FormTypes)
		{
			bool flag = FormTypes == null;
			IList<DynamicForm> result;
			if (flag)
			{
				result = new List<DynamicForm>();
			}
			else
			{
				List<DynamicForm> list = new List<DynamicForm>();
				foreach (eDynamicFormType formType in FormTypes)
				{
					list.AddRange(this.dao.LoadActiveFormsByFormType(formType) ?? new List<DynamicForm>());
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000432B4 File Offset: 0x000414B4
		public IList<DynamicForm> LoadDynamicFormsByIds(params int[] ScreenNums)
		{
			return this.dao.LoadDynamicFormsByIds(ScreenNums);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x000432D4 File Offset: 0x000414D4
		public IList<DynamicFormWithExtendedInfo> LoadActiveFormsWithExtendedInfo()
		{
			return this.dao.LoadActiveFormsWithExtendedInfo();
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x000432F4 File Offset: 0x000414F4
		public IList<DynamicForm> GetScreensAStudentHasDataOn(int PersonId)
		{
			return this.dao.GetScreensAStudentHasDataOn(PersonId);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00043314 File Offset: 0x00041514
		public IList<DynamicForm> FindFormByTitleSubstringMatch(string SubstringToMatch, bool SearchPrimaryTitle, bool SearchSecondaryTitle)
		{
			bool flag = string.IsNullOrEmpty(SubstringToMatch);
			IList<DynamicForm> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = this.dao.FindFormByTitleSubstringMatch(SubstringToMatch, SearchPrimaryTitle, SearchSecondaryTitle);
			}
			return result;
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00043344 File Offset: 0x00041544
		public Forest<DynamicFormOrGroupOrFormType> LoadAllForms()
		{
			IList<DynamicForm> list = this.dao.LoadAllForms();
			Forest<DynamicFormOrGroupOrFormType> forest = new Forest<DynamicFormOrGroupOrFormType>();
			TreeNodeCollection<DynamicFormOrGroupOrFormType> nodes = forest.Nodes;
			using (IEnumerator<DynamicForm> enumerator = list.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					DynamicForm form = enumerator.Current;
					string groupName = string.IsNullOrEmpty(form.GroupName) ? "Un-grouped" : form.GroupName;
					string name = Enum.GetName(typeof(eDynamicFormType), form.FormType);
					TreeNode<DynamicFormOrGroupOrFormType> treeNode = forest.Nodes.FirstOrDefault((TreeNode<DynamicFormOrGroupOrFormType> f) => !string.IsNullOrEmpty(f.Value.GroupName) && f.Value.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase));
					bool flag = treeNode == null;
					if (flag)
					{
						treeNode = forest.AppendNode(null, new DynamicFormOrGroupOrFormType
						{
							GroupName = groupName
						});
					}
					TreeNode<DynamicFormOrGroupOrFormType> treeNode2 = treeNode.Nodes.FirstOrDefault((TreeNode<DynamicFormOrGroupOrFormType> f) => f.Value.DynamicFormType != null && f.Value.DynamicFormType.Value == form.FormType);
					bool flag2 = treeNode2 == null;
					if (flag2)
					{
						treeNode2 = treeNode.AppendNode(new DynamicFormOrGroupOrFormType
						{
							DynamicFormType = new eDynamicFormType?(form.FormType)
						}, treeNode);
					}
					treeNode2.AppendNode(new DynamicFormOrGroupOrFormType
					{
						DynamicForm = form
					}, treeNode2);
				}
			}
			return forest;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000434D0 File Offset: 0x000416D0
		public string ExportFormsWithFieldsToXmlNew(bool IncludeXmlDeclaration = false, params int[] ScreenNums)
		{
			IList<DynamicFormWithExtendedInfo> list = this.LoadFormsWithExtendedInfoByScreenNums(ScreenNums);
			List<DynamicFieldOnForm> list2 = new List<DynamicFieldOnForm>();
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
			foreach (DynamicFormWithExtendedInfo dynamicFormWithExtendedInfo in list)
			{
				List<DynamicField> list3 = dynamicFieldManager.LoadFields(dynamicFormWithExtendedInfo);
				bool flag = list3 != null;
				if (flag)
				{
					foreach (DynamicField field in list3)
					{
						list2.Add(new DynamicFieldOnForm(field, dynamicFormWithExtendedInfo.ScreenNum));
					}
				}
			}
			string result;
			if (IncludeXmlDeclaration)
			{
				result = DynamicFormsAdapter.ConvertToXmlNew(list, list2);
			}
			else
			{
				XElement xelement = DynamicFormsAdapter.ConvertToXmlElementNew(list, list2);
				result = xelement.ToString();
			}
			return result;
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000435C4 File Offset: 0x000417C4
		public int DoesFormExist(string UniqueId)
		{
			bool flag = string.IsNullOrEmpty(UniqueId);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = this.dao.DoesFormExist(UniqueId);
			}
			return result;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x000435F0 File Offset: 0x000417F0
		public IList<BinaryFile> ExportFormsToXml(params int[] ScreenNums)
		{
			IList<DynamicForm> list = this.LoadDynamicFormsByIds(ScreenNums);
			List<BinaryFile> list2 = new List<BinaryFile>();
			foreach (DynamicForm dynamicForm in list)
			{
				string s = this.dao.ConvertDynamicFormDefinitionToXml(dynamicForm);
				BinaryFile item = new BinaryFile
				{
					FileName = string.Format("CWFORM_{0}.{1}.xml", this.FixFilename(dynamicForm.Title), DateTime.Now.ToString("yyyy-MM-dd")),
					ByteArray = Encoding.ASCII.GetBytes(s)
				};
				list2.Add(item);
			}
			return list2;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x000436AC File Offset: 0x000418AC
		public IList<DynamicFormWithFields> ImportFromsFromXmlNew(string xml)
		{
			bool flag = string.IsNullOrEmpty(xml);
			IList<DynamicFormWithFields> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = DynamicFormsAdapter.ConvertFromXmlNew(xml);
			}
			return result;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000436D2 File Offset: 0x000418D2
		public void ImportFormFromXml(string xml, int ScreenNumToImportControlsInto)
		{
			this.dao.ImportFormFromXml(xml, ScreenNumToImportControlsInto);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000436E4 File Offset: 0x000418E4
		public IList<DynamicFormWithExtendedInfo> LoadFormsWithExtendedInfoByScreenNums(params int[] ScreenNums)
		{
			return this.dao.LoadFormsWithExtendedInfoByScreenNums(ScreenNums);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00043704 File Offset: 0x00041904
		public int CreateForm(DynamicFormWithExtendedInfo Form)
		{
			return this.dao.CreateForm(Form);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00043722 File Offset: 0x00041922
		public void UpdateForm(DynamicFormWithExtendedInfo Form)
		{
			this.dao.UpdateForm(Form);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00043734 File Offset: 0x00041934
		public bool DeleteForm(int ScreenNum)
		{
			return this.dao.DeleteForm(ScreenNum);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00043754 File Offset: 0x00041954
		public IDictionary<int, string> LoadScreenUniqueIdsByScreenNums(params int[] ScreenNums)
		{
			return this.dao.LoadScreenUniqueIdsByScreenNums(ScreenNums);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00043774 File Offset: 0x00041974
		public IList<DynamicFormWithFields> ImportFormsFromXmlNew(string xml, bool writeToDatabase = false)
		{
			IList<DynamicFormWithFields> list = DynamicFormsAdapter.ConvertFromXmlNew(xml);
			bool flag = !writeToDatabase;
			IList<DynamicFormWithFields> result;
			if (flag)
			{
				result = list;
			}
			else
			{
				IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(this.OpContext);
				foreach (DynamicFormWithFields dynamicFormWithFields in list)
				{
					string text = (dynamicFormWithFields.Form.UniqueId ?? "").Trim();
					bool flag2 = text.Length > 0;
					if (flag2)
					{
						int num = this.DoesFormExist(text);
						bool flag3 = num < 1;
						if (flag3)
						{
							int num2 = this.CreateForm(dynamicFormWithFields.Form);
							bool flag4 = num2 > 0;
							if (flag4)
							{
								dynamicFormWithFields.Form.ScreenNum = num2;
								dynamicFieldManager.CreateFields(num2, dynamicFormWithFields.Fields.ToList<DynamicFieldOnForm>().ConvertAll<DynamicField>((DynamicFieldOnForm g) => new DynamicField(g)));
							}
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00043898 File Offset: 0x00041A98
		public IList<int> FindScreensAControlExistsOn(int ControlId)
		{
			return this.dao.FindScreensAControlExistsOn(ControlId);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000438B8 File Offset: 0x00041AB8
		[DebuggerStepThrough]
		public Task<IList<int>> FindScreensAControlExistsOnAsync(int ControlId)
		{
			DynamicFormManager.<FindScreensAControlExistsOnAsync>d__30 <FindScreensAControlExistsOnAsync>d__ = new DynamicFormManager.<FindScreensAControlExistsOnAsync>d__30();
			<FindScreensAControlExistsOnAsync>d__.<>t__builder = AsyncTaskMethodBuilder<IList<int>>.Create();
			<FindScreensAControlExistsOnAsync>d__.<>4__this = this;
			<FindScreensAControlExistsOnAsync>d__.ControlId = ControlId;
			<FindScreensAControlExistsOnAsync>d__.<>1__state = -1;
			<FindScreensAControlExistsOnAsync>d__.<>t__builder.Start<DynamicFormManager.<FindScreensAControlExistsOnAsync>d__30>(ref <FindScreensAControlExistsOnAsync>d__);
			return <FindScreensAControlExistsOnAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00043904 File Offset: 0x00041B04
		public IList<int> LoadControlIdsForScreenInOrder(int ScreenNum, bool RemoveNonDataHoldingControls)
		{
			return this.dao.LoadControlIdsForScreenInOrder(ScreenNum, RemoveNonDataHoldingControls);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00043924 File Offset: 0x00041B24
		public IDictionary<int, IList<int>> FindScreensControlIdsExistOn(IList<int> ControlIds, out IList<DynamicForm> Screens)
		{
			return this.dao.FindScreensControlIdsExistOn(ControlIds, out Screens);
		}
	}
}
