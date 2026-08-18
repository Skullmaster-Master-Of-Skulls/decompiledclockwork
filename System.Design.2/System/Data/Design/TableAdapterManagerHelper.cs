using System;
using System.Collections.Generic;

namespace System.Data.Design
{
	// Token: 0x02000268 RID: 616
	internal class TableAdapterManagerHelper
	{
		// Token: 0x060017AA RID: 6058 RVA: 0x00081E6C File Offset: 0x0008006C
		internal static DataRelation[] GetSelfRefRelations(DataTable dataTable)
		{
			List<DataRelation> list = new List<DataRelation>();
			List<DataRelation> list2 = new List<DataRelation>();
			foreach (object obj in dataTable.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (dataRelation.ChildTable == dataRelation.ParentTable)
				{
					list.Add(dataRelation);
					if (dataRelation.ChildKeyConstraint != null)
					{
						list2.Add(dataRelation);
					}
				}
			}
			if (list2.Count > 0)
			{
				return list2.ToArray();
			}
			return list.ToArray();
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00081F08 File Offset: 0x00080108
		internal static DataTable[] GetUpdateOrder(DataSet ds)
		{
			TableAdapterManagerHelper.HierarchicalObject[] array = new TableAdapterManagerHelper.HierarchicalObject[ds.Tables.Count];
			for (int i = 0; i < ds.Tables.Count; i++)
			{
				DataTable theObject = ds.Tables[i];
				TableAdapterManagerHelper.HierarchicalObject hierarchicalObject = new TableAdapterManagerHelper.HierarchicalObject(theObject);
				array[i] = hierarchicalObject;
			}
			for (int j = 0; j < array.Length; j++)
			{
				DataTable dataTable = array[j].TheObject as DataTable;
				foreach (object obj in dataTable.Constraints)
				{
					Constraint constraint = (Constraint)obj;
					ForeignKeyConstraint foreignKeyConstraint = constraint as ForeignKeyConstraint;
					if (foreignKeyConstraint != null && foreignKeyConstraint.RelatedTable != dataTable)
					{
						int num = ds.Tables.IndexOf(foreignKeyConstraint.RelatedTable);
						array[j].AddUniqueParent(array[num]);
					}
				}
				foreach (object obj2 in dataTable.ParentRelations)
				{
					DataRelation dataRelation = (DataRelation)obj2;
					if (dataRelation.ParentTable != dataTable)
					{
						int num2 = ds.Tables.IndexOf(dataRelation.ParentTable);
						array[j].AddUniqueParent(array[num2]);
					}
				}
			}
			foreach (TableAdapterManagerHelper.HierarchicalObject hierarchicalObject2 in array)
			{
				if (hierarchicalObject2.HasParent)
				{
					hierarchicalObject2.CheckParents();
				}
			}
			DataTable[] array2 = new DataTable[array.Length];
			Array.Sort<TableAdapterManagerHelper.HierarchicalObject>(array);
			for (int l = 0; l < array.Length; l++)
			{
				TableAdapterManagerHelper.HierarchicalObject hierarchicalObject3 = array[l];
				array2[l] = (DataTable)hierarchicalObject3.TheObject;
			}
			return array2;
		}

		// Token: 0x020004C1 RID: 1217
		internal class HierarchicalObject : IComparable<TableAdapterManagerHelper.HierarchicalObject>
		{
			// Token: 0x17000959 RID: 2393
			// (get) Token: 0x06002C42 RID: 11330 RVA: 0x00107309 File Offset: 0x00105509
			internal List<TableAdapterManagerHelper.HierarchicalObject> Parents
			{
				get
				{
					if (this.parents == null)
					{
						this.parents = new List<TableAdapterManagerHelper.HierarchicalObject>();
					}
					return this.parents;
				}
			}

			// Token: 0x1700095A RID: 2394
			// (get) Token: 0x06002C43 RID: 11331 RVA: 0x00107324 File Offset: 0x00105524
			internal bool HasParent
			{
				get
				{
					return this.parents != null && this.parents.Count > 0;
				}
			}

			// Token: 0x06002C44 RID: 11332 RVA: 0x0010733E File Offset: 0x0010553E
			internal HierarchicalObject(object theObject)
			{
				this.TheObject = theObject;
			}

			// Token: 0x06002C45 RID: 11333 RVA: 0x0010734D File Offset: 0x0010554D
			internal void AddUniqueParent(TableAdapterManagerHelper.HierarchicalObject parent)
			{
				if (!this.Parents.Contains(parent))
				{
					this.Parents.Add(parent);
				}
			}

			// Token: 0x06002C46 RID: 11334 RVA: 0x0010736C File Offset: 0x0010556C
			internal void CheckParents()
			{
				if (this.HasParent)
				{
					Stack<TableAdapterManagerHelper.HierarchicalObject> stack = new Stack<TableAdapterManagerHelper.HierarchicalObject>();
					Stack<TableAdapterManagerHelper.HierarchicalObject> stack2 = new Stack<TableAdapterManagerHelper.HierarchicalObject>();
					stack2.Push(this);
					stack.Push(this);
					this.CheckParents(stack2, stack);
				}
			}

			// Token: 0x06002C47 RID: 11335 RVA: 0x001073A4 File Offset: 0x001055A4
			internal void CheckParents(Stack<TableAdapterManagerHelper.HierarchicalObject> work, Stack<TableAdapterManagerHelper.HierarchicalObject> path)
			{
				if (!this.HasParent || (this != path.Peek() && path.Contains(this)))
				{
					TableAdapterManagerHelper.HierarchicalObject hierarchicalObject = path.Pop();
					TableAdapterManagerHelper.HierarchicalObject hierarchicalObject2 = work.Pop();
					while (work.Count > 0 && path.Count > 0 && hierarchicalObject == hierarchicalObject2)
					{
						hierarchicalObject = path.Pop();
						hierarchicalObject2 = work.Pop();
					}
					if (hierarchicalObject2 != hierarchicalObject)
					{
						path.Push(hierarchicalObject2);
						hierarchicalObject2.CheckParents(work, path);
					}
					return;
				}
				if (this.HasParent)
				{
					TableAdapterManagerHelper.HierarchicalObject hierarchicalObject3 = null;
					for (int i = this.Parents.Count - 1; i >= 0; i--)
					{
						TableAdapterManagerHelper.HierarchicalObject hierarchicalObject4 = this.Parents[i];
						if (!path.Contains(hierarchicalObject4) && hierarchicalObject4.Height <= this.Height)
						{
							hierarchicalObject4.Height = this.Height + 1;
							if (hierarchicalObject4.Height > 1000)
							{
								return;
							}
							work.Push(hierarchicalObject4);
							hierarchicalObject3 = hierarchicalObject4;
						}
					}
					if (hierarchicalObject3 != null)
					{
						path.Push(hierarchicalObject3);
						hierarchicalObject3.CheckParents(work, path);
					}
				}
			}

			// Token: 0x06002C48 RID: 11336 RVA: 0x00107497 File Offset: 0x00105697
			int IComparable<TableAdapterManagerHelper.HierarchicalObject>.CompareTo(TableAdapterManagerHelper.HierarchicalObject other)
			{
				return other.Height - this.Height;
			}

			// Token: 0x04001EB6 RID: 7862
			internal int Height;

			// Token: 0x04001EB7 RID: 7863
			internal object TheObject;

			// Token: 0x04001EB8 RID: 7864
			private List<TableAdapterManagerHelper.HierarchicalObject> parents;
		}
	}
}
