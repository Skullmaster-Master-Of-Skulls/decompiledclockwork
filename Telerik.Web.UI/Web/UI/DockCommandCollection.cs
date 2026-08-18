using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000FAC RID: 4012
	public class DockCommandCollection : List<DockCommand>
	{
		// Token: 0x06009A0A RID: 39434 RVA: 0x00225AB4 File Offset: 0x00223CB4
		internal DockCommandCollection(RadDock dock)
		{
			this._dock = dock;
		}

		// Token: 0x06009A0B RID: 39435 RVA: 0x00225AC3 File Offset: 0x00223CC3
		public new virtual void Add(DockCommand command)
		{
			command.RadDock = this._dock;
			base.Add(command);
		}

		// Token: 0x06009A0C RID: 39436 RVA: 0x00225AD8 File Offset: 0x00223CD8
		public new virtual void Insert(int index, DockCommand command)
		{
			command.RadDock = this._dock;
			base.Insert(index, command);
		}

		// Token: 0x06009A0D RID: 39437 RVA: 0x00225AF0 File Offset: 0x00223CF0
		internal string Serialize(JavaScriptSerializer serializer)
		{
			ArrayList arrayList = new ArrayList();
			foreach (DockCommand dockCommand in this)
			{
				arrayList.Add(dockCommand.GetProperties());
			}
			return serializer.Serialize(arrayList);
		}

		// Token: 0x04002BB8 RID: 11192
		private readonly RadDock _dock;
	}
}
