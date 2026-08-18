using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;
using Telerik.Web.UI.Gantt;

namespace Telerik.Web.UI
{
	// Token: 0x020004A9 RID: 1193
	public class XmlGanttProvider : GanttProviderBase
	{
		// Token: 0x06002A48 RID: 10824 RVA: 0x00088A3B File Offset: 0x00086C3B
		public XmlGanttProvider()
		{
			this._persistChanges = true;
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x00088A59 File Offset: 0x00086C59
		public XmlGanttProvider(XDocument document) : this()
		{
			this._document = document;
			this.EnsureStructure();
			this.LoadKeys();
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x00088A74 File Offset: 0x00086C74
		public XmlGanttProvider(string dataFileName, bool persistChanges = true) : this()
		{
			this._dataFileName = dataFileName;
			this.LoadDataFile();
			this._persistChanges = persistChanges;
			this.EnsureStructure();
			this.LoadKeys();
		}

		// Token: 0x17000DA3 RID: 3491
		// (get) Token: 0x06002A4B RID: 10827 RVA: 0x00088A9C File Offset: 0x00086C9C
		// (set) Token: 0x06002A4C RID: 10828 RVA: 0x00088AA4 File Offset: 0x00086CA4
		public int RetryAttempts
		{
			get
			{
				return this._retryAttempts;
			}
			set
			{
				this._retryAttempts = value;
			}
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06002A4D RID: 10829 RVA: 0x00088AAD File Offset: 0x00086CAD
		// (set) Token: 0x06002A4E RID: 10830 RVA: 0x00088AB5 File Offset: 0x00086CB5
		public int RetryDelay
		{
			get
			{
				return this._retryDelay;
			}
			set
			{
				this._retryDelay = value;
			}
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x00088AC0 File Offset: 0x00086CC0
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected void LoadDataFile()
		{
			if (this._documentLoaded)
			{
				return;
			}
			if (string.IsNullOrEmpty(this._dataFileName))
			{
				return;
			}
			if (!Path.IsPathRooted(this._dataFileName))
			{
				this._dataFileName = HttpContext.Current.Server.MapPath(this._dataFileName);
			}
			int i = 0;
			while (i < this.RetryAttempts)
			{
				try
				{
					this._document = XDocument.Load(this._dataFileName);
					this._documentLoaded = true;
					break;
				}
				catch (IOException)
				{
					i++;
					Thread.Sleep(this.RetryDelay);
				}
			}
		}

		// Token: 0x06002A50 RID: 10832 RVA: 0x00088B58 File Offset: 0x00086D58
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected void LoadKeys()
		{
			this._nextTaskId = this.ReadNextID("Tasks");
			this._nextDepId = this.ReadNextID("Dependencies");
			this._nextAsmId = this.ReadNextID("Assignments");
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x00088B8D File Offset: 0x00086D8D
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected void SaveDataFile()
		{
			if (this._persistChanges && !string.IsNullOrEmpty(this._dataFileName))
			{
				this._document.Save(this._dataFileName);
			}
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x00088BB8 File Offset: 0x00086DB8
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected internal void EnsureStructure()
		{
			if (this._document.XPathSelectElement("/Project") == null)
			{
				this._document.Add(new XElement("Project"));
			}
			if (this._document.XPathSelectElement("/Project/Tasks") == null)
			{
				this._document.XPathSelectElement("/Project").Add(new XElement("Tasks"));
			}
			if (this._document.XPathSelectElement("/Project/Dependencies") == null)
			{
				this._document.XPathSelectElement("/Project").Add(new XElement("Dependencies"));
			}
			if (this._document.XPathSelectElement("/Project/Assignments") == null)
			{
				this._document.XPathSelectElement("/Project").Add(new XElement("Assignments"));
			}
		}

		// Token: 0x06002A53 RID: 10835 RVA: 0x00088C94 File Offset: 0x00086E94
		public override void Initialize(string name, NameValueCollection config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			if (string.IsNullOrEmpty(name))
			{
				name = "XmlGanttProvider";
			}
			base.Initialize(name, config);
			this._dataFileName = config["fileName"];
			if (string.IsNullOrEmpty(this._dataFileName))
			{
				throw new ProviderException("Missing XML data file name. Please specify it with the fileName property.");
			}
			this.LoadDataFile();
			string value = config["persistChanges"];
			if (!string.IsNullOrEmpty(value))
			{
				if (!bool.TryParse(value, out this._persistChanges))
				{
					throw new ProviderException("Invalid value for PersistChanges attribute. Use 'True' or 'False'.");
				}
			}
			else
			{
				this._persistChanges = true;
			}
			this.EnsureStructure();
			this.LoadKeys();
		}

		// Token: 0x06002A54 RID: 10836 RVA: 0x00088D38 File Offset: 0x00086F38
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected int ReadNextID(string parentNode)
		{
			XElement xelement = this._document.XPathSelectElement(string.Format("//{0}/NextID", parentNode));
			if (xelement == null)
			{
				return 1;
			}
			return int.Parse(xelement.Value);
		}

		// Token: 0x06002A55 RID: 10837 RVA: 0x00088D6C File Offset: 0x00086F6C
		protected internal virtual XElement CreateTaskElement(ITask task)
		{
			IOrderedDictionary data = task.GetData();
			return this.CreateElementFromDictionary(data, "Task");
		}

		// Token: 0x06002A56 RID: 10838 RVA: 0x00088D8C File Offset: 0x00086F8C
		protected internal virtual XElement CreateDependencyElement(IDependency depd)
		{
			IOrderedDictionary data = depd.GetData();
			return this.CreateElementFromDictionary(data, "Dependency");
		}

		// Token: 0x06002A57 RID: 10839 RVA: 0x00088DAC File Offset: 0x00086FAC
		protected internal virtual XElement CreateAssignmentElement(IAssignment asm)
		{
			IOrderedDictionary data = asm.GetData();
			return this.CreateElementFromDictionary(data, "Assignment");
		}

		// Token: 0x06002A58 RID: 10840 RVA: 0x00088DCC File Offset: 0x00086FCC
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected virtual XElement CreateElementFromDictionary(IDictionary data, string name)
		{
			XElement xelement = new XElement(name);
			foreach (object obj in data.Keys)
			{
				string text = (string)obj;
				xelement.Add(new XElement(text, data[text]));
			}
			return xelement;
		}

		// Token: 0x06002A59 RID: 10841 RVA: 0x00088E44 File Offset: 0x00087044
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected internal virtual void IncrementNextId(string root, int value)
		{
			string text = string.Format("//{0}", root);
			if (this._document.XPathSelectElement(text + "/NextID") == null)
			{
				this._document.XPathSelectElement(text).Add(new XElement("NextID", value));
				return;
			}
			this._document.XPathSelectElement(text + "/NextID").Value = value.ToString();
		}

		// Token: 0x06002A5A RID: 10842 RVA: 0x00088EE4 File Offset: 0x000870E4
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override List<ITask> GetTasks()
		{
			List<ITask> list = new List<ITask>();
			IEnumerable<XElement> enumerable = this._document.XPathSelectElements("//Tasks/Task");
			foreach (XElement xelement in enumerable)
			{
				Task task = this.TaskFactory.CreateTask();
				Dictionary<string, string> values = xelement.Elements().ToDictionary((XElement k) => k.Name.LocalName, delegate(XElement v)
				{
					if (!string.IsNullOrEmpty(v.Value))
					{
						return v.Value;
					}
					return null;
				});
				task.LoadFromDictionary(values);
				list.Add(task);
			}
			return list;
		}

		// Token: 0x06002A5B RID: 10843 RVA: 0x00088FA4 File Offset: 0x000871A4
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override ITask UpdateTask(ITask task)
		{
			if (task.ID == null)
			{
				this.InsertTask(task);
			}
			XElement xelement = this._document.XPathSelectElement(string.Format("//Tasks/Task[ID={0}]", task.ID));
			xelement.ReplaceWith(this.CreateTaskElement(task));
			this.SaveDataFile();
			return task;
		}

		// Token: 0x06002A5C RID: 10844 RVA: 0x00088FF4 File Offset: 0x000871F4
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override ITask DeleteTask(ITask task)
		{
			XElement xelement = this._document.XPathSelectElement(string.Format("//Tasks/Task[ID={0}]", task.ID));
			xelement.Remove();
			this.SaveDataFile();
			return task;
		}

		// Token: 0x06002A5D RID: 10845 RVA: 0x0008902C File Offset: 0x0008722C
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override ITask InsertTask(ITask task)
		{
			task.ID = this._nextTaskId.ToString();
			XElement content = this.CreateTaskElement(task);
			this._document.XPathSelectElement("//Tasks").Add(content);
			this._nextTaskId++;
			this.IncrementNextId("Tasks", this._nextTaskId);
			this.SaveDataFile();
			return task;
		}

		// Token: 0x06002A5E RID: 10846 RVA: 0x000890A4 File Offset: 0x000872A4
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override List<IDependency> GetDependencies()
		{
			List<IDependency> list = new List<IDependency>();
			IEnumerable<XElement> enumerable = this._document.XPathSelectElements("//Dependencies/Dependency");
			foreach (XElement xelement in enumerable)
			{
				IDependency dependency = this.DependencyFactory.CreateDependency();
				Dictionary<string, string> values = xelement.Elements().ToDictionary((XElement k) => k.Name.LocalName, (XElement v) => v.Value);
				dependency.LoadFromDictionary(values);
				list.Add(dependency);
			}
			return list;
		}

		// Token: 0x06002A5F RID: 10847 RVA: 0x00089164 File Offset: 0x00087364
		public override IDependency UpdateDependency(IDependency dependency)
		{
			if (dependency.ID == null)
			{
				this.InsertDependency(dependency);
			}
			XElement xelement = this._document.XPathSelectElement(string.Format("//Dependencies/Dependency[ID={0}]", dependency.ID));
			xelement.ReplaceWith(this.CreateDependencyElement(dependency));
			this.SaveDataFile();
			return dependency;
		}

		// Token: 0x06002A60 RID: 10848 RVA: 0x000891B4 File Offset: 0x000873B4
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override IDependency DeleteDependency(IDependency dependency)
		{
			XElement xelement = this._document.XPathSelectElement(string.Format("//Dependencies/Dependency[ID={0}]", dependency.ID));
			xelement.Remove();
			this.SaveDataFile();
			return dependency;
		}

		// Token: 0x06002A61 RID: 10849 RVA: 0x000891EC File Offset: 0x000873EC
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override IDependency InsertDependency(IDependency dependency)
		{
			dependency.ID = this._nextDepId.ToString();
			XElement content = this.CreateDependencyElement(dependency);
			this._document.XPathSelectElement("//Dependencies").Add(content);
			this._nextDepId++;
			this.IncrementNextId("Dependencies", this._nextDepId);
			this.SaveDataFile();
			return dependency;
		}

		// Token: 0x06002A62 RID: 10850 RVA: 0x00089274 File Offset: 0x00087474
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override List<IResource> GetResources()
		{
			List<IResource> list = new List<IResource>();
			IEnumerable<XElement> enumerable = this._document.XPathSelectElements("//Resources/Resource");
			foreach (XElement xelement in enumerable)
			{
				IResource resource = this.ResourceFactory.CreatResource();
				Dictionary<string, string> values = xelement.Elements().ToDictionary((XElement k) => k.Name.LocalName, delegate(XElement v)
				{
					if (!string.IsNullOrEmpty(v.Value))
					{
						return v.Value;
					}
					return null;
				});
				resource.LoadFromDictionary(values);
				list.Add(resource);
			}
			return list;
		}

		// Token: 0x06002A63 RID: 10851 RVA: 0x00089358 File Offset: 0x00087558
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override List<IAssignment> GetAssignments()
		{
			List<IAssignment> list = new List<IAssignment>();
			IEnumerable<XElement> enumerable = this._document.XPathSelectElements("//Assignments/Assignment");
			foreach (XElement xelement in enumerable)
			{
				IAssignment assignment = this.AssignmentFactory.CreateAssignment();
				Dictionary<string, string> values = xelement.Elements().ToDictionary((XElement k) => k.Name.LocalName, delegate(XElement v)
				{
					if (!string.IsNullOrEmpty(v.Value))
					{
						return v.Value;
					}
					return null;
				});
				assignment.LoadFromDictionary(values);
				list.Add(assignment);
			}
			return list;
		}

		// Token: 0x06002A64 RID: 10852 RVA: 0x00089418 File Offset: 0x00087618
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override IAssignment UpdateAssignment(IAssignment assignment)
		{
			if (assignment.ID == null)
			{
				this.InsertAssignment(assignment);
			}
			XElement xelement = this._document.XPathSelectElement(string.Format("//Assignments/Assignment[ID={0}]", assignment.ID));
			xelement.ReplaceWith(this.CreateAssignmentElement(assignment));
			this.SaveDataFile();
			return assignment;
		}

		// Token: 0x06002A65 RID: 10853 RVA: 0x00089468 File Offset: 0x00087668
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override IAssignment DeleteAssignment(IAssignment assignment)
		{
			XElement xelement = this._document.XPathSelectElement(string.Format("//Assignments/Assignment[ID={0}]", assignment.ID));
			xelement.Remove();
			this.SaveDataFile();
			return assignment;
		}

		// Token: 0x06002A66 RID: 10854 RVA: 0x000894A0 File Offset: 0x000876A0
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override IAssignment InsertAssignment(IAssignment assignment)
		{
			assignment.ID = this._nextAsmId.ToString();
			XElement content = this.CreateAssignmentElement(assignment);
			this._document.XPathSelectElement("//Assignments").Add(content);
			this._nextAsmId++;
			this.IncrementNextId("Assignments", this._nextAsmId);
			this.SaveDataFile();
			return assignment;
		}

		// Token: 0x04000AF6 RID: 2806
		private string _dataFileName;

		// Token: 0x04000AF7 RID: 2807
		private XDocument _document;

		// Token: 0x04000AF8 RID: 2808
		private bool _documentLoaded;

		// Token: 0x04000AF9 RID: 2809
		private int _nextTaskId;

		// Token: 0x04000AFA RID: 2810
		private int _nextDepId;

		// Token: 0x04000AFB RID: 2811
		private int _nextAsmId;

		// Token: 0x04000AFC RID: 2812
		private bool _persistChanges;

		// Token: 0x04000AFD RID: 2813
		private int _retryAttempts = 5;

		// Token: 0x04000AFE RID: 2814
		private int _retryDelay = 100;
	}
}
