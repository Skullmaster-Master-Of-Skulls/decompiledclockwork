using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000323 RID: 803
	internal class XmlImporter
	{
		// Token: 0x06001AD2 RID: 6866 RVA: 0x00056C6C File Offset: 0x00054E6C
		public static List<Task> ParseTasks(XDocument xDocument)
		{
			XmlImporter.xnamespace = xDocument.Root.Name.Namespace;
			List<Task> list = new List<Task>();
			IEnumerable<XElement> taskElements = XmlImporter.GetTaskElements(xDocument);
			Dictionary<string, string> outlineToID = new Dictionary<string, string>();
			foreach (XElement taskElement in taskElements)
			{
				Task task = XmlImporter.CreateTask(taskElement);
				XmlImporter.SetupHierarchy(taskElement, task, outlineToID);
				list.Add(task);
			}
			return list;
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00056CF4 File Offset: 0x00054EF4
		public static List<Dependency> ParseDependencies(XDocument xDocument)
		{
			XmlImporter.xnamespace = xDocument.Root.Name.Namespace;
			List<Dependency> list = new List<Dependency>();
			IEnumerable<XElement> taskElements = XmlImporter.GetTaskElements(xDocument);
			foreach (XElement xelement in taskElements)
			{
				IEnumerable<XElement> enumerable = xelement.Elements(XmlImporter.xnamespace + "PredecessorLink");
				foreach (XElement dependencyElement in enumerable)
				{
					list.Add(XmlImporter.CreateDependency(xelement, dependencyElement));
				}
			}
			return list;
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00056DB8 File Offset: 0x00054FB8
		private static void SetupHierarchy(XElement taskElement, Task task, Dictionary<string, string> outlineToID)
		{
			string attributeValue = XmlImporter.GetAttributeValue(taskElement, "OutlineNumber");
			outlineToID.Add(attributeValue, task.ID.ToString());
			if (attributeValue.Split(new char[]
			{
				'.'
			}).Length > 1)
			{
				int num = attributeValue.LastIndexOf('.');
				string key = attributeValue.Substring(0, num);
				task.ParentID = outlineToID[key];
				string s = attributeValue.Substring(num + 1);
				task.OrderID = int.Parse(s) - 1;
				return;
			}
			task.OrderID = int.Parse(attributeValue) - 1;
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x00056E50 File Offset: 0x00055050
		private static Task CreateTask(XElement taskElement)
		{
			Task task = new Task
			{
				ID = XmlImporter.GetAttributeValue(taskElement, "UID"),
				Start = DateTime.Parse(XmlImporter.GetAttributeValue(taskElement, "Start")),
				End = DateTime.Parse(XmlImporter.GetAttributeValue(taskElement, "Finish")),
				PercentComplete = decimal.Parse(XmlImporter.GetAttributeValue(taskElement, "PercentComplete")) / 100m,
				Summary = (XmlImporter.GetAttributeValue(taskElement, "Summary") == "1")
			};
			if (taskElement.Element(XmlImporter.xnamespace + "Name") != null)
			{
				task.Title = XmlImporter.GetAttributeValue(taskElement, "Name");
			}
			return task;
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00056F08 File Offset: 0x00055108
		private static Dependency CreateDependency(XElement taskElement, XElement dependencyElement)
		{
			return new Dependency
			{
				PredecessorID = XmlImporter.GetAttributeValue(dependencyElement, "PredecessorUID"),
				SuccessorID = XmlImporter.GetAttributeValue(taskElement, "UID"),
				Type = (DependencyType)Enum.Parse(typeof(DependencyType), XmlImporter.GetAttributeValue(dependencyElement, "Type"))
			};
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x00056F64 File Offset: 0x00055164
		private static IEnumerable<XElement> GetTaskElements(XDocument xDocument)
		{
			IEnumerable<XElement> source = xDocument.Root.Elements(XmlImporter.xnamespace + "Tasks");
			IEnumerable<XElement> enumerable = source.Elements(XmlImporter.xnamespace + "Task");
			enumerable.FirstOrDefault<XElement>().Remove();
			return enumerable;
		}

		// Token: 0x06001AD8 RID: 6872 RVA: 0x00056FAE File Offset: 0x000551AE
		private static string GetAttributeValue(XElement element, string attributeName)
		{
			return element.Element(XmlImporter.xnamespace + attributeName).Value;
		}

		// Token: 0x040006CE RID: 1742
		private static XNamespace xnamespace;
	}
}
