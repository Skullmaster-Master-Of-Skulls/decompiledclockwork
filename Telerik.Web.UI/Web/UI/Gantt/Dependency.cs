using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200049F RID: 1183
	public class Dependency : StateManager, IDependency, IDependencyBase, IMarkableStateManager, IStateManager
	{
		// Token: 0x17000D90 RID: 3472
		// (get) Token: 0x060029F1 RID: 10737 RVA: 0x000872F5 File Offset: 0x000854F5
		// (set) Token: 0x060029F2 RID: 10738 RVA: 0x00087307 File Offset: 0x00085507
		public object ID
		{
			get
			{
				return base.ViewState["ID"];
			}
			set
			{
				base.ViewState["ID"] = value;
			}
		}

		// Token: 0x17000D91 RID: 3473
		// (get) Token: 0x060029F3 RID: 10739 RVA: 0x0008731A File Offset: 0x0008551A
		// (set) Token: 0x060029F4 RID: 10740 RVA: 0x0008732C File Offset: 0x0008552C
		public object SuccessorID
		{
			get
			{
				return base.ViewState["SuccessorID"];
			}
			set
			{
				base.ViewState["SuccessorID"] = value;
			}
		}

		// Token: 0x17000D92 RID: 3474
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x0008733F File Offset: 0x0008553F
		// (set) Token: 0x060029F6 RID: 10742 RVA: 0x00087351 File Offset: 0x00085551
		public object PredecessorID
		{
			get
			{
				return base.ViewState["PredecessorID"];
			}
			set
			{
				base.ViewState["PredecessorID"] = value;
			}
		}

		// Token: 0x17000D93 RID: 3475
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x00087364 File Offset: 0x00085564
		// (set) Token: 0x060029F8 RID: 10744 RVA: 0x0008736C File Offset: 0x0008556C
		public DependencyType Type { get; set; }

		// Token: 0x060029F9 RID: 10745 RVA: 0x00087378 File Offset: 0x00085578
		protected internal virtual IDictionary<string, object> GetSerializationData()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ID"] = this.ID;
			dictionary["PredecessorID"] = this.PredecessorID;
			dictionary["SuccessorID"] = this.SuccessorID;
			dictionary["Type"] = this.Type;
			return dictionary;
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000873D8 File Offset: 0x000855D8
		public IOrderedDictionary GetData()
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			orderedDictionary["ID"] = this.ID;
			orderedDictionary["PredecessorID"] = this.PredecessorID;
			orderedDictionary["SuccessorID"] = this.SuccessorID;
			orderedDictionary["Type"] = this.Type;
			return orderedDictionary;
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x00087438 File Offset: 0x00085638
		public void LoadFromDictionary(IDictionary values)
		{
			foreach (string text in Dependency.DependencyDataKeys.Keys)
			{
				if (values.Contains(text))
				{
					object obj = values[text];
					if (obj is string)
					{
						try
						{
							Guid guid = new Guid(obj.ToString());
							obj = guid;
						}
						catch
						{
						}
					}
					if (text == "ID")
					{
						this.ID = obj;
					}
					else if (text == "PredecessorID")
					{
						this.PredecessorID = obj;
					}
					else if (text == "SuccessorID")
					{
						this.SuccessorID = obj;
					}
					else if (text == "Type")
					{
						if (obj is string)
						{
							obj = (DependencyType)Enum.Parse(typeof(DependencyType), (string)obj);
						}
						this.Type = (DependencyType)obj;
					}
				}
			}
		}

		// Token: 0x020004A0 RID: 1184
		internal static class DependencyDataKeys
		{
			// Token: 0x04000ABF RID: 2751
			public const string ID = "ID";

			// Token: 0x04000AC0 RID: 2752
			public const string SuccessorID = "SuccessorID";

			// Token: 0x04000AC1 RID: 2753
			public const string PredecessorID = "PredecessorID";

			// Token: 0x04000AC2 RID: 2754
			public const string Type = "Type";

			// Token: 0x04000AC3 RID: 2755
			public static IList<string> Keys = new List<string>
			{
				"ID",
				"SuccessorID",
				"PredecessorID",
				"Type"
			};
		}
	}
}
