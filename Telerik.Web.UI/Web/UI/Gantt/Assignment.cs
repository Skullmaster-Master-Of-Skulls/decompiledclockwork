using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002E6 RID: 742
	[Serializable]
	public class Assignment : StateManager, IAssignment, IAssignmentBase, IMarkableStateManager, IStateManager
	{
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x000547EB File Offset: 0x000529EB
		// (set) Token: 0x060019B0 RID: 6576 RVA: 0x000547FD File Offset: 0x000529FD
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

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00054810 File Offset: 0x00052A10
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x00054822 File Offset: 0x00052A22
		public object TaskID
		{
			get
			{
				return base.ViewState["TaskID"];
			}
			set
			{
				base.ViewState["TaskID"] = value;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00054835 File Offset: 0x00052A35
		// (set) Token: 0x060019B4 RID: 6580 RVA: 0x00054847 File Offset: 0x00052A47
		public object ResourceID
		{
			get
			{
				return base.ViewState["ResourceID"];
			}
			set
			{
				base.ViewState["ResourceID"] = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x0005485A File Offset: 0x00052A5A
		// (set) Token: 0x060019B6 RID: 6582 RVA: 0x0005486C File Offset: 0x00052A6C
		public object Units
		{
			get
			{
				return base.ViewState["Units"];
			}
			set
			{
				base.ViewState["Units"] = value;
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00054880 File Offset: 0x00052A80
		protected internal virtual IDictionary<string, object> GetSerializationData()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ID"] = this.ID;
			dictionary["TaskID"] = this.TaskID;
			dictionary["ResourceID"] = this.ResourceID;
			dictionary["Units"] = this.Units;
			return dictionary;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x000548D8 File Offset: 0x00052AD8
		public IOrderedDictionary GetData()
		{
			return new OrderedDictionary
			{
				{
					"ID",
					this.ID
				},
				{
					"TaskID",
					this.TaskID
				},
				{
					"ResourceID",
					this.ResourceID
				},
				{
					"Units",
					this.Units
				}
			};
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x00054960 File Offset: 0x00052B60
		public void LoadFromDictionary(IDictionary values)
		{
			Dictionary<string, Action<object>> dictionary = new Dictionary<string, Action<object>>();
			dictionary.Add("ID", delegate(object obj)
			{
				this.ID = obj;
			});
			dictionary.Add("TaskID", delegate(object obj)
			{
				this.TaskID = obj;
			});
			dictionary.Add("ResourceID", delegate(object obj)
			{
				this.ResourceID = obj;
			});
			dictionary.Add("Units", delegate(object obj)
			{
				this.Units = Convert.ToDouble(obj);
			});
			foreach (string text in Assignment.AssignmentDataKeys.Keys)
			{
				if (values.Contains(text))
				{
					object obj2 = values[text];
					if (text != "Units" && obj2 is string)
					{
						try
						{
							Guid guid = new Guid(obj2.ToString());
							obj2 = guid;
						}
						catch
						{
						}
					}
					dictionary[text](obj2);
				}
			}
		}

		// Token: 0x020002E7 RID: 743
		internal static class AssignmentDataKeys
		{
			// Token: 0x040006A3 RID: 1699
			public const string ID = "ID";

			// Token: 0x040006A4 RID: 1700
			public const string TaskID = "TaskID";

			// Token: 0x040006A5 RID: 1701
			public const string ResourceID = "ResourceID";

			// Token: 0x040006A6 RID: 1702
			public const string Units = "Units";

			// Token: 0x040006A7 RID: 1703
			public static IList<string> Keys = new List<string>
			{
				"ID",
				"TaskID",
				"ResourceID",
				"Units"
			};
		}
	}
}
