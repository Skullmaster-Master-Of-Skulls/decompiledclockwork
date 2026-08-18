using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.InputManager
{
	// Token: 0x0200190D RID: 6413
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(typeof(TargetInput))]
	public class TargetControlCollection : CollectionBase
	{
		// Token: 0x0600F8D9 RID: 63705 RVA: 0x00383168 File Offset: 0x00381368
		public TargetInput FindTargetInputById(string id)
		{
			foreach (object obj in base.List)
			{
				TargetInput targetInput = (TargetInput)obj;
				if (targetInput.ControlID == id)
				{
					return targetInput;
				}
			}
			return null;
		}

		// Token: 0x0600F8DA RID: 63706 RVA: 0x003831D0 File Offset: 0x003813D0
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x17004B36 RID: 19254
		public TargetInput this[int index]
		{
			get
			{
				return base.List[index] as TargetInput;
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600F8DD RID: 63709 RVA: 0x003831F9 File Offset: 0x003813F9
		public int Add(TargetInput targetControl)
		{
			return base.List.Add(targetControl);
		}

		// Token: 0x0600F8DE RID: 63710 RVA: 0x00383207 File Offset: 0x00381407
		public void Remove(TargetInput targetControl)
		{
			base.List.Remove(targetControl);
		}

		// Token: 0x0600F8DF RID: 63711 RVA: 0x00383215 File Offset: 0x00381415
		public bool Contains(TargetInput targetControl)
		{
			return base.List.Contains(targetControl);
		}

		// Token: 0x0600F8E0 RID: 63712 RVA: 0x00383223 File Offset: 0x00381423
		public int IndexOf(TargetInput targetControl)
		{
			return base.List.IndexOf(targetControl);
		}

		// Token: 0x0600F8E1 RID: 63713 RVA: 0x00383231 File Offset: 0x00381431
		public void Insert(int index, TargetInput targetControl)
		{
			base.List.Insert(index, targetControl);
		}
	}
}
